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


public partial class User_Controls_UCAccessories : System.Web.UI.UserControl
{
    #region Variable declaration
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCAccessories));
    Cls_Accessories objAccessories = null;
    #endregion

    #region Page load
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
            pnlAccMaster.DefaultButton = "imgbtnSearch";
        }
        catch (Exception ex)
        {
            logger.Error("Page Load Event :" + ex.Message);
        }
    }
    #endregion

    #region gvAccessoryDetails RowCommand event
    protected void gvAccessoryDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objAccessories = new Cls_Accessories();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {
                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvAccessoryDetails.PageSize * gvAccessoryDetails.PageIndex);
                int Id = Convert.ToInt16(((HiddenField)gvAccessoryDetails.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvAccessoryDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objAccessories.ID = Id;
                objAccessories.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objAccessories.IsActive = (!IsActive);

                Result = objAccessories.SetActivenessOfAccessories();


                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "Accessory Activated Successfully";
                    else
                        lblResult.Text = "Accessory Deactivated Successfully";

                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the Accessory";
                    else
                        lblResult.Text = "Failed to Deactivate the Accessory";

                }

                BindData();
            }
            else if (e.CommandName == "MasterFlagUpdate")
            {
                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvAccessoryDetails.PageSize * gvAccessoryDetails.PageIndex);
                int Id = Convert.ToInt16(((HiddenField)gvAccessoryDetails.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsMaster = Convert.ToBoolean(((HiddenField)gvAccessoryDetails.Rows[RowIndex].FindControl("hdfIsMaster")).Value.ToString());
                objAccessories.ID = Id;
                objAccessories.DBOperation = DbOperations.CHANGE_MASTER;
                objAccessories.IsMaster = (!IsMaster);

                Result = objAccessories.SetMasterFlag();

                if (Result == 1)
                {
                    if (!IsMaster)
                        lblResult.Text = "Accessory Marked as Master";
                    else
                        lblResult.Text = "Accessory Un-Marked as Master";

                }
                else
                {
                    if (!IsMaster)
                        lblResult.Text = "Failed to Mark as Master";
                    else
                        lblResult.Text = "Failed to UnMark as Master";

                }

                BindData();
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvAccessoryDetails_RowCommand Event :" + ex.Message);
        }
    }
    #endregion

    #region gvAccessoryDetails Rowdata Bound Event
    protected void gvAccessoryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image imgBtnActive = ((Image)e.Row.FindControl("imgbtnActivate"));
                Image imgActive = ((Image)e.Row.FindControl("imgActive"));
                LinkButton lnkbtnActivate = ((LinkButton)e.Row.FindControl("lnkbtnActiveness"));
                ImageButton imgbtnEdit = ((ImageButton)e.Row.FindControl("imgbtnEdit"));
                if (imgBtnActive != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive")))
                    {
                        //imgbtnEdit.Enabled = true;
                        imgBtnActive.ImageUrl = "~/Images/Active.png";
                        imgActive.ImageUrl = "~/Images/active_bullate.jpg";
                        imgActive.ToolTip = "Deactivate This Record";
                        e.Row.CssClass = "gridactiverow";


                    }
                    else
                    {
                        //imgbtnEdit.Enabled = false;
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

                LinkButton lnkBtnMakeMaster = ((LinkButton)e.Row.FindControl("lnkBtnMarkMaster"));
                if (lnkBtnMakeMaster != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsMaster")))
                        lnkBtnMakeMaster.Text = "Unmark Master";
                    else
                        lnkBtnMakeMaster.Text = "Mark Master";
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error("gvAccessoryDetails_RowDataBound Event :" + ex.Message);
        }
    }
    #endregion

    #region gvAccessoryDetails Row editing event
    protected void gvAccessoryDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblResult.Text = "";
            Boolean IsActive = Convert.ToBoolean(((HiddenField)gvAccessoryDetails.Rows[e.NewEditIndex].FindControl("hdfIsActive")).Value.ToString());

            if (IsActive)
            {
                gvAccessoryDetails.EditIndex = e.NewEditIndex;
                BindData();
            }
            else
            {
                lblResult.Text = "Deactivated Accessory can not be updated.";
            }

        }
        catch (Exception ex)
        {
            logger.Error("gvAccessoryDetails_RowEditing Event :" + ex.Message);
        }
    }
    #endregion

    #region gvAccessoryDetails row updating event
    protected void gvAccessoryDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        try
        {
            int ID = Convert.ToInt16(((HiddenField)gvAccessoryDetails.Rows[e.RowIndex].FindControl("hdfID")).Value.ToString());
            String Accessory = ((TextBox)gvAccessoryDetails.Rows[e.RowIndex].FindControl("txtEditAccessories")).Text.ToString();
            Boolean IsActive = Convert.ToBoolean(((HiddenField)gvAccessoryDetails.Rows[e.RowIndex].FindControl("hdfIsActive")).Value.ToString());
            if (!IsActive)
            {
                objAccessories = new Cls_Accessories();
                objAccessories.ID = ID;
                objAccessories.Name = Accessory;
                if (objAccessories.CheckIfAccessoryExists().Rows.Count == 0)
                {
                    objAccessories.DBOperation = DbOperations.UPDATE;
                    int result = objAccessories.UpdateAccessories();

                    gvAccessoryDetails.EditIndex = -1;
                    BindData();

                    if (result == 1)
                        lblResult.Text = "Accessory Updated Successfully";
                    else
                        lblResult.Text = " Accessory Updation Failed";
                }
                else
                {
                    lblResult.Text = "Accessory " + Accessory + " already exists.";
                }
            }
            else
            {
                lblResult.Text = "Deactivated Accessory can not updated.";
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvAccessoryDetails_RowUpdating Event :" + ex.Message);
        }

    }
    #endregion

    #region Save button event
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate("VGAdd");
        if (Page.IsValid)
        {
            try
            {
                String Accessory = ((TextBox)gvAccessoryDetails.FooterRow.FindControl("txtAccessories")).Text.ToString();
                CheckBox isMaster = ((CheckBox)gvAccessoryDetails.FooterRow.FindControl("chkIsMaster"));

                objAccessories = new Cls_Accessories();
                objAccessories.Name = Accessory;
                objAccessories.IsMaster = (isMaster.Checked) ? true : false;

                if (objAccessories.CheckIfAccessoryExists().Rows.Count == 0)
                {
                    objAccessories.DBOperation = DbOperations.INSERT;
                    int result = objAccessories.AddAccessories();

                    if (result == 1)
                        lblResult.Text = "Accessory Added Successfully";
                    else
                        lblResult.Text = " Accessory Addition Failed";
                    BindData();
                }
                else
                {
                    lblResult.Text = "Accessory " + Accessory + " already exists.";
                }
            }
            catch (Exception ex)
            {
                logger.Error("imgbtnAdd_Click Event :" + ex.Message);
            }
        }

    }
    #endregion

    #region updatetion cancel button click event
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvAccessoryDetails.EditIndex = -1;
            ddlAccType.SelectedValue = "0";
            BindData();
        }

        catch (Exception ex)
        {
            logger.Error("imgbtnCancel_Click Event :" + ex.Message);
        }
    }
    #endregion

    #region gvAccessoryDetails page index changed event
    protected void gvAccessoryDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAccessoryDetails.PageIndex = e.NewPageIndex;
            BindData();
        }

        catch (Exception ex)
        {
            logger.Error("gvAccessoryDetails_PageIndexChanging Event :" + ex.Message);
        }

    }
    #endregion

    #region Bind Data
    private void BindData()
    {
        try
        {
            DataTable dtAccessory = null;

            objAccessories = new Cls_Accessories();
            dtAccessory = objAccessories.GetAllAccessories();
            BindGridView(dtAccessory);
        }

        catch (Exception ex)
        {
            logger.Error("BindData Event :" + ex.Message);
        }

    }
    #endregion

    #region
    private void BindGridView(DataTable dtAccessory)
    {
        try
        {
            if (dtAccessory != null)
            {
                DataView dv = dtAccessory.DefaultView;
                dv.RowFilter = "IsParameter = false";
                if (ddlAccType.SelectedValue == "0")
                    dv.RowFilter += " AND IsMaster = true";
                dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
                dtAccessory = dv.ToTable();

                if (dtAccessory.Rows.Count == 0)
                {
                    RemoveConstraints(dtAccessory);
                    dtAccessory.Rows.Add(dtAccessory.NewRow());

                    gvAccessoryDetails.DataSource = dtAccessory;
                    gvAccessoryDetails.DataBind();

                    gvAccessoryDetails.Rows[0].Visible = false;
                }
                else
                {
                    gvAccessoryDetails.DataSource = dtAccessory;
                    gvAccessoryDetails.DataBind();
                }

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            logger.Error("BindData Event :" + ex.Message);
        }
    }
    #endregion

    #region Remove constraints
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
        catch (Exception Ex)
        {

            logger.Error("Remove Constraints :" + Ex.Message);
        }
    }
    #endregion

    #region page sorting event
    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;


        //Swap sort direction
        this.DefineSortDirection();

        // BindData(objCourseMaster);
        this.BindData();
    }
    #endregion

    #region Define sort direction
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

    #region Search cancel button click
    protected void imgbtnSearchCancel_Click(object sender, ImageClickEventArgs e)
    {
        txtSearchAcc.Text = "";
        ddlAccType.SelectedValue = "0";
        BindData();
    }
    #endregion

    #region Search button click
    protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        lblResult.Text = "";
        objAccessories = new Cls_Accessories();
        DataTable dtAccessory = null;
        objAccessories.Name = txtSearchAcc.Text;
        dtAccessory = objAccessories.SearchAccessory();
        BindGridView(dtAccessory);
    }
    #endregion
}
