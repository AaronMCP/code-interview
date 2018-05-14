using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.Common.Xml;
using HYS.Common.Logging;
using System.Windows.Forms;

namespace HYS.MessageDevices.MessagePipe.Channels.RequestResponse
{
    public class RequestResponseChannelImplCfg : IChannelConfig
    {
        private ConfigurationInitializationParameter _param;

        private void WriteLog(Exception err)
        {
            _param.Log.Write(err);
        }
        private void WriteLog(LogType t, string logMsg)
        {
            _param.Log.Write(t, string.Format("[{0}]: {1}",
                RequestResponseChannelConfig.DEVICE_NAME, logMsg));
        }

        #region IChannelConfig Members

        public bool Initialize(ConfigurationInitializationParameter parameter)
        {
            if (parameter == null) return false;
            _param = parameter;
            return true;
        }

        public bool CreateConfig(Form parentForm, out string configXmlString)
        {
            configXmlString = "";
            FormRequestResponseChannelConfig frm = new FormRequestResponseChannelConfig(_param, null);
            if (frm.ShowDialog(parentForm) != DialogResult.OK) return false;
            configXmlString = frm.Config.ToXMLString();
            return true;
        }

        public bool EditConfig(Form parentForm, ref string configXmlString)
        {
            RequestResponseChannelConfig cfg = XObjectManager.CreateObject<RequestResponseChannelConfig>(configXmlString);
            if (cfg == null)
            {
                WriteLog(LogType.Error, "Deserialize configuration object failed.");
                return false;
            }
            else
            {
                FormRequestResponseChannelConfig frm = new FormRequestResponseChannelConfig(_param, cfg);
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
