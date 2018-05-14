using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using HYS.Adapter.Base;
using HYS.SQLInboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.SQLInboundAdapterConfiguration.Controls;
using HYS.Common.Objects.Config;
using System.IO;

namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    public partial class Channel : Form
    {
#region Local members
        ActiveMode parentForm;
        XCollection<SQLInboundChanel> channelSet;
        SQLInboundChanel channel;
        string type = "";
        public XCollection<SQLInQueryCriteriaItem> criteriaItemList;  //Use for reference the QueryCriteria mappinglist in the rule
        public XCollection<SQLInQueryResultItem> resultItemList;      //Use for reference the Queryresult mappinglist in the rule
        string sqlStr;
        bool IsExist{
            get { return lblExist.Visible; }  
        }
        List<string> nameSet = new List<string>();
        #endregion

        #region Constructor
        public Channel(ActiveMode frm, XCollection<SQLInboundChanel> channels)
        {
            InitializeComponent();
            parentForm = frm;
            channelSet = channels;
            channel = new SQLInboundChanel();
            type = "Add";
            this.Text = "Add Channel";
            criteriaItemList = new XCollection<SQLInQueryCriteriaItem>();
            resultItemList = new XCollection<SQLInQueryResultItem>();
            sqlStr = "";

            GetChannelNameSet(channels);
        }

        /// <summary>
        /// Copy Channel
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="channels"></param>
        /// <param name="copyChannel"></param>
        public Channel(ActiveMode frm, XCollection<SQLInboundChanel> channels, SQLInboundChanel copyChannel)
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
        }


        int channelIndex;
        public Channel(ActiveMode frm, XCollection<SQLInboundChanel> channels, int index)
        {
            InitializeComponent();
            parentForm = frm;
            channelSet = channels;
            type = "Edit";
            this.Text = "Edit Channel";
            channel = channelSet[index];
            channelIndex = index;
            criteriaItemList = channelSet[index].Rule.QueryCriteria.MappingList;
            resultItemList = channelSet[index].Rule.QueryResult.MappingList;
            sqlStr = channelSet[index].Rule.QueryCriteria.SQLStatement;

            GetChannelNameSet(channels);
            ShowChannel(channel);
            enumCmbbxOperationType.Enabled = false;
        }
        #endregion

        #region Show and Save
        public void ShowCriteriaList() {
            int i = 0;
            lstvCriteria.Items.Clear();
            foreach(SQLInQueryCriteriaItem criteriaItem in criteriaItemList){
                i++;
                ListViewItem viewItem = new ListViewItem(i.ToString());
                viewItem.SubItems.Add(criteriaItem.ThirdPartyDBPatamter.FieldName);
                viewItem.SubItems.Add(criteriaItem.ThirdPartyDBPatamter.FieldType.ToString());

                string str;
                if (criteriaItem.IsNull) str = "(Null)";
                else if (criteriaItem.IsGetFromStorageProcedure) str = "(Get From Storage Procedure)";
                else str = criteriaItem.Translating.ConstValue;
                
                viewItem.SubItems.Add(str);
                viewItem.Tag = i - 1;
                lstvCriteria.Items.Add(viewItem);
            }
        }
        
        public void ShowResultList()
        {
            int i = 0;
            lstvResult.Items.Clear();
            foreach (SQLInQueryResultItem resultItem in resultItemList)
            {
                i++;
                ListViewItem viewItem = new ListViewItem(i.ToString());
                viewItem.SubItems.Add(resultItem.ThirdPartyDBPatamter.FieldName);  //  3rd party field
                viewItem.SubItems.Add(resultItem.ThirdPartyDBPatamter.FieldType.ToString());
                viewItem.SubItems.Add(resultItem.GWDataDBField.ToString());//  Gateway field 
                viewItem.SubItems.Add(resultItem.Translating.ToString());
                viewItem.SubItems.Add(resultItem.RedundancyFlag.ToString());
                viewItem.Tag = i - 1;
                lstvResult.Items.Add(viewItem);
            }
        }

        public void ShowChannel( SQLInboundChanel channel)
        {
            this.checkBoxSQLText.Checked = channel.CallSPAsSQLText;

            //SQLInboundChanel channel = channelSet[channelIndex];
            txtChannelName.Text = channel.ChannelName;
            checkBoxStatus.Checked = channel.Enable;
            enumCmbbxOperationType.Text = channel.OperationType.ToString();
            txtModeName.Text = channel.OperationName;
            //Show Criteria
            if (enumCmbbxOperationType.Text == ThrPartyDBOperationType.StorageProcedure.ToString())
            {
                lblCriteria.Text = "Input parameters";
                txtStatement.Visible = false;
                lstvCriteria.Visible = true;
                btnCriteriaAdd.Visible = true;
                btnCriteriaModify.Visible = true;
                btnCriteriaDelete.Visible = true;
                
                txtStatement.Text = "";
                ShowCriteriaList();
            }
            else
            {
                lblCriteria.Text = "SQL Statement";
                txtStatement.Visible = true;
                lstvCriteria.Visible = false;
                btnCriteriaAdd.Visible = false;
                btnCriteriaModify.Visible = false;
                btnCriteriaDelete.Visible = false;

                txtStatement.Text = channel.Rule.QueryCriteria.SQLStatement;
                lstvCriteria.Items.Clear();
            }
            //Show Result
            ShowResultList();
        }

        public void Save()
        {
            channel.CallSPAsSQLText = this.checkBoxSQLText.Checked;

            channel.ChannelName = txtChannelName.Text;
            channel.Enable = checkBoxStatus.Checked;
            channel.OperationType = (ThrPartyDBOperationType)Enum.Parse(typeof(ThrPartyDBOperationType), enumCmbbxOperationType.Text);
            channel.OperationName = txtModeName.Text;

            //Save Query Criteria
            if (enumCmbbxOperationType.Text == ThrPartyDBOperationType.StorageProcedure.ToString())
            {
                channel.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
                channel.Rule.QueryCriteria.SQLStatement = "";
                channel.Rule.QueryCriteria.MappingList = criteriaItemList;
            }
            else
            {
                channel.Rule.QueryCriteria.Type = QueryCriteriaRuleType.SQLStatement;
                channel.Rule.QueryCriteria.SQLStatement = txtStatement.Text;
                channel.Rule.QueryCriteria.MappingList.Clear();
            }

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

            if (MessageBox.Show( "Are you sure to delete this query criteria item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

        #region Controls events
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
                panelModeName.Visible = true;

                lblCriteria.Text = "Input parameter";
                lstvCriteria.Visible = true;
                btnCriteriaAdd.Visible = true;
                btnCriteriaModify.Visible = true;
                btnCriteriaDelete.Visible = true;
                txtStatement.Visible = false;
                //btnApply.Visible = false;
                btnApply.Text = "Advance";
                txtStatement.Text = "";
                ShowCriteriaList();
            }
            else
            {
                lblModeName.Text = enumCmbbxOperationType.Text + " Name";
                panelModeName.Visible = false;

                lblCriteria.Text = "SQL Statement";
                lstvCriteria.Visible = false;
                btnCriteriaAdd.Visible = false;
                btnCriteriaModify.Visible = false;
                btnCriteriaDelete.Visible = false;
                txtStatement.Visible = true;
                //btnApply.Visible = true;
                btnApply.Text = "Apply";
                txtStatement.Text = channel.Rule.QueryCriteria.SQLStatement;
                criteriaItemList.Clear();
            }

            if (enumCmbbxOperationType.Text != ThrPartyDBOperationType.StorageProcedure.ToString())
            {
                if (txtStatement.Text.Trim() == "")
                {
                    if (parentForm.DBconfig.ConnectionParameter.FileConnection) txtStatement.Text = "select * from {FileName}";
                    else txtStatement.Text = "select * from";
                }
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (btnApply.Text == "Apply")
            {
                SQLTestResult frm = new SQLTestResult();
                if (frm.ShowDataResult(parentForm.ConnectionStr, txtStatement.Text))
                {
                    frm.ShowDialog(this);
                }
            }
            else if (btnApply.Text == "Advance")
            {
                FormInputParameterSP frm = new FormInputParameterSP(channel);
                frm.ShowDialog(this);
            }
        }

        private void checkBoxStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxStatus.Checked == true)
            {
                panelMain.Enabled = true;
            }
            else
            {
                panelMain.Enabled = false;
            }
        }

        private void checkBoxSQLText_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxSQLText.Checked)
            {
                lblModeName.Text = "Storage Procedure Call";
            }
            else
            {
                lblModeName.Text = "Storage Procedure Name";
            }
        }
        #endregion

        #region Checkout the identity and valid of channel name
        private void GetChannelNameSet(XCollection<SQLInboundChanel> channels)
        {
            foreach (SQLInboundChanel channel in channels)
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
                MessageBox.Show( "The channel name should only contain charactor, number or '_', and should begins with charactor, please input another name.",
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
                MessageBox.Show( "Channel name existed!", "Channel Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Save();
            parentForm.ShowChannelSet();

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion    

        private void ApplyFileQuery()
        {
            string sqlStr = txtStatement.Text;
            if (sqlStr.IndexOf("{FileName}") >= 0)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = ConfigHelper.GetFullPath(parentForm.DBconfig.ConnectionParameter.FileFolder);
                dlg.Multiselect = false;
                dlg.ShowReadOnly = false;
                dlg.Title = "Select a data file for testing.";
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog(this) != DialogResult.OK) return;

                string fileLocation = dlg.FileName;
                string fileDirectory = Path.GetDirectoryName(fileLocation);
                if (fileDirectory.ToLowerInvariant() != dlg.InitialDirectory.ToLowerInvariant())
                {
                    MessageBox.Show(this, string.Format("Please select a data file in {0} folder.", dlg.InitialDirectory),
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ApplyFileQuery();
                    return;
                }

                string fileName = dlg.SafeFileName;
                sqlStr = sqlStr.Replace("{FileName}", string.Format("[{0}]", fileName));
            }

            SQLTestResult frm = new SQLTestResult();
            if (frm.ShowDataResult(parentForm.DBconfig.ConnectionParameter.FileConnectionString, sqlStr))
            {
                frm.ShowDialog(this);
            }
        }
        private void Channel_Load(object sender, EventArgs e)
        {
            if (parentForm.DBconfig.ConnectionParameter.FileConnection)
            {
                this.enumCmbbxOperationType.SelectedIndex = 1;
                this.enumCmbbxOperationType.Enabled = false;
                this.labelFileSQLNote.Visible = true;
                this.btnApply.Visible = false;
                this.buttonTest.Visible = true;
                this.buttonTest.Location = this.btnApply.Location;
            }
        }
        private void buttonTest_Click(object sender, EventArgs e)
        {
            if (parentForm.DBconfig.ConnectionParameter.FileConnection)
            {
                ApplyFileQuery();
                return;
            }
        }
    }
}