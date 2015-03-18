using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using ServiceReference1;
using Mechsoft.GeneralUtilities;
using System.Configuration;

public partial class SendSMS : System.Web.UI.Page
{
    static ILog logger = LogManager.GetLogger(typeof(SendSMS));
    string ConsultantPhone = "";

    //page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        logger.Error("SMS page load starts");
        try
        {
            if (Convert.ToString(Session[Cls_Constants.PHONE]) != "")
            {
                ConsultantPhone = Convert.ToString(Session[Cls_Constants.PHONE]);

                if (ConsultantPhone.Contains("ext"))
                    ConsultantPhone = "1300 303 181 " + ConsultantPhone.Substring(ConsultantPhone.IndexOf("ext"));
                else
                    ConsultantPhone = "1300 303 181";
            }
            else
                ConsultantPhone = "-";

            ((Label)Master.FindControl("lblHeader")).Text = "Send SMS";

            string mobile = Convert.ToString(Session[Cls_Constants.MOBILE]);
            //txtDesc.Attributes.Add("onkeypress", "return maxLength(event,this);");
            //  txtSMSTo.Attributes.Add("OnTextChanged", "return chkMaxLimit_1(event,this);");

            //lblSMS.Text = Convert.ToString(160 - txtDesc.Text.Length);
            lblSMS.Text = Convert.ToString(txtDesc.Text.Length);
            if (!IsPostBack)
            {
                //lblCountryCode.Text = ConfigurationManager.AppSettings["CountryCode"];
                lblErrMsg.Visible = false;

                if (ConsultantPhone.Contains("ext"))
                    radioPreset.Items[2].Text += ConsultantPhone.Substring(ConsultantPhone.IndexOf("ext") + 3) + ".";
                else
                    radioPreset.Items[2].Text += "***.";

                txtDesc.Text = "Best no. is " + ConsultantPhone;
                if (!mobile.Equals(string.Empty))
                    txtDesc.Text += " OR " + mobile;
                txtDesc.Text += "\nThanks\n" + Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]) + "\nPrivate Fleet";
                // lblSMS.Text = Convert.ToString(160 - txtDesc.Text.Length);
                lblSMS.Text = Convert.ToString(txtDesc.Text.Length);


            }
        }
        catch (Exception ex)
        {
            logger.Error("SMS page load end");
        }


    }

    protected void btnSend_Click(object sender, ImageClickEventArgs e)
    {
        logger.Error("send st");
        try
        {
            if (txtSMSTo.Text.Length < 10)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "err_msg", "alert('Please enter 10 digit no.');", true);
                txtSMSTo.Focus();
            }
            else
                divConfirm.Visible = true;
        }
        catch (Exception ex)
        {
            logger.Error("Send SMS Error - " + ex.Message);
        }
        finally
        {
            logger.Error("send send");
        }
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        txtDesc.Text = String.Empty;
        txtSMSTo.Text = String.Empty;
        txtDesc.Text = "Best no. is " + ConsultantPhone + "\nThanks\n" + Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]) + "\nPrivate Fleet";
        // lblSMS.Text = Convert.ToString(160 - txtDesc.Text.Length);
        lblSMS.Text = Convert.ToString(txtDesc.Text.Length);
        radioPreset.SelectedIndex = -1;
    }

    protected void btnConfirm_Click(object sender, ImageClickEventArgs e)
    {
        logger.Error("Send sms start confirm");
        Cls_General objSendSMS = new Cls_General();
        Page page = HttpContext.Current.Handler as Page;
        ConfigValues objConfig = new ConfigValues();
        try
        {
            //61414400670 -old change to  PvtFleet on 16 Oct 2012
            string senderID = Convert.ToString(objConfig.GetValue(Cls_Constants.SENDER_ID));

            //string[] no = { lblCountryCode.Text.Trim() + txtSMSTo_1.Text.Trim('0') + txtSMSTo.Text };
            string pno = txtSMSTo.Text;
            if (pno.Substring(0, 1) == "0")
                pno = pno.Substring(1);

            string[] no = { Convert.ToString(ConfigurationManager.AppSettings["CountryCode"]) + pno };
            //no[0] = "919922879590";
            ServiceReference1.PushServerWSPortTypeClient ws = new PushServerWSPortTypeClient();
            // string[] data = ws.sendmsg("", 3334477, "ashishlathi82", "P@ssw0rd", no1, "Private Fleet", txtDesc.Text.Trim(), 0, 1, 0, 0, 3, 0, 3, 0, 0, "abc123", 0, "SMS_TEXT", "", "", 1440);
            string[] data = ws.sendmsg("", 3336013, ConfigurationManager.AppSettings["ClickatellUN"].ToString(), ConfigurationManager.AppSettings["ClickatellPwd"].ToString(), no, senderID, txtDesc.Text.Trim(), 0, 1, 0, 0, 3, 0, 3, 0, 0, txtSMSTo.Text, 0, "SMS_TEXT", "", "", 1440);
            string output = data[0].Substring(0, 8);
            lblErrMsg.Visible = true;

            #region Chk clickatell Exception
            switch (output)
            {
                case "ERR: 001":
                    lblErrMsg.Text = "Authentication failed.";
                    objSendSMS.status = "Authentication failed.";
                    break;
                case "ERR: 002":
                    lblErrMsg.Text = "Unknown username or password.";
                    objSendSMS.status = "Unknown username or password.";
                    break;
                case "ERR: 003":
                    lblErrMsg.Text = "Session ID expired.";
                    objSendSMS.status = "Session ID expired.";
                    break;
                case "ERR: 004":
                    lblErrMsg.Text = "Account frozen.";
                    objSendSMS.status = "Account frozen.";
                    break;
                case "ERR: 005":
                    lblErrMsg.Text = "Missing session ID.";
                    objSendSMS.status = "Missing session ID.";
                    break;
                case "ERR: 007":
                    lblErrMsg.Text = "IP Lockdown violation.";
                    objSendSMS.status = "IP Lockdown violation.";
                    break;
                case "ERR: 101":
                    lblErrMsg.Text = "Invalid or missing parameters.";
                    objSendSMS.status = "Invalid or missing parameters.";
                    break;
                case "ERR: 102":
                    lblErrMsg.Text = "Invalid user data header.";
                    objSendSMS.status = "Invalid user data header.";
                    break;
                case "ERR: 103":
                    lblErrMsg.Text = "Unknown API message ID.";
                    objSendSMS.status = "Unknown API message ID.";
                    break;
                case "ERR: 104":
                    lblErrMsg.Text = "Unknown client message ID.";
                    objSendSMS.status = "Unknown client message ID.";
                    break;
                case "ERR: 105":
                    lblErrMsg.Text = "Invalid destination address.";
                    objSendSMS.status = "Invalid destination address.";
                    break;
                case "ERR: 106":
                    lblErrMsg.Text = "Invalid source address.";
                    objSendSMS.status = "Invalid source address.";
                    break;
                case "ERR: 107":
                    lblErrMsg.Text = "Empty message.";
                    objSendSMS.status = "Empty message.";
                    break;
                case "ERR: 108":
                    lblErrMsg.Text = "Invalid or missing API ID.";
                    objSendSMS.status = "Invalid or missing API ID.";
                    break;
                case "ERR: 109":
                    lblErrMsg.Text = "Missing message ID.";
                    objSendSMS.status = "Missing message ID.";
                    break;
                case "ERR: 110":
                    lblErrMsg.Text = "Error with email message.";
                    objSendSMS.status = "Error with email message.";
                    break;
                case "ERR: 111":
                    lblErrMsg.Text = "Invalid protocol.";
                    objSendSMS.status = "Invalid protocol.";
                    break;
                case "ERR: 112":
                    lblErrMsg.Text = "Invalid message type.";
                    objSendSMS.status = "Invalid message type.";
                    break;
                case "ERR: 113":
                    lblErrMsg.Text = "Maximum message parts exceeded.";
                    objSendSMS.status = "Maximum message parts exceeded.";
                    break;
                case "ERR: 114":
                    lblErrMsg.Text = "Cannot route message.";
                    objSendSMS.status = "Cannot route message.";
                    break;
                case "ERR: 115":
                    lblErrMsg.Text = "Message expired.";
                    objSendSMS.status = "Message expired.";
                    break;
                case "ERR: 116":
                    lblErrMsg.Text = "Invalid Unicode data.";
                    objSendSMS.status = "Invalid Unicode data.";
                    break;
                case "ERR: 120":
                    lblErrMsg.Text = "Invalid delivery time.";
                    objSendSMS.status = "Invalid delivery time.";
                    break;
                case "ERR: 121":
                    lblErrMsg.Text = "Destination mobile number blocked.";
                    objSendSMS.status = "Destination mobile number blocked.";
                    break;
                case "ERR: 122":
                    lblErrMsg.Text = "Destination mobile opted out.";
                    objSendSMS.status = "Destination mobile opted out.";
                    break;
                case "ERR: 123":
                    lblErrMsg.Text = "Invalid Sender ID.";
                    objSendSMS.status = "Invalid Sender ID.";
                    break;
                case "ERR: 128":
                    lblErrMsg.Text = "Number delisted.";
                    objSendSMS.status = "Number delisted.";
                    break;
                case "ERR: 301":
                    lblErrMsg.Text = "No credit left.";
                    objSendSMS.status = "No credit left.";
                    break;
                case "ERR: 302":
                    lblErrMsg.Text = "Max allowed credit.";
                    objSendSMS.status = "Max allowed credit.";
                    break;
                default:
                    lblErrMsg.Text = "SMS Send Successfully.";
                    objSendSMS.status = "send";
                    break;

            }
            #endregion

            //SAVE sms details to DB
            objSendSMS.SMSTo = Convert.ToString(no[0]);
            logger.Error("SMS to - " + objSendSMS.SMSTo);
            if (Session[Cls_Constants.LOGGED_IN_USERID] != null)
                objSendSMS.SMSFrom = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            else
                Response.Redirect("index.aspx");

            objSendSMS.SMSText = txtDesc.Text.Trim();
            objSendSMS.sendSMS();

            Response.Redirect("SendSMS.aspx");
        }
        catch (Exception ex)
        {
            logger.Error("Send SMS Error - " + ex.Message);
        }
        finally
        {
            objSendSMS = null;
            objConfig = null;
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        divConfirm.Visible = false;
        txtDesc.Focus();
    }

    protected void imgBtnConfirm_Click(object sender, ImageClickEventArgs e)
    {
        divConfirm.Visible = false;
        txtDesc.Focus();
    }

    protected void radioPreset_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (radioPreset.SelectedValue == "1")
        //{
        //    txtDesc.Text = Convert.ToString(radioPreset.SelectedItem);
        //    txtDesc.Text += "\nBest no. is " + ConsultantPhone + "\nThanks\n" + Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]) + "\nPrivate Fleet";
        //}
        //else if (radioPreset.SelectedValue == "2")
        //{
        //    txtDesc.Text = Convert.ToString(radioPreset.SelectedItem);
        //    txtDesc.Text += "\nBest no. is " + ConsultantPhone + "\nThanks\n" + Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]) + "\nPrivate Fleet";
        //}
        //else if (radioPreset.SelectedValue == "3")
        //{
        //    txtDesc.Text = Convert.ToString(radioPreset.SelectedItem);
        //    txtDesc.Text += "\nBest no. is " + ConsultantPhone + "\nThanks\n" + Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]) + "\nPrivate Fleet";
        //}
        txtDesc.Text = Convert.ToString(radioPreset.SelectedItem);
        txtDesc.Text += "\nBest no. is " + ConsultantPhone + "\nThanks\n" + Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]) + "\nPrivate Fleet";
        //lblSMS.Text = Convert.ToString(160 - txtDesc.Text.Length);
        lblSMS.Text = Convert.ToString(txtDesc.Text.Length);
    }
}
