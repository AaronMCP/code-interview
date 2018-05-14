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
using HYS.FileAdapter.Configuration;
using HYS.FileAdapter.Common;
using HYS.Common.Xml;
using HYS.FileAdapter.FileOutboundAdapterConfiguration.Forms;

namespace HYS.FileAdapter.FileOutboundAdapterConfiguration
{
    public partial class FFileOutConfig : Form, IConfigUI
    {
        #region Local members        
        XCollection<FileOutChannel> channelSet;
        XCollection<LookupTable> LUTableSet;
        int channelIndex;
        int tableIndex;
        #endregion

        #region Constructor
        public FFileOutConfig()
        {
            InitializeComponent();
            ComboxLoader.LoadEncoding(this.cbCodePage);
            Initialization();

            this.btLoadDefault.Visible = Program.bRunSingle;
        }
        #endregion

        #region Initialization and Save
        private void Initialization() {            
            channelSet = FileOutboundAdapterConfigMgt.FileOutAdapterConfig.OutboundChanels;
            LUTableSet = FileOutboundAdapterConfigMgt.FileOutAdapterConfig.LookupTables;

            #region ClientFileParameters

            FileOutGeneralParams gp = FileOutboundAdapterConfigMgt.FileOutAdapterConfig.OutGeneralParams;
            this.mtbTimerInterval.Text = gp.TimerInterval.ToString();
            this.tbFilePath.Text = gp.FilePath;
            this.tbFilePrefix.Text = gp.FilePrefix;
            this.tbFileSuffix.Text = gp.FileSuffix;
            this.tbFileDTFormat.Text = gp.FileDtFormat;                            
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
                //Never occur as FileOutAdapterConfigMgt.FileOutAdapterConfig was be constuctured
                channelSet = new XCollection<FileOutChannel>();
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
                //Never occur as FileOutAdapterConfigMgt.FileOutAdapterConfig was be constuctured
                LUTableSet = new XCollection<LookupTable>();
            }
            #endregion

            string codePageName = FileOutboundAdapterConfigMgt.FileOutAdapterConfig.OutGeneralParams.CodePageName;
            ComboxLoader.SetEncoding(this.cbCodePage, codePageName);
        }

        private void Save()
        {
            #region Server File parameter
            FileOutGeneralParams gp = FileOutboundAdapterConfigMgt.FileOutAdapterConfig.OutGeneralParams;
            gp.TimerInterval = Convert.ToInt32(this.mtbTimerInterval.Text);
            gp.FilePath = this.tbFilePath.Text;
            gp.FilePrefix = this.tbFilePrefix.Text;
            gp.FileSuffix = this.tbFileSuffix.Text;
            gp.FileDtFormat = this.tbFileDTFormat.Text;                        
            #endregion

            string codePageName = ComboxLoader.GetEncoding(this.cbCodePage);
            FileOutboundAdapterConfigMgt.FileOutAdapterConfig.OutGeneralParams.CodePageName = codePageName;

            string FileName = Application.StartupPath + "\\" + FileOutboundAdapterConfigMgt.FileName;
            if (!FileOutboundAdapterConfigMgt.Save(FileName))
            {
                if (FileOutboundAdapterConfigMgt.LastException != null)
                    Program.log.Write(LogType.Error, "Cannot save information to configuration file: " + FileName);
                MessageBox.Show(FileOutboundAdapterConfigMgt.LastException.Message);
            }
        }
        #endregion

        #region Show channel information
        public void ShowChannelSetInformation()
        {
            int i = 0;
            lstvChannel.Items.Clear();
            foreach (FileOutChannel channel in channelSet)
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
                lstvChannel.Items.Add(viewItem);
            }
        }
        #endregion

        #region Show Lookup Table Information
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
                viewItem.SubItems.Add(j.ToString());
                //Add table source
                if (true)
                {
                    viewItem.SubItems.Add("Inner table");
                }
                else
                {
                    viewItem.SubItems.Add("Outer table");
                }
                lstvLUT.Items.Add(viewItem);
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
        private void lstvLUT_DoubleClick(object sender, EventArgs e)
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

            FileOutChannel copyChannel = channelSet[channelIndex].Clone();

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
        private void lstvChannel_DoubleClick(object sender, EventArgs e)
        {
            FChannel frm = new FChannel(this, channelSet, channelIndex);
            frm.ShowDialog(pMain);

            lstvChannel.Items[channelIndex].Selected = true;
        }

        private void btnChannelDelete_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show(this, "Are you sure to delete this channel?", "Confirm delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
            //return FileOutboundAdapterConfigMgt.Save(Application.StartupPath+"\\"+FileOutboundAdapterConfigMgt.FileName);   
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

        #region General Events
        private void btSelFilePath_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "Select Destination Folder";
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.tbFilePath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btLoadDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to reload all the default setting?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            FileOutboundAdapterConfigMgt.LoadDefault();
            Initialization();
            
        }

        private void btSelPreGWDBField_Click(object sender, EventArgs e)
        {
            FSelFields frm = new FSelFields();

            XCollection<GWDataDBField> fields = FileOutboundAdapterConfigMgt.FileOutAdapterConfig.OutGeneralParams.PreGWDataDBFields;

            if (frm.ShowDialog(null, fields) != DialogResult.OK)
                return;

            StringBuilder sb = new StringBuilder();

            foreach (GWDataDBField f in fields)
            {
                sb.Append("[%" + f.GetFullFieldName().Replace(".","_") + "%]"+"_");
            }
           
            this.tbFilePrefix.Text = sb.ToString();

            
        }

        #endregion


    }
}
