using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Processor;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.Common.Logging;
using System.Windows.Forms;
using HYS.Common.Xml;

namespace HYS.MessageDevices.MessagePipe.Processors.XSLT
{
    public class XSLTImplCfg : IProcessorConfig
    {
        private ConfigurationInitializationParameter _param;

        private void WriteLog(Exception err)
        {
            _param.Log.Write(err);
        }
        private void WriteLog(LogType t, string logMsg)
        {
            _param.Log.Write(t, string.Format("[{0}]: {1}",
                XSLTConfig.DEVICE_NAME, logMsg));
        }

        #region IProcessorConfig Members

        public bool Initialize(ConfigurationInitializationParameter parameter)
        {
            if (parameter == null) return false;
            _param = parameter;
            return true;
        }

        public bool CreateConfig(Form parentForm, out string configXmlString)
        {
            configXmlString = "";
            FormXSLTConfig frm = new FormXSLTConfig(_param, null);
            if (frm.ShowDialog(parentForm) != DialogResult.OK) return false;
            configXmlString = frm.Config.ToXMLString();
            return true;
        }

        public bool EditConfig(Form parentForm, ref string configXmlString)
        {
            XSLTConfig cfg = XObjectManager.CreateObject<XSLTConfig>(configXmlString);
            if (cfg == null)
            {
                WriteLog(LogType.Error, "Deserialize configuration object failed.");
                return false;
            }
            else
            {
                FormXSLTConfig frm = new FormXSLTConfig(_param, cfg);
                if (frm.ShowDialog(parentForm) != DialogResult.OK) return false;
                configXmlString = frm.Config.ToXMLString();
                return true;
            }
        }

        public bool Uninitilize()
        {
            return true;
        }

        #endregion
    }
}
