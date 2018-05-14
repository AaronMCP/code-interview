using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.License2
{
    public class DeviceLicense
    {
        private DeviceName _name;
        public DeviceName Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private DeviceType _type;
        public DeviceType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private DirectionType _direction;
        public DirectionType Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private byte _maxInterfaceCount;
        public byte MaxInterfaceCount
        {
            get { return _maxInterfaceCount; }
            set { _maxInterfaceCount = value; }
        }

        public const byte InfiniteInterfaceCount = 255;

        private byte _maxDayCountLevelID;
        public byte MaxDayCountLevelID
        {
            get { return _maxDayCountLevelID; }
            set { _maxDayCountLevelID = value; }
        }

        private bool _singleton;
        public bool Singleton
        {
            get { return _singleton; }
            set { _singleton = value; }
        }

        private byte _featureCode;
        public byte FeatureCode
        {
            get { return _featureCode; }
            set { _featureCode = value; }
        }

        public byte[] GetValue()
        {
            return new byte[]{ _maxInterfaceCount,
                (byte)((byte)(_maxDayCountLevelID << 4) | _featureCode)};
        }
        public void SetValue(byte[] value)
        {
            if (value.Length > 0)
            {
                _maxInterfaceCount = value[0];
            }

            if (value.Length > 1)
            {
                _maxDayCountLevelID = (byte)(value[1] >> 4);
                _featureCode = (byte)(value[1] & 0x0F);
            }
        }

        public bool IsExpired(DateTime createDate)
        {
            int MaxDayCount = MaxDayCountLevel.GetMaxDayCount(MaxDayCountLevelID);
            if (MaxDayCount == MaxDayCountLevel.NeverExpire) return false;

            DateTime dtNow = DateTime.Now;
            TimeSpan tSpan = dtNow.Subtract(createDate);

            return tSpan.Days < 0 || tSpan.Days > MaxDayCount;
        }
        public string GetMaxInstanceCount()
        {
            if (_maxInterfaceCount == InfiniteInterfaceCount) return "(infinite)";
            return _maxInterfaceCount.ToString();
        }
        public string GetMaxDayCount()
        {
            int count = MaxDayCountLevel.GetMaxDayCount(_maxDayCountLevelID);
            if (count == MaxDayCountLevel.NeverExpire) return "(infinite)";
            return count.ToString();
        }

        public DeviceLicense()
        {
        }
        public DeviceLicense(DeviceName name, DeviceType type, DirectionType direction, UInt16 value, bool singleton)
        {
            _name = name;
            _type = type;
            _direction = direction;
            _singleton = singleton;

            byte high = (byte)(value >> 8);
            byte low = (byte)(value & 0x00FF);
            SetValue(new byte[] { high, low });
        }
        public DeviceLicense(DeviceName name, DeviceType type, DirectionType direction, UInt16 value)
            : this( name, type, direction, value, false)
        {
        }
    }
}
