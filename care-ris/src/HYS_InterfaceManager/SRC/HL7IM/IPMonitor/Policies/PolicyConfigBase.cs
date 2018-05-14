using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.IPMonitor.Policies
{
    public class PolicyConfigBase : XObject
    {
        public PolicyConfigBase()
        {
            IntervalInMS = 3000;
            Enable = false;
        }

        public bool Enable { get; set; }
        public Int64 IntervalInMS { get; set; }
        public string AdapterName { get; set; }
        
    }
}
