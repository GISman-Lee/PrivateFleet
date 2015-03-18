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
using Mechsoft.GeneralUtilities;
using AccessControlUnit;
using log4net;
using System.Text;
using System.Windows.Forms;

public partial class index_1 : System.Web.UI.Page
{
    string username, password;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["username"] != null && Request.QueryString["password"] != null)
            {
                string username = Convert.ToString(Request.QueryString["username"]);
                string password = Convert.ToString(Request.QueryString["password"]);

            }
        }

    }

    public void chkLogin()
    { 
        Cls_Login objLogIn = new Cls_Login();
        try
        {
            objLogIn.UserName = username;
            objLogIn.Password = password;
            DataTable dtInfo = objLogIn.ValidateLogIn();
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    Session[Cls_Constants.LOGGED_IN_USERID] = dtInfo.Rows[0]["ID"].ToString();
                    Session[Cls_Constants.USER_NAME] = dtInfo.Rows[0]["UserName"].ToString();
                    Session[Cls_Constants.ROLE_ID] = dtInfo.Rows[0]["RoleID"].ToString();
                    if (Convert.ToInt32(dtInfo.Rows[0]["RoleID"].ToString()) == 2)
                    {
                        //string DMake = "";
                        //for (int i = 0; i < dtInfo.Rows.Count; i++)
                        //    DMake = DMake + " - " + dtInfo.Rows[i]["Make"].ToString();
                        //Session[Cls_Constants.Role_Name] = dtInfo.Rows[0]["Role"].ToString() + DMake;
                        Session[Cls_Constants.Role_Name] = dtInfo.Rows[0]["Role"].ToString() + " - " + dtInfo.Rows[0]["Make"].ToString();
                    }
                    else
                    {
                        Session[Cls_Constants.Role_Name] = dtInfo.Rows[0]["Role"].ToString();
                    }


                    Response.Redirect("index_1.aspx", true);
                }
                else
                {
                    Response.Redirect("Welcome.aspx", true);
                }
            }
            else
            {
                Response.Redirect("Welcome.aspx", true);
            }
        }
        catch (Exception ex)
        {
           
        }
    }
}
