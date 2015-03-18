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
using System.Text;
using System.Data.Common;

public partial class Welcome : System.Web.UI.Page
{
    //declare and inititalize logger object
    static ILog logger = LogManager.GetLogger(typeof(Welcome));

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //Set page header text
            if (!IsPostBack)
            {
                DataTable dtAnnouncement = new DataTable();
                Cls_AdminAnnouncement objAnnouncement = new Cls_AdminAnnouncement();
                try
                {

                    (this.Master.FindControl("lblUser") as Label).Text = "WelCome : " + Session[Cls_Constants.USER_NAME].ToString() + " ( " + Session[Cls_Constants.Role_Name].ToString() + " ) ";
                    Label lblHeader = (Label)Master.FindControl("lblHeader");

                    if (lblHeader != null)
                        lblHeader.Text = "Welcome To Private Fleet";
                    if (Convert.ToInt32(Session[Cls_Constants.ROLE_ID]) == 2)
                    {
                        string name = Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]);
                        string make = Convert.ToString(Session[Cls_Constants.Role_Name]);
                        string intro = "";

                        if (name.IndexOf(' ') > 0)
                            name = name.Substring(0, name.IndexOf(' '));

                        make = make.Substring(make.IndexOf('-') + 2);

                        dtAnnouncement = objAnnouncement.GetAdminAnnouncement();

                        tdDealer.Visible = true;

                        intro = "Welcome to Private Fleet";

                        if (dtAnnouncement != null && dtAnnouncement.Rows.Count == 1)
                            intro += "<br/><br/>" + Convert.ToString(dtAnnouncement.Rows[0]["PanelData"]);

                        intro += "<br/><br/>Hi " + name + ", welcome to the Private Fleet quoting system. You are currently logged in using the email address <b>" + Convert.ToString(Session[Cls_Constants.USER_NAME]) + "</b> and will be able to see any outstanding Quote Requests or previously completed Quote Requests for <b>" + make + "</b> simply by clicking the menu item Quote Requests to the left hand side.";
                        intro += "<br/><br/>If this is your first log in, we recommend that you immediately change your password to something you can remember by using the menu item on the left.  If you are a multi-franchised dealer, you will have a different password for each brand so it makes sense to use a pattern so you know which password to use when responding to a specific quote (eg ‘hon56’ for Honda, ‘sub56’ for Subaru etc)";
                        intro += "<br/><br/>If you can’t see a pending Quote Request that you were expecting, it’s likely that you logged in using a password related to a different brand or possibly an email address different to the email address to which the Quote Request notification was sent.  So check both these items, resetting the password for the brand if necessary.";
                        intro += "<br/><br/><b>IMPORTANT : </b> Please also add the domain <b>privatefleet.com.au</b> to your safe senders list to ensure you get all Quote Request notifications (usually done by right clicking an email from Private Fleet, choosing drop down menu option ‘Junk’ then selecting ‘Never block sender’s domain’)";
                        intro += "Thank you for quoting for our clients – we look forward to doing business with you.";

                        lblDealerIntro.Text = intro;
                    }
                    else
                    {
                        tdDealer.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("Page Load Event : " + ex.Message);
        }
    }


    //private void sendMailForVDTLoginDetails()
    //{
    //    logger.Debug("Start Automatic user mail sending");
    //    try
    //    {
    //        DataTable dt = null;
    //        DbCommand objCmd = null;

    //        objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.Text, "SELECT * FROM tbl_VDT_CustomerMaster");
    //        dt = Cls_DataAccess.getInstance().GetDataTable(objCmd);



    //        StringBuilder str = new StringBuilder();
    //        Cls_GenericEmailHelper objEmailHelper = new Cls_GenericEmailHelper();
    //        string link = ConfigurationManager.AppSettings["DummyPageUrl1"];
    //        foreach (DataRow dRow in dt.Rows)
    //        {

    //            str.Append("<p style='font: normal normal normal 16px Calibri; color:#1F497D;'>Dear " + Convert.ToString(dRow["FirstName"]) + "<br /><br />Once again, thank you for your order with Private Fleet.  The estimated delivery date for your car is " + Convert.ToDateTime(dRow["DeliveryDate"]).ToString("dd MMM yyyy") + " however due to the complexity of new vehicle orders and delivery in Australia, this *may* change in time.  We understand that you probably want the vehicle as soon as possible (like most people) so rest assured that we, and the dealer, will be doing everything possible to get the car to you as soon as possible.");
    //            str.Append("<br/><br/>To help you track the progress of your vehicle order, we have set up an online area where you can see the latest update from the dealer.");
    //            str.Append("<br/><br/> To access this area please go to <a href='" + link + "'>updates.privatefleet.com.au</a> and log in using your email address (" + Convert.ToString(dRow["Email"]) + ") and your Private Fleet member number (" + Convert.ToString(dRow["MemberId"]) + ")");
    //            str.Append("<br/><br/>To help minimize delays, there are a few things that you can help us with in the meantime:");
    //            str.Append("<br/><ol style='font: normal normal normal 16px Calibri; color:#1F497D;'><li>Please email a scan on your driver’s licence to <a href='<%# mailto:rego@privatefleet.com.au)%>' style='color: Blue; text-decoration: underline;'>rego@privatefleet.com.au</a></li>");
    //            str.Append("<li>If you are trading in a vehicle, please scan the registration papers and email those to <a href='<%# mailto:tradein@prtivatefleet.com.au)%>' style='color: Blue; text-decoration: underline;'>tradein@prtivatefleet.com.au</a></li>");
    //            str.Append("<li>If you are financing the vehicle, please pass on the details of your supplying dealer (" + Convert.ToString(dRow["DealerCompany"]) + ", " + Convert.ToString(dRow["DealerName"]) + ", " + Convert.ToString(dRow["DealerPhone"]) + ", " + Convert.ToString(dRow["DealerEmail"]) + ") to your financier ASAP so they may formally request an invoice in good time (this is one of the most common causes of delay as the dealer will not release the car until full payment has been received)</li></ol></p>");
    //            str.Append("<br/><p style='font: normal normal normal 16px Calibri; color:#1F497D;'>We will be requesting (and chasing up if necessary) the supplying dealer for delivery updates on at least a weekly basis.  Following each status update you will be emailed and you can also login using the above details to see the latest update as well as a history of the delivery process.  You can also use the online system to request any extra updates during this period directly from the dealer.  We are also on hand at any time to assist with any queries or concerns.");
    //            str.Append("<br/><br/>Best Regards");
    //            str.Append("<br/>Anna Mears");
    //            str.Append("<br/>Customer Service Manager");
    //            str.Append("<br/>Private Fleet");
    //            str.Append("<br/>Lvl 2, 845 Pacific Hwy");
    //            str.Append("<br/>Chatswood NSW 2067");
    //            str.Append("<br/>1300 303 181</p>");

    //        }


    //        objEmailHelper.EmailBody = str.ToString();
    //        objEmailHelper.EmailFromID = "manoj.mahagaonkar@mechsoftgroup.com";
    //        objEmailHelper.EmailToID = "manoj.mahagaonkar@mechsoftgroup.com";

    //        objEmailHelper.EmailSubject = "Welcome to Private Fleet";

    //        if (objEmailHelper.EmailToID != "" && objEmailHelper.EmailToID != null && objEmailHelper.EmailFromID != "" && objEmailHelper.EmailFromID != null)
    //            objEmailHelper.SendEmail();

    //    }
    //    catch (Exception ex)
    //    {
    //        logger.Debug("Error " + ex.Message);
    //    }
    //    finally
    //    {
    //        logger.Debug("ends Automatic user mail sending");
    //    }

    //}

}
