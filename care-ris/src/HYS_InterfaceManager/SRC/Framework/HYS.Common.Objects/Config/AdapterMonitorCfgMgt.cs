using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Config
{
    public class AdapterMonitorCfgMgt : ConfigMgtBase
    {
        public AdapterMonitorCfgMgt()
            : this(ConfigHelper.MonitorDefaultFileName)
        {
        }

        public AdapterMonitorCfgMgt(string filename)
        {
            _filename = filename;
            _configType = typeof(AdapterMonitorCfg);
            _config = new AdapterMonitorCfg();
        }

        public new AdapterMonitorCfg Config
        {
            get { return base.Config as AdapterMonitorCfg; }
            set { base.Config = value; }
        }
    }
}
