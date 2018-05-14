using System;
using System.Collections.Generic;

namespace HYS.Common.Objects.Config
{
    public class AdapterComposerCfgMgt : ConfigMgtBase
    {
        public AdapterComposerCfgMgt()
            : this(ConfigHelper.ComposerDefaultFileName)
        {
        }

        public AdapterComposerCfgMgt(string filename)
        {
            _filename = filename;
            _configType = typeof(AdapterComposerCfg);
            _config = new AdapterComposerCfg();
        }

        public new AdapterComposerCfg Config
        {
            get { return base.Config as AdapterComposerCfg; }
            set { base.Config = value; }
        }
    }
}
