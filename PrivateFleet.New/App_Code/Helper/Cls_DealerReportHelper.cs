using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Mechsoft.GeneralUtilities;
using log4net;
using System.Data.Common;

/// <summary>
/// Summary description for Cls_DealerReportHelper
/// </summary>
public class Cls_DealerReportHelper
{
    static ILog logger = LogManager.GetLogger(typeof(Cls_CompletedQuoatationReportHelper));
    private DateTime _ReportDate;
    public DateTime ReportDate
    {
        get { return _ReportDate; }
        set { _ReportDate = value; }
    }

    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string strContactName { get; set; }

    public Cls_DealerReportHelper()
    {
    }
    public DataTable GetReportForToday(DateTime date1)
    {
        try
        {
            logger.Debug("Get report for today is started");
            DbCommand objcmd;
            // date1 = Convert.ToDateTime("04/22/2011");
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "Sp_GetReportForDealerYesterday");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Datecheck", DbType.DateTime, date1);
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch (Exception ex)
        {
            logger.Error("Report cannot be generated" + ex.Message);
            return null;
        }
    }
    public DataTable GetReportForYesterday(DateTime date1)
    {
        try
        {
            logger.Debug("Start the debuging");
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "Sp_GetReportForDealerYesterday");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Datecheck", DbType.DateTime, date1);
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch (Exception ex)
        {
            logger.Error("Method Get Report For Yestaerday canny run " + ex.Message);
            return null;
        }
    }
    public DataTable GetReportForThisMonth(DateTime date1)
    {
        try
        {
            logger.Debug("Method Get Report For This method start ");
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "Sp_GetReportForDealerFomonth");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Datecheck", DbType.DateTime, date1);
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch (Exception ex)
        {
            logger.Error("Method get report for this month can not be run " + ex.Message);
            return null;
        }
    }
    public DataTable GetReportForLastMonth(DateTime date1, DateTime Date2)
    {
        try
        {
            logger.Debug("Method Get Report For Last method start ");
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "Sp_GetReportForDealerLastMonth");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@dateFirst", DbType.DateTime, date1);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@dateLast", DbType.DateTime, Date2);
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch (Exception ex)
        {
            logger.Error("Method get report for last month can not be run " + ex.Message);
            return null;
        }
    }
    public DataTable GetReportForAllTime()
    {
        try
        {
            logger.Debug("Get report for All time is started");
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "Sp_GetReportForDealerForAllTime");
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch (Exception ex)
        {
            logger.Error("Report cannot be generated" + ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Created By : Archana
    /// Date: 26 Feb 2012
    /// Show report to Admin for : Delivery Coming up in Next 10 days for each of the Primary Contact.
    /// </summary>
    /// <returns></returns>
    public DataTable GetDeliveryReportinNxt10Days(string PrimaryContact, int days)
    {
        try
        {
            logger.Debug("Get report for All time is started");
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_GetDeliveryReportinNxt10Days");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "PrimaryContact", DbType.String, PrimaryContact);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Days", DbType.Int32, days);
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch (Exception ex)
        {
            logger.Error("GetDeliveryReportinNxt10Days_Error: " + ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Created By : Archana
    /// Date: 26 Feb 2012
    /// Show report to Admin for : Delivery Coming up in Next 10 days for each of the Primary Contact.
    /// </summary>
    /// <returns></returns>
    public DataTable GetCustStatusUpdateForTrade2(string PrimaryContact, int days)
    {
        try
        {
            logger.Debug("Get report for All time is started");
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_GetCustStatusUpdateForTrade2");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "PrimaryContact", DbType.String, PrimaryContact);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Days", DbType.Int32, days);
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch (Exception ex)
        {
            logger.Error("GetCustStatusUpdateForTrade2_Error: " + ex.Message);
            return null;
        }
    }


    /// <summary>
    /// Created By : Manoj
    /// Date: 26 Oct 2012
    /// Show report to Admin for : Delivery Coming up in Next 10 days for each of the Primary Contact.
    /// </summary>
    /// <returns></returns>
    public DataTable GetDeliveredListReport()
    {
        try
        {
            logger.Debug("Get report for All time is started");
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_GetDeliveredListReport");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "PrimaryContact", DbType.String, strContactName);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@FromDate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@ToDate", DbType.DateTime, ToDate);
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch (Exception ex)
        {
            logger.Error("GetDeliveryReportinNxt10Days_Error: " + ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Created By: Ayyaj Mujawar
    /// Created Date: 3 June 2013
    /// Description: Get Get Process Last RunDate
    /// </summary>
    public DataTable GetProcessLastRunDate(string Process)
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_GetProcessLastRunDate");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Process", DbType.String, Process);
            return (Cls_DataAccess.getInstance().GetDataTable(objCmd, null));
            
        }
        catch (Exception ex)
        {
            logger.Error("GetProcessLastRunDate_Error:" + ex.Message);
            return null;
        }
    }


    /// <summary>
    /// Created By: Ayyaj Mujawar
    /// Created Date: 3 June 2013
    /// Description: Get Oldest Enquiry Date
    /// </summary>
    public Int64 UpdateProcessLastRunDate(string Process)
    {
        DbCommand objCmd = null;
        Int64 Result = 0;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_UpdateProcessLastRunDate");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Process", DbType.String, Process);
            Result = Convert.ToInt64(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null));
            return Result;

        }
        catch (Exception ex)
        {
            logger.Error("UpdateProcessLastRunDate_Error:" + ex.Message);
            return 0;
        }
    }

}
