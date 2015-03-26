using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mechsoft.FleetDeal;
using System.Data;
using System.Data.Common;
using Mechsoft.GeneralUtilities;

/// <summary>
/// Summary description for Miles_Cls_City
/// </summary>
public class Miles_Cls_City : Cls_CommonProperties
{
	public Miles_Cls_City()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetAllCities()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spGetAllCities");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}