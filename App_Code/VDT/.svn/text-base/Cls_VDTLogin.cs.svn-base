using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mechsoft.GeneralUtilities;
using System.Data.Common;
using log4net;


/// <summary>
/// Summary description for Cls_VDTLogin
/// </summary>
public class Cls_VDTLogin
{
    ILog logger = LogManager.GetLogger(typeof(Cls_VDTLogin));
    public string username { get; set; }
    public string password { get; set; }
    public int id { get; set; }
	public Cls_VDTLogin()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable ValidateLogIn()
    {
        DataTable dt = new DataTable();
        DbCommand objcmd = null;

        try
        {
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_VDT_Login");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "username", DbType.String, username);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "Password", DbType.String, password);
            dt = Cls_DataAccess.getInstance().GetDataTable(objcmd ,null );

        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
        return dt;
    }


}
