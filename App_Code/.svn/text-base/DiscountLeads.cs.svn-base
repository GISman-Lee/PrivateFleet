using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using log4net;
using System.Data;
using System.Data.Common;
using Mechsoft.GeneralUtilities;

/// <summary>
/// Summary description for DiscountLeads
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class DiscountLeads : System.Web.Services.WebService
{
    ILog logger = LogManager.GetLogger(typeof(DiscountLeads));

    public DiscountLeads()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public string AddDiscountLeads(string FullName, string Phone, string Mobile, string EmailID, string PostCode, string State, string IsFinanceQuoteRequired, string IsTradeIn, string TradeInMakeAndModel, string Comments, string MakeModel, string WebSite, string IsSendToOther)
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAddDiscountLeads");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@FullName", DbType.String, FullName);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Phone", DbType.String, Phone);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Mobile", DbType.String, Mobile);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@EmailID", DbType.String, EmailID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@PostCode", DbType.String, PostCode);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@State", DbType.String, State);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@IsFinanceQuoteRequired", DbType.Boolean, Convert.ToBoolean(IsFinanceQuoteRequired));
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@IsTradeIn", DbType.Boolean, Convert.ToBoolean(IsTradeIn));
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@TradeInMakeAndModel", DbType.String, TradeInMakeAndModel);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@Comments", DbType.String, Comments);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@MakeModel", DbType.String, MakeModel);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@WebSite", DbType.String, WebSite);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@IsSendToOther", DbType.Boolean, Convert.ToBoolean(IsSendToOther));
            int result = 0;// Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);

            //Execute command
            object obj = Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null);
            Int32.TryParse(obj.ToString(), out result);

            if (result > 0)
                return Convert.ToString(result);
            else
                return "0";
        }
        catch (Exception ex)
        {
            logger.Error("Error Discount Lead add - " + ex.Message);
            return "Exception Error - " + ex.Message;
        }
        finally
        {
        }
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public void AddDiscountError(string ErrorID, string ErrorNo, string ErrorMsg)
    {
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAddDiscountError");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@LeadID", DbType.Int64, Convert.ToInt64(ErrorID));
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ErrorNo", DbType.String, ErrorNo);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ErrorMsg", DbType.String, ErrorMsg);
            int result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);

        }
        catch (Exception ex)
        {
            logger.Error("Error Discount Error add - " + ex.Message);
        }
        finally
        {
        }
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public string AddDiscountLeadsError(string id, string ErrorMsg)
    {
        DbCommand objCmd = null;
        int result = 0;
        try
        {
            Int64 LeadID = 0;
            id = id.Trim().Substring(8);
            id = id.Trim().Substring(0, id.Length - 6);
            //char[] rev = id.ToCharArray();
            //id = "";
            //for (int i = rev.Length - 1; i >= 0; i--)
            //{
            //    id += rev[i].ToString();
            //}
            LeadID = Convert.ToInt64(id);

            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpAddDiscountErrorLeads");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@LeadID", DbType.String, LeadID);
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "@ErrorMsg", DbType.String, ErrorMsg);
            //int result = Cls_DataAccess.getInstance().ExecuteNonQuery(objCmd);

            //Execute command
            object obj = Cls_DataAccess.getInstance().ExecuteScaler(objCmd, null);
            Int32.TryParse(obj.ToString(), out result);

            if (result > 0)
                return "Success";
            else
                return "Error";
        }
        catch (Exception ex)
        {
            logger.Error("Error Discount Error add - " + ex.Message);
            return "Exception Error";
        }
        finally
        {
        }
    }
}

