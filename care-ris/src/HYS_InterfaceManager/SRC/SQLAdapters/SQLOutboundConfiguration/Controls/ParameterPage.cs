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
    public partial class ParameterPage : UserControl
    {
        #region Local members
        SQLOutboundChanel channel;
        XCollection<SQLOutQueryCriteriaItem> criteriaList;
        XCollection<SQLOutQueryResultItem> resultList;
        #endregion

        #region Constructor
        public ParameterPage(SQLOutboundChanel ch)
        {
            InitializeComponent();
            channel = ch;
            criteriaList = ch.Rule.QueryCriteria.MappingList;
            resultList = ch.Rule.QueryResult.MappingList;
        }
        #endregion

        #region Show and Save
        public void ShowSP() {
            ShowCriteriaList();
            ShowResultList();
        }

        public void ShowCriteriaList() {
            int i = 0;
            lstvInParameter.Items.Clear();
            if (criteriaList == null)
            {
                return;
            }
            foreach (SQLOutQueryCriteriaItem criteriaItem in criteriaList)
            {
                i++;
                ListViewItem viewItem = new ListViewItem(i.ToString());
                if (criteriaItem.Translating.Type == TranslatingType.FixValue)   //Parameter Save here
                {
                    viewItem.SubItems.Add("");
                }
                else {
                    viewItem.SubItems.Add(criteriaItem.SourceField);
                }               
                viewItem.SubItems.Add(criteriaItem.GWDataDBField.ToString());
                viewItem.SubItems.Add(criteriaItem.Translating.ToString());
                viewItem.SubItems.Add(criteriaItem.Operator.ToString());
                viewItem.SubItems.Add(criteriaItem.Type.ToString());
                viewItem.Tag = i - 1;
                lstvInParameter.Items.Add(viewItem);
            }
        }

        public void ShowResultList()
        {
            int i = 0;
            lstvOutParameter.Items.Clear();
            foreach (SQLOutQueryResultItem resultItem in resultList)
            {
                i++;
                ListViewItem viewItem = new ListViewItem(i.ToString());
                viewItem.SubItems.Add(resultItem.ThirdPartyDBPatamter.FieldName);  //  3rd party field
                viewItem.SubItems.Add(resultItem.GWDataDBField.ToString());//  Gateway field 
                viewItem.SubItems.Add(resultItem.Translating.ToString());
                viewItem.Tag = i - 1;
                lstvOutParameter.Items.Add(viewItem);
            }
        }

        public void Save()
        {
            channel.Rule.QueryCriteria.MappingList = criteriaList;
            channel.Rule.QueryResult.MappingList = resultList;
        }
        #endregion

        #region Input parameter events
        private void btnInParameterAdd_Click(object sender, EventArgs e)
        {
            int count = criteriaList.Count;

            QueryInParameter frm = new QueryInParameter(criteriaList);
            frm.ShowDialog(this);

            if (criteriaList.Count > count)
            {
                ShowCriteriaList();
                int i = 0;
                foreach (ListViewItem viewItem in lstvInParameter.Items)
                {
                    if ((int)viewItem.Tag == count)
                    {
                        lstvInParameter.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnInParameterModify_Click(object sender, EventArgs e)
        {
            int selectIndex = lstvInParameter.SelectedIndices[0];
            int itemIndex = (int)lstvInParameter.SelectedItems[0].Tag;

            QueryInParameter frm = new QueryInParameter(criteriaList, itemIndex);
            frm.ShowDialog(this);

            ShowCriteriaList();
            lstvInParameter.Items[selectIndex].Selected = true;
        }

        private void lstvInParameter_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = lstvInParameter.SelectedIndices[0];
            int itemIndex = (int)lstvInParameter.SelectedItems[0].Tag;

            QueryInParameter frm = new QueryInParameter(criteriaList, itemIndex);
            frm.ShowDialog(this);

            ShowCriteriaList();
            lstvInParameter.Items[selectIndex].Selected = true;
        }

        private void btnInParameterDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this input parameter information?", "Delete Parameter Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int selectIndex = lstvInParameter.SelectedIndices[0];
                int itemIndex = (int)lstvInParameter.SelectedItems[0].Tag;

                criteriaList.Remove(criteriaList[itemIndex]);
                ShowCriteriaList();
                if (criteriaList.Count < 1)  //if there is no item in CriteriaList, unenable the button of modify and delete
                {
                    btnResultModify.Enabled = false;
                    btnResultDelete.Enabled = false;
                }
                else
                {
                    if (selectIndex >= criteriaList.Count) //if the last item be removed, focus moves up
                    {
                        selectIndex--;
                    }
                    lstvInParameter.Items[selectIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Output parameter events
        private void btnOutParameterAdd_Click(object sender, EventArgs e)
        {
            int count = resultList.Count;

            QueryOutParameter frm = new QueryOutParameter(resultList);
            frm.ShowDialog(this);

            if (resultList.Count > count)
            {
                ShowResultList();

                int i = 0;
                foreach (ListViewItem viewItem in lstvOutParameter.Items)
                {
                    if ((int)viewItem.Tag == count)
                    {
                        lstvOutParameter.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnOutParameterModify_Click(object sender, EventArgs e)
        {
            int selectIndex = lstvOutParameter.SelectedIndices[0];
            int itemIndex = (int)lstvOutParameter.SelectedItems[0].Tag;

            QueryOutParameter frm = new QueryOutParameter(resultList, itemIndex);
            frm.ShowDialog(this);

            ShowResultList();
            lstvOutParameter.Items[selectIndex].Selected = true;
        }
        private void lstvOutParameter_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = lstvOutParameter.SelectedIndices[0];
            int itemIndex = (int)lstvOutParameter.SelectedItems[0].Tag;

            QueryOutParameter frm = new QueryOutParameter(resultList, itemIndex);
            frm.ShowDialog(this);
                        
            ShowResultList();
            lstvOutParameter.Items[selectIndex].Selected = true;
        }

        private void btnOutParameterDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this output parameter information?", "Delete Parameter Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int selectIndex = lstvOutParameter.SelectedIndices[0];
                MessageBox.Show(selectIndex.ToString());
                int itemIndex = (int)lstvOutParameter.SelectedItems[0].Tag;

                resultList.Remove(resultList[itemIndex]);
                ShowResultList();
                if (resultList.Count < 1)  //if there is no item in CriteriaList, unenable the button of modify and delete
                {
                    btnResultModify.Enabled = false;
                    btnResultDelete.Enabled = false;
                }
                else
                {
                    if (selectIndex >= resultList.Count) //if the last item be removed, focus moves up
                    {
                        selectIndex--;
                    }
                    MessageBox.Show(selectIndex.ToString());
                    lstvOutParameter.Items[selectIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Controls events
        private void lstvOutParameter_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
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

        private void lstvOutParameter_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvOutParameter.Columns[e.Column].Tag == null)
                this.lstvOutParameter.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvOutParameter.Columns[e.Column].Tag;
            if (tabK)
                this.lstvOutParameter.Columns[e.Column].Tag = false;
            else
                this.lstvOutParameter.Columns[e.Column].Tag = true;
            this.lstvOutParameter.ListViewItemSorter = new ListViewSort(e.Column, this.lstvOutParameter.Columns[e.Column].Tag);
        }

        private void lstvInParameter_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
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

        private void lstvInParameter_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvInParameter.Columns[e.Column].Tag == null)
                this.lstvInParameter.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvInParameter.Columns[e.Column].Tag;
            if (tabK)
                this.lstvInParameter.Columns[e.Column].Tag = false;
            else
                this.lstvInParameter.Columns[e.Column].Tag = true;
            this.lstvInParameter.ListViewItemSorter = new ListViewSort(e.Column, this.lstvInParameter.Columns[e.Column].Tag);
        }
        #endregion
    }
}
