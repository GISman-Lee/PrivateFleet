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
using log4net;
using Mechsoft.GeneralUtilities;

public partial class ViewQuotationReport : System.Web.UI.Page
{
    ILog logger = LogManager.GetLogger(typeof(ViewQuotationReport));
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Set page header text
            Label lblHeader = (Label)Master.FindControl("lblHeader");

            if (lblHeader != null)
                lblHeader.Text = "View  Quotation";




            if (Request.QueryString["QuoteID"] != null && Request.QueryString["QuoteID"] != string.Empty)
            {
                UcShortlistedQuotation1.QuotationId = Convert.ToInt32(Request.QueryString["QuoteID"]);
                ViewState["QuoteID"] = Request.QueryString["QuoteID"];
            }

            BindData();

        }
        catch (Exception ex)
        {
            logger.Error("Page load event : " + ex.Message);
        }

    }
    private void BindData()
    {
        Cls_Request objRequest = new Cls_Request();
        try
        {
            objRequest.QuotationID = Convert.ToInt32(ViewState["QuoteID"]);

            if (Request.QueryString["OptionID"] != null && Request.QueryString["OptionID"].ToString() != "")
                objRequest.OptionID = Convert.ToInt32(Request.QueryString["OptionID"]);


            DataTable dt = objRequest.GetOptionOfTheQuotation();

            gvMakeDetails.DataSource = dt;
            gvMakeDetails.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("BindData Method : " + ex.Message);
            throw;
        }
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Default2.aspx");
    }
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

                if (DataBinder.Eval(e.Row.DataItem, "Description").Equals("Recommended Retail Price Exc GST")
                    || DataBinder.Eval(e.Row.DataItem, "Description").Equals("Total-On Road Cost (Inclusive of GST)"))
                {
                    e.Row.CssClass = "gridactiverow";

                }
            }

        }
        catch (Exception ex)
        {
            logger.Error("gvMakeDetails_RowDataBound Event : " + ex.Message);
        }
    }
}
