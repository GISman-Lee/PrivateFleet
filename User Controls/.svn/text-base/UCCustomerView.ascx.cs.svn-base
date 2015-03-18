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
using Mechsoft.GeneralUtilities;
using log4net;

public partial class User_Controls_UCCustomerView : System.Web.UI.UserControl
{

    Cls_CustomerMaster objCustomer = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCCustomerView));

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                BindData();
            }
        }
        catch (Exception ex)
        { logger.Error("Page_Load Event :" + ex.Message); }
    }
    protected void gvCustomerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCustomerDetails.PageIndex = e.NewPageIndex;
        BindData();

    }
    protected void gvCustomerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Edit")
            {
                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());

                RowIndex = RowIndex - (gvCustomerDetails.PageSize * gvCustomerDetails.PageIndex);
                UCCustomerCRUD1.CustomerID = Convert.ToInt16(gvCustomerDetails.DataKeys[RowIndex]["ID"].ToString());
                UCCustomerCRUD1.DBOperation = DbOperations.UPDATE;
                UCCustomerCRUD1.SetHiddenFileds();
            }


            objCustomer = new Cls_CustomerMaster();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());

                RowIndex = RowIndex - (gvCustomerDetails.PageSize * gvCustomerDetails.PageIndex);
                int Id = Convert.ToInt16(((HiddenField)gvCustomerDetails.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvCustomerDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objCustomer.ID = Id;
                objCustomer.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objCustomer.IsActive = (!IsActive);

                Result = objCustomer.SetActivenessOfCustomer();


                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "Customer Activated Successfully";
                    else
                        lblResult.Text = "Customer Deactivated Successfully";

                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the Customer";
                    else
                        lblResult.Text = "Failed to Deactivate the Customer";

                }

                BindData();
            }
        }
        catch (Exception ex)
        { logger.Error("gvCustomerDetails_RowCommand Event :" + ex.Message); }
    }
    protected void gvCustomerDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvCustomerDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    #region Functions

    private void BindData()
    {
        try
        {
            objCustomer = new Cls_CustomerMaster();
            DataTable dtCustomers = objCustomer.GetAllCustomers();

            DataView dv = dtCustomers.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dtCustomers = dv.ToTable();

            gvCustomerDetails.DataSource = dtCustomers;
            gvCustomerDetails.DataBind();
        }
        catch (Exception ex) { logger.Error("BindCustomers Function :" + ex.Message); }
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
    protected void gvCustomerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                logger.Error("gvCustomerDetails_RowDataBound Event :" + Ex.Message);
            }
        }
    }
}
