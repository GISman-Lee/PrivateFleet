using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using log4net;
using AccessControlUnit;
using System.Data;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using System.Text;
using System.Configuration;


/// <summary>
/// Summary description for SyncDataFromCRM
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SyncDataFromCRM : System.Web.Services.WebService
{

    public SyncDataFromCRM()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Global Variables
    ILog logger = LogManager.GetLogger(typeof(SyncDataFromCRM));
    #endregion

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]



    /* Whenever Changes Done To Dealer In Crm Also Do Related Changes To Same Dealer And User*/
    /// <summary>
    /// Created By :Ayyaj Mujawar
    /// Created Date: 25 Oct 2013
    /// Description: Synchronized Data (Dealer) From CRM
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="CreatedDateTime"></param>
    /// <param name="MakeId"></param>
    /// <param name="CityId"></param>
    /// <param name="phone"></param>
    /// <param name="mobile"></param>
    /// <param name="Email"></param>
    /// <param name="PostalCode"></param>
    /// <param name="IsActive"></param>
    /// <param name="StateId"></param>
    /// <returns></returns>
    //[WebMethod]
    public Int64 SaveDealerFromCRM(string Name, string key, string company, string address, Int16 MakeId, Int32 CityId, string phone, string mobile, string Email, string fax, string PostalCode, Int32 StateId, Int32 SecStateId, Boolean IsActive, Boolean IsHotDealer, Boolean IsColdDealer, Boolean IsNew)
    {
        DbCommand objCmd = null;
        DbCommand objCmd1 = null;
        Int64 Result = 0;
        //Int64 Result1 = 0;//For Check Update Procedure
        //Int64 UserId = 0;
        DbConnection objConn = null;
        DbTransaction objTran = null;


        try
        {
            objConn = Cls_DataAccess.getInstance().GetConnection();
            objConn.Open();

            if (!string.IsNullOrEmpty(Name.Trim()) && !string.IsNullOrEmpty(key.Trim()))
            {
                objTran = objConn.BeginTransaction();
                //System.Web.DataAccess obj = new DataAccess();

                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_AddUpdateDealerFromCRM");
                //Cls_DataAccess.getInstance().AddInParameter(objCmd, "Id", DbType.Int64, objEmp.DesigId);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "Name", DbType.String, Name);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "Key", DbType.String, key);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "Company", DbType.String, company);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "Phone", DbType.String, phone);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "Mobile", DbType.String, mobile);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "Email", DbType.String, Email);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "Fax", DbType.String, fax);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "City_1", DbType.Int64, CityId);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "StateId", DbType.String, StateId.ToString());
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "SecondaryStateId", DbType.String, SecStateId.ToString());
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "PCode", DbType.String, PostalCode);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "IsActive", DbType.Boolean, IsActive);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "IsHotDealer", DbType.Boolean, IsHotDealer);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "IsColdDealer", DbType.Boolean, IsColdDealer);
                //Cls_DataAccess.getInstance().AddInParameter(objCmd, "CreatedDate", DbType.DateTime, CreatedDateTime);

                Result = Convert.ToInt64(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, objTran));

                if (Result > 0 && !string.IsNullOrEmpty(Name.Trim()))
                {
                    /*--To Get Dealer Id From KEY--*/
                    objCmd1 = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetDealerIDByKey");
                    Cls_DataAccess.getInstance().AddInParameter(objCmd1, "Key", DbType.String, key);
                    DataTable DtResult1 = Cls_DataAccess.getInstance().GetDataTable(objCmd1, objTran);


                    string password = string.Empty;
                    Int64 DealerID =Convert.ToInt64(DtResult1.Rows[0]["ID"]);
                    objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_AddUpdateUserFromCRM");
                    Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int64, DealerID);
                    Cls_DataAccess.getInstance().AddInParameter(objCmd, "Name", DbType.String, Name);
                    Cls_DataAccess.getInstance().AddInParameter(objCmd, "Email", DbType.String, Email);
                    Cls_DataAccess.getInstance().AddInParameter(objCmd, "Address", DbType.String, address);
                    Cls_DataAccess.getInstance().AddInParameter(objCmd, "Phone", DbType.String, phone);
                    Cls_DataAccess.getInstance().AddInParameter(objCmd, "Mobile", DbType.String, mobile);
                    Cls_DataAccess.getInstance().AddInParameter(objCmd, "IsActive", DbType.Boolean, IsActive);
                    DataTable dt = Cls_DataAccess.getInstance().GetDataTable(objCmd,objTran);
                    //Result = Convert.ToInt64(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, objTran));

                    objCmd1 = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetMakeByID");
                    Cls_DataAccess.getInstance().AddInParameter(objCmd1, "MakeID", DbType.Int32, MakeId);
                    DataTable DtResult = Cls_DataAccess.getInstance().GetDataTable(objCmd1, objTran);

                    
                    password = Convert.ToString(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, objTran));

                    if ( !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(Convert.ToString(MakeId)) && DealerID > 0)
                    {
                        objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_DealerMakeMappingFromCRM");
                        Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int64, DealerID);
                        Cls_DataAccess.getInstance().AddInParameter(objCmd, "MakeId", DbType.Int64, MakeId);
                        Cls_DataAccess.getInstance().AddInParameter(objCmd, "IsActive", DbType.Boolean, IsActive);
                        Result = Convert.ToInt64(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, objTran));
                        if (Result > 0)
                        {
                            if (IsNew == true)
                            {
                                sendMail(Name, Email, dt.Rows[0][0].ToString().Trim(), DtResult.Rows[0]["Make"].ToString().Trim());
                                
                            }                            

                            objTran.Commit();
                            return Result;
                        }
                        else
                        {
                            objTran.Rollback();
                            return Result;
                        }
                    }
                    else
                    {
                        objTran.Rollback();
                        return Result;
                    }

                }
                else
                {
                    objTran.Rollback();
                    return Result;
                }
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            objTran.Rollback();
            logger.Error(ex.Message + "AddLeadsToPFSales ,SaveDealerFromCRM.error" + ex.StackTrace);
            return 0;
        }
    }

    //Email send for Dealer Registration
    private void sendMail(string Name, string Email, string pass, string make)
    {
        string strEmailStatus=string.Empty;
        logger.Debug("Start Automatic user mail sending");
        try
        {
            StringBuilder str = new StringBuilder();
            Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();

            str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear " + Name + "<br /><br />Welcome to Private Fleet. Your registration to Private Fleet is successfully completed for the make " + make + ".<br />Use the following detail to login in Private Fleet.");
            str.Append("<br/><br/>User Name : " + Email + " <br/> Password : " + pass);

            string link = ConfigurationManager.AppSettings["DummyPageUrl1"];
            string EmailFrom = ConfigurationManager.AppSettings["EmailFromID"];
            str.Append("<br/><br/><a href='" + link + "'>Click here</a> to Log in.</p> ");

            objEmailHelper.EmailBody = str.ToString();

            objEmailHelper.EmailToID = Email;
            objEmailHelper.EmailFromID = EmailFrom;
            // objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
            logger.Debug("Mail TO - " + Email);
            logger.Debug("Mail From -" + EmailFrom);

            objEmailHelper.EmailSubject = "Welcome to Private Fleet";

            if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
              strEmailStatus= objEmailHelper.SendEmail();
            Cls_UserMaster objEmailDetails = new Cls_UserMaster();
            //SyncDataFromQuoteToCRM.SyncDataFromQuoteSoapClient objEmailDetails = new SyncDataFromQuoteToCRM.SyncDataFromQuoteSoapClient();
            objEmailDetails.SaveSendEmailDetailsFromQuote(Email, EmailFrom, str.ToString(), strEmailStatus, "Welcome to Private Fleet", "Web Service:SyncDataFromQuoteToCRM");

        }
        catch (Exception ex)
        {
            logger.Debug("Error " + ex.Message);
        }
        finally
        {
            logger.Debug("ends Automatic user mail sending");
        }

    }



}

