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

public partial class User_Controls_UCMakeDealer : System.Web.UI.UserControl
{
    Cls_MakeDealer objMakeDealer = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCMakeDealer));

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                 
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Dealer";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                BindDropDown();
            }
        }
        catch (Exception ex) { logger.Error("Page_Load Event :"+ex.Message); }
    }

    private void BindDropDown()
    {
        try
        {
            Cls_MakeHelper objMake = new Cls_MakeHelper();
            DataTable dtActiveMakes = objMake.GetActiveMakes();
            ddlMake.DataSource = dtActiveMakes;
            ddlMake.DataBind();

            if (ddlMake.Items.Count == 0)
                ddlMake.Items.Insert(0, new ListItem("-No Make Found-", "-Select-"));
            else
                ddlMake.Items.Insert(0, new ListItem("-Select Make-", "-Select-"));

        }
        catch (Exception ex) { logger.Error("BindDropDown Functin :"+ex.Message); }

    }
    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDealer.Items.Clear();
            Cls_Dealer objDealer = new Cls_Dealer();
            objDealer.MakeID = Convert.ToInt16(ddlMake.SelectedValue.ToString());
            DataTable dtActiveAllocatableDealers = objDealer.getActiveAllocatableDealers();
            ddlDealer.DataSource = dtActiveAllocatableDealers;
            ddlDealer.DataBind();

            if (ddlDealer.Items.Count == 0)
                ddlDealer.Items.Insert(0, new ListItem("-No Dealer Found-", "-Select-"));
            else
                ddlDealer.Items.Insert(0, new ListItem("-Select Dealer-", "-Select-"));



            BindData(1);
        }
        catch (Exception ex)
        {
            ddlDealer.Items.Insert(0, new ListItem("-Please Select Make -", "-Select-"));
            BindData(0);
            logger.Error("ddlMake_SelectedIndexChanged Event :" + ex.Message);
        }


    }

    private void BindData(int Choice)
    {
        try
        {
            objMakeDealer = new Cls_MakeDealer();
            if (Choice == 0)
                objMakeDealer.MakeID = 0;
            else
                objMakeDealer.MakeID = Convert.ToInt16(ddlMake.SelectedValue.ToString());
            DataTable dtMakeDealer = objMakeDealer.getAllDealersOfMake();
            DataView dv = dtMakeDealer.DefaultView;
             dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dtMakeDealer=dv.ToTable();
            gvMakeDealerDetails.DataSource = dtMakeDealer;
            gvMakeDealerDetails.DataBind();
        }
        catch (Exception ex)
        { logger.Error("BindData Function :"+ex.Message); }
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
        catch (Exception ex) { logger.Error(" RemoveConstraints Function :"+ex.Message); }
    }





    protected void gvMakeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Image imgBtnActive = ((Image)e.Row.FindControl("imgbtnActivate"));
                LinkButton lnkbtnActivate = ((LinkButton)e.Row.FindControl("lnkbtnActiveness"));
                if (imgBtnActive != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive")))
                    {

                        imgBtnActive.ImageUrl = "~/Images/Active.png";
                    }
                    else
                    {
                        imgBtnActive.ImageUrl = "~/Images/Inactive.ico";
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
        }
        catch (Exception ex) { logger.Error("gvMakeDealerDetails_RowDataBound Event :"+ex.Message); }

    }
    protected void imgbtnActivate_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void gvMakeDealerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        try
        {

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString()) - Convert.ToInt16(gvMakeDealerDetails.PageIndex);

                hdfID.Value = gvMakeDealerDetails.DataKeys[RowIndex][0].ToString();
                objMakeDealer = new Cls_MakeDealer();
                objMakeDealer.ID = Convert.ToInt16(hdfID.Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvMakeDealerDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objMakeDealer.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objMakeDealer.IsActive = (!IsActive);

                int Result = objMakeDealer.setActivenessOfMakeDealer();

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
        { logger.Error("gvMakeDealerDetails_RowCommand Event :"+ex.Message); }
    }


    protected void gvMakeDealerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                logger.Error("gvMakeDealerDetails_RowDataBound Event :" + ex.Message);
            }
        }
    }
  

    private void ClearFields()
    {

        // if (ddlMake.Items.Count > 0)
        //  ddlMake.SelectedIndex = 0;

        try
        {
            if (ddlMake.SelectedIndex != 0)
            {
                ddlMake.SelectedIndex = 0;
                ddlMake_SelectedIndexChanged(null, null);
            }
            else
                ddlDealer.Items.Insert(0, new ListItem("Please select the Make", "-Select-"));


            if (ddlDealer.Items.Count > 0)
                ddlDealer.SelectedIndex = 0;

        }
        catch (Exception ex) { logger.Error("ClearFields Function :"+ex.Message); }
    }
   
    protected void gvMakeDealerDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gvMakeDealerDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void gvMakeDealerDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvMakeDealerDetails.PageIndex = e.NewPageIndex;
            BindData(1);
        }
        catch (Exception ex)
        { logger.Error("gvMakeDealerDetails_PageIndexChanging Event :"+ex.Message); }
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            objMakeDealer = new Cls_MakeDealer();
            objMakeDealer.MakeID = Convert.ToInt16(ddlMake.SelectedValue.ToString());
            objMakeDealer.DealerID = Convert.ToInt16(ddlDealer.SelectedValue.ToString());
            int Result = 0;
            objMakeDealer.DBOperation = DbOperations.INSERT;
            Result = objMakeDealer.AddMakeDealer();
            if (Result == 1)
            {
                lblResult.Text = "Record Added Successfully";
                BindData(1);

                ddlMake_SelectedIndexChanged(null, null);

            }
        }
        catch (Exception ex)
        { logger.Error("imgbtnAdd_Click Event :"+ex.Message); }
    }
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearFields();
        }
        catch (Exception ex)
        { logger.Error("imgbtnCancel_Click Event :" + ex.Message); }
    }


    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;


        //Swap sort direction
        this.DefineSortDirection();

        // BindData(objCourseMaster);
        this.BindData(1);
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