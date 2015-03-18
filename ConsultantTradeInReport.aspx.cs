using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;
using System.Web.UI.HtmlControls;

public partial class ConsultantTradeInReport : System.Web.UI.Page
{
    #region Variables
    static ILog logger = LogManager.GetLogger(typeof(ConsultantTradeInReport));
    Cls_CompletedQuoatationReportHelper objReport;
    DataTable dt = null;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            Label lblHeader = (Label)Master.FindControl("lblHeader");
            if (lblHeader != null)
                lblHeader.Text = "Trade In Report";

            CallPageLoad();

            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "DeliveryDateSort";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;

            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            gvTInReport.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            ddl_NoRecords.Visible = false;
            lblRowsToDisplay.Visible = false;

            ddl_NoRecords_1.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            gvSearchCriteria.PageSize = Convert.ToInt32(ddl_NoRecords_1.SelectedValue.ToString());

            ddl_NoRecords_1.Visible = false;
            lblRowsToDisplay_1.Visible = false;

            GenerateReport(); // on 29 june 2012

            if (Request.QueryString["tir"] != null && !Convert.ToString(Request.QueryString["tir"]).Equals(String.Empty))
                viewTradeInDetails();
        }

    }

    #region Methods

    private void CallPageLoad()
    {
        objReport = new Cls_CompletedQuoatationReportHelper();
        try
        {
            FillMake();
            FillStates();
            FillTransmmision();
        }
        catch (Exception ex)
        {
            logger.Error("Ddl Load Error - " + ex.Message);
        }
        finally
        {
            objReport = null;
        }


    }

    private void FillMake()
    {
        try
        {
            if (Cache["MAKES"] == null)
                Mechsoft.GeneralUtilities.PreLoad.LoadMake();

            //DataTable dt = new DataTable();
            //dt = objReport.GetMake();

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

    private void FillTransmmision()
    {
        try
        {
            ddlTransmission.Items.Clear();
            DataTable dt = new DataTable();
            dt = objReport.GetTransmission(0);
            ddlTransmission.DataSource = dt;
            ddlTransmission.DataTextField = "Name";
            ddlTransmission.DataValueField = "ID";
            ddlTransmission.DataBind();
            ddlTransmission.Items.Insert(0, new ListItem("-Select Transmision-", "0"));
        }
        catch (Exception ex)
        {
            logger.Error("ddlModel_SelectedIndexChanged Event : " + ex.Message);
        }
    }

    private void FillStates()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objReport.GetStates();
            ddlState.DataSource = dt;
            ddlState.DataTextField = "State";
            ddlState.DataValueField = "ID";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("-Select State-", "0"));
        }
        catch (Exception ex)
        {
            logger.Error("Error in Fill State" + ex.Message);
        }
    }

    private void DataBind1()
    {
        if (ViewState["gvTInReport"] == null)
            dt = objReport.GetReportForTradeIn();
        else
            dt = (DataTable)ViewState["gvTInReport"];


        DataView dv = dt.DefaultView;
        dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

        dt = dv.ToTable();

        if (dt.Rows.Count > 0)
        {
            ddl_NoRecords.Visible = true;
            lblRowsToDisplay.Visible = true;
            ddl_NoRecords_1.Visible = false;
            lblRowsToDisplay_1.Visible = false;
            ViewState["gvTInReport"] = dt;
        }
        gvTInReport.DataSource = dt;
        gvTInReport.DataBind();
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

    /// <summary>
    /// 31 jan 2012 : To redirect to view details page of Trade in report
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void viewTradeInDetails()
    {
        try
        {
            foreach (GridViewRow gr in gvTInReport.Rows)
            {
                LinkButton lnkDetails = (LinkButton)gr.FindControl("lnkDetails");
                int intRequestId = Convert.ToInt32(gvTInReport.DataKeys[gr.RowIndex].Values["ID"]);
                if (intRequestId == Convert.ToInt32(Request.QueryString["tir"]))
                {
                    GridViewCommandEventArgs e = new GridViewCommandEventArgs(gr, lnkDetails, new CommandEventArgs("ViewDetails", null));
                    gvTInReport_RowCommand(lnkDetails, e);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Consultant Tradein Report viewTradeInDetailsError Error - " + ex.Message);
        }
        finally
        {

        }
    }


    #endregion

    #region Events

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvTInReport.PageIndex = 0;

        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {
            //for view 3 (vwAllSLQuotation)
            gvTInReport.PageSize = gvTInReport.PageCount * gvTInReport.Rows.Count;
            gvTInReport.DataSource = (DataTable)ViewState["gvTInReport"];
            gvTInReport.DataBind();

        }
        else
        {
            //for view 3 (vwAllSLQuotation)
            gvTInReport.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvTInReport.DataSource = (DataTable)ViewState["gvTInReport"];
            gvTInReport.DataBind();
        }
    }

    protected void ddl_NoRecords_1_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvSearchCriteria.PageIndex = 0;

        if (ddl_NoRecords_1.SelectedValue.ToString() == "All")
        {
            //for view 3 (vwAllSLQuotation)
            gvSearchCriteria.PageSize = gvSearchCriteria.PageCount * gvSearchCriteria.Rows.Count;
            gvSearchCriteria.DataSource = (DataTable)ViewState["gvSearchCriteriaReport"];
            gvSearchCriteria.DataBind();

        }
        else
        {
            //for view 3 (vwAllSLQuotation)
            gvSearchCriteria.PageSize = Convert.ToInt32(ddl_NoRecords_1.SelectedValue.ToString());
            gvSearchCriteria.DataSource = (DataTable)ViewState["gvSearchCriteriaReport"];
            gvSearchCriteria.DataBind();
        }
    }

    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        ACE_Model.ContextKey = ddlMake.SelectedValue;
    }

    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GenerateReport();
        }
        catch (Exception)
        {
        }
    }

    public void GenerateReport()
    {
        // IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
        objReport = new Cls_CompletedQuoatationReportHelper();

        try
        {
            pnlTrade12.Visible = true;
            pnlSearch.Visible = false;

            lblMsg.Visible = false;

            objReport.MakeId = Convert.ToInt32(ddlMake.SelectedValue);
            objReport.ModelName = Convert.ToString(txtModel.Text);
            objReport.Transmision = ddlTransmission.SelectedValue;
            objReport.HomeStateID = Convert.ToInt32(ddlState.SelectedValue);
            objReport.T1FromYear = txtT1FromYear.Text.Trim();
            objReport.T1ToYear = txtT1ToYear.Text.Trim();
            objReport.Surname = txtSurname.Text.Trim();
            objReport.Consultant = txtConsultant.Text.Trim();

            if (txtValueFrom.Text.Equals(String.Empty))
                objReport.T1FromValue = Convert.ToDouble("0");
            else
                objReport.T1FromValue = Convert.ToDouble(txtValueFrom.Text.Trim());

            if (txtValueTo.Text.Equals(String.Empty))
                objReport.T1ToValue = Convert.ToDouble("0");
            else
                objReport.T1ToValue = Convert.ToDouble(txtValueTo.Text.Trim());

            objReport.RegoNo = txtRegoNo.Text.Trim();

            ViewState["gvTInReport"] = null;
            DataBind1();
        }
        catch (Exception ex)
        {
            logger.Error("Trade in Generate rpt err -" + ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        ddlState.SelectedValue = "0";
        ddlMake.SelectedValue = "0";
        ddlTransmission.SelectedValue = "0";
        txtModel.Text = String.Empty;
        txtT1FromYear.Text = String.Empty;
        txtT1ToYear.Text = String.Empty;
        txtValueFrom.Text = String.Empty;
        txtValueTo.Text = String.Empty;

        pnlSearch.Visible = false;
        pnlTrade12.Visible = false;
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        pnlTradeIn_1.Visible = false;
        pnlTradeIn.Visible = true;
        Session["TradeInDate"] = null;
        GenerateReport();
    }

    protected void btnViewSearch_Click(object sender, ImageClickEventArgs e)
    {
        viewSavedSearch();
    }

    private void viewSavedSearch()
    {
        Cls_TradeInAlert objTAlert = new Cls_TradeInAlert();
        dt = new DataTable();
        try
        {
            pnlSearch.Visible = true;
            pnlTrade12.Visible = false;

            dt = null;
            gvSearchCriteria.DataSource = dt;
            gvSearchCriteria.DataBind();

            objTAlert.CreatedBy = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            dt = objTAlert.GetReportForTradeInSearch();

            if (dt.Rows.Count > 0)
            {
                ddl_NoRecords_1.Visible = true;
                lblRowsToDisplay_1.Visible = true;
                ddl_NoRecords.Visible = false;
                lblRowsToDisplay.Visible = false;

                gvSearchCriteria.DataSource = dt;
                gvSearchCriteria.DataBind();

                ViewState["gvSearchCriteriaReport"] = dt;
            }
        }
        catch (Exception ex)
        {
            logger.Error("Trade in Generate rpt err -" + ex.Message);
        }
    }

    protected void btnSaveasAlert_Click(object sender, ImageClickEventArgs e)
    {
        Cls_TradeInAlert objTAlert = new Cls_TradeInAlert();
        Page page = HttpContext.Current.Handler as Page;
        int cnt = 0;
        try
        {
            objTAlert.MakeID = Convert.ToInt32(ddlMake.SelectedValue);
            if (objTAlert.MakeID == 0)
                cnt++;

            objTAlert.Model = txtModel.Text.Trim();
            if (objTAlert.Model.Equals(String.Empty))
                cnt++;

            objTAlert.TransmissionID = Convert.ToInt32(ddlTransmission.SelectedValue);
            if (objTAlert.TransmissionID == 0)
                cnt++;

            objTAlert.StateID = Convert.ToInt32(ddlState.SelectedIndex);
            if (objTAlert.StateID == 0)
                cnt++;

            if (txtT1FromYear.Text.Trim().Equals(String.Empty))
            {
                objTAlert.FromYear = 0;
                cnt++;
            }
            else
                objTAlert.FromYear = Convert.ToInt32(txtT1FromYear.Text.Trim());

            if (txtT1ToYear.Text.Trim().Equals(String.Empty))
            {
                objTAlert.ToYear = 0;
                cnt++;
            }
            else
                objTAlert.ToYear = Convert.ToInt32(txtT1ToYear.Text.Trim());

            if (txtValueFrom.Text.Trim().Equals(String.Empty))
            {
                objTAlert.MinValue = 0;
                cnt++;
            }
            else
                objTAlert.MinValue = Convert.ToInt32(txtValueFrom.Text.Trim());

            if (txtValueTo.Text.Trim().Equals(String.Empty))
            {
                objTAlert.MaxValue = 0;
                cnt++;
            }
            else
                objTAlert.MaxValue = Convert.ToInt32(txtValueTo.Text.Trim());


            objTAlert.RegoNo = txtRegoNo.Text.Trim();
            if (objTAlert.RegoNo.Equals(String.Empty))
                cnt++;

            objTAlert.Surname = txtSurname.Text.Trim();
            if (objTAlert.Surname.Equals(String.Empty))
                cnt++;



            objTAlert.CreatedBy = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);

            if (cnt == 10)
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Please select Make to save Search Criteria.');", true);
            else
                divpopID.Visible = true;
            // result = objTAlert.saveTradeInSearch();

        }
        catch (Exception ex)
        {
            logger.Error("Save search for trade in Err - " + ex.Message);
        }
        finally
        {
            //objTAlert = null;
            //page = null;
        }

    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        Cls_TradeInAlert objTAlert = new Cls_TradeInAlert();
        Page page = HttpContext.Current.Handler as Page;
        try
        {
            objTAlert.MakeID = Convert.ToInt32(ddlMake.SelectedValue);
            objTAlert.Model = txtModel.Text.Trim();
            objTAlert.TransmissionID = Convert.ToInt32(ddlTransmission.SelectedValue);
            objTAlert.StateID = Convert.ToInt32(ddlState.SelectedIndex);
            if (txtT1FromYear.Text.Trim().Equals(String.Empty))
                objTAlert.FromYear = 0;
            else
                objTAlert.FromYear = Convert.ToInt32(txtT1FromYear.Text.Trim());

            if (txtT1ToYear.Text.Trim().Equals(String.Empty))
                objTAlert.ToYear = 0;
            else
                objTAlert.ToYear = Convert.ToInt32(txtT1ToYear.Text.Trim());

            if (txtValueFrom.Text.Trim().Equals(String.Empty))
                objTAlert.MinValue = 0;
            else
                objTAlert.MinValue = Convert.ToInt32(txtValueFrom.Text.Trim());

            if (txtValueTo.Text.Trim().Equals(String.Empty))
                objTAlert.MaxValue = 0;
            else
                objTAlert.MaxValue = Convert.ToInt32(txtValueTo.Text.Trim());

            objTAlert.CreatedBy = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);

            objTAlert.CName = txtCName.Text.Trim();
            objTAlert.Contact = txtContact.Text.Trim();
            objTAlert.AlertPeriod = Convert.ToInt32(ddlAType.SelectedValue);
            objTAlert.Notes = txtNotes.Text.Trim();

            objTAlert.RegoNo = txtRegoNo.Text.Trim();
            objTAlert.Surname = txtSurname.Text.Trim();
            int result = objTAlert.saveTradeInSearch();

            if (result > 0)
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Search saved Successfully.');", true);
            else
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Error while saving. Try again...');", true);
        }
        catch (Exception ex)
        {
            logger.Error("Save search for trade in Err - " + ex.Message);
        }
        finally
        {
            divpopID.Visible = false;
            objTAlert = null;
            page = null;
            txtCName.Text = "";
            txtContact.Text = "";
            ddlAType.SelectedValue = "90";
            txtNotes.Text = "";
        }
    }

    protected void imgbtnCancle_1_Click(object sender, ImageClickEventArgs e)
    {
        divpopID.Visible = false;
        txtCName.Text = "";
        txtContact.Text = "";
        ddlAType.SelectedValue = "90";
        txtNotes.Text = "";
    }

    #region Grid Events

    protected void gvSearchCriteria_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSearchCriteria.PageIndex = e.NewPageIndex;
        gvSearchCriteria.DataSource = (DataTable)ViewState["gvSearchCriteriaReport"];
        gvSearchCriteria.DataBind();
    }

    protected void gvSearchCriteria_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            //Swap sort direction
            this.DefineSortDirection();
            // BindData(objCourseMaster);

            dt = null;

            DataView dv = ((DataTable)ViewState["gvSearchCriteriaReport"]).DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

            dt = dv.ToTable();

            gvSearchCriteria.DataSource = dt;
            gvSearchCriteria.DataBind();

            ViewState["gvSearchCriteriaReport"] = dt;
        }
        catch (Exception ex)
        {
            logger.Error("gv search criteria err -" + ex.Message);
        }
    }

    protected void gvSearchCriteria_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Cls_TradeInAlert objTradeInAlerts = new Cls_TradeInAlert();
        Page page = HttpContext.Current.Handler as Page;
        DataTable dtDerails = new DataTable();
        try
        {
            if (e.CommandName == "Run")
            {
                dtDerails = (DataTable)ViewState["gvSearchCriteriaReport"];

                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;
                int intRequestId = Convert.ToInt32(gvSearchCriteria.DataKeys[gvRow.RowIndex].Values["ID"]);

                ddlMake.SelectedValue = ((HiddenField)gvRow.FindControl("hdfMakeID")).Value;
                txtModel.Text = (Convert.ToString(gvRow.Cells[2].Text) == "--") ? String.Empty : Convert.ToString(gvRow.Cells[2].Text);
                ddlTransmission.SelectedValue = ((HiddenField)gvRow.FindControl("hdfTrans")).Value;
                ddlState.SelectedValue = ((HiddenField)gvRow.FindControl("hdfState")).Value;

                txtT1FromYear.Text = (Convert.ToString(gvRow.Cells[5].Text) == "--") ? String.Empty : Convert.ToString(gvRow.Cells[5].Text);
                txtT1ToYear.Text = (Convert.ToString(gvRow.Cells[6].Text) == "--") ? String.Empty : Convert.ToString(gvRow.Cells[6].Text);

                txtValueFrom.Text = (Convert.ToString(gvRow.Cells[7].Text) == "--") ? String.Empty : Convert.ToString(Convert.ToInt32(gvRow.Cells[7].Text));
                txtValueTo.Text = (Convert.ToString(gvRow.Cells[8].Text) == "--") ? String.Empty : Convert.ToString(Convert.ToInt32(gvRow.Cells[8].Text));

                //on 23 June 12
                txtRegoNo.Text = ((HiddenField)gvRow.FindControl("hdfRegoNo")).Value;

                btnGenerateReport_Click(null, null);
            }
            if (e.CommandName == "me_Delete")
            {
                LinkButton lnkDetails = (LinkButton)e.CommandSource;
                GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;
                int intRequestId = Convert.ToInt32(gvSearchCriteria.DataKeys[gvRow.RowIndex].Values["ID"]);

                objTradeInAlerts.AlertID = intRequestId;
                int result = objTradeInAlerts.activateAlert_new();

                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Alert Deactivate Successfully.');", true);
                    btnViewSearch_Click(null, null);
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error("gv search criteria row command err -" + ex.Message);
        }
    }

    protected void gvTInReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTInReport.PageIndex = e.NewPageIndex;
        gvTInReport.DataSource = (DataTable)ViewState["gvTInReport"];
        gvTInReport.DataBind();
    }

    protected void gvTInReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        // BindData(objCourseMaster);
        DataBind1();
    }

    protected void gvTInReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtDerails = new DataTable();
        Cls_CompletedQuoatationReportHelper objTradeIn = new Cls_CompletedQuoatationReportHelper();
        if (e.CommandName == "ViewDetails")
        {
            pnlTradeIn_1.Visible = true;
            pnlTradeIn.Visible = false;

            dtDerails = (DataTable)ViewState["gvTInReport"];

            LinkButton lnkDetails = (LinkButton)e.CommandSource;
            GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;
            string key = Convert.ToString(((HiddenField)gvRow.FindControl("hdfKey")).Value);
            int intRequestId = Convert.ToInt32(gvTInReport.DataKeys[gvRow.RowIndex].Values["ID"]);

            DataView dv = new DataView();
            dv = dtDerails.DefaultView;
            dv.RowFilter = "ID=" + intRequestId;

            dtDerails = dv.ToTable();

            ((HtmlTableRow)ucTradeInData1.FindControl("trRemoveTradeIn")).Visible = true;
            ((HiddenField)ucTradeInData1.FindControl("hdfTradeInID")).Value = Convert.ToString(intRequestId);

            // on 16 july 20212
            ((Label)ucTradeInData1.FindControl("lblState_t")).Text = "<span style='color:Black; font-size:13px; font-weight:normal;'>State:</span>&nbsp;" + Convert.ToString(dtDerails.Rows[0]["HomeState"]);
            ((Label)ucTradeInData1.FindControl("lblCar_t")).Text = "<span style='color:Black; font-size:13px; font-weight:normal;'>Car:</span>&nbsp;" + Convert.ToString(dtDerails.Rows[0]["Make"]);
            if (!Convert.ToString(dtDerails.Rows[0]["T1Model"]).Equals(String.Empty) && !Convert.ToString(dtDerails.Rows[0]["T1Model"]).Equals("--"))
                ((Label)ucTradeInData1.FindControl("lblCar_t")).Text += " - " + Convert.ToString(dtDerails.Rows[0]["T1Model"]);
            
            // 11 Mar 2013 : to add t1Year at top
            if (!Convert.ToString(dtDerails.Rows[0]["T1Year"]).Equals(String.Empty) && !Convert.ToString(dtDerails.Rows[0]["T1Year"]).Equals("--"))
                ((Label)ucTradeInData1.FindControl("lblCar_t")).Text += " - " + Convert.ToString(dtDerails.Rows[0]["T1Year"]);


            
            ((Label)ucTradeInData1.FindControl("lblSurname_t")).Text = "<span style='color:Black; font-size:13px; font-weight:normal;'>Surname:</span>&nbsp;" + Convert.ToString(dtDerails.Rows[0]["LastName"]);
            //end 
            ucTradeInData1.DisplayTradeInData(dtDerails, "Report");

            dtDerails = null;
            dv = null;
            objTradeIn.Key = key;
            dtDerails = objTradeIn.GetTradeInHistoryByKey();
            ucTradeInData1.DisplayTradeInHistory(dtDerails);

        }
    }

    protected void gvTInReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdfIsPhoto = (HiddenField)e.Row.FindControl("hdfIsPhoto");
                BoundField trans = gvTInReport.Columns[4] as BoundField;
                if (e.Row.Cells[4].Text == "Automatic")
                {
                    e.Row.Cells[4].Text = "Auto";
                }
                else if (e.Row.Cells[4].Text == "Manual")
                {
                    e.Row.Cells[4].Text = "Man";
                }
                else if (e.Row.Cells[4].Text == "Semi-Automatic")
                {
                    e.Row.Cells[4].Text = "Semi-Auto";
                }

                // Change row color if that record have photo uploaded
                if (Convert.ToInt32(hdfIsPhoto.Value) == 1)
                    e.Row.BackColor = System.Drawing.Color.FromName("#87C8E3");
            }
        }
        catch (Exception ex)
        {


        }
    }
   
    #endregion

    #endregion


}
