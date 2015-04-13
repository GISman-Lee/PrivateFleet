using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DeliveryInNxt10Days : System.Web.UI.Page
{
    #region Private Fleet

    #endregion
    
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((Label)this.Page.Master.FindControl("lblHeader")).Text = "ETA Coming Closure";
        }
    }

    #endregion

    #region Methods

    #endregion
}
