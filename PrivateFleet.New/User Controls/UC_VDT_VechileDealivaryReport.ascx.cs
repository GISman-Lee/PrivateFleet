using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;
public partial class User_Controls_UC_VDT_VechileDealivaryReport : System.Web.UI.UserControl
{
    Cls_VDT_Report objCls_VDT_Report = new Cls_VDT_Report();
    Cls_Alies objAlies = new Cls_Alies();
    IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);
    ILog logge = LogManager.GetLogger(typeof(User_Controls_UC_VDT_VechileDealivaryReport));
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            this.Parent.Parent.Page.Form.DefaultButton = imgbtnSubmit.UniqueID;
            if (!Page.IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Make";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;

                ddl_NoRecords2.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
                grdVechileDelivaryReport.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
                
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;

                bindDropDown();

                if (Request.QueryString["ShowAdjustment"] != null)
                {
                    grdVechileDelivaryReport.PageSize = 5;

                    drpMake.SelectedValue = "ALL";
                    ddlSearch.SelectedValue = "1";
                    btnSubmit_Click(null, null);
                }
                else
                {
                    grdVechileDelivaryReport.PageIndex = Convert.ToInt32(ddl_NoRecords2.SelectedValue);
                    if (Request.QueryString["type"] != null)
                    {
                        drpMake.SelectedValue = Convert.ToString(Request.QueryString["make"]);
                        ddlSearch.SelectedValue = Convert.ToString(Request.QueryString["for"]);
                        btnSubmit_Click(null, null);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logge.Error(Convert.ToString(ex.Message));
        }
    }
    public void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["grdVechileDelivaryReport"] = null;
            bindData();
        }
        catch (Exception ex)
        {
            logge.Error(Convert.ToString(ex.Message));
        }

    }
    public void bindDropDown()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objAlies.GetMake();
            drpMake.DataSource = dt;
            drpMake.DataTextField = "Make";
            drpMake.DataValueField = "ID";
            drpMake.DataBind();
            drpMake.Items.Insert(0, new ListItem("-Select-", "0"));
            drpMake.Items.Insert(1, new ListItem("ALL", "ALL"));
        }
        catch (Exception ex)
        {
            logge.Error(Convert.ToString(ex.Message));
        }
    }
    public void drpMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToString(drpMake.SelectedValue) == "0")
            {
                grdVechileDelivaryReport.Visible = false;
                lblRowsToDisplay2.Visible = false;
                ddl_NoRecords2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            logge.Error(Convert.ToString(ex.Message));
        }
    }
    public void grdVechileDelivaryReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdVechileDelivaryReport.PageIndex = e.NewPageIndex;
            //bindData();
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["grdVechileDelivaryReport"];
            grdVechileDelivaryReport.DataSource = dt;
            grdVechileDelivaryReport.DataBind();
        }
        catch (Exception ex)
        {
            logge.Error(Convert.ToString(ex.Message));
        }
    }
    public void bindData()
    {
        try
        {
            DateTime fromDate = System.DateTime.Today; ;
            DateTime ToDate = System.DateTime.Today;

            if (Convert.ToString(drpMake.SelectedValue) == "ALL")
            {
                objCls_VDT_Report.makeid = 0;
            }
            else
            {
                objCls_VDT_Report.makeid = Convert.ToInt32(drpMake.SelectedValue);
            }
            objCls_VDT_Report.UserName = Convert.ToString(Session[Cls_Constants.USER_NAME]);
            switch (Convert.ToString(ddlSearch.SelectedValue))
            {
                //All Date
                case "0":
                case "6":
                    fromDate = Convert.ToDateTime("01/01/1900");
                   ToDate = System.DateTime.Now;
                    break;
                case "1":
                    //Today
                    fromDate = System.DateTime.Now;
                    ToDate = System.DateTime.Now;
                    break;
                case "2":
                    //Yesterday
                    fromDate = System.DateTime.Now.AddDays(-1);
                    ToDate = System.DateTime.Now.AddDays(-1);
                    break;
                case "3":
                    // last 7 days
                    fromDate = System.DateTime.Now.AddDays(-7);
                    ToDate = System.DateTime.Now;
                    break;
                case "4":
                    //This Month
                    fromDate = DateTime.Today.Subtract(TimeSpan.FromDays(DateTime.Today.Day));
                    ToDate = System.DateTime.Now;
                    break;
                case "5":
                    //Last Month ONLY
                    Int32 lastmonth = 0;
                    Int32 tempyear = 0;
                    lastmonth = Convert.ToInt32(DateTime.Today.AddMonths(-1).Month);
                    tempyear = Convert.ToInt32(DateTime.Today.AddMonths(-1).Year);
                    if (lastmonth == 1 || lastmonth == 3 || lastmonth == 5 || lastmonth == 7 || lastmonth == 8 || lastmonth == 10 || lastmonth == 12)
                    {
                        ToDate = Convert.ToDateTime( lastmonth +"/"+"31/"+ tempyear);
                        fromDate = Convert.ToDateTime(lastmonth + "/"+ "01/" + + tempyear);
                    }
                    else
                    {
                        if (lastmonth == 4 || lastmonth == 6 || lastmonth == 8 || lastmonth == 8)
                        {
                            ToDate = Convert.ToDateTime(lastmonth+"/"+"30/" +  tempyear);
                            fromDate = Convert.ToDateTime(lastmonth+"/"+"01/"   + tempyear);
                        }
                        else
                        {
                            if (lastmonth == 2)
                            {
                                if (tempyear % 4 == 0)
                                {
                                    ToDate = Convert.ToDateTime(lastmonth + "/" + "29/" +tempyear);
                                }
                                else
                                {
                                    ToDate = Convert.ToDateTime(lastmonth+"/"+"28/" +  tempyear);
                                }
                                fromDate = Convert.ToDateTime(lastmonth+"/"+"01/" +  tempyear);
                            }
                        }

                    }


                    break;
            }

            objCls_VDT_Report.FromDate = fromDate;
            objCls_VDT_Report.ToDate = ToDate;
            DataTable dt = new DataTable();
            if (ViewState["grdVechileDelivaryReport"] == null)
            {
                dt = objCls_VDT_Report.getVechicleDelivary_Report();
            }
            else
            {
                dt = (DataTable)ViewState["grdVechileDelivaryReport"];
            }

            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
            if (dt != null)
            {
                grdVechileDelivaryReport.Visible = true;
                grdVechileDelivaryReport.DataSource = dt;
                grdVechileDelivaryReport.DataBind();
                if (dt.Rows.Count > 0)
                {


                    lblRowsToDisplay2.Visible = true;
                    ddl_NoRecords2.Visible = true;
                    grdVechileDelivaryReport.ShowFooter = true;
                    if (grdVechileDelivaryReport.PageCount >= 1)
                    {
                        grdVechileDelivaryReport.FooterRow.Visible = false;
                    }
                    else
                    {
                        grdVechileDelivaryReport.FooterRow.Visible = false;
                    }
                }
                else
                {
                    lblRowsToDisplay2.Visible = false;
                    ddl_NoRecords2.Visible = false;
                }
                if (Request.QueryString["ShowAdjustment"] == null)
                {
                    if (dt.Rows.Count != 0)
                    {
                        grdVechileDelivaryReport.ShowFooter = true;
                        lblRowsToDisplay2.Visible = true;
                        ddl_NoRecords2.Visible = true;
                    }
                }
                else
                {
                    Label lbl = (Label)this.Parent.FindControl("lblVechileMarqueeMessage");
                    if (dt.Rows.Count > 0)
                    {
                       
                        string str = lbl.Text.Trim();
                        lbl.Text = " Total No of Vehicel Delivared " + dt.Rows.Count.ToString();
                    }
                    else
                    {
                         lbl.Text= " Total No of Vehicel Delivared 0";
                    }

                    HyperLink hyp = (HyperLink)this.Parent.FindControl("hypVehicle");
                    hyp.NavigateUrl = "~/VDT_VehicleDelivaryReportDealer.aspx?type=1&make=" + Convert.ToString(drpMake.SelectedValue) + "&for=" + Convert.ToString(ddlSearch.SelectedValue);
                    grdVechileDelivaryReport.ShowFooter = false;
                    lblRowsToDisplay2.Visible = false;
                    ddl_NoRecords2.Visible = false;
                }


            }

            ViewState["grdVechileDelivaryReport"] = dt;
        }
        catch (Exception ex)
        {
            logge.Error(ex.Message.ToString());

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
            logge.Error(Convert.ToString(ex.Message));
        }
    }

    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            //Swap sort direction
            this.DefineSortDirection();

            // BindData(objCourseMaster);
            bindData();
        }
        catch (Exception ex)
        {
            logge.Error(Convert.ToString(ex.Message));
        }
    }

    public void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            grdVechileDelivaryReport.PageIndex = 0;
            if (ddl_NoRecords2.SelectedValue.ToString() == "All")
            {
                //For view 1
                grdVechileDelivaryReport.PageSize = grdVechileDelivaryReport.PageCount * grdVechileDelivaryReport.Rows.Count;
                grdVechileDelivaryReport.DataSource = (DataTable)ViewState["grdVechileDelivaryReport"];
                grdVechileDelivaryReport.DataBind();
            }
            else
            {
                //for view 1
                grdVechileDelivaryReport.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
                grdVechileDelivaryReport.DataSource = (DataTable)ViewState["grdVechileDelivaryReport"];
                grdVechileDelivaryReport.DataBind();
            }
            grdVechileDelivaryReport.ShowFooter = false;
            grdVechileDelivaryReport.FooterRow.Visible = false;
        }
        catch (Exception ex)
        {
            logge.Error(Convert.ToString(ex.Message));
        }
    }

    public void grdVechileDelivaryReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Request.QueryString["ShowAdjustment"] != null)
                {
                    grdVechileDelivaryReport.Columns[1].Visible = false;
                }
                else
                {
                    grdVechileDelivaryReport.Columns[1].Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            logge.Error(Convert.ToString(ex.Message));
        }
    }
    public void ResetSearchCriteria()
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Make";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            lblRowsToDisplay2.Visible = false;
            ddl_NoRecords2.Visible = false;
            ViewState["grdVechileDelivaryReport"] = null;
            grdVechileDelivaryReport.Visible = false;
            if (drpMake.Items.Count > 0)
            {
                drpMake.SelectedValue = "ALL";
            }
            if (ddlSearch.Items.Count > 0)
            {
                ddlSearch.SelectedValue = "0";

            }
        }
        catch (Exception ex)
        {
            logge.Error(Convert.ToString(ex.Message));
        }
    }
}
