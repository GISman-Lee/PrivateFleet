using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mechsoft.FleetDeal;
using Mechsoft.GeneralUtilities;
using System.Data.Common;
using System.Data;
using log4net;

public partial class User_Controls_UCModel : System.Web.UI.UserControl
{
    #region Variable declaration
    Cls_ModelHelper objModel = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCModel));
    bool flag = true;
    #endregion

    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Make";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                BindData();

            }
        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Event :" + ex.Message);
        }
    }
    #endregion

    #region bind drop down list
    public void BindDropDrown(DropDownList ddlToBind, string ValueToSelect)
    {
        try
        {
            Cls_MakeHelper objMake = new Cls_MakeHelper();
            DataTable dtMakes = objMake.GetAllMakes();
            ddlToBind.DataSource = dtMakes;
            ddlToBind.DataBind();
            ddlToBind.Items.Insert(0, new ListItem("-Select Make-", "-Select-"));

            if (ValueToSelect != "0")
                ddlToBind.SelectedIndex = ddlToBind.Items.IndexOf(ddlToBind.Items.FindByValue(ValueToSelect.ToString()));

        }
        catch (Exception ex)
        {
            logger.Error("BindDropDrown Function :" + ex.Message);
        }
    }
    #endregion

    #region bind data
    private void BindData()
    {
        try
        {

            objModel = new Cls_ModelHelper();
            DataTable dtModels = null;
            dtModels = objModel.GetAllModels();
            BingGridView(dtModels);
            BindDropDrown(((DropDownList)gvModelDetails.FooterRow.FindControl("ddlMakes")), "0");
            BindDropDrown(ddlSearchMakes, "0");
        }
        catch (Exception ex)
        {
            logger.Error("BindData Function :" + ex.Message);
        }
    }
    #endregion

    #region bind grid
    private void BingGridView(DataTable dtModels)
    {
        try
        {

            if (dtModels != null)
            {
                DataView dv = dtModels.DefaultView;
                dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
                dtModels = dv.ToTable();

                if (dtModels.Rows.Count == 0)
                {
                    RemoveConstraints(dtModels);
                    dtModels.Rows.Add(dtModels.NewRow());

                    gvModelDetails.DataSource = dtModels;
                    gvModelDetails.DataBind();

                    gvModelDetails.Rows[0].Visible = false;
                }
                else
                {
                    gvModelDetails.DataSource = dtModels;
                    gvModelDetails.DataBind();
                }

            }
            else
            {

            }
        }
        catch (Exception ex)
        { }
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
        catch (Exception ex)
        {
            logger.Error("RemoveConstraints Function :" + ex.Message);
        }
    }
    #endregion

    #region Add button click
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objModel = new Cls_ModelHelper();
            objModel.MakeID = Convert.ToInt16(((DropDownList)gvModelDetails.FooterRow.FindControl("ddlMakes")).SelectedValue.ToString());
            objModel.Model = ((TextBox)gvModelDetails.FooterRow.FindControl("txtModel")).Text.ToString();

            bool AddAccessory = (((RadioButtonList)gvModelDetails.FooterRow.FindControl("rbAddAccessories")).SelectedValue).ToString() == "Yes" ? true : false;

            objModel.ReturnId = AddAccessory;

            if (objModel.CheckIfModelExists().Rows.Count == 0)
            {
                objModel.DBOperation = DbOperations.INSERT;
                int result = objModel.AddModel();

                if (AddAccessory)
                    if (result > 0)
                        Response.Redirect("ModelAccessory.aspx?make=" + objModel.MakeID.ToString() + "&model=" + result.ToString(), false);

                if (result == 1)
                    lblResult.Text = "Model Added Successfully";
                else
                    lblResult.Text = " Model Addition Failed";
                BindData();
            }
            else
            {
                lblResult.Text = "Model " + objModel.Model + " already exists.";
            }
        }
        catch (Exception ex)
        { logger.Error("imgbtnAdd_Click Event :" + ex.Message); }
    }
    #endregion

    #region Grid row command
    protected void gvModelDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objModel = new Cls_ModelHelper();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvModelDetails.PageSize * gvModelDetails.PageIndex);
                int Id = Convert.ToInt16(((HiddenField)gvModelDetails.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvModelDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objModel.ID = Id;
                objModel.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objModel.IsActive = (!IsActive);

                Result = objModel.SetActivenessOfModel();


                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "Model Activated Successfully";
                    else
                        lblResult.Text = "Model Deactivated Successfully";

                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the Model";
                    else
                        lblResult.Text = "Failed to Deactivate the Model";

                }

                BindData();
            }
        }
        catch (Exception ex)
        { logger.Error("gvModelDetails_RowCommand Event :" + ex.Message); }
    }
    #endregion

    #region Grid row data bound
    protected void gvModelDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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


                LinkButton lnkManageAccessories = e.Row.FindControl("lnkManageAccessories") as LinkButton;

                if (lnkManageAccessories != null)
                    lnkManageAccessories.PostBackUrl = "~/ModelAccessory.aspx?make=" + DataBinder.Eval(e.Row.DataItem, "MakeID").ToString() + "&model=" + DataBinder.Eval(e.Row.DataItem, "Id").ToString();

            }
            catch (Exception Ex)
            {
                logger.Error("gvModelDetails_RowDataBound Event :" + Ex.Message);
            }
        }
    }
    #endregion

    #region Grid row editing
    protected void gvModelDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblResult.Text = "";
        Boolean IsActive = Convert.ToBoolean(((HiddenField)gvModelDetails.Rows[e.NewEditIndex].FindControl("hdfIsActive")).Value.ToString());

        if (IsActive)
        {
            gvModelDetails.EditIndex = e.NewEditIndex;
            BindData();
            try
            {
                DropDownList ddtToBind = ((DropDownList)gvModelDetails.Rows[e.NewEditIndex].FindControl("ddlEditMakes"));
                HiddenField MakeID = ((HiddenField)gvModelDetails.Rows[e.NewEditIndex].FindControl("hdfMakeID"));
                BindDropDrown(ddtToBind, MakeID.Value.ToString());
            }
            catch (Exception ex)
            {
                logger.Error("gvModelDetails_RowEditing Event :" + ex.Message);
            }
        }
        else
        {
            lblResult.Text = "Deactivated model can not be updated";
        }
    }
    #endregion

    #region grid row updating
    protected void gvModelDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt16(((HiddenField)gvModelDetails.Rows[e.RowIndex].FindControl("hdfID")).Value.ToString());
            int MakeID = Convert.ToInt16(((DropDownList)gvModelDetails.Rows[e.RowIndex].FindControl("ddlEditMakes")).SelectedValue.ToString());
            String Model = ((TextBox)gvModelDetails.Rows[e.RowIndex].FindControl("txtEditModel")).Text.ToString();

            objModel = new Cls_ModelHelper();
            objModel.ID = ID;
            objModel.MakeID = MakeID;
            objModel.Model = Model;
            if (objModel.CheckIfModelExists().Rows.Count == 0)
            {
                objModel.DBOperation = DbOperations.UPDATE;

                int result = objModel.UpdateModel();

                gvModelDetails.EditIndex = -1;
                BindData();

                if (result == 1)
                    lblResult.Text = "Model Updated Successfully";
                else
                    lblResult.Text = " Model Updation Failed";
            }
            else
            {
                lblResult.Text = "Model " + objModel.Model + " already exists.";
            }
        }
        catch (Exception ex)
        { logger.Error("gvModelDetails_RowUpdating Event :" + ex.Message); }
    }
    #endregion

    #region cancel button click event
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvModelDetails.EditIndex = -1;
            BindData();
        }
        catch (Exception ex) { logger.Error("imgbtnCancel_Click Event :" + ex.Message); }
    }
    #endregion

    #region grid page index changing event
    protected void gvModelDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvModelDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        { logger.Error("gvModelDetails_PageIndexChanging Event :" + ex.Message); }
    }
    #endregion

    #region grid sorting
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

    #region Button search cancel click
    protected void imgbtnSearchCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlSearchMakes.SelectedIndex = 0;
            txtSearchModel.Text = "";
            BindData();
        }
        catch (Exception ex)
        { }
    }
    #endregion

    #region Button search click
    protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        objModel = new Cls_ModelHelper();
        try
        {
            DataTable dtModel = null;
            objModel.MakeID = Convert.ToInt32(ddlSearchMakes.SelectedValue.ToString());
            objModel.Model = txtSearchModel.Text;
            dtModel = objModel.SearchModels();
            BingGridView(dtModel);
        }
        catch (Exception ex)
        { }
    }
    #endregion
}
