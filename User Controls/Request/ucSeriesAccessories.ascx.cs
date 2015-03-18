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
using log4net;

public partial class User_Controls_Request_ucSeriesAccessories : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_Request_ucSeriesAccessories));
    
    #region "Variables and Properties"
    private int _seriesId;
    /// <summary>
    /// Unique Identifier for Series
    /// </summary>
    public int SeriesId
    {
        get { return _seriesId; }
        set { _seriesId = value; }
    }

    private string _accessoryIds;
    public string AccessoryIds
    {
        get
        {
            _accessoryIds = "";
            if (ViewState["dtAccessories"] != null)
            {
                DataTable dtAccessories = (DataTable)ViewState["dtAccessories"];

                //build comma separated string of selected city ids
                if (dtAccessories.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAccessories.Rows)
                        _accessoryIds += dr["ID"].ToString() + ",";
                }

                //remove last comma character
                if (_accessoryIds != "")
                    _accessoryIds = _accessoryIds.Remove(_accessoryIds.Length - 1, 1);
            }
            return _accessoryIds;
        }
        set
        {
            _accessoryIds = value;
        }
    }
    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
        }
        catch (Exception ex)
        {
            logger.Error("Page Load Event : " + ex.Message);
        }
    }
    #endregion

    #region "Methods"
    /// <summary>
    /// Bind accessories for selected series
    /// </summary>
    public void BindSeriesAccessories()
    {
        logger.Debug("BindSeriesAccessories Method Start");
        Cls_SeriesAccessories objMapping = new Cls_SeriesAccessories();
        try
        {
            //get actions for selected page
            objMapping.SeriesID = _seriesId;
            DataTable dt = objMapping.getAllAccessoriesOfSeries();

           
            ViewState["dtAccessories"] = dt;

            //bind actions to grid
            gvAccessories.DataSource = dt;
            gvAccessories.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            logger.Debug("BindSeriesAccessories Method End");
        }
    }
    #endregion
}
