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
using System.Data.Common;

public partial class User_Controls_UcLogin : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_UcLogin));
    Boolean flag = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        progressBackgroundFilter12.Visible = false;
        PanLogin.Visible = true;
        Cls_Login objLogIn = new Cls_Login();

        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.Text, "Select Email From tblServeyPrimaryContact where Name='Catheirne Heyes'");
            string PrimaryCntEmail = Convert.ToString(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null));

            //objRemoteCmd.CommandType = CommandType.Text;
            //string PrimaryCntEmail = Convert.ToString(objDataAccess.ExecuteScaler(objRemoteCmd));
        }
        catch (Exception ex)
        {
        }
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
            if (Request.Cookies["UName"] != null)
                txtUserName.Text = Request.Cookies["UName"].Value;
            if (Request.Cookies["Pwd"] != null)
                txtPassword.Attributes.Add("value", Request.Cookies["Pwd"].Value);

            if (Request.Cookies["UName"] != null && Request.Cookies["Pwd"] != null)
                chkRememberMe.Checked = true;

            txtUserName.Focus();

            //if (Request.Cookies["UName"] != null && Request.Cookies["Pwd"] != null)
            //{
            //    logger.Error("PL=" + Request.Cookies["UName"].Value);
            //    logger.Error("PL=" + Request.Cookies["Pwd"].Value);
            //}
            //else
            //{
            //    logger.Error(" PL Null value");
            //}
            // Password remember End
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
        if (Convert.ToInt32(ddl_Role.SelectedValue) == 2)
        {
            // For Dealer
            Label3.Visible = true;
            txtFEmail.Visible = true;
            txtFEmail.Text = "";
            lbl_make.Visible = true;
            ddl_make.Visible = true;
            BindDropDown();
        }
        else if (Convert.ToInt32(ddl_Role.SelectedValue) == 3 || Convert.ToInt32(ddl_Role.SelectedValue) == 1 || Convert.ToInt32(ddl_Role.SelectedValue) == 4)
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
        Response.Redirect("index.aspx");
    }

    protected void imgbut_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
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

            if (dt != null && dt.Rows.Count > 0)
            {
                if (ddl_Role.SelectedValue.ToString() == "2")
                {
                    DataView dvTemp = dt.DefaultView;
                    dvTemp.RowFilter = "MakeID=" + MakeID;
                    dt = dvTemp.ToTable();

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        lblMsg.Text = "Email recognised but not in connection with make selected.  Most likely is that the dealership has an alternate email address for this specific make.  If a quote request prompted you to visit this page, please refer to the email received and use that email.";
                        return;
                    }
                }


                #region Checking MakeId of Dealer
                if (ddl_Role.SelectedValue.ToString() == "2")
                {
                    if ((dt.Rows[0]["MakeID"].ToString()) != Convert.ToString(MakeID))
                    {
                        lblMsg.Text = "Email recognised but not in connection with make selected.  Most likely is that the dealership has an alternate email address for this specific make.  If a quote request prompted you to visit this page, please refer to the email received and use that email.";
                        return;
                    }
                }
                #endregion

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
                lblMsg.Text = "Email not recognised. Please make sure you use the email to which the Quote Request or Update Request was orginally sent.";
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

    // Login Click
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        Cls_Login objLogIn = new Cls_Login();
        try
        {
            objLogIn.UserName = txtUserName.Text.Trim();
            objLogIn.Password = txtPassword.Text.Trim();
            DataTable dtInfo = objLogIn.ValidateLogIn();
            if (dtInfo != null)
            {
                if (dtInfo.Rows.Count > 0 && dtInfo.Rows.Count >= 1)
                {
                    if (Convert.ToInt32(dtInfo.Rows[0]["UserActive"]) == 0)
                    {
                        lblError.Text = "This account is Deactive. Please contact Admin.";
                        return;
                    }

                    Session[Cls_Constants.LOGGED_IN_USERID] = dtInfo.Rows[0]["ID"].ToString();
                    Session[Cls_Constants.USER_NAME] = dtInfo.Rows[0]["UserName"].ToString();
                    Session[Cls_Constants.ROLE_ID] = dtInfo.Rows[0]["RoleID"].ToString();
                    Session[Cls_Constants.FromEmailID] = dtInfo.Rows[0]["Email"].ToString();
                    Session[Cls_Constants.CONSULTANT_NAME] = dtInfo.Rows[0]["Name"].ToString();
                    Session[Cls_Constants.PHONE] = dtInfo.Rows[0]["Phone1"].ToString();
                    Session[Cls_Constants.MOBILE] = dtInfo.Rows[0]["Mobile"].ToString();
                    if (Convert.ToInt32(dtInfo.Rows[0]["RoleID"].ToString()) == 2)
                    {
                        string makeL = "";
                        if (dtInfo.Rows.Count >= 1)
                        {
                            for (int i = 0; i < dtInfo.Rows.Count; i++)
                            {
                                makeL += ", " + Convert.ToString(dtInfo.Rows[i]["Make"]);
                            }
                            makeL = makeL.Substring(2);
                        }
                        Session[Cls_Constants.Role_Name] = dtInfo.Rows[0]["Role"].ToString() + " - " + makeL;
                    }
                    else
                    {
                        Session[Cls_Constants.Role_Name] = dtInfo.Rows[0]["Role"].ToString();
                    }

                    //For remember password

                    if (chkRememberMe.Checked)
                    {
                        Response.Cookies["UName"].Value = txtUserName.Text;
                        Response.Cookies["Pwd"].Value = txtPassword.Text;
                        Response.Cookies["UName"].Expires = DateTime.Now.AddMonths(12);
                        Response.Cookies["Pwd"].Expires = DateTime.Now.AddMonths(12);

                        //HttpCookie cookie = new HttpCookie("PFUsers");
                        //cookie.Value = txtUserName.Text;
                        //cookie.Expires = DateTime.Now.AddMonths(1);
                        //Response.SetCookie(cookie);

                    }
                    else
                    {
                        Response.Cookies["UName"].Expires = DateTime.Now.AddMonths(-10);
                        Response.Cookies["Pwd"].Expires = DateTime.Now.AddMonths(-10);
                    }
                    //if (Request.Cookies["UName"] != null && Request.Cookies["Pwd"] != null)
                    //{
                    //    logger.Error("Sub=" + Request.Cookies["UName"].Value);
                    //    logger.Error("Sub=" + Request.Cookies["Pwd"].Value);
                    //}
                    //else
                    //{
                    //    logger.Error("Sub Null value");
                    //}

                    if (Convert.ToString(Session[Cls_Constants.ROLE_ID]) == "2")
                    {
                        SendUpdateReminderToDealer();
                    }
                    if (Request.QueryString["tir"] != null && !Convert.ToString(Request.QueryString["tir"]).Equals(String.Empty))
                        Response.Redirect("ConsultantTradeInReport.aspx?tir=" + Convert.ToString(Request.QueryString["tir"]), true);

                    if (flag == false)
                    {
                        Response.Redirect("Welcome.aspx", true);
                    }
                    else
                    {
                        Response.Redirect("ClinetIfo_ForDealer.aspx", true);
                    }
                }
                else
                {
                    lblError.Text = "Looks like you have entered the wrong email or password. Please <a href='../Index.aspx?id=4got' runat='server' style='color: #055f86; text-align: left;'><b>click here</b></a> to request a new password to be sent to your registered email address.";
                }
            }
            else
            {
                lblError.Text = "Looks like you have entered the wrong email or password. Please <a href='../Index.aspx?id=4got' runat='server' style='color: #055f86; text-align: left;'><b>click here</b></a> to request a new password to be sent to your registered email address.";
            }
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnAdd_Click Event : " + ex.Message);
        }
    }

    public void SendUpdateReminderToDealer()
    {

        Cls_Login objCls_Login = new Cls_Login();
        objCls_Login.UserName = Session[Cls_Constants.USER_NAME].ToString();
        objCls_Login.Flag = 1;
        DataTable dt = new DataTable();
        dt = objCls_Login.GetDealerRespone();
        // Int32 nonResponse_LowerLimit = 0;
        Int32 nonResponse_UppeLimit = 0;

        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                ConfigValues objConfigue = new ConfigValues();

                //objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS";
                //nonResponse_LowerLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS";
                nonResponse_UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
                // objCls_Login.Dealer_Non_Response_LowerLimit = nonResponse_LowerLimit;
                // objCls_Login.Dealer_Non_Response_UpperLimit = nonResponse_UppeLimit;
                foreach (DataRow drow in dt.Rows)
                {
                    DataTable dt1 = new DataTable();
                    DataView dv1 = new DataView(dt);
                    dv1.RowFilter = "Customerid=" + Convert.ToString(drow["customerid"]);
                    dt1 = dv1.ToTable();
                    if (Convert.ToInt32(drow["nonResponseDate"]) >= nonResponse_UppeLimit)
                    {
                        flag = true;
                    }
                }
            }
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
            //if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
            //{
            //    logger.Error("TxtC=" + Request.Cookies["UserName"].Value);
            //    logger.Error("TxtC=" + Request.Cookies["Password"].Value);
            //}
            //else
            //{
            //    logger.Error("TxtC Null value");
            //}
        }
    }

    protected void imgbtnAdd0_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ind1.aspx");
    }


}
