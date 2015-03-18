using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using System.Text;
using Mechsoft.GeneralUtilities;

public partial class PrintViewQuotation : System.Web.UI.Page
{
    static ILog logger = LogManager.GetLogger(typeof(Cls_Request));
    string Fdate, Tdate;
    int cnt = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["QuoteID"] != null && Request.QueryString["QuoteID"] != string.Empty)
            {
                int QuotationId = Convert.ToInt32(Request.QueryString["QuoteID"]);
                ViewState["QuotationID"] = QuotationId;

                if (Request.QueryString["UserID"] != null && Request.QueryString["UserID"] != string.Empty)
                    ViewState["UserID"] = Convert.ToInt32(Request.QueryString["UserID"]);

                if (Request.QueryString["ReqID"] != null && Request.QueryString["ReqID"] != string.Empty)
                    ViewState["RequestID"] = Convert.ToInt32(Request.QueryString["ReqID"]);
                if (Request.QueryString["DID"] != null && Request.QueryString["DID"] != string.Empty)
                    ViewState["DID"] = Convert.ToInt32(Request.QueryString["DID"]);

                BindData();
                BindData1();
                //  UcRequestHeader1.DisplayRequestHeader((Int32) ViewState["RequestID"]);
                UcQuotationHeader1.DisplayQuotationHeader(QuotationId);
            }
            //Cls_Quotation objQuotation = new Cls_Quotation();
            //objQuotation.QuotationID = Convert.ToInt16(ViewState["QuotationID"].ToString());
            //DataTable dtData= objQuotation.GetQuotationHeaders();
            //DataList1.DataSource = dtData;
            //DataList1.DataBind();
        }


    }
    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }


    private void BindData1()
    {
        Cls_Request objRequest = null;
        objRequest = new Cls_Request();
        objRequest.RequestId = Convert.ToInt32((Int32)ViewState["RequestID"]);
        DataTable dtReq = objRequest.GetDataForQuotation();

        string strMake = "";
        string strModel = "";
        string strSeries = "";
        StringBuilder MakeModelSeries = new StringBuilder();
        DataTable dtReqHeader = objRequest.GetRequestHeaderInfo();
        if (dtReqHeader.Rows.Count > 0)
        {
            strMake = dtReqHeader.Rows[0]["Make"].ToString();
            strModel = dtReqHeader.Rows[0]["Model"].ToString();
            strSeries = dtReqHeader.Rows[0]["Series"].ToString();
        }
        MakeModelSeries.Append(strMake);
        if (strModel != "")
        {
            MakeModelSeries.Append("," + strModel);
        }
        if (strSeries != "")
        {
            MakeModelSeries.Append("," + strSeries);
        }

        if (dtReqHeader.Rows[0]["Series_1"].ToString() != String.Empty)
        {
            MakeModelSeries.Append(" (" + dtReqHeader.Rows[0]["Series_1"].ToString() + ")");
        }
        lblSub1.Text = Convert.ToString(dtReqHeader.Rows[0]["suburb"]);
        lblPCode1.Text = Convert.ToString(dtReqHeader.Rows[0]["pcode"]);

        DataTable dt = new DataTable();
        dt.Columns.Add("Header");
        dt.Columns.Add("Details");

        DataRow dRow = null;
        dRow = dt.NewRow();
        dRow["Header"] = "Make,Model,Series";
        dRow["Details"] = MakeModelSeries.ToString();
        dt.Rows.Add(dRow);

        DataList1.DataSource = dt;
        DataList1.DataBind();

        DataTable dt1 = new DataTable();
        dt1.Columns.Add("Header");
        dt1.Columns.Add("Details");

        DataRow dRow1 = null;

        DataList2.RepeatColumns = 2;

        DataTable dtReqParams = objRequest.GetRequestParameters();
        if (dtReqParams.Rows.Count > 0)
        {

            foreach (DataRow drParam in dtReqParams.Rows)
            {
                dRow1 = dt1.NewRow();
                dRow1["Header"] = drParam["Parameter"].ToString();
                if (drParam["ParamValue"].ToString() == "")
                {
                    dRow1["Details"] = "-";
                }
                else
                {
                    dRow1["Details"] = drParam["ParamValue"].ToString();
                }

                dt1.Rows.Add(dRow1);
            }
        }

        DataList2.DataSource = dt1;
        DataList2.DataBind();
    }



    private void BindData()
    {
        Cls_Quotation objQuotation = new Cls_Quotation();
        string colName = "";
        objQuotation.RequestID = Convert.ToInt16(ViewState["RequestID"].ToString()); ;
        objQuotation.UserID = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
        if (Request.QueryString["chk"] == "fromQC")
            objQuotation.UserID = Convert.ToInt32(ViewState["UserID"]);
        objQuotation.QuotationID = Convert.ToInt16(ViewState["QuotationID"].ToString());
        objQuotation.DealerID = Convert.ToInt32(ViewState["DID"]);
        DataTable dtData = objQuotation.GetPerticularQuotation();

        int NoOfOptions = 0;
        NoOfOptions = objQuotation.GetNoOfOptionsForQuotation();

        DataTable dtData1 = objQuotation.GetQuotationVersion();
        ViewState["QuotationInfo"] = dtData1;

        for (int j = 0; j < dtData1.Rows.Count; j++)
        {
            for (int i = 1; i <= NoOfOptions; i++)
            {
                BoundField BC = new BoundField();
                BC.DataField = "Value" + i.ToString();
                BC.HeaderText = "Option " + i.ToString();
                BC.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                if (j != 0)
                {
                    //BC.ItemStyle.BackColor = System.Drawing.Color.FromName("Red");
                    BC.DataField = "Value" + i.ToString() + "_V" + j.ToString();
                    //  BC.HeaderText = "Option " + i.ToString() + "_V" + j.ToString();
                }
                //heightlight color to latest quotation
                if (j == dtData1.Rows.Count - 1)
                {
                    BC.ItemStyle.BackColor = System.Drawing.Color.FromArgb(174, 255, 174);
                    // BC.ItemStyle.ForeColor = System.Drawing.Color.FromName("White");

                }
                gvMakeDetails.Columns.Add(BC);

            }
        }
        //DataRow dr1 = dtData.NewRow();
        //dr1["ID"] = "-9";
        //dr1["Key"] = "Unused";
        //dr1["IsAccessory"] = 0;
        //dr1["IsChargeType"] = 0;
        //dtData.Rows.Add(dr1);
        //cnt = dtData.Rows.Count;
        //ViewState["RowCnt"] = cnt;
        gvMakeDetails.DataSource = dtData;
        gvMakeDetails.DataBind();
    }


    protected void gvMakeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvMakeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.CssClass = "gvNormalRow";

            Image imgActive = ((Image)e.Row.FindControl("imgActive"));
            try
            {
                if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("Additional Accessories") || DataBinder.Eval(e.Row.DataItem, "Key").Equals("Fixed Charges"))
                {
                    ((Label)e.Row.FindControl("lblMake")).Style.Value = "padding-left: 5px";

                    //for (int i = 2; i <= e.Row.Controls.Count; i++)
                    //{     //}
                    e.Row.CssClass = "gridactiverow";
                    imgActive.Visible = true;

                    //e.Row.Cells[0].ColumnSpan = gvMakeDetails.Columns.Count;
                    //e.Row.Controls[1].Visible = false;
                    //e.Row.Controls[2].Visible = false;
                    //e.Row.Controls[3].Visible = false;
                    //e.Row.Controls[4].Visible = false;
                    //e.Row.Controls[5].Visible = false;

                }

            }
            catch (Exception ex)
            { }

            try
            {
                if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("Recommended Retail Price Exc GST") || DataBinder.Eval(e.Row.DataItem, "Key").Equals("Total-On Road Cost (Inclusive of GST)"))
                {
                    ((Label)e.Row.FindControl("lblMake")).Style.Value = "padding-left: 5px";

                    e.Row.CssClass = "gridactiverow";
                    imgActive.Visible = true;

                    if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("Total-On Road Cost (Inclusive of GST)"))
                    {
                        e.Row.Style.Value = "font-weight:bold";
                    }
                }

                // string strQutval = e.Row.Cells[1].Text;
                // strQutval = String.Format("{0:c}", Convert.ToDouble(strQutval));
                //// e.Row.Cells[1].Text = strQutval.ToString();
                // e.Row.Cells[1].Text = strQutval.Substring(0, strQutval.IndexOf('.'));

                // string strQutval1 = e.Row.Cells[2].Text;
                // strQutval1 = String.Format("{0:c}", Convert.ToDouble(strQutval1));
                // e.Row.Cells[1].Text = strQutval1.ToString();
                //// e.Row.Cells[2].Text = strQutval1.Substring(0, strQutval1.IndexOf('.'));
            }
            catch (Exception ex)
            { }
            try
            {
                if (DataBinder.Eval(e.Row.DataItem, "Key").Equals("Sub Total"))
                {
                    ((Label)e.Row.FindControl("lblMake")).Style.Value = "padding-left: 5px";
                    e.Row.CssClass = "gridactiverow";
                    e.Row.Style.Value = "font-weight:bold";
                    imgActive.Visible = true;
                }
            }
            catch (Exception ex)
            { }
        }
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ViewDealersQuotation.aspx?Fdate=" + Fdate + "&Tdate=" + Tdate, false);
    }
    protected void gvMakeDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        DataTable dtQuote = new DataTable();
        int tot = 0;
        if (ViewState["QuotationInfo"] != null)
        {
            dtQuote = (DataTable)ViewState["QuotationInfo"];
            tot = dtQuote.Rows.Count;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            int cnt = 0;
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell Cell_Header = new TableCell();
            Cell_Header.Text = "";
            Cell_Header.HorizontalAlign = HorizontalAlign.Center;
            Cell_Header.ColumnSpan = 1;
            HeaderRow.Cells.Add(Cell_Header);

            foreach (DataRow dr in dtQuote.Rows)
            {
                Cell_Header = new TableCell();

                Cell_Header.Text = "Version " + ++cnt + "<br/>" + dr["Date"].ToString();
                Cell_Header.HorizontalAlign = HorizontalAlign.Center;

                Cell_Header.ColumnSpan = 2;
                HeaderRow.Cells.Add(Cell_Header);
            }

            gvMakeDetails.Controls[0].Controls.AddAt(0, HeaderRow);

        }
    }
}


