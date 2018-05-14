using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.Common.Xml;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Config.Controls
{
    public partial class AdapterInfoControl : UserControl, IConfigControl
    {
        public AdapterInfoControl()
        {
            InitializeComponent();

            if (Program.ConfigMgt.Config.AdapterDirection != DirectionType.INBOUND)
            {
                this.tabControlInterface.TabPages.Remove(this.tabPageEventType);
            }
        }

        #region IConfigControl Members

        public bool LoadConfig()
        {
            LoadAdapterInfor();

            DeviceDir dir = Program.DeviceMgt.DeviceDirInfor;

            this.textBoxIName.Text = dir.Header.Name;
            this.textBoxIType.Text = dir.Header.Type.ToString();
            this.textBoxIVersion.Text = dir.Header.Version;
            this.textBoxIDirection.Text = dir.Header.Direction.ToString();
            this.textBoxIDescription.Text = dir.Header.Description;

            this.listViewFile.Items.Clear();
            foreach (DeviceFile f in dir.Files)
            {
                ListViewItem item = this.listViewFile.Items.Add(f.Location);
                item.SubItems.Add(f.Backupable.ToString());
                item.Tag = f;
            }

            this.listViewCommand.Items.Clear();
            foreach (Command c in dir.Commands)
            {
                ListViewItem item = this.listViewCommand.Items.Add(c.Path);
                item.SubItems.Add(c.Argument);
                item.Tag = c;
            }

            RefreshEventType();

            return true;
        }

        public bool SaveConfig()
        {
            SaveAdapterInfor();

            if (Program.DeviceMgt.SaveDeviceDir())
            {
                if (Program.ConfigMgt.Config.AdapterDirection == DirectionType.INBOUND)
                {
                    if (UpdateConfigDBEventType())
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Update event type to database failed.");
                        return false;
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("Save DeviceDir file failed.");
                return false;
            }
        }

        #endregion

        #region Event type control

        private void AddEventType()
        {
            FormEventType frm = new FormEventType();
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            GWEventType e = frm.EventType;
            if (e == null) return;

            DeviceDir dir = Program.DeviceMgt.DeviceDirInfor;
            dir.Header.EventTypes.Add(e);

            RefreshEventType();
        }
        private void DeleteEventType()
        {
            if (this.listViewEventTypes.SelectedItems.Count < 1) return;

            GWEventType e = this.listViewEventTypes.SelectedItems[0].Tag as GWEventType;
            if (e == null) return;

            DeviceDir dir = Program.DeviceMgt.DeviceDirInfor;
            dir.Header.EventTypes.Remove(e);

            RefreshEventType();
        }
        private void RefreshEventType()
        {
            DeviceDir dir = Program.DeviceMgt.DeviceDirInfor;

            this.listViewEventTypes.Items.Clear();
            foreach (GWEventType e in dir.Header.EventTypes)
            {
                ListViewItem item = this.listViewEventTypes.Items.Add(e.Code);
                item.SubItems.Add(e.Description);
                item.Tag = e;
            }

            RefreshEventTypeButton();
        }
        private void RefreshEventTypeButton()
        {
            this.buttonEventTypeDelete.Enabled = this.listViewEventTypes.SelectedItems.Count > 0;
        }
        private bool UpdateConfigDBEventType()
        {
            if (!Program.InIM) return true;

            string interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
            string eventType = Program.DeviceMgt.DeviceDirInfor.Header.EventType;
            DataBase db = new DataBase(Program.ConfigMgt.Config.ConfigDBConnection);
            LoggingHelper.EnableDatabaseLogging(db, Program.Log);
            Program.Log.Write("Updating event type in GWConfigDB.");

            string sqlString = "UPDATE Interface SET EVENT_TYPE = N'" + eventType + "' WHERE INTERFACE_NAME = '" + interfaceName + "'";
            return db.ExecuteQuery(sqlString) != null;
        }

        private void buttonEventTypeAdd_Click(object sender, EventArgs e)
        {
            AddEventType();
        }
        private void buttonEventTypeDelete_Click(object sender, EventArgs e)
        {
            DeleteEventType();
        }
        private void listViewEventTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshEventTypeButton();
        }

        #endregion

        #region Adapter control

        private void LoadAdapterInfor()
        {
            this.textBoxAFileName.Text = Program.ConfigMgt.Config.AdapterFileName;

            AdapterEntryAttributeBase a = null;

            switch (Program.ConfigMgt.Config.AdapterDirection)
            {
                case DirectionType.INBOUND:
                    {
                        if (Program.InAdapter != null)
                        {
                            a = Program.InAdapter.Attribute;
                        }
                        break;
                    }
                case DirectionType.OUTBOUND:
                    {
                        if (Program.OutAdapter != null)
                        {
                            a = Program.OutAdapter.Attribute;
                        }
                        break;
                    }
                case DirectionType.BIDIRECTIONAL:
                    {
                        if (Program.BiAdapter != null)
                        {
                            a = Program.BiAdapter.Attribute;
                        }
                        break;
                    }
            }

            if (a != null)
            {
                this.textBoxAName.Text = a.Name;
                this.textBoxADirection.Text = a.Direction.ToString();
                this.textBoxADescription.Text = a.Description;
            }
        }
        private void SaveAdapterInfor()
        {
            Program.DeviceMgt.DeviceDirInfor.Header.Description = this.textBoxIDescription.Text;
            switch (Program.ConfigMgt.Config.AdapterDirection)
            {
                case DirectionType.INBOUND:
                    {
                        Program.DeviceMgt.DeviceDirInfor.Header.ConfigurationSummary = Program.InAdapter.Instance.GetConfigurationSummary();
                        Instance_OnRegisterInboundRules(Program.InAdapter.Instance.GetRules());
                        CreateLookupTableScript(Program.InAdapter.Instance.GetLookupTables());
                        break;
                    }
                case DirectionType.OUTBOUND:
                    {
                        Program.DeviceMgt.DeviceDirInfor.Header.ConfigurationSummary = Program.OutAdapter.Instance.GetConfigurationSummary();
                        Instance_OnRegisterOutboundRules(Program.OutAdapter.Instance.GetRules());
                        CreateLookupTableScript(Program.OutAdapter.Instance.GetLookupTables());
                        break;
                    }
                case DirectionType.BIDIRECTIONAL:
                    {
                        Program.DeviceMgt.DeviceDirInfor.Header.ConfigurationSummary = Program.BiAdapter.Instance.GetConfigurationSummary();
                        if (Program.ConfigMgt.Config.AdapterFileName.Contains("DAPInOut"))
                        {
                            CreateTableScript();
                        }
                        break;
                    }
            }
        }

        // Add fields which need string processing but not included in mapping list into the mapping list.
        // This will not influnent inbound/outbound serivce implementation.     //20071205

        private IInboundRule[] AddStringProcessField(IInboundRule[] rules)
        {
            if (rules == null) return null;
            return null;
        }
        private IOutboundRule[] AddStringProcessField(IOutboundRule[] rules)
        {
            if (rules == null) return null;
            return null;
        }

        private void Instance_OnRegisterInboundRules(IInboundRule[] ruleList)
        {
            if (ruleList == null)
            {
                Program.Log.Write(LogType.Warning, "IInboundRule array is NULL, do not create any SQL scripts.");
                return;
            }

            CreateTableScript();

            if (ruleList.Length > 0)
            {
                CreateInboundSPScript(ruleList);
            }
            else
            {
                Program.Log.Write(LogType.Warning, "IInboundRule array is Empty, do not create any Storage Procedure.");
            }
        }
        private void Instance_OnRegisterOutboundRules(IOutboundRule[] ruleList)
        {
            if (ruleList == null)
            {
                Program.Log.Write(LogType.Warning, "IOutboundRule array is NULL, do not create any SQL scripts.");
                return;
            }

            CreateTableScript();

            if (ruleList.Length > 0)
            {
                CreateOutboundSPScript(ruleList);
            }
            else
            {
                Program.Log.Write(LogType.Warning, "IOutboundRule array is Empty, do not create any Storage Procedure.");
            }
        }

        private void CreateTableScript()
        {
            string interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
            string fnameInstall = Application.StartupPath + "\\" + RuleScript.InstallTable.FileName;
            string fnameUninstall = Application.StartupPath + "\\" + RuleScript.UninstallTable.FileName;

            Program.Log.Write("Creating install table script...");
            using (StreamWriter sw = File.CreateText(fnameInstall))
            {
                //US29442
                #region
                //Program.ServiceMgt.Config.GarbageCollection.MaxRecordCountLimitation = 500;
                #endregion
                string strSql = RuleControl.GetCreateTableSQL(interfaceName, Program.ServiceMgt.Config.GarbageCollection);
                sw.Write(strSql);
            }
            Program.Log.Write("Create install table script succeeded. " + fnameInstall);

            Program.Log.Write("Creating uninstall table script...");
            using (StreamWriter sw = File.CreateText(fnameUninstall))
            {
                string strSql = RuleControl.GetDropTableSQL(interfaceName);
                sw.Write(strSql);
            }
            Program.Log.Write("Create uninstall table script succeeded. " + fnameUninstall);
        }
        private void CreateInboundSPScript(IInboundRule[] ruleList)
        {
            string interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
            string fnameInstall = Application.StartupPath + "\\" + RuleScript.InstallSP.FileName;
            string fnameUninstall = Application.StartupPath + "\\" + RuleScript.UninstallSP.FileName;

            Program.Log.Write("Creating install storage procedure script...");
            //using (StreamWriter sw = File.CreateText(fnameInstall))     // 20110706 OSQL & SQLMgmtStudio only support ASCII and UNICODE
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(GWDataDB.GetUseDataBaseSql());
                foreach (IInboundRule rule in ruleList)
                {
                    string strSql = RuleControl.GetInboundSP(interfaceName, rule);
                    sb.AppendLine(strSql);

                    IRuleSupplier supplier = rule as IRuleSupplier;
                    if (supplier != null)
                    {
                        string strSqlSupplied = supplier.GetInstallDBScript();
                        if (strSqlSupplied != null) sb.AppendLine(strSqlSupplied);
                    }
                }
                //sw.Write(sb.ToString());
                File.WriteAllText(fnameInstall, sb.ToString(), Encoding.Unicode);
            }
            Program.Log.Write("Create install storage procedure script succeeded. " + fnameInstall);

            Program.Log.Write("Creating uninstall storage procedure script...");
            using (StreamWriter sw = File.CreateText(fnameUninstall))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(GWDataDB.GetUseDataBaseSql());
                foreach (IInboundRule rule in ruleList)
                {
                    string strSql = RuleControl.GetInboundSPUninstall(interfaceName, rule);
                    sb.AppendLine(strSql);

                    IRuleSupplier supplier = rule as IRuleSupplier;
                    if (supplier != null)
                    {
                        string strSqlSupplied = supplier.GetUninstallDBScript();
                        if (strSqlSupplied != null) sb.AppendLine(strSqlSupplied);
                    }
                }
                sw.Write(sb.ToString());
            }
            Program.Log.Write("Create uninstall storage procedure script succeeded. " + fnameUninstall);
        }
        private void CreateOutboundSPScript(IOutboundRule[] ruleList)
        {
            string interfaceName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
            string fnameInstall = Application.StartupPath + "\\" + RuleScript.InstallSP.FileName;
            string fnameUninstall = Application.StartupPath + "\\" + RuleScript.UninstallSP.FileName;

            //using (StreamWriter sw = File.CreateText(fnameInstall))   // 20110706 OSQL & SQLMgmtStudio only support ASCII and UNICODE
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(GWDataDB.GetUseDataBaseSql());
                foreach (IOutboundRule rule in ruleList)
                {
                    string strSql = RuleControl.GetOutboundSP(interfaceName, rule);
                    sb.AppendLine(strSql);

                    IRuleSupplier supplier = rule as IRuleSupplier;
                    if (supplier != null)
                    {
                        string strSqlSupplied = supplier.GetInstallDBScript();
                        if (strSqlSupplied != null) sb.AppendLine(strSqlSupplied);
                    }
                }
                //sw.Write(sb.ToString());
                File.WriteAllText(fnameInstall, sb.ToString(), Encoding.Unicode);
            }
            using (StreamWriter sw = File.CreateText(fnameUninstall))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(GWDataDB.GetUseDataBaseSql());
                foreach (IOutboundRule rule in ruleList)
                {
                    string strSql = RuleControl.GetOutboundSPUninstall(interfaceName, rule);
                    sb.AppendLine(strSql);

                    IRuleSupplier supplier = rule as IRuleSupplier;
                    if (supplier != null)
                    {
                        string strSqlSupplied = supplier.GetUninstallDBScript();
                        if (strSqlSupplied != null) sb.AppendLine(strSqlSupplied);
                    }
                }
                sw.Write(sb.ToString());
            }
        }
        private void CreateLookupTableScript(LookupTable[] tableList)
        {
            if (tableList == null)
            {
                Program.Log.Write(LogType.Warning, "LookupTable array is NULL, do not create any private look up table.");
                return;
            }

            string installTable = Application.StartupPath + "\\" + RuleScript.InstallLUT.FileName;
            string uninstallTable = Application.StartupPath + "\\" + RuleScript.UninstallLUT.FileName;

            Program.Log.Write("Creating install private look up table script...");
            //using (StreamWriter sw = File.CreateText(installTable))   // 20110706 OSQL & SQLMgmtStudio only support ASCII and UNICODE
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(GWDataDB.GetUseDataBaseSql());
                foreach (LookupTable table in tableList)
                {
                    string strSql = RuleControl.GetCreateLUTSQL(table);
                    sb.AppendLine(strSql);
                }
                //sw.Write(sb.ToString());
                File.WriteAllText(installTable, sb.ToString(), Encoding.Unicode);
            }
            Program.Log.Write("Create install private look up table script succeeded. " + installTable);

            Program.Log.Write("Creating uninstall private look up table script...");
            using (StreamWriter sw = File.CreateText(uninstallTable))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(GWDataDB.GetUseDataBaseSql());
                foreach (LookupTable table in tableList)
                {
                    string strSql = RuleControl.GetDropLUTSQL(table);
                    sb.AppendLine(strSql);
                }
                sw.Write(sb.ToString());
            }
            Program.Log.Write("Create uninstall private look up table script succeeded. " + uninstallTable);
        }

        public bool BackupScriptFile()
        {
            if (!BackupScriptFile(RuleScript.UninstallSP)) return false;
            if (!BackupScriptFile(RuleScript.UninstallLUT)) return false;
            if (!BackupScriptFile(RuleScript.UninstallTable)) return false;
            if (!BackupScriptFile(RuleScript.UninstallTrigger)) return false;
            if (!BackupScriptFile(RuleScript.UninstallConfigDB)) return false;
            return true;
        }
        private bool BackupScriptFile(RuleScript scriptFile)
        {
            if (scriptFile == null) return false;
            string oldFilename = Application.StartupPath + "\\" + scriptFile.FileName;
            string newFileName = Application.StartupPath + "\\" + scriptFile.GetBackupFileName();
            if (File.Exists(oldFilename))
            {
                Program.Log.Write("Uninstall script found. " + oldFilename);
                if (File.Exists(newFileName)) File.Delete(newFileName);
                File.Copy(oldFilename, newFileName);
                Program.Log.Write("Uninstall script backuped. " + newFileName);
            }
            return true;
        }

        #endregion
    }
}
