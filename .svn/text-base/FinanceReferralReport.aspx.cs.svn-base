using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Mechsoft.GeneralUtilities;
using System.Data;

public partial class FinanceReferralReport : System.Web.UI.Page
{
    #region Varirbles
    static ILog logger = LogManager.GetLogger(typeof(FinanceReferralReport));
    #endregion

    #region Page_Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pnlFinRefReport.DefaultButton = "btnGenerateReport";
            ((Label)Master.FindControl("lblHeader")).Text = "Finance Referral Report";
            if (!IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "ConsultantName";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;

                ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
                gvFinanceReferralReport.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                ddl_NoRecords.Visible = false;
                lblRowsToDisplay.Visible = false;

                CheckDate();
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Finance Referral Page Load error - " + ex.Message);
        }
        finally
        {

        }
    }

    #endregion

    #region Events

    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            logger.Error("Finance Referral btnGenerateReport_Click error - " + ex.Message);
        }
        finally
        {
        }
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            logger.Error("Finance Referral btnCancel_Click error - " + ex.Message);
        }
        finally
        {
        }
    }

    protected void gvFinanceReferralReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvFinanceReferralReport.PageIndex = e.NewPageIndex;
            gvFinanceReferralReport.DataSource = (DataTable)ViewState["dt"];
            gvFinanceReferralReport.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("Finance Referral gvFinanceReferralReport_PageIndexChanged error - " + ex.Message);
        }
        finally
        {
        }
    }

    protected void gvFinanceReferralReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;

            //Swap sort direction
            this.DefineSortDirection();

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["dt"];
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            gvFinanceReferralReport.DataSource = dt;
            gvFinanceReferralReport.DataBind();

            ViewState["dt"] = dt;
        }
        catch (Exception ex)
        {
            logger.Error("Finance Referral gvFinanceReferralReport_Sorting error - " + ex.Message);
        }
        finally
        {
        }
    }

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {

        gvFinanceReferralReport.PageIndex = 0;
        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {

            gvFinanceReferralReport.DataSource = (DataTable)ViewState["dt"];
            gvFinanceReferralReport.PageSize = gvFinanceReferralReport.PageCount * gvFinanceReferralReport.Rows.Count;
            gvFinanceReferralReport.DataBind();
        }
        else
        {
            gvFinanceReferralReport.DataSource = (DataTable)ViewState["dt"];
            gvFinanceReferralReport.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvFinanceReferralReport.DataBind();
        }
    }

    #endregion

    #region Methods


    private void CheckDate()
    {
        txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
        TxtToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
    }

    private void BindGrid()
    {
        Cls_Reports objReport = new Cls_Reports();
        try
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);

            objReport.FromDate = DateTime.Parse(txtCalenderFrom.Text.Trim(), culture);
            objReport.ToDate = DateTime.Parse(TxtToDate.Text.Trim(), culture);
            objReport.ConsultantName = Convert.ToString(txtConsultantName.Text.Trim());
            objReport.surname = Convert.ToString(txtSurname.Text.Trim());
            DataTable dt = objReport.GetFinanceReferralReport();

            DataView dv = dt.DefaultView;
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

            gvFinanceReferralReport.DataSource = dt;
            gvFinanceReferralReport.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("Finance Referral BindGrid error - " + ex.Message);
        }
        finally
        {
        }
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

}
