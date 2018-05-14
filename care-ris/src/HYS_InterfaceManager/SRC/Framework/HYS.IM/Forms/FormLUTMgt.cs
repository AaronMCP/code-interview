using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Translation;

namespace HYS.IM.Forms
{
    public partial class FormLUTMgt : Form
    {
        private LutMgt mgt;
        public FormLUTMgt()
        {
            InitializeComponent();

            mgt = new LutMgt(Program.DataDB);
        }

        private void RefreshTableList()
        {
            this.listViewLUTList.Items.Clear();

            string[] tableList = mgt.GetLutNames();
            if (tableList == null) return;

            foreach (string tname in tableList)
            {
                LUTItemMgt lut = new LUTItemMgt(mgt.DataBase,tname);
                ListViewAddTable(lut);
            }

            if (this.listViewLUTList.Items.Count > 0) this.listViewLUTList.Items[0].Selected = true;
        }
        private void ListViewSelectTable(LUTItemMgt lut)
        {
            if (lut == null) return;
            foreach (ListViewItem i in this.listViewLUTList.Items)
            {
                LUTItemMgt table = i.Tag as LUTItemMgt;
                if (table.TableName == lut.TableName)
                {
                    i.Selected = true;
                    i.EnsureVisible();
                    break;
                }
            }
        }
        private void ListViewAddTable(LUTItemMgt lut)
        {
            if (lut == null) return;
            int index = this.listViewLUTList.Items.Count + 1;
            ListViewItem i = this.listViewLUTList.Items.Add(index.ToString());
            i.SubItems.Add(lut.TableName);
            i.Tag = lut;
        }
        private void RefreshTableButton()
        {
            this.buttonAddTable.Enabled =
                this.textBoxTableName.Text.Trim().Length > 0;
            this.buttonDeleteTable.Enabled =
                this.listViewLUTList.SelectedItems.Count > 0;
            RefreshItemButton();
        }
        private void DeleteTable()
        {
            if (this.listViewLUTList.SelectedItems.Count < 1) return;
            LUTItemMgt lut = this.listViewLUTList.SelectedItems[0].Tag as LUTItemMgt;
            if (lut == null) return;

            if (lut.Drop())
            {
                RefreshItemList(null);
                RefreshTableList();
                RefreshTableButton();
            }
            else
            {
                MessageBox.Show(this, "Delete table \"" + lut.TableName + "\" failed.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddTable()
        {
            string str = this.textBoxTableName.Text.Trim();
            if (str.Length < 1) return;

            if (str.Length <= LutMgt.Prefix.Length ||
                str.ToLower().Substring(0, LutMgt.Prefix.Length) != LutMgt.Prefix)
            {
                str = LutMgt.Prefix + str;
                this.textBoxTableName.Text = str;
            }

            bool hasName = false;
            foreach (ListViewItem item in this.listViewLUTItem.Items)
            {
                LUTItemMgt table = item.Tag as LUTItemMgt;
                if (table != null && table.TableName == str)
                {
                    hasName = true;
                    break;
                }
            }

            if (hasName)
            {
                MessageBox.Show(this, "Look up table named \"" + str + "\" has already existed in the database, please input another name.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LUTItemMgt lut = new LUTItemMgt(mgt.DataBase, str);
            if (lut.Create())
            {
                this.textBoxTableName.Text = "";

                ListViewAddTable(lut);
                ListViewSelectTable(lut);
                //RefreshTableButton();
            }
            else
            {
                MessageBox.Show(this, "Create table \"" + str + "\" failed.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshItemList(LUTItemMgt lut)
        {
            this.listViewLUTItem.Items.Clear();
            if (lut == null) return;

            LUTItem[] itemList = lut.LUT;
            if (itemList == null) return;

            int index = 1;
            foreach (LUTItem item in itemList)
            {
                ListViewItem i = this.listViewLUTItem.Items.Add((index++).ToString());
                i.SubItems.Add(item.SourceValue);
                i.SubItems.Add(item.TargetValue);
                i.Tag = item;
            }
        }
        private void ListViewSelectItem(LUTItem item)
        {
            if (item == null) return;
            foreach (ListViewItem i in this.listViewLUTItem.Items)
            {
                LUTItem lutItem = i.Tag as LUTItem;
                if (lutItem.SourceValue == item.SourceValue &&
                    lutItem.TargetValue == item.TargetValue)
                {
                    i.Selected = true;
                    i.EnsureVisible();
                    break;
                }
            }
        }
        private void ListViewAddItem(LUTItem item)
        {
            if (item == null) return;
            int index = this.listViewLUTItem.Items.Count + 1;
            ListViewItem i = this.listViewLUTItem.Items.Add(index.ToString());
            i.SubItems.Add(item.SourceValue);
            i.SubItems.Add(item.TargetValue);
            i.Tag = item;
        }
        private bool CheckRepeat(LUTItem newItem)
        {
            if (newItem == null) return false;
            foreach (ListViewItem i in this.listViewLUTItem.Items)
            {
                LUTItem lutItem = i.Tag as LUTItem;
                if (lutItem.SourceValue == newItem.SourceValue &&
                    lutItem.TargetValue == newItem.TargetValue )
                {
                    MessageBox.Show(this, "Item (Source=\"" + lutItem.SourceValue + "\",Target=\"" + lutItem.TargetValue + "\") has already existed in the look up table",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        private void RefreshItemButton()
        {
            if (this.listViewLUTList.SelectedItems.Count < 1)
            {
                this.buttonAddItem.Enabled =
                       this.buttonDeleteItem.Enabled =
                       this.buttonModifyItem.Enabled =
                       this.textBoxSourceValue.Enabled =
                       this.textBoxTargetValue.Enabled = false;

                this.textBoxSourceValue.Text = "";
                this.textBoxTargetValue.Text = "";
                return;
            }
            else
            {
                this.textBoxSourceValue.Enabled =
                       this.textBoxTargetValue.Enabled = true;
            }

            this.buttonDeleteItem.Enabled =
                this.listViewLUTItem.SelectedItems.Count > 0;
            this.buttonAddItem.Enabled =
                this.textBoxSourceValue.Text.Length > 0 || this.textBoxTargetValue.Text.Length > 0;
            this.buttonModifyItem.Enabled =
                (this.textBoxSourceValue.Text.Length > 0 || this.textBoxTargetValue.Text.Length > 0) && this.listViewLUTItem.SelectedItems.Count > 0;
        }
        private void DeleteItem()
        {
            if (this.listViewLUTList.SelectedItems.Count < 1) return;
            LUTItemMgt lut = this.listViewLUTList.SelectedItems[0].Tag as LUTItemMgt;
            if (lut == null) return;

            if (this.listViewLUTItem.SelectedItems.Count < 1) return;
            LUTItem item = this.listViewLUTItem.SelectedItems[0].Tag as LUTItem;
            if (item == null) return;

            if (lut.Delete(item))
            {
                lut.ReloadLUT();
                RefreshItemList(lut);
                RefreshItemButton();
            }
            else
            {
                MessageBox.Show(this, "Delete look up table item failed.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ModifyItem()
        {
            string strSource = this.textBoxSourceValue.Text;
            string strTarget = this.textBoxTargetValue.Text;
            if (strSource.Length < 1 && strTarget.Length < 1) return;

            if (this.listViewLUTList.SelectedItems.Count < 1) return;
            LUTItemMgt lut = this.listViewLUTList.SelectedItems[0].Tag as LUTItemMgt;
            if (lut == null) return;

            if (this.listViewLUTItem.SelectedItems.Count < 1) return;
            LUTItem item = this.listViewLUTItem.SelectedItems[0].Tag as LUTItem;
            if (item == null) return;

            LUTItem newItem = new LUTItem();
            newItem.SourceValue = strSource;
            newItem.TargetValue = strTarget;

            if (!CheckRepeat(newItem)) return;

            item.SourceValue = strSource;
            item.TargetValue = strTarget;

            if (lut.Update(item))
            {
                lut.ReloadLUT();
                RefreshItemList(lut);
                ListViewSelectItem(item);
            }
            else
            {
                MessageBox.Show(this, "Modify look up table item failed.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddItem()
        {
            string strSource = this.textBoxSourceValue.Text;
            string strTarget = this.textBoxTargetValue.Text;
            if (strSource.Length < 1 && strTarget.Length < 1) return;

            if (this.listViewLUTList.SelectedItems.Count < 1) return;
            LUTItemMgt lut = this.listViewLUTList.SelectedItems[0].Tag as LUTItemMgt;
            if (lut == null) return;

            LUTItem item = new LUTItem();
            item.SourceValue = strSource;
            item.TargetValue = strTarget;

            if (!CheckRepeat(item)) return;

            if (lut.Insert(item))
            {
                lut.ReloadLUT();
                RefreshItemList(lut);
                ListViewSelectItem(item);
            }
            else
            {
                MessageBox.Show(this, "Add look up table item failed.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormLUTMgt_Load(object sender, EventArgs e)
        {
            RefreshTableList();
            RefreshTableButton();
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void listViewLUTList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTableButton();

            if (this.listViewLUTList.SelectedItems.Count > 0)
            {
                RefreshItemList(this.listViewLUTList.SelectedItems[0].Tag as LUTItemMgt);
            }
            else
            {
                RefreshItemList(null);
            }
        }
        private void listViewLUTItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshItemButton();

            if (this.listViewLUTItem.SelectedItems.Count > 0)
            {
                LUTItem item = this.listViewLUTItem.SelectedItems[0].Tag as LUTItem;
                if (item == null) return;

                this.textBoxSourceValue.Text = item.SourceValue;
                this.textBoxTargetValue.Text = item.TargetValue;
                this.buttonModifyItem.Enabled = false;
                this.buttonAddItem.Enabled = false;
            }
            else
            {
                this.textBoxSourceValue.Text = "";
                this.textBoxTargetValue.Text = "";
                this.buttonModifyItem.Enabled = false;
                this.buttonAddItem.Enabled = false;
            }
        }
        private void textBoxSourceValue_TextChanged(object sender, EventArgs e)
        {
            RefreshItemButton();
        }
        private void textBoxTargetValue_TextChanged(object sender, EventArgs e)
        {
            RefreshItemButton();
        }
        private void textBoxTableName_TextChanged(object sender, EventArgs e)
        {
            RefreshTableButton();
        }
        private void buttonDeleteTable_Click(object sender, EventArgs e)
        {
            DeleteTable();
        }
        private void buttonModifyItem_Click(object sender, EventArgs e)
        {
            ModifyItem();
        }
        private void buttonDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }
        private void buttonAddTable_Click(object sender, EventArgs e)
        {
            AddTable();
        }
        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            AddItem();
        }
    }
}