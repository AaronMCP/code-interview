using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Config;

namespace HYS.Adapter.Config.Controls
{
    public partial class FormCompose : Form
    {
        public FormCompose(ComposingRuleItem item)
        {
            InitializeComponent();

            _ruleItem = item;
            if (_ruleItem == null)
            {
                this.Text = "Add Composing Rule";
                _ruleItem = new ComposingRuleItem();
            }
            else
            {
                this.Text = "Edit Composing Rule";
            }

            LoadSetting();
        }

        private ComposingRuleItem _ruleItem;
        public ComposingRuleItem RuleItem
        {
            get { return _ruleItem; }
        }

        private void LoadSetting()
        {
            this.textBoxDescription.Text = _ruleItem.Description;
            this.textBoxPattern.Text = _ruleItem.ComposePattern;
            RefreshList();
        }
        private void SaveSetting()
        {
            _ruleItem.Description = this.textBoxDescription.Text;
            _ruleItem.ComposePattern = this.textBoxPattern.Text;

            GWDataDBField selField = this.comboBoxField.SelectedItem as GWDataDBField;
            _ruleItem.FieldName = selField.FieldName;
            _ruleItem.Table = selField.Table;
        }

        private void Add()
        {
            FormField2 frm = new FormField2(null);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            GWDataDBField f = frm.Field;
            if (f == null) return;

            _ruleItem.FromFields.Add(f);
            RefreshList();
        }
        private void Edit()
        {
            GWDataDBField f = GetSelectedField();
            if (f == null) return;

            FormField2 frm = new FormField2(f);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            RefreshList();
        }
        private void Delete()
        {
            GWDataDBField f = GetSelectedField();
            if (f == null) return;

            _ruleItem.FromFields.Remove(f);
            RefreshList();
        }
        
        private void RefreshList()
        {
            int index = 0;
            GWDataDBField selField = this.comboBoxField.SelectedItem as GWDataDBField;
            if (selField == null) selField = _ruleItem;

            this.comboBoxField.Items.Clear();
            this.listViewFields.Items.Clear();
            foreach (GWDataDBField f in _ruleItem.FromFields)
            {
                ListViewItem i = new ListViewItem("{" + (index++).ToString() + "}");
                i.SubItems.Add(f.ToString());
                i.Tag = f;

                this.listViewFields.Items.Add(i);
                this.comboBoxField.Items.Add(f);
            }

            foreach (GWDataDBField f in this.comboBoxField.Items)
            {
                if (f.Table == selField.Table &&
                    f.FieldName == selField.FieldName)
                {
                    this.comboBoxField.SelectedItem = f;
                    break;
                }
            }

            RefreshButton();
        }
        private void RefreshButton()
        {
            this.buttonDeleteField.Enabled = this.buttonEditField.Enabled = GetSelectedField() != null;
            GWDataDBField selField = this.comboBoxField.SelectedItem as GWDataDBField;
            this.buttonOK.Enabled = selField != null;
        }
        private void DefaultPattern()
        {
            if (this.textBoxPattern.Text.Length > 0) return;
            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem i in this.listViewFields.Items)
            {
                sb.Append(i.Text);
            }
            this.textBoxPattern.Text = sb.ToString();
        }

        private GWDataDBField GetSelectedField()
        {
            if (this.listViewFields.SelectedItems.Count < 1) return null;
            return this.listViewFields.SelectedItems[0].Tag as GWDataDBField;
        }
        private void SelectField(GWDataDBField field)
        {
            foreach (ListViewItem i in this.listViewFields.Items)
            {
                if (field == i.Tag as GWDataDBField)
                {
                    i.Selected = true;
                    i.EnsureVisible();
                    break;
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonAddField_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void buttonEditField_Click(object sender, EventArgs e)
        {
            Edit();
        }
        private void buttonDeleteField_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void comboBoxField_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshButton();
            DefaultPattern();
        }
        private void listViewFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }
        private void listViewFields_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }
    }
}