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
using log4net;

public partial class User_Controls_UCCustomerCRUD : System.Web.UI.UserControl
{

    #region Properties
    private int _CustomerID;

    public int CustomerID
    {
        get { return _CustomerID; }
        set { _CustomerID = value; }
    }

    private String _DBOperation;

    public String DBOperation
    {
        get { return _DBOperation; }
        set { _DBOperation = value; }
    }


    #endregion


    Cls_CustomerMaster objCustomer = null;
    ILog logger = LogManager.GetLogger(typeof(User_Controls_UCCustomerCRUD));

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbtGetLocations_Click(null, null);
        }
    }
    protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Page.Validate("VGSubmit");
            if (Page.IsValid)
            {
                objCustomer = new Cls_CustomerMaster();
                objCustomer.FirstName = txtFirstName.Text;
                objCustomer.MiddleName = txtMiddleName.Text;
                objCustomer.Email = txtEmail.Text;
                objCustomer.PhoneNo = txtPhone.Text;
                objCustomer.LastName = txtLastName.Text;
                objCustomer.Address = txtAddress.Text;
                objCustomer.PostalCode = txtPCode.Text;
                objCustomer.CreatedBy = Convert.ToInt16(Session[Cls_Constants.LOGGED_IN_USERID]);
                //if (ddlLocation.SelectedIndex != 0)
                objCustomer.LocationID = Convert.ToInt32(ddlLocation.SelectedValue.ToString());

                int Result = 0;

                if (hdfDBOperation.Value.ToString().Equals(DbOperations.INSERT) || String.IsNullOrEmpty(hdfDBOperation.Value.ToString()))
                {
                    if (objCustomer.CheckIfCustomerExists().Rows.Count == 0)
                    {
                        objCustomer.DBOperation = DbOperations.INSERT;
                        Result = objCustomer.AddCustomer();
                    }
                    else
                    {
                        lblResult.Text = "Customer " + objCustomer.FirstName + " " + objCustomer.MiddleName + " " + objCustomer.LastName + " already exists.";
                    }

                }
                else
                {
                    objCustomer.DBOperation = DbOperations.UPDATE;
                    objCustomer.ID = Convert.ToInt16(hdfID.Value.ToString());
                    Result = objCustomer.UpdateCustomer();
                }




                if (Result == 1)
                {
                    if (hdfDBOperation.Value.ToString().Equals(DbOperations.INSERT) || String.IsNullOrEmpty(hdfDBOperation.Value.ToString()))
                        lblResult.Text = "Customer Added Successfully";
                    else
                        lblResult.Text = "Customer Updated Successfully";
                }
                else
                {
                    if (hdfDBOperation.Value.ToString().Equals(DbOperations.INSERT) || String.IsNullOrEmpty(hdfDBOperation.Value.ToString()))
                        lblResult.Text = "Failed to add Customer";
                    else
                        lblResult.Text = "Failed to Update Customer";
                }

                GridView gvCustomerDetails = (GridView)this.Parent.FindControl("gvCustomerDetails");
                BindData(gvCustomerDetails);
                ClearFields();

            }
        }
        catch (Exception ex)
        { logger.Error("imgbtnAdd_Click Event :" + ex.Message); }
    }

    private void BindData(GridView gvCustomerDetails)
    {
        try
        {
            objCustomer = new Cls_CustomerMaster();
            DataTable dtCustomers = objCustomer.GetAllCustomers();

            //DataView dv = dtCustomers.DefaultView;
            //dv.Sort = string.Format("{0} {1}", ViewState[Cls_Constants.VIEWSTATE_SORTEXPRESSION].ToString(), ViewState[Cls_Constants.VIEWSTATE_SORTDIRECTION].ToString());
            //dtCustomers = dv.ToTable();

            gvCustomerDetails.DataSource = dtCustomers;
            gvCustomerDetails.DataBind();
        }
        catch (Exception ex) { logger.Error("BindCustomers Function :" + ex.Message); }
    }

    public void SetHiddenFileds()
    {

        try
        {
            hdfID.Value = CustomerID.ToString();
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
            objCustomer = new Cls_CustomerMaster();
            objCustomer.ID = Convert.ToInt16(hdfID.Value.ToString());
            DataTable dtCustomerDetails = null;
            dtCustomerDetails = this.objCustomer.GetCustomerDetails();

            if (dtCustomerDetails != null)
            {
                if (dtCustomerDetails.Rows.Count > 0)
                {
                    this.txtFirstName.Text = dtCustomerDetails.Rows[0]["FirstName"].ToString();
                    this.txtEmail.Text = dtCustomerDetails.Rows[0]["Email"].ToString();
                    this.txtMiddleName.Text = dtCustomerDetails.Rows[0]["MiddleName"].ToString();
                    this.txtLastName.Text = dtCustomerDetails.Rows[0]["LastName"].ToString();
                    this.txtPCode.Text = dtCustomerDetails.Rows[0]["PostalCode"].ToString();
                    if (this.txtPCode.Text.Length == 3)
                        this.txtPCode.Text = this.txtPCode.Text.PadLeft(4, '0');
                    lbtGetLocations_Click(null, null);
                    ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(dtCustomerDetails.Rows[0]["LocationID"].ToString()));
                    this.txtPhone.Text = dtCustomerDetails.Rows[0]["PhoneNo"].ToString();
                    this.txtAddress.Text = dtCustomerDetails.Rows[0]["Address"].ToString();


                }
            }
        }
        catch (Exception ex)
        { logger.Error("PopulateFileds Function :" + ex.Message); }
    }

    private void ClearFields()
    {
        try
        {
            this.txtFirstName.Text = this.txtEmail.Text = this.txtMiddleName.Text = this.txtLastName.Text = this.txtPCode.Text = this.txtPhone.Text = this.txtAddress.Text = "";
            hdfDBOperation.Value = "";
            lbtGetLocations_Click(null, null);
        }
        catch (Exception ex) { logger.Error("ClearFields Function :" + ex.Message); }
    }

    protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        ClearFields();
    }
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
