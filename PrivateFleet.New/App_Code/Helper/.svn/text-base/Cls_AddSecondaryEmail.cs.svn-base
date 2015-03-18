using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Data;
using System.Data.Common;
using Mechsoft.GeneralUtilities;

/// <summary>
/// Summary description for AddSecondaryEmail
/// </summary>
public class Cls_AddSecondaryEmail
{
    #region Variables
    ILog logger = LogManager.GetLogger(typeof(Cls_AddSecondaryEmail));

    public string UserEmail { get; set; }
    public Int32 UserID { get; set; }

    #endregion

    #region Constructor
    public Cls_AddSecondaryEmail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Methods
    public string BindExistingMail()
    {
        DataTable dt = new DataTable();
        string Email = "";
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(System.Data.CommandType.StoredProcedure, "SPGetDealerSecondaryMail");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "UserID", DbType.String, UserID);
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);
            if (dt != null && dt.Rows.Count == 1)
                Email = Convert.ToString(dt.Rows[0][0]);
        }
        catch (Exception ex)
        {
            logger.Error("BindExistingMail err - " + Convert.ToString(ex.Message));
        }
        return Email;
    }


    public int SaveSecondaryEmail()
    {
        try
        {
            DbCommand objCmd = null;

            objCmd = Cls_DataAccess.getInstance().GetCommand(System.Data.CommandType.StoredProcedure, "SPSaveSecondaryEmail");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "UserID", DbType.String, UserID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "UserEmail", DbType.String, UserEmail);
            return Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("SaveSecondaryEmail err - " + Convert.ToString(ex.Message));
            return 0;
        }
    }
    #endregion

}
