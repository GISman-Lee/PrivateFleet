<%@ Application Language="C#" %>
<%@ Import Namespace="log4net" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.IO" %>

<script RunAt="server">

   
    void Application_Start(object sender, EventArgs e)
    {

        // Code that runs on application startup
        log4net.Config.DOMConfigurator.Configure();
        //Application["StartTime"] = DateTime.Now.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["WaitSeconds"]));
        //Application["StartDate"] = DateTime.UtcNow.Date;
        Application["StartTime"] = DateTime.Now.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["WaitSeconds"]));
        Application["StartDate"] = DateTime.Now.Date;
        RegisterCacheEntry();

        new Mechsoft.GeneralUtilities.PreLoad();
    }

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        try
        {
            if (HttpContext.Current.Request.Url.ToString() == ConfigurationManager.AppSettings["DummyPageUrl"])
            {
                Application["StartTime"] = Convert.ToDateTime(Application["StartTime"]).AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["WaitSeconds"]));
                RegisterCacheEntry();
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
    {
        try
        {
            //uncomment below 6 lines while upload it on live
            //Cls_DealAging.AutomaticMailSendingForTradeInAlerts();
            //Cls_DealAging.SendUpdateReminderToDealer_new();
            //Cls_DealAging.AutomaticDrasticMailSend();
            //Cls_DealAging.EmailForCustSerRepVDTOverdues();
            //Cls_DealAging.AutomaticShortlistingOfQuote();
            //Cls_DealAging.SendFinanceAlertToFincarAdmin();

            //uncomment end 

            //Do Reminder Processing here... 
            //if (Convert.ToDateTime(Application["StartDate"].ToString()) < DateTime.Now.Date)
            //{
            //    Application["StartDate"] = DateTime.UtcNow.Date;

            //Need to uncomment this while uploadation
            if (System.DateTime.Now.Hour == 11 && System.DateTime.Now.Minute < 05)
            {
                //Cls_DealAging.HandleDealAgingFactor();
                //Cls_DealAging.MakeHotDealerAsNormalDealer();
            }

            if (System.DateTime.Now.Hour == 10 && System.DateTime.Now.Minute < 05)
                ; // Cls_DealAging.SendEmailToCustomerBefore3Days();

            //}
            //Cls_RemiderProcess objReminderProcess = new Cls_RemiderProcess();
            //objReminderProcess.ProcessRemiderBatch();
           // HitPage();
        }
        catch (Exception ex)
        {

        }
    }
    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        Session["SeriesID"] = "";
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    private void HitPage()
    {

        try
        {
            WebClient client = new WebClient();
            client.DownloadData(ConfigurationManager.AppSettings["DummyPageUrl"].ToString());
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }


    private bool RegisterCacheEntry()
    {

        if (null != HttpContext.Current.Cache[ConfigurationManager.AppSettings["DummyCacheItemKey"]])
            return false;
        HttpContext.Current.Cache.Add(ConfigurationManager.AppSettings["DummyCacheItemKey"], "Test", null,
            DateTime.MaxValue, TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["WaitSeconds"])),
            CacheItemPriority.Normal, new CacheItemRemovedCallback(CacheItemRemovedCallback));
        return true;
    }
       
</script>

