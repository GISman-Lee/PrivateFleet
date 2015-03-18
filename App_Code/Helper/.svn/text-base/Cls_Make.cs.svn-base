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
/// Summary description for Cls_MakeHelper
/// </summary>
/// 
namespace Mechsoft.GeneralUtilities
{
    public class Cls_MakeHelper : Cls_CommonProperties
    {

        Cls_DataAccess DataAccess = Cls_DataAccess.getInstance();
        ILog logger = LogManager.GetLogger(typeof(Cls_MakeHelper));

        #region Variables and Properties
        private string _strMake;

        public string Make
        {
            get { return _strMake; }
            set { _strMake = value; }
        }
        #endregion


        public Cls_MakeHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Function

        public DataTable GetAllMakes()
        {
            try
            {
                DbCommand objCmd = null;

                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetMakes");

                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            { logger.Error("GetAllMakes Event :" + ex.Message); return null; }
        }

        public DataTable CheckIfMakeExists()
        {
            try
            {

                DbCommand objCmd = null;

                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfMakeExists");
                setParameters(objCmd);
                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            { logger.Error("CheckIfMakeExists Function :" + ex.Message); return null; }
        }

        public int AddMake()
        {
            try
            {
                this.SpName = "SpAddMake";
                return HandleMakeMaster();
            }
            catch (Exception ex)
            { logger.Error("AddMake Function :"+ex.Message); return 0;}
        }

        public int UpdateMake()
        {
            try
            {
                this.SpName = "SpUpdateMake";
                return HandleMakeMaster();
            }
            catch (Exception ex)
            { logger.Error("UpdateMake Function :" + ex.Message); return 0; }
        }

        public int SetActivenessOfMake()
        {
            try
            {
                this.SpName = "SpActivateInactivateMake";
                return HandleMakeMaster();
            }
            catch (Exception ex)
            { logger.Error("SetActivenessOfMake Function :" + ex.Message); return 0; }
        }

        public int HandleMakeMaster()
        {
            try
            {
                DbCommand objCmd = null;

                objCmd = DataAccess.GetCommand(System.Data.CommandType.StoredProcedure, this.SpName);
                setParameters(objCmd);

                return DataAccess.ExecuteNonQuery(objCmd);
            }
            catch (Exception ex)
            { logger.Error("HandleMakeMaster Function :" + ex.Message); return 0; }
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

                    DataAccess.AddInParameter(objCmd, "Make", DbType.String, Make);

                    if (DbOperations.UPDATE.Equals(DBOperation))
                        DataAccess.AddInParameter(objCmd, "ID", DbType.Int16, ID);
                }


            }
            catch (Exception ex)
            { logger.Error( "setParameters Function :"+ex.Message); }
        }


        public DataTable GetActiveMakes()
        {
            try
            {
                DbCommand objCmd = null;

                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetActiveMakes");

                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex) { logger.Error("GetActiveMakes Function :" + ex.Message); return null; }
        }
        #endregion

    }
}