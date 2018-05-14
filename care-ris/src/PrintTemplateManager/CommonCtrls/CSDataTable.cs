using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Hys.CommonControls
{
    /// <summary>
    /// Note:
    /// If you want to add embedded controls to your GridView, please use CSDataTable as the data source of your GridView.
    /// CSDataTable provides several functions to create new row. You can call the ralated function for different type of embedded
    /// controls.
    /// 
    /// Attention:
    /// 1: DO NOT use the default NewRow() function if you have embedded controls.
    /// 2: DO NOT call CSDataTable.Rows.Add() function, because the newly provided functions has already add the new row
    /// to the table.
    /// </summary>
    public class CSDataTable : DataTable
    {
        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new CSDataRow(builder);
        }

        protected override Type GetRowType()
        {
            return typeof(CSDataRow);
        }

        /// <summary>
        /// Add a new CSDataRow which contains embedded control type info
        /// </summary>
        /// <param name="ctlType">embedded control type</param>
        /// <returns></returns>
        public CSDataRow NewRow(EmbedControlType ctlType)
        {
            CSDataRow dr = this.NewRow() as CSDataRow;
            dr.Tag = ctlType;
            this.Rows.Add(dr);
            return dr;
        }

        /// <summary>
        /// Add a new CSDataRow which contains embedded combobox
        /// </summary>
        /// <param name="dataSource">combobox data source</param>
        /// <param name="valueMember">combobox value member</param>
        /// <param name="displayMember">combobox display member</param>
        /// <returns></returns>
        public CSDataRow NewComboBoxRow(DataTable dataSource, string valueMember, string displayMember)
        {
            CSDataRow dr = NewRow(EmbedControlType.ComboBox);
            dr.EmbeddedCtlPara.ComBoCtlDataSource = dataSource;
            dr.EmbeddedCtlPara.DisplayMember = displayMember;
            dr.EmbeddedCtlPara.ValueMember = valueMember;
            return dr;
        }

        /// <summary>
        /// Add a new CSDataRow which contains embedded combobox
        /// </summary>
        /// <param name="dataSource">combobox data source</param>
        /// <param name="valueMember">combobox value member</param>
        /// <param name="displayMember">combobox display member</param>
        /// <returns></returns>
        public CSDataRow NewComboBoxExxRow(DataTable dataSource, string valueMember, string displayMember)
        {
            CSDataRow dr = NewRow(EmbedControlType.CSComboBoxExx);
            dr.EmbeddedCtlPara.ComBoCtlDataSource = dataSource;
            dr.EmbeddedCtlPara.DisplayMember = displayMember;
            dr.EmbeddedCtlPara.ValueMember = valueMember;
            return dr;
        }

        public CSDataRow NewSpinRow()
        {
            CSDataRow dr = NewRow(EmbedControlType.Spin);          
            return dr;
        }

        public CSDataRow NewNumericRow()
        {
            CSDataRow dr = NewRow(EmbedControlType.CSNumericTextBox);
            return dr;
        }

        public CSDataRow NewIntegerRow()
        {
            CSDataRow dr = NewRow(EmbedControlType.CSIntTextBox);
            return dr;
        }


        /// <summary>
        /// Add a new CSDataRow which contains embedded checkcombobox
        /// </summary>
        /// <param name="dataSource">checkcombobox data source</param>
        /// <returns></returns>
        public CSDataRow NewCheckComboBoxRow(DataTable dataSource)
        {
            CSDataRow dr = NewRow(EmbedControlType.CheckComboBox);
            dr.EmbeddedCtlPara.ComBoCtlDataSource = dataSource;
            return dr;
        }

        /// <summary>
        /// Add a new CSDataRow which contains embedded textbox
        /// </summary>
        /// <returns></returns>
        public CSDataRow NewTextBoxRow()
        {
            return NewRow(EmbedControlType.TextBox);
        }

        /// <summary>
        /// Add a new CSDataRow which contains embedded checkbox
        /// </summary>
        /// <returns></returns>
        public CSDataRow NewCheckBoxRow()
        {
            return NewRow(EmbedControlType.CheckBox);
        }

        /// <summary>
        /// Add a new CSDataRow which contains embedded datetime picker
        /// </summary>
        /// <returns></returns>
        public CSDataRow NewDateTimePickerRow()
        {
            return NewRow(EmbedControlType.DateTimePicker);
        }

        /// <summary>
        /// Add a new CSDataRow which contains embedded color selector
        /// </summary>
        /// <returns></returns>
        public CSDataRow NewColorSelectorRow()
        {
            return NewRow(EmbedControlType.ColorSelector);
        }
    }

    public class CSDataRow : DataRow
    {
        private object _userData;

        public object UserData
        {
            get { return _userData; }
            set { _userData = value; }
        }

        private object _tag;

        /// <summary>
        /// NOTE: 
        /// DO NOT use this property to store user data. Please use "UserData" instead.
        /// This field is used only for CSPropertyGridCellElement.
        /// </summary>
        internal object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        private EmbeddedControlParameter _embeddedCtlPara;

        public EmbeddedControlParameter EmbeddedCtlPara
        {
            get { return _embeddedCtlPara; }
            set { _embeddedCtlPara = value; }
        }

        private Type _dataType;

        public Type DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        public CSDataRow(DataRowBuilder builder)
            : base(builder)
        {
            _embeddedCtlPara = new EmbeddedControlParameter();
        }

    }

    public class EmbeddedControlParameter
    {
        private DataTable _comboCtlDataSource;

        public DataTable ComBoCtlDataSource
        {
            get { return _comboCtlDataSource; }
            set { _comboCtlDataSource = value; }
        }

        private string _valueMember;

        public string ValueMember
        {
            get { return _valueMember; }
            set { _valueMember = value; }
        }
        private string _displayMember;

        public string DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value; }
        }
        private CSPropertyGridCellElement _ctlCell;

        public CSPropertyGridCellElement CtlCell
        {
            get { return _ctlCell; }
            set { _ctlCell = value; }
        }

    }
}
