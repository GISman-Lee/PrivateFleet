using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using log4net;
using System.Xml;
using System.Text;
using Mechsoft.FleetDeal;


public partial class User_Controls_UCSendEnquiry : System.Web.UI.UserControl
{
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_UCSendEnquiry));

    #region---Event Handling-----------------

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string strUrl = Request.Url.ToString();
            string[] str = strUrl.Split('/');

            if (str[str.Length - 1].ToString() == "Finance_Referral.aspx")
            {
                lblTitle.Text = "Finance Referral";
                trTitle.Visible = false;

            }
            else
            {
                lblTitle.Text = "Enquiry Details";
                btnPopClose.Visible = true;
                // added on 8 oct 2012 as change on Fin ref.
                trSurname.Visible = false;
                trPhone.Visible = false;
                lblComment.Visible = false;

            }
        }
        catch (Exception ex)
        {
            logger.Error("Error in Page Load" + ex.Message);
        }
    }

    protected void btnPopClose_Click(object sender, ImageClickEventArgs e)
    {
        HidePage();
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {

            Page page = HttpContext.Current.Handler as Page;

            string strUrl = Request.Url.ToString();
            string[] str = strUrl.Split('/');

            if (str[str.Length - 1].ToString() == "DealerQuickLookup.aspx")
            {
                SendMailToDealers();
            }
            else
            {
                SendMailToGemmah();
            }
        }
        catch (Exception ex)
        {
            logger.Error("btnSend_Click Event : " + ex.Message);
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        divConfirm.Visible = false;
        dvEnq.Visible = true;
        txtDesc.Focus();
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        Page page = HttpContext.Current.Handler as Page;
        if (!txtDesc.Text.Equals(String.Empty))
        {
            dvEnq.Visible = false;
            divConfirm.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Please enter the text to send.');", true);
            return;
        }
    }

    protected void imgBtnConfirm_Click(object sender, ImageClickEventArgs e)
    {
        divConfirm.Visible = false;
        dvEnq.Visible = true;
        txtDesc.Focus();
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        txtDesc.Text = "";
        txtDesc.Focus();
    }

    #endregion

    #region-----------------Methods-----------

    private void HidePage()
    {
        Page.GetType().InvokeMember("hidePopupDiv", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
    }

    public static void ShowAlertMessage(string error)
    {

        Page page = HttpContext.Current.Handler as Page;

        if (page != null)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);

        }

    }

    private void SendMailToGemmah()
    {

        logger.Debug("Sending mail start to gemmah@fincar.com.au ");
        ConfigValues objConfigue = new ConfigValues();
        objConfigue.Key = "EMAIL_TO_GEMMAH";
        string strEmailToID = objConfigue.GetValue(objConfigue.Key).ToString();
        try
        {
            Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
            StringBuilder str = new StringBuilder();
            Cls_Dealer objEnquiry = new Cls_Dealer();
            str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear </p> ");
            //str.Append(" on " + System.DateTime.UtcNow.Date.ToString("MMMM dd, yyyy") + ".</p> ");
            if (Session[Cls_Constants.FromEmailID] != null)
            {
                objEmailHelper.EmailFromID = Session[Cls_Constants.FromEmailID].ToString();
            }
            objEmailHelper.EmailToID = strEmailToID;
            objEmailHelper.EmailCcID = Convert.ToString(Session[Cls_Constants.FromEmailID]);
            str.Append("<p style='font: normal 12px Tahoma;'>");

            //added on 8 Oct 2012
            str.Append("Surname - " + txtSurname.Text.Trim());
            str.Append("<br/>Phone Number - " + txtPhone.Text.Trim());

            str.Append("<br/>Comments - " + txtDesc.Text + "<br />");
            string strConsultantName = "";
            if (Session[Cls_Constants.CONSULTANT_NAME] != null)
            {
                strConsultantName = Session[Cls_Constants.CONSULTANT_NAME].ToString();
            }
            str.Append("<br /><br />Kind Regards<br /><br />" + strConsultantName);
            str.Append("</p><p><a href='http://www.privatefleet.com.au/'>www.privatefleet.com.au</a><br /><br />");
            str.Append("Please note:  This system is in beta.  Please report any bugs, suggestions or other feedback to ");
            str.Append("<a href='mailto:davidlye@privatefleet.com.au'>davidlye@privatefleet.com.au</a></p>");
            objEmailHelper.EmailBody = str.ToString();
            objEmailHelper.EmailSubject = "Finance Refereral";

            logger.Debug("Email To - " + objEmailHelper.EmailToID + " : Email From - " + objEmailHelper.EmailFromID);
            if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
            {

                string strMsg = objEmailHelper.SendEmail().ToString();
                Cls_Dealer objClsDeal = new Cls_Dealer();
                objClsDeal.UserID = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
                objClsDeal.EmailTo = strEmailToID;
                objClsDeal.Surname = txtSurname.Text.Trim();
                objClsDeal.Phone = txtPhone.Text.Trim();
                objClsDeal.Details = txtDesc.Text.Trim();
                objClsDeal.Createdby = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
                objClsDeal.Modifiedby = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
                objClsDeal.AddFinanceReferral();

                ShowAlertMessage(strMsg);
                dvEnq.Visible = true;
                divConfirm.Visible = false;
                txtDesc.Text = "";
                txtSurname.Text = "";
                txtPhone.Text = "";
                logger.Debug("Fin Ref save and send successfully");
            }

        }
        catch (Exception ex)
        {
            logger.Error("Error while sending email to gemmah@fincar.com.au - " + ex.Message);
        }
        finally
        {
            logger.Debug("Sending mail ends for  gemmah@fincar.com.au");
        }


    }

    public void ShowHideDivs()
    {
        dvEnq.Visible = true;
        divConfirm.Visible = false;
        txtDesc.Text = "";
    }

    private void SendMailToDealers()
    {
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
        Cls_Dealer objEnquiry = new Cls_Dealer();
        Cls_Dealer objClsDeal = new Cls_Dealer();

        DataTable dt = new DataTable();
        StringBuilder str = new StringBuilder();
        StringBuilder strDealerLst = new StringBuilder();
        StringBuilder dealerList = new StringBuilder();
        try
        {
            if (Session["DealerInfo"] != null)
                dt = (DataTable)Session["DealerInfo"];

            string strMsg = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    str = new StringBuilder();
                    strDealerLst.Append("," + Convert.ToString(dr["DealerID"]));

                    objClsDeal.ID = Convert.ToInt32(dr["DealerID"].ToString());
                    DataTable dtDealer = objClsDeal.GetDealerDetails();
                    if (dtDealer != null && dtDealer.Rows.Count == 1)
                    {
                        dealerList.Append(Convert.ToString(dtDealer.Rows[0]["Name"]) + " : " + Convert.ToString(dtDealer.Rows[0]["EMail"]) + "<br/>");
                        string strConsultantName = "";
                        if (Session[Cls_Constants.CONSULTANT_NAME] != null)
                            strConsultantName = Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]);

                        str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear " + Convert.ToString(dtDealer.Rows[0]["Name"]));
                        str.Append("<br/><br/>You received one vehicle enquiry request from " + strConsultantName + " on " + System.DateTime.UtcNow.Date.ToString("MMMM dd, yyyy") + ".</p> ");
                        str.Append("<p style='font: normal 12px Tahoma;'>");
                        str.Append(txtDesc.Text + "<br />");
                        str.Append("<br /><br />Kind Regards<br /><br />" + strConsultantName);
                        str.Append("<br /><a href='http://www.privatefleet.com.au/'>www.privatefleet.com.au</a><br /><br />");
                        str.Append("Please note:  This system is in beta.  Please report any bugs, suggestions or other feedback to ");
                        str.Append("<a href='mailto:davidlye@privatefleet.com.au'>davidlye@privatefleet.com.au</a></p>");

                        if (Session[Cls_Constants.FromEmailID] != null)
                            objEmailHelper.EmailFromID = Convert.ToString(Session[Cls_Constants.FromEmailID]);

                        objEmailHelper.EmailToID = Convert.ToString(dtDealer.Rows[0]["EMail"]);
                        objEmailHelper.EmailBody = str.ToString();
                        objEmailHelper.EmailSubject = "Received Vehicle Enquiry request from " + Session[Cls_Constants.CONSULTANT_NAME].ToString();

                        if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
                            strMsg = Convert.ToString(objEmailHelper.SendEmail());
                    }
                }

                //send email to consultant - all dealer list and msg
                str = new StringBuilder();
                objEmailHelper = new Cls_GenericEmailHelper();
                str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear " + Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]));
                str.Append("<br/><br/>Below is the dealer list to whome vehicle enquiry request is send on " + System.DateTime.UtcNow.Date.ToString("MMMM dd, yyyy") + ".</p> ");
                str.Append("<p style='font: normal 12px Tahoma;'>" + dealerList);
                str.Append("<br/>Enquiry Content : <br />");
                str.Append(txtDesc.Text + "<br />");
                str.Append("<br /><br />Kind Regards<br />Private Fleet.");
                str.Append("<br /><a href='http://www.privatefleet.com.au/'>www.privatefleet.com.au</a><br /><br />");
                str.Append("Please note:  This system is in beta.  Please report any bugs, suggestions or other feedback to ");
                str.Append("<a href='mailto:davidlye@privatefleet.com.au'>davidlye@privatefleet.com.au</a></p>");

                objEmailHelper.EmailFromID = ConfigurationManager.AppSettings["EmailFromID"];
                objEmailHelper.EmailToID = Convert.ToString(Session[Cls_Constants.FromEmailID]);
                objEmailHelper.EmailBody = str.ToString();
                objEmailHelper.EmailSubject = "Send Vehicle Enquiry request";
                if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
                    strMsg = Convert.ToString(objEmailHelper.SendEmail());

                // Add to DB dealer ID's
                objClsDeal.UserID = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
                objClsDeal.VehicleDealerID = Convert.ToString(strDealerLst).Substring(1);
                objClsDeal.Details = txtDesc.Text.Trim();
                objClsDeal.Createdby = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
                objClsDeal.Modifiedby = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
                objClsDeal.AddVehicleEnquiry();
                dt.Dispose();
                HidePage();
                ShowAlertMessage(strMsg);
            }
            foreach (GridViewRow gr in ((GridView)this.Parent.FindControl("gvDealerDetails")).Rows)
            {
                CheckBox chk = (CheckBox)gr.FindControl("chkSelect");
                chk.Checked = false;
            }
        }
        catch (Exception ex)
        {
            logger.Error("Error while sending email (Dealer quick lookup) - " + ex.Message);
        }
        finally
        {
            objEmailHelper = null;
            objClsDeal = null;
            objEnquiry = null;
            str = strDealerLst = dealerList = null;
            dt = null;
            logger.Debug("Sending mail ends for Dealer Quick Lookup");
        }

    }

    #endregion


}

