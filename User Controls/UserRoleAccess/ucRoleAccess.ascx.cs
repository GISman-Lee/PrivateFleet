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
using System.Text;
using System.Xml;
using Mechsoft.GeneralUtilities;

public partial class User_Controls_UserRoleAccess_ucRoleAccess : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_UserRoleAccess_ucRoleAccess));

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // add javascript for treeview control
                //tvSHAvailable.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");
                tvSHAvailable.Attributes.Add("onclick", "OnTreeClick(event)");

                //fill pages dropdown
                FillRoles();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page Load Event : " + ex.Message);
        }
    }
    #endregion

    #region "Methods"

    /// <summary>
    /// Method to fill roles dropdown
    /// </summary>
    private void FillRoles()
    {
        logger.Debug("FillPages Method Start");
        Cls_Role objRole = new Cls_Role();
        try
        {
            //get all pages
            DataTable dt = objRole.GetAllActiveRoles();

            //clear pages dropdown
            ddlRoles.Items.Clear();

            //fill pages dropdown
            ddlRoles.DataSource = dt;
            ddlRoles.DataTextField = "role";
            ddlRoles.DataValueField = "id";
            ddlRoles.DataBind();

            //insert default item in pages dropdown
            ddlRoles.Items.Insert(0, new ListItem("-Select-", "0"));
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
    /// fill treewiew control with the pages and actions
    /// </summary>
    /// /// <summary>
    /// fill treewiew control with the pages and actions
    /// </summary>
    private void FillTree()
    {
        lblMsg.Text = "";

        Cls_Page objPage = new Cls_Page();
        Cls_PageActionDetails objMapping = new Cls_PageActionDetails();

        DataSet ds = new DataSet();
        DataTable dtParent = null;
        DataTable dtChild = null;
        DataTable dtActions = null;
        DataTable dtActAccess = null;
        
        try
        {
            Cls_Access objAccess = new Cls_Access();
            Cls_ActionAccess objActAccess = new Cls_ActionAccess();

            objAccess.AccessFor = Convert.ToInt32(ddlRoles.SelectedValue);
            objAccess.AccessTypeId = 1;
            DataTable dtAccess = objAccess.GetPageAccess();

            tvSHAvailable.Visible = true;
            
            DataTable dtPages = objPage.GetAllActivePages();
            DataView dv = dtPages.DefaultView;

            //Add datatable containing parent menu items
            dv.RowFilter = "ParentID = 0";
            ds.Tables.Add(dv.ToTable());

            //Add datatable containing child menu items
            dv = dtPages.DefaultView;
            dv.RowFilter = "ParentID <> 0";
            ds.Tables.Add(dv.ToTable());

            //clear existing items
            tvSHAvailable.Nodes.Clear();

            dtParent = ds.Tables[0];
            foreach (DataRow dr in dtParent.Rows)
            {
                //Add parent node to treeview
                TreeNode rootNode = new TreeNode();
                
                rootNode.Text = dr["PageName"].ToString();
                rootNode.Value = dr["ID"].ToString();
                rootNode.NavigateUrl = "";

                //check if logged in user-role has access to this node or not
                DataView dvAccess = dtAccess.DefaultView;
                dvAccess.RowFilter = "ID = " + rootNode.Value;
                if (dvAccess.ToTable().Rows.Count > 0)
                    rootNode.Checked = true;
                else
                    rootNode.Checked = false;

                //Get datatable containing child pages for this root node
                dv = ds.Tables[1].DefaultView;
                dv.RowFilter = "ParentID = " + rootNode.Value;
                dtChild = dv.ToTable();

                foreach (DataRow drChild in dtChild.Rows)
                {
                    dtActAccess = null;

                    TreeNode childNode = new TreeNode();
                    childNode.Text = drChild["PageName"].ToString();
                    childNode.Value = drChild["ID"].ToString();
                    childNode.NavigateUrl = "";

                    childNode.Checked = false;
                    if (dtAccess != null)
                    {
                        //check if logged in user-role has access to this node or not
                        dvAccess = dtAccess.DefaultView;
                        dvAccess.RowFilter = "ID = " + childNode.Value;
                        if (dvAccess.ToTable().Rows.Count > 0)
                        {
                            childNode.Checked = true;

                            //get action access information
                            objActAccess.AccessId = Convert.ToInt32(dvAccess.ToTable().Rows[0]["AccessID"]);
                            dtActAccess = objActAccess.GetActionAccess();
                        }
                    }

                    rootNode.ChildNodes.Add(childNode);

                    //Get datatable containing active actions mapped to this page
                    objMapping.PageId = Convert.ToInt32(childNode.Value);
                    dtActions = objMapping.GetActivePageActions();

                    foreach (DataRow drAction in dtActions.Rows)
                    {
                        TreeNode actionNode = new TreeNode();
                        actionNode.Text = drAction["Action"].ToString();
                        actionNode.Value = drAction["ActionID"].ToString();
                        actionNode.NavigateUrl = "";

                        actionNode.Checked = false;
                        if (dtActAccess != null)
                        {
                            //check if logged in user-role has access to this node or not
                            DataView dvActAccess = dtActAccess.DefaultView;
                            dvActAccess.RowFilter = "ActionID = " + actionNode.Value;
                            if (dvActAccess.ToTable().Rows.Count > 0)
                                actionNode.Checked = true;
                        }

                        childNode.ChildNodes.Add(actionNode);
                    }
                }
                tvSHAvailable.Nodes.Add(rootNode);
            }
            tvSHAvailable.ExpandAll();
        }
        catch (Exception ex)
        {
            throw new Exception("FillTree Method : " + ex.Message);
        }
        finally
        {
        }
    }

    public XmlDocument ConvertDataTableToXML(DataTable dtSelected)
    {

        XmlDocument _XMLDoc = new XmlDocument();

        DataSet ds = new DataSet("Selectedds");
        DataTable dt = new DataTable("Selecteddt");

        dt = dtSelected;
        ds.Tables.Add(dt);

        _XMLDoc.LoadXml(ds.GetXml());
        return _XMLDoc;
    } 

    #endregion

    #region "Events"

    protected void ibtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        Cls_Access objAccess = new Cls_Access();
        try
        {
            //build datatable
            DataTable dtSelected = new DataTable();
            dtSelected.Columns.Add("XMLPageID");
            dtSelected.Columns.Add("ActionID");

            //iterate through parent pages
            foreach (TreeNode rootNode in tvSHAvailable.Nodes)
            {
                if (rootNode.Checked)
                {
                    DataRow drParent = dtSelected.NewRow();
                    drParent["XMLPageID"] = rootNode.Value;
                    drParent["ActionID"] = 0;

                    dtSelected.Rows.Add(drParent);
                }

                //if parent page has child pages
                if (rootNode.ChildNodes.Count > 0)
                {
                    //iterate through child pages
                    foreach (TreeNode childNode in rootNode.ChildNodes)
                    {
                        if (childNode.Checked)
                        {
                            DataRow drChild = dtSelected.NewRow();
                            drChild["XMLPageID"] = childNode.Value;
                            drChild["ActionID"] = 0;

                            dtSelected.Rows.Add(drChild);
                        }

                        //if child page has actions
                        if (childNode.ChildNodes.Count > 0)
                        {
                            //iterate through actions
                            foreach (TreeNode actionNode in childNode.ChildNodes)
                            {
                                if (actionNode.Checked)
                                {
                                    DataRow drAction = dtSelected.NewRow();
                                    drAction["XMLPageID"] = childNode.Value;
                                    drAction["ActionID"] = actionNode.Value;

                                    dtSelected.Rows.Add(drAction);
                                }
                            }
                        }
                        
                    }
                }
            }

            objAccess.AccessFor = Convert.ToInt32(ddlRoles.SelectedValue);
            objAccess.AccessTypeId = 1;
            objAccess.XmlDocument = ConvertDataTableToXML(dtSelected).InnerXml;
            objAccess.SetAccess();

            Session["dtPages"] = null;
            Response.Redirect("RoleAccess.aspx");
        }
        catch (Exception ex)
        {
            logger.Error("ibtnSubmit_Click Event : " + ex.Message);
        }
        finally
        {
        }
    }
    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRoles.SelectedValue != "0")
            {
                tblAccess.Visible = true;
                FillTree();
            }
            else
                tblAccess.Visible = false;
        }
        catch (Exception ex)
        {
            logger.Error("ddlRoles_SelectedIndexChanged Event : " + ex.Message);
        }
    }
    #endregion
}
