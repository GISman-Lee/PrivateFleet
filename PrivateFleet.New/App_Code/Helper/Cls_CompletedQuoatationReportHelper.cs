using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Data.SqlClient;
using Mechsoft.GeneralUtilities;
using log4net;
using System.Data.Common;


public class Cls_CompletedQuoatationReportHelper
{
    static ILog logger = LogManager.GetLogger(typeof(Cls_CompletedQuoatationReportHelper));
    public Cls_CompletedQuoatationReportHelper()
    {
    }

    #region Variable and properties
    private int _MakeId;
    public int MakeId
    {
        get { return _MakeId; }
        set { _MakeId = value; }
    }

    private int _ModelId;
    public int ModelId
    {
        get { return _ModelId; }
        set { _ModelId = value; }
    }

    public string ModelName { get; set; }

    private int _SeriesId;
    public int SeriesId
    {
        get { return _SeriesId; }
        set { _SeriesId = value; }
    }

    private int _StateId;
    public int StateId
    {
        get { return _StateId; }
        set { _StateId = value; }
    }
    private int _Winning;
    public int Winning
    {
        get { return _Winning; }
        set { _Winning = value; }
    }
    private string _Transmision;
    public string Transmision
    {
        get { return _Transmision; }
        set { _Transmision = value; }
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

    // added for Trade in report
    public int HomeStateID { get; set; }
    public string T1FromYear { get; set; }
    public string T1ToYear { get; set; }
    public double T1FromValue { get; set; }
    public double T1ToValue { get; set; }
    public int ConsultantID { get; set; }

    //on 23 june 12
    public string RegoNo { get; set; }
    public string Surname { get; set; }


    //on 27 june 12 For Completed Quotation Report
    public string FuelType { get; set; }

    // on 31 jul 2012 for history of trade in report
    public string Key { get; set; }
    public string Consultant { get; set; } // 15 feb 2013

    #endregion

    #region Methods
    public DataTable GetMake()
    {
        try
        {
            StringBuilder query = new StringBuilder();
            DataTable dt = new DataTable();

            SqlCommand objcmd = null;
            query.Append("select ID,Make from tblMakeMaster where IsActive=1;");
            objcmd = (SqlCommand)Cls_DataAccess.getInstance().GetCommand(CommandType.Text, query.ToString());
            dt = Cls_DataAccess.getInstance().GetDataTable(objcmd);
            return dt;
        }
        catch (Exception ex)
        { return null; }
    }

    public DataTable GetModel(int MakeId)
    {
        try
        {
            DataTable dt = new DataTable();
            StringBuilder queryForModel = new StringBuilder();
            SqlCommand objcmd = null;
            queryForModel.Append("select ID,Model from tblModelMaster where IsActive=1 AND MakeID=@id;");
            objcmd = (SqlCommand)Cls_DataAccess.getInstance().GetCommand(CommandType.Text, queryForModel.ToString());
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@id", DbType.Int32, MakeId);
            dt = Cls_DataAccess.getInstance().GetDataTable(objcmd);
            return dt;
        }
        catch (Exception ex)
        { return null; }
    }

    public DataTable GetSeries(int ModelId)
    {
        try
        {
            DataTable dt = new DataTable();
            StringBuilder queryForSeries = new StringBuilder();
            SqlCommand objcmd = null;
            queryForSeries.Append("SELECT ID,Series FROM tblSeriesMaster where IsActive=1 AND ModelID=@id;");
            objcmd = (SqlCommand)Cls_DataAccess.getInstance().GetCommand(CommandType.Text, queryForSeries.ToString());
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@id", DbType.Int32, ModelId);

            dt = Cls_DataAccess.getInstance().GetDataTable(objcmd);
            return dt;
        }
        catch (Exception ex)
        { return null; }
    }

    public DataTable GetStates()
    {
        try
        {
            DataTable dt = new DataTable();
            StringBuilder queryForState = new StringBuilder();
            SqlCommand objcmd = null;
            queryForState.Append("Select ID,State from tblStateMaster where IsActive=1;");
            objcmd = (SqlCommand)Cls_DataAccess.getInstance().GetCommand(CommandType.Text, queryForState.ToString());
            dt = Cls_DataAccess.getInstance().GetDataTable(objcmd);
            return dt;
        }
        catch (Exception ex)
        {
            logger.Error("Error in Get State Method" + ex.Message);
            return null;
        }
    }
    #endregion

    public DataTable GetReport()
    {
        StringBuilder sqlQuery = new StringBuilder();
        DataTable dt = new DataTable();
        DbCommand objcmd;
        try
        {
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Get_CompletedQuoatationReport");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@MakeId", DbType.Int32, MakeId);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@ModelId", DbType.Int32, ModelId);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@ModelName", DbType.String, ModelName);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@FromDate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Todate", DbType.DateTime, ToDate);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@SeriesId", DbType.Int32, SeriesId);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@stateId", DbType.Int32, StateId);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Transmision", DbType.String, Transmision);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@winning", DbType.Int32, Winning);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@ConsultantID", DbType.Int32, ConsultantID);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@FuelType", DbType.String, FuelType);
            dt = Cls_DataAccess.getInstance().GetDataTable(objcmd);
            return dt;
        }
        catch (Exception ex)
        {
            logger.Error("Report Cannot be generated " + ex.Message);
            return null;
        }
        finally
        {
            objcmd = null;
        }
    }



    public DataTable GetTransmission(int SeriesId)
    {
        try
        {
            StringBuilder query = new StringBuilder();
            DataTable dt = new DataTable();

            SqlCommand objcmd = null;
            // query.Append("select Transmmision from tblSeriesMaster where IsActive=1 AND ID=@SeriesId;");
            query.Append("select ID,Name from Tbl_Transmission_Type;");
            objcmd = (SqlCommand)Cls_DataAccess.getInstance().GetCommand(CommandType.Text, query.ToString());
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@SeriesId", DbType.Int32, SeriesId);
            dt = Cls_DataAccess.getInstance().GetDataTable(objcmd);
            return dt;
        }
        catch (Exception ex)
        { return null; }
    }

    // added on 16 nov 2011 for trade in report
    public DataTable GetReportForTradeIn()
    {
        try
        {
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spGetTradeInReport");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@MakeId", DbType.Int32, MakeId);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@ModelName", DbType.String, ModelName);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@HomeStateID", DbType.Int32, HomeStateID);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Transmision", DbType.Int32, Convert.ToInt32(Transmision));
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@T1FromYear", DbType.String, T1FromYear);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@T1ToYear", DbType.String, T1ToYear);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@T1FromValue", DbType.Double, T1FromValue);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@T1ToValue", DbType.Double, T1ToValue);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@RegoNo", DbType.String, RegoNo);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Surname", DbType.String, Surname);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Consultant", DbType.String, Consultant);

            return Cls_DataAccess.getInstance().GetDataTable(objcmd);
        }
        catch (Exception ex)
        {
            logger.Error("Report Cannot be generated " + ex.Message);
            return null;
        }
    }
    //end

    // on 31 Jul 2012 for history
    public DataTable GetTradeInHistoryByKey()
    {
        try
        {
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spGetTradeInHistoryByKey");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Key", DbType.String, Key);

            return Cls_DataAccess.getInstance().GetDataTable(objcmd);
        }
        catch (Exception ex)
        {
            logger.Error("Report Cannot be generated " + ex.Message);
            return null;
        }
    }
    //

    // added on 20 Jun 2013 for trade in 2 report
    public DataTable GetReportForTradeIn2()
    {
        try
        {
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spGetTradeIn2Report");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@MakeId", DbType.Int32, MakeId);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@ModelName", DbType.String, ModelName);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@HomeStateID", DbType.Int32, HomeStateID);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Transmision", DbType.Int32, Convert.ToInt32(Transmision));
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@RegoNo", DbType.String, RegoNo);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Surname", DbType.String, Surname);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Consultant", DbType.String, Consultant);

            return Cls_DataAccess.getInstance().GetDataTable(objcmd);
        }
        catch (Exception ex)
        {
            logger.Error("Report Cannot be generated " + ex.Message);
            return null;
        }
    }
    //end

    // on 20 Jun 2013 for history for Trade In 2
    public DataTable GetTradeIn2HistoryByKey()
    {
        try
        {
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spGetTradeIn2HistoryByKey");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Key", DbType.String, Key);

            return Cls_DataAccess.getInstance().GetDataTable(objcmd);
        }
        catch (Exception ex)
        {
            logger.Error("Report Cannot be generated " + ex.Message);
            return null;
        }
    }
    //
    /// <summary>
    /// Created By: Ayyaj Mujawar
    /// On: 17 Oct 2014
    /// Desc: Report for Trade in 1 and 2 
    /// </summary>
    /// <returns></returns>
    public DataTable GetReportForTradeIn1_2()
    {
        try
        {
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spGetTradeInReport_1&2");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@MakeId", DbType.Int32, MakeId);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@ModelName", DbType.String, ModelName);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@HomeStateID", DbType.Int32, HomeStateID);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Transmision", DbType.Int32, Convert.ToInt32(Transmision));
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@T1FromYear", DbType.String, T1FromYear);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@T1ToYear", DbType.String, T1ToYear);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@T1FromValue", DbType.Double, T1FromValue);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@T1ToValue", DbType.Double, T1ToValue);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@RegoNo", DbType.String, RegoNo);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Surname", DbType.String, Surname);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Consultant", DbType.String, Consultant);

            return Cls_DataAccess.getInstance().GetDataTable(objcmd);
        }
        catch (Exception ex)
        {
            logger.Error(" GetReportForTradeIn1_2 Report Cannot be generated " + ex.Message);
            return null;
        }
    }
}
