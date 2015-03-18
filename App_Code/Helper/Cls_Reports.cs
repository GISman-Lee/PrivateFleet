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
/// Summary description for Cls_Reports
/// </summary>
public class Cls_Reports : Cls_SeriesMaster
{
    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
    DbCommand objcmd = null;
    DataTable dtData = null;


    private int _QuotationID;
    public int QuotationID
    {
        get { return _QuotationID; }
        set { _QuotationID = value; }
    }

    private int _OptionID;
    public int OptionID
    {
        get { return _OptionID; }
        set { _OptionID = value; }
    }

    private int _DealerID;
    public int DealerID
    {
        get { return _DealerID; }
        set { _DealerID = value; }
    }

    private string _StartDate;
    public String StartDate
    {
        get { return _StartDate; }
        set { _StartDate = value; }
    }

    private string _EndDate;
    public String EndDate
    {
        get { return _EndDate; }
        set { _EndDate = value; }
    }

    private string _Date;
    public String Date
    {
        get { return Date; }
        set { Date = value; }
    }

    private int _ConsultantID;
    public int ConsultantID
    {
        get { return _ConsultantID; }
        set { _ConsultantID = value; }
    }

    private int _MakeID;
    public int MakeID
    {
        get { return _MakeID; }
        set { _MakeID = value; }
    }

    private int _ModelID;
    public int Modelid
    {
        get { return _ModelID; }
        set { _ModelID = value; }
    }
    private int _SeriesID;
    public int SeriesID
    {
        get { return _SeriesID; }
        set { _SeriesID = value; }
    }
    private String _State;
    public String State
    {
        get { return _State; }
        set { _State = value; }
    }


    private string _ReportType;
    public string TypeOfReport
    {
        get { return _ReportType; }
        set { _ReportType = value; }
    }

    private string _ReportGroupBy;
    public string GroupBy
    {
        get { return _ReportGroupBy; }
        set { _ReportGroupBy = value; }
    }

    private string _GroupByParameterValue;
    public string GroupByParameterValue
    {
        get { return _GroupByParameterValue; }
        set { _GroupByParameterValue = value; }
    }
    private DateTime _FromDate;
    public DateTime FromDate
    {
        get { return _FromDate; }
        set { _FromDate = value; }
    }

    private DateTime _ToDate;
    public DateTime ToDate
    {
        get { return _ToDate; }
        set { _ToDate = value; }
    }

    public string ConsultantName { get; set; }
    public string surname { get; set; }

    public Cls_Reports()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public DataTable GetInitialData()
    {
        switch (GroupBy)
        {
            case ReportGroupBy.GroupByDealer: this.SpName = "SpGetDealersToGroupReport";
                break;
            case ReportGroupBy.GroupByConsultant: this.SpName = "SpGetConsultantToGroupReport";
                break;
            case ReportGroupBy.GroupByState: this.SpName = "SpGetStateToGroupReport";
                break;
            case ReportGroupBy.GroupByDate: this.SpName = "SpGetDateToGroupReport";
                break;
            default: break;
        }

        objcmd = DataAccess.GetCommand(CommandType.StoredProcedure, this.SpName);
        SetDateRangeParameters(objcmd);
        return DataAccess.GetDataTable(objcmd);

    }

    private void SetDateRangeParameters(DbCommand objcmd)
    {
        DataAccess.AddInParameter(objcmd, "StartDate", DbType.String, StartDate);
        DataAccess.AddInParameter(objcmd, "EndDate", DbType.String, EndDate);
        DataAccess.AddInParameter(objcmd, "ReportType", DbType.String, DBOperation);
        DataAccess.AddInParameter(objcmd, "MakeID", DbType.Int32, MakeID);
        DataAccess.AddInParameter(objcmd, "ModelID", DbType.Int32, ModelID);
        DataAccess.AddInParameter(objcmd, "SeriesID", DbType.Int32, SeriesID);
        DataAccess.AddInParameter(objcmd, "State", DbType.String, State);
    }

    public DataTable GetDataByDate()
    {
        objcmd = DataAccess.GetCommand(CommandType.StoredProcedure, "SpReportGroupByDealer");
        DataAccess.AddInParameter(objcmd, "Date", DbType.Int32, Date);
        dtData = DataAccess.GetDataTable(objcmd);
        return dtData;
    }

    public DataTable GroupedData()
    {
        switch (GroupBy)
        {
            case ReportGroupBy.GroupByDealer: this.SpName = "SpReportGroupByDealer";
                break;
            case ReportGroupBy.GroupByConsultant: this.SpName = "SpReportGroupByConsultant";
                break;
            case ReportGroupBy.GroupByState: this.SpName = "SpReportGroupByState";
                break;
            case ReportGroupBy.GroupByDate: this.SpName = "SpGetDateToGroupReport";
                break;

            default: break;
        }

        objcmd = DataAccess.GetCommand(CommandType.StoredProcedure, this.SpName);
        SetDealerParameters(objcmd);
        dtData = DataAccess.GetDataTable(objcmd);
        return dtData;

    }

    private void SetDealerParameters(DbCommand objcmd)
    {

        DataAccess.AddInParameter(objcmd, "GroupByParameter", DbType.String, GroupByParameterValue);
        DataAccess.AddInParameter(objcmd, "StartDate", DbType.String, StartDate);
        DataAccess.AddInParameter(objcmd, "EndDate", DbType.String, EndDate);
        DataAccess.AddInParameter(objcmd, "ReportType", DbType.String, DBOperation);
        DataAccess.AddInParameter(objcmd, "MakeID", DbType.Int32, MakeID);
        DataAccess.AddInParameter(objcmd, "ModelID", DbType.Int32, ModelID);
        DataAccess.AddInParameter(objcmd, "SeriesID", DbType.Int32, SeriesID);
        DataAccess.AddInParameter(objcmd, "State", DbType.String, State);
    }

    public DataTable GetQuotes()
    {
        GenerateQueryToGetQuotes();
        objcmd = DataAccess.GetCommand(CommandType.StoredProcedure, this.SpName);
        SetParameters(objcmd);
        return DataAccess.GetDataTable(objcmd);
    }

    private void SetParameters(DbCommand objcmd)
    {
        DataAccess.AddInParameter(objcmd, "QuotationID", DbType.Int32, QuotationID);
        if (DBOperation.Equals(QuoteFilters.WinningQuotes))
            DataAccess.AddInParameter(objcmd, "OptionID", DbType.Int32, OptionID);
    }

    private void GenerateQueryToGetQuotes()
    {

        if (DBOperation == QuoteFilters.AllQuotesReturned)
        {
            this.SpName = "SpGetAllOptionsOfTheQuote";
        }
        else
        {
            this.SpName = "SpGetShoertListedOptionOfTheQuote";

        }
    }

    public DataTable GetConsultantSummary()
    {
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "Sp_GetReportForConsultant");
            //Execute command
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ConsultantID", DbType.Int32, ConsultantID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@FromDate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ToDate", DbType.DateTime, ToDate);
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch
        {
            throw;
        }
        finally
        {
            objCmd = null;
        }
        return dt;
    }

    public DataTable ValidateConsultant()
    {
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpConsultantSRCriteria");
            //Execute command
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ConsultantID", DbType.Int32, ConsultantID);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            objCmd = null;
        }

    }

    public DataTable GetFinanceReferralByUserID()
    {
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllFinanceReferral");
            //Execute command
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ConsultantID", DbType.Int32, ConsultantID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@FromDate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ToDate", DbType.DateTime, ToDate);
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch
        {
            throw;
        }
        finally
        {
            objCmd = null;
        }
        return dt;
    }

    public DataTable GetFinanceReferralReport()
    {
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetFinanceReferralReport");
            //Execute command
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@FromDate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ToDate", DbType.DateTime, ToDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ConsultantName", DbType.String, ConsultantName);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@surname", DbType.String, surname);

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch
        {
            return null;
        }
        finally
        {
            objCmd = null;
        }
    }
}
