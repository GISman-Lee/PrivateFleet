using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.Common;
using Mechsoft.GeneralUtilities;
using System.Data;
using System.Text;
using log4net;
using System.Configuration;


/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]

public class AutoComplete : System.Web.Services.WebService
{
    ILog logger = LogManager.GetLogger(typeof(AutoComplete));

    public AutoComplete()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        logger.Debug("Wel come to Hello World");
        return "Hello World";
    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public string[] RequestParametervalue1(string prefixText, int count)
    {
        try
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath(@"~\RequestParameters.xml"));
            DataTable dt = ds.Tables[0];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Name like '" + prefixText + "%'";
            dt = dv.ToTable();
            string[] items;
            if (dt != null)
            {
                items = new string[dt.Rows.Count];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    items.SetValue(dr["Name"].ToString(), i);
                    i++;
                }
                return items;
            }
            return null;
        }
        catch (Exception ex) { throw; return null; }
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public string[] RequestParametervalue2(string prefixText, int count)
    {
        try
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath(@"~\RequestParameters.xml"));
            DataTable dt = ds.Tables[1];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Name like '" + prefixText + "%'";
            dt = dv.ToTable();
            string[] items;
            if (dt != null)
            {
                items = new string[dt.Rows.Count];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    items.SetValue(dr["Name"].ToString(), i);
                    i++;
                }
                return items;
            }
            return null;
        }
        catch (Exception ex) { return null; }
    }



    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public string[] RequestParametervalue3(string prefixText, int count)
    {
        try
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath(@"~\RequestParameters.xml"));
            DataTable dt = ds.Tables[2];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Name like '" + prefixText + "%'";
            dt = dv.ToTable();
            string[] items;
            if (dt != null)
            {
                items = new string[dt.Rows.Count];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    items.SetValue(dr["Name"].ToString(), i);
                    i++;
                }
                return items;
            }
            return null;
        }
        catch (Exception ex) { return null; }
    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public string[] RequestParametervalue4(string prefixText, int count)
    {
        try
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath(@"~\RequestParameters.xml"));
            DataTable dt = ds.Tables[3];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Name like '" + prefixText + "%'";
            dt = dv.ToTable();
            string[] items;
            if (dt != null)
            {
                items = new string[dt.Rows.Count];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    items.SetValue(dr["Name"].ToString(), i);
                    i++;
                }
                return items;
            }
            return null;
        }
        catch (Exception ex) { return null; }
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] RequestParametervalue5(string prefixText, int count)
    {
        try
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath(@"~\RequestParameters.xml"));
            DataTable dt = ds.Tables[4];
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Name like '" + prefixText + "%'";
            dt = dv.ToTable();
            string[] items;
            if (dt != null)
            {
                items = new string[dt.Rows.Count];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    items.SetValue(dr["Name"].ToString(), i);
                    i++;
                }
                return items;
            }
            return null;
        }
        catch (Exception ex) { return null; }
    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]

    public string[] GetModels(string prefixText, int count, string contextKey)
    {
        //  int count = 10;
        DataTable dt = null;
        DbCommand objCmd = null;
        try
        {
            objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.StoredProcedure, "SpGetModels");
            Cls_DataAccess.getInstance().AddInParameter(objCmd, "isActive", DbType.Boolean, true);
            dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);

            DataView dv = dt.DefaultView;
            dv.RowFilter = "Model like '%" + prefixText + "%' AND MakeID='" + contextKey + "'";

            dt = dv.ToTable();
            string[] items;
            if (dt != null)
            {
                items = new string[dt.Rows.Count];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    items.SetValue(dr["Model"].ToString(), i);
                    i++;
                }
                return items;
            }
            return null;
        }
        catch (Exception ex) { return null; }
    }
}

