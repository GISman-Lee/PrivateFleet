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
using Mechsoft.FleetDeal;

public partial class User_Controls_Request_ucCustomerDetails : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void DisplayCustomerInfo(int CustomerId)
    {
        try
        {
            Cls_CustomerMaster objCustomer = new Cls_CustomerMaster();
            objCustomer.ID = CustomerId;
            DataList1.DataSource = objCustomer.GetCustomerBasicInfo();
            DataList1.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
