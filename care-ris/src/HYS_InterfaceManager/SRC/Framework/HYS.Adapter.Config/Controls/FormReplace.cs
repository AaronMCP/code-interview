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
    public partial class FormReplace : Form
    {
        public FormReplace(ReplacementRuleItem item)
        {
            InitializeComponent();

            _fieldControler = new FieldControler(this.comboBoxTable, this.comboBoxField);
            _fieldControler.ValueChanged += new EventHandler(_fieldControler_ValueChanged);

            _ruleItem = item;
            if (_ruleItem == null)
            {
                this.Text = "Add Replacement Rule";
                _ruleItem = new ReplacementRuleItem();
            }
            else
            {
                this.Text = "Edit Replacement Rule";
                _isEdit = true;
            }

            LoadSetting();   
        }

        private ReplacementRuleItem _ruleItem;
        public ReplacementRuleItem RuleItem
        {
            get { return _ruleItem; }
        }

        private bool _isEdit;
        private FieldControler _fieldControler;

        private void Import()
        {
            FormRegExpression frm = new FormRegExpression();
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            RegularExpressionItem item = frm.RegExpression;
            if (item == null) return;

            this.textBoxDescription.Text = item.Description;
            this.textBoxReplacement.Text = item.Replacement;
            this.textBoxRegExpression.Text = item.Expression;
        }
        private void LoadSetting()
        {
            _fieldControler.LoadField(_ruleItem);
            this.textBoxRegExpression.Text = _ruleItem.RegularExpression.Expression;
            this.textBoxReplacement.Text = _ruleItem.RegularExpression.Replacement;
            this.textBoxDescription.Text = _ruleItem.RegularExpression.Description;
        }
        private void SaveSetting()
        {
            _fieldControler.SaveField(_ruleItem);
            _ruleItem.RegularExpression.Expression = this.textBoxRegExpression.Text;
            _ruleItem.RegularExpression.Replacement = this.textBoxReplacement.Text;
            _ruleItem.RegularExpression.Description = this.textBoxDescription.Text;
        }
        private void RefreshButtons()
        {
            this.buttonOK.Enabled = _fieldControler.IsValid();

            GWDataDBField f = _fieldControler.GetField();
            if (f != null)
            {
                foreach (ReplacementRuleItem i in Program.ServiceMgt.Config.Replacement.Fields)
                {
                    if (_isEdit &&
                        i.Table == _ruleItem.Table &&
                        i.FieldName == _ruleItem.FieldName)
                        continue;

                    if (f.Table == i.Table &&
                        f.FieldName == i.FieldName)
                    {
                        MessageBox.Show(this, "Field " + i.ToString() + " is already in the list.", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.buttonOK.Enabled = false;
                        break;
                    }
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
        private void buttonImport_Click(object sender, EventArgs e)
        {
            Import();
        }
        private void _fieldControler_ValueChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }
    }
}