using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using log4net;
using AccessControlUnit;


/// <summary>
/// Summary description for UpdateUser
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class UpdateUser : System.Web.Services.WebService {

    ILog logger = LogManager.GetLogger(typeof(UpdateUser));

    public UpdateUser () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }


    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    // Used to update quotacon user if some change occurs in PF Sales
    public int UpdateQuotaconUser(Int64 UserID, string Address, string Email, string Name, string Password, string Phone, string Extension, string Mobile)
    {
        Cls_User objUser = new Cls_User();
        Cls_UserMaster objDealerUser = new Cls_UserMaster();
        try
        {
            objDealerUser.Address = Address;
            objDealerUser.Email = Email;
            objDealerUser.Name = Name;
            objDealerUser.UsernameExpiryDate = DateTime.Now;
            objDealerUser.Password = string.Empty;// not in use
            objDealerUser.Phone = Phone;
            objDealerUser.Extension = Extension;
            objDealerUser.Mobile = Mobile;

            objDealerUser.Id = Convert.ToInt16(UserID);
            bool UpdateStatus = objDealerUser.UpdateAdminConsultant();
            return 1;
        }
        catch (Exception ex)
        {
            logger.Error("Error UpdateQuotaconUser web service - " + ex.Message);
            return 0;
        }
        finally
        {
        }
    }
    
}

