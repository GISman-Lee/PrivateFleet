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
/// Summary description for Cls_CustomerMaster
/// </summary>
public class Cls_CustomerMaster : Cls_CommonProperties
{

    ILog logger = LogManager.GetLogger(typeof(Cls_CustomerMaster));
    private string _FirstName;
    public string FirstName
    {
        get { return _FirstName; }
        set { _FirstName = value; }
    }

    private string _MiddleName;
    public string MiddleName
    {
        get { return _MiddleName; }
        set { _MiddleName = value; }
    }

    private string _LastName;
    public string LastName
    {
        get { return _LastName; }
        set { _LastName = value; }
    }

    private string _Address;
    public string Address
    {
        get { return _Address; }
        set { _Address = value; }
    }

    private string _PostalCode;
    public string PostalCode
    {
        get { return _PostalCode; }
        set { _PostalCode = value; }
    }

    private string _PhoneNo;
    public string PhoneNo
    {
        get { return _PhoneNo; }
        set { _PhoneNo = value; }
    }

    private string _Email;
    public string Email
    {
        get { return _Email; }
        set { _Email = value; }
    }

    private int _CreatedBy;
    public int CreatedBy
    {
        get { return _CreatedBy; }
        set { _CreatedBy = value; }
    }

    private int _LocationID;
    public int LocationID
    {
        get { return _LocationID; }
        set { _LocationID = value; }
    }




    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();

    public Cls_CustomerMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public DataTable GetAllCustomers()
    {
        logger.Debug("GetAllCustomers Methode  Start :");
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllCustomer");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetAllCustomers Function :" + ex.Message); return null; }
        finally { logger.Debug("GetAllCustomers Methode End"); }
    }

    public DataTable GetAllActiveCustomer()
    {
        logger.Debug("GetAllActiveCustomer Methode Start");

        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllActiveCustomer");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetAllActiveCustomer Event :" + ex.Message); return null; }
        finally { logger.Debug("GetAllActiveCustomer Methode End"); }
    }

    public DataTable CheckIfCustomerExists()
    {
        logger.Debug("CheckIfCustomerExists Methode Start");
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfCustomerExists");

            DataAccess.AddInParameter(objCmd, "FirstName", DbType.String, FirstName);
            DataAccess.AddInParameter(objCmd, "MiddleName", DbType.String, MiddleName);
            DataAccess.AddInParameter(objCmd, "LastName", DbType.String, LastName);
            DataAccess.AddInParameter(objCmd, "PostalCode", DbType.String, PostalCode);


            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("CheckIfCustomerExists Function :" + ex.Message); return null; }
        finally { logger.Debug("CheckIfCustomerExists Methode End"); }
    }


    public int AddCustomer()
    {
        try
        {
            this.SpName = "SpAddCustomer";
            return HandleCustomerMaster();
        }
        catch (Exception ex)
        { logger.Error("AddCustomer Function :" + ex.Message); return 0; }
    }

    public int UpdateCustomer()
    {
        try
        {
            this.SpName = "SpUpdateCustomer";
            return HandleCustomerMaster();
        }
        catch (Exception ex)
        { logger.Error("UpdateCustomer Function :" + ex.Message); return 0; }
    }

    public int SetActivenessOfCustomer()
    {
        try
        {
            this.SpName = "SpSetActiveOrDeactivenessOfCustomer";
            return HandleCustomerMaster();
        }
        catch (Exception ex)
        { logger.Error("SetActivenessOfCustomer Function :" + ex.Message); return 0; }
    }

    public int HandleCustomerMaster()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            setParameters(objCmd);

            return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex) { logger.Error("HandleCustomerMaster Function :" + ex.Message); return 0; }
    }
    private void setParameters(DbCommand objCmd)
    {
        try
        {
            if (DbOperations.CHANGE_ACTIVENESS.Equals(DBOperation))
            {
                DataAccess.AddInParameter(objCmd, "IsActive", DbType.Boolean, IsActive);
                DataAccess.AddInParameter(objCmd, "CustID", DbType.Int16, ID);
            }
            else
            {

                DataAccess.AddInParameter(objCmd, "FirstName", DbType.String, FirstName);
                DataAccess.AddInParameter(objCmd, "MiddleName", DbType.String, MiddleName);
                DataAccess.AddInParameter(objCmd, "LastName", DbType.String, LastName);
                DataAccess.AddInParameter(objCmd, "Address", DbType.String, Address);
                DataAccess.AddInParameter(objCmd, "PostalCode", DbType.String, PostalCode);
                DataAccess.AddInParameter(objCmd, "PhoneNo", DbType.String, PhoneNo);
                DataAccess.AddInParameter(objCmd, "Email", DbType.String, Email);
                DataAccess.AddInParameter(objCmd, "LocationID", DbType.Int16, LocationID);
                if (DbOperations.INSERT.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "CreatedBy", DbType.Int16, CreatedBy);


                if (DbOperations.UPDATE.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "CustID", DbType.Int16, ID);
            }


        }
        catch (Exception ex) { logger.Error("setParameters Function :" + ex.Message); }
    }

    public DataTable GetCustomerDetails()
    {

        logger.Debug("GetCustomerDetails Methode Start");

        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetCustomerDetails");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "CustID", DbType.Int16, ID);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetCustomerDetails Event :" + ex.Message); return null; }
        finally { logger.Debug("GetCustomerDetails Methode End"); }

    }

    public DataTable GetSuburbsOfThePostalCode()
    {
        logger.Debug("Start Of GetSuburbsOfThePostalCode");
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetLocationsOfPostalCode");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "PostalCode", DbType.String, PostalCode);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetSuburbsOfThePostalCode Function :" + ex.Message); return null; }
    }

    public DataTable GetCustomerBasicInfo()
    {
        logger.Debug("Method Start: GetCustomerBasicInfo");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetCustomerBasicInfo");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "CustomerID", DbType.Int32, ID);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End: GetCustomerBasicInfo");
            objCmd = null;
        }
        return dt;
    }

    public DataTable GetSecondaryStates()
    {
        logger.Debug("Method Start: GetSecondaryStates");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            ApplyStatePostalCodeRule();

            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetSecondaryStates");

            //Add parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "PostalCode", DbType.String, (PostalCode.Substring(0,1) +"%"));
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Rule", DbType.String, DBOperation);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End: GetSecondaryStates");
            objCmd = null;
        }
        return dt;
    }

    private void ApplyStatePostalCodeRule()
    {
        int Pcode = Convert.ToInt16(PostalCode);
        if ((Pcode >= 2600 && Pcode < 2618) || PostalCode.StartsWith("29"))
        {
            DBOperation = DbOperations.StateSelectionRULE1;
            return;
        }

        if ((Pcode < 2600 && Pcode >= 2618) && PostalCode.StartsWith("29"))
        {
            DBOperation = DbOperations.StateSelectionRULE2;
            return;
        }

        if (PostalCode.StartsWith("08") || PostalCode.StartsWith("09"))
        {
            DBOperation = DbOperations.StateSelectionRULE3;
            return;
        }
        else
        {
            DBOperation = DbOperations.StateSelectionCommonRule;
        }

        

    }
}
