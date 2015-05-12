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
    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();

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

    public void DuplicateDealerMaster(string Name, string Company, string Email, string Fax, string Phone, string Mobile, string PCode, string State, string City, string Address, string StateId)
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "SpDuplicateDealerMaster");
            DataAccess.AddInParameter(objCmd, "Name", DbType.String, Name);
            DataAccess.AddInParameter(objCmd, "Company", DbType.String, Company);
            DataAccess.AddInParameter(objCmd, "Email", DbType.String, Email);
            DataAccess.AddInParameter(objCmd, "Fax", DbType.String, Fax);
            DataAccess.AddInParameter(objCmd, "Phone", DbType.String, Phone);
            DataAccess.AddInParameter(objCmd, "Mobile", DbType.String, Mobile);
            DataAccess.AddInParameter(objCmd, "PCode", DbType.String, PCode);
            DataAccess.AddInParameter(objCmd, "State", DbType.String, State);
            DataAccess.AddInParameter(objCmd, "City", DbType.String, City);
            DataAccess.AddInParameter(objCmd, "Address", DbType.String, Address);
            DataAccess.AddInParameter(objCmd, "StateId", DbType.String, StateId);

            DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex) { }
    }

    public void DuplicateMakeDealer(string MakeID, string DealerID)
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "SpDuplicateMakeDealer");
            DataAccess.AddInParameter(objCmd, "MakeID", DbType.Int32, Convert.ToInt32(MakeID));
            DataAccess.AddInParameter(objCmd, "DealerID", DbType.Int32, Convert.ToInt32(DealerID));

            DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex) { }
    }
}