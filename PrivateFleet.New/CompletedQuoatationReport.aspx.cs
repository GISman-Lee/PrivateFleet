using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using log4net;
using Mechsoft.GeneralUtilities;

public partial class CompletedQuoatationReport : System.Web.UI.Page
{
    static ILog logger = LogManager.GetLogger(typeof(CompletedQuoatationReport));
    Cls_CompletedQuoatationReportHelper objReport = new Cls_CompletedQuoatationReportHelper();
    DataTable dtdealer = new DataTable();
    string Fdate, Tdate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label lblHeader = (Label)Master.FindControl("lblHeader");
            if (lblHeader != null)
                lblHeader.Text = "Completed Quotation Report";

            CallPageLoad();

            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Date";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;

            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            gvReport.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            ddl_NoRecords.Visible = false;
            lblRowsToDisplay.Visible = false;

            if (Request.QueryString["Fdate"] != null && Request.QueryString["Tdate"] != null && !Convert.ToString(Request.QueryString["Fdate"]).Equals(string.Empty) && !Convert.ToString(Request.QueryString["Tdate"]).Equals(string.Empty))
            {
                txtCalenderFrom.Text = Convert.ToString(Request.QueryString["Fdate"]);
                TxtToDate.Text = Convert.ToString(Request.QueryString["Tdate"]);
                ddlMake.SelectedValue = Convert.ToString(Request.QueryString["Make"]);
                ddlModel.SelectedValue = Convert.ToString(Request.QueryString["Model"]);
                ddlTransmission.SelectedValue = Convert.ToString(Request.QueryString["Trans"]);
                ddlState.SelectedValue = Convert.ToString(Request.QueryString["State"]);
                //bellow part is added on 18/01/2012 for display searh criterial as it back -Start sachin
                txtModel.Text = Convert.ToString(Request.QueryString["model"]);
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = Convert.ToString(Request.QueryString["Sorton"]);
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Convert.ToString(Request.QueryString["SortOrder"]);
                //bellow part is added on 18/01/2012 for display searh criterial as it back -END
                ddlFuelType.SelectedValue = Convert.ToString(Request.QueryString["FType"]).Trim();
                btnGenerateReport_Click(null, null);

            }
            else
            {
                CheckDate();
            }

            // added to show the report for consultant
            if (Session[Cls_Constants.ROLE_ID] != null && (Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 3 || Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 4))
            {
                DataTable dt = ValidateConsultant();
                ConfigValues objConfig = new ConfigValues();

                int NoOfQuotePer = Convert.ToInt16(objConfig.GetValue(Cls_Constants.NO_OF_QUOTE_SHORTLISTING_PERVENTAGE));
                int NoOfFinRef = Convert.ToInt16(objConfig.GetValue(Cls_Constants.NO_OF_FINANCE_REFERAL));

                int NoOfQuotePerDays = Convert.ToInt16(objConfig.GetValue(Cls_Constants.NO_OF_DAYS_TO_CHECK_QUOTE_SHORTLISTED_PERVENTAGE));
                int NoOfFinRefDays = Convert.ToInt16(objConfig.GetValue(Cls_Constants.NO_OF_DAYS_TO_CHECK_FINANCE_REFERAL));

                if (dt != null)
                {
                    if (Convert.ToDouble(dt.Rows[0]["Percentage"]) >= NoOfQuotePer && Convert.ToInt32(dt.Rows[0]["FinCountt"]) >= NoOfFinRef)
                    {
                        pnlCQR.Visible = true;
                    }
                    else
                    {
                        pnlCQR.Visible = false;
                        pnlConsultantQR.Visible = true;

                        if (Convert.ToDouble(dt.Rows[0]["Percentage"]) < NoOfQuotePer)
                            lblC1.Text = "You need to have selected at least " + NoOfQuotePer + "% of winning quotes from quotes received in the past " + NoOfQuotePerDays + " days but we can only see <b>" + Convert.ToString(Convert.ToInt32(dt.Rows[0]["Percentage"])) + "%</b>";
                        else
                            lblC1.Text = "You need to have selected at least " + NoOfQuotePer + "% of winning quotes from quotes received in the past " + NoOfQuotePerDays + " days and we can see <b>" + Convert.ToString(Convert.ToInt32(dt.Rows[0]["Percentage"])) + "%</b>";

                        if (Convert.ToInt32(dt.Rows[0]["FinCountt"]) < NoOfFinRef)
                            lblC2.Text = "You have made " + NoOfFinRef + " finance referrals in the last " + NoOfFinRefDays + " full working days but we can only see <b>" + Convert.ToString(dt.Rows[0]["FinCountt"]) + "</b>";
                        else
                            lblC2.Text = "You have made " + NoOfFinRef + " finance referrals in the last " + NoOfFinRefDays + " full working days and we can see <b>" + Convert.ToString(dt.Rows[0]["FinCountt"]) + "</b>";
                    }
                }
            }
            else if (Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 1 && Session[Cls_Constants.ROLE_ID] != null)
            { }
            //Modified By: Ayyaj Suggested By: Mark on 13 May 2014
            //Desc: Giving Same Acces Writes to fincar consultant as Private fleet consultant
            else if (Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 5 && Session[Cls_Constants.ROLE_ID] != null)
            {
                DataTable dt = ValidateConsultant();
                ConfigValues objConfig = new ConfigValues();

                int NoOfQuotePer = Convert.ToInt16(objConfig.GetValue(Cls_Constants.NO_OF_QUOTE_SHORTLISTING_PERVENTAGE));
                int NoOfFinRef = Convert.ToInt16(objConfig.GetValue(Cls_Constants.NO_OF_FINANCE_REFERAL));

                int NoOfQuotePerDays = Convert.ToInt16(objConfig.GetValue(Cls_Constants.NO_OF_DAYS_TO_CHECK_QUOTE_SHORTLISTED_PERVENTAGE));
                int NoOfFinRefDays = Convert.ToInt16(objConfig.GetValue(Cls_Constants.NO_OF_DAYS_TO_CHECK_FINANCE_REFERAL));

                if (dt != null)
                {
                    if (Convert.ToDouble(dt.Rows[0]["Percentage"]) >= NoOfQuotePer && Convert.ToInt32(dt.Rows[0]["FinCountt"]) >= NoOfFinRef)
                    {
                        pnlCQR.Visible = true;
                    }
                    else
                    {
                        pnlCQR.Visible = false;
                        pnlConsultantQR.Visible = true;

                        if (Convert.ToDouble(dt.Rows[0]["Percentage"]) < NoOfQuotePer)
                            lblC1.Text = "You need to have selected at least " + NoOfQuotePer + "% of winning quotes from quotes received in the past " + NoOfQuotePerDays + " days but we can only see <b>" + Convert.ToString(Convert.ToInt32(dt.Rows[0]["Percentage"])) + "%</b>";
                        else
                            lblC1.Text = "You need to have selected at least " + NoOfQuotePer + "% of winning quotes from quotes received in the past " + NoOfQuotePerDays + " days and we can see <b>" + Convert.ToString(Convert.ToInt32(dt.Rows[0]["Percentage"])) + "%</b>";

                        if (Convert.ToInt32(dt.Rows[0]["FinCountt"]) < NoOfFinRef)
                            lblC2.Text = "You have made " + NoOfFinRef + " finance referrals in the last " + NoOfFinRefDays + " full working days but we can only see <b>" + Convert.ToString(dt.Rows[0]["FinCountt"]) + "</b>";
                        else
                            lblC2.Text = "You have made " + NoOfFinRef + " finance referrals in the last " + NoOfFinRefDays + " full working days and we can see <b>" + Convert.ToString(dt.Rows[0]["FinCountt"]) + "</b>";
                    }
                }
            }
            else
            {
                Response.Redirect("index.aspx");
            }
            //end

        }

    }

    private void CallPageLoad()
    {
        FillMake();
        FillStates();
        FillTransmmision();

    }

    private void CheckDate()
    {
        //DateTime dt = DateTime.ParseExact(DateTime.Today.Subtract(TimeSpan.FromDays(7)).ToString(), "dd/MM/yyyy", "", System.Globalization.DateTimeStyles.None);
        //DateTime toDate = DateTime.ParseExact(DateTime.Today.ToString(), "dd/MM/yyyy", "", System.Globalization.DateTimeStyles.None);
        txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS * 6)).ToShortDateString()).ToString("dd/MM/yyyy");
        TxtToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
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

    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        ACE_Model.ContextKey = ddlMake.SelectedValue;
        FillModel();
    }

    private void FillModel()
    {
        try
        {
            ddlModel.Items.Clear();

            //DataTable dt = new DataTable();
            //dt = objReport.GetModel(Convert.ToInt32(ddlMake.SelectedValue));
            if (Cache["MODELS"] == null)
                Mechsoft.GeneralUtilities.PreLoad.LoadModel();

            DataTable dt = Cache["MODELS"] as DataTable;

            if (dt != null)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format(@"MakeId={0}", ddlMake.SelectedValue);

                ddlModel.DataSource = dv.ToTable();
                ddlModel.DataTextField = "Model";
                ddlModel.DataValueField = "ID";
                ddlModel.DataBind();
            }
            ddlModel.Items.Insert(0, new ListItem("-Select Model-", "0"));
        }
        catch (Exception ex)
        {
            logger.Error("ddlMake_SelectedIndexChanged Event : " + ex.Message);
        }
    }

    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSeries();
    }

    private void FillSeries()
    {
        try
        {
            ddlSeries.Items.Clear();

            //DataTable dt = new DataTable();
            //dt = objReport.GetSeries(Convert.ToInt32(ddlModel.SelectedValue));
            if (Cache["SERIES"] == null)
                Mechsoft.GeneralUtilities.PreLoad.LoadSeries();

            DataTable dt = Cache["SERIES"] as DataTable;
            if (dt != null)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = string.Format(@"ModelId={0}", ddlModel.SelectedValue);

                ddlSeries.DataSource = dv.ToTable();
                ddlSeries.DataTextField = "Series";
                ddlSeries.DataValueField = "ID";
                ddlSeries.DataBind();
            }
            ddlSeries.Items.Insert(0, new ListItem("-Select Series-", "0"));
        }
        catch (Exception ex)
        {
            logger.Error("ddlModel_SelectedIndexChanged Event : " + ex.Message);
        }
    }

    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
        DataTable dtTemp = null;
        try
        {


            gvReport.DataSource = dtTemp;
            gvReport.DataBind();
            lblMsg.Visible = false;
            objReport.MakeId = Convert.ToInt32(ddlMake.SelectedValue);
            objReport.ModelId = Convert.ToInt32(ddlModel.SelectedValue);
            objReport.ModelName = Convert.ToString(txtModel.Text);
            objReport.SeriesId = Convert.ToInt32(ddlSeries.SelectedValue);
            objReport.StateId = Convert.ToInt32(ddlState.SelectedValue);
            if (ddlTransmission.SelectedValue.ToString() == "")
            {
                objReport.Transmision = "0";
            }
            else
            {
                objReport.Transmision = ddlTransmission.SelectedValue.ToString();
            }
            // added to show the report for consultant
            //if (Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 3)
            //    objReport.ConsultantID = Convert.ToInt16(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
            //else
            objReport.ConsultantID = 0;
            //end

            objReport.FromDate = DateTime.Parse(txtCalenderFrom.Text.Trim(), culture);
            objReport.ToDate = DateTime.Parse(TxtToDate.Text.Trim(), culture);
            //objReport.FromDate = DateTime.Parse(Request.Params[txtCalenderFrom.UniqueID].ToString(), culture);
            //objReport.ToDate = DateTime.Parse(Request.Params[TxtToDate.UniqueID].ToString(), culture);
            objReport.Winning = Convert.ToInt32(ddlWin.SelectedValue);
            //DataTable dt = new DataTable();

            // addedon 27 Jly 20212
            objReport.FuelType = ddlFuelType.SelectedValue;

            //if (dtdealer == null || dtdealer.Rows.Count == 0)
            //{
            //    lblMsg.Visible = true;
            //    return;
            //}
            //else
            //{
            ViewState["gvReport"] = null;
            DataBindDt();
            //}
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void DataBindDt()
    {
        if (ViewState["gvReport"] == null)
            dtdealer = objReport.GetReport();
        else
            dtdealer = (DataTable)ViewState["gvReport"];

        DataView dv = dtdealer.DefaultView;
        dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
        dtdealer = dv.ToTable();

        if (dtdealer.Rows.Count > 0)
        {
            ddl_NoRecords.Visible = true;
            lblRowsToDisplay.Visible = true;
            ViewState["gvReport"] = dtdealer;
        }
        gvReport.DataSource = dtdealer;
        gvReport.DataBind();
    }

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvReport.PageIndex = 0;

        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {
            //for view 3 (vwAllSLQuotation)
            gvReport.PageSize = gvReport.PageCount * gvReport.Rows.Count;
            gvReport.DataSource = (DataTable)ViewState["gvReport"];
            gvReport.DataBind();

        }
        else
        {
            //for view 3 (vwAllSLQuotation)
            gvReport.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvReport.DataSource = (DataTable)ViewState["gvReport"];
            gvReport.DataBind();
        }
    }

    protected void ddlSeries_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillTransmmision();
    }

    private void FillTransmmision()
    {
        try
        {
            ddlTransmission.Items.Clear();
            DataTable dt = new DataTable();
            dt = objReport.GetTransmission(Convert.ToInt32(ddlSeries.SelectedValue));
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

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        ddlMake.Items.Clear();
        ddlModel.SelectedIndex = 0;
        ddlSeries.SelectedIndex = 0;

        ddlState.Items.Clear();
        ddlWin.SelectedIndex = 0;
        //txtCalenderFrom.Text = "";
        //TxtToDate.Text = "";
        CheckDate();
        CallPageLoad();
        DataTable dtTemp = null;


        ViewState["gvReport"] = dtTemp;

        ddlTransmission.SelectedIndex = 0;
        ddl_NoRecords.Visible = false;
        lblRowsToDisplay.Visible = false;
        gvReport.DataSource = dtTemp;
        gvReport.DataBind();
        //for (int i = 0; i < gvReport.Rows.Count; i++)
        //{
        //    gvReport.DeleteRow(i);
        //}
        //gvReport.DataBind();
    }

    protected void gvReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        // BindData(objCourseMaster);
        DataBindDt();
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

    protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReport.PageIndex = e.NewPageIndex;
        gvReport.DataSource = (DataTable)ViewState["gvReport"];
        gvReport.DataBind();
        //DataBindDt();
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlAnchor ac = (HtmlAnchor)e.Row.FindControl("lnkViewQuote");
                //part is added on 18/01/2012 for display searh criterial as it back -Start sachin
                //  +"&Sorton="+Convert .ToString ( ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION])+"&SortOrder="+Convert .ToString (ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION]) and txtModel.Text.Trim() of model is put insteated of ddlMake.SelectedValue
                ac.HRef += "&Fdate=" + txtCalenderFrom.Text + "&Tdate=" + TxtToDate.Text + "&Make=" + ddlMake.SelectedValue + "&Model=" + txtModel.Text.Trim() + "&Trans=" + ddlTransmission.SelectedValue + "&State=" + ddlState.SelectedValue + "&Sorton=" + Convert.ToString(ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION]) + "&SortOrder=" + Convert.ToString(ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION]);

                BoundField trans = gvReport.Columns[4] as BoundField;
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
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lnkMake_Click(object sender, EventArgs e)
    {
        try
        {
            string QuotationID, RequestID, UserID, DealerID;

            GridViewRow gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            QuotationID = Convert.ToString(gvReport.DataKeys[gr.RowIndex]["QuotationID"]);
            RequestID = Convert.ToString(gvReport.DataKeys[gr.RowIndex]["RequestID"]);
            UserID = Convert.ToString(gvReport.DataKeys[gr.RowIndex]["UserID"]);
            DealerID = Convert.ToString(gvReport.DataKeys[gr.RowIndex]["DealerID"]);

            string QryString = "QuoteID=" + QuotationID + "&ReqID=" + RequestID + "&UserID=" + UserID + "&DID=" + DealerID + "&chk=fromAdminCQR";
            QryString += "&Fdate=" + txtCalenderFrom.Text + "&Tdate=" + TxtToDate.Text + "&Make=" + ddlMake.SelectedValue + "&Model=" + txtModel.Text.Trim() + "&Trans=" + ddlTransmission.SelectedValue + "&State=" + ddlState.SelectedValue + "&Sorton=" + Convert.ToString(ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION]) + "&FType=" + Convert.ToString(ddlFuelType.SelectedValue) + "&SortOrder=" + Convert.ToString(ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION]);
            Response.Redirect("ViewQuotation.aspx?" + QryString, false);
        }
        catch (Exception ex)
        {
            logger.Error("Completed Quotation Report lnkMake_Click Error - " + ex.Message);
        }
        finally
        {
        }
    }

    private DataTable ValidateConsultant()
    {
        Cls_Reports objReport = new Cls_Reports();
        try
        {
            objReport.ConsultantID = Convert.ToInt16(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
            return objReport.ValidateConsultant();
        }
        catch (Exception ex)
        {
            logger.Error("CSR validate Consultant Error - " + ex.Message);
            return null;
        }
        finally
        {
            objReport = null;
        }
    }

}
