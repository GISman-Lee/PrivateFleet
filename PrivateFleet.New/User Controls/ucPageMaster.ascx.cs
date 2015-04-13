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

public partial class User_Controls_ucPageMaster : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_ucPageMaster));

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "PageName";
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
        Cls_Page objPage = new Cls_Page();

        try
        {
            //Get all Pages from database
            dt = objPage.Get();
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();

            //if datatable is not empty
            if (dt.Rows.Count > 0)
            {
                //show footer row
                if (gvPages.EditIndex == -1)
                    gvPages.ShowFooter = true;

                //Bind grid data
                gvPages.DataSource = dt;
                gvPages.DataBind();
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
                if (gvPages.EditIndex == -1)
                    gvPages.ShowFooter = true;

                //Bind grid data
                gvPages.DataSource = dt;
                gvPages.DataBind();

                //hide blank row
                gvPages.Rows[0].Visible = false;
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            objPage = null;
            dt = null;
        }
    }

    private void FillParentMenus(DropDownList ddlParent)
    {
        Cls_Page objPage = new Cls_Page();
        try
        {
            DataTable dtPages = objPage.GetAllActivePages();

            if (dtPages != null)
            {
                DataView dv = dtPages.DefaultView;
                dv.RowFilter = "ParentID = 0";
                dtPages = dv.ToTable();

                //clear dropdown items
                ddlParent.Items.Clear();

                //fill dropdown
                ddlParent.DataSource = dtPages;
                ddlParent.DataTextField = "PageName";
                ddlParent.DataValueField = "ID";
                ddlParent.DataBind();

                //insert default item
                ddlParent.Items.Insert(0, new ListItem("Select Parent", "0"));
            }
        }
        catch (Exception ex)
        {
            logger.Error("FillParentMenus Method : " + ex.Message);
            throw;
        }
    }
    #endregion

    #region "Events"
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Make grid non-editable
            gvPages.EditIndex = -1;

            //bind grid data
            BindData();

            //show footer row
            gvPages.ShowFooter = true;
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnCancel_Click" + ex.Message);
        }
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        Cls_Page objPage = new Cls_Page();
        try
        {
            TextBox txtPage = (TextBox)gvPages.FooterRow.FindControl("txtAddPage");
            TextBox txtUrl = (TextBox)gvPages.FooterRow.FindControl("txtAddUrl");
            DropDownList ddlAddParent = (DropDownList)gvPages.FooterRow.FindControl("ddlAddParent");
            CheckBox chkInternal = (CheckBox)gvPages.FooterRow.FindControl("chkAddInternal");

            if (txtPage != null)
                objPage.PageName = txtPage.Text;

            if (txtUrl != null)
                objPage.PageUrl = txtUrl.Text;

            if (ddlAddParent != null)
                objPage.ParentId = Convert.ToInt32(ddlAddParent.SelectedValue);

            if (chkInternal != null)
                objPage.IsInternalLink = chkInternal.Checked;

            //add Page
            int lastInsertId = objPage.Add();

            if (lastInsertId > 0)
                lblMsg.Text = "Page saved successfully";
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
            objPage = null;
        }
    }
    protected void gvPages_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            //set new edit index for edit mode
            gvPages.EditIndex = e.NewEditIndex;

            //hide footer row
            gvPages.ShowFooter = false;

            //bind grid data
            BindData();

            DropDownList ddlEditParent = (DropDownList)gvPages.Rows[e.NewEditIndex].FindControl("ddlEditParent");
            HiddenField hdfParent = (HiddenField)gvPages.Rows[e.NewEditIndex].FindControl("hdfParent");

            if (ddlEditParent != null)
            {
                FillParentMenus(ddlEditParent);
                ddlEditParent.SelectedValue = hdfParent.Value;
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvPages_RowEditing" + ex.Message);
        }
    }
    protected void gvPages_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Cls_Page objPage = new Cls_Page();
        try
        {
            int intPageId = Convert.ToInt32(gvPages.DataKeys[e.RowIndex].Values["ID"]);
            TextBox txtPage = (TextBox)gvPages.Rows[e.RowIndex].FindControl("txtEditPage");
            TextBox txtUrl = (TextBox)gvPages.Rows[e.RowIndex].FindControl("txtEditUrl");
            HiddenField hdfIsActive = (HiddenField)gvPages.Rows[e.RowIndex].FindControl("hdfIsActive");
            DropDownList ddlEditParent = (DropDownList)gvPages.Rows[e.RowIndex].FindControl("ddlEditParent");
            CheckBox chkInternal = (CheckBox)gvPages.Rows[e.RowIndex].FindControl("chkEditInternal");

            //Page id
            objPage.Id = intPageId;

            //Page name
            if (txtPage != null)
                objPage.PageName = txtPage.Text;

            //Page Urlription
            if (txtUrl != null)
                objPage.PageUrl = txtUrl.Text;

            if (ddlEditParent != null)
                objPage.ParentId = Convert.ToInt32(ddlEditParent.SelectedValue);

            if (chkInternal != null)
                objPage.IsInternalLink = chkInternal.Checked;

            //Page status
            if (hdfIsActive != null)
                objPage.IsActive = Convert.ToInt32(Convert.ToBoolean(hdfIsActive.Value));

            //update Page
            bool result = objPage.Update();

            if (result)
                lblMsg.Text = "Page updated successfully";
            else
                lblMsg.Text = "Error Occured.. Please try again";

            //make grid non-editable
            gvPages.EditIndex = -1;

            //bind grid data
            BindData();

        }
        catch (Exception ex)
        {
            logger.Error("gvPages_RowUpdating" + ex.Message);
        }
        finally
        {
            objPage = null;
        }
    }
    protected void gvPages_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Cls_Access objAccess = new Cls_Access();
        try
        {
            if (e.CommandName == "Activeness")
            {
                Cls_Utilities objUtility = new Cls_Utilities();
                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvPages.PageIndex * gvPages.PageSize);

                bool IsActive = Convert.ToBoolean(((HiddenField)gvPages.Rows[RowIndex].FindControl("hdfIsActive")).Value);
                int ID = Convert.ToInt16(gvPages.DataKeys[RowIndex][0].ToString());

                bool Result = false; ;

                if (IsActive)
                {
                    //deactivate page
                    Result = objUtility.DeActivate(ID, "ACU_PageMaster");

                    if (Result)
                    {
                        objAccess.PageId = ID;
                        Result = objAccess.DeactivatePageAccessForAll();
                    }

                }
                else
                {
                    Result = objUtility.Actiavte(ID, "ACU_PageMaster");
                }
                if (IsActive)
                {
                    if (Result)
                        lblMsg.Text = "Page Deactivated successfully";
                    else
                        lblMsg.Text = "Failed to Deactivate the Page";
                }
                else
                {
                    if (Result)
                        lblMsg.Text = "Page Activated Successfully";
                    else
                        lblMsg.Text = "Failed to Activate the Page";
                }

                BindData();
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvPages_RowCommand Event : " + ex.Message);
        }
    }
    protected void gvPages_RowDataBound(object sender, GridViewRowEventArgs e)
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

                Label lblInternal = (Label)e.Row.FindControl("lblInternal");
                if (lblInternal != null)
                {
                    if (lblInternal.Text == "True")
                        lblInternal.Text = "Yes";
                    else
                        lblInternal.Text = "No";
                }
            }
            catch (Exception ex)
            {
                logger.Error("gvActions_RowDataBound Event : " + ex.Message);
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlAddParent = (DropDownList)e.Row.FindControl("ddlAddParent");

            if (ddlAddParent != null)
                FillParentMenus(ddlAddParent);
        }

    }
    protected void gvPages_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //set new page index
            gvPages.PageIndex = e.NewPageIndex;

            //bind grid data
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("gvPages_PageIndexChanging Event : " + ex.Message);
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
