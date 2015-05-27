using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text.pdf;
using System.IO;

public partial class ClientDealerContractT : System.Web.UI.Page
{
    static Cls_ClientDealerContract CDC = new Cls_ClientDealerContract();
    DataTable DealerInfo;
    DataTable CustomerInfo;
    DataTable HeaderInfo;
    DataTable ParameterInfo;
    DataTable OtherInfo;
    DataTable CreditCardInfo;
    DataTable TradeInInfo;
    string ReqID = "";
    string QuoteID = "";
    string ConsID = "";
    string TStatus = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ReqID = Request.QueryString["ReqID"];
        this.QuoteID = Request.QueryString["QuoteID"];
        this.ConsID = Request.QueryString["ConsID"];
        this.TStatus = Request.QueryString["TStatus"];
        this.CustomerInfo = CDC.SearchCustomerInfo(ReqID);
        this.DealerInfo = CDC.SearchDealerInfo(ReqID);
        this.HeaderInfo = CDC.SearchHeaderInfo(ReqID);
        this.ParameterInfo = CDC.SearchRequestParametersByReqID(ReqID);
        this.OtherInfo = CDC.SearchConsultantDeliveryDateByQuoteID(QuoteID);
        //if(this.CustomerInfo.Rows.Count !=0) this.CreditCardInfo = CDC.SearchCreditCard(Convert.ToInt64(this.CustomerInfo.Rows[0]["Id"].ToString()));
        this.TradeInInfo = CDC.SearchTradeInByReqID(ReqID);

        if (!IsPostBack)
        {
            this.BindStateXml();
            this.BindCities();
            this.BindDepositTakenXml();
            this.BindCarMakes();
            this.BindTCarMakes();
            this.BindFuelTypeXml();
            this.BindTFuelTypeXml();
            this.BindTransmissionXml();
            this.BindTTransmissionXml();
            this.BindLogBookXml();
            this.BindSuppilers();
            this.BindRegistrationXml();
            this.BindPrices();
            this.BindCardTypeXml();
            this.BindTradeStatusXml();

            if (this.ReqID != null && this.QuoteID != null || this.ConsID != null)
            {

                if (this.CustomerInfo.Rows.Count != 0) this.txtCustomerName.Text = CustomerInfo.Rows[0]["Fname"].ToString() + " " + CustomerInfo.Rows[0]["LName"].ToString();
                this.txtEmail.Text = CustomerInfo.Rows[0]["Email"].ToString();
                if (CustomerInfo.Columns.Contains("Add1"))
                {
                    this.txtAddress.Text = CustomerInfo.Rows[0]["Add1"].ToString() + " " + CustomerInfo.Rows[0]["Add2"].ToString();
                }
                else
                {
                    this.txtAddress.Text = CustomerInfo.Rows[0]["Address"].ToString();
                    this.txtMemberNo.Text = CustomerInfo.Rows[0]["MemberNo"].ToString();
                    this.txtDepositAmount.Text = CustomerInfo.Rows[0]["DepositAmount"].ToString();
                    this.txtMembershipFee.Text = CustomerInfo.Rows[0]["MembershipFee"].ToString();
                    this.ddlDepositTaken.Items.FindByText(CustomerInfo.Rows[0]["DepositTakenBy"].ToString()).Selected = true;
                    this.txtVehicleYear.Text = Convert.ToDateTime(CustomerInfo.Rows[0]["DeliveryDate"].ToString()).Year.ToString();
                }

                this.txtFax.Text = CustomerInfo.Rows[0]["Fax"].ToString();
                this.txtMobile.Text = CustomerInfo.Rows[0]["Mobile"].ToString();
                this.txtPhone.Text = CustomerInfo.Rows[0]["Phone"].ToString();
                this.txtPostCode.Text = CustomerInfo.Rows[0]["PostalCode"].ToString();
                if (CustomerInfo.Rows[0]["City"] != null && CustomerInfo.Rows[0]["City"].ToString() != "") this.ddlCity.Items.FindByText(CustomerInfo.Rows[0]["City"].ToString()).Selected = true;
                if (CustomerInfo.Rows[0]["State"] != null && CustomerInfo.Rows[0]["State"].ToString() != "") this.ddlState.Items.FindByText(CustomerInfo.Rows[0]["State"].ToString()).Selected = true;
                this.txtConsultant.Text = OtherInfo.Rows[0]["Name"].ToString();
                this.txtSurName.Text = CustomerInfo.Rows[0]["LName"].ToString();
                this.txtConsultantPhone.Text = OtherInfo.Rows[0]["CunsPhone"].ToString();
                this.txtConsultantMail.Text = OtherInfo.Rows[0]["CunsMail"].ToString();


                this.txtDealerCompany.Text = DealerInfo.Rows[0]["Company"].ToString();
                this.txtDealerName.Text = DealerInfo.Rows[0]["Name"].ToString();
                this.txtDealerEmail.Text = DealerInfo.Rows[0]["Email"].ToString();
                this.txtDealerPhone.Text = DealerInfo.Rows[0]["Phone"].ToString();
                this.txtDealerAddress.Text = DealerInfo.Rows[0]["Address"].ToString();
                this.txtDealerCity.Text = DealerInfo.Rows[0]["City"].ToString();
                this.txtDealerState.Text = DealerInfo.Rows[0]["State"].ToString();
                this.txtDealerPCode.Text = DealerInfo.Rows[0]["PCode"].ToString();


                if (HeaderInfo.Rows[0]["Make"] != null && HeaderInfo.Rows[0]["Make"].ToString() != "") this.ddlCarMake.Items.FindByText(HeaderInfo.Rows[0]["Make"].ToString()).Selected = true;
                this.txtModel.Text = HeaderInfo.Rows[0]["Model"].ToString();
                this.txtSeries.Text = HeaderInfo.Rows[0]["Series"].ToString();


                if (ParameterInfo.Rows[4]["ParamValue"] != null && ParameterInfo.Rows[4]["ParamValue"].ToString() != "")
                    try
                    {
                        this.ddlFuelType.Items.FindByText(ParameterInfo.Rows[4]["ParamValue"].ToString()).Selected = true;
                    }
                    catch (Exception)
                    {

                    }
                if (ParameterInfo.Rows[2]["ParamValue"] != null && ParameterInfo.Rows[2]["ParamValue"].ToString() != "")
                    try
                    {
                        this.ddlTransmission.Items.FindByText(ParameterInfo.Rows[2]["ParamValue"].ToString()).Selected = true;
                    }
                    catch (Exception)
                    {

                    }
                this.txtBodyShape.Text = ParameterInfo.Rows[0]["ParamValue"].ToString();
                this.txtBodyColor.Text = ParameterInfo.Rows[3]["ParamValue"].ToString();
                this.txtEstimatedDeliveryDate.Text = OtherInfo.Rows[0]["EstimatedDeliveryDate"].ToString();


                //if (DealerInfo.Rows[0]["Name"] != null && DealerInfo.Rows[0]["Name"].ToString() != "") this.ddlSupplier.Items.FindByText(DealerInfo.Rows[0]["Name"].ToString()).Selected = true;
                if (DealerInfo.Rows[0]["Company"] != null && DealerInfo.Rows[0]["Company"].ToString() != "") this.ddlSupplier.Items.FindByText(DealerInfo.Rows[0]["Company"].ToString()).Selected = true;

                //this.ddlCardType.Items.FindByText(CreditCardInfo.Rows[0]["CardType"].ToString()).Selected = true;
                //this.txtCardNumber.Text = CreditCardInfo.Rows[0]["CardNumber"].ToString();
                //this.txtCVNumber.Text = CreditCardInfo.Rows[0]["CVNumber"].ToString();
                //this.txtMonth.Text = Convert.ToDateTime(CreditCardInfo.Rows[0]["ExpiryDate"].ToString()).Month.ToString();
                //this.txtYear.Text = Convert.ToDateTime(CreditCardInfo.Rows[0]["ExpiryDate"].ToString()).Year.ToString();

                if (TradeInInfo.Rows.Count != 0)
                {
                    this.txtTradeYear.Text = TradeInInfo.Rows[0]["T1year"].ToString();
                    if (TradeInInfo.Rows[0]["UsedCar"] != null) this.ddlTCarMake.Items.FindByText(TradeInInfo.Rows[0]["UsedCar"].ToString().ToUpper()).Selected = true;
                    this.txtTModel.Text = TradeInInfo.Rows[0]["T1Model"].ToString();
                    this.txtTSeries.Text = TradeInInfo.Rows[0]["T1Series"].ToString();
                    this.txtTBodyShape.Text = TradeInInfo.Rows[0]["T1BodyShap"].ToString();
                    if (TradeInInfo.Rows[0]["T1FuelType"] != null && TradeInInfo.Rows[0]["T1FuelType"].ToString() != "") this.ddlTFuelType.Items.FindByText(TradeInInfo.Rows[0]["T1FuelType"].ToString()).Selected = true;
                    this.txtTOdometer.Text = TradeInInfo.Rows[0]["T1Odometer"].ToString();
                    if (TradeInInfo.Rows[0]["T1Transmission"] != null && TradeInInfo.Rows[0]["T1Transmission"].ToString() != "") this.ddlTTransmission.Items.FindByText(TradeInInfo.Rows[0]["T1Transmission"].ToString()).Selected = true;
                    this.txtTBodyColour.Text = TradeInInfo.Rows[0]["T1BodyColor"].ToString();
                    this.txtTTrimColour.Text = TradeInInfo.Rows[0]["T1TrimColor"].ToString();
                    this.txtTExpiryMonth.Text = TradeInInfo.Rows[0]["T1RegExpMnt"].ToString();
                    this.txtTExpiryYear.Text = TradeInInfo.Rows[0]["T1RegExpYear"].ToString();
                    if (TradeInInfo.Rows[0]["LogBooks"] != null && TradeInInfo.Rows[0]["LogBooks"].ToString() != "") this.ddlTLogBooks.Items.FindByText(TradeInInfo.Rows[0]["LogBooks"].ToString()).Selected = true;
                    this.txtTDescription.Text = TradeInInfo.Rows[0]["TradeInDesc"].ToString();
                    this.txtTOrigValue.Text = TradeInInfo.Rows[0]["T1OrigValue"].ToString();
                    if (TradeInInfo.Rows[0]["Tradestatus"] != null && TradeInInfo.Rows[0]["Tradestatus"].ToString() != "")
                    {
                        this.ddlTTradeStatus.Items.FindByText(TradeInInfo.Rows[0]["Tradestatus"].ToString()).Selected = true;
                    }
                    else
                    {
                        this.ddlTTradeStatus.Items.FindByText("Pending").Selected = true;
                    }
                }

            }

            //Tab1.CssClass = ".Clicked";
            //Tab2.CssClass = ".Initial";
            
            MainView.ActiveViewIndex = 0;
        }
    }

    protected void Tab1_Click(object sender, EventArgs e)
    {
        Tab_1.CssClass = ".Clicked";
        Tab_2.CssClass = ".Initial";
        Tab_3.CssClass = ".Initial";
        MainView.ActiveViewIndex = 0;
    }

    protected void Tab2_Click(object sender, EventArgs e)
    {
        Tab_1.CssClass = ".Initial";
        Tab_2.CssClass = ".Clicked";
        Tab_3.CssClass = ".Initial";
        MainView.ActiveViewIndex = 1;
    }

    protected void Tab3_Click(object sender, EventArgs e)
    {
        Tab_1.CssClass = ".Initial";
        Tab_2.CssClass = ".Initial";
        Tab_3.CssClass = ".Clicked";
        MainView.ActiveViewIndex = 2;
    }

    private void BindLogBookXml()
    {
        string filePath = Server.MapPath("~/LogBooks.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddlTLogBooks.DataSource = ds;
            ddlTLogBooks.DataTextField = "name";
            ddlTLogBooks.DataValueField = "id";
            ddlTLogBooks.DataBind();
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

    private void BindTTransmissionXml()
    {
        string filePath = Server.MapPath("~/Transmission.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddlTTransmission.DataSource = ds;
            ddlTTransmission.DataTextField = "name";
            ddlTTransmission.DataValueField = "id";
            ddlTTransmission.DataBind();
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

    private void BindTFuelTypeXml()
    {
        string filePath = Server.MapPath("~/FuelType.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddlTFuelType.DataSource = ds;
            ddlTFuelType.DataTextField = "name";
            ddlTFuelType.DataValueField = "id";
            ddlTFuelType.DataBind();
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

    private void BindTCarMakes()
    {
        try
        {
            ddlTCarMake.Items.Clear();
            Miles_Cls_CarMake objTCarMake = new Miles_Cls_CarMake();
            DataTable dtTCarMakes = objTCarMake.GetAllCarMakes();
            ddlTCarMake.DataSource = dtTCarMakes;
            ddlTCarMake.DataBind();

            if (ddlTCarMake.Items.Count == 0)
                ddlTCarMake.Items.Insert(0, new ListItem("No Car Makes Found", "-Select-"));
            else
                ddlTCarMake.Items.Insert(0, new ListItem("-Select Car Make-", "-Select-"));
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
                    if (AccessoryCounter == 0)
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

            CheckPriceNull();

        }
        catch (Exception ex)
        {

        }
    }

    private void CheckPriceNull()
    {
        if (this.txtAccessory1.Text == "") this.txtAccessory1.Text = "0.0";
        if (this.txtAccessory2.Text == "") this.txtAccessory2.Text = "0.0";
        if (this.txtAccessory3.Text == "") this.txtAccessory3.Text = "0.0";
        if (this.txtAccessory4.Text == "") this.txtAccessory4.Text = "0.0";

        if (this.txtVehicleRetailPrice.Text == "") this.txtVehicleRetailPrice.Text = "0.0";
        if (this.txtPreDelivery.Text == "") this.txtPreDelivery.Text = "0.0";
        if (this.txtRegistrationPrice.Text == "") this.txtRegistrationPrice.Text = "0.0";
        if (this.txtStampDuty.Text == "") this.txtStampDuty.Text = "0.0";

        if (this.txtCTP.Text == "") this.txtCTP.Text = "0.0";
        if (this.txtGST.Text == "") this.txtGST.Text = "0.0";
        if (this.txtPlateFee.Text == "") this.txtPlateFee.Text = "0.0";
        if (this.txtTotalAccessories.Text == "") this.txtTotalAccessories.Text = "0.0";

        if (this.txtTotalOnRoadCost.Text == "") this.txtTotalOnRoadCost.Text = "0.0";
        if (this.txtFleetDiscount.Text == "") this.txtFleetDiscount.Text = "0.0";

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

    private void BindTradeStatusXml()
    {
        string filePath = Server.MapPath("~/Tradestatus.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddlTTradeStatus.DataSource = ds;
            ddlTTradeStatus.DataTextField = "name";
            ddlTTradeStatus.DataValueField = "id";
            ddlTTradeStatus.DataBind();
        }
    }

    private void BindDepositTakenXml()
    {
        string filePath = Server.MapPath("~/DepositTaken.xml");
        using (DataSet ds = new DataSet())
        {
            ds.ReadXml(filePath);
            ddlDepositTaken.DataSource = ds;
            ddlDepositTaken.DataTextField = "name";
            ddlDepositTaken.DataValueField = "id";
            ddlDepositTaken.DataBind();
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
        string pdfTemplate="";
        string newFile="";
        if(this.TStatus == "1")
        {
            //pdfTemplate = Server.MapPath("~/Contract/ContractTrade.pdf");
            //newFile = Server.MapPath("~/Contract/ContractTrade2.pdf");
            //this.LinkDownLoad.NavigateUrl = "~/Contract/ContractTrade2.pdf";

            pdfTemplate = @"C:\inetpub\wwwroot\Quotesys\Contract\ContractTrade.pdf";
            newFile = @"C:\inetpub\wwwroot\Quotesys\Contract\ContractTrade2.pdf";
            this.LinkDownLoad.NavigateUrl = "~/Contract/ContractTrade2.pdf";
        } else  //else if (this.TStatus == "0")
        {
            //pdfTemplate = Server.MapPath("~/Contract/ContractNoTrade.pdf");
            //newFile = Server.MapPath("~/Contract/ContractNoTrade2.pdf");
            //this.LinkDownLoad.NavigateUrl = "~/Contract/ContractNoTrade2.pdf";

            pdfTemplate = @"C:\inetpub\wwwroot\Quotesys\Contract\ContractNoTrade.pdf";
            newFile = @"C:\inetpub\wwwroot\Quotesys\Contract\ContractNoTrade2.pdf";
            this.LinkDownLoad.NavigateUrl = "~/Contract/ContractNoTrade2.pdf";
        }
        //string pdfTemplate = Server.MapPath("~/Contract/ContractTrade.pdf");
        //string newFile = Server.MapPath("~/Contract/ContractTrade2.pdf");

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
        pdfFormFields.SetField("DealerCompanyNameP4", this.txtDealerCompany.Text);
        pdfFormFields.SetField("DealerAddressP4", this.txtDealerAddress.Text);
        pdfFormFields.SetField("CityStatePCodeP4", this.txtDealerCity.Text + " " + this.txtDealerState.Text + " " + this.txtDealerPCode.Text);  //Card Number
        pdfFormFields.SetField("DealerNameP4", this.txtDealerName.Text);
        pdfFormFields.SetField("DealerPhoneP4", this.txtDealerPhone.Text);
        pdfFormFields.SetField("DealerEmailP4", this.txtDealerEmail.Text);
        pdfFormFields.SetField("ConsultantP4", this.txtConsultant.Text);
        pdfFormFields.SetField("ConsultantP4", this.txtConsultant.Text);
        pdfFormFields.SetField("ConsultantPhoneP4", this.txtConsultantPhone.Text);
        pdfFormFields.SetField("ConsultantEmailP4", this.txtConsultantMail.Text);

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

        pdfFormFields.SetField("DeliveryDateP4", this.txtEstimatedDeliveryDate.Text);
        
        pdfFormFields.SetField("TradeInBasicInfoP4", this.txtTradeYear.Text
            + "  " + this.ddlTCarMake.SelectedItem.Text
            + "  " + this.txtTModel.Text
            + "  " + this.txtTSeries.Text
            + "  " + this.ddlTTransmission.SelectedItem.Text
            + "  " + this.txtBodyShape.Text
            + "  " + this.txtBodyColor.Text
            + "  " + this.ddlFuelType.SelectedItem.Text);
        pdfFormFields.SetField("OdemeterP4", this.txtTOdometer.Text);
        pdfFormFields.SetField("REP4", this.txtTExpiryMonth.Text + "/" + this.txtTExpiryYear.Text);
        pdfFormFields.SetField("LogBooksP4", this.ddlTLogBooks.SelectedItem.Text);
        pdfFormFields.SetField("TDescriptionP4", this.txtTDescription.Text);

        pdfFormFields.SetField("SupplierP5", this.txtDealerCompany.Text);

        // flatten the form to remove editting options, set it to false
        // to leave the form open to subsequent manual edits
        pdfStamper.FormFlattening = false;

        pdfStamper.Close();
        pdfReader.Close();

        this.LinkDownLoad.Visible = true;
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        DateTime ExpDate = new DateTime(Convert.ToInt32("20" + this.txtYear.Text), Convert.ToInt32(this.txtMonth.Text), 1);
        CDC.AddCreditCard(Convert.ToInt32(CustomerInfo.Rows[0]["Id"].ToString()), this.txtCVNumber.Text, this.txtCardNumber.Text, this.ddlCardType.SelectedItem.Text, ExpDate, this.txtMonth.Text, this.txtYear.Text, this.txtMemberNo.Text, this.txtDepositAmount.Text);
    }

    protected void Button6_Click(object sender, EventArgs e)
    {

    }

    protected void Button7_Click(object sender, EventArgs e)
    {
        CDC.AddTradeInInfo(Convert.ToInt64(this.CustomerInfo.Rows[0]["Id"].ToString()), this.txtTradeYear.Text
            , this.ddlTCarMake.SelectedItem.Text, this.txtTModel.Text, this.txtTSeries.Text, this.txtTBodyShape.Text
            , this.ddlTFuelType.SelectedItem.Text, Convert.ToInt64(this.txtTOdometer.Text), this.ddlTransmission.SelectedItem.Text
            , this.txtTBodyColour.Text, this.txtTTrimColour.Text, Convert.ToInt32(this.txtTExpiryMonth.Text)
            , Convert.ToInt32(this.txtTExpiryYear.Text), this.ddlTLogBooks.SelectedItem.Text, this.txtTDescription.Text, this.txtTOrigValue.Text
            , this.ddlTTradeStatus.SelectedItem.Text, this.txtCustomerName.Text, this.txtEmail.Text, this.txtPhone.Text, this.txtMobile.Text, this.ReqID);
    }

    protected void AddDeliveryTrack_Click(object sender, EventArgs e)
    {
        CDC.AddDeliveryTrack(this.txtEmail.Text, this.txtMemberNo.Text, this.txtMemberNo.Text, CustomerInfo.Rows[0]["Fname"].ToString()
            , this.txtSurName.Text, this.txtCustomerName.Text, this.txtEmail.Text, this.ddlState.SelectedItem.Text, this.ddlState.SelectedValue
            , this.ddlCity.SelectedItem.Text, this.txtPostCode.Text, this.txtAddress.Text, this.txtPhone.Text, this.txtMobile.Text
            , this.txtFax.Text, DealerInfo.Rows[0]["Company"].ToString(), DealerInfo.Rows[0]["Name"].ToString(), DealerInfo.Rows[0]["ID"].ToString()
            , DealerInfo.Rows[0]["Key"].ToString(), DealerInfo.Rows[0]["Phone"].ToString(), DealerInfo.Rows[0]["Email"].ToString()
            , this.ddlCarMake.SelectedItem.Text, this.ddlCarMake.SelectedValue, this.txtModel.Text, this.txtConsultant.Text, this.txtEstimatedDeliveryDate.Text
            , this.ddlTTradeStatus.SelectedItem.Text, CustomerInfo.Rows[0]["Id"].ToString(), this.ReqID);
    }

    protected void SearchCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ReqID == null || this.QuoteID == null || this.ConsID == null)
            {
                DataTable tCM = CDC.SearchCustomer(this.txtCustomerName.Text, this.txtEmail.Text);
                this.ReqID = tCM.Rows[0]["RequestId"].ToString();
                DataTable tQH = CDC.SearchQuoteIDbyReqID(this.ReqID);
                this.QuoteID = tQH.Rows[0]["ID"].ToString();
                this.HeaderInfo = CDC.SearchHeaderInfo(this.ReqID);
                this.ConsID = HeaderInfo.Rows[0]["ConsultantID"].ToString();
                this.TStatus = "1";

                ///////////////////////////////////////////////////////////////////////
                Response.Clear();
                string PageToRedirect = "http://localhost:2540/PrivateFleet.New/ClientDealerContractT.aspx?TStatus=0";
                //string PageToRedirect = "http://quotes.privatefleet.com.au/ClientDealerContractT.aspx?TStatus=0";
                DataTable TradeInInfo = CDC.CheckIfTradeIn(ReqID);
                if (TradeInInfo.Rows.Count != 0)
                {
                    if (Convert.ToBoolean(TradeInInfo.Rows[0]["TradeIn"]) == true)
                    {
                        PageToRedirect = "http://localhost:2540/PrivateFleet.New/ClientDealerContractT.aspx?TStatus=1";  //Test Version
                        //PageToRedirect = "http://quotes.privatefleet.com.au/ClientDealerContractT.aspx?TStatus=1";
                    }

                    Response.Redirect(PageToRedirect + "&ReqID=" + ReqID + "&QuoteID=" + QuoteID + "&ConsID=" + HeaderInfo.Rows[0]["ConsultantID"].ToString(), true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Not bind a client", "<script> alert('This Quote has not binded to a client, please go to CRM to bind the client');</script>", false);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("http://localhost:2540/PrivateFleet.New/ClientDealerContractT.aspx");
        }
    }

    protected void UpdatePFMembership_Click(object sender, EventArgs e)
    {
        CDC.UpdatePFMembership(this.txtSurName.Text, this.txtMemberNo.Text, this.txtMemberNo.Text, this.txtDepositAmount.Text, 
           this.ddlDepositTaken.SelectedItem.Text , this.txtMembershipFee.Text, CustomerInfo.Rows[0]["Id"].ToString());
    }
}