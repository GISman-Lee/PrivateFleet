using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;

public partial class PageActionMapping : System.Web.UI.Page
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(PageActionMapping));

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Set page header text
            if (!IsPostBack)
            {
                Label lblHeader = (Label)Master.FindControl("lblHeader");

                if (lblHeader != null)
                    lblHeader.Text = "Page-Action Mapping";
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Event : " + ex.Message);
        }
    }
    #endregion

    
}
