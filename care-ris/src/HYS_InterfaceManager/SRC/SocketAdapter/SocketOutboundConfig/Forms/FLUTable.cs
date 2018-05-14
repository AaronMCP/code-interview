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
using HYS.SocketAdapter.Configuration;
using HYS.Common.Xml;

namespace HYS.SocketAdapter.SocketOutboundAdapterConfiguration.Forms
{
    public partial class FLUTable : Form
    {
        #region Local members
        FSocketOutConfiguration parentForm;
        XCollection<LookupTable> tableSet;
        string type;
        LookupTable table;
        int tableIndex;  //  Only use for modify
        public XCollection<LookupItem> LUItemList;  //Use for reference the look up item list
        bool exist = false;
        List<string> nameSet = new List<string>();
        #endregion

        #region Constructor
        public FLUTable(FSocketOutConfiguration frm, XCollection<LookupTable> tables)
        {
            InitializeComponent();
            parentForm = frm;
            this.tableSet = tables;
            type = "Add";
            table = new LookupTable();
            LUItemList = new XCollection<LookupItem>();
            this.Text = "Add Translation Table";

            GetTableNameSet(tables);
            lblNo.Text = "0 item(s)";
        }

        public FLUTable(FSocketOutConfiguration frm, XCollection<LookupTable> tables, int index)
        {
            InitializeComponent();
            this.parentForm = frm;
            this.tableSet = tables;
            this.type = "Edit";
            this.table = tables[index];
            tableIndex = index;
            this.LUItemList = tables[index].Table;
            this.Text = "Edit Translation Table";

            GetTableNameSet(tables);
            ShowTable();
        }
        #endregion

        #region Show and Save configuration
        private void ShowTable()
        {
            this.txtTableName.Text = table.DisplayName;
            this.lblNo.Text = LUItemList.Count + " item(s)";

            ShowLUItemList();
        }

        public void ShowLUItemList()
        {
            int i = 0;
            lstvTable.Items.Clear();
            foreach (LookupItem item in LUItemList)
            {
                i++;
                ListViewItem viewItem = new ListViewItem(i.ToString());
                viewItem.SubItems.Add(item.SourceValue);
                viewItem.SubItems.Add(item.TargetValue);
                viewItem.Tag = i - 1;
                lstvTable.Items.Add(viewItem);
            }
        }

        private void AddSave()
        {
            table.DisplayName = this.txtTableName.Text;
            table.Table = LUItemList;
            tableSet.Add(table);
        }

        public void ModifySave()
        {
            table.DisplayName = this.txtTableName.Text;
            table.Table = LUItemList;
        }
        #endregion

        #region Checkout the identity of table name
        private bool IsTableExist(string table)
        {
            return nameSet.Contains(table);
        }

        private void GetTableNameSet(XCollection<LookupTable> tables)
        {
            foreach (LookupTable table in tables)
            {
                nameSet.Add(table.DisplayName);
            }
        }
        #endregion

        #region LUItem events
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int count = LUItemList.Count;

            FLUTItem frm = new FLUTItem(this);
            frm.Text = "Add Translation Item";
            frm.ShowDialog(this);

            this.lblNo.Text = LUItemList.Count + " item(s)";
            if (LUItemList.Count > count)
            {
                int i = 0;
                foreach (ListViewItem viewItem in lstvTable.Items)
                {
                    if ((int)viewItem.Tag == count)
                    {
                        lstvTable.Items[i].Selected = true;
                        break;
                    }
                    i++;
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            int selectIndex = lstvTable.SelectedIndices[0];
            int itemIndex = (int)lstvTable.SelectedItems[0].Tag;

            FLUTItem frm = new FLUTItem(this, itemIndex);
            frm.Text = "Edit Translation Item";
            frm.ShowDialog(this);

            lstvTable.Items[selectIndex].Selected = true;
        }
        private void FLUTable_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = lstvTable.SelectedIndices[0];
            int itemIndex = (int)lstvTable.SelectedItems[0].Tag;

            FLUTItem frm = new FLUTItem(this, itemIndex);
            frm.Text = "Edit Translation Item";
            frm.ShowDialog(this);

            lstvTable.Items[selectIndex].Selected = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Are you sure to delete this field item?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int selectIndex = lstvTable.SelectedIndices[0];
                int itemIndex = (int)lstvTable.SelectedItems[0].Tag;

                LUItemList.Remove(LUItemList[itemIndex]);
                ShowTable();
                if (LUItemList.Count < 1)  //if there is no item in LUItemList, unenable the button modify and delete
                {
                    btnModify.Enabled = false;
                    btnDelete.Enabled = false;
                }
                else
                {
                    if (selectIndex >= LUItemList.Count) //if the last item be removed, focus moves up
                    {
                        selectIndex--;
                    }
                    lstvTable.Items[selectIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Controls events
        private void lstvLUTable_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                btnModify.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnModify.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void txtTableName_TextChanged(object sender, EventArgs e)
        {
            if (type == "Add")
            {
                if (IsTableExist(txtTableName.Text))
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
                if (IsTableExist(txtTableName.Text) && (txtTableName.Text != tableSet[tableIndex].DisplayName))
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

        private void lstvTable_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvTable.Columns[e.Column].Tag == null)
                this.lstvTable.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvTable.Columns[e.Column].Tag;
            if (tabK)
                this.lstvTable.Columns[e.Column].Tag = false;
            else
                this.lstvTable.Columns[e.Column].Tag = true;
            this.lstvTable.ListViewItemSorter = new ListViewSort(e.Column, this.lstvTable.Columns[e.Column].Tag);
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtTableName.Text == "")
            {
                MessageBox.Show( "Please add a table name!", "Table Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (exist)
            {
                MessageBox.Show( "Table name is existing!", "Channel Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (type == "Add")
            {
                AddSave();
                parentForm.ShowLUTableSetInformation();
            }
            else if (type == "Edit")
            {
                ModifySave();
                parentForm.ShowLUTableSetInformation();
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion       
    }
}