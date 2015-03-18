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
public partial class User_Controls_UCMake : System.Web.UI.UserControl
{
    Cls_MakeHelper objMake = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCMake));

    #region Events
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
        { logger.Error("Page_Load Event :"+ex.Message); }
    }


    protected void gvMakeDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvMakeDetails.EditIndex = e.NewEditIndex;
            BindData();
        }
        catch (Exception ex) { logger.Error("gvMakeDetails_RowEditing Event :"+ex.Message); }

    }


    protected void gvMakeDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt16(((HiddenField)gvMakeDetails.Rows[e.RowIndex].FindControl("hdfID")).Value.ToString());
            String Make = ((TextBox)gvMakeDetails.Rows[e.RowIndex].FindControl("txtEditMake")).Text.ToString();

            objMake = new Cls_MakeHelper();
            objMake.ID = ID;
            objMake.Make = Make;
            if (objMake.CheckIfMakeExists().Rows.Count == 0)
            {
                objMake.DBOperation = DbOperations.UPDATE;
                int result = objMake.UpdateMake();

                gvMakeDetails.EditIndex = -1;
                BindData();

                if (result == 1)
                    lblResult.Text = "Make Updated Successfully";
                else
                    lblResult.Text = " Make Updation Failed";
            }
            else
            {
                lblResult.Text = "Make " + Make + " already exists.";
            }
        }
        catch (Exception ex)
        { logger.Error("gvMakeDetails_RowUpdating Event :" + ex.Message); }
    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            String Make = ((TextBox)gvMakeDetails.FooterRow.FindControl("txtMake")).Text.ToString();

            objMake = new Cls_MakeHelper();
            objMake.Make = Make;
            if (objMake.CheckIfMakeExists().Rows.Count == 0)
            {
                objMake.DBOperation = DbOperations.INSERT;
                int result = objMake.AddMake();


                if (result == 1)
                    lblResult.Text = "Make Added Successfully";
                else
                    lblResult.Text = " Make Addition Failed";
                BindData();
            }
            else
            {
                lblResult.Text = "Make " + Make + " already exists.";
            }
        }
        catch (Exception ex)
        { logger.Error("imgbtnAdd_Click Event :"+ex.Message); }
    }

    protected void gvMakeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                logger.Error("gvMakeDetails_RowDataBound Event :"+ex.Message);
            }
        }
    }
    protected void imgbtnActivate_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void gvMakeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objMake = new Cls_MakeHelper();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvMakeDetails.PageIndex * gvMakeDetails.PageSize);
                int Id = Convert.ToInt16(((HiddenField)gvMakeDetails.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvMakeDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objMake.ID = Id;
                objMake.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objMake.IsActive = (!IsActive);

                Result = objMake.SetActivenessOfMake();


                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "Make Activated Successfully";
                    else
                        lblResult.Text = "Make Deactivated Successfully";

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



        }
        catch (Exception ex)
        { logger.Error("gvMakeDetails_RowCommand Event :"+ex.Message); }
    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvMakeDetails.EditIndex = -1;
            BindData();
        }
        catch (Exception ex) { logger.Error("imgbtnCancel_Click Event :"+ex.Message); }
    }
    protected void gvMakeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMakeDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        { logger.Error("gvMakeDetails_PageIndexChanging Event :"+ex.Message); }
    }


    #endregion

    
    #region Functions
    private void BindData()
    {
        try
        {
            objMake = new Cls_MakeHelper();
            DataTable dtmakes = null;

            dtmakes = objMake.GetAllMakes();

            if (dtmakes != null)
            {
                DataView dv = dtmakes.DefaultView;
                dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
                dtmakes = dv.ToTable();

                if (dtmakes.Rows.Count == 0)
                {
                    RemoveConstraints(dtmakes);
                    dtmakes.Rows.Add(dtmakes.NewRow());

                    gvMakeDetails.DataSource = dtmakes;
                    gvMakeDetails.DataBind();

                    gvMakeDetails.Rows[0].Visible = false;
                }
                else
                {
                    gvMakeDetails.DataSource = dtmakes;
                    gvMakeDetails.DataBind();
                }

            }
            else
            {

            }
        }
        catch (Exception ex) { logger.Error("BindData Function :"+ex.Message); }
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
        { logger.Error("RemoveConstraints Function :"+ex.Message); }
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
