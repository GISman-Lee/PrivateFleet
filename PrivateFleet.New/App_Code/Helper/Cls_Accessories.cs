using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mechsoft.FleetDeal;
using Mechsoft.GeneralUtilities;
using System.Data.Common;

using log4net;
using System.Text;


public class Cls_Accessories : Cls_CommonProperties
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(Cls_Accessories));

    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();


    #region Properties
    private bool _returnId;
    public bool ReturnAccessoryId
    {
        get { return _returnId; }
        set { _returnId = value; }
    }

    private string _strType;

    public string Type
    {
        get { return _strType; }
        set { _strType = value; }
    }
    private float _floatPrice;

    public float Price
    {
        get { return _floatPrice; }
        set { _floatPrice = value; }
    }

    private Boolean _IsParameter;

    public Boolean IsParameter
    {
        get { return _IsParameter; }
        set { _IsParameter = value; }
    }

    private String _Name;

    public String Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    private bool _Master;

    public bool IsMaster
    {
        get { return _Master; }
        set { _Master = value; }
    }
    #endregion

    #region Constructor
    public Cls_Accessories()
    {
    }
    #endregion

    #region GetAllAccessories
    /// <summary>
    /// returns all accessories avaialable
    /// </summary>
    /// <returns>Data Table : Containing all available Accessories</returns>
    public DataTable GetAllAccessories()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllAccessories");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetAllAccessories Function :" + ex.Message); return null; }
    }
    #endregion

    #region CheckIfAccessory is exist
    /// <summary>
    /// to Check whether this accessory already exists
    /// </summary>
    /// <returns> Data Table</returns>
    public DataTable CheckIfAccessoryExists()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfAccessoryExists");

            setParameters(objCmd);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex) { logger.Error("CheckIfAccessoryExists Function :" + ex.Message); return null; }
    }
    #endregion

    #region Add Accessory
    /// <summary>
    /// to add New Accessory
    /// </summary>
    /// <returns> int : indicating no. of records affected in DB</returns>
    public int AddAccessories()
    {
        try
        {
            this.SpName = "SpAddAccessories";
            return HandleAccessoriesMaster();
        }
        catch (Exception ex) { logger.Error("AddAccessories Function :" + ex.Message); return 0; }
    }
    #endregion

    #region Update accessory
    /// <summary>
    /// to update the existing acessory
    /// </summary>
    /// <returns> int : indicating no. of records affected in DataBase</returns>
    public int UpdateAccessories()
    {
        try
        {
            this.SpName = "SpUpdateAccessories";
            return HandleAccessoriesMaster();
        }
        catch (Exception ex) { logger.Error("UpdateAccessories Function :" + ex.Message); return 0; }
    }
    #endregion

    #region Set Activeness Of Accessory
    /// <summary>
    /// Activates or Inactivate the accessory
    /// </summary>
    /// <returns>int : indicating no. of records affected in DataBase</returns>
    public int SetActivenessOfAccessories()
    {
        try
        {
            this.SpName = "SpActivateInactivateAccessories";
            return HandleAccessoriesMaster();
        }
        catch (Exception ex)
        { logger.Error("SetActivenessOfAccessories Function :" + ex.Message); return 0; }
    }
    #endregion

    #region Set Master Flag Of Accessory
    /// <summary>
    /// 
    /// </summary>
    /// <returns>int : indicating no. of records affected in DataBase</returns>
    public int SetMasterFlag()
    {
        try
        {
            this.SpName = "SpChangeMasterFlag";
            return HandleAccessoriesMaster();
        }
        catch (Exception ex)
        { logger.Error("SetMasterFlag Function :" + ex.Message); return 0; }
    }
    #endregion

    #region Handel Accessory master
    /// <summary>
    /// Handles the respective actions on DB
    /// </summary>
    /// <returns>int : returns the integer to subscriber indicating the no of. rows affected in Data Base</returns>
    public int HandleAccessoriesMaster()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            setParameters(objCmd);

            if (ReturnAccessoryId)
                return Convert.ToInt32(DataAccess.ExecuteScaler(objCmd, null));
            else
                return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        { logger.Error("HandleAccessoriesMaster Function :" + ex.Message); return 0; }
    }
    #endregion

    #region Set the parameters
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
            else if (DbOperations.CHANGE_MASTER.Equals(DBOperation))
            {
                DataAccess.AddInParameter(objCmd, "IsMaster", DbType.Boolean, IsMaster);
                DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
            }
            else
            {
                DataAccess.AddInParameter(objCmd, "Name", DbType.String, this.Name);
                DataAccess.AddInParameter(objCmd, "IsParameter", DbType.Boolean, IsParameter);

                if (DbOperations.INSERT.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "IsMaster", DbType.Boolean, IsMaster);

                if (DbOperations.UPDATE.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);

            }

        }
        catch (Exception ex) { logger.Error("setParameters Function :" + ex.Message); }

    }
    #endregion

    #region Get Additional Accessories for series
    /// <summary>
    /// Method to retrieve additional accessories for series
    /// </summary>
    /// <param name="SeriesId"></param>
    /// <returns></returns>
    public DataTable GetAdditionalAccessoriesForSeries()
    {
        logger.Debug("GetAdditionalAccessories Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAdditionalAccessories");

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch
        {
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetAdditionalAccessories Method End");
        }
        return dt;
    }
    #endregion

    #region Get Active parameters
    /// <summary>
    /// Method to retrieve active parameters
    /// </summary>
    /// <returns></returns>
    public DataTable GetActiveParameters()
    {
        logger.Debug("GetActiveParameters Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetActiveParameters");

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetActiveParameters Method : " + ex.Message);
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetActiveParameters Method End");
        }
        return dt;
    }
    #endregion

    #region Add new accessory
    public void AddNewAccessory(string p)
    {
        logger.Debug("Custom Accessory Method Start");
        StringBuilder query = new StringBuilder();
        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            query.Append("INSERT INTO  tblAccessoriesMaster(Name,IsParameter,IsActive,IsCustom)Values('" + p + "',0,1,1)");
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.Text, query.ToString());

            //Execute command
            Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("Custom Accessory Method : " + ex.Message);
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("Custom Accessory Method End");
        }
    }
    #endregion

    #region Get Id
    public int GetId(string p)
    {
        int ID;
        logger.Debug("Custom Accessory Method Start");
        StringBuilder query = new StringBuilder();
        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            query.Append("SELECT Max(ID) FROM tblAccessoriesMaster WHERE Name='" + p + "'");
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.Text, query.ToString());

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
            ID = Convert.ToInt32(dt.Rows[0][0].ToString());
            return ID;
        }
        catch (Exception ex)
        {
            logger.Error("Custom Accessory Method : " + ex.Message);
            return 0;
        }
        finally
        {
            objCmd = null;
            logger.Debug("Custom Accessory Method End");
        }
    }
    #endregion

    #region Search Accessory
    public DataTable SearchAccessory()
    {
        try
        {
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchAccessory");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Acc", DbType.String, Name);
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch (Exception ex)
        { return null; }
    }
    #endregion

    public int AddAccessoriesGetId()
    {
        try
        {
            this.SpName = "SpAddAccessories_ReturnID";
            return HandleAccessoriesMaster();
        }
        catch (Exception ex)
        {
            logger.Error("AddAccessories Function :" + ex.Message);
            return 0;
        }
    }
}
