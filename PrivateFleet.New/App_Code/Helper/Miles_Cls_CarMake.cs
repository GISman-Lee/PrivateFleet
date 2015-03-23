using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Mechsoft.GeneralUtilities;
using System.Data.Common;

/// <summary>
/// Summary description for Miles_Cls_CarMake
/// </summary>
public class Miles_Cls_CarMake
{
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