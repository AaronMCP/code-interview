using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.MessageDevices.MessagePipe.Base;
using HYS.MessageDevices.MessagePipe.Channels.Common;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.MessageDevices.MessagePipe.Processors;
using HYS.MessageDevices.MessagePipe.Processors.Forms;

namespace HYS.MessageDevices.MessagePipe.Channels.SubscribePublish
{
    public partial class FormSubscribePublishChannelConfig : Form
    {
        private ConfigurationInitializationParameter _param;

        private SubscribePublishChannelConfig _config;
        public SubscribePublishChannelConfig Config
        {
            get { return _config; }
        }

        public FormSubscribePublishChannelConfig(ConfigurationInitializationParameter param, SubscribePublishChannelConfig config)
        {
            InitializeComponent();

            _param = param;
            _config = config;

            if (_config == null)
            {
                _config = new SubscribePublishChannelConfig();
            }            
        }

        private void LoadSetting()
        {
            LoadEntryConfig();

            lViewProcessor.Items.Clear();
            for (int i = 0; i < _config.Processors.Count; i++)
            {
                ListViewItem item = new ListViewItem((i + 1).ToString());
                item.SubItems.Add(_config.Processors[i].Name);
                item.SubItems.Add(_config.Processors[i].DeviceName);
                item.SubItems.Add(_config.Processors[i].Description);
                lViewProcessor.Items.Add(item);
            }

            btnAddProcessor.Enabled = true;
            btnDeleteProcessor.Enabled = false;
            btnEditProcessor.Enabled = false;

        }

        private void OpenEntryConfig()
        {
            FormChannelEntryConfig frm = new FormChannelEntryConfig(_param, _config.EntryControl);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadEntryConfig();
            }
        }
        private bool SaveSetting()
        {
            return true;
        }
        
        private void LoadEntryConfig()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Entry Checking Model: " + _config.EntryControl.CheckingModel.ToString() + "\n");
            sb.Append("Receving Message Type: " + _config.EntryControl.EntryMessageType + "\n");
            sb.Append("Entry Criteria" + "\n");
            sb.Append("     XPath :" + _config.EntryControl.EntryCriteria.XPath + "\n");
            sb.Append("     XPath Prefix: " + _config.EntryControl.EntryCriteria.XPathPrefixDefinition + "\n");
            sb.Append("     Regular Expression: " + _config.EntryControl.EntryCriteria.RegularExpression);

            rtBoxEntryCriteria.Clear();
            rtBoxEntryCriteria.Text = sb.ToString();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveSetting())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonEntry_Click(object sender, EventArgs e)
        {
            OpenEntryConfig();
        }

        private void FormSubscribePublishChannelConfig_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void btnAddProcessor_Click(object sender, EventArgs e)
        {
            ProcessorInstance pi = new ProcessorInstance();
            pi.Name = "";
            pi.DeviceName = ProcessorFactory.ProcessorRegistry[0].ToString();
            pi.Description = "";
            pi.Setting = "";

            FormProcessor formP = new FormProcessor(_param, pi);

            if (formP.ShowDialog() == DialogResult.OK)
            {
                _config.Processors.Add(formP.Processor);
                ListViewItem lvi = new ListViewItem((lViewProcessor.Items.Count + 1).ToString());
                lvi.SubItems.Add(formP.Processor.Name);
                lvi.SubItems.Add(formP.Processor.DeviceName);
                lvi.SubItems.Add(formP.Processor.Description);

                lViewProcessor.Items.Add(lvi);

            }
        }

        private void btnDeleteProcessor_Click(object sender, EventArgs e)
        {
            if (lViewProcessor.SelectedItems.Count < 1)
            {
                return;
            }

            if (MessageBox.Show("The selected item will be deleted, continue?", "Warining", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _config.Processors.Remove(_config.Processors[lViewProcessor.SelectedIndices[0]]); ;
                lViewProcessor.Items.Remove(lViewProcessor.SelectedItems[0]);
                btnEditProcessor.Enabled = false;
                btnDeleteProcessor.Enabled = false;
            }
        }

        private void btnEditProcessor_Click(object sender, EventArgs e)
        {
            if (lViewProcessor.SelectedItems.Count < 1)
            {
                return;
            }
            int i = lViewProcessor.Items.IndexOf(lViewProcessor.SelectedItems[0]);

            FormProcessor formP = new FormProcessor(_param, _config.Processors[i]);
            if (formP.ShowDialog() == DialogResult.OK)
            {
                _config.Processors[i].Name = formP.Processor.Name;
                _config.Processors[i].DeviceName = formP.Processor.DeviceName;
                _config.Processors[i].Description = formP.Processor.Description;
                _config.Processors[i].Setting = formP.Processor.Setting;

                lViewProcessor.SelectedItems[0].SubItems[1].Text = _config.Processors[i].Name;
                lViewProcessor.SelectedItems[0].SubItems[2].Text = _config.Processors[i].DeviceName;
                lViewProcessor.SelectedItems[0].SubItems[3].Text = _config.Processors[i].Description;
            }
        }

        private void lViewProcessor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lViewProcessor.SelectedItems.Count > 0)
            {
                btnDeleteProcessor.Enabled = true;
                btnEditProcessor.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormMessageType frm = new FormMessageType(_config.PublishMessageType);
            frm.ShowDialog(this);
        }
    }
}
