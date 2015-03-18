using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;

public partial class User_Controls_UC_PrimaryContact : System.Web.UI.UserControl
{
    #region Private Variables

    ILog logger = LogManager.GetLogger(typeof(User_Controls_UC_PrimaryContact));
    Cls_PrimaryContact objPrimaryContact = null;
    #endregion

    #region Events

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

    protected void grdPrimaryContactDtls_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblResult.Text = "";

            // Boolean IsActive = Convert.ToBoolean(((HiddenField)grdPrimaryContactDtls.Rows[e.NewEditIndex].FindControl("hdfIsActive")).Value.ToString());
            // if (IsActive)
            {
                grdPrimaryContactDtls.EditIndex = e.NewEditIndex;
                BindData();
            }
            //else
            //{
            //    lblResult.Text = "Deactivated Primary Contact can not be updated";
            //}
        }
        catch (Exception ex) { logger.Error("grdPrimaryContactDtls_RowEditing Function :" + ex.Message); }
    }

    protected void grdPrimaryContactDtls_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt16(((HiddenField)grdPrimaryContactDtls.Rows[e.RowIndex].FindControl("hdfID")).Value.ToString());
            String Name = ((Label)grdPrimaryContactDtls.Rows[e.RowIndex].FindControl("lblName")).Text.ToString();
            String Email = ((TextBox)grdPrimaryContactDtls.Rows[e.RowIndex].FindControl("txtEditEmail")).Text.ToString();
            //String PrimaryContact = ((TextBox)grdPrimaryContactDtls.Rows[e.RowIndex].FindControl("txtEditPrimaryContactFor")).Text.ToString();

            objPrimaryContact = new Cls_PrimaryContact();
            objPrimaryContact.Id = ID;
            objPrimaryContact.Name = Name;
            objPrimaryContact.Email = Email;
            //objPrimaryContact.PrimaryContactFor = PrimaryContact;
            objPrimaryContact.IsActive = true;
            //objPrimaryContact.DBOperation = DbOperations.CHECK_IF_EXIST;
            //if (objPrimaryContact.CheckIfStateExists().Rows.Count == 0)
            //{
            //    objPrimaryContact.DBOperation = DbOperations.UPDATE;
            bool result = objPrimaryContact.UpdatePrimaryContact();

            grdPrimaryContactDtls.EditIndex = -1;
            BindData();

            if (result)
                lblResult.Text = "Primary Contact Updated Successfully";
            else
                lblResult.Text = "Primary Contact Updation Failed";
            //  }
            //else
            //{
            //    lblResult.Text = Name + " already exists.";
            //}
        }
        catch (Exception ex)
        {
            logger.Error("grdPrimaryContactDtls_RowUpdating Event :" + ex.Message);
        }
    }

    protected void grdPrimaryContactDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Image imgBtnActive = ((Image)e.Row.FindControl("imgbtnActivate"));
                Image imgActive = ((Image)e.Row.FindControl("imgActive"));
                LinkButton lnkbtnActivate = ((LinkButton)e.Row.FindControl("lnkbtnActiveness"));
                //if (e.Row.RowState == DataControlRowState.Edit)
                //{
                //    ImageButton imgbtnUpdate = ((ImageButton)e.Row.FindControl("imgbtnUpdate"));
                //    if (imgbtnUpdate != null)
                //    {
                //        imgbtnUpdate.Attributes.Add("onClick", "javascript:return ConfirmUpdate();");
                //    }
                //}
                if (imgBtnActive != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive")))
                    {
                        imgBtnActive.ImageUrl = "~/Images/Active.png";
                        //imgActive.ImageUrl = imgActive != null ? "~/Images/active_bullate.jpg" : "";
                        //imgActive.ToolTip = "Deactivate This Record";
                        e.Row.CssClass = "gridactiverow";
                    }
                    else
                    {
                        imgBtnActive.ImageUrl = "~/Images/Inactive.ico";
                        // imgActive.ImageUrl = "~/Images/deactive_bullate.jpg";
                        e.Row.CssClass = "griddeactiverow";
                        //imgActive.ToolTip = "Activate This Record";
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
                logger.Error("grdPrimaryContactDtls_RowDataBound Event :" + Ex.Message);
            }
        }

    }

    protected void imgbtnActivate_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void grdPrimaryContactDtls_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objPrimaryContact = new Cls_PrimaryContact();

            bool Result = false;

            if (e.CommandName == "Activeness")
            {
                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (grdPrimaryContactDtls.PageIndex * grdPrimaryContactDtls.PageSize);
                int Id = Convert.ToInt16(((HiddenField)grdPrimaryContactDtls.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)grdPrimaryContactDtls.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());

                objPrimaryContact = new Cls_PrimaryContact();

                objPrimaryContact.Id = Id;
                objPrimaryContact.SetActiveness = true;

                //objPrimaryContact.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objPrimaryContact.IsActive = (!IsActive);

                Result = objPrimaryContact.UpdatePrimaryContact();
                if (Result)
                {
                    if (!IsActive)
                        lblResult.Text = "Primary Contact Activated Successfully";
                    else
                        lblResult.Text = "Primary Contact Deactivated Successfully";
                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the Primary Contact";
                    else
                        lblResult.Text = "Failed to Deactivate the Primary Contact";
                }
                BindData();
            }
        }
        catch (Exception ex) { logger.Error("grdPrimaryContactDtls_RowCommand Event :" + ex.Message); }
    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            grdPrimaryContactDtls.EditIndex = -1;
            BindData();
        }
        catch (Exception ex) { logger.Error("imgbtnCancel_Click Event :" + ex.Message); }
    }

    protected void grdPrimaryContactDtls_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdPrimaryContactDtls.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("grdPrimaryContactDtls_PageIndexChanging Event :" + ex.Message);
        }
    }

    protected void grdPrimaryContactDtls_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;


        //Swap sort direction
        this.DefineSortDirection();

        // BindData(objCourseMaster);
        this.BindData();
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

    #endregion

    #region Methods
    private void BindData()
    {
        try
        {
            objPrimaryContact = new Cls_PrimaryContact();
            DataTable dtPrimaryContact = null;

            dtPrimaryContact = objPrimaryContact.GetAllPrimaryContacts();
            DataView dv = dtPrimaryContact.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dv.RowFilter = "primaryContactFor<>'Survey'";
            dtPrimaryContact = dv.ToTable();

            if (dtPrimaryContact != null)
            {
                if (dtPrimaryContact.Rows.Count == 0)
                {
                    RemoveConstraints(dtPrimaryContact);
                    dtPrimaryContact.Rows.Add(dtPrimaryContact.NewRow());

                    grdPrimaryContactDtls.DataSource = dtPrimaryContact;
                    grdPrimaryContactDtls.DataBind();

                    grdPrimaryContactDtls.Rows[0].Visible = false;
                }
                else
                {
                    grdPrimaryContactDtls.DataSource = dtPrimaryContact;
                    grdPrimaryContactDtls.DataBind();
                }
            }
            else
            {
            }
        }
        catch (Exception ex)
        { logger.Error("BindData Function :" + ex.Message); }
        finally
        { objPrimaryContact = null; }
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
}
