using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Mechsoft.GeneralUtilities;
using System.Data;

public partial class SendSMSReport : System.Web.UI.Page
{
    #region Variable
    static ILog logger = LogManager.GetLogger(typeof(SendSMSReport));
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Label)this.Master.FindControl("lblHeader")).Text = "SMS Send Report";
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
                    else if (Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 3)
                    {
                        trConsultant.Visible = false;
                    }
                }
                // GenerateReport();
            }
            catch (Exception ex)
            {
                logger.Error("Error in  Page load" + ex.Message);
            }
        }
    }

    private void CheckDate()
    {
        txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
        txtCalenderToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
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
    protected void btnGenerateReport_Click(object sender, ImageClickEventArgs e)
    {
        GenerateReport();
    }

    private void GenerateReport()
    {
        Cls_General objSMSReport = new Cls_General();
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
                    if (Convert.ToInt16(Session[Cls_Constants.ROLE_ID]) == 1)
                    {
                        if (ddlConsultantLst.SelectedValue == "Select All")
                        {
                            objSMSReport.SMSFrom = 0;
                        }
                        else
                        {
                            objSMSReport.SMSFrom = Convert.ToInt32(ddlConsultantLst.SelectedValue);
                        }
                    }
                    else
                    {
                        if (Session[Cls_Constants.LOGGED_IN_USERID] != null)
                        {
                            objSMSReport.SMSFrom = Convert.ToInt16(Session[Cls_Constants.LOGGED_IN_USERID].ToString());
                        }
                    }
                    if (dtFromDate != null)
                    {
                        objSMSReport.FromDate = dtFromDate;
                    }

                    if (dtToDate != null)
                    {
                        objSMSReport.ToDate = dtToDate;
                    }


                }
            }

            ViewState["gvAllConsultant"] = null;
            DataTable dt = new DataTable();

            if (ViewState["gvAllConsultant"] == null)
                dt = objSMSReport.GetSMSSummary();
            else
                dt = (DataTable)ViewState["gvAllConsultant"];


            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

            dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                lblRowsToDisplay2.Visible = true;
                ddl_NoRecords.Visible = true;
                ddl_NoRecords.SelectedIndex = 3;

                gvAllConsultant.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue);
                gvAllConsultant.DataSource = dt;
                gvAllConsultant.DataBind();
                ViewState["gvAllConsultant"] = dt;
            }
            else
            {
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords.Visible = false;
                DataTable dttemp= null;
                ViewState["gvAllConsultant"] = dttemp;
                gvAllConsultant.DataSource = null;
                gvAllConsultant.DataBind();
            }

        }
        catch (Exception ex)
        {
            logger.Error("Error in Bind Data" + ex.Message);
        }
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

    protected void gvAllConsultant_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAllConsultant.PageIndex = e.NewPageIndex;
        gvAllConsultant.DataSource = (DataTable)ViewState["gvAllConsultant"];
        gvAllConsultant.DataBind();
    }

    protected void gvAllConsultant_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = new DataTable();
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();

        DataView dv = ((DataTable)ViewState["gvAllConsultant"]).DefaultView;
        dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

        dt = dv.ToTable();
        gvAllConsultant.DataSource = dt;
        gvAllConsultant.DataBind();
        ViewState["gvAllConsultant"] = dt;
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

    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
        CheckDate();
        ddlConsultantLst.SelectedIndex = 0;
        lblRowsToDisplay2.Visible = false;
        ddl_NoRecords.Visible = false;
        DataTable dttemp = null;
        ViewState["gvAllConsultant"] = dttemp;
        gvAllConsultant.DataSource = null;
        gvAllConsultant.DataBind();
    }
}
