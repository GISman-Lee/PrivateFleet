using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Mechsoft.GeneralUtilities;

/// <summary>
/// Summary description for Miles_Cls_Dealer
/// </summary>
public class Miles_Cls_Dealer
{
	public Miles_Cls_Dealer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable GetAllDealers()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spGetAllDealersForClients");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}