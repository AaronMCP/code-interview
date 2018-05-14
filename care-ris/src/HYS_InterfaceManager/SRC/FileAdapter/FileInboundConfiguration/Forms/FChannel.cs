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
using HYS.FileAdapter.Configuration;
using HYS.Common.Xml;

namespace HYS.FileAdapter.FileInboundAdapterConfiguration.Forms
{
    public partial class FChannel : Form
    {
        #region Local members
        FFileInConfig parentForm;
        XCollection<FileInChannel> channelSet;
        string type;
        FileInChannel channel;
        int channelIndex;
        public XCollection<FileInQueryCriteriaItem> criteriaList;
        public XCollection<FileInQueryResultItem> resultList;
        bool exist = false;
        List<string> nameSet = new List<string>();
        int criteriaIndex = -1;
        int resultIndex = -1;
        #endregion
         
        #region Constructor
        public FChannel(FFileInConfig frm, XCollection<FileInChannel> channels)
        {
            InitializeComponent();
            parentForm = frm;
            channelSet = channels;
            type = "Add";
            channel = new FileInChannel();
            criteriaList = new XCollection<FileInQueryCriteriaItem>();
            resultList = new XCollection<FileInQueryResultItem>();

            this.Text = "Add Channel";
            GetChannelNameSet(channels);
        }

        /// <summary>
        /// Copy Channel
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="channels"></param>
        /// <param name="copyChannel"></param>
        public FChannel(FFileInConfig frm, XCollection<FileInChannel> channels, FileInChannel copyChannel)
        {
            InitializeComponent();
            parentForm = frm;
            channelSet = channels;
            type = "Add";

            channel = copyChannel;  
            criteriaList = channel.Rule.QueryCriteria.MappingList;
            resultList = channel.Rule.QueryResult.MappingList;

            this.Text = "Add Channel";
            GetChannelNameSet(channels);
            ShowChannel();
        }

        public FChannel(FFileInConfig frm,XCollection<FileInChannel> channels, int index)
        {
            InitializeComponent();
            parentForm = frm;
            channelSet = channels;
            type = "Edit";
            channel = channels[index];
            channelIndex = index;
            criteriaList = channel.Rule.QueryCriteria.MappingList;
            resultList = channel.Rule.QueryResult.MappingList;

            this.Text = "Edit Channel";
            GetChannelNameSet(channels);
            ShowChannel();
        }
        #endregion

        #region Checkout the identity of channel name
        private bool IsChannelExist(string channelName) {
            return nameSet.Contains(channelName);
        }

        private void GetChannelNameSet(XCollection<FileInChannel> channels){
            foreach ( FileInChannel channel in channelSet )
            {
                nameSet.Add(channel.ChannelName);
            }
        }
        #endregion

        #region Show and Save
        private void ShowChannel() {
            this.txtChannelName.Text = channel.ChannelName;
            this.checkBoxStatus.Checked = channel.Enable;
            //Show Criteria
            if (channel.Rule.QueryCriteria.Type == QueryCriteriaRuleType.SQLStatement)
            {
                radiobtnStatement.Checked = true;
                txtStatement.Text = channel.Rule.QueryCriteria.SQLStatement;
            }
            else {
                radiobtnDataset.Checked = true;
                ShowCriteriaList();
            }

            //Show Result
            ShowResultList();
        }

        public void ShowCriteriaList() {
            int i = 0;
            lstvCriteria.Items.Clear();
            foreach (FileInQueryCriteriaItem item in criteriaList) {
                i++;
                ListViewItem viewItem = new ListViewItem(i.ToString());
                viewItem.SubItems.Add(item.ThirdPartyDBPatamter.SectionName);
                viewItem.SubItems.Add(item.ThirdPartyDBPatamter.FieldName);                
                viewItem.SubItems.Add(item.Operator.ToString());
                viewItem.SubItems.Add(item.Translating.ConstValue);
                viewItem.SubItems.Add(item.Type.ToString());
                lstvCriteria.Items.Add(viewItem);
            }
        }

        public void ShowResultList() {
            int i = 0;
            lstvResult.Items.Clear();
            foreach( FileInQueryResultItem item in resultList){
                i++;
                ListViewItem viewItem = new ListViewItem(i.ToString());                
                viewItem.SubItems.Add(item.ThirdPartyDBPatamter.SectionName);
                viewItem.SubItems.Add(item.ThirdPartyDBPatamter.FieldName);
                viewItem.SubItems.Add(item.GWDataDBField.Table.ToString());
                viewItem.SubItems.Add(item.GWDataDBField.FieldName);
                viewItem.SubItems.Add(item.Translating.ToString());
                viewItem.SubItems.Add(item.RedundancyFlag.ToString());
                lstvResult.Items.Add(viewItem);
            }
        }

        private void Save() {
            channel.ChannelName = this.txtChannelName.Text;
            channel.Enable = this.checkBoxStatus.Checked;
            if (radiobtnStatement.Checked == true)
            {
                channel.Rule.QueryCriteria.Type = QueryCriteriaRuleType.SQLStatement;
                channel.Rule.QueryCriteria.SQLStatement = this.txtStatement.Text;
                channel.Rule.QueryCriteria.MappingList.Clear();
            }
            else {
                channel.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
                channel.Rule.QueryCriteria.SQLStatement = "";
                channel.Rule.QueryCriteria.MappingList = criteriaList;
            }
            channel.Rule.QueryResult.MappingList = resultList;

            if(type == "Add"){
                channelSet.Add(channel);
            }
        }
        #endregion       

        #region Criteria events
        private void btnCriteriaAdd_Click(object sender, EventArgs e)
        {
            int count = criteriaList.Count;

            FQueryCriteria frm = new FQueryCriteria(this,criteriaList);
            frm.Text = "Add Filter Item";
            frm.ShowDialog(this);

            if (criteriaList.Count > count)
            {
                criteriaIndex = count;
                lstvCriteria.Items[criteriaIndex].Selected = true;
            }
        }

        private void btnCriteriaModify_Click(object sender, EventArgs e)
        {
            FQueryCriteria frm = new FQueryCriteria(this,criteriaList,criteriaIndex);
            frm.Text = "Edit Filter Item";
            frm.ShowDialog(this);

            lstvCriteria.Items[criteriaIndex].Selected = true;
        }
        private void lstvCriteria_DoubleClick(object sender, EventArgs e)
        {
            FQueryCriteria frm = new FQueryCriteria(this, criteriaList, criteriaIndex);
            frm.Text = "Edit Filter Item";
            frm.ShowDialog(this);

            lstvCriteria.Items[criteriaIndex].Selected = true;
        }

        private void btnCriteriaDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this query filter item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                criteriaList.Remove(criteriaList[criteriaIndex]);
                ShowCriteriaList();
                if (criteriaList.Count < 1)  //if there is no item in CriteriaList, unenable the button of modify and delete
                {
                    btnCriteriaModify.Enabled = false;
                    btnCriteriaDelete.Enabled = false;
                }
                else
                {
                    if (criteriaIndex >= criteriaList.Count) //if the last item be removed, focus moves up
                    {
                        criteriaIndex--;
                    }
                    lstvCriteria.Items[criteriaIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Result events
        private void btnResultAdd_Click(object sender, EventArgs e)
        {
            int count = resultList.Count;

            FQueryResult frm = new FQueryResult(this,resultList);
            frm.Text = "Add Result Item";
            frm.ShowDialog(this);

            if (resultList.Count > count)
            {
                resultIndex = count;
                lstvResult.Items[resultIndex].Selected = true;
            }
        }

        private void btnResultModify_Click(object sender, EventArgs e)
        {
            FQueryResult frm = new FQueryResult(this, resultList,resultIndex);
            frm.Text = "Edit Result Item";
            frm.ShowDialog(this);

            lstvResult.Items[resultIndex].Selected = true;
        }
        private void lstvResult_DoubleClick(object sender, EventArgs e)
        {
            FQueryResult frm = new FQueryResult(this, resultList, resultIndex);
            frm.Text = "Edit Result Item";
            frm.ShowDialog(this);

            lstvResult.Items[resultIndex].Selected = true;
        }

        private void btnResultDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this query result item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                resultList.Remove(resultList[resultIndex]);
                ShowResultList();
                if (resultList.Count < 1)  //if there is no item in CriteriaList, unenable the button of modify and delete
                {
                    btnResultModify.Enabled = false;
                    btnResultDelete.Enabled = false;
                }
                else
                {
                    if (resultIndex >= resultList.Count) //if the last item be removed, focus moves up
                    {
                        resultIndex--;
                    }
                    lstvResult.Items[resultIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Controls events
        private void txtChannelName_TextChanged(object sender, EventArgs e)
        {
            if (type == "Add")
            {
                if (IsChannelExist(txtChannelName.Text))
                {
                    lblExist.Visible = true;
                    exist = true;
                }
                else
                {
                    lblExist.Visible = false;
                    exist = false;
                }
            }
            else
            {
                if (IsChannelExist(txtChannelName.Text) && (txtChannelName.Text != channelSet[channelIndex].ChannelName))
                {
                    lblExist.Visible = true;
                    exist = true;
                }
                else
                {
                    lblExist.Visible = false;
                    exist = false;
                }
            }
        }

        private void radiobtnStatement_CheckedChanged(object sender, EventArgs e)
        {
            if (radiobtnStatement.Checked == true)
            {
                txtStatement.Enabled = true;
                btnStatement.Enabled = true;
                lstvCriteria.Enabled = false;
                btnCriteriaAdd.Enabled = false;
                btnCriteriaModify.Enabled = false;
                btnCriteriaDelete.Enabled = false;

                txtStatement.Text = channel.Rule.QueryCriteria.SQLStatement;
                lstvCriteria.Items.Clear();
            }
            else
            {
                txtStatement.Enabled = false;
                btnStatement.Enabled = false;
                lstvCriteria.Enabled = true;
                btnCriteriaAdd.Enabled = true;
                btnCriteriaModify.Enabled = false;
                btnCriteriaDelete.Enabled = false;

                txtStatement.Text = "";
                ShowCriteriaList();
            }
        }

        private void btnStatement_Click(object sender, EventArgs e)
        {
            FQuerySQL frm = new FQuerySQL(this);
            frm.ShowDialog(this);
        }

        private void lstvCriteria_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            criteriaIndex = e.ItemIndex;
            if (e.IsSelected == true)
            {
                btnCriteriaModify.Enabled = true;
                btnCriteriaDelete.Enabled = true;
            }
            else
            {
                btnCriteriaModify.Enabled = false;
                btnCriteriaDelete.Enabled = false;
            }
        }

        private void lstvResult_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            resultIndex = e.ItemIndex;
            if (e.IsSelected == true)
            {
                btnResultModify.Enabled = true;
                btnResultDelete.Enabled = true;
            }
            else
            {
                btnResultModify.Enabled = false;
                btnResultDelete.Enabled = false;
            }
        }

        public string SQLString {
            get { return this.txtStatement.Text; }
            set { this.txtStatement.Text = value; }
        }

        private void lstvCriteria_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvCriteria.Columns[e.Column].Tag == null)
                this.lstvCriteria.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvCriteria.Columns[e.Column].Tag;
            if (tabK)
                this.lstvCriteria.Columns[e.Column].Tag = false;
            else
                this.lstvCriteria.Columns[e.Column].Tag = true;
            this.lstvCriteria.ListViewItemSorter = new ListViewSort(e.Column, this.lstvCriteria.Columns[e.Column].Tag);
        }

        private void lstvResult_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvResult.Columns[e.Column].Tag == null)
                this.lstvResult.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvResult.Columns[e.Column].Tag;
            if (tabK)
                this.lstvResult.Columns[e.Column].Tag = false;
            else
                this.lstvResult.Columns[e.Column].Tag = true;
            this.lstvResult.ListViewItemSorter = new ListViewSort(e.Column, this.lstvResult.Columns[e.Column].Tag);
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtChannelName.Text == "")
            {
                MessageBox.Show( "Please add a channel name!", "Channel Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (exist)
            {
                MessageBox.Show(this, "Channel Name Existed!", "Channel Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Save();
            parentForm.ShowChannelSetInformation();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}