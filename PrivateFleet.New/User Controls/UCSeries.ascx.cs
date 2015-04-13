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
using Mechsoft.FleetDeal;
using log4net;


public partial class User_Controls_UCSeries : System.Web.UI.UserControl
{
    #region Variable declaration
    Cls_SeriesMaster objSeries = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCSeries));
    #endregion

    #region Bind models
    public void BindModelDropDrown(DropDownList ddlToBind, string ValueToSelect, int MakeID)
    {
        try
        {
            ddlToBind.Items.Clear();
            Cls_ModelHelper objModel = new Cls_ModelHelper();
            objModel.MakeID = MakeID;
            DataTable dtModels = objModel.GetModelsOfMake();
            ddlToBind.DataSource = dtModels;
            ddlToBind.DataBind();


            if (ddlToBind.Items.Count == 0)
            {
                ddlToBind.Items.Insert(0, new ListItem("-No Models Found-", "0"));
            }
            else
            {
                ddlToBind.Items.Insert(0, new ListItem("-Select Model-", "0"));
            }
            if (ValueToSelect != "0")
                ddlToBind.SelectedIndex = ddlToBind.Items.IndexOf(ddlToBind.Items.FindByValue(ValueToSelect.ToString()));

        }
        catch (Exception ex) { logger.Error("BindDropDrown Function :" + ex.Message); }
    }
#endregion

    #region BindData
    private void BindData()
    {
        try
        {
            objSeries = new Cls_SeriesMaster();
            DataTable dtSeries = null;

            dtSeries = objSeries.GetAllSeries();
            DataView dv = dtSeries.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dtSeries = dv.ToTable();
            BindGridView(dtSeries);
            BindMakeDropDown(((DropDownList)gvSeriesDetails.FooterRow.FindControl("ddlMakes")),null, "0","");
            BindMakeDropDown(ddlSearchMakes,null, "0", "");
            ddlSearchModel.Items.Insert(0, new ListItem("-Select Make-", "0"));
        }
        catch (Exception ex)
        {
            logger.Error("BindData Function :" + ex.Message);
        }
    }
#endregion

    #region Bind grid view
    private void BindGridView(DataTable dtSeries)
    {

        try
        {

            if (dtSeries != null)
            {
                if (dtSeries.Rows.Count == 0)
                {
                    RemoveConstraints(dtSeries);
                    dtSeries.Rows.Add(dtSeries.NewRow());

                    gvSeriesDetails.DataSource = dtSeries;
                    gvSeriesDetails.DataBind();

                    gvSeriesDetails.Rows[0].Visible = false;
                }
                else
                {
                    gvSeriesDetails.DataSource = dtSeries;
                    gvSeriesDetails.DataBind();
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

    #region Bind makes
    private void BindMakeDropDown(DropDownList dropDownList, DropDownList ddlModelsToBind, string ValueToSelect, string ModelValueToSelect)
    {
        try
        {
            Cls_MakeHelper objMake = new Cls_MakeHelper();
            DataTable dtMakes = objMake.GetActiveMakes();
            dropDownList.DataSource = dtMakes;
            dropDownList.DataBind();
            dropDownList.Items.Insert(0, new ListItem("-Select Make-", "0"));

            if (ValueToSelect != "0")
            {
                dropDownList.SelectedIndex = dropDownList.Items.IndexOf(dropDownList.Items.FindByValue(ValueToSelect.ToString()));
                BindModelDropDrown(ddlModelsToBind, ModelValueToSelect, Convert.ToInt16(ValueToSelect));
            }

        }
        catch (Exception ex) { logger.Error("BindMakeDropDown Function :" + ex.Message); }
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
        { logger.Error("RemoveConstraints Function :" + ex.Message); }
    }

    #endregion

    #region  Page load
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
        catch (Exception ex) { logger.Error("Page_Load Event :" + ex.Message); }
    }
#endregion

    #region Add button click 
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objSeries = new Cls_SeriesMaster();
            objSeries.ModelID = Convert.ToInt16(((DropDownList)gvSeriesDetails.FooterRow.FindControl("ddlModels")).SelectedValue.ToString());
            objSeries.Series = ((TextBox)gvSeriesDetails.FooterRow.FindControl("txtSeries")).Text.ToString();
            if (objSeries.CheckIfSeriesExists().Rows.Count == 0)
            {
                objSeries.DBOperation = DbOperations.INSERT;
                int result = objSeries.AddSeries();

                if (result == 1)
                    lblResult.Text = "Make Added Successfully";
                else
                    lblResult.Text = " Make Addition Failed";
                BindData();
            }
            else
            {
                lblResult.Text = "Series " + objSeries.Series + " already exists.";
            }
        }
        catch (Exception ex)
        { logger.Error("imgbtnAdd_Click Event :" + ex.Message); }
    }
#endregion

    #region Grid row updating
    protected void gvSeriesDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt16(gvSeriesDetails.DataKeys[e.RowIndex][0].ToString());
            int ModelID = Convert.ToInt16(((DropDownList)gvSeriesDetails.Rows[e.RowIndex].FindControl("ddlEditModels")).SelectedValue.ToString());
            String Series = ((TextBox)gvSeriesDetails.Rows[e.RowIndex].FindControl("txtEditSeries")).Text.ToString();

            objSeries = new Cls_SeriesMaster();
            objSeries.ID = ID;
            objSeries.ModelID = ModelID;
            objSeries.Series = Series;
            if (objSeries.CheckIfSeriesExists().Rows.Count == 0)
            {
                objSeries.DBOperation = DbOperations.UPDATE;

                int result = objSeries.UpdateSeries();

                gvSeriesDetails.EditIndex = -1;
                BindData();

                if (result == 1)
                    lblResult.Text = "Series Updated Successfully";
                else
                    lblResult.Text = " Series Updation Failed";
            }
            else
            {
                lblResult.Text = "Series " + Series + " already exists.";
            }
        }
        catch (Exception ex)
        { logger.Error("gvSeriesDetails_RowUpdating Event :" + ex.Message); }
    }
#endregion

    #region Grid row command
    protected void gvSeriesDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objSeries = new Cls_SeriesMaster();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvSeriesDetails.PageIndex * gvSeriesDetails.PageSize);
                int ID = Convert.ToInt16(gvSeriesDetails.DataKeys[RowIndex][0].ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvSeriesDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objSeries.ID = ID;
                objSeries.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objSeries.IsActive = (!IsActive);

                Result = objSeries.SetActivenessOfSeries();


                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "Series Activated Successfully";
                    else
                        lblResult.Text = "Series Deactivated Successfully";

                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the Make";
                    else
                        lblResult.Text = "Failed to Deactivate the Make";

                }

                BindData();

            }

            if (e.CommandName.Equals("GetModelsofMake"))
            {

            }

        }
        catch (Exception ex)
        { logger.Error("gvSeriesDetails_RowCommand Event :" + ex.Message); }
    }
#endregion

    #region Cancel button click
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvSeriesDetails.EditIndex = -1;
            BindData();
        }
        catch (Exception ex) { logger.Error("imgbtnCancel_Click Event :" + ex.Message); }
    }
#endregion

    #region Gris row data bound
    protected void gvSeriesDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                logger.Error("gvSeriesDetails_RowDataBound Event :" + ex.Message);
            }
        }
    }
#endregion

    #region Grid row editing
    protected void gvSeriesDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblResult.Text = "";
        Boolean IsActive = Convert.ToBoolean(((HiddenField)gvSeriesDetails.Rows[e.NewEditIndex].FindControl("hdfIsActive")).Value.ToString());
        if (IsActive)
        {
            gvSeriesDetails.EditIndex = e.NewEditIndex;
            BindData();
            try
            {
                DropDownList ddlModelsToBind = ((DropDownList)gvSeriesDetails.Rows[e.NewEditIndex].FindControl("ddlEditModels"));
                DropDownList dropDownList = ((DropDownList)gvSeriesDetails.Rows[e.NewEditIndex].FindControl("ddlEditMakes"));
                HiddenField MakeID = ((HiddenField)gvSeriesDetails.Rows[e.NewEditIndex].FindControl("hdfMakeID"));
                HiddenField ModelID = ((HiddenField)gvSeriesDetails.Rows[e.NewEditIndex].FindControl("hdfModelID"));
                BindMakeDropDown(dropDownList, ddlModelsToBind, MakeID.Value.ToString(), ModelID.Value.ToString());
            }
            catch (Exception ex)
            {
                logger.Error("gvSeriesDetails_RowEditing Event :" + ex.Message);
            }
        }
        else
        {
            lblResult.Text = "Deactivated series cna not be updated";
        }
    }
#endregion

    #region Grid page index changing
    protected void gvSeriesDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvSeriesDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex) { logger.Error("gvSeriesDetails_PageIndexChanging Event :" + ex.Message); }
    }
    #endregion

    #region Grid sorting
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

    #region ddlmake selected index
    protected void ddlMakes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSearchModel.Items.Clear();
        int MakeID = Convert.ToInt16(((DropDownList)sender).SelectedValue.ToString());
        BindModelDropDrown(ddlSearchModel, "0", MakeID);

    }
    #endregion

    #region ddl Search make selected index
    protected void ddlSearchMakes_SelectedIndexChanged(object sender, EventArgs e)
    {
        int MakeID = Convert.ToInt16(((DropDownList)sender).SelectedValue.ToString());
        BindModelDropDrown(ddlSearchModel, "0", MakeID);

    }
#endregion

    #region ddl Edit makes selected index changed
    protected void ddlEditMakes_SelectedIndexChanged(object sender, EventArgs e)
    {
            GridViewRow gvRow = (GridViewRow)((DropDownList)(sender)).Parent.Parent;
            HiddenField hdfkMakeID = (HiddenField)gvRow.FindControl("hdfMakeID");

            DropDownList ddtToBind = ((DropDownList)gvRow.FindControl("ddlEditModels"));
            HiddenField ModelID = ((HiddenField)gvRow.FindControl("hdfModelID"));

        if (!(sender as DropDownList).SelectedValue.Equals("-Select-"))
        {
            int i = gvSeriesDetails.EditIndex;
           
            int MakeID = Convert.ToInt16((sender as DropDownList).SelectedValue.ToString());
          
            BindModelDropDrown(ddtToBind, ModelID.Value.ToString(), MakeID);
        }
        else
        {
            ddtToBind.Items.Clear();
            ddtToBind.Items.Insert(0, new ListItem("-Select Make First-", "-Select-"));
        }
    }
    #endregion

    #region Search cancel button 
    protected void imgbtnSearchCancel_Click(object sender, ImageClickEventArgs e)
    {
        ddlSearchModel.Items.Clear();
        ddlSearchMakes.SelectedIndex = 0;
        ddlSearchModel.Items.Insert(0, new ListItem("-Select Make-", "0"));
        txtSearchSeries.Text = "";
        BindData();
    }
    #endregion

    #region Search button click
    protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        objSeries = new Cls_SeriesMaster();
        DataTable dtSeries = null;
        try
        {
            objSeries.MakeID = Convert.ToInt32(ddlSearchMakes.SelectedValue.ToString());
            objSeries.ModelID = Convert.ToInt32(ddlSearchModel.SelectedValue.ToString());
            objSeries.Series = txtSearchSeries.Text;
          dtSeries=objSeries.GetSeriesByMakeAndModel();
          BindGridView(dtSeries);
        }
        catch (Exception ex)
        { }
    }
    #endregion
}
