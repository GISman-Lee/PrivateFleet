using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Mechsoft.GeneralUtilities;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace Mechsoft.FleetDeal
{
    public abstract class Cls_CommonProperties
    {
        private string _StrDBOperation;
        private Boolean _boolIsActive;

        public Boolean IsActive
        {
            get { return _boolIsActive; }
            set { _boolIsActive = value; }
        }
        private int _intID;

        public int ID
        {
            get { return _intID; }
            set { _intID = value; }
        }

        public String DBOperation
        {
            get { return _StrDBOperation; }
            set { _StrDBOperation = value; }
        }

        private DateTime _dtCreatedDate;

        public DateTime CreatedDate
        {
            get { return _dtCreatedDate; }
            set { _dtCreatedDate = value; }
        }

        //private Cls_DataAccess _DataAccess = Cls_DataAccess.getInstance();

        //public Cls_DataAccess DataAccess
        //{
        //    get { return _DataAccess; }
        //}

        private String _SPName;

        public String SpName
        {
            get { return _SPName; }
            set { _SPName = value; }
        }
    }
}
