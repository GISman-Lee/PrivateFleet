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
/// Summary description for Cls_MakeDealer
/// </summary>
public class Cls_MakeDealer : Cls_CommonProperties
{

    Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
    ILog logger = LogManager.GetLogger(typeof(Cls_MakeDealer));

    #region Properties
    private int _intMakeID;

    public int MakeID
    {
        get { return _intMakeID; }
        set { _intMakeID = value; }
    }


    private int _intDealerID;

    public int DealerID
    {
        get { return _intDealerID; }
        set { _intDealerID = value; }
    }

    #endregion
    public Cls_MakeDealer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Functions
    public DataTable getAllDealersOfMake()
    {

        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, "SpGetAllMakeDealers");
            DataAccess.AddInParameter(objCmd, "MakeID", DbType.Int16, MakeID);
            return DataAccess.GetDataTable(objCmd);
        }
        catch (Exception ex) { logger.Error("getAllDealersOfMake Function :" + ex.Message); return null; }
    }
    public int AddMakeDealer()
    {
        try
        {
            this.SpName = "spAddMakeDealer";
            return HandleMakeDealerMaster();
        }
        catch (Exception ex)
        { logger.Error("AddMakeDealer Fucntion :" + ex.Message); return 0; }
    }

    public int UpdateMakeDealer()
    {
        try
        {
            this.SpName = "SpUpdateMakeDealer";
            return HandleMakeDealerMaster();
        }
        catch (Exception ex)
        { logger.Error("UpdateMakeDealer Function :"+ex.Message); return 0; }
    }

    public int setActivenessOfMakeDealer()
    {
        try
        {
            this.SpName = "SpActivateInactivateMakeDealer";
            return HandleMakeDealerMaster();
        }
        catch (Exception ex) { logger.Error("setActivenessOfMakeDealer Function :"+ex.Message); return 0; }
    }

    public int HandleMakeDealerMaster()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
            setParameters(objCmd);

            return DataAccess.ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("HandleMakeDealerMaster Function :"+ex.Message);
            return 0;
        }
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
                DataAccess.AddInParameter(objCmd, "DealerID", DbType.Int16, DealerID);
                DataAccess.AddInParameter(objCmd, "MakeID", DbType.Int16, MakeID);

                if (DbOperations.UPDATE.Equals(DBOperation))
                {
                    DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("setParameters Function :"+ex.Message);
        }
    }




    #endregion

    
}
