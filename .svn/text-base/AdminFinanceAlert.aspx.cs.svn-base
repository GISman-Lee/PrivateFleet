using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;

public partial class AdminFinanceAlert : System.Web.UI.Page
{
    #region Private Variables

    ILog logger = LogManager.GetLogger(typeof(AdminFinanceAlert));
    DataTable dt = null;

    #endregion

    #region Page_Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ((Label)this.Master.FindControl("lblHeader")).Text = "Finance Alerts";
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Date1";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
                BindReportData();
            }
        }
        catch (Exception ex)
        {
            logger.Error("AdminFinanceAlert Page_Load Error - " + ex.Message + " :: " + ex.StackTrace);
        }
    }

    #endregion

    #region Events

    protected void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gvAdminFinAlert.PageIndex = 0;
            if (ddl_NoRecords2.SelectedValue.ToString() == "All")
            {
                //For view 1
                gvAdminFinAlert.PageSize = gvAdminFinAlert.PageCount * gvAdminFinAlert.Rows.Count;
                gvAdminFinAlert.DataSource = (DataTable)ViewState["AdminFinAlert"];
                gvAdminFinAlert.DataBind();
            }
            else
            {
                //for view 1
                gvAdminFinAlert.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
                gvAdminFinAlert.DataSource = (DataTable)ViewState["AdminFinAlert"];
                gvAdminFinAlert.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error("AdminFinanceAlert ddl_NoRecords2_SelectedIndexChanged Error - " + ex.Message + " :: " + ex.StackTrace);
        }
    }

    public void gvAdminFinAlert_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAdminFinAlert.PageIndex = e.NewPageIndex;
            dt = new DataTable();
            dt = (DataTable)ViewState["AdminFinAlert"];
            gvAdminFinAlert.DataSource = dt;
            gvAdminFinAlert.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("AdminFinanceAlert grdDeliveryReport_PageIndexChanging Error - " + ex.Message + " :: " + ex.StackTrace);
        }
    }

    protected void gvAdminFinAlert_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            //Swap sort direction
            this.DefineSortDirection();
            BindReportData();

        }
        catch (Exception ex)
        {
            logger.Error("AdminFinanceAlert grdDeliveryReport_Sorting Error - " + ex.Message + " :: " + ex.StackTrace);
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
            logger.Error("AdminFinanceAlert DefineSortDirection Error - " + ex.Message + " :: " + ex.StackTrace);
        }
    }

    /// <summary>
    /// 07 May 2013 : Manoj : Get list of Customer for whome Finance alert was send to fincar admin
    /// </summary>
    /// <returns></returns>
    private void BindReportData()
    {
        Cls_AdminFinanceAlert cls_AdminFinanceAlert = new Cls_AdminFinanceAlert();
        try
        {
            DataTable dtData = new DataTable();

            if (ViewState["AdminFinAlert"] == null)
            {
                dtData = cls_AdminFinanceAlert.GetAdminFinanceAlerts();
            }
            else
            {
                dtData = (DataTable)ViewState["AdminFinAlert"];
            }

            if (dtData != null && dtData.Rows.Count > 0)
            {
                DataView dv = new DataView(dtData);
                dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

                ViewState["DeliveryReportDT"] = dv.ToTable();
                gvAdminFinAlert.DataSource = dv.ToTable();
                gvAdminFinAlert.DataBind();
            }
            else
            {
                gvAdminFinAlert.DataSource = null;
                gvAdminFinAlert.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error("AdminFinanceAlert BindReportData Error - " + ex.Message + " :: " + ex.StackTrace);
        }
    }

    #endregion

}
