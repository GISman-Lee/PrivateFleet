using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using Mechsoft.GeneralUtilities;

public partial class AdminAnnouncement : System.Web.UI.Page
{
    #region Variables

    static ILog logger = LogManager.GetLogger(typeof(AdminAnnouncement));

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblAdminAnnMsg.Visible = false;
                Label lblHeader = (Label)Master.FindControl("lblHeader");
                if (lblHeader != null)
                    lblHeader.Text = "Admin Announcement";
                //lblMsg.Text = lblMsg.Text.Replace("\\","");

                GetCurrentAnnouncement();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Admin Announcement err - " + ex.Message);
        }
    }

    #endregion

    #region Methods

    private void GetCurrentAnnouncement()
    {
        Cls_AdminAnnouncement objAnnouncement = new Cls_AdminAnnouncement();
        DataTable dtAnnouncement = new DataTable();
        try
        {
            dtAnnouncement = objAnnouncement.GetAdminAnnouncement();
            if (dtAnnouncement != null && dtAnnouncement.Rows.Count == 1)
            {
                txtAnn.Text = Convert.ToString(dtAnnouncement.Rows[0]["PanelData"]);
                ViewState["Announcement"] = dtAnnouncement;
            }

        }
        catch (Exception ex)
        {
            logger.Error("GetCurrentAnnouncement err - " + ex.Message);
        }
        finally
        {
            dtAnnouncement = null;
            objAnnouncement = null;
        }
    }

    #endregion

    #region Events

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        Cls_AdminAnnouncement objAnnouncement = new Cls_AdminAnnouncement();
        DataTable dtAnnouncement = new DataTable();
        try
        {
            dtAnnouncement = (DataTable)ViewState["Announcement"];
            if (dtAnnouncement != null && Convert.ToString(dtAnnouncement.Rows[0]["PanelData"]) == txtAnn.Text.Trim())
            {
                lblAdminAnnMsg.Visible = true;
                lblAdminAnnMsg.Text = "You are trying to save same text.";
                return;
            }
            objAnnouncement.PanelID = "lblDealerIntro";
            objAnnouncement.PanelDate = txtAnn.Text.Trim();
            objAnnouncement.CreatedBy = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
            int result = objAnnouncement.SaveAdminAnnouncement();
            if (result > 0)
            {
                lblAdminAnnMsg.Visible = true;
                lblAdminAnnMsg.Text = "Announcement save successfully.";
                GetCurrentAnnouncement();
            }
            else
            {
                lblAdminAnnMsg.Visible = true;
                lblAdminAnnMsg.Text = "Error while save. Try again";
            }
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnCancel_Click err - " + ex.Message);
        }
        finally
        {

        }
    }
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblAdminAnnMsg.Visible = false;
            GetCurrentAnnouncement();

        }
        catch (Exception ex)
        {
            logger.Error("imgbtnCancel_Click err - " + ex.Message);
        }
        finally
        {

        }
    }


    #endregion

}
