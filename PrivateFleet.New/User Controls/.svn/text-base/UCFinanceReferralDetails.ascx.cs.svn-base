using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mechsoft.GeneralUtilities;

public partial class User_Controls_UCFinanceReferralDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
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
            Cls_Reports objReport = new Cls_Reports();
            objReport.ConsultantID = Convert.ToInt32(Request.QueryString["ID"].ToString());
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

}
