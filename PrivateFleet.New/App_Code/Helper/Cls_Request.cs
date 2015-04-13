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
using System.Text;

/// <summary>
/// Summary description for Cls_Request
/// </summary>
public class Cls_Request : Cls_SeriesMaster
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(Cls_Request));

    #region "Constructor"
    public Cls_Request()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region "Variables and Properties"


    private int _QuotationID;
    public int QuotationID
    {
        get { return _QuotationID; }
        set { _QuotationID = value; }
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


    private int _OptionID;
    public int OptionID
    {
        get { return _OptionID; }
        set { _OptionID = value; }
    }


    private int _UserID;
    public int UserID
    {
        get { return _UserID; }
        set { _UserID = value; }
    }

    private int _SuburbID;
    public int SuburbID
    {
        get { return _SuburbID; }
        set { _SuburbID = value; }
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

    private int _customerId;
    public int CustomerId
    {
        get { return _customerId; }
        set { _customerId = value; }
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

    private int _dealerId;
    public int DealerId
    {
        get { return _dealerId; }
        set { _dealerId = value; }
    }

    private int _points;
    public int Points
    {
        get { return _points; }
        set { _points = value; }
    }

    private string _xmlDocument;
    public string XmlDocument
    {
        get { return _xmlDocument; }
        set { _xmlDocument = value; }
    }

    private string _PCode;
    public string PCode
    {
        get { return _PCode; }
        set { _PCode = value; }
    }

    private string _Series;
    public string Series
    {
        get { return _Series; }
        set { _Series = value; }
    }

    private int _OrderTaken;
    public int OrderTaken
    {
        get { return _OrderTaken; }
        set { _OrderTaken = value; }
    }

    private int _Urgent;
    public int Urgent
    {
        get { return _Urgent; }
        set { _Urgent = value; }
    }

    private int _BuildYear;
    public int BuildYear
    {
        get { return _BuildYear; }
        set { _BuildYear = value; }
    }

    //12 may 12
    public string Email { get; set; }
    public string CustomerEmail { get; set; }
    public string Suburb { get; set; }
    //end

    public int QuoteRequestId { get; set; }
    public int QuoteconConsultantId { get; set; }
    public int ProspectId { get; set; }
    public bool IsQuoteCreated { get; set; }

    #endregion

    #region "Methods"

    // <summary>
    /// Method to save quote request information to database
    /// <para>Expected Properties: SeriesId, ConsultantId, ConsultantNotes, DealerIds, AccessoryIds</para>
    /// </summary>
    public int SaveQuoteRequest()
    {
        logger.Debug("SaveQuoteRequest Method Start");

        DbCommand objCmd = null;
        int result = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSaveQuoteRequest");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "MakeID", DbType.Int32, ID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ModelID", DbType.Int32, ModelID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "SeriesId", DbType.Int32, _seriesId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Series", DbType.String, Series);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "SuburbID", DbType.Int32, SuburbID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "PCode", DbType.String, PCode);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ConsultantId", DbType.Int32, _consultantId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ConsultantNotes", DbType.String, _consultantNotes);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerIds", DbType.String, _dealerIds);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "AccessoryIds", DbType.String, _accessoryIds);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "XmlDocument", DbType.Xml, _xmlDocument);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "OrderTaken", DbType.Int32, OrderTaken);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Urgent", DbType.Int32, Urgent);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "BuildYear", DbType.Int32, BuildYear);

            //Execute command
            object obj = Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null);
            Int32.TryParse(obj.ToString(), out result);

            //DataTable Rdt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            throw;
            //logger.Error("SaveQuoteRequest Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("SaveQuoteRequest Method End");
        }
        return result;
    }

    public int SaveQuoteRequestAtStep1()
    {
        logger.Debug("SaveQuoteRequest at Step 1 Method Start");

        DbCommand objCmd = null;
        int result = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSaveQuoteRequestAtStep1");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestId", DbType.Int32, RequestId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "MakeID", DbType.Int32, ID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ModelID", DbType.Int32, ModelID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "SeriesId", DbType.Int32, _seriesId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Series", DbType.String, Series);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ConsultantId", DbType.Int32, _consultantId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ConsultantNotes", DbType.String, _consultantNotes);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "XmlDocument", DbType.Xml, _xmlDocument);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "OrderTaken", DbType.Int32, OrderTaken);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Urgent", DbType.Int32, Urgent);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "BuildYear", DbType.Int32, BuildYear);

            //Execute command
            object obj = Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null);
            Int32.TryParse(obj.ToString(), out result);

            //DataTable Rdt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("SaveQuoteRequest at step 1 Method Error : " + ex.Message);
            return 0;
        }
        finally
        {
            objCmd = null;
            logger.Debug("SaveQuoteRequest Method End");
        }
        return result;
    }

    public int SaveQuoteRequestAtStep2()
    {
        logger.Debug("SaveQuoteRequest at Step 1 Method Start");

        DbCommand objCmd = null;
        int result = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSaveQuoteRequestAtStep2");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestId", DbType.Int32, RequestId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Suburb", DbType.String, Suburb);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "SuburbID", DbType.Int32, SuburbID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerIds", DbType.String, DealerIds);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "PCode", DbType.String, PCode);

            //Execute command
            object obj = Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null);
            Int32.TryParse(obj.ToString(), out result);

            //DataTable Rdt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("SaveQuoteRequest at step 2 Method Error : " + ex.Message);
            return 0;
        }
        finally
        {
            objCmd = null;
            logger.Debug("SaveQuoteRequest Method End");
        }
        return result;
    }

    public int SaveQuoteRequestAtStep3()
    {
        logger.Debug("SaveQuoteRequest at Step 1 Method Start");

        DbCommand objCmd = null;
        int result = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSaveQuoteRequestAtStep3");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestId", DbType.Int32, RequestId);

            //Execute command
            object obj = Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null);
            Int32.TryParse(obj.ToString(), out result);

        }
        catch (Exception ex)
        {
            logger.Error("SaveQuoteRequest at step 3 Method Error : " + ex.Message);
            return 0;
        }
        finally
        {
            objCmd = null;
            logger.Debug("SaveQuoteRequest Method End");
        }
        return result;
    }

    public int addMoreDealer()
    {
        DbCommand objCmd = null;
        int result = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAddMoreDealer");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@DealerIds", DbType.String, DealerIds);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@RequestId", DbType.Int32, RequestId);

            //Execute command
            result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);

            //DataTable Rdt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("addMoreDealer Method err : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("SaveQuoteRequest Method End");
        }
        return result;
    }

    public int UpdateCustomerEmail()
    {
        DbCommand objCmd = null;
        int result = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.Text, "Update tblRequestHeader Set CustomerEmail='" + CustomerEmail + "', IsQRSend=1, QRSendDate=getdate() Where ID=" + RequestId);
            //Execute command
            result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);

            //DataTable Rdt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("UpdateCustomerEmail Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("SaveQuoteRequest Method End");
        }
        return result;
    }


    // <summary>
    /// Method to get sent quote requests for consultant
    /// <para>Expected Properties: ConsultantId</para>
    /// </summary>
    /// <returns>Datatable containing sent quote requests for consultant</returns>
    public DataTable GetSentQuoteRequests()
    {
        logger.Debug("GetSentQuoteRequests Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetSentQuoteRequests");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "consultantId", DbType.Int32, _consultantId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@FromDate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ToDate", DbType.DateTime, ToDate);
            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetSentQuoteRequests Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetSentQuoteRequests Method End");
        }
        return dt;
    }



    // <summary>
    /// Method to get request header information
    /// <para>Expected Properties: RequestId</para>
    /// </summary>
    /// <returns>Datatable containing request header information</returns>
    public DataTable GetRequestHeaderInfo()
    {
        logger.Debug("GetRequestHeaderInfo Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetRequestHeaderInfo");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "requestId", DbType.Int32, _requestId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetRequestHeaderInfo Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetRequestHeaderInfo Method End");
        }
        return dt;
    }


    /// Get Quotation ID
    /// 
    public int GetQuotationID(string type)
    {
        DbCommand objCmd = null;
        DataTable dt = null;
        string tableName = "";
        if (type == "consultant")
            tableName = "tblRequestHeader";
        else if (type == "dealer")
            tableName = "tblQuotationHeader";
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetQuotationID");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@tableName", DbType.String, tableName);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetRequestAccessories Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetRequestAccessories Method End");
        }
        return Int32.Parse(dt.Rows[0][0].ToString());
    }


    // <summary>
    /// Method to get request accessories information
    /// <para>Expected Properties: RequestId</para>
    /// </summary>
    /// <returns>Datatable containing request accessories information</returns>
    public DataTable GetRequestAccessories()
    {
        logger.Debug("GetRequestAccessories Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetRequestAccessories");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "requestId", DbType.Int32, _requestId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetRequestAccessories Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetRequestAccessories Method End");
        }
        return dt;
    }

    //manoj
    //12 mar 2011
    //Acc. by dealer

    public DataTable GetAccessoriesForDealer()
    {
        logger.Debug("GetAccessoriesForDealer Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spGetAccessoriesForDealer");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "requestId", DbType.Int32, _requestId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "QuotationID", DbType.Int32, _QuotationID);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            throw;
            logger.Error("GetAccessoriesForDealer Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetAccessoriesForDealer Method End");
        }
        return dt;
    }


    // <summary>
    /// Method to get request parameters information
    /// <para>Expected Properties: RequestId</para>
    /// </summary>
    /// <returns>Datatable containing request parameters information</returns>
    public DataTable GetRequestParameters()
    {
        logger.Debug("GetRequestParameters Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetRequestParameters");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "requestId", DbType.Int32, _requestId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetRequestParameters Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetRequestParameters Method End");
        }
        return dt;
    }

    // <summary>
    /// Method to get request dealers information
    /// <para>Expected Properties: RequestId</para>
    /// </summary>
    /// <returns>Datatable containing request dealers information</returns>
    public DataTable GetRequestDealers()
    {
        logger.Debug("GetRequestDealers Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetRequestDealers");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "requestId", DbType.Int32, _requestId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetRequestDealers Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetRequestDealers Method End");
        }
        return dt;
    }

    // <summary>
    /// Method to get Data for creating the Quotation 
    /// <para>Expected Properties: RequestId</para>
    /// </summary>
    /// <returns>Datatable containing Data For creating the quotation</returns>
    public DataTable GetDataForQuotation()
    {
        logger.Debug("GetDataForQuotation Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetDataForQuotation");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "requestId", DbType.Int32, _requestId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetDataForQuotation Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetDataForQuotation Method End");
        }
        return dt;
    }

    public DataTable GetRecievedQuoteRequestsForDealer()
    {
        logger.Debug("GetRecievedQuoteRequestsForDealer Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;

        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpViewRecivedQuoteRequestForDealer");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "UserID", DbType.Int32, UserID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@FromDate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ToDate", DbType.DateTime, ToDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@MakeID", DbType.Int32, MakeID);
            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetRecievedQuoteRequestsForDealer Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetRecievedQuoteRequestsForDealer Method End");
        }
        return dt;
    }

    public string GetConsultantNotesForRequest()
    {
        logger.Debug("GetConsultantNotesForRequest Method Start");

        DbCommand objCmd = null;

        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetConsultantNoteForrequest");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "requestId", DbType.Int32, _requestId);

            //Execute command
            return Convert.ToString(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null));
        }
        catch (Exception ex)
        {

            logger.Error("GetConsultantNotesForRequest Method : " + ex.Message);
            return "Consultant Notes Not Found";
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetConsultantNotesForRequest Method End");
        }

    }

    // <summary>
    /// Method to get request dealers information
    /// <para>Expected Properties: RequestId</para>
    /// </summary>
    /// <returns>Datatable containing request dealers information</returns>
    public int GetQuotationsCountForRequest()
    {
        //logger.Debug("GetQuotationsCountForRequest Method Start");

        DbCommand objCmd = null;
        int count = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetQuotationCountForReq");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "requestId", DbType.Int32, _requestId);

            //Execute command
            count = Convert.ToInt32(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null));
        }
        catch (Exception ex)
        {
            logger.Error("GetQuotationsCountForRequest Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            //logger.Debug("GetQuotationsCountForRequest Method End");
        }
        return count;
    }

    // <summary>
    /// Method to get selected quotation details
    /// </summary>
    /// <returns>Datatable containing selected quotation details</returns>
    public DataTable GetSelectedQuotation()
    {
        logger.Debug("GetSelectedQuotation Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetSelectedQuotation");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "requestId", DbType.Int32, _requestId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "consultantId", DbType.Int32, _consultantId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetSelectedQuotation Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetSelectedQuotation Method End");
        }
        return dt;
    }

    // <summary>
    /// Method to add points to dealer
    /// </summary>
    public int AddPointsToDealer()
    {
        logger.Debug("AddPointsToDealer Method Start");

        DbCommand objCmd = null;
        int result = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAddPointsToDealer");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestId", DbType.Int32, _requestId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerId", DbType.Int32, _dealerId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Points", DbType.Int32, _points);

            //Execute command
            result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            throw;
            logger.Error("AddPointsToDealer Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("AddPointsToDealer Method End");
        }
        return result;
    }

    #endregion



    public DataTable GetOptionOfTheQuotation()
    {
        logger.Debug("GetOptionOfTheQuotation Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetSelectedQuotationInReport");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "QuotationID", DbType.Int32, QuotationID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "OptionID", DbType.Int32, OptionID);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetOptionOfTheQuotation Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetOptionOfTheQuotation Method End");
        }
        return dt;
    }

    public void DeleteQuote()
    {
        DbCommand objCmd = null;
        objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.Text, "Update tblRequestHeader set IsDealerDelete=1 where ID='" + RequestId + "'");
        Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
    }

    public DataTable GetAccBySeries(int RequestId)
    {
        logger.Debug("Get Accessoryes Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        StringBuilder Query = new StringBuilder();
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAccBySeriesId");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@SeriesId", DbType.Int32, RequestId);


            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);

        }
        catch (Exception ex)
        {
            logger.Error("Get Accessories Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("Get Accessoryies Method End");
        }
        return dt;
    }

    public DataTable GetAccessoriesForModel(int ModelId)
    {
        logger.Debug("GetAccessoriesForModel Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        StringBuilder Query = new StringBuilder();
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAccByModelId");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ModelId", DbType.Int32, ModelId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetAccessoriesForModel Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetAccessoriesForModel Method End");
        }
        return dt;
    }

    public DataTable GetOldQuotation()
    {
        logger.Debug("GetOldQuotation Method Start");

        DbCommand objCmd = null;
        DataTable dt = null;
        StringBuilder Query = new StringBuilder();
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetOldQuotation");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@QuotationID", DbType.Int32, QuotationID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@RequestId", DbType.Int32, RequestId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetOldQuotation Method Error : " + ex.Message);
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("GetOldQuotation Method End");
        }
        return dt;
    }

    /// <summary>
    /// 12 may 12
    /// Used to bind the make for perticular dealer
    /// </summary>
    /// <returns></returns>
    public DataTable BindDealerMakes()
    {

        DbCommand objCmd = null;
        DataTable dt = null;
        StringBuilder Query = new StringBuilder();
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetDealerMakes");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Email", DbType.String, Email);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("App code BindDealerMakes Method Error : " + ex.Message);
        }
        finally
        {
            objCmd = null;
        }
        return dt;
    }
    //end 12 may 2012


    //2 July 12
    public DataSet GetAllRequestInfo()
    {
        DbCommand objCmd = null;
        DataSet ds = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllRequestInfo");
            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@requestId", DbType.Int32, RequestId);
            //Execute command
            ds = Cls_DataAccess.getInstance().GetDataSet(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("Get All Request Info err -  " + ex.Message);
        }
        finally
        {
            objCmd = null;
        }
        return ds;
    }
    //end 2 jul 12

    /// <summary>
    /// to cancel Qr by consultant on 6 sept 12
    /// </summary>
    public DataTable CancelQR()
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCancelQuoteRequest");
            //Execute command
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ConsultantID", DbType.Int32, ConsultantId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@RequestId", DbType.Int32, RequestId);

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        { }
    }

    public DataTable IsInCompleteQuote(Cls_Request cls_Request)
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpIsInCompleteQuote");
            //Execute command
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@RequestId", DbType.Int32, RequestId);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception)
        {
            return null;
        }
        finally
        { }
    }

    public int AddProspectMapping()
    {
        DbCommand objCmd = null;
        int result = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "usp_AddProspectMapping");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ID", DbType.Int32, ID);
            if (ProspectId > 0)
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "ProspectId", DbType.Int32, ProspectId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "QuoteconConsultantId", DbType.Int32, QuoteconConsultantId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "QuoteRequestId", DbType.Int32, QuoteRequestId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "IsQuoteCreated", DbType.Boolean, IsQuoteCreated);

            //Execute command
            object obj = Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null);
            Int32.TryParse(obj.ToString(), out result);

            //DataTable Rdt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            throw;
            //logger.Error("SaveQuoteRequest Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("SaveQuoteRequest Method End");
        }
        return result;
    }
    // <summary>
    /// Added By Chetan on 11-01-2014
    /// Method to check wining quote email sent or not
    /// </summary>
    /// <returns>1 for sent 0 for not </returns>
    public int CheckEmailWiningQuoteSent()
    {
        logger.Debug("CheckEmailWiningQuoteSent Method Start");

        DbCommand objCmd = null;
        DataTable dt=null ;
        int IsExist = 0;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "usp_CheckEmailWiningQuoteSent");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestId", DbType.Int32, _requestId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int32, _dealerId);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
            IsExist =Convert.ToInt32( dt.Rows[0]["IsExist"].ToString());
        }
        catch (Exception ex)
        {
            logger.Error("CheckEmailWiningQuoteSent Method : " + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("CheckEmailWiningQuoteSent Method End");
        }
        return IsExist;
    }
}
