using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Data;
using System.Data.Common;
using Mechsoft.GeneralUtilities;

/// <summary>
/// Summary description for Cls_DiscountLeadsReport
/// </summary>
public class Cls_DiscountLeadsReport
{
    #region Variables

    static ILog logger = LogManager.GetLogger(typeof(Cls_DiscountLeadsReport));

    public int LeadType { get; set; }
    public int Company { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    #endregion

    #region Constructor

    public Cls_DiscountLeadsReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #endregion

    #region Method

    public DataTable getDiscountLeadsReport()
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetDiscountLeads");
            //Execute command
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@LeadType", DbType.Int32, LeadType);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Company", DbType.Int32, Company);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@FromDate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ToDate", DbType.DateTime, ToDate);
            //Execute command
            return Cls_DataAccess.getInstance().GetDataTable(objCmd, null);
        }
        catch (Exception ex)
        {
            logger.Error("Cls_DiscountLeadsReport getDiscountLeadsReport Error - " + ex.Message + " :: Trace - " + ex.StackTrace);
            return null;
        }
        finally
        {
            objCmd = null;
        }
    }

    #endregion
}
