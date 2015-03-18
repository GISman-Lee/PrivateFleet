using System;
using System.Data;
using System.Configuration;
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

/// <summary>
/// Summary description for Cls_ModelHelper
/// </summary>
public class Cls_ModelHelper : Cls_CommonProperties
{
    //declare and initialize logger object
    #region Variable declaration
    static ILog logger = LogManager.GetLogger(typeof(Cls_ModelHelper));

    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
    #endregion

    #region Constructor
    public Cls_ModelHelper()
    {
    }
    #endregion

    #region Properties
    private int _intMakeID;

    public int MakeID
    {
        get { return _intMakeID; }
        set { _intMakeID = value; }
    }
    private string _strModel;

    public string Model
    {
        get { return _strModel; }
        set { _strModel = value; }
    }

    private int _modelId;
    public int ModelId
    {
        get { return _modelId; }
        set { _modelId = value; }
    }

    private int _ModelAccessoryId;
    public int ModelAccessoryId
    {
        get { return _ModelAccessoryId; }
        set { _ModelAccessoryId = value; }
    }

    private int _AccessoryId;
    public int AccessoryId
    {
        get { return _AccessoryId; }
        set { _AccessoryId = value; }
    }

    private string _specs;
    public string Specification
    {
        get { return _specs; }
        set { _specs = value; }
    }

    private bool _returnId;
    public bool ReturnId
    {
        get { return _returnId; }
        set { _returnId = value; }
    }

    #endregion


    #region Get all models
    public DataTable GetAllModels()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetModels");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetAllModels Function :" + ex.Message); return null; }
    }
    #endregion

    #region Check if exist
    public DataTable CheckIfModelExists()
    {
        try
        {

            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfModelExists");
            setParameters(objCmd);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("CheckIfModelExists Function :" + ex.Message); return null; }
    }
    #endregion

    #region Add models
    public int AddModel()
    {
        try
        {
            if (ReturnId)
                this.SpName = "SpAddModel_ReturnId";
            else
                this.SpName = "SpAddModel";

            return HandleModelMaster();
        }
        catch (Exception ex)
        { logger.Error("AddModel Function :" + ex.Message); return 0; }
    }
    #endregion

    #region Update model
    public int UpdateModel()
    {
        try
        {
            this.SpName = "SpUpdateModel";
            return HandleModelMaster();
        }
        catch (Exception ex)
        { logger.Error("UpdateModel Function :" + ex.Message); return 0; }
    }
    #endregion

    #region Set activeness of models
    public int SetActivenessOfModel()
    {
        try
        {
            this.SpName = "SpActivateInactivateModel";
            return HandleModelMaster();
        }
        catch (Exception ex)
        { logger.Error("SetActivenessOfModel Function :" + ex.Message); return 0; }
    }
    #endregion

    #region model master
    public int HandleModelMaster()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            setParameters(objCmd);

            if (ReturnId)
                return Convert.ToInt32(DataAccess.ExecuteScaler(objCmd, null));
            else
                return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        { logger.Error("HandleModelMaster Function :" + ex.Message); return 0; }
    }
    #endregion

    #region Set PArameters
    private void setParameters(DbCommand objCmd)
    {
        try
        {
            if (DbOperations.CHANGE_ACTIVENESS.Equals(DBOperation))
            {
                DataAccess.AddInParameter(objCmd, "IsActive", DbType.Boolean, IsActive);
                DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
            }
            else if (DbOperations.CHANGE_ACTIVENESS_ACCESSORY.Equals(DBOperation))
            {
                DataAccess.AddInParameter(objCmd, "IsActive", DbType.Boolean, IsActive);
                DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ModelAccessoryId);
            }
            else if (DbOperations.INSERT_ACCESSORY.Equals(DBOperation))
            {
                DataAccess.AddInParameter(objCmd, "AccessoryId", DbType.Int32, AccessoryId);
                DataAccess.AddInParameter(objCmd, "Specification", DbType.String, Specification);
                DataAccess.AddInParameter(objCmd, "ModelId", DbType.Int32, ModelId);
            }
            else if (DbOperations.UPDATE_ACCESSORY.Equals(DBOperation))
            {
                DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ModelAccessoryId);
                DataAccess.AddInParameter(objCmd, "Specification", DbType.String, Specification);
            }
            else
            {
                DataAccess.AddInParameter(objCmd, "MakeID", DbType.String, MakeID);
                DataAccess.AddInParameter(objCmd, "Model", DbType.String, Model);

                if (DbOperations.UPDATE.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
            }
        }
        catch (Exception ex)
        { logger.Error("setParameters Function :" + ex.Message); }
    }
    #endregion

    #region get all active models
    /// <summary>
    /// Method to retrieve all active models
    /// </summary>
    /// <returns></returns>
    public DataTable GetAllActiveModels()
    {
        logger.Debug("GetAllActiveModels Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetModels");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "isActive", DbType.Boolean, true);

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
            logger.Debug("GetAllActiveModels Method End");
        }
        return dt;
    }
    #endregion

    #region models of make
    /// <summary>
    /// Method to retrieve active models of a particular make
    /// <para>Expected Properties: MakeID</para>
    /// </summary>
    /// <returns></returns>
    public DataTable GetModelsOfMake()
    {
        logger.Debug("GetModelsOfMake Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetModelsOfMake");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "makeId", DbType.Int32, _intMakeID);

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
            logger.Debug("GetModelsOfMake Method End");
        }
        return dt;
    }
    #endregion

    public DataTable SearchModels()
    {
        try
        {
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchModelByMake");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@makeId", DbType.Int32, MakeID);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Model", DbType.String, Model);
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch
        {
            return null;
        }
    }

    public DataTable GetModelAccessory()
    {
        try
        {
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_GetModelAccessories");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@ModelId", DbType.Int32, ModelId);
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch
        {
            return null;
        }
    }

    public int SetActivenessOfModelAccessory()
    {
        try
        {
            this.SpName = "SpActivateInactivateModelAccessory";
            return HandleModelMaster();
        }
        catch (Exception ex)
        {
            logger.Error("SetActivenessOfModelAccessory Function :" + ex.Message);
            return 0;
        }
    }

    public int AddModelAccessory()
    {
        try
        {
            this.SpName = "SpAddModelAccessory";
            return HandleModelMaster();
        }
        catch (Exception ex)
        {
            logger.Error("AddModel Function :" + ex.Message);
            return 0;
        }
    }

    public int UpdateModelAccessory()
    {
        try
        {
            this.SpName = "SpUpdateModelAccessory";
            return HandleModelMaster();
        }
        catch (Exception ex)
        {
            logger.Error("UpdateModelAccessory Function :" + ex.Message);
            return 0;
        }
    }
}
