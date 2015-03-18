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
/// Summary description for Cls_AdminConfig
/// </summary>
public class Cls_AdminConfig : Cls_CommonProperties
{
    public Cls_AdminConfig()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
    ILog logger = LogManager.GetLogger(typeof(Cls_AdminConfig));

    #region Properties
    private string _strName;

    public string Name
    {
        get { return _strName; }
        set { _strName = value; }
    }

    private string _strValue;

    public string Value
    {
        get { return _strValue; }
        set { _strValue = value; }
    }

    private Int64 _intModifiedBy;

    public Int64 ModifiedBy
    {
        get { return _intModifiedBy; }
        set { _intModifiedBy = value; }
    }

    #endregion

    #region Functions

    public DataTable GetAllConfigValues()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllConfigValues");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetAllConfigValues Function :" + ex.Message);
            return null;
        }
    }

    public DataTable CheckIfConfigValuesExists()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfKeyExists");
            DataAccess.AddInParameter(objCmd, "Name", DbType.String, this.Name);
            DataAccess.AddInParameter(objCmd, "Value", DbType.String, this.Value);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("CheckIfConfigValuesExists Function :" + ex.Message);
            return null;
        }
    }

    public int AddConfigValue()
    {
        try
        {
            this.SpName = "SpAddConfigData";
            return HandleConfigValues();
        }
        catch (Exception ex)
        {
            logger.Error("AddConfigValue Functions :" + ex.Message);
            return 0;
        }
    }

    public int UpdateConfigValue()
    {
        try
        {
            this.SpName = "SpUpdateConfigData";
            return HandleConfigValues();
        }
        catch (Exception ex)
        {
            logger.Error("UpdateConfigValue Function :" + ex.Message);
            return 0;
        }
    }

    public int SetActivenessOfConfigValue()
    {
        try
        {
            this.SpName = "SpActivateInactivateConfigValue";
            return HandleConfigValues();
        }
        catch (Exception ex)
        {
            logger.Error("SetActivenessOfConfigValue Function :" + ex.Message);
            return 0;
        }
    }

    public int HandleConfigValues()
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
            logger.Error("HandleConfigValues Function :" + ex.Message);
            return 0;
        }
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
                // DataAccess.AddInParameter(objCmd, "Name", DbType.String, this.Name);
                DataAccess.AddInParameter(objCmd, "Value", DbType.String, Value);
                DataAccess.AddInParameter(objCmd, "ModifiedBy", DbType.String, ModifiedBy);
                if (DbOperations.UPDATE.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
            }
        }
        catch (Exception ex)
        {
            logger.Error("setParameters Function :" + ex.Message);
        }
    }

    #endregion
}
