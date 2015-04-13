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
using AccessControlUnit;



public partial class User_Controls_UCUserMasterView : System.Web.UI.UserControl
{

    Cls_User objUser = null;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.Text = "";
        if (!IsPostBack)
        {
            ddl_NoRecords.SelectedValue = "20";
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            gvUserDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            BindData();

        }
        
     
    }

    private void BindData()
    {
        objUser = new Cls_User();
        DataTable dtUsers = objUser.Get();


        DataView dv = dtUsers.DefaultView;
        dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
        dtUsers = dv.ToTable();

        ViewState["dtUsers"] = dtUsers;
        gvUserDetails.DataSource = dtUsers;
        gvUserDetails.DataBind();

    }
    protected void gvUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblResult.Text = "";
        if (e.CommandName == "Edit")
        {


            int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
            RowIndex = RowIndex - (gvUserDetails.PageIndex * gvUserDetails.PageSize);
            Boolean IsActive = Convert.ToBoolean(((HiddenField)gvUserDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
            //Boolean IsActive = Convert.ToBoolean(Convert.ToByte(e.CommandArgument.ToString()));
            if (IsActive)
            {
                //RowIndex = RowIndex - (gvUserDetails.PageIndex * gvUserDetails.PageSize);
                UCUserMasterCRUD1.ID = gvUserDetails.DataKeys[RowIndex][0].ToString();
                UCUserMasterCRUD1.SetHiddenFields();
            }
            else
            {
                lblResult.Text = "Deactivated user can not be updated";
            }
        }
        if (e.CommandName == "SeePassword" || e.CommandName=="HidePassword")
        {
            int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
            RowIndex = RowIndex - (gvUserDetails.PageIndex * gvUserDetails.PageSize);

            if (e.CommandName == "SeePassword")
            {
                            
                gvUserDetails.Rows[RowIndex].FindControl("lblPassword").Visible = true;
                gvUserDetails.Rows[RowIndex].FindControl("lblPassword1").Visible = false;

                ImageButton imgbut=(ImageButton) gvUserDetails.Rows[RowIndex].FindControl("imgbtnSeePass");
                imgbut.CommandName = "HidePassword";
                imgbut.ToolTip = "Click to Hide Password";
            }
            if (e.CommandName == "HidePassword")
            {
                gvUserDetails.Rows[RowIndex].FindControl("lblPassword").Visible = false;
                gvUserDetails.Rows[RowIndex].FindControl("lblPassword1").Visible = true;
                ImageButton imgbut = (ImageButton)gvUserDetails.Rows[RowIndex].FindControl("imgbtnSeePass");
                imgbut.CommandName = "SeePassword";
                imgbut.ToolTip = "Click To See Password";
                
            }
                   
        }

        if (e.CommandName == "Activeness")
        {

            Cls_Utilities objUtility = new Cls_Utilities();
            int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
            RowIndex = RowIndex - (gvUserDetails.PageIndex * gvUserDetails.PageSize);
            bool IsActive = Convert.ToBoolean(((HiddenField)gvUserDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value);
            int ID = Convert.ToInt16(gvUserDetails.DataKeys[RowIndex][0].ToString());
            if (ID != 1)
            {
                bool Result = false;

                if (IsActive)
                {
                    Result = objUtility.DeActivate(ID, "ACU_UserMaster");
                }
                else
                {
                    Result = objUtility.Actiavte(ID, "ACU_UserMaster");
                }
                if (IsActive)
                {
                    if (Result)
                        lblResult.Text = "User Deactivated successfully";
                    else
                        lblResult.Text = "Failed to Deactivate the User";
                }
                else
                {
                    if (Result)
                        lblResult.Text = "User Activated Successfully";
                    else
                        lblResult.Text = "Failed to Activate the User";
                }

                BindData();
            }
            else
            {
                lblResult.Text = "You can not deactivate Admin User.";
            }

        }

    }
    protected void gvUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
            }
        }
    }
    protected void gvUserDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvUserDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void gvUserDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUserDetails.PageIndex = e.NewPageIndex;
        UCUserMasterCRUD1.searchDealer("PageChange");
       
        //BindData();

    }
    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl=(DropDownList)UCUserMasterCRUD1.FindControl("ddlRoles");
        //if (ddl.SelectedValue.ToString() == "-Select-")
        //{
        //    if (ddl_NoRecords.SelectedValue.ToString() == "All")
        //    {
                
        //        gvUserDetails.DataSource = (DataTable)ViewState["dtUsers"];
        //        gvUserDetails.PageSize = gvUserDetails.PageCount * gvUserDetails.Rows.Count;
        //        gvUserDetails.DataBind();
        //        //BindData();
        //    }
        //    else
        //    {
        //        gvUserDetails.DataSource = (DataTable)ViewState["dtUsers"];
        //        gvUserDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
        //        gvUserDetails.DataBind();
        //       // BindData();
        //    }
        //}
        //else
       // {
            if (ddl_NoRecords.SelectedValue.ToString() == "All")
            {
                gvUserDetails.PageSize = gvUserDetails.PageCount * gvUserDetails.Rows.Count;
                UCUserMasterCRUD1.searchDealer("search");
            }
            else
            {
                gvUserDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
                UCUserMasterCRUD1.searchDealer("search");
            }
        //}

    }



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
