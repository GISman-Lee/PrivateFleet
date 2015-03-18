using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Mechsoft.GeneralUtilities;

namespace Mechsoft.GeneralUtilities
{
    public static class DbOperations
    {
        public const string UPDATE = "UPDATE";
        public const string INSERT = "INSERT";
        public const string INSERT_ACCESSORY = "INSERT_ACCESSORY";
        public const string UPDATE_ACCESSORY = "UPDATE_ACCESSORY";
        public const string CHANGE_ACTIVENESS = "ACTIVE";
        public const string CHANGE_ACTIVENESS_ACCESSORY = "ACTIVE_ACCESSORY";
        public const String CHECK_IF_EXIST = "CHECK_IF_EXIST";
        public const String StateSelectionRULE1 = "RULE1";
        public const string StateSelectionRULE2 = "RULE2";
        public const string StateSelectionRULE3 = "RULE3";
        public const string StateSelectionCommonRule = "COMMON_RULE";
        public const string CHANGE_MASTER = "ISMASTER";
        //public const string StateSelectionRULE5 = "RULE5";
        //public const string StateSelectionRULE6 = "RULE6";
        //public const string StateSelectionRULE7 = "RULE7";
        //public const string StateSelectionRULE8 = "RULE8";
   
    }

    public class ReportType
    {
        public const string ByMake = "ByMake";
        public const string ByModel = "ByModel";
        public const string BySeries = "BySeries";
        public const string ByState= "ByState";
        public const string ByStateAndMake = "ByStateAndMake";
        public const string ByStateAndModel = "ByStateAndModel";
        public const string ByStateAndSeries = "ByStateAndSeries";
        public const string GroupBy = "GroupBy";
        public const string ByNothing = "ByNothing";
        
    }

    public class ReportGroupBy
    {
        public const string GroupByDealer = "GroupByDealer";
        public const string GroupByConsultant = "GroupByConsultant";
        public const string GroupByState = "GroupByState";
        public const string GroupByDate = "GroupByDate";
    }


    public class QuoteFilters
    {
        public const string AllQuotesReturned = "AllQuotesReturned";
        public const string WinningQuotes = "WinningQuotes";
    }
}

