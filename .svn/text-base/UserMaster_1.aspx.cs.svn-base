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
using PFSales;


public partial class UserMaster_1 : System.Web.UI.Page
{
    ILog logger = LogManager.GetLogger(typeof(UserMaster_1));
    Cls_User objUser = null;

    #region Properties
    private String _strID;
    public String ID
    {
        get { return _strID; }
        set { _strID = value; }
    }
    #endregion

    #region Page_Load

    protected void Page_Load(object sender, EventArgs e)
    {
        lblResult.Text = "";
        logger.Debug("Page Load Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        if (!IsPostBack)
        {
            txtEmail.Enabled = true;
            ((Label)Master.FindControl("lblHeader")).Text = "User Master";
            ddl_NoRecords.SelectedValue = Convert.ToString(Cls_Constants.NO_OF_ROWS_IN_REPORT);
            ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = "Name";
            ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            gvUserDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());


            GetAllActiveRoles();
            GetAllActiveDealers();
            ddlDealer.Enabled = false;

            BindData1();

        }
        txtPassword.Visible = false;
        txtUserName.Visible = false;
        logger.Debug("Page Load Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
    }

    #endregion

    #region Events

    protected void gvUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblResult.Text = "";
        if (e.CommandName == "Edit")
        {
            txtEmail.Enabled = false;
            ViewState["Role"] = ddlRoles.SelectedValue;
            ViewState["Dealer"] = ddlDealer.SelectedValue;
            ViewState["Name"] = txtName.Text.Trim();

            int RowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            RowIndex = RowIndex - (gvUserDetails.PageIndex * gvUserDetails.PageSize);
            Boolean IsActive = Convert.ToBoolean(((HiddenField)gvUserDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value.ToString());
            //Boolean IsActive = Convert.ToBoolean(Convert.ToByte(e.CommandArgument.ToString()));
            if (IsActive)
            {
                //RowIndex = RowIndex - (gvUserDetails.PageIndex * gvUserDetails.PageSize);
                ID = gvUserDetails.DataKeys[RowIndex][0].ToString();
                SetHiddenFields();
            }
            else
            {
                lblResult.Text = "Deactivated user can not be updated";
            }
        }
        if (e.CommandName == "SeePassword" || e.CommandName == "HidePassword")
        {
            int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
            RowIndex = RowIndex - (gvUserDetails.PageIndex * gvUserDetails.PageSize);

            if (e.CommandName == "SeePassword")
            {

                gvUserDetails.Rows[RowIndex].FindControl("lblPassword").Visible = true;
                gvUserDetails.Rows[RowIndex].FindControl("lblPassword1").Visible = false;

                ImageButton imgbut = (ImageButton)gvUserDetails.Rows[RowIndex].FindControl("imgbtnSeePass");
                imgbut.CommandName = "HidePassword";
                imgbut.ToolTip = "Click to Hide Password";
            }
            if (e.CommandName == "HidePassword")
            {
                gvUserDetails.Rows[RowIndex].FindControl("lblPassword").Visible = false;
                gvUserDetails.Rows[RowIndex].FindControl("lblPassword1").Visible = true;
                ImageButton imgbut = (ImageButton)gvUserDetails.Rows[RowIndex].FindControl("imgbtnSeePass");
                imgbut.CommandName = "SeePassword";
                imgbut.ToolTip = "Click To See Password";
            }
        }

        if (e.CommandName == "Activeness")
        {
            Cls_Utilities objUtility = new Cls_Utilities();
            int RowIndex = Convert.ToInt16(e.CommandArgument.ToString());
            RowIndex = RowIndex - (gvUserDetails.PageIndex * gvUserDetails.PageSize);
            bool IsActive = Convert.ToBoolean(((HiddenField)gvUserDetails.Rows[RowIndex].FindControl("hdfIsActive")).Value);
            int ID1 = Convert.ToInt16(gvUserDetails.DataKeys[RowIndex][0].ToString());
            if (ID1 != 1)
            {
                bool Result = false;

                if (IsActive)
                {
                    Result = objUtility.DeActivate(ID1, "ACU_UserMaster");
                }
                else
                {
                    Result = objUtility.Actiavte(ID1, "ACU_UserMaster");
                }
                if (IsActive)
                {
                    if (Result)
                        lblResult.Text = "User Deactivated successfully";
                    else
                        lblResult.Text = "Failed to Deactivate the User";
                }
                else
                {
                    if (Result)
                        lblResult.Text = "User Activated Successfully";
                    else
                        lblResult.Text = "Failed to Activate the User";
                }

                //BindData1();
                searchDealer("PageChange");
                // gvUserDetails.DataSource = (DataTable)ViewState["dtUsers"];
                //gvUserDetails.DataBind();
            }
            else
            {
                lblResult.Text = "You can not deactivate Admin User.";
            }
        }
    }

    protected void gvUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Image imgBtnActive = ((Image)e.Row.FindControl("imgbtnActivate"));
                Image imgActive = ((Image)e.Row.FindControl("imgActive"));
                LinkButton lnkbtnActivate = ((LinkButton)e.Row.FindControl("lnkbtnActiveness"));
                if (imgBtnActive != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive")))
                    {
                        imgBtnActive.ImageUrl = "~/Images/Active.png";
                        imgActive.ImageUrl = "~/Images/active_bullate.jpg";
                        imgActive.ToolTip = "Deactivate This Record";

                        e.Row.CssClass = "gridactiverow";
                    }
                    else
                    {
                        imgBtnActive.ImageUrl = "~/Images/Inactive.ico";
                        imgActive.ImageUrl = "~/Images/deactive_bullate.jpg";
                        e.Row.CssClass = "griddeactiverow";
                        imgActive.ToolTip = "Activate This Record";
                    }
                }
                if (lnkbtnActivate != null)
                {
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive")))
                    {
                        lnkbtnActivate.Text = "Deactivate";
                    }
                    else
                    {
                        lnkbtnActivate.Text = "Activate";
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }

    protected void gvUserDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUserDetails.PageIndex = e.NewPageIndex;
        //searchDealer("PageChange");
        gvUserDetails.DataSource = (DataTable)ViewState["dtUsers"];
        gvUserDetails.DataBind();

        //BindData();

    }

    protected void ddl_NoRecords_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Debug("No. of records Selected Index Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        DropDownList ddl = (DropDownList)FindControl("ddlRoles");

        if (ddl_NoRecords.SelectedValue.ToString() == "All")
        {
            gvUserDetails.PageSize = gvUserDetails.PageCount * gvUserDetails.Rows.Count;
            searchDealer("search");
        }
        else
        {
            gvUserDetails.PageSize = Convert.ToInt32(ddl_NoRecords.SelectedValue.ToString());
            searchDealer("search");
        }
        logger.Debug("No. of records Selected Index Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));

    }

    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {

        ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] = e.SortExpression;
        //Swap sort direction
        this.DefineSortDirection();
        //BindData(objCourseMaster);
        //this.BindData1();
        gvUserDetails.DataSource = (DataTable)ViewState["dtUsers"];
        gvUserDetails.DataBind();
    }

    //manoj
    // to search a user
    protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        searchDealer("search");
    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
       // Page.Validate("VGSubmit");
        if (Page.IsValid)
        {
            Cls_User objUser = new Cls_User();
            Cls_UserMaster objDealerUser = new Cls_UserMaster();
            //AddLeadsToPFSales service = new AddLeadsToPFSales();
            // service.Url = "http://180.235.129.33/PFSales/AddLeadsToPFSales.asmx";

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
                    objDealerUser.Extension = Convert.ToString(txtExtension.Text.Trim());
                    objDealerUser.Mobile = Convert.ToString(txtMobile.Text.Trim());
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
                    logger.Error("Add admin & Consultant");
                    //save user information
                    txtUserName.Text = txtEmail.Text;
                    txtPassword.Text = pass;

                    objDealerUser.Address = txtAddress.Text;
                    objDealerUser.Email = txtEmail.Text;
                    objDealerUser.Name = txtName.Text;
                    objDealerUser.UsernameExpiryDate = DateTime.Now;
                    objDealerUser.Password = txtPassword.Text;
                    objDealerUser.Phone = txtPhone.Text;
                    objDealerUser.Username = txtUserName.Text;
                    objDealerUser.Password = txtPassword.Text;

                    objDealerUser.IsActive = true;
                    objDealerUser.RoleID = Convert.ToInt16(ddlRoles.SelectedValue.ToString());
                    objDealerUser.Extension = Convert.ToString(txtExtension.Text.Trim());
                    objDealerUser.Mobile = Convert.ToString(txtMobile.Text.Trim());

                    if (String.IsNullOrEmpty(hdfID.Value.ToString()))
                    {
                        #region "Add User"
                        //check if user already exists or not
                        int ExistsResult1 = objDealerUser.CheckUserExists();

                        if (ExistsResult1 == 0)
                        {
                            //check if password exist.(5 Jan 11 manoj)
                            bool ExistsResult = objDealerUser.CheckPasswordExists();

                            //add user if not exists
                            if (ExistsResult)
                            {
                                //Guid gi = Guid.NewGuid();
                                //objEmp.EmpKey = gi.ToString().Trim();
                                Result = objDealerUser.AddAdminConsultant();
                                if (Result > 0)
                                {
                                    logger.Error("Send email to new User");
                                    sendMail("consultant/admin");
                                    ViewState["Role"] = ddlRoles.SelectedValue;
                                    ViewState["Dealer"] = ddlDealer.SelectedValue;
                                    ViewState["Name"] = txtName.Text.Trim();
                                    // Int64 result = service.SaveEmpToPFSales(0, Convert.ToInt32(objDealerUser.RoleID), Convert.ToString(objDealerUser.Name).Trim(), Convert.ToString(objDealerUser.Phone), Convert.ToString(objDealerUser.Extension), Convert.ToString(objDealerUser.Email), Convert.ToString(objDealerUser.Mobile), Convert.ToString(objDealerUser.Address), string.Empty, string.Empty, Convert.ToString(objDealerUser.Password));
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

                        objDealerUser.Id = Convert.ToInt16(hdfID.Value.ToString());
                        UpdateStatus = objDealerUser.UpdateAdminConsultant();

                        if (UpdateStatus)
                            lblResult.Text = "User Details Updated successfully";
                        else
                            lblResult.Text = "Failed to Update user Details";
                        #endregion
                    }
                }
                ClearFields();
                // BindData1();
                if (ViewState["Role"] != null && !Convert.ToString(ViewState["Role"]).Equals(string.Empty))
                    ddlRoles.SelectedValue = Convert.ToString(ViewState["Role"]);
                if (ViewState["Dealer"] != null && !Convert.ToString(ViewState["Dealer"]).Equals(string.Empty))
                    ddlDealer.SelectedValue = Convert.ToString(ViewState["Dealer"]);
                if (ViewState["Name"] != null && !Convert.ToString(ViewState["Name"]).Equals(string.Empty))
                    txtName.Text = Convert.ToString(ViewState["Name"]);
                ViewState["Role"] = null;
                ViewState["Dealer"] = null;
                ViewState["Name"] = null;
                searchDealer("update");

            }
            catch (Exception Ex)
            {
                logger.Error("imgbtnAdd_Click Event :" + Ex.Message);
            }
        }
        else
        {
            logger.Error("Page not valid");
        }
    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        logger.Debug("Cancle click Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        try
        {
            //reset form fields
            ClearFields();
            BindData1();
        }
        catch (Exception Ex)
        {
            logger.Error("imgbtnCancel_Click Event :" + Ex.Message);
        }
        finally
        {
            logger.Debug("Cancle click Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }

    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblResult.Text = string.Empty;
        txtName.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtExtension.Text = string.Empty;
        txtMobile.Text = string.Empty;

        if (ddlRoles.SelectedIndex == 2)
        {
            ddlDealer.Enabled = true;
            //GetAllActiveDealers();
        }
        else
        {
            ddlDealer.SelectedIndex = 0;
            ddlDealer.Enabled = false;
        }
        searchDealer("search");
        logger.Debug("Role Selected Index Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
    }

    protected void ddlDealer_SelectedIndexChanged(object sender, EventArgs e)
    {
        logger.Debug("Dealer Selected Index Starts=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        Cls_UserMaster obj = new Cls_UserMaster();
        DataTable dt = new DataTable();
        obj.DealerName = ddlDealer.SelectedItem.ToString();
        dt = obj.GetDealerdata();
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txtName.Text = dt.Rows[i][1].ToString();
                txtEmail.Text = dt.Rows[i][2].ToString();
                txtPhone.Text = dt.Rows[i][3].ToString();
                txtEmail.Enabled = false;
            }
        }
        else
        {
            txtName.Text = txtEmail.Text = txtPhone.Text = string.Empty;
            txtEmail.Enabled = true;
        }
        logger.Debug("Dealer Selected Index Ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
    }

    protected void gvUserDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvUserDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    #endregion

    #region "Methods"

    /// <summary>
    /// Fill roles dropdown
    /// </summary>
    private void GetAllActiveRoles()
    {
        logger.Debug("Get Roles Start=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
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
        finally
        {
            logger.Debug("Get role Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }

    }

    /// <summary>
    /// Fill dealers dropdown
    /// </summary>
    private void GetAllActiveDealers()
    {
        logger.Debug("Get Dealers Start =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
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
        finally
        {
            logger.Debug("Get Dealers Ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
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
            txtEmail.Enabled = true;

            txtExtension.Text = string.Empty;
            txtMobile.Text = string.Empty;

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
        logger.Debug("Populate fields Start =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
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
                txtExtension.Text = Convert.ToString(dtUserDetails.Rows[0]["Extension"]);
                txtMobile.Text = Convert.ToString(dtUserDetails.Rows[0]["Mobile"]);

                imgbtnAdd.ImageUrl = "~/Images/UpdateUser.gif";
                imgbtnAdd.Attributes.Add("onmouseout", "this.src='Images/UpdateUser.gif'");
                imgbtnAdd.Attributes.Add("onmouseover", "this.src='Images/UpdateUser_hvr.gif'");

                ddlRoles.SelectedIndex = ddlRoles.Items.IndexOf(ddlRoles.Items.FindByValue(dtUserDetails.Rows[0]["RoleID"].ToString()));
                ddlDealer.Enabled = true;
                ddlDealer.SelectedIndex = ddlDealer.Items.IndexOf(ddlDealer.Items.FindByValue(dtUserDetails.Rows[0]["DealerID"].ToString()));
                ddlDealer.Enabled = false;
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
        finally
        {
            logger.Debug("Populate fields Ends=" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        }
    }

    private void BindData1()
    {
        logger.Debug("Bind Data Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        objUser = new Cls_User();
        DataTable dtUsers = objUser.Get();

        DataView dv = dtUsers.DefaultView;
        dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
        dtUsers = dv.ToTable();

        ViewState["dtUsers"] = dtUsers;
        gvUserDetails.PageIndex = 0;
        gvUserDetails.DataSource = dtUsers;
        gvUserDetails.DataBind();
        logger.Debug("Bind Data Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
    }

    /// <summary>
    /// Define sort direction for grid.
    /// </summary>
    /// <param name="objAlias"></param>
    private void DefineSortDirection()
    {
        if (ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION] != null)
        {
            if (ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString() == Cls_Constants.VIEWSTATE_ASC)
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_DESC;
            }
            else
            {
                ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION] = Cls_Constants.VIEWSTATE_ASC;
            }

        }
    }

    //manoj
    // to search a user
    public void searchDealer(string action)
    {
        logger.Debug("Search Starts =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
        DataTable dt = new DataTable();
        Cls_UserMaster objDealerUser = new Cls_UserMaster();
        Cls_User objUser = new Cls_User();

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
            dt = objUser.Get();
            DataView dv = dt.DefaultView;
            dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            dt = dv.ToTable();
        }
        else
        {
            dt = objDealerUser.SearchUser();
        }
        gvUserDetails.PageIndex = 0;
        gvUserDetails.DataSource = dt;
        gvUserDetails.DataBind();

        if (dt.Rows.Count > 0)
        {
            lblResult.Text = "";
            ViewState["dtUsers"] = dt;
        }
        else
        {
            lblResult.Text = "No Records Found.";
        }
        logger.Debug("Search Ends =" + Convert.ToString(System.DateTime.Now.ToString("hh:mm:ss:ffff")));
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

    private void addEmpToPFSales()
    {
        AddLeadsToPFSales service = new AddLeadsToPFSales();
        service.Url = "http://180.235.129.33/PFSales/AddLeadsToPFSales.asmx";

        try
        {
            DataTable dt = (DataTable)ViewState["dtUsers"];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "IsActive=1 AND RoleID IN (1,3)";
            dv.Sort = "ID ASC";
            dt = dv.ToTable();

            for (int i = 2; i < dt.Rows.Count; i++)
            {
                // Int64 result = service.SaveEmpToPFSales(Convert.ToInt32(dt.Rows[i]["RoleID"]), Convert.ToString(dt.Rows[i]["Name"]).Trim(), Convert.ToString(dt.Rows[i]["Phone"]), Convert.ToString(dt.Rows[i]["Extension"]), Convert.ToString(dt.Rows[i]["Email"]), Convert.ToString(dt.Rows[i]["Mobile"]), Convert.ToString(dt.Rows[i]["Address"]), string.Empty, string.Empty, Convert.ToString(dt.Rows[i]["Password"]));
            }
        }
        catch (Exception)
        {
            service = null;
        }
    }


    #endregion


}
