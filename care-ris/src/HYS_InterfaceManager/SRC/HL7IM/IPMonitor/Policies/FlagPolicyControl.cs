using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using HYS.IM.Common.Logging;

namespace HYS.IM.IPMonitor.Policies
{
    public class FlagPolicyControl : PolicyControlBase
    {

        private FlagPolicyConfig _myConfig;

        public FlagPolicyControl(FlagPolicyConfig cfg, object entry)
            : base(cfg, entry, "Flag Policy")
        {
            _myConfig = cfg;
        }

        protected override void ExecutePolicy()
        {

            try
            {
                if (!Helper.IsAdapterConnected(_myConfig.AdapterName) || !Helper.HasServicesValidated())
                {
                    if (_entry.GetType() == typeof(EntityImpl))
                    {
                        (_entry as EntityImpl).RaiseBaseServiceStop();
                    }
                    else if (_entry.GetType() == typeof(FormMain))
                    {
                        (_entry as FormMain).RaiseStop();
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
