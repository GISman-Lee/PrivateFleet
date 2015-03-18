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
using log4net;
using System.Text;
using System.Web.Mail;
using System.Linq;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;

public partial class User_Controls_ucTradeInData : System.Web.UI.UserControl
{
    #region Variables

    static ILog logger = LogManager.GetLogger(typeof(Cls_Request));
    string status_1 = "";
    string strCurrentPage = string.Empty;
    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        strCurrentPage = Request.Url.Segments[Request.Url.Segments.Length - 1];
        string surl = "<script src='http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=" + Convert.ToString(ConfigurationManager.AppSettings["GoogleAPIKey"]) + "' type='text/javascript'></script>";
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoad", surl);
        if (!IsPostBack)
        {
            tcTradeIn.ActiveTabIndex = 1;
        }

        if (strCurrentPage.ToLower() == "ConsultantTradeIn2Report.aspx".ToLower())
        {
            btnRemove.Visible = false;
            imgbtnClientPDF.Visible = false;
            imgbtnClientPDFWithPhoto.Visible = false;
            imgbtnStandardPDF.Visible = false;
            imgbtnStandardPDFWithPhoto.Visible = false;
        }
        else if (strCurrentPage.ToLower() == "ConsultantTradeInReport.aspx".ToLower())
        {
            btnRemove.Visible = true;
            imgbtnClientPDF.Visible = true;
            imgbtnClientPDFWithPhoto.Visible = true;
            imgbtnStandardPDF.Visible = true;
            imgbtnStandardPDFWithPhoto.Visible = true;
        }
    }

    #endregion

    #region Methods

    public void DisplayTradeInData(DataTable dt, string status)
    {
        try
        {
            BindPhotos();
            status_1 = status;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (Convert.ToString(dt.Rows[i][j]) == String.Empty || Convert.ToString(dt.Rows[i][j]) == null)
                    {
                        dt.Columns[j].ReadOnly = false;
                        if (dt.Columns[j].ColumnName != "DeliveryDateSort") // not assign -- to column havin dattime as datatype
                            dt.Rows[i][j] = "--";
                    }

                }

                if (Convert.ToString(dt.Rows[i]["T1RegExpMnt"]) != null && Convert.ToString(dt.Rows[i]["T1RegExpYear"]) != null)
                {
                    string month = "";
                    string year = "";

                    if (Convert.ToInt32(dt.Rows[i]["T1RegExpMnt"]) <= 9)
                        month = "0" + Convert.ToString(dt.Rows[i]["T1RegExpMnt"]);
                    else
                        month = Convert.ToString(dt.Rows[i]["T1RegExpMnt"]);

                    if (Convert.ToInt32(dt.Rows[i]["T1RegExpYear"]) <= 9)
                        year = "0" + Convert.ToString(dt.Rows[i]["T1RegExpYear"]);
                    else
                        year = Convert.ToString(dt.Rows[i]["T1RegExpYear"]);

                    dt.Rows[i]["Rego"] = month + "/" + year;
                }
                else
                    dt.Rows[i]["Rego"] = "--/--";

            }

            DataList1.RepeatColumns = 1;
            DataList1.DataSource = dt;
            DataList1.DataBind();
            Session["TradeInDate"] = dt;

            if (strCurrentPage.ToLower() == "ConsultantTradeIn2Report.aspx".ToLower())
            {
                ((Label)DataList1.Items[0].FindControl("lblComission_1")).Visible = false;
                ((Label)DataList1.Items[0].FindControl("lblComission")).Visible = false;
            }
            else if (strCurrentPage.ToLower() == "ConsultantTradeInReport.aspx".ToLower())
            {
                ((Label)DataList1.Items[0].FindControl("lblComission_1")).Visible = true;
                ((Label)DataList1.Items[0].FindControl("lblComission")).Visible = true;
            }
        }

        catch (Exception ex)
        {
            logger.Error("DisplayTradeInData err - " + ex.Message);
        }
        finally
        { }
    }

    public void DisplayTradeInHistory(DataTable dt)
    {
        try
        {
            tcTradeIn.Visible = true;
            gvTradeInHistory.DataSource = dt;
            gvTradeInHistory.DataBind();
        }
        catch (Exception ex)
        {
            logger.Error("DisplayTradeInHistory err - " + ex.Message);
        }
        finally
        { }
    }

    private void htmlToPdf(DataTable dt, string mode)
    {
        string makemodel = "";
        try
        {
            HtmlToPdfBuilder builder = new HtmlToPdfBuilder(PageSize.LETTER);
            HtmlPdfPage first = builder.AddPage();

            first.AppendHtml("<table cellspacing='0' cellpadding='3'  border='1' style='background-color: White; border-color: #CCCCCC; border-width: 1px; border-style: Solid; width: 60%; border-collapse: collapse;'>");
            first.AppendHtml("<tr valign='middle'><th>Trade In Details</th></tr>");
            first.AppendHtml("</table><br/>");
            //end header

            //Start Header
            first.AppendHtml("<table width='100%' cellspacing='0' cellpadding='0' border='1' align='center' style='color: Red; font-size: 14px; font-weight: bold;'>");
            first.AppendHtml("<tr valign='middle'>");

            first.AppendHtml("<td style='width: 20%;'>");
            first.AppendHtml("<span style='color:Black; font-size:12px; font-weight:normal;'>State:</span>&nbsp;" + Convert.ToString(dt.Rows[0]["HomeState"]) + "</span></td>");

            makemodel = Convert.ToString(dt.Rows[0]["Make"]);
            if (!Convert.ToString(dt.Rows[0]["T1Model"]).Equals(String.Empty) && !Convert.ToString(dt.Rows[0]["T1Model"]).Equals("--"))
                makemodel += " - " + Convert.ToString(dt.Rows[0]["T1Model"]);
            // 11 Mar 2013 : to add t1Year at top
            if (!Convert.ToString(dt.Rows[0]["T1Year"]).Equals(String.Empty) && !Convert.ToString(dt.Rows[0]["T1Year"]).Equals("--"))
                makemodel += " - " + Convert.ToString(dt.Rows[0]["T1Year"]);


            first.AppendHtml("<td style='width: 30%;'>");
            first.AppendHtml("<span style='color:Black; font-size:13px; font-weight:normal;'>Car:</span>&nbsp;" + makemodel + "</span></td>");

            if (mode.ToLower() != "client")
            {
                first.AppendHtml("<td style='width: 20%;'>");
                first.AppendHtml("<span style='color:Black; font-size:13px; font-weight:normal;'>Surname:</span>&nbsp;" + Convert.ToString(dt.Rows[0]["LastName"]) + "</span></td>");
            }

            first.AppendHtml("</tr></table> <br/>");
            //end header

            first.AppendHtml("<table cellspacing='0' cellpadding='0' border='1'style='>");
            //start 1st tr
            if (mode.ToLower() != "client")
            {
                first.AppendHtml("<tr style='font-size: 12px !important;'>");
                first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Surname -</td>");
                first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["LastName"]) + "</td>");
                first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Rego Number -</td>");
                first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["T1RegoNumber"]) + "</td>");
                first.AppendHtml("</tr>");
            }

            first.AppendHtml("<tr style='font-size: 12px !important;'>");
            first.AppendHtml("<td style='width: 18%; font-weight: normal;'>State -</td>");
            first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["HomeState"]) + "</td>");
            first.AppendHtml("<td style='width: 18%; font-weight: normal;'>City -</td>");
            first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["HomeCity"]) + "</td>");
            first.AppendHtml("</tr>");

            first.AppendHtml("<tr style='font-size: 12px !important;'>");
            first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Body Shape -</td>");
            first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["T1BodyShap"]) + "</td>");
            first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Body Color -</td>");
            first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["T1BodyColor"]) + "</td>");
            first.AppendHtml("</tr>");

            first.AppendHtml("<tr style='font-size: 12px !important;'>");
            first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Fuel Type -</td>");
            first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["T1FuelType"]) + "</td>");
            first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Transmission -</td>");
            first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["T1Transmission"]) + "</td>");
            first.AppendHtml("</tr>");

            first.AppendHtml("<tr style='font-size: 12px !important;'>");
            first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Trim Color -</td>");
            first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["T1TrimColor"]) + "</td>");
            first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Odometer -</td>");
            first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["T1Odometer"]) + "</td>");
            first.AppendHtml("</tr>");

            if (mode.ToLower() != "client")
            {
                first.AppendHtml("<tr style='font-size: 12px !important;'>");
                first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Rego -</td>");
                first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["Rego"]) + "</td>");
                first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Orig Trade in Value -</td>");
                first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["T1OrigValue"]) + "</td>");
                first.AppendHtml("</tr>");

                first.AppendHtml("<tr style='font-size: 12px !important;'>");
                first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Delivery Date -</td>");
                first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["DeliveryDate"]) + "</td>");
                first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Trade Status -</td>");
                first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["TradeStatus"]) + "</td>");
                first.AppendHtml("</tr>");
            }
            else if (mode.ToLower() == "client")
            {
                first.AppendHtml("<tr style='font-size: 12px !important;'>");
                first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Rego -</td>");
                first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["Rego"]) + "</td>");
                first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Delivery Date -</td>");
                first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["DeliveryDate"]) + "</td>");
                first.AppendHtml("</tr>");
            }

            if (mode.ToLower() != "client")
            {
                first.AppendHtml("<tr style='font-size: 12px !important;'>");
                first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Log Books -</td>");
                first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["LogBooks"]) + "</td>");

                if (strCurrentPage.ToLower() == "ConsultantTradeIn2Report.aspx".ToLower())
                {
                    first.AppendHtml("<td style='width: 18%; font-weight: normal;'></td>");
                    first.AppendHtml("<td style='width: 32%;'></td>");
                    first.AppendHtml("</tr>");
                }
                else if (strCurrentPage.ToLower() == "ConsultantTradeInReport.aspx".ToLower())
                {
                    first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Commission -</td>");
                    first.AppendHtml("<td style='width: 32%;'>" + Convert.ToString(dt.Rows[0]["Comission"]) + "</td>");
                    first.AppendHtml("</tr>");
                }
            }
            else if (mode.ToLower() == "client")
            {
                first.AppendHtml("<tr style='font-size: 12px !important;'>");
                first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Log Books -</td>");
                first.AppendHtml("<td style='width: 32%;' colspan='3'>" + Convert.ToString(dt.Rows[0]["LogBooks"]) + "</td>");
                first.AppendHtml("</tr>");
            }
            first.AppendHtml("<tr style='font-size: 12px !important;'>");
            first.AppendHtml("<td style='width: 18%; font-weight: normal;'>Trade In Description -</td>");
            first.AppendHtml("<td style='width: 32%;' colspan='3'>" + Convert.ToString(dt.Rows[0]["TradeInDesc"]) + "</td>");
            first.AppendHtml("</tr>");
            first.AppendHtml("</table>");

            // Image Addition
            if (hdfISWithPhoto.Value == "Yes")
            {
                DataTable dtTradeInPhoto = (DataTable)ViewState["gvTradeInPhoto"];

                if (dtTradeInPhoto != null && dtTradeInPhoto.Rows.Count > 0)
                {
                    int cnt = 0;
                    string domain = Convert.ToString(ConfigurationManager.AppSettings["DomainUrl"]);
                    first.AppendHtml("<table cellspacing='0' cellpadding='3'  border='0' style='background-color: White; border-width: 0px; width: 100%;'>");
                    foreach (DataRow dr in dtTradeInPhoto.Rows)
                    {
                        first.AppendHtml("<tr><td width: 100%; align='center' style='padding:0;  margin:0;'><img id='img_" + cnt + "' width='700' height='300' src='" + domain + Convert.ToString(dr["PhotoPath"]).Substring(1) + "' alt='No Image Found'/></td></tr>");
                        cnt++;
                    }
                    first.AppendHtml("</table>");
                }
            }

            builder.AddStyle("h1", "text-align:right;font-family: Arial, Helvetica, sans-serif, Verdana;font-size: 9px;font-weight: bold;text-decoration: none;color: #ffffff;vertical-align:top;");
            builder.AddStyle("h2", "vertical-align:top;top:0px;font-family: Arial, Helvetica, sans-serif, verdana;font-size: 8px;text-decoration: none;color: #333333;padding-left:5px; text-align:right; direction:rtl;");
            builder.AddStyle("h3", "vertical-align:top; font-family: Arial, Helvetica, sans-serif, verdana;font-size: 8px;text-decoration: none;color: #333333; padding-left:18px; text-align:right; ");
            builder.AddStyle("h5", "text-align:right;font-family: Arial, Helvetica, sans-serif, Verdana;font-size: 8px;text-decoration: none;color: #FFFFFF;vertical-align:top;font-style: italic;");
            builder.AddStyle("td", "vertical-align:middle;  font-weight:normal; font-family: Arial, Helvetica, sans-serif, verdana;font-size: 8.5px;text-decoration: none;color: #333333;padding-left:5px; text-align:left;background-color:Blue; ");
            builder.AddStyle("th", "vertical-align:middle;  font-weight:bold; font-family: Arial, Helvetica, sans-serif, verdana;font-size: 12px;text-decoration: none;color: #333333;padding-left:5px;text-align:center; align:center;");
            builder.AddStyle("img", "vertical-align:top;top:0px;");
            builder.AddStyle("h4", "vertical-align:top; font-family: Arial, Helvetica, sans-serif, verdana;font-size: 9px;text-decoration: none;color: #333333; background-color:Blue;");
            builder.AddStyle("table", "vertical-align:middle;");

            byte[] file = builder.RenderPdf();

            System.IO.File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/Report/ReportTradeIn.pdf"), file);
            Response.AddHeader("Content-Disposition", "attachment; filename=ReportTradeIn.pdf");
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile("~/Report/ReportTradeIn.pdf");
        }
        catch (Exception ex)
        {
            logger.Error("To pdf and send it err - " + ex.Message + ". Error" + ex.StackTrace);
        }
        finally
        {
            hdfISWithPhoto.Value = "";
        }
    }

    private void assignMap()
    {

        // lnk1.Attributes.Add("onclick", "javascript:seeMap('" + lbllati.Text + "','" + lbllongi.Text + "','" + lblname.Text + "','" + lblcompany.Text + "','" + lblmail.Text + "','" + lblmobile.Text + "','" + lblphone.Text + "');");

    }

    /// <summary>
    /// 25 Jan 2013 : To view photos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BindPhotos()
    {
        Cls_UploadFile cls_UploadFile = new Cls_UploadFile();
        DataTable dt = new DataTable();
        try
        {
            if (strCurrentPage.ToLower() == "ConsultantTradeIn2Report.aspx".ToLower())
                cls_UploadFile.photoFor = "TradeIn2";
            else if (strCurrentPage.ToLower() == "ConsultantTradeInReport.aspx".ToLower())
                cls_UploadFile.photoFor = "TradeIn1";
            cls_UploadFile.TradeInID = Convert.ToInt32(hdfTradeInID.Value);
            dt = cls_UploadFile.GetTradeInPhoto();

            dlTradeInImage.DataSource = dt;
            dlTradeInImage.DataBind();
            ViewState["gvTradeInPhoto"] = dt;


        }
        catch (Exception ex)
        {
            logger.Error("ucTradeInData BindPhotos Error - " + ex.Message + " :: Trace - " + ex.StackTrace);
        }
        finally
        {
            cls_UploadFile = null;
        }
    }

    /// <summary>
    /// 25 jAN 2013 : TO DELETE TADE IN PHOTOS FROM DB
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteFromTable(Int32 ID)
    {
        Cls_UploadFile cls_UploadFile = new Cls_UploadFile();
        try
        {
            cls_UploadFile.PhotoID = ID;
            if (strCurrentPage.ToLower() == "ConsultantTradeIn2Report.aspx".ToLower())
                cls_UploadFile.photoFor = "TradeIn2";
            else if (strCurrentPage.ToLower() == "ConsultantTradeInReport.aspx".ToLower())
                cls_UploadFile.photoFor = "TradeIn1";

            int result = cls_UploadFile.DeleteFromTable();
        }
        catch (Exception ex)
        {
            logger.Error("Trade in Data DeleteFromTable err - " + ex.Message);
        }
        finally
        {
            cls_UploadFile = null;
        }
    }

    #endregion

    #region Events

    /// <summary>
    /// 25 jAN 2013 : TO DELETE TADE IN PHOTOS
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton ImgDelete = (ImageButton)sender;
            DataListItem dlImage = (DataListItem)ImgDelete.NamingContainer;
            HiddenField hdfPhotoID = (HiddenField)dlImage.FindControl("hdfPhotoID");
            System.Web.UI.WebControls.Image imgPhoto = (System.Web.UI.WebControls.Image)dlImage.FindControl("imgPhoto");

            string fileName = imgPhoto.ImageUrl;
            if (File.Exists(Server.MapPath(fileName)))
            {
                File.Delete(Server.MapPath(fileName));
                DeleteFromTable(Convert.ToInt32(hdfPhotoID.Value));
                BindPhotos();
            }
        }
        catch (Exception ex)
        {
            logger.Error("Trade in Data imgDelete_Click err - " + ex.Message);
        }
        finally
        {

        }
    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (status_1 == "Report")
            ((Panel)e.Item.FindControl("pnl_1")).Visible = false;

        HiddenField hdfLati = (HiddenField)e.Item.FindControl("hdfLati");
        HiddenField hdfLongi = (HiddenField)e.Item.FindControl("hdfLongi");

        HyperLink lnkCityMap = (HyperLink)e.Item.FindControl("lnkCityMap");

        lnkCityMap.Attributes.Add("onclick", "javascript:seeMap('" + Convert.ToString(hdfLati.Value) + "','" + Convert.ToString(hdfLongi.Value) + "','" + Convert.ToString(lnkCityMap.Text) + "','','','','');");
    }

    protected void btnRemove_Click(object sender, ImageClickEventArgs e)
    {
        Cls_TradeInAlert objTradeInAlerts = new Cls_TradeInAlert();
        try
        {
            objTradeInAlerts.TradeID = Convert.ToInt32(hdfTradeInID.Value);
            int result = objTradeInAlerts.RemoveTradeIn();

            ((Panel)this.Parent.FindControl("pnlTradeIn_1")).Visible = false;
            ((Panel)this.Parent.FindControl("pnlTradeIn")).Visible = true;

            //System.Reflection.MethodInfo method = this.Page.NamingContainer.GetType().GetMethod("GenerateReport");
            //if (method != null)
            //    method.Invoke(this.Parent.NamingContainer, new object[] { });

            this.Page.GetType().InvokeMember("GenerateReport", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });

        }
        catch (Exception ex)
        {
            logger.Error("btnRemove_Click err - " + ex.Message);
        }
        finally
        {
            objTradeInAlerts = null;
        }
    }

    protected void imgbtnClientPDF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            if (Convert.ToString(imgbtn.CommandName).ToLower().Equals("cwith"))
                hdfISWithPhoto.Value = "Yes";
            else
                hdfISWithPhoto.Value = "No";

            htmlToPdf((DataTable)Session["TradeInDate"], "client");
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnClientPDF_Click err - " + ex.Message);
        }
    }

    protected void imgbtnStandardPDF_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            if (Convert.ToString(imgbtn.CommandName).ToLower().Equals("swith"))
                hdfISWithPhoto.Value = "Yes";
            else
                hdfISWithPhoto.Value = "No";

            htmlToPdf((DataTable)Session["TradeInDate"], "standard");
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnStandardPDF_Click err - " + ex.Message);
        }
    }

    /// <summary>
    /// 24 Jan 2012 To upload photo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgbtnUploadPhoto_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divpopID.Visible = true;
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnStandardPDF_Click err - " + ex.Message);
        }
    }

    protected void btnPopClose_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            divpopID.Visible = false;
            ((Label)ucTradeInPhoto1.FindControl("lblmsg")).Visible = true;
            ((Label)ucTradeInPhoto1.FindControl("lblmsg")).Text = String.Empty;
            BindPhotos();
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnStandardPDF_Click err - " + ex.Message);
        }
    }

    /// <summary>
    /// 24 jan 2013 : to view photo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgbtnViewPhoto_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tcTradeIn.ActiveTabIndex = 1;
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnStandardPDF_Click err - " + ex.Message);
        }
    }

    protected void gvTradeInHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblRD = (Label)e.Row.FindControl("lblRD");
                string Regardings = Convert.ToString(((HiddenField)e.Row.FindControl("hdfRegardings")).Value);
                string Details = Convert.ToString(((HiddenField)e.Row.FindControl("hdfDetails")).Value);
                string Result = Convert.ToString(((HiddenField)e.Row.FindControl("hdfResult")).Value);

                if (Result.ToLower() == "call completed" || Result.ToLower() == "error")
                {
                    //Create the RichTextBox. (Requires a reference to System.Windows.Forms.dll.)
                    System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                    // Get the contents of the RTF file. Note that when it is
                    // stored in the string, it is encoded as UTF-16.
                    string s = Details;
                    // Convert the RTF to plain text.
                    rtBox.Rtf = s;
                    Details = rtBox.Text;
                }
                if (Regardings != null && !Regardings.Equals(String.Empty))
                    lblRD.Text = Regardings + "<br/>" + Details;
                else
                    lblRD.Text = Regardings + Details;
            }
        }
        catch (Exception ex)
        {
            logger.Error("Error history data bound -" + ex.Message);
        }

    }


    /// <summary>
    /// Author      : Manoj
    /// Date        : 25 Jan 2013
    /// Description : Wihout PDF
    protected void imgbtnYes_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            hdfISWithPhoto.Value = "Yes";
            //  htmlToPdf((DataTable)ViewState["TradeInDate"], Convert.ToString(hdfIsClient.Value));

        }
        catch (Exception ex)
        {
            logger.Error("imgbtnQRCancelYes_Click err -" + ex.Message);
        }
        finally
        {
        }
    }

    /// <summary>
    /// Author      : Manoj
    /// Date        : 25 Jan 2013
    /// Description : Wihout PDF
    protected void imgbtnNo_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            hdfISWithPhoto.Value = "No";
            //htmlToPdf((DataTable)ViewState["TradeInDate"], Convert.ToString(hdfIsClient.Value));
        }
        catch (Exception ex)
        {
            logger.Error("imgbtnQRCancelNo_Click err -" + ex.Message);
        }
        finally
        {

        }
    }

    #endregion
}
