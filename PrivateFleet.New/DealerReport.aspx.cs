using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Mechsoft.GeneralUtilities;

public partial class DealerReport : System.Web.UI.Page
{
    Cls_DealerReportHelper objHelper = new Cls_DealerReportHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label lblHeader = (Label)Master.FindControl("lblHeader");
            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            gvDealerReport.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            
            ddl_NoRecords.Visible = false;
            lblRowsToDisplay.Visible = false;

            if (lblHeader != null)
                lblHeader.Text = "Dealer Report";

            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Dealer";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
        }
    }
    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        Binddata();
    }

    private void Binddata()
    {
        DataTable dt1 = null;
        gvDealerReport.DataSource = dt1;
        gvDealerReport.DataBind();
        DataTable dt = new DataTable();
        lblMsg.Visible = false;
        if (Convert.ToInt32(ddlSearch.SelectedValue) == 1)
        {
            DateTime date1;
            date1 = DateTime.Today;//.AddDays(1);
            dt = objHelper.GetReportForToday(date1);
        }
        if (Convert.ToInt32(ddlSearch.SelectedValue) == 2)
        {
            DateTime date1;
            date1 = DateTime.Today.Subtract(TimeSpan.FromDays(1));
            dt = objHelper.GetReportForYesterday(date1);
        }
        if (Convert.ToInt32(ddlSearch.SelectedValue) == 3)
        {
            DateTime date1;
            date1 = DateTime.Today.Subtract(TimeSpan.FromDays(7));
            dt = objHelper.GetReportForThisMonth(date1);
        }
        if (Convert.ToInt32(ddlSearch.SelectedValue) == 4)
        {
            DateTime date1;
            int days = 0;
            days = DateTime.Today.Day;
            date1 = DateTime.Today.Subtract(TimeSpan.FromDays(days));
            dt = objHelper.GetReportForThisMonth(date1);
        }
        if (Convert.ToInt32(ddlSearch.SelectedValue) == 5)
        {
            DateTime date1;
            DateTime date2;
            int days = 31;
            days = days + DateTime.Today.Day;
            date1 = DateTime.Today.Subtract(TimeSpan.FromDays(days));
            date2 = DateTime.Today.Subtract(TimeSpan.FromDays(DateTime.Today.Day));
            dt = objHelper.GetReportForLastMonth(date1, date2);
        }
        if (Convert.ToInt32(ddlSearch.SelectedValue) == 6)
        {
            dt = objHelper.GetReportForAllTime();
        }
        if (dt.Rows.Count == 0 || dt == null)
        {
            lblMsg.Visible = true;
        }
        
        ViewState["dt"] = dt;
        if (dt.Rows.Count == 0)
        {
            ddl_NoRecords.Visible = false;
            lblRowsToDisplay.Visible = false;
        }
        else
        {
            ddl_NoRecords.Visible = true;
            lblRowsToDisplay.Visible = true;
        }

        //sorting
        DataView dv = dt.DefaultView;
        dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
        dt = dv.ToTable();


        gvDealerReport.DataSource = dt;
        gvDealerReport.DataBind();
    }
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        ddlSearch.SelectedIndex = 0;
        DataTable dt = null;
        gvDealerReport.DataSource = dt;
        gvDealerReport.DataBind();
        //for (int i = 0; i < gvDealerReport.Rows.Count; i++)
        //{
        //    gvDealerReport.DeleteRow(i);
        //}
    }
    protected void gvDealerReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void gvDealerReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDealerReport.PageIndex = e.NewPageIndex;
        Binddata();
    }
    protected void gvDealerReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        Binddata();
    }


    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
     
            if (ddl_NoRecords.SelectedValue.ToString() == "All")
            {

                gvDealerReport.DataSource = (DataTable)ViewState["dt"];
                gvDealerReport.PageSize = gvDealerReport.PageCount * gvDealerReport.Rows.Count;
                gvDealerReport.DataBind();
                //BindData();
            }
            else
            {
                gvDealerReport.DataSource = (DataTable)ViewState["dt"];
                gvDealerReport.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                gvDealerReport.DataBind();
                // BindData();
            }
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
