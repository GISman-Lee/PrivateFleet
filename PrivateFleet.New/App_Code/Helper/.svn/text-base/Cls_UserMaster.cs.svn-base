using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AccessControlUnit;
using log4net;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
/// <summary>
/// Summary description for Cls_User
/// </summary>
public class Cls_UserMaster : AccessControlUnit.Cls_User
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(Cls_UserMaster));

    #region Variables and Properties
    private string _DealerName;
    public string DealerName
    {
        get { return _DealerName; }
        set { _DealerName = value; }
    }
    private int _DealerID;
    public int DealerID
    {
        get { return _DealerID; }
        set { _DealerID = value; }
    }

    private int _ConsultantID;
    public int ConsultantID
    {
        get { return _ConsultantID; }
        set { _ConsultantID = value; }
    }

    private int _RequestID;
    public int RequestID
    {
        get { return _RequestID; }
        set { _RequestID = value; }
    }

    private int _Reminder;
    public int Reminder
    {
        get { return _Reminder; }
        set { _Reminder = value; }
    }

    public string Extension { get; set; }
    public string Mobile { get; set; }

    #endregion

    #region "Methods"

    ///<summary>
    ///     This Method add new user to data base
    ///<![CDATA[Username]]>
    /// <![CDATA[Email]]>
    ///</summary>
    public int Add()
    {
        logger.Debug("Method Start: Add");

        DbCommand objCmd = null;
        DbTransaction objTrans = null;
        DbConnection objConn = null;
        try
        {
            //Get database connection object
            objConn = Cls_DataAccess.getInstance().GetConnection();

            //open database connection
            objConn.Open();

            //initialize transaction
            objTrans = objConn.BeginTransaction();

            #region "Save User Details"
            //Get command object to save user details
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_acu_saveUser");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "username", DbType.String, Username);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "password", DbType.String, Password);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "name", DbType.String, Name.Trim());
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "email", DbType.String, Email);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "phone", DbType.String, Phone);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "address", DbType.String, Address);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "usernameExpiryDate", DbType.DateTime, UsernameExpiryDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "isActive", DbType.Boolean, IsActive);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Extension", DbType.String, Extension);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Mobile", DbType.String, Mobile);
            //Execute command
            int UserID = Convert.ToInt32(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, objTrans));
            #endregion

            #region "Save User-Role Details"
            objCmd = null;

            //Get command object to save user-role details
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_acu_saveUserRoleDetails");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "userId", DbType.Int32, UserID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "roleId", DbType.Int32, RoleID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "isActive", DbType.Boolean, IsActive);

            //Execute command
            int lastInsertId = Convert.ToInt32(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, objTrans));
            #endregion

            #region "Save User-Dealer Details"
            objCmd = null;
            //Get command object to save user-dealer details
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAddDealerUser");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "UserID", DbType.Int32, UserID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int32, DealerID);

            //Execute command
            lastInsertId = Convert.ToInt32(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, objTrans));
            #endregion

            if (lastInsertId > 0)
            {
                //commit transaction on success
                objTrans.Commit();
            }
            else
            {
                //rollback trasaction on failure
                objTrans.Rollback();
            }
            return lastInsertId;
        }
        catch (Exception ex)
        {
            //Rollback transaction if exception occurred
            objTrans.Rollback();

            logger.Error("Add Method : " + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("Method End: Add");
            objCmd = null;

            //Close database connection
            objConn.Close();
        }
        return 0;
    }

    /// <summary>
    /// Method to update user details
    /// </summary>
    /// <returns>Returns true on success else returns false</returns>
    public virtual bool Update()
    {
        logger.Debug("Method Start: Update");

        DbCommand objCmd = null;
        DbTransaction objTrans = null;
        DbConnection objConn = null;
        try
        {
            //Get command object
            objConn = Cls_DataAccess.getInstance().GetConnection();
            objConn.Open();
            objTrans = objConn.BeginTransaction();

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_acu_updateUser");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "id", DbType.Int32, Id);
            //Cls_DataAccess.getInstance().AddInParameter(objCmd, "UserName", DbType.String, _username);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "password", DbType.String, Password);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "name", DbType.String, Name);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "email", DbType.String, Email);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "phone", DbType.String, Phone);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "address", DbType.String, Address);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "usernameExpiryDate", DbType.DateTime, UsernameExpiryDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "isActive", DbType.Boolean, IsActive);

            //Execute command
            int result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd, objTrans);

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_acu_updateRoleOfUser");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "userId", DbType.Int32, Id);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "roleId", DbType.Int32, RoleID);

            //Execute command
            result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd, objTrans);


            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpUpdateDealerUser");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "UserID", DbType.Int32, Id);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int32, DealerID);

            //Execute command
            result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd, objTrans);
            if (result > 0)
            {
                objTrans.Commit();
                return true;
            }
            else
            {
                objTrans.Rollback();
                return false;
            }




            //else
            //{
            //    objTrans.Rollback();
            //    return false;
            //}
        }
        catch
        {
            objTrans.Rollback();
            return false;
            throw;
        }
        finally
        {
            logger.Debug("Method End: Update");
            objCmd = null;
            objConn.Close();
        }
        return false;
    }

    /// <summary>
    /// returns all Active  Dealers avaialable
    /// </summary>
    /// <returns>Data Table : Containing all available Dealers</returns>
    public DataTable GetAllActiveDealers()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllActiveDealers");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetAllDealers Function :" + ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Created By : Archana : 28 March 2012
    /// returns all Active  Dealers Available with Company
    /// </summary>
    /// <returns>Data Table : Containing all available Dealers</returns>
    public DataTable GetAllActiveDealersWithCompany()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllActiveDealersWithCompany");

            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetAllDealers Function :" + ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Gives you the deatils of perticular user
    /// </summary>
    /// <param name="UserID"></param>
    /// <returns>
    ///     Data Table : Containig User Specific Details
    /// </returns>
    public DataTable GetUserDetails(int UserID)
    {
        logger.Debug("Method Start: GetUserDetails");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetUserDetails");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "UserID", DbType.Int16, UserID);
            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End: GetUserDetails");
            objCmd = null;
        }
        return dt;
    }

    public int getConsultantID()
    {
        logger.Debug("Method Start: GetConsultantID");
        DataTable dt = new DataTable();
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetConsultantID");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestID", DbType.Int16, RequestID);
            //Execute command

            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Debug("Method Error : GetConsultantID - " + ex.Message);
        }
        finally
        {
            logger.Debug("Method Ends: GetConsultantID");
        }
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetConsultantBasicInfo()
    {
        logger.Debug("Method Start: GetConsultantBasicInfo");
        DataTable dt = null;
        DataTable dt1 = new DataTable();
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetConsultantBasicInfo");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ConsultantID", DbType.Int16, ConsultantID);
            //Execute command

            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);


            dt1.Columns.Add("Header");
            dt1.Columns.Add("Details");

            DataRow dRow = null;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dParam in dt.Rows)
                {
                    dRow = dt1.NewRow();
                    string s1 = dParam["Header"].ToString();
                    string s2 = dParam["Details"].ToString();
                    dRow["Header"] = dParam["Header"].ToString();
                    if (dParam["Details"].ToString() == "")
                    {
                        dRow["Details"] = "-";
                    }
                    else
                    {
                        dRow["Details"] = dParam["Details"].ToString();
                    }
                    dt1.Rows.Add(dRow);
                }
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End: GetConsultantBasicInfo");
            objCmd = null;
        }

        return dt1;
        //return dt;
    }

    //by manoj on 16 Mar 2011 for reminder mail info
    public DataTable GetReminderInfo()
    {
        logger.Debug("Method Start: GetReminderInfo");

        DataTable dt = new DataTable();
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetReminderInfo");

            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestID", DbType.Int32, RequestID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int32, DealerID);
            //Execute command

            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);

        }
        catch
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End: GetReminderInfo");
            objCmd = null;
        }
        return dt;
    }

    public DataTable GetDealerInfo()
    {
        logger.Debug("Method Start: GetDealerInfo");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SP_DealerInfo");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@RequestId", DbType.Int16, RequestID);
            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End: GetConsultantBasicInfo");
            objCmd = null;
        }
        return dt;
    }

    public DataTable GetAllConsultants()
    {
        logger.Debug("Method Start: GetAllConsultants");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAllConsultant");
            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End: GetAllConsultants");
            objCmd = null;
        }
        return dt;
    }
    #endregion

    public DataTable GetDealerdata()
    {
        try
        {
            string DName = DealerName.Substring(0, DealerName.IndexOf("("));
            DataTable dt = null;
            DbCommand objCmd = null;
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "Sp_GetDealerData");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@DealerName", DbType.String, DName);
            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
            return dt;
        }
        catch (Exception ex)
        { return null; }
    }

    /// <summary>
    /// By Manoj
    /// Method to auto generate character Pasword.
    /// </summary>

    public string generatePassword()
    {
        try
        {
            Guid g = Guid.NewGuid();
            string pass = Convert.ToBase64String(g.ToByteArray());
            pass = pass.Replace("=", "");
            pass = pass.Replace("+", "");
            pass = pass.Replace("/", "");
            pass = pass.Substring(0, 8);

            Password = pass;
            bool chk = CheckPasswordExists();

            if (chk)
                return pass;
            else
            {
                pass = generatePassword();
                return pass;
            }
        }
        catch
        { throw; }
    }

    /// <summary>
    /// 5 Jan 2011 (manoj)
    /// Method to check if password exists already in DB.
    /// </summary>
    public bool CheckPasswordExists()
    {
        logger.Debug("Method Start: CheckPasswordExists");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpCheckIfPasswordExists");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Password", DbType.String, Password);
            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
            if (dt.Rows.Count > 0)
                return false;
            else
                return true;

        }
        catch
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End: CheckPasswordExists");
            objCmd = null;
            dt = null;
        }
    }

    public string getMake()
    {
        logger.Debug("Method Start: getMakeFor sending mail");
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetMakesForDealer");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@DealerID", DbType.Int32, DealerID);
            //Execute command
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
            return dt.Rows[0][0].ToString();
        }
        catch (Exception ex)
        {
            logger.Debug(ex.Message.ToString());
            throw;
        }
        finally
        {
            logger.Debug("Method End: getMakeFor sending mail");
            objCmd = null;
            dt = null;
        }
    }

    public DataTable SearchUser()
    {
        logger.Debug("Method Start: Search User");

        DbCommand objCmd = null;
        try
        {
            string RID = RoleID.ToString();

            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSearchSpecificUser");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@RoleID", DbType.String, RID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@UserName", DbType.String, Name.Trim());
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Email", DbType.String, Email.Trim());
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Phone", DbType.String, Phone.Trim());
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Address", DbType.String, Address.Trim());
            //Execute command
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Debug(ex.Message.ToString());
            throw;
        }
        finally
        {
            logger.Debug("Method End: Search User");
            objCmd = null;

        }
    }


    ///<summary>
    ///     This Method add new user to data base
    ///<![CDATA[Username]]>
    /// <![CDATA[Email]]>
    ///</summary>
    public int updateReminder()
    {
        logger.Debug("Method Start: updateReminder");

        DbCommand objCmd = null;

        try
        {
            #region "Save Reminder Details"
            //Get command object to save user details
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "spUpdateReminderDetails");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "RequestID", DbType.Int32, RequestID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "DealerID", DbType.Int32, DealerID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Reminder", DbType.Int32, Reminder);


            //Execute command
            int UserID = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
            #endregion

            return UserID;
        }
        catch (Exception ex)
        {
            logger.Error("Add Method : " + ex.Message);
            throw;
        }
        finally
        {
            logger.Debug("Method End: updateReminder");
            objCmd = null;

        }
        return 0;

    }

    public virtual int AddAdminConsultant()
    {
        logger.Debug("Method Start: Add");

        DbCommand objCmd = null;
        DbTransaction objTrans = null;
        DbConnection objConn = null;
        try
        {
            //Get database connection object
            objConn = Cls_DataAccess.getInstance().GetConnection();

            //open database connection
            objConn.Open();

            //initialize transaction
            objTrans = objConn.BeginTransaction();

            #region "Save User Details"
            //Get command object to save user details
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_acu_saveUser");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "username", DbType.String, Username);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "password", DbType.String, Password);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "name", DbType.String, Name);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "email", DbType.String, Email);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "phone", DbType.String, Phone);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "address", DbType.String, Address);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "usernameExpiryDate", DbType.DateTime, UsernameExpiryDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "isActive", DbType.Boolean, IsActive);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Extension", DbType.String, Extension);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Mobile", DbType.String, Mobile);

            //Execute command
            int UserID = Convert.ToInt32(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, objTrans));
            #endregion

            #region "Save User-Role Details"
            objCmd = null;

            //Get command object to save user-role details
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_acu_saveUserRoleDetails");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "userId", DbType.Int32, UserID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "roleId", DbType.Int32, RoleID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "isActive", DbType.Boolean, IsActive);

            //Execute command
            int lastInsertId = Convert.ToInt32(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, objTrans));
            #endregion

            if (lastInsertId > 0)
            {
                //commit transaction on success
                objTrans.Commit();
            }
            else
            {
                //rollback trasaction on failure
                objTrans.Rollback();
            }
            return UserID;
        }
        catch (Exception ex)
        {
            //Rollback transaction if exception occurred
            objTrans.Rollback();

            logger.Error("Add Method : " + ex.Message);
            return 0;
        }
        finally
        {
            logger.Debug("Method End: Add");
            objCmd = null;

            //Close database connection
            objConn.Close();
        }
        return 0;
    }

    /// <summary>
    /// This method checks if the user exists with same user name
    /// </summary>
    /// <returns>int : Indicating the user existence</returns>
    public int CheckUserExists()
    {
        logger.Debug("Method Start: CheckUserExists");
        int result = 0;
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "acu_Sp_CheckUserAlreadyExists");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "UserName", DbType.String, Username);

            //Execute command
            result = Convert.ToInt32(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null));
        }
        catch
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End: CheckUserExists");
            objCmd = null;
        }
        return result;
    }

    /// <summary>
    /// Method to update user details
    /// </summary>
    /// <returns>Returns true on success else returns false</returns>
    public virtual bool UpdateAdminConsultant()
    {
        logger.Debug("Method Start: Update");

        DbCommand objCmd = null;

        try
        {
            //Get command object

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_acu_updateUser");

            //Add Parameters
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "id", DbType.Int32, Id);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "password", DbType.String, Password);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "name", DbType.String, Name);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "email", DbType.String, Email);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "phone", DbType.String, Phone);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "address", DbType.String, Address);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "usernameExpiryDate", DbType.DateTime, UsernameExpiryDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "isActive", DbType.Boolean, IsActive);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Extension", DbType.String, Extension);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Mobile", DbType.String, Mobile);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "roleId", DbType.String, RoleID);
            //Execute command
            int result = 0;
            result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
            if (result > 0)
                return true;
        }
        catch
        {
            throw;
        }
        finally
        {
            logger.Debug("Method End: Update");
            objCmd = null;
        }
        return false;
    }

    /// <summary>
    /// Added By: Ayyaj Mujawar
    /// Method to Trace Record Of sending Mails
    /// </summary>
    /// <returns>Returns true on success else returns false</returns>
    public Int64 SaveSendEmailDetailsFromQuote(string EmailTo, string EmailFromID, string EmailText, string Status, string Subject, string PageName)
    {
        DbCommand objCmd = null;
        Int64 Result = 0;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_SendEmailDetails");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Subject", DbType.String, Subject);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "EmailTo", DbType.String, EmailTo);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "EmailFromID", DbType.String, EmailFromID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "EmailText", DbType.String, EmailText);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "Status", DbType.String, Status);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "PageName", DbType.String, PageName);
            Result = Convert.ToInt64(Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null));
            return Result;
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message + "SyncDataFromQuote, SaveSendEmailDetailsFromQuote.Error:" + ex.StackTrace);
            return 0;
        }
    }



}
