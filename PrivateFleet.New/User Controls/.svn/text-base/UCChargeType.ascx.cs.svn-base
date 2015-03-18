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

public partial class User_Controls_UCChargeType : System.Web.UI.UserControl
{
    Cls_ChargeType objChargeType = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCChargeType));

    #region Page load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Type";
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
                BindData();
            }
        }
        catch (Exception ex)
        { logger.Error("Page_Load Event :" + ex.Message); }
    }
    #endregion

    protected void gvChargeTypeDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            lblResult.Text = "";
            Boolean IsActive = Convert.ToBoolean(((HiddenField)gvChargeTypeDetails.Rows[e.NewEditIndex].FindControl("hdfIsActive")).Value.ToString());

           if (IsActive)
           {
               gvChargeTypeDetails.EditIndex = e.NewEditIndex;
               BindData();
           }
           else
           {
               lblResult.Text = "Deactivated charge type can not updated.";
           }
        }
        catch (Exception ex)
        { logger.Error("gvChargeTypeDetails_RowEditing Event :"+ex.Message); }
    }


    protected void gvChargeTypeDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt16(((HiddenField)gvChargeTypeDetails.Rows[e.RowIndex].FindControl("hdfID")).Value.ToString());
            String ChargeType = ((TextBox)gvChargeTypeDetails.Rows[e.RowIndex].FindControl("txtEditChargeType")).Text.ToString();

            objChargeType = new Cls_ChargeType();
            objChargeType.ID = ID;
            objChargeType.ChargeType = ChargeType;
            if (objChargeType.CheckIfChargeTypeExists().Rows.Count == 0)
            {
                objChargeType.DBOperation = DbOperations.UPDATE;
                int result = objChargeType.UpdateChargeType();

                gvChargeTypeDetails.EditIndex = -1;
                BindData();

                if (result == 1)
                    lblResult.Text = "Charge Type Updated Successfully";
                else
                    lblResult.Text = " Charge Type Updation Failed";
            }
            else
            {
                lblResult.Text = "Charge Type " + ChargeType + " already exists.";
            }
        }
        catch (Exception ex)
        { logger.Error("gvChargeTypeDetails_RowUpdating Event :"+ ex.Message); }
    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            String ChargeType = ((TextBox)gvChargeTypeDetails.FooterRow.FindControl("txtChargeType")).Text.ToString();

            objChargeType = new Cls_ChargeType();
            objChargeType.ChargeType = ChargeType;
            if (objChargeType.CheckIfChargeTypeExists().Rows.Count == 0)
            {
                objChargeType.DBOperation = DbOperations.INSERT;
                int result = objChargeType.AddChargeType();


                if (result == 1)
                    lblResult.Text = "Charge Type Added Successfully";
                else
                    lblResult.Text = " Charge Type Addition Failed";
                BindData();
            }
            else
            {
                lblResult.Text = "Charge Type " + ChargeType + " already exists.";
            }
        }
        catch (Exception ex) { logger.Error("imgbtnAdd_Click Event :"+ex.Message); }
    }

    protected void gvChargeTypeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
        { logger.Error("gvChargeTypeDetails_RowDataBound Event :"+ex.Message); }
    }
    protected void imgbtnActivate_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void gvChargeTypeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       



        try
        {
            objChargeType = new Cls_ChargeType();

            int Result = 0;

            if (e.CommandName == "Activeness")
            {

                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                int Id = Convert.ToInt16(((HiddenField)gvChargeTypeDetails.Rows[RowIndex].FindControl("hdfID")).Value.ToString());
                Boolean IsActive = Convert.ToBoolean(((HiddenField)gvChargeTypeDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
                objChargeType.ID = Id;
                objChargeType.DBOperation = DbOperations.CHANGE_ACTIVENESS;
                objChargeType.IsActive = (!IsActive);

                Result = objChargeType.SetActivenessOfChargeType();


                if (Result == 1)
                {
                    if (!IsActive)
                        lblResult.Text = "Charge Type Activated Successfully";
                    else
                        lblResult.Text = "Charge Type Deactivated Successfully";

                }
                else
                {
                    if (!IsActive)
                        lblResult.Text = "Failed to Activate the Charge Type";
                    else
                        lblResult.Text = "Failed to Deactivate the Charge Type";

                }

                BindData();
            }
        }
        catch (Exception ex)
        { logger.Error("gvChargeTypeDetails_RowCommand Event :"+ex.Message); }



    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvChargeTypeDetails.EditIndex = -1;
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnCancel_Click Event :"+ex.Message);
        }
    }

    protected void gvChargeTypeDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvChargeTypeDetails.PageIndex = e.NewPageIndex;
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("gvChargeTypeDetails_PageIndexChanging Event :"+ ex.Message);
        }
    }


    #region Functions
    private void BindData()
    {
        try
        {
            objChargeType = new Cls_ChargeType();
            DataTable dtChargeTypes = null;

            dtChargeTypes = objChargeType.GetAllChargeTypes();

            if (dtChargeTypes != null)
            {
                DataView dv = dtChargeTypes.DefaultView;
                dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
                dtChargeTypes = dv.ToTable();

                if (dtChargeTypes.Rows.Count == 0)
                {
                    RemoveConstraints(dtChargeTypes);
                    dtChargeTypes.Rows.Add(dtChargeTypes.NewRow());

                    gvChargeTypeDetails.DataSource = dtChargeTypes;
                    gvChargeTypeDetails.DataBind();

                    gvChargeTypeDetails.Rows[0].Visible = false;
                }
                else
                {
                    gvChargeTypeDetails.DataSource = dtChargeTypes;
                    gvChargeTypeDetails.DataBind();
                }

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            logger.Error("BindData Function  :" + ex.Message);
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
            logger.Error("RemoveConstraints Function :" + ex.Message);
        }
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
