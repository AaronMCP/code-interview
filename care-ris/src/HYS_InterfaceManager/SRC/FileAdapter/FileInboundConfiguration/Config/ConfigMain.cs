using System;
using System.Collections.Generic;
using System.Text;
using HYS.Adapter.Base;
using HYS.Common.Objects;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Translation;
using HYS.FileAdapter.Common;
using HYS.FileAdapter.Configuration;

namespace HYS.FileAdapter.FileInboundAdapterConfiguration
{
    [AdapterConfigEntry("FILE_In_CONFIG", DirectionType.INBOUND, "File Inbound Adapter for GC Gateway")]
    public class ConfigMain : IInboundAdapterConfig
    {
        private FFileInConfig frm;
                
        #region IInboundAdapterConfig Members

        public HYS.Common.Objects.Rule.IInboundRule[] GetRules()
        {
            List<IInboundRule> rules = new List<IInboundRule>();
            foreach (FileInChannel ch in FileInboundAdapterConfigMgt.FileInAdapterConfig.InboundChanels)
            {
                rules.Add(ch.Rule);
            }

            return rules.ToArray();
        }

        #endregion

        #region IAdapterConfig Members

        public IConfigUI[] GetConfigUI()
        {
            frm = new FFileInConfig();

            return new IConfigUI[] { frm };
        }

        public string GetConfigurationSummary()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Output Path:").Append(FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.FilePath).Append("; ");
            if (FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.TimerEnable)
            {
                sb.Append("Timer:").Append(FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.TimerInterval.ToString()).Append("; ");
            }
            else
            {
                sb.Append("Timer:Disable; ");
            }
            return sb.ToString();
        }

        public HYS.Common.Objects.Rule.LookupTable[] GetLookupTables()
        {
            List<LookupTable> lts = new List<LookupTable>();
            foreach (LookupTable table in FileInboundAdapterConfigMgt.FileInAdapterConfig.LookupTables)
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
