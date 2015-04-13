using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class User_Controls_UC_VDT_DealerReports : System.Web.UI.UserControl
{
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UC_VDT_DealerReports));
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["type"] != null)
                {


                    switch (Convert.ToString(Request.QueryString["type"]))
                    {
                        case "1":
                            drpReportType.SelectedValue = "1";
                            drpReportType_SelectedIndexChanged(null, null);
                            break;

                        case "2":
                            drpReportType.SelectedValue = "2";
                            drpReportType_SelectedIndexChanged(null, null);
                            break;
                        case "3":
                            drpReportType.SelectedValue = "3";
                            drpReportType_SelectedIndexChanged(null, null);
                            break;
                        case "4":
                            drpReportType.SelectedValue = "4";
                            drpReportType_SelectedIndexChanged(null, null);
                            break;

                    }
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }
    public void drpReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string value = "";
            value = drpReportType.SelectedValue.ToString();
            pnlDelivaryReport.Visible = false;
            pnlDealerResponseReport.Visible = false;
            pnlClientRequest.Visible = false;
            pnlETA.Visible = false;
            switch (value)
            {
                case "1":
                    ucVechileDelivaryReport.ResetSearchCriteria();
                    pnlDelivaryReport.Visible = true;
                    break;
                case "2":
                    ucDealerResponseReport.ResetSearchCriteria();
                    pnlDealerResponseReport.Visible = true;
                    break;
                case "3":
                    Uc_Client_Request.ResetSearchCriteria();
                    pnlClientRequest.Visible = true;
                    break;
                case "4":
                    ucETA.ResetSearchCriteria();
                    pnlETA.Visible = true;
                    break;

            }
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }
}
