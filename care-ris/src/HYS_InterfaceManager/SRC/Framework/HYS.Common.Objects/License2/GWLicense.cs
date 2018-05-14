using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.License2
{
    public class GWLicense
    {
        private GWLicenseHeader _header = new GWLicenseHeader();
        public GWLicenseHeader Header
        {
            get { return _header; }
            set { _header = value; }
        }

        private List<DeviceLicense> _devices = new List<DeviceLicense>();
        public List<DeviceLicense> Devices
        {
            get { return _devices; }
            set { _devices = value; }
        }

        public DeviceLicense FindDevice(string name, DeviceType type, DirectionType direction)
        {
            foreach (DeviceLicense d in _devices)
            {
                if (d.Name.ToString() == name && d.Type == type && d.Direction == direction) return d;
            }
            return null;
        }
    }
}
