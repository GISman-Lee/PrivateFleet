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
using log4net;
using AccessControlUnit;

public partial class User_Controls_ucPageAction : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_ucPageAction));

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //fill pages dropdown
                FillPages();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page Load Event : "+ex.Message);
        }
    } 
    #endregion

    #region "Methods"

    /// <summary>
    /// Method to fill pages dropdown
    /// </summary>
    private void FillPages()
    {
        logger.Debug("FillPages Method Start");
        Cls_Page objPage = new Cls_Page();
        try
        {
            //get all pages
            DataTable dt = objPage.GetAllActivePages();

            //clear pages dropdown
            ddlPages.Items.Clear();

            //fill pages dropdown
            ddlPages.DataSource = dt;
            ddlPages.DataTextField = "pagename";
            ddlPages.DataValueField = "id";
            ddlPages.DataBind();

            //insert default item in pages dropdown
            ddlPages.Items.Insert(0, new ListItem("-Select-","0"));
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            logger.Debug("FillPages Method End");
        }
    }

    /// <summary>
    /// Bind page actions to grid
    /// </summary>
    private void BindPageActions()
    {
        logger.Debug("BindPageActions Method Start");
        Cls_PageActionDetails objMapping = new Cls_PageActionDetails();
        Cls_Action objAction = new Cls_Action();
        try
        {
            ViewState["dtMapping"] = null;

            //get actions for selected page
            objMapping.PageId = Convert.ToInt32(ddlPages.SelectedValue);
            DataTable dtMapping = objMapping.GetActivePageActions();

            if (dtMapping.Rows.Count > 0)
                ViewState["dtMapping"] = dtMapping;

            //get active actions
            DataTable dt = objAction.GetAllActiveActions();

            //bind actions to grid
            gvActions.DataSource = dt;
            gvActions.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            logger.Debug("BindPageActions Method End");
        }
    }

    #endregion

    #region "Events"

    protected void ibtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        Cls_PageActionDetails objMapping = new Cls_PageActionDetails();
        try
        {
            CheckBox chkSelect = null;
            string strSelected = "";
            foreach (GridViewRow gvRow in gvActions.Rows)
            {
                chkSelect = (CheckBox)gvRow.FindControl("chkSelect");

                if (chkSelect.Checked)
                    strSelected += gvActions.DataKeys[gvRow.RowIndex].Values["ID"].ToString() + ",";
            }

            if (strSelected != "")
                strSelected = strSelected.Remove(strSelected.Length - 1);

            //set properties
            objMapping.PageId = Convert.ToInt32(ddlPages.SelectedValue);
            objMapping.ActionIds = strSelected;
            objMapping.IsActive = 1;

            //save page-action mapping
            objMapping.Save();

            ViewState["dtMapping"] = null;

            //bind grid data
            BindPageActions();
        }
        catch (Exception ex)
        {
            logger.Error("ibtnSubmit_Click Event : " + ex.Message);
        }
        finally
        {
            objMapping = null;
        }
    }
    protected void ddlPages_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPages.SelectedValue != "0")
            {
                tblActions.Visible = true;
                BindPageActions();
            }
            else
                tblActions.Visible = false;
        }
        catch (Exception ex)
        {
            logger.Error("ddlPages_SelectedIndexChanged Event : " + ex.Message);
        }
    }
    protected void gvActions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkSelect = null;
            try
            {
                if (ViewState["dtMapping"] != null)
                {
                    DataTable dtMapping = (DataTable)ViewState["dtMapping"];
                    int intID = Convert.ToInt32(gvActions.DataKeys[e.Row.RowIndex].Values["ID"]);

                    DataView dv = dtMapping.DefaultView;
                    dv.RowFilter = "ActionID = "+intID;

                    chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                    if (dv.ToTable().Rows.Count > 0)
                        chkSelect.Checked = true;
                    else
                        chkSelect.Checked = false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("gvActions_RowDataBound Event : " + ex.Message);
            }
        }
    }
    #endregion
}
