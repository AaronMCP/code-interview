using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Translation;

namespace HYS.Adapter.Config.Controls
{
    public partial class FormFieldKanJi : Form
    {

        public FormFieldKanJi(Level3KanJiReplacementRuleItem field)
        {
            InitializeComponent();

            _fieldControler = new FieldControler(this.comboBoxTable, this.comboBoxField, DirectionType.INBOUND);
            _fieldControler.ValueChanged += new EventHandler(_fieldControler_ValueChanged);

            _field = field;
            if (_field == null)
            {
                _field = new Level3KanJiReplacementRuleItem();
                this.Text = "Add Convertion Rule";
            }
            else
            {
                this.Text = "Edit Convertion Rule";
                _isEdit = true;
            }

            LoadSetting();
        }

        private Level3KanJiReplacementRuleItem _field;
        public Level3KanJiReplacementRuleItem Field
        {
            get { return _field; }
        }

      
        private void LoadSetting()
        {
            _fieldControler.LoadField(_field);
            this.txtboxReplacementChar.Text= _field.ReplacementChar;
            
        }
        private void SaveSetting()
        {
            _fieldControler.SaveField(_field);
            _field.ReplacementChar = this.txtboxReplacementChar.Text;
            
        }
        private void RefreshButton()
        {
            this.buttonOK.Enabled = _fieldControler.IsValid();

            GWDataDBField f = _fieldControler.GetField();
            if (f != null)
            {
                foreach (Chinese2PinyinRuleItem i in Program.ServiceMgt.Config.Chinese2Pinyin.Fields)
                {
                    if (_isEdit &&
                        i.Table == _field.Table &&
                        i.FieldName == _field.FieldName)
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


        private bool _isEdit;
        private FieldControler _fieldControler;

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

        private void _fieldControler_ValueChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }
      

     
    }
}
