using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.IPMonitor.Policies;

namespace HYS.IM.IPMonitor
{
    public class MonitorControl
    {
        private IPPolicyControl _p1;
        private FlagPolicyControl _p2;

        public MonitorControl(object entry)
        {
            _p1 = new IPPolicyControl(Program.ConfigMgt.Config.PolicyIP, entry);
            _p2 = new FlagPolicyControl(Program.ConfigMgt.Config.PolicyFlag, entry);
        }
        public void Start()
        {
            _p1.Start();
            _p2.Start();
        }
        public void Stop()
        {
            _p1.Stop();
            _p2.Stop();
        }
    }
}
