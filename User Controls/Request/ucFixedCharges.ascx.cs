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

public partial class User_Controls_Request_ucFixedCharges : System.Web.UI.UserControl
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(User_Controls_Request_ucFixedCharges));

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
    /// Bind fixed charge types
    /// </summary>
    public void BindFixedCharges()
    {
        logger.Debug("BindFixedCharges Method Start");
        Cls_ChargeType objChargeType = new Cls_ChargeType();
        try
        {
            //get fixed charge types
            DataTable dt = objChargeType.GetAllChargeTypes();
            DataView dv = dt.DefaultView;

            dv.RowFilter = "IsActive=true";
            dt = dv.ToTable();

            //bind actions to grid
            gvCharges.DataSource = dt;
            gvCharges.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            logger.Debug("BindFixedCharges Method End");
        }
    }

    #endregion
}
