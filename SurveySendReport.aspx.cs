using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;

public partial class SurveySendReport : System.Web.UI.Page
{
    ILog logger = LogManager.GetLogger(typeof(SurveySendReport));

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label lblHeader = (Label)Master.FindControl("lblHeader");
            if (lblHeader != null)
            {
                lblHeader.Text = "Survey Send Report";
            }
            ViewState["SurveySendReport"] = null;

            //ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "DeliveryDate";
            
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC ;

            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            gvSurveySendR.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());

            BindData();
        }

    }
   
    public void BindData()
    {
        DataTable dt = null;
        Cls_General objSurveyReport = new Cls_General();
        try
        {

            if (ViewState["SurveySendReport"] != null)
                dt = (DataTable)ViewState["SurveySendReport"];
            else
            {
                dt = objSurveyReport.getSurveySendReport();
             //  ViewState["SurveySendReport"] = dt;
            }

            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
          
            ViewState["SurveySendReport"] = dt;

            if (dt != null && dt.Rows.Count > 0)
            {
                lblRowsToDisplay.Visible = true;
                ddl_NoRecords.Visible = true;
                gvSurveySendR.DataSource = dt;
                gvSurveySendR.DataBind();
            }
            else
            {
                gvSurveySendR.DataSource = null ;
                gvSurveySendR.DataBind();
            }


        }
        catch (Exception ex)
        {
            logger.Error("Generate survey report error - " + ex.Message);
        }
        finally
        {
            objSurveyReport = null;

        }
    }

    protected void gvSurveySendR_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvSurveySendR.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            logger.Debug("Gv Send report page index changing error - " + ex.Message);
        }
    }
   
    protected void gvSurveySendR_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
            //Swap sort direction
            this.DefineSortDirection();

            BindData();
        }
        catch
        {
            throw;
        }
    }

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_NoRecords.SelectedValue.ToString() == "All")
            {

                gvSurveySendR.DataSource = (DataTable)ViewState["SurveySendReport"];
                gvSurveySendR.PageSize = gvSurveySendR.PageCount * gvSurveySendR.Rows.Count;
                gvSurveySendR.DataBind();
            }
            else
            {
                gvSurveySendR.DataSource = (DataTable)ViewState["SurveySendReport"];
                gvSurveySendR.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                gvSurveySendR.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {

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
