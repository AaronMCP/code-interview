using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.SQLInboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.SQLInboundAdapterConfiguration.Forms;

namespace HYS.SQLInboundAdapterConfiguration.Controls
{
    public partial class ParameterPage : UserControl
    {
        #region Local members
        SQLInboundChanel channel;
        XCollection<SQLInQueryResultItem> parameterList;
        #endregion

        #region Constructor
        public ParameterPage(SQLInboundChanel ch)
        {
            InitializeComponent();
            channel = ch;
            parameterList = ch.Rule.QueryResult.MappingList;
        }
        #endregion

        #region Show and Save
        public void ShowParameter()
        {
            //Show ListView
            int i = 0;
            lstvParameter.Items.Clear();
            foreach (SQLInQueryResultItem resultItem in parameterList)
            {
                i++;
                ListViewItem viewItem = new ListViewItem(i.ToString());
                if (resultItem.Translating.Type == TranslatingType.FixValue)
                {
                    viewItem.SubItems.Add("");
                }
                else
                {
                    //viewItem.SubItems.Add(resultItem.TargetField);
                    viewItem.SubItems.Add(resultItem.SourceField);
                }
                viewItem.SubItems.Add(resultItem.GWDataDBField.ToString());//  Gateway field 
                viewItem.SubItems.Add(resultItem.Translating.ToString());
                viewItem.SubItems.Add(resultItem.RedundancyFlag.ToString());
                viewItem.Tag = i - 1;
                lstvParameter.Items.Add(viewItem);
            }
        }

        public void Save() {
            channel.Rule.QueryResult.MappingList = parameterList;
        }
        #endregion

        #region Parameter events
        private void btnParameterAdd_Click(object sender, EventArgs e)
        {
            int count = parameterList.Count;
            QueryParameter frm = new QueryParameter(parameterList);
            frm.ShowDialog(this);

            if (parameterList.Count > count)
            {
                ShowParameter();

                int i = 0;
                foreach (ListViewItem viewItem in lstvParameter.Items) {
                    if ((int)viewItem.Tag == count) {
                        lstvParameter.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnParameterModify_Click(object sender, EventArgs e)
        {
            int selectIndex = lstvParameter.SelectedIndices[0];
            int itemIndex = (int)lstvParameter.SelectedItems[0].Tag;

            QueryParameter frm = new QueryParameter(parameterList, itemIndex);
            frm.ShowDialog(this);

            ShowParameter();
            lstvParameter.Items[selectIndex].Selected = true;
        }
        private void lstvParameter_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = lstvParameter.SelectedIndices[0];
            int itemIndex = (int)lstvParameter.SelectedItems[0].Tag;

            QueryParameter frm = new QueryParameter(parameterList, itemIndex);
            frm.ShowDialog(this);

            ShowParameter();
            lstvParameter.Items[selectIndex].Selected = true;
        }

        private void btnParameterDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this parameter item?", "Delete Parameter Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int selectIndex = lstvParameter.SelectedIndices[0];
                int itemIndex = (int)lstvParameter.SelectedItems[0].Tag;

                parameterList.Remove(parameterList[itemIndex]);
                ShowParameter();
                if (parameterList.Count < 1)  //if there is no item in CriteriaList, unenable the button of modify and delete
                {
                    btnResultModify.Enabled = false;
                    btnResultDelete.Enabled = false;
                }
                else
                {
                    if (selectIndex >= parameterList.Count) //if the last item be removed, focus moves up
                    {
                        selectIndex--;
                    }
                    lstvParameter.Items[selectIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Controls events
        private void lstvParameter_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
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

        private void lstvParameter_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvParameter.Columns[e.Column].Tag == null)
                this.lstvParameter.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvParameter.Columns[e.Column].Tag;
            if (tabK)
                this.lstvParameter.Columns[e.Column].Tag = false;
            else
                this.lstvParameter.Columns[e.Column].Tag = true;
            this.lstvParameter.ListViewItemSorter = new ListViewSort(e.Column, this.lstvParameter.Columns[e.Column].Tag);
        }
        #endregion

    }
}
