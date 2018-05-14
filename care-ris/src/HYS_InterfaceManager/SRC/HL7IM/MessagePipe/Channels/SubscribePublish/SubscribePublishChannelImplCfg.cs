﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.MessageDevices.MessagePipe.Base;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Common.Logging;

namespace HYS.MessageDevices.MessagePipe.Channels.SubscribePublish
{
    public class SubscribePublishChannelImplCfg : IChannelConfig
    {
        private ConfigurationInitializationParameter _param;

        private void WriteLog(Exception err)
        {
            _param.Log.Write(err);
        }
        private void WriteLog(LogType t, string logMsg)
        {
            _param.Log.Write(t, string.Format("[{0}]: {1}",
                SubscribePublishChannelConfig.DEVICE_NAME, logMsg));
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
            FormSubscribePublishChannelConfig frm = new FormSubscribePublishChannelConfig(_param, null);
            if (frm.ShowDialog(parentForm) != DialogResult.OK) return false;
            configXmlString = frm.Config.ToXMLString();
            return true;
        }

        public bool EditConfig(Form parentForm, ref string configXmlString)
        {
            SubscribePublishChannelConfig cfg = XObjectManager.CreateObject<SubscribePublishChannelConfig>(configXmlString);
            if (cfg == null)
            {
                WriteLog(LogType.Error, "Deserialize configuration object failed.");
                return false;
            }
            else
            {
                FormSubscribePublishChannelConfig frm = new FormSubscribePublishChannelConfig(_param, cfg);
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
