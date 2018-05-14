using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Adapter.Base;
using HYS.XmlAdapter.Inbound.Objects;
using HYS.XmlAdapter.Common.Controlers;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Common.Forms;

namespace HYS.XmlAdapter.Inbound.Forms
{
    public partial class FormConfig : Form, IConfigUI
    {
        public FormConfig()
        {
            InitializeComponent();
            _listViewCtrl = new MessageListViewControler<XIMInboundMessage>
                (this.listViewMessage, Program.ConfigMgt.Config.Messages);
            _listViewCtrl.DoubleClick += new EventHandler(_listViewCtrl_DoubleClick);
            _listViewCtrl.SelectedIndexChanged += new EventHandler(_listViewCtrl_SelectedIndexChanged);
        }

        private MessageListViewControler<XIMInboundMessage> _listViewCtrl;

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
            this.numericUpDownPort.Value = Program.ConfigMgt.Config.SocketConfig.Port;
            this.textBoxSouceDeviceName.Text = Program.ConfigMgt.Config.SocketConfig.SourceDeviceName;
            this.radioButtonFile.Checked = Program.ConfigMgt.Config.InboundFromFile;
            this.radioButtonSocket.Checked = !Program.ConfigMgt.Config.InboundFromFile;
            this.radioButtonDelete.Checked = Program.ConfigMgt.Config.DirectoryConfig.DeleteProcessedFile;
            this.radioButtonMove.Checked = !Program.ConfigMgt.Config.DirectoryConfig.DeleteProcessedFile;
            this.textBoxSourceDir.Text = Program.ConfigMgt.Config.DirectoryConfig.SourcePath;
            this.numericUpDownTimerInterval.Value = Program.ConfigMgt.Config.DirectoryConfig.TimerInterval;
            _listViewCtrl.RefreshList();
        }
        private void SaveSetting()
        {
            Program.ConfigMgt.Config.InboundFromFile = this.radioButtonFile.Checked;
            Program.ConfigMgt.Config.SocketConfig.Port = (int)this.numericUpDownPort.Value;
            Program.ConfigMgt.Config.SocketConfig.SourceDeviceName = this.textBoxSouceDeviceName.Text;
            Program.ConfigMgt.Config.DirectoryConfig.DeleteProcessedFile = this.radioButtonDelete.Checked;
            Program.ConfigMgt.Config.DirectoryConfig.SourcePath = this.textBoxSourceDir.Text;
            Program.ConfigMgt.Config.DirectoryConfig.TimerInterval = (int)this.numericUpDownTimerInterval.Value;
        }

        private void Add()
        {
            FormMessage<XIMInboundMessage> frm = new FormMessage<XIMInboundMessage>(null, Program.ConfigMgt.Config.Messages, Program.ConfigMgt.Config.GWDataDBConnection, Program.Log);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            XIMInboundMessage message = frm.Message;
            if (message == null) return;

            Program.ConfigMgt.Config.Messages.Add(message);
            _listViewCtrl.RefreshList();
            RefreshButtons();
        }
        private void Edit()
        {
            XIMInboundMessage message = _listViewCtrl.GetSelectedItem();
            if (message == null) return;

            XIMInboundMessage testMessage = message.Clone();
            testMessage.Rule.RuleID = message.Rule.RuleID;
            FormMessage<XIMInboundMessage> frm = new FormMessage<XIMInboundMessage>(testMessage, Program.ConfigMgt.Config.Messages, Program.ConfigMgt.Config.GWDataDBConnection, Program.Log);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            int index = Program.ConfigMgt.Config.Messages.IndexOf(message);
            if (index < 0) return;

            Program.ConfigMgt.Config.Messages.Remove(message);
            Program.ConfigMgt.Config.Messages.Insert(index, testMessage);

            _listViewCtrl.RefreshList();
            _listViewCtrl.SelectItem(testMessage);
        }
        private void Copy()
        {
            XIMInboundMessage message = _listViewCtrl.GetSelectedItem();
            if (message == null) return;

            XIMInboundMessage newMessage = message.Clone();
            FormMessage<XIMInboundMessage> frm = new FormMessage<XIMInboundMessage>(newMessage, Program.ConfigMgt.Config.Messages, Program.ConfigMgt.Config.GWDataDBConnection, Program.Log, true);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            Program.ConfigMgt.Config.Messages.Add(newMessage);

            _listViewCtrl.RefreshList();
            _listViewCtrl.SelectItem(newMessage);
        }
        private void Delete()
        {
            XIMInboundMessage message = _listViewCtrl.GetSelectedItem();
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
            dlg.Description = "Please select a data source folder.";
            dlg.SelectedPath = Application.StartupPath;
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            this.textBoxSourceDir.Text = dlg.SelectedPath;
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
                return "XIM Inbound Setting";
            }
        }

        #endregion

        
    }
}