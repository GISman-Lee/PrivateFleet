using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using System.Data;

/// <summary>
/// Summary description for Cls_AddQuoteProspectMapping
/// </summary>
public class Cls_AddQuoteProspectMapping
{
    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();

	public Cls_AddQuoteProspectMapping()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void AddQuoteProspectMapping(string ProspectID, string ConsID, int intRequestId)
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "SpAddQuoteProspectMapping");
            DataAccess.AddInParameter(objCmd, "ProspectId", DbType.Int64, Convert.ToInt64(ProspectID));
            DataAccess.AddInParameter(objCmd, "QuoteconConsultantId", DbType.Int64, Convert.ToInt64(ConsID));
            DataAccess.AddInParameter(objCmd, "QuoteRequestId", DbType.Int64, Convert.ToInt64(intRequestId));

            DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex) { }
    }
}