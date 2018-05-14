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
        private void LoadEntitySetting()
        {
            RefreshEntityList();
        }
        private bool SaveEntitySetting()
        {
            return true;
        }

        private void RefreshEntityList()
        {
            this.listViewEntity.Items.Clear();
            foreach (EntityContractBase c in Program.ConfigMgt.Config.Entities)
            {
                ListViewItem i = new ListViewItem(c.EntityID.ToString());
                i.SubItems.Add(c.Name);
                i.SubItems.Add(c.Interaction.ToString());
                i.SubItems.Add(c.AssemblyConfig.AssemblyLocation);
                i.Tag = c;

                this.listViewEntity.Items.Add(i);
            }

            RefreshEntityButton();
        }
        private void RefreshEntityButton()
        {
            this.buttonEntityDelete.Enabled = this.listViewEntity.SelectedItems.Count > 0;
        }
        private void DeleteEntity()
        {
            if (this.listViewEntity.SelectedItems.Count < 1) return;
            EntityContractBase c = this.listViewEntity.SelectedItems[0].Tag as EntityContractBase;
            //Program.ConfigMgt.Config.Entities.Remove(c);
            Program.ConfigMgt.Config.UnregisterEnity(c);
            RefreshEntityList();
        }
    }
}
