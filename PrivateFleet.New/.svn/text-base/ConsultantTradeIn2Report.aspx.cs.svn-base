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
using System.Text;
using System.Configuration;

public partial class ConsultantTradeIn2Report : System.Web.UI.Page
{
    #region Variables

    static ILog logger = LogManager.GetLogger(typeof(ConsultantTradeIn2Report));
    Cls_CompletedQuoatationReportHelper objReport;
    DataTable dt = null;

    #endregion

    #region Page_Load

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            Label lblHeader = (Label)Master.FindControl("lblHeader");
            if (lblHeader != null)
                lblHeader.Text = "Trade In Report";
          //=================================================================
          //========== Change By Chetan on 15-01-2014========================
            if (Request.QueryString["mode"] != null && Convert.ToString(Request.QueryString["mode"]) == "book")
            {
                ddlstatus.Visible = true;
                td_trade.Visible = true;
            }
            else
            {
                ddlstatus.Visible = false;
                td_trade.Visible = false;
            }
          //==================================================================
            CallPageLoad();

            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "DeliveryDateSort";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;

            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            gvTradeIn2Report.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            ddl_NoRecords.Visible = false;
            lblRowsToDisplay.Visible = false;

            GenerateReport(); // on 29 june 2012
        }
    }

    #endregion

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

    public void GenerateReport()
    {
        // IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
        objReport = new Cls_CompletedQuoatationReportHelper();

        try
        {
            objReport.MakeId = Convert.ToInt32(ddlMake.SelectedValue);
            objReport.ModelName = Convert.ToString(txtModel.Text);
            objReport.Transmision = ddlTransmission.SelectedValue;
            objReport.HomeStateID = Convert.ToInt32(ddlState.SelectedValue);
            objReport.Surname = txtSurname.Text.Trim();
            objReport.Consultant = txtConsultant.Text.Trim();
            objReport.RegoNo = txtRegoNo.Text.Trim();

            ViewState["gvTradeIn2Report"] = null;
            DataBind1();
        }
        catch (Exception ex)
        {
            logger.Error("Trade in 2 GenerateReport Error - " + ex.Message);
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

    private void DataBind1()
    {
        if (ViewState["gvTradeIn2Report"] == null)
            dt = objReport.GetReportForTradeIn();
        else
            dt = (DataTable)ViewState["gvTradeIn2Report"];

        string query = "";
        DataView dv = dt.DefaultView;
        dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
        if (Request.QueryString["mode"] != null && Convert.ToString(Request.QueryString["mode"]) == "just")
            dv.RowFilter = "LastsyncDate>='" + System.DateTime.Now.Date.AddDays(-3).ToString("yyyy-MM-dd") + "'";
        else if (Request.QueryString["mode"] != null && Convert.ToString(Request.QueryString["mode"]) == "book")
        {
            //========================================================================
            //============== Changes done by chetan on 15-01=2014
            //dv.RowFilter = "DeliveryDateSort<='" + System.DateTime.Now.Date.AddDays(4).ToString("yyyy-MM-dd") + "' AND DeliveryDateSort>='" + System.DateTime.Now.Date.ToString("yyyy-MM-dd") + "'";

            if (ddlstatus.SelectedValue != "0")
            {
                query = " TradeStatus like '%" + ddlstatus.SelectedValue + "%' and ";
            }
            query = query+ "DeliveryDateSort<='" + System.DateTime.Now.Date.AddDays(4).ToString("yyyy-MM-dd") + "' AND DeliveryDateSort>='" + System.DateTime.Now.Date.ToString("yyyy-MM-dd") + "'";
            dv.RowFilter = query;
            //========================================================================
            dv.Sort = "DeliveryDateSort ASC";
        }      
        dt = dv.ToTable();

        if (dt.Rows.Count > 0)
        {
            ddl_NoRecords.Visible = true;
            lblRowsToDisplay.Visible = true;
            ViewState["gvTradeIn2Report"] = dt;
        }
        gvTradeIn2Report.DataSource = dt;
        gvTradeIn2Report.DataBind();
        if (Session[Cls_Constants.ROLE_ID] != null && Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 4)
        {
            gvTradeIn2Report.Columns[1].Visible = false;
        }
        else if (Session[Cls_Constants.ROLE_ID] != null && Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 1)
        {
            gvTradeIn2Report.Columns[11].Visible = false;
        }
    }

    #endregion

    #region Events

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

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        ddlState.SelectedValue = "0";
        ddlMake.SelectedValue = "0";
        ddlTransmission.SelectedValue = "0";
        txtModel.Text = String.Empty;
        ddlstatus.SelectedValue = "0";
    }

    protected void gvTradeIn2Report_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTradeIn2Report.PageIndex = e.NewPageIndex;
        gvTradeIn2Report.DataSource = (DataTable)ViewState["gvTradeIn2Report"];
        gvTradeIn2Report.DataBind();
    }

    protected void gvTradeIn2Report_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        // BindData(objCourseMaster);
        DataBind1();
    }

    protected void gvTradeIn2Report_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtDerails = new DataTable();
        Cls_CompletedQuoatationReportHelper objTradeIn = new Cls_CompletedQuoatationReportHelper();
        if (e.CommandName == "ViewDetails")
        {
            pnlTradeIn_1.Visible = true;
            pnlTradeIn.Visible = false;

            dtDerails = (DataTable)ViewState["gvTradeIn2Report"];

            LinkButton lnkDetails = (LinkButton)e.CommandSource;
            GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;
            string key = Convert.ToString(((HiddenField)gvRow.FindControl("hdfKey")).Value);
            int intRequestId = Convert.ToInt32(gvTradeIn2Report.DataKeys[gvRow.RowIndex].Values["ID"]);

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
            dtDerails = objTradeIn.GetTradeIn2HistoryByKey();
            ucTradeInData1.DisplayTradeInHistory(dtDerails);
        }
        else if (e.CommandName == "ReportProblem")
        {
            divpopID.Visible = true;

            LinkButton lnkDetails = (LinkButton)e.CommandSource;
            GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;
            hdfKey1.Value = Convert.ToString(((HiddenField)gvRow.FindControl("hdfKey")).Value);
            btnSend.CommandArgument = Convert.ToString(gvTradeIn2Report.DataKeys[gvRow.RowIndex].Values["ID"]);
        }
    }

    protected void gvTradeIn2Report_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdfIsPhoto = (HiddenField)e.Row.FindControl("hdfIsPhoto");
                BoundField trans = gvTradeIn2Report.Columns[4] as BoundField;
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
            logger.Error("gvTradeIn2Report_RowDataBound Error - " + ex.Message);
        }
    }

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvTradeIn2Report.PageIndex = 0;

        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {
            //for view 3 (vwAllSLQuotation)
            gvTradeIn2Report.PageSize = gvTradeIn2Report.PageCount * gvTradeIn2Report.Rows.Count;
            gvTradeIn2Report.DataSource = (DataTable)ViewState["gvTradeIn2Report"];
            gvTradeIn2Report.DataBind();

        }
        else
        {
            //for view 3 (vwAllSLQuotation)
            gvTradeIn2Report.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvTradeIn2Report.DataSource = (DataTable)ViewState["gvTradeIn2Report"];
            gvTradeIn2Report.DataBind();
        }
    }

    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        ACE_Model.ContextKey = ddlMake.SelectedValue;
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        pnlTradeIn_1.Visible = false;
        pnlTradeIn.Visible = true;
        Session["TradeIn2Date"] = null;
        GenerateReport();
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
        StringBuilder str = new StringBuilder();

        try
        {
            DataTable dtDerails = (DataTable)ViewState["gvTradeIn2Report"];

            string key = Convert.ToString((hdfKey1.Value));
            int intRequestId = Convert.ToInt32(((ImageButton)sender).CommandArgument);

            DataView dv = new DataView();
            dv = dtDerails.DefaultView;
            dv.RowFilter = "ID=" + intRequestId;

            dtDerails = dv.ToTable();

            if (dtDerails != null && dtDerails.Rows.Count > 0)
            {
                // Email Sending start
                str.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear User<br /><br />Stuart has reported \"" + Convert.ToString(dtDerails.Rows[0]["LastName"]) + "\" trade as having an issue with valuation please contact him immediately to resolve.");
                str.Append("<br /><br /><b>Comment -</b><br />" + txtDesc.Text.Trim());
                str.Append("<br /><br />Kind Regards<br /><br /> Private Fleet");
                str.Append("<br /><a href='http://www.privatefleet.com.au/'>www.privatefleet.com.au</a><br /><br />");
                str.Append("Please note:  This system is in beta.  Please report any bugs, suggestions or other feedback to ");
                str.Append("<a href='mailto:davidlye@privatefleet.com.au'>davidlye@privatefleet.com.au</a></p>");

                objEmailHelper.EmailBody = str.ToString();

                //objEmailHelper.EmailToID = "catherineheyes@privatefleet.com.au;nicholas@privatefleet.com.au";
                //objEmailHelper.EmailToID = "catherineheyes@privatefleet.com.au;Nicholas@privatefleet.com.au";

                objEmailHelper.EmailFromID = Convert.ToString(ConfigurationManager.AppSettings["EmailFromID"]);
                objEmailHelper.EmailSubject = "Stuart Report a Problem for Trade In";
                objEmailHelper.EmailToID = "chetan.shejole@mechsoftgroup.com";
                if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
                    objEmailHelper.SendEmail();
            }
        }
        catch (Exception ex)
        {
            logger.Error("btnSend_Click Error - " + ex.Message);
        }
        finally
        {
            txtDesc.Text = string.Empty;
            hdfKey1.Value = null;
            divpopID.Visible = false;
        }
    }

    protected void btnCancelPopup_Click(object sender, ImageClickEventArgs e)
    {
        hdfKey1.Value = null;
        divpopID.Visible = false;
    }

    #endregion
}
