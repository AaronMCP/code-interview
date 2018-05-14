using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.XmlAdapter.Common;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Outbound.Objects;
using HYS.XmlAdapter.Outbound.Forms;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Outbound.Adapters
{
    [AdapterConfigEntry("XIM(HL7) Outbound Adapter Configuration", DirectionType.OUTBOUND, "XIM(HL7) outbound adapter for GC Gateway 2.0")]
    public class ConfigMain : IOutboundAdapterConfig
    {
        public const string DATAID = "DATAID";
        public static void SetDataIDMapping(XIMOutboundMessage msg)
        {
            if (msg == null) return;
            XIMMappingItem item = new XIMMappingItem();
            item.GWDataDBField = GWDataDBField.i_IndexGuid;
            item.TargetField = DATAID;
            msg.Rule.QueryResult.MappingList.Add(item);
        }
        public static string GetGUID(DataSet dataSet)
        {
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                DataTable dt = dataSet.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    object o = dr[DATAID];
                    if (o != null) return o.ToString();
                }
            }
            return "";
        }

        public static void ClearFixValueMapping(XIMOutboundMessage msg)
        {
            if (msg == null) return;
            List<XIMMappingItem> list = new List<XIMMappingItem>();
            foreach (XIMMappingItem item in msg.Rule.QueryResult.MappingList)
            {
                if (item.Translating.Type == TranslatingType.FixValue)
                {
                    list.Add(item);
                }
            }
            foreach (XIMMappingItem item in list)
            {
                msg.Rule.QueryResult.MappingList.Remove(item);
            }
        }

        #region IOutboundAdapterConfig Members

        public IOutboundRule[] GetRules()
        {
            List<IOutboundRule> ruleList = new List<IOutboundRule>();

            //rules have been prepared in XIMOutboundConfigMgt.Save().
            foreach (XIMOutboundMessage msg in Program.ConfigMgt.Config.Messages)
            {
                SetDataIDMapping(msg);
                ClearFixValueMapping(msg);
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
            if (Program.ConfigMgt.Config.OutboundToFile)
            {
                sb.Append("Output Path:").Append(Program.ConfigMgt.Config.TargetPath).Append("; ");
            }
            else
            {
                sb.Append("Server IP:").Append(Program.ConfigMgt.Config.SocketConfig.IPAddress.ToString()).Append("; ");
                sb.Append("Server Port:").Append(Program.ConfigMgt.Config.SocketConfig.Port.ToString()).Append("; ");
            }
            sb.Append("Timer:").Append(Program.ConfigMgt.Config.DataCheckingInterval.ToString()).Append("; ");
            return sb.ToString();
        }

        public LookupTable[] GetLookupTables()
        {
            return null;
        }

        public AdapterOption Option
        {
            get { return new AdapterOption(DirectionType.OUTBOUND); }
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
