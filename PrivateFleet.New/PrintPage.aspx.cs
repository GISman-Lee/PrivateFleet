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
using Mechsoft.GeneralUtilities;
using log4net;
using System.IO;
using System.Text;
using System.Web.Mail;
using System.Reflection;
//using System.Net.Mail;



public partial class PrintPage : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    int intQuote;
    static ILog logger = LogManager.GetLogger(typeof(PrintPage));
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ReqID"] != null && Request.QueryString["ReqID"] != string.Empty)
        {
            int intRequestId = Convert.ToInt32(Request.QueryString["ReqID"]);
            ViewState["RequestId"] = intRequestId;

            DataList dl1 = (DataList)UcRequestHeader1.FindControl("DataList2");
            UcRequestHeader1.DisplayRequestHeader(intRequestId);
            dl1.RepeatColumns = 2;
            dl1.Style.Add("width", "100%");



            if (Request.QueryString["QuoteID"] != null && Request.QueryString["QuoteID"] != string.Empty)
            {
                UcShortlistedQuotation1.QuotationId = Convert.ToInt32(Request.QueryString["QuoteID"]);
                intQuote = Convert.ToInt32(Request.QueryString["QuoteID"]);
            }

            BindData();
        }

    }
    #region "Methods"
    private void BindData()
    {
        Cls_Request objRequest = new Cls_Request();
        try
        {
            objRequest.RequestId = Convert.ToInt32(ViewState["RequestId"]);

            if (Request.QueryString["ConsultantID"] != null && Request.QueryString["ConsultantID"].ToString() != "")
                objRequest.ConsultantId = Convert.ToInt32(Request.QueryString["ConsultantID"]);
            else
                objRequest.ConsultantId = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);

            dt = objRequest.GetSelectedQuotation();


            //to add RRp on top 
            foreach (DataRow drTemp in dt.Rows)
            {
                DataRow dr1 = dt.NewRow();
                if (Convert.ToString(drTemp["Description"]) == "Recommended Retail Price Exc GST")
                {
                    for (int i = 0; i < drTemp.ItemArray.Length; i++)
                    {
                        dr1[i] = drTemp.ItemArray[i];
                    }
                    dt.Rows.Remove(drTemp);
                    dt.Rows.InsertAt(dr1, 0);
                    break;

                }
            }
            //end

            //calculate sum of accessory
            double sum = 0;

            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt16(dr["SumCol"]) == 1)
                    sum += Convert.ToDouble(dr["QuoteValue"]);
            }
            string strQutval = String.Format("{0:c}", Convert.ToDouble(sum));
            strQutval = strQutval.Substring(1, strQutval.Length - 1);
            strQutval = strQutval.Replace(",", "");
            dt.PrimaryKey = new DataColumn[] { dt.Columns["Description"] };
            dt.Columns["QuoteValue"].ReadOnly = false;
            DataRow dr2 = dt.Rows.Find("Total Cost of Accessories");
            if (dr2 != null)
            {
                dr2.BeginEdit();
                dr2["QuoteValue"] = strQutval;
                dr2.EndEdit();
            }
            //end

            gvMakeDetails.Columns[1].ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            gvMakeDetails.DataSource = dt;
            gvMakeDetails.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("BindData Method : " + ex.Message);
            throw;
        }
    }
    #endregion
    protected void gvMakeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "gvNormalRow";

                if (DataBinder.Eval(e.Row.DataItem, "Description").Equals("Additional Accessories")
                    || DataBinder.Eval(e.Row.DataItem, "Description").Equals("Fixed Charges"))
                {
                    e.Row.CssClass = "gridactiverow";
                    e.Row.Cells[0].ColumnSpan = gvMakeDetails.Columns.Count;
                    for (int i = 1; i < e.Row.Controls.Count; i++)
                    {
                        e.Row.Cells[i].Visible = false;
                    }
                }

                Label lblQuoteValue = (Label)e.Row.FindControl("lblQuoteValue");
                string strQutval = lblQuoteValue.Text;
                // strQutval = String.Format("{0:c}", Convert.ToDouble(strQutval));
                //lblQuoteValue.Text=strQutval.Substring(0, strQutval.IndexOf('.'));
                lblQuoteValue.Text = strQutval;

                if (DataBinder.Eval(e.Row.DataItem, "Description").Equals("Recommended Retail Price Exc GST")
                    || DataBinder.Eval(e.Row.DataItem, "Description").Equals("Total-On Road Cost (Inclusive of GST)"))
                {
                    e.Row.CssClass = "gridactiverow";
                    if (DataBinder.Eval(e.Row.DataItem, "Description").Equals("Total-On Road Cost (Inclusive of GST)"))
                    {
                        e.Row.Style.Value = "Font-Weight:bold";
                    }


                }
                if (DataBinder.Eval(e.Row.DataItem, "Description").Equals("Sub Total") || DataBinder.Eval(e.Row.DataItem, "Description").Equals("Sub Total -"))
                {
                    e.Row.CssClass = "gridactiverow";
                    e.Row.Style.Value = "Font-Weight:bold";
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error("gvMakeDetails_RowDataBound Event : " + ex.Message);
        }


    }
    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnSendMail_Click(object sender, ImageClickEventArgs e)
    {
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
        Cls_Quotation objQuotation = new Cls_Quotation();
        DataTable dtDealer = new DataTable();
        objQuotation.QuotationID = intQuote;
        dtDealer = objQuotation.GetQuotationHeaders();
        HtmlTable httbl = new HtmlTable();
        StringBuilder str = new StringBuilder();
        str.Append("<table align='Center' cellpadding='3' cellspacing='3' border='1'>");
        str.Append("<tr style='color:#ffffff; background:#0A73A2; font-size:13; font-weight: bold;'>");
        str.Append("<td>Consultant Name</td>");
        str.Append("  ");
        str.Append("<td>Consultant Note</td>");
        str.Append("  ");
        str.Append("<td>Quotetion Date</td>");
        str.Append("  ");
        str.Append("</tr>");
        for (int i = 0; i < dtDealer.Rows.Count; i++)
        {
            str.Append("<tr>");
            str.Append("<td>");
            str.Append(dtDealer.Rows[i][8].ToString());
            str.Append("</td>");
            str.Append("  ");
            str.Append("<td>");
            str.Append(dtDealer.Rows[i][13].ToString());
            str.Append("</td>");
            str.Append("  ");
            str.Append("<td>");
            str.Append(dtDealer.Rows[i][0].ToString());
            str.Append("</td>");
            str.Append("  ");
            str.Append("</tr>");
        }
        str.Append("<tr style='color:#ffffff; background:#0A73A2; font-size:13; font-weight: bold;'>");
        str.Append("<td>Dealer Name</td>");
        str.Append("  ");
        str.Append("<td>Dealer Note</td>");
        str.Append("  ");
        str.Append("<td>Estimated Deliveri date</td>");
        str.Append("  ");
        str.Append("</tr>");
        for (int i = 0; i < dtDealer.Rows.Count; i++)
        {
            str.Append("<tr>");
            str.Append("<td>");
            str.Append(dtDealer.Rows[i][10].ToString());
            str.Append("</td>");
            str.Append("  ");
            str.Append("<td>");
            str.Append(dtDealer.Rows[i][9].ToString());
            str.Append("</td>");
            str.Append("  ");
            str.Append("<td>");
            str.Append(dtDealer.Rows[i][4].ToString());
            str.Append("</td>");
            str.Append("  ");
            str.Append("</tr>");
        }
        str.Append("<tr style='color:#ffffff; background:#0A73A2; font-size:13; font-weight: bold;'>");
        str.Append("<td>Ex Stock</td>");
        str.Append("  ");
        str.Append("<td>Order</td>");
        str.Append("  ");
        str.Append("<td>Compilance Date</td>");
        str.Append("  ");
        str.Append("</tr>");
        for (int i = 0; i < dtDealer.Rows.Count; i++)
        {
            str.Append("<tr>");
            str.Append("<td>");
            str.Append(dtDealer.Rows[i][5].ToString());
            str.Append("</td>");
            str.Append("  ");
            str.Append("<td>");
            str.Append(dtDealer.Rows[i][6].ToString());
            str.Append("</td>");
            str.Append("  ");
            str.Append("<td>");
            str.Append(dtDealer.Rows[i][7].ToString());
            str.Append("</td>");
            str.Append("  ");
            str.Append("</tr>");
        }
        str.Append("</table>");
        str.Append("<table align='Center' cellpadding='3' cellspacing='3' border='1'>");
        str.Append("<tr style='color:#ffffff; background:#0A73A2; font-size:13; font-weight: bold;'>");
        str.Append("<td>Description</td>");
        str.Append("  ");
        str.Append("<td>Quote Value</td>");
        str.Append("  ");
        str.Append("</tr>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str.Append("<tr>");
            str.Append("<td>");
            str.Append(dt.Rows[i][0].ToString());
            str.Append("</td>");
            str.Append("  ");
            str.Append("<td>");
            str.Append(dt.Rows[i][3].ToString());
            str.Append("</td>");
            str.Append("  ");
            str.Append("</tr>");
        }
        str.Append("</table>");
        objEmailHelper.EmailBody = str.ToString();
        objEmailHelper.EmailToID = dtDealer.Rows[0][11].ToString();
        objEmailHelper.EmailFromID = dtDealer.Rows[0][12].ToString();

        objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
        objEmailHelper.EmailFromID = "manoj.mahagaonkar@mechsoftgroup.com";

        objEmailHelper.EmailSubject = "Shortlisted Quotetion";
        objEmailHelper.SendEmail();


    }

}
