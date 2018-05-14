using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Composer
{
    public partial class FormMain : Form
    {
        private void LoadHostSetting()
        {
            RefreshHostList();
        }
        private bool SaveHostSetting()
        {
            return true;
        }

        private void RefreshHostList()
        {
            this.listViewHost.Items.Clear();
            foreach (NTServiceHostInfo c in Program.ConfigMgt.Config.Hosts)
            {
                ListViewItem i = new ListViewItem(c.ServiceName);
                i.SubItems.Add(c.ServiceDescription);
                i.SubItems.Add(c.ServicePath);
                i.Tag = c;

                this.listViewHost.Items.Add(i);
            }

            RefreshHostButton();
        }
        private void RefreshHostButton()
        {
            this.buttonHostDelete.Enabled = this.listViewHost.SelectedItems.Count > 0;
        }
        private void DeleteHost()
        {
            if (this.listViewHost.SelectedItems.Count < 1) return;
            NTServiceHostInfo c = this.listViewHost.SelectedItems[0].Tag as NTServiceHostInfo;
            Program.ConfigMgt.Config.Hosts.Remove(c);
            RefreshHostList();
        }
    }
}
