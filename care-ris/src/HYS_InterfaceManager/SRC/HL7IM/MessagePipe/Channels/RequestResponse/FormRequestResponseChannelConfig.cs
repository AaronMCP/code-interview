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

namespace HYS.MessageDevices.MessagePipe.Channels.RequestResponse
{
    public partial class FormRequestResponseChannelConfig : Form
    {
        private ConfigurationInitializationParameter _param;

        private RequestResponseChannelConfig _config;
        public RequestResponseChannelConfig Config
        {
            get { return _config; }
            set { _config = value; }

        }

        public FormRequestResponseChannelConfig(ConfigurationInitializationParameter param, RequestResponseChannelConfig config)
        {
            InitializeComponent();

            _param = param;
            _config = config;

            if (_config == null)
            {
                _config = new RequestResponseChannelConfig();
            }

            
        }

        private void LoadSetting()
        {
            LoadEntryConfig();

            lViewRequestProcessor.Items.Clear();
            for (int i = 0; i < _config.RequestMessageProcessors.Count;i++ )
            {
                ListViewItem item = new ListViewItem((i + 1).ToString());
                item.SubItems.Add(_config.RequestMessageProcessors[i].Name);
                item.SubItems.Add(_config.RequestMessageProcessors[i].DeviceName);
                item.SubItems.Add(_config.RequestMessageProcessors[i].Description);
                lViewRequestProcessor.Items.Add(item);
            }

            btnAddRequestProcessor.Enabled = true;
            btnDeleteRequestProcessor.Enabled = false;
            btnEditRequestProcessor.Enabled = false;

            lViewResponseProcessor.Items.Clear();
            for (int i = 0; i < _config.ResponseMessageProcessors.Count; i++)
            {
                ListViewItem item = new ListViewItem((i + 1).ToString());
                item.SubItems.Add(_config.ResponseMessageProcessors[i].Name);
                item.SubItems.Add(_config.ResponseMessageProcessors[i].DeviceName);
                item.SubItems.Add(_config.ResponseMessageProcessors[i].Description);
                lViewResponseProcessor.Items.Add(item);
            }

            btnAddResponseProcessor.Enabled = true;
            btnEditResponseProcessor.Enabled = false;
            btnDeleteResponseProcessor.Enabled = false;

        }
        private bool SaveSetting()
        {
            return true;
        }
        private void OpenEntryConfig()
        {
            FormChannelEntryConfig frm = new FormChannelEntryConfig(_param, _config.EntryControl);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                
                LoadEntryConfig();
                
            }
        }

        private void LoadEntryConfig()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Entry Checking Model: " + _config.EntryControl.CheckingModel.ToString() + "\n");
            sb.Append("Receving Message Type: " + _config.EntryControl.EntryMessageType+"\n");
            sb.Append("Entry Criteria"+"\n");
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

        private void FormRequestResponseChannelConfig_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void lViewRequestProcessor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lViewRequestProcessor.SelectedItems.Count>0)
            {
                btnDeleteRequestProcessor.Enabled = true;
                btnEditRequestProcessor.Enabled = true;
            }

        }

        private void btnAddRequestProcessor_Click(object sender, EventArgs e)
        {
            ProcessorInstance pi = new ProcessorInstance();
            pi.Name = "";
            pi.DeviceName = ProcessorFactory.ProcessorRegistry[0].ToString();
            pi.Description = "";
            pi.Setting = "";

            FormProcessor formP = new FormProcessor(_param,pi);
            
            if (formP.ShowDialog()== DialogResult.OK)
            {
                _config.RequestMessageProcessors.Add(formP.Processor);
                ListViewItem lvi = new ListViewItem((lViewRequestProcessor.Items.Count + 1).ToString());
                lvi.SubItems.Add(formP.Processor.Name);
                lvi.SubItems.Add(formP.Processor.DeviceName);
                lvi.SubItems.Add(formP.Processor.Description);

                lViewRequestProcessor.Items.Add(lvi);
                
            }
        }

        private void btnDeleteRequestProcessor_Click(object sender, EventArgs e)
        {
            if (lViewRequestProcessor.SelectedItems.Count<1)
            {
                return;
            }

            if (MessageBox.Show("The selected item will be deleted, continue?", "Warining", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _config.RequestMessageProcessors.Remove(_config.RequestMessageProcessors[lViewRequestProcessor.SelectedIndices[0]]); ;
                lViewRequestProcessor.Items.Remove(lViewRequestProcessor.SelectedItems[0]);
                btnEditRequestProcessor.Enabled = false;
                btnDeleteRequestProcessor.Enabled = false;
            }
        }

        private void btnEditRequestProcessor_Click(object sender, EventArgs e)
        {
            if (lViewRequestProcessor.SelectedItems.Count<1)
            {
                return;
            }
            int i=lViewRequestProcessor.Items.IndexOf(lViewRequestProcessor.SelectedItems[0]);

            FormProcessor formP = new FormProcessor(_param,_config.RequestMessageProcessors[i]);
            if (formP.ShowDialog() == DialogResult.OK)
            {
                _config.RequestMessageProcessors[i].Name = formP.Processor.Name;
                _config.RequestMessageProcessors[i].DeviceName = formP.Processor.DeviceName;
                _config.RequestMessageProcessors[i].Description = formP.Processor.Description;
                _config.RequestMessageProcessors[i].Setting = formP.Processor.Setting;

                lViewRequestProcessor.SelectedItems[0].SubItems[1].Text = _config.RequestMessageProcessors[i].Name;
                lViewRequestProcessor.SelectedItems[0].SubItems[2].Text = _config.RequestMessageProcessors[i].DeviceName;
                lViewRequestProcessor.SelectedItems[0].SubItems[3].Text = _config.RequestMessageProcessors[i].Description;
            }
        }

        private void btnAddResponseProcessor_Click(object sender, EventArgs e)
        {
            ProcessorInstance pi = new ProcessorInstance();
            pi.Name = "";
            pi.DeviceName = ProcessorFactory.ProcessorRegistry[0].ToString();
            pi.Description = "";
            pi.Setting = "";

            FormProcessor formP = new FormProcessor(_param,pi);
            if (formP.ShowDialog() == DialogResult.OK)
            {
                _config.ResponseMessageProcessors.Add(formP.Processor);
                ListViewItem lvi = new ListViewItem((lViewResponseProcessor.Items.Count + 1).ToString());
                lvi.SubItems.Add(formP.Processor.Name);
                lvi.SubItems.Add(formP.Processor.DeviceName);
                lvi.SubItems.Add(formP.Processor.Description);

                lViewResponseProcessor.Items.Add(lvi);

            }
        }

        private void btnDeleteResponseProcessor_Click(object sender, EventArgs e)
        {
            if (lViewResponseProcessor.SelectedItems.Count < 1)
            {
                return;
            }

            if (MessageBox.Show("The selected item will be deleted, continue?", "Warining", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _config.ResponseMessageProcessors.Remove(_config.ResponseMessageProcessors[lViewResponseProcessor.SelectedIndices[0]]); ;
                lViewResponseProcessor.Items.Remove(lViewResponseProcessor.SelectedItems[0]);
                btnEditResponseProcessor.Enabled = false;
                btnDeleteResponseProcessor.Enabled = false;
            }
        }

        private void btnEditResponseProcessor_Click(object sender, EventArgs e)
        {
            if (lViewResponseProcessor.SelectedItems.Count<1)
            {
                return;
            }
            int i = lViewResponseProcessor.Items.IndexOf(lViewResponseProcessor.SelectedItems[0]);

            FormProcessor formP = new FormProcessor(_param,_config.ResponseMessageProcessors[i]);
            
            if (formP.ShowDialog() == DialogResult.OK)
            {
                _config.ResponseMessageProcessors[i].Name = formP.Processor.Name;
                _config.ResponseMessageProcessors[i].DeviceName = formP.Processor.DeviceName;
                _config.ResponseMessageProcessors[i].Description = formP.Processor.Description;
                _config.ResponseMessageProcessors[i].Setting = formP.Processor.Setting;

                lViewResponseProcessor.SelectedItems[0].SubItems[1].Text = _config.ResponseMessageProcessors[i].Name;
                lViewResponseProcessor.SelectedItems[0].SubItems[2].Text = _config.ResponseMessageProcessors[i].DeviceName;
                lViewResponseProcessor.SelectedItems[0].SubItems[3].Text = _config.ResponseMessageProcessors[i].Description;
            }
        }

        private void lViewResponseProcessor_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEditResponseProcessor.Enabled = true;
            btnDeleteResponseProcessor.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormMessageTypePair frm = new FormMessageTypePair(_config.RequestMessageTypePair);
            frm.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormMessageType frm = new FormMessageType(_config.ResponseMessageType);
            frm.ShowDialog(this);
        }
    }
}
