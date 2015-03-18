using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VDT_CustomerHelp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ((Label)this.Page.Master.FindControl("lblHeader")).Text = "Vehicle Delivery Tracking Report";
            if (Convert.ToString(Request.QueryString["type"]) != "")
            {
                drpReportType.SelectedValue = Convert.ToString(Request.QueryString["type"]);
                drpReportType_SelectedIndexChanged(null, null);
            }
        }
       
    }
    public void drpReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlCustomerHelp.Visible = false;
        pnlAutomaticMailReport.Visible = false;
        pnlDealerResponseReport.Visible = false;
        PnlCustomerReport.Visible = false;
        pnlDealerCustomerCount.Visible = false;
        pnDrasticChangeETA.Visible = false;
        switch (Convert.ToString(drpReportType.SelectedValue))
        {
            case "1":
                ucCustomerHelp.resetSeachCriterial();
                pnlCustomerHelp.Visible = true;
                //ImageButton btn = (ImageButton)ucCustomerHelp.FindControl("btnGenerateReport");
                //pnlCustomerHelp.DefaultButton = btn.ID;
                break;
            case "2":
                UC_VDTAutomaticMail.ResetSearchCriteria();
                pnlAutomaticMailReport.Visible = true;
                break;
            case "3":
                ucDealerResponse.ResetSearchCriteria();
                pnlDealerResponseReport.Visible = true;
                break;
            case "4":
                UC_VDT_Customer.ResetSearchCriteria();
                PnlCustomerReport.Visible = true;
                break;
            case "5":
                UC_VDTDealerCustomer.ResetSearchCriteria();
                pnlDealerCustomerCount.Visible = true;
                break;
            case "6":
                uc_DrasticChangeETA.ResetSearchCriteria();
                pnDrasticChangeETA.Visible = true;
                uc_DrasticChangeETA.bindData();
              
                break;
        }
    }
}
