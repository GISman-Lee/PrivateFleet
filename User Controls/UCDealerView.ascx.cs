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
using Mechsoft.FleetDeal;
using Mechsoft.GeneralUtilities;
using log4net;

public partial class User_Controls_UCDealerView : System.Web.UI.UserControl
{
    Cls_Dealer objDealer = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCDealerView));
    

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
                ctrlAddDealer.lblFlag = true;
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                gvDealerDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                BindData();
            }
        }
        catch (Exception ex)
        { logger.Error("Page_Load Event :" + ex.Message); }

    }
    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            if (ddl_NoRecords.SelectedValue.ToString() == "All")
            {
                gvDealerDetails.PageSize = gvDealerDetails.PageCount * gvDealerDetails.Rows.Count;
                ctrlAddDealer.SearchDealer();
            }
            else
            {
                gvDealerDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                ctrlAddDealer.SearchDealer();
            }
           
       
    }
    protected void gvMakeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvDealerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit")
            {
                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());

                RowIndex = RowIndex - (gvDealerDetails.PageSize * gvDealerDetails.PageIndex);
                ctrlAddDealer.DealerID = Convert.ToInt16(gvDealerDetails.DataKeys[RowIndex]["ID"].ToString());
                ctrlAddDealer.DBOperation = DbOperations.UPDATE;
                ctrlAddDealer.SetHiddenFileds();
                
            }


            objDealer = new Cls_Dealer();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());

                RowIndex = RowIndex - (gvDealerDetails.PageSize * gvDealerDetails.PageIndex);
                int Id = Convert.ToInt16(((HiddenField)gvDealerDetails.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvDealerDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objDealer.ID = Id;
                objDealer.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objDealer.IsActive = (!IsActive);

                Result = objDealer.SetActivenessOfDealer();


                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "Dealer Activated Successfully";
                    else
                        lblResult.Text = "Dealer Deactivated Successfully";

                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the Dealer";
                    else
                        lblResult.Text = "Failed to Deactivate the Dealer";

                }


                ctrlAddDealer.SearchDealer();

              // BindData();
            }
        }
        catch (Exception ex)
        { logger.Error("gvDealerDetails_RowCommand Event :" + ex.Message); }
    }

    protected void gvDealerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Image imgBtnActive = ((Image)e.Row.FindControl("imgbtnActivate"));
                Image imgActive = ((Image)e.Row.FindControl("imgActive"));
                LinkButton lnkbtnActivate = ((LinkButton)e.Row.FindControl("lnkbtnActiveness"));
                if (imgBtnActive != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive")))
                    {


                        imgBtnActive.ImageUrl = "~/Images/Active.png";
                        imgActive.ImageUrl = "~/Images/active_bullate.jpg";
                        imgActive.ToolTip = "Deactivate This Record";

                         e.Row.CssClass = "gridactiverow";


                    }
                    else
                    {

                        imgBtnActive.ImageUrl = "~/Images/Inactive.ico";
                        imgActive.ImageUrl = "~/Images/deactive_bullate.jpg";
                        e.Row.CssClass = "griddeactiverow";
                        imgActive.ToolTip = "Activate This Record";

                    }
                }
                if (lnkbtnActivate != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive")))
                    {

                        lnkbtnActivate.Text = "Deactivate";
                    }
                    else
                    {
                        lnkbtnActivate.Text = "Activate";
                    }
                }
            }
            catch (Exception Ex)
            {
                logger.Error("gvDealerDetails_RowDataBound Event :" + Ex.Message);
            }
        }
    }
    protected void gvDealerDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvDealerDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void hdfBindData_ValueChanged(object sender, EventArgs e)
    {
        try
        {
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("hdfBindData_ValueChanged Function :" + ex.Message);
        }
    }
    protected void gvDealerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvDealerDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        { logger.Error("gvDealerDetails_PageIndexChanging Event :" + ex.Message); }
    }
    #endregion


    #region Functions
    private void BindData()
    {
        try
        {
            DataTable dtDealers=new DataTable();
            
            
                objDealer = new Cls_Dealer();
                //dtDealers = objDealer.GetAllDealers();

                dtDealers = ctrlAddDealer.SearchDealer();
           

            DataView dv = dtDealers.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dtDealers = dv.ToTable();

            gvDealerDetails.DataSource = dtDealers;
            gvDealerDetails.DataBind();
        }
        catch (Exception ex)
        { logger.Error("BindData Function :" + ex.Message); }

    }
    #endregion
    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;


        //Swap sort direction
        this.DefineSortDirection();

        // BindData(objCourseMaster);
        this.BindData();
    }


    /// <summary>
    /// Define sort direction for grid.
    /// </summary>
    /// <param name="objAlias"></param>
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
