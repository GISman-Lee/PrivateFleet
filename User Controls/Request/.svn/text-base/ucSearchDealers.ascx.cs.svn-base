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

public partial class User_Controls_Request_ucSearchDealers : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_Request_ucSearchDealers));

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //fill states dropdown
                FillStates();

                //fill cities listbox
                FillCities();

                //clear items in locations listbox
                lstLocation.Items.Clear();

                //fill locations listbox
                FillLocations();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page Load Event : " + ex.Message);
        }
    }
    #endregion

    #region "Methods"

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
            int []selectedCityIds = lstCity.GetSelectedIndices();
            string strCityIds = "";

            //build comma separated string of selected city ids
            if(selectedCityIds.Length > 0)
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
    #endregion

    #region "Events"
    
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //fill cities listbox
            FillCities();
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
    #endregion


    protected void btnSearchDealers_Click(object sender, EventArgs e)
    {
        Cls_Dealer objDealer = new Cls_Dealer();
        DataTable dt = null;
        try
        {
            if (lstLocation.SelectedValue == "0" || lstLocation.SelectedValue == "" || lstLocation.SelectedValue == null)
            {
                #region search dealers in selected cities

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

                //search dealers
                objDealer.CityIds = strCityIds;
                dt = objDealer.SearchDealersInLocations();

                #endregion
            }
            else
            {
                #region search dealers in selected locations
                #endregion
            }
            UcDealerSelection1.dtDealers = dt;
            UcDealerSelection1.BindDealers(dt);   
        }
        catch (Exception ex)
        {
            logger.Error("btnSearchDealers_Click Event : " + ex.Message);
        }
    }
}
