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
    public partial class FormDuplexProcess : Form
    {
        private readonly string _cfgFolderPath;
        public readonly DuplexProcessConfig Config;

        public FormDuplexProcess(DuplexProcessConfig cfg, string cfgFolderPath, bool forRequester)
        {
            InitializeComponent();

            Config = cfg;
            _cfgFolderPath = cfgFolderPath;

            this.checkBoxXSLTRequest.Text = string.Format(this.checkBoxXSLTRequest.Text,
                forRequester ? "going out" : "coming in");
            this.checkBoxXSLTResponse.Text = string.Format(this.checkBoxXSLTResponse.Text,
                forRequester ? "coming in" : "going out");

            LoadSetting();
        }

        private void LoadSetting()
        {
            this.checkBoxXSLTRequest.Checked = Config.PreProcessConfig.EnableXSLTTransform;
            this.textBoxXSLTRequest.Text = Config.PreProcessConfig.XSLTFileLocation;
            this.checkBoxXSLTResponse.Checked = Config.PostProcessConfig.EnableXSLTTransform;
            this.textBoxXSLTResponse.Text = Config.PostProcessConfig.XSLTFileLocation;
        }
        private bool SaveSetting()
        {
            bool enableRequest = this.checkBoxXSLTRequest.Checked;
            string xslFileRequest = this.textBoxXSLTRequest.Text.Trim();
            bool enableResponse = this.checkBoxXSLTResponse.Checked;
            string xslFileResponse = this.textBoxXSLTResponse.Text.Trim();

            if (enableRequest && string.IsNullOrEmpty(xslFileRequest))
            {
                MessageBox.Show(this, "Please input the XSLT file location.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxXSLTRequest.Focus();
                return false;
            }

            if (enableResponse && string.IsNullOrEmpty(xslFileResponse))
            {
                MessageBox.Show(this, "Please input the XSLT file location.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxXSLTResponse.Focus();
                return false;
            }

            Config.PreProcessConfig.EnableXSLTTransform = enableRequest;
            Config.PreProcessConfig.XSLTFileLocation = xslFileRequest;
            Config.PostProcessConfig.EnableXSLTTransform = enableResponse;
            Config.PostProcessConfig.XSLTFileLocation = xslFileResponse;
            return true;
        }
        private void BrowseXSLTFile(TextBox tb)
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
            if (this.checkBoxRelativePath.Checked) tb.Text = ConfigHelper.GetRelativePath(basePath, fname);
            else tb.Text = fname;
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
        private void buttonBrowseRequest_Click(object sender, EventArgs e)
        {
            BrowseXSLTFile(this.textBoxXSLTRequest);
        }
        private void buttonBrowseResponse_Click(object sender, EventArgs e)
        {
            BrowseXSLTFile(this.textBoxXSLTResponse);
        }
    }
}
