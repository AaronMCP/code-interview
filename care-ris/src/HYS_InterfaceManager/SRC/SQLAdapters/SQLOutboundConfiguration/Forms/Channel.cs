using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.SQLOutboundAdapterConfiguration.Controls;

namespace HYS.SQLOutboundAdapterConfiguration.Forms
{
    public partial class Channel : Form
    {
        #region Local members 
        public ActiveMode parentForm;
        XCollection<SQLOutboundChanel> channelSet;
        string type = "";
        SQLOutboundChanel channel;
        public XCollection<SQLOutQueryCriteriaItem> criteriaItemList;  //Use for reference the QueryCriteria mappinglist in the rule
        public XCollection<SQLOutQueryResultItem> resultItemList;      //Use for reference the Queryresult mappinglist in the rule
        bool IsExist{
            get { return lblExist.Visible; }  
        }
        List<string> nameSet = new List<string>();

        public string QueryMode {
            get { return enumCmbbxOperationType.Text; }
        }
        #endregion

        #region Constructor 
        public Channel(ActiveMode frm, XCollection<SQLOutboundChanel> channels)
        {
            InitializeComponent();

            parentForm = frm;
            channelSet = channels;
            type = "Add";
            this.Text = "Add Channel";
            channel = new SQLOutboundChanel();
            criteriaItemList = new XCollection<SQLOutQueryCriteriaItem>();
            resultItemList = new XCollection<SQLOutQueryResultItem>();

            GetChannelNameSet(channels);

            if (parentForm.DBconfig.ConnectionParameter.FileConnection)
            {
                this.enumCmbbxOperationType.SelectedIndex = 1;
                this.enumCmbbxOperationType.Enabled = false;
                this.txtModeName.Enabled = false;
            }
        }

        /// <summary>
        /// Copy Channel
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="channels"></param>
        /// <param name="copyChannel"></param>
        public Channel(ActiveMode frm, XCollection<SQLOutboundChanel> channels, SQLOutboundChanel copyChannel)
        {
            InitializeComponent();
            parentForm = frm;
            channelSet = channels;
            type = "Add";

            channel = copyChannel;
            criteriaItemList = channel.Rule.QueryCriteria.MappingList;
            resultItemList = channel.Rule.QueryResult.MappingList;

            this.Text = "Add Channel";
            
            GetChannelNameSet(channels);
            ShowChannel(channel);
            enumCmbbxOperationType.Enabled = false;

            if (parentForm.DBconfig.ConnectionParameter.FileConnection)
            {
                this.enumCmbbxOperationType.SelectedIndex = 1;
                this.enumCmbbxOperationType.Enabled = false;
                this.txtModeName.Enabled = false;
            }
        }


        int channelIndex;
        public Channel(ActiveMode frm, XCollection<SQLOutboundChanel> channels, int index)
        {
            InitializeComponent();
            parentForm = frm;
            channelSet = channels;
            type = "Edit";
            this.Text = "Edit Channel";
            channel = channelSet[index];
            criteriaItemList = channelSet[index].Rule.QueryCriteria.MappingList;
            resultItemList = channelSet[index].Rule.QueryResult.MappingList;

            channelIndex = index;

            GetChannelNameSet(channels);
            ShowChannel(channel);
            enumCmbbxOperationType.Enabled = false;

            if (parentForm.DBconfig.ConnectionParameter.FileConnection)
            {
                this.enumCmbbxOperationType.SelectedIndex = 1;
                this.enumCmbbxOperationType.Enabled = false;
                this.txtModeName.Enabled = false;
            }
        }
        #endregion

        #region Show and Save
        public void ShowCriteriaList()
        {
            int i = 0;
            lstvCriteria.Items.Clear();
            if (criteriaItemList == null)
            {
                return;
            }
            foreach (SQLOutQueryCriteriaItem criteriaItem in criteriaItemList)
            {
                i++;
                ListViewItem viewItem = new ListViewItem(i.ToString());
                viewItem.SubItems.Add(criteriaItem.GWDataDBField.ToString());
                viewItem.SubItems.Add(criteriaItem.Operator.ToString());
                viewItem.SubItems.Add(criteriaItem.Translating.ConstValue);
                viewItem.SubItems.Add(criteriaItem.Type.ToString());
                viewItem.Tag = i - 1;
                lstvCriteria.Items.Add(viewItem);
            }
        }

        public void ShowResultList()
        {
            int i = 0;
            lstvResult.Items.Clear();
            if (resultItemList == null) {
                return;
            }
            foreach (SQLOutQueryResultItem resultItem in resultItemList)
            {
                i++;
                ListViewItem viewItem = new ListViewItem(i.ToString());
                viewItem.SubItems.Add(resultItem.ThirdPartyDBPatamter.FieldName);
                viewItem.SubItems.Add(resultItem.ThirdPartyDBPatamter.FieldType.ToString());//  3rd party field
                viewItem.SubItems.Add(resultItem.GWDataDBField.ToString());//  Gateway field
                viewItem.SubItems.Add(resultItem.Translating.ToString());
                viewItem.SubItems.Add(resultItem.RedundancyFlag.ToString());
                viewItem.Tag = i - 1;
                lstvResult.Items.Add(viewItem);
            }
        }

        public void ShowChannel(SQLOutboundChanel channel)
        {
            //SQLOutboundChanel channel = channelSet[channelIndex];
            txtChannelName.Text = channel.ChannelName;
            checkBoxStatus.Checked = channel.Enable;

            enumCmbbxOperationType.Text = channel.OperationType.ToString();
            txtModeName.Text = channel.OperationName;
            
            //Show QueryCriteria ItemList
            ShowCriteriaList();
            
            //Show Mapping ItemList
            ShowResultList();
        }

        public void Save()
        {
            channel.ChannelName = txtChannelName.Text;
            channel.Enable = checkBoxStatus.Checked;
            channel.OperationType = (ThrPartyDBOperationType)Enum.Parse(typeof(ThrPartyDBOperationType), enumCmbbxOperationType.Text);
            channel.OperationName = txtModeName.Text;
            channel.Rule.RuleName = "SP_" + Program.DeviceMgt.DeviceDirInfor.Header.Name + "_" + this.txtChannelName.Text;

            //Save Query Criteria
            channel.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            channel.Rule.QueryCriteria.SQLStatement = "";
            channel.Rule.QueryCriteria.MappingList = criteriaItemList;

            //Save Query Result
            channel.Rule.QueryResult.MappingList = resultItemList;

            if (type == "Add")
            {
                channelSet.Add(channel);
            }
        }
        #endregion

        #region Criteria events
        private void btnCriteriaAdd_Click(object sender, EventArgs e)
        {
            int count = criteriaItemList.Count;

            QueryCriteria queryCriteria = new QueryCriteria(this);
            queryCriteria.ShowDialog(this);

            if (criteriaItemList.Count > count)
            {
                int i = 0;
                foreach (ListViewItem viewItem in lstvCriteria.Items)
                {
                    if ((int)viewItem.Tag == count)
                    {
                        lstvCriteria.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnCriteriaModify_Click(object sender, EventArgs e)
        {
            int selectIndex = lstvCriteria.SelectedIndices[0];
            int itemIndex = (int)lstvCriteria.SelectedItems[0].Tag;

            QueryCriteria modifyCriteria = new QueryCriteria(this, itemIndex);
            modifyCriteria.ShowDialog(this);

            lstvCriteria.Items[selectIndex].Selected = true;
        }
        private void lstvCriteria_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = lstvCriteria.SelectedIndices[0];
            int itemIndex = (int)lstvCriteria.SelectedItems[0].Tag;

            QueryCriteria modifyCriteria = new QueryCriteria(this, itemIndex);
            modifyCriteria.ShowDialog(this);

            lstvCriteria.Items[selectIndex].Selected = true;
        }

        private void btnCriteriaDelete_Click(object sender, EventArgs e)
        {
            int selectIndex = lstvCriteria.SelectedIndices[0];
            int itemIndex = (int)lstvCriteria.SelectedItems[0].Tag;

            if (MessageBox.Show("Are you sure to delete this query criteria item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                criteriaItemList.Remove(criteriaItemList[itemIndex]);
                ShowCriteriaList();
                if (criteriaItemList.Count < 1)  //if there is no item in CriteriaList, unenable the button of modify and delete
                {
                    btnCriteriaModify.Enabled = false;
                    btnCriteriaDelete.Enabled = false;
                }
                else
                {
                    if (selectIndex >= criteriaItemList.Count) //if the last item be removed, focus moves up
                    {
                        selectIndex--;
                    }
                    lstvCriteria.Items[selectIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Result events
        private void btnResultAdd_Click(object sender, EventArgs e)
        {
            int count = resultItemList.Count;

            QueryResult queryResult = new QueryResult(this);
            queryResult.ShowDialog(this);

            if (resultItemList.Count > count)
            {
                int i = 0;
                foreach (ListViewItem viewItem in lstvResult.Items)
                {
                    if ((int)viewItem.Tag == count)
                    {
                        lstvResult.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnResultModify_Click(object sender, EventArgs e)
        {
            int selectIndex = lstvResult.SelectedIndices[0];
            int itemIndex = (int)lstvResult.SelectedItems[0].Tag;

            QueryResult modifyResult = new QueryResult(this, itemIndex);
            modifyResult.ShowDialog(this);

            lstvResult.Items[selectIndex].Selected = true;
        }
        private void lstvResult_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = lstvResult.SelectedIndices[0];
            int itemIndex = (int)lstvResult.SelectedItems[0].Tag;

            QueryResult modifyResult = new QueryResult(this, itemIndex);
            modifyResult.ShowDialog(this);

            lstvResult.Items[selectIndex].Selected = true;
        }

        private void btnResultDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this query result item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int selectIndex = lstvResult.SelectedIndices[0];
                int itemIndex = (int)lstvResult.SelectedItems[0].Tag;

                resultItemList.Remove(resultItemList[itemIndex]);
                ShowResultList();
                if (resultItemList.Count < 1)  //if there is no item in CriteriaList, unenable the button of modify and delete
                {
                    btnResultModify.Enabled = false;
                    btnResultDelete.Enabled = false;
                }
                else
                {
                    if (selectIndex >= resultItemList.Count) //if the last item be removed, focus moves up
                    {
                        selectIndex--;
                    }
                    lstvResult.Items[selectIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Control events
        private void lstvCriteria_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
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

        private void enumCmbbxOperationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (enumCmbbxOperationType.Text == ThrPartyDBOperationType.StorageProcedure.ToString())
            {
                lblModeName.Text = "Storage Procedure Name";
                lblResult.Text = "Parameter Mapping";

                lstvResult.Columns[1].Text = "Parameter Name";
                lstvResult.Columns[2].Text = "Parameter Type";

                this.panelQueryMode.Height = 67;
                this.txtModeName.Multiline = false;
                if (this.txtModeName.Lines.Length > 0) this.txtModeName.Text = this.txtModeName.Lines[0];
                
                this.columnHeaderRD.Width = 0;
            }
            else if (enumCmbbxOperationType.Text == ThrPartyDBOperationType.Table.ToString())
            {
                lblModeName.Text = enumCmbbxOperationType.Text + " Name";
                lblResult.Text = "Field Mapping";

                lstvResult.Columns[1].Text = "Field Name";
                lstvResult.Columns[2].Text = "Field Type";

                this.panelQueryMode.Height = 67;
                this.txtModeName.Multiline = false;
                if (this.txtModeName.Lines.Length > 0) this.txtModeName.Text = this.txtModeName.Lines[0];

                if (parentForm.DBconfig.ConnectionParameter.FileConnection) this.columnHeaderRD.Width = 0;
                else this.columnHeaderRD.Width = 112;
            }
            else if (enumCmbbxOperationType.Text == ThrPartyDBOperationType.SQLStatement.ToString())
            {
                lblModeName.Text = enumCmbbxOperationType.Text + "\r\n\r\nNote: please use {Parameter Name} to represent the value, e.g. if Parameter PID is mapped to GC Gateway field Patient.PatientID, SQL statement may be: insert into table (field) values ('{PID}')";
                lblResult.Text = "Parameter Mapping";

                lstvResult.Columns[1].Text = "Parameter Name";
                lstvResult.Columns[2].Text = "Parameter Type";

                this.panelQueryMode.Height = 200;
                this.txtModeName.Multiline = true;

                this.columnHeaderRD.Width = 0;
            }
        }

        private void txtChannelName_TextChanged(object sender, EventArgs e)
        {
            if (IsChannelExist(txtChannelName.Text))
            {
                lblExist.Visible = true;
            }
            else
            {
                lblExist.Visible = false;
            }
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

        private void checkBoxStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxStatus.Checked == true)
            {
                panelCriteria.Enabled = true;
                panelMapping.Enabled = true;
            }
            else
            {
                panelCriteria.Enabled = false;
                panelMapping.Enabled = false;
            }
        }
        #endregion

        #region Checkout the identity of channel name
        private void GetChannelNameSet(XCollection<SQLOutboundChanel> channels)
        {
            foreach (SQLOutboundChanel channel in channels)
            {
                nameSet.Add(channel.ChannelName);
            }
        }

        private bool IsChannelExist(string channelName)
        {
            if (type == "Add")
            {
                if (CheckItemValid.IsContain(channelName, nameSet, StringComparison.Ordinal))
                {
                    return true;
                }
            }
            else
            {
                if (CheckItemValid.IsContain(channelName, nameSet, StringComparison.Ordinal) && (txtChannelName.Text != channelSet[channelIndex].ChannelName))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsChannelNameValid(string name)
        {
            if (name == "")
            {
                MessageBox.Show( "Please add a channel name!", "Channel Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtChannelName.Focus();
                return false;
            }
            else if (!CheckItemValid.IsValid(name))
            {
                MessageBox.Show( "The channel name should only contain character, number or '_', and should begin with character, please input another name.",
                    "Channel Name Validate", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.txtChannelName.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsChannelNameValid(txtChannelName.Text))
            {
                return;
            }

            if (IsExist)
            {
                MessageBox.Show( "Channel name is existing!", "Channel Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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