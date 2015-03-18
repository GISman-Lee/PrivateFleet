using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Cls_General
/// </summary>
/// 
namespace Mechsoft.GeneralUtilities
{
    public class Cls_General
    {
        public Cls_General()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private DataTable dtAllowedActions;
        public DataTable AllowedActions
        {
            get { return dtAllowedActions; }
            set { dtAllowedActions = value; }
        }


        private string strActionToMap;
        public string ActionToCheck
        {
            get { return strActionToMap; }
            set { strActionToMap = value; }
        }


        public bool CheckForThisAction()
        {
            if (AllowedActions != null)
            {
                foreach (DataRow dRow in AllowedActions.Rows)
                {
                    if (String.Equals(dRow["Action"].ToString(), ActionToCheck, StringComparison.InvariantCultureIgnoreCase))
                        return true;
                    else
                        return false;
                }
            }
            else
            {

                return false;
            }
            return false;
        }
    }
}