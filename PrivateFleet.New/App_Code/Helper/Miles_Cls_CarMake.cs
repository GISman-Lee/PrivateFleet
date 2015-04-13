using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Mechsoft.FleetDeal;
using Mechsoft.GeneralUtilities;
using System.Data.Common;

/// <summary>
/// Summary description for Miles_Cls_CarMake
/// </summary>
public class Miles_Cls_CarMake : Cls_CommonProperties
{
    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();

    #region Properties
    private string _strCarMake;

    public string CarMake
    {
        get { return _strCarMake; }
        set { _strCarMake = value; }
    }

    #endregion

	public Miles_Cls_CarMake()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetAllCarMakes()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllActiveCarMakes");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}