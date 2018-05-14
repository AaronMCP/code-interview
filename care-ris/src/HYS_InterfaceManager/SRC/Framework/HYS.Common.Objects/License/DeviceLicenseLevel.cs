using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.License
{
    [Obsolete("Please use classes in HYS.Common.Objects.License2 namespace instead.", true)]
    public class DeviceLicenseLevel : XObject
    {
        private byte _id;
        public byte ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _maxInstanceCount;
        public int MaxInstanceCount
        {
            get { return _maxInstanceCount; }
            set { _maxInstanceCount = value; }
        }

        private double _maxDayCount;
        public double MaxDayCount
        {
            get { return _maxDayCount; }
            set { _maxDayCount = value; }
        }

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public DeviceLicenseLevel()
        {
        }
        public DeviceLicenseLevel(byte id, int maxInstance, int maxDay, string description)
        {
            _id = id;
            _maxDayCount = maxDay;
            _maxInstanceCount = maxInstance;
            _description = description;
        }

        public override string ToString()
        {
            return Description;
        }

        public string GetMaxInstanceCount()
        {
            if (_maxInstanceCount == -1) return "(infinite)";
            return _maxInstanceCount.ToString();
        }
        public string GetMaxDayCount()
        {
            if (_maxDayCount == -1) return "(infinite)";
            return _maxDayCount.ToString();
        }

        public bool IsExpired(DateTime createDate)
        {
            if (MaxDayCount < 0) return false;

            DateTime dtNow = DateTime.Now;
            TimeSpan tSpan = dtNow.Subtract(createDate);

            return tSpan.Days < 0 || tSpan.Days > MaxDayCount;
        }
    }
}

