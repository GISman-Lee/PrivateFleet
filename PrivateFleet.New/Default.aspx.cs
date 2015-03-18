using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;

public partial class _Default : System.Web.UI.Page
{
    static ILog logger = LogManager.GetLogger(typeof(_Default));
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string desc = Convert.ToString(Request.Headers["X-File-Err"]);
            string FieldName = Convert.ToString(Request.Headers["X-File-Name"]);

            string BrowserName = Convert.ToString(Request.Headers["X-BrowserName"]);
            string BrowserVersion = Convert.ToString(Request.Headers["X-BrowserVersion"]);
            string BrowserCookieEnable = Convert.ToString(Request.Headers["X-BrowserCookieEnable"]);
            string Platform = Convert.ToString(Request.Headers["X-Platform"]);
            string UserAgent = Convert.ToString(Request.Headers["X-UserAgent"]);

            logger.Error("Quotation fill Javascript Error - " + FieldName + " ::" + desc);
            logger.Error("BrowserName - " + BrowserName + " :: BrowserVersion - " + BrowserVersion);
            logger.Error("BrowserCookieEnable - " + BrowserCookieEnable + " :: Platform - " + Platform);
            logger.Error("UserAgent - " + UserAgent);
        }
        catch (Exception ex)
        {
            logger.Error("Javascript Error - " + ex.Message);
        }
    }
}