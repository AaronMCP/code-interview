using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Dicom;
using HYS.Common.Dicom.Net;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Translation;

namespace HYS.DicomAdapter.Common
{
    public class FieldControler
    {
        private ComboBox _comboTable;
        private ComboBox _comboField;
        private CheckBox _checkFixValue;
        private CheckBox _checkLookupTable;
        private ComboBox _comboLookupTable;
        private TextBox _textFixValue;
        private GroupBox _groupGWData;

        private bool _isInbound;
        private bool _asQueryResult;
        private bool _lutInitialized;
        private DataBase _dbConnection;

        public FieldControler(bool asQueryResult, GroupBox gwData, ComboBox table, ComboBox field, CheckBox cFixValue, TextBox tFixValue, CheckBox cLookupTable, ComboBox lut, bool isInbound)
        {
            _comboTable = table;
            _comboField = field;
            _groupGWData = gwData;
            _checkFixValue = cFixValue;
            _checkLookupTable = cLookupTable;
            _textFixValue = tFixValue;
            _comboLookupTable = lut;
            _asQueryResult = asQueryResult;
            _isInbound = isInbound;
            _comboField.SelectedIndexChanged += new EventHandler(_comboField_SelectedIndexChanged);
            _comboTable.SelectedIndexChanged += new EventHandler(_comboTable_SelectedIndexChanged);
            _checkLookupTable.CheckedChanged += new EventHandler(_checkLookupTable_CheckedChanged);
            _checkFixValue.CheckedChanged += new EventHandler(_checkFixValue_CheckedChanged);
        }

        public void Initialize(string dataDBConnectionString, Logging log)
        {
            _dbConnection = new DataBase(dataDBConnectionString);
            LoggingHelper.EnableDatabaseLogging(_dbConnection, log);

            string[] tableList = GWDataDB.GetTableNames();
            foreach (string table in tableList)
            {
                _comboTable.Items.Add(table);
            }
            if (_comboTable.Items.Count > 0)
                _comboTable.SelectedIndex = 0;
        }
        public void LoadSetting(MappingItem item)
        {
            if (item == null) return;

            _textFixValue.Text = item.Translating.ConstValue;
            SelectItem(_comboLookupTable, item.Translating.LutName);
            SelectItem(_comboTable, item.GWDataDBField.Table.ToString());
            SelectItem(_comboField, item.GWDataDBField.FieldName);

            switch (item.Translating.Type)
            {
                case TranslatingType.FixValue:
                    {
                        _checkFixValue.Checked = true;
                        break;
                    }
                case TranslatingType.LookUpTable:
                    {
                        _checkFixValue.Checked = true;
                        break;
                    }
            }
        }
        public void SaveSetting(MappingItem item)
        {
            if (item == null) return;

            string lut = GetLUT();
            string field = GetField();
            GWDataDBTable table = GetTable();

            item.GWDataDBField.Table = table;
            item.GWDataDBField.FieldName = field;
            item.Translating.Type = TranslatingType.None;

            if (_checkFixValue.Checked)
            {
                item.Translating.Type = TranslatingType.FixValue;
                item.Translating.ConstValue = _textFixValue.Text;
            }

            if (_checkLookupTable.Checked)
            {
                item.Translating.Type = TranslatingType.LookUpTable;
                item.Translating.LutName = lut;
            }
        }
        public bool ValidateValue()
        {
            bool fix = GetFix();
            string lut = GetLUT();
            string field = GetField();
            GWDataDBTable table = GetTable();

            if (_asQueryResult)
            {
                if (fix)
                {
                    return true;
                }
                else
                {
                    return (lut != "") && (table != GWDataDBTable.None) && (field != "");
                }
            }
            else
            {
                if (fix)
                {
                    return (table != GWDataDBTable.None) && (field != "");
                }
                else
                {
                    if (_asQueryResult)
                    {
                        return (lut != "") && (table != GWDataDBTable.None) && (field != "");
                    }
                    else
                    {
                        if (table != GWDataDBTable.None && field.Length < 1) return false;
                        return true;
                    }
                }
            }
        }
        public bool Enabled
        {
            get { return _groupGWData.Enabled; }
            set { _groupGWData.Enabled = value; }
        }
        public void Clear()
        {
            this._checkFixValue.Checked = this._checkLookupTable.Checked = false;
            string str = GWDataDBTable.None.ToString();
            foreach (object o in _comboTable.Items)
            {
                if (str == o.ToString())
                {
                    _comboTable.SelectedItem = o;
                    break;
                }
            }
        }

        public event EventHandler OnValueChanged;

        private void InitializeLUT()
        {
            if (_lutInitialized) return;
            _comboLookupTable.Items.Clear();

            _groupGWData.FindForm().Cursor = Cursors.WaitCursor;
            LutMgt mgt = new LutMgt(_dbConnection);
            string[] lutList = mgt.GetLutNames();
            _groupGWData.FindForm().Cursor = Cursors.Default;

            if (lutList == null) return;
            _lutInitialized = true;
            foreach (string lut in lutList)
            {
                _comboLookupTable.Items.Add(lut);
            }
        }
        private void RefreshFieldList(GWDataDBTable table)
        {
            _comboField.Items.Clear();
            _comboField.Enabled = (table != GWDataDBTable.None);

            GWDataDBField[] fieldList;
            if (_isInbound) fieldList = GWDataDBField.GetFields(table, DirectionType.INBOUND);
            else fieldList = GWDataDBField.GetFields(table, DirectionType.OUTBOUND);
            if (fieldList == null) return;

            foreach (GWDataDBField field in fieldList)
            {
                if (_isInbound &&
                    field.Table == GWDataDBField.i_EventType.Table &&
                    field.FieldName == GWDataDBField.i_EventType.FieldName) continue;

                _comboField.Items.Add(field.FieldName);
            }
        }
        private void SelectItem(ComboBox cb, string str)
        {
            foreach (object o in cb.Items)
            {
                string s = o as string;
                if (s == str)
                {
                    cb.SelectedItem = o;
                    break;
                }
            }
        }
        private GWDataDBTable GetTable()
        {
            string str = _comboTable.SelectedItem as string;
            return GWDataDB.GetTable(str);
        }
        private string GetField()
        {
            string str = _comboField.SelectedItem as string;
            if (str == null) return "";
            return str;
        }
        private string GetLUT()
        {
            if (!_checkLookupTable.Checked) return "[NotApply]";
            string str = _comboLookupTable.SelectedItem as string;
            if (str == null) return "";
            return str;
        }
        private bool GetFix()
        {
            return _checkFixValue.Checked;
        }

        private void _checkFixValue_CheckedChanged(object sender, EventArgs e)
        {
            _textFixValue.Enabled = _checkFixValue.Checked;
            if (_checkFixValue.Checked) _checkLookupTable.Checked = false;
            if (OnValueChanged != null) OnValueChanged(sender, e);

            if (_asQueryResult)
            {
                _comboField.Enabled = _comboTable.Enabled = !_checkFixValue.Checked;
            }
        }
        private void _checkLookupTable_CheckedChanged(object sender, EventArgs e)
        {
            _comboLookupTable.Enabled = _checkLookupTable.Checked;
            if (_checkLookupTable.Checked) _checkFixValue.Checked = false;
            if (_comboLookupTable.Enabled) InitializeLUT();
            if (OnValueChanged != null) OnValueChanged(sender, e);
        }
        private void _comboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFieldList(GetTable());
            if (OnValueChanged != null) OnValueChanged(sender, e);
        }
        private void _comboField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnValueChanged != null) OnValueChanged(sender, e);
        }
    }
}
