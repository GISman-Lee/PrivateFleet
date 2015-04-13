using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VDT_Customer_User_Controls_UC_VDT_AdminDashboardl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Binding data for Dealer Summary Report
            //DropDownList ddlMake = (DropDownList)UCDealerOrer.FindControl("drpMake");
            //DropDownList ddlCompany = (DropDownList)UCDealerOrer.FindControl("ddlCompany");
            //int makeid = 0;
            //string ComapanyName = null;
            //UCDealerOrer.BindCompanies();
            //UCDealerOrer.bindDropDown();
            //if (Convert.ToString(ddlCompany.SelectedItem.Text).Equals("ALL"))
            //{
            //}
            //else
            //{
            //    ComapanyName = Convert.ToString(ddlCompany.SelectedItem.Text.Trim());
            //}
            //if (Convert.ToString(ddlMake.SelectedItem.Text.Trim()).Equals("ALL"))
            //{
            //}
            //else
            //{
            //    makeid = Convert.ToInt32(ddlMake.SelectedItem.Value.Trim());
            //}
            //UCDealerOrer.bindData(makeid, ComapanyName);
            #endregion
        }
    }
}
