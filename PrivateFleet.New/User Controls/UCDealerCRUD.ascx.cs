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
using Mechsoft.FleetDeal;
using Mechsoft.GeneralUtilities;
using log4net;
public partial class User_Controls_UCDealerCRUD : System.Web.UI.UserControl
{

    #region Properties

    private int _DealerID;

    public int DealerID
    {
        get { return _DealerID; }
        set { _DealerID = value; }
    }

    private String _DBOperation;

    public String DBOperation
    {
        get { return _DBOperation; }
        set { _DBOperation = value; }
    }


    #endregion

    public string OldMake = "AUDI"; 
    public bool lblFlag = true;
    Cls_Dealer objDealer = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCDealerCRUD));

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ImagebtnSearch.Enabled = true;
                ImagebtnSearch.ImageUrl = "~/Images/Search_dealer.gif";
                BindStates();
                BindCarMakes();
                BindCities();
                lbtGetLocations_Click(null, null);
            }
            DealerMasterPanel.DefaultButton = "ImagebtnSearch";
        }
        catch (Exception ex)
        { logger.Error("Page_Load Event :" + ex.Message); }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlState.SelectedIndex > 0)
            {
                ddlCity.Items.Clear();

                Cls_Dealer objDealer = new Cls_Dealer();
                objDealer.State = ddlState.SelectedItem.Value.ToString();
                DataTable dtCities = objDealer.GetAllCitiesOfState();
                ddlCity.DataSource = dtCities;
                ddlCity.DataBind();

                if (ddlCity.Items.Count == 0)
                    ddlCity.Items.Insert(0, new ListItem("No City Found", "-Select-"));
                else
                    ddlCity.Items.Insert(0, new ListItem("-Select City-", "-Select-"));
            }
        }
        catch (Exception ex)
        {
            logger.Error("ddlState_SelectedIndexChanged Event :" + ex.Message);
        }
    }

    protected void ddlCarMake_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCarMakeStorageFunction();
    }

    private void ddlCarMakeStorageFunction()
    {
        try
        {
            if (ddlCarMake.SelectedIndex > 0)
            {
                if (this.TextBox1.Text != "")
                {
                    this.TextBox2.Text = this.TextBox1.Text.ToString();
                    this.TextBox1.Text = ddlCarMake.SelectedItem.ToString();
                }
                else
                {
                    this.TextBox1.Text = ddlCarMake.SelectedItem.ToString();
                    this.TextBox2.Text = this.TextBox1.Text.ToString();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (ddlCity.SelectedIndex > 0)
        //    {
        //        ddlLocation.Items.Clear();

        //        Cls_Dealer objDealer = new Cls_Dealer();
        //        objDealer.City = Convert.ToInt16(ddlCity.SelectedValue.ToString());
        //        DataTable dtLocations = objDealer.GetAllLocationsOfCity();
        //        ddlLocation.DataSource = dtLocations;
        //        ddlLocation.DataBind();

        //        if (ddlLocation.Items.Count == 0)
        //            ddlLocation.Items.Insert(0, new ListItem("No Location Found", "-Select-"));
        //        else
        //        {
        //            if (string.IsNullOrEmpty(hdfID.Value))
        //                ddlLocation.Items.Insert(0, new ListItem("-Select Location-", "-Select-"));
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{ logger.Error("ddlCity_SelectedIndexChanged Event :" + ex.Message); }
    }

    protected void ImagebtnSearch_Click(object sender, ImageClickEventArgs e)
    {
        lblFlag = false;
        SearchDealer();
    }

    public DataTable SearchDealer2()
    {
        return null;
    }

    public DataTable SearchDealer()
    {
        DataTable dt = new DataTable();
        try
        {
            GridView gvDealerDetails = (GridView)this.Parent.FindControl("gvDealerDetails");

            int int_count = 0;
            string[] values = new string[11];  //When Add a enabled control in this page, number should be added one

            objDealer = new Cls_Dealer();
            objDealer.Name = txtName.Text.Replace('.', ' ');
            objDealer.Company = txtCompany.Text;
            objDealer.Email = txtEmail.Text;
            objDealer.Phone = txtPhone.Text;
            objDealer.Fax = txtFax.Text;
            objDealer.Mobile = txtMobile.Text;
            values[0] = objDealer.Name;
            values[1] = objDealer.Company;
            values[2] = objDealer.Email;
            values[3] = objDealer.Phone;
            values[4] = objDealer.Fax;
            values[10] = objDealer.Mobile;

            if (ddlState.SelectedValue.ToString() == "-Select-" || ddlState.SelectedValue.ToString() == "" || ddlState.SelectedValue.ToString() == "-Select")
            {
                values[5] = "";
            }
            else
            {
                values[5] = ddlState.SelectedValue.ToString();
            }
            if (ddlCity.SelectedValue.ToString() == "-Select-" || ddlCity.SelectedValue.ToString() == "" || ddlCity.SelectedValue.ToString() == "-Select")
            {
                values[6] = "";
            }
            else
            {
                values[6] = ddlCity.SelectedValue.ToString();
            }
            if (ddlLocation.SelectedValue.ToString() == "-Select-" || ddlLocation.SelectedValue.ToString() == "" || ddlLocation.SelectedValue.ToString() == "-Select")
            {
                values[7] = "";
            }
            else
            {
                values[7] = ddlLocation.SelectedValue.ToString();
            }
            if (txtPCode.Text.ToString() == "")
            {
                values[8] = "";
            }
            else
            {
                values[8] = txtPCode.Text.ToString();
            }
            if (ddlCarMake.SelectedValue.ToString() == "-Select-" || ddlCarMake.SelectedValue.ToString() == "" || ddlCarMake.SelectedValue.ToString() == "-Select")
            {
                values[9] = "";
            }
            else
            {
                values[9] = ddlCarMake.SelectedItem.ToString();
            }
            /*
            if(ddlCity.SelectedValue.ToString() == "-Select-" || ddlCity.SelectedValue.ToString() == "" || ddlCity.SelectedValue.ToString() == "-Select")
            {
                values[10] = "";
            }
            else
            {
                values[10] = ddlCity.SelectedItem.ToString();
            }
             * */
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].ToString() == "")
                {
                    int_count++;
                }
            }
            if (int_count == 11)   //When Add a enabled control in this page, number should be added one
            {
                objDealer = new Cls_Dealer();
                dt = objDealer.GetAllDealers();
            }
            else
            {
                dt = objDealer.SearchDealer(values, int_count);
            }
            gvDealerDetails.DataSource = dt;
            gvDealerDetails.DataBind();

        }
        catch (Exception ex)
        {
            logger.Error("ImagebtnSearch_Click Event :" + ex.Message);
        }
        return dt;
    }

    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Page.Validate("VGSubmit");
            if (Page.IsValid)
            {
                ImagebtnSearch.Enabled = true;
                ImagebtnSearch.ImageUrl = "~/Images/Search_dealer.gif";
                objDealer = new Cls_Dealer();
                objDealer.Name = txtName.Text.Replace('.', ' ');
                objDealer.Company = txtCompany.Text;
                objDealer.Email = txtEmail.Text;
                objDealer.Phone = txtPhone.Text;
                objDealer.Fax = txtFax.Text;
                objDealer.Address = txtLocation.Text;
                objDealer.State = ddlState.SelectedItem.ToString();
                objDealer.StateId = ddlState.SelectedValue.ToString();
                objDealer.City = ddlCity.SelectedValue.ToString();
                //objDealer.City = Convert.ToInt16(ddlCity.SelectedValue.ToString());
                //objDealer.Location = Convert.ToInt16(ddlLocation.SelectedValue.ToString());
                objDealer.Pcode = Convert.ToInt16(txtPCode.Text).ToString();
                objDealer.Mobile = txtMobile.Text;
                objDealer.Make = ddlCarMake.SelectedItem.ToString();
                objDealer.OldMake = this.TextBox2.Text;

                int Result = 0;
                if (objDealer.CheckIFDealerExist().Rows.Count == 0)
                {
                    if (hdfDBOperation.Value.ToString().Equals(DbOperations.INSERT) || String.IsNullOrEmpty(hdfDBOperation.Value.ToString()))
                    {
                        objDealer.DBOperation = DbOperations.INSERT;
                        Result = objDealer.AddDealer();
                    }
                    else
                    {
                        objDealer.DBOperation = DbOperations.UPDATE;
                        objDealer.ID = Convert.ToInt16(hdfID.Value.ToString());
                        Result = objDealer.UpdateDealer();
                    }




                    if (Result == 1 || Result == 2)
                    {
                        if (hdfDBOperation.Value.ToString().Equals(DbOperations.INSERT) || String.IsNullOrEmpty(hdfDBOperation.Value.ToString()))
                            lblResult.Text = "Dealer Added Successfully";
                        else
                            lblResult.Text = "Dealer Updated Successfully";
                    }
                    else
                    {
                        if (hdfDBOperation.Value.ToString().Equals(DbOperations.INSERT) || String.IsNullOrEmpty(hdfDBOperation.Value.ToString()))
                            lblResult.Text = "Failed to add Dealer";
                        else
                            lblResult.Text = "Failed to Update Dealer";
                    }

                    GridView gvDealerDetails = (GridView)this.Parent.FindControl("gvDealerDetails");
                    BindDealers(gvDealerDetails);
                    ClearFields();
                }
                else
                {
                    lblResult.Text = "Dealer " + objDealer.Name + " already exists.";
                }
            }
        }
        catch (Exception ex)
        { logger.Error("imgbtnAdd_Click Event :" + ex.Message); }
    }
    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearFields();
        }
        catch (Exception ex)
        { logger.Error("imgbtnCancel_Click Event :" + ex.Message); }
    }
    #endregion



    #region Functions
    private void BindDealers(GridView gvDealerDetails)
    {
        try
        {
            objDealer = new Cls_Dealer();
            DataTable dtDealers = objDealer.GetAllDealers();
            gvDealerDetails.DataSource = dtDealers;
            gvDealerDetails.DataBind();
        }
        catch (Exception ex) { logger.Error("BindDealers Function :" + ex.Message); }
    }
    private void BindStates()
    {
        try
        {
            ddlState.Items.Clear();
            Cls_State objState = new Cls_State();
            DataTable dtStates = objState.GetAllActiveStates();
            ddlState.DataSource = dtStates;
            ddlState.DataBind();

            if (ddlState.Items.Count == 0)
                ddlState.Items.Insert(0, new ListItem("No States Found", "-Select-"));
            else
                ddlState.Items.Insert(0, new ListItem("-Select State-", "-Select-"));

        }
        catch (Exception ex)
        { logger.Error("BindStates Function :" + ex.Message); }
    }

    private void BindCarMakes()
    {
        try
        {
            ddlCarMake.Items.Clear();
            Miles_Cls_CarMake objCarMake = new Miles_Cls_CarMake();
            DataTable dtCarMakes = objCarMake.GetAllCarMakes();
            ddlCarMake.DataSource = dtCarMakes;
            ddlCarMake.DataBind();

            if (ddlCarMake.Items.Count == 0)
                ddlCarMake.Items.Insert(0, new ListItem("No Car Makes Found", "-Select-"));
            else
                ddlCarMake.Items.Insert(0, new ListItem("-Select Car Make-", "-Select-"));

        }
        catch (Exception ex)
        {

        }
    }

    private void BindCities()
    {
        try
        {
            ddlCity.Items.Clear();
            Miles_Cls_City objCity = new Miles_Cls_City();
            DataTable dtCities = objCity.GetAllCities();
            ddlCity.DataSource = dtCities;
            ddlCity.DataBind();

            if (ddlCity.Items.Count == 0)
                ddlCity.Items.Insert(0, new ListItem("No Cities Found", "-Select-"));
            else
                ddlCity.Items.Insert(0, new ListItem("-Select Cities-", "-Select-"));

        }
        catch (Exception ex)
        {

        }
    }

    private void ClearFields()
    {
        try
        {
            ImagebtnSearch.Enabled = true;
            ImagebtnSearch.ImageUrl = "~/Images/Search_dealer.gif";
            txtCompany.Text = txtEmail.Text = txtFax.Text = txtName.Text = txtPCode.Text = txtPhone.Text = txtMobile.Text = txtLocation.Text= "";

            if (ddlState.Items.Count > 0)
                ddlState.SelectedIndex = 0;

            ddlState_SelectedIndexChanged(null, null);

            if (ddlCarMake.Items.Count > 0)
                ddlCarMake.SelectedIndex = 0;

            if (ddlCity.Items.Count > 0)
                ddlCity.SelectedIndex = 0;

            //ddlCity.Items.Clear();
            //ddlCity.Items.Insert(0, new ListItem("- Please select the state -", "-Select"));
            ddlLocation.Items.Clear();
            ddlLocation.Items.Insert(0, new ListItem("- Please Enter Postal Code To get Locations -", "-Select"));


            hdfDBOperation.Value = "";

        }
        catch (Exception ex) { logger.Error("ClearFields Function :" + ex.Message); }
    }

    public void SetHiddenFileds()
    {

        try
        {
            hdfID.Value = DealerID.ToString();
            hdfDBOperation.Value = DBOperation;
            PopulateFileds();

        }
        catch (Exception ex)
        { logger.Error("SetHiddenFileds Function :" + ex.Message); }
    }

    private void PopulateFileds()
    {
        try
        {

            objDealer = new Cls_Dealer();
            objDealer.ID = Convert.ToInt16(hdfID.Value.ToString());
            DataTable dtDealerDetails = null;
            dtDealerDetails = this.objDealer.GetDealerDetails();
            ImagebtnSearch.Enabled = false;
            ImagebtnSearch.ImageUrl = "~/Images/disable_search_dealer.gif";
            if (dtDealerDetails != null)
            {
                if (dtDealerDetails.Rows.Count > 0)
                {
                    this.txtCompany.Text = dtDealerDetails.Rows[0]["Company"].ToString();
                    this.txtEmail.Text = dtDealerDetails.Rows[0]["Email"].ToString();
                    this.txtFax.Text = dtDealerDetails.Rows[0]["Fax"].ToString();
                    this.txtName.Text = dtDealerDetails.Rows[0]["Name"].ToString();
                    this.txtPCode.Text = dtDealerDetails.Rows[0]["PCode"].ToString();
                    this.txtMobile.Text = dtDealerDetails.Rows[0]["Mobile"].ToString();
                    if (this.txtPCode.Text.Length == 3)
                        this.txtPCode.Text = this.txtPCode.Text.PadLeft(4, '0');
                    lbtGetLocations_Click(null, null);

                    ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(dtDealerDetails.Rows[0]["LocationID"].ToString()));
                    this.txtPhone.Text = dtDealerDetails.Rows[0]["Phone"].ToString();
                    this.BindStates();
                    //this.ddlState.SelectedIndex = this.ddlState.Items.IndexOf(this.ddlState.Items.FindByValue(dtDealerDetails.Rows[0][10].ToString()));
                    if (dtDealerDetails.Rows[0]["StateId"].ToString() == "")
                    {
                        this.ddlState.SelectedIndex = 0;
                    }
                    else
                    {
                        this.ddlState.SelectedIndex = (Convert.ToInt32(dtDealerDetails.Rows[0]["StateId"].ToString()));
                    }


                    this.BindCarMakes();

                    if (dtDealerDetails.Rows[0]["Make"].ToString() == "")
                    {
                        this.ddlCarMake.SelectedIndex = 0;
                    }
                    else
                    {
                        this.ddlCarMake.SelectedIndex = this.ddlCarMake.Items.IndexOf(this.ddlCarMake.Items.FindByText(dtDealerDetails.Rows[0]["Make"].ToString()));
                        ddlCarMakeStorageFunction();
                    }

                    this.BindCities();

                    if (dtDealerDetails.Rows[0]["CityId"].ToString() == "")
                    {
                        this.ddlCity.SelectedIndex = 0;
                    }
                    else
                    {
                        this.ddlCity.SelectedIndex = this.ddlCity.Items.IndexOf(this.ddlCity.Items.FindByText(dtDealerDetails.Rows[0]["CityId"].ToString()));
                    }
                    #region previousdeveloper
                    //if (ddlState.SelectedIndex == 0)
                    //{
                    //    ddlState.Items.Clear();
                    //    ddlStateReq.InitialValue = "-Select-";
                    //    ddlState.Items.Insert(0, new ListItem("State Not Found", "-Select-"));
                    //    //ddlCity.Items.Insert(0, new ListItem("City Not Found", "-Select-"));
                    //   // ddlLocation.Items.Insert(0, new ListItem("Loaction Not Found", "-Select-"));
                    //}
                    //else
                    //{
                    //    this.ddlState_SelectedIndexChanged(null, null);
                    //   // this.ddlCity.SelectedIndex = this.ddlCity.Items.IndexOf(this.ddlCity.Items.FindByValue(dtDealerDetails.Rows[0]["CityID"].ToString()));
                    //   // if (ddlCity.SelectedIndex == 0)
                    //   // {
                    //   //     CityReq.InitialValue = "-Select-";
                    //   //     ddlCity.Items.Insert(0, new ListItem("City Not Found", "-Select-"));
                    //   ////     ddlLocation.Items.Insert(0, new ListItem("Loaction Not Found", "-Select-"));
                    //   // }
                    //   // else
                    //   // {

                    //   //     this.ddlCity_SelectedIndexChanged(null, null);
                    //   //     LocationReq.InitialValue = "-Select-";
                    //   //   //  this.ddlLocation.SelectedIndex = this.ddlLocation.Items.IndexOf(this.ddlLocation.Items.FindByValue(dtDealerDetails.Rows[0]["CityID"].ToString()));
                    //   // }
                    //}
                    #endregion

                }
            }
        }
        catch (Exception ex)
        { logger.Error("PopulateFileds Function :" + ex.Message); }
    }

    #endregion
    protected void lbtGetLocations_Click(object sender, EventArgs e)
    {
        Cls_CustomerMaster objCustomer = new Cls_CustomerMaster();
        int PostalCode = 0;
        if (!(String.IsNullOrEmpty(txtPCode.Text)))
            PostalCode = Convert.ToInt16(txtPCode.Text);
        objCustomer.PostalCode = PostalCode.ToString();
        DataTable dtSuburbs = objCustomer.GetSuburbsOfThePostalCode();

        ddlLocation.Items.Clear();
        ddlLocation.DataSource = dtSuburbs;
        ddlLocation.DataBind();

        if (String.IsNullOrEmpty(txtPCode.Text))
        {
            ddlLocation.Items.Insert(0, new ListItem("- Please Enter Postal Code First -", "-Select-"));
            return;
        }
        if (ddlLocation.Items.Count == 0)
            ddlLocation.Items.Insert(0, new ListItem("- No Loaction Found -", "-Select-"));
        else
            ddlLocation.Items.Insert(0, new ListItem("- Select Location -", "-Select-"));
    }
}
