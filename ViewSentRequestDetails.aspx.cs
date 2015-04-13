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
using Mechsoft.GeneralUtilities;
using Mechsoft.FleetDeal;
using AccessControlUnit;
using System.Text;
using System.Web.Mail;


public partial class ViewSentRequestDetails : System.Web.UI.Page
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(ViewSentRequestDetails));
    string Fdate, Tdate;

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Fdate = Request.QueryString["Fdate"];
            Tdate = Request.QueryString["Tdate"];
            ViewState["MakeID"] = Convert.ToString(Request.QueryString["MakeID"]);

            string surl = "<script src='http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=" + Convert.ToString(ConfigurationManager.AppSettings["GoogleAPIKey"]) + "' type='text/javascript'></script>";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "onLoad", surl);
            if (!IsPostBack)
            {

                //Set page header text
                Label lblHeader = (Label)Master.FindControl("lblHeader");

                if (lblHeader != null)
                    lblHeader.Text = " Quote Requests Details ";

                if (Request.QueryString["id"] != null && Request.QueryString["id"] != string.Empty)
                {
                    int RequestId = Convert.ToInt32(Request.QueryString["id"]);

                    ViewState["RequestId"] = RequestId;

                    //display request information
                    DisplayRequestHeader();
                    //DisplayRequestParameters();
                    DisplayRequestAccessories();
                    // DisplayRequestDealers();
                    UcFixedCharges1.BindFixedCharges();
                    UcRequestHeader1.DisplayRequestHeader(RequestId);

                }

                if (Request.QueryString["QuoteID"] != null && Request.QueryString["OptionID"] != null)
                {
                    ViewState["QuotationID"] = Request.QueryString["QuoteID"].ToString();
                    ViewState["OptionID"] = Request.QueryString["OptionID"].ToString();
                    trViewShortListedQuotation.Visible = true;
                }
                else
                {
                    trViewShortListedQuotation.Visible = false;
                }

                trDealerInfo.Visible = false;
                //Label lblDealer=(Label)lblDealer.FindControl("lblDealer").Visible = false;
                // objvsrd . FindControl("lblDealer").Visible = false;
                if (Request.QueryString["ConsultantID"] != null)
                {
                    ViewState["ConsultantID"] = Request.QueryString["ConsultantID"];
                    trConsultantInfo.Visible = true;
                    Cls_UserMaster objUserMaster = new Cls_UserMaster();
                    objUserMaster.RequestID = Convert.ToInt16(ViewState["RequestId"]);
                    if (Convert.ToInt32(Request.QueryString["ConsultantID"]) == -9999)
                    {
                        objUserMaster.ConsultantID = objUserMaster.getConsultantID();
                    }
                    else
                    {
                        objUserMaster.ConsultantID = Convert.ToInt16(Request.QueryString["ConsultantID"].ToString());
                    }
                    dlConsultantInfo.DataSource = objUserMaster.GetConsultantBasicInfo();
                    dlConsultantInfo.RepeatColumns = 1;
                    dlConsultantInfo.DataBind();

                    if (Request.QueryString["Dealer"] != null)
                    { }
                    else
                    {
                        trDealerInfo.Visible = true;
                        DataTable dtDealerInfo = objUserMaster.GetDealerInfo();
                        gvDealerInfo.DataSource = dtDealerInfo;
                        gvDealerInfo.DataBind();

                        ViewState["DealerInfo1"] = dtDealerInfo;
                        //dlDealerInfo.DataSource = objUserMaster.GetDealerInfo();
                        //dlDealerInfo.RepeatColumns = 1;
                        //dlDealerInfo.DataBind();
                    }
                }
                else
                {
                    trConsultantInfo.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Event : " + ex.Message);
        }
    }
    #endregion

    #region "Methods"
    /// <summary>
    /// Method to bind parameters to grid
    /// </summary>
    /// 


    private void DisplayRequestHeader()
    {
        logger.Debug("Method Start : DisplayRequestParameters");
        Cls_Request objRequest = new Cls_Request();
        try
        {
            if (ViewState["RequestId"] != null)
            {
                objRequest.RequestId = Convert.ToInt32(ViewState["RequestId"]);
                DataTable dt = objRequest.GetRequestHeaderInfo();

                //assigne on click event to suburb
                map.Attributes.Add("onclick", "javascript:seeMap('" + Convert.ToString(dt.Rows[0]["Latitude"]) + "','" + Convert.ToString(dt.Rows[0]["Longitude"]) + "','');");

                if (dt.Rows[0]["suburb"].ToString() == null || dt.Rows[0]["suburb"].ToString() == "")
                    lblSub1.Text = "--";
                else
                    lblSub1.Text = dt.Rows[0]["suburb"].ToString();

                if (dt.Rows[0]["pcode"].ToString() == null || dt.Rows[0]["pcode"].ToString() == "")
                    lblPCode1.Text = "--";
                else
                    lblPCode1.Text = dt.Rows[0]["pcode"].ToString();

                if (dt.Rows.Count > 0)
                {
                    //lblMake.Text = dt.Rows[0]["Make"].ToString();
                    //lblModel.Text = dt.Rows[0]["Model"].ToString();
                    //lblSeries.Text = dt.Rows[0]["Series"].ToString();

                    //if (dt.Rows[0]["ConsultantNotes"].ToString() != "")
                    //    lblNotes.Text = dt.Rows[0]["ConsultantNotes"].ToString();
                    //else
                    //    lblNotes.Text = "-";


                    //by manoj on 9 apr 2011
                    String ConsultantNotes = "";
                    int cnt = 0;
                    if (dt.Rows[0]["OrderTaken"].ToString() != String.Empty)
                    {
                        cnt++;
                        ConsultantNotes = "<b style='color:Red; text-decoration:blink;'>" + dt.Rows[0]["OrderTaken"].ToString() + "</b><br/>";
                    }
                    if (dt.Rows[0]["Urgent"].ToString() != String.Empty)
                    {
                        cnt++;
                        ConsultantNotes += dt.Rows[0]["Urgent"].ToString() + "<br/>";
                    }
                    if (dt.Rows[0]["BuildYear"].ToString() != String.Empty)
                    {
                        cnt++;
                        ConsultantNotes += dt.Rows[0]["BuildYear"].ToString() + "<br/>";
                    }
                    if (dt.Rows[0]["ConsultantNotes"].ToString() != String.Empty)
                    {
                        cnt++;
                        ConsultantNotes += dt.Rows[0]["ConsultantNotes"].ToString();
                    }
                    if (cnt == 0)
                        ConsultantNotes = "--";
                    lblNotes.Text = ConsultantNotes;


                    //if (dt.Rows[0]["CustomerId"] != null)
                    //{
                    // //   UcCustomerDetails1.DisplayCustomerInfo(Convert.ToInt32(dt.Rows[0]["CustomerId"]));
                    //}

                    //Bind accessories for selected series
                    UcSeriesAccessories1.Visible = true;
                    UcSeriesAccessories1.SeriesId = Convert.ToInt32(dt.Rows[0]["SeriesId"]);
                    UcSeriesAccessories1.BindSeriesAccessories();
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("DisplayRequestParameters Method : " + ex.Message);
            throw;
        }
        finally
        {
            objRequest = null;
            logger.Debug("Method End : DisplayRequestParameters");
        }
    }

    /// <summary>
    /// Method to bind accessories to grid
    /// </summary>
    private void DisplayRequestAccessories()
    {
        logger.Debug("Method Start : DisplayRequestAccessories");
        Cls_Request objRequest = new Cls_Request();
        try
        {
            if (ViewState["RequestId"] != null)
            {
                objRequest.RequestId = Convert.ToInt32(ViewState["RequestId"]);
                DataTable dt = objRequest.GetRequestAccessories();

                //bind request parameters to grid
                lblAccessory1.Visible = false;
                if (dt.Rows.Count == 0)
                {
                    lblAccessory1.Visible = true;
                    lblAccessory1.Text = "No Accessories.";

                }

                gvAccessories.DataSource = dt;
                gvAccessories.DataBind();


            }
        }
        catch (Exception ex)
        {
            logger.Error("DisplayRequestAccessories Method : " + ex.Message);
            throw;
        }
        finally
        {
            objRequest = null;
            logger.Debug("Method End : DisplayRequestAccessories");
        }
    }

    /// <summary>
    /// Method to bind parameters to grid
    /// </summary>
    //private void DisplayRequestParameters()
    //{
    //    logger.Debug("Method Start : DisplayRequestParameters");
    //    Cls_Request objRequest = new Cls_Request();
    //    try
    //    {
    //        if (ViewState["RequestId"] != null)
    //        {
    //            objRequest.RequestId = Convert.ToInt32(ViewState["RequestId"]);
    //            DataTable dt = objRequest.GetRequestParameters();

    //            //bind request parameters to grid
    //            gvParameters.DataSource = dt;
    //            gvParameters.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        logger.Error("DisplayRequestParameters Method : " + ex.Message);
    //        throw;
    //    }
    //    finally
    //    {
    //        objRequest = null;
    //        logger.Debug("Method End : DisplayRequestParameters");
    //    }
    //}

    /// <summary>
    /// Method to bind dealers to grid
    /// </summary>
    private void DisplayRequestDealers()
    {
        logger.Debug("Method Start : DisplayRequestDealers");
        Cls_Request objRequest = new Cls_Request();
        try
        {
            if (ViewState["RequestId"] != null)
            {
                objRequest.RequestId = Convert.ToInt32(ViewState["RequestId"]);
                DataTable dt = objRequest.GetRequestDealers();

                //bind request parameters to grid
                gvDealers.DataSource = dt;
                gvDealers.DataBind();
            }
        }
        catch (Exception ex)
        {
            logger.Error("DisplayRequestDealers Method : " + ex.Message);
            throw;
        }
        finally
        {
            objRequest = null;
            logger.Debug("Method End : DisplayRequestDealers");
        }
    }
    #endregion

    #region "Events"

    // by manoj on 16 Mar 2011 for daler reminder
    protected void gvDealerInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int min = 0;
        ConfigValues objConfig = new ConfigValues();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                min = Convert.ToInt32(objConfig.GetValue(Cls_Constants.MINUTES_TO_REMIND_DEALER));
                DateTime dtnow = System.DateTime.Now;
                TimeSpan ts = new TimeSpan(0, -min, 00);
                DateTime dtNew = dtnow.Add(ts);
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRemind");

                if (((HiddenField)e.Row.FindControl("QoutationID")).Value.ToString() == "")
                {
                    // int QId = Convert.ToInt32(((HiddenField)e.Row.FindControl("QoutaionExist")).Value);
                    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Reminder").ToString()) == 1)
                    {

                        lnk.Enabled = false;
                        lnk.ForeColor = System.Drawing.Color.FromName("Gray");
                        lnk.Text = "Quotation Pending <br/> Reminded";
                    }
                    else
                    {
                        if (dtNew <= Convert.ToDateTime(((HiddenField)e.Row.FindControl("LastDate")).Value))
                        {
                            lnk.Enabled = false;
                            lnk.ForeColor = System.Drawing.Color.FromName("Gray");
                        }
                    }
                }
                else
                {
                    lnk.Enabled = false;
                    lnk.ForeColor = System.Drawing.Color.FromName("Gray");
                    lnk.Text = "Qoutation Received";
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    // by manoj on 16 Mar 2011 for daler reminder
    protected void gvDealerInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemindDealer")
        {
            Cls_UserMaster objUserMaster = new Cls_UserMaster();
            DataTable dtTemp = (DataTable)ViewState["DealerInfo1"];
            DateTime LastTemindDateTime = System.DateTime.Now;
            int DealerId = 0, RequestId = 0;
            string Email = "";
            try
            {
                DealerId = Convert.ToInt32(e.CommandArgument);
                objUserMaster.DealerID = DealerId;

                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtTemp.Rows[i]["DealerID"].ToString()) == Convert.ToInt32(e.CommandArgument))
                    {
                        LastTemindDateTime = Convert.ToDateTime(dtTemp.Rows[i]["LastRemindDateTime"].ToString());
                        RequestId = Convert.ToInt32(dtTemp.Rows[i]["RequestId"].ToString());
                        objUserMaster.RequestID = RequestId;
                        objUserMaster.Reminder = Convert.ToInt32(dtTemp.Rows[i]["Reminder"].ToString()) + 1;
                        Email = dtTemp.Rows[i]["Email"].ToString();
                    }
                }

                int result = objUserMaster.updateReminder();
                if (result > 0)
                {
                    SendMail(Email, DealerId, RequestId);
                }

                gvDealerInfo.DataSource = objUserMaster.GetDealerInfo();
                gvDealerInfo.DataBind();

            }
            catch
            {
            }
            finally
            {
                objUserMaster = null;
                dtTemp = null;
            }

        }
    }


    public void SendMail(string Email, int DealerId, int RequestId)
    {
        Cls_UserMaster objUserMaster = new Cls_UserMaster();
        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
        DataTable dtMail = new DataTable();
        StringBuilder str = new StringBuilder();

        objUserMaster.RequestID = RequestId;
        objUserMaster.DealerID = DealerId;
        dtMail = objUserMaster.GetReminderInfo();

        str.Append("<p style='font: normal normal normal 12px Tahoma;'>Dear " + dtMail.Rows[0]["DealerName"].ToString() + "<br /><br />This is just a gentle reminder that we are still chasing a quote for a ");
        str.Append(dtMail.Rows[0]["Make"].ToString() + " and are looking forward to your response.<br /><br />");
        str.Append("We are still working closely with the potential buyer and hope to have a commitment soon if we haven’t already.<br /><br />");
        str.Append("So if you have any questions, please let me know, otherwise I look forward to receiving your quote and hopefully putting a deal together!<br /><br />");
        str.Append("The quote request is waiting for you at <a href='http://quotes.privatefleet.com.au/'>quotes.privatefleet.com.au</a><br /><br />");
        str.Append("Thanks once again.<br /><br />");
        str.Append(dtMail.Rows[0]["ConsultantName"].ToString());

        objEmailHelper.EmailBody = str.ToString();


        objEmailHelper.EmailToID = Email;
        objEmailHelper.EmailFromID = dtMail.Rows[0]["Email"].ToString();
        //objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";
        //objEmailHelper.EmailFromID = "manoj.mahagaonkar@mechsoftgroup.com";
        objEmailHelper.EmailSubject = "Reminder for the Quote Request for " + dtMail.Rows[0]["Make"].ToString() + " from " + dtMail.Rows[0]["ConsultantName"].ToString();

        if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
        {
            objEmailHelper.SendEmail();
        }
    }


    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session[Cls_Constants.SESSION_BACK_PAGE_URL] != null && !(String.IsNullOrEmpty(Session[Cls_Constants.SESSION_BACK_PAGE_URL].ToString())))
            {

                String PageToRedirect = Session[Cls_Constants.SESSION_BACK_PAGE_URL].ToString();
                Session[Cls_Constants.SESSION_BACK_PAGE_URL] = "";
                if (PageToRedirect.Contains("ViewSentRequestDetails.aspx"))
                {
                    Response.Redirect("AdminReport.aspx?&FDate=" + Fdate, true);
                }
                if (Request.QueryString["doRedirect"] != null)
                {
                    Response.Redirect("~/ViewRecivedQuoteRequests.aspx?Fdate=" + Fdate + "&Tdate=" + Tdate + "&MakeID=" + ViewState["MakeID"]);

                }
                else
                {
                    Response.Redirect(PageToRedirect + "?FDate=" + Fdate + "&TDate=" + Tdate + "&MakeID=" + ViewState["MakeID"], true);
                }
            }
            else
            {
                //redirect to received requests listing page
                Response.Redirect("~/ViewRecivedQuoteRequests.aspx?Fdate=" + Fdate + "&Tdate=" + Tdate + "&MakeID=" + ViewState["MakeID"]);
            }
        }
        catch (Exception ex)
        {
            logger.Error("btnBack_Click Event : " + ex.Message);
        }
    }
    #endregion

    protected void lbtnViewSLQuotation_Click(object sender, EventArgs e)
    {
        Session[Cls_Constants.SESSION_BACK_PAGE_URL] = "ViewSentRequestDetails.aspx" + Request.Url.Query;
        String PageToRedirect = "~/ViewShortlistedQuotation.aspx?QuoteID=" + ViewState["QuotationID"].ToString() + "&ReqID=" + ViewState["RequestId"].ToString() + "&OptionID=" + ViewState["OptionID"].ToString() + "&ConsultantID=" + ViewState["ConsultantID"].ToString();
        Response.Redirect(PageToRedirect);
    }


}
