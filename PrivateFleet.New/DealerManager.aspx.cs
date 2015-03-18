using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DealerManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label lblHeader = (Label)Master.FindControl("lblHeader");
        lblHeader.Text = "Dealer Manager";
    }
}