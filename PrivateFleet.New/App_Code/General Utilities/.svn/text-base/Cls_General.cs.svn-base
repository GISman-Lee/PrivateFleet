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
using log4net;

/// <summary>
/// Summary description for Cls_General
/// </summary>
/// 
namespace Mechsoft.GeneralUtilities
{

    public class Cls_General
    {
        #region Veriables

        static ILog logger = LogManager.GetLogger(typeof(Cls_General));
        public Cls_General()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private DataTable _dtAllowedActions;
        public DataTable AllowedActions
        {
            get { return _dtAllowedActions; }
            set { _dtAllowedActions = value; }
        }


        private string _actionToCheck;
        public string ActionToCheck
        {
            get { return _actionToCheck; }
            set { _actionToCheck = value; }
        }

        //29 sept for SMS sending funcationality.
        public string SMSTo { get; set; }
        public int SMSFrom { get; set; }
        public string SMSText { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string status { get; set; }

        public int RoleID { get; set; }
        public int ReportID { get; set; }
        public int userid { get; set; }
        public int Customerid { get; set; }
        public int flag { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime enddate { get; set; }
        public string Name { get; set; }
        public string keyword { get; set; }
        public int PrimaryContactID { get; set; }

        //on 27 jul 12
        public string XmlDocument { get; set; }

        // 24 Jan 2013
        public int Rating { get; set; }

        #endregion

        public bool CheckForThisAction()
        {
            try
            {
                if (_dtAllowedActions != null)
                {
                    DataView dv = _dtAllowedActions.DefaultView;
                    dv.RowFilter = "Action = '" + _actionToCheck + "'";

                    if (dv.ToTable().Rows.Count > 0)
                    {
                        //action is accessible
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            //action is not accessible
            return false;
        }

        public bool sendSMS()
        {
            DbCommand objCmd = null;
            int result = 0;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSaveSMSDetails");

                //Add parameters
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "SMSTo", DbType.String, SMSTo);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "SMSText", DbType.String, SMSText);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "SMSFrom", DbType.Int32, SMSFrom);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "status", DbType.String, status);

                //Execute command
                result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd, null);

                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                logger.Error("Send SMS Error : " + ex.Message);
                return false;
            }
            finally
            {
                objCmd = null;
            }

        }

        public DataTable GetSMSSummary()
        {
            DataTable dt = null;
            DbCommand objCmd = null;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetReportForSMS");
                //Execute command
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "SMSFrom", DbType.Int32, SMSFrom);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "FromDate", DbType.DateTime, FromDate);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "ToDate", DbType.DateTime, ToDate);
                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch
            {
                throw;
            }
            finally
            {
                objCmd = null;
                dt = null;
            }

        }

        public DataTable getSurveyReport()
        {
            //DataTable dt = null;
            DbCommand objCmd = null;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSurveyGetReport");
                //Execute command
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "RoleID", DbType.Int32, RoleID);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "ReportID", DbType.Int32, ReportID);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "FromDate", DbType.DateTime, fromdate);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "ToDate", DbType.DateTime, enddate);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "Rating", DbType.Int32, Rating);
                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("Survey Rep Error - " + ex.Message);
                return null;
            }
            finally
            {
                objCmd = null;
                //dt = null;
            }

        }

        public DataTable getSurveySendReport()
        {
            //DataTable dt = null;
            DbCommand objCmd = null;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSurveyGetSendReport");
                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("Survey send Rep Error - " + ex.Message);
                return null;
            }
            finally
            {
                objCmd = null;
                //dt = null;
            }

        }

        public DataTable getProspectReport()
        {
            //DataTable dt = null;
            DbCommand objCmd = null;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSurveyGetProspectReport");
                //Execute command
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "FromDate", DbType.DateTime, FromDate);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "ToDate", DbType.DateTime, ToDate);
                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("Survey Rep Error - " + ex.Message);
                return null;
            }
            finally
            {
                objCmd = null;
                //dt = null;
            }

        }

        /// <summary>
        /// returns all Active  Dealers avaialable
        /// </summary>
        /// <returns>Data Table : Containing all available Dealers</returns>
        public DataTable GetAllDealersForSR()
        {
            try
            {
                DbCommand objCmd = null;

                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSurveyGetAllDealers");

                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("GetAllDealers Function :" + ex.Message);
                return null;
            }
        }

        public DataTable GetListof_ServeryCustomer()
        {
            DataTable dt = null;
            DbCommand objCmd = null;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Get_ServeyClientList");
                //Execute command
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "userid", DbType.Int32, userid);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "FromDate", DbType.DateTime, fromdate);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "ToDate", DbType.DateTime, enddate);

                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error(Convert.ToString(ex.Message));
                throw;
            }
            finally
            {
                objCmd = null;
                dt = null;
            }

        }

        public DataTable GET_Servay_By_Customerid()
        {

            DbCommand objCmd = null;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_GetServayDetailByCustomerid");
                //Execute command
                if (Customerid != 0)
                    Cls_DataAccess.getInstance().AddInParameter(objCmd, "customerid", DbType.Int32, Customerid);
                else if (Customerid == 0)
                    Cls_DataAccess.getInstance().AddInParameter(objCmd, "XmlDocument", DbType.Xml, XmlDocument);

                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error(Convert.ToString(ex.Message));
                throw;
            }
            finally
            {
                objCmd = null;

            }

        }

        public DataTable FillRadioBut()
        {
            DbCommand objCmd = null;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSurveyGetRadioAnswer");
                //Execute command
                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("Fill make Error - " + ex.Message);
                return null;
            }
            finally
            {
                objCmd = null;
            }
        }

        public DataTable GetAllMake()
        {
            DataTable dt = null;
            DbCommand objCmd = null;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetActiveMakes");
                //Execute command
                dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("Fill make Error - " + ex.Message);
            }
            finally
            {
                objCmd = null;
            }
            return dt;
        }

        public DataTable GetServey()
        {
            //DataTable dt = null;
            DbCommand objCmd = null;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spSearchServery");
                //Execute command
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "flag", DbType.Int32, flag);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "fromdate", DbType.DateTime, fromdate);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "enddate", DbType.DateTime, enddate);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "name", DbType.String, Name);
                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("Survey Rep Error - " + ex.Message);
                return null;
            }
            finally
            {
                objCmd = null;
                //dt = null;
            }

        }

        public DataTable GetServey_Report2()
        {
            //DataTable dt = null;
            DbCommand objCmd = null;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spSearchServeryByQ3");
                //Execute command
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "flag", DbType.Int32, flag);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "fromdate", DbType.DateTime, fromdate);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "enddate", DbType.DateTime, enddate);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "name", DbType.String, Name);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "PrimaryContactID", DbType.String, PrimaryContactID);
                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("Survey Rep Error - " + ex.Message);
                return null;
            }
            finally
            {
                objCmd = null;
                //dt = null;
            }

        }

        public DataTable GetServey_Report3_ByKeyword()
        {
            //DataTable dt = null;
            DbCommand objCmd = null;
            try
            {
                //Get command object
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spSearchServeryByKeyword");
                //Execute command
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "keyword", DbType.String, keyword);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "fromdate", DbType.DateTime, fromdate);
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "enddate", DbType.DateTime, enddate);
                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("Survey Rep Error - " + ex.Message);
                return null;
            }
            finally
            {
                objCmd = null;
                //dt = null;
            }

        }

        /// <summary>
        /// Created By Archana
        /// Date: 23 Feb 2012
        /// details: get all Primary Contact from Primary Contact Table
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllPrimaryContacts()
        {
            DbCommand objCmd = null;
            try
            {
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetPromaryContacts");
                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("GetAllPrimaryContacts Error - " + ex.Message);
                return null;
            }
            finally
            {
                objCmd = null;
            }
        }

        /// <summary>
        /// Created By Archana
        /// Date: 23 Feb 2012
        /// details: get all Primary Contact from "tbl_VDT_CustomerMaster" Table
        /// </summary>
        /// <returns></returns>
        public DataTable GetDistinctPrimaryContacts()
        {
            DbCommand objCmd = null;
            try
            {
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_GetDistinctPrimaryContact");
                return Cls_DataAccess.getInstance().GetDataTable(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("GetDistinctPrimaryContacts Error - " + ex.Message);
                return null;
            }
            finally
            {
                objCmd = null;
            }
        }


        /// <summary>
        /// Created By Archana
        /// Date: 23 Feb 2012
        /// </summary>
        /// <param name="PrimaryContactID"></param>
        /// <returns></returns>
        public DataSet GetServeyReportForQuestn3(Int64 PrimaryContactID, DateTime _FromDate, DateTime _ToDate)
        {
            DbCommand objCmd = null;
            try
            {
                objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpServeyGetQuestionRpt");
                Cls_DataAccess.getInstance().AddInParameter(objCmd, "PrimaryContactID", DbType.Int64, PrimaryContactID);
                if (_FromDate != DateTime.MinValue || _FromDate != DateTime.MaxValue)
                {
                    Cls_DataAccess.getInstance().AddInParameter(objCmd, "FromDate", DbType.DateTime, _FromDate);
                }
                if (_ToDate != DateTime.MinValue || _ToDate != DateTime.MaxValue)
                {
                    Cls_DataAccess.getInstance().AddInParameter(objCmd, "ToDate", DbType.DateTime, _ToDate);
                }
                return Cls_DataAccess.getInstance().GetDataSet(objCmd);
            }
            catch (Exception ex)
            {
                logger.Error("GetServeyReportForQuestn3 Error - " + ex.Message);
                return null;
            }
            finally
            {
                objCmd = null;
            }
        }


    }
}