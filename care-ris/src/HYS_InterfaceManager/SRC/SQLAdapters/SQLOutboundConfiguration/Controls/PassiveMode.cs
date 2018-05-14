using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.SQLOutboundAdapterConfiguration.Forms;

namespace HYS.SQLOutboundAdapterConfiguration.Controls
{
    public partial class PassiveMode : UserControl
    {
        #region Local members
        XCollection<SQLOutboundChanel> channelSet;
        ConnectionConfig DBconfig;
        #endregion

        #region Constructor
        public PassiveMode()
        {
            InitializeComponent();
            DBconfig = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig;
            channelSet = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundPassiveChanels;
        }
        #endregion

        #region Show and Save
        public void ShowInformation()
        {
            ShowSPList();
        }

        public void ShowSPList()
        {
            int i = 0;
            lstvSP.Items.Clear();
            foreach (SQLOutboundChanel channel in channelSet)
            {
                i++;
                ListViewItem viewItem = lstvSP.Items.Add(i.ToString());
                
                viewItem.Tag = i - 1;
                viewItem.SubItems.Add(channel.SPName);
            }
        }

        public void Save() {
            DBconfig.ConnectionParameter.Server = "";
            DBconfig.ConnectionParameter.Database = "";
            DBconfig.ConnectionParameter.User = "";
            DBconfig.ConnectionParameter.Password = "";
            DBconfig.ConnectionParameter.ConnectionStr = "";

            DBconfig.TimerInterval = 1000;
        }
        #endregion

        #region Controls events
        private void lstvSP_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected == true)
            {
                btnSPModify.Enabled = true;
                btnSPDelete.Enabled = true;
                btCopy.Enabled = true;
            }
            else
            {
                btnSPModify.Enabled = false;
                btnSPDelete.Enabled = false;
                btCopy.Enabled = false;
            }
        }

        private void lstvSP_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvSP.Columns[e.Column].Tag == null)
                this.lstvSP.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvSP.Columns[e.Column].Tag;
            if (tabK)
                this.lstvSP.Columns[e.Column].Tag = false;
            else
                this.lstvSP.Columns[e.Column].Tag = true;
            this.lstvSP.ListViewItemSorter = new ListViewSort(e.Column, this.lstvSP.Columns[e.Column].Tag);
            this.lstvSP.Sort();
        }
        #endregion

        #region SP events
        private void btnSPAdd_Click(object sender, EventArgs e)
        {
            int count = channelSet.Count;

            StorageProcedure frm = new StorageProcedure(channelSet);
            frm.ShowDialog(this);

            if (channelSet.Count > count)
            {
                ShowSPList();

                int i = 0;
                foreach (ListViewItem viewItem in lstvSP.Items)
                {
                    if ((int)viewItem.Tag == count)
                    {
                        lstvSP.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }
        }


        private void btCopy_Click(object sender, EventArgs e)
        {
            int count = channelSet.Count;
            int selectIndex = lstvSP.SelectedIndices[0];
            int itemIndex = (int)lstvSP.SelectedItems[0].Tag;

            StorageProcedure frm = new StorageProcedure(channelSet, channelSet[itemIndex]);
            frm.ShowDialog(this);

            if (channelSet.Count > count)
            {
                ShowSPList();

                int i = 0;
                foreach (ListViewItem viewItem in lstvSP.Items)
                {
                    if ((int)viewItem.Tag == count)
                    {
                        lstvSP.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnSPModefy_Click(object sender, EventArgs e)
        {
            int selectIndex = lstvSP.SelectedIndices[0];
            int itemIndex = (int)lstvSP.SelectedItems[0].Tag;

            StorageProcedure frm = new StorageProcedure(channelSet, itemIndex);
            frm.ShowDialog(this);

            ShowSPList();
            lstvSP.Items[selectIndex].Selected = true;
        }
        private void lstvSP_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = lstvSP.SelectedIndices[0];
            int itemIndex = (int)lstvSP.SelectedItems[0].Tag;

            StorageProcedure frm = new StorageProcedure(channelSet, itemIndex);
            frm.ShowDialog(this);

            ShowSPList();
            lstvSP.Items[selectIndex].Selected = true;
        }

        private void btnSPDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this storage procedure?", "Delete Storage Procedure Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int selectIndex = lstvSP.SelectedIndices[0];
                int itemIndex = (int)lstvSP.SelectedItems[0].Tag;

                channelSet.Remove(channelSet[itemIndex]);
                ShowSPList();
                if (channelSet.Count < 1)  //if there is no item in CriteriaList, unenable the button of modify and delete
                {
                    btnSPModify.Enabled = false;
                    btnSPDelete.Enabled = false;
                }
                else
                {
                    if (selectIndex >= channelSet.Count) //if the last item be removed, focus moves up
                    {
                        selectIndex--;
                    }
                    lstvSP.Items[selectIndex].Selected = true;
                }
            }
        }

        private void btDefault_Click(object sender, EventArgs e)
        {
            if (channelSet.Count > 0)
            {
                if (MessageBox.Show("If you do this operation, all the existed storage procedure will delete.\nAre you sure to go on?", "Load Default Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }

            SQLOutAdapterConfigMgt.LoadDefaultPassiveChannels(channelSet, "sp_" + Program.DeviceMgt.DeviceDirInfor.Header.Name + "_", Program.DeviceMgt.DeviceDirInfor.Header.Name);
            ShowSPList();
        }
        #endregion

       

        
    }
}
