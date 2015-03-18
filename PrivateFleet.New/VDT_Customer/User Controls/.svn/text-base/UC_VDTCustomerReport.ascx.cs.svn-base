using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;

public partial class VDT_Customer_UC_VDTCustomerReport : System.Web.UI.UserControl
{

    #region Private Variables

    ILog Logger = LogManager.GetLogger(typeof(VDT_Customer_UC_VDTCustomerReport));
    Cls_VDTAdminReport objCls_VDTAdminReport = new Cls_VDTAdminReport();
    Cls_Dealer objCls_Dealer = new Cls_Dealer();
    Cls_Alies objAlies = new Cls_Alies();
    DataTable dt = new DataTable();
    DataView dv = new DataView();

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                this.Parent.Parent.Page.Form.DefaultButton = btnGenerateReport.UniqueID;
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "name";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;

                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
                bindDropDown();
                grdCustomer.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue);
                ImageButton imgBtn = (ImageButton)ucClient.FindControl("imgbtnSameETA");

                if (!String.IsNullOrEmpty(Convert.ToString(Session[Cls_Constants.USER_NAME])))
                {
                    if (Convert.ToString(Session[Cls_Constants.ROLE_ID]) == "1")
                    {
                        imgBtn.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("Page_Load:" + ex.Message));
        }

    }

    public void btnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["_CustomerDT"] = null;
            bindData();
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("btnGenerateReport_Click:" + ex.Message));
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
            Logger.Error(Convert.ToString("btnCancel_Click:" + ex.Message));
        }
    }

    public void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grdCustomer.PageIndex = 0;
            if (ddl_NoRecords2.SelectedValue.ToString() == "All")
            {
                //For view 1
                grdCustomer.PageSize = grdCustomer.PageCount * grdCustomer.Rows.Count;
                grdCustomer.DataSource = (DataTable)ViewState["_CustomerDT"];
                grdCustomer.DataBind();
            }
            else
            {
                //for view 1
                grdCustomer.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
                grdCustomer.DataSource = (DataTable)ViewState["_CustomerDT"];
                grdCustomer.DataBind();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("ddl_NoRecords2_SelectedIndexChanged :" + ex.Message));
        }
    }

    public void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdCustomer.PageIndex = e.NewPageIndex;
            dt = new DataTable();
            dt = (DataTable)ViewState["_CustomerDT"];
            grdCustomer.DataSource = dt;
            grdCustomer.DataBind();
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("grdCustomer_PageIndexChanging :" + ex.Message));
        }
    }

    protected void grdCustomer_Sorting(object sender, GridViewSortEventArgs e)
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
            Logger.Error(Convert.ToString("grdCustomer_Sorting :" + ex.Message));
        }

    }

    public void grdCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            LinkButton imgbtn = (LinkButton)e.CommandSource;
            string strcommand = Convert.ToString(e.CommandName);
            string dealerusername = "";
            dealerusername = Convert.ToString(e.CommandArgument);

            GridViewRow grdrow = (GridViewRow)imgbtn.NamingContainer;
            HiddenField hyp = (HiddenField)grdrow.FindControl("hiddencustomerid");
            pnlClientView.Visible = true;
            modal.Enabled = true;
            ucClient.username = dealerusername;
            ucClient.customerid = hyp.Value;
            ucClient.binddata1();
            GridView grd = (GridView)ucClient.FindControl("grdClientList");

            grd.FooterRow.Visible = false;
            grd.ShowFooter = false;
            modal.Show();
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString(ex.Message));
        }

    }

    public void imgbtnModalCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlClientView.Visible = false;
            modal.Enabled = false;
            modal.Hide();
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("imgbtnModalCancel_Click :" + ex.Message));
        }

    }

    public void grdCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("grdCustomer_RowDataBound :" + ex.Message));
        }
    }

    #endregion

    #region Methods

    public void bindData()
    {
        try
        {
            string fullname = "";
            string email = "";
            string phone = "";
            int makeid = 0;
            int Dealerid = 0;
            string orderStatus = "";
            fullname = Convert.ToString(txtFullName.Text.Trim());
            email = Convert.ToString(txtEmail.Text.Trim());
            phone = Convert.ToString(txtphone.Text.Trim());
            if (Convert.ToString(drpMake.SelectedValue) == "ALL")
            {
                makeid = 0;
            }
            else
            {
                makeid = Convert.ToInt32(drpMake.SelectedValue);
            }
            if (Convert.ToString(drpDealer.SelectedValue) == "ALL")
            {
                Dealerid = 0;

            }
            else
            {
                Dealerid = Convert.ToInt32(drpDealer.SelectedValue);
            }

            if (Convert.ToString(drpOrderStatus.SelectedValue) != "All")
            {
                orderStatus = Convert.ToString(drpOrderStatus.SelectedValue);
            }
            objCls_VDTAdminReport.fullname = fullname;
            objCls_VDTAdminReport.name = Dealerid;
            objCls_VDTAdminReport.phone = phone;
            objCls_VDTAdminReport.makeid = makeid;
            objCls_VDTAdminReport.dealerid = Dealerid;
            objCls_VDTAdminReport.email = email;
            if (ViewState["_CustomerDT"] == null)
            {
                dt = objCls_VDTAdminReport.get_VDTCustomerReport();
                if (orderStatus != "")
                {
                    orderStatus = Convert.ToString(drpOrderStatus.SelectedValue);
                    dv = new DataView(dt);
                    dv.RowFilter = "orderStatus='" + orderStatus + "'";
                    dt = dv.ToTable();
                }
            }
            else
            {
                dt = (DataTable)ViewState["_CustomerDT"];
            }

            dv = new DataView(dt);

            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            ViewState["_CustomerDT"] = dt;
            grdCustomer.DataSource = dt;
            grdCustomer.DataBind();
            grdCustomer.Visible = true;
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
                grdCustomer.PageSize = 5;

                if (dt.Rows.Count > 0)
                {
                    Label lblCustomerHelpMarquee = (Label)this.Parent.FindControl("lblCustomerHelpMarquee");
                    lblCustomerHelpMarquee.Text = Convert.ToString(dt.Rows[0]["fullname"]) + " Has requested Admin Help.";

                }

            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("bindData:" + ex.Message));
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

    public void bindDropDown()
    {
        try
        {

            dt = objAlies.GetMake();
            drpMake.DataSource = dt;
            drpMake.DataTextField = "Make";
            drpMake.DataValueField = "ID";
            drpMake.DataBind();
            drpMake.Items.Insert(0, new ListItem("-Select-", "0"));
            drpMake.Items.Insert(1, new ListItem("ALL", "ALL"));
            drpMake.SelectedValue = "ALL";

            Cls_UserMaster objUser = new Cls_UserMaster();
            dt = objUser.GetAllActiveDealers();

            drpDealer.DataSource = dt;
            drpDealer.DataTextField = "Dealer";
            drpDealer.DataValueField = "id";
            drpDealer.DataBind();
            drpDealer.Items.Insert(0, new ListItem("ALL", "ALL"));
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
            ViewState["_CustomerDT"] = null;
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "name";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;

            lblRowsToDisplay2.Visible = false;
            ddl_NoRecords2.Visible = false;
            grdCustomer.Visible = false;
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtphone.Text = "";
            if (drpDealer.Items.Count > 0)
            {
                drpDealer.SelectedValue = "ALL";
            }
            if (drpMake.Items.Count > 0)
            {

                drpMake.SelectedValue = "ALL";
            }
            drpOrderStatus.SelectedValue = "All";
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("ResetSearchCriteria :" + ex.Message));
        }
    }

    #endregion

}
