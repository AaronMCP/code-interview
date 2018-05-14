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
    public partial class FormMInterface : Form
    {
        private MInterface _mInterface;
        public MInterface MInterface
        {
            get { return _mInterface; }
        }

        public FormMInterface(MInterface mi)
        {
            InitializeComponent();

            _mInterface = mi;
            if (_mInterface == null)
            {
                _mInterface = new MInterface();
                this.Text = "Add Interface";
            }
            else
            {
                this.Text = "Edit Interface";
            }
            LoadSetting();
        }

        private void LoadSetting()
        {
            this.textBoxName.Text = _mInterface.Name;
            this.textBoxDescription.Text = _mInterface.Description;

            RefreshHostList();
            RefreshConfigPageList();
            RefreshMonitorPageList();
        }
        private bool SaveSetting()
        {
            string name = this.textBoxName.Text.Trim();
            if (name.Length < 1)
            {
                MessageBox.Show(this, "Please input the interface name.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxName.Focus();
                return false;
            }

            _mInterface.Name = name;
            _mInterface.Description = this.textBoxDescription.Text;
            return true;
        }

        private void AddHost()
        {
            FormMInterfaceHost frm = new FormMInterfaceHost(_mInterface);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            NTServiceHostInfo h = frm.Host;
            if (h == null) return;

            _mInterface.Hosts.Add(h);
            RefreshHostList();
        }
        private void DeleteHost()
        {
            if (this.listViewHost.SelectedItems.Count < 1) return;
            NTServiceHostInfo h = this.listViewHost.SelectedItems[0].Tag as NTServiceHostInfo;
            if (_mInterface.Hosts.Contains(h)) _mInterface.Hosts.Remove(h);
            RefreshHostList();
        }
        private void RefreshHostList()
        {
            this.listViewHost.Items.Clear();
            foreach (NTServiceHostInfo h in _mInterface.Hosts)
            {
                ListViewItem i = new ListViewItem(h.ServiceName);
                i.SubItems.Add(h.ServiceDescription);
                i.Tag = h;
                this.listViewHost.Items.Add(i);
            }
            RefreshHostBottons();
        }
        private void RefreshHostBottons()
        {
            this.buttonHostDelete.Enabled = this.listViewHost.SelectedItems.Count > 0;
        }

        private void AddConfigPage()
        {
            FormMInterfaceConfigPage frm = new FormMInterfaceConfigPage(null);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            MInterfaceConfigPage p = frm.ConfigPage;
            if (p == null) return;
            _mInterface.ConfigPages.Add(p);
            RefreshConfigPageList();
        }
        private void EditConfigPage()
        {
            if (this.listViewConfig.SelectedItems.Count < 1) return;
            MInterfaceConfigPage p = this.listViewConfig.SelectedItems[0].Tag as MInterfaceConfigPage;
            if (p == null) return;
            FormMInterfaceConfigPage frm = new FormMInterfaceConfigPage(p);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            RefreshConfigPageList();
        }
        private void DeleteConfigPage()
        {
            if (this.listViewConfig.SelectedItems.Count < 1) return;
            MInterfaceConfigPage p = this.listViewConfig.SelectedItems[0].Tag as MInterfaceConfigPage;
            if (_mInterface.ConfigPages.Contains(p)) _mInterface.ConfigPages.Remove(p);
            RefreshConfigPageList();
        }
        private void RefreshConfigPageList()
        {
            this.listViewConfig.Items.Clear();
            foreach (MInterfaceConfigPage p in _mInterface.ConfigPages)
            {
                ListViewItem i = new ListViewItem(p.Name);
                i.SubItems.Add(p.Description);
                i.Tag = p;
                this.listViewConfig.Items.Add(i);
            }
            RefreshConfigPageButtons();
        }
        private void RefreshConfigPageButtons()
        {
            this.buttonConfigDelete.Enabled
                = this.buttonConfigEdit.Enabled
                = this.listViewConfig.SelectedItems.Count > 0;
        }

        private void AddMonitorPage()
        {
            FormMInterfaceMonitor frm = new FormMInterfaceMonitor(null);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            MInterfaceMonitorPage p = frm.MonitorPage;
            if (p == null) return;
            _mInterface.MonitorPages.Add(p);
            RefreshMonitorPageList();
        }
        private void EditMonitorPage()
        {
            if (this.listViewMonitor.SelectedItems.Count < 1) return;
            MInterfaceMonitorPage p = this.listViewMonitor.SelectedItems[0].Tag as MInterfaceMonitorPage;
            if (p == null) return;
            FormMInterfaceMonitor frm = new FormMInterfaceMonitor(p);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            RefreshMonitorPageList();
        }
        private void DeleteMonitorPage()
        {
            if (this.listViewMonitor.SelectedItems.Count < 1) return;
            MInterfaceMonitorPage p = this.listViewMonitor.SelectedItems[0].Tag as MInterfaceMonitorPage;
            if (_mInterface.MonitorPages.Contains(p)) _mInterface.MonitorPages.Remove(p);
            RefreshMonitorPageList();
        }
        private void RefreshMonitorPageList()
        {
            this.listViewMonitor.Items.Clear();
            foreach (MInterfaceMonitorPage p in _mInterface.MonitorPages)
            {
                ListViewItem i = new ListViewItem(p.Name);
                i.SubItems.Add(p.Description);
                i.Tag = p;
                this.listViewMonitor.Items.Add(i);
            }
            RefreshMonitorPageButtons();
        }
        private void RefreshMonitorPageButtons()
        {
            this.buttonMonitorDelete.Enabled
                = this.buttonMonitorEdit.Enabled
                = this.listViewMonitor.SelectedItems.Count > 0;
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

        private void buttonHostAdd_Click(object sender, EventArgs e)
        {
            AddHost();
        }
        private void buttonHostDelete_Click(object sender, EventArgs e)
        {
            DeleteHost();
        }
        private void listViewHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshHostBottons();
        }

        private void buttonConfigAdd_Click(object sender, EventArgs e)
        {
            AddConfigPage();
        }
        private void buttonConfigEdit_Click(object sender, EventArgs e)
        {
            EditConfigPage();
        }
        private void buttonConfigDelete_Click(object sender, EventArgs e)
        {
            DeleteConfigPage();
        }
        private void listViewConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshConfigPageButtons();
        }

        private void buttonMonitorAdd_Click(object sender, EventArgs e)
        {
            AddMonitorPage();
        }
        private void buttonMonitorEdit_Click(object sender, EventArgs e)
        {
            EditMonitorPage();
        }
        private void buttonMonitorDelete_Click(object sender, EventArgs e)
        {
            DeleteMonitorPage();
        }
        private void listViewMonitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMonitorPageButtons();
        }
    }
}
