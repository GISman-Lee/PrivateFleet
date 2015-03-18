using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;

public partial class VDT_Customer_User_Controls_UC_VDT_AutomaticMailSendReport : System.Web.UI.UserControl
{
    #region Private fleet
    ILog Logger = LogManager.GetLogger(typeof(VDT_Customer_User_Controls_UC_VDT_AutomaticMailSendReport));
    Cls_VDTAdminReport objCls_VDTAdminReport = new Cls_VDTAdminReport();
    DataTable dt = new DataTable();
    IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.Parent.Parent.Page.Form.DefaultButton = btnGenerateReport.UniqueID;
            if (!Page.IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "date";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;

                txtFromDate.Text = Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
                txtToDate.Text = Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }
    public void btnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["_AutomaticMailDT"] = null;
            bindData();
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }

    }
    public void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ResetSearchCriteria();
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }
    public void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            grdAutomaticMail.PageIndex = 0;
            if (ddl_NoRecords2.SelectedValue.ToString() == "All")
            {
                //For view 1
                grdAutomaticMail.PageSize = grdAutomaticMail.PageCount * grdAutomaticMail.Rows.Count;
                grdAutomaticMail.DataSource = (DataTable)ViewState["_AutomaticMailDT"];
                grdAutomaticMail.DataBind();
            }
            else
            {
                //for view 1
                grdAutomaticMail.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
                grdAutomaticMail.DataSource = (DataTable)ViewState["_AutomaticMailDT"];
                grdAutomaticMail.DataBind();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }
    public void grdAutomaticMail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdAutomaticMail.PageIndex = e.NewPageIndex;
            dt = new DataTable();
            dt = (DataTable)ViewState["_AutomaticMailDT"];
            grdAutomaticMail.DataSource = dt;
            grdAutomaticMail.DataBind();
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }
    protected void grdAutomaticMail_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            //Swap sort direction
            this.DefineSortDirection();

            // BindData(objCourseMaster);
            bindData();
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }

    #endregion

    #region Methods

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
            Logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void bindData()
    {
        try
        {
            DateTime fromdate;
            DateTime toDate;
            fromdate = DateTime.Parse(txtFromDate.Text.Trim(), culture);
            toDate = DateTime.Parse(txtToDate.Text.Trim(), culture);
            objCls_VDTAdminReport.FromDate = fromdate;
            objCls_VDTAdminReport.ToDate = toDate;
            if (ViewState["_AutomaticMailDT"] == null)
            {

                dt = objCls_VDTAdminReport.get_VDTAutomaticMail();
            }
            else
            {
                dt = (DataTable)ViewState["_AutomaticMailDT"];
            }
            DataView dv = new DataView(dt);
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
            grdAutomaticMail.DataSource = dt;
            grdAutomaticMail.DataBind();
            ViewState["_AutomaticMailDT"] = dt;
            grdAutomaticMail.Visible = true;
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
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void ResetSearchCriteria()
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "date";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            lblRowsToDisplay2.Visible = false;
            ddl_NoRecords2.Visible = false;

            txtFromDate.Text = Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
            txtToDate.Text = Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
            ViewState["_AutomaticMailDT"] = null;

            grdAutomaticMail.Visible = false;
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }
    #endregion
}
