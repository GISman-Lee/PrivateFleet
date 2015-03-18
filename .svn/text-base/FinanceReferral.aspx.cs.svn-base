using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mechsoft.GeneralUtilities;

public partial class FinanceReferral : System.Web.UI.Page
{
    string FDate, TDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            ((Label)this.Master.FindControl("lblHeader")).Text = "Finance Referral Details";

            if (Request.QueryString["FDate"] != null && Request.QueryString["FDate"] != String.Empty)
                FDate = Convert.ToString(Request.QueryString["FDate"]);
            if (Request.QueryString["TDate"] != null && Request.QueryString["TDate"] != String.Empty)
                TDate = Convert.ToString(Request.QueryString["TDate"]);

            BindData();

        }
    }
    protected void gvFinanceReferral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFinanceReferral.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvFinanceReferral_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        BindData();
    }

    private void BindData()
    {
        if (Request.QueryString["ID"] != null)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
            Cls_Reports objReport = new Cls_Reports();
            objReport.ConsultantID = Convert.ToInt32(Request.QueryString["ID"].ToString());
            objReport.FromDate = DateTime.Parse(FDate.ToString(), culture);
            objReport.ToDate = DateTime.Parse(TDate.ToString(), culture);
            DataTable dtFinanceReferral = objReport.GetFinanceReferralByUserID();
            DataView dv = dtFinanceReferral.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

            dtFinanceReferral = dv.ToTable();
            if (dtFinanceReferral.Rows.Count > 0)
            {
                gvFinanceReferral.DataSource = dtFinanceReferral;
                gvFinanceReferral.DataBind();
                
            }
            else
            {
                gvFinanceReferral.DataSource = null;
                gvFinanceReferral.DataBind();
            }
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

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Consultant_Summary_Report.aspx");
    }
}
