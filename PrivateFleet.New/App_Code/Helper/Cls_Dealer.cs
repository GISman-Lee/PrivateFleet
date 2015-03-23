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
using System.Data.SqlClient;
using Mechsoft.FleetDeal;
using log4net;
using System.Text;
using System.Collections;

/// <summary>
/// Summary description for Cls_Dealer
/// </summary>
public class Cls_Dealer : Cls_CommonProperties
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(Cls_Dealer));

    public Cls_Dealer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();

    #region "Variables and Properities"


    private string _PostalCode;

    public string PostalCode
    {
        get { return _PostalCode; }
        set { _PostalCode = value; }
    }

    //Added by Amol for search the Dealer against of the state
    private string _StateId;

    public string StateId
    {
        get { return _StateId; }
        set { _StateId = value; }
    }

    private int _CustomerID;
    public int CustomerID
    {
        get { return _CustomerID; }
        set { _CustomerID = value; }
    }
    private int _intMakeID;

    public int MakeID
    {
        get { return _intMakeID; }
        set { _intMakeID = value; }
    }
    private string _strDealer;

    private string _strName;

    public string Name
    {
        get { return _strName; }
        set { _strName = value; }
    }
    private string _strCompany;

    public string Company
    {
        get { return _strCompany; }
        set { _strCompany = value; }
    }
    private string _strEmail;

    public string Email
    {
        get { return _strEmail; }
        set { _strEmail = value; }
    }
    private string _strFax;

    public string Fax
    {
        get { return _strFax; }
        set { _strFax = value; }
    }
    private string _strPhone;

    public string Phone
    {
        get { return _strPhone; }
        set { _strPhone = value; }
    }
    private string _strMobile;
    public string Mobile
    {
        get { return _strMobile; }
        set { _strMobile = value; }
    }

    private int _intLocation;

    public int Location
    {
        get { return _intLocation; }
        set { _intLocation = value; }
    }
    private int _intCity;

    public int City
    {
        get { return _intCity; }
        set { _intCity = value; }
    }
    private string _intState;

    public string State
    {
        get { return _intState; }
        set { _intState = value; }
    }
    private string _strPcode;

    public string Pcode
    {
        get { return _strPcode; }
        set { _strPcode = value; }
    }

    private int _SuburbID;


    public int SuburbID
    {
        get { return _SuburbID; }
        set { _SuburbID = value; }
    }

    private string _strCityIds;
    /// <summary>
    /// City ids as comma separated string
    /// </summary>
    public string CityIds
    {
        get { return _strCityIds; }
        set { _strCityIds = value; }
    }

    private string _strLocationIds;
    /// <summary>
    /// City ids as comma separated string
    /// </summary>
    public string LocationIds
    {
        get { return _strLocationIds; }
        set { _strLocationIds = value; }
    }

    private int _dealerId;
    public int DealerID
    {
        get { return _dealerId; }
        set { _dealerId = value; }
    }

    private bool _isHotDealer;
    public bool IsHotDealer
    {
        get { return _isHotDealer; }
        set { _isHotDealer = value; }
    }

    private bool _isColdDealer;
    public bool IsColdDealer
    {
        get { return _isColdDealer; }
        set { _isColdDealer = value; }
    }

    private long _Radius;
    public long Radius
    {
        get { return _Radius; }
        set { _Radius = value; }
    }


    private string _strdealerId;
    public string VehicleDealerID
    {
        get { return _strdealerId; }
        set { _strdealerId = value; }
    }

    private Int32 _UserID;
    public int UserID
    {
        get { return _UserID; }
        set { _UserID = value; }
    }
    private string _Details;
    public string Details
    {
        get { return _Details; }
        set { _Details = value; }
    }

    private Int32 _Createdby;
    public Int32 Createdby
    {
        get { return _Createdby; }
        set { _Createdby = value; }
    }

    private Int32 _Modifiedby;
    public int Modifiedby
    {
        get { return _Modifiedby; }
        set { _Modifiedby = value; }
    }

    private string _EmailTo;
    public string EmailTo
    {
        get { return _EmailTo; }
        set { _EmailTo = value; }
    }

    //on 5th July 2012
    public string Contact { get; set; }
    // on 8 Oct 2012
    public string Surname { get; set; }
    #endregion

    #region "Methods"
    /// <summary>
    /// returns all Dealers avaialable
    /// </summary>
    /// <returns>Data Table : Containing all available Dealers</returns>
    public DataTable GetAllDealers()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllDealers");  //SpGetAllDealers

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetAllDealers Function :" + ex.Message);
            return null;
        }
    }

    /// <summary>
    /// to Check whether this Dealer already exists
    /// </summary>
    /// <returns> Data Table</returns>
    public DataTable CheckIFDealerExist()
    {

        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfDealerExists");

            setParameters(objCmd);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }

        catch (Exception ex)
        {
            logger.Error("CheckIFDealerExist Function :" + ex.Message);
            return null;
        }
    }

    /// <summary>
    /// to add New Dealer
    /// </summary>
    /// <returns> int : indicating no. of records affected in DB</returns>
    public int AddDealer()
    {
        try
        {
            this.SpName = "SpAddDealer";
            return HandleDealerMaster();
        }
        catch (Exception ex) { logger.Error("AddDealer Function :" + ex.Message); return 0; }
    }

    /// <summary>
    /// to update the existing Dealer
    /// </summary>
    /// <returns> int : indicating no. of records affected in DataBase</returns>
    public int UpdateDealer()
    {
        try
        {
            this.SpName = "SpUpdateDealer";
            return HandleDealerMaster();
        }
        catch (Exception ex)
        { logger.Error("UpdateDealer Function :" + ex.Message); return 0; }
    }

    /// <summary>
    /// Activates or Inactivate the Dealer
    /// </summary>
    /// <returns>int : indicating no. of records affected in DataBase</returns>
    public int SetActivenessOfDealer()
    {
        try
        {
            this.SpName = "SpActivateInactivateDealer";
            return HandleDealerMaster();
        }
        catch (Exception ex) { logger.Error("SetActivenessOfDealer Function :" + ex.Message); return 0; }
    }

    /// <summary>
    /// Handles the respective actions on DB
    /// </summary>
    /// <returns>int : returns the integer to subscriber indicating the no of. rows affected in Data Base</returns>
    public int HandleDealerMaster()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            setParameters(objCmd);

            return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        { logger.Error("HandleDealerMaster Function :" + ex.Message); return 0; }
    }

    /// <summary>
    /// Sets the parameter for respective stored procedure
    /// </summary>
    /// <param name="objCmd">
    /// DBCommand object to which you want to add the parameter
    /// </param>
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
                DataAccess.AddInParameter(objCmd, "Name", DbType.String, Name);
                DataAccess.AddInParameter(objCmd, "Company", DbType.String, Company);
                DataAccess.AddInParameter(objCmd, "Email", DbType.String, Email);
                DataAccess.AddInParameter(objCmd, "Fax", DbType.String, Fax);
                DataAccess.AddInParameter(objCmd, "Phone", DbType.String, Phone);
                DataAccess.AddInParameter(objCmd, "Location", DbType.Int16, Location);
                DataAccess.AddInParameter(objCmd, "City", DbType.Int16, City);
                DataAccess.AddInParameter(objCmd, "State", DbType.String, State);
                DataAccess.AddInParameter(objCmd, "StateId", DbType.String, StateId);
                DataAccess.AddInParameter(objCmd, "PCode", DbType.String, Pcode);


                if (DbOperations.UPDATE.Equals(DBOperation))
                {
                    DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
                }
            }

        }
        catch (Exception ex) { logger.Error("setParameters Function :" + ex.Message); }

    }

    /// <summary>
    /// returns the all cities that belongs to perticular state
    /// </summary>
    /// <returns>DataTable : Containing the all cities of a  state</returns>
    public DataTable GetAllCitiesOfState()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetCitiesOfState");
            DataAccess.AddInParameter(objCmd, "StateID", DbType.Int16, State);

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetAllCitiesOfState Function :" + ex.Message); return null; }
    }

    /// <summary>
    /// returns the all locations that belongs to perticular city
    /// </summary>
    /// <returns>DataTable : Containing the all location of a  city</returns>
    public DataTable GetAllLocationsOfCity()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetLocationsOfCity");
            DataAccess.AddInParameter(objCmd, "CityID", DbType.Int16, City);

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("GetAllLocationsOfCity Function :" + ex); return null; }
    }

    /// <summary>
    /// returns the details of individual dealer 
    /// </summary>
    /// <returns>DataTable : Containing the details of indicidual dealer</returns>
    public DataTable GetDealerDetails()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetDealerDetails");
            DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex) { logger.Error("GetDealerDetails Function :" + ex.Message); return null; }
    }

    /// <summary>
    /// returns the dealers which are nit yet allocated to the make
    /// </summary>
    /// <returns> Data Table </returns>
    public DataTable getActiveAllocatableDealers()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetActiveAllocatableDealers");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "MakeID", DbType.Int16, MakeID);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        { logger.Error("getActiveAllocatableDealers Function :" + ex.Message); return null; }
    }

    /// <summary>
    /// Method to retrieve hot dealers in the system
    /// </summary>
    /// <returns>Datatable containing hot dealers information</returns>
    public DataTable GetHotDealers()
    {
        logger.Debug("Method Start : GetHotDealers");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetHotDealers");

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            throw;
            logger.Error("GetHotDealers Function :" + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("Method End : GetHotDealers");
        }
        return dt;


    }

    /// <summary>
    /// Method to retrieve locations in selected cities
    /// Expected Properties: CityIds as comma separated string
    /// </summary>
    /// <returns>DataTable containing locations in selected cities</returns>
    public DataTable GetLocationsInCities()
    {
        //logger.Debug("Method Start : GetLocationsInCities");
        DataTable dt = null;
        //DbCommand objCmd = null;
        //try
        //{
        //    //Get command object
        //    objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetLocationsInCities");

        //    //Add Parameters
        //    Cls_DataAccess.getInstance().AddInParameter(objCmd, "CityIds", DbType.String, _strCityIds);

        //    //Execute command
        //    dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        //}
        //catch (Exception)
        //{
        //    throw;
        //}
        //finally
        //{
        //    objCmd = null;
        //    logger.Debug("Method End : GetLocationsInCities");
        //}
        return dt;
    }

    /// <summary>
    /// Method to search dealers in selected locations
    /// </summary>
    /// <returns>Datatable containing dealers in selected locations</returns>
    public DataTable SearchDealersInCities()
    {
        logger.Debug("Method Start : SearchDealersInCities");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchDealers");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "CityIds", DbType.String, _strCityIds);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "LocationIds", DbType.String, string.Empty);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "SearchCriteria", DbType.Int32, 0);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("Method End : SearchDealersInCities");
        }
        return dt;
    }

    /// <summary>
    /// Method to search dealers in selected locations
    /// </summary>
    /// <returns>Datatable containing dealers in selected locations</returns>
    public DataTable SearchDealersInLocations()
    {
        logger.Debug("Method Start : SearchDealers");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchDealers");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "CityIds", DbType.String, string.Empty);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "LocationIds", DbType.String, _strLocationIds);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "SearchCriteria", DbType.Int32, 1);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("Method End : SearchDealers");
        }
        return dt;
    }

    /// <summary>
    /// Method to search dealers for particular make in selected locations 
    /// </summary>
    /// <returns>Datatable containing dealers in selected locations</returns>
    public DataTable SearchDealersForMakeInCities()
    {
        logger.Debug("Method Start : SearchDealersForMakeInCities");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchDealersForMake");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@MakeID", DbType.Int32, _intMakeID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@PostalCode", DbType.String, PostalCode);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);

        }
        catch (Exception ex)
        {
            logger.Error("Search dealer for make err- " + ex.Message);
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("Method End : SearchDealersForMakeInCities");
        }
        return dt;
    }

    /// <summary>
    /// Method to search dealers for particular make in selected locations  om 9 jul 12 for cold dealer
    /// </summary>
    /// <returns>Datatable containing dealers in selected locations</returns>
    public DataTable SearchDealersForMakeCompany()
    {
        logger.Debug("Method Start : SearchDealersForMakeInCities");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchDealersForMakeCompany");

            if (_intMakeID != 0)
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "@MakeID", DbType.Int32, _intMakeID);
            if (!PostalCode.Equals(String.Empty))
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "@PostalCode", DbType.String, PostalCode);
            if (!Company.Equals(String.Empty))
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Company", DbType.String, Company);
            if (!Contact.Equals(String.Empty))
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Contact", DbType.String, Contact);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);

        }
        catch (Exception ex)
        {
            logger.Error("Search dealer for make err- " + ex.Message);
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("Method End : SearchDealersForMakeInCities");
        }
        return dt;
    }

    /// <summary>
    /// Method to search dealers for particular make in selected locations
    /// </summary>
    /// <returns>Datatable containing dealers in selected locations</returns>
    public DataTable SearchDealersForMakeInLocations()
    {
        logger.Debug("Method Start : SearchDealersForMakeInLocations");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchDealersForMake");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "CityIds", DbType.String, string.Empty);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "LocationIds", DbType.String, _strLocationIds);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "SearchCriteria", DbType.Int32, 1);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "MakeID", DbType.Int32, _intMakeID);

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("Method End : SearchDealersForMakeInLocations");
        }
        return dt;
    }


    /// <summary>
    /// to add Vehicle enquiry related information
    /// </summary>
    /// <returns> int : indicating no. of records affected in DB</returns>
    public int AddVehicleEnquiry()
    {
        try
        {
            this.SpName = "AddVehicalEnquire";
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            DataAccess.AddInParameter(objCmd, "DealerID", DbType.String, VehicleDealerID);
            DataAccess.AddInParameter(objCmd, "UserID", DbType.Int32, UserID);
            DataAccess.AddInParameter(objCmd, "Details", DbType.String, Details);
            DataAccess.AddInParameter(objCmd, "Createdby", DbType.Int32, Createdby);
            DataAccess.AddInParameter(objCmd, "Modifiedby", DbType.Int32, Modifiedby);

            return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex) { logger.Error("AddDealer Function :" + ex.Message); return 0; }
    }

    /// <summary>
    /// to add Vehicle enquiry related information
    /// </summary>
    /// <returns> int : indicating no. of records affected in DB</returns>
    public int AddFinanceReferral()
    {
        try
        {
            this.SpName = "AddFinReferral";
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            DataAccess.AddInParameter(objCmd, "UserID", DbType.Int32, UserID);
            DataAccess.AddInParameter(objCmd, "Surname", DbType.String, Surname);
            DataAccess.AddInParameter(objCmd, "Phone", DbType.String, Phone);
            DataAccess.AddInParameter(objCmd, "Details", DbType.String, Details);
            DataAccess.AddInParameter(objCmd, "EmailTo", DbType.String, EmailTo);
            DataAccess.AddInParameter(objCmd, "Createdby", DbType.Int32, Createdby);
            DataAccess.AddInParameter(objCmd, "Modifiedby", DbType.Int32, Modifiedby);

            return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex) { logger.Error("AddDealer Function :" + ex.Message); return 0; }
    }

    public int MarkDealerAsHotOrNormal()
    {
        logger.Debug("Method Start : MarkDealerAsHotOrNormal");
        int result = 0;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpMarkDealerAsHotOrNormal");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int32, _dealerId);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "IsHotDealer", DbType.Boolean, _isHotDealer);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "IsColdDealer", DbType.Boolean, _isColdDealer);
            //Execute command
            result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objCmd = null;
            logger.Debug("Method End : MarkDealerAsHotOrNormal");
        }
        return result;
    }

    /// <summary>
    /// Created By Archana
    /// Date: 22 March 2012
    /// Details : Get All distinct companies from Dealer Master
    /// </summary>
    /// <returns></returns>
    public DataTable GetAllCompanies_VDT()
    {

        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_VDT_GetAllCompanies");

            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("Method End : GetAllCompanies_VDT" + ex.Message);
        }
        finally
        {
            objCmd = null;
            logger.Debug("Method End : GetAllCompanies_VDT");
        }
        return dt;
    }
    #endregion


    public DataTable GetAllMakesNormalDealers()
    {

        logger.Debug("Method Start : GetAllMakesNormalDealers");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllMakesAndAssociatedNormalDealers");


            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            objCmd = null;
            logger.Debug("Method End : GetAllMakesNormalDealers");
        }


    }

    public DataTable GetAllMakesHotDealers()
    {

        logger.Debug("Method Start : GetAllMakesHotDealers");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllMakesAndAssociatedHotDealers");


            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            objCmd = null;
            logger.Debug("Method End : GetAllMakesHotDealers");
        }


    }

    #region Code for search dealer
    /// <summary>
    /// Code added by Amol for searching dealers
    /// </summary>
    /// <returns>Data Table return matching dealers records.</returns>
    public DataTable SearchDealer(string[] values, int int_count)
    {
        StringBuilder query = new StringBuilder();
        DataTable dt = new DataTable();
        try
        {
            DbCommand objcmd = null;
            objcmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "MilesTest"); //sptest
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@name", DbType.String, values[0].Trim());
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Company", DbType.String, values[1].Trim());
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Email", DbType.String, values[2].Trim());
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Phone", DbType.String, values[3].Trim());
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Fax", DbType.String, values[4].Trim());
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@State", DbType.String, values[5].Trim());
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@City", DbType.String, values[6].Trim());
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Location", DbType.String, values[7].Trim());
            Cls_DataAccess.getInstance().AddInParameter(objcmd, "@Pcode", DbType.String, values[8].Trim());
            dt = Cls_DataAccess.getInstance().GetDataTable(objcmd);
            return dt;

        }
        catch (Exception ex)
        {
            logger.Error("Search dealer Function :" + ex.Message);
            return null;
        }
    }

    #endregion

}
