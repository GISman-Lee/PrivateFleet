using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ClientDealerContract : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            this.BindFuelTypeXml();
            this.BindTransmissionXml();
            this.BindStateXml();
            this.BindCities();
            this.BindCarMakes();
            this.BindSuppilers();
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
}