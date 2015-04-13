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
using System.Data.Common;
using log4net;
using System.Text;


/// <summary>
/// Summary description for Cls_Admin
/// </summary>
/// 


public class Cls_Admin : Cls_CommonProperties
{
    ILog logger = LogManager.GetLogger(typeof(Cls_Admin));
    public string id { get; set; }
    public Cls_Admin()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetAllAdminDT()
    {
        DataTable dt = new DataTable();
        DbCommand objCmd;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_GETAllAdminList");
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message.ToString ());
        }
        finally
        {

        }
        return dt;
    }
}
