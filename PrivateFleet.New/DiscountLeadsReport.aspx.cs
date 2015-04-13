using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Mechsoft.GeneralUtilities;
using System.Data;

public partial class DiscountLeadsReport : System.Web.UI.Page
{
    #region

    ILog logger = LogManager.GetLogger(typeof(DiscountLeadsReport));

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Label lblHeader = (Label)Master.FindControl("lblHeader");
                if (lblHeader != null)
                {
                    lblHeader.Text = "Leads Report";
                }
                ViewState["LeadReport"] = null;

                //ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "LeadDate";

                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;

                ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
                gvLeadReport.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                CheckDate();
            }
        }
        catch (Exception ex)
        {
        }
        finally
        { }
    }

    #endregion

    #region Events

    protected void ddlLeadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt16(ddlLeadType.SelectedValue) == 1)
            {
                trCompany.Visible = true;
                ddlCompany.SelectedValue = "0";
            }
            else
            {
                trCompany.Visible = false;
                ddlCompany.SelectedValue = "0";
            }
            gvLeadReport.DataSource = null;
            gvLeadReport.DataBind();

        }
        catch (Exception)
        {
        }
    }

    protected void gvLeadReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLeadReport.PageIndex = e.NewPageIndex;
            BindLeads();
        }
        catch (Exception ex)
        {
            logger.Debug("DiscountLeadsReport page gvLeadReport_PageIndexChanging error - " + ex.Message);
        }
    }

    protected void gvLeadReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            //Swap sort direction
            this.DefineSortDirection();

            BindLeads();
        }
        catch (Exception ex)
        {
            logger.Debug("DiscountLeadsReport page gvLeadReport_Sorting error - " + ex.Message);
        }
    }

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_NoRecords.SelectedValue.ToString() == "All")
            {

                gvLeadReport.DataSource = (DataTable)ViewState["LeadReport"];
                gvLeadReport.PageSize = gvLeadReport.PageCount * gvLeadReport.Rows.Count;
                gvLeadReport.DataBind();
            }
            else
            {
                gvLeadReport.DataSource = (DataTable)ViewState["LeadReport"];
                gvLeadReport.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                gvLeadReport.DataBind();
            }
        }
        catch (Exception)
        {
        }
        finally
        {
        }
    }

    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        ViewState["LeadReport"] = null;
        BindLeads();
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        trCompany.Visible = false;
        ddlCompany.SelectedValue = "0";
        ddlLeadType.SelectedValue = "0";
        CheckDate();
    }

    #endregion

    #region Methods

    private void CheckDate()
    {
        txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
        TxtToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
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

    public void BindLeads()
    {
        DataTable dt = null;
        Cls_DiscountLeadsReport objLeadReport = new Cls_DiscountLeadsReport();
        IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);

        try
        {
            if (ViewState["LeadReport"] != null)
                dt = (DataTable)ViewState["LeadReport"];
            else
            {
                objLeadReport.LeadType = Convert.ToInt16(ddlLeadType.SelectedValue);
                objLeadReport.Company = Convert.ToInt16(ddlCompany.SelectedValue);
                objLeadReport.FromDate = DateTime.Parse(txtCalenderFrom.Text.Trim(), culture); ;
                objLeadReport.ToDate = DateTime.Parse(TxtToDate.Text.Trim(), culture); ;

                dt = objLeadReport.getDiscountLeadsReport();
            }

            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            ViewState["LeadReport"] = dt;

            if (dt != null && dt.Rows.Count > 0)
            {
                lblRowsToDisplay.Visible = true;
                ddl_NoRecords.Visible = true;
                gvLeadReport.DataSource = dt;
                gvLeadReport.DataBind();
            }
            else
            {
                gvLeadReport.DataSource = null;
                gvLeadReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Generate survey report error - " + ex.Message);
        }
        finally
        {
            objLeadReport = null;
        }
    }

    #endregion


}
