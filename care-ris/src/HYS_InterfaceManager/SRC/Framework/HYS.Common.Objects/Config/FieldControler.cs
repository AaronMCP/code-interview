using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.Config
{
    public class FieldControler
    {
        private ComboBox comboBoxTable;
        private ComboBox comboBoxField;
        private DirectionType directionType;

        public FieldControler(ComboBox cbTable, ComboBox cbField, DirectionType direction)
        {
            this.comboBoxTable = cbTable;
            this.comboBoxField = cbField;
            this.directionType = direction;

            Initialize();

            this.comboBoxTable.SelectedIndexChanged += new EventHandler(comboBoxTable_SelectedIndexChanged);
            this.comboBoxField.SelectedIndexChanged += new EventHandler(comboBoxField_SelectedIndexChanged);
        }
        public FieldControler(ComboBox cbTable, ComboBox cbField)
            : this(cbTable, cbField, DirectionType.UNKNOWN)
        {
        }

        private void Initialize()
        {
            this.comboBoxTable.Items.Clear();
            string[] tableList = GetTableList();
            foreach (string table in tableList)
            {
                this.comboBoxTable.Items.Add(table);
            }
            if (this.comboBoxTable.Items.Count > 0)
                this.comboBoxTable.SelectedIndex = 0;
        }
        private void NotifyValueChanged()
        {
            if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
        }
        private void RefreshFieldList(GWDataDBTable table)
        {
            this.comboBoxField.Items.Clear();
            this.comboBoxField.Enabled = (table != GWDataDBTable.None);

            GWDataDBField[] fieldList = GetFieldList(table, directionType);
            if (fieldList != null)
            {
                this.comboBoxField.Tag = fieldList;
                foreach (GWDataDBField field in fieldList)
                {
                    this.comboBoxField.Items.Add(field.FieldName);
                }
            }

            NotifyValueChanged();
        }

        protected virtual string[] GetTableList()
        {
            return GWDataDB.GetTableNames();
        }
        protected virtual GWDataDBField[] GetFieldList(GWDataDBTable table, DirectionType direction)
        {
            return GWDataDBField.GetFields(table, directionType);
        }

        public bool IsValid()
        {
            GWDataDBTable t = GetTable();
            GWDataDBField f = GetField();
            return (t != GWDataDBTable.None && f != null);
        }
        public GWDataDBTable GetTable()
        {
            string str = this.comboBoxTable.SelectedItem as string;
            return GWDataDB.GetTable(str);
        }
        public GWDataDBField GetField()
        {
            GWDataDBField[] fieldList = this.comboBoxField.Tag as GWDataDBField[];
            int index = this.comboBoxField.SelectedIndex;
            if (index < 0 || index >= fieldList.Length) return null;
            return fieldList[index].Clone();
        }
        public event EventHandler ValueChanged;
        public void LoadField(GWDataDBField field)
        {
            if (field == null) return;

            string table = field.Table.ToString();
            foreach (object t in this.comboBoxTable.Items)
            {
                if (t.ToString() == table)
                {
                    this.comboBoxTable.SelectedItem = t;
                    break;
                }
            }

            if (field.Table != GWDataDBTable.None)
            {
                string name = field.FieldName;
                foreach (object f in this.comboBoxField.Items)
                {
                    if (f.ToString() == name)
                    {
                        this.comboBoxField.SelectedItem = f;
                        break;
                    }
                }
            }
        }
        public void SaveField(GWDataDBField field)
        {
            if (field == null) return;
            GWDataDBField f = GetField();
            field.FieldName = f.FieldName;
            field.Table = f.Table;
        }

        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFieldList(GetTable());
        }
        private void comboBoxField_SelectedIndexChanged(object sender, EventArgs e)
        {
            NotifyValueChanged();
        }
    }
}
