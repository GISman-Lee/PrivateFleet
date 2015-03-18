using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;
using System.Text;
using System.Configuration;

public partial class TradeInAlerts : System.Web.UI.Page
{
    #region Variables
    static ILog logger = LogManager.GetLogger(typeof(TradeInAlerts));
    DataTable dt = null;
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)Master.FindControl("lblHeader")).Text = "Trade In Alerts";

        if (!IsPostBack)
        {
            FillMake();

            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "CustName";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;

            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            gvTradeInAlerts.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            ddl_NoRecords.Visible = false;
            lblRowsToDisplay.Visible = false;

            BindData();

        }
    }
    #endregion

    #region Events

    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        ACE_Model.ContextKey = ddlMake.SelectedValue;
    }

    protected void btnSaveasAlert_Click(object sender, ImageClickEventArgs e)
    {

        Cls_TradeInAlert objTAlert = new Cls_TradeInAlert();
        Page page = HttpContext.Current.Handler as Page;
        try
        {
            objTAlert.CName = txtCName.Text.Trim();
            objTAlert.Contact = txtContact.Text.Trim();
            objTAlert.MakeID = Convert.ToInt32(ddlMake.SelectedValue);
            objTAlert.Model = txtModel.Text.Trim();
            objTAlert.AlertPeriod = Convert.ToInt32(ddlAType.SelectedValue);
            objTAlert.Notes = txtNotes.Text.Trim();
            objTAlert.CreatedBy = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);

            int result = objTAlert.saveAlerts();

            if (result > 0)
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Alert added Successfully.');", true);
            else
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Error while adding alert. Try again...');", true);
            InitializeComponent();
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("Save Alert Err - " + ex.Message);
        }
        finally
        {
            objTAlert = null;
            page = null;
        }
    }

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvTradeInAlerts.PageIndex = 0;

        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {
            //for view 3 (vwAllSLQuotation)
            gvTradeInAlerts.PageSize = gvTradeInAlerts.PageCount * gvTradeInAlerts.Rows.Count;
            gvTradeInAlerts.DataSource = (DataTable)ViewState["Alerts"];
            gvTradeInAlerts.DataBind();

        }
        else
        {
            //for view 3 (vwAllSLQuotation)
            gvTradeInAlerts.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvTradeInAlerts.DataSource = (DataTable)ViewState["Alerts"];
            gvTradeInAlerts.DataBind();
        }
    }

    protected void gvTradeInAlerts_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        BindData();
    }

    protected void gvTradeInAlerts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTradeInAlerts.PageIndex = e.NewPageIndex;
        gvTradeInAlerts.DataSource = (DataTable)ViewState["Alerts"];
        gvTradeInAlerts.DataBind();
    }

    protected void gvTradeInAlerts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Cls_TradeInAlert objTradeInAlerts = new Cls_TradeInAlert();
        Page page = HttpContext.Current.Handler as Page;
        try
        {
            if (e.CommandName == "Activate")
            {
                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;
                int intRequestId = Convert.ToInt32(gvTradeInAlerts.DataKeys[gvRow.RowIndex].Values["ID"]);

                bool Active = Convert.ToBoolean(((HiddenField)gvRow.FindControl("hdfActive")).Value);

                objTradeInAlerts.AlertID = intRequestId;

                if (Active)
                    objTradeInAlerts.IsActive = 0;
                else
                    objTradeInAlerts.IsActive = 1;

                int result = objTradeInAlerts.activateAlert();

                if (result > 0)
                {
                    if (Active)
                        ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Alert Deactivate Successfully.');", true);
                    else
                        ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Alert Activate Successfully.');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Alert added Successfully.');", true);

                ViewState["Alerts"] = null;
                BindData();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Row cmd err- " + ex.Message);
        }
        finally
        {
            objTradeInAlerts = null;
            page = null;
        }

    }

    protected void gvTradeInAlerts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image img = (Image)e.Row.FindControl("imgActivate");
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkActive");
            bool Active = Convert.ToBoolean(((HiddenField)e.Row.FindControl("hdfActive")).Value);

            if (Active)
            {
                img.ImageUrl = "~/Images/Active.png";
                lnk.Text = "Deactivate";
                e.Row.CssClass = "gridactiverow";
            }
            else
            {
                img.ImageUrl = "~/Images/Inactive.ico";
                lnk.Text = "Activate";
                e.Row.CssClass = "griddeactiverow";
            }

        }

    }

    protected void btnRun_Click(object sender, ImageClickEventArgs e)
    {

        pnlAlert_1.Visible = true;
        pnlAlert.Visible = false;

        Cls_TradeInAlert objTradeInAlert = new Cls_TradeInAlert();
        //DataTable dtAlertID, dt = null;
        DataSet ds;
        //StringBuilder str = new StringBuilder();
        //Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();

        try
        {
            objTradeInAlert.CreatedBy = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            objTradeInAlert.RunStatus = 0;
            ds = objTradeInAlert.RunSavedSearch();
            ucTradeInData1.DisplayTradeInData(ds.Tables[0],"Alert");

            #region Commented

            //if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            //{
            //    dtAlertID = ds.Tables[1];

            //    for (int i = 0; i < dtAlertID.Rows.Count; i++)
            //    {
            //        dt = ds.Tables[0];
            //        DataView dv = dt.DefaultView;
            //        dv.RowFilter = "AlertID=" + dtAlertID.Rows[i]["AlertID"];
            //        dt = dv.ToTable();

            //        str = new StringBuilder();

            //        str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear ");
            //        string CName = Convert.ToString(dt.Rows[0]["Name"]);
            //        if (CName.IndexOf(" ") > 0)
            //        {
            //            CName = CName.Substring(0, CName.IndexOf(" "));
            //        }

            //        str.Append(CName);

            //        str.Append("<br/><br/>Following makes are available for Trade In -");

            //        str.Append("<br/><br/> Customer Name - " + Convert.ToString(dt.Rows[0]["CustName"]));
            //        str.Append("<br/> Customer Contact - " + Convert.ToString(dt.Rows[0]["Contact"]));

            //        for (int j = 0; j < dt.Rows.Count; j++)
            //        {
            //            try
            //            {
            //                str.Append("<br/><br/> Make - " + Convert.ToString(dt.Rows[j]["Make"]));
            //                str.Append("<br/> Model - " + Convert.ToString(dt.Rows[j]["T1Model"]));
            //                str.Append("<br/> Series - " + Convert.ToString(dt.Rows[j]["T1Series"]));
            //                str.Append("<br/> Transmission - " + Convert.ToString(dt.Rows[j]["T1Transmission"]));
            //                str.Append("<br/> BodyShap - " + Convert.ToString(dt.Rows[j]["T1BodyShap"]));
            //                str.Append("<br/> BodyColor - " + Convert.ToString(dt.Rows[j]["T1BodyColor"]));
            //                str.Append("<br/> TrimColor - " + Convert.ToString(dt.Rows[j]["T1TrimColor"]));
            //                str.Append("<br/> FuelType - " + Convert.ToString(dt.Rows[j]["T1FuelType"]));
            //                str.Append("<br/> Odometer - " + Convert.ToString(dt.Rows[j]["T1Odometer"]));
            //            }
            //            catch (Exception ex)
            //            {
            //            }
            //        }

            //        str.Append("</p>");
            //        objEmailHelper.EmailBody = str.ToString();
            //        objEmailHelper.EmailSubject = "Alert";

            //        objEmailHelper.EmailToID = Convert.ToString(dt.Rows[0]["Email"]);
            //        objEmailHelper.EmailFromID = ConfigurationManager.AppSettings["EmailFromID"];

            //        objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";

            //        if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
            //        {
            //            try
            //            {
            //                objEmailHelper.SendEmail();

            //            }
            //            catch (Exception ex)
            //            {
            //            }
            //        }
            //    }
            //}
            //ViewState["Alerts"] = null;
            //BindData();
            #endregion
        }
        catch (Exception ex)
        {
            logger.Error("Run alert err- " + ex.Message);
        }
        finally
        {
            objTradeInAlert = null;
        }


    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        InitializeComponent();
        BindData();
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        pnlAlert_1.Visible = false;
        pnlAlert.Visible = true;
    }

    #endregion

    #region Methods
    /// <summary>
    /// To fill the existing makes
    /// </summary>
    private void FillMake()
    {
        try
        {
            if (Cache["MAKES"] == null)
                Mechsoft.GeneralUtilities.PreLoad.LoadMake();

            DataTable dt = Cache["MAKES"] as DataTable;

            if (dt != null)
            {
                ddlMake.DataSource = dt;
                ddlMake.DataTextField = "Make";
                ddlMake.DataValueField = "ID";
                ddlMake.DataBind();
            }
            ddlMake.Items.Insert(0, new ListItem("-Select Make-", "0"));
        }
        catch (Exception ex)
        {
            logger.Error("Fill make Event : " + ex.Message);
        }
    }

    private void InitializeComponent()
    {
        txtCName.Text = String.Empty;
        txtContact.Text = String.Empty;
        txtModel.Text = String.Empty;
        txtNotes.Text = String.Empty;

        ddlMake.SelectedValue = "0";
        ddlAType.SelectedValue = "90";
    }

    private void BindData()
    {
        Cls_TradeInAlert objTradeInAlert = new Cls_TradeInAlert();
        dt = null;
        try
        {
            objTradeInAlert.CreatedBy = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            if (ViewState["Alerts"] != null)
                dt = (DataTable)ViewState["Alerts"];
            else
                dt = objTradeInAlert.getAlertData();

            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            if (dt != null && dt.Rows.Count > 0)
            {
                gvTradeInAlerts.DataSource = dt;
                gvTradeInAlerts.DataBind();

                ViewState["Alerts"] = dt;
            }
        }
        catch (Exception ex)
        {
            logger.Error("Alert bind data - " + ex.Message);
        }
        finally
        {
            objTradeInAlert = null;
        }
    }

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

    #endregion


}
