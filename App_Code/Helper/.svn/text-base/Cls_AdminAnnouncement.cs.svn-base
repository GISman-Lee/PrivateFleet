using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using System.Data;

/// <summary>
/// Summary description for Cls_AdminAnnouncement
/// </summary>
public class Cls_AdminAnnouncement
{
    #region Veriables

    static ILog logger = LogManager.GetLogger(typeof(Cls_AdminAnnouncement));
    public string PanelID { get; set; }
    public string PanelDate { get; set; }
    public int CreatedBy { get; set; }

    #endregion

    #region Constructor
    public Cls_AdminAnnouncement()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #endregion

    #region Methods

  
    public DataTable GetAdminAnnouncement()
    {
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetAdminAnnouncement");
            //Execute command
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetAdminAnnouncement Error - " + ex.Message);
            return null;
        }
        finally
        {
            objCmd = null;
        }
    }

    public int SaveAdminAnnouncement()
    {
        DbCommand objCmd = null;
        try
        {
            //Get command object
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpSaveAdminAnnouncement");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "PanelID", DbType.String, PanelID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "PanelDate", DbType.String, PanelDate);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "CreatedBy", DbType.Int32, CreatedBy);
            return Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("GetAdminAnnouncement Error - " + ex.Message);
            return 0;
        }
        finally
        {
            objCmd = null;
        }
    }

    #endregion
}
