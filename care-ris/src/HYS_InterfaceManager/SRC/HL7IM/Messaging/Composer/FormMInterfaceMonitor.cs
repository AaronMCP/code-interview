using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Composer
{
    public partial class FormMInterfaceMonitor : Form
    {
        private MInterfaceMonitorPage _monitorPage;
        public MInterfaceMonitorPage MonitorPage
        {
            get { return _monitorPage; }
        }

        public FormMInterfaceMonitor(MInterfaceMonitorPage mp)
        {
            InitializeComponent();
            _monitorPage = mp;
            if (_monitorPage == null)
            {
                _monitorPage = new MInterfaceMonitorPage();
                this.Text = "Add Monitor Page";
            }
            else
            {
                this.Text = "Edit Monitor Page";
            }
            LoadSetting();
        }

        private void LoadSetting()
        {
            this.textBoxName.Text = _monitorPage.Name;
            this.textBoxConfigFile.Text = _monitorPage.ConfigFileName;
            this.textBoxDescription.Text = _monitorPage.Description;
            this.comboBoxType.SelectedIndex = 0;
        }
        private bool SaveSetting()
        {
            string pName = this.textBoxName.Text.Trim();
            if (pName.Length < 1)
            {
                MessageBox.Show(this, "Please enter the page name.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxName.Focus();
                return false;
            }

            string cfgFile = this.textBoxConfigFile.Text.Trim();
            if (cfgFile.Length < 1)
            {
                MessageBox.Show(this, "Please enter the configuration file name.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxConfigFile.Focus();
                return false;
            }

            _monitorPage.Name = pName;
            _monitorPage.Description = this.textBoxDescription.Text;
            _monitorPage.ConfigFileName = this.textBoxConfigFile.Text;
            _monitorPage.Type = MInterfaceMonitorPageType.MessageBoxMonitor;
            return true;
        }
        private void BrowseConfigurationFile()
        {
            string iniPath = ConfigHelper.DismissDotDotInThePath(Program.GetSolutionRoot());
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Please select the configuration file of the messagse box.";
            dlg.Filter = "XML Files|*.xml|All Files|*.*";
            dlg.InitialDirectory = iniPath;
            dlg.Multiselect = false;
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            string fname = ConfigHelper.GetFullPath(dlg.FileName);
            fname = ConfigHelper.GetRelativePath(iniPath, fname);
            this.textBoxConfigFile.Text = fname;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!SaveSetting()) return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            BrowseConfigurationFile();
        }
    }
}
