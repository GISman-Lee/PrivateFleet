using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;
public partial class User_Controls_Uc_VDT_CustomerHelp : System.Web.UI.UserControl
{
    #region Private Variables
    ILog Logger = LogManager.GetLogger(typeof(User_Controls_Uc_VDT_CustomerHelp));
    Cls_VDTAdminReport objCls_VDTAdminReport = new Cls_VDTAdminReport();
    DataTable dt = new DataTable();
    IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
    public const string strStartConst = "Customer Comment </br>";
    public const string strEndConst = "Thanks";

    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Parent.Parent.Page.Form.DefaultButton = btnGenerateReport.UniqueID;
        //this.Parent.Parent.Page.Master.Page.Form.DefaultButton = "btnGenerateReport";

        try
        {

            if (!Page.IsPostBack)
            {

                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "date";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;

                txtFromDate.Text = (DateTime.Today.AddMonths(-1)).ToString("dd/MM/yyyy");
                txtToDate.Text = (DateTime.Today.ToString("dd/MM/yyyy"));
                if (Convert.ToString(Request.QueryString["showadjustment"]) != null)
                {
                    grdAdminHelp.PageSize = 5;
                    btnGenerateReport_Click(null, null);
                }
                else
                {

                    grdAdminHelp.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue);
                    if (Convert.ToString(Request.QueryString["type"]) != null)
                    {
                        txtFromDate.Text = Convert.ToString(Request.QueryString["fromdate"]);
                        txtToDate.Text = Convert.ToString((Request.QueryString["todate"]));
                        btnGenerateReport_Click(null, null);
                    }
                }


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

            ViewState["_AdminHelpDt"] = null;
            bindData();
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
        grdAdminHelp.PageIndex = 0;
        if (ddl_NoRecords2.SelectedValue.ToString() == "All")
        {
            //For view 1
            grdAdminHelp.PageSize = grdAdminHelp.PageCount * grdAdminHelp.Rows.Count;
            grdAdminHelp.DataSource = (DataTable)ViewState["_AdminHelpDt"];
            grdAdminHelp.DataBind();
        }
        else
        {
            //for view 1
            grdAdminHelp.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
            grdAdminHelp.DataSource = (DataTable)ViewState["_AdminHelpDt"];
            grdAdminHelp.DataBind();
        }
    }

    public void grdAdminHelp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAdminHelp.PageIndex = e.NewPageIndex;
        dt = new DataTable();
        dt = (DataTable)ViewState["_AdminHelpDt"];
        grdAdminHelp.DataSource = dt;
        grdAdminHelp.DataBind();
    }

    protected void grdAdminHelp_Sorting(object sender, GridViewSortEventArgs e)
    {

        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;


        //Swap sort direction
        this.DefineSortDirection();

        // BindData(objCourseMaster);
        bindData();
    }

    protected void grdAdminHelp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDesc = new Label();
                lblDesc = (Label)e.Row.FindControl("lblDescription");
                if (lblDesc.Text.Trim().Contains(strStartConst) && lblDesc.Text.Trim().Contains(strEndConst))
                {
                    int startIndx = lblDesc.Text.Trim().LastIndexOf(strStartConst);

                    lblDesc.Text = lblDesc.Text.Trim().Replace(lblDesc.Text.Trim().Substring(0, startIndx + strStartConst.Length), "");
                    int endIndx = lblDesc.Text.Trim().LastIndexOf(strEndConst);
                    lblDesc.Text = lblDesc.Text.Trim().Replace(lblDesc.Text.Trim().Substring(endIndx, (lblDesc.Text.Trim().Length - endIndx)), "");

                    endIndx = lblDesc.Text.Trim().LastIndexOf("</p>");
                    lblDesc.Text = lblDesc.Text.Trim().Replace(lblDesc.Text.Trim().Substring(endIndx, (lblDesc.Text.Trim().Length - endIndx)), "");
                }
                else
                {
                    Logger.Debug("grdAdminHelpCustomerRpt_RowDataBound_Error: String Constants are not found.." );
                }
            }
            //<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear Catherine,</p><p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Craig Slattery has just requested assistance with the delivery of their new BMW Hilux.</p><p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Customer Comment </br>test msg </p><p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Thanks</br>Quotacon</p>
        }
        catch (Exception ex)
        {
            Logger.Error("grdAdminHelpCustomerRpt_RowDataBound_Error: " + ex.Message.ToString());
        }
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
            if (ViewState["_AdminHelpDt"] == null)
            {

                dt = objCls_VDTAdminReport.get_VDTAdminHelpReport();
            }
            else
            {
                dt = (DataTable)ViewState["_AdminHelpDt"];
            }
            DataView dv = new DataView(dt);
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
            grdAdminHelp.DataSource = dt;
            grdAdminHelp.DataBind();
            ViewState["_AdminHelpDt"] = dt;
            grdAdminHelp.Visible = true;
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
            grdAdminHelp.Columns[2].Visible = true;
            if (Convert.ToString(Request.QueryString["showadjustment"]) != null)
            {
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
                grdAdminHelp.Columns[2].Visible = false;

                if (dt.Rows.Count > 0)
                {
                    Label lblCustomerHelpMarquee = (Label)this.Parent.FindControl("lblCustomerHelpMarquee");
                    // lblCustomerHelpMarquee.Text = Convert.ToString(dt.Rows[0]["fullname"]) + " has requested to Admin for HELP on " + Convert.ToDateTime(dt.Rows[0]["date"]).ToString("dd/MMM/yyyy") + ".";
                    lblCustomerHelpMarquee.Text = Convert.ToString(dt.Rows[0]["fullname"]) + " has requested to Admin for HELP on " + Convert.ToDateTime(dt.Rows[0]["date"]).ToString("dd/MMM/yyyy") + ".";

                }
                HyperLink hypCustomerNeedHelp = (HyperLink)this.Parent.FindControl("hypCustomerNeedHelp");
                hypCustomerNeedHelp.NavigateUrl = "~/Admin_CustomerHelpReport.aspx?type=1" + "&fromdate=" + txtFromDate.Text.Trim() + "&todate=" + txtToDate.Text.Trim();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void resetSeachCriterial()
    {
        ViewState["_AdminHelpDt"] = null;
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "date";
        ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
        lblRowsToDisplay2.Visible = false;
        ddl_NoRecords2.Visible = false;

        txtFromDate.Text = Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
        txtToDate.Text = Convert.ToString(DateTime.Today.ToString("dd/MM/yyyy"));
        grdAdminHelp.Visible = false;
    }
    #endregion
}
