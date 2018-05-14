using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Management.Config
{
    public class DeviceInfo : XObject
    {
        public DeviceInfo()
        {
            Name = "";
            Description = "";
            FolderPath = "";
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string FolderPath { get; set; }
    }
}
