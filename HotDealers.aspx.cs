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
using log4net;
using Mechsoft.FleetDeal;
using Mechsoft.GeneralUtilities;
using System.Xml;

public partial class HotDealers : System.Web.UI.Page
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(HotDealers));

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Set page header text
            if (!IsPostBack)
            {
                Label lblHeader = (Label)Master.FindControl("lblHeader");
                if (lblHeader != null)
                    lblHeader.Text = "Hot Dealer Selection";

                FillMake();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Event : " + ex.Message);
        }
    }
    #endregion

    #region "Dealer Selection Methods"

    /// <summary>
    /// Method to fill states dropdown
    /// </summary>
    private void FillStates()
    {
        logger.Debug("FillStates Method Start");
        Cls_State objState = new Cls_State();
        try
        {
            //get all pages
            DataTable dt = objState.GetAllStates();

            //clear pages dropdown
            ddlState.Items.Clear();

            //fill pages dropdown
            ddlState.DataSource = dt;
            ddlState.DataTextField = "state";
            ddlState.DataValueField = "id";
            ddlState.DataBind();

            //insert default item in pages dropdown
            ddlState.Items.Insert(0, new ListItem("-Select State-", "0"));
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            logger.Debug("FillStates Method End");
        }
    }

    /// <summary>
    /// Method to fill cities listbox
    /// </summary>
    private void FillCities()
    {
        logger.Debug("FillCities Method Start");
        Cls_Dealer objDealer = new Cls_Dealer();
        try
        {
            //get all pages
            objDealer.State = ddlState.SelectedValue;
            DataTable dt = objDealer.GetAllCitiesOfState();

            //clear actions dropdown
            lstCity.Items.Clear();

            //fill actions dropdown
            lstCity.DataSource = dt;
            lstCity.DataTextField = "city";
            lstCity.DataValueField = "id";
            lstCity.DataBind();

            //insert default item in actions dropdown
            lstCity.Items.Insert(0, new ListItem("-Select City-", "0"));
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            logger.Debug("FillCities Method End");
        }
    }

    /// <summary>
    /// Method to fill locations listbox
    /// </summary>
    private void FillLocations()
    {
        logger.Debug("FillLocations Method Start");
        Cls_Dealer objDealer = new Cls_Dealer();
        try
        {
            //get selected city ids from listbox
            int[] selectedCityIds = lstCity.GetSelectedIndices();
            string strCityIds = "";

            //build comma separated string of selected city ids
            if (selectedCityIds.Length > 0)
            {
                for (int i = 0; i < selectedCityIds.Length; i++)
                    strCityIds += lstCity.Items[selectedCityIds[i]].Value + ",";
            }

            //remove last comma character
            if (strCityIds != "")
                strCityIds = strCityIds.Remove(strCityIds.Length - 1, 1);

            //get locations
            objDealer.CityIds = strCityIds;
            DataTable dt = objDealer.GetLocationsInCities();

            //fill locations listbox
            lstLocation.DataSource = dt;
            lstLocation.DataTextField = "location";
            lstLocation.DataValueField = "id";
            lstLocation.DataBind();

            //insert default item in locations listbox
            lstLocation.Items.Insert(0, new ListItem("-Select Location-", "0"));
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            logger.Debug("FillLocations Method End");
        }
    }

    private void FillMake()
    {
        logger.Debug("FillMake Method Start");
        //Cls_MakeHelper objMake = new Cls_MakeHelper();
        try
        {
            //get all active make
            //DataTable dt = objMake.GetActiveMakes();

            //clear make dropdown
            ddlMake.Items.Clear();


            if (Cache["MAKES"] == null)
                Mechsoft.GeneralUtilities.PreLoad.LoadMake();

            DataTable dt = Cache["MAKES"] as DataTable;

            if (dt != null)
            {
                //fill make dropdown
                ddlMake.DataSource = dt;
                ddlMake.DataTextField = "Make";
                ddlMake.DataValueField = "id";
                ddlMake.DataBind();
            }

            //insert default item in make dropdown
            ddlMake.Items.Insert(0, new ListItem("-Select Make-", "0"));
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            logger.Debug("FillMake Method End");
        }
    }

    #endregion

    #region "Dealer Selection Events"
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //fill cities listbox
            FillCities();
            FillLocations();
        }
        catch (Exception ex)
        {
            logger.Error("ddlState_SelectedIndexChanged Event : " + ex.Message);
        }
    }

    protected void lnkGetLocations_Click(object sender, EventArgs e)
    {
        try
        {
            //fill locations listbox
            FillLocations();
        }
        catch (Exception ex)
        {
            logger.Error("lnkGetLocations_Click Event : " + ex.Message);
        }
    }

    protected void btnSearchDealers_Click(object sender, ImageClickEventArgs e)
    {
        #region old Commented Code
        //Cls_Dealer objDealer = new Cls_Dealer();
        //DataTable dt = null;
        //try
        //{
        //    if (lstLocation.SelectedValue == "0" || lstLocation.SelectedValue == "" || lstLocation.SelectedValue == null)
        //    {
        //        #region search dealers in selected cities

        //        //get selected city ids from listbox
        //        int[] selectedCityIds = lstCity.GetSelectedIndices();
        //        string strCityIds = "";

        //        //build comma separated string of selected city ids
        //        if (selectedCityIds.Length > 0)
        //        {
        //            for (int i = 0; i < selectedCityIds.Length; i++)
        //                strCityIds += lstCity.Items[selectedCityIds[i]].Value + ",";
        //        }

        //        //remove last comma character
        //        if (strCityIds != "")
        //            strCityIds = strCityIds.Remove(strCityIds.Length - 1, 1);

        //        //search dealers
        //        objDealer.CityIds = strCityIds;
        //        dt = objDealer.SearchDealersInCities();

        //        #endregion
        //    }
        //    else
        //    {
        //        #region search dealers in selected locations

        //        //get selected location ids from listbox
        //        int[] selLocationIds = lstLocation.GetSelectedIndices();
        //        string strLocationIds = "";

        //        //build comma separated string of selected location ids
        //        if (selLocationIds.Length > 0)
        //        {
        //            for (int i = 0; i < selLocationIds.Length; i++)
        //                strLocationIds += lstLocation.Items[selLocationIds[i]].Value + ",";
        //        }

        //        //remove last comma character
        //        if (strLocationIds != "")
        //            strLocationIds = strLocationIds.Remove(strLocationIds.Length - 1, 1);

        //        //search dealers
        //        objDealer.LocationIds = strLocationIds;
        //        dt = objDealer.SearchDealersInLocations();
        //        #endregion
        //    }


        //    UcHotDealerSelection1.Visible = true;
        //    UcHotDealerSelection1.dtDealers = dt;
        //    UcHotDealerSelection1.BindDealers();
        //}
        //catch (Exception ex)
        //{
        //    logger.Error("btnSearchDealers_Click Event : " + ex.Message);
        //}

        #endregion

        Cls_Dealer objDealer = new Cls_Dealer();
        DataTable dt = null;
        try
        {

            objDealer.PostalCode = txtPCode.Text;
            objDealer.MakeID = Convert.ToInt32(ddlMake.SelectedValue);
            //objDealer.Radius = 20000;
            objDealer.Company = txtCompany.Text.Trim();
            objDealer.Contact = txtContact.Text.Trim();
            dt = objDealer.SearchDealersForMakeCompany();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Company like '%" + txtCompany.Text.Trim() + "%' AND Name like '%" + txtContact.Text.Trim() + "%'";
            dt = dv.ToTable();


            UcHotDealerSelection1.Visible = true;
            UcHotDealerSelection1.dtDealers = dt;
            UcHotDealerSelection1.BindDealers();
        }
        catch (Exception ex)
        {
            logger.Error("Hot dealer err- " + ex.Message);
        }
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