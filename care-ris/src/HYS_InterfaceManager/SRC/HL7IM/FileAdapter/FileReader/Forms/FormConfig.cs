using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base;
using HYS.IM.MessageDevices.FileAdapter.FileReader.Config;
using HYS.IM.Common.HL7v2.MLLP;
using System.Diagnostics;
using System.IO;

namespace HYS.IM.MessageDevices.FileAdapter.FileReader.Forms
{
    public partial class FormConfig : Form, IConfigUI
    {
        public FormConfig()
        {
            InitializeComponent();
            InitializePage();

            LoadConfig();
        }

        private void InitializePage()
        {
            this.comboBoxDispose.DataSource = Enum.GetNames(typeof(FileDisposeType));

            CodePageHelper.LoadEncoding(this.comboBoxCodePage);
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
            this.textBoxINFolder.Text = Program.Context.ConfigMgr.Config.FileInboundFolder;
            this.textBoxTimeInterval.Value = (Decimal)Program.Context.ConfigMgr.Config.TimerInterval;
            this.comboBoxProcessingType.SelectedIndex = (int)Program.Context.ConfigMgr.Config.MessageProcessingType;
            this.textBoxFileExtension.Text = Program.Context.ConfigMgr.Config.FileExtension;
            CodePageHelper.SetEncoding(this.comboBoxCodePage, Program.Context.ConfigMgr.Config.EncodeName);

            this.comboBoxDispose.Text = Enum.GetName(typeof(FileDisposeType), Program.Context.ConfigMgr.Config.SourceFileDisposeType);
            this.textBoxDisposeFolder.Text = Program.Context.ConfigMgr.Config.FileOutboundFolder;

            return true;
        }

        public bool ValidateConfig()
        {
            if (this.textBoxINFolder.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input file folder.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBoxINFolder.Focus();
                return false;
            }

            if (!Directory.Exists(this.textBoxINFolder.Text))
            {
                MessageBox.Show("File folder don't exists.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBoxINFolder.Focus();
                return false;
            }

            if (this.textBoxFileExtension.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input file extension.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBoxFileExtension.Focus();
                return false;
            }

            if (this.textBoxDisposeFolder.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input result output folder.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBoxDisposeFolder.Focus();
                return false;
            }

            if (!Directory.Exists(this.textBoxDisposeFolder.Text))
            {
                MessageBox.Show("Output folder don't exists.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBoxDisposeFolder.Focus();
                return false;
            }

            Program.Context.ConfigMgr.Config.FileInboundFolder = this.textBoxINFolder.Text.Trim();
            Program.Context.ConfigMgr.Config.TimerInterval = (double)this.textBoxTimeInterval.Value;
            Program.Context.ConfigMgr.Config.MessageProcessingType = (MessageProcessType)this.comboBoxProcessingType.SelectedIndex;
            Program.Context.ConfigMgr.Config.FileExtension = this.textBoxFileExtension.Text.Trim();
            Program.Context.ConfigMgr.Config.EncodeName = CodePageHelper.GetEncoding(this.comboBoxCodePage);

            Program.Context.ConfigMgr.Config.SourceFileDisposeType = (FileDisposeType)Enum.Parse(typeof(FileDisposeType), this.comboBoxDispose.Text);
            Program.Context.ConfigMgr.Config.FileOutboundFolder = this.textBoxDisposeFolder.Text.Trim();

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

        private void buttonDispatch_Click(object sender, EventArgs e)
        {
            FormConfigDispatch frm = new FormConfigDispatch(Program.Context.ConfigMgr.Config.InboundMessageDispatching);
            frm.ShowDialog(this);
        }

        private void buttonSuccessTemplate_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            if ((MessageProcessType)this.comboBoxProcessingType.SelectedIndex == MessageProcessType.HL7v2Text
                || (MessageProcessType)this.comboBoxProcessingType.SelectedIndex == MessageProcessType.HL7v2XML)
            {
                fileName = Program.Context.ConfigMgr.Config.GetHL7AckAATemplateFileNameWithFullPath();
            }
            else
            {
                fileName = Program.Context.ConfigMgr.Config.GetSuccessXSLTFilePath();
            }
            Process proc = Process.Start("notepad.exe", "\"" + fileName + "\"");
            proc.EnableRaisingEvents = false;
        }

        private void buttonFailureTemplate_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            if ((MessageProcessType)this.comboBoxProcessingType.SelectedIndex == MessageProcessType.HL7v2Text
                || (MessageProcessType)this.comboBoxProcessingType.SelectedIndex == MessageProcessType.HL7v2XML)
            {
                fileName = Program.Context.ConfigMgr.Config.GetHL7AckAETemplateFileNameWithFullPath();
            }
            else
            {
                fileName = Program.Context.ConfigMgr.Config.GetFailureXSLTFilePath();
            }
            Process proc = Process.Start("notepad.exe", "\"" + fileName + "\"");
            proc.EnableRaisingEvents = false;
        }
    }
}
