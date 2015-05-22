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
    DataTable DealerInfo;
    DataTable CustomerInfo;
    DataTable HeaderInfo;
    DataTable ParameterInfo;
    DataTable OtherInfo;
    DataTable CreditCardInfo;
    string ReqID = "";
    string QuoteID = "";
    string ConsID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ReqID = Request.QueryString["ReqID"];
        this.QuoteID = Request.QueryString["QuoteID"];
        this.ConsID = Request.QueryString["ConsID"];
        this.CustomerInfo = CDC.SearchCustomerInfo(ReqID);
        this.DealerInfo = CDC.SearchDealerInfo(ReqID);    
        this.HeaderInfo = CDC.SearchHeaderInfo(ReqID);
        this.ParameterInfo = CDC.SearchRequestParametersByReqID(ReqID);
        this.OtherInfo = CDC.SearchConsultantDeliveryDateByQuoteID(QuoteID);
        this.CreditCardInfo = CDC.SearchCreditCard(Convert.ToInt64(this.CustomerInfo.Rows[0]["Id"].ToString()));
        
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
            this.txtSurName.Text = CustomerInfo.Rows[0]["LName"].ToString();

            this.BindCarMakes();
            this.ddlCarMake.Items.FindByText(HeaderInfo.Rows[0]["Make"].ToString()).Selected = true;
            this.txtModel.Text = HeaderInfo.Rows[0]["Model"].ToString();
            this.txtSeries.Text = HeaderInfo.Rows[0]["Series"].ToString();

            this.BindFuelTypeXml();
            this.BindTransmissionXml();
            if (ParameterInfo.Rows[4]["ParamValue"].ToString() != "") this.ddlFuelType.Items.FindByText(ParameterInfo.Rows[4]["ParamValue"].ToString()).Selected = true;
            if (ParameterInfo.Rows[2]["ParamValue"] != null && ParameterInfo.Rows[2]["ParamValue"].ToString() !="") this.ddlTransmission.Items.FindByText(ParameterInfo.Rows[2]["ParamValue"].ToString()).Selected = true;
            this.txtBodyShape.Text = ParameterInfo.Rows[0]["ParamValue"].ToString();
            this.txtBodyColor.Text = ParameterInfo.Rows[3]["ParamValue"].ToString();
            this.txtEstimatedDeliveryDate.Text = OtherInfo.Rows[0]["EstimatedDeliveryDate"].ToString();

            this.BindSuppilers();
            this.ddlSupplier.Items.FindByText(DealerInfo.Rows[0]["Name"].ToString()).Selected = true;

            this.BindRegistrationXml();

            this.BindPrices();

            this.BindCardTypeXml();
            this.ddlCardType.Items.FindByText(CreditCardInfo.Rows[0]["CardType"].ToString()).Selected = true;
            this.txtCardNumber.Text = CreditCardInfo.Rows[0]["CardNumber"].ToString();
            this.txtCVNumber.Text = CreditCardInfo.Rows[0]["CVNumber"].ToString();
            this.txtMonth.Text = Convert.ToDateTime(CreditCardInfo.Rows[0]["ExpiryDate"].ToString()).Month.ToString();
            this.txtYear.Text = Convert.ToDateTime(CreditCardInfo.Rows[0]["ExpiryDate"].ToString()).Year.ToString();
            this.txtMemberNo.Text = CreditCardInfo.Rows[0]["MemberNo"].ToString();
            this.txtDeposit.Text = CreditCardInfo.Rows[0]["Deposit"].ToString();
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

    private void BindCardTypeXml()
    {
        string filePath = Server.MapPath("~/CardType.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddlCardType.DataSource = ds;
            ddlCardType.DataTextField = "name";
            ddlCardType.DataValueField = "id";
            ddlCardType.DataBind();
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

    private void BindPrices()
    {
        try
        {
            int AccessoryCounter = 0;
            DataTable PricesInfo = CDC.SearchPricesByReqIDConsID(this.ReqID, this.ConsID);
            foreach (DataRow Row in PricesInfo.Rows)
            {
                if (Row["SumCol"].ToString() == "0")
                {
                    if (Row["Description"].ToString() == "Recommended Retail Price Exc GST")
                    {
                        this.txtVehicleRetailPrice.Text = Row["QuoteValue"].ToString();
                    }

                    if (Row["Description"].ToString() == "Pre-Delivery")
                    {
                        this.txtPreDelivery.Text = Row["QuoteValue"].ToString();
                    }

                    if (Row["Description"].ToString() == "Stamp Duty")
                    {
                        this.txtStampDuty.Text = Row["QuoteValue"].ToString();
                    }

                    if (Row["Description"].ToString() == "Registration")
                    {
                        this.txtRegistrationPrice.Text = Row["QuoteValue"].ToString();
                    }

                    if (Row["Description"].ToString() == "CTP")
                    {
                        this.txtCTP.Text = Row["QuoteValue"].ToString();
                    }

                    if (Row["Description"].ToString() == "GST ( LCT if applicable)")
                    {
                        this.txtGST.Text = Row["QuoteValue"].ToString();
                    }

                    if (Row["Description"].ToString() == "Premium Plate Fee")
                    {
                        this.txtPlateFee.Text = Row["QuoteValue"].ToString();
                    }

                    if (Row["Description"].ToString() == "Total Cost of Accessories")
                    {           
                        this.txtTotalAccessories.Text = Convert.ToString(Convert.ToDouble(this.txtAccessory1.Text)
                                                        + Convert.ToDouble(this.txtAccessory2.Text)
                                                        + Convert.ToDouble(this.txtAccessory3.Text)
                                                        + Convert.ToDouble(this.txtAccessory4.Text));
                    }

                    if (Row["Description"].ToString() == "Total-On Road Cost (Inclusive of GST)")
                    {
                        this.txtTotalOnRoadCost.Text = Row["QuoteValue"].ToString();
                    }

                    if (Row["Description"].ToString() == "Fleet Discount")
                    {
                        this.txtFleetDiscount.Text = Row["QuoteValue"].ToString();
                    }
                }
                else
                {
                    if(AccessoryCounter == 0)
                    {
                        this.Label37.Text = Row["Description"].ToString();
                        this.txtAccessory1.Text = Row["QuoteValue"].ToString();
                    }

                    if (AccessoryCounter == 1)
                    {
                        this.Label38.Text = Row["Description"].ToString();
                        this.txtAccessory2.Text = Row["QuoteValue"].ToString();
                    }

                    if (AccessoryCounter == 2)
                    {
                        this.Label39.Text = Row["Description"].ToString();
                        this.txtAccessory3.Text = Row["QuoteValue"].ToString();
                    }

                    if (AccessoryCounter == 3)
                    {
                        this.Label40.Text = Row["Description"].ToString();
                        this.txtAccessory4.Text = Row["QuoteValue"].ToString();
                    }

                    AccessoryCounter++;
                }
            }

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
        //string pdfTemplate = @"E:\ContractNoTrade.pdf";
        //string newFile = @"E:\ContractNoTrade2.pdf";
        string pdfTemplate = Server.MapPath("~/Contract/ContractNoTrade.pdf");
        string newFile = Server.MapPath("~/Contract/ContractNoTrade2.pdf");

        PdfReader pdfReader = new PdfReader(pdfTemplate);
        PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
        AcroFields pdfFormFields = pdfStamper.AcroFields;

        pdfFormFields.SetField("ContactP1", this.txtCustomerName.Text);
        pdfFormFields.SetField("VNMSP1", this.txtVehicleYear.Text + " " + this.ddlCarMake.SelectedItem.Text + " " + this.txtModel.Text + " " + this.txtSeries.Text);

        pdfFormFields.SetField("CustomerContactDetail", this.txtCustomerName.Text 
            + "  of  " + this.txtCompany.Text
            + "  " + this.txtAddress.Text
            + "  " + this.ddlCity.SelectedItem.Text 
            + "  " + this.ddlState.SelectedItem.Text 
            + "  " + this.txtPostCode.Text);

        pdfFormFields.SetField("VNP2", this.txtVehicleYear.Text + " " + this.ddlCarMake.SelectedItem.Text);
        pdfFormFields.SetField("MSBP2", this.txtModel.Text + " " + this.txtSeries.Text + " " + this.txtBodyShape.Text);
        pdfFormFields.SetField("TSP2", this.ddlTransmission.SelectedItem.Text);
        pdfFormFields.SetField("MSBP2", this.txtModel.Text + " " + this.txtSeries.Text + " " + this.txtBodyShape.Text);
        pdfFormFields.SetField("FTP2", this.ddlFuelType.SelectedItem.Text);
        pdfFormFields.SetField("BCP2", this.txtBodyColor.Text);

        pdfFormFields.SetField("FNP3", this.txtCustomerName.Text);
        pdfFormFields.SetField("MNP3", this.txtMemberNo.Text);
        pdfFormFields.SetField("CTP3", this.ddlCardType.SelectedItem.Text);  //Card Type
        pdfFormFields.SetField("CDNP3", this.txtCardNumber.Text);  //Card Number
        pdfFormFields.SetField("EEP3", this.txtMonth.Text + "/" + this.txtYear.Text);  //Exp Month Exp Year
        pdfFormFields.SetField("CVNP3", this.txtCVNumber.Text);  //CV Number

        pdfFormFields.SetField("ContactP4", this.txtCustomerName.Text);
        pdfFormFields.SetField("PHMP4", this.txtPhone.Text + " " + this.txtMobile.Text);
        pdfFormFields.SetField("EmailP4", this.txtEmail.Text);
        pdfFormFields.SetField("HomeAddressP4", this.txtAddress.Text);
        pdfFormFields.SetField("HHPP4", this.ddlCity.SelectedItem.Text + " " + this.ddlState.SelectedItem.Text + " " + this.txtPostCode.Text);
        pdfFormFields.SetField("RegistrationP4", this.ddlRegistration.SelectedItem.Text);
        pdfFormFields.SetField("DealerCompanyNameP4", DealerInfo.Rows[0]["Company"].ToString());
        pdfFormFields.SetField("DealerAddressP4", DealerInfo.Rows[0]["Address"].ToString());
        pdfFormFields.SetField("CityStatePCodeP4", DealerInfo.Rows[0]["City"].ToString() + " " + DealerInfo.Rows[0]["State"].ToString() + " " + DealerInfo.Rows[0]["PCode"].ToString());  //Card Number
        pdfFormFields.SetField("DealerNameP4", DealerInfo.Rows[0]["Name"].ToString());
        pdfFormFields.SetField("DealerPhoneP4", DealerInfo.Rows[0]["Phone"].ToString());
        pdfFormFields.SetField("DealerEmailP4", DealerInfo.Rows[0]["Email"].ToString());
        pdfFormFields.SetField("ConsultantP4", this.txtConsultant.Text);
        pdfFormFields.SetField("DealerEmailP4", DealerInfo.Rows[0]["Email"].ToString());
        pdfFormFields.SetField("ConsultantP4", this.txtConsultant.Text);
        pdfFormFields.SetField("ConsultantPhoneP4", OtherInfo.Rows[0]["CunsPhone"].ToString());
        pdfFormFields.SetField("ConsultantEmailP4", OtherInfo.Rows[0]["CunsMail"].ToString());

        pdfFormFields.SetField("PreDelivery_Price", this.txtPreDelivery.Text);
        pdfFormFields.SetField("Stamp_Duty_Price", this.txtStampDuty.Text);
        pdfFormFields.SetField("Registration_Price", this.txtRegistrationPrice.Text);
        pdfFormFields.SetField("CTP_Price", this.txtCTP.Text);
        pdfFormFields.SetField("Plate_Fee_Price", this.txtPlateFee.Text);
        pdfFormFields.SetField("Insert Accessories_1", this.Label37.Text);
        pdfFormFields.SetField("Accessories_Price_1", this.txtAccessory1.Text);
        pdfFormFields.SetField("Insert Accessories_2", this.Label38.Text);
        pdfFormFields.SetField("Accessories_Price_2", this.txtAccessory2.Text);
        pdfFormFields.SetField("Insert Accessories_3", this.Label39.Text.TrimStart());
        pdfFormFields.SetField("Accessories_Price_3", this.txtAccessory3.Text);
        pdfFormFields.SetField("Insert Accessories_4", this.Label40.Text.TrimStart());
        pdfFormFields.SetField("Accessories_Price_4", this.txtAccessory4.Text);
        pdfFormFields.SetField("Retail Price of  Vehicle", this.txtVehicleRetailPrice.Text);
        pdfFormFields.SetField("FleetDiscount", this.txtFleetDiscount.Text);


        double TempTotalAccessories = Convert.ToDouble(this.txtAccessory1.Text)
            + Convert.ToDouble(this.txtAccessory2.Text)
            + Convert.ToDouble(this.txtAccessory3.Text)
            + Convert.ToDouble(this.txtAccessory4.Text);

        pdfFormFields.SetField("TotalAccessories", TempTotalAccessories.ToString());

        double TempTotalonRoadCost = Convert.ToDouble(this.txtVehicleRetailPrice.Text)
            + Convert.ToDouble(TempTotalAccessories)
            + Convert.ToDouble(this.txtPreDelivery.Text)
            + Convert.ToDouble(this.txtStampDuty.Text)
            + Convert.ToDouble(this.txtRegistrationPrice.Text)
            + Convert.ToDouble(this.txtCTP.Text)
            + Convert.ToDouble(this.txtPlateFee.Text)
            - Convert.ToDouble(this.txtFleetDiscount.Text);
        
        pdfFormFields.SetField("TotalonRoadCost", TempTotalonRoadCost.ToString());

        pdfFormFields.SetField("DeliveryDateP4", OtherInfo.Rows[0]["EstimatedDeliveryDate"].ToString());

        pdfFormFields.SetField("SupplierP5", DealerInfo.Rows[0]["Company"].ToString());
        
        // flatten the form to remove editting options, set it to false
        // to leave the form open to subsequent manual edits
        pdfStamper.FormFlattening = false;

        pdfStamper.Close();
        pdfReader.Close();

        //Download file
        this.LinkDownLoad.Visible = true;
        
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        DateTime ExpDate = new DateTime(Convert.ToInt32("20"+this.txtYear.Text), Convert.ToInt32(this.txtMonth.Text), 1);
        CDC.AddCreditCard(Convert.ToInt32(CustomerInfo.Rows[0]["Id"].ToString()), this.txtCVNumber.Text, this.txtCardNumber.Text, this.ddlCardType.SelectedItem.Text, ExpDate, this.txtMonth.Text, this.txtYear.Text, this.txtMemberNo.Text, this.txtDeposit.Text);
    }

    protected void AddDeliveryTrack_Click(object sender, EventArgs e)
    {
        CDC.AddDeliveryTrack(this.txtEmail.Text, this.txtMemberNo.Text, this.txtMemberNo.Text, CustomerInfo.Rows[0]["Fname"].ToString()
            , this.txtSurName.Text, this.txtCustomerName.Text ,this.txtEmail.Text, this.ddlState.SelectedItem.Text, this.ddlState.SelectedValue
            , this.ddlCity.SelectedItem.Text, this.txtPostCode.Text, this.txtAddress.Text, this.txtPhone.Text, this.txtMobile.Text
            , this.txtFax.Text, DealerInfo.Rows[0]["Company"].ToString(), DealerInfo.Rows[0]["Name"].ToString(), DealerInfo.Rows[0]["ID"].ToString()
            , DealerInfo.Rows[0]["Key"].ToString(), DealerInfo.Rows[0]["Phone"].ToString(), DealerInfo.Rows[0]["Email"].ToString()
            , this.ddlCarMake.SelectedItem.Text, this.ddlCarMake.SelectedValue, this.txtModel.Text, this.txtConsultant.Text, this.txtEstimatedDeliveryDate.Text
            , "0", CustomerInfo.Rows[0]["Id"].ToString(), this.ReqID);
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        
    }
}