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

public partial class User_Controls_UCAdminConfig : System.Web.UI.UserControl
{
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCAdminConfig));

    Cls_AdminConfig objAdminConfig = null;

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Description";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                BindData();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page Load Event :" + ex.Message);
        }
    }

    protected void gvConfigValuesDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvConfigValuesDetails.EditIndex = e.NewEditIndex;
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("gvConfigValuesDetails_RowEditing Event :" + ex.Message);
        }
    }

    protected void gvConfigValuesDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt16(((HiddenField)gvConfigValuesDetails.Rows[e.RowIndex].FindControl("hdfID")).Value.ToString());
            String Value = ((TextBox)gvConfigValuesDetails.Rows[e.RowIndex].FindControl("txtEditValue")).Text.ToString();
            // String Name = ((TextBox)gvConfigValuesDetails.Rows[e.RowIndex].FindControl("txtEditName")).Text.ToString();

            objAdminConfig = new Cls_AdminConfig();
            objAdminConfig.ID = ID;
            objAdminConfig.Name = "";
            objAdminConfig.Value = Value;
            objAdminConfig.ModifiedBy = Convert.ToInt64(Session[Cls_Constants.LOGGED_IN_USERID]);

            if (objAdminConfig.CheckIfConfigValuesExists().Rows.Count == 0)
            {
                objAdminConfig.DBOperation = DbOperations.UPDATE;
                int result = objAdminConfig.UpdateConfigValue();

                gvConfigValuesDetails.EditIndex = -1;
                BindData();

                if (result == 1)
                    lblResult.Text = "Config Values Updated Successfully";
                else
                    lblResult.Text = " Config Values Updation Failed";
            }
            else
            {
                lblResult.Text = "Config Values Pair already exists.";
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvConfigValuesDetails_RowUpdating Event :" + ex.Message);
        }
    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            //String ConfigValues = ((TextBox)gvConfigValuesDetails.FooterRow.FindControl("txtConfigValues")).Text.ToString();

            objAdminConfig = new Cls_AdminConfig();
            String Name = ((TextBox)gvConfigValuesDetails.FooterRow.FindControl("txtName")).Text;
            string Value = ((TextBox)gvConfigValuesDetails.FooterRow.FindControl("txtValue")).Text;
            objAdminConfig.Value = Value;
            objAdminConfig.Name = Name;
            objAdminConfig.ModifiedBy = Convert.ToInt64(Session[Cls_Constants.LOGGED_IN_USERID]);

            if (objAdminConfig.CheckIfConfigValuesExists().Rows.Count == 0)
            {
                objAdminConfig.DBOperation = DbOperations.INSERT;
                int result = objAdminConfig.AddConfigValue();


                if (result == 1)
                    lblResult.Text = "Config Values Added Successfully";
                else
                    lblResult.Text = " Config Values Addition Failed";
                BindData();
            }
            else
            {
                lblResult.Text = "Config Values pairs" + Name + " And " + Value + " already exists.";
            }
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnAdd_Click Event :" + ex.Message);
        }
    }

    protected void gvConfigValuesDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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

                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvConfigValuesDetails_RowDataBound Event :" + ex.Message);
        }
    }

    protected void imgbtnActivate_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void gvConfigValuesDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            objAdminConfig = new Cls_AdminConfig();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvConfigValuesDetails.PageIndex * gvConfigValuesDetails.PageSize);
                int Id = Convert.ToInt16(((HiddenField)gvConfigValuesDetails.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvConfigValuesDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objAdminConfig.ID = Id;
                objAdminConfig.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objAdminConfig.IsActive = (!IsActive);

                Result = objAdminConfig.SetActivenessOfConfigValue();


                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "Config Values Activated Successfully";
                    else
                        lblResult.Text = "Config Values Deactivated Successfully";

                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the Config Values";
                    else
                        lblResult.Text = "Failed to Deactivate the Config Values";

                }

                BindData();
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvConfigValuesDetails_RowCommand Event :" + ex.Message);
        }



    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvConfigValuesDetails.EditIndex = -1;
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnCancel_Click Event :" + ex.Message);
        }

    }

    protected void gvConfigValuesDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvConfigValuesDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("gvConfigValuesDetails_PageIndexChanging Event :" + ex.Message);
        }
    }

    protected void imgActive_Click(object sender, ImageClickEventArgs e)
    {

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

    #region Functions
    private void BindData()
    {
        try
        {
            objAdminConfig = new Cls_AdminConfig();
            DataTable dtConfigValues = null;

            dtConfigValues = objAdminConfig.GetAllConfigValues();

            DataView dv = dtConfigValues.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dtConfigValues = dv.ToTable();

            if (dtConfigValues != null)
            {
                if (dtConfigValues.Rows.Count == 0)
                {
                    RemoveConstraints(dtConfigValues);
                    dtConfigValues.Rows.Add(dtConfigValues.NewRow());

                    gvConfigValuesDetails.DataSource = dtConfigValues;
                    gvConfigValuesDetails.DataBind();

                    gvConfigValuesDetails.Rows[0].Visible = false;
                }
                else
                {
                    gvConfigValuesDetails.DataSource = dtConfigValues;
                    gvConfigValuesDetails.DataBind();
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
        catch (Exception ex)
        {
            logger.Error("Remove Constraints :" + ex.Message);
        }
    }

    #endregion
}
