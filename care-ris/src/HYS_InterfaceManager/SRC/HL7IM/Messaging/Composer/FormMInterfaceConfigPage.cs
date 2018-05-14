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
    public partial class FormMInterfaceConfigPage : Form
    {
        private MInterfaceConfigPage _configPage;
        public MInterfaceConfigPage ConfigPage
        {
            get { return _configPage; }
        }

        public FormMInterfaceConfigPage(MInterfaceConfigPage cp)
        {
            InitializeComponent();
            _configPage = cp;
            if (_configPage == null)
            {
                _configPage = new MInterfaceConfigPage();
                this.Text = "Add Configuration Page";
            }
            else
            {
                this.Text = "Edit Configuration Page";
            }
            LoadSetting();
        }

        private void LoadSetting()
        {
            this.textBoxName.Text = _configPage.Name;
            this.textBoxDescription.Text = _configPage.Description;
            this.textBoxConfigFile.Text = _configPage.ConfigFileName;
            this.comboBoxType.SelectedIndex = (int)_configPage.Type;
            RefreshSectionList();
        }
        private bool SaveSetting()
        {
            int index = this.comboBoxType.SelectedIndex;
            if (index < 0) return false;

            string pName = this.textBoxName.Text.Trim();
            if (pName.Length < 1)
            {
                MessageBox.Show(this, "Please enter the page name.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxName.Focus();
                return false;
            }
            
            string cfgFile = this.textBoxConfigFile.Text.Trim();
            if (index != (int)MInterfaceConfigPageType.CutomizedConfig && cfgFile.Length < 1)
            {
                MessageBox.Show(this, "Please enter the configuration file name.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxConfigFile.Focus();
                return false;
            }

            _configPage.Name = pName;
            _configPage.ConfigFileName = cfgFile;
            _configPage.Description = this.textBoxDescription.Text;
            _configPage.Type = (MInterfaceConfigPageType)index;
            return true;
        }

        private void AddSection()
        {
            FormMInterfaceConfigSection frm = new FormMInterfaceConfigSection(null);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            MInterfaceConfigSection s = frm.ConfigSection;
            if (s == null) return;
            _configPage.Sections.Add(s);
            RefreshSectionList();
        }
        private void EditSection()
        {
            if (this.listViewSection.SelectedItems.Count < 1) return;
            MInterfaceConfigSection s = this.listViewSection.SelectedItems[0].Tag as MInterfaceConfigSection;
            if (s == null) return;
            FormMInterfaceConfigSection frm = new FormMInterfaceConfigSection(s);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            RefreshSectionList();
        }
        private void DeleteSection()
        {
            if (this.listViewSection.SelectedItems.Count < 1) return;
            MInterfaceConfigSection s = this.listViewSection.SelectedItems[0].Tag as MInterfaceConfigSection;
            if (_configPage.Sections.Contains(s)) _configPage.Sections.Remove(s);
            RefreshSectionList();
        }
        private void RefreshSectionList()
        {
            this.listViewSection.Items.Clear();
            foreach (MInterfaceConfigSection s in _configPage.Sections)
            {
                ListViewItem i = new ListViewItem(s.Name);
                i.SubItems.Add(s.Description);
                i.Tag = s;
                this.listViewSection.Items.Add(i);
            }
            RefreshSectionButtons();
        }
        private void RefreshSectionButtons()
        {
            this.buttonSectionDelete.Enabled
                = this.buttonSectionEdit.Enabled
                = this.listViewSection.SelectedItems.Count > 0;
        }
        private void BrowseConfigurationFile()
        {
            string iniPath = ConfigHelper.DismissDotDotInThePath(Program.GetSolutionRoot());
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Please select a configuration file.";
            dlg.Filter = "XML Files|*.xml|All Files|*.*";
            dlg.InitialDirectory = iniPath;
            dlg.Multiselect = false;
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            string fname = ConfigHelper.GetFullPath(dlg.FileName);
            fname = ConfigHelper.GetRelativePath(iniPath, fname);
            this.textBoxConfigFile.Text = fname;
        }
        private void RefreshConfigurationButtons()
        {
            if (this.comboBoxType.SelectedIndex == (int)MInterfaceConfigPageType.CutomizedConfig)
            {
                this.textBoxConfigFile.ReadOnly = true;
                this.buttonBrowse.Enabled = false;
                this.panelSection.Enabled = true;
            }
            else
            {
                this.textBoxConfigFile.ReadOnly = false;
                this.buttonBrowse.Enabled = true;
                this.panelSection.Enabled = false;
            }
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

        private void buttonSectionAdd_Click(object sender, EventArgs e)
        {
            AddSection();
        }
        private void buttonSectionEdit_Click(object sender, EventArgs e)
        {
            EditSection();
        }
        private void buttonSectionDelete_Click(object sender, EventArgs e)
        {
            DeleteSection();
        }
        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshConfigurationButtons();
        }
        private void listViewSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSectionButtons();
        }
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            BrowseConfigurationFile();
        }   
    }
}
