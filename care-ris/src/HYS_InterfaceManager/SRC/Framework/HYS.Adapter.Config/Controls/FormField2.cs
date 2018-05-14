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

namespace HYS.Adapter.Config.Controls
{
    public partial class FormField2 : Form
    {
        public FormField2(GWDataDBField field)
        {
            InitializeComponent();

            _fieldControler = new FieldControler(this.comboBoxTable, this.comboBoxField, Program.DeviceMgt.DeviceDirInfor.Header.Direction);
            _fieldControler.ValueChanged += new EventHandler(_fieldControler_ValueChanged);

            _field = field;
            if (_field == null)
            {
                _field = new GWDataDBField();
                this.Text = "Add Field";
            }
            else
            {
                this.Text = "Edit Field";
            }

            LoadSetting();
        }

        private GWDataDBField _field;
        public GWDataDBField Field
        {
            get { return _field; }
        }

        private FieldControler _fieldControler;

        private void LoadSetting()
        {
            _fieldControler.LoadField(_field);
        }
        private void SaveSetting()
        {
            _fieldControler.SaveField(_field);
        }
        private void RefreshButton()
        {
            this.buttonOK.Enabled = _fieldControler.IsValid();
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
        private void _fieldControler_ValueChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }
    }
}