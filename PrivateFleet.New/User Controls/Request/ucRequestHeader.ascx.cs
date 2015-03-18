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
using System.Text;

public partial class User_Controls_Request_ucRequestHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void DisplayRequestHeader(int RequestID)
    {
        Cls_Request objRequest = new Cls_Request();
        try
        {
            string strMake = "";
            string strModel = "";
            string strSeries = "";


            objRequest.RequestId = RequestID;
            DataTable dtReqHeader = objRequest.GetRequestHeaderInfo();

            DataList1.RepeatColumns = 1;
            if (dtReqHeader.Rows.Count > 0)
            {
                strMake = dtReqHeader.Rows[0]["Make"].ToString();
                strModel = dtReqHeader.Rows[0]["Model"].ToString();
                strSeries = dtReqHeader.Rows[0]["Series"].ToString();
            }
            StringBuilder MakeModelSeries = new StringBuilder();
            MakeModelSeries.Append(strMake);
            if (strModel != "")
            {
                MakeModelSeries.Append("," + strModel);
            }
            if (strSeries != "")
            {
                MakeModelSeries.Append("," + strSeries);
            }

            if (dtReqHeader.Rows[0]["Series_1"].ToString() != String.Empty)
            {
                MakeModelSeries.Append(" (" + dtReqHeader.Rows[0]["Series_1"].ToString() + ")");
            }

            //Postal code and suburb
            Label lblSub1 = (Label)this.Parent.FindControl("lblSub1");
            Label lblPCode = (Label)this.Parent.FindControl("lblPCode1");

            if (lblSub1 != null && lblPCode != null)
            {
               
                if (dtReqHeader.Rows[0]["suburb"].ToString() == null || dtReqHeader.Rows[0]["suburb"].ToString() == "")
                    lblSub1.Text = "--";
                else
                    lblSub1.Text = dtReqHeader.Rows[0]["suburb"].ToString();

                if (dtReqHeader.Rows[0]["pcode"].ToString() == null || dtReqHeader.Rows[0]["pcode"].ToString() == "")
                    lblPCode.Text = "--";
                else
                    lblPCode.Text = dtReqHeader.Rows[0]["pcode"].ToString();
            }



            DataTable dt = new DataTable();
            dt.Columns.Add("Header");
            dt.Columns.Add("Details");

            DataRow dRow = null;
            dRow = dt.NewRow();
            dRow["Header"] = "Make,Model,Series";
            dRow["Details"] = MakeModelSeries.ToString();
            dt.Rows.Add(dRow);

            DataList1.DataSource = dt;
            DataList1.DataBind();



            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Header");
            dt1.Columns.Add("Details");

            DataRow dRow1 = null;


            DataList2.RepeatColumns = 1;

            DataTable dtReqParams = objRequest.GetRequestParameters();
            if (dtReqParams.Rows.Count > 0)
            {


                foreach (DataRow drParam in dtReqParams.Rows)
                {
                    dRow1 = dt1.NewRow();
                    dRow1["Header"] = drParam["Parameter"].ToString();
                    if (drParam["ParamValue"].ToString() == "")
                    {
                        dRow1["Details"] = "-";
                    }
                    else
                    {
                        dRow1["Details"] = drParam["ParamValue"].ToString();
                    }
                    //dRow1["Details"] = drParam["ParamValue"].ToString();
                    dt1.Rows.Add(dRow1);
                }
            }

            DataList2.DataSource = dt1;
            DataList2.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}
