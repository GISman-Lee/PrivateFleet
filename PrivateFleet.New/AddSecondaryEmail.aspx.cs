using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mechsoft.GeneralUtilities;

public partial class AddSecondaryEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ((Label)this.Master.FindControl("lblHeader")).Text = "Add Secondary Email";
            BindExistingMail();

        }
    }

    #region Events
    protected void imgbtnSubmit_Click(object sender, ImageClickEventArgs e)
    {
        Cls_AddSecondaryEmail objSecondaryEmail = new Cls_AddSecondaryEmail();
        objSecondaryEmail.UserEmail = Convert.ToString(txtSecondaryMail.Text);
        objSecondaryEmail.UserID = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
        int result = objSecondaryEmail.SaveSecondaryEmail();
        if (result > 0)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Email Saved Successfully";
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Error while saving. Try again...";
        }
        BindExistingMail();
    }

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {

        txtSecondaryMail.Visible = true;
        txtSecondaryMail.Text = lblSecondaryMail_1.Text;
        lblSecondaryMail_1.Visible = false;
        lnkUpdate.Visible = false;
        imgbtnSubmit.Visible = true;
    }
    #endregion

    #region Methods
    private void BindExistingMail()
    {
        Cls_AddSecondaryEmail objSecondaryEmail = new Cls_AddSecondaryEmail();
        objSecondaryEmail.UserID = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);
        string Email = objSecondaryEmail.BindExistingMail();
        if (Email.Equals(String.Empty))
        {
            txtSecondaryMail.Visible = true;
            lblSecondaryMail_1.Visible = false;
            lnkUpdate.Visible = false;
        }
        else
        {
            txtSecondaryMail.Visible = false;
            imgbtnSubmit.Visible = false;
            lblSecondaryMail_1.Visible = true;
            lblSecondaryMail_1.Text = Email;
            lnkUpdate.Visible = true;
        }
    }
    #endregion
}
