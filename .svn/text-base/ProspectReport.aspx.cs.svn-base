using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;

public partial class ProspectReport : System.Web.UI.Page
{
    #region Veriable
    static ILog logger = LogManager.GetLogger(typeof(ProspectReport));
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)this.Master.FindControl("lblHeader")).Text = "Prospect Report";
        if (!IsPostBack)
        {
            try
            {
                CheckDate();

                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                ddl_NoRecords.SelectedIndex = 3;
                gvProspect.PageSize = Convert.ToInt16(ddl_NoRecords.SelectedValue);
                // GenerateReport();
            }
            catch (Exception ex)
            {
                logger.Error("Error in  Page load" + ex.Message);
            }
        }
    }

    #region Event

    protected void gvProspect_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = new DataTable();
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();

        DataView dv = ((DataTable)ViewState["gvProspect"]).DefaultView;
        dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

        dt = dv.ToTable();
        gvProspect.DataSource = dt;
        gvProspect.DataBind();
        ViewState["gvProspect"] = dt;
    }

    protected void gvProspect_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProspect.PageIndex = e.NewPageIndex;
        gvProspect.DataSource = (DataTable)ViewState["gvProspect"];
        gvProspect.DataBind();
    }


    protected void gvProspect_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvProspect.PageIndex = 0;

        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {
            //for view 3 (vwAllSLQuotation)
            gvProspect.PageSize = gvProspect.PageCount * gvProspect.Rows.Count;
            gvProspect.DataSource = (DataTable)ViewState["gvProspect"];
            gvProspect.DataBind();

        }
        else
        {
            //for view 3 (vwAllSLQuotation)
            gvProspect.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvProspect.DataSource = (DataTable)ViewState["gvProspect"];
            gvProspect.DataBind();
        }
    }

    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        CheckDate();
        gvProspect.DataSource = null;
        gvProspect.DataBind();
    }

    #endregion

    #region Methods

    private void CheckDate()
    {
        txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
        txtCalenderToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
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
            logger.Error("Error in DefineSortDirection" + ex.Message);
        }
    }

    private void GenerateReport()
    {
        Cls_General objSMSReport = new Cls_General();
        try
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
            DateTime dtFromDate = DateTime.Parse(Request.Params[txtCalenderFrom.UniqueID].ToString(), culture);
            DateTime dtToDate = DateTime.Parse(Request.Params[txtCalenderToDate.UniqueID].ToString(), culture);
            calFrom.SelectedDate = dtFromDate;
            calTO.SelectedDate = dtToDate;
            if (dtFromDate > dtToDate)
            {
                Page page = HttpContext.Current.Handler as Page;

                if (page != null)
                {
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Start date should be greater than or equal to end date');", true);

                }
            }
            else
            {
                if (dtFromDate != null)
                {
                    objSMSReport.FromDate = dtFromDate;
                }

                if (dtToDate != null)
                {
                    objSMSReport.ToDate = dtToDate;
                }

            }

            ViewState["gvProspect"] = null;
            DataTable dt = new DataTable();

            if (ViewState["gvProspect"] == null)
                dt = objSMSReport.getProspectReport();
            else
                dt = (DataTable)ViewState["gvProspect"];


            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

            dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                lblRowsToDisplay2.Visible = true;
                ddl_NoRecords.Visible = true;
                ddl_NoRecords.SelectedIndex = 3;
                gvProspect.DataSource = dt;
                gvProspect.DataBind();
                ViewState["gvProspect"] = dt;
            }
            else
            {
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords.Visible = false;
                gvProspect.DataSource = null;
                gvProspect.DataBind();
            }

        }
        catch (Exception ex)
        {
            logger.Error("Error in Bind Data" + ex.Message);
        }
    }

    #endregion



}
