using System;
using System.Collections.Generic;
using HYS.Adapter.Base;
using SQLOutboundAdapter.Objects;
using HYS.SQLOutboundAdapterObjects;

namespace SQLOutboundAdapter.Forms
{
    public class FormConfigAgent : IConfigUI
    {
        private FormConfig frmConfig;
        public FormConfigAgent()
        {
            frmConfig = new FormConfig();
        }

        
        #region IConfigUI Members

        public System.Windows.Forms.Control GetControl()
        {
            return frmConfig.ConfigPanel;
        }

        public bool LoadConfig()
        {
            frmConfig.LoadConfig();
            return true;
        }

        public bool SaveConfig()
        {
            frmConfig.SaveConfig();
            return true;
        }

        public string FileName
        {
            get { return SQLOutAdapterConfigMgt._FileName; }
        }

        public string Name
        {
            get { return frmConfig.Text; }
        }

        #endregion
    }
}
