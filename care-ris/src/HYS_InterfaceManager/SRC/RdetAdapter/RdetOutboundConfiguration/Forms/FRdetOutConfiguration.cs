using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Rule;
using HYS.RdetAdapter.Configuration;
using HYS.Common.Xml;
using HYS.Common.Objects.Logging;

using HYS.RdetAdapter.RdetOutboundAdapterConfiguration.Forms;

namespace HYS.RdetAdapter.RdetOutboundAdapterConfiguration
{
    public partial class FRdetOutConfiguration : Form, IConfigUI
    {
        #region Local members
        ClientRdetParams clientRdetParams;
        XCollection<RdetOutChannel> channelSet;
        XCollection<LookupTable> LUTableSet;
        int channelIndex;
        int tableIndex;
        #endregion

        #region Constructor
        public FRdetOutConfiguration()
        {
            InitializeComponent();
            Initialization();
        }
        #endregion

        #region Initialization and Save
        private void Initialization() {
            clientRdetParams = RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.ClientRdetParams;
            channelSet = RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.OutboundChanels;
            LUTableSet = RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.LookupTables;

            #region ClientRdetParameters
            this.txtLocalIP.Text = clientRdetParams.LocalIP.ToString();
            this.txtLocalPort.Text = clientRdetParams.LocalPort.ToString();
            this.txtServerIP.Text = clientRdetParams.ServerIP.ToString();
            this.txtServerPort.Text = clientRdetParams.ServerPort.ToString();
            this.txtInterval.Text = RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.OutGeneralParams.TimerInterval.ToString();
            this.txtConnectTimeout.Text = clientRdetParams.ConnectTimeout.ToString();
            this.txtTryCount.Text = clientRdetParams.ConnectTryCount.ToString();
            this.txtReceiveTimeout.Text = clientRdetParams.RecTimeout.ToString();
            this.txtSendTimeout.Text = clientRdetParams.SendTimeout.ToString();
            #endregion
            #region Channel
            if (channelSet != null)
            {
                if (channelSet.Count != 0)
                {
                    ShowChannelSetInformation();
                }
                channelIndex = -1;     //It sign that there is no channel in the channelset
            }
            else
            {
                //Never occur as RdetOutAdapterConfigMgt.RdetOutAdapterConfig was be constuctured
                channelSet = new XCollection<RdetOutChannel>();
            }
            #endregion
            #region Look up tables
            if (LUTableSet != null)
            {
                if (LUTableSet.Count != 0)
                {
                    ShowLUTableSetInformation();
                }
                tableIndex = -1;     //It sign that there is no channel in the channelset
            }
            else
            {
                //Never occur as RdetOutAdapterConfigMgt.RdetOutAdapterConfig was be constuctured
                LUTableSet = new XCollection<LookupTable>();
            }
            #endregion
        }

        private void Save()
        {
            #region Server Rdet parameter
            clientRdetParams.LocalIP = this.txtLocalIP.Text;
            clientRdetParams.LocalPort = Int32.Parse(this.txtLocalPort.Text);
            clientRdetParams.ServerIP = this.txtServerIP.Text;
            clientRdetParams.ServerPort = Int32.Parse(this.txtServerPort.Text);
            RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.OutGeneralParams.TimerInterval = Int32.Parse(this.txtInterval.Text);
            clientRdetParams.ConnectTimeout = Int32.Parse(this.txtConnectTimeout.Text);
            clientRdetParams.ConnectTryCount = Int32.Parse(this.txtTryCount.Text);
            clientRdetParams.RecTimeout = Int32.Parse(this.txtReceiveTimeout.Text);
            clientRdetParams.SendTimeout = Int32.Parse(this.txtSendTimeout.Text);
            #endregion

            string FileName = Application.StartupPath + "\\" + RdetOutboundAdapterConfigMgt.FileName;
            if (!RdetOutboundAdapterConfigMgt.Save(FileName))
            {
                if (RdetOutboundAdapterConfigMgt.LastException != null)
                    Program.log.Write(LogType.Error, "Cannot save information to configuration file: " + FileName);
                MessageBox.Show(RdetOutboundAdapterConfigMgt.LastException.Message);
            }
        }
        #endregion

        #region Show channel information
        public void ShowChannelSetInformation()
        {
            int i = 0;
            lstvChannel.Items.Clear();
            foreach (RdetOutChannel channel in channelSet)
            {
                i++;
                //Add #
                ListViewItem viewItem = lstvChannel.Items.Add(i.ToString());
                //Add Channel name
                viewItem.SubItems.Add(channel.ChannelName);
                //Add Access mode
                viewItem.SubItems.Add(channel.Rule.QueryCriteriaRuleType.ToString());
                //Add amount of mapping fields
                int j = 0;
                foreach (RdetOutQueryResultItem resultItem in channelSet[i - 1].Rule.QueryResult.MappingList)
                {
                    j++;
                }
                viewItem.SubItems.Add(j.ToString() + " item(s)");
                //Add channel status
                if (channel.Enable)
                {
                    viewItem.SubItems.Add("Enable");
                }
                else
                {
                    viewItem.SubItems.Add("Disable");
                }
            }
        }
        #endregion

        #region Show Look up table information
        public void ShowLUTableSetInformation()
        {
            int i = 0;
            lstvLUT.Items.Clear();
            foreach (LookupTable table in LUTableSet)
            {
                i++;
                //Add #
                ListViewItem viewItem = lstvLUT.Items.Add(i.ToString());
                //Add Table name
                viewItem.SubItems.Add(table.DisplayName);
                //Add amount of mapping fields
                int j = 0;
                foreach (LookupItem item in LUTableSet[i - 1].Table)
                {
                    j++;
                }
                viewItem.SubItems.Add(j.ToString() + " item(s)");
                //Add table source
                if (true)
                {
                    viewItem.SubItems.Add("Inner table");
                }
                else
                {
                    viewItem.SubItems.Add("Outer table");
                }
            }
        }
        #endregion

        #region Controls events
        private void lstvLUT_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            tableIndex = e.ItemIndex;
            if (e.IsSelected)
            {
                btnLUTModify.Enabled = true;
                btnLUTDelete.Enabled = true;
            }
            else {
                btnLUTModify.Enabled = false;
                btnLUTDelete.Enabled = false;
            }
        }

        private void lstvChannel_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            channelIndex = e.ItemIndex;
            if (e.IsSelected)
            {
                btnChannelModify.Enabled = true;
                btnChannelDelete.Enabled = true;
                btnChannelCopy.Enabled = true;
            }
            else
            {
                btnChannelModify.Enabled = false;
                btnChannelDelete.Enabled = false;
                btnChannelCopy.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstvLUT_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvLUT.Columns[e.Column].Tag == null)
                this.lstvLUT.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvLUT.Columns[e.Column].Tag;
            if (tabK)
                this.lstvLUT.Columns[e.Column].Tag = false;
            else
                this.lstvLUT.Columns[e.Column].Tag = true;
            this.lstvLUT.ListViewItemSorter = new ListViewSort(e.Column, this.lstvLUT.Columns[e.Column].Tag);
            this.lstvLUT.Sort();
        }

        private void lstvChannel_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvChannel.Columns[e.Column].Tag == null)
                this.lstvChannel.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvChannel.Columns[e.Column].Tag;
            if (tabK)
                this.lstvChannel.Columns[e.Column].Tag = false;
            else
                this.lstvChannel.Columns[e.Column].Tag = true;
            this.lstvChannel.ListViewItemSorter = new ListViewSort(e.Column, this.lstvChannel.Columns[e.Column].Tag);
            this.lstvChannel.Sort();
        }
        #endregion

        #region LUTable events
        private void btnLUTAdd_Click(object sender, EventArgs e)
        {
            int count = LUTableSet.Count;
            
            FLUTable frm = new FLUTable(this,LUTableSet);
            frm.Show(pMain);

            if (LUTableSet.Count > count) {
                tableIndex = count;
                lstvLUT.Items[tableIndex].Selected = true;
            }
        }

        private void btnLUTModify_Click(object sender, EventArgs e)
        {
            FLUTable frm = new FLUTable(this, LUTableSet, tableIndex);
            frm.ShowDialog(pMain);

            lstvLUT.Items[tableIndex].Selected = true;
        }

        private void btnLUTDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this translation table?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                LUTableSet.Remove(LUTableSet[tableIndex]);
                ShowLUTableSetInformation();
                if (LUTableSet.Count < 1)
                {
                    btnLUTModify.Enabled = false;
                    btnLUTDelete.Enabled = false;
                }
                else {
                    if (tableIndex >= LUTableSet.Count) {
                        tableIndex--;
                    }
                    lstvLUT.Items[tableIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Channel events
        private void btnChannelAdd_Click(object sender, EventArgs e)
        {
            int count = channelSet.Count;

            FChannel frm = new FChannel(this,channelSet);
            frm.ShowDialog(pMain);

            if (channelSet.Count > count) {  // Add successfully
                channelIndex = count;
                lstvChannel.Items[channelIndex].Selected = true;
            }
        }

        private void btnChannelCopy_Click(object sender, EventArgs e)
        {
            int count = channelSet.Count;

            RdetOutChannel copyChannel = channelSet[channelIndex].Clone();

            copyChannel.ChannelName += "_copy";

            FChannel frm = new FChannel(this, channelSet, copyChannel);
            frm.ShowDialog(pMain);

            if (channelSet.Count > count)
            {  // Add successfully
                channelIndex = count;
                lstvChannel.Items[channelIndex].Selected = true;
            }
        }

        private void btnChannelModify_Click(object sender, EventArgs e)
        {
            FChannel frm = new FChannel(this,channelSet,channelIndex);
            frm.ShowDialog(pMain);

            lstvChannel.Items[channelIndex].Selected = true;
        }

        private void btnChannelDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this channel?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                channelSet.Remove(channelSet[channelIndex]);
                ShowChannelSetInformation();
                if (channelSet.Count < 1)
                {
                    btnChannelModify.Enabled = false;
                    btnChannelDelete.Enabled = false;
                    btnChannelCopy.Enabled = false;
                }
                else
                {
                    if (channelIndex >= channelSet.Count)
                    {
                        channelIndex--;
                    }
                    lstvChannel.Items[channelIndex].Selected = true;
                }
            }
        }
        #endregion

        #region IConfigUI Members

        Control IConfigUI.GetControl()
        {
            return this.pMain;
        }

        bool IConfigUI.LoadConfig()
        {
            return true;
        }

        string IConfigUI.Name
        {
            get { return this.Text; }
        }

        bool IConfigUI.SaveConfig()
        {
            //return RdetOutboundAdapterConfigMgt.Save(Application.StartupPath+"\\"+RdetOutboundAdapterConfigMgt.FileName);   
            try
            {
                Save();
                return true;
            }
            catch (Exception ex)
            {
                Program.log.Write(ex);
                return false;
            }
        }

        #endregion

       
    }
}
