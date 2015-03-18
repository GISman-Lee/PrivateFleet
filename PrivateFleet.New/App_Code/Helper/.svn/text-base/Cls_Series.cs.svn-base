using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Mechsoft.FleetDeal;
using Mechsoft.GeneralUtilities;
using System.Data.Common;
using System.Data;
using log4net;

/// <summary>
/// Summary description for Cls_SeriesMaster
/// </summary>
public class Cls_SeriesMaster : Cls_CommonProperties
{
    
    #region Variable declaration
    static ILog logger = LogManager.GetLogger(typeof(Cls_SeriesMaster));
    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
    #endregion

    #region  Properties
    private int _intModelID;

    public int ModelID
    {
        get { return _intModelID; }
        set { _intModelID = value; }
    }

    private int _intMakeID;

    public int MakeID
    {
        get { return _intMakeID; }
        set { _intMakeID = value; }
    }
    private string _strSeries;

    public string Series
    {
        get { return _strSeries; }
        set { _strSeries = value; }
    }

    #endregion

    #region Constructor
    public Cls_SeriesMaster()
	{
    }
    #endregion

    #region Get All Series
    public DataTable GetAllSeries()
    {
        logger.Debug("GetAllSeries Methode  Start :");
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllSeries");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetAllSeries Function :" + ex.Message); return null; }
        finally { logger.Debug("GetAllSeries Methode End"); }
    }
#endregion

    #region Get all Active Series
    public DataTable GetAllActiveSeries()
    {
        logger.Debug("GetAllActiveSeries Methode Start");

        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllActiveSeries");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetAllActiveSeries Event :" + ex.Message); return null; }
        finally { logger.Debug("GetAllActiveSeries Methode End"); }
    }
#endregion

    #region Check if exist
    public DataTable CheckIfSeriesExists()
    {
        logger.Debug("CheckIfSeriesExists Methode Start");
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfSeriesExists");
            setParameters(objCmd);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("CheckIfSeriesExists Function :" + ex.Message); return null; }
        finally { logger.Debug("CheckIfSeriesExists Methode End"); }
    }
#endregion

    #region Add series
    public int AddSeries()
    {
        try
        {
            this.SpName = "SpAddSeries";
            return HandleSeriesMaster();
        }
        catch (Exception ex)
        { logger.Error("AddSeries Function :" + ex.Message); return 0; }
    }
#endregion

    #region Update series
    public int UpdateSeries()
    {
        try
        {
            this.SpName = "SpUpdateSeries";
            return HandleSeriesMaster();
        }
        catch (Exception ex)
        { logger.Error("UpdateSeries Function :" + ex.Message); return 0; }
    }
#endregion

    #region Set activeness of Series
    public int SetActivenessOfSeries()
    {
        try
        {
            this.SpName = "SpActivateInactivateSeries";
            return HandleSeriesMaster();
        }
        catch (Exception ex)
        { logger.Error("SetActivenessOfSeries Function :" + ex.Message); return 0; }
    }
#endregion

    #region Handel SeriesMaster
    public int HandleSeriesMaster()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            setParameters(objCmd);

            return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex) { logger.Error("HandleSeriesMaster Function :" + ex.Message); return 0; }
    }
#endregion

    #region Set parameters
    private void setParameters(DbCommand objCmd)
    {
        try
        {
            if (DbOperations.CHANGE_ACTIVENESS.Equals(DBOperation))
            {
                DataAccess.AddInParameter(objCmd, "IsActive", DbType.Boolean, IsActive);
                DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
            }
            else
            {

                DataAccess.AddInParameter(objCmd, "ModelID", DbType.String, ModelID);
                DataAccess.AddInParameter(objCmd, "Series", DbType.String, Series);


                if (DbOperations.UPDATE.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
            }


        }
        catch (Exception ex) { logger.Error("setParameters Function :"+ex.Message); }
    }
    #endregion

    #region Get Series of model
    /// <summary>
    /// Method to retrieve active series of a particular model
    /// <para>Expected Properties: ModelID</para>
    /// </summary>
    /// <returns></returns>
    public DataTable GetSeriesOfModel()
    {
        logger.Debug("GetSeriesOfModel Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetSeriesOfModel");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "modelId", DbType.Int32, _intModelID);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch
        {
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetSeriesOfModel Method End");
        }
        return dt;
    }
    #endregion

    public DataTable GetSeriesByMakeAndModel()
    {
        try
        {
            DbCommand objcmd;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchSeriesByMakeAndModel");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@MakeId", DbType.Int32, MakeID);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@modelId", DbType.Int32, ModelID);
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Series", DbType.String, Series);
            return (Cls_DataAccess.getInstance().GetDataTable(objcmd));
        }
        catch (Exception ex)
        { return null; }
    }
}
