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
using Mechsoft.FleetDeal;
using System.Data.Common;
using System.Data;
using log4net;  


/// <summary>
/// Summary description for Cls_SeriesAccessories
/// </summary>
public class Cls_SeriesAccessories:Cls_CommonProperties
{
	public Cls_SeriesAccessories()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();

    private string _strSpecification;

    public string Specification
    {
        get { return _strSpecification; }
        set { _strSpecification = value; }
    }

    private int _intSeriesID;

    public int SeriesID
    {
        get { return _intSeriesID; }
        set { _intSeriesID = value; }
    }


    private int _intAccessoryID;

    public int AccessoryID
    {
        get { return _intAccessoryID; }
        set { _intAccessoryID = value; }
    }
	

    public DataTable getAllAccessoriesOfSeries()
    {

        DbCommand objCmd = null;

        objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "SpGetAllSeriesAccessories");
        DataAccess.AddInParameter(objCmd, "SeriesID", DbType.Int16, SeriesID);
        return DataAccess.GetDataTable(objCmd);
    }
    public int AddSeriesAccessory()
    {
        this.SpName = "SpAddSeriesAccessory";
        return HandleSeriesAccesotyMaster();
    }

    public int UpdateSeriesAccessory()
    {
        this.SpName = "SpUpdateSeriesAccessory";
        return  HandleSeriesAccesotyMaster();
    }

    public int setActivenessOfSeriesAccessory()
    {
        this.SpName = "SpActivateInactivateSeriesAccessory";
        return HandleSeriesAccesotyMaster();
    }

    public int HandleSeriesAccesotyMaster()
    {
        DbCommand objCmd = null;

        objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
        setParameters(objCmd);

        return DataAccess.ExecuteNonQuery(objCmd);
    }

    private void setParameters(DbCommand objCmd)
    {
        if (DbOperations.CHANGE_ACTIVENESS.Equals(DBOperation))
        {
            DataAccess.AddInParameter(objCmd, "IsActive", DbType.Boolean, IsActive);
            DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
        }
        else
        {
            DataAccess.AddInParameter(objCmd, "SeriesID", DbType.Int16, SeriesID);
            DataAccess.AddInParameter(objCmd, "AccessoryID", DbType.Int16, AccessoryID);
            DataAccess.AddInParameter(objCmd, "Specification", DbType.String, Specification);

            if (DbOperations.UPDATE.Equals(DBOperation))
            {
                DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
            }
        }
    }


    public DataTable getActiveAllocatableAcessories()
    {
        DbCommand objCmd = null;

        objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetActiveAllocatableAccessories");
        Cls_DataAccess.getInstance().AddInParameter(objCmd, "SeriesID", DbType.Int16, SeriesID);
        return Cls_DataAccess.getInstance().GetDataTable(objCmd);
    }
}
