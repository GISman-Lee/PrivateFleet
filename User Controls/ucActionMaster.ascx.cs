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
using AccessControlUnit;
using log4net;

public partial class User_Controls_ucActionMaster : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_ucActionMaster));

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Event : " + ex.Message);
        }
    }
    #endregion

    #region "Methods"
    private void BindData()
    {
        DataTable dt = null;
        Cls_Action objAction = new Cls_Action();

        try
        {
            //Get all Actions from database
            dt = objAction.Get();

            //if datatable is not empty
            if (dt.Rows.Count > 0)
            {
                //show footer row
                if (gvActions.EditIndex == -1)
                    gvActions.ShowFooter = true;

                //Bind grid data
                gvActions.DataSource = dt;
                gvActions.DataBind();
            }
            else
            {
                //add blank row
                foreach (DataColumn Dc in dt.Columns)
                {
                    Dc.ReadOnly = false;
                    Dc.AllowDBNull = true;
                }
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);

                //show footer row
                if (gvActions.EditIndex == -1)
                    gvActions.ShowFooter = true;

                //Bind grid data
                gvActions.DataSource = dt;
                gvActions.DataBind();

                //hide blank row
                gvActions.Rows[0].Visible = false;
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            objAction = null;
            dt = null;
        }
    }
    #endregion

    #region "Events"
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Make grid non-editable
            gvActions.EditIndex = -1;

            //bind grid data
            BindData();

            //show footer row
            gvActions.ShowFooter = true;
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnCancel_Click" + ex.Message);
        }
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        Cls_Action objAction = new Cls_Action();
        try
        {
            TextBox txtAction = (TextBox)gvActions.FooterRow.FindControl("txtAddAction");
            TextBox txtDesc = (TextBox)gvActions.FooterRow.FindControl("txtAddDesc");

            if (txtAction != null)
                objAction.Action = txtAction.Text;

            if (txtDesc != null)
                objAction.Description = txtDesc.Text;

            objAction.IsActive = 1;

            //add Action
            int lastInsertId = objAction.Add();

            if (lastInsertId > 0)
                lblMsg.Text = "Action saved successfully";
            else
                lblMsg.Text = "Error Occured.. Please try again";

            //bind grid data
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnAdd_Click" + ex.Message);
        }
        finally
        {
            objAction = null;
        }
    }
    protected void gvActions_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            //set new edit index for edit mode
            gvActions.EditIndex = e.NewEditIndex;

            //hide footer row
            gvActions.ShowFooter = false;

            //bind grid data
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("gvActions_RowEditing" + ex.Message);
        }
    }
    protected void gvActions_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Cls_Action objAction = new Cls_Action();
        try
        {
            int intActionId = Convert.ToInt32(gvActions.DataKeys[e.RowIndex].Values["ID"]);
            TextBox txtAction = (TextBox)gvActions.Rows[e.RowIndex].FindControl("txtEditAction");
            TextBox txtDesc = (TextBox)gvActions.Rows[e.RowIndex].FindControl("txtEditDesc");
            HiddenField hdfIsActive = (HiddenField)gvActions.Rows[e.RowIndex].FindControl("hdfIsActive");            

            //Action id
            objAction.Id = intActionId;

            //Action name
            if (txtAction != null)
                objAction.Action = txtAction.Text;

            //Action description
            if (txtDesc != null)
                objAction.Description = txtDesc.Text;

            //Action status
            if (hdfIsActive != null)
                objAction.IsActive = Convert.ToInt32(Convert.ToBoolean(hdfIsActive.Value));

            //update Action
            bool result = objAction.Update();

            if (result)
                lblMsg.Text = "Action updated successfully";
            else
                lblMsg.Text = "Error Occured.. Please try again";

            //make grid non-editable
            gvActions.EditIndex = -1;

            //bind grid data
            BindData();

        }
        catch (Exception ex)
        {
            logger.Error("gvActions_RowUpdating" + ex.Message);
        }
        finally
        {
            objAction = null;
        }
    }
    protected void gvActions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Cls_Utilities objUtility = new Cls_Utilities();
        try
        {
            if (e.CommandName == "Activeness")
            {
                //get rowindex of the record
                int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
                RowIndex = RowIndex - (gvActions.PageIndex * gvActions.PageSize);

                bool IsActive = Convert.ToBoolean(((HiddenField)gvActions.Rows[RowIndex].FindControl("hdfIsActive")).Value);
                int ID = Convert.ToInt16(gvActions.DataKeys[RowIndex][0].ToString());

                bool Result = false;

                if (IsActive)
                {
                    //deactivate recrod
                    Result = objUtility.DeActivate(ID, "ACU_ActionMaster");

                    if (Result)
                        lblMsg.Text = "Action Deactivated successfully";
                    else
                        lblMsg.Text = "Failed to Deactivate the Action";
                }
                else
                {
                    //activate record
                    Result = objUtility.Actiavte(ID, "ACU_ActionMaster");

                    if (Result)
                        lblMsg.Text = "Action Activated Successfully";
                    else
                        lblMsg.Text = "Failed to Activate the Action";
                }

                //bind grid data
                BindData();
            }
        }
        catch (Exception ex)
        {
            logger.Error("gvActions_RowCommand Event : " + ex.Message);
        }
        finally
        {
            objUtility = null;
        }
    }
    protected void gvActions_RowDataBound(object sender, GridViewRowEventArgs e)
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
                logger.Error("gvActions_RowDataBound Event : "+ex.Message);
            }
        }
    }
    protected void gvActions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //set new page index
            gvActions.PageIndex = e.NewPageIndex;

            //bind grid data
            BindData();
        }
        catch (Exception ex)
        {
            logger.Error("gvActions_PageIndexChanging Event : "+ex.Message);
        } 
    }
    #endregion
}
