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
        private void LoadInterfaceSetting()
        {
            RefreshInterfaceList();
        }
        private bool SaveInterfaceSetting()
        {
            return true;
        }

        private void RefreshInterfaceList()
        {
            this.listViewInterface.Items.Clear();
            foreach (MInterface mi in Program.ConfigMgt.Config.Interfaces)
            {
                ListViewItem i = new ListViewItem(mi.Name);
                i.SubItems.Add(mi.Description);
                i.Tag = mi;

                this.listViewInterface.Items.Add(i);
            }

            RefreshInterfaceButton();
        }
        private void RefreshInterfaceButton()
        {
            this.buttonInterfaceDelete.Enabled 
                = this.buttonInterfaceEdit.Enabled 
                = this.listViewInterface.SelectedItems.Count > 0;
        }

        private void AddInterface()
        {
            FormMInterface frm = new FormMInterface(null);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            MInterface mi = frm.MInterface;
            if (mi == null) return;

            Program.ConfigMgt.Config.Interfaces.Add(mi);
            RefreshInterfaceList();
        }
        private void EditInterface()
        {
            if (this.listViewInterface.SelectedItems.Count < 1) return;
            MInterface mi = this.listViewInterface.SelectedItems[0].Tag as MInterface;
            
            FormMInterface frm = new FormMInterface(mi);
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            
            RefreshInterfaceList();
        }
        private void DeleteInterface()
        {
            if (this.listViewInterface.SelectedItems.Count < 1) return;
            MInterface mi = this.listViewInterface.SelectedItems[0].Tag as MInterface;
            Program.ConfigMgt.Config.Interfaces.Remove(mi);
            RefreshInterfaceList();
        }
    }
}
