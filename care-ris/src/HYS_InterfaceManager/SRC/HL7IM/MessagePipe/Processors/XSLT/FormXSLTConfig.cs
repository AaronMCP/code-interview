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

namespace HYS.MessageDevices.MessagePipe.Processors.XSLT
{
    public partial class FormXSLTConfig : Form
    {
        private ConfigurationInitializationParameter _param;
        private XSLTConfig _config;
        public XSLTConfig Config
        {
            get { return _config; }
        }

        public FormXSLTConfig(ConfigurationInitializationParameter param, XSLTConfig config)
        {
            InitializeComponent();
            _param = param;
            _config = config;

            if (_config == null)
            {
                _config = new XSLTConfig();
            }

            LoadSetting();
        }

        private void LoadSetting()
        {
            tBoxXSLTFile.Text = _config.XSLTFileName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (SaveSetting())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool SaveSetting()
        {
            string fn = this.tBoxXSLTFile.Text.Trim();
            if (fn.Length > 0)
            {
                _config.XSLTFileName = fn;
                return true;
            }
            else
            {
                MessageBox.Show(this,
                    "Please input the XSLT file name.",
                    this.Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                this.tBoxXSLTFile.Focus();
                return false;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Browse();
        }

        private void Browse()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (File.Exists(tBoxXSLTFile.Text.Trim()))
            {
                dlg.InitialDirectory = tBoxXSLTFile.Text.Trim();
            }
            else
            {
                dlg.InitialDirectory = _param.StartupPath;
            }
            dlg.Filter = "XSL Files(*.xsl)|*.xsl|XSLT Files(*.xslt)|*.xslt|All Files(*.*)|*.*";
            dlg.Multiselect = false;
            if (dlg.ShowDialog() != DialogResult.OK) return;

            string fn = dlg.FileName;
            this.tBoxXSLTFile.Text = _param.GetRelativePath(fn);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
