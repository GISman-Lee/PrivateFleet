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
using System.Data.Common;
using System.Data.SqlClient;
using Mechsoft.FleetDeal;
using log4net;
using System.Text;
using System.Collections;

/// <summary>
/// Summary description for Cls_VDT_Report
/// </summary>
public class Cls_VDT_Report
{

    public int makeid { get; set; }
    public string UserName { get; set; }
    public string startdate { get; set; }
    public string endDate { get; set; }
    static ILog logger = LogManager.GetLogger(typeof(Cls_VDT_Report));
    DataTable dt = new DataTable();
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

	public Cls_VDT_Report()
	{
		//
		// TODO: Add constructor logic here

		//
	}

    public DataTable getVechicleDelivary_Report()
    {
        dt = new DataTable();
        try
        {
            DbCommand objcommand = null;
            objcommand = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Search_VechileDelivared");
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "make", DbType.Int32, makeid);
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "username", DbType.String, UserName);
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "startdate", DbType.DateTime ,FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "enddate", DbType.DateTime,ToDate);
            dt = Cls_DataAccess.getInstance().GetDataTable(objcommand);


        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
           
        }
        return dt;
    }


    public DataTable getDealerNonResponse_Report()
    {
        dt = new DataTable();
        try
        {
            DbCommand objcommand = null;
            objcommand = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Seach_VDTDealerNonResponse");
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "make", DbType.Int32, makeid);
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "username", DbType.String, UserName);
            dt = Cls_DataAccess.getInstance().GetDataTable(objcommand);


        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));

        }
        return dt;
    }

    public DataTable getCustomerRequest_Report()
    {
        dt = new DataTable();
        try
        {
            DbCommand objcommand = null;
            objcommand = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Search_VDTClientRequest");
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "makeid", DbType.Int32, makeid);
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "username", DbType.String, UserName);
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "forStartdate", DbType.DateTime, FromDate);
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "forEnddate", DbType.DateTime, ToDate);
            dt = Cls_DataAccess.getInstance().GetDataTable(objcommand);


        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));

        }
        return dt;
    }


    public DataTable getETACommingCloser_Report()
    {
        dt = new DataTable();
        try
        {
            DbCommand objcommand = null;
            objcommand = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "sp_Seach_VDTETAComming");
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "make", DbType.Int32, makeid);
            Cls_DataAccess.getInstance().AddInParameter(objcommand, "username", DbType.String, UserName);
            dt = Cls_DataAccess.getInstance().GetDataTable(objcommand);


        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));

        }
        return dt;
    }
}
