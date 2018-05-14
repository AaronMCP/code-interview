using System;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using HYS.Adapter.Base;
using HYS.Common.Xml;
using HYS.Common.DataAccess;
using HYS.Common.Objects;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.SQLInboundAdapterObjects;
using HYS.SQLInboundAdapterConfiguration;

namespace HYS.SQLInboundAdapterConfiguration.Adapter
{
    [AdapterConfigEntry("SQL Inbound Adapter Configuration", DirectionType.INBOUND, "SQL Inbound Adapter for GC Gateway")]
    public class ConfigMain : IInboundAdapterConfig 
    {
        private SQLInboundConfiguration frm;

        #region IInboundAdapterConfig Members
        public HYS.Common.Objects.Rule.IInboundRule[] GetRules()
        {
            if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Active)
            {
                List<IInboundRule> rules = new List<IInboundRule>();
                foreach (SQLInboundChanel ch in SQLInAdapterConfigMgt.SQLInAdapterConfig.InboundChanels)
                {
                    ch.Rule.GenerateInputParameterSPName(Program.DeviceMgt.DeviceDirInfor.Header.Name);
                    rules.Add(ch.Rule);
                }
                return rules.ToArray();
            }
            else {
                CreatPassiveSPScript(SQLInAdapterConfigMgt.SQLInAdapterConfig.InboundPassiveChanels);
                return new IInboundRule[] { };
            }
        }
        #endregion

        #region IAdapterConfig Members

        public IConfigUI[] GetConfigUI()
        {
            frm = new SQLInboundConfiguration();

            return new IConfigUI[] { frm };
        }

        public string GetConfigurationSummary()
        {
            StringBuilder sb = new StringBuilder();
            if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Active)
            {
                sb.Append("Active; ");
                sb.Append("DataSource:").Append(SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.Server).Append("; ");
                if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.TimerEnable)
                {
                    sb.Append("Timer:").Append(SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.TimerInterval.ToString()).Append("ms; ");
                }
                else
                {
                    sb.Append("Timer:Disable; ");
                }

                if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.OracleDriver)
                {
                    sb.Append("OraleDriver: true");
                }
                else
                {
                    sb.Append("OraleDriver: false");
                }
            }
            else
            {
                sb.Append("Passive;");
            }
            return sb.ToString();
        }

        public HYS.Common.Objects.Rule.LookupTable[] GetLookupTables()
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

        #region Creat Storage Procedure Script in passive mode
        public void CreatPassiveSPScript(XCollection<SQLInboundChanel> channelSet) {
            string interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
            string fnameInstall = Application.StartupPath + "\\" + RuleScript.InstallSP.FileName;
            string fnameUninstall = Application.StartupPath + "\\" + RuleScript.UninstallSP.FileName;

            //Install
            //using (StreamWriter sw = File.CreateText(fnameInstall))   // 20110706 OSQL & SQLMgmtStudio only support ASCII and UNICODE
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(GWDataDB.GetUseDataBaseSql());
                foreach (SQLInboundChanel channel in channelSet)
                {
                    sb.AppendLine(channel.SPStatement);
                }
                //sw.Write(sb.ToString());
                File.WriteAllText(fnameInstall, sb.ToString(), Encoding.Unicode);
            }

            //UnInstall
            using (StreamWriter sw = File.CreateText(fnameUninstall))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(GWDataDB.GetUseDataBaseSql());
                foreach (SQLInboundChanel channel in channelSet)
                {
                    string strSql = RuleControl.GetInboundSPUninstall(interfaceName, channel.Rule);
                    sb.AppendLine(strSql);
                }
                sw.Write(sb.ToString());
            }
        }
        #endregion
    }
}
