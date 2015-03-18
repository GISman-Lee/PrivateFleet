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

public partial class User_Controls_Quotation_ucShortlistedQuotation : System.Web.UI.UserControl
{
    private int _quotationId;

    public int QuotationId
    {
        get { return _quotationId; }
        set { _quotationId = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UcQuotationHeader1.DisplayQuotationHeader(_quotationId);
    }
}
