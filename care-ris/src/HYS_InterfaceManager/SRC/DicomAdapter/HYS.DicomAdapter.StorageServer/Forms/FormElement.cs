using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Common.Dicom;
using HYS.Common.Dicom.Net;
using HYS.Common.Objects.Rule;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.StorageServer.Dicom;
using HYS.DicomAdapter.StorageServer.Objects;

namespace HYS.DicomAdapter.StorageServer.Forms
{
    public partial class FormElement : Form
    {
        private const int Width_1 = 342;
        private const int Width_2 = 673;

        private StorageItem _storageItem;
        public StorageItem StorageItem
        {
            get { return _storageItem; }
        }

        private TagControler _tagControler;
        private FieldControler _fieldControler;

        private XCollection<StorageItem> _itemList;
        public FormElement(StorageItem item, XCollection<StorageItem> itemList)
        {
            InitializeComponent();

            _storageItem = item;
            _itemList = itemList;

            _tagControler = new TagControler(false, this.groupBoxDicom,
                this.comboBoxVR, this.comboBoxTag, this.textBoxGroupNum, this.textBoxElementNum);
            _tagControler.OnValueChanged += new EventHandler(_tagControler_OnValueChanged);

            _fieldControler = new FieldControler(false, this.groupBoxGateway,
                this.comboBoxTable, this.comboBoxField, this.checkBoxFixValue,
                this.textBoxFixValue, this.checkBoxLUT, this.comboBoxLUT, true);
            _fieldControler.OnValueChanged += new EventHandler(_fieldControler_OnValueChanged);

            _tagControler.Enabled = false;
        }

        private void InitializeData()
        {
            _tagControler.Initialize();
            _fieldControler.Initialize(Program.ConfigMgt.Config.GWDataDBConnection, Program.Log);
        }
        private void RefreshButton()
        {
            this.buttonOK.Enabled = _fieldControler.ValidateValue() && _tagControler.ValidateValue();
        }
        private void LoadSetting()
        {
            _fieldControler.LoadSetting(_storageItem);
            _tagControler.LoadSetting(_storageItem.DPath);
            this.checkBoxRedundancy.Checked = _storageItem.RedundancyFlag;
        }
        private bool SaveSetting()
        {
            StorageItem testItem = _storageItem.Clone() as StorageItem;
            _fieldControler.SaveSetting(testItem);
            _tagControler.SaveSetting(testItem.DPath);

            foreach (StorageItem item in _itemList)
            {
                if (item == _storageItem ||
                    item.GWDataDBField.Table == GWDataDBTable.None) continue;
                if (item.GWDataDBField.Table == testItem.GWDataDBField.Table &&
                    item.GWDataDBField.FieldName == testItem.GWDataDBField.FieldName)
                {
                    MessageBox.Show(this, "Element (" + item.DPath.GetTagName() +
                        ") has been mapped to this GC Gateway field ("
                        + item.GWDataDBField.GetFullFieldName() + "). \r\n"
                        + "Pease change another GC Gateway field.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.comboBoxField.Focus();
                    return false;
                }
            }

            _fieldControler.SaveSetting(_storageItem);
            _tagControler.SaveSetting(_storageItem.DPath);
            _storageItem.RedundancyFlag = this.checkBoxRedundancy.Checked;
            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!SaveSetting()) return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void _tagControler_OnValueChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }
        private void _fieldControler_OnValueChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }
        private void checkBoxFixValue_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxFixValue.Checked)
            {
                labelDirection.Visible = false;
                this.Width = Width_1;
            }
            else
            {
                labelDirection.Visible = true;
                this.Width = Width_2;
            }
        }
        private void FormElement_Load(object sender, EventArgs e)
        {
            InitializeData();
            LoadSetting();
            checkBoxFixValue_CheckedChanged(sender, e);
        }
    }
}