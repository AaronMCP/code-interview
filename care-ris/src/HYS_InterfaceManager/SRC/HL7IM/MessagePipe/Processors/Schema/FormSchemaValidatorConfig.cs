using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.MessageDevices.MessagePipe.Base;
using System.IO;

namespace HYS.MessageDevices.MessagePipe.Processors.Schema
{
    public partial class FormSchemaValidatorConfig : Form
    {
        private ConfigurationInitializationParameter _param;

        private SchemaValidatorConfig _config;
        public SchemaValidatorConfig Config
        {
            get { return _config; }
        }

        public FormSchemaValidatorConfig(ConfigurationInitializationParameter param, SchemaValidatorConfig config)
        {
            InitializeComponent();

            _param = param;
            _config = config;

            if (_config == null)
            {
                _config = new SchemaValidatorConfig();
            }

            LoadSetting();
        }

        private void LoadSetting()
        {
            this.textBoxFile.Text = _config.SchemaFileName;
        }
        private bool SaveSetting()
        {
            string fn = this.textBoxFile.Text.Trim();
            if (fn.Length > 0)
            {
                _config.SchemaFileName = fn;
                return true;
            }
            else
            {
                MessageBox.Show(this,
                    "Please input the XML schema file name.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.textBoxFile.Focus();
                return false;
            }
        }
        private void Browse()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (File.Exists(textBoxFile.Text.Trim()))
            {
                dlg.InitialDirectory = textBoxFile.Text.Trim();
            }
            else
            {
                dlg.InitialDirectory = _param.StartupPath;
            }
            dlg.Filter = "XML Schema Files(*.xsd)|*.xsd|All Files(*.*)|*.*";
            dlg.Multiselect = false;
            if (dlg.ShowDialog() != DialogResult.OK) return;

            string fn = dlg.FileName;
            this.textBoxFile.Text = _param.GetRelativePath(fn);
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
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            Browse();
        }
    }
}
