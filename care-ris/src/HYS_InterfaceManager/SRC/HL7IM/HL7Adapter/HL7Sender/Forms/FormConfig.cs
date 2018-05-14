using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Config;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Forms
{
    public partial class FormConfig : Form, IConfigUI
    {
        public FormConfig()
        {
            InitializeComponent();
            CodePageHelper.LoadEncoding(this.comboBoxCodePage);
            LoadConfig();
        }

        #region IConfigUI Members

        public Control GetControl()
        {
            this.buttonCancel.Visible
                = this.buttonOK.Visible = false;
            return this;
        }

        public bool LoadConfig()
        {
            this.textBoxIP.Text = Program.Context.ConfigMgr.Config.SocketConfig.IPAddress;
            this.numericUpDownPort.Value = Program.Context.ConfigMgr.Config.SocketConfig.Port;
            CodePageHelper.SetEncoding(this.comboBoxCodePage, Program.Context.ConfigMgr.Config.SocketConfig.CodePageName);
            this.comboBoxCodePage.Enabled = Program.Context.ConfigMgr.Config.SocketConfig.CodePageCode < 0;

            //this.checkBoxMultipleSession.Checked = Program.Context.ConfigMgr.Config.SocketConfig.SocketClientType == SocketClientWithLongConnection.DEVICE_NAME;
            this.comboBoxProcessingType.SelectedIndex = (int)Program.Context.ConfigMgr.Config.MessageProcessingType;

            switch (Program.Context.ConfigMgr.Config.SocketConfig.SocketClientType)
            {
                case SocketClient.DEVICE_NAME:
                    this.checkBoxDisableMLLP.Checked = false;
                    this.checkBoxMultipleSession.Checked = false;
                    break;
                case SocketClientWithLongConnection.DEVICE_NAME:
                    this.checkBoxDisableMLLP.Checked = false;
                    this.checkBoxMultipleSession.Checked = true;
                    break;
                case SocketClientNoMLLP.DEVICE_NAME:
                    this.checkBoxDisableMLLP.Checked = true;
                    this.checkBoxMultipleSession.Checked = false;
                    break;
                case SocketClientWithLongConnectionNoMLLP.DEVICE_NAME:
                    this.checkBoxDisableMLLP.Checked = true;
                    this.checkBoxMultipleSession.Checked = true;
                    break;
                default:
                    this.checkBoxMultipleSession.CheckState = CheckState.Indeterminate;
                    this.checkBoxDisableMLLP.CheckState = CheckState.Indeterminate;
                    break;
            }

            return true;
        }

        public bool ValidateConfig()
        {
            string ipAddress = this.textBoxIP.Text.Trim();
            if (ipAddress.Length > 0)
            {
                Program.Context.ConfigMgr.Config.SocketConfig.IPAddress = ipAddress;
            }
            else
            {
                MessageBox.Show(this,
                    "Please input the HL7 receiver IP address.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            Program.Context.ConfigMgr.Config.SocketConfig.Port = (int)this.numericUpDownPort.Value;
            Program.Context.ConfigMgr.Config.SocketConfig.CodePageName = CodePageHelper.GetEncoding(this.comboBoxCodePage);
            //Program.Context.ConfigMgr.Config.SocketConfig.SocketClientType = this.checkBoxMultipleSession.Checked ? SocketClientWithLongConnection.DEVICE_NAME : SocketClient.DEVICE_NAME;
            Program.Context.ConfigMgr.Config.MessageProcessingType = (MessageProcessType)this.comboBoxProcessingType.SelectedIndex;

            if (this.checkBoxMultipleSession.CheckState != CheckState.Indeterminate &&
                this.checkBoxDisableMLLP.CheckState != CheckState.Indeterminate)
            {
                bool multiSession = this.checkBoxMultipleSession.Checked;
                bool noMLLP = this.checkBoxDisableMLLP.Checked;
                if (multiSession)
                    if (noMLLP)
                        Program.Context.ConfigMgr.Config.SocketConfig.SocketClientType = SocketClientWithLongConnectionNoMLLP.DEVICE_NAME;
                    else
                        Program.Context.ConfigMgr.Config.SocketConfig.SocketClientType = SocketClientWithLongConnection.DEVICE_NAME;
                else
                    if (noMLLP)
                        Program.Context.ConfigMgr.Config.SocketConfig.SocketClientType = SocketClientNoMLLP.DEVICE_NAME;
                    else
                        Program.Context.ConfigMgr.Config.SocketConfig.SocketClientType = SocketClient.DEVICE_NAME;
            }

            return true;
        }

        public string Title
        {
            get { return this.Text; }
        }

        #endregion

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!ValidateConfig()) return;
            if (Program.Context.ConfigMgr.Save())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                Program.Context.Log.Write(Program.Context.ConfigMgr.LastError);
                MessageBox.Show(this,
                    "Save configuration file failed",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
