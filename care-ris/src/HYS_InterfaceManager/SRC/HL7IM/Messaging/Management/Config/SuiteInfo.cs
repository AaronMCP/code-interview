using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Management.Config
{
    public class SuiteInfo : XObject
    {
        public SuiteInfo()
        {
            Name = "";
            Description = "";
            Devices = new XCollection<DeviceInfo>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public XCollection<DeviceInfo> Devices { get; set; }
    }
}
