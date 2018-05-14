using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base;
using System.Diagnostics;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Config;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Forms
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
            this.numericUpDownPort.Value = Program.Context.ConfigMgr.Config.SocketConfig.Port;
            CodePageHelper.SetEncoding(this.comboBoxCodePage, Program.Context.ConfigMgr.Config.SocketConfig.CodePageName);
            this.comboBoxCodePage.Enabled = Program.Context.ConfigMgr.Config.SocketConfig.CodePageCode < 0;

            switch (Program.Context.ConfigMgr.Config.SocketConfig.SocketWorkerType)
            {
                case SocketWorker.DEVICE_NAME:
                    this.checkBoxMultipleSession.Checked = false;
                    this.checkBoxDisableMLLP.Checked = false;
                    break;
                case SocketWorkerWithLongConnection.DEVICE_NAME:
                    this.checkBoxMultipleSession.Checked = true;
                    this.checkBoxDisableMLLP.Checked = false;
                    break;
                case SocketWorkerNoMLLP.DEVICE_NAME:
                    this.checkBoxMultipleSession.Checked = false;
                    this.checkBoxDisableMLLP.Checked = true;
                    break;
                case SocketWorkerWithLongConnectionNoMLLP.DEVICE_NAME:
                    this.checkBoxMultipleSession.Checked = true;
                    this.checkBoxDisableMLLP.Checked = true;
                    break;
                default:
                    this.checkBoxMultipleSession.CheckState = CheckState.Indeterminate;
                    this.checkBoxDisableMLLP.CheckState = CheckState.Indeterminate;
                    break;
            }

            this.comboBoxProcessingType.SelectedIndex = (int)Program.Context.ConfigMgr.Config.MessageProcessingType;
            return true;
        }

        public bool ValidateConfig()
        {
            Program.Context.ConfigMgr.Config.SocketConfig.Port = (int)this.numericUpDownPort.Value;
            Program.Context.ConfigMgr.Config.SocketConfig.CodePageName = CodePageHelper.GetEncoding(this.comboBoxCodePage);

            if (this.checkBoxMultipleSession.CheckState != CheckState.Indeterminate &&
                this.checkBoxDisableMLLP.CheckState != CheckState.Indeterminate)
            {
                bool multiSession = this.checkBoxMultipleSession.Checked;
                bool noMLLP = this.checkBoxDisableMLLP.Checked;
                if (multiSession) 
                    if(noMLLP)
                        Program.Context.ConfigMgr.Config.SocketConfig.SocketWorkerType = SocketWorkerWithLongConnectionNoMLLP.DEVICE_NAME;
                    else
                        Program.Context.ConfigMgr.Config.SocketConfig.SocketWorkerType = SocketWorkerWithLongConnection.DEVICE_NAME;
                else
                    if(noMLLP)
                        Program.Context.ConfigMgr.Config.SocketConfig.SocketWorkerType = SocketWorkerNoMLLP.DEVICE_NAME;
                    else
                        Program.Context.ConfigMgr.Config.SocketConfig.SocketWorkerType = SocketWorker.DEVICE_NAME;
            }

            Program.Context.ConfigMgr.Config.MessageProcessingType = (MessageProcessType)this.comboBoxProcessingType.SelectedIndex;
            return true;
        }

        public string Title
        {
            get { return this.Text; }
        }

        #endregion

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
        
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonDispatch_Click(object sender, EventArgs e)
        {
            FormConfigDispatch frm = new FormConfigDispatch(Program.Context.ConfigMgr.Config.InboundMessageDispatching);
            frm.ShowDialog(this);
        }

        private void buttonSuccessTemplate_Click(object sender, EventArgs e)
        {
            MessageProcessType mpt = (MessageProcessType)this.comboBoxProcessingType.SelectedIndex;
            string fileName = Program.Context.ConfigMgr.Config.GetPublishingSuccessXSLTFileNameWithFullPath();
            //string fileName = (mpt == MessageProcessType.OtherXML) ?
            //    Program.Context.ConfigMgr.Config.GetPublishingSuccessXSLTFileNameWithFullPath() :
            //    Program.Context.ConfigMgr.Config.GetHL7AckAATemplateFileNameWithFullPath();
            Process proc = Process.Start("notepad.exe", "\"" + fileName + "\"");
            proc.EnableRaisingEvents = false;
        }

        private void buttonFailureTemplate_Click(object sender, EventArgs e)
        {
            MessageProcessType mpt = (MessageProcessType)this.comboBoxProcessingType.SelectedIndex;
            string fileName = Program.Context.ConfigMgr.Config.GetPublishingFailureXSLTFileNameWithFullPath();
            //string fileName = (mpt == MessageProcessType.OtherXML) ?
            //    Program.Context.ConfigMgr.Config.GetPublishingFailureXSLTFileNameWithFullPath() :
            //    Program.Context.ConfigMgr.Config.GetHL7AckAETemplateFileNameWithFullPath();
            Process proc = Process.Start("notepad.exe", "\"" + fileName + "\"");
            proc.EnableRaisingEvents = false;
        }

        private void comboBoxProcessingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageProcessType mpt = (MessageProcessType)this.comboBoxProcessingType.SelectedIndex;
            this.buttonDispatch.Enabled = (mpt != MessageProcessType.HL7v2Text);
        }
    }
}
