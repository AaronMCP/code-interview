using System;
using System.Collections.Generic;
using System.Text;
using HYS.Adapter.Base;
using DemoAdapter.Controlers;

namespace DemoAdapter.Configuration
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
            get { return "DemoAdapter.exe.config"; }
        }

        public string Name
        {
            get { return "DemoAdapter Configuration"; }
        }

        #endregion
    }
}
