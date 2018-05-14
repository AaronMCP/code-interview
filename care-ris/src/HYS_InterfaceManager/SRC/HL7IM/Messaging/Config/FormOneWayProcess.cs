using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Mapping;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Config
{
    public partial class FormOneWayProcess : Form
    {
        private readonly string _cfgFolderPath;
        public readonly OneWayProcessConfig Config;

        public FormOneWayProcess(OneWayProcessConfig cfg, string cfgFolderPath, bool forMsgIn)
        {
            InitializeComponent();

            Config = cfg;
            _cfgFolderPath = cfgFolderPath;

            this.checkBoxXSLT.Text = string.Format(this.checkBoxXSLT.Text,
                forMsgIn ? "coming in" : "going out");
            
            LoadSetting();
        }

        private void LoadSetting()
        {
            this.checkBoxXSLT.Checked = Config.EnableXSLTTransform;
            this.textBoxXSLT.Text = Config.XSLTFileLocation;
        }
        private bool SaveSetting()
        {
            bool enable = this.checkBoxXSLT.Checked;
            string xslFile = this.textBoxXSLT.Text.Trim();

            if (enable && string.IsNullOrEmpty(xslFile))
            {
                MessageBox.Show(this, "Please input the XSLT file location.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxXSLT.Focus();
                return false;
            }

            Config.EnableXSLTTransform = enable;
            Config.XSLTFileLocation = xslFile;
            return true;
        }
        private void BrowseXSLTFile()
        {
            string basePath = ConfigHelper.GetFullPath(Application.StartupPath, _cfgFolderPath);
            basePath = ConfigHelper.DismissDotDotInThePath(basePath);

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = basePath;
            dlg.Filter = "XSLT Files (*.xsl,*.xslt)|*.xsl;*.xslt|All Files (*.*)|*.*";
            dlg.Title = "Select XSLT File";
            dlg.Multiselect = false;

            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            string fname = dlg.FileName;
            if (this.checkBoxRelativePath.Checked) this.textBoxXSLT.Text = ConfigHelper.GetRelativePath(basePath, fname);
            else this.textBoxXSLT.Text = fname;
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
            BrowseXSLTFile();
        }
    }
}
