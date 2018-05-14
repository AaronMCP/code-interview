using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.XmlAdapter.Common.Objects;

namespace HYS.XmlAdapter.Common.Forms
{
    public partial class FormElement : Form
    {
        public FormElement(XmlElement parent)
        {
            _ele = parent.Clone();
            _ele.ClassTypeName = "";
            InitializeComponent();
            InitializeList();
        }

        private XmlElement _ele;
        public XmlElement Element
        {
            get { return _ele; }
        }

        private void SaveSetting()
        {
            _ele.XPath += "/" + this.textBoxName.Text.Trim();
            _ele.Type = (XIMType)this.comboBoxType.SelectedItem;
        }

        private void InitializeList()
        {
            Array tlist = Enum.GetValues(typeof(XIMType));
            this.comboBoxType.Items.Clear();
            this.comboBoxType.Items.Add(XIMType.complex_type);
            foreach (XIMType t in tlist)
            {
                if (t.ToString().Length > 3) continue;
                this.comboBoxType.Items.Add(t);
            }
            this.comboBoxType.SelectedItem = XIMType.STR;
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

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = this.textBoxName.Text.Trim().Length > 0;
        }
    }
}