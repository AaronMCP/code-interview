using System;
using System.Reflection;
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
using HYS.FileAdapter.FileInboundAdapterConfiguration.Forms;
using HYS.Common.Xml;

namespace HYS.FileAdapter.FileInboundAdapterConfiguration
{
    public partial class FFileInConfig : Form,IConfigUI
    {
        #region Local members 
        XCollection<FileInChannel> channelSet;
        XCollection<LookupTable> LUTableSet;
        int channelIndex;
        int tableIndex;
        #endregion

        #region Constructor
        public FFileInConfig()
        {
            InitializeComponent();
            ComboxLoader.LoadEncoding(this.cbCodePage);
            Initialization();

            RefreshForm();

            this.btLoadDefault.Visible = Program.bRunSingle;
        }
        #endregion

        #region Initialization and Save
        private void Initialization() {            
            channelSet = FileInboundAdapterConfigMgt.FileInAdapterConfig.InboundChanels;
            LUTableSet = FileInboundAdapterConfigMgt.FileInAdapterConfig.LookupTables;

            FillCombox();
            
            #region ServerFileParameters
            FileInGeneralParams gp = FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams;
            this.mtbTimerInterval.Text = gp.TimerInterval.ToString();
            this.tbFilePath.Text = gp.FilePath;
            this.tbFilePrefix.Text = gp.FilePrefix;
            this.tbFileSuffix.Text = gp.FileSuffix;
            this.tbFilePathForMove.Text = gp.InFileMovePath;
            this.cbFTTAR.Text = gp.FileTreatTypeAfterRead.ToString();            
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
                //Never occur as FileInAdapterConfigMgt.FileInAdapterConfig was be constuctured
                channelSet = new XCollection<FileInChannel>();
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
                //Never occur as FileInAdapterConfigMgt.FileInAdapterConfig was be constuctured
                LUTableSet = new XCollection<LookupTable>();
            }
            #endregion

            string codePageName = FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.CodePageName;
            ComboxLoader.SetEncoding(this.cbCodePage, codePageName);
        }

        private void FillCombox()
        {
            this.cbFTTAR.Items.Clear();

            Type t = typeof( FileInGeneralParams.InFileTreatTypeAfterRead);
            FieldInfo[] fis = t.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo fi in fis)
            {
                this.cbFTTAR.Items.Add(fi.Name.ToString());
            }

        }


        private void RefreshForm()
        {
            if (this.cbFTTAR.Text == FileInGeneralParams.InFileTreatTypeAfterRead.Move.ToString())
            {
                this.labFilePathForMove.Visible = true;
                this.tbFilePathForMove.Visible = true;
                this.btSelFilePathForMove.Visible = true;
            }
            else
            {
                this.labFilePathForMove.Visible = false;
                this.tbFilePathForMove.Visible = false;
                this.btSelFilePathForMove.Visible = false;
            }
        }

        private void Save()
        {
            #region Check input
            if (this.cbFTTAR.Text.ToString() == FileInGeneralParams.InFileTreatTypeAfterRead.Move.ToString())
            {
                if (this.tbFilePathForMove.Text == this.tbFilePath.Text)
                {
                    throw new Exception("File Path is not allow identical to file path for move!");
                }
            }
            #endregion
            #region Server File parameter
            FileInGeneralParams gp = FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams;
            gp.TimerInterval = Convert.ToInt32(this.mtbTimerInterval.Text);
            gp.FilePath = this.tbFilePath.Text;
            gp.FilePrefix = this.tbFilePrefix.Text;
            gp.FileSuffix = this.tbFileSuffix.Text ;
            gp.InFileMovePath = this.tbFilePathForMove.Text ;
            gp.FileTreatTypeAfterRead = (FileInGeneralParams.InFileTreatTypeAfterRead)System.Enum.Parse(typeof(FileInGeneralParams.InFileTreatTypeAfterRead), this.cbFTTAR.Text);            
            #endregion

            string codePageName = ComboxLoader.GetEncoding(this.cbCodePage);
            FileInboundAdapterConfigMgt.FileInAdapterConfig.InGeneralParams.CodePageName = codePageName;
            
            string FileName = Application.StartupPath + "\\" + FileInboundAdapterConfigMgt.FileName;
            if (!FileInboundAdapterConfigMgt.Save(FileName))
            {
                if (FileInboundAdapterConfigMgt.LastException != null)
                    Program.log.Write(LogType.Error, "Cannot Save information to Configuration file: " + FileName);
                MessageBox.Show(FileInboundAdapterConfigMgt.LastException.Message);
            }
        }
        #endregion

        #region Show channel information
        public void ShowChannelSetInformation()
        {
            int i = 0;
            lstvChannel.Items.Clear();
            foreach (FileInChannel channel in channelSet)
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
                btChannelCopy.Enabled = true;
            }
            else {
                btnChannelModify.Enabled = false;
                btnChannelDelete.Enabled = false;
                btChannelCopy.Enabled = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btChannelCopy_Click(object sender, EventArgs e)
        {
            int count = channelSet.Count;

            FileInChannel copyChannel = channelSet[channelIndex].Clone();

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
                    btChannelCopy.Enabled = false;
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
            //return FileInboundAdapterConfigMgt.Save(Application.StartupPath + "\\" + FileInboundAdapterConfigMgt.FileName);
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

        private void FFileInConfiguration_Load(object sender, EventArgs e)
        {

        }

        private void btSelFilePath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Select Folder";
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                this.tbFilePath.Text = this.folderBrowserDialog1.SelectedPath;

        }

        private void btSelFilePathForMove_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Select Folder For Move";
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                this.tbFilePathForMove.Text = this.folderBrowserDialog1.SelectedPath;
        }

        private void cbFTTAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to reload all the default setting?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
                
            FileInboundAdapterConfigMgt.LoadDefault();
            Initialization();
            this.RefreshForm();
        }

        private void lstvLUT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      
    }
}