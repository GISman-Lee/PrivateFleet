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
using Mechsoft.GeneralUtilities;
using Mechsoft.FleetDeal;
using AccessControlUnit;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using log4net;

public partial class ViewDealersQuotation : System.Web.UI.Page
{
    static ILog logger = LogManager.GetLogger(typeof(ViewDealersQuotation));

    Cls_Quotation objQuote = new Cls_Quotation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDealerMakes();

            lblResult.Visible = false;
            lblResult.Text = "";
            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            gvMakeDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            ddl_NoRecords.Visible = false;
            lblRowsToDisplay.Visible = false;

            CheckDate();

            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "CreatedDate1";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
            (this.Master.FindControl("lblHeader") as Label).Text = "View Quotations";
            if (Request.QueryString["Fdate"] != null && Request.QueryString["Tdate"] != null && Request.QueryString["Fdate"].ToString() != "" && Request.QueryString["Tdate"].ToString() != "" && !String.IsNullOrEmpty(Request.QueryString["MakeId"]))
            {

                txtCalenderFrom.Text = Request.QueryString["Fdate"];
                TxtToDate.Text = Request.QueryString["Tdate"];
                ddlMake.SelectedValue = Convert.ToString(Request.QueryString["MakeId"]);
            }
            BindData();
            GetActionsAllowed();
        }
        panDeler.DefaultButton = "btnGenerateReport";

    }
    private void CheckDate()
    {
        txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
        TxtToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
    }

    private void BindDealerMakes()
    {
        Cls_Request objRequest = new Cls_Request();
        if (Session[Cls_Constants.USER_NAME] != null)
            objRequest.Email = Convert.ToString(Session[Cls_Constants.USER_NAME]);
        else
            Response.Redirect("index.aspx");

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

    private void GetActionsAllowed()
    {
        Cls_Access objAccess = new Cls_Access();
        objAccess.PageURL = Request.Url.Segments[Request.Url.Segments.Length - 1].ToString();
        objAccess.AccessFor = Convert.ToInt32(Session[Cls_Constants.ROLE_ID]);
        objAccess.AccessTypeId = 1;
        DataTable dtActionsAllowed = objAccess.GetAllowedActionsOnPerticularPage();
        ViewState[Cls_Constants.ALLOWED_ACTION_ON_PAGE] = dtActionsAllowed;
    }
    private void BindData()
    {

        lblResult.Visible = false;
        lblResult.Text = "";
        IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);

        gvMakeDetails.DataSource = null;
        gvMakeDetails.DataBind();
        objQuote.UserID = Convert.ToInt16(Session[Cls_Constants.LOGGED_IN_USERID]);
        TimeSpan ts = new TimeSpan(1, 0, 0, 0);
        DateTime date1;
        DataTable dtQuotatios = null;
        try
        {
            objQuote.FromDate = Convert.ToDateTime(txtCalenderFrom.Text.Trim(), culture);
            date1 = Convert.ToDateTime(TxtToDate.Text.Trim(), culture);
            objQuote.ToDate = date1.Add(ts);
            objQuote.MakeID = Convert.ToInt32(ddlMake.SelectedValue);
            dtQuotatios = objQuote.GetDealersQuotations();
        }
        catch (Exception ex1)
        {
            lblResult.Visible = true;
            lblResult.Text = "Enter proper date";
            ddl_NoRecords.Visible = false;
            lblRowsToDisplay.Visible = false;
            return;
        }
        DataView dv = dtQuotatios.DefaultView;
        dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

        dtQuotatios = dv.ToTable();

        ViewState["dtQuotatios"] = dtQuotatios;
        if (dtQuotatios.Rows.Count > 0)
        {
            ddl_NoRecords.Visible = true;
            lblRowsToDisplay.Visible = true;
        }
        else
        {
            ddl_NoRecords.Visible = false;
            lblRowsToDisplay.Visible = false;
        }

        gvMakeDetails.DataSource = dtQuotatios;
        gvMakeDetails.DataBind();
    }
    protected void gvMakeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMakeDetails.PageIndex = e.NewPageIndex;
        gvMakeDetails.DataSource = (DataTable)ViewState["dtQuotatios"];
        gvMakeDetails.DataBind();
    }
    protected void gvMakeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Cls_General objGeneral = new Cls_General();
        objGeneral.AllowedActions = ((DataTable)ViewState[Cls_Constants.ALLOWED_ACTION_ON_PAGE]);
        if (e.CommandName.Equals("ViewDetails", StringComparison.InvariantCultureIgnoreCase))
        {
            objGeneral.ActionToCheck = e.CommandName;
            //if (objGeneral.CheckForThisAction())
            //{
            LinkButton lnkDetails = (LinkButton)e.CommandSource;

            GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;
            HiddenField hdfQRStatus = (HiddenField)gvRow.FindControl("hdfQRStatus");

            int intQuotationId = Convert.ToInt32(gvMakeDetails.DataKeys[gvRow.RowIndex].Values["ID"]);
            int intRequestId = Convert.ToInt16(((HiddenField)gvRow.FindControl("hdfRequestID")).Value.ToString());
            int DID = Convert.ToInt32(((HiddenField)gvRow.FindControl("hdfDID")).Value.ToString());
            //ViewState["FormDate1"] = txtCalenderFrom.Text;
            //ViewState["ToDate1"] = TxtToDate.Text;
            // ViewState["m1"]="Hi";
            Response.Redirect("~/ViewQuotation.aspx?QuoteID=" + intQuotationId.ToString() + "&ReqID=" + intRequestId + "&Fdate=" + txtCalenderFrom.Text + "&Tdate=" + TxtToDate.Text + "&DID=" + DID + "&MakeId=" + ddlMake.SelectedValue + "&QRCancel=" + hdfQRStatus.Value);
        }

        // Comment on 23 mar 2011 bcoz of client requirement
        //if (e.CommandName.Equals("Delete", StringComparison.InvariantCultureIgnoreCase))
        //{
        //    LinkButton lnkDetails = (LinkButton)e.CommandSource;
        //    GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;

        //    int intQuotationId = Convert.ToInt32(gvMakeDetails.DataKeys[gvRow.RowIndex].Values["ID"]);
        //    int intRequestId = Convert.ToInt16(((HiddenField)gvRow.FindControl("hdfRequestID")).Value.ToString());
        //    objQuote.QuotationID = intQuotationId;
        //    objQuote.RequestID = intRequestId;
        //    objQuote.DeleteQuote();
        //    BindData();
        //}
    }


    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {

        gvMakeDetails.PageIndex = 0;
        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {

            gvMakeDetails.DataSource = (DataTable)ViewState["dtQuotatios"];
            gvMakeDetails.PageSize = gvMakeDetails.PageCount * gvMakeDetails.Rows.Count;
            gvMakeDetails.DataBind();
        }
        else
        {
            gvMakeDetails.DataSource = (DataTable)ViewState["dtQuotatios"];
            gvMakeDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvMakeDetails.DataBind();





        }
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
                HiddenField hdfQRStatus = (HiddenField)e.Row.FindControl("hdfQRStatus");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                //LinkButton lnkDetails = (LinkButton)e.Row.FindControl("lnkDetails");

                if (Convert.ToString(hdfQRStatus.Value).ToLower() == "true")
                {
                    lblStatus.Text = "Request Closed";
                    lblStatus.Enabled = false;
                    lblStatus.CssClass = "";
                    //lnkDetails.Enabled = false;
                    //lnkDetails.CssClass = "";
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvRequests_RowDataBound Event : " + ex.Message);
        }
    }
    protected void gvMakeDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {

        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;


        //Swap sort direction
        this.DefineSortDirection();

        // BindData(objCourseMaster);
        this.BindData();
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
    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        BindData();
    }
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        CheckDate();
        DataTable dt = null;
        ViewState["dtQuotatios"] = dt;
        gvMakeDetails.DataSource = dt;
        gvMakeDetails.DataBind();

        lblResult.Visible = false;
        lblResult.Text = "";

        ddl_NoRecords.Visible = false;
        lblRowsToDisplay.Visible = false;
    }
    protected void gvMakeDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

}
