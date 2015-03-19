using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DealerManager : System.Web.UI.Page
{
    Miles_Cls_Dealer Md = new Miles_Cls_Dealer();

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lblHeader = (Label)Master.FindControl("lblHeader");
        lblHeader.Text = "Dealer Manager";
        
        //GridDealer.DataSource = Md.GetDealers();
        //GridDealer.DataBind();
        Md.GetDealers2(ref GridDealer);
    }

    protected void Records_IndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridDealer_PageIndexChanged(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridDealer.PageIndex = e.NewPageIndex;
            Md.GetDealers2(ref GridDealer);
        }
        catch (Exception ex)
        {
            
        }
    }
}