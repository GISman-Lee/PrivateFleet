using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;


public partial class VDT_Customer_User_Controls_UC_VDTCustomerReport_PC : System.Web.UI.UserControl
{

    #region Private Variables

    ILog Logger = LogManager.GetLogger(typeof(VDT_Customer_User_Controls_UC_VDTCustomerReport_PC));
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
                ImageButton imgBtnCancelOrder = (ImageButton)ucClient.FindControl("imgBtnCancelOrder");

                if (!String.IsNullOrEmpty(Convert.ToString(Session[Cls_Constants.USER_NAME])))
                {
                    if (Convert.ToString(Session[Cls_Constants.ROLE_ID]) == "1")
                    {
                        imgBtn.Visible = false;
                        imgBtnCancelOrder.Visible = false;
                    }
                }
                BindPrimaryContact();

                if (Request.QueryString["fromPage"].ToLower() == "updateclient")
                {
                    txtFullName.Text = Convert.ToString(Request.QueryString["custName"]);
                    drpDealer.SelectedValue = Convert.ToString(Request.QueryString["DealerID"]);
                    txtEmail.Text = Convert.ToString(Request.QueryString["custEmail"]);
                    txtphone.Text = Convert.ToString(Request.QueryString["phone"]);
                    drpMake.SelectedValue = Convert.ToString(Request.QueryString["make"]);
                    drpOrderStatus.SelectedValue = Convert.ToString(Request.QueryString["orderstatus"]);
                    ddlPrimaryContact.SelectedValue = Convert.ToString(Request.QueryString["pcontact"]);
                    btnGenerateReport_Click(sender, e);
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
            if (e.CommandName.ToLower() == "show")
            {
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
            else if (e.CommandName.ToLower() == "unmark")
            {
                objCls_VDTAdminReport.CustomerId = Convert.ToInt64(hyp.Value);
                int result = objCls_VDTAdminReport.UpdateAsUnmark();
                btnGenerateReport_Click(null, null);
            }
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
                LinkButton lnkbtnView = (LinkButton)e.Row.FindControl("lnkbtnView");
                LinkButton lnkUnmark = (LinkButton)e.Row.FindControl("lnkUnmark");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                HiddenField hdnIsActiveCustomer = (HiddenField)e.Row.FindControl("hdnIsActiveCustomer");
                HiddenField hdfUnmark = (HiddenField)e.Row.FindControl("hdfUnmark");
                HyperLink hypcustomerlinkview = (HyperLink)e.Row.FindControl("hypcustomerlinkview");

                string _id = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "DealerID"));
                string _eml = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "dealeremail"));
                string searchData = "&custName=" + txtFullName.Text.Trim() + "&DealerID=" + drpDealer.SelectedValue + "&custEmail=" + txtEmail.Text.Trim() + "&phone=" + txtphone.Text.Trim() + "&make=" + drpMake.SelectedValue + "&orderstatus=" + drpOrderStatus.SelectedValue + "&pcontact=" + ddlPrimaryContact.SelectedValue;

                if (lblStatus.Text.Trim().ToLower() == "inprocess")
                {
                    hypcustomerlinkview.NavigateUrl = "~/ClinetIfo_ForDealer.aspx?tempid=" + hypcustomerlinkview.NavigateUrl + "&dlrid=" + _id + "&eml=" + _eml + "&ReqFrm=custOS" + searchData;
                    //hypcustomerlinkview.NavigateUrl = "~/ClinetIfo_ForDealer.aspx?tempid=" + hypcustomerlinkview.NavigateUrl;
                }
                else
                {
                    if (lblStatus.Text.Trim().ToLower() == "complete" && Convert.ToInt32(hdfUnmark.Value) == 0)
                    {
                        lnkUnmark.Enabled = true;
                        lnkUnmark.CssClass = "activeLink";
                    }
                    lnkbtnView.Visible = true;
                    hypcustomerlinkview.Visible = false;

                    if (Convert.ToInt32(hdfUnmark.Value) >= 1)
                    {
                        lnkUnmark.Text = "Already Unmarked";
                        if (Convert.ToInt32(hdfUnmark.Value) == 1)
                        {
                            lnkbtnView.Visible = false;
                            hypcustomerlinkview.Visible = true;
                            hypcustomerlinkview.NavigateUrl = "~/ClinetIfo_ForDealer.aspx?tempid=" + hypcustomerlinkview.NavigateUrl + "&dlrid=" + _id + "&eml=" + _eml + "&ReqFrm=custOS" + searchData;
                        }
                    }

                }
                //if (!string.IsNullOrEmpty(hdnIsActiveCustomer.Value))
                //{
                //    if (!Convert.ToBoolean(hdnIsActiveCustomer.Value))
                //    {
                //        lnkbtnView.Enabled = false;
                //        lnkbtnView.ForeColor = System.Drawing.Color.FromName("Gray");
                //    }
                //    else
                //    {
                //        lnkbtnView.Enabled = true;
                //        lnkbtnView.CssClass = "activeLink";
                //    }
                //}
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
            if (ddlPrimaryContact.SelectedIndex > 0)
            {
                objCls_VDTAdminReport.PrimaryContact = ddlPrimaryContact.SelectedItem.Text.Trim();
            }
            if (ViewState["_CustomerDT"] == null)
            {
                dt = objCls_VDTAdminReport.get_VDTCustomerReport_PC();
                if (orderStatus != "")
                {
                    orderStatus = Convert.ToString(drpOrderStatus.SelectedValue);
                    dv = new DataView(dt);
                    dv.RowFilter = "Status='" + orderStatus + "'";
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
            if (ddlPrimaryContact.Items.Count > 0)
            {
                ddlPrimaryContact.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("ResetSearchCriteria :" + ex.Message));
        }
    }

    //Added by Archana : 
    //Date : 24 March 2012
    public void BindPrimaryContact()
    {
        Cls_PrimaryContact objPrimaryContact = new Cls_PrimaryContact();
        DataTable dtPrimaryContact = new DataTable();
        try
        {
            //dtPrimaryContact = objGeneral.GetDistinctPrimaryContacts();
            dtPrimaryContact = objPrimaryContact.GetAllPrimaryContacts();
            DataView dv = dtPrimaryContact.DefaultView;
            dv.RowFilter = "primaryContactFor<>'Survey' AND IsActive=1";
            dtPrimaryContact = dv.ToTable();
            if (dtPrimaryContact != null && dtPrimaryContact.Rows.Count > 0)
            {
                ddlPrimaryContact.DataTextField = "Name";
                ddlPrimaryContact.DataValueField = "Id";
                ddlPrimaryContact.DataSource = dtPrimaryContact;
                ddlPrimaryContact.DataBind();
            }
            ddlPrimaryContact.Items.Insert(0, new ListItem("--Select--", "-1"));
        }
        catch (Exception ex)
        {
            Logger.Error(Convert.ToString("BindPrimaryContact :" + ex.Message));
        }
    }

    #endregion
}
