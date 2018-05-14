using System;
using System.Text;
using System.Collections.Generic;

namespace HYS.Common.Objects.License2
{
    public class GWLicenseHeader 
    {
        private string _title = "GW20";
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

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

        private string _otherInformation = "";
        public string OtherInformation
        {
            get { return _otherInformation; }
            set { _otherInformation = value; }
        }
    }
}
