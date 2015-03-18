using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using log4net;
using Mechsoft.FleetDeal;

/// <summary>
/// Summary description for Cls_QuoteComparison
/// </summary>
public class Cls_QuoteComparison : Cls_CommonProperties
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(Cls_QuoteComparison));

    #region "Constructor"
    public Cls_QuoteComparison()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region "Variables and Properties"
    private int _UserID;
    public int UserID
    {
        get { return _UserID; }
        set { _UserID = value; }
    }

    private int _requestId;
    public int RequestId
    {
        get { return _requestId; }
        set { _requestId = value; }
    }

    private int _consultantId;
    public int ConsultantId
    {
        get { return _consultantId; }
        set { _consultantId = value; }
    }


    private int _QuotationID;
    public int QuotationID
    {
        get { return _QuotationID; }
        set { _QuotationID = value; }
    }

    private int _OptionId;
    public int OptionID
    {
        get { return _OptionId; }
        set { _OptionId = value; }
    }

    private int _DealerID;
    public int DealerID
    {
        get { return _DealerID; }
        set { _DealerID = value; }
    }

    private int _Points;
    public int Points
    {
        get { return _Points; }
        set { _Points = value; }
    }

    private string _SelectedBy;
    public string SelectedBy
    {
        get { return _SelectedBy; }
        set { _SelectedBy = value; }
    }
    #endregion

    #region "Methods"

    /// <summary>
    /// Method to retrieve quote-comparison information
    /// </summary>
    /// <returns>DataSet containing quote-comparison information</returns>
    public DataSet GetQuoteComparisonInfo()
    {
        logger.Debug("GetQuoteComparisonInfo Method Start");

        DbCommand objCmd = null;
        DataSet ds = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetQuoteComparisonInfo");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestId", DbType.Int32, _requestId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ConsultantId", DbType.Int32, _consultantId);

            //Execute command
            ds = Cls_DataAccess.getInstance().GetDataSet(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetQuoteComparisonInfo Method : " + ex.Message);
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetQuoteComparisonInfo Method End");
        }
        return ds;
    }

    /// <summary>
    /// Method to retrieve dealers and options for particular request
    /// </summary>
    /// <returns>Datatable containing dealers and options</returns>
    public DataTable GetDealersAndOptions()
    {
        logger.Debug("GetDealersAndOptions Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetDealersAndOptions");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "requestId", DbType.Int32, _requestId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            throw;
            logger.Error("GetDealersAndOptions Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetDealersAndOptions Method End");
        }
        return dt;
    }


    /// <summary>
    /// Added by Archana : 14 March 2012
    /// Updating Dealer Status
    /// </summary>
    /// <returns></returns>
    public bool UpdateDealerStatus(Int64 _RequestId, Int64 _DealerId)
    {
        logger.Debug("UpdateDealerDStatus Method Start");

        DbCommand objCmd = null;
        bool success = false;
        Int64 result = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_UpdateDealerStatus");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestId", DbType.Int64, _RequestId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerId", DbType.Int64, _DealerId);

            //Execute command
            result = Convert.ToInt64(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null));
            success = result > 0 ? true : false;
        }
        catch (Exception ex)
        {
            logger.Error("UpdateDealerDStatus Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("UpdateDealerDStatus Method End");
        }
        return success;
    }

    public int ShortListQuotation()
    {
        logger.Debug("ShortListQuotation Method Start");

        DbCommand objCmd = null;
        DbTransaction objTrans = null;
        DbConnection objCon = null;
        try
        {
            objCon = Cls_DataAccess.getInstance().GetConnection();
            objCon.Open();
            objTrans = objCon.BeginTransaction();

            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spShortListQuotation");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "QuotationId", DbType.Int32, QuotationID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "OptionID", DbType.Int32, OptionID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Id", DbType.Int32, ID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int32, DealerID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "SelectedBy", DbType.String, SelectedBy);
            //Execute command
            Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd, objTrans);

            objCmd = null;
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAddPointDealerAfterShortListing");
            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestID", DbType.Int32, ID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int32, DealerID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Points", DbType.Int32, Points);

            int Result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd, objTrans);

            if (Result < 1)
            {
                objTrans.Rollback();
                return 0;
            }
            else
            {
                objTrans.Commit();
                return Result;
            }
        }
        catch (Exception ex)
        {
            objTrans.Rollback();
            objCon.Close();
            objCon = null;
            objTrans = null;
            logger.Error("ShortListQuotation Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            objCon.Close();
            objCon = null;
            objTrans = null;

            logger.Debug("ShortListQuotation Method End");
        }
        return 0;
    }
    #endregion
}
