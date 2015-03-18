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
using Mechsoft.GeneralUtilities;
using System.Data.Common;
using System.Data;
using log4net;
using Mechsoft.FleetDeal;
/// <summary>
/// Summary description for Cls_ChargeType
/// </summary>
public class Cls_ChargeType : Cls_CommonProperties
{
    public Cls_ChargeType()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
    ILog logger = LogManager.GetLogger(typeof(Cls_ChargeType));

    #region Properties
    private string _strChargeType;

    public string ChargeType
    {
        get { return _strChargeType; }
        set { _strChargeType = value; }
    }


    #endregion


    #region Functions

    /// <summary>
    /// returns all Charge Types avaialable
    /// </summary>
    /// <returns>Data Table : Containing all available Charge Types</returns>
    public DataTable GetAllChargeTypes()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllChargeType");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetAllChargeTypes Function :" + ex.Message);
            return null;
        }
    }


    /// <summary>
    /// to Check whether this Charge Types already exists
    /// </summary>
    /// <returns> Data Table</returns>
    /// 

    public DataTable CheckIfChargeTypeExists()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIFChargeTypeExists");

            setParameters(objCmd);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex) { logger.Error("CheckIfChargeTypeExists Function :" + ex.Message); return null; }
    }

    /// <summary>
    /// to add New Charge Types
    /// </summary>
    /// <returns> int : indicating no. of records affected in DB</returns>
    public int AddChargeType()
    {
        try
        {
            this.SpName = "SpAddChargeType";
            return HandleChargeTypeMaster();
        }
        catch (Exception ex)
        {
            logger.Error("AddChargeType Function :" + ex.Message);
            return 0;
        }
    }

    /// <summary>
    /// to update the existing Charge Type
    /// </summary>
    /// <returns> int : indicating no. of records affected in DataBase</returns>
    public int UpdateChargeType()
    {
        try
        {
            this.SpName = "SpUpdateChargeType";
            return HandleChargeTypeMaster();
        }
        catch (Exception ex)
        {
            logger.Error("UpdateChargeType Function :" + ex.Message);
            return 0;
        }
    }

    /// <summary>
    /// Activates or Inactivate the Charge Type
    /// </summary>
    /// <returns>int : indicating no. of records affected in DataBase</returns>
    public int SetActivenessOfChargeType()
    {
        try
        {
            this.SpName = "SpActivateInactivateChargeType";
            return HandleChargeTypeMaster();
        }
        catch (Exception ex)
        {
            logger.Error("SetActivenessOfChargeType Function :" + ex.Message);
            return 0;
        }
    }

    public int HandleChargeTypeMaster()
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
            logger.Error("HandleChargeTypeMaster Function :" + ex.Message);
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

                DataAccess.AddInParameter(objCmd, "Type", DbType.String, ChargeType);

                if (DbOperations.UPDATE.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);

            }


        }
        catch (Exception ex) { logger.Error("setParameters Function :" + ex.Message); }
    }
    #endregion


}
