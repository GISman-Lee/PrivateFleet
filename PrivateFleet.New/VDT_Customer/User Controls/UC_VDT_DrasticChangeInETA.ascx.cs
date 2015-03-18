using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;


public partial class VDT_Customer_UC_VDT_DrasticChangeInETA : System.Web.UI.UserControl
{
    #region Private Variables
    ILog Logger = LogManager.GetLogger(typeof(VDT_Customer_UC_VDT_DrasticChangeInETA));
    Cls_Dealer objCls_Dealer = new Cls_Dealer();
    Cls_VDTAdminReport objCls_VDTAdminReport = new Cls_VDTAdminReport();
    DataTable dt = new DataTable();

    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                ViewState["_DrasticETAChangeDT"] = null;
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "diff";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;

                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
                bindDropDown();
                if (Request.QueryString["showadjustment"] == null)
                {
                    grdDrasticETAChange.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue);
                    if (Request.QueryString["type"] != null)
                    {
                        drpDealer.SelectedValue = "0";
                        btnGenerateReport_Click(null, null);
                    }
                }
                else
                {
                    grdDrasticETAChange.PageSize = 5;
                    drpDealer.SelectedValue = "0";
                    btnGenerateReport_Click(null, null);
                }
                //bindData();
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
            ViewState["_DrasticETAChangeDT"] = null;
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
            grdDrasticETAChange.PageIndex = 0;
            if (ddl_NoRecords2.SelectedValue.ToString() == "All")
            {
                //For view 1
                grdDrasticETAChange.PageSize = grdDrasticETAChange.PageCount * grdDrasticETAChange.Rows.Count;
                grdDrasticETAChange.DataSource = (DataTable)ViewState["_DrasticETAChangeDT"];
                grdDrasticETAChange.DataBind();
            }
            else
            {
                //for view 1
                grdDrasticETAChange.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
                grdDrasticETAChange.DataSource = (DataTable)ViewState["_DrasticETAChangeDT"];
                grdDrasticETAChange.DataBind();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void grdDrasticETAChange_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDrasticETAChange.PageIndex = e.NewPageIndex;
            dt = new DataTable();
            dt = (DataTable)ViewState["_DrasticETAChangeDT"];
            grdDrasticETAChange.DataSource = dt;
            grdDrasticETAChange.DataBind();
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }

    protected void grdDrasticETAChange_Sorting(object sender, GridViewSortEventArgs e)
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

    public void bindData()
    {
        try
        {
            if (Convert.ToString(drpDealer.SelectedValue).ToLower() == "all")
            {
                objCls_VDTAdminReport.dealerid = 0;
            }
            else
            {
                objCls_VDTAdminReport.dealerid = Convert.ToInt32(drpDealer.SelectedValue);
            }
            //string ComapanyName = null;

            //if (Convert.ToString(ddlCompany.SelectedValue) == "ALL")
            //{
            //}
            //else
            //{
            //    ComapanyName = ddlCompany.SelectedItem.Text.Trim();
            //}
            if (ViewState["_DrasticETAChangeDT"] == null)
            {
                dt = objCls_VDTAdminReport.get_DrasticETAChange();
            }
            else
            {
                dt = (DataTable)ViewState["_DrasticETAChangeDT"];
            }
            DataView dv = new DataView(dt);
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
            grdDrasticETAChange.DataSource = dt;
            grdDrasticETAChange.DataBind();
            ViewState["_DrasticETAChangeDT"] = dt;
            grdDrasticETAChange.Visible = true;
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

            if (Convert.ToString(Request.QueryString["showadjustment"]) != null)
            {
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
                grdDrasticETAChange.PageSize = 5;

                if (dt.Rows.Count > 0)
                {
                    Label lblDrasticETAChangeMarqueeMessage = (Label)this.Parent.FindControl("lblDrasticETAChangeMarqueeMessage");
                    lblDrasticETAChangeMarqueeMessage.Text = Convert.ToString(dt.Rows[0]["name"]) + " has drasctic change in ETA( No of days - " + Convert.ToString(dt.Rows[0]["diff"]) + " )";

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
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "diff";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
            lblRowsToDisplay2.Visible = false;
            ddl_NoRecords2.Visible = false;
            ViewState["_DrasticETAChangeDT"] = null;
            grdDrasticETAChange.Visible = false;
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }
    }

    public void bindDropDown()
    {
        try
        {
            Cls_UserMaster objUser = new Cls_UserMaster();
            dt = objUser.GetAllActiveDealersWithCompany();


            // dt = objCls_Dealer.GetAllDealers();
            drpDealer.DataSource = dt;
            drpDealer.DataTextField = "Dealer";
            drpDealer.DataValueField = "id";
            drpDealer.DataBind();
            drpDealer.Items.Insert(0, new ListItem("ALL", "ALL"));
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
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
            Logger.Error(Convert.ToString(ex.Message));
        }
    }

    /// <summary>
    /// Created By : Archana : 24 March 2012
    /// Binding Companies to dropdown
    /// </summary>
    //public void BindCompanies()
    //{
    //    try
    //    {
    //        Cls_Dealer objDealer = new Cls_Dealer();
    //        DataTable dtCompanies = objDealer.GetAllCompanies_VDT();
    //        if (dtCompanies != null && dtCompanies.Rows.Count > 0)
    //        {
    //            ddlCompany.DataTextField = "company";
    //            ddlCompany.DataValueField = "company";
    //            ddlCompany.DataSource = dtCompanies;
    //            ddlCompany.DataBind();
    //            ddlCompany.Items.Insert(0, new ListItem("ALL"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.Error(Convert.ToString("bindDropDown :" + ex.Message));
    //    }

    //}

    #endregion
}
