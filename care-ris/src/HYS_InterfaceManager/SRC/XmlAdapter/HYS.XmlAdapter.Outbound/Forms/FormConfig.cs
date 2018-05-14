using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Adapter.Base;
using HYS.XmlAdapter.Outbound.Objects;
using HYS.XmlAdapter.Common.Controlers;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Common.Forms;

namespace HYS.XmlAdapter.Outbound.Forms
{
    public partial class FormConfig : Form, IConfigUI
    {
        public FormConfig()
        {
            InitializeComponent();

            _listViewCtrl = new MessageListViewControler<XIMOutboundMessage>
                (this.listViewMessage, Program.ConfigMgt.Config.Messages);
            _listViewCtrl.DoubleClick += new EventHandler(_listViewCtrl_DoubleClick);
            _listViewCtrl.SelectedIndexChanged += new EventHandler(_listViewCtrl_SelectedIndexChanged);
        }

        private MessageListViewControler<XIMOutboundMessage> _listViewCtrl;

        private void _listViewCtrl_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }
        private void _listViewCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        private void RefreshButtons()
        {
            this.buttonDelete.Enabled =
                this.buttonCopy.Enabled =
                this.buttonEdit.Enabled = _listViewCtrl.HasSelectedItem();
        }
        private void LoadSetting()
        {
            if (Program.ConfigMgt.Config.DataMergingPKIndex < 0 ||
                Program.ConfigMgt.Config.DataMergingPKIndex >= this.comboBoxMergePrimaryKey.Items.Count)
            {
                Program.ConfigMgt.Config.DataMergingPKIndex = 1;
            }

            this.textBoxTargetDir.Text = Program.ConfigMgt.Config.TargetPath;
            this.textBoxPrefix.Text = Program.ConfigMgt.Config.FileNamePrefix;
            this.textBoxSuffix.Text = Program.ConfigMgt.Config.FileNameSuffix;
            this.radioButtonFile.Checked = Program.ConfigMgt.Config.OutboundToFile;
            this.radioButtonSocket.Checked = !Program.ConfigMgt.Config.OutboundToFile;
            this.textBoxSourceDeviceName.Text = Program.ConfigMgt.Config.SocketConfig.SourceDeviceName;
            this.textBoxTargetDeviceName.Text = Program.ConfigMgt.Config.SocketConfig.TargetDeviceName;
            this.comboBoxMergePrimaryKey.SelectedIndex = Program.ConfigMgt.Config.DataMergingPKIndex;
            this.numericUpDownPort.Value = Program.ConfigMgt.Config.SocketConfig.Port;
            this.numericUpDownInterval.Value = Program.ConfigMgt.Config.DataCheckingInterval;
            this.textBoxIPAddress.Text = Program.ConfigMgt.Config.SocketConfig.IPAddress;
            this.checkBoxMerge.Checked = Program.ConfigMgt.Config.EnableDataMerging;
            _listViewCtrl.RefreshList();
        }
        private void SaveSetting()
        {
            Program.ConfigMgt.Config.TargetPath = this.textBoxTargetDir.Text;
            Program.ConfigMgt.Config.FileNamePrefix = this.textBoxPrefix.Text;
            Program.ConfigMgt.Config.FileNameSuffix = this.textBoxSuffix.Text;
            Program.ConfigMgt.Config.OutboundToFile = this.radioButtonFile.Checked;
            Program.ConfigMgt.Config.SocketConfig.IPAddress = this.textBoxIPAddress.Text;
            Program.ConfigMgt.Config.SocketConfig.Port = (int)this.numericUpDownPort.Value;
            Program.ConfigMgt.Config.DataCheckingInterval = (int)this.numericUpDownInterval.Value;
            Program.ConfigMgt.Config.DataMergingPKIndex = this.comboBoxMergePrimaryKey.SelectedIndex;
            Program.ConfigMgt.Config.SocketConfig.SourceDeviceName = this.textBoxSourceDeviceName.Text;
            Program.ConfigMgt.Config.SocketConfig.TargetDeviceName = this.textBoxTargetDeviceName.Text;
            Program.ConfigMgt.Config.EnableDataMerging = this.checkBoxMerge.Checked;
        }

        private void Add()
        {
            FormMessage<XIMOutboundMessage> frm = new FormMessage<XIMOutboundMessage>(null, Program.ConfigMgt.Config.Messages, Program.ConfigMgt.Config.GWDataDBConnection, Program.Log);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            XIMOutboundMessage message = frm.Message;
            if (message == null) return;

            Program.ConfigMgt.Config.Messages.Add(message);
            _listViewCtrl.RefreshList();
            RefreshButtons();
        }
        private void Copy()
        {
            XIMOutboundMessage message = _listViewCtrl.GetSelectedItem();
            if (message == null) return;

            XIMOutboundMessage newMessage = message.Clone();
            FormMessage<XIMOutboundMessage> frm = new FormMessage<XIMOutboundMessage>(newMessage, Program.ConfigMgt.Config.Messages, Program.ConfigMgt.Config.GWDataDBConnection, Program.Log, true);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            Program.ConfigMgt.Config.Messages.Add(newMessage);

            _listViewCtrl.RefreshList();
            _listViewCtrl.SelectItem(newMessage);
        }
        private void Edit()
        {
            XIMOutboundMessage message = _listViewCtrl.GetSelectedItem();
            if (message == null) return;

            XIMOutboundMessage testMessage = message.Clone();
            testMessage.Rule.RuleID = message.Rule.RuleID;
            FormMessage<XIMOutboundMessage> frm = new FormMessage<XIMOutboundMessage>(testMessage, Program.ConfigMgt.Config.Messages, Program.ConfigMgt.Config.GWDataDBConnection, Program.Log);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            int index = Program.ConfigMgt.Config.Messages.IndexOf(message);
            if (index < 0) return;

            Program.ConfigMgt.Config.Messages.Remove(message);
            Program.ConfigMgt.Config.Messages.Insert(index, testMessage);

            _listViewCtrl.RefreshList();
            _listViewCtrl.SelectItem(testMessage);
        }
        private void Delete()
        {
            XIMOutboundMessage message = _listViewCtrl.GetSelectedItem();
            if (message == null) return;

            if (Program.ConfigMgt.Config.WarnBeforeDeleteChannel &&
                MessageBox.Show(this, "Are you sure to delete this message mapping?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                != DialogResult.Yes) return;

            Program.ConfigMgt.Config.Messages.Remove(message);
            _listViewCtrl.RefreshList();
            RefreshButtons();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void buttonBrowseSource_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Please select a data target folder.";
            dlg.SelectedPath = Application.StartupPath;
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            this.textBoxTargetDir.Text = dlg.SelectedPath;
        }
        private void checkBoxMerge_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxMergePrimaryKey.Enabled = this.checkBoxMerge.Checked;
        }
        private void radioButtonSocket_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSocket.Checked)
            {
                this.groupBoxSocket.BringToFront();
                this.groupBoxSocket.Visible = true;
                this.groupBoxFile.Visible = false;
            }
            else
            {
                this.groupBoxSocket.Visible = false;
                this.groupBoxFile.Visible = true;
                this.groupBoxFile.BringToFront();
            }
        }
        private void FormConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSetting();
        }
        private void FormConfig_Load(object sender, EventArgs e)
        {
            LoadSetting();
            RefreshButtons();
        }

        #region IConfigUI Members

        public Control GetControl()
        {
            return this;
        }

        public bool LoadConfig()
        {
            return true;
        }

        public bool SaveConfig()
        {
            SaveSetting();
            Program.ConfigMgt.Save();
            return true;
        }

        string IConfigUI.Name
        {
            get
            {
                return "XIM Outbound Setting";
            }
        }

        #endregion
    }
}