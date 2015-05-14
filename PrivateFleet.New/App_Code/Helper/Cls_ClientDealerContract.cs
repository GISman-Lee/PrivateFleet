using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using System.Data;

/// <summary>
/// Summary description for Cls_ClientDealerContract
/// </summary>
public class Cls_ClientDealerContract
{
    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();

	public Cls_ClientDealerContract()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Property
    private string _Customer;
    public string Customer
    {
        get { return _Customer; }
        set { _Customer = value; }
    }

    private string _Email;
    public string Email 
    {
        get { return _Email; }
        set {_Email = value; }
    }

    private string _Company;
    public string Company
    {
        get { return _Company; }
        set { _Email = value; }
    }

    private string _Fax;
    public string Fax
    {
        get { return _Fax; }
        set { _Fax = value; }
    }

    private string _City;
    public string City
    {
        get { return _City; }
        set { _City = value; }
    }

    private string _Address;
    public string Address
    {
        get { return _Address; }
        set { _Address = value; }
    }

    private string _PostCode;
    public string PostCode
    {
        get { return _PostCode; }
        set { _PostCode = value; }
    }

    private string _State;
    public string State
    {
        get { return _State; }
        set { _State = value; }
    }

    private string _Mobile;
    public string Mobile
    {
        get { return _Mobile; }
        set { _Mobile = value; }
    }

    private string _Phone;
    public string Phone
    {
        get { return _Phone; }
        set { _Phone = value; }
    }

    private string _VehicleYear;
    public string VehicleYear
    {
        get { return _VehicleYear; }
        set { _VehicleYear = value; }
    }

    private string _CarMake;
    public string CarMake
    {
        get { return _CarMake; }
        set { _CarMake = value; }
    }

    private string _Model;
    public string Model
    {
        get { return _Model; }
        set { _Model = value; }
    }

    private string _Series;
    public string Series
    {
        get { return _Series;  }
        set { _Series = value; }
    }

    private string _BodyShape;
    public string BodyShape
    {
        get { return _BodyShape; }
        set { _BodyShape = value; }
    }

    private string _FuelType;
    public string FuelType
    {
        get { return _FuelType; }
        set { _FuelType = value; }
    }

    private string _Transimission;
    public string Transimission
    {
        get { return _Transimission; }
        set { _Transimission = value; }
    }

    private string _BodyColor;
    public string BodyColor
    {
        get { return _BodyColor; }
        set { _BodyColor = value; }
    }

    private string _TrimColor;
    public string Trimcolor
    {
        get { return _TrimColor; }
        set { _TrimColor = value; }
    }

    private string _Price;
    public string Price
    {
        get { return _Price; }
        set { _Price = value; }
    }

    private string _Commission;
    public string Commision
    {
        get { return _Commission; }
        set { _Commission = value; }
    }

    private string _MemberNo;
    public string MemberNo
    {
        get { return _MemberNo; }
        set { _MemberNo = value; }
    }

    private string _Supplier;
    public string Supplier
    {
        get { return _Supplier; }
        set { _Supplier = value; }
    }
    #endregion

    //====The below is the TradeInSpecific
    public DataTable CheckIfTradeIn(string ReqID)
    {
        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfTradeIn");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ReqID", DbType.Int64, Convert.ToInt64(ReqID));
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public DataTable SearchTradeInByReqID(string ReqID)
    {
        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchTradeInByReqID");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ReqID", DbType.Int64, Convert.ToInt64(ReqID));
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public void AddTradeInInfo(long ProspectId, string T1Year, string UsedCar, string T1Model, string T1Series
        , string T1BodyShap, string T1FuelType, long T1Odometer, string T1Transmission, string T1BodyColor, string T1TrimColor
        , int T1RegExpMnt, int T1RegExpYear, string LogBooks, string TradeInDesc, string T1OrigValue, string Tradestatus, string Name, string Email, string Phone, string Mobile, string ReqID)
    {
        DbCommand objCmd = null;

        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAddTradeInInfo");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ProspectId", DbType.Int64, ProspectId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1Year", DbType.String, T1Year);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "UsedCar", DbType.String, UsedCar);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1Model", DbType.String, T1Model);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1Series", DbType.String, T1Series);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1BodyShap", DbType.String, T1BodyShap);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1FuelType", DbType.String, T1FuelType);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1Odometer", DbType.Int64, T1Odometer);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1Transmission", DbType.String, T1Transmission);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1BodyColor", DbType.String, T1BodyColor);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1TrimColor", DbType.String, T1TrimColor);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1RegExpMnt", DbType.Int32, T1RegExpMnt);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1RegExpYear", DbType.Int32, T1RegExpYear);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "LogBooks", DbType.String, LogBooks);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "TradeInDesc", DbType.String, TradeInDesc);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "T1OrigValue", DbType.String, T1OrigValue);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Tradestatus", DbType.String, Tradestatus);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Name", DbType.String, Name);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Email", DbType.String, Email);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "phone", DbType.String, Phone);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Mobile", DbType.String, Mobile);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestId", DbType.Int64, Convert.ToInt64(ReqID));

            DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {

        }
    }

    //====The above is the TradeInSpecific
    public DataTable SearchCreditCard(long ProspectId)
    {
        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchCreditCard");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ProspectId", DbType.Int64, Convert.ToInt32(ProspectId));
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public void AddCreditCard(long ProspectId, string CVNumber, string CardNumber, string CardType, DateTime expirydate, string ExpiryMonth, string ExpiryYear, string MemberNo, string Deposit)
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "SpAddCreditCard");
            DataAccess.AddInParameter(objCmd, "ProspectId", DbType.Int64, ProspectId);
            DataAccess.AddInParameter(objCmd, "CVNumber", DbType.String, CVNumber);
            DataAccess.AddInParameter(objCmd, "CardNumber", DbType.String, CardNumber);
            DataAccess.AddInParameter(objCmd, "CardType", DbType.String, CardType);
            DataAccess.AddInParameter(objCmd, "ExpiryDate", DbType.DateTime, expirydate);
            DataAccess.AddInParameter(objCmd, "ExpiryMonth", DbType.String, ExpiryMonth);
            DataAccess.AddInParameter(objCmd, "ExpiryYear", DbType.String, ExpiryYear);
            DataAccess.AddInParameter(objCmd, "MemberNo", DbType.String, MemberNo);
            DataAccess.AddInParameter(objCmd, "Deposit", DbType.Double, Convert.ToDouble(Deposit));

            DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex) { }
    }

    public DataTable SearchPricesByReqIDConsID(string ReqID, string ConsID)
    {
        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetSelectedQuotation");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@RequestId", DbType.Int32, Convert.ToInt32(ReqID));
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ConsultantId", DbType.Int32, Convert.ToInt32(ConsID));
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public DataTable SearchConsultantDeliveryDateByQuoteID(string QuoteID)
    {
        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetQuotationHeaderDetails");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@QuotationID", DbType.Int32, Convert.ToInt32(QuoteID));
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {

        }
        return dt;
    }

    public DataTable SearchRequestParametersByReqID(string ReqID)
    {
        DbCommand objCmd = null;
        DataTable dt = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetRequestParameters");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "requestId", DbType.Int32, Convert.ToInt32(ReqID));
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            
        }
        return dt;
    }

    public DataTable SearchCustomerInfo(string ReqID)
    {
        DataTable dt = new DataTable();
        try
        {
            DbCommand objcmd = null;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchCustomerByReqID"); //sptest
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@ReqID", DbType.Int64, Convert.ToInt64(ReqID));
            dt = Cls_DataAccess.getInstance().GetDataTable(objcmd);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable SearchDealerInfo(string ReqID)
    {
        DataTable dt = new DataTable();
        try
        {
            DbCommand objcmd = null;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchDealerByReqID"); //sptest
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@ReqID", DbType.String, ReqID);
            dt = Cls_DataAccess.getInstance().GetDataTable(objcmd);
            return dt;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable SearchHeaderInfo(string ReqID)
    {
        DataTable dt = new DataTable();
        try
        {
            DbCommand objcmd = null;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchHeaderByReqID");
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@ReqID", DbType.String, ReqID);
            dt = Cls_DataAccess.getInstance().GetDataTable(objcmd);
            return dt;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}