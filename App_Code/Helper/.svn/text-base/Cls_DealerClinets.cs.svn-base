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
using System.Data.SqlClient;
using Mechsoft.FleetDeal;
using log4net;
using System.Text;
using System.Collections;

/// <summary>
/// Summary description for Cls_DealerClinets
/// </summary>
public class Cls_DealerClinets
{
    Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
    Cls_VDTWelcome objCls_VDTWelcome = new Cls_VDTWelcome();
    Cls_Admin objCls_Admin = new Cls_Admin();

    public int CustomerID { get; set; }
    public int StatusID { get; set; }
    public DateTime ETA { get; set; }
    public string DealerNotes { get; set; }
    public int DealerID { get; set; }
    public string ModifiedDate { get; set; }
    public string Email { get; set; }
    public int flag { get; set; }
    public Int64 UpdatedBy { get; set; }
    public Int64 OrderCancelledBy { get; set; }

    //on 23 Jun 2012
    public string StockNo { get; set; }
    public string BuildYear { get; set; }
    public string ComplianceYear { get; set; }
    public Int64 ChangeBy { get; set; }
    //end

    public Cls_DealerClinets()
    {  //declare and initialize logger object
        ILog logger = LogManager.GetLogger(typeof(Cls_Dealer));

        //
        // TODO: Add constructor logic here
        //


    }

    public DataSet getDealerClinetsListOnly()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_getClinetsByDealer");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "email", DbType.String, Email);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "flag", DbType.Int32, flag);
            if (Email.ToLower() == "admin")
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "@CustomerID", DbType.Int32, CustomerID);
            return Cls_DataAccess.getInstance().GetDataSet(objCmd);
        }
        catch (Exception ex)
        {
            //  logger.Error("GetAllDealers Function :" + ex.Message);
            return null;
        }
    }

    public DataTable getDealerClinets(string username)
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.Text, "select *,FirstName +' '+lastname as Name from tbl_VDT_CustomerMaster");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            //  logger.Error("GetAllDealers Function :" + ex.Message);
            return null;
        }
    }

    public DataTable getVDTStatus()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_getVDTStausMaster");
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            //  logger.Error("GetAllDealers Function :" + ex.Message);
            return null;
        }
    }

    public int Save_VDTDealerStatus()
    {
        int result = 0;
        try
        {

            DbCommand objCmd = null;
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_SaveVDTDealerUpdateStatus");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "CustomerID", DbType.Int32, CustomerID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "StatusID", DbType.Int32, StatusID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ETA", DbType.DateTime, Convert.ToDateTime(ETA));
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerNotes", DbType.String, DealerNotes);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int32, DealerID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "UpdatedBy", DbType.Int32, UpdatedBy);

            DataTable dt = new DataTable();
            // result = Convert.ToInt32(Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd, null));

            object obj = Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null);
            Int32.TryParse(obj.ToString(), out result);


        }
        catch (Exception ex1)
        {

        }
        return result;
    }

    public void SendETA_DrasticChange_Email(DataTable dt, int drasticID)
    {
        try
        {
            string message = "";
            string dealername = "";
            string make = "";
            string model = "";
            DateTime LastETA;
            DateTime CurrentETA;
            string customername = "";
            ConfigValues objConfigue = new ConfigValues();

            make = Convert.ToString(dt.Rows[0]["make"]);
            model = Convert.ToString(dt.Rows[0]["model"]);
            dealername = Convert.ToString(dt.Rows[0]["name"]);

            LastETA = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 2]["ETA"]);
            CurrentETA = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["ETA"]);
            customername = Convert.ToString(dt.Rows[0]["FullName"]);

            message = "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear Catherine,</p>";

            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Just to let you know, " + dealername + " has just updated " + customername + " on the delivery of their new " + make + " " + model + " and there has been a significant change in the ETA.  The prior delivery date was " + LastETA.ToString("dd/MM/yyyy") + " which has just been updated to " + CurrentETA.ToString("dd/MM/yyy") + "</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>This is just for your info.</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Thanks</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Quotacon</p>";

            clsemail.EmailBody = message;

            #region Adding admin email address to send email
            //DataTable dt1 = new DataTable();
            //dt1 = objCls_Admin.GetAllAdminDT();
            //if (dt1 != null)
            //{
            //    foreach (DataRow drow in dt1.Rows)
            //    {
            //        if (Convert.ToString(ConfigurationManager.AppSettings["EmailToAdmin"]).ToLower() == "single")
            //        {
            //            clsemail.EmailToID = "Admin<" + Convert.ToString(drow["email"]) + ">";

            //            objConfigue.Key = "EMAIL_TO_ADMIN_DRASTIC_CHANGE_IN_ETA";
            //            clsemail.EmailToID = clsemail.EmailToID + "; " + objConfigue.GetValue(objConfigue.Key).ToString();
            //            break;
            //        }
            //        else
            //        {
            //            if (Convert.ToString(clsemail.EmailToID) == null)
            //            {
            //                clsemail.EmailToID = Convert.ToString(drow["email"]);
            //            }
            //            else
            //            {
            //                if (Convert.ToString(clsemail.EmailCcID) == null)
            //                {
            //                    clsemail.EmailCcID = Convert.ToString(drow["email"]);
            //                }
            //                else
            //                {
            //                    clsemail.EmailCcID = clsemail.EmailCcID + ";" + Convert.ToString(drow["email"]);
            //                }
            //            }

            //        }
            //    }
            //}
            #endregion

            if (dt.Rows[0]["CustSerRep"] != null && !Convert.ToString(dt.Rows[0]["CustSerRep"]).Equals(String.Empty))
            {
                clsemail.EmailToID = Convert.ToString(dt.Rows[0]["CustSerRep"]);
                objConfigue.Key = "EMAIL_TO_ADMIN_DRASTIC_CHANGE_IN_ETA";
                clsemail.EmailCcID = objConfigue.GetValue(objConfigue.Key).ToString();
            }
            else
                clsemail.EmailToID = objConfigue.GetValue(objConfigue.Key).ToString();


            //Calculation Dealy Hours
            int Delayhours = 0, DayStartHour = 0, DayEndHour = 0;

            objConfigue.Key = "DAY_WORKING_START_HOURS";
            DayStartHour = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

            objConfigue.Key = "DAY_WORKING_END_HOURS";
            DayEndHour = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

            objConfigue.Key = "DREASTIC_EMAIL_SEND_DELAY_HOURS";
            Delayhours = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());


            TimeSpan ts = new TimeSpan(Delayhours, 0, 2);
            DateTime dtCurrent = System.DateTime.Now;
            DateTime dtNew;
            if (dtCurrent.Hour < DayEndHour)
            {
                dtNew = dtCurrent.Add(ts);
                if (dtNew.Hour > DayEndHour)
                {
                    int diff = dtNew.Hour - DayEndHour;
                    dtNew = System.DateTime.Now.AddDays(1).Date;
                    dtNew = dtNew.Add(new TimeSpan(DayStartHour + diff, 0, 2));
                }
            }
            else
            {
                dtNew = System.DateTime.Now.AddDays(1).Date;
                dtNew = dtNew.Add(new TimeSpan(DayStartHour + Delayhours, 0, 0));
            }
            //End delay hours

            //Adding admin email address to send email ENd
            // string strEmailToID = Convert.ToString(Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]));
            clsemail.EmailFromID = "Private Fleet<" + Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]) + ">";
            //clsemail.EmailToID = Convert.ToString("archana.jagtap@mechsoftgroup.com");

            clsemail.EmailSubject = "Drastic change in ETA.";
            clsemail.SendEmail();

            objCls_VDTWelcome.CustomerID = Convert.ToInt32(dt.Rows[0]["id"]);
            objCls_VDTWelcome.DealerID = Convert.ToInt32(dt.Rows[0]["dealerID"]);
            objCls_VDTWelcome.description = message;
            objCls_VDTWelcome.RequestToID = "DrasticAdmin";
            objCls_VDTWelcome.MailSendDate = dtNew;
            objCls_VDTWelcome.IsMailSend = drasticID;
            objCls_VDTWelcome.mode = "drastic";
            objCls_VDTWelcome.Save_VDTCustomerRequestDetails();
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// Created By : Archana : Om 19 April 2012
    /// Detaiols : Cancelling customer Order.
    /// </summary>
    /// <returns></returns>
    public bool CancelOrder()
    {
        try
        {
            DbCommand objCmd = null;
            Int32 result = 0;
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_CancelOrder");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "CustomerID", DbType.Int32, CustomerID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "OrderCancelledBy", DbType.Int64, OrderCancelledBy);

            result = Convert.ToInt32(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null));
            return result > 0 ? true : false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public int SaveVDTDealerInput()
    {
        int result = 0;
        try
        {

            DbCommand objCmd = null;
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SPSaveVDTDealerInput");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "StockNo", DbType.String, StockNo);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "BuildYear", DbType.String, BuildYear);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ComplianceYear", DbType.String, ComplianceYear);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ChangeBy", DbType.Int64, ChangeBy);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "CustomerID", DbType.Int32, CustomerID);

            result = Convert.ToInt32(Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd, null));


        }
        catch (Exception ex1)
        {

        }
        return result;
    }

    /// <summary>
    /// Manoj - 9 jan 2013 - Get last three update status. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public DataTable chkRepeatStatus()
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SPGetLast3StatusofVDT");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@CustomerID", DbType.Int32, CustomerID);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd, null);
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
        }
    }

}
