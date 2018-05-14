using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class LicenseConfig : XObject
    {
        public const string LicenseConfigFileName = "License.lc";

        public string DeviceName { get; set; }

        public string Type { get; set; }

        public string Direction { get; set; }

        public bool Enabled { get; set; }
    }
}
