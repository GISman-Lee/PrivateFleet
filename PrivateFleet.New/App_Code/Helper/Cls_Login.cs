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
/// Summary description for Cls_Login
/// </summary>
public class Cls_Login
{
    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();

    ILog logger = LogManager.GetLogger(typeof(Cls_Login));
    public Cls_Login()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private String _UserName;

    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }


    private string _Password;

    public string Password
    {
        get { return _Password; }
        set { _Password = value; }
    }

    private string _FEmail;
    public string FEmail
    {
        get { return _FEmail; }
        set { _FEmail = value; }
    }

    private string _ID;
    public string ID
    {
        get { return _ID; }
        set { _ID = value; }
    }

    public int AdminID { get; set; }
    public int CustomerID { get; set; }
    public string DealerID { get; set; }
    public string ReplySubject { get; set; }
    public string Description { get; set; }
    public int Dealer_Non_Response_LowerLimit { get; set; }
    public int Dealer_Non_Response_UpperLimit { get; set; }
    public int Flag { get; set; }


    /// <summary>
    /// manoj 5 Jan 2011
    /// Used to check mail id entered by user in forgot password functionality.
    /// </summary>
    public DataTable chkEmail(int MakeID, int Role)
    {
        DbCommand objCmd = null;
        objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "SpForgotPass");
        DataAccess.AddInParameter(objCmd, "Email", DbType.String, FEmail.Trim());
        DataAccess.AddInParameter(objCmd, "MakeID", DbType.Int16, MakeID);
        DataAccess.AddInParameter(objCmd, "Role", DbType.Int32, Role);
        return DataAccess.GetDataTable(objCmd);
    }

    /// <summary>
    /// To update the password of user.
    /// </summary>

    public bool updatePassword()
    {
        DbCommand objCmd = null;
        int result = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spUpdatePassword");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "id", DbType.Int32, ID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Password", DbType.String, Password);

            //Execute command
            result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public DataTable ValidateLogIn()
    {
        DbCommand objCmd = null;

        objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "SpLogin");
        setParameters(objCmd);

        return DataAccess.GetDataTable(objCmd);
    }

    private void setParameters(DbCommand objCmd)
    {
        DataAccess.AddInParameter(objCmd, "UserName", DbType.String, UserName);
        DataAccess.AddInParameter(objCmd, "Password", DbType.String, Password);
    }

    public DataTable GetAllActiveRolesForLogin()
    {
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_acu_getAllRoles");

            //Execute command
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch
        { throw; }
        finally
        { objCmd = null; }

    }

    // new code added on 11/01/2012
    public DataTable GetDealerRespone()
    {
        DataTable dt = new DataTable();
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "sp_GetDealerResponse");
            DataAccess.AddInParameter(objCmd, "emailid", DbType.String, UserName);
            DataAccess.AddInParameter(objCmd, "flag", DbType.Int32, Flag);
            dt = DataAccess.GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
        return dt;
    }

    public void sendMail_FirstResponse(DataTable dt, String DealerIDs)
    {
        int cnt = 0;
        try
        {
            DealerID = DealerIDs;
            Cls_GenericEmailHelper objCls_GenericEmailHelper = new Cls_GenericEmailHelper();

            objCls_GenericEmailHelper.EmailSubject = "PF to Dealer for updating the ETA status ";
            string message = "";
            message = "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + Convert.ToString(dt.Rows[0]["name"]) + ",</p>";
            // message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>It has been " + Convert.ToString(Dealer_Non_Response_LowerLimit) + " days since the last update for the below clients and their make so if you could please log in at <a href='http://www.quotes.privatefleet.com.au' target='_blank'>quotes.privatefleet.com.au</a> and update the status that would be great.  Even if there has been no change, we do require a response as we have made a commitment to the client for at least weekly updates.</p>";
            message += "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>This is a reminder that the following updates are now overdue:</p>";
            // new added on 30 apr 2012
            message += "<br/><table cellspacing='0' border='1' style='width: 80%; border-collapse: collapse;font: normal normal normal 16px Calibri; color:#1F497D;'><tr style='font-weight: bold; height: 30px;' class='gvHeader'><th align='center' valign='middle' scope='col'>Client Name</th><th align='center' valign='middle' scope='col'>Make</th><th align='center' valign='middle' scope='col'>Model</th></tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                message += "<tr style='height: 30px;'><td align='center' valign='middle' style='width: 120px;' class='showPadding'>" + Convert.ToString(dt.Rows[i]["Firstname"]) + " " + Convert.ToString(dt.Rows[i]["LastName"]) + "</td><td align='left' valign='middle' style='width: 100px;' class='showPadding'>" + Convert.ToString(dt.Rows[i]["make"]) + "</td><td align='center' valign='middle' style='width: 90px;' class='showPadding'>" + Convert.ToString(dt.Rows[i]["model"]) + "</td></tr>";

                AdminID = 1;
                CustomerID = Convert.ToInt32(dt.Rows[i]["Customerid"]);
                //DealerID = Convert.ToString(dt.Rows[0]["allDealerids"]);
                ReplySubject = "PF to Dealer for updating the ETA status";
                Description = "";

                if (Convert.ToInt32(Save_VDT_Admin_Replay()) == 1)
                {
                    cnt++;
                    // objCls_GenericEmailHelper.SendEmail();
                }
            }
            message += "</table>";
            //end
            // message += "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>If you can please take a few minutes to log on to <a href='http://quotes.privatefleet.com.au' target='_blank'>quotes.privatefleet.com.au</a> and update the estimated delivery dates.";
            // message += "If there is a major issue or you are having problems with updating the system, please let us know on 02 9411 6777 or by email <a href='mailto:davidlye@privatefleet.com.au'>davidlye@privatefleet.com.au</a>";
            message += "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>If you can please take a few minutes to log on to <a href='http://quotes.privatefleet.com.au' target='_blank'>quotes.privatefleet.com.au</a> using the email address [" + Convert.ToString(dt.Rows[0]["DealerEmail"]) + "] and your password (which can be reset <a href='http://quotes.privatefleet.com.au' target='_blank'>here</a> if not known) and update the estimated delivery dates.";
            message += "<br/><br/>If there is a major issue or you are experiencing technical problems with updating the system, please let us know on 02 9411 6777 or by email <a href='mailto:davidlye@privatefleet.com.au'>davidlye@privatefleet.com.au</a> <b>(note: please do not send updates to this email address)</b>";
            message += "<br/><br/>Thanks very much";
            message += "<br/><br/>Private Fleet";
            message += "<br/><a href='http://www.privatefleet.com.au' target='_blank'>www.privatefleet.com.au</a>";

            //on 30 may 2012
            message += "<br/><br/>PS: Your login details are [" + Convert.ToString(dt.Rows[0]["DealerEmail"]) + "] and your regular password. If you aren’t sure what your password is, you can reset it here <a href='http://quotes.privatefleet.com.au/' target='_blank'>quotes.privatefleet.com.au</a></p>";

            //message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>If there are any problems please don’t hestitate to get in touch with me directly.</p>";
            //message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Many thanks</br>" + "Sophie Harris" + "<br />Customer Service<br />Private Fleet</br>Lvl 2, 845 Pacific Hwy</br>Chatswood NSW 2067</br>1300 303 181 ext 205</p>";

            ConfigValues objConfigue = new ConfigValues();

            objCls_GenericEmailHelper.EmailBody = message;
            objCls_GenericEmailHelper.EmailFromID = "Private Fleet<" + Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]) + ">";
            objCls_GenericEmailHelper.EmailToID = Convert.ToString(dt.Rows[0]["DealerEmail"]);
            //objCls_GenericEmailHelper.EmailBccID = "manoj.mahagaonkar@mechsoftgroup.com";

            //AdminID = 1;
            //CustomerID = Convert.ToInt32(dt.Rows[0]["Customerid"]);
            ////DealerID = Convert.ToString(dt.Rows[0]["allDealerids"]);
            //ReplySubject = "PF to Dealer for updating the ETA status";
            //Description = message;

            if (cnt == dt.Rows.Count)
            {
                objCls_GenericEmailHelper.SendEmail();
            }
        }
        catch (Exception ex)
        {
            logger.Error("sendMail_FirstResponse err - " + Convert.ToString(ex.Message));
        }

    }

    public void sendMail_SecondResponse(DataTable dt, String DealerIDs)
    {
        int cnt = 0;
        try
        {
            DealerID = DealerIDs;

            Cls_GenericEmailHelper objCls_GenericEmailHelper = new Cls_GenericEmailHelper();
            objCls_GenericEmailHelper.EmailToID = Convert.ToString(UserName);

            objCls_GenericEmailHelper.EmailSubject = "PF to Dealer on locking event";
            string message = "";
            message = "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + Convert.ToString(dt.Rows[0]["name"]) + ",</p>";
            //   message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Please update the delivery status for " + Convert.ToString(dt.Rows[0]["Firstname"]) + " " + Convert.ToString(dt.Rows[0]["LastName"]) + " and their new " + Convert.ToString(dt.Rows[0]["make"]) + " " + Convert.ToString(dt.Rows[0]["model"]) + " ASAP.  It has been more than " + Convert.ToString(Dealer_Non_Response_UpperLimit) + " days since the last update which is outside the commitment we have given to our mutual client.</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Please update the delivery status for the below clients and their new make ASAP.  It has been more than " + Convert.ToString(Dealer_Non_Response_UpperLimit) + " days since the last update which is outside the commitment we have given to our mutual client.</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>If you have updated the client directly, that’s great but please do log in at <a href='http://www.quotes.privatefleet.com.au' target='_blank'>quotes.privatefleet.com.au</a> and submit an update for the record.  If there are any issues, don’t hesitate to let me know.</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Unfortunately, if delivery updates are not received in a timely manner, we may be forced to limit quote requests for new business.</p>";
            //message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Many thanks</br>" + strCustometSerResp + "<br />Customer Service Manager<br />Private Fleet</br>Lvl 2, 845 Pacific Hwy</br>Chatswood NSW 2067</br>1300 303 181</p>";

            // new added on 30 apr 2012
            message += "<br/><table cellspacing='0' border='1' style='width: 80%; border-collapse: collapse; font: normal normal normal 16px Calibri; color:#1F497D;'><tr style='font-weight: bold; height: 30px;' class='gvHeader'><th align='center' valign='middle' scope='col'>Client Name</th><th align='center' valign='middle' scope='col'>Make</th><th align='center' valign='middle' scope='col'>Model</th></tr>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                message += "<tr style='height: 30px;'><td align='center' valign='middle' style='width: 120px;' class='showPadding'>" + Convert.ToString(dt.Rows[i]["Firstname"]) + " " + Convert.ToString(dt.Rows[i]["LastName"]) + "</td><td align='left' valign='middle' style='width: 100px;' class='showPadding'>" + Convert.ToString(dt.Rows[i]["make"]) + "</td><td align='center' valign='middle' style='width: 90px;' class='showPadding'>" + Convert.ToString(dt.Rows[i]["model"]) + "</td></tr>";
                AdminID = 1;
                CustomerID = Convert.ToInt32(dt.Rows[i]["Customerid"]);
                //DealerID = Convert.ToString(dt.Rows[0]["allDealerids"]);
                ReplySubject = "PF to Dealer on locking event";
                Description = "";

                if (Convert.ToInt32(Save_VDT_Admin_Replay()) == 1)
                {
                    cnt++;
                    // objCls_GenericEmailHelper.SendEmail();
                }
            }
            message += "</table>";
            //end

            //Commented By : Ayyaj Desc: To Replace Catherine
            //message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'> Many thanks<br /> <span style='color: #000080; font-family: Book Antiqua; font-size: 12pt; font-weight: bold;'>Catherine Heyes<br /> Group Customer Services Manager<br /> Private Fleet - Car Buying Made Easy</span><br />";
            //message = message + "<span style='font-size: 10.0pt; color: #1F497D'>Lvl 2 845 Pacific Hwy<span style='padding-left: 187px;'>Tel: 1300 303 181 (220)</span></span><br /><span style='font-size: 10.0pt; color: #1F497D'>Chatswood NSW 2067<span style='padding-left: 183px;'>Fax: 1300 303 981</span></span><br /> <span style='font: normal normal normal 11px Calibri;'> ABN: 70 080 056 408 | Dealer Lic: MD 19913</span> <span style='padding-left: 96px; font-size: 10pt;'><a href='www.privatefleet.com.au'target='_blank'>www.privatefleet.com.au</a></span><br /><br /></p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'> Many thanks<br /> <span style='color: #000080; font-family: Book Antiqua; font-size: 12pt; font-weight: bold;'>Mark Ellis<br />  Managing Director <br /> Private Fleet - Car Buying Made Easy</span><br />";
            message = message + "<span style='font-size: 10.0pt; color: #1F497D'>Lvl 2 845 Pacific Hwy<span style='padding-left: 187px;'>Tel: 1300 303 181 (246)</span></span><br /><span style='font-size: 10.0pt; color: #1F497D'>Chatswood NSW 2067<span style='padding-left: 183px;'>Fax: 1300 303 981</span></span><br /> <span style='font: normal normal normal 11px Calibri;'> ABN: 70 080 056 408 | Dealer Lic: MD 19913</span> <span style='padding-left: 96px; font-size: 10pt;'><a href='www.privatefleet.com.au'target='_blank'>www.privatefleet.com.au</a></span><br /><br /></p>";
            ConfigValues objConfigue = new ConfigValues();

            /*Commented On: 10 Sept 2014, By: Ayyaj, Desc:Catherine To Mark*/
            //objCls_GenericEmailHelper.EmailFromID = "Private Fleet<" + Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]) + ">";
            objCls_GenericEmailHelper.EmailFromID = "Private Fleet<" + Convert.ToString(ConfigurationManager.AppSettings["SecEmailFromID"]) + ">";
            objCls_GenericEmailHelper.EmailBody = message;
            objCls_GenericEmailHelper.EmailToID = Convert.ToString(dt.Rows[0]["DealerEmail"]);

            //AdminID = 1;
            //CustomerID = Convert.ToInt32(dt.Rows[0]["Customerid"]); ;
            //DealerID = Convert.ToString(dt.Rows[0]["allDealerids"]);
            //ReplySubject = "PF to Dealer on locking event";
            //Description = message;

            if (cnt == dt.Rows.Count)
            {
                objCls_GenericEmailHelper.SendEmail();
            }
        }
        catch (Exception ex)
        {
            logger.Error("sendMail_SecondResponse err -" + Convert.ToString(ex.Message));
        }

    }

    public Int32 Save_VDT_Admin_Replay()
    {
        int result = 0;
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "sp_SaveVDT_AdminReply");
            DataAccess.AddInParameter(objCmd, "AdminID", DbType.Int32, AdminID);
            DataAccess.AddInParameter(objCmd, "CustomerID", DbType.Int32, CustomerID);
            DataAccess.AddInParameter(objCmd, "DealerID", DbType.String, DealerID);
            DataAccess.AddInParameter(objCmd, "ReplySubject", DbType.String, ReplySubject);
            DataAccess.AddInParameter(objCmd, "Description", DbType.String, Description.Trim().Replace("'", "''"));

            //DataAccess.ExecuteNonQuery(objCmd);
            result = Convert.ToInt32(DataAccess.ExecuteScaler(objCmd, null));


        }
        catch (Exception ex)
        {
            logger.Error("Save_VDT_Admin_Replay err -" + Convert.ToString(ex.Message));
        }
        return result;
    }
    // new code END on 11/01/2012

    // on 15 Oct 2012
    public DataTable GetCustomerToSendEmailBefore3Days()
    {
        DataTable dt = new DataTable();
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "SPAutomaticEmailSendBefor3Days");
            dt = DataAccess.GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
        return dt;
    }
    //end


    public DataTable getUserDetailsFromID()
    {
        try
        {
            DbCommand objCmd = null;
            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "usp_getUserDetailsFromID");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ID", DbType.Int64, ID);
            return DataAccess.GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
            return null;
        }
    }

}

