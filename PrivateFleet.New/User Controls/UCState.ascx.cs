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
using System.Data.Common;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;


public partial class User_Controls_UCState : System.Web.UI.UserControl
{
    Cls_State objState = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCState));

    #region Functions
    private void BindData()
    {
        try
        {
            objState = new Cls_State();
            DataTable dtState = null;

            dtState = objState.GetAllStates();
            DataView dv = dtState.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dtState = dv.ToTable();

            if (dtState != null)
            {
                if (dtState.Rows.Count == 0)
                {
                    RemoveConstraints(dtState);
                    dtState.Rows.Add(dtState.NewRow());

                    gvStateDetails.DataSource = dtState;
                    gvStateDetails.DataBind();

                    gvStateDetails.Rows[0].Visible = false;
                }
                else
                {
                    gvStateDetails.DataSource = dtState;
                    gvStateDetails.DataBind();
                }

            }
            else
            {

            }
        }
        catch (Exception ex) { logger.Error("BindData Function :" + ex.Message); }
    }
    private void RemoveConstraints(DataTable dt)
    {
        try
        {
            foreach (DataColumn Dc in dt.Columns)
            {
                Dc.ReadOnly = false;
                Dc.AllowDBNull = true;
            }
        }
        catch (Exception ex) { logger.Error("RemoveConstraints Function :" + ex.Message); }
    }
    #endregion


    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "State";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                BindData();
            }
        }
        catch (Exception ex)
        { logger.Error("Page_Load Event :" + ex.Message); }
    }


    protected void gvStateDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
             lblResult.Text = "";
             Boolean IsActive = Convert.ToBoolean(((HiddenField)gvStateDetails.Rows[e.NewEditIndex].FindControl("hdfIsActive")).Value.ToString());
        if (IsActive)
        {
            gvStateDetails.EditIndex = e.NewEditIndex;
            BindData();
        }
        else
        {
            lblResult.Text = "Deactivated States can not be updated";
        }
        }
        catch (Exception ex) { logger.Error("gvStateDetails_RowEditing Function :" + ex.Message); }
    }


    protected void gvStateDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt16(((HiddenField)gvStateDetails.Rows[e.RowIndex].FindControl("hdfID")).Value.ToString());
            String State = ((TextBox)gvStateDetails.Rows[e.RowIndex].FindControl("txtEditState")).Text.ToString();

            objState = new Cls_State();
            objState.ID = ID;
            objState.State = State;
            objState.DBOperation = DbOperations.CHECK_IF_EXIST;
            if (objState.CheckIfStateExists().Rows.Count == 0)
            {
                objState.DBOperation = DbOperations.UPDATE;
                int result = objState.UpdateState();

                gvStateDetails.EditIndex = -1;
                BindData();

                if (result == 1)
                    lblResult.Text = "State Updated Successfully";
                else
                    lblResult.Text = " State Updation Failed";
            }
            else
            {
                lblResult.Text = State + " already exists.";
            }
        }
        catch (Exception ex) { logger.Error("gvStateDetails_RowUpdating Event :" + ex.Message); }
    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            String State = ((TextBox)gvStateDetails.FooterRow.FindControl("txtState")).Text.ToString();

            objState = new Cls_State();
            objState.State = State;
            objState.DBOperation = DbOperations.CHECK_IF_EXIST;
            if (objState.CheckIfStateExists().Rows.Count == 0)
            {

                objState.DBOperation = DbOperations.INSERT;
                int result = objState.AddState();


                if (result == 1)
                    lblResult.Text = "State Added Successfully";
                else
                    lblResult.Text = " State Addition Failed";
                BindData();
            }
            else
            {
                lblResult.Text = State + " already exists.";
            }
        }
        catch (Exception ex) { logger.Error("imgbtnAdd_Click Event :" + ex.Message); }
    }

    protected void gvStateDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                logger.Error("gvStateDetails_RowDataBound Event :" + Ex.Message);
            }
        }
    }
    protected void imgbtnActivate_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void gvStateDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objState = new Cls_State();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvStateDetails.PageIndex * gvStateDetails.PageSize);
                int Id = Convert.ToInt16(((HiddenField)gvStateDetails.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvStateDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objState.ID = Id;
                objState.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objState.IsActive = (!IsActive);

                Result = objState.SetActivenessOfState();


                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "State Activated Successfully";
                    else
                        lblResult.Text = "State Deactivated Successfully";

                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the State";
                    else
                        lblResult.Text = "Failed to Deactivate the State";

                }

                BindData();
            }


        }
        catch (Exception ex) { logger.Error("gvStateDetails_RowCommand Evenr :" + ex.Message); }

    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvStateDetails.EditIndex = -1;
            BindData();
        }
        catch (Exception ex) { logger.Error("imgbtnCancel_Click Event :" + ex.Message); }
    }
    protected void gvStateDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvStateDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("gvStateDetails_PageIndexChanging Event :" + ex.Message);
        }
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
