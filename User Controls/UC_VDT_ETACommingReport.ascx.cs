using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;

public partial class User_Controls_UC_VDT_ETACommingReport : System.Web.UI.UserControl
{
    Cls_VDT_Report objCls_VDT_Report = new Cls_VDT_Report();
    Cls_Alies objAlies = new Cls_Alies();
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UC_VDT_ETACommingReport));
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.Parent.Parent.Page.Form.DefaultButton = imgbtnSubmit.UniqueID;
            if (!Page.IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Make";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
                bindDropDown();
                if (Request.QueryString["ShowAdjustment"] == null)
                {
                    grdVechileDelivaryReport.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue);
                    if (Request.QueryString["type"] != null)
                    {
                        drpMake.SelectedValue = Convert.ToString(Request.QueryString["make"]);
                        grdVechileDelivaryReport.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue);
                        drpMake_SelectedIndexChanged(null, null);
                        btnSubmit_Click(null, null);
                    }
                }
                else
                {
                    grdVechileDelivaryReport.PageSize = 5;
                    drpMake.SelectedValue = "ALL";
                    btnSubmit_Click(null, null);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }
    public void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["grdVechileDelivaryReport"] = null;
            bindData();
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }

    }
    public void bindDropDown()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objAlies.GetMake();
            drpMake.DataSource = dt;
            drpMake.DataTextField = "Make";
            drpMake.DataValueField = "ID";
            drpMake.DataBind();
            drpMake.Items.Insert(0, new ListItem("-Select-", "0"));
            drpMake.Items.Insert(1, new ListItem("ALL", "ALL"));
            drpMake.SelectedValue = "ALL";
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }
    public void drpMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(drpMake.SelectedValue) == "0")
            {
                grdVechileDelivaryReport.Visible = false;
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }
    public void grdVechileDelivaryReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdVechileDelivaryReport.PageIndex = e.NewPageIndex;
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["grdVechileDelivaryReport"];
            grdVechileDelivaryReport.DataSource = dt;
            grdVechileDelivaryReport.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }

    }
    public void bindData()
    {
        try
        {

            if (Convert.ToString(drpMake.SelectedValue) == "ALL")
            {
                objCls_VDT_Report.makeid = 0;
            }
            else
            {
                objCls_VDT_Report.makeid = Convert.ToInt32(drpMake.SelectedValue);
            }
            objCls_VDT_Report.UserName = Convert.ToString(Session[Cls_Constants.USER_NAME]);
            DataTable dt = new DataTable();
            if (ViewState["grdVechileDelivaryReport"] == null)
            {
                dt = objCls_VDT_Report.getETACommingCloser_Report();
            }
            else
            {
                dt = (DataTable)ViewState["grdVechileDelivaryReport"];
            }

            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
            if (dt != null)
            {
                grdVechileDelivaryReport.Visible = true;
                grdVechileDelivaryReport.DataSource = dt;
                grdVechileDelivaryReport.DataBind();
                if (dt.Rows.Count > 0)
                {
                    lblRowsToDisplay2.Visible = true;
                    ddl_NoRecords2.Visible = true;
                    grdVechileDelivaryReport.ShowFooter = false; ;

                }
                else
                {
                    lblRowsToDisplay2.Visible = false;
                    ddl_NoRecords2.Visible = false;
                }
                if (Request.QueryString["ShowAdjustment"] == null)
                {

                }
                else
                {
                    HyperLink hyp = (HyperLink)this.Parent.FindControl("hypETA");
                    hyp.NavigateUrl = "~/VDT_ETACommingCloser.aspx?type=4&make=" + Convert.ToString(drpMake.SelectedValue);
                    if (dt.Rows.Count > 0)
                    {
                        Label lbl = (Label)this.Parent.FindControl("lblETAMarquee");
                        string str = lbl.Text.Trim();
                        lbl.Text = "ETA Coming closer for customer " + Convert.ToString(dt.Rows[0]["fullname"]) + " ( " + Convert.ToString(Convert.ToDateTime(dt.Rows[0]["ETA"]).ToString("dd/MM/yyyy")) + " ).";
                    }
                    grdVechileDelivaryReport.ShowFooter = false; ;
                    lblRowsToDisplay2.Visible = false;
                    ddl_NoRecords2.Visible = false;
                }
            }

            ViewState["grdVechileDelivaryReport"] = dt;
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
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
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
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
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grdVechileDelivaryReport.PageIndex = 0;
            if (ddl_NoRecords2.SelectedValue.ToString() == "All")
            {
                //For view 1
                grdVechileDelivaryReport.PageSize = grdVechileDelivaryReport.PageCount * grdVechileDelivaryReport.Rows.Count;
                grdVechileDelivaryReport.DataSource = (DataTable)ViewState["grdVechileDelivaryReport"];
                grdVechileDelivaryReport.DataBind();
            }
            else
            {
                //for view 1
                grdVechileDelivaryReport.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
                grdVechileDelivaryReport.DataSource = (DataTable)ViewState["grdVechileDelivaryReport"];
                grdVechileDelivaryReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }
    public void grdVechileDelivaryReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Request.QueryString["ShowAdjustment"] != null)
                {
                    grdVechileDelivaryReport.Columns[1].Visible = false;
                }
                else
                {
                    grdVechileDelivaryReport.Columns[1].Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }
    public void ResetSearchCriteria()
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Make";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            lblRowsToDisplay2.Visible = false;
            ddl_NoRecords2.Visible = false;
            grdVechileDelivaryReport.Visible = false;
            ViewState["grdVechileDelivaryReport"] = null;
            if (drpMake.Items.Count > 0)
            {
                drpMake.SelectedValue = "ALL";
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }
}
