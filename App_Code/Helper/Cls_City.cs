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
using Mechsoft.FleetDeal;
using log4net;

/// <summary>
/// Summary description for Cls_City
/// </summary>
public class Cls_City : Cls_CommonProperties
{
    public Cls_City()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
    ILog logger = LogManager.GetLogger(typeof(Cls_City));

    #region Properties
    private string _strCity;

    public string City
    {
        get { return _strCity; }
        set { _strCity = value; }
    }
    private int _intStateID;

    public int StateID
    {
        get { return _intStateID; }
        set { _intStateID = value; }
    }


    #endregion


    #region Functions
    /// <summary>
    /// returns all cities avaialable
    /// </summary>
    /// <returns>Data Table : Containing all available cities</returns>
    public DataTable GetAllCities()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllCity");


            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetAllCities Function :" + ex.Message);
            return null;
        }
    }


    /// <summary>
    /// to Check whether this City already exists
    /// </summary>
    /// <returns> Data Table</returns>
    /// 
    public DataTable CheckIfCityExists()
    {

        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfCityExists");

            setParameters(objCmd);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("CheckIfCityExists Function :" + ex.Message);
            return null;
        }
    }


    /// <summary>
    /// to add New City
    /// </summary>
    /// <returns> int : indicating no. of records affected in DB</returns>

    public int AddCity()
    {
        try
        {
            this.SpName = "SpAddCity";
            return HandleCityMaster();
        }
        catch (Exception ex)
        {
            logger.Error("AddCity Function :" + ex.Message);
            return 0;
        }
    }



    /// <summary>
    /// to update the existing City 
    /// </summary>
    /// <returns> int : indicating no. of records affected in DataBase</returns>

    public int UpdateCity()
    {
        try
        {
            this.SpName = "SpUpdateCity";
            return HandleCityMaster();
        }
        catch (Exception ex) { logger.Error("UpdateCity Function :" + ex.Message); return 0; }
    }


    /// <summary>
    /// Activates or Inactivate the City
    /// </summary>
    /// <returns>int : indicating no. of records affected in DataBase</returns>
    public int SetActivenessOfCity()
    {
        try
        {
            this.SpName = "SpActivateInactivateCity";
            return HandleCityMaster();
        }
        catch (Exception ex)
        {
            logger.Error("SetActivenessOfCity Function :" + ex.Message);
            return 0;
        }

    }

    /// <summary>
    /// Handles the respective actions on DB
    /// </summary>
    /// <returns>int : returns the integer to subscriber indicating the no of. rows affected in Data Base</returns>
    /// 
    public int HandleCityMaster()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            setParameters(objCmd);

            return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("HandleCityMaster Function :" + ex.Message);
            return 0;
        }
    }

    /// <summary>
    /// Sets the parameter for respective stored procedure
    /// </summary>
    /// <param name="objCmd">
    /// DBCommand object to which you want to add the parameter
    /// </param>
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


                DataAccess.AddInParameter(objCmd, "StateID", DbType.String, StateID);
                DataAccess.AddInParameter(objCmd, "City", DbType.String, City);

                if (DbOperations.UPDATE.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);

            }
        }
        catch (Exception ex)
        { logger.Error("setParameters Function :" + ex.Message); }


    }

    #endregion
}
