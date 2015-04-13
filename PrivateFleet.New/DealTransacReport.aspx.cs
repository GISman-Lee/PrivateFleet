using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Mechsoft.GeneralUtilities;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Configuration;

public partial class DealTransacReport : System.Web.UI.Page
{
    #region Global Variables
    ILog Logger = LogManager.GetLogger(typeof(DealTransacReport));
    int AmountCredited = 0, AmountSpent = 0, BalanceAmount = 0, LeadsSold=0;
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "CompanyName";
            calFromDate.Format = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            calToDate.Format = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            txtToDate.Text = DateTime.Now.ToShortDateString(); //CompValFromDate.ValueToCompare =
            txtFromDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            GetAllDealers();
            GetDealerTopUpReport();
            Label lblHeader = (Label)Master.FindControl("lblHeader");
            if (lblHeader != null)
                lblHeader.Text = "Dealer Topup Report";

        }
    }

    /// <summary>
    /// Created By: Pravin Gholap
    /// Cretaed Date: 4 Sept 2013
    /// Description: Selected Index Changed Event Of No of Records Drop Down List
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDealTopUp.PageIndex = 0;
        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {
            gvDealTopUp.PageSize = gvDealTopUp.PageCount * gvDealTopUp.Rows.Count;
            gvDealTopUp.DataSource = (DataTable)ViewState["AdminTrxnDetails"];
            gvDealTopUp.DataBind();
        }
        else
        {
            gvDealTopUp.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvDealTopUp.DataSource = (DataTable)ViewState["AdminTrxnDetails"];
            gvDealTopUp.DataBind();
        }
    }

    /// <summary>
    /// Created By: Pravin Gholap
    /// Cretaed Date: 4 Sept 2013
    /// Description: Page Index Changed Event Of GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDealTopUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvDealTopUp.PageIndex = e.NewPageIndex;
            GetDealerTopUpReport();
            lblAdminMsg.Text = "";
            lblAdminMsg.CssClass = "";

        }
        catch (Exception ex)
        {

            Logger.Error(ex.Message + ".Error" + ex.StackTrace);
        }
    }

    /// <summary>
    /// Created By: Pravin Gholap
    /// Cretaed Date: 4 Sept 2013
    /// Description: Sorting Event Of GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDealTopUp_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            DefineSortDirection();
            GetDealerTopUpReport();
        }
        catch (Exception ex)
        {
            Logger.Error(ex.Message + ". Error" + ex.StackTrace);
        }
    }

    /// <summary>
    /// Created By: Pravin Gholap
    /// Cretaed Date: 4 Sept 2013
    /// Description: Selected Index Changed Event Of Dealer's Drop Down List
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDealerTopUpReport();
    }

    /// <summary>
    /// Created By: Pravin Gholap
    /// Cretaed Date: 4 Sept 2013
    /// Description: Search Button Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkSearch_Click(object sender, ImageClickEventArgs e)
    {
        GetDealerTopUpReport();
    }

    /// <summary>
    /// Created By: Pravin Gholap
    /// Cretaed Date: 4 Sept 2013
    /// Description: Clear Button Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkClear_Click(object sender, ImageClickEventArgs e)
    {
        txtToDate.Text = txtFromDate.Text = string.Empty;
        ddlDealer.SelectedValue = "0";
        GetDealerTopUpReport();
    }

    /// <summary>
    /// Created By: Pravin Gholap
    /// Cretaed Date: 4 Sept 2013
    /// Description: Row Data Bound Event Of Grid View
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDealTopUp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAmountCredited = (Label)e.Row.FindControl("lblAmountCredited");
            Label lblAmountSpent = (Label)e.Row.FindControl("lblAmountSpent");
            Label lblBalance = (Label)e.Row.FindControl("lblBalance");
            Label lblLeadsSold = (Label)e.Row.FindControl("lblLeadsSold");

            AmountCredited = AmountCredited + Convert.ToInt32(lblAmountCredited.Text.Trim());
            AmountSpent = AmountSpent + Convert.ToInt32(lblAmountSpent.Text.Trim());
            BalanceAmount = BalanceAmount + Convert.ToInt32(lblBalance.Text.Trim());
            LeadsSold = LeadsSold + Convert.ToInt32(lblLeadsSold.Text.Trim());
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblFooterAmtDeposited = (Label)e.Row.FindControl("lblFooterAmtDeposited");
            Label lblFooterAmtSpent = (Label)e.Row.FindControl("lblFooterAmtSpent");
            Label lblFooterAmtBalance = (Label)e.Row.FindControl("lblFooterAmtBalance");
            Label lblFooterLeadSolds = (Label)e.Row.FindControl("lblFooterLeadSolds");

            lblFooterAmtDeposited.Text = AmountCredited.ToString();
            lblFooterAmtSpent.Text = AmountSpent.ToString();
            lblFooterAmtBalance.Text = BalanceAmount.ToString();
            lblFooterLeadSolds.Text = LeadsSold.ToString();
        }
    }
    #endregion

    #region Methods

    /// <summary>
    /// Created By: Pravin Gholap
    /// Cretaed Date: 4 Sept 2013
    /// Description: Get Dealer Report Data
    /// </summary>
    private void GetDealerTopUpReport()
    {
        string leadsCon = ConfigurationManager.AppSettings["LeadsSalesCon"].ToString().Trim();
        SqlConnection con = new SqlConnection(leadsCon);
        try
        {
            DateTime FromDate, ToDate;
            if (!string.IsNullOrEmpty(txtFromDate.Text.Trim()))
                FromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            else
                FromDate = DateTime.MinValue;

            if (!string.IsNullOrEmpty(txtToDate.Text.Trim()))
                ToDate = Convert.ToDateTime(txtToDate.Text.Trim());
            else
                ToDate = DateTime.MinValue;

            con.Open();
            SqlCommand Cmd = new SqlCommand("USP_GetLeadDetRepForQuote", con);
            Cmd.CommandType = CommandType.StoredProcedure;
            if (FromDate != DateTime.MinValue)
                Cmd.Parameters.AddWithValue("@FromDate", FromDate);
            if (ToDate != DateTime.MinValue)
                Cmd.Parameters.AddWithValue("@ToDate", ToDate);
            Cmd.Parameters.AddWithValue("@DealerId", Convert.ToInt64(ddlDealer.SelectedValue.Trim()));
            SqlDataAdapter DA = new SqlDataAdapter(Cmd);
            DataTable dtReport = new DataTable();
            DA.Fill(dtReport);
            if (dtReport.Rows.Count > 0)
            {
                DataView dVDealer = dtReport.DefaultView;
                dVDealer.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
                ViewState["AdminTrxnDetails"] = dtReport;
                if (dtReport.Rows.Count > 0)
                {
                    gvDealTopUp.DataSource = dtReport;
                    gvDealTopUp.DataBind();
                    lblAdminMsg.Text = "";
                    lblAdminMsg.CssClass = "";
                }
                else
                {
                    gvDealTopUp.DataSource = null;
                    gvDealTopUp.DataBind();
                    lblAdminMsg.Text = "No Any TopUp Done Yet";
                    lblAdminMsg.CssClass = "error";
                }
            }
            else
            {
                gvDealTopUp.DataSource = null;
                gvDealTopUp.DataBind();
                lblAdminMsg.Text = "No Any TopUp Done Yet";
                lblAdminMsg.CssClass = "error";
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex.Message + "GetDealerTopUpReport. Error" + ex.StackTrace);
        }
        finally
        {
            con.Close();
        }
    }

    /// <summary>
    /// Created By: Pravin Gholap
    /// Cretaed Date: 2 Sept 2013
    /// Description: Define Sort Direction
    /// </summary>
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
        catch (Exception ex) { Logger.Error(ex.Message + ". Error" + ex.StackTrace); }
    }

    /// <summary>
    /// Created By: Pravin Gholap
    /// Cretaed Date: 3 Sept 2013
    /// Description: Get All Dealers
    /// </summary>
    private void GetAllDealers()
    {
        string leadsCon = ConfigurationManager.AppSettings["LeadsSalesCon"].ToString().Trim();
        SqlConnection con = new SqlConnection(leadsCon);
        DataTable dtDealers = new DataTable();
        try
        {
            con.Open();
            SqlCommand Cmd = new SqlCommand("usp_GetAllDealers", con);
            Cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter DA = new SqlDataAdapter(Cmd);
            DA.Fill(dtDealers);
            if (dtDealers != null && dtDealers.Rows.Count > 0)
            {

                ddlDealer.DataSource = dtDealers;
                ddlDealer.DataTextField = "DealerName";
                ddlDealer.DataValueField = "Id";
                ddlDealer.DataBind();
                ListItem lst = new ListItem("--Select--", "0");
                if (!ddlDealer.Items.Contains(lst))
                    ddlDealer.Items.Insert(0,lst);
                ddlDealer.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            Logger.Error("DealerLeadCost GetAllDealers Error -" + ex.Message + ".Error" + ex.StackTrace);
        }
    }
    #endregion
}
