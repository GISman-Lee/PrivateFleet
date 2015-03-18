using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;
using System.Collections.Specialized;

public partial class VDT_Customer_User_Controls_UC_VDTDealerOrederCountl : System.Web.UI.UserControl
{
    #region Private Variables
    ILog Logger = LogManager.GetLogger(typeof(VDT_Customer_User_Controls_UC_VDTDealerOrederCountl));
    Cls_Alies objAlies = new Cls_Alies();
    Cls_VDTAdminReport objCls_VDTAdminReport = new Cls_VDTAdminReport();
    DataTable dt = new DataTable();
    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ClientScriptManager cs = Page.ClientScript;
            //cs.RegisterStartupScript(GetType(), "_test", "javascript:autoWidth();", true);

            if (!Page.IsPostBack)
            {

                this.Parent.Parent.Page.Form.DefaultButton = btnGenerateReport.UniqueID;
                ViewState["_DealerOrderCountDT"] = null;

                if (Session["SortExpr"] != null && Session["SortDirection"] != null)
                {
                    ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = Convert.ToString(Session["SortExpr"]);
                    ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Convert.ToString(Session["SortDirection"]);
                }
                else
                {
                    ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "name";
                    ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                }
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
                bindDropDown();
                BindCompanies();
                if (Request.QueryString["showadjustment"] == null)
                {
                    grdDealerOrder.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue);
                    if (Request.QueryString["type"] != null)
                    {
                        drpMake.SelectedValue = Convert.ToString(Request.QueryString["make"]);
                        btnGenerateReport_Click(null, null);
                    }
                }
                else
                {
                    grdDealerOrder.PageSize = 5;
                    btnGenerateReport_Click(null, null);
                }

                string ComapanyName = null;
                int makeid = 0;
                if (Request.QueryString["cmpny"] != null && Request.QueryString["cmpny"].Length > 0 && Request.QueryString["make"] != null && Request.QueryString["make"].Length > 0)
                {
                    if (Convert.ToString(Request.QueryString["cmpny"]).Equals("ALL"))
                    {
                    }
                    else
                    {
                        ComapanyName = Convert.ToString(Request.QueryString["cmpny"]);
                        ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByText(ComapanyName.Trim()));
                    }

                    if (Convert.ToString(Request.QueryString["make"]) == "ALL")
                    {
                    }
                    else
                    {
                        makeid = Convert.ToInt32(Request.QueryString["make"]);
                        drpMake.SelectedIndex = drpMake.Items.IndexOf(drpMake.Items.FindByValue(Convert.ToString(makeid)));
                    }
                    bindData(makeid, ComapanyName);

                    //Response.Redirect(Request.Path, false);
                    //string url = Request.Path;
                    //NameValueCollection nmCollection = new NameValueCollection();
                    //nmCollection.Remove("cmpny");
                    //nmCollection.Remove("make");
                    //Response.Redirect(Request.Path,false);
                }
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
            grdDealerOrder.PageIndex = 0;
            if (ddl_NoRecords2.SelectedValue.ToString() == "All")
            {
                //For view 1
                grdDealerOrder.PageSize = grdDealerOrder.PageCount * grdDealerOrder.Rows.Count;
                grdDealerOrder.DataSource = (DataTable)ViewState["_DealerOrderCountDT"];
                grdDealerOrder.DataBind();
            }
            else
            {
                //for view 1
                grdDealerOrder.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
                grdDealerOrder.DataSource = (DataTable)ViewState["_DealerOrderCountDT"];
                grdDealerOrder.DataBind();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("ddl_NoRecords2_SelectedIndexChanged :" + ex.Message));
        }
    }

    public void grdDealerOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDealerOrder.PageIndex = e.NewPageIndex;
            dt = new DataTable();
            dt = (DataTable)ViewState["_DealerOrderCountDT"];
            grdDealerOrder.DataSource = dt;
            grdDealerOrder.DataBind();
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("grdDealerOrder_PageIndexChanging :" + ex.Message));
        }
    }

    protected void grdDealerOrder_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;

            //Swap sort direction
            this.DefineSortDirection();

            // BindData(objCourseMaster);
            int makeid = 0;
            string ComapanyName = null;

            if (Convert.ToString(ddlCompany.SelectedItem.Text).Equals("ALL"))
            {
            }
            else
            {
                ComapanyName = Convert.ToString(ddlCompany.SelectedItem.Text.Trim());
            }
            if (Convert.ToString(drpMake.SelectedItem.Text.Trim()).Equals("ALL"))
            {
            }
            else
            {
                makeid = Convert.ToInt32(drpMake.SelectedItem.Value.Trim());
            }
            bindData(makeid, ComapanyName);
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("grdDealerOrder_Sorting :" + ex.Message));
        }
    }

    public void btnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["_DealerOrderCountDT"] = null;
            int makeid = 0;
            string ComapanyName = null;

            if (Convert.ToString(ddlCompany.SelectedItem.Text).Equals("ALL"))
            {
            }
            else
            {
                ComapanyName = Convert.ToString(ddlCompany.SelectedItem.Text.Trim());
            }
            if (Convert.ToString(drpMake.SelectedItem.Text.Trim()).Equals("ALL"))
            {
            }
            else
            {
                makeid = Convert.ToInt32(drpMake.SelectedItem.Value.Trim());
            }
            bindData(makeid, ComapanyName);
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("btnGenerateReport_Click :" + ex.Message));
        }
    }

    protected void grdDealerOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hypInCompleteOrders = new HyperLink();
                hypInCompleteOrders = (HyperLink)e.Row.FindControl("hypInCompleteOrders");
                string Cmpny = ddlCompany.SelectedItem.Text.Trim();
                string _make = drpMake.SelectedItem.Value.Trim();

                string _id = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "id"));
                string _eml = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "email"));

                hypInCompleteOrders.NavigateUrl = "../../ClinetIfo_ForDealer.aspx?dlrid=" + _id + "&eml=" + _eml + "&make=" + _make + "&cmpny=" + Cmpny;
                //NavigateUrl='<%# "../../ClinetIfo_ForDealer.aspx?dlrid=" + Eval("id").ToString() +"&eml="+ Eval("email").ToString()  %>'
            }
        }
        catch (Exception ex)
        {
            Logger.Error("grdDealerOrder_RowDataBound" + ex.Message);
        }
    }


    //NavigateUrl='<%# "../../ClinetIfo_ForDealer.aspx?dlrid=" + Eval("id").ToString() +"&eml="+ Eval("email").ToString()  %>'

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
                Session["SortExpr"] = ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION];
                Session["SortDirection"] = ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION];
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("DefineSortDirection :" + ex.Message));
        }
    }

    public void bindData(int makeID, string companyName)
    {
        try
        {

            objCls_VDTAdminReport.makeid = makeID;
            if (ViewState["_DealerOrderCountDT"] == null)
            {
                dt = objCls_VDTAdminReport.get_VDTDealerCustomerCount(companyName);
            }
            else
            {
                dt = (DataTable)ViewState["_DealerOrderCountDT"];
            }

            DataView dv = new DataView(dt);
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
            grdDealerOrder.DataSource = dt;
            grdDealerOrder.DataBind();
            ViewState["_DealerOrderCountDT"] = dt;
            grdDealerOrder.Visible = true;
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
            grdDealerOrder.Columns[1].Visible = true;
            if (Request.QueryString["showadjustment"] != null)
            {
                grdDealerOrder.Columns[1].Visible = false;
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
                HyperLink hypDealercount = (HyperLink)this.Parent.FindControl("hypDealercount");
                hypDealercount.NavigateUrl = "~/Admin_DealerSummaryReport.aspx?type=5&make=" + Convert.ToString(drpMake.SelectedValue); ;

            }
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
            drpMake.Items.Clear();
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
            Logger.Error(Convert.ToString("bindDropDown :" + ex.Message));
        }
    }

    /// <summary>
    /// Created By : Archana : 23 March 2012
    /// Binding Companies to dropdown
    /// </summary>
    public void BindCompanies()
    {
        try
        {
            ddlCompany.Items.Clear();
            Cls_Dealer objDealer = new Cls_Dealer();
            DataTable dtCompanies = objDealer.GetAllCompanies_VDT();
            if (dtCompanies != null && dtCompanies.Rows.Count > 0)
            {
                ddlCompany.DataTextField = "company";
                ddlCompany.DataValueField = "company";
                ddlCompany.DataSource = dtCompanies;
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("ALL"));
                ddlCompany.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("bindDropDown :" + ex.Message));
        }

    }

    public void ResetSearchCriteria()
    {
        try
        {
            ViewState["_DealerOrderCountDT"] = null;
            if (drpMake.Items.Count > 0)
            {
                drpMake.SelectedValue = "ALL";
            }

            ViewState["_DealerOrderCountDT"] = null;
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "name";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            lblRowsToDisplay2.Visible = false;
            ddl_NoRecords2.Visible = false;
            grdDealerOrder.Visible = false;
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("ResetSearchCriteria :" + ex.Message));
        }
    }

    #endregion
}
