using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Miles_Cls_Dealer
/// </summary>
public class Miles_Cls_Dealer
{
	public Miles_Cls_Dealer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public IQueryable<DealersInfo> GetDealers()
    {
        DealerMasterDataContext Db = new DealerMasterDataContext();
        var query = from m in Db.tblDealerMasters
                    where m.City != ""
                    select new DealersInfo
                    {
                       ID = m.ID,
                       Name = m.Name,
                       Company = m.Company,
                       Email = m.Email,
                       Phone = m.Phone,
                       IsActive = m.IsActive
                    };

        return query;
    }

}