using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mechsoft.GeneralUtilities;
using System.Data.Common;
using System.Text;
using System.IO;
using System.Drawing;

public partial class Default2 : System.Web.UI.Page
{


    Cls_Reports objReport = null;
    DataTable dtData = null;
    String TypeOfReport = null;
    string GroupBy = null;
    DataTable dtCollectedData = null, dtCollectedQuoteValues = null;
    Boolean FirstTime = false, FirstTimeForQuoteValues = false;



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMakes();

        }

    }


    private void BindMakes()
    {
        ddlMake.Items.Clear();
        Cls_MakeHelper objMake = new Cls_MakeHelper();
        DataTable dtMakes = objMake.GetAllMakes();
        ddlMake.DataSource = dtMakes;
        ddlMake.DataBind();
        ddlMake.Items.Insert(0, new ListItem("-Select Make-", "- Select -"));

    }
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails")
        {
            GridView gvDetails = (GridView)sender;
            int QuoteID = Convert.ToInt16(gvDetails.DataKeys[Convert.ToInt16(e.CommandArgument)][0].ToString());
            int RequestID = Convert.ToInt16(gvDetails.DataKeys[Convert.ToInt16(e.CommandArgument)][1].ToString());
            string ToRedirect = "ViewQuotation.aspx?QuoteID=" + QuoteID.ToString() + "&ReqID=" + RequestID.ToString();
            Response.Redirect(ToRedirect);
        }
    }
    protected void gvCustom_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }



    private void ClearConstrinatsOFDataTable(DataTable dtData)
    {
        if (dtData != null)
        {
            if (dtData.Columns.Count > 0)
            {
                foreach (DataColumn dc in dtData.Columns)
                {
                    dc.AllowDBNull = true;
                    dc.Unique = false;
                    dc.ReadOnly = false;

                }
            }
        }
    }

    private void SetProperties(string TypeOfReport, Cls_Reports objReport)
    {
        switch (TypeOfReport)
        {
            case ReportType.ByMake: objReport.MakeID = Convert.ToInt16(ddlMake.SelectedValue);
                break;
            case ReportType.ByModel: objReport.ModelID = Convert.ToInt16(ddlModel.SelectedValue);
                break;
            case ReportType.BySeries: objReport.SeriesID = Convert.ToInt16(ddlSeries.SelectedValue);
                break;
            case ReportType.ByStateAndMake: objReport.MakeID = Convert.ToInt16(ddlMake.SelectedValue);
                objReport.State = ddlState.SelectedValue;
                break;
            case ReportType.ByStateAndModel: objReport.ModelID = Convert.ToInt16(ddlModel.SelectedValue);
                objReport.State = ddlState.SelectedValue;
                break;
            case ReportType.ByStateAndSeries: objReport.SeriesID = Convert.ToInt16(ddlSeries.SelectedValue);
                objReport.State = ddlState.SelectedValue;
                break;
            case ReportType.ByState:
                objReport.State = ddlState.SelectedValue;
                break;

            default: break;
        }
    }
    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMake.SelectedIndex > 0)
        {
            ddlModel.Items.Clear();
            Cls_ModelHelper objModel = new Cls_ModelHelper();
            objModel.MakeID = Convert.ToInt16(ddlMake.SelectedValue);
            DataTable dtModels = objModel.GetModelsOfMake();
            ddlModel.DataSource = dtModels;
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, new ListItem("- Select Model -", "- Select -"));


        }
        else
        {
            ddlModel.Items.Clear();
            ddlModel.Items.Insert(0, new ListItem("- No Models Found -", "- Select -"));
            ddlSeries.Items.Clear();
            ddlSeries.Items.Insert(0, new ListItem("- No Series Found -", "- Select -"));
        }
    }
    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlModel.SelectedIndex > 0)
        {
            ddlSeries.Items.Clear();
            Cls_SeriesMaster objSeries = new Cls_SeriesMaster();
            objSeries.ModelID = Convert.ToInt16(ddlModel.SelectedValue);
            DataTable dtSeries = objSeries.GetSeriesOfModel();
            ddlSeries.DataSource = dtSeries;
            ddlSeries.DataBind();
            ddlSeries.Items.Insert(0, new ListItem("- Select Series -", "- Select -"));
        }
        else
        {
            ddlSeries.Items.Clear();
            ddlSeries.Items.Insert(0, new ListItem("- No Series Found -", "- Select -"));
        }
    }
    protected void ddlSeries_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {



        if (ddlMake.SelectedIndex > 0)
        {

            if (ddlModel.SelectedIndex > 0)
            {
                if (ddlSeries.SelectedIndex > 0)
                {
                    TypeOfReport = ReportType.BySeries;
                }
                else
                {
                    TypeOfReport = ReportType.ByModel;
                }
            }
            else
            {
                TypeOfReport = ReportType.ByMake;
            }
        }
        if (ddlState.SelectedIndex > 0)
        {
            if (String.IsNullOrEmpty(TypeOfReport))
            {
                TypeOfReport = ReportType.ByState;
            }
            else
            {
                switch (TypeOfReport)
                {
                    case ReportType.ByMake: TypeOfReport = ReportType.ByStateAndMake;
                        break;
                    case ReportType.ByModel: TypeOfReport = ReportType.ByStateAndModel;
                        break;
                    case ReportType.BySeries: TypeOfReport = ReportType.ByStateAndSeries;
                        break;
                    default: break;

                }
            }
        }
        else
        {
            if (String.IsNullOrEmpty(TypeOfReport))
            {
                TypeOfReport = ReportType.ByNothing;
            }

        }


        objReport = new Cls_Reports();
        SetGroupBy(objReport);







        setDateProperties(objReport);

        objReport.DBOperation = TypeOfReport;
        SetProperties(TypeOfReport, objReport);
        dtData = objReport.GetInitialData();

        gvCustom.DataSource = dtData;
        gvCustom.DataBind();
    }

    private void setDateProperties(Cls_Reports objReport)
    {
        txtStartDate.Text = objReport.StartDate = String.IsNullOrEmpty(txtStartDate.Text) ? DateTime.UtcNow.AddDays(-30).ToString("MM/dd/yyyy") : txtStartDate.Text;
        objReport.EndDate = txtEndDate.Text = String.IsNullOrEmpty(txtEndDate.Text) ? DateTime.UtcNow.ToString("MM/dd/yyyy") : txtEndDate.Text;

    }

    private void SetGroupBy(Cls_Reports objReport)
    {
        switch (ddlGroupBy.SelectedValue)
        {
            case "Dealer": objReport.GroupBy = ReportGroupBy.GroupByDealer;
                break;
            case "Consultant": objReport.GroupBy = ReportGroupBy.GroupByConsultant;
                break;
            case "Quote Created Date": objReport.GroupBy = ReportGroupBy.GroupByDate;
                break;
            case "State": objReport.GroupBy = ReportGroupBy.GroupByState;
                break;
            default: break;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {


        DataTable dtData = (DataTable)ViewState["dtReportData"];

        if (dtData != null)
        {
            dtData.Columns.RemoveAt(0);
            dtData.Columns.RemoveAt(4);
            dtData.Columns.RemoveAt(6);

            int i = 10;
            while (i < dtData.Columns.Count)
            {
                dtData.Columns.RemoveAt(i);
            }
            GridView GridView1 = new GridView();
            GridView1.RowDataBound += new GridViewRowEventHandler(GridView1_RowDataBound);
            GridView1.DataSource = dtData;
            GridView1.DataBind();
            GridView1.HeaderRow.BackColor = Color.FromName("#ECE9D8");

            GridView1.HeaderRow.Style.Add("font-weight", "bold");
            GridView1.HeaderRow.Style.Add("font-size", "13px");
            GridView1.HeaderRow.Style.Add("font-family", "arial");

            string attachment = "attachment; filename=Contacts.xls";

            Response.ClearContent();

            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/ms-word";

            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);



            GridView1.RenderControl(htw);

            Response.Write(sw.ToString());

            Response.End();

            //  ExportToSpreadsheet((DataTable)ViewState["dtReportData"], "Report");
        }


    }

    void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "Model").ToString().Equals("SpanIT", StringComparison.InvariantCultureIgnoreCase))
            {
                e.Row.Cells[0].ColumnSpan = 10;
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[0].BackColor = Color.FromName("#BBD3F8");
                e.Row.Cells[0].ForeColor = Color.Black;
                e.Row.Cells[0].Style.Add("font-weight", "bold");
                e.Row.Cells[0].Style.Add("font-size", "13px");
                e.Row.Cells[0].Style.Add("font-family", "arial");

                while (e.Row.Cells.Count > 1)
                {
                    RemoveCells(e.Row);
                }

            }
            else
            {
                int CellCount = 0;
                while (CellCount < e.Row.Cells.Count)
                {
                    e.Row.Cells[CellCount].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[CellCount].Style.Add("padding", "0px 5px 0px 5px");
                    //e.Row.Cells[CellCount].BackColor = Color.FromName("#ECE9D8");
                    e.Row.Cells[CellCount].Style.Add("font-size", "12px");
                    e.Row.Cells[CellCount].Style.Add("font-family", "verdana");
                    CellCount++;
                }
            }
        }


    }

    private void RemoveCells(GridViewRow gridViewRow)
    {
        for (int i = 1; i < gridViewRow.Cells.Count; i++)
        {
            gridViewRow.Cells.RemoveAt(i);
        }
    }



    public void ExportToSpreadsheet(DataTable table, string name)
    {

        HttpContext context = HttpContext.Current;

        context.Response.Clear();



        foreach (DataColumn column in table.Columns)
        {

            context.Response.Write(column.ColumnName + ";");

        }



        context.Response.Write(Environment.NewLine);



        foreach (DataRow row in table.Rows)
        {

            for (int i = 0; i < table.Columns.Count; i++)
            {

                context.Response.Write(row[i].ToString().Replace(";", string.Empty) + ";");

            }

            context.Response.Write(Environment.NewLine);

        }



        context.Response.ContentType = "text/csv";

        context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + name + ".pdf");

        context.Response.End();

    }



    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            objReport = new Cls_Reports();

            if (ddlQuoteFilters.SelectedValue.Equals("AQR"))
            {
                objReport.DBOperation = QuoteFilters.AllQuotesReturned;
                objReport.QuotationID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID"));
            }
            else
            {
                objReport.DBOperation = QuoteFilters.WinningQuotes;
                objReport.QuotationID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID"));
                objReport.OptionID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "OptionID"));
            }



            dtData = objReport.GetQuotes();


            #region Code to have this data in viewstate while exporting the data
            //if (!FirstTime)
            //{
            //    if (dtData != null)
            //    {
            //        dtCollectedQuoteValues = dtData.Clone();
            //        FirstTime = true;
            //    }

            //}

            //ClearConstrinatsOFDataTable(dtData);
            //ClearConstrinatsOFDataTable(dtCollectedQuoteValues);


            //if (dtData != null && dtCollectedData != null)
            //{
            //    dtCollectedQuoteValues.Merge(dtData);
            //} 
            // ViewState["dtCollectedQuotedValues"] = dtCollectedQuoteValues;

            #endregion

            DataList dtList = (e.Row.FindControl("dtListQuoteValues") as DataList);
            dtList.DataSource = dtData;
            dtList.DataBind();

        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "EmailBody", "<script> SetHiddenField();</script>", false);

    }
    protected void gvCustom_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            String GroupParameterValue = DataBinder.Eval(e.Row.DataItem, "FieldToQuery").ToString();
            String HeaderField = DataBinder.Eval(e.Row.DataItem, "HeaderField").ToString();
            Cls_Reports objReport = new Cls_Reports();

            objReport.DBOperation = TypeOfReport;
            SetProperties(TypeOfReport, objReport);

            objReport.GroupByParameterValue = GroupParameterValue;
            setDateProperties(objReport);
            SetGroupBy(objReport);


            GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
            DataTable dtData = objReport.GroupedData();

            DataView dv = dtData.DefaultView;
            if (ddlQuoteFilters.SelectedValue.Equals("AQR"))
            {
                dv.RowFilter = "IsShortListedQuote=false";
            }
            else
            {
                dv.RowFilter = "IsShortListedQuote=true";
            }

            dtData = dv.ToTable();



            if (!FirstTime)
            {
                if (dtData != null)
                {
                    dtCollectedData = dtData.Clone();
                    FirstTime = true;
                }

            }


            ClearConstrinatsOFDataTable(dtData);
            ClearConstrinatsOFDataTable(dtCollectedData);

            if (dtCollectedData != null)
            {
                DataRow dRow = dtCollectedData.NewRow();
                dRow["Make"] = HeaderField.ToString() + " ( " + ddlGroupBy.Text + " )";
                dRow["Model"] = "SpanIT";
                dtCollectedData.Rows.Add(dRow);
            }
            if (dtData != null && dtCollectedData != null)
            {
                dtCollectedData.Merge(dtData);
            }


            gvDetails.DataSource = dtData;
            gvDetails.DataBind();


            ViewState["dtReportData"] = dtCollectedData;
        }
    }
    protected void dtListQuoteValues_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("ViewDetails"))
        {
            HiddenField hdfRequestID = (HiddenField)e.Item.FindControl("hdfRequestID");
            HiddenField hdfOptionID = (HiddenField)e.Item.FindControl("hdfOptionID");
            HiddenField hdfQuotationID = (HiddenField)e.Item.FindControl("hdfQuotationID");

            Response.Redirect("ViewQuotationReport.aspx?OptionID=" + hdfOptionID.Value.ToString() + "&QuoteID=" + hdfQuotationID.Value.ToString());

        }
    }
    protected void Email_Click(object sender, EventArgs e)
    {

        Cls_GenericEmailHelper objEmail = new Cls_GenericEmailHelper();
        objEmail.ImagePath = Server.MapPath("mailer-banner.gif");

        objEmail.EmailBody = hdnEmail.Value.ToString();
        objEmail.EmailFromID = ConfigurationManager.AppSettings["EmailFromID"];
        objEmail.SMTPServerIP = ConfigurationManager.AppSettings["SMTPServerIP"];
        objEmail.SMTPUserID = ConfigurationManager.AppSettings["SMTPUserID"];
        objEmail.SMTPUserPwd = ConfigurationManager.AppSettings["SMTPUserPwd"];
        objEmail.EmailToID = "prasad.raskar@mechsoftgroup.com";
        objEmail.EmailSubject = "Report";
        objEmail.EmailPriority = System.Net.Mail.MailPriority.Low;
        objEmail.SendEmail();
    }
    protected void dtListQuoteValues_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        
        //(e.Item.FindControl("link") as HtmlAnchor).Attributes.Add("onclick", "javascript:window.open('http://" + Request.Url.Host.ToString() + "/ViewQuotationReport.aspx?OptionID=" + DataBinder.Eval(e.Item.DataItem, "OptionID").ToString() + "&QuoteID=" + DataBinder.Eval(e.Item.DataItem, "QuotationID").ToString() + "','View Quotation','toolbar=yes,menubar=yes,resizable=yes')");
        (e.Item.FindControl("lnkDetails") as HyperLink).Attributes.Add("onclick", "javascript: window.open('http://" + Request.Url.Host.ToString() + "/ViewQuotationReport.aspx?OptionID=" + DataBinder.Eval(e.Item.DataItem, "OptionID").ToString() + "&QuoteID=" + DataBinder.Eval(e.Item.DataItem, "QuotationID").ToString() + "','View Quotation')");
    }
}
