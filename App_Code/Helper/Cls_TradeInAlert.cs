using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using System.Data;

/// <summary>
/// Summary description for Cls_TradeInAlert
/// </summary>
public class Cls_TradeInAlert
{
    static ILog logger = LogManager.GetLogger(typeof(Cls_TradeInAlert));

    public Cls_TradeInAlert()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public int AlertID { get; set; }
    public string CName { get; set; }
    public string Contact { get; set; }
    public int MakeID { get; set; }
    public string Model { get; set; }
    public int AlertPeriod { get; set; }
    public string Notes { get; set; }
    public int CreatedBy { get; set; }
    public int IsActive { get; set; }
    public int RunStatus { get; set; }

    public int TransmissionID { get; set; }
    public int StateID { get; set; }
    public int FromYear { get; set; }
    public int ToYear { get; set; }
    public int MaxValue { get; set; }
    public int MinValue { get; set; }

    //on 23 june 12 - change on 28 jun 12
    public string RegoNo { get; set; }
    public string Surname { get; set; }

    public int TradeID { get; set; }

    public int saveAlerts()
    {
        DbCommand objCmd;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSaveTradeInAlerts");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@CName", DbType.String, CName);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Contact", DbType.String, Contact);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@MakeID", DbType.Int32, MakeID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Model", DbType.String, Model);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@AlertPeriod", DbType.Int32, AlertPeriod);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Notes", DbType.String, Notes);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@CreatedBy", DbType.Int32, CreatedBy);
            int result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);

            return result;
        }
        catch (Exception ex)
        {
            logger.Error("Err while saving alert - " + ex.Message);
            return 0;
        }
        finally
        {
            objCmd = null;
        }

    }

    public DataTable getAlertData()
    {
        DbCommand objCmd;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetTradeInAlert");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@CreatedBy", DbType.Int32, CreatedBy);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("Cls Alert data bind err - " + ex.Message);
            return null;
        }
        finally
        {
            objCmd = null;
        }
    }

    public int activateAlert()
    {
        DbCommand objCmd;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpActivateAlert");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@AlertID", DbType.Int32, AlertID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@IsActive", DbType.Int32, IsActive);
            return Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("Cls Alert data bind err - " + ex.Message);
            return 0;
        }
        finally
        {
            objCmd = null;
        }
    }

    public int activateAlert_new()
    {
        DbCommand objCmd;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpDeActivateAlert");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@AlertID", DbType.Int32, AlertID);
            return Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("Cls Alert data bind err - " + ex.Message);
            return 0;
        }
        finally
        {
            objCmd = null;
        }
    }

    public DataSet RunSavedSearch()
    {
        DbCommand objCmd;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpRunSavedSearch");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@CreatedBy", DbType.Int32, CreatedBy);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@RunStatus", DbType.Int32, RunStatus);
            return Cls_DataAccess.getInstance().GetDataSet(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("run save search err app code - " + ex.Message);
            return null;
        }
        finally
        {
            objCmd = null;
        }
    }

    public int saveTradeInSearch()
    {
        DbCommand objCmd;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSaveTradeInSearch");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@MakeID", DbType.Int32, MakeID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Model", DbType.String, Model);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@TransmissionID", DbType.Int32, TransmissionID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@StateID", DbType.Int32, StateID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@FromYear", DbType.Int32, FromYear);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ToYear", DbType.Int32, ToYear);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@MinValue", DbType.Double, MinValue);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@MaxValue", DbType.Double, MaxValue);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@CreatedBy", DbType.Int32, CreatedBy);

            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@CName", DbType.String, CName);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Contact", DbType.String, Contact);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@AlertPeriod", DbType.Int32, AlertPeriod);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Notes", DbType.String, Notes);

            // on 23 June 12
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@RegoNo", DbType.String, RegoNo);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Surname", DbType.String, Surname);

            int result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);

            return result;
        }
        catch (Exception ex)
        {
            logger.Error("Err while saving alert - " + ex.Message);
            return 0;
        }
        finally
        {
            objCmd = null;
        }
    }

    public DataTable GetReportForTradeInSearch()
    {
        DbCommand objCmd;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spGetReportForTradeInSearch");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@CreatedBy", DbType.Int32, CreatedBy);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("get saved search err- " + ex.Message);
            return null;
        }
        finally
        {
            objCmd = null;
        }
    }

    public DataSet GetReportForTradeInAlert()
    {
        DbCommand objCmd;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spGetReportForTradeInAlert");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@CreatedBy", DbType.Int32, CreatedBy);
            return Cls_DataAccess.getInstance().GetDataSet(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("get saved search err- " + ex.Message);
            return null;
        }
        finally
        {
            objCmd = null;
        }
    }

    public int RemoveTradeIn()
    {
        DbCommand objCmd;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpRemoveTradeIn");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@TradeID", DbType.Int32, TradeID);
            return Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("Cls trade in Alert RemoveTradeIn err - " + ex.Message);
            return 0;
        }
        finally
        {
            objCmd = null;
        }
    }

}
