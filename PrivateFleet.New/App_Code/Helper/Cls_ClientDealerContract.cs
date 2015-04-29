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

    private int HandleDealerMaster()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "");
            setParametersAdd(objCmd);

            return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        { return 0; }
    }

    private void setParametersAdd(DbCommand objCmd)
    {
        try
        {
                DataAccess.AddInParameter(objCmd, "Customer", DbType.String, this.Customer);
                DataAccess.AddInParameter(objCmd, "Company", DbType.String, this.Company);
                DataAccess.AddInParameter(objCmd, "Email", DbType.String, this.Email);
                DataAccess.AddInParameter(objCmd, "Fax", DbType.String, this.Fax);
                DataAccess.AddInParameter(objCmd, "Phone", DbType.String, this.Phone);
                DataAccess.AddInParameter(objCmd, "Mobile", DbType.String, this.Mobile);
                DataAccess.AddInParameter(objCmd, "Address", DbType.String, this.Address);
                DataAccess.AddInParameter(objCmd, "City", DbType.String, this.City);
                DataAccess.AddInParameter(objCmd, "State", DbType.String, State);
                DataAccess.AddInParameter(objCmd, "PostCode", DbType.String, this.PostCode);
                DataAccess.AddInParameter(objCmd, "VehicleYear", DbType.String, this.VehicleYear);
                DataAccess.AddInParameter(objCmd, "Make", DbType.String, this.CarMake);
                DataAccess.AddInParameter(objCmd, "MakeID", DbType.String, "");  //TODO MakeID
                DataAccess.AddInParameter(objCmd, "Model", DbType.String, this.Model);  //TODO Model
                DataAccess.AddInParameter(objCmd, "Series", DbType.String, this.Series);  //TODO MakeID
                DataAccess.AddInParameter(objCmd, "Model", DbType.String, this.Model);  //TODO Model
        }
        catch (Exception ex) { }

    }
}