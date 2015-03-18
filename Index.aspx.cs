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
