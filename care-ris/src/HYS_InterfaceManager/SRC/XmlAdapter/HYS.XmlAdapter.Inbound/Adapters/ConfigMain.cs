using System;
using System.Text;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.XmlAdapter.Common;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Inbound.Objects;
using HYS.XmlAdapter.Inbound.Forms;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Inbound.Adapters
{
    [AdapterConfigEntry("XIM(HL7) Inbound Adapter Configuration", DirectionType.INBOUND, "XIM(HL7) inbound adapter for GC Gateway 2.0")]
    public class ConfigMain : IInboundAdapterConfig
    {
        #region IInboundAdapterConfig Members

        public IInboundRule[] GetRules()
        {
            List<IInboundRule> ruleList = new List<IInboundRule>();

            //rules have been prepared in XIMInboundConfigMgt.Save().
            foreach (XIMInboundMessage msg in Program.ConfigMgt.Config.Messages)
            {
                ruleList.Add(msg.Rule);
            }

            return ruleList.ToArray();
        }

        #endregion

        #region IAdapterConfig Members

        public IConfigUI[] GetConfigUI()
        {
            return new IConfigUI[] { new FormConfig() };
        }

        public string GetConfigurationSummary()
        {
            StringBuilder sb = new StringBuilder();
            if (Program.ConfigMgt.Config.InboundFromFile)
            {
                sb.Append("Input Path:").Append(Program.ConfigMgt.Config.DirectoryConfig.SourcePath).Append("; ");
                sb.Append("Timer:").Append(Program.ConfigMgt.Config.DirectoryConfig.TimerInterval.ToString()).Append("; ");
            }
            else
            {
                sb.Append("Listening Port:").Append(Program.ConfigMgt.Config.SocketConfig.Port.ToString()).Append("; ");
            }
            return sb.ToString();
        }

        public LookupTable[] GetLookupTables()
        {
            return null;
        }

        public AdapterOption Option
        {
            get { return new AdapterOption(DirectionType.INBOUND); }
        }

        #endregion

        #region IAdapterBase Members

        public bool Exit(string[] arguments)
        {
            Program.BeforeExit();
            return true;
        }

        public bool Initialize(string[] arguments)
        {
            Program.PreLoading();
            Program.Log.Write(Program.AppName + " is running in Adapter.Config host.");

            if (arguments != null && arguments.Length > 0)
            {
                string str = arguments[0];
                //Program.Log.Write("GWDataDB connection: " + str); //contains db pw
                Program.ConfigMgt.Config.GWDataDBConnection = str;
            }

            return true;
        }

        public Exception LastError
        {
            get { return null; }
        }

        #endregion
    }
}
