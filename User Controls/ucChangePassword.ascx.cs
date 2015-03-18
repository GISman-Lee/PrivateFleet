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
using AccessControlUnit;
using Mechsoft.FleetDeal;
using Mechsoft.GeneralUtilities;
using log4net;

public partial class User_Controls_ucChangePassword : System.Web.UI.UserControl
{

    ILog logger = LogManager.GetLogger(typeof(User_Controls_ucChangePassword));
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void imgbtnChangePassword_Click(object sender, ImageClickEventArgs e)
    {

        Page.Validate("VGSubmit");
        Cls_UserMaster objDealerUser = new Cls_UserMaster();
        if (Page.IsValid)
        {
            logger.Debug("Event Start :imgbtnChangePassword_Click");
            try
            {

                Cls_User objUser = new Cls_User();
                objUser.NewPassword = txtPassword.Text;
                objUser.OldPassword = txtoldPassword.Text;
                objUser.Id = Convert.ToInt16(Session[Cls_Constants.LOGGED_IN_USERID].ToString());

                objDealerUser.Password = txtPassword.Text;
                bool ExistsResult = objDealerUser.CheckPasswordExists();

                if (ExistsResult)
                {
                    int Result = objUser.ChangePassword();

                    if (Result > 0)
                        lblError.Text = "Password Changed Successfully";
                    else
                        lblError.Text = "Please enter the correct current password";
                }
                else
                {
                    lblError.Text = "User Password Already Exists. Try again...";
                }
            }
            catch (Exception Ex)
            {
                logger.Error("imgbtnChangePassword_Click :" + Ex.Message);
            }
           
        }
    }
}
