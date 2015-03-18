using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using log4net;

/// <summary>
/// Summary description for Cls_Calculator
/// </summary>
public class Cls_Calculator
{
    #region
    static ILog logger = LogManager.GetLogger(typeof(Cls_Calculator));
    DataTable dt;

    public int FromStateID { get; set; }
    public int ToStateID { get; set; }
    #endregion

    public Cls_Calculator()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetCharges()
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetCharges");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "FromStateID", DbType.Int32, FromStateID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "ToStateID", DbType.Int32, ToStateID);
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("Get Charges Err - " + ex.Message);
            return null;
        }
    }


}
