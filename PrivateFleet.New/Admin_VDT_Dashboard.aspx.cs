using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_VDT_Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Request.QueryString.ToString()) == "")
        {
            Response.Redirect("Admin_VDT_Dashboard.aspx?showadjustment=yes");
        }
        if (!Page.IsPostBack)
        {
            ((Label)this.Page.Master.FindControl("lblHeader")).Text = "Delivery Update Tracker DashBoard";
        }
    }
}
