using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;
using HYS.Adapter.Monitor.Utility;
using HYS.Adapter.Monitor.Objects;

namespace HYS.Adapter.Monitor.Controls
{
    public partial class UCListviewMode : UserControl
    {
        #region Local members
        private XCollection<QueryCriteriaItem> _filterItemList;
        public XCollection<QueryCriteriaItem> FilterItemList
        {
            get { return _filterItemList; }
        }
        private FilterDataInfo _queryDaraInfo;
        #endregion

        public UCListviewMode(FilterDataInfo queryDaraInfo)
        {
            InitializeComponent();
            _queryDaraInfo = queryDaraInfo;
            _filterItemList = new XCollection<QueryCriteriaItem>();
            CloneFilterItem(_filterItemList, _queryDaraInfo.FilterItemList);
        }

        private void CloneFilterItem(XCollection<QueryCriteriaItem> target, XCollection<QueryCriteriaItem> source)
        {
            foreach (QueryCriteriaItem item in source) {
                target.Add(item);
            }
        }

        public void ShowListView() {
            int i = 0;
            lstvFilter.Items.Clear();
            foreach (QueryCriteriaItem item in _filterItemList) {
                i++;
                
                ListViewItem viewItem = new ListViewItem(i.ToString());
                viewItem.SubItems.Add(item.GWDataDBField.GetFullFieldName());
                viewItem.SubItems.Add(item.Operator.ToString());
                viewItem.SubItems.Add(item.Translating.ConstValue);
                viewItem.SubItems.Add(item.Type.ToString());
                lstvFilter.Items.Add(viewItem);
            }
        }

        public string GetQueryString() {
            return QueryRuleControl.GetFilterString(_filterItemList);
        }

        public void Save() {
            _queryDaraInfo.FilterItemList = _filterItemList;
            _queryDaraInfo.FilterMode = FilterMode.AdvancedListView;
        }

        #region Filter items events
        int itemIndex = -1;
        private void lstvFilter_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                itemIndex = e.ItemIndex;
                btnModify.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                itemIndex = -1;
                btnModify.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int count = _filterItemList.Count;

            FilterItem frm = new FilterItem(_filterItemList);
            frm.ShowDialog(this);

            if (_filterItemList.Count > count)
            {
                ShowListView();
                itemIndex = count;
                lstvFilter.Items[itemIndex].Selected = true;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            FilterItem frm = new FilterItem(_filterItemList, itemIndex);
            frm.ShowDialog(this);

            ShowListView();
            lstvFilter.Items[itemIndex].Selected = true;
        }
        private void lstvFilter_DoubleClick(object sender, EventArgs e)
        {
            FilterItem frm = new FilterItem(_filterItemList, itemIndex);
            frm.ShowDialog(this);

            ShowListView();
            lstvFilter.Items[itemIndex].Selected = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure to delete this filter item?", "Delete Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _filterItemList.Remove(_filterItemList[itemIndex]);
                ShowListView();
                if (_filterItemList.Count < 1)  //if there is no item in CriteriaList, unenable the button of modify and delete
                {
                    btnModify.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    if (itemIndex >= _filterItemList.Count) //if the last item be removed, focus moves up
                    {
                        itemIndex--;
                    }
                    lstvFilter.Items[itemIndex].Selected = true;
                }
            }
        }
        #endregion   
    }
}
