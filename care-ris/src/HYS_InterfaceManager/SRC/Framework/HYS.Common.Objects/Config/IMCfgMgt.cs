using System;
using System.Collections.Generic;
using HYS.Common.Objects.License2;

namespace HYS.Common.Objects.Config
{
    public class IMCfgMgt : ConfigMgtBase
    {
        public IMCfgMgt()
            : this(ConfigHelper.IMDefaultFileName)
        {
        }

        public IMCfgMgt(string filename)
        {
            _filename = filename;
            _configType = typeof(IMCfg);
            _config = new IMCfg();
        }

        public new IMCfg Config
        {
            get { return base.Config as IMCfg; }
            set { base.Config = value; }
        }

        public override bool Load()
        {
            lock (this)
            {
                if (!base.Load()) return false;
                if (Config != null && Config.IsPasswordSecret == true)
                {
                    DataCrypto dc = new DataCrypto();
                    Config.LoginUser = dc.Decrypto(Config.LoginUser);
                    Config.LoginPassword = dc.Decrypto(Config.LoginPassword);
                    Config.IsPasswordSecret = false;
                }
                return true;
            }
        }

        public override bool Save()
        {
            return _Save() && Load();
        }

        public bool _Save()
        {
            lock (this)
            {
                if (Config != null && Config.IsPasswordSecret == false)
                {
                    DataCrypto dc = new DataCrypto();
                    Config.LoginUser = dc.Encrypto(Config.LoginUser);
                    Config.LoginPassword = dc.Encrypto(Config.LoginPassword);
                    Config.IsPasswordSecret = true;
                }
                return base.Save();
            }
        }
    }
}
