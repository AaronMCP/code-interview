using System;
using System.IO;
using System.ServiceProcess;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Forms;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base.Controler;
using HYS.IM.Messaging.Management.Scripts;

namespace HYS.IM.Messaging.ServiceConfig
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            InitializeLogType();
            InitializeStartMode();
            InitializeAccountType();

            if (Program.DisableModifyingEntity)
            {
                this.buttonAdd.Visible = this.buttonDelete.Visible = false;
                this.buttonView.Location = this.buttonAdd.Location;
            }
        }

        private void InitializeLogType()
        {
            Array tlist = Enum.GetValues(typeof(LogType));
            this.comboBoxLogType.Items.Clear();
            foreach (LogType t in tlist)
            {
                this.comboBoxLogType.Items.Add(t);
            }
            if (this.comboBoxLogType.Items.Count > 0)
                this.comboBoxLogType.SelectedIndex = 0;
        }
        private void InitializeStartMode()
        {
            Array tlist = Enum.GetValues(typeof(ServiceStartMode));
            this.comboBoxStart.Items.Clear();
            foreach (ServiceStartMode t in tlist)
            {
                this.comboBoxStart.Items.Add(t);
            }
            if (this.comboBoxLogType.Items.Count > 0)
                this.comboBoxLogType.SelectedIndex = 0;
        }
        private void InitializeAccountType()
        {
            Array tlist = Enum.GetValues(typeof(ServiceAccount));
            this.comboBoxAccount.Items.Clear();
            foreach (ServiceAccount t in tlist)
            {
                this.comboBoxAccount.Items.Add(t);
            }
            if (this.comboBoxLogType.Items.Count > 0)
                this.comboBoxLogType.SelectedIndex = 0;
        }

        private void LoadSetting()
        {
            this.textBoxServiceName.Text = Program.ConfigMgt.Config.ServiceName;
            this.textBoxServiceDescription.Text = Program.ConfigMgt.Config.Description;
            this.textBoxDepends.Text = Program.ConfigMgt.Config.DependOnServiceNameList;
            this.comboBoxStart.SelectedItem = Program.ConfigMgt.Config.StartType;
            this.comboBoxAccount.SelectedItem = Program.ConfigMgt.Config.AccountType;
            this.textBoxUserName.Text = Program.ConfigMgt.Config.UserName;
            this.textBoxPassword.Text = Program.ConfigMgt.Config.Password;

            this.comboBoxLogType.SelectedItem = Program.ConfigMgt.Config.LogConfig.LogType;
            this.numericUpDownLogDuration.Value = Program.ConfigMgt.Config.LogConfig.DurationDay;
            this.checkBoxDumpData.Checked = Program.ConfigMgt.Config.LogConfig.DumpData;

            RefreshEntityList();
            RefreshEntityButton();
        }
        private bool SaveSetting()
        {
            string sname = this.textBoxServiceName.Text;

            if (!NTServiceHostConfig.IsValidServiceName(sname))
            {
                MessageBox.Show(this, "NT Service name should only contains charactor or number or '_', and should begins with charactor, and should be not more than 256 characters, please input another name.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxServiceName.Focus();
                return false;
            }

            if (!CheckDuplicatedService(sname))
            {
                MessageBox.Show(this, "NT Service \"" + sname + "\" has already exsited, please input another name.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxServiceName.Focus();
                return false;
            }

            Program.ConfigMgt.Config.ServiceName = sname;
            Program.ConfigMgt.Config.Description = this.textBoxServiceDescription.Text;
            Program.ConfigMgt.Config.DependOnServiceNameList = this.textBoxDepends.Text.Trim();
            Program.ConfigMgt.Config.StartType = (ServiceStartMode)this.comboBoxStart.SelectedItem;
            Program.ConfigMgt.Config.AccountType = (ServiceAccount)this.comboBoxAccount.SelectedItem;
            Program.ConfigMgt.Config.UserName = this.textBoxUserName.Text.Trim();
            Program.ConfigMgt.Config.Password = this.textBoxPassword.Text;

            Program.ConfigMgt.Config.LogConfig.LogType = (LogType)this.comboBoxLogType.SelectedItem;
            Program.ConfigMgt.Config.LogConfig.DurationDay = (int)this.numericUpDownLogDuration.Value;
            Program.ConfigMgt.Config.LogConfig.HostName = Program.ConfigMgt.Config.ServiceName;
            Program.ConfigMgt.Config.LogConfig.DumpData = this.checkBoxDumpData.Checked;

            string installSvcBat = Path.Combine(Application.StartupPath, "InstallService.bat");
            string uninstallSvcBat = Path.Combine(Application.StartupPath, "UninstallService.bat");
            //string registerSvcBat = Path.Combine(Application.StartupPath, "RegisterHostToSolution.bat");
            //string unregisterSvcBat = Path.Combine(Application.StartupPath, "UnregisterHostFromSolution.bat");
            string svcCfgExe = Path.Combine(Application.StartupPath, "HYS.IM.Messaging.ServiceConfig.exe");

            if (Program.AutoRunBatScript)
            {
                //ScriptMgt.ExecuteBatFile(uninstallSvcBat, Application.StartupPath, Program.Log);
                //ScriptMgt.ExecuteBatFile(svcCfgExe, "-s -u", Application.StartupPath, Program.Log);
                ScriptMgt.ExecuteAssembly(uninstallSvcBat, "", true, Program.Log);
                ScriptMgt.ExecuteAssembly(svcCfgExe, "-s -u", true, Program.Log);
            }

            if (Program.ConfigMgt.Save())
            {
                if (Program.AutoRunBatScript)
                {
                    //ScriptMgt.ExecuteBatFile(installSvcBat, Application.StartupPath, Program.Log);
                    //ScriptMgt.ExecuteBatFile(svcCfgExe, "-s -r", Application.StartupPath, Program.Log);
                    ScriptMgt.ExecuteAssembly(installSvcBat, "", true, Program.Log);
                    ScriptMgt.ExecuteAssembly(svcCfgExe, "-s -r", true, Program.Log);
                }

                GenerateBat();
                return true;
            }
            else
            {
                MessageBox.Show(this, "Save configuration file failed."
                    + "\r\n\r\nDetail Information: \r\n\r\n" + Program.ConfigMgt.LastErrorInfor,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private void GenerateBat()
        {
            string startFName = Path.Combine(Application.StartupPath , "StartService.bat");
            string stopFName = Path.Combine (Application.StartupPath , "StopService.bat");

            using (StreamWriter sw = File.CreateText(startFName))
            {
                sw.Write("net start " + Program.ConfigMgt.Config.ServiceName);
            }

            using (StreamWriter sw = File.CreateText(stopFName))
            {
                sw.Write("net stop " + Program.ConfigMgt.Config.ServiceName);
            }
        }

        private static bool CheckDuplicatedService(string sname)
        {
            if (Program.ConfigMgt.Config.ServiceName.Length > 0)
            {
                // not the first time setting the NT service name,
                // assume that the NT service has been created.
                return true;
            }

            ServiceController[] scList = ServiceController.GetServices();
            if (scList != null)
            {
                foreach (ServiceController sc in scList)
                {
                    if (sc.ServiceName == sname) return false;
                }
            }
            scList = ServiceController.GetDevices();
            if (scList != null)
            {
                foreach (ServiceController sc in scList)
                {
                    if (sc.ServiceName == sname) return false;
                }
            }
            return true;
        }

        private void AddEntity()
        {
            FormEntity<MessageEntityEntryAttribute, IMessageEntity> frm = new FormEntity<MessageEntityEntryAttribute, IMessageEntity>(Program.Log);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            EntityAssemblyConfig e = frm.Entity;
            if (e == null) return;

            bool existed = false;
            foreach (EntityAssemblyConfig cfg in Program.ConfigMgt.Config.Entities)
            {
                if (cfg.EntityInfo.EntityID == e.EntityInfo.EntityID)
                {
                    existed = true;
                    break;
                }
            }

            if (existed)
            {
                MessageBox.Show(this, string.Format("The entity ({0}) is already existed in this host.",
                    e.EntityInfo.Name), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Program.ConfigMgt.Config.Entities.Add(e);

            RefreshEntityList();
            RefreshEntityButton();
        }
        private void ViewEntity()
        {
            if (this.listViewEntity.SelectedItems.Count < 1) return;

            EntityAssemblyConfig e = this.listViewEntity.SelectedItems[0].Tag as EntityAssemblyConfig;
            if (e == null) return;

            FormViewEntity frm = new FormViewEntity(e);
            frm.ShowDialog(this);
        }
        private void DeleteEntity()
        {
            if (this.listViewEntity.SelectedItems.Count < 1) return;

            EntityAssemblyConfig e = this.listViewEntity.SelectedItems[0].Tag as EntityAssemblyConfig;
            if (e == null) return;

            Program.ConfigMgt.Config.Entities.Remove(e);

            RefreshEntityList();
            RefreshEntityButton();
        }
        private void RefreshEntityList()
        {
            this.listViewEntity.Items.Clear();
            foreach (EntityAssemblyConfig e in Program.ConfigMgt.Config.Entities)
            {
                ListViewItem i = new ListViewItem();
                i.Checked = e.Enable;
                i.SubItems.Add(e.EntityInfo.EntityID.ToString());
                i.SubItems.Add(e.EntityInfo.Name);
                i.SubItems.Add(e.AssemblyLocation);
                i.SubItems.Add(e.InitializeArgument.ConfigFilePath);
                i.Tag = e;
                this.listViewEntity.Items.Add(i);
            }
        }
        private void RefreshEntityButton()
        {
            this.buttonView.Enabled =
            this.buttonDelete.Enabled = this.listViewEntity.SelectedItems.Count > 0;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddEntity();
        }
        private void buttonView_Click(object sender, EventArgs e)
        {
            ViewEntity();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteEntity();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Program.ConfigResult = this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!SaveSetting()) return;
            Program.ConfigResult = this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void listViewEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshEntityButton();
        }
        private void comboBoxAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panelUserAccount.Enabled = ((ServiceAccount)this.comboBoxAccount.SelectedItem == ServiceAccount.User);
        }
        private void listViewEntity_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            EntityAssemblyConfig cfg = e.Item.Tag as EntityAssemblyConfig;
            if (cfg != null) cfg.Enable = e.Item.Checked;
        }
    }
}