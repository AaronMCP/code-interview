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
using HYS.SQLOutboundAdapterObjects;
using HYS.SQLOutboundAdapterConfiguration;

namespace SQLOutboundAdapterConfiguration.Adapter
{
    [AdapterConfigEntry("SQL Outbound Adapter Configuration", DirectionType.OUTBOUND, "SQL Outbound Adapter for GC Gateway")]
    public class ConfigMain : IOutboundAdapterConfig
    {

        private SQLOutboundConfiguration frm;

        #region IInboundAdapterConfig Members
        public HYS.Common.Objects.Rule.IOutboundRule[] GetRules()
        {
            if (SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Active)
            {
                List<IOutboundRule> rules = new List<IOutboundRule>();
                foreach (SQLOutboundChanel ch0 in SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels)
                {
                    SQLOutboundChanel ch = ch0.Clone();
                    ch.Rule.RuleID = ch0.Rule.RuleID;

                    if (!IsExist(ch, GWDataDBField.i_IndexGuid.FieldName))
                    {
                        InsertDataID(ch);
                    }
                    if (!IsExist(ch, GWDataDBField.i_DataDateTime.FieldName))
                    {
                        InsertDataDT(ch);
                    }

                    rules.Add(ch.Rule);
                }
                return rules.ToArray();
            }
            else {
                CreatPassiveSPScript(SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundPassiveChanels);
                return new IOutboundRule[] { };
            }
        }
        #endregion

        #region IAdapterConfig Members

        public IConfigUI[] GetConfigUI()
        {
            frm = new SQLOutboundConfiguration();

            return new IConfigUI[] { frm };
        }

        public string GetConfigurationSummary()
        {
            StringBuilder sb = new StringBuilder();
            if (SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Active)
            {
                sb.Append("Active; ");
                sb.Append("DataSource:").Append(SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.Server).Append("; ");
                if (SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.TimerEnable)
                {
                    sb.Append("Timer:").Append(SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.TimerInterval.ToString()).Append("ms; ");
                }
                else
                {
                    sb.Append("Timer:Disable; ");
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
            get { return new AdapterOption(DirectionType.OUTBOUND); }
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
        public void CreatPassiveSPScript(XCollection<SQLOutboundChanel> channelSet)
        {
            string interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
            string fnameInstall = Application.StartupPath + "\\" + RuleScript.InstallSP.FileName;
            string fnameUninstall = Application.StartupPath + "\\" + RuleScript.UninstallSP.FileName;

            //Install
            //using (StreamWriter sw = File.CreateText(fnameInstall))   // 20110706 OSQL & SQLMgmtStudio only support ASCII and UNICODE
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(GWDataDB.GetUseDataBaseSql());
                foreach (SQLOutboundChanel channel in channelSet)
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
                foreach (SQLOutboundChanel channel in channelSet)
                {
                    string strSql = RuleControl.GetOutboundSPUninstall(interfaceName, channel.Rule);
                    sb.AppendLine(strSql);
                }
                sw.Write(sb.ToString());
            }
        }
        #endregion

        #region Insert DataID & DataTD
        private void InsertDataID(SQLOutboundChanel chn)
        {
            SQLOutQueryResultItem item = new SQLOutQueryResultItem();

            item.ThirdPartyDBPatamter.FieldName = "";
            item.TargetField = "data_id";
            item.GWDataDBField = GWDataDBField.i_IndexGuid;
            item.SourceField = item.GWDataDBField.FieldName;

            chn.Rule.QueryResult.MappingList.Add(item);
        }

        private void InsertDataDT(SQLOutboundChanel chn)
        {
            SQLOutQueryResultItem item = new SQLOutQueryResultItem();

            item.ThirdPartyDBPatamter.FieldName = "";
            item.TargetField = "data_dt";
            item.GWDataDBField = GWDataDBField.i_DataDateTime;
            item.SourceField = item.GWDataDBField.FieldName;

            chn.Rule.QueryResult.MappingList.Add(item);
        }

        private bool IsExist(SQLOutboundChanel ch, string fieldname)
        {
            foreach (SQLOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if (item.TargetField == fieldname)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
