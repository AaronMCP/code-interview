using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.MessageDevices.MessagePipe.Base.Channel;
using HYS.MessageDevices.MessagePipe.Base;

namespace HYS.MessageDevices.MessagePipe.Channels.Common
{
    public partial class FormChannelEntryConfig : Form
    {
        private ConfigurationInitializationParameter _param;

        private MessageEntryConfig _config;
        public MessageEntryConfig Config
        {
            get { return _config; }
        }

        public FormChannelEntryConfig(ConfigurationInitializationParameter param, MessageEntryConfig config)
        {
            InitializeComponent();

            _param = param;
            _config = config;
            /*_config.CheckingModel = config.CheckingModel;
            _config.EntryMessageType.Code = config.EntryMessageType.Code;
            _config.EntryMessageType.CodeSystem = config.EntryMessageType.CodeSystem;
            _config.EntryCriteria.XPath = config.EntryCriteria.XPath;
            _config.EntryCriteria.XPathPrefixDefinition = config.EntryCriteria.XPathPrefixDefinition;
            _config.EntryCriteria.RegularExpression = config.EntryCriteria.RegularExpression;
            */
            if (_config == null)
            {
                _config = new MessageEntryConfig();
            }

            cmBoxCheckMode.Items.Clear();

            cmBoxCheckMode.DataSource = Enum.GetValues(typeof(MessageEntryCheckingModel));
            
        }

        private void LoadSetting()
        {
            cmBoxCheckMode.Text = _config.CheckingModel.ToString().Trim();
            tBoxMsgCode.Text = _config.EntryMessageType.Code;
            tBoxMsgScheme.Text = _config.EntryMessageType.CodeSystem;
            tBoxXPath.Text = _config.EntryCriteria.XPath;
            tBoxXPathPrefix.Text = _config.EntryCriteria.XPathPrefixDefinition;
            tBoxRE.Text = _config.EntryCriteria.RegularExpression;

            LoadMode();
            
            
        }

        private void LoadMode()
        {
            tBoxMsgCode.Enabled = false;
            tBoxMsgScheme.Enabled = false;
            tBoxXPath.Enabled = false;
            tBoxXPathPrefix.Enabled = false;
            tBoxRE.Enabled = false;

            switch (cmBoxCheckMode.SelectedItem.ToString())
            {
                case "AcceptAnyUnacceptedMessage":
                    {
                       
                        break;
                    }

                case "AcceptUnacceptedMessageAccordingToMessageType":
                    {
                        tBoxMsgCode.Enabled = true;
                        tBoxMsgScheme.Enabled = true;
                        break;
                    }

                case "AcceptUnacceptedMessageAccordingToEntryCriteria":
                    {
                        tBoxXPath.Enabled = true;
                        tBoxXPathPrefix.Enabled = true;
                        tBoxRE.Enabled = true;
                        break;
                    }
            }
        }
        private bool SaveSetting()
        {
            switch (cmBoxCheckMode.SelectedItem.ToString())
            {
                case "AcceptAnyUnacceptedMessage":
                    {
                        break;
                    }

                case "AcceptUnacceptedMessageAccordingToMessageType":
                    {
                        if (tBoxMsgCode.Text.Trim().Equals(""))
                        {
                            MessageBox.Show("Message type code can not be empty, please input the message type code.");
                            tBoxMsgCode.Focus();
                            return false;
                        }

                        if (tBoxMsgScheme.Text.Trim().Equals(""))
                        {
                            MessageBox.Show("Message type scheme can not be empty, please input the message type scheme.");
                            tBoxMsgScheme.Focus();
                            return false;
                        }
                        break;
                    }

                case "AcceptUnacceptedMessageAccordingToEntryCriteria":
                    {
                        if (tBoxXPath.Text.Trim().Equals(""))
                        {
                            MessageBox.Show("Please input the XPath.");
                            tBoxXPath.Focus();
                            return false;
                        }

                        if (tBoxRE.Text.Trim().Equals(""))
                        {
                            MessageBox.Show("Please input the Regular Expression.");
                            tBoxRE.Focus();
                            return false;
                        }

                        break;
                    }
            }

            _config.CheckingModel = (MessageEntryCheckingModel)Enum.Parse(typeof(MessageEntryCheckingModel),cmBoxCheckMode.SelectedItem.ToString());
            _config.EntryMessageType.Code = tBoxMsgCode.Text.Trim();
            _config.EntryMessageType.CodeSystem = tBoxMsgScheme.Text.Trim();
            _config.EntryCriteria.XPath = tBoxXPath.Text.Trim();
            _config.EntryCriteria.XPathPrefixDefinition = tBoxXPathPrefix.Text.Trim();
            _config.EntryCriteria.RegularExpression = tBoxRE.Text.Trim();

            return true;
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

        private void FormChannelEntryConfig_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void cmBoxCheckMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMode();
        }

        
    }
}
