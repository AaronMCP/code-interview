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
using HYS.Common.Objects.Logging;
using HYS.SocketAdapter.Configuration;
using HYS.Common.Xml;
using HYS.SocketAdapter.Common;
using HYS.SocketAdapter.SocketOutboundAdapterConfiguration.Forms;

namespace HYS.SocketAdapter.SocketOutboundAdapterConfiguration
{
    public partial class FSocketOutConfiguration : Form, IConfigUI
    {
        #region Local members
        ClientSocketParams clientSocketParams;
        XCollection<SocketOutChannel> channelSet;
        XCollection<LookupTable> LUTableSet;
        int channelIndex=-1;
        #endregion

        #region Constructor
        public FSocketOutConfiguration()
        {
            InitializeComponent();
            Initialization();
        }
        #endregion

        #region Initialization and Save
        private void Initialization() {
            InitCodePage();
            
            clientSocketParams = SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.ClientSocketParams;
            channelSet = SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.OutboundChanels;
            LUTableSet = SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.LookupTables;

            #region ClientSocketParameters
            this.txtCallbackIP.Text = clientSocketParams.CallbackIP.ToString();
            this.txtCallbackPort.Value = clientSocketParams.CallbackPort;
            this.txtServerIP.Text = clientSocketParams.ServerIP.ToString();
            this.txtServerPort.Text = clientSocketParams.ServerPort.ToString();
            this.txtInterval.Text = SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.OutGeneralParams.TimerInterval.ToString();
            this.txtConnectTimeout.Text = clientSocketParams.ConnectTimeout.ToString();
            this.txtTryCount.Text = clientSocketParams.ConnectTryCount.ToString();
            this.txtReceiveTimeout.Text = clientSocketParams.RecTimeout.ToString();
            this.txtSendTimeout.Text = clientSocketParams.SendTimeout.ToString();
            this.cbCodePage.SelectedIndex = EncodingPage.GetIndex(clientSocketParams.CodePageName);
            #endregion
            #region Channel
            if (channelSet != null)
            {
                if (channelSet.Count != 0)
                {
                    ShowChannelSetInformation();
                }     //It sign that there is no channel in the channelset
            }
            else
            {
                //Never occur as SocketOutAdapterConfigMgt.SocketOutAdapterConfig was be constuctured
                channelSet = new XCollection<SocketOutChannel>();
            }
            #endregion
            #region Look up tables
            if (LUTableSet != null)
            {
                if (LUTableSet.Count != 0)
                {
                    ShowLUTableSetInformation();
                }
            }
            else
            {
                //Never occur as SocketOutAdapterConfigMgt.SocketOutAdapterConfig was be constuctured
                LUTableSet = new XCollection<LookupTable>();
            }
            #endregion
        }

        private void InitCodePage()
        {
            this.cbCodePage.Items.Clear();
            for (int i = 0; i < EncodingPage.GetAllCodePages().Length; i++)
            {
                EncodingInfo ei = EncodingPage.GetAllCodePages()[i];
                cbCodePage.Items.Add(ei.DisplayName+" ("+ei.CodePage.ToString()+")");
            }
        }
        private void Save()
        {
            #region  Socket parameter
            clientSocketParams.LocalIP = "";
            clientSocketParams.LocalPort = -1;
            clientSocketParams.CallbackIP = this.txtCallbackIP.Text;
            clientSocketParams.CallbackPort = (int)this.txtCallbackPort.Value;
            clientSocketParams.ServerIP = this.txtServerIP.Text;
            clientSocketParams.ServerPort = (int)this.txtServerPort.Value;
            SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.OutGeneralParams.TimerInterval = Int32.Parse(this.txtInterval.Text);
            clientSocketParams.ConnectTimeout = Int32.Parse(this.txtConnectTimeout.Text);
            clientSocketParams.ConnectTryCount = Int32.Parse(this.txtTryCount.Text);
            clientSocketParams.RecTimeout = Int32.Parse(this.txtReceiveTimeout.Text);
            clientSocketParams.SendTimeout = Int32.Parse(this.txtSendTimeout.Text);
            clientSocketParams.CodePageName = EncodingPage.GetAllCodePages()[cbCodePage.SelectedIndex].Name;
            #endregion

            string FileName = Application.StartupPath + "\\" + SocketOutboundAdapterConfigMgt.FileName;
            if (!SocketOutboundAdapterConfigMgt.Save(FileName))
            {
                if (SocketOutboundAdapterConfigMgt.LastException != null)
                    Program.log.Write(LogType.Error, "Cannot save information to configuration file: " + FileName);
                MessageBox.Show(SocketOutboundAdapterConfigMgt.LastException.Message);
            }
        }
        #endregion

        #region Show channel information
        public void ShowChannelSetInformation()
        {
            int i = 0;
            lstvChannel.Items.Clear();
            foreach (SocketOutChannel channel in channelSet)
            {
                i++;
                //Add #
                ListViewItem viewItem = new ListViewItem(i.ToString());
                //Add Channel name
                viewItem.SubItems.Add(channel.ChannelName);
                //Add Access mode
                viewItem.SubItems.Add(channel.Rule.QueryCriteriaRuleType.ToString());
                //Add channel status
                if (channel.Enable)
                {
                    viewItem.SubItems.Add("Enable");
                }
                else
                {
                    viewItem.SubItems.Add("Disable");
                }
                viewItem.Tag = i - 1;
                lstvChannel.Items.Add(viewItem);
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
                ListViewItem viewItem = new ListViewItem(i.ToString());
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
                viewItem.Tag = i - 1;
                lstvLUT.Items.Add(viewItem);
            }
        }
        #endregion

        #region Controls events
        private void lstvLUT_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
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
        }
        #endregion

        #region LUTable events
        private void btnLUTAdd_Click(object sender, EventArgs e)
        {
            int count = LUTableSet.Count;
            
            FLUTable frm = new FLUTable(this,LUTableSet);
            frm.ShowDialog(pMain);

            if (LUTableSet.Count > count) {
                int i = 0;
                foreach (ListViewItem viewItem in lstvLUT.Items)
                {
                    if ((int)viewItem.Tag == count)
                    {
                        lstvLUT.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnLUTModify_Click(object sender, EventArgs e)
        {
            int selectIndex = lstvLUT.SelectedIndices[0];
            int itemIndex = (int)lstvLUT.SelectedItems[0].Tag;

            FLUTable frm = new FLUTable(this, LUTableSet, itemIndex);
            frm.ShowDialog(pMain);

            lstvLUT.Items[selectIndex].Selected = true;
        }
        private void lstvLUT_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = lstvLUT.SelectedIndices[0];
            int itemIndex = (int)lstvLUT.SelectedItems[0].Tag;

            FLUTable frm = new FLUTable(this, LUTableSet, itemIndex);
            frm.ShowDialog(pMain);

            lstvLUT.Items[selectIndex].Selected = true;
        }

        private void btnLUTDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this translation table?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int selectIndex = lstvLUT.SelectedIndices[0];
                int itemIndex = (int)lstvLUT.SelectedItems[0].Tag;

                LUTableSet.Remove(LUTableSet[itemIndex]);
                ShowLUTableSetInformation();
                if (LUTableSet.Count < 1)
                {
                    btnLUTModify.Enabled = false;
                    btnLUTDelete.Enabled = false;
                }
                else {
                    if (selectIndex >= LUTableSet.Count)
                    {
                        selectIndex--;
                    }
                    lstvLUT.Items[selectIndex].Selected = true;
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

            if (channelSet.Count > count)
            {  // Add successfully
                int i = 0;
                foreach (ListViewItem viewItem in lstvChannel.Items)
                {
                    if ((int)viewItem.Tag == count)
                    {
                        lstvChannel.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnChannelCopy_Click(object sender, EventArgs e)
        {
            int count = channelSet.Count;

            SocketOutChannel copyChannel = channelSet[channelIndex].Clone();

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
            int selectIndex = lstvChannel.SelectedIndices[0];
            int itemIndex = (int)lstvChannel.SelectedItems[0].Tag;

            FChannel frm = new FChannel(this, channelSet, itemIndex);
            frm.ShowDialog(pMain);

            lstvChannel.Items[selectIndex].Selected = true;
        }
        private void lstvChannel_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = lstvChannel.SelectedIndices[0];
            int itemIndex = (int)lstvChannel.SelectedItems[0].Tag;

            FChannel frm = new FChannel(this, channelSet, itemIndex);
            frm.ShowDialog(pMain);

            lstvChannel.Items[selectIndex].Selected = true;
        }

        private void btnChannelDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this channel?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int selectIndex = lstvChannel.SelectedIndices[0];
                int itemIndex = (int)lstvChannel.SelectedItems[0].Tag;

                channelSet.Remove(channelSet[itemIndex]);
                ShowChannelSetInformation();
                if (channelSet.Count < 1)
                {
                    btnChannelModify.Enabled = false;
                    btnChannelDelete.Enabled = false;
                    btnChannelCopy.Enabled = false;
                }
                else
                {
                    if (selectIndex >= channelSet.Count)
                    {
                        selectIndex--;
                    }
                    lstvChannel.Items[selectIndex].Selected = true;
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
            //return SocketOutboundAdapterConfigMgt.Save(Application.StartupPath+"\\"+SocketOutboundAdapterConfigMgt.FileName);   
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
