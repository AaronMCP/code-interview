using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;

namespace HYS.XmlAdapter.Common.Forms
{
    public partial class FormQCItem : Form
    {
        private XCollection<QueryCriteriaItem> _qcList;

        private QueryCriteriaItem _qcitem;
        public QueryCriteriaItem QCItem
        {
            get { return _qcitem; }
            set { _qcitem = value; }
        }

        private MyFieldControler _fieldControler;
        private class MyFieldControler : FieldControler
        {
            public MyFieldControler(ComboBox cbTable, ComboBox cbField)
                : base(cbTable, cbField, DirectionType.OUTBOUND)
            {
            }

            protected override GWDataDBField[] GetFieldList(GWDataDBTable table, DirectionType direction)
            {
                GWDataDBField[] list = base.GetFieldList(table, direction);
                if (list == null) return null;

                List<GWDataDBField> outlist = new List<GWDataDBField>();
                foreach (GWDataDBField f in list)
                {
                    if (f.Table == GWDataDBField.i_EventType.Table &&
                        f.FieldName == GWDataDBField.i_EventType.FieldName) continue;
                    if (f.Table == GWDataDBField.i_PROCESS_FLAG.Table &&
                        f.FieldName == GWDataDBField.i_PROCESS_FLAG.FieldName) continue;
                    outlist.Add(f);
                }
                return outlist.ToArray();
            }
        }

        //private bool _isEdit;
        public FormQCItem(QueryCriteriaItem item, XCollection<QueryCriteriaItem> list)
        {
            InitializeComponent();
            InitializeCombox();
            
            _qcitem = item;
            _qcList = list;

            if (_qcitem == null)
            {
                _qcitem = new QueryCriteriaItem();
                _qcitem.Translating.Type = TranslatingType.FixValue;
                _qcitem.Operator = QueryCriteriaOperator.Equal;
                _qcitem.Type = QueryCriteriaType.And;
                this.Text = "Add Query Criteria Item";
            }
            else
            {
                //_isEdit = true;
                this.Text = "Edit Query Criteria Item";
            }

            _fieldControler = new MyFieldControler(this.comboBoxTable, this.comboBoxField);
            _fieldControler.ValueChanged += new EventHandler(_fieldControler_ValueChanged);
        }

        private void LoadSetting()
        {
            _fieldControler.LoadField(_qcitem.GWDataDBField);
            this.textBoxFixValue.Text = _qcitem.Translating.ConstValue;
            this.comboBoxOperator.SelectedIndex = (int)_qcitem.Operator;
            this.comboBoxType.SelectedIndex = (int)_qcitem.Type - 1;
        }
        private void SaveSetting()
        {
            _fieldControler.SaveField(_qcitem.GWDataDBField);
            _qcitem.Translating.ConstValue = this.textBoxFixValue.Text;
            _qcitem.Operator = (QueryCriteriaOperator)this.comboBoxOperator.SelectedIndex;
            _qcitem.Type = (QueryCriteriaType)(this.comboBoxType.SelectedIndex + 1);
        }
        private void RefreshButtons()
        {
            this.buttonOK.Enabled = _fieldControler.IsValid();

            //GWDataDBField f = _fieldControler.GetField();
            //if (f != null)
            //{
            //    foreach (QueryCriteriaItem i in _qcList)
            //    {
            //        if (_isEdit &&
            //            i.GWDataDBField.Table == _qcitem.GWDataDBField.Table &&
            //            i.GWDataDBField.FieldName == _qcitem.GWDataDBField.FieldName)
            //            continue;

            //        if (f.Table == i.GWDataDBField.Table &&
            //            f.FieldName == i.GWDataDBField.FieldName)
            //        {
            //            MessageBox.Show(this, "Field " + i.GWDataDBField.GetFullFieldName() + " is already in the list.", "Warning",
            //                MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            this.buttonOK.Enabled = false;
            //            break;
            //        }
            //    }
            //}
        }
        private void InitializeCombox()
        {
            string[] strList;

            this.comboBoxOperator.Items.Clear();
            strList = Enum.GetNames(typeof(QueryCriteriaOperator));
            foreach (string str in strList) this.comboBoxOperator.Items.Add(str);
            if (this.comboBoxOperator.Items.Count > 0) this.comboBoxOperator.SelectedIndex = 0;

            this.comboBoxType.Items.Clear();
            strList = Enum.GetNames(typeof(QueryCriteriaType));
            for (int i = 1; i < strList.Length; i++) this.comboBoxType.Items.Add(strList[i]);
            if (this.comboBoxType.Items.Count > 0) this.comboBoxType.SelectedIndex = 0;
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
            RefreshButtons();
        }
        private void FormQCItem_Load(object sender, EventArgs e)
        {
            LoadSetting();
            RefreshButtons();
        }
    }
}