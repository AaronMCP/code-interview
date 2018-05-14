using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.IPMonitor.Policies
{
    public class FlagPolicyConfig : PolicyConfigBase
    {
        public FlagPolicyConfig()
        {
        }

        [XCDataAttribute(true)]
        public string ServicesWithValidations { get; set; }
    }
}
