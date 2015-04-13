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

public partial class VDT_Customer_User_Controls_UC_VDT_Loginl : System.Web.UI.UserControl
{
    static ILog logger = LogManager.GetLogger(typeof(VDT_Customer_User_Controls_UC_VDT_Loginl));


    protected void Page_Load(object sender, EventArgs e)
    {
        progressBackgroundFilter12.Visible = false;
        PanLogin.Visible = true;

        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
            {
                if (Request.QueryString["id"].Equals("4got"))
                {
                    MultiViewLogin.SetActiveView(ViewForgotPass);
                    lblMsg.Text = "";
                    txtFEmail.Text = "";
                    GetAllActiveRoles();
                    ddl_Role.Focus();

                    ddl_make.Visible = false;
                    lbl_make.Visible = false;
                    Label3.Visible = false;
                    txtFEmail.Visible = false;
                }
                else
                {


                    MultiViewLogin.SetActiveView(ViewLogin);
                    txtUserName.Focus();
                }
            }
            else
            {
                MultiViewLogin.SetActiveView(ViewLogin);
                txtUserName.Focus();
            }

            // Password remember
            if (Request.Cookies["VDTUName"] != null)
                txtUserName.Text = Request.Cookies["VDTUName"].Value;
            if (Request.Cookies["VDTPwd"] != null)
                txtPassword.Attributes.Add("value", Request.Cookies["VDTPwd"].Value);

            if (Request.Cookies["VDTUName"] != null && Request.Cookies["VDTPwd"] != null)
                chkRememberMe.Checked = true;
            txtUserName.Focus();


        }
    }

    private void GetAllActiveRoles()
    {
        try
        {
            Cls_Login objLogin = new Cls_Login();
            DataTable dtAllRoles = objLogin.GetAllActiveRolesForLogin();
            ddl_Role.DataSource = dtAllRoles;
            ddl_Role.DataTextField = "Role";
            ddl_Role.DataValueField = "ID";
            ddl_Role.DataBind();

            //ddl_Role.Items.RemoveAt(0);
            ddl_Role.Items.Insert(0, new ListItem("- Select Role -", "0"));
        }
        catch (Exception Ex)
        {
            logger.Error("GetAllActiveRoles Function :" + Ex.Message);
        }
    }

    private void BindDropDown()
    {
        try
        {
            Cls_MakeHelper objMake = new Cls_MakeHelper();
            DataTable dtActiveMakes = objMake.GetActiveMakes();

            ddl_make.DataSource = dtActiveMakes;
            ddl_make.DataTextField = "Make";
            ddl_make.DataValueField = "ID";
            ddl_make.DataBind();

            ddl_make.Items.Insert(0, new ListItem("- Select Make -", "0"));
        }
        catch (Exception ex) { logger.Error("BindDropDown Functin :" + ex.Message); }

    }

    protected void ddl_Role_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (ddl_Role.SelectedItem.ToString() == "Dealer")
        {
            Label3.Visible = true;
            txtFEmail.Visible = true;
            txtFEmail.Text = "";
            lbl_make.Visible = true;
            ddl_make.Visible = true;
            BindDropDown();
        }
        else if (ddl_Role.SelectedItem.ToString() == "Consultant" || ddl_Role.SelectedItem.ToString() == "Admin")
        {
            Label3.Visible = true;
            txtFEmail.Visible = true;
            txtFEmail.Text = "";
            lbl_make.Visible = false;
            ddl_make.Visible = false;
        }
        else
        {
            ddl_make.Visible = false;
            lbl_make.Visible = false;
            Label3.Visible = false;
            txtFEmail.Visible = false;
        }
    }

    protected void but_ok_Click(object sender, EventArgs e)
    {
        Response.Redirect("vdtWelcome.aspx");
    }

    protected void imgbut_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("vdtWelcome.aspx");
    }

    //Forgot password by manoj 
    protected void imgbut_Email_Click(object sender, EventArgs e)
    {
        Cls_Login objLogIn = new Cls_Login();
        DataTable dt = new DataTable();
        dt = null;
        string pass = "", UName = "", UserName = "";
        int Role = Convert.ToInt32(ddl_Role.SelectedValue.ToString());
        int MakeID = 0;

        if (ddl_Role.SelectedValue.ToString() == "2")
            MakeID = Convert.ToInt16(ddl_make.SelectedValue.ToString());

        try
        {
            objLogIn.FEmail = txtFEmail.Text;
            dt = objLogIn.chkEmail(MakeID, Role);

            if (dt.Rows.Count > 0)
            {
                Cls_UserMaster objDealerUser = new Cls_UserMaster();
                pass = objDealerUser.generatePassword();

                objDealerUser.Password = pass;
                //chk if password already exists.
                bool ExistsResult = objDealerUser.CheckPasswordExists();
                if (ExistsResult)
                {
                    UName = dt.Rows[0]["Name"].ToString();
                    UserName = dt.Rows[0]["UserName"].ToString();
                    objLogIn.Password = pass;
                    objLogIn.ID = dt.Rows[0]["ID"].ToString();
                    bool result = objLogIn.updatePassword();
                    if (result)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Hi')", true);
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "xx", "<script type='text/javascript'>alert('HI');</script>", false);

                        sendMail(pass, UName, UserName);
                        lblMsg.Text = "New Password is send to your registered Email ID.";
                        progressBackgroundFilter12.Visible = true;
                        PanLogin.Visible = false;
                        but_ok.Focus();

                        //Response.Redirect("index.aspx?id=dmsg");
                    }
                    else
                    {
                        lblMsg.Text = "New Password can not be created. Try again....";
                    }
                }
                else
                {
                    lblMsg.Text = "New Password can not be created. Try again....";
                }
            }
            else
            {
                lblMsg.Text = "Incorrect Data.Please enter correct data.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }

    }
    //sending mail to user when forgot password changes password
    private void sendMail(string pass, string UName, string UserName)
    {
        StringBuilder str = new StringBuilder();
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();

        str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear " + UName + "<br /><br />Your password changed successfully for the make " + ddl_make.SelectedItem + ".<br />Your new Password is-.");
        str.Append("<br/><br/>User Name : " + UserName + " <br/> Password : " + pass);

        string link = ConfigurationManager.AppSettings["DummyPageUrl1"];
        string EmailFrom = ConfigurationManager.AppSettings["EmailFromID"];
        str.Append("<br/><br/><a href='" + link + "'>Click here</a> to Log in.</p> ");

        objEmailHelper.EmailBody = str.ToString();

        objEmailHelper.EmailToID = txtFEmail.Text;
        objEmailHelper.EmailFromID = EmailFrom;
        objEmailHelper.EmailSubject = "Change Password.";

        if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
            objEmailHelper.SendEmail();
    }

    protected void btnSumit_Click(object sender, ImageClickEventArgs e)
    {
        Cls_Login objLogIn = new Cls_Login();
        try
        {
            objLogIn.UserName = txtUserName.Text;
            objLogIn.Password = txtPassword.Text;
            DataTable dtInfo = objLogIn.ValidateLogIn();
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0)
                {
                    Session[Cls_Constants.LOGGED_IN_USERID] = dtInfo.Rows[0]["ID"].ToString();
                    Session[Cls_Constants.USER_NAME] = dtInfo.Rows[0]["UserName"].ToString();
                    Session[Cls_Constants.ROLE_ID] = dtInfo.Rows[0]["RoleID"].ToString();
                    Session[Cls_Constants.Role_Name] = dtInfo.Rows[0]["Role"].ToString();

                    Response.Redirect("Welcome.aspx", true);
                }
                else
                {
                    lblError.Text = "Wrong User Name or Password";
                }
            }
            else
            {
                lblError.Text = "Wrong User Name or Password";
            }
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnAdd_Click Event : " + ex.Message);
        }
    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
     {
        Cls_VDTLogin objCls_VDTLogin = new Cls_VDTLogin();
        try
        {
            objCls_VDTLogin.username = txtUserName.Text;
            objCls_VDTLogin.password = txtPassword.Text;
            DataTable dtInfo = objCls_VDTLogin.ValidateLogIn();
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0 && dtInfo.Rows.Count == 1 && !Convert.ToBoolean(dtInfo.Rows[0]["IsActive"]))
                {
                    lblError.Text = "As your order is \"Cancelled\".Your Account is Deactivated.";
                    return;
                }
                if (dtInfo.Rows.Count > 0 && dtInfo.Rows.Count == 1 && Convert.ToInt32(dtInfo.Rows[0]["Status"]) != 7)
                {
                    Session["id"] = Convert.ToString(dtInfo.Rows[0]["id"]);
                    Session["usernmae"] = Convert.ToString(txtUserName.Text);

                    //For remember password

                    if (chkRememberMe.Checked)
                    {
                        Response.Cookies["VDTUName"].Value = txtUserName.Text;
                        Response.Cookies["VDTPwd"].Value = txtPassword.Text;
                        Response.Cookies["VDTUName"].Expires = DateTime.Now.AddMonths(12);
                        Response.Cookies["VDTPwd"].Expires = DateTime.Now.AddMonths(12);
                    }
                    else
                    {
                        Response.Cookies["VDTUName"].Expires = DateTime.Now.AddMonths(-10);
                        Response.Cookies["VDTPwd"].Expires = DateTime.Now.AddMonths(-10);
                    }
                    Response.Redirect("vdtWelcome.aspx", true);
                }
                else if (dtInfo.Rows.Count > 0 && dtInfo.Rows.Count == 1 && Convert.ToInt32(dtInfo.Rows[0]["Status"]) == 7)
                {
                    lblError.Text = "As your car status is \"Delivered\".Your Account is Deactivated.";
                }
                else
                {
                    lblError.Text = "Wrong User Name or Password";
                }
            }
            else
            {
                lblError.Text = "Wrong User Name or Password";
            }
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnAdd_Click Event : " + ex.Message);
        }
    }

    // User for password remember
    protected void txtUserName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["UserName"] != null)
            {
                if (txtUserName.Text != Request.Cookies["UserName"].Value)
                {
                    txtPassword.Attributes.Add("value", string.Empty);
                    chkRememberMe.Checked = false;
                    txtPassword.Focus();
                }
                else
                {
                    txtPassword.Attributes.Add("value", Request.Cookies["Password"].Value);
                    if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                    {
                        chkRememberMe.Checked = true;
                    }
                    imgbtnAdd.Focus();
                }
            }
            else
            {
                if (txtPassword.Text == "" && txtUserName.Text != "")
                {
                    txtPassword.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message + ". Error" + ex.StackTrace); throw ex;
        }
        finally
        {

        }
    }



    protected void imgbtnAdd0_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ind1.aspx");
    }

    public void lnkClickHere_Click(object sender, EventArgs e)
    {
        try
        {
            pnlmodal.Visible = true;
            modal.Enabled = true;
            modal.Show();
        }
        catch (Exception ex)
        {

            logger.Error(Convert.ToString(ex.Message));
        }
    }
    public void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            pnlmodal.Visible = false;
            modal.Enabled = false;
            modal.Hide();


        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }

    }

}
