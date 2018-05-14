using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using HYS.IM.Common.Logging;

namespace HYS.IM.IPMonitor.Policies
{
    public class IPPolicyControl : PolicyControlBase
    {

        private IPPolicyConfig _myConfig;

        public IPPolicyControl(IPPolicyConfig cfg,object entry)
            : base(cfg, entry,"IP Policy")
        {
            _myConfig = cfg;
        }


        protected override void ExecutePolicy()
        {
            try
            {
                if (Helper.IsAdapterConnected(_myConfig.AdapterName)
                   && Helper.IsServiceRunning(_myConfig.FlagService))
                {
                    if (!Helper.IsCurrentIP(_myConfig.AdapterName, _myConfig.IPPublic))
                    {
                        if (!Helper.PingIP(_myConfig.IPPublic))
                        {
                            Helper.SetIP(_myConfig.AdapterName, _myConfig.IPPublic, _myConfig.SubnetPublic, _myConfig.GatewayPublic, _myConfig.DNSPublic);
                        }
                    }
                }
                else
                {
                    if (!Helper.IsCurrentIP(_myConfig.AdapterName, _myConfig.IPPrivate))
                    {
                        Helper.SetIP(_myConfig.AdapterName, _myConfig.IPPrivate, _myConfig.SubnetPrivate, _myConfig.GatewayPrivate, _myConfig.DNSPrivate);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);
            }
        }
    }
}
