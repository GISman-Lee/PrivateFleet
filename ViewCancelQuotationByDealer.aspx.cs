using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Mechsoft.GeneralUtilities;
using System.Data;

public partial class ViewCancelQuotationByDealer : System.Web.UI.Page
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(ViewCancelQuotationByDealer));
    Cls_Request objRequest = new Cls_Request();
    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindDealerMakes();
                ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
                gvMakeDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                ddl_NoRecords.Visible = false;
                lblRowsToDisplay.Visible = false;

                //Set page header text
                Label lblHeader = (Label)Master.FindControl("lblHeader");
                CheckDate();
                if (lblHeader != null)
                    lblHeader.Text = "View Received Quote Requests";

                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "RequestDate1";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;

                if (Request.QueryString["Fdate"] != null && Request.QueryString["Tdate"] != null && Request.QueryString["Fdate"].ToString() != "" && Request.QueryString["Tdate"].ToString() != "" && !String.IsNullOrEmpty(Request.QueryString["MakeID"]))
                {
                    txtCalenderFrom.Text = Request.QueryString["Fdate"];
                    TxtToDate.Text = Request.QueryString["Tdate"];
                    ddlMake.SelectedValue = Convert.ToString(Request.QueryString["MakeID"]);
                }
                BindRequests();

            }
            ViewReceivedQR.DefaultButton = "btnGenerateReport";
        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Event : " + ex.Message);
        }
    }
    private void CheckDate()
    {
        txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
        TxtToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
    }
    #endregion

    #region "Methods"

    private void BindDealerMakes()
    {
        objRequest = new Cls_Request();
        objRequest.Email = Convert.ToString(Session[Cls_Constants.USER_NAME]);
        DataTable dtMake = objRequest.BindDealerMakes();
        if (dtMake != null && dtMake.Rows.Count > 0)
        {
            ddlMake.DataSource = dtMake;
            ddlMake.DataTextField = "Make";
            ddlMake.DataValueField = "ID";
            ddlMake.DataBind();

            ddlMake.Items.Insert(0, new ListItem(" -- All --", "0"));

            if (dtMake.Rows.Count == 1)
            {
                ddlMake.SelectedIndex = 1;
                lblMake.Visible = false;
                ddlMake.Visible = false;
            }

        }
    }

    private void BindRequests()
    {
        IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
        ConfigValues objConfigue = new ConfigValues();
        try
        {
            TimeSpan ts = new TimeSpan(1, 0, 0, 0);
            DateTime date1;
            objRequest.FromDate = Convert.ToDateTime(txtCalenderFrom.Text.Trim(), culture);
            date1 = Convert.ToDateTime(TxtToDate.Text.Trim(), culture);
            objRequest.ToDate = date1.Add(ts);
            objRequest.UserID = Convert.ToInt16(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
            objRequest.MakeID = Convert.ToInt32(ddlMake.SelectedValue);
            DataTable dtRequests = objRequest.GetRecievedQuoteRequestsForDealer();

            // OnAbortTransaction 13 Sept 2012 For Hiding QR before 10 days
            objConfigue.Key = "NO_DAYS_BEFOT_TO_HIDE_QR";
            int NoDays = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
            //end

            DataView dv = dtRequests.DefaultView;
            dv.RowFilter = "RequestDate1<'" + System.DateTime.Now.AddDays(-NoDays).Date + "'";
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dtRequests = dv.ToTable();

            ViewState["dtRequests"] = dtRequests;
            if (dtRequests.Rows.Count > 0)
            {
                ddl_NoRecords.Visible = true;
                lblRowsToDisplay.Visible = true;
            }
            else
            {
                ddl_NoRecords.Visible = false;
                lblRowsToDisplay.Visible = false;
            }

            gvMakeDetails.DataSource = dtRequests;
            gvMakeDetails.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("BindRequests Method : " + ex.Message);
            throw;
        }
    }
    #endregion

    #region "Events"
    protected void gvMakeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMakeDetails.PageIndex = e.NewPageIndex;
        gvMakeDetails.DataSource = (DataTable)ViewState["dtRequests"];
        gvMakeDetails.DataBind();

    }
    protected void gvMakeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (String.Equals(e.CommandName, "ViewDetails", StringComparison.InvariantCultureIgnoreCase))
            {
                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvMakeDetails.DataKeys[gvRow.RowIndex].Values["ID"]);
                int ConsultantID = Convert.ToInt32(((HiddenField)gvRow.FindControl("hdfConsultantID")).Value.ToString());
                Response.Redirect("~/ViewSentRequestDetails.aspx?id=" + intRequestId + "&ConsultantID=" + ConsultantID + "&Dealer='no'&doReddirect=1&Fdate=" + txtCalenderFrom.Text + "&Tdate=" + TxtToDate.Text + "&MakeID=" + ddlMake.SelectedValue);
            }
            if (String.Equals(e.CommandName, "CreateQuote", StringComparison.InvariantCultureIgnoreCase))
            {

                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvMakeDetails.DataKeys[gvRow.RowIndex].Values["ID"]);
                int ConsultantID = Convert.ToInt32(((HiddenField)gvRow.FindControl("hdfConsultantID")).Value.ToString());
                // added on 11 may 12 to show all quote of multi franc. dealer
                int DID = Convert.ToInt32(((HiddenField)gvRow.FindControl("hdfDealerID")).Value.ToString());
                Response.Redirect("~/Quotation.aspx?id=" + intRequestId + "&ConsultantID=" + ConsultantID + "&DID=" + DID);
            }
            if (String.Equals(e.CommandName, "DeleteQuote", StringComparison.InvariantCultureIgnoreCase))
            {
                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;

                objRequest.RequestId = Convert.ToInt32(gvMakeDetails.DataKeys[gvRow.RowIndex].Values["ID"]);
                objRequest.DeleteQuote();
                BindRequests();
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvMakeDetails_RowCommand event : " + ex.Message);
        }
    }
    protected void gvMakeDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvMakeDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void gvMakeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //for closed dealer
                HiddenField hdfQRStatus = (HiddenField)e.Row.FindControl("hdfQRStatus");
                LinkButton lnkCreateQuotation = (LinkButton)e.Row.FindControl("lnkCreateQuotation");
                LinkButton lnkDetails = (LinkButton)e.Row.FindControl("lnkDetails");

                if (Convert.ToString(hdfQRStatus.Value).ToLower() == "true")
                {
                    //lnkDetails.Enabled = false;
                    //lnkDetails.CssClass = "";

                    lnkCreateQuotation.Enabled = false;
                    lnkCreateQuotation.Text = "Request Closed";
                    lnkCreateQuotation.CssClass = "";
                }
            }
        }
        catch (Exception ex)
        {

            throw;
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
    #endregion

    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        BindRequests();
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        CheckDate();
        DataTable dt = null;

        ViewState["dtRequests"] = dt;
        ddl_NoRecords.Visible = false;
        lblRowsToDisplay.Visible = false;
        this.gvMakeDetails.DataSource = dt;
        this.gvMakeDetails.DataBind();
    }

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {

        gvMakeDetails.PageIndex = 0;
        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {

            gvMakeDetails.DataSource = (DataTable)ViewState["dtRequests"];
            gvMakeDetails.PageSize = gvMakeDetails.PageCount * gvMakeDetails.Rows.Count;
            gvMakeDetails.DataBind();
        }
        else
        {
            gvMakeDetails.DataSource = (DataTable)ViewState["dtRequests"];
            gvMakeDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvMakeDetails.DataBind();
        }
    }
}
