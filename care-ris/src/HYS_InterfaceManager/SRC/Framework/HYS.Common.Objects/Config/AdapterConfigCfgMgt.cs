using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Config
{
    public class AdapterConfigCfgMgt : ConfigMgtBase
    {
        public AdapterConfigCfgMgt()
            : this(ConfigHelper.ConfigDefaultFileName)
        {
        }

        public AdapterConfigCfgMgt( string filename )
        {
            _filename = filename;
            _configType = typeof(AdapterConfigCfg);
            _config = new AdapterConfigCfg();
        }

        public new AdapterConfigCfg Config
        {
            get { return base.Config as AdapterConfigCfg; }
            set { base.Config = value; }
        }
    }
}
