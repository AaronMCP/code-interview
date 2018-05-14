using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Logging;

namespace HYS.IM.Forms
{
    public partial class FormRegExpMgt : Form
    {
        public FormRegExpMgt()
        {
            InitializeComponent();
        }

        private bool _isEdit;
        private bool _isLock;
        
        private void RefreshList()
        {
            this.listViewList.Items.Clear();
            foreach (RegularExpressionItem i in Program.ConfigMgt.Config.RegularExpressions)
            {
                ListViewItem item = new ListViewItem(i.Expression);
                item.SubItems.Add(i.Replacement);
                item.SubItems.Add(i.Description);
                item.Tag = i;
                this.listViewList.Items.Add(item);
            }
        }
        private void RefreshText()
        {
            _isEdit = false;

            _isLock = true;
            RegularExpressionItem item = GetSelectedItem();
            if (item == null)
            {
                this.textBoxDescription.Text = "";
                this.textBoxRegExpression.Text = "";
                this.textBoxReplacement.Text = "";
            }
            else
            {
                this.textBoxReplacement.Text = item.Replacement;
                this.textBoxRegExpression.Text = item.Expression;
                this.textBoxDescription.Text = item.Description;
            }
            _isLock = false;
        }
        private void RefreshButton()
        {
            RegularExpressionItem item = GetSelectedItem();
            this.buttonModifyItem.Enabled = item != null && _isEdit;
            this.buttonDeleteItem.Enabled = item != null;
            this.buttonAddItem.Enabled = _isEdit;
        }

        private void Add()
        {
            RegularExpressionItem item = new RegularExpressionItem();

            item.Description = this.textBoxDescription.Text;
            item.Expression = this.textBoxRegExpression.Text;
            item.Replacement = this.textBoxReplacement.Text;

            Program.ConfigMgt.Config.RegularExpressions.Add(item);
            
            _isEdit = false;

            RefreshList();
            SelectItem(item);
        }
        private void Edit()
        {
            RegularExpressionItem item = GetSelectedItem();
            if (item == null) return;

            item.Description = this.textBoxDescription.Text;
            item.Expression = this.textBoxRegExpression.Text;
            item.Replacement = this.textBoxReplacement.Text;

            RefreshList();
            RefreshText();
            RefreshButton();
        }
        private void Delete()
        {
            RegularExpressionItem item = GetSelectedItem();
            if (item == null) return;

            Program.ConfigMgt.Config.RegularExpressions.Remove(item);

            RefreshList();
            RefreshText();
            RefreshButton();
        }
        private void Test()
        {
            string exp = this.textBoxRegExpression.Text;
            string rep = this.textBoxReplacement.Text;

            FormRegExpTest frm = new FormRegExpTest(exp, rep);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            this.textBoxRegExpression.Text = frm.Expression;
            this.textBoxReplacement.Text = frm.Replacement;
        }
        
        private bool SaveSetting()
        {
            if (Program.ConfigMgt.Save())
            {
                return true;
            }
            else
            {
                MessageBox.Show(this, "Save to configuration file failed.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private RegularExpressionItem GetSelectedItem()
        {
            if (this.listViewList.SelectedItems.Count < 1) return null;
            return this.listViewList.SelectedItems[0].Tag as RegularExpressionItem;
        }
        private void SelectItem(RegularExpressionItem item)
        {
            if (item == null) return;
            foreach (ListViewItem i in this.listViewList.Items)
            {
                if (item == i.Tag as RegularExpressionItem)
                {
                    i.Selected = true;
                    break;
                }
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            Test();
        }
        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void buttonModifyItem_Click(object sender, EventArgs e)
        {
            Edit();
        }
        private void buttonDeleteItem_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void textBoxReplacement_TextChanged(object sender, EventArgs e)
        {
            if (_isLock) return;
            _isEdit = true;
            RefreshButton();
        }
        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            if (_isLock) return;
            _isEdit = true;
            RefreshButton();
        }
        private void textBoxRegExpression_TextChanged(object sender, EventArgs e)
        {
            if (_isLock) return;
            _isEdit = true;
            RefreshButton();
        }
        private void listViewList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshText();
            RefreshButton();
        }
        private void FormRegExpMgt_Load(object sender, EventArgs e)
        {
            RefreshList();
            RefreshText();
            RefreshButton();
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!SaveSetting()) return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}