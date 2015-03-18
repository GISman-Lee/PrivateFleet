using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mechsoft.FleetDeal;
using Mechsoft.GeneralUtilities;
using log4net;

public partial class ModelAccessory : System.Web.UI.Page
{
    private DataTable _dtModelAccessory = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((Label)Master.FindControl("lblHeader")).Text = "Model-Accessories Mapping";

            PrepareTable();
            FillMake();

            if (Request.QueryString.Count > 0)
            {
                try
                {
                    int MakeId = Convert.ToInt32(Request.QueryString["make"]);
                    int ModelId = Convert.ToInt32(Request.QueryString["model"]);

                    ddlMake.SelectedIndex = -1;
                    ddlMake.Items.FindByValue(MakeId.ToString()).Selected = true;

                    ddlMake_SelectedIndexChanged(null, null);

                    if (ddlModel.Items.Count > 0)
                    {
                        ddlModel.SelectedIndex = -1;
                        ddlModel.Items.FindByValue(ModelId.ToString()).Selected = true;
                        ddlModel_SelectedIndexChanged(null, null);
                    }

                }
                catch { return; }
            }
        }
    }

    private void PrepareTable()
    {
        if (_dtModelAccessory == null)
            _dtModelAccessory = new DataTable();

        _dtModelAccessory.Columns.Add(new DataColumn("ID"));
        _dtModelAccessory.Columns.Add(new DataColumn("AccessoryId"));
        _dtModelAccessory.Columns.Add(new DataColumn("Accessory"));
        _dtModelAccessory.Columns.Add(new DataColumn("Specification"));
        _dtModelAccessory.Columns.Add(new DataColumn("IsActive"));
    }

    private void FillMake()
    {
        try
        {
            Cls_MakeHelper objMake = new Cls_MakeHelper();
            DataTable dtmakes = null;

            dtmakes = objMake.GetAllMakes();

            if (dtmakes.Rows.Count > 0)
            {
                ddlMake.DataTextField = "Make";
                ddlMake.DataValueField = "ID";

                ddlMake.DataSource = dtmakes;
                ddlMake.DataBind();

                ddlMake.Items.Insert(0, new ListItem("-SELECT-", "0"));
            }
        }
        catch { }
    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Cls_Accessories objAccessory = new Cls_Accessories();
            Cls_ModelHelper objModel = new Cls_ModelHelper();

            //Add new Accessory ...
            objAccessory.Name = ((TextBox)gvModelAccessories.FooterRow.FindControl("txtAccessory")).Text.ToString();
            objAccessory.IsActive = true;
            objAccessory.IsMaster = false;
            objAccessory.IsParameter = false;
            objAccessory.DBOperation = DbOperations.INSERT;
            objAccessory.ReturnAccessoryId = true;
            int accessoryId = objAccessory.AddAccessoriesGetId();

            //Add Model - Accessory Mapping...
            objModel.ModelId = Convert.ToInt32(ddlModel.SelectedValue);
            objModel.IsActive = true;
            objModel.AccessoryId = accessoryId;
            objModel.DBOperation = DbOperations.INSERT_ACCESSORY;
            objModel.Specification = ((TextBox)gvModelAccessories.FooterRow.FindControl("txtAddDesc")).Text.ToString();

            int result = objModel.AddModelAccessory();

            if (result == 1)
                lblResult.Text = "Accessory for Model Added Successfully";
            else
                lblResult.Text = "Unable to map Accessory to Selected Model";

            BindAccessories();
        }
        catch
        {
        }
    }

    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillModels();

        ClearGrid();
    }

    private void ClearGrid()
    {
        if (_dtModelAccessory != null)
        {
            _dtModelAccessory.Rows.Clear();
            gvModelAccessories.DataSource = _dtModelAccessory;
            gvModelAccessories.DataBind();
        }
    }

    private void FillModels()
    {
        if (ddlMake.SelectedIndex > 0)
        {
            try
            {
                Cls_ModelHelper objModel = new Cls_ModelHelper();
                objModel.MakeID = Convert.ToInt32(ddlMake.SelectedValue);
                objModel.Model = "";

                DataTable dtModel = objModel.SearchModels();

                if (dtModel != null)
                    if (dtModel.Rows.Count > 0)
                    {
                        ddlModel.Items.Clear();

                        ddlModel.DataTextField = "Model";
                        ddlModel.DataValueField = "ID";

                        ddlModel.DataSource = dtModel;
                        ddlModel.DataBind();

                        ddlModel.Items.Insert(0, new ListItem("-SELECT-", "0"));
                    }
            }
            catch { }
        }
    }

    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Accessory";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;

            BindAccessories();
        }
        catch { }
    }

    private void BindAccessories()
    {
        Cls_ModelHelper objModel = new Cls_ModelHelper();

        objModel.ModelId = Convert.ToInt32(ddlModel.SelectedValue);

        DataTable dtModelAccessories = objModel.GetModelAccessory();

        if (dtModelAccessories != null)
        {
            if (dtModelAccessories.Rows.Count == 0)
            {
                RemoveConstraints(dtModelAccessories);
                dtModelAccessories.Rows.Add(dtModelAccessories.NewRow());

                gvModelAccessories.DataSource = dtModelAccessories;
                gvModelAccessories.DataBind();

                gvModelAccessories.Rows[0].Visible = false;
            }
            else
            {
                DataView dv = dtModelAccessories.DefaultView;
                dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
                dtModelAccessories = dv.ToTable();

                gvModelAccessories.DataSource = dtModelAccessories;
                gvModelAccessories.DataBind();
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
        catch
        {
        }
    }

    protected void gvModelAccessories_RowDataBound(object sender, GridViewRowEventArgs e)
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
                        lnkbtnActivate.Text = "Deactivate";
                    else
                        lnkbtnActivate.Text = "Activate";
                }
            }
            catch
            {

            }
        }
    }

    protected void gvModelAccessories_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            Cls_ModelHelper objModel = new Cls_ModelHelper();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {
                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvModelAccessories.PageSize * gvModelAccessories.PageIndex);
                int Id = Convert.ToInt16(((HiddenField)gvModelAccessories.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvModelAccessories.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objModel.ModelAccessoryId = Id;
                objModel.DBOperation = DbOperations.CHANGE_ACTIVENESS_ACCESSORY;
                objModel.IsActive = (!IsActive);

                Result = objModel.SetActivenessOfModelAccessory();

                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "Model Accessory Activated Successfully";
                    else
                        lblResult.Text = "Model Accessory Deactivated Successfully";
                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the Accessory";
                    else
                        lblResult.Text = "Failed to Deactivate the Accessory";
                }

                BindAccessories();
            }
        }
        catch
        {
        }
    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvModelAccessories.EditIndex = -1;
            gvModelAccessories.FooterRow.Visible = true;
            BindAccessories();
        }
        catch
        {
        }
    }
    protected void gvModelAccessories_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblResult.Text = "";
        Boolean IsActive = Convert.ToBoolean(((HiddenField)gvModelAccessories.Rows[e.NewEditIndex].FindControl("hdfIsActive")).Value.ToString());

        if (IsActive)
        {
            gvModelAccessories.EditIndex = e.NewEditIndex;
            BindAccessories();
            gvModelAccessories.FooterRow.Visible = false;
        }
        else
        {
            lblResult.Text = "De-Activated Accessory Mapping Can Not be updated";
        }
    }

    protected void gvModelAccessories_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvModelAccessories.PageIndex = e.NewPageIndex;
            BindAccessories();
        }
        catch (Exception ex)
        { }
    }

    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;

        //Swap sort direction
        this.DefineSortDirection();

        this.BindAccessories();
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

    protected void gvModelAccessories_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(((HiddenField)gvModelAccessories.Rows[e.RowIndex].FindControl("hdfID")).Value.ToString());

            //Update Model - Accessory Mapping Specification...
            Cls_ModelHelper objModel = new Cls_ModelHelper();
            objModel.ModelAccessoryId = ID;
            objModel.Specification = (gvModelAccessories.Rows[e.RowIndex].FindControl("txtEditDesc_Edit") as TextBox).Text.Trim();

            objModel.DBOperation = DbOperations.UPDATE_ACCESSORY;

            int result = objModel.UpdateModelAccessory();

            //Update Accessory Name
            Cls_Accessories objAccessory = new Cls_Accessories();
            objAccessory.Name = (gvModelAccessories.Rows[e.RowIndex].FindControl("txtAccessory_Edit") as TextBox).Text.Trim();
            objAccessory.ID = Convert.ToInt32(((HiddenField)gvModelAccessories.Rows[e.RowIndex].FindControl("hdfAccessoryId")).Value.ToString());
            objAccessory.IsParameter = false;
            objAccessory.DBOperation = DbOperations.UPDATE;
            result = objAccessory.UpdateAccessories();

            if (result > 0)
                lblResult.Text = "Accessory mapping Information updated successfully.";
            else
                lblResult.Text = "Unable to update Information.";

            gvModelAccessories.EditIndex = -1;
            BindAccessories();
        }
        catch
        { 
        }
    }
}
