using System;
using System.Collections.Generic;
using System.Text;
using HYS.Adapter.Base;
using HYS.Common.Objects;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Translation;
using HYS.SocketAdapter.Common;
using HYS.SocketAdapter.Configuration;

namespace HYS.SocketAdapter.SocketInboundAdapterConfiguration
{
    [AdapterConfigEntry("SOCKET_In_CONFIG", DirectionType.INBOUND, "Socket Inbound Adapter for GC Gateway")]
    public class ConfigMain : IInboundAdapterConfig
    {
        private FSocketInConfiguration frm;
                
        #region IInboundAdapterConfig Members

        public HYS.Common.Objects.Rule.IInboundRule[] GetRules()
        {
            List<IInboundRule> rules = new List<IInboundRule>();
            foreach (SocketInChannel ch in SocketInboundAdapterConfigMgt.SocketInAdapterConfig.InboundChanels)
            {
                rules.Add(ch.Rule);
            }

            return rules.ToArray();
        }

        #endregion

        #region IAdapterConfig Members

        public IConfigUI[] GetConfigUI()
        {
            frm = new FSocketInConfiguration();

            return new IConfigUI[] { frm };
        }

        public string GetConfigurationSummary()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Listening Port:").Append(SocketInboundAdapterConfigMgt.SocketInAdapterConfig.ListenServerSocketParams.ListenPort.ToString()).Append("; ");
            return sb.ToString();
        }

        public HYS.Common.Objects.Rule.LookupTable[] GetLookupTables()
        {
            List<LookupTable> lts = new List<LookupTable>();
            foreach (LookupTable table in SocketInboundAdapterConfigMgt.SocketInAdapterConfig.LookupTables)
                lts.Add(table);
            return lts.ToArray();
        }

        public AdapterOption Option
        {
            get { return new AdapterOption(DirectionType.INBOUND); }
        }

        #endregion

        #region IAdapterBase Members

        public bool Exit(string[] arguments)
        {
            return true;
        }

        public bool Initialize(string[] arguments)
        {
            Program.PreLoading();

            if (arguments != null && arguments.Length > 0)
            {
                string str = arguments[0];
                //Program.Log.Write("GWDataDB connection: " + str); //contains db pw
                Program.GWDataDBConnection = str;
            }

            return true;
        }

        public Exception LastError
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion
    }
}
