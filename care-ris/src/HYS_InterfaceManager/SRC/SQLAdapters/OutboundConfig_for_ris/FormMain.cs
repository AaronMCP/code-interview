using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Config;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Objects.Device;

namespace OutboundConfig_for_ris
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        internal Panel ConfigPanel
        {
            get
            {
                this.panelMain.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                return this.panelMain;
            }
        }

        internal void LoadConfig()
        {
            string FileName = Application.StartupPath + "\\" + SQLOutAdapterConfigMgt._FileName;
            if (!SQLOutAdapterConfigMgt.Load(FileName))
            {
                MessageBox.Show("Load configuration failed. \r\n" + FileName + "\r\n", "SQLOutboundAdapter");
            }

            this.checkBoxTimerEnable.Checked = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.TimerEnable;
            this.numericUpDownInterval.Value = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.TimerInterval;

            this.textBoxIName.Text = Properties.Settings.Default.InterfaceName;
            this.textBoxRISDBName.Text = Properties.Settings.Default.RISDBName;
            this.textBoxTP.Text = Properties.Settings.Default.RISTablePrefix;
            this.textBoxInstance.Text = Properties.Settings.Default.RISDBInstanceName;
            this.textBoxRISOrderSPMultiSiteSuffix.Text = Properties.Settings.Default.RISOrderSPMultiSiteSuffix;

            this.textBoxRISUser.Text = Properties.Settings.Default.RISDBUser;
            this.textBoxRISPwd.Text = Properties.Settings.Default.RISDBPassword;
            this.checkBoxWindowsAnthen.Checked = Properties.Settings.Default.RISDBWindowsAuthentication;

            this.checkBoxAutoInstall.Checked = Properties.Settings.Default.AutoInstall;

            this.textBoxBrokerDBName.Text = Properties.Settings.Default.BrokerDBName;
            this.textBoxBrokerDBPwd.Text = Properties.Settings.Default.BrokerDBPassword;
            this.textBoxBrokerDBUser.Text = Properties.Settings.Default.BrokerDBUser;
            this.textBoxBrokerServer.Text = Properties.Settings.Default.BrokerInstanceName;
            this.checkBoxBrokerWindowsAnthen.Checked = Properties.Settings.Default.BrokerDBWindowsAuthentication;

            this.textBoxDBCnn.Text = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.ConnectionStr;
        }

        internal void SaveConfig()
        {
            LoadDefaultSetting();

            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.TimerEnable = this.checkBoxTimerEnable.Checked;
            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.TimerInterval = (int)this.numericUpDownInterval.Value;
            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.ConnectionStr = this.textBoxDBCnn.Text.Trim();

            string FileName = Application.StartupPath + "\\" + SQLOutAdapterConfigMgt._FileName;
            if (!SQLOutAdapterConfigMgt.Save(FileName))
            {
                MessageBox.Show("Save configuration failed. \r\n" + FileName + "\r\n", "SQLOutboundAdapter");
            }

            Properties.Settings.Default.RISDBPassword = this.textBoxRISPwd.Text;
            Properties.Settings.Default.RISDBUser = this.textBoxRISUser.Text.Trim();
            Properties.Settings.Default.RISDBWindowsAuthentication = this.checkBoxWindowsAnthen.Checked;

            Properties.Settings.Default.RISDBInstanceName = this.textBoxInstance.Text.Trim();
            Properties.Settings.Default.InterfaceName = this.textBoxIName.Text.Trim();
            Properties.Settings.Default.RISDBName = this.textBoxRISDBName.Text.Trim();
            Properties.Settings.Default.RISTablePrefix = this.textBoxTP.Text.Trim();
            Properties.Settings.Default.RISOrderSPMultiSiteSuffix = this.textBoxRISOrderSPMultiSiteSuffix.Text.Trim();

            Properties.Settings.Default.AutoInstall = this.checkBoxAutoInstall.Checked;

            Properties.Settings.Default.BrokerDBName = this.textBoxBrokerDBName.Text.Trim();
            Properties.Settings.Default.BrokerDBPassword = this.textBoxBrokerDBPwd.Text;
            Properties.Settings.Default.BrokerDBUser = this.textBoxBrokerDBUser.Text.Trim();
            Properties.Settings.Default.BrokerInstanceName = this.textBoxBrokerServer.Text.Trim();
            Properties.Settings.Default.BrokerDBWindowsAuthentication = this.checkBoxBrokerWindowsAnthen.Checked;

            Properties.Settings.Default.Save();

            CreateTableScript();
            CreateSPScript();

            UpdateDeviceDir();
        }

        private void UpdateDeviceDir()
        {
            string fileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;
            DeviceDirManager mgt = new DeviceDirManager(fileName);
            if (!mgt.LoadDeviceDir())
            {
                MessageBox.Show(this, "Load devicedir file failed. \r\n" + fileName);
                return;
            }
            mgt.DeviceDirInfor.Header.Name = this.textBoxTP.Text;
            if (!mgt.SaveDeviceDir())
            {
                MessageBox.Show(this, "Save devicedir file failed. \r\n" + fileName);
                return;
            }
        }

        private void LoadDefaultSetting()
        {
            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels.Clear();

            string inboundName = this.textBoxIName.Text.Trim();
            string tablePrefix = this.textBoxTP.Text;

            SQLOutboundChanel chnPatient = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels.Add(new SQLOutboundChanel());
            chnPatient.ChannelName = GetSPName(inboundName, GWDataDBTable.Patient);
            chnPatient.OperationName = GetSPName(inboundName, GWDataDBTable.Patient);
            chnPatient.OperationType = ThrPartyDBOperationType.StorageProcedure;
            chnPatient.Rule.RuleName = "sp_" + tablePrefix + "_Patient";
            chnPatient.Rule.RuleID = "Patient";
            chnPatient.Rule.CheckProcessFlag = true;
            chnPatient.Enable = true;

            //LoadEventTypeSetting(chnPatient, tablePrefix, new string[] { "00", "01", "02", "03" });
            LoadEventTypeSetting(chnPatient, tablePrefix, this.textBoxEventTypePatient.Text.Trim().Split(','));
            LoadTableSetting(chnPatient, GWDataDBTable.Index, inboundName, tablePrefix);
            LoadTableSetting(chnPatient, GWDataDBTable.Patient, inboundName, tablePrefix);

            SQLOutboundChanel chnOrder = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels.Add(new SQLOutboundChanel());
            chnOrder.ChannelName = GetSPName(inboundName, GWDataDBTable.Order);
            chnOrder.OperationName = GetSPName(inboundName, GWDataDBTable.Order);
            chnOrder.OperationType = ThrPartyDBOperationType.StorageProcedure;
            chnOrder.Rule.RuleName = "sp_" + tablePrefix + "_Order";
            chnOrder.Rule.RuleID = "Order" + this.textBoxRISOrderSPMultiSiteSuffix.Text.Trim();
            chnOrder.Rule.CheckProcessFlag = true;
            chnOrder.Enable = true;

            //LoadEventTypeSetting(chnOrder, tablePrefix, new string[] { "10", "11", "12", "13", "20", "21", "22", "23" });
            LoadEventTypeSetting(chnOrder, tablePrefix, this.textBoxEventTypeOrder.Text.Trim().Split(','));
            LoadTableSetting(chnOrder, GWDataDBTable.Index, inboundName, tablePrefix);
            LoadTableSetting(chnOrder, GWDataDBTable.Patient, inboundName, tablePrefix);
            LoadTableSetting(chnOrder, GWDataDBTable.Order, inboundName, tablePrefix);

            SQLOutboundChanel chnReport = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels.Add(new SQLOutboundChanel());
            chnReport.ChannelName = GetSPName(inboundName, GWDataDBTable.Report);
            chnReport.OperationName = GetSPName(inboundName, GWDataDBTable.Report);
            chnReport.OperationType = ThrPartyDBOperationType.StorageProcedure;
            chnReport.Rule.RuleName = "sp_" + tablePrefix + "_Report";
            chnReport.Rule.RuleID = "Report";
            chnReport.Rule.CheckProcessFlag = true;
            chnReport.Enable = true;

            //LoadEventTypeSetting(chnReport, tablePrefix, new string[] { "30", "31", "32", "33" });
            LoadEventTypeSetting(chnReport, tablePrefix, this.textBoxEventTypeReport.Text.Trim().Split(','));
            LoadTableSetting(chnReport, GWDataDBTable.Index, inboundName, tablePrefix);
            LoadTableSetting(chnReport, GWDataDBTable.Patient, inboundName, tablePrefix);
            LoadTableSetting(chnReport, GWDataDBTable.Order, inboundName, tablePrefix);
            LoadTableSetting(chnReport, GWDataDBTable.Report, inboundName, tablePrefix);
        }

        private void LoadAdapterServiceCfg()
        {
            AdapterServiceCfgMgt mgt = new AdapterServiceCfgMgt();
            mgt.FileName = Application.StartupPath + "\\" + mgt.FileName;
            if (mgt.Load())
            {
                this.textBoxRISDBCnn.Text = mgt.Config.DataDBConnection;
            }
            else
            {
                MessageBox.Show(this, "Load Adapter Service configuration failed.", "Warning");
            }
        }

        private void LoadEventTypeSetting(SQLOutboundChanel chn, string tablePrefix, string[] eventTypes)
        {
            chn.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            foreach (string str in eventTypes)
            {
                SQLOutQueryCriteriaItem item = new SQLOutQueryCriteriaItem();
                item.TargetField = GWDataDBField.i_EventType.GetFullFieldName(tablePrefix);
                item.GWDataDBField = GWDataDBField.i_EventType;

                ThrPartyDBParamter p = new ThrPartyDBParamter();
                p.FieldType = System.Data.OleDb.OleDbType.Empty;
                item.ThirdPartyDBPatamter = p;
                item.SourceField = "";

                item.Translating.Type = TranslatingType.FixValue;
                item.Translating.ConstValue = str;

                item.Type = QueryCriteriaType.Or;

                chn.Rule.QueryCriteria.MappingList.Add(item);
            }

        }

        private void LoadTableSetting(SQLOutboundChanel chn, GWDataDBTable table, string inboundName, string tablePrefix)
        {
            GWDataDBField[] iFields = GWDataDBField.GetFields(table);
            foreach (GWDataDBField f in iFields)
            {
                if (f.IsAuto) continue;
                string paramName = f.GetFullFieldName(inboundName).Replace(".", "_");
                SQLOutQueryResultItem item = new SQLOutQueryResultItem();

                item.SourceField = f.GetFullFieldName(tablePrefix);
                item.GWDataDBField = f;

                item.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                item.ThirdPartyDBPatamter.FieldName = paramName;
                item.TargetField = paramName;

                chn.Rule.QueryResult.MappingList.Add(item);
            }
        }

        private void InsertDataId(SQLOutboundChanel chn)
        {
            SQLOutQueryResultItem item = new SQLOutQueryResultItem();

            item.ThirdPartyDBPatamter.FieldName = "";
            item.TargetField = "data_id";
            item.GWDataDBField = GWDataDBField.i_IndexGuid;
            item.SourceField = item.GWDataDBField.FieldName;

            chn.Rule.QueryResult.MappingList.Add(item);
        }

        private void RefreshTableName()
        {
            string interfaceName = this.textBoxTP.Text;
            this.textBoxTName.Text = GWDataDB.GetTableName(interfaceName, GWDataDBTable.Index)
                + "," + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Patient)
                + "," + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Order)
                + "," + GWDataDB.GetTableName(interfaceName, GWDataDBTable.Report);
        }

        private void RefreshSPName()
        {
            string interfaceName = this.textBoxIName.Text.Trim();
            this.textBoxSPName.Text = GetSPName(interfaceName, GWDataDBTable.Patient)
                + "," + GetSPName(interfaceName, GWDataDBTable.Order)
                + "," + GetSPName(interfaceName, GWDataDBTable.Report);
        }

        private string GetSPName(string interfaceName, GWDataDBTable table)
        {
            switch (table)
            {
                case GWDataDBTable.Patient: return "sp_" + interfaceName + "_Patient";
                case GWDataDBTable.Order: return "sp_" + interfaceName + "_Order";
                case GWDataDBTable.Report: return "sp_" + interfaceName + "_Report";
                default: return "";
            }
        }

        private void CreateTableScript()
        {
            string dbName = this.textBoxRISDBName.Text.Trim();
            string interfaceName = this.textBoxTP.Text.Trim();
            string fnameInstall = Application.StartupPath + "\\" + RuleScript.InstallTable.FileName;
            string fnameUninstall = Application.StartupPath + "\\" + RuleScript.UninstallTable.FileName;

            GarbageRule gcRule = null;
            string serviceCfgFile = Application.StartupPath + "\\" + ConfigHelper.ServiceDefaultFileName;
            AdapterServiceCfgMgt mgt = new AdapterServiceCfgMgt(serviceCfgFile);
            if (mgt.Load())
            {
                gcRule = mgt.Config.GarbageCollection;
                //US29442
                #region
                //gcRule.MaxRecordCountLimitation = 500;
                #endregion
            }
            else
            {
                MessageBox.Show(this, "Cannot open file : " + serviceCfgFile + "\r\n" + mgt.LastErrorInfor,
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            using (StreamWriter sw = File.CreateText(fnameInstall))
            {
                string strSql = RuleControl.GetCreateTableSQL(interfaceName, gcRule);
                strSql = strSql.Replace("USE GWDataDB", "USE " + dbName);
                sw.Write(strSql);
            }
            using (StreamWriter sw = File.CreateText(fnameUninstall))
            {
                string strSql = RuleControl.GetDropTableSQL(interfaceName);
                strSql = strSql.Replace("USE GWDataDB", "USE " + dbName);
                sw.Write(strSql);
            }
        }

        private void CreateSPScript()
        {
            string dbName = this.textBoxRISDBName.Text.Trim();
            string interfaceName = this.textBoxTP.Text.Trim();
            string fnameInstall = Application.StartupPath + "\\" + RuleScript.InstallSP.FileName;
            string fnameUninstall = Application.StartupPath + "\\" + RuleScript.UninstallSP.FileName;
            using (StreamWriter sw = File.CreateText(fnameInstall))
            {
                StringBuilder sb = new StringBuilder();
                foreach (SQLOutboundChanel chn in SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels)
                {
                    InsertDataId(chn);
                    string strSql = RuleControl.GetOutboundSP(interfaceName, chn.Rule);
                    strSql = strSql.Replace("USE GWDataDB", "");
                    sb.AppendLine(strSql);
                }
                sw.Write("USE " + dbName + "\r\n" + sb.ToString());
            }
            using (StreamWriter sw = File.CreateText(fnameUninstall))
            {
                StringBuilder sb = new StringBuilder();
                foreach (SQLOutboundChanel chn in SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels)
                {
                    string strSql = RuleControl.GetOutboundSPUninstall(interfaceName, chn.Rule);
                    strSql = strSql.Replace("USE GWDataDB", "");
                    sb.AppendLine(strSql);
                }
                sw.Write("USE " + dbName + "\r\n" + sb.ToString());
            }
        }

        private void UpdateCreateSPBat()
        {
            bool trusted = this.checkBoxWindowsAnthen.Checked;
            string instance = this.textBoxInstance.Text.Trim();
            string databse = this.textBoxRISDBName.Text.Trim();
            string user = this.textBoxRISUser.Text.Trim();
            string pwd = this.textBoxRISPwd.Text;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("echo off");
            sb.AppendLine("");
            sb.AppendLine("echo Making Temp Dir");
            sb.AppendLine("mkdir C:\\temp");
            sb.AppendLine("");
            sb.AppendLine("echo Set Osql Path");
            sb.AppendLine("set path=osql;%path%");
            sb.AppendLine("");
            sb.AppendLine("echo Creating Table and GC Storage Procedure");
            if (trusted)
            {
                sb.AppendLine("osql -S " + instance + " -d " + databse + " -E -i InstallTable.sql > C:\\temp\\GCRisTableInstall.log");
            }
            else
            {
                sb.AppendLine("osql -S " + instance + " -d " + databse + " -U " + user + " -P " + pwd + " -i InstallTable.sql > C:\\temp\\GCRisTableInstall.log");
            }
            sb.AppendLine("");
            sb.AppendLine("echo Dropping Old Storage Procedure");
            if (trusted)
            {
                sb.AppendLine("osql -S " + instance + " -d " + databse + " -E -i UninstallSP.sql > C:\\temp\\GCRisSPUninstall.log");
            }
            else
            {
                sb.AppendLine("osql -S " + instance + " -d " + databse + " -U " + user + " -P " + pwd + " -i UninstallSP.sql > C:\\temp\\GCRisSPUninstall.log");
            }
            sb.AppendLine("");
            sb.AppendLine("echo Creating New Storage Procedure");
            if (trusted)
            {
                sb.AppendLine("osql -S " + instance + " -d " + databse + " -E -i InstallSP.sql > C:\\temp\\GCRisSPInstall.log");
            }
            else
            {
                sb.AppendLine("osql -S " + instance + " -d " + databse + " -U " + user + " -P " + pwd + " -i InstallSP.sql > C:\\temp\\GCRisSPInstall.log");
            }
            sb.AppendLine("");
            sb.AppendLine("echo DONE");
            string str = sb.ToString();

            using(StreamWriter sw = File.CreateText("InstallRISSP.bat"))
            {
                sw.Write(str);
            }
        }

        private void UpdateAdapterServiceCfg()
        {
            AdapterServiceCfgMgt mgt = new AdapterServiceCfgMgt();
            mgt.FileName = Application.StartupPath + "\\" + mgt.FileName;
            if (mgt.Load())
            {
                ReplacementRuleItem itemComments = new ReplacementRuleItem(GWDataDBField.r_COMMENTS);
                itemComments.RegularExpression.Expression = @"[\n]";
                itemComments.RegularExpression.Replacement = "\r\n";

                ReplacementRuleItem itemDiagnosis = new ReplacementRuleItem(GWDataDBField.r_DIAGNOSE);
                itemDiagnosis.RegularExpression.Expression = @"[\n]";
                itemDiagnosis.RegularExpression.Replacement = "\r\n";

                mgt.Config.Replacement.Enable = true;
                mgt.Config.Replacement.Fields.Add(itemComments);
                mgt.Config.Replacement.Fields.Add(itemDiagnosis);

                mgt.Config.DataDBConnection = this.textBoxRISDBCnn.Text.Trim();
                if (!mgt.Save())
                {
                    MessageBox.Show(this, "Save Adapter Service configuration failed.", "Warning");
                }
            }
            else
            {
                MessageBox.Show(this, "Load Adapter Service configuration failed.", "Warning");
            }
        }

        private void RunInstallation()
        {
            ProcessStartInfo siInstallSP = new ProcessStartInfo();
            siInstallSP.FileName = Application.StartupPath + "\\InstallRISSP.bat";
            siInstallSP.WorkingDirectory = Application.StartupPath;
            Process procInstallSP = Process.Start(siInstallSP);
            procInstallSP.WaitForExit();

            ProcessStartInfo siInstallNTService = new ProcessStartInfo();
            siInstallNTService.FileName = Application.StartupPath + "\\InstallService.bat";
            siInstallNTService.WorkingDirectory = Application.StartupPath;
            Process procInstallNTService = Process.Start(siInstallNTService);
            procInstallNTService.WaitForExit();

            ProcessStartInfo siStartNTService = new ProcessStartInfo();
            siStartNTService.FileName = Application.StartupPath + "\\netstart.bat";
            siStartNTService.WorkingDirectory = Application.StartupPath;
            Process procStartNTService = Process.Start(siStartNTService);
            procStartNTService.WaitForExit();
        }

        private void RefreshRISConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Provider=SQLNCLI;Data Source=").Append(this.textBoxInstance.Text.Trim())
            .Append(";Database=").Append(this.textBoxRISDBName.Text.Trim());
            if (this.checkBoxWindowsAnthen.Checked)
            {
                sb.Append(";Trusted_Connection=Yes;");
            }
            else
            {
                sb.Append(";User ID=").Append(this.textBoxRISUser.Text.Trim())
                .Append(";Password=").Append(this.textBoxRISPwd.Text).Append(";");
            }
            this.textBoxRISDBCnn.Text = sb.ToString();
        }

        private void RefreshBrokerConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Provider=SQLNCLI;Data Source=").Append(this.textBoxBrokerServer.Text.Trim())
            .Append(";Database=").Append(this.textBoxBrokerDBName.Text.Trim());
            if (this.checkBoxBrokerWindowsAnthen.Checked)
            {
                sb.Append(";Trusted_Connection=Yes;");
            }
            else
            {
                sb.Append(";User ID=").Append(this.textBoxBrokerDBUser.Text.Trim())
                .Append(";Password=").Append(this.textBoxBrokerDBPwd.Text).Append(";");
            }
            this.textBoxDBCnn.Text = sb.ToString();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            LoadConfig();
            LoadAdapterServiceCfg();
            RefreshSPName();
            RefreshTableName();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveConfig();
            UpdateCreateSPBat();
            UpdateAdapterServiceCfg();

            if (this.checkBoxAutoInstall.Checked)
            {
                RunInstallation();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBoxTP_TextChanged(object sender, EventArgs e)
        {
            RefreshTableName();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataBase db = new DataBase(this.textBoxDBCnn.Text);
            bool res = db.TestDBConnection();
            this.Cursor = Cursors.Default;

            if (res)
            {
                MessageBox.Show("Test database connection succeeded.");
            }
            else
            {
                MessageBox.Show("Test database connection failed.\r\n\r\n" + db.LastError.Message);
            }
        }

        private void textBoxIName_TextChanged(object sender, EventArgs e)
        {
            RefreshSPName();
        }

        private void buttonRISDBCnn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataBase db = new DataBase(this.textBoxRISDBCnn.Text);
            bool res =db.TestDBConnection();
            this.Cursor = Cursors.Default;

            if (res)
            {
                MessageBox.Show("Test database connection succeeded.");
            }
            else
            {
                MessageBox.Show("Test database connection failed.\r\n\r\n" + db.LastError.Message);
            }
        }

        private void checkBoxWindowsAnthen_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = !this.checkBoxWindowsAnthen.Checked;
            this.textBoxRISUser.Enabled = flag;
            this.textBoxRISPwd.Enabled = flag;
            RefreshRISConnectionString();
        }

        private void checkBoxBrokerWindowsAnthen_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = !this.checkBoxBrokerWindowsAnthen.Checked;
            this.textBoxBrokerDBUser.Enabled = flag;
            this.textBoxBrokerDBPwd.Enabled = flag;
            RefreshBrokerConnectionString();
        }

        private void textBoxInstance_TextChanged(object sender, EventArgs e)
        {
            RefreshRISConnectionString();
        }

        private void textBoxRISDBName_TextChanged(object sender, EventArgs e)
        {
            RefreshRISConnectionString();
        }

        private void textBoxRISUser_TextChanged(object sender, EventArgs e)
        {
            RefreshRISConnectionString();
        }

        private void textBoxRISPwd_TextChanged(object sender, EventArgs e)
        {
            RefreshRISConnectionString();
        }

        private void textBoxBrokerServer_TextChanged(object sender, EventArgs e)
        {
            RefreshBrokerConnectionString();
        }

        private void textBoxBrokerDBName_TextChanged(object sender, EventArgs e)
        {
            RefreshBrokerConnectionString();
        }

        private void textBoxBrokerDBUser_TextChanged(object sender, EventArgs e)
        {
            RefreshBrokerConnectionString();
        }

        private void textBoxBrokerDBPwd_TextChanged(object sender, EventArgs e)
        {
            RefreshBrokerConnectionString();
        }

        private void textBoxEventTypeOrder_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEventTypeOrder.Text.Trim().Length < 1) textBoxEventTypeOrder.Text = "10,11,12,13,20,21,22,23,40,41,42,43";
        }

        private void textBoxEventTypePatient_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEventTypePatient.Text.Trim().Length < 1) textBoxEventTypePatient.Text = "00,01,02,03";
        }

        private void textBoxEventTypeReport_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEventTypeReport.Text.Trim().Length < 1) textBoxEventTypeReport.Text = "30,31,32,33";
        }
    }
}