using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text.pdf;
using System.IO;

public partial class ClientDealerContract : System.Web.UI.Page
{
    static Cls_ClientDealerContract CDC = new Cls_ClientDealerContract();

    protected void Page_Load(object sender, EventArgs e)
    {
        string ReqID = Request.QueryString["ReqID"];
        string QuoteID = Request.QueryString["QuoteID"];
        DataTable DealerInfo = CDC.SearchDealerInfo(ReqID);
        DataTable CustomerInfo = CDC.SearchCustomerInfo(ReqID);
        DataTable HeaderInfo = CDC.SearchHeaderInfo(ReqID);
        DataTable ParameterInfo = CDC.SearchRequestParametersByReqID(ReqID);
        DataTable OtherInfo = CDC.SearchConsultantDeliveryDateByQuoteID(QuoteID);
        
        if(!IsPostBack)
        {
            this.BindStateXml();
            this.BindCities();
            this.txtCustomerName.Text = CustomerInfo.Rows[0]["Fname"].ToString() + " " + CustomerInfo.Rows[0]["LName"].ToString();
            this.txtEmail.Text = CustomerInfo.Rows[0]["Email"].ToString();
            this.txtAddress.Text = CustomerInfo.Rows[0]["Add1"].ToString() + " " + CustomerInfo.Rows[0]["Add2"].ToString();
            this.txtFax.Text = CustomerInfo.Rows[0]["Fax"].ToString();
            this.txtMobile.Text = CustomerInfo.Rows[0]["Mobile"].ToString();
            this.txtPhone.Text = CustomerInfo.Rows[0]["Phone"].ToString();
            this.txtPostCode.Text = CustomerInfo.Rows[0]["PostalCode"].ToString();
            this.ddlCity.Items.FindByText(CustomerInfo.Rows[0]["City"].ToString()).Selected = true;
            this.ddlState.Items.FindByText(CustomerInfo.Rows[0]["State"].ToString()).Selected = true;
            this.txtConsultant.Text = OtherInfo.Rows[0]["Name"].ToString();

            this.BindCarMakes();
            this.ddlCarMake.Items.FindByText(HeaderInfo.Rows[0]["Make"].ToString()).Selected = true;
            this.txtModel.Text = HeaderInfo.Rows[0]["Model"].ToString();
            this.txtSeries.Text = HeaderInfo.Rows[0]["Series"].ToString();

            this.BindFuelTypeXml();
            this.BindTransmissionXml();
            if (ParameterInfo.Rows[4]["ParamValue"].ToString() != "") this.ddlFuelType.Items.FindByText(ParameterInfo.Rows[4]["ParamValue"].ToString()).Selected = true;
            if (ParameterInfo.Rows[2]["ParamValue"] != null) this.ddlTransmission.Items.FindByText(ParameterInfo.Rows[2]["ParamValue"].ToString()).Selected = true;
            this.txtBodyShape.Text = ParameterInfo.Rows[0]["ParamValue"].ToString();
            this.txtBodyColor.Text = ParameterInfo.Rows[3]["ParamValue"].ToString();
            this.txtEstimatedDeliveryDate.Text = OtherInfo.Rows[0]["EstimatedDeliveryDate"].ToString();

            this.BindSuppilers();
            this.ddlSupplier.Items.FindByText(DealerInfo.Rows[0]["Name"].ToString()).Selected = true;

            this.BindRegistrationXml();
        }
    }

    private void BindTransmissionXml()
    {
        string filePath = Server.MapPath("~/Transmission.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddlTransmission.DataSource = ds;
            ddlTransmission.DataTextField = "name";
            ddlTransmission.DataValueField = "id";
            ddlTransmission.DataBind();
        }
    }

    private void BindFuelTypeXml()
    {
        string filePath = Server.MapPath("~/FuelType.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddlFuelType.DataSource = ds;
            ddlFuelType.DataTextField = "name";
            ddlFuelType.DataValueField = "id";
            ddlFuelType.DataBind();
        }
    }

    private void BindStateXml()
    {
        string filePath = Server.MapPath("~/State.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddlState.DataSource = ds;
            ddlState.DataTextField = "name";
            ddlState.DataValueField = "id";
            ddlState.DataBind();
        }
    }

    private void BindCities()
    {
        try
        {
            ddlCity.Items.Clear();
            Miles_Cls_City objCity = new Miles_Cls_City();
            DataTable dtCities = objCity.GetAllCitiesForClients();
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

    private void BindSuppilers()
    {
        try
        {
            ddlSupplier.Items.Clear();
            Miles_Cls_Dealer objSupplier = new Miles_Cls_Dealer();
            DataTable dtSuppliers = objSupplier.GetAllDealers();
            ddlSupplier.DataSource = dtSuppliers;
            ddlSupplier.DataBind();

            if (ddlSupplier.Items.Count == 0)
                ddlSupplier.Items.Insert(0, new ListItem("No Supplier Found", "-Select-"));
            else
                ddlSupplier.Items.Insert(0, new ListItem("-Select Supplier-", "-Select-"));
        }
        catch (Exception ex)
        {

        }
    }

    private void BindRegistrationXml()
    {
        string filePath = Server.MapPath("~/Registration.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddlRegistration.DataSource = ds;
            ddlRegistration.DataTextField = "name";
            ddlRegistration.DataValueField = "id";
            ddlRegistration.DataBind();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //this.txtPrice.Text = ddlCarMake.SelectedItem.Value +ddlCarMake.SelectedItem.Text;  
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        DataTable DealerInfo = CDC.SearchDealerInfo(Request.QueryString["ReqID"]);
        string pdfTemplate = @"E:\ContractNoTrade.pdf";
        string newFile = @"E:\ContractNoTrade2.pdf";

        PdfReader pdfReader = new PdfReader(pdfTemplate);
        PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
        AcroFields pdfFormFields = pdfStamper.AcroFields;

        pdfFormFields.SetField("ContactP1", this.txtCustomerName.Text);
        pdfFormFields.SetField("VNMSP1", this.txtVehicleYear.Text + " " + this.ddlCarMake.SelectedItem.Text + " " + this.txtModel.Text + " " + this.txtSeries.Text);
        pdfFormFields.SetField("VNP2", this.txtVehicleYear.Text + " " + this.ddlCarMake.SelectedItem.Text);
        pdfFormFields.SetField("MSBP2", this.txtModel.Text + " " + this.txtSeries.Text + " " + this.txtBodyShape.Text);
        pdfFormFields.SetField("TSP2", this.ddlTransmission.SelectedItem.Text);
        pdfFormFields.SetField("MSBP2", this.txtModel.Text + " " + this.txtSeries.Text + " " + this.txtBodyShape.Text);
        pdfFormFields.SetField("FTP2", this.ddlFuelType.SelectedItem.Text);
        pdfFormFields.SetField("BCP2", this.txtBodyColor.Text);

        pdfFormFields.SetField("FNP3", this.txtCustomerName.Text);
        pdfFormFields.SetField("MNP3", this.txtMemberNo.Text);
        pdfFormFields.SetField("CTP3", "");  //Card Type
        pdfFormFields.SetField("CDNP3", "");  //Card Number
        pdfFormFields.SetField("EEP3", "");  //Exp Month Exp Year
        pdfFormFields.SetField("CVNP3", "");  //CV Number

        pdfFormFields.SetField("DealerCompanyNameP4", DealerInfo.Rows[0]["Company"].ToString());
        pdfFormFields.SetField("DealerAddressP4", DealerInfo.Rows[0]["Address"].ToString());
        pdfFormFields.SetField("CityStatePCodeP4", DealerInfo.Rows[0]["City"].ToString() + " " + DealerInfo.Rows[0]["State"].ToString() + " " + DealerInfo.Rows[0]["PCode"].ToString());  //Card Number
        pdfFormFields.SetField("DealerNameP4", DealerInfo.Rows[0]["Name"].ToString());
        pdfFormFields.SetField("DealerPhoneP4", DealerInfo.Rows[0]["Phone"].ToString());
        pdfFormFields.SetField("DealerEmailP4", DealerInfo.Rows[0]["Email"].ToString());
        pdfFormFields.SetField("ConsultantP4", this.txtConsultant.Text);
        // flatten the form to remove editting options, set it to false
        // to leave the form open to subsequent manual edits
        pdfStamper.FormFlattening = false;

        pdfStamper.Close();
        pdfReader.Close();
    }
}