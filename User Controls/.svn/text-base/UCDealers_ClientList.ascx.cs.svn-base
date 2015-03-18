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
using System.Text;

public partial class User_Controls_UCDealers_ClientList : System.Web.UI.UserControl
{
    #region Private Variable

    static ILog logger = LogManager.GetLogger(typeof(User_Controls_UCDealers_ClientList));
    Cls_DealerClinets objCls_DealerClinets = new Cls_DealerClinets();
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    public string username = string.Empty;
    public string customerid = string.Empty;
    IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Url.ToString().Contains("VDT_CustomerHelp.aspx") != true)
            {
                ((Label)this.Parent.Page.Master.FindControl("lblHeader")).Text = "Customer Status Update";
            }
            msgpop.Style.Add("display", "none");
            if (!Page.IsPostBack)
            {
                ViewState["_clintList"] = null;
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "date";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                grdCustomerListOnly.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue);

                #region Added by ARchana: 24 MArch 2012

                if (Request.QueryString["dlrid"] != null && Request.QueryString["dlrid"].Length > 0 && Request.QueryString["eml"] != null && Request.QueryString["eml"].Length > 0)
                {
                    imgBtnBacktoMainRpt.Visible = true;
                    Session["DlrEmail"] = Convert.ToString(Request.QueryString["eml"]);
                    Session["DlrId"] = Convert.ToString(Request.QueryString["dlrid"]);
                }
                else
                {
                    imgBtnBacktoMainRpt.Visible = false;
                }
                imgBtnBacktoMainRpt.Visible = Convert.ToString(Session[Cls_Constants.ROLE_ID]).Equals("1") ? true : false;

                #endregion

                binddata();

                //Checking Whether Client was updated or not
                if (Request.QueryString["Mode"] != null && Request.QueryString.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["Mode"]) && Request.QueryString["Mode"].Equals("U"))
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "test", "javascript:a();", true);
                        //mdlPopClientUpdated.Show();
                        hdnIsClientUpdated.Value = "yes";
                        msgpop.Style.Add("display", "block");

                        Page page = HttpContext.Current.Handler as Page;
                        ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "javascript:SetTimerToHideDiv();", true);
                        hdnIsClientUpdated.Value = string.Empty;

                        if (Convert.ToString(Session[Cls_Constants.ROLE_ID]).Equals("2"))
                        {
                            //Server.Transfer("~/ClinetIfo_ForDealer.aspx");
                            pnlCustomerList.Visible = true;
                            pnlCustomerDetail.Visible = false;
                        }
                    }
                    else
                    {

                    }
                }

                //Added by archana
                if (Request.QueryString["cmpny"] != null && Request.QueryString["cmpny"].Length > 0 && Request.QueryString["make"] != null && Request.QueryString["make"].Length > 0)
                {
                    string _company = string.Empty, _make = string.Empty, _dlrId = string.Empty, _eml = string.Empty;
                    _company = !string.IsNullOrEmpty(Request.QueryString["cmpny"]) ? Convert.ToString(Request.QueryString["cmpny"]) : "";
                    _make = !string.IsNullOrEmpty(Request.QueryString["make"]) ? Convert.ToString(Request.QueryString["make"]) : "";

                    Session["Cmpny"] = _company;
                    Session["make"] = _make;
                }

                if (Request.QueryString["PC"] != null && !Convert.ToString(Request.QueryString["PC"]).Equals(String.Empty))
                {
                    ((GridView)grdClientList.Rows[0].FindControl("grdDetails")).FooterRow.Visible = false;
                    ((ImageButton)grdClientList.Rows[0].FindControl("btnEditDealerIp")).Visible = false;
                    imgbtnSameETA.Visible = false;
                    imgBtnCancelOrder.Visible = false;

                    // grdDetails.ShowFooter = false;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    #endregion

    #region Events

    public void grdClientList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt1 = new DataTable();
            try
            {
                Label lblCustomerID = (Label)e.Row.FindControl("lblCustomerID");
                GridView grdDetails = (GridView)e.Row.FindControl("grdDetails");
                ds = (DataSet)ViewState["_clintList"];
                DataView dv = new DataView(ds.Tables[1]);
                dv.RowFilter = "CustomerID=" + Convert.ToString(lblCustomerID.Text.Trim());
                dt1 = dv.ToTable();
                if (dt != null)
                {
                    grdDetails.DataSource = dt1;
                    grdDetails.DataBind();
                    if (dt1.Rows.Count > 0)
                    {
                        e.Row.Visible = true;

                        TextBox txtETA = (TextBox)grdDetails.FooterRow.FindControl("txtETA");
                        // comment on 9 Jan 13
                        //As per requirement ETA footer textbox become blank
                        // txtETA.Text = Convert.ToDateTime(dt1.Rows[dt1.Rows.Count - 1]["ETA"]).ToString("dd/MM/yyyy");

                        HiddenField hidETAOrg = (HiddenField)grdDetails.FooterRow.FindControl("hidETAOrg");
                        hidETAOrg.Value = Convert.ToDateTime(dt1.Rows[dt1.Rows.Count - 1]["ETA"]).ToString("dd/MM/yyyy");

                        RequiredFieldValidator requiredStatus = (RequiredFieldValidator)grdDetails.FooterRow.FindControl("requiredStatus");
                        requiredStatus.ValidationGroup = "Dealer" + Convert.ToString(grdClientList.Rows.Count + 1);

                        RequiredFieldValidator requiredNotes = (RequiredFieldValidator)grdDetails.FooterRow.FindControl("requiredNotes");
                        requiredNotes.ValidationGroup = "Dealer" + Convert.ToString(grdClientList.Rows.Count + 1);

                        RegularExpressionValidator ss = (RegularExpressionValidator)grdDetails.FooterRow.FindControl("ss");
                        ss.ValidationGroup = "Dealer" + Convert.ToString(grdClientList.Rows.Count + 1);

                        //on 9 jan 2013. to add req field to ETA txt box
                        RequiredFieldValidator RFV_ETADateNew = (RequiredFieldValidator)grdDetails.FooterRow.FindControl("RFV_ETADateNew");
                        RFV_ETADateNew.ValidationGroup = "Dealer" + Convert.ToString(grdClientList.Rows.Count + 1);

                        ImageButton btnSubmit = (ImageButton)grdDetails.FooterRow.FindControl("btnSubmit");
                        btnSubmit.ValidationGroup = "Dealer" + Convert.ToString(grdClientList.Rows.Count + 1);


                        // logig for blocking nochange ETA and Submit buton 
                        DateTime lastUpdatedate = Convert.ToDateTime(dt1.Rows[dt1.Rows.Count - 1]["date"]);
                        if (lastUpdatedate.AddHours(Convert.ToInt32(ds.Tables[2].Rows[0]["value"])) > DateTime.Now)
                        {
                            grdDetails.FooterRow.Visible = true;
                        }
                        else
                        {
                            grdDetails.FooterRow.Visible = true;
                        }

                        // logig for blocking nochange ETA and Submit buton  ENd
                    }
                    else
                    {
                        e.Row.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(Convert.ToString(ex.Message));
            }
        }
    }

    public void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Visible = true;
                if (customerid != "")
                {
                    e.Row.Visible = false;
                }
                if (ViewState["_VTDStatus"] == null)
                {
                    dt = objCls_DealerClinets.getVDTStatus();
                    ViewState["_VTDStatus"] = dt;
                }
                else
                {
                    dt = (DataTable)ViewState["_VTDStatus"];
                }
                DropDownList drp = (DropDownList)e.Row.FindControl("drpStatus");

                drp.DataSource = dt;
                drp.DataTextField = "Status";
                drp.DataValueField = "ID";
                drp.DataBind();
                drp.Items.Add(new ListItem("-- Select --", "0"));
                drp.SelectedValue = "0";

                Label txt = (Label)e.Row.FindControl("txtDateOfUpdate");
                txt.Text = Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            int CustomerID = 0, StatusID = 0, DealerID = 0;
            DateTime ETA = DateTime.Today;
            string DealerNotes = string.Empty, ModifiedDate = string.Empty;

            ImageButton btnsubmit = (ImageButton)sender;
            GridViewRow container = (GridViewRow)btnsubmit.NamingContainer;
            GridView grdmain = (GridView)container.NamingContainer;

            DropDownList drp = (DropDownList)container.FindControl("drpStatus");
            string[] strarray = (((TextBox)container.FindControl("txtETA")).Text.Trim()).Split('/');

            ETA = DateTime.Parse(((TextBox)container.FindControl("txtETA")).Text.Trim(), culture);
            DealerNotes = ((TextBox)container.FindControl("txtNotes")).Text.Trim();
            DealerID = Convert.ToInt32(((HiddenField)grdmain.Rows[0].FindControl("hiddenDealerid")).Value);

            ModifiedDate = Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
            CustomerID = Convert.ToInt32(((Label)grdmain.Rows[0].FindControl("lblCustomerID")).Text);
            objCls_DealerClinets.CustomerID = CustomerID;
            objCls_DealerClinets.StatusID = Convert.ToInt32(drp.SelectedValue);
            objCls_DealerClinets.ETA = ETA;
            objCls_DealerClinets.DealerNotes = DealerNotes;
            objCls_DealerClinets.DealerID = DealerID;
            objCls_DealerClinets.ModifiedDate = ModifiedDate;
            objCls_DealerClinets.UpdatedBy = Convert.ToInt64(Convert.ToString(Session[Cls_Constants.LOGGED_IN_USERID]));
            int resulr_temp = objCls_DealerClinets.Save_VDTDealerStatus();

            // used for update the unmark fild when admin make car as delivered which is 2nd time
            if (Convert.ToInt32(Session[Cls_Constants.ROLE_ID]) == 1 && Convert.ToInt32(drp.SelectedValue) == 7)
            {
                Cls_VDTAdminReport objCls_VDTAdminReport = new Cls_VDTAdminReport();
                objCls_VDTAdminReport.CustomerId = Convert.ToInt64(CustomerID);
                int result = objCls_VDTAdminReport.UpdateAsUnmark();
                objCls_VDTAdminReport = null;
            }
            DataTable dtTempList = ((DataSet)ViewState["_clintList"]).Tables[0];
            ViewState["_clintList"] = null;
            binddata();

            // on 20 Mar 2013
            // If any one update ETA date before Todays date then send email to Catherine
            // and status is not Cat Delivered i.e. 7
            if (ETA < DateTime.Parse(System.DateTime.Now.Date.ToString("dd/MM/yyyy"), culture) && StatusID != 7)
                SendMail_CatherineOnOldETAUpdate(CustomerID);

            //logic for ETA Drastic chagne starte
            DataSet ds = new DataSet();
            ds = (DataSet)ViewState["_clintList"];
            DataView dv = new DataView(ds.Tables[1]);
            dv.RowFilter = "customerid=" + Convert.ToString(CustomerID);
            DataTable dt = new DataTable();
            dt = dv.ToTable();
            DateTime lastETADate;

            int difference_BetwenETADays = 0, Actual_WaitingTime = 0;
            double Fifty_percent_of_WaitingTime = 0;
            Boolean flag = false;
            if (dt != null)
            {
                lastETADate = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 2]["ETA"]);

                difference_BetwenETADays = Convert.ToInt32(ETA.Subtract(Convert.ToDateTime(dt.Rows[dt.Rows.Count - 2]["ETA"])).Days);
                Actual_WaitingTime = DateTime.Today.Subtract(lastETADate).Days;
                if (Actual_WaitingTime < 0)
                    Actual_WaitingTime = lastETADate.Subtract(DateTime.Today).Days;

                if (difference_BetwenETADays > 10)
                    flag = true;

                Fifty_percent_of_WaitingTime = Math.Ceiling(Convert.ToDouble(Actual_WaitingTime / 2));
                if (lastETADate.AddDays(Convert.ToInt32(Fifty_percent_of_WaitingTime + Actual_WaitingTime)) < ETA)
                    flag = true;

                // If car is delivered the status update mail was not send to client
                // as well as if ETA is changed drastically with status as delivered no drastic mail is send to admin.
                if (Convert.ToInt32(drp.SelectedValue) != 7)// provision of not sending mail when status is dellivered
                {
                    if (flag == true)
                    {
                        string ClientMobile = String.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["Mobile"])) ? "--" : Convert.ToString(dt.Rows[0]["Mobile"]);

                        lblOrderCancelConfirmation.Text = "This is a significant change in date.  If you haven’t already done, tt would be appreciated if you could contact the client by telephone  on " + ClientMobile + " to advise the reason for the change.  Thank you";
                        btnNo.Visible = false;
                        btnOk.Visible = false;
                        btnDrasticMsg.Visible = true;
                        divOrderCancelConfirm.Style.Add("display", "block");
                        objCls_DealerClinets.SendETA_DrasticChange_Email(dt, resulr_temp);
                        //Update mail send will be send after 4 hrs.
                        // other navigation are on OK btn of div divOrderCancelConfirm
                    }
                    else
                    {
                        // for normal update send immediately after update
                        SendMail_DealerUpdatesStatus(CustomerID);
                        // if change is not drastic then redirection
                        btnDrasticMsg_Click(null, null);
                    }
                }
            }

            //when admin makes the status as delivered send mail 
            if (Convert.ToInt32(drp.SelectedValue) == 7)
            {
                // on 7 mar 2013 as Delivered email goes only to Cust. Ser. Rep. no matter who update it 
                // either dealer or admin
                SendMail_CustomerSerRep(CustomerID, dtTempList);
                btnDrasticMsg_Click(null, null);
                return;
            }
            //logic for ETA Drastic chagne END

            //Sending Email to "Sue Hona" when Dealer/Admin update Customer status as "Car Arrived at dealership"  
            if (drp.SelectedItem.Value.Trim().Equals("4"))
            {
                string ClientName = !String.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["FullName"])) ? Convert.ToString(dt.Rows[0]["FullName"]) : "";
                SendMail_CarArrivedAtDealerShip(ClientName);
            }
            //End

            if (Convert.ToInt32(drp.SelectedValue) == 7)
            {
                Response.Redirect("ClinetIfo_ForDealer.aspx");
                return;
            }

            // 9 jan 2013 - manoj
            chkRepeatStatus(CustomerID);
        }
        catch (Exception ex)
        {
            logger.Error("User_Controls_UCDealers_ClientList btnSubmit_Click Error -  " + Convert.ToString(ex.Message));
        }
        finally
        { }
    }

    /// <summary>
    /// Manoj - 9 jan 2013 - To check Repeatative status for VDT and send mail accordingly. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void chkRepeatStatus(int CustomerID)
    {
        logger.Debug("Check Same Status");
        Cls_DealerClinets cls_DealerClinets = new Cls_DealerClinets();
        DataTable dt = new DataTable();
        try
        {
            cls_DealerClinets.CustomerID = CustomerID;
            dt = cls_DealerClinets.chkRepeatStatus();
            if (dt != null && dt.Rows.Count >= 2)
            {
                if (Convert.ToInt16(dt.Rows[0]["StatusID"]) == Convert.ToInt16(dt.Rows[1]["StatusID"]) && Convert.ToInt16(dt.Rows[0]["StatusID"]) != 1)
                {
                    //Send a warning to Catherine if the dropdown status remains the same for 2 consecutive updates EXCEPT for ‘Vehicle Ordered’ status

                    ConfigValues objConfigue = new ConfigValues();
                    StringBuilder message = new StringBuilder();

                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear Catherine,</p>");
                    message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Vehicle Delivery Tracker analytics found that following record updated with SAME STATUS consecutively for 2 times.");
                    message.Append("<br/><br/>Record details:");

                    message.Append("<br/>Customer Name : " + dt.Rows[0]["FullName"]);
                    message.Append("<br/>Email : " + dt.Rows[0]["UserName"] + "<br/>Make-Model : " + dt.Rows[0]["Make"] + " " + dt.Rows[0]["Model"]);
                    message.Append("<br/>Dealer Name : " + dt.Rows[0]["DealerName"] + "<br/>Dealer Company : " + dt.Rows[0]["DealerCompany"]);

                    message.Append("<br/><br/> Thanks<br/>Private Fleet</p>");

                    Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
                    clsemail.EmailBody = message.ToString();
                    clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
                    clsemail.EmailToID = "catherineheyes@privatefleet.com.au";
                    clsemail.EmailSubject = "Alert: Same status updated 2 times consecutively.";
                    clsemail.SendEmail();
                }

                if (Convert.ToInt16(dt.Rows[0]["StatusID"]) == Convert.ToInt16(dt.Rows[1]["StatusID"]) && Convert.ToDateTime(dt.Rows[0]["ETA"]) == Convert.ToDateTime(dt.Rows[1]["ETA"]) && Convert.ToString(dt.Rows[0]["DealerNotes"]) == Convert.ToString(dt.Rows[1]["DealerNotes"]))
                {
                    if (Convert.ToInt16(dt.Rows[1]["StatusID"]) == Convert.ToInt16(dt.Rows[2]["StatusID"]) && Convert.ToDateTime(dt.Rows[1]["ETA"]) == Convert.ToDateTime(dt.Rows[2]["ETA"]) && Convert.ToString(dt.Rows[1]["DealerNotes"]) == Convert.ToString(dt.Rows[2]["DealerNotes"]))
                    {
                        //Send a warning email to Catherine if we have three consecutive identical updates (Status, ETA & Notes)

                        ConfigValues objConfigue = new ConfigValues();
                        StringBuilder message = new StringBuilder();

                        message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear Catherine,</p>");
                        message.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Vehicle Delivery Tracker analytics found that following record updated with SAME STATUS,NOTES and ETA  Combination consecutively for 3 times.");
                        message.Append("<br/><br/>Record details:");

                        message.Append("<br/>Customer Name : " + dt.Rows[0]["FullName"]);
                        message.Append("<br/>Email : " + dt.Rows[0]["UserName"] + "<br/>Make-Model : " + dt.Rows[0]["Make"] + " " + dt.Rows[0]["Model"]);
                        message.Append("<br/>Dealer Name : " + dt.Rows[0]["DealerName"] + "<br/>Dealer Company : " + dt.Rows[0]["DealerCompany"]);

                        message.Append("");
                        message.Append("<br/><br/> Thanks<br/>Private Fleet</p>");

                        Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
                        clsemail.EmailBody = message.ToString();
                        clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
                        clsemail.EmailToID = "catherineheyes@privatefleet.com.au";
                        clsemail.EmailSubject = "Alert: Same Status, Notes and ETA updated 3 times consecutively.";
                        clsemail.SendEmail();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("chkRepeatStatus Error -" + ex.Message);
        }
        finally
        {
            cls_DealerClinets = null;
        }
    }

    public void btnDrasticMsg_Click(object sender, EventArgs e)
    {
        try
        {
            divOrderCancelConfirm.Style.Add("display", "none");

            string[] str1 = Request.Url.ToString().Split('/');
            if (string.IsNullOrEmpty(Request.QueryString["Mode"]))
            {
                string _dlrId = string.Empty, _eml = string.Empty;

                if (Session["DlrEmail"] != null && Session["DlrId"] != null)
                {
                    //FOR ADMIN 
                    _eml = !string.IsNullOrEmpty(Convert.ToString(Session["DlrEmail"])) ? Convert.ToString(Session["DlrEmail"]) : "";
                    _dlrId = !string.IsNullOrEmpty(Convert.ToString(Session["DlrId"])) ? Convert.ToString(Session["DlrId"]) : "";

                    if (Request.QueryString["ReqFrm"] != null && Request.QueryString["ReqFrm"].ToLower().Trim() == "custos")
                    {
                        Session["DlrEmail"] = null;
                        Session["DlrId"] = null;
                        string searchData = "custName=" + Request.QueryString["custName"] + "&DealerID=" + Request.QueryString["DealerID"] + "&custEmail=" + Request.QueryString["custEmail"] + "&phone=" + Request.QueryString["phone"] + "&make=" + Request.QueryString["make"] + "&orderstatus=" + Request.QueryString["orderstatus"] + "&pcontact=" + Request.QueryString["pcontact"] + "&fromPage=updateClient";
                        Response.Redirect("Admin_CustomerOrderStatus_PC.aspx?" + searchData, false);
                    }
                }
                else
                {
                    //FOR DEALER
                    Response.Redirect(str1[str1.Length - 1] + "&Mode=U", false);
                }
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["Mode"]))
            {
                Response.Redirect(str1[str1.Length - 1], false);
            }
        }
        catch (Exception ex)
        {
            logger.Error("btnDrasticMsg_Click : " + Convert.ToString(ex.Message));
        }
    }

    public void txtETA_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txt = (TextBox)sender;
            GridViewRow grd = (GridViewRow)txt.NamingContainer;
            HiddenField hiddenETAOringlvalue = (HiddenField)grd.FindControl("hidETAOrg");
            RequiredFieldValidator req = (RequiredFieldValidator)grd.FindControl("requiredNotes");
            ImageButton btnSubmit = (ImageButton)grd.FindControl("btnSubmit");
            btnSubmit.Enabled = true;
            Page page = HttpContext.Current.Handler as Page;

            string[] orginalarray = hiddenETAOringlvalue.Value.Split('/');
            string[] actualValue = txt.Text.Trim().Split('/');
            if (!txt.Equals(String.Empty))
            {
                DateTime oringalDate;
                DateTime ActualDate;
                oringalDate = DateTime.Parse(hiddenETAOringlvalue.Value, culture);
                ActualDate = DateTime.Parse(txt.Text.Trim(), culture);

                if (ActualDate != oringalDate)
                {
                    req.Enabled = true;
                }
                else
                {
                    req.Enabled = false;
                }

                // if (Convert.ToDateTime(actualValue[1] + "/" + actualValue[0] + "/" + actualValue[2]) <= Convert.ToDateTime(DateTime.Today))
                DateTime dtLast7Days = new DateTime();
                dtLast7Days = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToShortDateString());
                //if (ActualDate <= Convert.ToDateTime(DateTime.Today))
                if (ActualDate <= dtLast7Days)
                {
                    //  this.Page.RegisterClientScriptBlock("ss22", "ShowAlert()");
                    //txt.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txt.Text = oringalDate.ToString("dd/MM/yyyy");
                    btnSubmit.Enabled = false;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "ShowDateAlert('" + String.Format("{0:dd-MMM-yyyy}", dtLast7Days) + "');", true);
                    txt.Text = string.Empty;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Please enter ETA')", true);
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void grdClientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdClientList.PageIndex = e.NewPageIndex;
        binddata();
    }

    //No Change ETA Functionality
    public void btnNoChangeETA_Click(object sender, EventArgs e)
    {
        try
        {
            int CustomerID = 0, StatusID = 0, DealerID = 0;
            DateTime ETA = DateTime.Today;
            string DealerNotes, ModifiedDate;
            DealerNotes = ModifiedDate = string.Empty;

            CustomerID = Convert.ToInt32(((Label)grdClientList.Rows[0].FindControl("lblCustomerID")).Text);
            DataSet ds = new DataSet();
            ds = (DataSet)ViewState["_clintList"];
            DataView dv = new DataView(ds.Tables[1]);
            dv.RowFilter = "customerid=" + CustomerID.ToString();
            DataTable dt = new DataTable();
            dt = dv.ToTable();
            ETA = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["ETA"]);
            DealerNotes = Convert.ToString(dt.Rows[dt.Rows.Count - 1]["DealerNotes"]);
            DealerID = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["dealerid"]);

            ModifiedDate = Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
            objCls_DealerClinets.CustomerID = CustomerID;
            objCls_DealerClinets.StatusID = Convert.ToInt32(Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["statusid"]));
            objCls_DealerClinets.ETA = ETA;
            objCls_DealerClinets.DealerNotes = DealerNotes;
            objCls_DealerClinets.DealerID = DealerID;
            objCls_DealerClinets.ModifiedDate = ModifiedDate;
            objCls_DealerClinets.UpdatedBy = Convert.ToInt64(Convert.ToString(Session[Cls_Constants.LOGGED_IN_USERID]));
            objCls_DealerClinets.Save_VDTDealerStatus();

            ViewState["_clintList"] = null;
            binddata();
            SendMail_DealerUpdatesStatus(CustomerID);

            // on 20 Mar 2013
            // If any one update ETA date before Todays date then send email to Catherine
            // and status is not Cat Delivered i.e. 7
            if (ETA < DateTime.Parse(System.DateTime.Now.Date.ToString("dd/MM/yyyy"), culture) && StatusID != 7)
                SendMail_CatherineOnOldETAUpdate(CustomerID);

            string[] str1 = Request.Url.ToString().Split('/');
            string _dlrId = string.Empty, _eml = string.Empty;

            if (Session["DlrEmail"] != null && Session["DlrId"] != null)
            {
                //FOR ADMIN 
                //_eml = !string.IsNullOrEmpty(Convert.ToString(Session["DlrEmail"])) ? Convert.ToString(Session["DlrEmail"]) : "";
                //_dlrId = !string.IsNullOrEmpty(Convert.ToString(Session["DlrId"])) ? Convert.ToString(Session["DlrId"]) : "";

                //Response.Redirect("ClinetIfo_ForDealer.aspx?dlrId=" + _dlrId + "&eml=" + _eml + "&Mode=U", false);
                btnDrasticMsg_Click(null, null);
            }
            else
            {
                //FOR DEALER
                Response.Redirect(str1[str1.Length - 1] + "&Mode=U", false);
            }

            // 9 jan 2013 - manoj
            chkRepeatStatus(CustomerID);
        }
        catch (Exception ex)
        {
            logger.Error("User_Controls_UCDealers_ClientList btnNoChangeETA_Click Error -  " + Convert.ToString(ex.Message));
        }

    }

    public void grdCustomerListOnly_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfigValues objConfigue = new ConfigValues();
            int LowerLimit = 0, MiddleLimit = 0, UppeLimit = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hypcustomerlinkview = (HyperLink)e.Row.FindControl("hypcustomerlinkview");
                hypcustomerlinkview.NavigateUrl = "~/ClinetIfo_ForDealer.aspx?tempid=" + hypcustomerlinkview.NavigateUrl;
                //Sachin code added on 06/02/2012
                // As per the mail to show customer with new colored status -Start
                HiddenField hidden = (HiddenField)e.Row.FindControl("hiddenDealerStatus");
                HiddenField tempHiddenCustomerRequest = (HiddenField)e.Row.FindControl("HiddenCustomerRequest");
                HiddenField hdfDateDiff = (HiddenField)e.Row.FindControl("hdfDateDiff");
                DateTime DeliveryDate = Convert.ToDateTime(((Label)e.Row.FindControl("lbleta")).Text);

                TimeSpan dateDiff = DeliveryDate.Subtract(System.DateTime.Now);

                if (dateDiff.Days <= 50)
                {
                    objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS";
                    LowerLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                    objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS_MIDDLE";
                    MiddleLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                    objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS";
                    UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                }
                else if (dateDiff.Days > 50 && dateDiff.Days <= 120)
                {
                    objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS_50_TO_120";
                    LowerLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                    objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS_MIDDLE_50_TO_120";
                    MiddleLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                    objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS_50_TO_120";
                    UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                }
                else if (dateDiff.Days > 120)
                {
                    objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS_MORE_120";
                    LowerLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                    objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS_MIDDLE_MORE_120";
                    MiddleLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                    objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS_MORE_120";
                    UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                }

                if (Convert.ToInt32(hdfDateDiff.Value) >= LowerLimit && Convert.ToInt32(hdfDateDiff.Value) < MiddleLimit)
                {
                    e.Row.Style.Add("Background-Color", "#FFF7CC");
                }
                else if (Convert.ToInt32(hdfDateDiff.Value) >= MiddleLimit && Convert.ToInt32(hdfDateDiff.Value) < UppeLimit)
                {
                    e.Row.CssClass = "nearDanger";
                }
                else if (Convert.ToInt32(hdfDateDiff.Value) >= UppeLimit)
                {
                    e.Row.CssClass = "Danger";
                }

                //if (hidden.Value.ToLower().Trim() == "dealerupdatereq")
                //{
                //    e.Row.Style.Add("Background-Color", "#FFF7CC");
                //}
                //else if (hidden.Value.ToLower().Trim() == "dealerneartolock")
                //{
                //    e.Row.CssClass = "nearDanger";
                //}
                //else
                //{
                //    if (hidden.Value.ToLower().Trim() == "dealerlock")
                //    {
                //        e.Row.CssClass = "Danger";
                //    }

                //}

                if (tempHiddenCustomerRequest.Value.ToLower().Trim() == "show_customerrequest")
                {
                    if (hidden.Value.ToLower().Trim() != "dealerlock")
                    {
                        e.Row.CssClass = "ClientRequest_Dealer";
                    }
                }
                // As per the mail to show customer with new colored status -ENd
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message.ToString()));
        }
    }

    public void grdCustomerListOnly_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;

            //Swap sort direction
            this.DefineSortDirection();

            // BindData(objCourseMaster);
            binddata();
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void grdCustomerListOnly_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdCustomerListOnly.PageIndex = e.NewPageIndex;
            if (ViewState["_clintList"] == null)
            {
                ds = objCls_DealerClinets.getDealerClinetsListOnly();
            }
            else
            {
                ds = (DataSet)ViewState["_clintList"];
            }
            dt = ds.Tables[0].Copy();
            DataView dv = dt.DefaultView;
            if (Convert.ToInt32(Session[Cls_Constants.ROLE_ID]) == 2) // used for not showing unmarked customer to dealer
            {
                dv.RowFilter = "Unmark=0 AND IsActive=1 AND CStatus<>7";
            }
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
            grdCustomerListOnly.DataSource = dt;
            grdCustomerListOnly.DataBind();

        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["_clintList"] == null)
            {
                ds = objCls_DealerClinets.getDealerClinetsListOnly();
            }
            else
            {
                ds = (DataSet)ViewState["_clintList"];
            }


            dt = ds.Tables[0].Copy();
            DataView dv = dt.DefaultView;
            if (Convert.ToInt32(Session[Cls_Constants.ROLE_ID]) == 2) // used for not showing unmarked customer to dealer
            {
                dv.RowFilter = "Unmark=0 AND IsActive=1 AND CStatus<>7";
            }
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();




            grdCustomerListOnly.PageIndex = 0;
            if (ddl_NoRecords2.SelectedValue.ToString() == "All")
            {
                //For view 1
                grdCustomerListOnly.PageSize = grdCustomerListOnly.PageCount * grdCustomerListOnly.Rows.Count;
                grdCustomerListOnly.DataSource = dt;
                grdCustomerListOnly.DataBind();
            }
            else
            {
                //for view 1
                grdCustomerListOnly.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
                grdCustomerListOnly.DataSource = dt;
                grdCustomerListOnly.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void imgbtnBack_Click(object sender, EventArgs e)
    {
        try
        {
            string _dlrId = string.Empty, _eml = string.Empty;

            if (Request.QueryString["ReqFrm"] != null && Request.QueryString["ReqFrm"].ToLower().Trim() == "custos")
            {
                Session["DlrEmail"] = null;
                Session["DlrId"] = null;
                string searchData = "custName=" + Request.QueryString["custName"] + "&DealerID=" + Request.QueryString["DealerID"] + "&custEmail=" + Request.QueryString["custEmail"] + "&phone=" + Request.QueryString["phone"] + "&make=" + Request.QueryString["make"] + "&orderstatus=" + Request.QueryString["orderstatus"] + "&pcontact=" + Request.QueryString["pcontact"] + "&fromPage=updateClient";

                Response.Redirect("Admin_CustomerOrderStatus_PC.aspx?" + searchData, false);
            }
            else if (Request.QueryString["ReqFrm"] != null && Request.QueryString["ReqFrm"].ToLower().Trim() == "etacoming")
            {
                Session["DlrEmail"] = null;
                Session["DlrId"] = null;
                Response.Redirect("Admin_DeliveryInNxt10Days.aspx?PC=" + Request.QueryString["PC"], false);
            }


            if (Convert.ToString(Session[Cls_Constants.ROLE_ID]).Equals("1"))
            {
                if (Session["DlrEmail"] != null && Session["DlrId"] != null)
                {
                    _eml = !string.IsNullOrEmpty(Convert.ToString(Session["DlrEmail"])) ? Convert.ToString(Session["DlrEmail"]) : "";
                    _dlrId = !string.IsNullOrEmpty(Convert.ToString(Session["DlrId"])) ? Convert.ToString(Session["DlrId"]) : "";

                    Response.Redirect("ClinetIfo_ForDealer.aspx?dlrId=" + _dlrId + "&eml=" + _eml, false);
                }
            }
            else
            {
                Response.Redirect("ClinetIfo_ForDealer.aspx", false);
            }

        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    /// <summary>
    ///Added By Archana
    ///Date : 23 March 2012
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void imgBtnBacktoMainRpt_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session[Cls_Constants.ROLE_ID]).Equals("1"))
            {
                string _company = string.Empty, _make = string.Empty;
                _company = !string.IsNullOrEmpty(Convert.ToString(Session["Cmpny"])) ? Convert.ToString(Session["Cmpny"]) : "";
                _make = !string.IsNullOrEmpty(Convert.ToString(Session["make"])) ? Convert.ToString(Session["make"]) : "";

                Session["Cmpny"] = null;
                Session["make"] = null;
                Session["DlrEmail"] = null;
                Session["DlrId"] = null;

                Response.Redirect("Admin_DealerSummaryReport.aspx?cmpny=" + _company + "&make=" + _make, false);
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            msgpop.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            logger.Error("btnClose_Click : " + Convert.ToString(ex.Message));
        }
    }

    public void imgBtnCancelOrder_Click(object sender, EventArgs e)
    {
        try
        {
            configureEmailContent();
            divOrderCancelConfirm.Style.Add("display", "block");
        }
        catch (Exception ex)
        {
            logger.Error("imgBtnCancelOrder_Click : " + Convert.ToString(ex.Message));
        }
    }

    public void btnYes_Click(object sender, EventArgs e)
    {
        lblMessage.Text = string.Empty;
        Cls_DealerClinets objDealerClients = new Cls_DealerClinets();
        Int32 _customerID = 0;
        string DealerName = String.Empty, ClientName = string.Empty, DealerEmail = String.Empty;
        string _Make = String.Empty, _Model = string.Empty;


        try
        {
            if (Request.QueryString["tempid"] != null)
            {
                if (Request.QueryString["tempid"].Length > 0)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(Request.QueryString["tempid"])))
                    {
                        _customerID = Convert.ToInt32(Request.QueryString["tempid"]);
                    }
                }
            }
            objDealerClients.CustomerID = _customerID;
            objDealerClients.OrderCancelledBy = Convert.ToInt64(Session[Cls_Constants.LOGGED_IN_USERID]);

            if (ViewState["_clintList"] != null)
            {
                DataSet dsClientLst = (DataSet)ViewState["_clintList"];
                DataTable dtClientDetails = new DataTable();
                dtClientDetails = dsClientLst.Tables[0];
                DataRow[] dr = dtClientDetails.Select("id=" + _customerID);
                if (dr != null && dr.Length > 0)
                {
                    DealerName = Convert.ToString(dr[0]["DealerName"]);
                    ClientName = Convert.ToString(dr[0]["fullname"]);
                    DealerEmail = Convert.ToString(dr[0]["DealerEmail"]);
                    _Make = Convert.ToString(dr[0]["Make"]);
                    _Model = Convert.ToString(dr[0]["Model"]);
                }
            }
            else
            {
                return;
            }

            if (objDealerClients.CancelOrder())
            {
                Cls_DealerClinets objCls_DealerClinets = new Cls_DealerClinets();

                if (!string.IsNullOrEmpty(hdnDealerEmail.Value))
                {
                    objCls_DealerClinets.Email = hdnDealerEmail.Value;
                }
                else if (Session["DlrEmail"] != null)
                {
                    objCls_DealerClinets.Email = Convert.ToString(Session["DlrEmail"]);
                }
                else
                {
                    objCls_DealerClinets.Email = Convert.ToString(Session[Cls_Constants.USER_NAME]);
                }

                objCls_DealerClinets.flag = 0;
                DataSet ds = objCls_DealerClinets.getDealerClinetsListOnly();

                DataTable dtNew = new DataTable();
                dtNew = ds.Tables[0];
                DataView dvNew = dtNew.DefaultView;

                if (Session["DlrId"] != null && !string.IsNullOrEmpty(Convert.ToString(Session["DlrId"])))
                {
                    dvNew = dtNew.DefaultView;
                    dvNew.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
                    dvNew.RowFilter = "DealerID=" + Convert.ToString(Session["DlrId"]);
                }

                if (dvNew.Count > 0)
                {
                    grdCustomerListOnly.DataSource = dvNew.ToTable();
                    grdCustomerListOnly.DataBind();
                }
                else
                {
                    grdCustomerListOnly.DataSource = null;
                    grdCustomerListOnly.DataBind();
                }
                lblMessage.Text = "Order of Client " + ClientName + " cancelled.";
                pnlCustomerDetail.Visible = false;
                pnlCustomerList.Visible = true;

                //Send email to catherine as well as dealer of Cancel order
                SendMail_OrderCancel(DealerName, DealerEmail, ClientName, _Make, _Model);
            }
            else if (!objDealerClients.CancelOrder())
            {
                lblMessage.Text = "Unable to cancel Order of Client " + ClientName;
            }
            divOrderCancelConfirm.Style.Add("display", "none");
            if (Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 1)// if order cancel by admin        
                imgbtnBack_Click(null, null);
            else // if by dealer
                Response.Redirect("ClinetIfo_ForDealer.aspx");
        }
        catch (Exception ex)
        {
            logger.Error("imgBtnCancelOrder_Click : " + Convert.ToString(ex.Message));
        }
        finally
        {
            divOrderCancelConfirm.Style.Add("display", "none");
        }
    }

    public void btnNo_Click(object sender, EventArgs e)
    {
        try
        {
            divOrderCancelConfirm.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            logger.Error("btnNo_Click : " + Convert.ToString(ex.Message));
        }
    }

    public void btnEditDealerIp_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditDealerIp = (ImageButton)sender;
            GridViewRow container = (GridViewRow)btnEditDealerIp.NamingContainer;
            GridView grdmain = (GridView)container.NamingContainer;

            //Label lblBuildYear1 = (Label)container.FindControl("lblBuildYear1");
            //Label lblComplianceYear1 = (Label)container.FindControl("lblComplianceYear1");
            //Label lblStockNo1 = (Label)container.FindControl("lblStockNo1");

            TextBox txtBuildYear = (TextBox)container.FindControl("txtBuildYear");
            TextBox txtComplianceYear = (TextBox)container.FindControl("txtComplianceYear");
            TextBox txtStockNo = (TextBox)container.FindControl("txtStockNo");

            ImageButton btnSaveDealerIp = (ImageButton)container.FindControl("btnSaveDealerIp");
            ImageButton btnCancelDealerIp = (ImageButton)container.FindControl("btnCancelDealerIp");

            //lblBuildYear1.Visible = false;
            //lblComplianceYear1.Visible = false;
            //lblStockNo1.Visible = false;

            txtBuildYear.Enabled = true;
            txtComplianceYear.Enabled = true;
            txtStockNo.Enabled = true;

            btnEditDealerIp.Visible = false;
            btnSaveDealerIp.Visible = true;
            btnCancelDealerIp.Visible = true;
        }
        catch (Exception ex)
        {
            logger.Error("btnEditDealerIp_Click : " + Convert.ToString(ex.Message));
        }
    }

    public void btnSaveDealerIp_Click(object sender, EventArgs e)
    {
        objCls_DealerClinets = new Cls_DealerClinets();
        try
        {
            ImageButton btnEditDealerIp = (ImageButton)sender;
            GridViewRow container = (GridViewRow)btnEditDealerIp.NamingContainer;
            GridView grdmain = (GridView)container.NamingContainer;

            TextBox txtBuildYear = (TextBox)container.FindControl("txtBuildYear");
            TextBox txtComplianceYear = (TextBox)container.FindControl("txtComplianceYear");
            TextBox txtStockNo = (TextBox)container.FindControl("txtStockNo");

            ImageButton btnSaveDealerIp = (ImageButton)container.FindControl("btnSaveDealerIp");
            ImageButton btnCancelDealerIp = (ImageButton)container.FindControl("btnCancelDealerIp");

            Int32 CustomerID = Convert.ToInt32(((Label)grdmain.Rows[0].FindControl("lblCustomerID")).Text);
            objCls_DealerClinets.StockNo = txtStockNo.Text.Trim();
            objCls_DealerClinets.BuildYear = txtBuildYear.Text.Trim();
            objCls_DealerClinets.ComplianceYear = txtComplianceYear.Text.Trim();
            objCls_DealerClinets.ChangeBy = Convert.ToInt64(Session[Cls_Constants.LOGGED_IN_USERID]);
            objCls_DealerClinets.CustomerID = CustomerID;
            int result = objCls_DealerClinets.SaveVDTDealerInput();

            ViewState["_clintList"] = null;
            binddata();


        }
        catch (Exception ex)
        {
            logger.Error("btnSaveDealerIp_Click : " + Convert.ToString(ex.Message));
        }
    }

    public void btnCancelDealerIp_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                ImageButton btnCancelDealerIp = (ImageButton)sender;
                GridViewRow container = (GridViewRow)btnCancelDealerIp.NamingContainer;
                GridView grdmain = (GridView)container.NamingContainer;

                //Label lblBuildYear1 = (Label)container.FindControl("lblBuildYear1");
                //Label lblComplianceYear1 = (Label)container.FindControl("lblComplianceYear1");
                //Label lblStockNo1 = (Label)container.FindControl("lblStockNo1");

                TextBox txtBuildYear = (TextBox)container.FindControl("txtBuildYear");
                TextBox txtComplianceYear = (TextBox)container.FindControl("txtComplianceYear");
                TextBox txtStockNo = (TextBox)container.FindControl("txtStockNo");

                ImageButton btnSaveDealerIp = (ImageButton)container.FindControl("btnSaveDealerIp");
                ImageButton btnEditDealerIp = (ImageButton)container.FindControl("btnEditDealerIp");

                //lblBuildYear1.Visible = true;
                //lblComplianceYear1.Visible = true;
                //lblStockNo1.Visible = true;

                txtBuildYear.Enabled = false;
                txtComplianceYear.Enabled = false;
                txtStockNo.Enabled = false;

                btnEditDealerIp.Visible = true;
                btnSaveDealerIp.Visible = false;
                btnCancelDealerIp.Visible = false;

                binddata();
            }
            catch (Exception ex)
            {
                logger.Error("btnCancelDealerIp_Click : " + Convert.ToString(ex.Message));
            }
        }
        catch (Exception ex)
        {
            logger.Error("btnEditDealerIp_Click : " + Convert.ToString(ex.Message));
        }
    }

    #endregion

    #region Methods

    public void binddata()
    {
        try
        {
            imgbtnBack.Visible = true;
            DataView dv = new DataView();

            if (!string.IsNullOrEmpty(hdnDealerEmail.Value))
            {
                objCls_DealerClinets.Email = hdnDealerEmail.Value;
            }
            else if (Session["DlrEmail"] != null)
            {
                objCls_DealerClinets.Email = Convert.ToString(Session["DlrEmail"]);
            }
            else
            {
                objCls_DealerClinets.Email = Convert.ToString(Session[Cls_Constants.USER_NAME]);
                if (Convert.ToInt32(Session[Cls_Constants.ROLE_ID]) == 1)
                    objCls_DealerClinets.Email = "Admin";
            }

            objCls_DealerClinets.flag = 0;
            //added on 26 oct 12 for the dealer whoes Email was deleted to deactive it from ACT
            objCls_DealerClinets.CustomerID = Convert.ToInt32(Request.QueryString["tempid"]);
            if (ViewState["_clintList"] == null)
            {
                ds = objCls_DealerClinets.getDealerClinetsListOnly();
            }
            else
            {
                ds = (DataSet)ViewState["_clintList"];
            }

            dt = ds.Tables[0].Copy();
            dv = dt.DefaultView;
            if (Convert.ToInt32(Session[Cls_Constants.ROLE_ID]) == 2) // used for not showing unmarked customer to dealer
            {
                dv.RowFilter = "Unmark=0 AND IsActive=1 AND CStatus<>7";
            }
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();


            #region Added by Archana
            if (Session["DlrId"] != null && !string.IsNullOrEmpty(Convert.ToString(Session["DlrId"])))
            {
                DataView dvDlr = new DataView();
                dvDlr = dt.DefaultView;
                dvDlr.RowFilter = "DealerID=" + Convert.ToString(Session["DlrId"]);

                grdCustomerListOnly.DataSource = dvDlr.ToTable();
                grdCustomerListOnly.DataBind();
            }
            else
            {
                grdCustomerListOnly.DataSource = dt;
                grdCustomerListOnly.DataBind();
            }
            #endregion

            ViewState["_clintList"] = ds;
            dt = ds.Tables[0].Copy();
            pnlCustomerList.Visible = true;
            pnlCustomerDetail.Visible = false;

            if (Request.QueryString["tempid"] != null)
            {
                pnlCustomerList.Visible = false;
                pnlCustomerDetail.Visible = true;
                dv = new DataView(dt);
                dv.RowFilter = "ID=" + Convert.ToString(Request.QueryString["tempid"]);
                dt = dv.ToTable();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                grdClientList.DataSource = dt;
                grdClientList.DataBind();

                lblRowsToDisplay2.Visible = true;
                ddl_NoRecords2.Visible = true;
            }
            else
            {
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
                grdClientList.DataSource = null;
                grdClientList.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    /// <summary>
    /// Used to send an email to Customer Service Rep. when dealer change the status to Delivered
    /// </summary>
    /// <param name="id"></param>
    public void SendMail_CustomerSerRep(int id, DataTable dtTemp)
    {
        try
        {
            dt = new DataTable();
            DataView dv = new DataView(dtTemp);
            dv.RowFilter = "ID=" + id;
            dt = dv.ToTable();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string message = "";
                    string emailid = "";
                    string BccEmailid = string.Empty;
                    string username = "";
                    string status = "";
                    string fullname = "";

                    emailid = Convert.ToString(dt.Rows[0]["CustSerRepEmail"]);

                    username = Convert.ToString(dt.Rows[0]["FirstName"]);
                    //status = Convert.ToString(dt.Rows[0]["Status"]);
                    fullname = Convert.ToString(dt.Rows[0]["fullname"]);
                    dv = new DataView(ds.Tables[1]);
                    dv.RowFilter = "Customerid=" + id;
                    dv.Sort = "date desc";
                    dt = dv.ToTable();
                    status = Convert.ToString(dt.Rows[0]["Status"]);
                    message = "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + username + ",</p>";
                    message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>This is to advise that the supplying dealer of your new car has just updated the delivery status.</p>";
                    message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>The current " + "<b>estimated</b>" + " delivery date is " + Convert.ToDateTime(Convert.ToString(dt.Rows[0]["ETA"])).ToString("dd/MM/yyyy") + "</p>";
                    if (Convert.ToString(dt.Rows[0]["DealerNotes"]).Trim() != "")
                    {
                        message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dealer Notes</br>" + Convert.ToString(dt.Rows[0]["DealerNotes"]).Trim() + "</p>";
                    }

                    message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Remember for any clarification or to see a history of the delivery process of your new car, you can always log on to <a href='" + Convert.ToString(ConfigurationManager.AppSettings["VDTCustomerLoginURL"]) + "' target='_Blank'>updates.privatefleet.com.au</a> using your surname ( ";
                    message = message + dt.Rows[0]["lastname"] + " )and your Private Fleet member number ( " + Convert.ToString(dt.Rows[0]["MemberId"]).Trim() + " )</p>";

                    message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><br /><br />Best Regards</p>";
                    message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><a href='http://www.privatefleet.com.au/' target='_Blank'>www.privatefleet.com.au</a><br /><br /></p>";

                    Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
                    clsemail.EmailBody = message;

                    if (Convert.ToInt64(Session[Cls_Constants.ROLE_ID]).Equals(1))
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(Convert.ToString(dt.Rows[0]["DealerEmail"]))))
                        {
                            clsemail.EmailFromID = Convert.ToString(dt.Rows[0]["name"]) + "<" + Convert.ToString(dt.Rows[0]["DealerEmail"]) + ">"; ;//Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
                        }
                        else
                        {
                            clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
                        }
                    }
                    else if (Convert.ToInt64(Session[Cls_Constants.ROLE_ID]).Equals(2))
                    {
                        clsemail.EmailFromID = Convert.ToString(dt.Rows[0]["name"]) + "<" + Convert.ToString(Session[Cls_Constants.USER_NAME]) + ">";
                    }

                    clsemail.EmailToID = emailid;
                    // clsemail.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
                    clsemail.EmailSubject = "Status updated by Dealer.";
                    clsemail.SendEmail();
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void SendMail_DealerUpdatesStatus(int id)
    {
        try
        {
            DataSet ds = (DataSet)ViewState["_clintList"];
            dt = new DataTable();
            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "ID=" + id;
            dt = dv.ToTable();
            if (dt != null)
            {
                // If dealer is deactive do not send any mail to dealer
                if (dt.Rows.Count == 1 && Convert.ToString(dt.Rows[0]["DealerActive"]).ToLower() == "true")
                {
                    string message = "";
                    string emailid = "";
                    string BccEmailid = string.Empty;
                    string username = "";
                    string status = "";
                    string fullname = "";

                    emailid = Convert.ToString(dt.Rows[0]["email"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["Email_2"])))
                    {
                        BccEmailid = Convert.ToString(dt.Rows[0]["Email_2"]);
                    }
                    username = Convert.ToString(dt.Rows[0]["FirstName"]);
                    //status = Convert.ToString(dt.Rows[0]["Status"]);
                    fullname = Convert.ToString(dt.Rows[0]["fullname"]);
                    dv = new DataView(ds.Tables[1]);
                    dv.RowFilter = "Customerid=" + id;
                    dv.Sort = "date desc";
                    dt = dv.ToTable();
                    status = Convert.ToString(dt.Rows[0]["Status"]);
                    message = "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + username + ",</p>";
                    message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>This is to advise that the supplying dealer of your new car has just updated the delivery status.</p>";
                    message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>The current " + "<b>estimated</b>" + " delivery date is " + Convert.ToDateTime(Convert.ToString(dt.Rows[0]["ETA"])).ToString("dd/MM/yyyy") + "</p>";
                    if (Convert.ToString(dt.Rows[0]["DealerNotes"]).Trim() != "")
                    {
                        message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dealer Notes</br>" + Convert.ToString(dt.Rows[0]["DealerNotes"]).Trim() + "</p>";
                    }

                    message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Remember for any clarification or to see a history of the delivery process of your new car, you can always log on to <a href='" + Convert.ToString(ConfigurationManager.AppSettings["VDTCustomerLoginURL"]) + "' target='_Blank'>updates.privatefleet.com.au</a> using your surname ( ";
                    message = message + dt.Rows[0]["lastname"] + " )and your Private Fleet member number ( " + Convert.ToString(dt.Rows[0]["MemberId"]).Trim() + " )</p>";

                    message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><br /><br />Best Regards</p>";
                    message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><a href='http://www.privatefleet.com.au/' target='_Blank'>www.privatefleet.com.au</a><br /><br /></p>";

                    Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
                    clsemail.EmailBody = message;

                    if (Convert.ToInt64(Session[Cls_Constants.ROLE_ID]).Equals(1))
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(Convert.ToString(dt.Rows[0]["DealerEmail"]))))
                        {
                            clsemail.EmailFromID = Convert.ToString(dt.Rows[0]["name"]) + "<" + Convert.ToString(dt.Rows[0]["DealerEmail"]) + ">"; ;//Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
                        }
                        else
                        {
                            clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
                        }
                    }
                    else if (Convert.ToInt64(Session[Cls_Constants.ROLE_ID]).Equals(2))
                    {
                        clsemail.EmailFromID = Convert.ToString(dt.Rows[0]["name"]) + "<" + Convert.ToString(Session[Cls_Constants.USER_NAME]) + ">";
                    }

                    clsemail.EmailToID = emailid;
                    clsemail.EmailBccID = BccEmailid;
                    clsemail.EmailSubject = "Status updated by Dealer.";
                    clsemail.SendEmail();
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void binddata1()
    {
        try
        {
            imgbtnBack.Visible = false;
            pnlCustomerList.Visible = false;
            pnlCustomerDetail.Visible = true;
            DataView dv = new DataView();
            objCls_DealerClinets.Email = Convert.ToString(username);
            objCls_DealerClinets.flag = 1;
            ds = objCls_DealerClinets.getDealerClinetsListOnly();

            ViewState["_clintList"] = ds;
            dt = ds.Tables[0].Copy();

            dv = new DataView(dt);
            dv.RowFilter = "ID=" + Convert.ToString(customerid);
            dt = dv.ToTable();

            grdClientList.ShowFooter = false;

            if (ds.Tables[1].Rows.Count > 0)
            {
                grdClientList.DataSource = dt;
                grdClientList.DataBind();

                ((GridView)grdClientList.Rows[0].FindControl("grdDetails")).FooterRow.Visible = false;
            }
            else
            {
                grdClientList.DataSource = null;
                grdClientList.DataBind();
            }
            grdClientList.ShowFooter = false;
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    private void DefineSortDirection()
    {
        try
        {
            if (ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] != null)
            {
                if (ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString() == Cls_Constants.VIEWSTATE_ASC)
                {
                    ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
                }
                else
                {
                    ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    /// <summary>
    /// Created By : Archana : 19 April 2012
    /// Details : Whenever dealer updates in VDT OR vehicle status changes 
    /// to "Car Arrived at DealerShip" in VDT we will send automatic email to "suehona@privatefleet.com.au".
    /// </summary>
    private void SendMail_CarArrivedAtDealerShip(string ClientName)
    {
        try
        {
            string emailid = string.Empty, message = string.Empty;
            //Get Email Id of Sue from DataBase
            ConfigValues objConfigue = new ConfigValues();
            objConfigue.Key = "EMAIL_TO_SUE_CUSTOMER_CAR_STATUS_IS_IN_STOCK";
            emailid = objConfigue.GetValue(objConfigue.Key).ToString();
            //End
            message = "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Hi Sue " + ",</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Please be advised that " + ClientName + " vehicle has arrived in stock.";
            message = message + "If you have sold this client aftermarket please get in touch with the dealership ASAP to ensure it is arranged before delivery.</p>";

            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><br />Thanks,</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Private Fleet - Car Buying Made Easy<br /><a href='http://www.privatefleet.com.au/' target='_Blank'>www.privatefleet.com.au</a><br /><br /></p>";

            Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
            clsemail.EmailBody = message;
            clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
            clsemail.EmailToID = emailid;
            clsemail.EmailSubject = "Updates: Vehicle In Stock.";
            clsemail.SendEmail();

        }
        catch (Exception ex)
        {
            logger.Error("SendMail_CarArrivedAtDealerShip : " + Convert.ToString(ex.Message));
        }
    }

    /// <summary>
    /// Created By : Archana : 19 April 2012
    /// Details :Sending Email to the Catherine as Dealer Cancelling the order of the Client. 
    /// </summary>
    private void SendMail_OrderCancel(string DealerName, string DealerEmail, string ClientName, string _make, string _model)
    {
        try
        {
            string emailid = string.Empty, message = string.Empty;
            string EmailDealDetails = string.Empty;
            if (ViewState["EmailData"] != null)
            {
                EmailDealDetails = Convert.ToString((StringBuilder)ViewState["EmailData"]);
            }
            //Get Email Id of Sue from DataBase i.e. of Catherine
            ConfigValues objConfigue = new ConfigValues();
            objConfigue.Key = "EMAIL_TO_ADMIN_DRASTIC_CHANGE_IN_ETA";
            emailid = objConfigue.GetValue(objConfigue.Key).ToString();
            //End
            message = "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear Catherine" + ",</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dealer " + DealerName + " has advised that Client " + ClientName + " has cancelled.";

            message = message + "<br />Following are the details :</p><br /><br />";
            message = message + EmailDealDetails + "<br /><br />";

            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'><br />Thanks,</p>";
            message = message + "<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Private Fleet - Car Buying Made Easy<br /><a href='http://www.privatefleet.com.au/' target='_Blank'>www.privatefleet.com.au</a><br /><br /></p>";

            Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
            clsemail.EmailBody = message;
            clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
            clsemail.EmailToID = DealerEmail; // to dealer
            clsemail.EmailCcID = emailid; // cc to cathrine
            clsemail.EmailSubject = "Order Cancelled by Dealer.";
            clsemail.SendEmail();
        }
        catch (Exception ex)
        {
            logger.Error("SendMail_OrderCancel : " + Convert.ToString(ex.Message));
        }
    }

    private void configureEmailContent()
    {
        try
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<table rules='all' cellspacing='0' style='width: 100%; border-collapse: collapse;'>");
            //str.Append("<tr style='height: 100px;'><td width=100%><table width=100%><td valign='top' style='width: 100%;'><table width=100%>");
            ////border='1'
            foreach (GridViewRow grdRow in grdClientList.Rows)
            {

                if (grdClientList.Rows.Count == 1)
                {
                    #region Customer Personal Info
                    Label lblCustomerNameValue = (Label)grdRow.FindControl("lblCustomerNameValue");
                    Label lblEmailValue = (Label)grdRow.FindControl("lblEmailValue");
                    Label lblphoneValue = (Label)grdRow.FindControl("lblphoneValue");
                    Label lblmobile = (Label)grdRow.FindControl("lblmobile");
                    Label lblmakevalue = (Label)grdRow.FindControl("lblmakevalue");
                    Label lblModel = (Label)grdRow.FindControl("Label1");

                    //  str.Append("<tr style='height: 100px;'><td><table width='100%'><tr><td>");
                    str.Append("<table cellpadding='3px' width='100%' style='background-color: rgb(213, 236, 253); color: Black;'>");
                    str.Append("<tr>");
                    str.Append("<td align='left' style='width: 18%;'>");
                    str.Append("<span style='font-weight:bold'>Customer Name </span></td>");
                    str.Append("<td align='left' colspan='3' style='padding-left: 5px;'>");
                    str.Append(lblCustomerNameValue.Text + "</td></tr>");

                    str.Append("<tr>");
                    str.Append("<td align='left'>");
                    str.Append("<span style='font-weight:bold'>Email ID </span></td>");
                    str.Append("<td align='left' colspan='3' style='padding-left: 5px;'>");
                    str.Append(lblEmailValue.Text + "</td></tr>");

                    str.Append("<tr>");
                    str.Append("<td align='left'>");
                    str.Append("<span style='font-weight:bold'>Phone </span></td>");
                    str.Append("<td align='left' style='padding-left: 5px;'>");
                    str.Append(lblphoneValue.Text + "</td>");


                    str.Append("<td align='left'>");
                    str.Append("<span style='font-weight:bold'>Mobile </span></td>");
                    str.Append("<td align='left' style='padding-left: 5px;'>");
                    str.Append(lblmobile.Text + "</td></tr>");

                    str.Append("<tr>");
                    str.Append("<td align='left'>");
                    str.Append("<span style='font-weight:bold'>Make </span></td>");
                    str.Append("<td align='left'  style='padding-left: 5px;'>");
                    str.Append(lblmakevalue.Text + "</td>");

                    str.Append("<td align='left'>");
                    str.Append("<span style='font-weight:bold'>Model </span></td>");
                    str.Append("<td align='left' style='padding-left: 5px;'>");
                    str.Append(lblModel.Text + "</td></tr>");

                    str.Append("</table>");

                    #endregion

                    GridView grdDetails = (GridView)grdRow.FindControl("grdDetails");
                    if (grdDetails != null)
                    {
                        #region GRD Customer Details
                        str.Append("<table cellspacing='0' border='1' style='width:100%; border-collapse: collapse;'>");
                        str.Append("<tr style='font-weight: bold; height: 30px;' class='gvHeader'><th align='center' valign='middle' scope='col'>Date of Update</th><th align='center' valign='middle' scope='col'>Status </th><th align='center' valign='middle' scope='col'><span style='color: Red;'>*</span> ETA</th><th align='center' valign='middle' scope='col'>Notes </th></tr>");
                        foreach (GridViewRow grdRow1 in grdDetails.Rows)
                        {
                            Label lbldatofUpdate = (Label)grdRow1.FindControl("lbldatofUpdate");
                            Label lblStatus = (Label)grdRow1.FindControl("lblStatus");
                            Label lblETA = (Label)grdRow1.FindControl("lblETA");
                            Label lblNotes = (Label)grdRow1.FindControl("lblNotes");

                            str.Append("<tr style='height: 30px;'>");
                            str.Append("<td align='center' valign='middle' style='width: 90px;' class='showPadding'>");
                            str.Append(lbldatofUpdate.Text + "</td>");

                            str.Append("<td align='left' valign='middle' style='width: 200px;' class='showPadding'>");
                            str.Append(lblStatus.Text + "</td>");

                            str.Append("<td align='center' valign='middle' style='width: 90px;' class='showPadding'>");
                            str.Append(lblETA.Text + "</td>");

                            str.Append("<td align='left' valign='middle' style='width: 300px;' class='showPadding'>");
                            str.Append(lblNotes.Text + "</td>");

                            str.Append("</tr>");
                        }
                        str.Append("</table>");
                        #endregion
                    }
                }
            }
            //str.Append("</tr>");
            //str.Append("</table>");
            ViewState["EmailData"] = str;
        }
        catch (Exception ex)
        {
            logger.Error("configureEmailContent : " + Convert.ToString(ex.Message));
        }
    }

    /// <summary>
    /// Created By : Manoj : 20 Mar 2013
    /// Details : Send Email to catherine if any one update the status with the ETA date before todays date
    /// </summary>
    public void SendMail_CatherineOnOldETAUpdate(int id)
    {
        logger.Error("Send Email to Catherine Starts. ETA less than todays");
        Cls_GenericEmailHelper clsemail = new Cls_GenericEmailHelper();
        StringBuilder EmailBody = new StringBuilder();
        DataSet ds = (DataSet)ViewState["_clintList"];
        dt = new DataTable();
        DataView dv = new DataView();
        try
        {
            dv = new DataView(ds.Tables[1]);
            dv.RowFilter = "Customerid=" + id;
            dv.Sort = "date desc";
            dt = dv.ToTable();
            // If dealer is deactive do not send any mail to dealer
            if (dt != null && dt.Rows.Count >= 1)
            {
                EmailBody.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear catherine,");
                EmailBody.Append("<br/><br/>This is to inform that the supplying dealer just updated the delivery status with the date that has already passed.");
                EmailBody.Append("<br/>The current " + "<b>estimated</b>" + " delivery date is " + Convert.ToDateTime(Convert.ToString(dt.Rows[0]["ETA"])).ToString("dd/MM/yyyy"));
                EmailBody.Append("<br/><br/>Other Details are -");
                EmailBody.Append("<br/><br/>Customer Name - " + Convert.ToString(dt.Rows[0]["fullname"]));
                EmailBody.Append("<br/>Make, model - " + Convert.ToString(dt.Rows[0]["Make"]) + " " + Convert.ToString(dt.Rows[0]["Model"]));
                EmailBody.Append("<br/>Dealer Company - " + Convert.ToString(dt.Rows[0]["DealerCompany"]));
                EmailBody.Append("<br/>Dealer Name - " + Convert.ToString(dt.Rows[0]["DealerName"]));
                if (Convert.ToString(dt.Rows[0]["DealerNotes"]).Trim() != "")
                {
                    EmailBody.Append("<br/>Dealer Notes -" + Convert.ToString(dt.Rows[0]["DealerNotes"]).Trim());
                }
                EmailBody.Append("<br /><br />Best Regards");
                EmailBody.Append("<br /><br /><a href='http://www.privatefleet.com.au/' target='_Blank'>www.privatefleet.com.au</a><br /></p>");

                clsemail.EmailBody = Convert.ToString(EmailBody);
                clsemail.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
                clsemail.EmailToID = "catherineheyes@privatefleet.com.au";
                //clsemail.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
                clsemail.EmailSubject = "Status updated by Dealer with an ETA date that has already passed.";
                clsemail.SendEmail();
            }
        }
        catch (Exception ex)
        {
            logger.Error("User_Controls_UCDealers_ClientList SendMail_CatherineOnOldETAUpdate Error - " + ex.Message);
        }
        finally
        {
            clsemail = null;
            EmailBody = null;
            ds = null; dt = null; dv = null;
        }
    }

    #endregion

}
