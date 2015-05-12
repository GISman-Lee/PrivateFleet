using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
using Mechsoft.GeneralUtilities;
using AccessControlUnit;
using System.Text;

public partial class ViewSentRequests : System.Web.UI.Page
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(ViewSentRequests));

    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            msgpop.Style.Add("display", "none");
            if (!IsPostBack)
            {
                //Set page header text
                Label lblHeader = (Label)Master.FindControl("lblHeader");

                ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
                gvRequests.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                ddl_NoRecords.Visible = false;
                lblRowsToDisplay.Visible = false;

                if (lblHeader != null)
                    lblHeader.Text = "View Sent Quote Requests";

                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "RequestDate1";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;

                CheckDate();

                if (Request.QueryString["Fdate"] != null && Request.QueryString["Tdate"] != null && Request.QueryString["Fdate"].ToString() != "" && Request.QueryString["Tdate"].ToString() != "")
                {
                    txtCalenderFrom.Text = Request.QueryString["Fdate"];
                    TxtToDate.Text = Request.QueryString["Tdate"];

                }

                //bind sent quote requests to grid
                BindRequests();

                //get allowed actions for logged in user/role on current page
                ViewState[Cls_Constants.ALLOWED_ACTION_ON_PAGE] = GetAllowedActionsOnPage();

            }
            viewSentReq.DefaultButton = "btnGenerateReport";
        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Event : " + ex.Message + " :: " + ex.StackTrace);
        }
    }

    private void CheckDate()
    {
        txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
        TxtToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
    }

    #endregion

    #region "Methods"

    /// <summary>
    /// Method to bind sent quote requests to grid
    /// </summary>
    private void BindRequests()
    {
        logger.Debug("Method Start : BindRequests");
        Cls_Request objRequest = new Cls_Request();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        try
        {
            //TimeSpan ts = new TimeSpan(1, 0, 0, 0);
            DateTime date1;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);

            objRequest.FromDate = DateTime.Parse(txtCalenderFrom.Text.Trim(), culture);
            //Convert.ToDateTime(txtCalenderFrom.Text.ToString());

            date1 = DateTime.Parse(TxtToDate.Text.Trim(), culture);
            //Convert.ToDateTime(TxtToDate.Text.ToString());
            objRequest.ToDate = date1;//.Add(ts);
            objRequest.ConsultantId = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            dt = objRequest.GetSentQuoteRequests();

            dv = dt.DefaultView;
            dv.RowFilter = "SaveAtStep=3";
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            ViewState["dt"] = dt;

            if (dt.Rows.Count > 0)
            {
                ddl_NoRecords.Visible = true;
                lblRowsToDisplay.Visible = true;
            }
            else
            {
                ddl_NoRecords.Visible = false;
                lblRowsToDisplay.Visible = false;
            }

            gvRequests.DataSource = dt;
            gvRequests.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("BindRequests Method : " + ex.Message);
        }
        finally
        {
            objRequest = null;
            dt = null;
            dv = null;
        }
    }

    /// <summary>
    /// Method to get all allowed actions for logged in user/role on current page
    /// </summary>
    /// <returns></returns>
    private DataTable GetAllowedActionsOnPage()
    {
        Cls_Access objAccess = new Cls_Access();
        try
        {
            objAccess.PageURL = Request.Url.Segments[Request.Url.Segments.Length - 1].ToString();
            objAccess.AccessFor = Convert.ToInt32(Session[Cls_Constants.ROLE_ID]);
            objAccess.AccessTypeId = 1;

            DataTable dtActionsAllowed = objAccess.GetAllowedActionsOnPerticularPage();

            return dtActionsAllowed;
        }
        catch (Exception ex)
        {
            logger.Error("GetAllowedActions Method : " + ex.Message);
            throw;
        }
        finally
        {
            //release resources
            objAccess = null;
        }
        return null;
    }

    #endregion

    #region "Events"

    protected void gvRequests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Cls_General objGeneral = new Cls_General();
        try
        {
            if(e.CommandName == "SelectRequest")
            {


            }

            else if (e.CommandName == "ViewDetails")
            {
                if (ViewState[Cls_Constants.ALLOWED_ACTION_ON_PAGE] != null)
                    objGeneral.AllowedActions = ((DataTable)ViewState[Cls_Constants.ALLOWED_ACTION_ON_PAGE]);
                objGeneral.ActionToCheck = e.CommandName;

                //check if user has access to action or not
                //if (objGeneral.CheckForThisAction())
                //{
                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvRequests.DataKeys[gvRow.RowIndex].Values["ID"]);
                Session[Cls_Constants.SESSION_BACK_PAGE_URL] = "ViewSentRequests.aspx";
                Response.Redirect("~/ViewSentRequestDetails.aspx?id=" + intRequestId + "&f=0&ConsultantID=" + Session[Cls_Constants.LOGGED_IN_USERID].ToString() + "&Fdate=" + txtCalenderFrom.Text + "&Tdate=" + TxtToDate.Text);
                //}
                //else
                //{
                //    (this.Master.FindControl("lblMasterMsg") as Label).Text = "Access denied for the selected action!!!";
                //    (this.Master.FindControl("trAccess") as HtmlTableRow).Visible = true;
                //}
            }

            else if (e.CommandName == "CompareQuotations")
            {
                LinkButton lnkCompare = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkCompare.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvRequests.DataKeys[gvRow.RowIndex].Values["ID"]);

                Response.Redirect("~/QuoteComparison.aspx?ReqID=" + intRequestId + "&Fdate=" + txtCalenderFrom.Text + "&Tdate=" + TxtToDate.Text);
            }

            else if (e.CommandName == "ViewShortlistedQuote")
            {
                LinkButton lnkCompare = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkCompare.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvRequests.DataKeys[gvRow.RowIndex].Values["ID"]);
                int intQuoteId = Convert.ToInt32(lnkCompare.CommandArgument);

                Response.Redirect("~/ViewShortListedQuotation.aspx?ReqID=" + intRequestId.ToString() + "&QuoteID=" + intQuoteId.ToString() + "&Fdate=" + txtCalenderFrom.Text + "&Tdate=" + TxtToDate.Text);
            }
            else if (e.CommandName == "AddMoreDealer") // added on 30 Jun 2012
            {
                LinkButton lnkCompare = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkCompare.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvRequests.DataKeys[gvRow.RowIndex].Values["ID"]);
                Response.Redirect("~/QuoteRequest.aspx?moveto=addDealer&RequestID=" + intRequestId);
            }
            else if (e.CommandName == "CancelQR")
            {
                LinkButton lnkCancelQR = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkCancelQR.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvRequests.DataKeys[gvRow.RowIndex].Values["ID"]);
                ViewState["CancelRequestId"] = intRequestId;
                msgpop.Style.Add("display", "block");
            }
            //This condition is write for set the deal is done.
            //if (e.CommandName == "DealDone")
            //{
            //    LinkButton lnkCompare = (LinkButton)e.CommandSource;
            //    GridViewRow gvRow = (GridViewRow)lnkCompare.Parent.Parent;
            //    int intQouateId = Convert.ToInt32(lnkCompare.CommandArgument);
            //    Cls_Quotation objcls = new Cls_Quotation();
            //    objcls.SetDealDone(intQouateId);
            //}

        }
        catch (Exception ex)
        {
            logger.Error("gvRequests_RowCommand Event : " + ex.Message);
        }
    }

    #endregion

    protected void gvRequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Cls_Request objRequest = new Cls_Request();
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkCompare = (LinkButton)e.Row.FindControl("lnkCompare");
                LinkButton lnkDetails = (LinkButton)e.Row.FindControl("lnkDetails");
                LinkButton lnkShortListed = (LinkButton)e.Row.FindControl("lnkShortListed");

                //for closed dealer on 6 sept 12
                LinkButton lnkAddDealer = (LinkButton)e.Row.FindControl("lnkAddDealer");
                LinkButton lnkCancelQR = (LinkButton)e.Row.FindControl("lnkCancelQR");
                HiddenField hdfQRStatus = (HiddenField)e.Row.FindControl("hdfQRStatus");

                int intQuotationID = Convert.ToInt32(lnkCompare.CommandArgument);

                //if quotation is not shortlisted
                if (intQuotationID == 0)
                {
                    GridViewRow gvRow = (GridViewRow)lnkCompare.Parent.Parent;
                    int intRequestId = Convert.ToInt32(gvRequests.DataKeys[gvRow.RowIndex].Values["ID"]);

                    objRequest.RequestId = intRequestId;
                    int intQuotationCount = objRequest.GetQuotationsCountForRequest();

                    if (intQuotationCount > 0)
                    {
                        lnkCompare.Text = "Compare Quotations";
                        lnkCompare.Enabled = true;
                        lnkCompare.CssClass = "activeLink";
                        lnkCompare.CommandName = "CompareQuotations";
                    }
                    else
                    {
                        lnkCompare.Text = "No Quotation Available";
                        lnkCompare.Enabled = false;
                        lnkCompare.CssClass = "";
                        lnkCompare.CommandName = "";
                    }
                }
                else
                {
                    lnkShortListed.Enabled = true;
                    lnkShortListed.CssClass = "activeLink";
                    //lnkCompare.Enabled = true;
                    //lnkCompare.CssClass = "activeLink";
                    //lnkCompare.Text = "Quotation Shortlisted";
                    //lnkCompare.CommandName = "ViewShortlistedQuote";
                    lnkDetails.Enabled = false;
                    lnkDetails.CssClass = "";

                }

                //for closed dealer
                if (Convert.ToString(hdfQRStatus.Value).ToLower() == "true")
                {
                    lnkCompare.Enabled = false;
                    lnkCompare.CssClass = "";

                    lnkCancelQR.Enabled = false;
                    lnkCancelQR.CssClass = "";

                    lnkShortListed.Enabled = false;
                    lnkShortListed.CssClass = "";

                    lnkAddDealer.Enabled = false;
                    lnkAddDealer.Text = "Request Closed";
                    lnkAddDealer.CssClass = "";
                }
                //end

            }
        }
        catch (Exception ex)
        {
            logger.Error("gvRequests_RowDataBound Event : " + ex.Message);
        }
    }

    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {

        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;


        //Swap sort direction
        this.DefineSortDirection();

        // BindData(objCourseMaster);
        this.BindRequests();
    }

    /// <summary>
    /// Define sort direction for grid.
    /// </summary>
    /// <param name="objAlias"></param>
    private void DefineSortDirection()
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

    protected void gvRequests_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRequests.PageIndex = e.NewPageIndex;
        gvRequests.DataSource = (DataTable)ViewState["dt"];
        gvRequests.DataBind();
        //BindRequests();
    }

    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        BindRequests();
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        CheckDate();
        DataTable dt = null;
        ViewState["dt"] = dt;
        lblRowsToDisplay.Visible = false;
        ddl_NoRecords.Visible = false;
        gvRequests.DataSource = dt;
        gvRequests.DataBind();
    }

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {

        gvRequests.PageIndex = 0;
        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {

            gvRequests.DataSource = (DataTable)ViewState["dt"];
            gvRequests.PageSize = gvRequests.PageCount * gvRequests.Rows.Count;
            gvRequests.DataBind();
        }
        else
        {
            gvRequests.DataSource = (DataTable)ViewState["dt"];
            gvRequests.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvRequests.DataBind();
        }
    }

    /// <summary>
    /// Author  : Manoj
    /// Date    : 6 Sept 2012
    /// Description : used to cancel Qute Request by consultant to confirm
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgbtnQRCancelYes_Click(object sender, ImageClickEventArgs e)
    {
        Cls_Request objRequest = new Cls_Request();
        DataTable dt = new DataTable();

        try
        {
            objRequest.ConsultantId = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            objRequest.RequestId = Convert.ToInt32(ViewState["CancelRequestId"]);
            dt = objRequest.CancelQR();
            BindRequests();
            ViewState["CancelRequestId"] = null;
            SendEmailToDealer(dt);
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnQRCancelYes_Click err -" + ex.Message);
        }
        finally
        {
            objRequest = null;
        }
    }

    private void SendEmailToDealer(DataTable dt)
    {
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
        try
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string DealerFName = "";
                DealerFName = Convert.ToString(dt.Rows[i]["DealerName"]).Trim();
                if (DealerFName.Contains(" "))
                {
                    DealerFName = DealerFName.Substring(0, DealerFName.IndexOf(" "));
                }

                StringBuilder str = new StringBuilder();
                str.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + DealerFName + " <br /><br />The tender request for " + Convert.ToString(dt.Rows[i]["Make"]) + " - " + Convert.ToString(dt.Rows[i]["Model"]) + " submitted by ");
                str.Append(Convert.ToString(dt.Rows[i]["ConsultantName"]) + " has now been closed. So please do not worry about completing the quote request at this time.");
                str.Append("<br/><br/>Thank you, as ever, for your assistance.");
                str.Append("<br/><br/>Private Fleet</p>");

                objEmailHelper.EmailBody = str.ToString();
                objEmailHelper.EmailToID = Convert.ToString(dt.Rows[i]["DealerEmail"]);
                objEmailHelper.EmailFromID = Convert.ToString(dt.Rows[i]["ConsultantName"]) + " <" + Convert.ToString(dt.Rows[i]["ConsultantEmail"]) + ">"; ;
                //objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
                objEmailHelper.EmailSubject = "Cancel Quotation";

                logger.Debug("QR To -" + objEmailHelper.EmailToID);
                logger.Debug("QR From -" + objEmailHelper.EmailFromID);

                if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
                    objEmailHelper.SendEmail();
            }
        }
        catch (Exception ex)
        {
            logger.Error("ViewSentRequests SendEmailToDealer error - " + ex.Message);
        }
        finally
        {

        }
    }

    /// <summary>
    /// Author      : Manoj
    /// Date        : 6 Sept 2012
    /// Description : used to cancel Qute Request by consultant
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgbtnQRCancelNo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            msgpop.Style.Add("display", "none");
            lblMessageForModal.Text = "Quote Request Saved Successfully.";
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnQRCancelNo_Click err -" + ex.Message);
        }
        finally
        {

        }
    }
}
