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
using Mechsoft.GeneralUtilities;

public partial class User_Controls_Quotation_ucQuotationHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string DisplayQuotationHeader(int QuotationID)
    {
        Cls_Quotation objQuotation = new Cls_Quotation();
        try
        {
            objQuotation.QuotationID = QuotationID;

            DataTable dtData = objQuotation.GetQuotationHeaders();
            string ConsultantID = dtData.Rows[0]["ConsultantID"].ToString();
            //combine build date and Com[liance date for view purpose
            string str = Convert.ToDateTime(dtData.Rows[0]["ComplianceDate"]).ToString("MMM yyyy");
            str = str + " / " + Convert.ToDateTime(dtData.Rows[0]["BuilDate"]).ToString("MMM yyyy");
            dtData.Rows[0].BeginEdit();
            dtData.Columns["ComplianceDate"].ReadOnly = false;
            dtData.Columns["ComplianceDate"].MaxLength = 500;
            dtData.Rows[0]["ComplianceDate"] = str.ToString();
            dtData.Columns["ComplianceDate"].ReadOnly = true;
            dtData.Rows[0].EndEdit();


            dtData.Columns["ExStock"].ReadOnly = false;
            string x = dtData.Rows[0]["ExStock"].ToString();
            if (Int32.Parse(x.Substring(0, x.IndexOf('.'))) > 0)
                dtData.Rows[0]["ExStock"] = "Yes";
            else
                dtData.Rows[0]["ExStock"] = "No";
            dtData.Columns["ExStock"].ReadOnly = true;

            dtData.Columns["Order"].ReadOnly = false;
            x = dtData.Rows[0]["Order"].ToString();
            if (Int32.Parse(x.Substring(0, x.IndexOf('.'))) > 0)
                dtData.Rows[0]["Order"] = "Yes";
            else
                dtData.Rows[0]["Order"] = "No";
            dtData.Columns["Order"].ReadOnly = true;

            if (dtData.Rows[0]["ConsultantNotes"].ToString() == "")
                dtData.Rows[0]["ConsultantNotes"] = "-";

            if (dtData.Rows[0]["DealerNotes"].ToString() == "")
                dtData.Rows[0]["DealerNotes"] = "-";
            else
            {
                string DNotes = dtData.Rows[0]["DealerNotes"].ToString();
                DNotes = DNotes.Replace("^", "<br/>");
                dtData.Rows[0]["DealerNotes"] = DNotes;
            }
            DataList1.DataSource = dtData;
            DataList1.DataBind();
            return ConsultantID;
        }
        catch (Exception ex)
        {
            throw;
        }
    }


}
