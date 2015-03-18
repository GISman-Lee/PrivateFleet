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
using log4net;

/// <summary>
/// Summary description for Cls_Location
/// </summary>
public class Cls_Location : Cls_CommonProperties
{
    public Cls_Location()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
    ILog logger = LogManager.GetLogger(typeof(Cls_Location));


    #region Variables and Propeties
    private int _intLocationID;

    public int LocationID
    {
        get { return _intLocationID; }
        set { _intLocationID = value; }
    }
    private int _intCityID;

    public int CityID
    {
        get { return _intCityID; }
        set { _intCityID = value; }
    }



    #endregion


    #region Functions

    /// <summary>
    /// returns all Locations avaialable
    /// </summary>
    /// <returns>Data Table : Containing all available Locations</returns>
    public DataTable GetAllLocations()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllLocation");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetAllLocations Function :" + ex.Message); return null; }
    }

    /// <summary>
    /// to Check whether this location already exists
    /// </summary>
    /// <returns> Data Table</returns>
    public DataTable CheckIfLocationExists()
    {
        try
        {

            DbCommand objCmd = null;


            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfLocationExists");

            setParameters(objCmd);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex) { logger.Error("CheckIfLocationExists Function :" + ex.Message); return null; }
    }


    /// <summary>
    /// to add New Location
    /// </summary>
    /// <returns> int : indicating no. of records affected in DB</returns>
    public int AddLocation()
    {
        try
        {
            this.SpName = "SpAddLocation";
            return HandleLocationMaster();
        }
        catch (Exception ex) { logger.Error("AddLocation Function :" + ex.Message); return 0; }
    }


    /// <summary>
    /// to update the existing location
    /// </summary>
    /// <returns> int : indicating no. of records affected in DataBase</returns>
    public int UpdateLocation()
    {
        try
        {
            this.SpName = "SpUpdateLocation";
            return HandleLocationMaster();
        }
        catch (Exception ex)
        { logger.Error("UpdateLocation Function :" + ex.Message); return 0; }
    }


    /// <summary>
    /// Activates or Inactivate the location
    /// </summary>
    /// <returns>int : indicating no. of records affected in DataBase</returns>
    public int SetActivenessOfLocation()
    {
        try
        {
            this.SpName = "SpActivateInactivateLocation";
            return HandleLocationMaster();
        }
        catch (Exception ex) { logger.Error("SetActivenessOfLocation Function :" + ex.Message); return 0; }
    }





    /// <summary>
    /// Handles the respective actions on DB
    /// </summary>
    /// <returns>int : returns the integer to subscriber indicating the no of. rows affected in Data Base</returns>
    public int HandleLocationMaster()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            setParameters(objCmd);

            return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        { logger.Error("HandleLocationMaster Function :" + ex.Message); return 0; }
    }
    private void setParameters(DbCommand objCmd)
    {
        try
        {
            if (DbOperations.CHANGE_ACTIVENESS.Equals(DBOperation))
            {
                DataAccess.AddInParameter(objCmd, "IsActive", DbType.Boolean, IsActive);
                DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
            }
            else
            {


                DataAccess.AddInParameter(objCmd, "CityID", DbType.String, CityID);
                DataAccess.AddInParameter(objCmd, "LocationID", DbType.Int32, LocationID);

                if (DbOperations.UPDATE.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);

            }

        }
        catch (Exception ex)
        { logger.Error( "setParameters Function :"+ ex.Message); }

    }



    public DataTable GetAllSuburbs()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllSuburb");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetAllLocations Function :" + ex.Message); return null; }
    }

  
    #endregion

}
