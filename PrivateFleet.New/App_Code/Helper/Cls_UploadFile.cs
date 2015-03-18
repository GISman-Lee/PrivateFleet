using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using System.Data;

/// <summary>
/// Summary description for Cls_UploadFile
/// </summary>
public class Cls_UploadFile
{
    #region Variables

    static ILog logger = LogManager.GetLogger(typeof(Cls_UploadFile));

    public Int32 PhotoID { get; set; }
    public string PhotoName { get; set; }
    public string PhotoPath { get; set; }
    public Int32 TradeInID { get; set; }
    public Int32 CreatedBy { get; set; }
    public Int32 ModifiedBy { get; set; }

    public string photoFor { get; set; }

    #endregion

    #region Constructor

    public Cls_UploadFile()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #endregion

    #region Methods

    public int SaveTradeInPhoto()
    {
        DbCommand objCmd = null;
        try
        {
            int result = 0;
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSaveTradeInPhoto");

            Cls_DataAccess.getInstance().AddInParameter(objCmd, "PhotoName", DbType.String, PhotoName);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "PhotoPath", DbType.String, PhotoPath);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "TradeInID", DbType.Int32, TradeInID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "CreatedBy", DbType.Int32, CreatedBy);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "photoFor", DbType.String, photoFor);

            //  return Convert.ToInt32(Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd, null));
            //Execute command
            object obj = Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null);
            Int32.TryParse(obj.ToString(), out result);
            return result;
        }
        catch (Exception ex)
        {
            logger.Error("Cls_UploadFile SaveTradeInPhoto Error - " + ex.Message + " :: Trace - " + ex.StackTrace);
            return 0;
        }
        finally
        {
            objCmd = null;
        }
    }

    public DataTable GetTradeInPhoto()
    {
        DbCommand objCmd = null;
        try
        {

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetTradeInPhoto");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "TradeInID", DbType.Int32, TradeInID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "photoFor", DbType.String, photoFor);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd, null);
        }
        catch (Exception ex)
        {
            logger.Error("Cls_UploadFile GetTradeInPhoto Error - " + ex.Message + " :: Trace - " + ex.StackTrace);
            return null;
        }
        finally
        {
            objCmd = null;
        }
    }

    public int DeleteFromTable()
    {
        DbCommand objCmd = null;
        try
        {

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpDeleteTradeInPhoto");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "PhotoID", DbType.Int32, PhotoID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "photoFor", DbType.String, photoFor);
            return Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd, null);
        }
        catch (Exception ex)
        {
            logger.Error("Cls_UploadFile DeleteFromTable Error - " + ex.Message + " :: Trace - " + ex.StackTrace);
            return 0;
        }
        finally
        {
            objCmd = null;
        }
    }

    #endregion

}
