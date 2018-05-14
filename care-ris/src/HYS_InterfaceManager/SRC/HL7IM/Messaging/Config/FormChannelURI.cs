using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Queuing;

namespace HYS.IM.Messaging.Config
{
    public partial class FormChannelURI : Form
    {
        private PullChannelConfig _pullChannel;
        private PushChannelConfig _pushChannel;

        public void SetReadOnly()
        {
            this.textBoxURI.ReadOnly = true;
            this.buttonOK.Visible = false;
            this.buttonCancel.Text = "Close";
        }

        public FormChannelURI(PullChannelConfig chn)
        {
            InitializeComponent();
            _pullChannel = chn;

            if (_pullChannel.ProtocolType == ProtocolType.RPC_NamedPipe)
            {
                this.groupBoxURI.Text = "WCF Named Pipe Address";
            }
            else if (_pullChannel.ProtocolType == ProtocolType.RPC_SOAP)
            {
                this.groupBoxURI.Text = "WCF SOAP Address";
            }
            else if (_pullChannel.ProtocolType == ProtocolType.RPC_TCP)
            {
                this.groupBoxURI.Text = "WCF TCP Address";
            }

            this.textBoxURI.Text = _pullChannel.RPCConfig.URI;
        }
        public FormChannelURI(PushChannelConfig chn)
        {
            InitializeComponent();
            _pushChannel = chn;

            if (_pushChannel.ProtocolType == ProtocolType.MSMQ)
            {
                this.groupBoxURI.Text = "MSMQ Path";
            }

            this.textBoxURI.Text = _pushChannel.MSMQConfig.SenderParameter.MSMQ.Path;
        }

        private bool Save()
        {
            string channelURI = this.textBoxURI.Text.Trim();

            if (channelURI == null || channelURI.Length < 1)
            {
                MessageBox.Show(this, "Please input the routing URI, or click the \"Cancel\" button to auto generate it.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxURI.Focus();
                return false;
            }

            if (_pullChannel != null)
                _pullChannel.RPCConfig.URI = channelURI;
            if (_pushChannel != null)
                _pushChannel.MSMQConfig.ReceiverParameter.MSMQ.Path =
                    _pushChannel.MSMQConfig.SenderParameter.MSMQ.Path = channelURI;

            return true;
        }

        private void textBoxURI_TextChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = this.textBoxURI.Text.Trim().Length > 0;
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!Save()) return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
