using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VDT_CustomerRequestStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ((Label)this.Page.Master.FindControl("lblHeader")).Text = "Customer Request to Update Status Report";
        }
    }
}
