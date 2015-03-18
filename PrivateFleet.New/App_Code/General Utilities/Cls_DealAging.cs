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
using Mechsoft.FleetDeal;
using log4net;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for Cls_DealAging
/// </summary>
public class Cls_DealAging
{
    static ILog logger = LogManager.GetLogger(typeof(Cls_DealAging));

    public Cls_DealAging()
    {
        //
        // TODO: Add constructor logic here
        // 
    }

    public static void HandleDealAgingFactor()
    {
        ConfigValues objConfig = new ConfigValues();
        try
        {
            DateTime dtActualDate = new DateTime();
            dtActualDate = DateTime.UtcNow.Date;

            int NoOfDaysForAging = Convert.ToInt16(objConfig.GetValue(Cls_Constants.NO_OF_DAYS_TO_REDUCE_POINTS_AFTER_SHORTLISTING));
            int NoOfPointsToReduce = Convert.ToInt16(objConfig.GetValue(Cls_Constants.NO_OF_POINTS_TO_REDUCE));

            DateTime dtToCompare = dtActualDate.AddDays((0 - NoOfDaysForAging));
            DbCommand objCmd = null;
            Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
            objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpHandleAgingOfDeal");
            //Add Parameters to the stored procedure
            DataAccess.AddInParameter(objCmd, "ActualPointModificationDate", DbType.DateTime, dtActualDate);
            DataAccess.AddInParameter(objCmd, "DateToCompare", DbType.DateTime, dtToCompare);
            DataAccess.AddInParameter(objCmd, "Points", DbType.Int16, NoOfPointsToReduce);

            DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("HandleDealAgingFactor Error - " + ex.Message);
        }
        finally
        {
            objConfig = null;
        }
    }

    public static void MakeHotDealerAsNormalDealer()
    {
        ConfigValues objConfig = new ConfigValues();
        try
        {
            ;
            int NoOfDaysForComparion = Convert.ToInt16(objConfig.GetValue(Cls_Constants.NO_OF_DAYS_AFTER_WHICH_HOT_DEALER_BECOMES_NORMAL));
            DateTime dtToCompare = DateTime.UtcNow.Date.AddDays((0 - NoOfDaysForComparion));
            DbCommand objCmd = null;
            Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
            objCmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpMakeDealerAsNormal");

            //Add Parameters to the stored procedure
            DataAccess.AddInParameter(objCmd, "DateToCompare", DbType.DateTime, dtToCompare);
            DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("MakeHotDealerAsNormalDealer Error - " + ex.Message);
        }
        finally
        {
            objConfig = null;
        }
    }

    public static void AutomaticMailSendingForTradeInAlerts()
    {
        DbCommand objCmd = null;
        DataSet ds = new DataSet();
        DataTable dtID = new DataTable();
        DataTable dtData = new DataTable();
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
        Cls_Quotation objQuotation = new Cls_Quotation();
        StringBuilder str = new StringBuilder();
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAutomaticMailSendForTradeInSearch");
            ds = Cls_DataAccess.getInstance().GetDataSet(objCmd);
            dtID = ds.Tables[1];
            dtData = ds.Tables[0];

            // mail sending start
            if (dtID.Rows.Count > 0)
            {
                for (int i = 0; i < dtID.Rows.Count; i++)
                {
                    DataTable dtTemp = new DataTable();
                    DataView dv = dtData.DefaultView;
                    dv.RowFilter = "AlertID=" + dtID.Rows[i]["AlertID"];
                    dtTemp = dv.ToTable();

                    if (dtTemp.Rows.Count > 0)
                    {
                        objCmd = null;
                        objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpUpdateTradeInAlertCount");
                        Cls_DataAccess.getInstance().AddInParameter(objCmd, "@AlertID", DbType.Int32, Convert.ToInt32(dtID.Rows[i]["AlertID"]));
                        int re = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);

                        //mail sending start
                        str = new StringBuilder();
                        str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear ");
                        str.Append(dtTemp.Rows[0]["Name"].ToString());

                        str.Append("<br /><br />Your alert match below Trade in Data for the customer");
                        str.Append("<br /><br />Customer Name - " + dtTemp.Rows[0]["CustName"]);
                        str.Append("<br />Customer Contact - " + dtTemp.Rows[0]["Contact"]);

                        str.Append("<br /><br/><b>Search Criteria</b>");
                        str.Append("<br /><br />Make - " + dtTemp.Rows[0]["SerMake"]);
                        str.Append("<br />Model - " + dtTemp.Rows[0]["Model"]);
                        str.Append("<br />Transmission - " + dtTemp.Rows[0]["SerTransmission"]);
                        str.Append("<br />State - " + dtTemp.Rows[0]["SerState"]);
                        str.Append("<br />From Year - " + dtTemp.Rows[0]["SerFY"]);
                        str.Append("<br />To Year - " + dtTemp.Rows[0]["SerTY"]);
                        str.Append("<br />Min Value - " + dtTemp.Rows[0]["SerMiV"]);
                        str.Append("<br />Max Value - " + dtTemp.Rows[0]["SerMxV"]);

                        str.Append("<br /><br/><b>Trade In Data</b>");
                        for (int j = 0; j < dtTemp.Rows.Count; j++)
                        {
                            objCmd = null;
                            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAddTradeInAlertReport");
                            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ID", DbType.Int32, Convert.ToInt32(dtTemp.Rows[j]["ID"]));
                            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@AlertID", DbType.Int32, Convert.ToInt32(dtTemp.Rows[j]["AlertID"]));
                            int re1 = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);

                            str.Append("<br/><br/><b>Match " + (j + 1) + " - </b>");
                            str.Append("<br />Make - </b>" + Convert.ToString(dtTemp.Rows[j]["Make"]));
                            str.Append("<br />Model - </b>" + Convert.ToString(dtTemp.Rows[j]["T1Model"]));
                            str.Append("<br />State - </b>" + Convert.ToString(dtTemp.Rows[j]["HomeState"]));
                            str.Append("<br />Transmission - </b>" + Convert.ToString(dtTemp.Rows[j]["T1Transmission"]));
                            str.Append("<br />Year - </b>" + Convert.ToString(dtTemp.Rows[j]["T1Year"]));
                            str.Append("<br />Delivery Date - </b>" + Convert.ToString(dtTemp.Rows[j]["DeliveryDate"]));
                            str.Append("<br />Orig Trade in Value - </b>" + Convert.ToString(dtTemp.Rows[j]["T1OrigValue"]));
                        }

                        //add email sender and receiver. EmailFromID
                        objEmailHelper.EmailFromID = ConfigurationManager.AppSettings["EmailFromID"]; ;
                        objEmailHelper.EmailToID = dtData.Rows[0]["Email"].ToString();

                        str.Append(" </p>");

                        objEmailHelper.EmailBody = str.ToString();
                        //objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
                        //objEmailHelper.EmailFromID = "manoj.mahagaonkar@mechsoftgroup.com";
                        objEmailHelper.EmailSubject = "Alert match for Trade In Data";

                        if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
                        {
                            objEmailHelper.SendEmail();
                        }
                    }
                }
            }
            // mail sending end
        }
        catch (Exception ex)
        {
            logger.Error("AutomaticMailSendingForTradeInAlerts Error - " + ex.Message);
        }
        finally
        {
            objCmd = null;
            ds = null;
            dtID = null;
            dtData = null;
            objEmailHelper = null;
            objQuotation = null;
            str = null;
        }
    }

    public static void AutomaticShortlistingOfQuote()
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAutomaticShortlisting");
            objCmd.CommandTimeout = 300;
            int result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("AutomaticShortlistingOfQuote Error - " + ex.Message);
        }
        finally
        {
            objCmd = null;
        }
    }

    public static void SendUpdateReminderToDealer_new()
    {
        Cls_Login objCls_Login = new Cls_Login();
        objCls_Login.UserName = "";
        objCls_Login.Flag = 0;
        DataTable dt = new DataTable();
        dt = objCls_Login.GetDealerRespone();
        Int32 LowerLimit = 0, MiddleLimit = 0, UppeLimit = 0;
        Int32 LowerLimit_50_120 = 0, MiddleLimit_50_120 = 0, UppeLimit_50_120 = 0;
        Int32 LowerLimit_120 = 0, MiddleLimit_120 = 0, UppeLimit_120 = 0;
        String DealerEmail_Temp = "", DealerIDs = "";
        DataTable dt_t; int DealerID_t = 0;
        ConfigValues objConfigue = new ConfigValues();

        try
        {
            objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS";
            LowerLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

            objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS_MIDDLE";
            MiddleLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

            objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS";
            UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());


            objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS_50_TO_120";
            LowerLimit_50_120 = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

            objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS_MIDDLE_50_TO_120";
            MiddleLimit_50_120 = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

            objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS_50_TO_120";
            UppeLimit_50_120 = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

            objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS_MORE_120";
            LowerLimit_120 = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

            objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS_MIDDLE_MORE_120";
            MiddleLimit_120 = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

            objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS_MORE_120";
            UppeLimit_120 = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

            DataView dv_Temp = dt.DefaultView;
            //  dv_Temp.RowFilter = "nonResponseDate=" + nonResponse_LowerLimit + " OR nonResponseDate=" + nonResponse_MiddleLimit + " OR nonResponseDate=" + nonResponse_UppeLimit;
            dv_Temp.RowFilter = "nonResponseDate in (" + LowerLimit + "," + MiddleLimit + "," + UppeLimit + "," + LowerLimit_50_120 + "," + MiddleLimit_50_120 + "," + UppeLimit_50_120 + "," + LowerLimit_120 + "," + MiddleLimit_120 + "," + UppeLimit_120 + ")";
            dt = dv_Temp.ToTable();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (!Convert.ToString(drow["DealerEmail"]).Equals(String.Empty) && DealerEmail_Temp != Convert.ToString(drow["DealerEmail"]))
                        {
                            DealerEmail_Temp = Convert.ToString(drow["DealerEmail"]);
                            DataTable dt1 = new DataTable();
                            dt1 = null;
                            DataView dv1 = dt.DefaultView;
                            dv1.RowFilter = "DealerEmail='" + Convert.ToString(drow["DealerEmail"]) + "'";
                            dt1 = dv1.ToTable();
                            if (dt1.Rows.Count > 0)
                            {
                                DealerIDs = ""; DealerID_t = 0;
                                for (int j = 0; j < dt1.Rows.Count; j++)
                                {
                                    if (DealerID_t != Convert.ToInt32(dt1.Rows[j]["DealerID"]))
                                    {
                                        DealerIDs += "," + Convert.ToString(dt1.Rows[j]["DealerID"]);
                                        DealerID_t = Convert.ToInt32(dt1.Rows[j]["DealerID"]);
                                    }
                                }
                                DealerIDs = DealerIDs.Substring(1);

                                objCls_Login.Dealer_Non_Response_UpperLimit = UppeLimit;
                                // 7days
                                DataView dv_Temp1 = dt1.DefaultView;
                                dv_Temp1.RowFilter = "nonResponseDate=" + LowerLimit + " and diff<50";
                                dt_t = dv_Temp1.ToTable();
                                if (dt_t.Rows.Count > 0)
                                    objCls_Login.sendMail_FirstResponse(dt_t, DealerIDs);

                                //10 days
                                dv_Temp1 = null; dt_t = null;
                                dv_Temp1 = dt1.DefaultView;
                                dv_Temp1.RowFilter = "nonResponseDate=" + MiddleLimit + " and diff<50";
                                dt_t = dv_Temp1.ToTable();
                                if (dt_t.Rows.Count > 0)
                                    objCls_Login.sendMail_FirstResponse(dt_t, DealerIDs);

                                //12 days
                                dv_Temp1 = null; dt_t = null;
                                dv_Temp1 = dt1.DefaultView;
                                dv_Temp1.RowFilter = "nonResponseDate=" + UppeLimit + " and diff<50";
                                dt_t = dv_Temp1.ToTable();
                                if (dt_t.Rows.Count > 0)
                                    objCls_Login.sendMail_SecondResponse(dt_t, DealerIDs);

                                //-----------------------------------------------------------------------------

                                objCls_Login.Dealer_Non_Response_UpperLimit = UppeLimit_50_120;
                                // 14 days
                                dv_Temp1 = null; dt_t = null;
                                dv_Temp1 = dt1.DefaultView;
                                dv_Temp1.RowFilter = "nonResponseDate=" + LowerLimit_50_120 + " and diff>=50 and diff<120";
                                dt_t = dv_Temp1.ToTable();
                                if (dt_t.Rows.Count > 0)
                                    objCls_Login.sendMail_FirstResponse(dt_t, DealerIDs);

                                //17 days
                                dv_Temp1 = null; dt_t = null;
                                dv_Temp1 = dt1.DefaultView;
                                dv_Temp1.RowFilter = "nonResponseDate=" + MiddleLimit_50_120 + "and diff>=50 and diff<120";
                                dt_t = dv_Temp1.ToTable();
                                if (dt_t.Rows.Count > 0)
                                    objCls_Login.sendMail_FirstResponse(dt_t, DealerIDs);

                                //19 days
                                dv_Temp1 = null; dt_t = null;
                                dv_Temp1 = dt1.DefaultView;
                                dv_Temp1.RowFilter = "nonResponseDate=" + UppeLimit_50_120 + " and diff>=50 and diff<120";
                                dt_t = dv_Temp1.ToTable();
                                if (dt_t.Rows.Count > 0)
                                    objCls_Login.sendMail_SecondResponse(dt_t, DealerIDs);

                                //----------------------------------------------------------------------

                                objCls_Login.Dealer_Non_Response_UpperLimit = UppeLimit_120;
                                // 28 days
                                dv_Temp1 = null; dt_t = null;
                                dv_Temp1 = dt1.DefaultView;
                                dv_Temp1.RowFilter = "nonResponseDate=" + LowerLimit_120 + " and diff>=120";
                                dt_t = dv_Temp1.ToTable();
                                if (dt_t.Rows.Count > 0)
                                    objCls_Login.sendMail_FirstResponse(dt_t, DealerIDs);

                                //31 days
                                dv_Temp1 = null; dt_t = null;
                                dv_Temp1 = dt1.DefaultView;
                                dv_Temp1.RowFilter = "nonResponseDate=" + MiddleLimit_120 + " and diff>=120";
                                dt_t = dv_Temp1.ToTable();
                                if (dt_t.Rows.Count > 0)
                                    objCls_Login.sendMail_FirstResponse(dt_t, DealerIDs);

                                //33 days
                                dv_Temp1 = null; dt_t = null;
                                dv_Temp1 = dt1.DefaultView;
                                dv_Temp1.RowFilter = "nonResponseDate=" + UppeLimit_120 + " and diff>=120";
                                dt_t = dv_Temp1.ToTable();
                                if (dt_t.Rows.Count > 0)
                                    objCls_Login.sendMail_SecondResponse(dt_t, DealerIDs);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Debug("SendUpdateReminderToDealer_new err - " + ex.Message);
        }
        finally
        {
            objCls_Login = null;
            dt = null;
            objConfigue = null;
        }
    }

    /// <summary>
    /// To send email to Client after 4 hrs if ETA change was drastic
    /// </summary>
    public static void AutomaticDrasticMailSend()
    {
        Cls_VDTWelcome objCls_VDTWelcome = new Cls_VDTWelcome();
        Cls_DealAging obj = new Cls_DealAging();
        DataTable dt = new DataTable();
        dt = objCls_VDTWelcome.AutomaticDrasticMailSend();
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                obj.SendMail_DealerUpdatesStatus(dr);
            }
        }
        catch (Exception ex)
        {
            logger.Debug("SendUpdateReminderToDealer_new err - " + ex.Message);
        }
        finally
        {
            objCls_VDTWelcome = null;
            dt = null;
            obj = null;
        }
    }

    public void SendMail_DealerUpdatesStatus(DataRow dRow)
    {
        Cls_VDTWelcome objCls_VDTWelcome = new Cls_VDTWelcome();
        try
        {
            string message = "";
            string emailid = "";
            string BccEmailid = string.Empty;
            string username = "";
            string status = "";
            string fullname = "";

            emailid = Convert.ToString(dRow["email"]);
            if (!string.IsNullOrEmpty(Convert.ToString(dRow["Email_2"])))
            {
                BccEmailid = Convert.ToString(dRow["Email_2"]);
            }
            username = Convert.ToString(dRow["FirstName"]);
            fullname = Convert.ToString(dRow["FullName"]);

            message = "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + username + ",</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>This is to advise that the supplying dealer of your new car has just updated the delivery status.</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>The current " + "<b>estimated</b>" + " delivery date is " + Convert.ToDateTime(Convert.ToString(dRow["ETA"])).ToString("dd/MM/yyyy") + "</p>";

            if (Convert.ToString(dRow["DealerNotes"]).Trim() != "")
            {
                message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dealer Notes</br>" + Convert.ToString(dRow["DealerNotes"]).Trim() + "</p>";
            }

            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Remember for any clarification or to see a history of the delivery process of your new car, you can always log on to <a href='" + Convert.ToString(ConfigurationManager.AppSettings["VDTCustomerLoginURL"]) + "' target='_Blank'>updates.privatefleet.com.au</a> using your surname ( ";
            message = message + Convert.ToString(dRow["LastName"]).Trim() + " )and your Private Fleet member number ( " + Convert.ToString(dRow["MemberId"]).Trim() + " )</p>";

            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><br /><br />Best Regards</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><a href='http://www.privatefleet.com.au/' target='_Blank'>www.privatefleet.com.au</a><br /><br /></p>";

            Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
            clsemail.EmailBody = message;

            clsemail.EmailFromID = Convert.ToString(dRow["Name"]) + "<" + Convert.ToString(dRow["DealerEmail"]) + ">";
            clsemail.EmailToID = emailid;
            clsemail.EmailBccID = BccEmailid;
            clsemail.EmailSubject = "Status updated by Dealer.";
            clsemail.SendEmail();

            objCls_VDTWelcome.updateDrasticMailSend(Convert.ToInt64(dRow["ID"]));

        }
        catch (Exception ex)
        {
            logger.Error("SendMail_DealerUpdatesStatus Error - " + ex.Message);
        }
        finally
        {
            objCls_VDTWelcome = null;
        }
    }

    public static void SendEmailToCustomerBefore3Days()
    {
        Cls_Login objCls_Login = new Cls_Login();
        DataTable dt = new DataTable();
        dt = objCls_Login.GetCustomerToSendEmailBefore3Days();
        Cls_GenericEmailHelper objCls_GenericEmailHelper = new Cls_GenericEmailHelper();
        StringBuilder str = new StringBuilder();
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // To Dealer
                    str = new StringBuilder();
                    string DealerFirstName = "User";
                    string DealerFullName = "-";
                    if (!Convert.ToString(dt.Rows[i]["DealerName"]).Equals(String.Empty) && dt.Rows[i]["DealerName"] != null)
                    {
                        DealerFirstName = Convert.ToString(dt.Rows[i]["DealerName"]);
                        DealerFullName = Convert.ToString(dt.Rows[i]["DealerName"]);
                    }
                    if (DealerFirstName.Contains(" "))
                    {
                        DealerFirstName = DealerFirstName.Substring(0, DealerFirstName.IndexOf(' '));
                    }

                    string CustPhone = "-";
                    if (!Convert.ToString(dt.Rows[i]["CustPhone"]).Equals(String.Empty) && dt.Rows[i]["CustPhone"] != null)
                        CustPhone = Convert.ToString(dt.Rows[i]["CustPhone"]);
                    if (CustPhone == "-")
                    {
                        if (!Convert.ToString(dt.Rows[i]["CustMobile"]).Equals(String.Empty) && dt.Rows[i]["CustMobile"] != null)
                            CustPhone = Convert.ToString(dt.Rows[i]["CustMobile"]);
                    }

                    str.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + DealerFirstName + ",</p>");
                    str.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>This is a system generated email to alert you that the delivery for " + dt.Rows[i]["CustFullName"] + " is currently recorded as being on " + dt.Rows[i]["CustDD"] + ", three days from now.");
                    str.Append("<br/><br/>So if you haven’t already, please contact our client by email (" + dt.Rows[i]["CustEmail"] + ") or phone (" + CustPhone + ") and arrange the finer details for delivery (place, time etc).  If you could also update in the system, that would be great too.");
                    str.Append("<br/><br/>Many thanks – if any assistance is required, please don’t hesitate to get in touch.<br/><br/>Kind regards<br/><br/>Private Fleet<br/><br/>1300 303 181</p>");

                    objCls_GenericEmailHelper.EmailSubject = "Delivery Date for " + Convert.ToString(dt.Rows[i]["CustFullName"]) + " Approaching";
                    objCls_GenericEmailHelper.EmailBody = str.ToString();
                    objCls_GenericEmailHelper.EmailFromID = "Private Fleet<" + Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]) + ">";
                    objCls_GenericEmailHelper.EmailToID = Convert.ToString(dt.Rows[i]["DealerEmail"]);
                    // objCls_GenericEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";

                    if (!objCls_GenericEmailHelper.EmailFromID.Equals(String.Empty) && objCls_GenericEmailHelper.EmailFromID != null && !objCls_GenericEmailHelper.EmailToID.Equals(String.Empty) && objCls_GenericEmailHelper.EmailToID != null)
                    {
                        objCls_GenericEmailHelper.SendEmail();
                    }
                    //end dealer

                    // To customer
                    string CustFirstName = "User";
                    if (!Convert.ToString(dt.Rows[i]["CustFullName"]).Equals(String.Empty) && dt.Rows[i]["CustFirstName"] != null)
                        CustFirstName = Convert.ToString(dt.Rows[i]["CustFirstName"]);

                    str = new StringBuilder();
                    str.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + CustFirstName + ",</p>");
                    str.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>This is a system generated email regarding the delivery of your new car.");
                    str.Append("<br/><br/>The latest update on our system from " + DealerFullName + " at " + dt.Rows[i]["DealerCompany"] + " has delivery date currently recorded as being on " + dt.Rows[i]["CustDD"] + ", three days from now.");
                    str.Append("<br/><br/>So if you haven’t already, please contact the dealer directly by email (" + dt.Rows[i]["DealerEmail"] + ") or phone (" + dt.Rows[i]["DealerPhone"] + ") to arrange the finer details for the most convenient delivery.");
                    str.Append("<br/><br/>Many thanks – if any assistance is required, please don’t hesitate to get in touch.<br/><br/>Kind regards<br/><br/>Private Fleet<br/><br/>1300 303 181");

                    objCls_GenericEmailHelper.EmailSubject = "New Car Delivery Date Approaching";
                    objCls_GenericEmailHelper.EmailBody = str.ToString();
                    objCls_GenericEmailHelper.EmailFromID = "Private Fleet<" + Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]) + ">";
                    objCls_GenericEmailHelper.EmailToID = Convert.ToString(dt.Rows[i]["CustEmail"]);
                    //objCls_GenericEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";

                    if (!objCls_GenericEmailHelper.EmailFromID.Equals(String.Empty) && objCls_GenericEmailHelper.EmailFromID != null && !objCls_GenericEmailHelper.EmailToID.Equals(String.Empty) && objCls_GenericEmailHelper.EmailToID != null)
                    {
                        objCls_GenericEmailHelper.SendEmail();

                        objCls_Login.AdminID = 1;
                        objCls_Login.CustomerID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        objCls_Login.DealerID = Convert.ToString(dt.Rows[i]["DealerID"]);
                        objCls_Login.ReplySubject = "New Car Delivery Date Approaching";
                        objCls_Login.Description = "";
                        objCls_Login.Save_VDT_Admin_Replay();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            logger.Debug("SendEmailToCustomerBefore3Days err - " + ex.Message);
        }
        finally
        {
            objCls_Login = null;
            dt = null;
            objCls_GenericEmailHelper = null;
            str = null;
        }
    }

    public static void EmailForCustSerRepVDTOverdues()
    {
        Cls_VDTAdminReport objCls_VDTAdminReport = new Cls_VDTAdminReport();
        ConfigValues objConfigue = new ConfigValues();
        try
        {
            //Added By: Ayyaj Mujawar
            DateTime startTime = DateTime.Now;
            string temp = Convert.ToString(ConfigurationManager.AppSettings["EMailTime"]);
            int hour = Convert.ToInt32(temp.Substring(0, temp.IndexOf(".")));
            int min = Convert.ToInt32(temp.Substring(temp.IndexOf(".") + 1));

            Cls_DealerReportHelper objDealerRpt = new Cls_DealerReportHelper();

            DataTable dtDate = new DataTable();
            dtDate = objDealerRpt.GetProcessLastRunDate("EmailForCustSerRepVDTOverdues");
            DateTime LastRunDate = Convert.ToDateTime(dtDate.Rows[0]["LastRunDate"]);

            if (startTime.Hour == hour && LastRunDate.Date != startTime.Date)
            {

                DataTable dt = objCls_VDTAdminReport.get_VDTDealerResponse();

                DataView dv = new DataView(dt);
                // do not show the records which are unmarked by Admin and not updated
                dv.RowFilter = "Unmark=0";
                dt = dv.ToTable();

                int UppeLimit = 0;
                DataTable dtNew = new DataTable();
                dtNew = dt.Clone();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int DateDiff = Convert.ToInt32(dt.Rows[i]["Diff"]);
                    if (dt.Rows[i]["ETA"] == null || Convert.ToString(dt.Rows[i]["ETA"]).Equals(String.Empty))
                        continue;
                    DateTime DeliveryDate = Convert.ToDateTime(dt.Rows[i]["ETA"]);
                    TimeSpan dateDiff = DeliveryDate.Subtract(System.DateTime.Now);

                    if (dateDiff.Days <= 50)
                    {
                        objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS";
                        UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
                    }
                    else if (dateDiff.Days > 50 && dateDiff.Days <= 120)
                    {
                        objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS_50_TO_120";
                        UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
                    }
                    else if (dateDiff.Days > 120)
                    {
                        objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS_MORE_120";
                        UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
                    }
                    if (DateDiff >= UppeLimit)
                    {
                        dtNew.Rows.Add(dt.Rows[i].ItemArray);
                    }
                }
                dt = dtNew;



                if (dt.Rows.Count > 0)
                {
                    SendAlertEmailForCustSerRep(dt);
                }
                SendETAComingClosureForCustSerRep();
                SendDeliveryListForCustSerRep();

                Int64 result = objDealerRpt.UpdateProcessLastRunDate("EmailForCustSerRepVDTOverdues");
                if (result > 0)
                {
                    logger.Error("Process: EmailForCustSerRepVDTOverdues Run Successfully ");
                }
            }

        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    private static void SendAlertEmailForCustSerRep(DataTable dt)
    {
        DataTable dtTemp = new DataTable();
        ConfigValues objConfigue = new ConfigValues();
        try
        {
            String[] col = new String[4];
            col[0] = "CustomerSerRep";
            col[1] = "CustomerSerRepEmail";
            col[2] = "IsEmailSendDate";
            col[3] = "CustSerRepID";
            DataTable distinctCustSerRep = dt.DefaultView.ToTable(true, col);
            DataView dv = dt.DefaultView;
            TimeSpan span = new TimeSpan(0, 0, 0);
            StringBuilder message = new StringBuilder();
            int count = 0;
            List<int> list = new List<int>();
            //string Res_EmailId = "";

            #region
            //foreach (DataRow dr in distinctCustSerRep.Rows)
            //{
            //    dtTemp = null;
            //    dv.RowFilter = "CustomerSerRepEmail='" + dr["CustomerSerRepEmail"] + "'";
            //    dtTemp = dv.ToTable();
            //    //int diffHrs=DateTime
            //    if (dtTemp != null && dtTemp.Rows.Count > 0)
            //    {
            //        span = System.DateTime.Now.Subtract(Convert.ToDateTime(dr["IsEmailSendDate"]));
            //        if (span.TotalHours < 24)
            //            continue;

            //        StringBuilder message = new StringBuilder();
            //        string name = Convert.ToString(dr["CustomerSerRep"]);
            //        if (name.Contains(" "))
            //            name = name.Substring(0, name.IndexOf(" "));

            //        message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + name + ",</p>");
            //        message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>This is the Dealer no response list whose updates to clients are overdues.<br/><br/>");
            //        message.Append("<table cellspacing='0' border='1' style='width:100%;border-collapse:collapse;'>");
            //        message.Append("<tr style='font-weight:bold;height:30px;' class='gvHeader'><td>Customer Service Rep.</td><td>Dealer Name</td><td>Make</td><td>Dealer Email</td><td>Customer Name</td><td>Last Updated On</td></tr>");
            //        foreach (DataRow drow in dtTemp.Rows)
            //        {
            //            message.Append("<tr style='height:30px;'><td>" + Convert.ToString(drow["CustomerSerRep"]) + "</td><td>" + Convert.ToString(drow["Name"]) + "</td><td>" + Convert.ToString(drow["Make"]) + "</td><td>" + Convert.ToString(drow["Email"]) + "</td><td>" + Convert.ToString(drow["fullname"]) + "</td>");
            //            message.Append("<td style='width:80px;' class='grid_padding'>" + Convert.ToDateTime(drow["lastupdate"]).ToString("dd MMM yyyy") + "</td></tr>");
            //        }

            //        message.Append("</table></p>");
            //        message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><br /><br />Thanks & Regards<br/>Private Fleet</p>");
            //        message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><a href='http://www.privatefleet.com.au/' target='_Blank'>www.privatefleet.com.au</a></p>");

            //        Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
            //        clsemail.EmailBody = Convert.ToString(message);

            //        clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
            //        clsemail.EmailToID = Convert.ToString(dr["CustomerSerRepEmail"]);
            //        clsemail.EmailBccID = Convert.ToString(ConfigurationManager.AppSettings["TestEmailBCCID"]);
            //        clsemail.EmailSubject = "Dealer No Response List.";
            //        if (clsemail.EmailToID != null && !clsemail.EmailToID.Equals(String.Empty))
            //        {
            //            clsemail.SendEmail();
            //            objConfigue.updatePrimaryContacttbl(Convert.ToInt32(dr["CustSerRepID"]));
            //        }

            //    }
            //}
            #endregion

            message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear,</p>");
            message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>This is the Dealer no response list whose updates to clients are overdues.<br/><br/>");
            message.Append("<table cellspacing='0' border='1' style='width:100%;border-collapse:collapse;font-family:verdana;font-size:12px;'>");
            message.Append("<tr style='font-weight:bold;height:30px;' class='gvHeader'><td>Customer Service Rep.</td><td>Dealer Name</td><td>Make</td><td>Dealer Email</td><td>Customer Name</td><td>Last Updated On</td></tr>");

            foreach (DataRow dr in distinctCustSerRep.Rows)
            {
                dtTemp = null;
                dv.RowFilter = "CustomerSerRepEmail='" + dr["CustomerSerRepEmail"] + "'";
                dtTemp = dv.ToTable();
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    span = System.DateTime.Now.Subtract(Convert.ToDateTime(dr["IsEmailSendDate"]));
                    if (span.TotalHours < 24)
                        continue;

                    foreach (DataRow drow in dtTemp.Rows)
                    {
                        count = count + 1;
                        //Res_EmailId = Res_EmailId  + "," + drow["CustSerRepID"];
                        list.Add(Convert.ToInt32(drow["CustSerRepID"]));
                        message.Append("<tr style='height:30px;'><td>" + Convert.ToString(drow["CustomerSerRep"]) + "</td><td>" + Convert.ToString(drow["Name"]) + "</td><td>" + Convert.ToString(drow["Make"]) + "</td><td>" + Convert.ToString(drow["Email"]) + "</td><td>" + Convert.ToString(drow["fullname"]) + "</td>");
                        message.Append("<td style='width:80px;' class='grid_padding'>" + Convert.ToDateTime(drow["lastupdate"]).ToString("dd MMM yyyy") + "</td></tr>");
                    }
                }
            }
            //Res_EmailId = Res_EmailId.Substring(1, Res_EmailId.Length - 1);
            message.Append("</table></p>");
            message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><br /><br />Thanks & Regards<br/>Private Fleet</p>");
            message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><a href='http://www.privatefleet.com.au/' target='_Blank'>www.privatefleet.com.au</a></p>");

            Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
            clsemail.EmailBody = Convert.ToString(message);

            clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
            clsemail.EmailToID = Convert.ToString(ConfigurationManager.AppSettings["Dealer_NoResponse"]); //"chetan.shejole@mechsoftgroup.com";
            clsemail.EmailBccID = Convert.ToString(ConfigurationManager.AppSettings["TestEmailBCCID"]);
            clsemail.EmailSubject = "Dealer No Response List.";
            if (clsemail.EmailToID != null && !clsemail.EmailToID.Equals(String.Empty))
            {
                if (count > 0)
                {
                    clsemail.SendEmail();
                    var result = (from m in list
                                  select m).Distinct().ToList();

                    foreach (var item in result)
                    {
                        objConfigue.updatePrimaryContacttbl(item);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Deal Aging Send email to Cust Ser rep. error -  " + Convert.ToString(ex.Message));
        }
        finally
        {
            dtTemp = null;
        }
    }

    /// <summary>
    /// 25 Dec 2013
    /// Ayyaj Mujawar
    /// To send email of Todays ETA Coming Closure List to Customer Service Representative " 
    /// </summary>
    public static void SendETAComingClosureForCustSerRep()
    {
        DataTable dtTemp = new DataTable();
        ConfigValues objConfigue = new ConfigValues();
        try
        {
            //DateTime startTime = DateTime.Now;
            //string temp = Convert.ToString(ConfigurationManager.AppSettings["EMailTime"]);
            //int hour = Convert.ToInt32(temp.Substring(0, temp.IndexOf(".")));
            //int min = Convert.ToInt32(temp.Substring(temp.IndexOf(".") + 1));

            //if (startTime.Hour == hour && startTime.Minute == min && startTime.Second == 01)
            // Sync();
            Cls_DealerReportHelper objDealerRpt = new Cls_DealerReportHelper();
            Cls_PrimaryContact objPrimaryContact = new Cls_PrimaryContact();
            DataTable dtData = new DataTable();
            dtData = objPrimaryContact.GetAllPrimaryContacts();
            DataView dv = dtData.DefaultView;
            dv.RowFilter = "primaryContactFor<>'Survey' AND IsActive=1";
            dtData = dv.ToTable();


            DataTable distinctCustSerRep = dtData;

            foreach (DataRow dr in distinctCustSerRep.Rows)
            {

                dtTemp = objDealerRpt.GetDeliveryReportinNxt10Days(dr["Name"].ToString(), 10);



                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {


                    StringBuilder message = new StringBuilder();
                    string name = Convert.ToString(dr["Name"]);
                    if (name.Contains(" "))
                        name = name.Substring(0, name.IndexOf(" "));

                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + name + ",</p>");
                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>This is the ETA Coming Closure list .<br/><br/>");
                    message.Append("<table cellspacing='0' border='1' style='width:100%;border-collapse:collapse;'>");
                    message.Append("<tr style='font-weight:bold;height:30px;' class='gvHeader'><td>Customer Service Rep.</td><td>Customer Name</td><td>Make</td><td>Supplying Dealership</td><td>Dealer Name</td><td>Trade In Status</td><td>ETA</td></tr>");
                    foreach (DataRow drow in dtTemp.Rows)
                    {
                        message.Append("<tr style='height:30px;'><td>" + Convert.ToString(drow["PrimaryContact"]) + "</td><td>" + Convert.ToString(drow["customerName"]) + "</td><td>" + Convert.ToString(drow["Make"]) + "</td><td>" + Convert.ToString(drow["Company"]) + "</td><td>" + Convert.ToString(drow["DealerName"]) + "</td><td>" + Convert.ToString(drow["Tradestatus"]) + "</td>");
                        message.Append("<td style='width:80px;' class='grid_padding'>" + Convert.ToDateTime(drow["ETA"]).ToString("dd MMM yyyy") + "</td></tr>");
                    }

                    message.Append("</table></p>");
                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><br /><br />Thanks & Regards<br/>Private Fleet</p>");
                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><a href='http://www.privatefleet.com.au/' target='_Blank'>www.privatefleet.com.au</a></p>");

                    Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
                    clsemail.EmailBody = Convert.ToString(message);

                    clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
                    clsemail.EmailToID = Convert.ToString(dr["Email"]);//CustomerSerRepEmail
                    //clsemail.EmailBccID = Convert.ToString(ConfigurationManager.AppSettings["TestEmailBCCID"]);
                    clsemail.EmailBccID = "ayyaj.mujawar@mechsoftgroup.com";
                    clsemail.EmailSubject = "ETA Coming Closure List.";
                    if (clsemail.EmailToID != null && !clsemail.EmailToID.Equals(String.Empty))
                    {
                        clsemail.SendEmail();

                    }

                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("ETA Coming Closure Send email to Cust Ser rep. error -  " + Convert.ToString(ex.Message));
        }
        finally
        {
            dtTemp = null;
        }
    }

    /// <summary>
    /// 25 Dec 2013
    /// Ayyaj Mujawar
    /// To send email of Todays Delivery List to Customer Service Representative " 
    /// </summary>
    public static void SendDeliveryListForCustSerRep()
    {
        IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
        DataTable dtTemp = new DataTable();
        ConfigValues objConfigue = new ConfigValues();
        try
        {
            Cls_DealerReportHelper objDealerRpt = new Cls_DealerReportHelper();
            Cls_PrimaryContact objPrimaryContact = new Cls_PrimaryContact();
            DataTable dtData = new DataTable();
            dtData = objPrimaryContact.GetAllPrimaryContacts();
            DataView dv = dtData.DefaultView;
            dv.RowFilter = "primaryContactFor<>'Survey' AND IsActive=1";
            dtData = dv.ToTable();


            DataTable distinctCustSerRep = dtData;

            foreach (DataRow dr in distinctCustSerRep.Rows)
            {
                string tempToDate = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
                string tempFromDate = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
                //string tempFromDate = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
                objDealerRpt.strContactName = dr["Name"].ToString();
                objDealerRpt.FromDate = DateTime.Parse(tempFromDate.Trim(), culture);
                TimeSpan ts = new TimeSpan(1, 0, 0, 0);
                DateTime date1 = DateTime.Parse(tempToDate.Trim(), culture);
                objDealerRpt.ToDate = date1.Add(ts);


                dtTemp = objDealerRpt.GetDeliveredListReport();


                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {


                    StringBuilder message = new StringBuilder();
                    string name = Convert.ToString(dr["Name"]);
                    if (name.Contains(" "))
                        name = name.Substring(0, name.IndexOf(" "));

                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + name + ",</p>");
                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>This is the Delivery list .<br/><br/>");
                    message.Append("<table cellspacing='0' border='1' style='width:100%;border-collapse:collapse;'>");
                    message.Append("<tr style='font-weight:bold;height:30px;' class='gvHeader'><td>Customer Service Rep.</td><td>Customer Name</td><td>Make</td><td>Supplying Dealership</td><td>Dealer Name</td><td>Trade In Status</td><td>Status</td><td>Order Status</td><td>ETA</td></tr>");
                    foreach (DataRow drow in dtTemp.Rows)
                    {
                        message.Append("<tr style='height:30px;'><td>" + Convert.ToString(drow["PrimaryContact"]) + "</td><td>" + Convert.ToString(drow["customerName"]) + "</td><td>" + Convert.ToString(drow["Make"]) + "</td><td>" + Convert.ToString(drow["Company"]) + "</td><td>" + Convert.ToString(drow["DealerName"]) + "</td><td>" + Convert.ToString(drow["Tradestatus"]) + "</td><td>" + Convert.ToString(drow["Status1"]) + "</td><td>" + Convert.ToString(drow["OrderStatus"]) + "</td>");
                        message.Append("<td style='width:80px;' class='grid_padding'>" + Convert.ToDateTime(drow["ETA"]).ToString("dd MMM yyyy") + "</td></tr>");
                    }

                    message.Append("</table></p>");
                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><br /><br />Thanks & Regards<br/>Private Fleet</p>");
                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><a href='http://www.privatefleet.com.au/' target='_Blank'>www.privatefleet.com.au</a></p>");

                    Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
                    clsemail.EmailBody = Convert.ToString(message);

                    clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
                    clsemail.EmailToID = Convert.ToString(dr["Email"]);//CustomerSerRepEmail
                    //clsemail.EmailBccID = Convert.ToString(ConfigurationManager.AppSettings["TestEmailBCCID"]);
                    clsemail.EmailBccID = "ayyaj.mujawar@mechsoftgroup.com";
                    clsemail.EmailSubject = "Todays Delivery List.";
                    if (clsemail.EmailToID != null && !clsemail.EmailToID.Equals(String.Empty))
                    {
                        clsemail.SendEmail();

                    }

                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Delivery List Send email to Cust Ser rep. error -  " + Convert.ToString(ex.Message));
        }
        finally
        {
            dtTemp = null;
        }
    }

    /// <summary>
    /// 07 may 2013
    /// Manoj
    /// Get records to send email to Fincar admin if Finance field in ACt is "Fincar" 
    /// </summary>
    public static void SendFinanceAlertToFincarAdmin()
    {
        DataTable dtTemp = new DataTable();
        ConfigValues objConfigue = new ConfigValues();
        Cls_AdminFinanceAlert cls_AdminFinanceAlert = new Cls_AdminFinanceAlert();
        StringBuilder message = new StringBuilder();
        try
        {
            dtTemp = cls_AdminFinanceAlert.GetRecordstoSendAlert();
            //FINANCE_ALERT_EMAIL_TO
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                objConfigue.Key = "FINANCE_ALERT_EMAIL_TO";
                string EmailTo = Convert.ToString(objConfigue.GetValue(objConfigue.Key).ToString());

                foreach (DataRow dr in dtTemp.Rows)
                {
                    string custSer = "CUSTOMER SERVICE REPRESENTATIVE";
                    if (Convert.ToString(dr["CustomerSerRep"]) != string.Empty)
                        custSer = Convert.ToString(dr["CustomerSerRep"]);

                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear Admin,</p>");
                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Private Fleet would like to let you know that " + Convert.ToString(dr["CustName"]) + " vehicle has arrived or is due to arrive in dealership stock in the coming days.<br/><br/>");
                    message.Append("Please ensure invoice is to hand and communicate with " + custSer + " to advise of when finance is likely to settle so delivery can be organised</p>");
                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><br /><br />Thanks & Regards<br/>Private Fleet</p>");
                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><a href='http://www.privatefleet.com.au/' target='_Blank'>www.privatefleet.com.au</a></p>");

                    Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
                    clsemail.EmailBody = Convert.ToString(message);

                    clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
                    clsemail.EmailToID = EmailTo;
                    clsemail.EmailSubject = "Private Fleet/Fincar Client Upcoming Delivery";

                    if (clsemail.EmailToID != null && !clsemail.EmailToID.Equals(String.Empty))
                    {
                        clsemail.SendEmail();
                        cls_AdminFinanceAlert.updateFinanceEmailSend(Convert.ToInt32(dr["CustID"]));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Deal Aging SendFinanceAlertToFincarAdmin error -  " + Convert.ToString(ex.Message));
        }
        finally
        {
            dtTemp = null;
            objConfigue = null;
            cls_AdminFinanceAlert = null;
            message = null;
        }

    }


}
