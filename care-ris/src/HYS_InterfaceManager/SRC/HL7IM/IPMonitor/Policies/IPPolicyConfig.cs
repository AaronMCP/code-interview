using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.IPMonitor.Policies
{
    public class IPPolicyConfig : PolicyConfigBase
    {
        public IPPolicyConfig()
        {
            TimeOutInS4Ping = 5;
            FlagService = "FlagService";
        }
        public Int32 TimeOutInS4Ping { get; set; }        
        public string FlagService { get; set; }
        public string IPPrivate { get; set; }
        public string IPPublic { get; set; }
        public string SubnetPrivate { get; set; }
        public string SubnetPublic { get; set; }
        public string GatewayPrivate { get; set; }
        public string GatewayPublic { get; set; }
        public string DNSPrivate { get; set; }
        public string DNSPublic { get; set; }
    }
}
