using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.IO;
using Mechsoft.GeneralUtilities;
using System.Data;
using System.Text;
using System.Configuration;

public partial class User_Controls_ucTradeInPhoto : System.Web.UI.UserControl
{
    #region Variables

    static ILog logger = LogManager.GetLogger(typeof(User_Controls_ucTradeInPhoto));

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //  BindPhotos();
        }
        catch (Exception ex)
        {
            logger.Error("ucTradeInPhoto Page_Load Error - " + ex.Message + " :: Trace - " + ex.StackTrace);
        }
        finally
        {
        }

    }

    #endregion

    #region Events

    protected void imgbtnUploadPhoto_Click(object sender, EventArgs e)
    {
        string fileName = "";
        string PhotoPath = "";
        Int32 TradeInID, CreatedBy, ModifiedBy;
        Cls_UploadFile cls_UploadFile = new Cls_UploadFile();
        try
        {
            if (FUTradeIn.HasFile)
            {

                string strCurrentPage = Request.Url.Segments[Request.Url.Segments.Length - 1];

                PhotoPath = Server.MapPath("Images/TradeInPhoto");
                TradeInID = Convert.ToInt32(((HiddenField)this.Parent.FindControl("hdfTradeInID")).Value);
                CreatedBy = Convert.ToInt32(Session[Cls_Constants.LOGGED_IN_USERID]);

                fileName = Convert.ToString(FUTradeIn.FileName).Trim();
                PhotoPath = CheckFileExist(PhotoPath, fileName);

                //upload file to server
                FUTradeIn.SaveAs(PhotoPath);

                PhotoPath = PhotoPath.Substring(PhotoPath.IndexOf("Images"));
                PhotoPath = PhotoPath.Replace("\\", "/");
                PhotoPath = "~/" + PhotoPath;

                // Save in DB
                cls_UploadFile.PhotoName = "";// txtPhotoName.Text.Trim();
                cls_UploadFile.PhotoPath = PhotoPath;
                cls_UploadFile.TradeInID = TradeInID;
                cls_UploadFile.CreatedBy = CreatedBy;
                if (strCurrentPage.ToLower() == "ConsultantTradeIn2Report.aspx".ToLower())
                    cls_UploadFile.photoFor = "TradeIn2";
                else if (strCurrentPage.ToLower() == "ConsultantTradeInReport.aspx".ToLower())
                    cls_UploadFile.photoFor = "TradeIn1";
                int result = cls_UploadFile.SaveTradeInPhoto();

                if (result > 0)
                {
                    //  txtPhotoName.Text = String.Empty;
                    lblmsg.Visible = true;
                    lblmsg.Text = "Photo Upload Successfully.";
                    if (result == 1)
                        sendNotification();
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Error. Try again....";
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("ucTradeInPhoto lnkbtnUpload_Click Error - " + ex.Message + " :: Trace - " + ex.StackTrace);
        }
        finally
        {
            cls_UploadFile = null;
        }

    }

    protected void imgbtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            // txtPhotoName.Text = String.Empty;
            lblmsg.Visible = false;
            lblmsg.Text = "";
        }
        catch (Exception ex)
        {
            logger.Error("ucTradeInPhoto imgbtnCancel_Click Error - " + ex.Message + " :: Trace - " + ex.StackTrace);
        }
        finally
        { }
    }

    //protected void gvTradeInPhoto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        //gvTradeInPhoto.PageIndex = e.NewPageIndex;
    //        //gvTradeInPhoto.DataSource = (DataTable)ViewState["gvTradeInPhoto"];
    //        //gvTradeInPhoto.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        logger.Error("ucTradeInPhoto gvTInReport_PageIndexChanging Error - " + ex.Message + " :: Trace - " + ex.StackTrace);
    //    }
    //    finally
    //    { }
    //}

    #endregion

    #region Methods

    //public void BindPhotos()
    //{
    //    Cls_UploadFile cls_UploadFile = new Cls_UploadFile();
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        cls_UploadFile.TradeInID = Convert.ToInt32(((HiddenField)this.Parent.FindControl("hdfTradeInID")).Value);
    //        dt = cls_UploadFile.GetTradeInPhoto();


    //        gvTradeInPhoto.DataSource = dt;
    //        gvTradeInPhoto.DataBind();
    //        ViewState["gvTradeInPhoto"] = dt;


    //    }
    //    catch (Exception ex)
    //    {
    //        logger.Error("ucTradeInPhoto BindPhotos Error - " + ex.Message + " :: Trace - " + ex.StackTrace);
    //    }
    //    finally
    //    {
    //        cls_UploadFile = null;
    //    }
    //}

    private string CheckFileExist(string serverPath, string filename)
    {
        if (File.Exists(serverPath + @"\" + filename))
        {
            filename = "New_" + filename;
            serverPath = CheckFileExist(serverPath, filename);
            return serverPath;
        }
        else
        {
            return serverPath + @"\" + filename;
        }
    }

    private void sendNotification()
    {
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
        try
        {
            DataTable dt = (DataTable)Session["TradeInDate"];
            if (dt != null && dt.Rows.Count == 1)
            {
                objEmailHelper.EmailFromID = ConfigurationManager.AppSettings["EmailFromID"].ToString();
                objEmailHelper.EmailSubject = "Trade In Photo Uploaded";
                objEmailHelper.EmailToID = Convert.ToString(ConfigurationManager.AppSettings["TradeInPhotoToEmail"]);
                objEmailHelper.EmailCcID = Convert.ToString(ConfigurationManager.AppSettings["TradeInPhotoCCEmail"]);
                // Email body
                StringBuilder strContent = new StringBuilder();
                strContent.Append("<p style='font-family:Calibri;font-size:14px;color:#1E4996;'>Dear User,");
                strContent.Append("<br/><br/>New photo uploaded to trade in report for the trade in");
                strContent.Append("<br/>Name - " + Convert.ToString(dt.Rows[0]["LastName"]));
                strContent.Append("<br/>Make - " + Convert.ToString(dt.Rows[0]["Make"]) + " " + Convert.ToString(dt.Rows[0]["T1Model"]));
                strContent.Append("<br/>State - " + Convert.ToString(dt.Rows[0]["HomeState"]));
                strContent.Append("<br/>City - " + Convert.ToString(dt.Rows[0]["HomeCity"]));
                strContent.Append("<br/><br/>Please click <a href='" + ConfigurationManager.AppSettings["DomainUrl"] + "/index.aspx?tir=" + Convert.ToString(dt.Rows[0]["ID"]) + "' target='_Blank'>here</a> to view uploaded Trade In photos.");
                strContent.Append("<br/><br/>Thanks,<br/>Private Fleet Leads</p>");

                objEmailHelper.EmailBody = Convert.ToString(strContent);
                objEmailHelper.SendEmail();
            }
        }

        catch (Exception ex)
        {
            logger.Error("ucTradeInPhoto sendNotification Error - " + ex.Message + " :: Trace - " + ex.StackTrace);
        }
        finally
        {
            objEmailHelper = null;
        }

    }

    #endregion
}
