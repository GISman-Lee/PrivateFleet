using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mechsoft.GeneralUtilities;
using log4net;


public partial class Masters_EndSession : System.Web.UI.Page
{
    ILog logger = LogManager.GetLogger(typeof(Masters_EndSession));

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session[Cls_Constants.LOGGED_IN_USERID] = Session[Cls_Constants.LOGGED_IN_USERID];
        //Session[Cls_Constants.USER_NAME] = Session[Cls_Constants.USER_NAME];
        //Session[Cls_Constants.ROLE_ID] = Session[Cls_Constants.ROLE_ID];
        //Session[Cls_Constants.FromEmailID] = Session[Cls_Constants.FromEmailID];
        //Session[Cls_Constants.CONSULTANT_NAME] = Session[Cls_Constants.CONSULTANT_NAME];
        //Session[Cls_Constants.PHONE] = Session[Cls_Constants.PHONE];
        //Session[Cls_Constants.Role_Name] = Session[Cls_Constants.Role_Name];

        Response.ContentType = "text/xml";
        Response.Write("Session Updated - Server Time: " + DateTime.Now.ToString());
        // logger.Error("Session Updated - Server Time: " + DateTime.Now.ToString());

        //////if (User.Identity.IsAuthenticated == true)
        //////{
        //////    FormsAuthentication.SignOut();
        //////}
        //////Session.Abandon();
        //////Response.Redirect("../Default.aspx");
    }
}
