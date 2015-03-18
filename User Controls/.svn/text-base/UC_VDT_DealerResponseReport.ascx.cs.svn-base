using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;
public partial class User_Controls_UC_VDT_DealerResponseReport : System.Web.UI.UserControl
{
    Cls_VDT_Report objCls_VDT_Report = new Cls_VDT_Report();
    Cls_Alies objAlies = new Cls_Alies();
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UC_VDT_DealerResponseReport));
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Show_ColorCode();
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "diff";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;

                DealerNonResponseLowervalue.Visible = false;
                lblDealerNonResponseLowervalue.Visible = false;


                bindDropDown();

                if (Request.QueryString["ShowAdjustment"] == null)
                {
                    grdVechileDelivaryReport.PageIndex = Convert.ToInt32(ddl_NoRecords2.SelectedValue);
                    if (Request.QueryString["type"] != null)
                    {

                        drpMake.SelectedValue = Convert.ToString(Request.QueryString["make"]);
                        grdVechileDelivaryReport.ShowFooter = true;
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
                DealerNonResponseLowervalue.Visible = false;
                lblDealerNonResponseLowervalue.Visible = false;
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
            Int32 nonResponse_LowerLimit = 0;
            Int32 nonResponse_UppeLimit = 0;

            ConfigValues objConfigue = new ConfigValues();


            objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS";
            nonResponse_LowerLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());


            objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS";
            nonResponse_UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());


            lblDealerNonResponseLowervalue.Text = "No Dealer response from last" + nonResponse_UppeLimit.ToString() + " days.";


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
                dt = objCls_VDT_Report.getDealerNonResponse_Report();
            }
            else
            {
                dt = (DataTable)ViewState["grdVechileDelivaryReport"];
            }


            DataView dv = null;
            dv = new DataView(dt);
            dv.RowFilter = "Diff>=" + (nonResponse_LowerLimit - 1);
            dt = dv.ToTable();

            dv = dt.DefaultView;



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

                    DealerNonResponseLowervalue.Visible = true;
                    lblDealerNonResponseLowervalue.Visible = true;


                    if (grdVechileDelivaryReport.PageCount > 1)
                    {
                        grdVechileDelivaryReport.FooterRow.Visible = true;
                    }
                    else
                    {
                        grdVechileDelivaryReport.FooterRow.Visible = false;
                    }
                }
                else
                {
                    lblRowsToDisplay2.Visible = false;
                    ddl_NoRecords2.Visible = false;

                    DealerNonResponseLowervalue.Visible = false;
                    lblDealerNonResponseLowervalue.Visible = false;

                }

                if (Request.QueryString["ShowAdjustment"] == null)
                {

                }

                else
                {
                    grdVechileDelivaryReport.PageSize = 5;

                    HyperLink hyp = (HyperLink)this.Parent.FindControl("hypDealerResponse");
                    hyp.NavigateUrl = "~/VDT_DealerNoResponse.aspx?type=2&make=" + Convert.ToString(drpMake.SelectedValue);


                    if (dt.Rows.Count > 0)
                    {
                        Label lbl = (Label)this.Parent.FindControl("lblDealerResponseMarquee");
                        string str = lbl.Text.Trim();
                        lbl.Text = str + "No response from last " + Convert.ToString(dt.Rows[0]["diff"]) + " Days for " + Convert.ToString(dt.Rows[0]["fullname"]) + ".";

                    }
                    lblRowsToDisplay2.Visible = false;
                    ddl_NoRecords2.Visible = false;
                    grdVechileDelivaryReport.ShowFooter = false;
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

                HiddenField hid = (HiddenField)e.Row.FindControl("hidenClientid");
                HyperLink hyp = (HyperLink)e.Row.FindControl("hypupdate");
                hyp.NavigateUrl = "~/ClinetIfo_ForDealer.aspx?tempid=" + hid.Value;

                HiddenField hiddendiff = (HiddenField)e.Row.FindControl("hiddendiff");
                HiddenField HiddenLowerActucalval = (HiddenField)e.Row.FindControl("HiddenLowerActucalval");

                HiddenField hiddenhighvalue = (HiddenField)e.Row.FindControl("hiddenhighvalue");
                if (Convert.ToInt32(hiddendiff.Value) < Convert.ToInt32(HiddenLowerActucalval.Value))
                {
                    //  e.Row.BackColor = "#f34141";

                    e.Row.CssClass = "NoColor";
                }
                else
                {
                    if (Convert.ToInt32(HiddenLowerActucalval.Value) <= Convert.ToInt32(hiddendiff.Value) && Convert.ToInt32(hiddendiff.Value) < Convert.ToInt32(hiddenhighvalue.Value))
                    {
                        e.Row.CssClass = "nearDanger";
                    }
                    else
                    {

                        e.Row.CssClass = "Danger";
                    }
                }

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
            ViewState["grdVechileDelivaryReport"] = null;
            grdVechileDelivaryReport.Visible = false;
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
    public void Show_ColorCode()
    {
        try
        {
            Int32 nonResponse_LowerLimit = 0;
            Int32 nonResponse_UppeLimit = 0;

            ConfigValues objConfigue = new ConfigValues();


            objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS";
            nonResponse_LowerLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());


            objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS";
            nonResponse_UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());


            lblDealerNonResponseLowervalue.Text = "No Dealer response from last" + nonResponse_UppeLimit.ToString() + " days.";
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }
}
