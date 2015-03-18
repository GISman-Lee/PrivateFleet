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

public partial class User_Controls_UCSeriesAccessories : System.Web.UI.UserControl
{
    Cls_SeriesAccessories objSeriesAccessory = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCSeriesAccessories));


    #region Functions
    private void BindData(int Choice)
    {
        try
        {
            objSeriesAccessory = new Cls_SeriesAccessories();
            if (Choice == 0)
                objSeriesAccessory.SeriesID = 0;
            else
                objSeriesAccessory.SeriesID = Convert.ToInt16(ddlSeries.SelectedValue.ToString());
            DataTable dtSeriesDealer = objSeriesAccessory.getAllAccessoriesOfSeries();
            gvSeriesAccessoriesDetails.DataSource = dtSeriesDealer;
            gvSeriesAccessoriesDetails.DataBind();
        }
        catch (Exception ex) { logger.Error("BindData (int Choice) :" + ex.Message); }
    }

    private void BindDropDown()
    {
        try
        {
            Cls_SeriesMaster objSeries = new Cls_SeriesMaster();
            DataTable dtActiveSeriess = objSeries.GetAllActiveSeries();
            ddlSeries.DataSource = dtActiveSeriess;
            ddlSeries.DataBind();

            if (ddlSeries.Items.Count == 0)
                ddlSeries.Items.Insert(0, new ListItem("- No Series Found -", "-Select-"));
            else
                ddlSeries.Items.Insert(0, new ListItem("- Select Series -", "-Select-"));

        }
        catch (Exception ex) { logger.Error("BindDropDown Function :" + ex.Message); }

    }
    private void ClearFields()
    {

        // if (ddlSeries.Items.Count > 0)
        //  ddlSeries.SelectedIndex = 0;
        try
        {
            if (ddlSeries.SelectedIndex != 0)
            {
                ddlSeries.SelectedIndex = 0;
                ddlSeries_SelectedIndexChanged(null, null);
            }
            else
                ddlAccessory.Items.Insert(0, new ListItem("- Please select the series First -", "-Select-"));

            if (ddlAccessory.Items.Count > 0)
                ddlAccessory.SelectedIndex = 0;

            txtSpecification.Text = "";
        }
        catch (Exception ex)
        { logger.Error("ClearFields Function :" + ex.Message); }
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
        { logger.Error("RemoveConstraints Function :" + ex.Message); }
    }


    #endregion


    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //BindDropDown();
                BindMakeDropDrown();
            }
        }
        catch (Exception ex) { logger.Error("Page_Load Event :" + ex.Message); }
    }


    protected void ddlSeries_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlAccessory.Items.Clear();
            objSeriesAccessory = new Cls_SeriesAccessories();
            objSeriesAccessory.SeriesID = Convert.ToInt16(ddlSeries.SelectedValue.ToString());
            DataTable dtActiveAllocatableAccessories = objSeriesAccessory.getActiveAllocatableAcessories();
            ddlAccessory.DataSource = dtActiveAllocatableAccessories;
            ddlAccessory.DataBind();

            if (ddlAccessory.Items.Count == 0)
                ddlAccessory.Items.Insert(0, new ListItem("- No Accessory Found -", "-Select-"));
            else
                ddlAccessory.Items.Insert(0, new ListItem("- Select Accessory -", "-Select-"));



            BindData(1);
        }
        catch (Exception ex)
        {
            if (ddlAccessory.Items.Count == 0)
                ddlAccessory.Items.Insert(0, new ListItem("- Please select the Series First -", "-Select-"));
            BindData(0);
            logger.Error("ddlSeries_SelectedIndexChanged Event :" + ex.Message);
        }


    }


    protected void imgbtnActivate_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void gvSeriesAccessoriesDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        try
        {

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvSeriesAccessoriesDetails.PageIndex * gvSeriesAccessoriesDetails.PageSize);
                // - Convert.ToInt16(gvSeriesAccessoriesDetails.PageIndex);
                hdfID.Value = gvSeriesAccessoriesDetails.DataKeys[RowIndex][0].ToString();
                objSeriesAccessory = new Cls_SeriesAccessories();
                objSeriesAccessory.ID = Convert.ToInt16(hdfID.Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvSeriesAccessoriesDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objSeriesAccessory.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objSeriesAccessory.IsActive = (!IsActive);

                int Result = objSeriesAccessory.setActivenessOfSeriesAccessory();
                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "Record Activated Successfully";
                    else
                        lblResult.Text = "Record Deactivated Successfully";

                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the Record";
                    else
                        lblResult.Text = "Failed to Deactivate the Record";

                }

                BindData(1);
            }

        }
        catch (Exception ex)
        { logger.Error("gvSeriesAccessoriesDetails_RowCommand Event :" + ex.Message); }
    }


    protected void gvSeriesAccessoriesDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                logger.Error("gvSeriesAccessoriesDetails_RowDataBound Event :" + ex.Message);
            }
        }
    }



    protected void gvSeriesAccessoriesDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvSeriesAccessoriesDetails.EditIndex = e.NewEditIndex;
            BindData(1);
        }
        catch (Exception ex)
        { logger.Error("gvSeriesAccessoriesDetails_RowEditing Event :" + ex.Message); }
    }
    protected void gvSeriesAccessoriesDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void gvSeriesAccessoriesDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvSeriesAccessoriesDetails.PageIndex = e.NewPageIndex;
            BindData(1);
        }
        catch (Exception ex)
        { logger.Error("gvSeriesAccessoriesDetails_PageIndexChanging Event :" + ex.Message); }
    }
    protected void gvSeriesAccessoriesDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt16(gvSeriesAccessoriesDetails.DataKeys[e.RowIndex][0].ToString());
            String Specification = ((TextBox)gvSeriesAccessoriesDetails.Rows[e.RowIndex].FindControl("txtEditSpecification")).Text;

            objSeriesAccessory = new Cls_SeriesAccessories();
            objSeriesAccessory.SeriesID = 0;
            objSeriesAccessory.AccessoryID = 0;
            objSeriesAccessory.ID = ID;
            objSeriesAccessory.Specification = Specification;
            objSeriesAccessory.DBOperation = DbOperations.UPDATE;
            int Result = objSeriesAccessory.UpdateSeriesAccessory();

            if (Result == 1)
            {
                lblResult.Text = "Record updated successfully";
            }
            else
            {
                lblResult.Text = "Failed to update the record";
            }

            BindData(1);
            imgbtnCancel_Click(null, null);
        }
        catch (Exception ex)
        { logger.Error("gvSeriesAccessoriesDetails_RowUpdating Event :" + ex.Message); }
    }
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvSeriesAccessoriesDetails.EditIndex = -1;
            BindData(1);
            ClearFields();
        }
        catch (Exception ex)
        { logger.Error("imgbtnCancel_Click Event :" + ex.Message); }
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objSeriesAccessory = new Cls_SeriesAccessories();
            objSeriesAccessory.SeriesID = Convert.ToInt16(ddlSeries.SelectedValue.ToString());
            objSeriesAccessory.AccessoryID = Convert.ToInt16(ddlAccessory.SelectedValue.ToString());
            objSeriesAccessory.Specification = txtSpecification.Text;
            int Result = 0;
            objSeriesAccessory.DBOperation = DbOperations.INSERT;
            Result = objSeriesAccessory.AddSeriesAccessory();
            if (Result == 1)
            {
                lblResult.Text = "Record Added Successfully";
                BindData(1);

                ddlSeries_SelectedIndexChanged(null, null);
            }

        }
        catch (Exception ex)
        { logger.Error("imgbtnAdd_Click Event :" + ex.Message); }
    }
    #endregion

    public void BindMakeDropDrown()
    {
        try
        {
            ddlMake.Items.Clear();
            Cls_MakeHelper objMake = new Cls_MakeHelper();
            DataTable dtMakes = objMake.GetActiveMakes();
            ddlMake.DataSource = dtMakes;
            ddlMake.DataBind();

            if (ddlMake.Items.Count > 0)
                ddlMake.Items.Insert(0, new ListItem("-Select Make-", "-Select-"));
            else
                ddlMake.Items.Add(new ListItem("- No Makes Found -", "-Select-"));
        }
        catch (Exception ex)
        {
            logger.Error("BindDropDrown Function :" + ex.Message);
        }
    }
    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlModel.Items.Clear();
            Cls_ModelHelper objModel = new Cls_ModelHelper();

            if (ddlMake.SelectedIndex == 0)
                ddlModel.Items.Add(new ListItem("- Please select the Make First -", "-Select-"));

            objModel.MakeID = Convert.ToInt16(ddlMake.SelectedValue);
            DataTable dtModels = objModel.GetModelsOfMake();
            ddlModel.DataSource = dtModels;
            ddlModel.DataBind();

            ddlSeries.Items.Clear();
            ddlSeries.Items.Add(new ListItem("- Please select the Model First -", "-Select-"));

            ddlAccessory.Items.Clear();
            ddlAccessory.Items.Insert(0, new ListItem("- Please select the Series First -", "-Select-"));
            if (ddlModel.Items.Count > 0)
                ddlModel.Items.Insert(0, new ListItem("-Select Model-", "-Select-"));
            else
                ddlModel.Items.Add(new ListItem("- No Models Found -", "-Select-"));

        }
        catch (Exception ex)
        {
        }
    }
    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSeries.Items.Clear();
            Cls_SeriesMaster objSeries = new Cls_SeriesMaster();
            if (ddlModel.SelectedIndex == 0)
                ddlSeries.Items.Add(new ListItem("- Please select the Model First -", "-Select-"));

            objSeries.ModelID = Convert.ToInt16(ddlModel.SelectedValue);
            DataTable dtSeries = objSeries.GetSeriesOfModel();
            ddlSeries.DataSource = dtSeries;
            ddlSeries.DataBind();

            if (ddlSeries.Items.Count > 0)
                ddlSeries.Items.Insert(0, new ListItem("-Select Series-", "-Select-"));
            else
                ddlSeries.Items.Add(new ListItem("- No Series Found -", "-Select-"));

        }
        catch (Exception ex)
        {
        }

    }
}
