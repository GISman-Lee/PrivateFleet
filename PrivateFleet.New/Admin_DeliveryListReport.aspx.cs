using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;

public partial class Admin_DeliveryListReport : System.Web.UI.Page
{
    #region Variables

    ILog logger = LogManager.GetLogger(typeof(Admin_DeliveryListReport));
    DataTable dt = null;

    #endregion

    #region Page load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["DeliveryReportDT"] = null;
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "ETA";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
            BindPrimaryContacts();
            CheckDate();
        }
    }

    #endregion

    #region Methods

    private void CheckDate()
    {
        txtCalenderFrom.Text = Convert.ToDateTime(DateTime.Today.Subtract(TimeSpan.FromDays(Cls_Constants.DEFAULT_DATE_DIFFERENCE_IN_DAYS_FOR_REPORTS)).ToShortDateString()).ToString("dd/MM/yyyy");
        TxtToDate.Text = Convert.ToDateTime(DateTime.Today.ToShortDateString()).ToString("dd/MM/yyyy");
    }

    private void BindData()
    {
        Cls_DealerReportHelper objDealerRpt = new Cls_DealerReportHelper();
        try
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("en-au", false);

            DataTable dtData = new DataTable();
            string strContactName = string.Empty;
            if (ddlPrimaryContact.SelectedIndex > 0)
            {
                strContactName = ddlPrimaryContact.SelectedItem.Text;
            }

            objDealerRpt.strContactName = strContactName;
            objDealerRpt.FromDate = DateTime.Parse(txtCalenderFrom.Text.Trim(), culture);
            //Convert.ToDateTime(txtCalenderFrom.Text.ToString());
            TimeSpan ts = new TimeSpan(1, 0, 0, 0);
            DateTime date1 = DateTime.Parse(TxtToDate.Text.Trim(), culture);
            //Convert.ToDateTime(TxtToDate.Text.ToString());
            objDealerRpt.ToDate = date1.Add(ts);
            if (ViewState["DeliveryReportDT"] == null)
            {
                dtData = objDealerRpt.GetDeliveredListReport();
            }
            else
            {
                dtData = (DataTable)ViewState["DeliveryReportDT"];
            }

            if (dtData != null && dtData.Rows.Count > 0)
            {
                DataView dv = new DataView(dtData);
                dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());

                ViewState["DeliveryReportDT"] = dv.ToTable();
                grdDeliveryReport.DataSource = dv.ToTable();
                grdDeliveryReport.DataBind();
            }
            else
            {
                grdDeliveryReport.DataSource = null;
                grdDeliveryReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error("BindData_Error" + Convert.ToString(ex.Message));
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
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    private void BindPrimaryContacts()
    {
        try
        {
            Cls_PrimaryContact objPrimaryContact = new Cls_PrimaryContact();
            DataTable dtData = new DataTable();
            dtData = objPrimaryContact.GetAllPrimaryContacts();
            DataView dv = dtData.DefaultView;
            dv.RowFilter = "primaryContactFor<>'Survey' AND IsActive=1";
            dtData = dv.ToTable();
            if (dtData != null && dtData.Rows.Count > 0)
            {
                ddlPrimaryContact.DataTextField = "Name";
                ddlPrimaryContact.DataValueField = "Id";
                ddlPrimaryContact.DataSource = dtData;
                ddlPrimaryContact.DataBind();
            }
            ddlPrimaryContact.Items.Insert(0, new ListItem("--All--"));
        }
        catch (Exception ex)
        {
            logger.Error("BindPrimaryContacts_Error" + Convert.ToString(ex.Message));
        }
    }

    #endregion

    #region Events

    public void ddl_NoRecords2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grdDeliveryReport.PageIndex = 0;
            if (ddl_NoRecords2.SelectedValue.ToString() == "All")
            {
                //For view 1
                grdDeliveryReport.PageSize = grdDeliveryReport.PageCount * grdDeliveryReport.Rows.Count;
                grdDeliveryReport.DataSource = (DataTable)ViewState["DeliveryReportDT"];
                grdDeliveryReport.DataBind();
            }
            else
            {
                //for view 1
                grdDeliveryReport.PageSize = Convert.ToInt32(ddl_NoRecords2.SelectedValue.ToString());
                grdDeliveryReport.DataSource = (DataTable)ViewState["DeliveryReportDT"];
                grdDeliveryReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            trPaging.Visible = true;
            ViewState["DeliveryReportDT"] = null;
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("btnGenerateReport_Click_Error" + Convert.ToString(ex.Message));
        }
    }

    public void grdDeliveryReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDeliveryReport.PageIndex = e.NewPageIndex;
            dt = new DataTable();
            dt = (DataTable)ViewState["DeliveryReportDT"];
            grdDeliveryReport.DataSource = dt;
            grdDeliveryReport.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }

    protected void grdDeliveryReport_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            //Swap sort direction
            this.DefineSortDirection();

            // BindData(objCourseMaster);
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }


    #endregion

}
