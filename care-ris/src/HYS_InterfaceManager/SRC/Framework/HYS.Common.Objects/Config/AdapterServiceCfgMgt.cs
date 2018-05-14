using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Config
{
    public class AdapterServiceCfgMgt : ConfigMgtBase
    {
        public AdapterServiceCfgMgt()
            : this(ConfigHelper.ServiceDefaultFileName)
        {
        }

        public AdapterServiceCfgMgt(string filename)
        {
            _filename = filename;
            _configType = typeof(AdapterServiceCfg);
            _config = new AdapterServiceCfg();
        }

        public new AdapterServiceCfg Config
        {
            get { return base.Config as AdapterServiceCfg; }
            set { base.Config = value; }
        }
    }
}
