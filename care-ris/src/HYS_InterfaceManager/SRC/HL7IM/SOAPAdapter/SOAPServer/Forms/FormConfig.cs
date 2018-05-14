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
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Forms
{
    public partial class FormConfig : Form, IConfigUI
    {
        public FormConfig()
        {
            InitializeComponent();
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
            this.textBoxURI.Text = Program.Context.ConfigMgr.Config.SOAPServiceURI;
            switch (Program.Context.ConfigMgr.Config.OutboundProcessing.Model)
            {
                case MessageProcessModel.Direct: this.radioButtonSndDirect.Checked = true; break;
                case MessageProcessModel.Xslt: this.radioButtonSndXslt.Checked = true; break;
            }
            switch (Program.Context.ConfigMgr.Config.InboundProcessing.Model)
            {
                case MessageProcessModel.Direct: this.radioButtonRcvDirect.Checked = true; break;
                case MessageProcessModel.Xslt: this.radioButtonRcvXslt.Checked = true; break;
            }
            return true;
        }

        public bool ValidateConfig()
        {
            string uri = this.textBoxURI.Text.Trim();
            if (uri.Length > 0)
            {
                Program.Context.ConfigMgr.Config.SOAPServiceURI = uri;
            }
            else
            {
                MessageBox.Show(this,
                    "Please input the SOAP Server URI.",
                    Program.Context.AppName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }

            if (this.radioButtonSndDirect.Checked) Program.Context.ConfigMgr.Config.OutboundProcessing.Model = MessageProcessModel.Direct;
            else if (this.radioButtonSndXslt.Checked) Program.Context.ConfigMgr.Config.OutboundProcessing.Model = MessageProcessModel.Xslt;
            if (this.radioButtonRcvDirect.Checked) Program.Context.ConfigMgr.Config.InboundProcessing.Model = MessageProcessModel.Direct;
            else if (this.radioButtonRcvXslt.Checked) Program.Context.ConfigMgr.Config.InboundProcessing.Model = MessageProcessModel.Xslt;

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
                MessageBox.Show(this,
                    "Save configuration file failed",
                    Program.Context.AppName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonAdvance_Click(object sender, EventArgs e)
        {
            Program.Context.ConfigMgr.Config.OpenWCFConfigFile();
            //string wcfConfigFileName = Program.Context.ConfigMgr.Config.GetWCFConfigFileNameWithFullPath();
            //Process proc = Process.Start("notepad.exe", "\"" + wcfConfigFileName + "\"");
            //proc.EnableRaisingEvents = false;
        }

        private void buttonRcvDispatch_Click(object sender, EventArgs e)
        {
            FormConfigDispatch frm = new FormConfigDispatch(Program.Context.ConfigMgr.Config.InboundMessageDispatching);
            frm.ShowDialog(this);
        }

        private void buttonRcvXsltExt_Click(object sender, EventArgs e)
        {
            FormXSLTExt frm = new FormXSLTExt(Program.Context.ConfigMgr.Config.InboundProcessing);
            frm.ShowDialog(this);
        }

        private void buttonSndXsltExt_Click(object sender, EventArgs e)
        {
            FormXSLTExt frm = new FormXSLTExt(Program.Context.ConfigMgr.Config.OutboundProcessing);
            frm.ShowDialog(this);
        }

        private void buttonRcvXslt_Click(object sender, EventArgs e)
        {
            Program.Context.ConfigMgr.Config.OpenXSLTFile_SOAPMessageToXDSGatewayMessage();
        }

        private void buttonSndXslt_Click(object sender, EventArgs e)
        {
            Program.Context.ConfigMgr.Config.OpenXSLTFile_XDSGatewayMessageToSOAPMessage();
        }

        private void buttonSndSuccess_Click(object sender, EventArgs e)
        {
            Program.Context.ConfigMgr.Config.OpenResponseXDSGWMessageTemplateFile_PublishingSuccess();
        }

        private void buttonSndFailure_Click(object sender, EventArgs e)
        {
            Program.Context.ConfigMgr.Config.OpenResponseXDSGWMessageTemplateFile_PublishingFailure();
        }
    }
}
