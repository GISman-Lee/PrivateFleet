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

public partial class User_Controls_UCCity : System.Web.UI.UserControl
{
    Cls_City objCity = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCCity));


    #region Functions
    public void BindDropDrown(DropDownList ddlToBind, string ValueToSelect)
    {
        try
        {
            Cls_State objState = new Cls_State();
            DataTable dtStates = objState.GetAllStates();
            ddlToBind.DataSource = dtStates;
            ddlToBind.DataBind();
            ddlToBind.Items.Insert(0, new ListItem("-Select State-", "-Select-"));

            if (ValueToSelect != "0")
                ddlToBind.SelectedIndex = ddlToBind.Items.IndexOf(ddlToBind.Items.FindByValue(ValueToSelect.ToString()));

        }
        catch (Exception ex) { logger.Error("BindDropDrown Function : " + ex.Message); }
    }

    private void BindData()
    {
        try
        {
            objCity = new Cls_City();
            DataTable dtCity = null;

            dtCity = objCity.GetAllCities();
            DataView dv = dtCity.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dtCity = dv.ToTable();

            if (dtCity != null)
            {
                if (dtCity.Rows.Count == 0)
                {
                    RemoveConstraints(dtCity);
                    dtCity.Rows.Add(dtCity.NewRow());

                    gvCityDetails.DataSource = dtCity;
                    gvCityDetails.DataBind();

                    gvCityDetails.Rows[0].Visible = false;
                }
                else
                {
                    gvCityDetails.DataSource = dtCity;
                    gvCityDetails.DataBind();
                }

            }
            else
            {

            }

            try
            {
                BindDropDrown(((DropDownList)gvCityDetails.FooterRow.FindControl("ddlStates")), "0");
            }
            catch (Exception ex)
            {
            }
        }
        catch (Exception ex)
        { logger.Error("BindData  :" + ex.Message); }
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
            logger.Error("RemoveConstraints Function :" + ex.Message);
        }
    }
    #endregion


    #region  Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "State";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                BindData();
            }
        }
        catch (Exception ex) { logger.Error("Page_Load Event :" + ex.Message); }

    }



    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objCity = new Cls_City();
            objCity.StateID = Convert.ToInt16(((DropDownList)gvCityDetails.FooterRow.FindControl("ddlStates")).SelectedValue.ToString());
            objCity.City = ((TextBox)gvCityDetails.FooterRow.FindControl("txtCity")).Text.ToString();
            objCity.DBOperation = DbOperations.CHECK_IF_EXIST;
            if (objCity.CheckIfCityExists().Rows.Count == 0)
            {

                objCity.DBOperation = DbOperations.INSERT;
                int result = objCity.AddCity();

                if (result == 1)
                    lblResult.Text = "City Added Successfully";
                else
                    lblResult.Text = " City Addition Failed";
                BindData();
            }
            else
            {
                lblResult.Text = objCity.City + " already exists.";
            }
        }
        catch (Exception ex)
        { logger.Error("imgbtnAdd_Click Event :" + ex.Message); }
    }
    protected void gvCityDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objCity = new Cls_City();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvCityDetails.PageIndex * gvCityDetails.PageSize);

                int Id = Convert.ToInt16(((HiddenField)gvCityDetails.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvCityDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objCity.ID = Id;
                objCity.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objCity.IsActive = (!IsActive);

                Result = objCity.SetActivenessOfCity();


                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "City Activated Successfully";
                    else
                        lblResult.Text = "City Deactivated Successfully";

                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the City";
                    else
                        lblResult.Text = "Failed to Deactivate the City";

                }

                BindData();
            }
        }
        catch (Exception ex)
        { logger.Error("gvCityDetails_RowCommand Event :" + ex.Message); }
    }
    protected void gvCityDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                catch (Exception Ex)
                {
                }
            }
        }
        catch (Exception ex)
        { logger.Error("gvCityDetails_RowDataBound Event :" + ex.Message); }
    }
    protected void gvCityDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvCityDetails.EditIndex = e.NewEditIndex;
            BindData();
            try
            {
                DropDownList ddtToBind = ((DropDownList)gvCityDetails.Rows[e.NewEditIndex].FindControl("ddlEditStates"));
                HiddenField StateID = ((HiddenField)gvCityDetails.Rows[e.NewEditIndex].FindControl("hdfStateID"));
                BindDropDrown(ddtToBind, StateID.Value.ToString());
            }
            catch (Exception ex)
            {
            }
        }
        catch (Exception ex)
        { logger.Error("gvCityDetails_RowEditing Event :" + ex.Message); }
    }


    protected void gvCityDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt16(((HiddenField)gvCityDetails.Rows[e.RowIndex].FindControl("hdfID")).Value.ToString());
            int StateID = Convert.ToInt16(((DropDownList)gvCityDetails.Rows[e.RowIndex].FindControl("ddlEditStates")).SelectedValue.ToString());
            String City = ((TextBox)gvCityDetails.Rows[e.RowIndex].FindControl("txtEditCity")).Text.ToString();

            objCity = new Cls_City();
            objCity.ID = ID;
            objCity.StateID = StateID;
            objCity.City = City;
            objCity.DBOperation = DbOperations.CHECK_IF_EXIST;
            if (objCity.CheckIfCityExists().Rows.Count == 0)
            {
                objCity.DBOperation = DbOperations.UPDATE;

                int result = objCity.UpdateCity();

                gvCityDetails.EditIndex = -1;
                BindData();

                if (result == 1)
                    lblResult.Text = "City Updated Successfully";
                else
                    lblResult.Text = " City Updation Failed";
            }
            else
            {
                lblResult.Text = objCity.City + " already exists.";
            }
        }
        catch (Exception ex)
        { logger.Error("gvCityDetails_RowUpdating Event :" + ex.Message); }
    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvCityDetails.EditIndex = -1;
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnCancel_Click Event :" + ex.Message);
        }
    }
    protected void gvCityDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            gvCityDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        { logger.Error("gvCityDetails_PageIndexChanging Eveny :" + ex.Message); }
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
