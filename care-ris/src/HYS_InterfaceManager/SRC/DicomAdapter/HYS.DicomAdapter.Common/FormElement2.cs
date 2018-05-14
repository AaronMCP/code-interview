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
using HYS.Adapter.Base;

namespace HYS.DicomAdapter.Common
{
    public partial class FormElement2<T> : Form
        where T: MappingItem, IDicomMappingItem, new()
    {
        private T _mappingItem;
        public T MappingItem
        {
            get { return _mappingItem; }
        }

        public TagControler2 _tagControler;
        public FieldControler2 _fieldControler;

        private Logging _log;
        private string _gwDataDBConnection;

        private T _baseItem;
        private bool _addChild;
        private bool _isInbound;
        private bool _isQueryResult;
        private XCollection<T> _itemList;
        public FormElement2(T item, XCollection<T> itemList, T baseItem, bool addChild, string gwDataDBConnection, Logging log, bool isQueryResult, bool isInbound)
        {
            InitializeComponent();

            _mappingItem = item;
            _itemList = itemList;
            _baseItem = baseItem;
            _addChild = addChild;

            _isInbound = isInbound;
            _isQueryResult = isQueryResult;

            _log = log;
            _gwDataDBConnection = gwDataDBConnection;

            if (_mappingItem == null)
            {
                _mappingItem = new T();
                this.Text = "Add Element Mapping";
            }
            else
            {
                this.Text = "Edit Element Mapping";
            }

            _tagControler = new TagControler2(this.groupBoxDicom,
                this.comboBoxVR, this.comboBoxTag, this.textBoxGroupNum, this.textBoxElementNum);
            _tagControler.OnValueChanged += new EventHandler(_tagControler_OnValueChanged);

            //_fieldControler = new FieldControler2(false, this.groupBoxGateway,
            //    this.comboBoxTable, this.comboBoxField, this.checkBoxFixValue,
            //    this.textBoxFixValue, this.checkBoxLUT, this.comboBoxLUT, true);
            _fieldControler = new FieldControler2(isQueryResult, this.groupBoxGateway,
                this.comboBoxTable, this.comboBoxField, this.checkBoxFixValue,
                this.textBoxFixValue, this.checkBoxLUT, this.comboBoxLUT, isInbound);
            _fieldControler.OnValueChanged += new EventHandler(_fieldControler_OnValueChanged);
        }

        private void InitializeData()
        {
            _tagControler.Initialize();
            _fieldControler.Initialize(_gwDataDBConnection, _log);
        }
        private void RefreshButton()
        {
            if (_tagControler.GetVR() == DVR.SQ)
            {
                _fieldControler.Clear();
                _fieldControler.Enabled = false;
                this.checkBoxRedundancy.Checked = false;
                this.checkBoxRedundancy.Enabled = false;
                this.buttonOK.Enabled = _tagControler.ValidateValue();
            }
            else
            {
                _fieldControler.Enabled = true;
                this.checkBoxRedundancy.Enabled = true;
                this.buttonOK.Enabled = _fieldControler.ValidateValue() && _tagControler.ValidateValue();
            }
        }
        private void LoadSetting()
        {
            _fieldControler.LoadSetting(_mappingItem);
            _tagControler.LoadSetting(_mappingItem.DPath);
            if (_isInbound) this.checkBoxRedundancy.Checked = _mappingItem.RedundancyFlag;

            if (_baseItem != null)
            {
                //this.textBoxListGroup.Text = _mappingItem.DPath.Catagory;
                this.textBoxListGroup.Text = _baseItem.DPath.Catagory;
                this.textBoxListGroup.ReadOnly = true;
            }
            else
            {
                this.textBoxListGroup.Text = "Other";
            }
        }
        private bool SaveSetting()
        {
            T testItem = _mappingItem.Clone() as T;
            _fieldControler.SaveSetting(testItem);
            _tagControler.SaveSetting(testItem.DPath, null);

            // get parallel item list
            int sqIndex = -1;
            XCollection<T> iList;
            int baseIndex = _baseItem == null ? -1 : _itemList.IndexOf(_baseItem);
            if (_addChild)
            {
                //when adding child _baseItem is the parent SQ item
                iList = DicomMappingHelper.GetSequence<T>(baseIndex, _itemList);
            }
            else
            {
                int tbaseIndex = baseIndex;
                sqIndex = DicomMappingHelper.FindParentSQItemIndex<T>(ref tbaseIndex, _itemList);
                iList = DicomMappingHelper.GetSequence<T>(sqIndex, _itemList);
            }
            if (iList == null) iList = _itemList;

            // avoid duplicated DICOM tag
            foreach (T item in iList)
            {
                if (item == _mappingItem ) continue;
                if (item.DPath.GetTag() == testItem.DPath.GetTag())
                {
                    MessageBox.Show(this, "Element (" + item.DPath.GetTagName() +
                       ") has already been in the mapping list");
                    this.comboBoxTag.Focus();
                    return false;
                }
            }

            // avoid duplicated CS Broker field in inbound interface or in query criteria list of outbound interface
            if (_isInbound || !_isQueryResult)
            {
                foreach (T item in _itemList)
                {
                    if (item == _mappingItem) continue;
                    if (item.GWDataDBField.Table == GWDataDBTable.None) continue;
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
            }

            // delete items in SQ sequence when VR type change from non-SQ to SQ
            if (_mappingItem.DPath.VR == DVR.SQ && testItem.DPath.VR != DVR.SQ)
            {
                int index = _itemList.IndexOf(_mappingItem);
                if (DicomMappingHelper.HasSequence<T>(index, _itemList))
                {
                    if (MessageBox.Show(this, "Changing VR type from \"SQ\" to \"" + testItem.DPath.VR.ToString()
                        + "\" will automatically delete all elements in the SQ sequence.\r\nAre you sure to continue?", "Warning",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DicomMappingHelper.DeleteSequence<T>(index, _itemList);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            // save field and tag information
            _fieldControler.SaveSetting(_mappingItem);
            if (_addChild && _baseItem != null)
            {
                _tagControler.SaveSetting(_mappingItem.DPath, _baseItem.DPath);
            }
            else
            {
                if (sqIndex < 0)
                    _tagControler.SaveSetting(_mappingItem.DPath, null);
                else
                    _tagControler.SaveSetting(_mappingItem.DPath, _itemList[sqIndex].DPath);
            }

            // save other information
            _mappingItem.Refresh();
            if (_isInbound) _mappingItem.RedundancyFlag = this.checkBoxRedundancy.Checked;
            if (_baseItem == null) _mappingItem.DPath.Catagory = this.textBoxListGroup.Text.Trim();

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
        private void FormQCElement_Load(object sender, EventArgs e)
        {
            InitializeData();
            LoadSetting();
        }
        private void _tagControler_OnValueChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }
        private void _fieldControler_OnValueChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }
    }
}