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

public partial class User_Controls_UCLoaction : System.Web.UI.UserControl
{
    Cls_Location objLocation = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCLoaction));

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Location";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                BindData();
            }
        }
        catch (Exception ex)
        { logger.Error("Page_Load Event :" + ex.Message); }

    }


    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objLocation = new Cls_Location();
            objLocation.CityID = Convert.ToInt16(((DropDownList)gvLocationDetails.FooterRow.FindControl("ddlCity")).SelectedValue.ToString());
            objLocation.LocationID =Convert.ToInt32 (((DropDownList)gvLocationDetails.FooterRow.FindControl("ddlAddSuburb")).SelectedValue.ToString());

            if (objLocation.CheckIfLocationExists().Rows.Count == 0)
            {
                objLocation.DBOperation = DbOperations.INSERT;
                int result = objLocation.AddLocation();

                if (result == 1)
                    lblResult.Text = "Location Added Successfully";
                else
                    lblResult.Text = " Location Addition Failed";
                BindData();
            }
            else
            {
                lblResult.Text = objLocation.LocationID + " already exists";
            }
        }
        catch (Exception ex)
        { logger.Error("imgbtnAdd_Click Event :" + ex.Message); }
    }
    protected void gvLocationDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objLocation = new Cls_Location();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvLocationDetails.PageSize * gvLocationDetails.PageIndex);
                int Id = Convert.ToInt16(((HiddenField)gvLocationDetails.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvLocationDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objLocation.ID = Id;
                objLocation.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objLocation.IsActive = (!IsActive);

                Result = objLocation.SetActivenessOfLocation();


                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "Location Activated Successfully";
                    else
                        lblResult.Text = "Location Deactivated Successfully";

                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the Location";
                    else
                        lblResult.Text = "Failed to Deactivate the Location";

                }

                BindData();
            }
        }
        catch (Exception ex) { logger.Error("gvLocationDetails_RowCommand Event :" + ex.Message); }
    }
    protected void gvLocationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                logger.Error("gvLocationDetails_RowDataBound Event :" + Ex.Message);
            }
        }
    }
    protected void gvLocationDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvLocationDetails.EditIndex = e.NewEditIndex;
        BindData();
        try
        {
            DropDownList ddtToBind = ((DropDownList)gvLocationDetails.Rows[e.NewEditIndex].FindControl("ddlEditCity"));
            HiddenField CityID = ((HiddenField)gvLocationDetails.Rows[e.NewEditIndex].FindControl("hdfCityID"));
            BindDropDrown(ddtToBind, CityID.Value.ToString());


             ddtToBind = ((DropDownList)gvLocationDetails.Rows[e.NewEditIndex].FindControl("ddlEditSuburb"));
             HiddenField SuburbID = ((HiddenField)gvLocationDetails.Rows[e.NewEditIndex].FindControl("hdfSuburbID"));
            BindLocationDropDrown(ddtToBind, SuburbID.Value.ToString());

        }
        catch (Exception ex)
        {
            logger.Error("gvLocationDetails_RowEditing Event :" + ex.Message);
        }
    }


    protected void gvLocationDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt16(((HiddenField)gvLocationDetails.Rows[e.RowIndex].FindControl("hdfID")).Value.ToString());
            int CityID = Convert.ToInt16(((DropDownList)gvLocationDetails.Rows[e.RowIndex].FindControl("ddlEditCity")).SelectedValue.ToString());
            int LocationID = Convert.ToInt32(((DropDownList)gvLocationDetails.Rows[e.RowIndex].FindControl("ddlEditSuburb")).SelectedValue.ToString());

            objLocation = new Cls_Location();
            objLocation.ID = ID;
            objLocation.CityID = CityID;
            objLocation.LocationID = LocationID;
            if (objLocation.CheckIfLocationExists().Rows.Count == 0)
            {
                objLocation.DBOperation = DbOperations.UPDATE;

                int result = objLocation.UpdateLocation();

                gvLocationDetails.EditIndex = -1;
                BindData();

                if (result == 1)
                    lblResult.Text = "Location Updated Successfully";
                else
                    lblResult.Text = " Location Updation Failed";
            }
            else
            {
                lblResult.Text = LocationID + " already exists.";
            }
        }
        catch (Exception ex)
        { logger.Error("gvLocationDetails_RowUpdating Event :" + ex.Message); }
    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvLocationDetails.EditIndex = -1;
            BindData();
        }
        catch (Exception ex)
        { logger.Error("imgbtnCancel_Click Event :" + ex.Message); }
    }
    protected void gvLocationDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLocationDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        { logger.Error("gvLocationDetails_PageIndexChanging Event :" + ex.Message); }

    }


    #endregion


    #region Functions
    public void BindDropDrown(DropDownList ddlToBind, string ValueToSelect)
    {
        try
        {
            Cls_City objCity = new Cls_City();
            DataTable dtCities = objCity.GetAllCities();
            ddlToBind.DataSource = dtCities;
            ddlToBind.DataBind();
            ddlToBind.Items.Insert(0, new ListItem("-Select City-", "-Select-"));

            if (ValueToSelect != "0")
                ddlToBind.SelectedIndex = ddlToBind.Items.IndexOf(ddlToBind.Items.FindByValue(ValueToSelect.ToString()));

        }
        catch (Exception ex)
        { logger.Error("BindDropDrown Fucntion :" + ex.Message); }
    }


    public void BindLocationDropDrown(DropDownList ddlToBind, string ValueToSelect)
    {
        try
        {
            Cls_Location objLocation = new Cls_Location();
            DataTable dtSuburbs = objLocation.GetAllSuburbs();
            ddlToBind.DataSource = dtSuburbs;
            ddlToBind.DataBind();
            ddlToBind.Items.Insert(0, new ListItem("-Select Location-", "-Select-"));

            if (ValueToSelect != "0")
                ddlToBind.SelectedIndex = ddlToBind.Items.IndexOf(ddlToBind.Items.FindByValue(ValueToSelect.ToString()));

        }
        catch (Exception ex)
        { logger.Error("BindDropDrown Fucntion :" + ex.Message); }
    }

    private void BindData()
    {
        try
        {
            objLocation = new Cls_Location();
            DataTable dtLocation = null;

            dtLocation = objLocation.GetAllLocations();

            if (dtLocation != null)
            {
                DataView dv = dtLocation.DefaultView;
                dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
                dtLocation = dv.ToTable();

                if (dtLocation.Rows.Count == 0)
                {
                    RemoveConstraints(dtLocation);
                    dtLocation.Rows.Add(dtLocation.NewRow());

                    gvLocationDetails.DataSource = dtLocation;
                    gvLocationDetails.DataBind();

                    gvLocationDetails.Rows[0].Visible = false;
                }
                else
                {
                    gvLocationDetails.DataSource = dtLocation;
                    gvLocationDetails.DataBind();
                }

            }
            else
            {

            }

            try
            {
                BindDropDrown(((DropDownList)gvLocationDetails.FooterRow.FindControl("ddlCity")), "0");
                BindLocationDropDrown(((DropDownList)gvLocationDetails.FooterRow.FindControl("ddlAddSuburb")), "0");
            }
            catch (Exception ex)
            {
            }
        }
        catch (Exception ex)
        { logger.Error("BindData Function :" + ex.Message); }
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
        { logger.Error("RemoveConstraints Functions :" + ex.Message); }
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

