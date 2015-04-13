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
using Mechsoft.GeneralUtilities;

public partial class Index : System.Web.UI.Page
{
    ILog logger = LogManager.GetLogger(typeof(Index));

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["frm"] != null && Request.QueryString["q"] != null && Request.QueryString["p"] != null)
            {
                string ReqFrom = Cls_Encryption.DecryptTripleDES(Request.QueryString["frm"]);
                if (ReqFrom.ToLower() == "pfsales")
                {
                    MovetoCreatQuote();
                    return;
                }
            }

            Title = "Private Fleet Login";
            if (Request.QueryString != null)
            {
                if (Convert.ToString(Request.QueryString["id"]) == "4got")
                {
                    Title = "Private Fleet Get Password";
                }
            }
        }
        if (Session["tempURL"] != null)
        {
            logger.Error(Convert.ToString(Session["tempURL"]));
            if (Session[Cls_Constants.USER_NAME] != null)
                Response.Redirect(Convert.ToString(Session["tempURL"]));
        }
    }

    private void MovetoCreatQuote()
    {
        Cls_Login cls_Login = new Cls_Login();
        try
        {
            //User ID
            cls_Login.ID = Cls_Encryption.DecryptTripleDES(Request.QueryString["q"]).ToString();
            DataTable dtLogin = cls_Login.getUserDetailsFromID();
            if (dtLogin != null && dtLogin.Rows.Count == 1)
            {
                ((TextBox)UcLogin1.FindControl("txtUserName")).Text = Convert.ToString(dtLogin.Rows[0]["Username"]);
                ((TextBox)UcLogin1.FindControl("txtPassword")).Text = Convert.ToString(dtLogin.Rows[0]["Password"]);
                UcLogin1.imgbtnAdd_Click(null,null);
            }
        }
        catch (Exception ex)
        {
            logger.Error("Index MovetoCreatQuote error = " + ex.Message);
        }
    }

    //removing the session of Quote Request Page
    public void RemoveSessions()
    {
        //remove session of QR_1
        Session.Remove("Make_Model_Series");
        Session.Remove("ConsultantNotes");
        Session.Remove("chkBox");
        Session.Remove("dtParameters");
        Session.Remove("SELECT_ACC");
        // Session.Remove("dtAccessories");

        //remove Session of QR_2
        Session.Remove("PCode_Suburb");
        Session.Remove("dtAllDealers");
        Session.Remove("DEALER_SELECTED");
    }
}
