/// Filename    : ServiceFeeMstr.aspx.cs
/// Author      : Kalpana @ MechSoftGroup.com.
/// Date        : 04-11-2011
/// Purpose     : Code for Handling Service Fee
/// History     : 
/// -----------------------------------------------------------------------------------------
/// Sr.No.		Date                    Author		        Comments
/// -----------------------------------------------------------------------------------------
///   1.		03-11-2011	           Kalpana		        Intial Version
///  
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
/// Summary description for Cls_ServiceFeeMaster
/// Class for handling service charge(editng & updating service fee)
/// </summary>
public class Cls_ServiceFeeMaster
{
    #region Variables
    static ILog logger = LogManager.GetLogger(typeof(Cls_ServiceFeeMaster));
    
    public int Id  {  get;  set;  }

    public double fee { get; set; }

    public int regoCTP { get; set; }
    #endregion

    #region Methods
    
    public Cls_ServiceFeeMaster()
    {
        //
        // TODO: Add constructor logic here

        //


    }

    /// <summary>
    /// Created By   : Kalpana Shinde
    /// Created Date : 04-11- 2011
    /// Description  : Method to get service fee
    /// </summary>
    public DataTable getServiceFee()
    {
        try
        {
            DbCommand objCmd = null;
            objCmd = Cls_DataAccess.getInstance().GetCommand(System.Data.CommandType.StoredProcedure, "SpgetServiceFee");
            return Cls_DataAccess.getInstance().GetDataTable(objCmd);
        }
        catch (Exception ex)
        {
            logger.Error("getServiceFee() error" + ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Created By   : Kalpana Shinde
    /// Created Date : 04-11- 2011
    /// Description  : Method to update service fee
    /// </summary>

    public bool UpdateFee()
    {
        try
        {
            int result;
            DbCommand objcmd1 = null;

            objcmd1 = Cls_DataAccess.getInstance().GetCommand(System.Data.CommandType.StoredProcedure, "SPupdateFee");
            Cls_DataAccess.getInstance().AddInParameter(objcmd1, "pmrID", DbType.Int32, Id);
            Cls_DataAccess.getInstance().AddInParameter(objcmd1, "pmrFee", DbType.Double, fee);
            Cls_DataAccess.getInstance().AddInParameter(objcmd1, "pmrRegoCTP", DbType.Int16 , regoCTP );

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
            logger.Error("UpdateFee() error" + ex.Message);

            return false;
        }
    }
    #endregion

}
