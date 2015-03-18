using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using System.Data;

/// <summary>
/// Summary description for Cls_AdminFinanceAlert
/// </summary>
public class Cls_AdminFinanceAlert
{
    #region variables

    ILog logger = LogManager.GetLogger(typeof(Cls_AdminReport));

    #endregion

    #region Constructor

    public Cls_AdminFinanceAlert()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #endregion

    #region Metods

    /// <summary>
    /// 07 May 2013 : Manoj : Get list of Customer for whome Finance alert was send to fincar admin
    /// </summary>
    /// <returns></returns>
    public DataTable GetAdminFinanceAlerts()
    {

        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAdminFinAlertsData");
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("Admin Report GetAdminFinanceAlerts Error : " + ex.Message);
            return null;

        }
        finally
        {
            objCmd = null;
        }
    }

    public DataTable GetRecordstoSendAlert()
    {
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetRecordstoSendAlert");
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("Admin Report GetRecordstoSendAlert Error : " + ex.Message);
            return null;

        }
        finally
        {
            objCmd = null;
        }
    }


    public int updateFinanceEmailSend(Int32 CustID)
    {
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpUpdateFinanceEmailSend");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "CustID", DbType.Int32, CustID);
            return Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("Admin Report GetRecordstoSendAlert Error : " + ex.Message);
            return 0;

        }
        finally
        {
            objCmd = null;
        }
    }

    #endregion

}
