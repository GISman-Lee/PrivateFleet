using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;

public partial class User_Controls_Uc_VDT_CustomerHelpStausRpt : System.Web.UI.UserControl
{

    #region Private Variables

    ILog Logger = LogManager.GetLogger(typeof(User_Controls_Uc_VDT_CustomerHelpStausRpt));
    Cls_VDTAdminReport objCls_VDTAdminReport = new Cls_VDTAdminReport();
    DataTable dt = new DataTable();
    IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Parent.Parent.Page.Form.DefaultButton = btnGenerateReport.UniqueID;
        //this.Parent.Parent.Page.Master.Page.Form.DefaultButton = "btnGenerateReport";

        try
        {
            if(!Page.IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "CustomerRequestedDate";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;

                System.Globalization.CultureInfo cultr = new System.Globalization.CultureInfo("en-au");
                ajxFromDateCalender.Format = cultr.DateTimeFormat.ShortDatePattern;
                ajxTodateCalender.Format = cultr.DateTimeFormat.ShortDatePattern;

                txtFromDate.Text = (DateTime.Today.AddMonths(-1)).ToString("dd/MM/yyyy");
                txtToDate.Text = (DateTime.Today.ToString("dd/MM/yyyy"));
                //if (Convert.ToString(Request.QueryString["showadjustment"]) != null)
                //{
                //    grdAdminHelpCustomerRpt.PageSize = 5;
                //    btnGenerateReport_Click(null, null);
                //}
                //else
                //{
                //    grdAdminHelpCustomerRpt.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue);
                //    if (Convert.ToString(Request.QueryString["type"]) != null)
                //    {
                //        txtFromDate.Text = Convert.ToString(Request.QueryString["fromdate"]);
                //        txtToDate.Text = Convert.ToString((Request.QueryString["todate"]));
                //        btnGenerateReport_Click(null, null);
                //    }
                //}
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex.Message.ToString() + ex.InnerException.ToString());
            throw;
        }
    }

    public void btnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["_AdminHelpCustomerRptDt"] = null;
            BindData();
        }
        catch (Exception ex)
        {
            Logger.Error(ex.Message.ToString());
        }
    }

    public void btnCancel_Click(object sender, EventArgs e)
    {
        resetSeachCriterial();
    }

    public void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdAdminHelpCustomerRpt.PageIndex = 0;
        if (ddl_NoRecords2.SelectedValue.ToString() == "All")
        {
            //For view 1
            grdAdminHelpCustomerRpt.PageSize = grdAdminHelpCustomerRpt.PageCount * grdAdminHelpCustomerRpt.Rows.Count;
            grdAdminHelpCustomerRpt.DataSource = (DataTable)ViewState["_AdminHelpCustomerRptDt"];
            grdAdminHelpCustomerRpt.DataBind();
        }
        else
        {
            //for view 1
            grdAdminHelpCustomerRpt.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
            grdAdminHelpCustomerRpt.DataSource = (DataTable)ViewState["_AdminHelpCustomerRptDt"];
            grdAdminHelpCustomerRpt.DataBind();
        }
    }

    public void grdAdminHelp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAdminHelpCustomerRpt.PageIndex = e.NewPageIndex;
        dt = new DataTable();
        dt = (DataTable)ViewState["_AdminHelpCustomerRptDt"];
        grdAdminHelpCustomerRpt.DataSource = dt;
        grdAdminHelpCustomerRpt.DataBind();
    }

    protected void grdAdminHelp_Sorting(object sender, GridViewSortEventArgs e)
    {

        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;

        //Swap sort direction
        this.DefineSortDirection();

        // BindData(objCourseMaster);
        BindData();
    }
       
    #endregion

    #region Methods

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

    public void BindData()
    {
        try
        {
            DateTime fromdate;
            DateTime toDate;
            fromdate = DateTime.Parse(txtFromDate.Text.Trim(), culture);
            toDate = DateTime.Parse(txtToDate.Text.Trim(), culture);
            objCls_VDTAdminReport.FromDate = fromdate;
            objCls_VDTAdminReport.ToDate = toDate;
            if (ViewState["_AdminHelpCustomerRptDt"] == null)
            {
                dt = objCls_VDTAdminReport.get_VDTAdminHelpCustomerStatusRpt();
            }
            else
            {
                dt = (DataTable)ViewState["_AdminHelpCustomerRptDt"];
            }
            DataView dv = new DataView(dt);
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
            grdAdminHelpCustomerRpt.DataSource = dt;
            grdAdminHelpCustomerRpt.DataBind();
            ViewState["_AdminHelpCustomerRptDt"] = dt;
            grdAdminHelpCustomerRpt.Visible = true;
            if (dt.Rows.Count > 0)
            {
                lblRowsToDisplay2.Visible = true;
                ddl_NoRecords2.Visible = true;
            }
            else
            {
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
            }

            //grdAdminHelpCustomerRpt.Columns[2].Visible = true;
            //if (Convert.ToString(Request.QueryString["showadjustment"]) != null)
            //{
            //    lblRowsToDisplay2.Visible = false;
            //    ddl_NoRecords2.Visible = false;
            //    grdAdminHelpCustomerRpt.Columns[2].Visible = false;

            //    if (dt.Rows.Count > 0)
            //    {
            //        Label lblCustomerHelpMarquee = (Label)this.Parent.FindControl("lblCustomerHelpMarquee");
            //        // lblCustomerHelpMarquee.Text = Convert.ToString(dt.Rows[0]["fullname"]) + " has requested to Admin for HELP on " + Convert.ToDateTime(dt.Rows[0]["date"]).ToString("dd/MMM/yyyy") + ".";
            //        lblCustomerHelpMarquee.Text = Convert.ToString(dt.Rows[0]["fullname"]) + " has requested to Admin for HELP on " + Convert.ToDateTime(dt.Rows[0]["date"]).ToString("dd/MMM/yyyy") + ".";

            //    }
            //    HyperLink hypCustomerNeedHelp = (HyperLink)this.Parent.FindControl("hypCustomerNeedHelp");
            //    hypCustomerNeedHelp.NavigateUrl = "~/Admin_CustomerHelpReport.aspx?type=1" + "&fromdate=" + txtFromDate.Text.Trim() + "&todate=" + txtToDate.Text.Trim();
            //}
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void resetSeachCriterial()
    {
        ViewState["_AdminHelpCustomerRptDt"] = null;
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "date";
        ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
        lblRowsToDisplay2.Visible = false;
        ddl_NoRecords2.Visible = false;

        txtFromDate.Text = Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
        txtToDate.Text = Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
        grdAdminHelpCustomerRpt.Visible = false;
    }

    #endregion
}
