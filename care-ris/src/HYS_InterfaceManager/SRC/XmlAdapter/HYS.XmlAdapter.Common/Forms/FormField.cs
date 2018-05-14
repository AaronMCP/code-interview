using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Rule;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Common.Controlers;

namespace HYS.XmlAdapter.Common.Forms
{
    public partial class FormField : Form
    {
        private Logging _log;
        private string _dbConnection;
        
        public FormField(XIMMappingItem item, List<XIMMappingItem> list, string dbConnection, Logging log, bool isInbound)
        {
            InitializeComponent();
            this.checkBoxRedundancy.Visible = isInbound;

            _log = log;
            _dbConnection = dbConnection;

            _item = item;
            _list = list;
            _controler = new FieldControler(!isInbound,
                this.groupBoxGateway,
                this.comboBoxTable,
                this.comboBoxField,
                this.checkBoxFixValue,
                this.textBoxFixValue,
                this.checkBoxLUT,
                this.comboBoxLUT,
                isInbound);

            _controler.OnValueChanged += new EventHandler(_controler_OnValueChanged);
        }

        private XIMMappingItem _item;
        private List<XIMMappingItem> _list;
        private FieldControler _controler;

        private void FormField_Load(object sender, EventArgs e)
        {
            _controler.Initialize(_dbConnection, _log);
            _controler.LoadSetting(_item);
            this.checkBoxRedundancy.Checked = _item.RedundancyFlag;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            XIMMappingItem testItem = _item.Clone() as XIMMappingItem;
            _controler.SaveSetting(testItem);

            if (_list != null)
            {
                foreach (XIMMappingItem item in _list)
                {
                    if (item == _item ||
                        item.GWDataDBField.Table == GWDataDBTable.None) continue;
                    if (item.GWDataDBField.Table == testItem.GWDataDBField.Table &&
                        item.GWDataDBField.FieldName == testItem.GWDataDBField.FieldName)
                    {
                        MessageBox.Show(this, "Element (" + item.Element.XPath +
                            ") has been mapped to this GC Gateway field ("
                            + item.GWDataDBField.GetFullFieldName() + "). \r\n\r\n"
                            + "Pease change another GC Gateway field.", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.comboBoxField.Focus();
                        return;
                    }
                }
            }

            _controler.SaveSetting(_item);
            _item.RedundancyFlag = this.checkBoxRedundancy.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void _controler_OnValueChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = _controler.ValidateValue();
        }
    }
}