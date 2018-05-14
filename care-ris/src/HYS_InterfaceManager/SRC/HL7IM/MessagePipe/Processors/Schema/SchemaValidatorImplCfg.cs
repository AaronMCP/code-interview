using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Logging;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.MessageDevices.MessagePipe.Base.Processor;
using HYS.Messaging.Objects;
using HYS.Common.Xml;
using System.Windows.Forms;

namespace HYS.MessageDevices.MessagePipe.Processors.Schema
{
    public class SchemaValidatorImplCfg : IProcessorConfig
    {
        private ConfigurationInitializationParameter _param;

        private void WriteLog(Exception err)
        {
            _param.Log.Write(err);
        }
        private void WriteLog(LogType t, string logMsg)
        {
            _param.Log.Write(t, string.Format("[{0}]: {1}",
                SchemaValidatorConfig.DEVICE_NAME, logMsg));
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
            FormSchemaValidatorConfig frm = new FormSchemaValidatorConfig(_param, null);
            if (frm.ShowDialog(parentForm) != DialogResult.OK) return false;
            configXmlString = frm.Config.ToXMLString();
            return true;
        }

        public bool EditConfig(Form parentForm, ref string configXmlString)
        {
            SchemaValidatorConfig cfg = XObjectManager.CreateObject<SchemaValidatorConfig>(configXmlString);
            if (cfg == null)
            {
                WriteLog(LogType.Error, "Deserialize configuration object failed.");
                return false;
            }
            else
            {
                FormSchemaValidatorConfig frm = new FormSchemaValidatorConfig(_param, cfg);
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
