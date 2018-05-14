using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.License
{
    [Obsolete("Please use classes in HYS.Common.Objects.License2 namespace instead.", true)]
    public class GWLicense : XObject
    {
        private GWLicenseHeader _header = new GWLicenseHeader();
        public GWLicenseHeader Header
        {
            get { return _header; }
            set { _header = value; }
        }

        private XCollection<DeviceLicense> _devices = new XCollection<DeviceLicense>();
        public XCollection<DeviceLicense> Devices
        {
            get { return _devices; }
            set { _devices = value; }
        }

        private XCollection<DeviceLicenseLevel> _licenseLevels = new XCollection<DeviceLicenseLevel>();
        public XCollection<DeviceLicenseLevel> LicenseLevels
        {
            get { return _licenseLevels; }
            set { _licenseLevels = value; }
        }

        public DeviceLicense FindDevice(string name, DeviceType type, DirectionType direction)
        {
            foreach (DeviceLicense d in _devices)
            {
                if (d.Name.ToString() == name && d.Type == type && d.Direction == direction) return d;
            }
            return null;
        }
        public DeviceLicenseLevel FindLicenseLevel(string name, DeviceType type, DirectionType direction)
        {
            DeviceLicense lic = FindDevice(name, type, direction);
            if (lic == null) return null;
            return FindLicenseLevel(lic.LevelID);
        }
        public DeviceLicenseLevel FindLicenseLevel(byte id)
        {
            foreach (DeviceLicenseLevel l in _licenseLevels)
            {
                if (l.ID == id) return l;
            }
            return null;
        }
        
    }
}
