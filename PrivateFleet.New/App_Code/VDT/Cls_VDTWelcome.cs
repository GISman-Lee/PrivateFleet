using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mechsoft.GeneralUtilities;
using System.Data.Common;
using log4net;

/// <summary>
/// Summary description for Cls_VDTWelcome
/// </summary>
public class Cls_VDTWelcome
{
    ILog logger = LogManager.GetLogger(typeof(Cls_VDTWelcome));
    Cls_GenericEmailHelper objCls_GenericEmailHelper = new Cls_GenericEmailHelper();
    Cls_Admin objCls_Admin = new Cls_Admin();

    public string username { get; set; }
    public string password { get; set; }
    public int id { get; set; }
    public string description { get; set; }
    public Int32 DealerID { get; set; }
    public string RequestToID { get; set; }
    public int CustomerID { get; set; }
    public string Message { get; set; }

    //on 10 Jul 12
    public DateTime MailSendDate { get; set; }
    public string mode { get; set; }
    public Int64 IsMailSend { get; set; }
    public string clientComment { get; set; }

    public Cls_VDTWelcome()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet getVDT_DealerStatusByCustomerid()
    {//get information from Dealer Stauts by cusotmer id
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DbCommand objCmd = null;
        try
        {

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_VDT_GetDealerStausByCustomer");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "id", DbType.Int32, id);
            ds = Cls_DataAccess.getInstance().GetDataSet(objCmd);

        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
        objCmd = null;
        return ds;
    }

    public void SendUpdateRequestTODealer(DataTable dt)
    {
        try
        {
            string message;
            string dealername = "";
            string clientname = "";
            string clientAddress = "";
            string make = "";
            string model = "";
            DateTime eta;
            string emailid = "";
            string strCustomerSerRespName = "";

            strCustomerSerRespName = Convert.ToString(dt.Rows[0]["CustomerSerRep"]);
            dealername = Convert.ToString(dt.Rows[0]["name"]);
            clientname = Convert.ToString(dt.Rows[0]["firstname"]) + " " + Convert.ToString(dt.Rows[0]["lastname"]);
            clientAddress = "";
            make = Convert.ToString(dt.Rows[0]["make"]);
            model = Convert.ToString(dt.Rows[0]["model"]);
            eta = Convert.ToDateTime(Convert.ToString(dt.Rows[dt.Rows.Count - 1]["ETA"]));


            emailid = Convert.ToString(dt.Rows[0]["email"]);
            //emailid = "archana.jagtap@mechsoftgroup.com";

            message = "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + dealername + ",</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Our mutual client, " + clientname + " has just requested an update as to the delivery of their " + make + " " + model + ".  Currently the ETA is " + eta.ToString("dd/MM/yyyy") + " so if you can please log on at <a href='http://quotes.privatefleet.com.au' target='_blank'>quotes.privatefleet.com.au</a>, click the Delivery Updates tab on the LHS and respond that would be great.  Even if there is no change since the last update, it would be greatly appreciated if you could still respond by logging in and clicking the button.";

            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Client Comment - <br/>" + clientComment + "</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Thanks very much.</p>";

            if (strCustomerSerRespName.Trim().ToUpper() == "RACHAEL RODGER" || strCustomerSerRespName.Trim().ToUpper() == "STACEY BREWER" || strCustomerSerRespName.Trim().ToUpper() == "CHARMAINE AXIAK" || strCustomerSerRespName.Trim().ToUpper() == "ELLIE GOODCHILD" || strCustomerSerRespName.Trim().ToUpper() == "JESSICA VAN WYK" || strCustomerSerRespName.Trim().ToUpper() == "LAURA ASHLEY")
            {
                message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>" + strCustomerSerRespName.Trim() + "<br>Delivery Consultant<br>Private Fleet<br>Lvl 2, 845 Pacific Hwy<br>Chatswood NSW 2067<br>1300 303 181</p>";
            }
            else if (strCustomerSerRespName.Trim().ToUpper() == "ANNA MEARS")
            {
                message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>" + strCustomerSerRespName.Trim() + "<br>Senior Delivery Consultant<br>Private Fleet<br>Lvl 2, 845 Pacific Hwy<br>Chatswood NSW 2067<br>1300 303 181</p>";
            }
            else
            {
                message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>" + strCustomerSerRespName.Trim() + "<br>Customer Service Manager<br>Private Fleet<br>Lvl 2, 845 Pacific Hwy<br>Chatswood NSW 2067<br>1300 303 181</p>";
            }
            Message = message;
            objCls_GenericEmailHelper.EmailSubject = "Customer Request to dealer for ETA update";
            objCls_GenericEmailHelper.EmailFromID = "Private Fleet<" + Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]) + ">";
            objCls_GenericEmailHelper.EmailToID = emailid;
            objCls_GenericEmailHelper.EmailBccID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
            //  objCls_GenericEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";

            objCls_GenericEmailHelper.EmailBody = message;
            objCls_GenericEmailHelper.SendEmail();
            description = message;

        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }


    }

    public void SendRequestTOPFforHELP(DataTable dt)
    {
        try
        {
            string message;
            string dealername = "";
            string clientname = "";
            string clientAddress = "";
            string make = "";
            string model = "";
            DateTime eta;
            string emailid = "";



            clientname = Convert.ToString(dt.Rows[0]["firstname"]) + " " + Convert.ToString(dt.Rows[0]["lastname"]);
            clientAddress = "";
            make = Convert.ToString(dt.Rows[0]["make"]);
            model = Convert.ToString(dt.Rows[0]["model"]);

            message = "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear ,</p>";//Remove Catherine,By :Ayyaj On 13 Sept 2014
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>" + clientname + " has just requested assistance with the delivery of their new " + make + " " + model + ".</p>";
            if (description != "")
            {
                message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Customer Comment </br>" + description + " </p>";
            }
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Thanks</br>Quotacon</p>";

            //message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Quotacon</p>";

            objCls_GenericEmailHelper.EmailSubject = "Customer request to PF for HELP";
            //objCls_GenericEmailHelper.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
            // objCls_GenericEmailHelper.EmailToID = emailid;


            //Adding admin email address to send email
            DataTable dt1 = new DataTable();
            dt1 = objCls_Admin.GetAllAdminDT();
            if (dt1 != null)
            {


                foreach (DataRow drow in dt1.Rows)
                {
                    if (Convert.ToString(ConfigurationManager.AppSettings["EmailToAdmin"]).ToLower() == "single")
                    {
                        objCls_GenericEmailHelper.EmailToID = "admin<" + Convert.ToString(drow["email"]) + ">";

                        //Sending Email to Cathrine as well. 
                        //Commented By:Ayyaj On:15 Sept 2014 to replace Catherine
                        //ConfigValues objConfigue = new ConfigValues();
                        //objConfigue.Key = "EMAIL_TO_ADMIN_DRASTIC_CHANGE_IN_ETA";
                        objCls_GenericEmailHelper.EmailToID = objCls_GenericEmailHelper.EmailToID;// +"; " + objConfigue.GetValue(objConfigue.Key).ToString();
                        break;
                    }
                    else
                    {
                        if (Convert.ToString(objCls_GenericEmailHelper.EmailToID) == null)
                        {
                            objCls_GenericEmailHelper.EmailToID = "admin<" + Convert.ToString(drow["email"]) + ">";
                        }
                        else
                        {
                            if (Convert.ToString(objCls_GenericEmailHelper.EmailCcID) == null)
                            {
                                objCls_GenericEmailHelper.EmailCcID = Convert.ToString(drow["email"]);
                            }
                            else
                            {
                                objCls_GenericEmailHelper.EmailCcID = objCls_GenericEmailHelper.EmailCcID + "," + Convert.ToString(drow["email"]);
                            }
                        }
                    }
                }

            }

            //Adding admin email address to send email ENd

            Message = message;
            /*Commented On: 10 Sept 2014, By: Ayyaj, Desc:Catherine To Mark*/
            //objCls_GenericEmailHelper.EmailFromID = "admin<" + Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]) + ">";
            objCls_GenericEmailHelper.EmailFromID = "admin<" + Convert.ToString(ConfigurationManager.AppSettings["SecEmailFromID"]) + ">";
            // objCls_GenericEmailHelper.EmailToID = "sachin.pujari@mechsoftgroup.com";
            objCls_GenericEmailHelper.EmailBody = message;
            objCls_GenericEmailHelper.SendEmail();
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void Save_VDTCustomerRequestDetails()
    {
        DbCommand objComd = null;

        try
        {

            objComd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Save_VDTCustomerRequestDetails");
            Cls_DataAccess.getInstance().AddInParameter(objComd, "CustomerID", DbType.Int32, CustomerID);
            Cls_DataAccess.getInstance().AddInParameter(objComd, "DealerID", DbType.Int32, DealerID);
            Cls_DataAccess.getInstance().AddInParameter(objComd, "Description", DbType.String, Message);
            Cls_DataAccess.getInstance().AddInParameter(objComd, "RequestToID", DbType.String, RequestToID);
            if (!String.IsNullOrEmpty(mode) && mode.ToLower().Equals("drastic"))// use null at SP side if not drastic
            {
                Cls_DataAccess.getInstance().AddInParameter(objComd, "MailSendDate", DbType.DateTime, MailSendDate);
                Cls_DataAccess.getInstance().AddInParameter(objComd, "IsMailSend", DbType.Int64, IsMailSend);
            }
            Cls_DataAccess.getInstance().ExecuteNonQuery(objComd, null);

        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    // on 10 jul 2012 delay drastic mail


    public void updateDrasticMailSend(Int64 ID)
    {
        DbCommand objComd = null;
        try
        {

            objComd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SPupdateDrasticMailSend");
            Cls_DataAccess.getInstance().AddInParameter(objComd, "@Id", DbType.Int64, ID);
            Cls_DataAccess.getInstance().ExecuteNonQuery(objComd, null);

        }
        catch (Exception ex)
        {
            logger.Error("updateDrasticMailSend err - " + ex.Message);
        }
    }

    public DataTable AutomaticDrasticMailSend()
    {
        DataTable dt = new DataTable();
        DbCommand objCmd = null;
        try
        {

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAutomaticDrasticMailSend");
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);

        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
        objCmd = null;
        return dt;
    }
    //end
}
