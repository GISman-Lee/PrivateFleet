using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mechsoft.GeneralUtilities;
using log4net;

public partial class Consultant_Summary_Report : System.Web.UI.Page
{

    DataTable dtConsultant = new DataTable();
    static ILog logger = LogManager.GetLogger(typeof(Consultant_Summary_Report));
    Cls_Reports objReport = new Cls_Reports();

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {

        ((Label)this.Master.FindControl("lblHeader")).Text = "Consultant Summary Report";
        if (!IsPostBack)
        {
            try
            {
                CheckDate();

                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Consultant";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                ddl_NoRecords.SelectedIndex = 3;
                gvAllConsultant.PageSize = Convert.ToInt16(ddl_NoRecords.SelectedValue);
                if (Session[Cls_Constants.ROLE_ID] != null)
                {
                    if (Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 1)
                    {
                        trConsultant.Visible = true;
                        FillConsultant();
                    }
                    else if (Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 3 || Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 4)
                    {
                        trConsultant.Visible = false;
                    }
                    //Modified By: Ayyaj Suggested By: Mark on 13 May 2014
                    //Desc: Giving Same Acces Writes to fincar consultant as Private fleet consultant
                    else if (Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 5)
                    {
                        trConsultant.Visible = false;
                    }
                }

                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords.Visible = false;

            }
            catch (Exception ex)
            {
                logger.Error("Error in  Page load" + ex.Message);
            }
        }
    }

    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        BindData();

    }

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        txtCalenderFrom.Text = "";
        txtCalenderToDate.Text = "";
        CheckDate();
        if (ddlConsultantLst.Items.Count > 0)
        {
            ddlConsultantLst.SelectedIndex = 0;
        }
        DataTable dtTemp = null;

        ViewState["gvAllConsultant"] = dtTemp;

        lblRowsToDisplay2.Visible = false ;

        ddl_NoRecords.Visible = false;

        gvAllConsultant.DataSource = dtTemp;
        gvAllConsultant.DataBind();

    }
    #region No of rows to display
    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvAllConsultant.PageIndex = 0;

        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {
            //for view 3 (vwAllSLQuotation)
            gvAllConsultant.PageSize = gvAllConsultant.PageCount * gvAllConsultant.Rows.Count;
            gvAllConsultant.DataSource = (DataTable)ViewState["gvAllConsultant"];
            gvAllConsultant.DataBind();

        }
        else
        {
            //for view 3 (vwAllSLQuotation)
            gvAllConsultant.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            gvAllConsultant.DataSource = (DataTable)ViewState["gvAllConsultant"];
            gvAllConsultant.DataBind();
        }
    }
    #endregion

    protected void gvAllConsultant_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        Rebind();
    }

    protected void gvAllConsultant_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllConsultant.PageIndex = e.NewPageIndex;
        Rebind();
    }


    protected void lnkFR_Click(object sender, EventArgs e)
    {
        try
        {
            Int32 UserID = Int32.Parse(((LinkButton)sender).CommandArgument.ToString());
            hdFinanceReferral.Value = UserID.ToString();

            Response.Redirect("FinanceReferral.aspx?id=" + UserID + "&FDate=" + txtCalenderFrom.Text + "&TDate=" + txtCalenderToDate.Text);
            /*ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            BindFinRefData(UserID);*/
        }
        catch (Exception ex)
        {
            logger.Error("Error in lnkFR" + ex.Message);
        }
    }

    protected void gvFinanceReferral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFinanceReferral.PageIndex = e.NewPageIndex;

        BindFinRefData(Convert.ToInt32(hdFinanceReferral.Value));
    }

    protected void gvFinanceReferral_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        BindFinRefData(Convert.ToInt32(hdFinanceReferral.Value));
    }

    protected void btnPopClose_Click(object sender, ImageClickEventArgs e)
    {
        divFinRef.Visible = false;
    }
    #endregion

    #region Methods

    private void BindFinRefData(Int32 UserID)
    {
        try
        {
            if (UserID != 0)
            {
                Cls_Reports objReport = new Cls_Reports();
                objReport.ConsultantID = UserID;
                DataTable dtFinanceReferral = objReport.GetFinanceReferralByUserID();
                DataView dv = dtFinanceReferral.DefaultView;
                dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

                dtFinanceReferral = dv.ToTable();
                if (dtFinanceReferral.Rows.Count > 0)
                {
                    gvFinanceReferral.DataSource = dtFinanceReferral;
                    gvFinanceReferral.DataBind();
                    divFinRef.Visible = true;
                }
                else
                {
                    gvFinanceReferral.DataSource = null;
                    gvFinanceReferral.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Error in Bind Finance Referral Data" + ex.Message);
        }
    }

    private void CheckDate()
    {
        //DateTime dt = DateTime.ParseExact(DateTime.Today.Subtract(TimeSpan.FromDays(7)).ToString(), "dd/MM/yyyy", "", System.Globalization.DateTimeStyles.None);
        //DateTime toDate = DateTime.ParseExact(DateTime.Today.ToString(), "dd/MM/yyyy", "", System.Globalization.DateTimeStyles.None);
        txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
        txtCalenderToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
    }



    private void Rebind()
    {

        if (ViewState["gvAllConsultant"] == null)
            dtConsultant = objReport.GetConsultantSummary();
        else
            dtConsultant = (DataTable)ViewState["gvAllConsultant"];


        DataView dv = dtConsultant.DefaultView;
        dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

        dtConsultant = dv.ToTable();
        if (dtConsultant.Rows.Count > 0)
        {
            gvAllConsultant.DataSource = dtConsultant;
            gvAllConsultant.DataBind();
            ViewState["gvAllConsultant"] = dtConsultant;

            lblRowsToDisplay2.Visible = true;

            ddl_NoRecords.Visible = true;
        }
        else
        {
            DataTable dttemp = new DataTable();
            dttemp = null;
            ViewState["gvAllConsultant"] = null;
            gvAllConsultant.DataSource = null;
            gvAllConsultant.DataBind();


            lblRowsToDisplay2.Visible = false;

            ddl_NoRecords.Visible = false;
        }
    }

    private void BindData()
    {
        try
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);

            DateTime dtFromDate = DateTime.Parse(Request.Params[txtCalenderFrom.UniqueID].ToString(), culture);
            DateTime dtToDate = DateTime.Parse(Request.Params[txtCalenderToDate.UniqueID].ToString(), culture);
            calFrom.SelectedDate = dtFromDate;
            calTO.SelectedDate = dtToDate;
            if (dtFromDate > dtToDate)
            {
                Page page = HttpContext.Current.Handler as Page;

                if (page != null)
                {
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Start date should be greater than or equal to end date');", true);

                }
            }
            else
            {

                if (Session[Cls_Constants.ROLE_ID] != null)
                {
                    //check user is admin or consultant
                    if (Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 1)
                    {
                        if (ddlConsultantLst.SelectedValue == "Select All")
                        {
                            objReport.ConsultantID = 0;
                        }
                        else
                        {
                            objReport.ConsultantID = Convert.ToInt32(ddlConsultantLst.SelectedValue);
                        }
                    }
                    else
                    {
                        if (Session[Cls_Constants.LOGGED_IN_USERID] != null)
                        {
                            objReport.ConsultantID = Convert.ToInt16(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
                        }
                    }
                    if (dtFromDate != null)
                    {
                        objReport.FromDate = dtFromDate;
                    }

                    if (dtToDate != null)
                    {
                        objReport.ToDate = dtToDate;
                    }


                }
            }

            ViewState["gvAllConsultant"] = null;
            Rebind();


        }
        catch (Exception ex)
        {
            logger.Error("Error in Bind Data" + ex.Message);
        }
    }

    private void FillConsultant()
    {
        try
        {
            Cls_UserMaster objUserMaster = new Cls_UserMaster();
            DataTable dt = objUserMaster.GetAllConsultants();
            if (dt.Rows.Count > 1)
            {
                ddlConsultantLst.DataSource = dt;
                ddlConsultantLst.DataTextField = "Name";
                ddlConsultantLst.DataValueField = "ID";
                ddlConsultantLst.DataBind();
            }
            ddlConsultantLst.Items.Insert(0, "Select All");
        }
        catch (Exception ex)
        {
            logger.Error("Error in Fill Consultant" + ex.Message);
        }
    }

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
        catch (Exception ex)
        {
            logger.Error("Error in DefineSortDirection" + ex.Message);
        }
    }

    protected void gvAllConsultant_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                LinkButton lnkFR = (LinkButton)e.Row.FindControl("lnkFR");
                if (Convert.ToInt16(lnkFR.Text) == 0)
                {
                    lnkFR.Enabled = false;
                }
            }
            catch (Exception ex) { 

            }                
        }
    }
    
    #endregion
    
}
