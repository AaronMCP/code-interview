using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.IPMonitor.Policies;

namespace HYS.IM.IPMonitor
{
    public class MonitorConfig : EntityConfigBase
    {
        public const string ConfigFileName = "IPMonitorConfig.xml";
        public const string ValidationConfigName = "ServiceValidations.xml";

        public MonitorConfig()
        {
            PolicyIP = new IPPolicyConfig();
            PolicyFlag = new FlagPolicyConfig();
        }

        public IPPolicyConfig PolicyIP { get; set; }
        public FlagPolicyConfig PolicyFlag { get; set; }
    }
}
