using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using log4net;
using System.Text;

/// <summary>
/// Summary description for Cls_Alies
/// </summary>
public class Cls_Alies
{
	public Cls_Alies()
	{
	}
    private int _intMakeID;
    public int MakeID
    {
        get { return _intMakeID; }
        set { _intMakeID = value; }
    }

    private int _strMake;
    public int Make
    {
        get { return _strMake; }
        set { _strMake = value; }
    }
    private string _strAlies;

    public string Alies
    {
        get { return _strAlies; }
        set { _strAlies = value; }
    }


    public int AddAlies()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAddAlies");
            Cls_DataAccess.getInstance().AddInParameter(objCmd,"@Make", DbType.String, Make);
            Cls_DataAccess.getInstance().AddInParameter(objCmd,"@Alies", DbType.String, Alies);
            return Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        { return 0; }
    }

    public DataTable GetAlies()
    {
        try
        {
            DbCommand objcmd = null;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAlies");
            return Cls_DataAccess.getInstance().GetDataTable(objcmd);
        }
        catch (Exception ex)
        { return null; }
    }

    public DataTable GetMake()
    {
        try
        {

            DbCommand objcmd = null;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.Text,"SELECT ID,Make FROM tblMakeMaster ORDER BY Make;");
            return Cls_DataAccess.getInstance().GetDataTable(objcmd);
        }
        catch (Exception)
        { return null; }
    }
}
