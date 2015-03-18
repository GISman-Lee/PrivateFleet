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
using AccessControlUnit;
using log4net;
using System.IO;
using System.Net;

public partial class MasterPage : System.Web.UI.MasterPage
{
    //declare and initialize logger object
    static ILog logger = LogManager.GetLogger(typeof(MasterPage));
    Boolean flag = false;

    //void get()
    //{

    //    DataTable dt = null;
    //    System.Data.Common.DbCommand objCmd = null;
    //    try
    //    {
    //        //Get command object
    //        string LastSyncDateTime = Cls_DataAccess.getInstance().ExecuteScaler(CommandType.Text, "Select Distinct LastSyncDatetime from tblSyncDataHolder").ToString();
    //        //objCmd = Cls_DataAccess.getInstance().GetCommand(CommandType.Text, "Select * from tblSyncDataHolder");


    //        dt = Cls_DataAccess.getInstance().GetDataTable(CommandType.Text, "Select * from tblSyncDataHolder order by EditedDate desc ");

    //        DataView dv = dt.DefaultView;
    //        dv.RowFilter = "EditedDate > '" + Convert.ToDateTime(LastSyncDateTime) + "'";
    //        dt = dv.ToTable();
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    finally
    //    {
    //        objCmd = null;
    //        logger.Debug("Method End : GetAllMakesNormalDealers");
    //    }
    //}

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session[Cls_Constants.USER_NAME] != null)
        {
            if (String.IsNullOrEmpty(Session[Cls_Constants.USER_NAME].ToString()))
            {
                Response.Redirect("index.aspx", true);
            }
        }
        else
        {
            Response.Redirect("index.aspx", true);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // get();
        try
        {
            if (!IsPostBack)
            {
                string filename = Path.GetFileName(Request.Path);
                if (filename != "QuoteRequest_1.aspx" && filename != "QuoteRequest_2.aspx" && filename != "QuoteRequest_3.aspx")
                    RemoveSessions();
            }

            if (Session[Cls_Constants.USER_NAME] != null)
            {
                if (String.IsNullOrEmpty(Session[Cls_Constants.USER_NAME].ToString()))
                    Response.Redirect("index.aspx", false);

                if (!IsPostBack)
                {

                    // statrt of new code 11/01/2011 by sachin 
                    //purpose if Dealer doesnot update its status then send mail to it and make menu links in accesssiable
                    if (Convert.ToString(Session[Cls_Constants.ROLE_ID]) == "2")
                    {
                        Cls_Login objCls_Login = new Cls_Login();
                        objCls_Login.UserName = Session[Cls_Constants.USER_NAME].ToString();
                        objCls_Login.Flag = 1;
                        DataTable dt = new DataTable();
                        dt = objCls_Login.GetDealerRespone();
                        // Int32 nonResponse_LowerLimit = 0;
                        Int32 nonResponse_UppeLimit = 0;

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                ConfigValues objConfigue = new ConfigValues();

                                //objConfigue.Key = "VDT_STATUS_REMINDER_TIME_IN_DAYS";
                                //nonResponse_LowerLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());

                                objConfigue.Key = "VDT_ACCOUNT_LOCK_TIME_IN_DAYS";
                                nonResponse_UppeLimit = Convert.ToInt32(objConfigue.GetValue(objConfigue.Key).ToString());
                                //objCls_Login.Dealer_Non_Response_LowerLimit = nonResponse_LowerLimit;
                                //objCls_Login.Dealer_Non_Response_UpperLimit = nonResponse_UppeLimit;
                                foreach (DataRow drow in dt.Rows)
                                {
                                    DataTable dt1 = new DataTable();
                                    DataView dv1 = new DataView(dt);
                                    dv1.RowFilter = "Customerid=" + Convert.ToString(drow["customerid"]);
                                    dt1 = dv1.ToTable();
                                    if (Convert.ToInt32(drow["nonResponseDate"]) >= nonResponse_UppeLimit)
                                    {
                                        flag = true;
                                    }
                                }
                            }
                        }
                    }

                    // end by sachin
                    //generate menu items to which logged in user has access
                    GenerateMenu();
                    lblUser.Text = "Welcome : " + Session[Cls_Constants.USER_NAME].ToString() + " ( " + Session[Cls_Constants.Role_Name].ToString() + " ) ";

                }

                //used to deduct dealer point and make dealer as normal
                //if (System.DateTime.Now.Hour == 11 && System.DateTime.Now.Minute < 15)
                //{
                //    Cls_DealAging.HandleDealAgingFactor();
                //    Cls_DealAging.MakeHotDealerAsNormalDealer();
                //}
                //end

            }
            else
                Response.Redirect("index.aspx", false);
        }
        catch (Exception ex)
        {
            logger.Error("Page_Load Event : " + ex.Message);
        }
    }

    //public void page_PreRender(object sender,EventArgs e)
    //{
    //    if (flag == true)
    //    {
    //        modal.TargetControlID = "test";

    //    }
    //    else
    //    {
    //        modal.TargetControlID = "lblinvoke";


    //    }


    //}

    protected void imgbtnLogOut_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Session["tempURL"] = null;
        Response.Redirect("index.aspx", false);
    }

    //used to remove session on the page quote requests
    public void RemoveSessions()
    {
        //remove session of QR_1
        Session.Remove("Make_Model_Series");
        Session.Remove("ConsultantNotes");
        Session.Remove("chkBox");
        Session.Remove("dtParameters");
        Session.Remove("SELECT_ACC");
        //Session.Remove("dtAccessories");

        //remove Session of QR_2
        Session.Remove("PCode_Suburb");
        Session.Remove("dtAllDealers");
        Session.Remove("DEALER_SELECTED");
    }

    private void GenerateMenu()
    {
        Cls_Access objAccess = new Cls_Access();
        DataTable dtPages = null;
        try
        {
            objAccess.AccessFor = Convert.ToInt32(Session[Cls_Constants.ROLE_ID]);
            objAccess.AccessTypeId = 1;

            if (Session["dtPages"] == null)
            {
                dtPages = objAccess.GetPageAccess();
                Session["dtPages"] = dtPages;
            }
            else
                dtPages = (DataTable)Session["dtPages"];

            int index = Request.Url.Segments.Length - 1;
            string strCurrentPage = Request.Url.Segments[index];

            // to check loged in user have access of this page or not
            // 24 aug 2012
            DataView dvTemp = dtPages.DefaultView;
            dvTemp.RowFilter = "ParentID<>0";
            DataTable dtChkAuthorization = dvTemp.ToTable();
            if (strCurrentPage == "ConsultantTradeIn2Report.aspx")
                strCurrentPage += Request.Url.Query;

            dtChkAuthorization.PrimaryKey = new DataColumn[] { dtChkAuthorization.Columns["PageUrl"] };
            DataRow dr = dtChkAuthorization.Rows.Find(strCurrentPage);
            //Miles Modification
            /*
            if (dr == null)
            {
                logger.Error("No Access :-");
                logger.Error("ID - " + Convert.ToString(Session[Cls_Constants.LOGGED_IN_USERID]) + " :: User Name - " + Convert.ToString(Session[Cls_Constants.USER_NAME]));
                logger.Error("Name - " + Convert.ToString(Session[Cls_Constants.CONSULTANT_NAME]) + ":: Page - " + strCurrentPage + " :: Role ID - " + Convert.ToString(Session[Cls_Constants.ROLE_ID]));
                trContent.Visible = false;
                divNoAccess.Visible = true;
            }
             * */

            //end

            //if(Convert .ToString (Request .QueryString .ToString())!="")
            //{
            //    strCurrentPage =strCurrentPage +"?"+Convert .ToString (Request .QueryString .ToString());
            //}
            Session["tempURL"] = strCurrentPage;
            string strActive = "";

            DataTable dtParent = null;
            if (dtPages != null)
            {
                DataView dv = dtPages.DefaultView;
                dv.RowFilter = "IsInternalLink=0 AND ParentID=0";
                dtParent = dv.ToTable();
            }

            GenerateMenuHtml(dtPages, dtParent, strCurrentPage, ref strActive);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void GenerateMenuHtml(DataTable dtPages, DataTable dtParent, string strCurrentPage, ref string strActive)
    {
        DataTable dtChild = null;

        tdMenu.InnerHtml += "<div class='menumain' style='float:left;'><div id='firstpane' class='menu_list'>";
        foreach (DataRow drParent in dtParent.Rows)
        {
            DataView dvChild = dtPages.DefaultView;
            dvChild.RowFilter = "IsInternalLink=0 AND ParentID=" + drParent["ID"].ToString();
            dtChild = dvChild.ToTable();

            if (strActive == "noaccess")
                strActive = "activemenu";
            else
                strActive = "";

            if (strCurrentPage.Contains(drParent["PageUrl"].ToString()) == true)
            //if (drParent["PageUrl"].ToString().Contains (strCurrentPage)==true)
            // if (drParent["PageUrl"].ToString() == strCurrentPage)
            {
                strActive = "activemenu";
            }
            else
            {
                dvChild = dtChild.DefaultView;
                dvChild.RowFilter = "IsInternalLink=0 AND PageUrl = '" + strCurrentPage + "'";
                if (dvChild.ToTable().Rows.Count > 0)
                    strActive = "activemenu";
            }

            //add all parent menus first
            //  tdMenu.InnerHtml += "<div class='menu_head " + strActive + "'>" + drParent["PageName"].ToString() + "</div>";

            if (Convert.ToString(Session[Cls_Constants.ROLE_ID]) == "2")
            {
                if (Convert.ToString(drParent["PageName"]) == "Quotation")
                {
                    //Note:for restricted user
                    if (flag == true)
                    {
                        // tdMenu.InnerHtml += "<div class='menu_head " + strActive + "' onClick='go_toDashboard()'>" + drParent["PageName"].ToString() + "</div>";
                        //  tdMenu.InnerHtml += "<div class='menu_head_1" + strActive + "' onClick='go_toDashboard()'>" + drParent["PageName"].ToString() + "</div>";
                        tdMenu.InnerHtml += "<div class='menu_head " + strActive + "' >" + drParent["PageName"].ToString() + "</div>";
                    }
                    else
                    {
                        tdMenu.InnerHtml += "<div class='menu_head " + strActive + "' >" + drParent["PageName"].ToString() + "</div>";
                    }
                }
                else
                {
                    tdMenu.InnerHtml += "<div class='menu_head " + strActive + "'>" + drParent["PageName"].ToString() + "</div>";
                }
                if (dtChild.Rows.Count > 0)
                {
                    tdMenu.InnerHtml += "<div class='menu_body'>";

                    //add child menus for this parent menu
                    foreach (DataRow drChild in dtChild.Rows)
                    {
                        string strSelected = "";
                        if (strCurrentPage.Contains(drChild["PageUrl"].ToString()) == true)
                            //   if (drChild["PageUrl"].ToString() == strCurrentPage)
                            strSelected = "class = 'selectedMenu'";
                        else
                            strSelected = "";

                        tdMenu.InnerHtml += "<a href='" + drChild["PageUrl"].ToString() + "' " + strSelected + ">" + drChild["PageName"].ToString() + "</a>";
                    }

                    tdMenu.InnerHtml += "</div>";
                }
            }
            else
            {
                tdMenu.InnerHtml += "<div class='menu_head " + strActive + "'>" + drParent["PageName"].ToString() + "</div>";

                if (dtChild.Rows.Count > 0)
                {
                    tdMenu.InnerHtml += "<div class='menu_body'>";

                    //add child menus for this parent menu
                    foreach (DataRow drChild in dtChild.Rows)
                    {
                        string strSelected = "";
                        if (strCurrentPage.Contains(drChild["PageUrl"].ToString()) == true)
                            //if (drChild["PageUrl"].ToString() == strCurrentPage)
                            strSelected = "class = 'selectedMenu'";
                        else
                            strSelected = "";

                        tdMenu.InnerHtml += "<a href='" + drChild["PageUrl"].ToString() + "' " + strSelected + ">" + drChild["PageName"].ToString() + "</a>";
                    }

                    tdMenu.InnerHtml += "</div>";
                }

            }
        }

        /*Added By Pravin For Showing CRM's Menue To Consultant*/
        if (Convert.ToString(Session[Cls_Constants.ROLE_ID]) == "3" || Convert.ToString(Session[Cls_Constants.ROLE_ID]) == "1" || Convert.ToString(Session[Cls_Constants.ROLE_ID]) == "5")
        {
            string strSelected = "";
            //if (strCurrentPage.Contains("http://180.235.129.33/PFSales/UserLogin.aspx") == true)
            if (strCurrentPage.Contains("http://122.99.112.75/PFSales/UserLogin.aspx") == true)
                //if (strCurrentPage.Contains("http://localhost:2831/PFSalesWeb/UserLogin.aspx") == true)122.99.112.75
                //if (drChild["PageUrl"].ToString() == strCurrentPage)
                strSelected = "class = 'selectedMenu'";
            else
                strSelected = "";
            //string strHref = "http://localhost:2831/PFSalesWeb/UserLogin.aspx?UserId=" + Session[Cls_Constants.LOGGED_IN_USERID].ToString().Trim();
            //string strHref = "http://180.235.129.33/PFSales/UserLogin.aspx?UserId=" + Session[Cls_Constants.LOGGED_IN_USERID].ToString().Trim();
            string strHref = "http://122.99.112.75/PFSales/UserLogin.aspx?UserId=" + Session[Cls_Constants.LOGGED_IN_USERID].ToString().Trim();
            tdMenu.InnerHtml += "<div class='menu_head' style='color:white;' " + strActive + "'>" + "<a target='_blank' href='" + strHref + "'" + ">" + "CRM </a></div>";
            //tdMenu.InnerHtml += "<div class='menu_body'>";
            //tdMenu.InnerHtml += "<a target='_blank' href='" + strHref + "'" + ">Get Lead Information</a>";
            //tdMenu.InnerHtml += "</div>";
        }
        /*------------------------------ END ---------------------------------- */

        tdMenu.InnerHtml += "</div></div>";
    }

    public void Restart()
    {

        logger.Error("Restarting application at  : " + DateTime.Now.ToString());
        logger.Debug("Restarting application at  : " + DateTime.Now.ToString());

        System.Web.HttpRuntime.UnloadAppDomain();

        WebClient myWebClient = null;
        try
        {
            string url = "http://localhost:1264/PrivateFleet.Web_New/index.aspx";
            myWebClient = new WebClient();
            byte[] stuff = myWebClient.DownloadData(url);
        }
        catch
        {
        }
        finally
        {
            logger.Error("Restarting application End  : " + DateTime.Now.ToString());
            logger.Debug("Restarting application End  : " + DateTime.Now.ToString());
            myWebClient.Dispose();
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        //logger.Error("Timer1_Tick  at  : " + DateTime.Now.ToString());
        int hour = DateTime.Now.TimeOfDay.Hours;
        int min = DateTime.Now.TimeOfDay.Minutes;

        int sec = DateTime.Now.TimeOfDay.Seconds;
        TimeSpan tm = new TimeSpan(hour, min, sec);
        string miniutes = "";
        string hours = "";
        if (hour >= 0 && hour <= 9)
        {
            hours = "0" + min.ToString();
        }
        else
        {
            hours = hour.ToString();
        }
        if (min >= 0 && min <= 9)
        {
            miniutes = "0" + min.ToString();
        }
        else
        {
            miniutes = min.ToString();
        }
        string str = hour.ToString() + ":" + miniutes;
        string str1 = "17:48";
        if (str == str1)
        {
            Restart();
        }

        // logger.Error("Timer1_Tick  at  : " + DateTime.Now.ToString());
    }
    public void btnModalInvoke_Click(object sender, EventArgs e)
    {
        try
        {
            pnlmodal.Visible = true;
            modal.Enabled = true;
            modal.Show();
        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }
    }
    public void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            pnlmodal.Visible = false;
            modal.Enabled = false;
            modal.Hide();
            Response.Redirect("ClinetIfo_ForDealer.aspx");

        }
        catch (Exception ex)
        {
            logger.Error(Convert.ToString(ex.Message));
        }

    }

    protected void btnPrevious_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("Welcome.aspx");
        }
        catch (Exception ex)
        {
            logger.Error("Back No Access err - " + ex.Message);
        }
    }
}



