using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using log4net;
using Mechsoft.GeneralUtilities;

public partial class User_Controls_UCCustStatusUpForTrade2 : System.Web.UI.UserControl
{
    #region Private Variables
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCCustStatusUpForTrade2));
    DataTable dt = null;
    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["DeliveryReportDT"] = null;
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "ETA";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
            BindPrimaryContacts();
            if (Request.QueryString["PC"] != null && !Convert.ToString(Request.QueryString["PC"]).Equals(String.Empty))
            {
                ddlPrimaryContact.SelectedValue = Convert.ToString(Request.QueryString["PC"]);
                btnGenerateReport_Click(sender, e);
            }
        }
    }

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

    protected void grdDeliveryReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "View")
            {
                //HyperLink hypcustomerlinkview = (HyperLink)e.Row.FindControl("hypcustomerlinkview");
                //hypcustomerlinkview.NavigateUrl = "~/ClinetIfo_ForDealer.aspx?tempid=" + hypcustomerlinkview.NavigateUrl + "&dlrid=" + _id + "&eml=" + _eml + "&ReqFrm=custOS";

                GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                HiddenField hdfDealerId = (HiddenField)gvRow.FindControl("hdfDealerId");
                HiddenField hdfDealerEmail = (HiddenField)gvRow.FindControl("hdfDealerEmail");

                Response.Redirect("ClinetIfo_ForDealer.aspx?tempid=" + e.CommandArgument + "&dlrid=" + hdfDealerId.Value + "&eml=" + hdfDealerEmail.Value + "&ReqFrm=ETAComing&PC=" + ddlPrimaryContact.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            logger.Error("UC_DeliveryInNxt10days - grdDeliveryReport_RowCommand err :: " + ex.Message);
        }
        finally
        {

        }
    }

    #endregion

    #region Methods

    private void BindData()
    {
        Cls_DealerReportHelper objDealerRpt = new Cls_DealerReportHelper();
        try
        {
            DataTable dtData = new DataTable();
            string strContactName = string.Empty;
            if (ddlPrimaryContact.SelectedIndex > 0)
            {
                strContactName = ddlPrimaryContact.SelectedItem.Text;
            }

            if (ViewState["DeliveryReportDT"] == null)
            {
                dtData = objDealerRpt.GetCustStatusUpdateForTrade2(strContactName, 10);
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
}
