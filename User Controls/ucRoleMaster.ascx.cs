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
using AccessControlUnit;
using log4net;
using Mechsoft.GeneralUtilities;

public partial class User_Controls_ucRoleMaster : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_ucRoleMaster));

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Role";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                BindData();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Event : " + ex.Message);
        }
    }
    #endregion

    #region "Methods"
    private void BindData()
    {
        DataTable dt = null;
        Cls_Role objRole = new Cls_Role();

        try
        {
            //Get all roles from database
            dt = objRole.Get();

            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            //if datatable is not empty
            if (dt.Rows.Count > 0)
            {
                //show footer row
                if (gvRoles.EditIndex == -1)
                    gvRoles.ShowFooter = true;

                //Bind grid data
                gvRoles.DataSource = dt;
                gvRoles.DataBind();
            }
            else
            {
                //add blank row
                foreach (DataColumn Dc in dt.Columns)
                {
                    Dc.ReadOnly = false;
                    Dc.AllowDBNull = true;
                }
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);

                //show footer row
                if (gvRoles.EditIndex == -1)
                    gvRoles.ShowFooter = true;

                //Bind grid data
                gvRoles.DataSource = dt;
                gvRoles.DataBind();

                //hide blank row
                gvRoles.Rows[0].Visible = false;
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            objRole = null;
            dt = null;
        }
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
    #endregion

    #region "Events"
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Make grid non-editable
            gvRoles.EditIndex = -1;

            //bind grid data
            BindData();

            //show footer row
            gvRoles.ShowFooter = true;
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnCancel_Click" + ex.Message);
        }
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        Cls_Role objRole = new Cls_Role();
        try
        {
            TextBox txtRole = (TextBox)gvRoles.FooterRow.FindControl("txtAddRole");
            TextBox txtDesc = (TextBox)gvRoles.FooterRow.FindControl("txtAddDesc");

            if (txtRole != null)
                objRole.Role = txtRole.Text;

            if (txtDesc != null)
                objRole.Description = txtDesc.Text;

            objRole.IsActive = 1;

            //add role
            int lastInsertId = objRole.Add();

            if (lastInsertId > 0)
                lblMsg.Text = "Role saved successfully";
            else
                lblMsg.Text = "Error Occured.. Please try again";

            //bind grid data
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnAdd_Click" + ex.Message);
        }
        finally
        {
            objRole = null;
        }
    }
    protected void gvRoles_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            //set new edit index for edit mode
            gvRoles.EditIndex = e.NewEditIndex;

            //hide footer row
            gvRoles.ShowFooter = false;

            //bind grid data
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("gvRoles_RowEditing" + ex.Message);
        }
    }
    protected void gvRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Cls_Role objRole = new Cls_Role();
        try
        {
            int intRoleId = Convert.ToInt32(gvRoles.DataKeys[e.RowIndex].Values["ID"]);
            TextBox txtRole = (TextBox)gvRoles.Rows[e.RowIndex].FindControl("txtEditRole");
            TextBox txtDesc = (TextBox)gvRoles.Rows[e.RowIndex].FindControl("txtEditDesc");
            HiddenField hdfIsActive = (HiddenField)gvRoles.Rows[e.RowIndex].FindControl("hdfIsActive");            

            //role id
            objRole.Id = intRoleId;

            //role name
            if (txtRole != null)
                objRole.Role = txtRole.Text;

            //role description
            if (txtDesc != null)
                objRole.Description = txtDesc.Text;

            //role status
            if (hdfIsActive != null)
                objRole.IsActive = Convert.ToInt32(Convert.ToBoolean(hdfIsActive.Value));

            //update role
            bool result = objRole.Update();

            if (result)
                lblMsg.Text = "Role updated successfully";
            else
                lblMsg.Text = "Error Occured.. Please try again";

            //make grid non-editable
            gvRoles.EditIndex = -1;

            //bind grid data
            BindData();

        }
        catch (Exception ex)
        {
            logger.Error("gvRoles_RowUpdating" + ex.Message);
        }
        finally
        {
            objRole = null;
        }
    }
    protected void gvRoles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Activeness")
        {
            Cls_Page objPage = new Cls_Page();
            Cls_Utilities objUtility = new Cls_Utilities();
            int RowIndex=Convert.ToInt16(e.CommandArgument.ToString());
            bool IsActive=Convert.ToBoolean(((HiddenField) gvRoles.Rows[RowIndex].FindControl("hdfIsActive")).Value);
            int ID = Convert.ToInt16(gvRoles.DataKeys[RowIndex][0].ToString());

            bool Result = false;;

            if (IsActive)
            {
                Result = objUtility.DeActivate(ID, "ACU_RoleMaster");
            }
            else
            {
                Result = objUtility.Actiavte(ID, "ACU_RoleMaster");
            }
            if (IsActive)
            {
                if (Result)
                    lblMsg.Text = "Role Deactivated successfully";
                else
                    lblMsg.Text = "Failed to Deactivate the role";
            }
            else
            {
                if (Result)
                    lblMsg.Text = "Role Activated Successfully";
                else
                    lblMsg.Text = "Failed to Activate the role";
            }

            BindData();

        }
    }
    protected void gvRoles_RowDataBound(object sender, GridViewRowEventArgs e)
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
            catch (Exception ex)
            {
                logger.Error("gvActions_RowDataBound Event : " + ex.Message);
            }
        }
    }
    protected void gvRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //set new page index
            gvRoles.PageIndex = e.NewPageIndex;

            //bind grid data
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("gvRoles_PageIndexChanging Event : " + ex.Message);
        }
    }
    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;

        //Swap sort direction
        this.DefineSortDirection();

        // BindData(objCourseMaster);
        this.BindData();
    }
    #endregion

    
}
