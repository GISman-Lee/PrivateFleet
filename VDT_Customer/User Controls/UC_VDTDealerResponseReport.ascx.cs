using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;

public partial class VDT_Customer_User_Controls_UC_VDTDealerResponseReport : System.Web.UI.UserControl
{
    ILog Logger = LogManager.GetLogger(typeof(VDT_Customer_User_Controls_UC_VDTDealerResponseReport));
    Cls_VDTAdminReport objCls_VDTAdminReport = new Cls_VDTAdminReport();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                ViewState["_DealerResponseDT"] = null;
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "diff";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;

                DealerNonResponseLowervalue.Visible = false;
                lblDealerNonResponseLowervalue.Visible = false;

                if (Convert.ToString(Request.QueryString["showadjustment"]) == null)
                {
                    grdDealerResponse.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue);
                }
                else
                {
                    grdDealerResponse.PageSize = 5;
                }

                Int32 nonResponse_LowerLimit = 0;
                Int32 nonResponse_UppeLimit = 0;

                ConfigValues objConfigue = new ConfigValues();

                objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS";
                nonResponse_LowerLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS";
                nonResponse_UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                lblDealerNonResponseLowervalue.Text = "No Dealer response, Update Overdue"; // from " + nonResponse_UppeLimit.ToString() + " days.";
                bindData();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("Page_Load :" + ex.Message));
        }
    }

    public void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grdDealerResponse.PageIndex = 0;
            if (ddl_NoRecords2.SelectedValue.ToString() == "All")
            {
                //For view 1
                grdDealerResponse.PageSize = ((DataTable)ViewState["_DealerResponseDT"]).Rows.Count;// grdDealerResponse.PageCount * grdDealerResponse.Rows.Count;
                grdDealerResponse.DataSource = (DataTable)ViewState["_DealerResponseDT"];
                grdDealerResponse.DataBind();
            }
            else
            {
                //for view 1
                grdDealerResponse.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
                grdDealerResponse.DataSource = (DataTable)ViewState["_DealerResponseDT"];
                grdDealerResponse.DataBind();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("ddl_NoRecords2_SelectedIndexChanged :" + ex.Message));
        }
    }

    public void grdDealerResponse_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDealerResponse.PageIndex = e.NewPageIndex;
            dt = new DataTable();
            dt = (DataTable)ViewState["_DealerResponseDT"];
            grdDealerResponse.DataSource = dt;
            grdDealerResponse.DataBind();
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("grdDealerResponse_PageIndexChanging :" + ex.Message));
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
            Logger.Error(Convert.ToString("DefineSortDirection :" + ex.Message));
        }
    }

    protected void grdDealerResponse_Sorting(object sender, GridViewSortEventArgs e)
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
            Logger.Error(Convert.ToString("grdDealerResponse_Sorting :" + ex.Message));
        }
    }


    public void bindData()
    {
        try
        {
            if (ViewState["_DealerResponseDT"] == null)
            {
                dt = objCls_VDTAdminReport.get_VDTDealerResponse();
            }
            else
            {
                dt = (DataTable)ViewState["_DealerResponseDT"];
            }

            DataView dv = new DataView(dt);
            // do not show the records which are unmarked by Admin and not updated
            dv.RowFilter = "Unmark=0";
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            dt = getOverDuesRecord(dt);
            grdDealerResponse.DataSource = dt;
            grdDealerResponse.DataBind();
            ViewState["_DealerResponseDT"] = dt;
            if (dt.Rows.Count > 0)
            {
                lblRowsToDisplay2.Visible = true;
                ddl_NoRecords2.Visible = true;

                DealerNonResponseLowervalue.Visible = true;
                lblDealerNonResponseLowervalue.Visible = true;
            }
            else
            {
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;

                DealerNonResponseLowervalue.Visible = false;
                lblDealerNonResponseLowervalue.Visible = false;
            }
            grdDealerResponse.Columns[2].Visible = true;

            if (Convert.ToString(Request.QueryString["showadjustment"]) != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Label lblDealerNoResponseMarquee = (Label)this.Parent.FindControl("lblDealerNoResponseMarquee");
                    //  lblDealerNoResponseMarquee.Text = Convert.ToString(dt.Rows[0]["name"]) + " has no response from last " + Convert.ToString(dt.Rows[0]["diff"]) + " Days.";
                    lblDealerNoResponseMarquee.Text = "No response from last " + Convert.ToString(dt.Rows[0]["diff"]) + " Days for " + Convert.ToString(dt.Rows[0]["name"]) + ".";
                }
                grdDealerResponse.Columns[2].Visible = false;
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
                grdDealerResponse.PageSize = 5;
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }

    private DataTable getOverDuesRecord(DataTable dtTemp)
    {
        try
        {
            ConfigValues objConfigue = new ConfigValues();
            int UppeLimit = 0;
            DataTable dtNew = new DataTable();
            dtNew = dtTemp.Clone();

            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                int DateDiff = Convert.ToInt32(dtTemp.Rows[i]["Diff"]);
                if (dtTemp.Rows[i]["ETA"] == null || Convert.ToString(dtTemp.Rows[i]["ETA"]).Equals(String.Empty))
                    continue;
                DateTime DeliveryDate = Convert.ToDateTime(dtTemp.Rows[i]["ETA"]);
                TimeSpan dateDiff = DeliveryDate.Subtract(System.DateTime.Now);

                if (dateDiff.Days <= 50)
                {
                    objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS";
                    UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
                }
                else if (dateDiff.Days > 50 && dateDiff.Days <= 120)
                {
                    objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS_50_TO_120";
                    UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
                }
                else if (dateDiff.Days > 120)
                {
                    objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS_MORE_120";
                    UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
                }
                if (DateDiff >= UppeLimit)
                {
                    dtNew.Rows.Add(dtTemp.Rows[i].ItemArray);
                }
            }
            return dtNew;
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
            return null;
        }
        finally
        {
        }
    }

    public void grdDealerResponse_RowDataBound(object sener, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // e.Row.CssClass = "Danger";
                //HiddenField hiddencsss = (HiddenField)e.Row.FindControl("hiddencsss");
                //e.Row.CssClass = Convert.ToString(hiddencsss.Value);

                ConfigValues objConfigue = new ConfigValues();
                int UppeLimit = 0;

                HiddenField hdfDateDiff = (HiddenField)e.Row.FindControl("hdfDateDiff");
                DateTime DeliveryDate = Convert.ToDateTime(((HiddenField)e.Row.FindControl("hdfeta")).Value);
                TimeSpan dateDiff = DeliveryDate.Subtract(System.DateTime.Now);

                if (dateDiff.Days <= 50)
                {
                    objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS";
                    UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
                }
                else if (dateDiff.Days > 50 && dateDiff.Days <= 120)
                {
                    objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS_50_TO_120";
                    UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
                }
                else if (dateDiff.Days > 120)
                {
                    objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS_MORE_120";
                    UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
                }

                if (Convert.ToInt32(hdfDateDiff.Value) >= UppeLimit)
                {
                    e.Row.CssClass = "Danger";
                }
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
            ViewState["_DealerResponseDT"] = null;
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("ResetSearchCriteria :" + ex.Message));
        }
    }
}
