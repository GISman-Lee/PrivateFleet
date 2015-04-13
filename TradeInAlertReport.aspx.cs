using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;

public partial class TradeInAlertReport : System.Web.UI.Page
{
    ILog logger = LogManager.GetLogger(typeof(TradeInAlertReport));

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label lblHeader = (Label)Master.FindControl("lblHeader");
            if (lblHeader != null)
                lblHeader.Text = "Trade In Alert Report";
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "ConsultantName";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            ViewState["dataset"] = null;
            BindData();
        }
    }

    #region Method
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

    //
    private void BindData()
    {
        Cls_TradeInAlert objTAlert = new Cls_TradeInAlert();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        try
        {

            if (Session[Cls_Constants.LOGGED_IN_USERID] != null)
            {
                objTAlert.CreatedBy = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            }
            else
            {
                Response.Redirect("index.aspx");
            }
            if (ViewState["dataset"] != null)
            {
                ds = (DataSet)ViewState["dataset"];
            }
            else
                ds = objTAlert.GetReportForTradeInAlert();

            ViewState["dataset"] = ds;

            DataView dv = ds.Tables[1].DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            if (dt.Rows.Count > 0)
            {
                trNoRows.Visible = true;
            }
            gvTradeInAlert.DataSource = dt;
            gvTradeInAlert.DataBind();

            ViewState["gvTradeInAlert"] = dt;

        }
        catch (Exception ex)
        {
            logger.Error("Err get trade in alerts - " + ex.Message);

        }
    }



    #region Events

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvTradeInAlert.PageIndex = 0;

        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {
            //for view 3 (vwAllSLQuotation)
            gvTradeInAlert.PageSize = gvTradeInAlert.PageCount * gvTradeInAlert.Rows.Count;
            gvTradeInAlert.DataSource = (DataTable)ViewState["gvTradeInAlert"];
            gvTradeInAlert.DataBind();

        }
        else
        {
            //for view 3 (vwAllSLQuotation)
            gvTradeInAlert.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvTradeInAlert.DataSource = (DataTable)ViewState["gvTradeInAlert"];
            gvTradeInAlert.DataBind();
        }
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        pnlTradeInAlert.Visible = true;
        pnl1.Visible = false;

    }

    protected void gvTradeInAlert_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtDerails = new DataTable();
        DataSet ds_temp = new DataSet();
        if (e.CommandName == "view")
        {
            pnlTradeInAlert.Visible = false;
            pnl1.Visible = true;


            dtDerails = ((DataSet)ViewState["dataset"]).Tables[0];

            LinkButton lnkDetails = (LinkButton)e.CommandSource;
            GridViewRow gvRow = (GridViewRow)lnkDetails.Parent.Parent;
            int intRequestId = Convert.ToInt32(gvTradeInAlert.DataKeys[gvRow.RowIndex].Values["ID"]);

            DataView dv = new DataView();
            dv = dtDerails.DefaultView;
            dv.RowFilter = "ID=" + intRequestId;

            dtDerails = dv.ToTable();

            ucTradeInData1.DisplayTradeInData(dtDerails, "Empty");

        }
    }

    protected void gvTradeInAlert_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        // BindData(objCourseMaster);
        BindData();
    }

    protected void gvTradeInAlert_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTradeInAlert.PageIndex = e.NewPageIndex;
        gvTradeInAlert.DataSource = (DataTable)ViewState["gvTradeInAlert"];
        gvTradeInAlert.DataBind();
    }
    #endregion
}
