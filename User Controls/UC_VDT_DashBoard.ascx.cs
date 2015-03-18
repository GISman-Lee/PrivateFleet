using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
public partial class User_Controls_UC_VDT_DashBoard : System.Web.UI.UserControl
{
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UC_VDT_DashBoard));
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToString(Request.QueryString.ToString()) == "")
                {
                    Response.Redirect("~/VDT_Dashboard.aspx?ShowAdjustment=yes");
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }

    }
  
}
