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
    public partial class FormMInterfaceConfigSection : Form
    {
        private MInterfaceConfigSection _configSection;
        public MInterfaceConfigSection ConfigSection
        {
            get { return _configSection; }
        }

        public FormMInterfaceConfigSection(MInterfaceConfigSection cs)
        {
            InitializeComponent();
            _configSection = cs;
            if (_configSection == null)
            {
                _configSection = new MInterfaceConfigSection();
                this.Text = "Add Configuration Section";
            }
            else
            {
                this.Text = "Edti Configuration Section";
            }
            LoadSetting();
        }

        private void LoadSetting()
        {
            this.textBoxName.Text = _configSection.Name;
            this.textBoxDescription.Text = _configSection.Description;
            this.textBoxConfigFile.Text = _configSection.ConfigFileName;
            RefreshItemList();
        }
        private bool SaveSetting()
        {
            string pName = this.textBoxName.Text.Trim();
            if (pName.Length < 1)
            {
                MessageBox.Show(this, "Please enter the section name.",
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

            _configSection.Name = pName;
            _configSection.ConfigFileName = cfgFile;
            _configSection.Description = this.textBoxDescription.Text;
            return true;
        }

        private void AddItem()
        {
            FormMInterfaceConfigItem frm = new FormMInterfaceConfigItem(null);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            MInterfaceConfigItem i = frm.ConfigItem;
            if (i == null) return;
            _configSection.Items.Add(i);
            RefreshItemList();
        }
        private void EditItem()
        {
            if (this.listViewItem.SelectedItems.Count < 1) return;
            MInterfaceConfigItem s = this.listViewItem.SelectedItems[0].Tag as MInterfaceConfigItem;
            if (s == null) return;
            FormMInterfaceConfigItem frm = new FormMInterfaceConfigItem(s);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            RefreshItemList();
        }
        private void DeleteItem()
        {
            if (this.listViewItem.SelectedItems.Count < 1) return;
            MInterfaceConfigItem s = this.listViewItem.SelectedItems[0].Tag as MInterfaceConfigItem;
            if (_configSection.Items.Contains(s)) _configSection.Items.Remove(s);
            RefreshItemList();
        }
        private void RefreshItemList()
        {
            this.listViewItem.Items.Clear();
            foreach (MInterfaceConfigItem s in _configSection.Items)
            {
                ListViewItem i = new ListViewItem(s.Caption);
                i.SubItems.Add(s.XPath);
                i.SubItems.Add(s.EnableValidation.ToString());
                i.Tag = s;
                this.listViewItem.Items.Add(i);
            }
            RefreshItemButtons();
        }
        private void RefreshItemButtons()
        {
            this.buttonItemDelete.Enabled
                = this.buttonItemEdit.Enabled
                = this.listViewItem.SelectedItems.Count > 0;
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
            AddItem();
        }
        private void buttonSectionEdit_Click(object sender, EventArgs e)
        {
            EditItem();
        }
        private void buttonSectionDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }
        private void listViewItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshItemButtons();
        }
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            BrowseConfigurationFile();
        }
    }
}
