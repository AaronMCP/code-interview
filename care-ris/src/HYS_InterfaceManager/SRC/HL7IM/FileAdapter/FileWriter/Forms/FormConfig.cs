using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base;
using System.Data.OleDb;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.MessageDevices.FileAdpater.FileWriter.Config;

namespace HYS.IM.MessageDevices.FileAdpater.FileWriter.Forms
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
            this.textBoxOutputFolder.Text = Program.Context.ConfigMgr.Config.OutputFileFolder;
            this.textBoxExtension.Text = Program.Context.ConfigMgr.Config.FileExtension;
            CodePageHelper.SetEncoding(this.comboBoxCodePage, Program.Context.ConfigMgr.Config.CodePageName);
            this.comboBoxOrganizeMode.SelectedIndex = (int)Program.Context.ConfigMgr.Config.OrganizationMode;
            this.comboBoxProcessingType.SelectedIndex = (int)Program.Context.ConfigMgr.Config.MessageProcessingType;

            return true;
        }

        public bool ValidateConfig()
        {
            Program.Context.ConfigMgr.Config.OutputFileFolder = this.textBoxOutputFolder.Text.Trim();
            Program.Context.ConfigMgr.Config.FileExtension = this.textBoxExtension.Text.Trim();
            Program.Context.ConfigMgr.Config.CodePageName = CodePageHelper.GetEncoding(this.comboBoxCodePage);
            Program.Context.ConfigMgr.Config.OrganizationMode = (FileOrganizationMode)this.comboBoxOrganizeMode.SelectedIndex;
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
       
    }
}
