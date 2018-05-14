using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.License
{
    [Obsolete("Please use classes in HYS.Common.Objects.License2 namespace instead.", true)]
    public class GWLicenseHeader : XObject
    {
        private string _title = "GW20";
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        // different device may have different expire date

        private DateTime _createDate = DateTime.MinValue;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        public double DayCountTillNow()
        {
            DateTime dt = DateTime.Now;
            TimeSpan ts = dt.Subtract(_createDate);
            return ts.TotalDays;
        }

        //private DateTime _expireDate = DateTime.MinValue;
        //public DateTime ExpireDate
        //{
        //    get { return _expireDate; }
        //    set { _expireDate = value; }
        //}

        //[XNode(false)]
        //public bool HasExpireDate
        //{
        //    get
        //    {
        //        return _expireDate != DateTime.MinValue;
        //    }
        //    set
        //    {
        //        if (value)
        //        {
        //            if(_expireDate == DateTime.MinValue)
        //                _expireDate = DateTime.MaxValue;
        //        }
        //        else
        //        {
        //            _expireDate = DateTime.MinValue;
        //        }
        //    }
        //}

        private string _otherInformation = "";
        public string OtherInformation
        {
            get { return _otherInformation; }
            set { _otherInformation = value; }
        }
    }
}
