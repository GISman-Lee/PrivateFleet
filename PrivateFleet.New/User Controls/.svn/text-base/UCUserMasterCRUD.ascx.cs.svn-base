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
using Mechsoft.GeneralUtilities;
using log4net;
using System.Text;

public partial class User_Controls_UCUserMasterCRUD : System.Web.UI.UserControl
{
    //declare and initialize logger object
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCUserMasterCRUD));

    #region Properties
    private String _strID;
    public String ID
    {
        get { return _strID; }
        set { _strID = value; }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                GetAllActiveRoles();
                GetAllActiveDealers();
                ddlDealer.Enabled = false;

            }
            txtPassword.Visible = false;
            txtUserName.Visible = false;
        }
        catch (Exception Ex)
        {
            logger.Error("Page Load Event :" + Ex.Message);
        }

    }

    //manoj
    // to search a user
    protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        searchDealer("search");

    }

    //manoj
    // to search a user
    public void searchDealer(string action)
    {
        DataTable dt = new DataTable();
        Cls_UserMaster objDealerUser = new Cls_UserMaster();
        Cls_User objUser = new Cls_User();

        GridView Dummy = ((GridView)this.Parent.FindControl("gvUserDetails"));
        int cnt = 0;
        //if (action == "search")
        //{
            if (ddlRoles.SelectedValue.ToString() == "-Select-")
            {
                objDealerUser.RoleID = 0;
                cnt++;
            }
            else
                objDealerUser.RoleID = Convert.ToInt16(ddlRoles.SelectedValue.ToString());

            if (txtName.Text == String.Empty)
                cnt++;
            objDealerUser.Name = txtName.Text;

            if (txtEmail.Text == String.Empty)
                cnt++;
            objDealerUser.Email = txtEmail.Text;

            if (txtPhone.Text == String.Empty)
                cnt++;
            objDealerUser.Phone = txtPhone.Text;

            if (txtAddress.Text == String.Empty)
                cnt++;
            objDealerUser.Address = txtAddress.Text;

            if (cnt == 5)
            {
                dt = objUser.Get(); ;
            }
            else
                dt = objDealerUser.SearchUser();

            Dummy.DataSource = dt;
            Dummy.DataBind();
            if (dt.Rows.Count > 0)
            {
                lblResult.Text = "";
            }
            else
            {
                lblResult.Text = "No Records Found.";
            }

        //}
        //else if (action == "PageChange")
        //{
        //    DataTable dtUsers = objUser.Get();

        //    Dummy.DataSource = dtUsers;
        //    Dummy.DataBind();
        //}

    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate("VGSubmit");
        if (Page.IsValid)
        {
            Cls_User objUser = new Cls_User();
            Cls_UserMaster objDealerUser = new Cls_UserMaster();
            int Result = 0;
            bool UpdateStatus = false;
            string pass = objDealerUser.generatePassword();


            try
            {
                if (ddlDealer.SelectedValue != "0" || ddlDealer.SelectedValue.ToString() == "")
                {
                    //save dealer user information
                    txtUserName.Text = txtEmail.Text;
                    txtPassword.Text = pass;

                    objDealerUser.Address = txtAddress.Text;
                    objDealerUser.Email = txtEmail.Text;
                    objDealerUser.Name = txtName.Text;
                    objDealerUser.UsernameExpiryDate = DateTime.Now;
                    objDealerUser.Password = txtPassword.Text;
                    objDealerUser.Phone = txtPhone.Text;
                    objDealerUser.Username = txtUserName.Text;

                    objDealerUser.IsActive = true;
                    objDealerUser.RoleID = Convert.ToInt16(ddlRoles.SelectedValue.ToString());
                    objDealerUser.DealerID = Convert.ToInt16(ddlDealer.SelectedValue.ToString());
                    string make = objDealerUser.getMake();
                    if (String.IsNullOrEmpty(hdfID.Value.ToString()))
                    {
                        #region "Add Dealer User"
                        //check if dealer user already exists or not
                        // int ExistsResult = objDealerUser.CheckUserExists();

                        //check if password exist.(5 Jan 11 manoj)
                        bool ExistsResult = objDealerUser.CheckPasswordExists();


                        //add user if not exists
                        if (ExistsResult)
                        {
                            Result = objDealerUser.Add();

                            if (Result > 0)
                            {
                                sendMail(make);
                                lblResult.Text = "User added successfully";
                            }
                            else
                                lblResult.Text = "Failed to add user";
                        }
                        else
                            lblResult.Text = "User Password Already Exists. Try again...";
                        #endregion
                    }
                    else
                    {
                        #region "Update Dealer User"
                        objDealerUser.Id = Convert.ToInt16(hdfID.Value.ToString());
                        UpdateStatus = objDealerUser.Update();

                        if (UpdateStatus)
                            lblResult.Text = "User Details Updated successfully";
                        else
                            lblResult.Text = "Failed to Update user Details";
                        #endregion
                    }
                }
                else
                {
                    //save user information
                    txtUserName.Text = txtEmail.Text;
                    txtPassword.Text = pass;

                    objUser.Address = txtAddress.Text;
                    objUser.Email = txtEmail.Text;
                    objUser.Name = txtName.Text;
                    objUser.UsernameExpiryDate = DateTime.Now;
                    objUser.Password = txtPassword.Text;
                    objUser.Phone = txtPhone.Text;
                    objUser.Username = txtUserName.Text;
                    objDealerUser.Password = txtPassword.Text;


                    objUser.IsActive = true;
                    objUser.RoleID = Convert.ToInt16(ddlRoles.SelectedValue.ToString());

                    if (String.IsNullOrEmpty(hdfID.Value.ToString()))
                    {
                        #region "Add User"
                        //check if user already exists or not
                        int ExistsResult1 = objUser.CheckUserExists();

                        if (ExistsResult1 == 0)
                        {
                            //check if password exist.(5 Jan 11 manoj)
                            bool ExistsResult = objDealerUser.CheckPasswordExists();

                            //add user if not exists
                            if (ExistsResult)
                            {
                                Result = objUser.Add();
                                if (Result > 0)
                                {
                                    sendMail("consultant/admin");
                                    lblResult.Text = "User added successfully";
                                }
                                else
                                    lblResult.Text = "Failed to add user";
                            }
                            else
                                lblResult.Text = "User Password Already Exists. Try again...";
                        }
                        else
                            lblResult.Text = "User Already Exists. Try again...";

                        #endregion
                    }
                    else
                    {
                        #region "Update User"
                        objUser.Id = Convert.ToInt16(hdfID.Value.ToString());
                        UpdateStatus = objUser.Update();

                        if (UpdateStatus)
                            lblResult.Text = "User Details Updated successfully";
                        else
                            lblResult.Text = "Failed to Update user Details";
                        #endregion
                    }
                }
                BindData();
                ClearFields();
            }
            catch (Exception Ex)
            {
                logger.Error("imgbtnAdd_Click Event :" + Ex.Message);
            }
        }
    }
    private void sendMail(string make)
    {
        StringBuilder str = new StringBuilder();
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();

        if (make == "consultant/admin")
        {
            str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear " + txtName.Text + "<br /><br />Welcome to Private Fleet. Your registration to Private Fleet is successfully completed.<br />Use the following detail to login in Private Fleet.");
            str.Append("<br/><br/>User Name : " + txtUserName.Text + " <br/> Password : " + txtPassword.Text);
        }
        else
        {
            str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear " + txtName.Text + "<br /><br />Welcome to Private Fleet. Your registration to Private Fleet is successfully completed for the make " + make + ".<br />Use the following detail to login in Private Fleet.");
            str.Append("<br/><br/>User Name : " + txtUserName.Text + " <br/> Password : " + txtPassword.Text);
        }
        string link = ConfigurationManager.AppSettings["DummyPageUrl1"];
        string EmailFrom = ConfigurationManager.AppSettings["EmailFromID"];
        str.Append("<br/><br/><a href='" + link + "'>Click here</a> to Log in.</p> ");

        objEmailHelper.EmailBody = str.ToString();

        objEmailHelper.EmailToID = txtEmail.Text;
        objEmailHelper.EmailFromID = EmailFrom;
        //objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
        logger.Debug("Mail TO - " + txtEmail.Text);
        logger.Debug("Mail From -" + EmailFrom);


        objEmailHelper.EmailSubject = "Welcome to Private Fleet";

        if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
            objEmailHelper.SendEmail();

    }


    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //reset form fields
            ClearFields();
        }
        catch (Exception Ex)
        {
            logger.Error("imgbtnCancel_Click Event :" + Ex.Message);
        }
    }
    #endregion

    #region "Methods"

    /// <summary>
    /// Fill roles dropdown
    /// </summary>
    private void GetAllActiveRoles()
    {
        try
        {
            Cls_Role objRole = new Cls_Role();
            DataTable dtAllRoles = objRole.GetAllActiveRoles();
            ddlRoles.DataSource = dtAllRoles;
            ddlRoles.DataBind();
          
            ddlRoles.Items.Insert(0, new ListItem("- Select Role -", "-Select-"));
        }
        catch (Exception Ex)
        {
            logger.Error("GetAllActiveRoles Function :" + Ex.Message);
        }

    }

    /// <summary>
    /// Fill dealers dropdown
    /// </summary>
    private void GetAllActiveDealers()
    {
        try
        {
            Cls_UserMaster objUser = new Cls_UserMaster();
            DataTable dtAllDealers = objUser.GetAllActiveDealers();
            ddlDealer.DataSource = dtAllDealers;
            ddlDealer.DataTextField = "Dealer";
            ddlDealer.DataValueField = "ID";
            ddlDealer.DataBind();

            if (ddlDealer.Items.Count == 0)
            {
                ddlDealer.Items.Insert(0, new ListItem("- No Dealer Found -", "0"));
            }
            else
            {
                ddlDealer.Items.Insert(0, new ListItem("- Select Dealer -", "0"));
            }
        }
        catch (Exception Ex)
        {
            logger.Error("GetAllActiveRoles Function :" + Ex.Message);
        }

    }

    private void BindData()
    {
        try
        {
            GridView Dummy = ((GridView)this.Parent.FindControl("gvUserDetails"));
            Dummy.DataSource = BindUsers();
            Dummy.DataBind();
        }
        catch (Exception Ex)
        {
            logger.Error("BindData Function :" + Ex.Message);
        }

    }

    private DataTable BindUsers()
    {
        try
        {
            Cls_User objUser = new Cls_User();
            DataTable dtUsers = objUser.Get();
            return dtUsers;
        }
        catch (Exception Ex)
        {
            logger.Error("BindUsers Function :" + Ex.Message);
            return null;
        }
    }

    private void ClearFields()
    {
        try
        {
            txtAddress.Text = txtEmail.Text = txtExpiryDate.Text = txtName.Text = txtPassword.Text = txtPhone.Text = txtUserName.Text = "";
            this.hdfID.Value = null;
            if (ddlRoles.Items.Count >= 0)
                ddlRoles.SelectedIndex = 0;

            if (ddlDealer.Items.Count >= 0)
                ddlDealer.SelectedIndex = 0;

            imgbtnAdd.ImageUrl = "~/Images/AddUser.gif";
            imgbtnAdd.Attributes.Add("onmouseout", "this.src='Images/AddUser.gif'");
            imgbtnAdd.Attributes.Add("onmouseover", "this.src='Images/AddUser_hvr.gif'");

            trUserName.Visible = true;
            trPassword.Visible = true;
            ddlDealer.Enabled = false;

        }
        catch (Exception Ex)
        {
            logger.Error("Clear Fields Function :" + Ex.Message);
        }
    }

    public void SetHiddenFields()
    {
        try
        {
            hdfID.Value = this.ID;
            PopulateFields();
        }
        catch (Exception Ex)
        {
            logger.Error("SetHiddenFields Function :" + Ex.Message);
        }
    }

    private void PopulateFields()
    {
        try
        {
            Cls_UserMaster objUser = new Cls_UserMaster();
            DataTable dtUserDetails = objUser.GetUserDetails(Convert.ToInt16(hdfID.Value.ToString()));

            if (dtUserDetails.Rows.Count > 0)
            {
                txtPhone.Text = dtUserDetails.Rows[0]["Phone"].ToString();

                trUserName.Visible = false;
                trPassword.Visible = false;

                txtName.Text = dtUserDetails.Rows[0]["Name"].ToString();
                txtExpiryDate.Text = dtUserDetails.Rows[0]["UsernameExpiryDate"].ToString();
                txtEmail.Text = dtUserDetails.Rows[0]["Email"].ToString();
                txtAddress.Text = dtUserDetails.Rows[0]["Address"].ToString();

                imgbtnAdd.ImageUrl = "~/Images/UpdateUser.gif";
                imgbtnAdd.Attributes.Add("onmouseout", "this.src='Images/UpdateUser.gif'");
                imgbtnAdd.Attributes.Add("onmouseover", "this.src='Images/UpdateUser_hvr.gif'");

                ddlRoles.SelectedIndex = ddlRoles.Items.IndexOf(ddlRoles.Items.FindByValue(dtUserDetails.Rows[0]["RoleID"].ToString()));
                ddlDealer.SelectedIndex = ddlDealer.Items.IndexOf(ddlDealer.Items.FindByValue(dtUserDetails.Rows[0]["DealerID"].ToString()));
            }
            else
            {
                ((Label)this.Parent.FindControl("lblResult")).Text = "User Details Not Found.";
            }
        }
        catch (Exception Ex)
        {
            logger.Error("PopulateFields Function :" + Ex.Message);
        }
    }
    #endregion

    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblResult.Text = "";
        if (ddlRoles.SelectedIndex == 2)
        {
            ddlDealer.Enabled = true;
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            //GetAllActiveDealers();
        }
        else
        {
            ddlDealer.SelectedIndex = 0;
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            ddlDealer.Enabled = false;
        }
       
    }
    protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cls_UserMaster obj = new Cls_UserMaster();
        DataTable dt = new DataTable();
        obj.DealerName = ddlDealer.SelectedItem.ToString();
        dt = obj.GetDealerdata();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            txtName.Text = dt.Rows[i][1].ToString();
            txtEmail.Text = dt.Rows[i][2].ToString();
            txtPhone.Text = dt.Rows[i][3].ToString();
        }
    }
}
