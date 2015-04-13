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
using Mechsoft.FleetDeal;
using System.Data.Common;
using System.Data;
using log4net;

/// <summary>
/// Summary description for Cls_State
/// </summary>
public class Cls_State:Cls_CommonProperties
{

    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
    ILog logger = LogManager.GetLogger(typeof(Cls_State));


    #region Properties
    private string _strState;

    public string State
    {
        get { return _strState; }
        set { _strState = value; }
    }

    #endregion
    
	public Cls_State()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region "Methods"
    public DataTable GetAllStates()
    {
        try
        {
            this.SpName = "SpGetAllStates";
            return GetData();
        }
        catch (Exception ex)
        { logger.Error("GetAllStates Fucntion :" + ex.Message); return null; }
    }

    public int AddState()
    {
        try
        {
            this.SpName = "SpAddState";
            return HandleStateMaster();
        }
        catch (Exception ex) { logger.Error("AddState Function :" + ex.Message); return 0; }
    }

    public int UpdateState()
    {
        try
        {
            this.SpName = "SpUpdateState";
            return HandleStateMaster();
        }
        catch (Exception ex)
        { logger.Error("UpdateState Function :"+ex.Message); return 0; }
    }

    public int SetActivenessOfState()
    {
        try
        {
            this.SpName = "SpActivateInactivateState";
            return HandleStateMaster();
        }
        catch (Exception ex) { logger.Error("SetActivenessOfState Function :"+ex.Message); return 0; }

    }

    public DataTable CheckIfStateExists()
    {
        try
        {
            this.SpName = "SpCheckIfStateExists";
            return GetData();
        }
        catch (Exception ex)
        { logger.Error("CheckIfStateExists Functions :"+ex.Message); return null; }
    }
    public DataTable GetData()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, this.SpName);

            setParameters(objCmd);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex) { logger.Error("GetData Function :" + ex.Message); return null; }
    }

    public int HandleStateMaster()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            setParameters(objCmd);

            return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex) { logger.Error("HandleStateMaster Function :" + ex.Message); return 0; }
    }


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
                if (DbOperations.INSERT.Equals(DBOperation) || DbOperations.CHECK_IF_EXIST.Equals(DBOperation))
                    DataAccess.AddInParameter(objCmd, "State", DbType.String, State);

                if (DbOperations.UPDATE.Equals(DBOperation))
                {
                    DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
                    DataAccess.AddInParameter(objCmd, "State", DbType.String, State);
                }
            }


        }
        catch (Exception ex) { logger.Error("setParameters Function :" + ex.Message); }
    }

    public DataTable GetAllActiveStates()
    {
        try
        {
            this.SpName = "SpGetAllActiveStates";
            return GetData();
        }
        catch (Exception ex)
        { logger.Error("GetAllActiveStates Function :"+ex.Message); return null; }
    } 
    #endregion
}
