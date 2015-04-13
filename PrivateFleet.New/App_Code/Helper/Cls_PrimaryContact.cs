using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using System.Data;
using Mechsoft.FleetDeal;


/// <summary>
/// Summary description for Cls_PrimaryContact
/// </summary>
public class Cls_PrimaryContact : Cls_CommonProperties
{
    #region Private Variables

    static ILog logger = LogManager.GetLogger(typeof(Cls_PrimaryContact));
    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();

    public Int64 Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PrimaryContactFor { get; set; }
    public bool IsActive { get; set; }
    public bool SetActiveness { get; set; }

    #endregion

    public Cls_PrimaryContact()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Created By Archana
    /// Date: 23 Feb 2012
    /// details: get all Primary Contact from Primary Contact Table
    /// </summary>
    /// <returns></returns>
    public DataTable GetAllPrimaryContacts()
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetPromaryContacts");
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetAllPrimaryContacts Error - " + ex.Message);
            return null;
        }
        finally
        {
            objCmd = null;
        }
    }

    /// <summary>
    /// Created By : Archana: 4 Aprl 2012
    /// </summary>
    /// <returns></returns>
    public bool UpdatePrimaryContact()
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_UpdatePrimaryContacts");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Id", DbType.Int64, Id);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Name", DbType.String, Name);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Email", DbType.String, Email);
            //Cls_DataAccess.getInstance().AddInParameter(objCmd, "PrimaryContactFor", DbType.String, PrimaryContactFor);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "IsActive", DbType.Boolean, IsActive);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "SetActiveness", DbType.Boolean, SetActiveness);

           return (Convert.ToInt64(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null)) > 0 ? true : false);
        }
        catch (Exception ex)
        {
            logger.Error("GetAllPrimaryContacts Error - " + ex.Message);

        }
        finally
        {
            objCmd = null;
        }
        return false;
    }

    /// <summary>
    /// to add New Dealer
    /// </summary>
    /// <returns> int : indicating no. of records affected in DB</returns>
    public int AddPrimaryContact()
    {
        try
        {
            this.SpName = "sp_AddPrimaryContacts";
            return HandlePrimaryContactMaster();
        }
        catch (Exception ex) { logger.Error("AddPrimaryContact Function :" + ex.Message); return 0; }
    }

    /// <summary>
    /// Activates or Inactivate the SetActivenessOfPrimaryContact
    /// </summary>
    /// <returns>int : indicating no. of records affected in DataBase</returns>
    public int SetActivenessOfPrimaryContact()
    {
        try
        {
            this.SpName = "SpActivateInactivatePrimaryContact";
            return HandlePrimaryContactMaster();
        }
        catch (Exception ex) { logger.Error("SetActivenessOfPrimaryContact Function :" + ex.Message); return 0; }
    }

    /// <summary>
    /// Handles the respective actions on DB
    /// </summary>
    /// <returns>int : returns the integer to subscriber indicating the no of. rows affected in Data Base</returns>
    public int HandlePrimaryContactMaster()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            setParameters(objCmd);

            return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        { logger.Error("HandlePrimaryContactMaster Function :" + ex.Message); return 0; }
    }


    /// <summary>
    /// Created By : Archana: 4 Aprl 2012
    /// </summary>
    /// <returns></returns>
    //public bool AddPrimaryContact()
    //{
    //    DbCommand objCmd = null;
    //    try
    //    {
    //        objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_UpdatePrimaryContacts");
            
    //        Cls_DataAccess.getInstance().AddInParameter(objCmd, "Name", DbType.String, Name);
    //        Cls_DataAccess.getInstance().AddInParameter(objCmd, "Email", DbType.String, Email);
    //        Cls_DataAccess.getInstance().AddInParameter(objCmd, "PrimaryContactFor", DbType.String, PrimaryContactFor);
    //        Cls_DataAccess.getInstance().AddInParameter(objCmd, "IsActive", DbType.Boolean, IsActive);
            

    //        return (Convert.ToInt64(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null)) > 0 ? true : false);
    //    }
    //    catch (Exception ex)
    //    {
    //        logger.Error("GetAllPrimaryContacts Error - " + ex.Message);

    //    }
    //    finally
    //    {
    //        objCmd = null;
    //    }
    //    return false;
    //}

    #region CheckIfPrimaryContact is exist
    /// <summary>
    /// Added By : Ayyaj
    /// to Check whether this accessory already exists
    /// </summary>
    /// <returns> Data Table</returns>
    public DataTable CheckIfPrimaryContactExists()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfPrimaryContactExists");

            setParameters(objCmd);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex) { logger.Error("CheckIfPrimaryContactExists Function :" + ex.Message); return null; }
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
                DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, Id);
            }
            else
            {
                DataAccess.AddInParameter(objCmd, "Name", DbType.String, this.Name);
                //DataAccess.AddInParameter(objCmd, "Email", DbType.String, this.Email);
                //DataAccess.AddInParameter(objCmd, "PrimaryContactFor", DbType.String, this.PrimaryContactFor);

                if (DbOperations.INSERT.Equals(DBOperation))
                {
                    //DataAccess.AddInParameter(objCmd, "Name", DbType.String, this.Name);
                    DataAccess.AddInParameter(objCmd, "Email", DbType.String, this.Email);
                    DataAccess.AddInParameter(objCmd, "PrimaryContactFor", DbType.String, this.PrimaryContactFor);
                }


                if (DbOperations.UPDATE.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, Id);

            }

        }
        catch (Exception ex) { logger.Error("setParameters Function :" + ex.Message); }

    }
    #endregion
}
