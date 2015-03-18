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



public partial class AdminReport : System.Web.UI.Page
{
    ILog logger = LogManager.GetLogger(typeof(AdminReport));


    protected void Page_Load(object sender, EventArgs e)
    {

        //Set page header text
        if (!IsPostBack)
        {
            Label lblHeader = (Label)Master.FindControl("lblHeader");

            if (lblHeader != null)
                lblHeader.Text = "Quotation Report";

            if (Request.QueryString["Fdate"] != null && Request.QueryString["Fdate"].ToString() != "")
            {
                ddlSearchCriteria.SelectedIndex = Convert.ToInt32(Request.QueryString["Fdate"].ToString());
                ddlSearchCriteria_SelectedIndexChanged(sender, e);
            }

            //by manoj
            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            ddl_NoRecords1.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            ddl_NoRecords2.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);

            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "RequestDate1";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;

            gvRequests.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
            ddl_NoRecords2.Visible = false;
            lblRowsToDisplay2.Visible = false;


            gvSLRequests.PageSize = Convert.ToInt32(ddl_NoRecords1.SelectedValue.ToString());
            ddl_NoRecords1.Visible = false;
            lblRowsToDisplay1.Visible = false;

            gvAllSLQuotations.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
        }

        PView1.DefaultButton = "btnSearchByConsultant";
        PView2.DefaultButton = "btnSLGetRequest";
        lblTotal.Text = "";
        lblTotalShortlested.Text = "";
        lblTotalSLQC.Text = "";
    }

    #region "Methods"

    /// <summary>
    /// Method to bind sent quote requests to grid
    /// </summary>
    private void BindRequests()
    {
        logger.Debug("Method Start : BindRequests");
        Cls_Request objRequest = new Cls_Request();
        try
        {
            objRequest.ConsultantId = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            DataTable dt = objRequest.GetSentQuoteRequests();

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

                //check if user has access to action or not
                //if (objGeneral.CheckForThisAction())
                //{
                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;


                int intRequestId = Convert.ToInt32(gvRequests.DataKeys[gvRow.RowIndex][0].ToString());
                //int intQuotationId = Convert.ToInt32(gvSLRequests.DataKeys[gvRow.RowIndex][1].ToString());
                //String OptionID = gvSLRequests.DataKeys[gvRow.RowIndex][2].ToString();
                Session[Cls_Constants.SESSION_BACK_PAGE_URL] = "AdminReport.aspx";
                Response.Redirect("~/ViewSentRequestDetails.aspx?id=" + intRequestId + "&ConsultantID=" + (ddlConsultants.SelectedValue.ToString()));
                //}
                //else
                //{
                //    (this.Master.FindControl("lblMasterMsg") as Label).Text = "Access denied for the selected action!!!";
                //    (this.Master.FindControl("trAccess") as HtmlTableRow).Visible = true;
                //}
            }

            if (e.CommandName == "CompareQuotations")
            {
                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvRequests.DataKeys[gvRow.RowIndex].Values["ID"]);

                Response.Redirect("~/QuoteComparison.aspx?ReqID=" + intRequestId + "");
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvRequests_RowCommand Event : " + ex.Message);
        }
    }
    #endregion


    private void BindConsultants(DropDownList ddlSLConsultant)
    {
        ddlSLConsultant.Items.Clear();

        Cls_UserMaster objUser = new Cls_UserMaster();
        DataTable dtUsers = objUser.GetAllConsultants();
        foreach (DataRow dr in dtUsers.Rows)
        {
            if (Convert.ToInt32(dr["IsActive"]) == 0)
                dr["Name"] = dr["Name"] + " (In Active)";
        }

        ddlSLConsultant.DataSource = dtUsers;
        ddlSLConsultant.DataBind();

        ddlSLConsultant.Items.Insert(0, new ListItem("- Select Consultant -", "0"));
        ddlSLConsultant.Items.Insert(1, new ListItem("All", "-9999"));
    }

    #region Code to Get all request from all consultant
    protected void btnSearchByConsultant_Click(object sender, ImageClickEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
        BindAllRequestByConsultant();

    }
    public void BindAllRequestByConsultant()
    {
        Cls_Request objRequest = new Cls_Request();
        try
        {
            objRequest.ConsultantId = Convert.ToInt32(ddlConsultants.SelectedValue.ToString());
            IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);

            objRequest.FromDate = DateTime.Parse(txtCalenderFrom.Text.Trim(), culture);
            //  date1 = 
            //Convert.ToDateTime(TxtToDate.Text.ToString());
            objRequest.ToDate = DateTime.Parse(TxtToDate.Text.Trim(), culture);
            DataTable dt = objRequest.GetSentQuoteRequests();

            //sorting
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            ViewState["gvRequests"] = dt;
            if (dt.Rows.Count > 0)
            {
                ddl_NoRecords2.Visible = true;
                lblRowsToDisplay2.Visible = true;
                if (Convert.ToInt32(ddlConsultants.SelectedValue.ToString()) == -9999)
                    lblTotal.Text = "<b>No. of Quote Request by All Consultant  - </b>" + Convert.ToString(dt.Rows.Count);
                else
                    lblTotal.Text = "<b>No. of Quote Request by " + Convert.ToString(ddlConsultants.SelectedItem.ToString()) + " - </b>" + Convert.ToString(dt.Rows.Count);
            }
            else
            {
                ddl_NoRecords2.Visible = false;
                lblRowsToDisplay2.Visible = false;
            }

            gvRequests.Visible = true;
            gvRequests.DataSource = dt;
            gvRequests.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void gvRequests_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();

        BindAllRequestByConsultant();
    }
    protected void gvAllSLQuotations_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();

        BindAllShortlistedQuotation();
    }

    protected void gvSLRequests_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();

        BindSLQuoteOFConsultant();
    }

    protected void ddlConsultants_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvRequests.DataSource = null;
        gvRequests.DataBind();
    }
    protected void gvRequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Cls_Request objRequest = new Cls_Request();

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkCompare = (LinkButton)e.Row.FindControl("lnkCompare");
                int intQuotationID = Convert.ToInt32(lnkCompare.CommandArgument);
                //if (Convert.ToInt32(ddlConsultants.SelectedValue.ToString()) == -9999)
                //{
                //    Label lblName = (Label)e.Row.FindControl("lblName");
                //    lblName.Visible = true;
                //}
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
                    }
                    else
                    {
                        lnkCompare.Text = "No Quotation Available";
                        lnkCompare.Enabled = false;
                        lnkCompare.CssClass = "";
                    }
                }
                else
                {
                    lnkCompare.Enabled = false;
                    lnkCompare.CssClass = "";
                    lnkCompare.Text = "Quotation Shortlisted";
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvRequests_RowDataBound Event : " + ex.Message);
        }
    }
    #endregion

    #region No of rows to display
    protected void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvRequests.PageIndex = 0;
        if (ddl_NoRecords2.SelectedValue.ToString() == "All")
        {
            //For view 1
            gvRequests.PageSize = gvRequests.PageCount * gvRequests.Rows.Count;
            gvRequests.DataSource = (DataTable)ViewState["gvRequests"];
            gvRequests.DataBind();
        }
        else
        {
            //for view 1
            gvRequests.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
            gvRequests.DataSource = (DataTable)ViewState["gvRequests"];
            gvRequests.DataBind();
        }
    }


    protected void ddl_NoRecords1_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSLRequests.PageIndex = 0;
        if (ddl_NoRecords1.SelectedValue.ToString() == "All")
        {
            //For view 2
            gvSLRequests.PageSize = gvSLRequests.PageCount * gvSLRequests.Rows.Count;
            gvSLRequests.DataSource = (DataTable)ViewState["gvSLRequests"];
            gvSLRequests.DataBind();
        }
        else
        {
            //for view 2
            gvSLRequests.PageSize = Convert.ToInt32(ddl_NoRecords1.SelectedValue.ToString());
            gvSLRequests.DataSource = (DataTable)ViewState["gvSLRequests"];
            gvSLRequests.DataBind();
        }
    }

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvAllSLQuotations.PageIndex = 0;

        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {
            //for view 3 (vwAllSLQuotation)
            gvAllSLQuotations.PageSize = gvAllSLQuotations.PageCount * gvAllSLQuotations.Rows.Count;
            gvAllSLQuotations.DataSource = (DataTable)ViewState["AllSLQuotations1"];
            gvAllSLQuotations.DataBind();

        }
        else
        {
            //for view 3 (vwAllSLQuotation)
            gvAllSLQuotations.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvAllSLQuotations.DataSource = (DataTable)ViewState["AllSLQuotations1"];
            gvAllSLQuotations.DataBind();
        }
    }
    #endregion

    //added by manoj
    protected void gvAllSLQuotations_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllSLQuotations.PageIndex = e.NewPageIndex;
        gvAllSLQuotations.DataSource = (DataTable)ViewState["AllSLQuotations1"];
        gvAllSLQuotations.DataBind();
    }
    //by manoj 
    protected void gvSLRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSLRequests.PageIndex = e.NewPageIndex;
        gvSLRequests.DataSource = (DataTable)ViewState["gvSLRequests"];
        gvSLRequests.DataBind();
    }
    //by manoj
    protected void gvRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRequests.PageIndex = e.NewPageIndex;
        gvRequests.DataSource = (DataTable)ViewState["gvRequests"];
        gvRequests.DataBind();
    }
    
    protected void ddlSearchCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["gvRequests"] = null;
        ddl_NoRecords2.Visible = false;
        lblRowsToDisplay2.Visible = false;



        ViewState["AllSLQuotations1"] = null;
        ddl_NoRecords.Visible = false;
        lblRowsToDisplay.Visible = false;



        ViewState["gvSLRequests"] = null;
        ddl_NoRecords1.Visible = false;
        lblRowsToDisplay1.Visible = false;


        gvSLRequests.Visible = false;

        gvRequests.Visible = false;
        gvSLRequests.Visible = false;




        if (ddlSearchCriteria.SelectedValue.ToString().Equals("RBC"))
        {
            MultiView1.SetActiveView(View1);
            BindConsultants(ddlConsultants);
            txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
            TxtToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
        }

        if (ddlSearchCriteria.SelectedValue.ToString().Equals("SQOC"))
        {
            MultiView1.SetActiveView(View2);
            BindConsultants(ddlSLConsultant);
            gvSLRequests.Visible = false;
        }

        if (ddlSearchCriteria.SelectedValue.ToString().Equals("ASQ"))
        {
            MultiView1.SetActiveView(vwAllSLQuotation);
            BindAllShortlistedQuotation();

            //   BindConsultants(ddlAllSLConsultants);

        }
        if (ddlSearchCriteria.SelectedIndex <= 0)
        {
            MultiView1.Visible = false;

            
        }
        else
        {
            MultiView1.Visible = true;
        }
       




    }

    private void BindAllShortlistedQuotation()
    {
        Cls_AdminReport objAdminReport = new Cls_AdminReport();
        try
        {
            DataTable dt = objAdminReport.GetAllShortListedQuotationsRequests();

            //sorting
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            ViewState["AllSLQuotations1"] = dt;
            if (dt.Rows.Count > 0)
            {
                ddl_NoRecords.Visible = true;
                lblRowsToDisplay.Visible = true;
                lblTotalShortlested.Text = "<b>No. of Shortlisted Quotations - </b>" + Convert.ToString(dt.Rows.Count);
            }
            else
            {
                ddl_NoRecords.Visible = false;
                lblRowsToDisplay.Visible = false;
            }

            gvSLRequests.Visible = true;
            gvAllSLQuotations.DataSource = dt;
            gvAllSLQuotations.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvAllSLQuotations.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    #region Code to get request against which the quotations are already shortlisted

    protected void gvSLRequests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkMarkAsDealDone = ((LinkButton)e.Row.FindControl("lnkMarkAsDealDone"));
            Label lblMarkAsDealDone = ((Label)e.Row.FindControl("lblMarkAsDealDone"));
            //Image imgActive = ((Image)e.Row.FindControl("imgActive"));
            //LinkButton lnkbtnActivate = ((LinkButton)e.Row.FindControl("lnkbtnActiveness"));
            if (lnkMarkAsDealDone != null)
            {
                if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsDealDone")))
                {

                    e.Row.CssClass = "griddeactiverow";
                    lblMarkAsDealDone.Visible = true;
                    lnkMarkAsDealDone.Visible = false;

                }
                else
                {
                    e.Row.CssClass = "gridactiverow";
                    lblMarkAsDealDone.Visible = false;
                    lnkMarkAsDealDone.Visible = true;
                }
            }

        }
    }
    protected void btnSLGetRequest_Click(object sender, ImageClickEventArgs e)
    {
        BindSLQuoteOFConsultant();
    }

    public void BindSLQuoteOFConsultant()
    {
        Cls_AdminReport objAdminReport = new Cls_AdminReport();
        try
        {
            objAdminReport.ConsultantId = Convert.ToInt32(ddlSLConsultant.SelectedValue.ToString());
            DataTable dt = objAdminReport.GetShortListedQuotationsRequests();

            //sorting
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            ViewState["gvSLRequests"] = dt;
            if (dt.Rows.Count > 0)
            {
                ddl_NoRecords1.Visible = true;
                lblRowsToDisplay1.Visible = true;
                if (Convert.ToInt32(ddlSLConsultant.SelectedValue.ToString()) == -9999)
                    lblTotalSLQC.Text = "<b>No. of Shortlisted Quotation by All Consultant  - </b>" + Convert.ToString(dt.Rows.Count);
                else
                    lblTotalSLQC.Text = "<b>No. of Shortlisted Quotation by " + Convert.ToString(ddlSLConsultant.SelectedItem.ToString()) + " - </b>" + Convert.ToString(dt.Rows.Count);
            }
            else
            {
                ddl_NoRecords1.Visible = false;
                lblRowsToDisplay1.Visible = false;
            }


            gvSLRequests.Visible = true;
            gvSLRequests.DataSource = dt;
            gvSLRequests.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    protected void ddlSLConsultant_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSLRequests.DataSource = null;
        gvSLRequests.DataBind();
    }
    #endregion


    protected void gvSLRequests_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Cls_General objGeneral = new Cls_General();
        try
        {
            if (e.CommandName == "ViewQuotations")
            {
                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                int intRequestId = Convert.ToInt32(gvSLRequests.DataKeys[RowIndex][0].ToString());
                int intQuotationId = Convert.ToInt32(gvSLRequests.DataKeys[RowIndex][1].ToString());
                String OptionID = gvSLRequests.DataKeys[RowIndex][2].ToString();
                Session[Cls_Constants.SESSION_BACK_PAGE_URL] = "AdminReport.aspx";
                Response.Redirect("~/ViewShortlistedQuotation.aspx?QuoteID=" + intQuotationId.ToString() + "&ReqID=" + intRequestId.ToString() + "&ConsultantID=" + (ddlSLConsultant.SelectedValue.ToString()));

            }

            if (e.CommandName == "ViewDetails")
            {
                if (ViewState[Cls_Constants.ALLOWED_ACTION_ON_PAGE] != null)
                    objGeneral.AllowedActions = ((DataTable)ViewState[Cls_Constants.ALLOWED_ACTION_ON_PAGE]);
                objGeneral.ActionToCheck = e.CommandName;

                //check if user has access to action or not
                //if (objGeneral.CheckForThisAction())
                //{
                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvSLRequests.DataKeys[gvRow.RowIndex][0].ToString());
                int intQuotationId = Convert.ToInt32(gvSLRequests.DataKeys[gvRow.RowIndex][1].ToString());
                String OptionID = gvSLRequests.DataKeys[gvRow.RowIndex][2].ToString();
                Session[Cls_Constants.SESSION_BACK_PAGE_URL] = "AdminReport.aspx";
                Response.Redirect("~/ViewSentRequestDetails.aspx?id=" + intRequestId + "&QuoteID=" + intQuotationId.ToString() + "&OptionID=" + OptionID + "&ConsultantID=" + (ddlSLConsultant.SelectedValue.ToString()));
                //}
                //else
                //{
                //    (this.Master.FindControl("lblMasterMsg") as Label).Text = "Access denied for the selected action!!!";
                //    (this.Master.FindControl("trAccess") as HtmlTableRow).Visible = true;
                //}
            }

            if (e.CommandName.Equals("MarkAsDealDone"))
            {
                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvSLRequests.DataKeys[gvRow.RowIndex][0].ToString());
                int DealerID = Convert.ToInt32(gvSLRequests.DataKeys[gvRow.RowIndex][3].ToString());
                int Points = Convert.ToInt32((new ConfigValues()).GetValue("NO_OF_POINTS_AFTER_DEAL_DONE"));
                Cls_AdminReport objAdminReport = new Cls_AdminReport();
                objAdminReport.DealerId = DealerID;
                objAdminReport.Points = Points;
                objAdminReport.RequestId = intRequestId;
                int Result = objAdminReport.MarkAsDealDone();
                if (Result > 0)
                    lblHeader.Text = "Deal Done Successfully";
                else
                    lblHeader.Text = "Problem Occured while completing the deal";
                btnSLGetRequest_Click(null, null);
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvSLRequests_RowCommand Event : " + ex.Message);
        }
    }
    protected void gvAllSLQuotations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Cls_General objGeneral = new Cls_General();
        try
        {
            if (e.CommandName == "ViewQuotations")
            {
                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                int intRequestId = Convert.ToInt32(gvAllSLQuotations.DataKeys[RowIndex][0].ToString());
                int intQuotationId = Convert.ToInt32(gvAllSLQuotations.DataKeys[RowIndex][1].ToString());
                String OptionID = gvAllSLQuotations.DataKeys[RowIndex][2].ToString();
                string ConsulantID = gvAllSLQuotations.DataKeys[RowIndex][3].ToString();
                Session[Cls_Constants.SESSION_BACK_PAGE_URL] = "AdminReport.aspx";
                Response.Redirect("~/ViewShortlistedQuotation.aspx?QuoteID=" + intQuotationId.ToString() + "&ReqID=" + intRequestId.ToString() + "&ConsultantID=" + ConsulantID + "&FDate=" + ddlSearchCriteria.SelectedIndex);

            }

            if (e.CommandName == "ViewDetails")
            {
                if (ViewState[Cls_Constants.ALLOWED_ACTION_ON_PAGE] != null)
                    objGeneral.AllowedActions = ((DataTable)ViewState[Cls_Constants.ALLOWED_ACTION_ON_PAGE]);
                objGeneral.ActionToCheck = e.CommandName;

                //check if user has access to action or not
                //if (objGeneral.CheckForThisAction())
                //{
                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;

                int intRequestId = Convert.ToInt32(gvAllSLQuotations.DataKeys[gvRow.RowIndex][0].ToString());
                int intQuotationId = Convert.ToInt32(gvAllSLQuotations.DataKeys[gvRow.RowIndex][1].ToString());
                String OptionID = gvAllSLQuotations.DataKeys[gvRow.RowIndex][2].ToString();
                string ConsulantID = gvAllSLQuotations.DataKeys[gvRow.RowIndex][3].ToString();
                Session[Cls_Constants.SESSION_BACK_PAGE_URL] = "AdminReport.aspx";
                Response.Redirect("~/ViewSentRequestDetails.aspx?id=" + intRequestId + "&QuoteID=" + intQuotationId.ToString() + "&OptionID=" + OptionID + "&ConsultantID=" + ConsulantID + "&FDate=" + ddlSearchCriteria.SelectedIndex);
                //}
                //else
                //{
                //    (this.Master.FindControl("lblMasterMsg") as Label).Text = "Access denied for the selected action!!!";
                //    (this.Master.FindControl("trAccess") as HtmlTableRow).Visible = true;
                //}
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvAllSLQuotations_RowCommand Event : " + ex.Message);
        }

    }




    protected void gvAllSLQuotations_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnGetAllShortListedQuotatoins_Click(object sender, EventArgs e)
    {

    }
    protected void ddlAllSLQuotations_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvAllSLQuotations.DataSource = null;
        gvAllSLQuotations.DataBind();
    }
    protected void gvSLRequests_DataBound(object sender, EventArgs e)
    {


    }


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

}