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
using System.IO;
using System.Text;
using System.Web.SessionState;

public partial class ViewShortlistedQuotation : System.Web.UI.Page
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(ViewShortlistedQuotation));

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Set page header text
            Label lblHeader = (Label)Master.FindControl("lblHeader");

            if (lblHeader != null)
                lblHeader.Text = "View Completed Quotation";


            if (Request.QueryString["ReqID"] != null && Request.QueryString["ReqID"] != string.Empty)
            {
                int intRequestId = Convert.ToInt32(Request.QueryString["ReqID"]);
                ViewState["RequestId"] = intRequestId;

                if (Request.QueryString["QuoteID"] != null && Request.QueryString["QuoteID"] != string.Empty)
                {
                    int intQouteId = Convert.ToInt32(Request.QueryString["QuoteID"]);
                    ViewState["QuoteID"] = intQouteId;
                    UcShortlistedQuotation1.QuotationId = Convert.ToInt32(Request.QueryString["QuoteID"]);
                }
                UcRequestHeader1.DisplayRequestHeader(intRequestId);

                BindData();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page load event : " + ex.Message);
        }
    }
    #endregion

    #region "Methods"
    private void BindData()
    {
        Cls_Request objRequest = new Cls_Request();
        Cls_UserMaster objUserMaster = new Cls_UserMaster();
        try
        {
            objRequest.RequestId = Convert.ToInt32(ViewState["RequestId"]);

            if (Request.QueryString["ConsultantID"] != null && Request.QueryString["ConsultantID"].ToString() != "")
            {
                if (Convert.ToInt32(Request.QueryString["ConsultantID"]) == -9999)
                {
                    objUserMaster.RequestID = Convert.ToInt32(ViewState["RequestId"]);
                    objRequest.ConsultantId = objUserMaster.getConsultantID();
                }
                else
                    objRequest.ConsultantId = Convert.ToInt32(Request.QueryString["ConsultantID"]);
            }
            else
                objRequest.ConsultantId = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);

            ViewState["ConsultantId"] = objRequest.ConsultantId;

            DataTable dt = objRequest.GetSelectedQuotation();

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

    #region "Events"
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
                if (DataBinder.Eval(e.Row.DataItem, "Description").Equals("Sub Total") || DataBinder.Eval(e.Row.DataItem, "Description").Equals("Sub Total -") || DataBinder.Eval(e.Row.DataItem, "Description").Equals("Total Cost of Accessories"))
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

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    ((Button)e.Row.FindControl("btnShortList")).CommandArgument = DataBinder.Eval(e.Row.DataItem, "CommandArgument").ToString();
        //}
    }

    protected void CreateContract(object sender, EventArgs e)
    {
        try
        {
            string PageToRedirect = "http://localhost:2540/PrivateFleet.New/ClientDealerContract.aspx";
            string ReqID = Request.QueryString["ReqID"];
            string QuoteID = Request.QueryString["QuoteID"];
            Cls_ClientDealerContract CDC = new Cls_ClientDealerContract();
            DataTable TradeInInfo = CDC.CheckIfTradeIn(ReqID);
            DataTable HeaderInfo = CDC.SearchHeaderInfo(ReqID);
            if(Convert.ToBoolean(TradeInInfo.Rows[0]["TradeIn"]) == true)
            {
                PageToRedirect = "http://localhost:2540/PrivateFleet.New/ClientDealerContractT.aspx";
            }
     
            Response.Redirect(PageToRedirect + "?ReqID=" + ReqID + "&QuoteID=" + QuoteID + "&ConsID=" + HeaderInfo.Rows[0]["ConsultantID"].ToString(), true);
        }
        catch (Exception ex)
        {
            Console.Write("Nothing");
        }
    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session[Cls_Constants.SESSION_BACK_PAGE_URL] != null && !(String.IsNullOrEmpty(Session[Cls_Constants.SESSION_BACK_PAGE_URL].ToString())))
            {
                String PageToRedirect = Session[Cls_Constants.SESSION_BACK_PAGE_URL].ToString();
                if (PageToRedirect.Contains("ViewSentRequestDetails.aspx"))
                {
                }
                else
                    Session[Cls_Constants.SESSION_BACK_PAGE_URL] = "";

                // Response.Redirect(PageToRedirect + "?FDate=" + Request.QueryString["FDate"], true);
                if (PageToRedirect.Contains("ViewSentRequestDetails.aspx"))
                    Response.Redirect(PageToRedirect, true);
                else
                    Response.Redirect(PageToRedirect + "?FDate=" + Request.QueryString["FDate"], true);
            }
            else
            {
                //redirect to sent requests listing page
                Response.Redirect("~/ViewSentRequests.aspx?FDate=" + Request.QueryString["FDate"] + "&TDate=" + Request.QueryString["TDate"]);
            }
        }
        catch (Exception ex)
        {
            logger.Error("btnBack_Click Event : " + ex.Message);
        }
    }
    #endregion


    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string strWindowParams = "menubar=no,scrollbars=yes,status=no,toolbar=no,resizable=yes,left=200,top=20,width=600,height=600";
        string strSCRIPT = "window.open('PrintPage.aspx?ReqID=" + ViewState["RequestId"].ToString() + "&QuoteID=" + ViewState["QuoteID"].ToString() + "&ConsultantID=" + ViewState["ConsultantId"].ToString() + "','my_win','" + strWindowParams + "')";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "_deo", strSCRIPT, true);

    }
}
