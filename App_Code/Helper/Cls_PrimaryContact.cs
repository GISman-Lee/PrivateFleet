using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using System.Data;

/// <summary>
/// Summary description for Cls_PrimaryContact
/// </summary>
public class Cls_PrimaryContact
{
    #region Private Variables

    static ILog logger = LogManager.GetLogger(typeof(Cls_PrimaryContact));

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
}
