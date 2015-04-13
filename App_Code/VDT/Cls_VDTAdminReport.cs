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
using log4net;

/// <summary>
/// Summary description for Cls_VDTAdminReport
/// </summary>
public class Cls_VDTAdminReport
{
    ILog logger = LogManager.GetLogger(typeof(Cls_VDTAdminReport));
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string fullname { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public int makeid { get; set; }
    public int name { get; set; }
    public int dealerid { get; set; }
    public string PrimaryContact { get; set; }
    public Int64 CustomerId { get; set; }
    DataTable dt = new DataTable();
    public Cls_VDTAdminReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int UpdateAsUnmark()
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.Text, "Update tbl_VDT_CustomerMaster Set Unmark=Unmark+1 Where ID=" + CustomerId);
            return Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd, null);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            objCmd = null;
            return -1;
        }

    }

    public DataTable get_VDTAdminHelpReport()
    {
        dt = new DataTable();
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Search_VDTAdminHelp");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "fromdate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "todate", DbType.DateTime, ToDate);
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd, null);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            objCmd = null;
        }
        return dt;


    }

    /// <summary>
    /// Created By : Archana On 29 March 2012
    /// Details : Admin customer Help Status Report
    /// </summary>
    /// <returns></returns>
    public DataTable get_VDTAdminHelpCustomerStatusRpt()
    {
        dt = new DataTable();
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Search_VDTAdminHelpCustomerStatusRpt");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "fromdate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "todate", DbType.DateTime, ToDate);
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd, null);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            objCmd = null;
        }
        return dt;


    }

    public DataTable get_VDTAutomaticMail()
    {
        dt = new DataTable();
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_search_VDTAutomaticMail");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "fromdate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "todate", DbType.DateTime, ToDate);
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd, null);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            objCmd = null;
        }
        return dt;
    }


    public DataTable get_VDTDealerResponse()
    {
        dt = new DataTable();
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_SearchVDTDealerResponseAdmin");

            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd, null);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            objCmd = null;
        }
        return dt;


    }

    public DataTable get_VDTCustomerReport()
    {
        dt = new DataTable();
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Search_VDTCustomerReport");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "fullname", DbType.String, fullname);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "email", DbType.String, email);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "phone", DbType.String, phone);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "makeid", DbType.Int32, makeid);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "dealerid", DbType.Int32, dealerid);

            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd, null);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            objCmd = null;
        }
        return dt;
    }

    /// <summary>
    /// Created By Archana:on 24 Feb 2012
    /// </summary>
    /// <returns></returns>
    public DataTable get_VDTCustomerReport_PC()
    {
        dt = new DataTable();
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Search_VDTCustomerReport_PC");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "fullname", DbType.String, fullname);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "email", DbType.String, email);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "phone", DbType.String, phone);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "makeid", DbType.Int32, makeid);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "dealerid", DbType.Int32, dealerid);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "PrimaryContact", DbType.String, PrimaryContact);

            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd, null);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            objCmd = null;
        }
        return dt;
    }

    public DataTable get_VDTDealerCustomerCount(string Company)
    {
        dt = new DataTable();
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Search_DealerCustomerCount");

            Cls_DataAccess.getInstance().AddInParameter(objCmd, "makeid", DbType.Int32, makeid);
            if (!string.IsNullOrEmpty(Company))
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "Company", DbType.String, Company);

            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd, null);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            objCmd = null;
        }
        return dt;
    }

    public DataTable get_DrasticETAChange()
    {
        dt = new DataTable();
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Search_DrasticETAChange");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "dalerid", DbType.Int32, dealerid);

            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd, null);
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
            objCmd = null;
        }
        return dt;
    }
}
