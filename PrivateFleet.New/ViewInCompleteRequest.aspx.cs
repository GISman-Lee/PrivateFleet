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

public partial class ViewInCompleteRequest : System.Web.UI.Page
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(ViewInCompleteRequest));

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
                    lblHeader.Text = "View Incomplete Quote Requests";

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
            logger.Error("Page_Load Event : " + ex.Message);
        }
    }

    #endregion

    #region "Methods"

    private void CheckDate()
    {
        txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
        TxtToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
    }

    /// <summary>
    /// Method to bind sent quote requests to grid
    /// </summary>
    private void BindRequests()
    {
        logger.Debug("Method Start : BindRequests");
        Cls_Request objRequest = new Cls_Request();
        try
        {
            TimeSpan ts = new TimeSpan(1, 0, 0, 0);
            DateTime date1;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);

            objRequest.FromDate = DateTime.Parse(txtCalenderFrom.Text.Trim(), culture);
            //Convert.ToDateTime(txtCalenderFrom.Text.ToString());

            date1 = DateTime.Parse(TxtToDate.Text.Trim(), culture);
            //Convert.ToDateTime(TxtToDate.Text.ToString());
            objRequest.ToDate = date1.Add(ts);
            objRequest.ConsultantId = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            DataTable dt = objRequest.GetSentQuoteRequests();

            DataView dv = dt.DefaultView;
            dv.RowFilter = "SaveAtStep<3";
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
            throw;
        }
        finally
        {
            objRequest = null;
            logger.Debug("Method End : BindRequests");
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

    private bool IsInComplete(int intRequestId)
    {
        Cls_Request objRequest = new Cls_Request();
        try
        {
            objRequest.RequestId = intRequestId;
            DataTable dt=objRequest.IsInCompleteQuote(objRequest);

            if (dt != null && dt.Rows.Count == 1)
                return Convert.ToBoolean(dt.Rows[0]["IsInComplete"]);
            else
                return true;
            
        }
        catch (Exception ex)
        {
              logger.Error("IsInComplete Event : " + ex.Message);
              return false;
        }
    }

    #endregion

    #region "Events"

    protected void gvRequests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Cls_General objGeneral = new Cls_General();
        try
        {
            if (e.CommandName == "ViewDetails")
            {
                if (ViewState[Cls_Constants.ALLOWED_ACTION_ON_PAGE] != null)
                    objGeneral.AllowedActions = ((DataTable)ViewState[Cls_Constants.ALLOWED_ACTION_ON_PAGE]);
                objGeneral.ActionToCheck = e.CommandName;

                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvRequests.DataKeys[gvRow.RowIndex].Values["ID"]);
                Session[Cls_Constants.SESSION_BACK_PAGE_URL] = "ViewInCompleteRequest.aspx";
                Response.Redirect("~/ViewSentRequestDetails.aspx?id=" + intRequestId + "&f=0&ConsultantID=" + Session[Cls_Constants.LOGGED_IN_USERID].ToString() + "&Fdate=" + txtCalenderFrom.Text + "&Tdate=" + TxtToDate.Text);

            }
            else if (e.CommandName == "Complete") // added on 30 Jun 2012
            {
                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvRequests.DataKeys[gvRow.RowIndex].Values["ID"]);
                if (IsInComplete(intRequestId))
                    Response.Redirect("~/QuoteRequest.aspx?moveto=incomplete&RequestID=" + intRequestId);
                else
                    BindRequests();
            }
            else if (e.CommandName == "CancelQR")
            {
                LinkButton lnkCancelQR = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkCancelQR.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvRequests.DataKeys[gvRow.RowIndex].Values["ID"]);
                ViewState["CancelRequestId"] = intRequestId;
                msgpop.Style.Add("display", "block");
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvRequests_RowCommand Event : " + ex.Message);
        }
    }

    protected void gvRequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Cls_Request objRequest = new Cls_Request();
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdfQRStatus = (HiddenField)e.Row.FindControl("hdfQRStatus");
                LinkButton lnkComplete = (LinkButton)e.Row.FindControl("lnkComplete");
                LinkButton lnkDetails = (LinkButton)e.Row.FindControl("lnkDetails");
                LinkButton lnkCancelQR = (LinkButton)e.Row.FindControl("lnkCancelQR");

                if (Convert.ToString(hdfQRStatus.Value).ToLower() == "true")
                {
                    lnkComplete.Text = "Request Closed";
                    lnkComplete.Enabled = false;
                    lnkComplete.CssClass = "";

                    lnkCancelQR.Enabled = false;
                    lnkCancelQR.CssClass = "";
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

    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {

        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;

        //Swap sort direction
        this.DefineSortDirection();

        // BindData(objCourseMaster);
        this.BindRequests();
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
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnQRCancelYes_Click err -" + ex.Message);
        }
        finally
        {
            dt = null;
            objRequest = null;
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

    #endregion
}
