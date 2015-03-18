/// Filename    : ServiceFeeMstr.aspx.cs
/// Author      : Kalpana @ MechSoftGroup.com.
/// Date        : 04-11-2011
/// Purpose     : Code for Freight cost details
/// History     : 
/// -----------------------------------------------------------------------------------------
/// Sr.No.		Date                    Author		        Comments
/// -----------------------------------------------------------------------------------------
///   1.		04-11-2011	           Kalpana		        Intial Version
///==================================================================================================*/
///</history>
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data;
using Mechsoft.GeneralUtilities;
using log4net;

/// <summary>
/// Summary description for Cls_FreightCost
/// </summary>
//to get freight details

public class Cls_FreightCost
{
    #region Variables
    static ILog logger = LogManager.GetLogger(typeof(Cls_ServiceFeeMaster));
    public int SId { get; set; }
    public double ACTcost { get; set; }
    public double NSWcost { get; set; }
    public double QLDcost { get; set; }
    public double SAcost { get; set; }
    public double TAScost { get; set; }
    public double VICcost { get; set; }
    public double WAcost { get; set; }
    #endregion

    #region Methods
    public Cls_FreightCost()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Created By   : Kalpana Shinde
    /// Created Date : 04-11- 2011
    /// Description  : Method to get freight cost details
    /// </summary>
    public DataTable getFreightCost()
    {
        try
        {
            DbCommand objCmd = null;
            objCmd = Cls_DataAccess.getInstance().GetCommand(System.Data.CommandType.StoredProcedure, "SPFreightCost");
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("getFreightCost() error" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// Created By   : Kalpana Shinde
    /// Created Date : 04-11- 2011
    /// Description  : Method to update freight cost
    /// </summary>

    public bool UpdateFreightCost()
    {

        try
        {
            int result;
            DbCommand objcmd1 = null;

            objcmd1 = Cls_DataAccess.getInstance().GetCommand(System.Data.CommandType.StoredProcedure, "SPupdateFreightDetail");
            Cls_DataAccess.getInstance().AddInParameter(objcmd1, "pmrFrmSID", DbType.Int32, SId);
            Cls_DataAccess.getInstance().AddInParameter(objcmd1, "pmrACT", DbType.Double, ACTcost);
            Cls_DataAccess.getInstance().AddInParameter(objcmd1, "pmrNSW", DbType.Double, NSWcost);
            Cls_DataAccess.getInstance().AddInParameter(objcmd1, "pmrQLD", DbType.Double, QLDcost);
            Cls_DataAccess.getInstance().AddInParameter(objcmd1, "pmrVIC", DbType.Double,VICcost);
            Cls_DataAccess.getInstance().AddInParameter(objcmd1, "pmrSA", DbType.Double, SAcost);
            Cls_DataAccess.getInstance().AddInParameter(objcmd1, "pmrWA", DbType.Double, WAcost);
            Cls_DataAccess.getInstance().AddInParameter(objcmd1, "pmrTAS", DbType.Double, TAScost);
            result = Cls_DataAccess.getInstance().ExecuteNonQuery(objcmd1);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            logger.Error("UpdateFreightCost() error" + ex.Message);

            return false;
        }
    }
    #endregion
}
