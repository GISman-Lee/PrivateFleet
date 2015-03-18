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
using System.Xml;
using Mechsoft.FleetDeal;


/// <summary>
/// Summary description for Cls_AdminReport
/// </summary>
public class Cls_AdminReport : Cls_CommonProperties
{

    ILog logger = LogManager.GetLogger(typeof(Cls_AdminReport));

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

    private int _seriesId;
    public int SeriesId
    {
        get { return _seriesId; }
        set { _seriesId = value; }
    }

    private int _consultantId;
    public int ConsultantId
    {
        get { return _consultantId; }
        set { _consultantId = value; }
    }

    private string _consultantNotes;
    public string ConsultantNotes
    {
        get { return _consultantNotes; }
        set { _consultantNotes = value; }
    }

    private string _dealerIds;
    public string DealerIds
    {
        get { return _dealerIds; }
        set { _dealerIds = value; }
    }

    private string _accessoryIds;
    public string AccessoryIds
    {
        get { return _accessoryIds; }
        set { _accessoryIds = value; }
    }

    //private string _xmlDocument;
    //public string XmlDocument
    //{
    //    get { return _xmlDocument; }
    //    set { _xmlDocument = value; }
    //}

    private string _xmlDocument;
    public string XmlDocument
    {
        get { return _xmlDocument; }
        set { _xmlDocument = value; }
    }


    private int _DealerId;
    public int DealerId
    {
        get { return _DealerId; }
        set { _DealerId = value; }
           
    }

    private int _Points;
    public int Points
    {
        get { return _Points; }
        set { _Points = value; }
           
    }
    
    #endregion

    public Cls_AdminReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    // <summary>
    /// Method to get Request against which the quotation is shortlisted for consultant
    /// <para>Expected Properties: ConsultantId</para>
    /// </summary>
    /// <returns>Datatable containing sent quote requests for consultant</returns>
    public DataTable GetShortListedQuotationsRequests()
    {
        logger.Debug("GetShortListedQuotationsRequests Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetShortListedQuotationsRequests");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "consultantId", DbType.Int32, _consultantId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            throw;
            logger.Error("GetShortListedQuotationsRequests Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetShortListedQuotationsRequests Method End");
        }
        return dt;
    }


    public DataTable GetAllShortListedQuotationsRequests()
    {
        logger.Debug("GetAllShortListedQuotationsRequests Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllShortListedQuotationsRequests");

            //Add parameters
            //Cls_DataAccess.getInstance().AddInParameter(objCmd, "consultantId", DbType.Int32, _consultantId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            throw;
            logger.Error("GetAllShortListedQuotationsRequests Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetAllShortListedQuotationsRequests Method End");
        }
        return dt;
    }

    public int MarkAsDealDone()
    {
        logger.Debug("Start Methode : MarkAsDealDone");

        DbCommand objCmd = null;
        DbTransaction objTrans = null;
        DbConnection objCon = null;
        try
        {
            objCon = Cls_DataAccess.getInstance().GetConnection();
            objCon.Open();
            objTrans = objCon.BeginTransaction();

            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpMarkAsDealDone");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestID", DbType.Int32, RequestId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int32, DealerId);
            Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
            
            objCmd = null;
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAddPointDealerAfterShortListing");
            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestID", DbType.Int32, RequestId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int32, DealerId);
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

   
}
