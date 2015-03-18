using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel;

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

    public DataTable ConvertToDatatable<T>(List<T> data)
    {
        PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        for (int i = 0; i < props.Count; i++)
        {
            PropertyDescriptor prop = props[i];
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                table.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
            else
                table.Columns.Add(prop.Name, prop.PropertyType);
        }
        object[] values = new object[props.Count];
        foreach (T item in data)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = props[i].GetValue(item);
            }
            table.Rows.Add(values);
        }
        return table;
    }
}