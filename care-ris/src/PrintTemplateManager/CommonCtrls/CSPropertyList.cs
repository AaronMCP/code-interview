using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Layouts;
using System.Drawing.Printing;
using CarestreamCommonCtrls;

namespace Hys.CommonControls
{
    ///<summary>
    /// Example:
    /// // prepare the date table
    /// _dt = new DataTable();
    /// // default column name for Name
    /// _dt.Columns.Add("Name");
    /// // default column name for Value
    /// _dt.Columns.Add("Value");
    /// _dt.Columns.Add("City");
    /// // default column name for type
    /// // see PropertyList.PropertyType enumaration for detail
    /// _dt.Columns.Add("PropertyType");
    /// // default column name for options
    /// // for string editor, it supports the regular expression.
    /// //      e.g.    ^[a-zA-Z_-]{1,5}$
    /// // for integer editor, it supports the range.
    /// //      e.g.    [50-300]
    /// // for DropDown, DropDownList and CheckComboBox, 
    /// //      it supports the selection options seperated by '|'
    /// //      e.g.    beijing|shanghai|chengdu
    /// _dt.Columns.Add("PropertyOptions");
    /// for (int i = 0; i < 20; i++)
    /// {
    ///     DataRow dr = _dt.NewRow();
    ///     dr[0] = PropertyList.toEnumType<PropertyList.PropertyType>(i).ToString();
    ///     dr[1] = System.Convert.ToChar('A' + i);
    ///     dr[2] = System.Convert.ToString(i % 4);
    ///     dr[3] = System.Convert.ToString(i % 16);
    ///     dr[4] = "[50-300]";
    ///    _dt.Rows.Add(dr);
    /// }
    ///
    /// // group by City
    /// this.propertyList1.GroupBy = "City";
    ///
    /// // data bind
    /// this.propertyList1.DataSource = _dt.DefaultView;
    ///</summary>
    public class CSPropertyList : Control
    {
        #region private memebers

        System.Windows.Forms.SplitContainer splitContainer1;
        System.Windows.Forms.TextBox _txtDesc;
        Grid _gridview;

        bool _valueChanged = false;

        string _uniqueColumn = "Id";
        string _nameColumn = "Name";
        string _valueColumn = "Value";
        string _typeColumn = "PropertyType";
        string _optionsColumn = "PropertyOptions";
        string _defaultValueColumn = "DefaultValue";
        string _regularExpressionColumn = "RegularExpress";
        string _descriptionColumn = "Description";
        string _orderColumn = "OrderID";
        string _SiteColumn = "Site";

        string _currentEditingValue = string.Empty;

        MyDateTimeEditor _datetime = null;
        MyGridEditor<MyGridEditorElement<RadSpinEditor>, RadSpinEditor> _spinGridEditor = null;
        MyGridEditor<MyGridEditorElement<EmbeddedCheckCombobox>, EmbeddedCheckCombobox> _chkcbxGridEditor = null;
        MyGridEditor<MyGridEditorElement<EmbeddedCombobox>, EmbeddedCombobox> _cbxGridEditor = null;        
        MyGridEditor<MyGridEditorElement<RadTextBox>, RadTextBox> _pwdGridEditor = null;

        ITranslator _translator = null;

        StringDictionary _mapChangedName = null;
        public List<object> ReadOnlyRowsUniqueColumn = new List<object>();

        #endregion

        public CSPropertyList()
        {
            InitializeComponent();

            _mapChangedName = new StringDictionary();

            _datetime = new MyDateTimeEditor();

            _spinGridEditor = new MyGridEditor<MyGridEditorElement<RadSpinEditor>, RadSpinEditor>();

            _chkcbxGridEditor = new MyGridEditor<MyGridEditorElement<EmbeddedCheckCombobox>, EmbeddedCheckCombobox>();

            _cbxGridEditor = new MyGridEditor<MyGridEditorElement<EmbeddedCombobox>, EmbeddedCombobox>();

            _pwdGridEditor = new MyGridEditor<MyGridEditorElement<RadTextBox>, RadTextBox>();
            RadHostItem host = _pwdGridEditor.EditorElement as RadHostItem;
            if (host != null)
            {
                RadTextBox txt = host.HostedControl as RadTextBox;
                if (txt != null)
                {
                    txt.PasswordChar = '*';
                }
            }
        }

        public object DataSource
        {
            get
            {
                // EK_HI00112939
                // EK_HI00112840
                DataTable dt = _gridview.DataSource as DataTable;               
                if (dt != null)
                {
                    DataTable mydt = dt.Copy();
                    mydt.TableName = "MyCopy";
                    RestoreValueColumn(mydt);
                    dt.AcceptChanges();
                    return mydt;
                }


                return null;
            }

            set
            {
                ValueChanged = false;

                //_gridview.MasterGridViewTemplate.SortExpressions.Clear();
                //_gridview.MasterGridViewTemplate.SortExpressions.Add(_orderColumn, RadSortOrder.Ascending);

                int selectedIndex = _gridview.CurrentRow == null ? 0 : _gridview.Rows.IndexOf(_gridview.CurrentRow as GridViewDataRowInfo);

                if (value is DataTable)
                {
                    (value as DataTable).Columns.Add(ValueColumn + "__BACKUP", typeof(System.String));
                    BackupValueColumn(value as DataTable);
                }
                else
                {
                    return;
                }

                _gridview.DataSource = value;

                foreach (GridViewDataColumn col in _gridview.Columns)
                {
                    if (col.FieldName.ToUpper() == _valueColumn.ToUpper())
                    {
                        ;
                    }
                    else if (col.FieldName.ToUpper() == _nameColumn.ToUpper())
                    {
                        col.ReadOnly = true;
                    }
                    else
                    {
                        col.IsVisible = false;
                    }
                }

                if (_gridview.DataSource != null && _gridview.Rows.Count > selectedIndex && selectedIndex > -1)
                {
                    //ShowTextForValue();
                    _gridview.Rows[selectedIndex].IsSelected = true;
                    _gridview.Rows[selectedIndex].IsCurrent = true;
                }
                else
                {
                    this._txtDesc.Text = string.Empty;
                }
                
            }


        }

        public void BackupValueColumn(DataTable dt)
        {
            if (dt == null)
                return;

            foreach (DataRow row in dt.Rows)
            {
                if (row[TypeColumn]== null)
                    continue;
                if (row[TypeColumn].ToString() != Convert.ToInt32(PropertyType.DropDownList).ToString() &&
                    row[TypeColumn].ToString() != Convert.ToInt32(PropertyType.DropDown).ToString() &&
                    row[TypeColumn].ToString() != Convert.ToInt32(PropertyType.CheckCombo).ToString())
                    continue;

                if (row[OptionsColumn] is DataTable)
                {
                    string[] checkedItems = { };
                    char seperator = '|';
                    string txt = "";
                    if (row[ValueColumn] != null)
                        checkedItems = row[ValueColumn].ToString().Split(new char[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
                    DataTable mydt = row[OptionsColumn] as DataTable;
                    foreach (string myitem in checkedItems)
                    {
                        foreach (DataRow myrow in mydt.Rows)
                        {
                            myrow["VALUE"] = myrow["VALUE"] == null ? "" : myrow["VALUE"].ToString();
                            if (myitem == myrow["VALUE"].ToString())
                            {
                                txt += myrow["TEXT"] == null ? "" : myrow["TEXT"].ToString() + seperator;
                                break;
                            }
                        }
                    }

                    char[] charsToTrim = { seperator };
                    row[ValueColumn + "__BACKUP"] = row[ValueColumn].ToString();
                    row[ValueColumn] = txt.TrimEnd(charsToTrim);

                    foreach (DataColumn column in mydt.Columns)
                    {
                        column.ColumnName = column.ColumnName.ToUpper();
                    }

                    if (row[TypeColumn].ToString() == Convert.ToInt32(PropertyType.DropDownList).ToString() ||
                    row[TypeColumn].ToString() == Convert.ToInt32(PropertyType.DropDown).ToString())
                    {
                        DataRow newrow = mydt.NewRow();
                        newrow["Text"] = "";
                        newrow["VALUE"] = "";
                        mydt.Rows.InsertAt(newrow, 0);
                    }

                    mydt.AcceptChanges();
                }
                else if(row[TypeColumn].ToString() == Convert.ToInt32(PropertyType.DropDown).ToString())
                {
                    row[ValueColumn + "__BACKUP"] = row[ValueColumn].ToString();
                }
            }

            dt.AcceptChanges();
        }

        public void RestoreValueColumn(DataTable dt)
        {
            if (dt == null)
                return;

            foreach (DataRow row in dt.Rows)
            {
                if (row[TypeColumn] == null)
                    continue;
                if (row[TypeColumn].ToString() != Convert.ToInt32(PropertyType.DropDownList).ToString() &&
                    row[TypeColumn].ToString() != Convert.ToInt32(PropertyType.DropDown).ToString() &&
                    row[TypeColumn].ToString() != Convert.ToInt32(PropertyType.CheckCombo).ToString())
                    continue;

                if (row[OptionsColumn] is DataTable)
                {
                    row[ValueColumn] = row[ValueColumn + "__BACKUP"];
                }
            }
            dt.Columns.Remove(ValueColumn + "__BACKUP");
            dt.AcceptChanges();
        }

        //public void ShowTextForValue()
        //{
        //    foreach (GridViewRowInfo row in _gridview.Rows)
        //    {
        //        if (!(row.Cells[TypeColumn].Value is string))
        //            continue;
        //        if (getCellType(row) != PropertyType.DropDownList && getCellType(row) != PropertyType.CheckCombo)
        //            continue;

        //        if (row.Cells[OptionsColumn].Value is DataTable)
        //        {
        //            string[] checkedItems = { };
        //            char seperator = '|';
        //            string txt = "";
        //            if (row.Cells[ValueColumn].Value is string)
        //                checkedItems = row.Cells[ValueColumn].Value.ToString().Split(new char[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
        //            DataTable dt = row.Cells[OptionsColumn].Value as DataTable;
        //            foreach (string myitem in checkedItems)
        //            {
        //                foreach (DataRow myrow in dt.Rows)
        //                {
        //                    myrow["VALUE"] = myrow["VALUE"] == null ? "" : myrow["VALUE"].ToString();
        //                    if (myitem == myrow["VALUE"].ToString())
        //                    {
        //                        txt += myrow["TEXT"] == null ? "" : myrow["TEXT"].ToString() + seperator;
        //                        //e.CellElement.Value = row["VALUE"] == null ? "": row["VALUE"].ToString().Trim();
        //                        //e.CellElement.Text = row["TEXT"] == null ? "" : row["TEXT"].ToString().Trim();
        //                        break;
        //                    }
        //                }
        //            }
        //            char[] charsToTrim = { seperator };  
        //            row.Tag  = row.Cells[ValueColumn].Value.ToString ();
        //            row.Cells[ValueColumn].Value  = txt.TrimEnd(charsToTrim);
        //        }
        //    }

        //    DataTable mydt = _gridview.DataSource as DataTable;
        //    if (mydt != null)
        //        mydt.AcceptChanges();
        //    ValueChanged = false;
        //}

        //public void SetValueForText(DataTable dt)
        //{            
        //    foreach (GridViewRowInfo row in _gridview.Rows)
        //    {
        //        if (!(row.Cells[TypeColumn].Value is string))
        //            continue;
        //        if (getCellType(row) != PropertyType.DropDownList && getCellType(row) != PropertyType.CheckCombo)
        //            continue;

        //        if (row.Cells[OptionsColumn].Value is DataTable)
        //        {
        //            string Name = Convert.ToString(row.Cells[NameColumn]==null ? "":row.Cells[NameColumn].Value).Trim();
        //            string strExpression = string.Format("KeyGuid='{0}'", Name);
        //            DataRow[] foundRows = dt.Select(strExpression);
        //            if (foundRows.Length > 0)
        //            {
        //                foundRows[0][ValueColumn] = row.Tag==null?"":row.Tag;
        //            }                    
        //        }
        //    }
        //    dt.AcceptChanges();
        //}

        public string UniqueColumn
        {
            get { return _uniqueColumn; }
            set { _uniqueColumn = value; }
        }

        public string NameColumn
        {
            get { return _nameColumn; }
            set { _nameColumn = value; }
        }

        public string ValueColumn
        {
            get { return _valueColumn; }
            set { _valueColumn = value; }
        }

        public string TypeColumn
        {
            get { return _typeColumn; }
            set { _typeColumn = value; }
        }

        public string OptionsColumn
        {
            get { return _optionsColumn; }
            set { _optionsColumn = value; }
        }
        public string SiteColumn
        {
            get { return _SiteColumn; }
            set { _SiteColumn = value; }
        }
        /// <summary>
        /// e.g.
        ///     "ModuleID" ;
        ///     "Title Group By ModuleID" means "Title" as Category text and "ModuleID" as Category order;
        /// </summary>
        public string GroupBy
        {
            get
            {
                return this._gridview.MasterGridViewTemplate.GroupBy;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                _gridview.MasterGridViewTemplate.GroupByExpressions.Clear();

                if (value.ToUpper().IndexOf("GROUP BY") < 0)
                {
                    //! Sample
                    //this._gridview.MasterGridViewTemplate.GroupByExpressions.Add("City GroupBy City");
                    this._gridview.MasterGridViewTemplate.GroupBy = value + " GroupBy " + value;
                }
                else
                {
                    // "Title Group By ModuleID"
                    GridGroupByExpression exp1 = GridGroupByExpression.Parse(value);
                    exp1.DefaultFormatString = "{1}";
                    //exp1.SelectFields[0].SortOrder = asc ? RadSortOrder.Ascending : RadSortOrder.Descending;

                    _gridview.MasterGridViewTemplate.GroupByExpressions.Add(exp1);
                }
            }
        }

        public string DefaultValueColumn
        {
            get { return _defaultValueColumn; }
            set { _defaultValueColumn = value; }
        }

        public string RegularExpressionColumn
        {
            get { return _regularExpressionColumn; }
            set { _regularExpressionColumn = value; }
        }

        public string DescriptionColumn
        {
            get { return _descriptionColumn; }
            set { _descriptionColumn = value; }
        }

        /// <summary>
        /// e.g. 
        ///     "ColumnName" or "ColumnName ASC" (default Ascending) ;
        ///     "ColumnName DESC" ;
        /// </summary>
        public string OrderColumn
        {
            get { return _orderColumn; }
            set
            {
                // Now we support only one sort column.
                _gridview.MasterGridViewTemplate.SortExpressions.Clear();

                string tmp = value;
                if (string.IsNullOrEmpty(tmp))
                    return;

                tmp = tmp.Trim();

                int idx = tmp.IndexOf(' ');

                RadSortOrder sort = RadSortOrder.Ascending;

                if (idx >= 0)
                {
                    _orderColumn = tmp.Substring(0, idx);

                    if (tmp.ToUpper().IndexOf(" ASC") > 0)
                        sort = RadSortOrder.Ascending;
                    else if (tmp.ToUpper().IndexOf(" DESC") > 0)
                        sort = RadSortOrder.Descending;
                }
                else
                    _orderColumn = value;

                _gridview.MasterGridViewTemplate.SortExpressions.Add(_orderColumn, sort);
            }
        }

        public Grid GridView
        {
            get { return _gridview; }
        }

        public enum PropertyType
        {
            Unknown = 999,
            Text = 0,
            Integer = 1,
            Numeric = 2,
            DropDownList = 3,
            DropDown = 4,
            CheckCombo = 5,
            Color = 6,
            Font = 7,
            File = 8,
            Folder = 9,
            URL = 10, // useless
            CheckBox = 11,
            DateTime = 12,
            Time = 13,
            Password = 15,
            PrinterDropDownList = 16,
            #region Added by Blue for [RC603.10] - US16934, 06/07/2014
            OnlineUserSetting = 17,
            #endregion
        }

        public ITranslator Translator
        {
            get { return _translator; }
            set { _translator = value; }
        }

        public bool ValueChanged
        {
            get { return _valueChanged; }
            set
            {
                _valueChanged = value;

                if (!value)
                {
                    _mapChangedName.Clear();
                }
            }
        }

        public string CurrentCellName
        {
            get
            {
                return getCurrentRowValue(_nameColumn);
            }
            set
            {
                setCurrentCellByName(value);
            }
        }

        public event EventHandler<ErrorPromptEventArgs> ErrorPormpted;

        #region protected & private funtions

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._txtDesc = new System.Windows.Forms.TextBox();
            this._gridview = new Grid();
            ((System.ComponentModel.ISupportInitialize)(this._gridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridview.MasterGridViewTemplate)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _gridview
            // 
            this._gridview.TabIndex = 1;
            this._gridview.Dock = DockStyle.Fill;
            this._gridview.Location = new System.Drawing.Point(0, 0);
            this._gridview.Name = "_gridview";
            this._gridview.ShowGroupPanel = false;
            this._gridview.MultiSelect = false;
            this._gridview.SelectionMode = GridViewSelectionMode.FullRowSelect;
            this._gridview.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            this._gridview.MasterGridViewTemplate.ShowColumnHeaders = false;
            this._gridview.MasterGridViewTemplate.ShowRowHeaderColumn = false;
            this._gridview.MasterGridViewTemplate.ShowFilteringRow = false;
            this._gridview.MasterGridViewTemplate.ShowGroupedColumns = false;
            this._gridview.MasterGridViewTemplate.AllowAddNewRow = false;
            this._gridview.MasterGridViewTemplate.AutoExpandGroups = true;
            this._gridview.MasterGridViewTemplate.AllowColumnHeaderContextMenu = false;
            this._gridview.MasterGridViewTemplate.AllowCellContextMenu = false;
            this._gridview.EnableCustomDrawing = true;
            //this._gridview.CreateCell += new GridViewCreateCellEventHandler(_gridview_CreateCell);
            //this._gridview.CellValidated += new CellValidatedEventHandler(_gridview_CellValidated);
            this._gridview.EditorRequired += new EditorRequiredEventHandler(_gridview_EditorRequired);
            this._gridview.CellBeginEdit += new GridViewCellCancelEventHandler(_gridview_CellBeginEdit);
            this._gridview.CellEndEdit += new GridViewCellEventHandler(_gridview_CellEndEdit);
            this._gridview.CellPaint += new GridViewCellPaintEventHandler(_gridview_CellPaint);
            this._gridview.CellFormatting += new CellFormattingEventHandler(_gridview_CellFormatting);
            this._gridview.SelectionChanged += new EventHandler(_gridview_SelectionChanged);
            this._gridview.CellValueChanged += new GridViewCellEventHandler(_gridview_CellValueChanged);
            this._gridview.MouseLeave += new EventHandler(_gridview_MouseLeave);
            this._gridview.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(_gridview_ToolTipTextNeeded);
            // 
            // _txtDesc
            // 
            this._txtDesc.Dock = DockStyle.Fill;
            this._txtDesc.Multiline = true;
            this._txtDesc.Location = new System.Drawing.Point(0, 0);
            this._txtDesc.Name = "_txtDesc";
            this._txtDesc.ReadOnly = true;
            this._txtDesc.BackColor = Color.LightGray;
            this._txtDesc.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._gridview);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._txtDesc);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 0;
            // 
            // PropertyList
            // 
            this.ClientSize = new System.Drawing.Size(600, 371);
            this.Controls.Add(this.splitContainer1);
            // 
            // 
            // 
            ((System.ComponentModel.ISupportInitialize)(this._gridview.MasterGridViewTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._gridview)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        void _gridview_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            GridDataCellElement gdce = (sender as GridDataCellElement);
            if (gdce != null)
            {
                GridViewDataColumn gvdc = gdce.ColumnInfo as GridViewDataColumn;
                if (gvdc != null && gvdc.FieldName.Equals(_nameColumn))
                {
                    e.ToolTipText = Convert.ToString((gdce as GridDataCellElement).Value);
                }
                else
                {
                    e.ToolTipText = "";
                }
            }
            else
            {
                e.ToolTipText = "";
            }
        }

        void _gridview_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (string.Compare(e.Column.HeaderText, ValueColumn, StringComparison.OrdinalIgnoreCase) != 0)
                return;
            string name = getColumnValue(e.Row, _nameColumn);

            if (name != null && name != string.Empty && !_mapChangedName.ContainsKey(name))
                _mapChangedName.Add(name, "");

            if (e.Row.Cells[e.ColumnIndex].Value.GetType().Equals(typeof(string)))
            {
                _gridview.CellValueChanged -= _gridview_CellValueChanged;

                int maxLength = (e.Row.DataBoundItem as DataRowView).Row.Table.Columns[(e.Column as GridViewDataColumn).UniqueName].MaxLength;
                maxLength = maxLength < 256 ? 256 : maxLength;

                if (e.Value.ToString().Trim().Length > maxLength)
                {
                    if (ErrorPormpted != null)
                    {
                        ErrorPromptEventArgs args = new ErrorPromptEventArgs();
                        args.ErrorText = "Value is too long. It must be in {0} charactors";
                        args.ErrorTextParameters = new List<string>(new string[] { maxLength.ToString() });
                        ErrorPormpted(e.Row.Cells[e.ColumnIndex], args);

                        e.Row.Cells[e.ColumnIndex].Value = string.Empty;
                    }
                }
                else
                {

                    //Edit by Blue for EK_HI00135225
                    if (e.Row.Cells[_nameColumn].Value.ToString().EndsWith("Separator"))
                    {
                        e.Row.Cells[e.ColumnIndex].Value = string.IsNullOrWhiteSpace(e.Value.ToString()) ? e.Value.ToString() : e.Value.ToString().Trim();
                    }
                    else
                    {
                        e.Row.Cells[e.ColumnIndex].Value = e.Value.ToString().Trim();
                    }
                }

                _gridview.CellValueChanged += new GridViewCellEventHandler(_gridview_CellValueChanged);
            }


            _valueChanged = true;
        }

        void _gridview_SelectionChanged(object sender, EventArgs e)
        {
            _txtDesc.Text = string.Empty;

            if (_gridview.SelectedRows == null || _gridview.SelectedRows.Count < 1)
                return;

            if (_descriptionColumn == null || _descriptionColumn.Trim() == string.Empty ||
                !_gridview.Columns.Contains(_descriptionColumn))
                return;

            string desc = getColumnValue(_gridview.SelectedRows[0], _descriptionColumn);

            if (_translator != null)
            {
                _txtDesc.Text = _translator.GetLanguage(desc, "", "");
            }
            else
            {
                _txtDesc.Text = desc;
            }
        }

        void _gridview_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridViewDataColumn column = e.CellElement.ColumnInfo as GridViewDataColumn;
            if (column == null || _valueColumn == null || column.FieldName == null ||
                column.FieldName.ToUpper() != _valueColumn.ToUpper())
                return;

            CSPropertyList.PropertyType pType = getCellType(e.CellElement.RowInfo);

            switch (pType)
            {
                case PropertyType.CheckBox:
                    {
                        if (e.CellElement.Text == "0")
                        {
                            e.CellElement.Text = "·ñ";
                        }
                        else if (e.CellElement.Text == "1")
                        {
                            e.CellElement.Text = "ÊÇ";
                        }
                    }
                    break;
                case PropertyType.CheckCombo:
                    //if (e.CellElement.RowInfo.Cells[OptionsColumn].Value is DataTable)
                    //{
                    //    string[] checkedItems;
                    //    char seperator = '|';
                    //    string txt = "";
                    //    checkedItems = e.CellElement.Text.Trim().Split(new char[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
                    //    DataTable dt = e.CellElement.RowInfo.Cells[OptionsColumn].Value as DataTable;
                    //    foreach (string myitem in checkedItems)
                    //    {
                    //        foreach (DataRow row in dt.Rows)
                    //        {
                    //            if (myitem == row["VALUE"].ToString())
                    //            {
                    //                txt += row["TEXT"] == null ? "" : row["TEXT"].ToString() + seperator;
                    //                //e.CellElement.Value = row["VALUE"] == null ? "": row["VALUE"].ToString().Trim();
                    //                //e.CellElement.Text = row["TEXT"] == null ? "" : row["TEXT"].ToString().Trim();
                    //                break;
                    //            }
                    //        }
                    //    }
                    //    char[] charsToTrim = { seperator };
                    //    e.CellElement.Text = txt.TrimEnd(charsToTrim);
                    //    //System.Diagnostics.Debug.Print(System.DateTime.Now.ToString()); 
                    //}
                    break;
                default:
                    break;
            }
        }

        void _gridview_CellPaint(object sender, GridViewCellPaintEventArgs e)
        {
            GridViewDataColumn column = e.Cell.ColumnInfo as GridViewDataColumn;
            if (column == null || column.FieldName == null || column.FieldName.Trim() == string.Empty)
                return;

            if (_nameColumn != null && column.FieldName.ToUpper() == _nameColumn.ToUpper())
            #region Name Column
            {
                string name = getColumnValue(e.Cell.RowInfo, _nameColumn);

                if (name != null && _mapChangedName.ContainsKey(name))
                {
                    Color clr = Color.Blue;
                    Brush brush = new SolidBrush(clr);

                    //e.Graphics.FillRectangle(brush, e.Cell.FullRectangle);

                    e.Graphics.DrawString("*", new Font("", 20), brush,
                        new PointF(e.Cell.FaceRectangle.Right - 20, e.Cell.FaceRectangle.Top + 2));
                }
            }
            #endregion
            else if (_valueColumn != null && column.FieldName.ToUpper() == _valueColumn.ToUpper())
            #region Value Column
            {
                CSPropertyList.PropertyType pType = getCellType(e.Cell.RowInfo);

                switch (pType)
                {
                    case PropertyType.Color:
                        {
                            Color clr = new Color();

                            if (!CSCommonFunctions.Convert2Color(e.Cell.Text, ref clr))
                                return;

                            Brush brush = new SolidBrush(clr);

                            e.Graphics.FillRectangle(brush, e.Cell.FullRectangle);

                            string name = getColumnValue(e.Cell.RowInfo, _nameColumn);
                            string valu = getColumnValue(e.Cell.RowInfo, _valueColumn);

                            //System.Diagnostics.Debug.WriteLine(name + ",\t\tColor=" + e.Cell.Text + ",\t\tValue=" + valu);
                        }
                        break;
                    case PropertyType.Password:
                        {
                            e.Graphics.FillRectangle(Brushes.Black, e.Cell.FullRectangle);
                        }
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }

        void _gridview_EditorRequired(object sender, EditorRequiredEventArgs e)
        {
            try
            {
                if (_gridview.CurrentCell == null)
                    return;
                if (ReadOnlyRowsUniqueColumn.Count > 0 && ReadOnlyRowsUniqueColumn.Contains(_gridview.CurrentRow.Cells[this.UniqueColumn].Value.ToString()))
                {                    
                    return;
                }

                CSPropertyList.PropertyType pType = getCellType(_gridview.CurrentCell.RowInfo);

                string currentValue = string.Empty;
                if (_gridview.CurrentCell != null)
                {
                    currentValue = System.Convert.ToString(_gridview.CurrentCell.Value);
                }

                switch (pType)
                {
                    case PropertyType.Integer:
                        #region Integer
                        {
                            //e.EditorType = typeof(MyGridEditor<MyGridEditorElement<RadSpinEditor>, RadSpinEditor>);

                            RadHostItem host = _spinGridEditor.EditorElement as RadHostItem;
                            if (host != null)
                            {
                                RadSpinEditor spin = host.HostedControl as RadSpinEditor;
                                if (spin != null)
                                {
                                    decimal min = 0, max = 200;

                                    getSpinRange(_gridview.CurrentCell.RowInfo, ref min, ref max);

                                    spin.Minimum = min;
                                    spin.Maximum = max;
                                    spin.DecimalPlaces = 0;
                                }
                            }

                            _spinGridEditor.ValueChanging -= _spinGridEditor_ValueChanging;
                            _spinGridEditor.ValueChanging += new ValueChangingEventHandler(_spinGridEditor_ValueChanging);

                            _spinGridEditor.Value = _gridview.CurrentCell.Text;

                            e.Editor = _spinGridEditor;
                        }
                        #endregion
                        break;
                    case PropertyType.Numeric:
                        break;
                    case PropertyType.PrinterDropDownList:
                        #region PrinterDropDownList
                        {
                            setCellOptions(_gridview.CurrentCell.RowInfo, getPrinterListFromClient());

                            OnRequireDropDownList(e, currentValue, RadDropDownStyle.DropDownList);
                        }
                        #endregion
                        break;
                    case PropertyType.DropDownList:
                        #region DropDownList
                        {
                            if (_gridview.CurrentRow.Cells[OptionsColumn].Value is DataTable)
                                OnRequireDropDownList(e, _gridview.CurrentRow.Cells[ValueColumn].Value.ToString(), RadDropDownStyle.DropDownList);
                            else
                                OnRequireDropDownList(e, currentValue, RadDropDownStyle.DropDownList);
                        }
                        #endregion
                        break;
                    case PropertyType.DropDown:
                        #region DropDown
                        {
                            if (_gridview.CurrentRow.Cells[OptionsColumn].Value is DataTable)
                                OnRequireDropDownList(e, _gridview.CurrentRow.Cells[ValueColumn].Value.ToString(), RadDropDownStyle.DropDown);
                            else
                                OnRequireDropDownList(e, currentValue, RadDropDownStyle.DropDown);
                        }
                        #endregion
                        break;
                    case PropertyType.CheckCombo:
                        #region CheckCombo
                        {
                            if (_gridview.CurrentRow.Cells[OptionsColumn].Value is DataTable)
                                OnRequireCheckComboBox(e, _gridview.CurrentRow.Cells[ValueColumn + "__BACKUP"].Value.ToString());
                            else
                                OnRequireCheckComboBox(e, currentValue);
                        }
                        #endregion
                        break;
                    case PropertyType.Color:
                        break;
                    case PropertyType.Font:
                        break;
                    case PropertyType.File:
                        break;
                    #region #region Added by Blue for [RC603.10] - US16934, 06/07/2014
                    case PropertyType.OnlineUserSetting:
                        break;
                    #endregion
                    case PropertyType.Folder:
                        break;
                    case PropertyType.URL:
                        // not implement
                        break;
                    case PropertyType.CheckBox:
                        break;
                    case PropertyType.DateTime:
                        #region Date
                        {
                            DateTime dt;
                            if (!DateTime.TryParse(currentValue, out dt))
                            {
                                _gridview.CurrentCell.Value = System.DateTime.Now.ToString("yyyy-MM-dd");
                            }

                            _datetime.CustomFormat = "yyyy-MM-dd";
                            e.Editor = _datetime;
                        }
                        #endregion
                        break;
                    case PropertyType.Time:
                        #region Time
                        {
                            DateTime dt;
                            if (!DateTime.TryParse(currentValue, out dt))
                            {
                                _gridview.CurrentCell.Value = System.DateTime.Now.ToString("HH:mm");
                            }

                            _datetime.CustomFormat = "HH:mm";
                            e.Editor = _datetime;
                        }
                        #endregion
                        break;
                    case PropertyType.Password:
                        {
                            e.Editor = _pwdGridEditor;
                        }
                        break;
                    default:
                        break;
                }

                //if (e.Editor != null)
                //    System.Diagnostics.Debug.WriteLine(e.ToString() + ",\t" + e.Editor.ToString());
                //else if(e.EditorType != null)
                //    System.Diagnostics.Debug.WriteLine(e.ToString() + ",\t" + e.EditorType.ToString());
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                System.Diagnostics.Debug.WriteLine("_gridview_EditorRequired, " + ex.Message);
            }
        }

        void _gridview_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            try
            {
                if (ReadOnlyRowsUniqueColumn.Count >0 && ReadOnlyRowsUniqueColumn.Contains(_gridview.CurrentRow.Cells[this.UniqueColumn].Value.ToString()))
                    e.Cancel=true;

                GridViewDataRowInfo row = _gridview.Rows[e.RowIndex];
                CSPropertyList.PropertyType pType = getCellType(row as GridViewRowInfo);

                string currentValue = string.Empty;
                if (_gridview.CurrentCell != null)
                {
                    currentValue = System.Convert.ToString(_gridview.CurrentCell.Value);
                }

                switch (pType)
                {
                    case PropertyType.Integer:
                        break;
                    case PropertyType.Numeric:
                        break;
                    case PropertyType.PrinterDropDownList:
                        break;
                    case PropertyType.DropDownList:
                        break;
                    case PropertyType.DropDown:
                        break;
                    case PropertyType.CheckCombo:
                        break;
                    case PropertyType.Color:
                        #region Color
                        {
                            CSColorSelectForm dlg = new CSColorSelectForm();
                            Color selectedColor = Color.FromArgb(127, 127, 127);
                            if (!string.IsNullOrEmpty(_gridview.CurrentCell.Value.ToString()))
                            {
                                string[] argb = _gridview.CurrentCell.Value.ToString().Split(",".ToCharArray());
                                if (argb != null && argb.Length > 0)
                                {
                                    selectedColor = Color.FromArgb(int.Parse(argb[0]), int.Parse(argb[1]), int.Parse(argb[2]), int.Parse(argb[3]));
                                }
                            }
                            dlg.SelectedColor = selectedColor;
                            if (DialogResult.OK == dlg.ShowDialog())
                            {
                                _gridview.CurrentCell.Value = dlg.SelectedColor.A + ","
                                    + dlg.SelectedColor.R + ","
                                    + dlg.SelectedColor.G + ","
                                    + dlg.SelectedColor.B;
                            }

                            e.Cancel = true;
                        }
                        #endregion
                        break;
                    case PropertyType.Font:
                        #region Font
                        {
                            FontDialog dlg = new FontDialog();
                            //dlg.Font.FontFamily.Name = System.Convert.ToString(_gridview.CurrentCell.Value);
                            if (DialogResult.OK == dlg.ShowDialog())
                            {
                                _gridview.CurrentCell.Value = dlg.Font.FontFamily.Name;
                            }

                            e.Cancel = true;
                        }
                        #endregion
                        break;
                    case PropertyType.File:
                        #region File
                        {
                            FileDialog dlg = new OpenFileDialog();
                            dlg.FileName = System.Convert.ToString(_gridview.CurrentCell.Value);
                            if (DialogResult.OK == dlg.ShowDialog())
                            {
                                _gridview.CurrentCell.Value = dlg.FileName;
                            }

                            e.Cancel = true;
                        }
                        #endregion
                        break;
                    #region #region Added by Blue for [RC603.10] - US16934, 06/07/2014
                    case PropertyType.OnlineUserSetting:
                        using (CSOnlineUserCountSettingForm frm = new CSOnlineUserCountSettingForm())
                        {
                            frm.OnlineUserCountSettingString = _gridview.CurrentCell.Value.ToString();
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                _gridview.CurrentCell.Value = frm.OnlineUserCountSettingString;
                            }
                        }
                        e.Cancel = true;
                        break;
                    #endregion
                    case PropertyType.Folder:
                        #region Folder
                        {
                            FolderBrowserDialog dlg = new FolderBrowserDialog();
                            dlg.SelectedPath = System.Convert.ToString(_gridview.CurrentCell.Value);
                            if (DialogResult.OK == dlg.ShowDialog())
                            {
                                _gridview.CurrentCell.Value = dlg.SelectedPath;
                            }

                            e.Cancel = true;
                        }
                        #endregion
                        break;
                    case PropertyType.URL:
                        // not implement
                        break;
                    case PropertyType.CheckBox:
                        #region CheckBox
                        if(e.Cancel==false)
                        {
                            if (currentValue.ToUpper() == "TRUE" ||
                                currentValue.ToUpper() == "YES" ||
                                currentValue.ToUpper() == "ÊÇ" ||
                                currentValue.ToUpper() == "1")
                            {
                                _gridview.CurrentCell.Value = "0";

                                setColumnValue(_gridview.CurrentCell.RowInfo, _valueColumn, "0");
                            }
                            else if (currentValue.ToUpper() == "FALSE" ||
                                currentValue.ToUpper() == "NO" ||
                                currentValue.ToUpper() == "·ñ" ||
                                currentValue.ToUpper() == "0")
                            {
                                _gridview.CurrentCell.Value = "1";

                                setColumnValue(_gridview.CurrentCell.RowInfo, _valueColumn, "1");
                            }

                            e.Cancel = true;
                        }
                        #endregion
                        break;
                    case PropertyType.DateTime:
                        break;
                    case PropertyType.Time:
                        break;
                    case PropertyType.Password:
                        break;
                    default:
                        break;
                }

                if (_gridview.CurrentCell != null && _gridview.CurrentCell.Value != null)
                    System.Diagnostics.Debug.WriteLine("_gridview_CellBeginEdit," + _gridview.CurrentCell.Value.ToString());
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                System.Diagnostics.Debug.WriteLine("_gridview_CellBeginEdit, " + ex.Message);
            }
        }

        void _gridview_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if (_gridview.CurrentCell == null)
                return;
            if (ReadOnlyRowsUniqueColumn.Count > 0 && ReadOnlyRowsUniqueColumn.Contains(_gridview.CurrentRow.Cells[this.UniqueColumn].Value.ToString()))
                return;

            CSPropertyList.PropertyType pType = getCellType(_gridview.CurrentCell.RowInfo);

            //string currentValue = string.Empty;
            //if (_gridview.CurrentCell != null)
            //{
            //    currentValue = System.Convert.ToString(_gridview.CurrentCell.Value);
            //}

            switch (pType)
            {
                case PropertyType.Integer:
                    RadHostItem host = _spinGridEditor.EditorElement as RadHostItem;
                    if (host != null)
                    {
                        RadSpinEditor spin = host.HostedControl as RadSpinEditor;
                        if (spin != null)
                        {
                            decimal currentValue = 0, maxValue = spin.Maximum, minValue = spin.Minimum;
                            decimal.TryParse(_gridview.CurrentCell.Value.ToString(), out currentValue);

                            _gridview.CurrentCell.Value = spin.Text;
                            if (currentValue > maxValue)
                            {
                                _gridview.CurrentCell.Value = spin.Maximum;
                            }
                            else if (currentValue < minValue)
                            {
                                _gridview.CurrentCell.Value = spin.Minimum;
                            }                            
                        }
                    }
                    break;
                case PropertyType.Numeric:
                    break;
                case PropertyType.PrinterDropDownList:
                case PropertyType.DropDownList:
                    //_gridview.CurrentCell.Value = _currentEditingValue;
                    if (e.Row.Cells[OptionsColumn].Value is DataTable)
                    {
                        MyGridEditorElement<EmbeddedCombobox> host1 = _cbxGridEditor.EditorElement as MyGridEditorElement<EmbeddedCombobox>;
                        if (host1 != null)
                        {
                            EmbeddedCombobox cbb = host1.HostedControl as EmbeddedCombobox;
                            if (cbb != null)
                            {
                                _gridview.CurrentCell.RowInfo.Cells[ValueColumn + "__BACKUP"].Value = cbb.SelectedItem == null ? "" : ((RadComboBoxItem)(cbb.SelectedItem)).Name;
                                _gridview.CurrentCell.Value = cbb.Text;
                            }
                        }
                    }
                    break;                
                case PropertyType.DropDown:
                    //_gridview.CurrentCell.Value = _currentEditingValue;
                    //if (e.Row.Cells[OptionsColumn].Value is DataTable)
                    //{
                    MyGridEditorElement<EmbeddedCombobox> host2 = _cbxGridEditor.EditorElement as MyGridEditorElement<EmbeddedCombobox>;                  
                        if (host2 != null)
                        {
                            EmbeddedCombobox cbb = host2.HostedControl as EmbeddedCombobox;                            
                            if (cbb != null)
                            {
                                if (cbb.SelectedIndex < 0 && cbb.Text != "")
                                {
                                    if (e.Row.Cells[OptionsColumn].Value is DataTable)
                                    {
                                        foreach (RadComboBoxItem item in cbb.Items)
                                        {
                                            if (item.Name == _gridview.CurrentCell.RowInfo.Cells[ValueColumn + "__BACKUP"].Value)
                                            {                                                
                                                _gridview.CurrentCell.Value = item.Text;
                                                break;
                                            }
                                        }
                                        //cbb.SelectedValue = _gridview.CurrentCell.RowInfo.Cells[ValueColumn + "__BACKUP"].Value;
                                        //_gridview.CurrentCell.Value = cbb.Text;
                                    }
                                    else
                                    {
                                        _gridview.CurrentCell.Value  = _gridview.CurrentCell.RowInfo.Cells[ValueColumn + "__BACKUP"].Value.ToString();
                                    }
                                }
                                else
                                {
                                    if (e.Row.Cells[OptionsColumn].Value is DataTable)
                                    {
                                        _gridview.CurrentCell.RowInfo.Cells[ValueColumn + "__BACKUP"].Value = cbb.SelectedItem == null ? "" : ((RadComboBoxItem)(cbb.SelectedItem)).Name;
                                        _gridview.CurrentCell.Value = cbb.Text;
                                    }
                                    else
                                    {
                                        _gridview.CurrentCell.RowInfo.Cells[ValueColumn + "__BACKUP"].Value = cbb.Text;
                                        _gridview.CurrentCell.Value = cbb.Text;
                                    }
                                }
                            }
                        }
                    //}
                    break;
                case PropertyType.CheckCombo:
                    if (e.Row.Cells[OptionsColumn].Value is DataTable)
                    {
                        MyGridEditorElement<EmbeddedCheckCombobox> host1 = _chkcbxGridEditor.EditorElement as MyGridEditorElement<EmbeddedCheckCombobox>;
                        if (host1 != null)
                        {
                            EmbeddedCheckCombobox ccb = host1.HostedControl as EmbeddedCheckCombobox;
                            if (ccb != null)
                            {
                                _gridview.CurrentCell.RowInfo.Cells[ValueColumn + "__BACKUP"].Value = ccb.CheckedNames;
                            }
                        }
                    }
                    break;
                case PropertyType.Color:
                    break;
                case PropertyType.Font:
                    break;
                case PropertyType.File:
                    break;
                #region #region Added by Blue for [RC603.10] - US16934, 06/07/2014
                case PropertyType.OnlineUserSetting:
                    break;
                #endregion
                case PropertyType.Folder:
                    break;
                case PropertyType.URL:
                    // not implement
                    break;
                case PropertyType.CheckBox:
                    break;
                case PropertyType.DateTime:
                    break;
                case PropertyType.Time:
                    break;
                case PropertyType.Password:
                    break;
                default:
                    break;
            }

            CSCommonFunctions.log("CellEndEdit,"
                + e.RowIndex + ","
                + e.ColumnIndex + ","
                + (e.Value == null ? "null" : e.Value.ToString()));
        }

        /// <summary>
        /// EK_HI00112855
        /// EK_HI00114448
        /// EK_HI00114466
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _gridview_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                Point p = _gridview.PointToClient(Cursor.Position);

                if (p.X <= 0 || p.X >= _gridview.Width ||
                    p.Y <= 0 || p.Y >= _gridview.Height)
                    _gridview.CloseEditor();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                System.Diagnostics.Debug.WriteLine("_gridview_MouseLeave, " + ex.Message);
            }
        }

        private CSPropertyList.PropertyType getCellType(GridViewRowInfo rowInfo)
        {
            if (rowInfo == null)
                return CSPropertyList.PropertyType.Unknown;

            if (_typeColumn == null || _typeColumn == string.Empty)
                return CSPropertyList.PropertyType.Text;

            IEnumerator etor = rowInfo.Cells.GetEnumerator();
            while (etor.MoveNext())
            {
                GridViewCellInfo cel = etor.Current as GridViewCellInfo;
                if (cel == null)
                    continue;

                if (cel.ColumnInfo.FieldName.ToUpper() == _typeColumn.ToUpper())
                {
                    return CSCommonFunctions.toEnumType<CSPropertyList.PropertyType>(cel.Value);
                }
            }

            return CSPropertyList.PropertyType.Unknown;
        }

        private string getCellOptions(GridViewRowInfo rowInfo)
        {
            if (rowInfo == null)
                return string.Empty;

            if (_optionsColumn == null || _optionsColumn == string.Empty)
                return string.Empty;

            IEnumerator etor = rowInfo.Cells.GetEnumerator();
            while (etor.MoveNext())
            {
                GridViewCellInfo cel = etor.Current as GridViewCellInfo;
                if (cel == null)
                    continue;

                if (cel.ColumnInfo.FieldName.ToUpper() == _optionsColumn.ToUpper())
                {
                    return System.Convert.ToString(cel.Value);
                }
            }

            return String.Empty;
        }

        private void setCellOptions(GridViewRowInfo rowInfo, string theOptions)
        {
            setColumnValue(rowInfo, _optionsColumn, theOptions);
        }

        private string getColumnValue(GridViewRowInfo rowInfo, string columnName)
        {
            if (rowInfo == null)
                return string.Empty;

            if (columnName == null || columnName == string.Empty)
                return string.Empty;

            IEnumerator etor = rowInfo.Cells.GetEnumerator();
            while (etor.MoveNext())
            {
                GridViewCellInfo cel = etor.Current as GridViewCellInfo;
                if (cel == null)
                    continue;

                if (cel.ColumnInfo.FieldName.ToUpper() == columnName.ToUpper())
                {
                    return System.Convert.ToString(cel.Value);
                }
            }

            return String.Empty;
        }

        private void setColumnValue(GridViewRowInfo rowInfo, string columnName, string columnValues)
        {
            if (rowInfo == null)
                return;

            if (columnName == null || columnName == string.Empty)
                return;

            IEnumerator etor = rowInfo.Cells.GetEnumerator();
            while (etor.MoveNext())
            {
                GridViewCellInfo cel = etor.Current as GridViewCellInfo;
                if (cel == null)
                    continue;

                if (cel.ColumnInfo.FieldName.ToUpper() == columnName.ToUpper())
                {
                    cel.Value = columnValues;
                    return;
                }
            }
        }

        private string getCurrentRowValue(string columnName)
        {
            if (_gridview.CurrentCell == null)
                return string.Empty;

            return getColumnValue(_gridview.CurrentCell.RowInfo, columnName);
        }

        private void setCurrentCellByName(string name)
        {
            IEnumerator etor = _gridview.Rows.GetEnumerator();
            while (etor.MoveNext())
            {
                GridViewRowInfo row = etor.Current as GridViewRowInfo;
                if (row == null)
                    continue;

                string tmpName = getColumnValue(row, _nameColumn);
                if (tmpName != null && tmpName.Trim() != string.Empty && tmpName.ToUpper() == name.ToUpper())
                {
                    _gridview.CurrentRow = row;

                    return;
                }
            }
        }

        public void getSpinRange(GridViewRowInfo rowInfo, ref decimal min, ref decimal max)
        {
            string options = getCellOptions(rowInfo);
            string[] tmp = options.Split('-');

            if (tmp.Length > 0)
            {
                string tmp0 = tmp[0].Trim(" ,-[]".ToCharArray());

                int i0;
                if (tmp0.ToUpper() == "MIN")
                {
                    min = decimal.MinValue;
                }
                else if (int.TryParse(tmp0.ToUpper().Replace("M","-"), out i0))
                {
                    min = i0;
                }
            }

            if (tmp.Length > 1)
            {
                string tmp1 = tmp[1].Trim(" ,-[]".ToCharArray());
                
                int i1;
                if (tmp1.ToUpper() == "MAX")
                {
                    max = decimal.MaxValue;
                }
                else if (int.TryParse(tmp1.ToUpper().Replace("M", "-"), out i1))
                {
                    max = i1;
                }
            }
        }

        private void FillComboBox(RadComboBoxElement cbx, GridViewRowInfo row)
        {
            cbx.Items.Clear();

            string sz = getCellOptions(row);
            string[] arr = sz.Split('|');

            if (arr != null && arr.Length > 0)
            {
                List<string> arrList = new List<string>(arr);
                arrList.Sort();
                foreach (string tmp in arrList)
                {
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        RadComboBoxItem ri = new RadComboBoxItem();
                        ri.Name = tmp;
                        ri.Text = tmp;
                        cbx.Items.Add(ri);
                    }
                }
            }

            cbx.Items.Insert(0, new RadComboBoxItem(string.Empty));
        }

        private void FillComboBox(RadComboBox cbx, GridViewRowInfo row)
        {
            cbx.Items.Clear();
            if (row.Cells[OptionsColumn].Value is DataTable)
            {                
                DataTable dt = (row.Cells[OptionsColumn].Value as DataTable);
                foreach (DataRow myrow in dt.Rows)
                {
                    if (myrow["TEXT"] != null && myrow["VALUE"] != null)
                    {
                        RadComboBoxItem ri = new RadComboBoxItem();
                        ri.Name = myrow["VALUE"].ToString();
                        ri.Text = myrow["TEXT"].ToString();
                        cbx.Items.Add(ri);
                    }
                }                               
            }
            else
            {                
                string sz = getCellOptions(row);
                string[] arr = sz.Split('|');

                if (arr != null && arr.Length > 0)
                {
                    List<string> arrList = new List<string>(arr);
                    arrList.Sort();
                    
                    foreach (string tmp in arrList)
                    {
                        if (!string.IsNullOrEmpty(tmp))
                        {
                            RadComboBoxItem ri = new RadComboBoxItem();
                            ri.Name = tmp;
                            ri.Text = tmp;
                            cbx.Items.Add(ri);
                        }
                    }
                }
                RadComboBoxItem item = new RadComboBoxItem();
                item.Name = "";
                item.Text = "";
                cbx.Items.Insert(0, item);
            }
        }

        private void FillCheckComboBox(CSCheckComboBox chkcbx, GridViewRowInfo row)
        {
            /*
            if (chkcbx.Items.Count == chkcbx.MaxDropDownItems + 1)
            {
                chkcbx = new EmbeddedCheckCombobox();
            }
            else
            {
                (chkcbx as EmbeddedCheckCombobox).ClearOldData();
                chkcbx.Items.Clear();
            }            
            */
            (chkcbx as EmbeddedCheckCombobox).ClearOldData();
            chkcbx.Items.Clear();


            if (row.Cells[OptionsColumn].Value is DataTable)
            {
                DataTable dt = row.Cells[OptionsColumn].Value as DataTable;                       
                foreach (DataRow myrow in dt.Rows)
                {
                    chkcbx.AddItem(myrow["VALUE"].ToString(),myrow["TEXT"].ToString());
                }
            }
            else
            {
                string sz = getCellOptions(row);
                string[] arr = sz.Split('|');

                if (arr != null && arr.Length > 0)
                {
                    List<string> arrList = new List<string>(arr);
                    arrList.Sort();
                    foreach (string tmp in arrList)
                    {
                        if (!string.IsNullOrEmpty(tmp))
                        {
                            chkcbx.AddItem(tmp, tmp);
                        }
                    }
                }
            }
            chkcbx.CompleteFillData();
        }

        private Control GetControlFromGridEditor(BaseGridEditor gridEditor)
        {
            RadHostItem host = gridEditor.EditorElement as RadHostItem;
            if (host != null)
            {
                return host.HostedControl;
            }

            return null;
        }

        private string getPrinterListFromClient()
        {
            string strInstalledPrinters = "";

            foreach (string strPrinters in PrinterSettings.InstalledPrinters)
            {
                strInstalledPrinters = strInstalledPrinters + strPrinters + "|";
            }

            return strInstalledPrinters.Trim(" |".ToCharArray());
        }

        private void OnRequireDropDownList(EditorRequiredEventArgs e, string currentValue, RadDropDownStyle ddStyle)
        {
            RadComboBox cbx = GetControlFromGridEditor(_cbxGridEditor) as RadComboBox; ;

            if (cbx != null)
            {
                cbx.ComboBoxElement.TextChanging -= ComboBoxElement_TextChanging;
                if (ddStyle == RadDropDownStyle.DropDown)
                {
                    cbx.ComboBoxElement.TextChanging += new TextChangingEventHandler(ComboBoxElement_TextChanging);
                }

                cbx.DropDownStyle = ddStyle;

                string defaultValue = getColumnValue(_gridview.CurrentCell.RowInfo, _defaultValueColumn);

                FillComboBox(cbx, _gridview.CurrentCell.RowInfo);

                if (currentValue != null)
                {
                    cbx.Text = currentValue;
                }
                else if (!string.IsNullOrEmpty(defaultValue))
                {
                    cbx.Text = defaultValue;
                }
            }

            e.Editor = _cbxGridEditor;

        }

        void ComboBoxElement_TextChanging(object sender, TextChangingEventArgs e)
        {
            RadComboBoxElement cbx = sender as RadComboBoxElement;
            if (!e.NewValue.Equals(string.Empty))
            {
                if (cbx.FindItem(e.NewValue) == null)
                {
                    cbx.Text = e.OldValue;
                    e.Cancel = true;
                }

            }

        }

        void _spinGridEditor_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            RadHostItem host = _spinGridEditor.EditorElement as RadHostItem;
            if (host != null)
            {
                RadSpinEditor spin = host.HostedControl as RadSpinEditor;
                if (spin != null)
                {
                    decimal newValue = (decimal)e.NewValue;
                    if (newValue > spin.Maximum || newValue < spin.Minimum)
                    {
                        _spinGridEditor.Value = e.OldValue;
                        e.Cancel = true;
                    }
                }
            }
        }

        private void OnRequireCheckComboBox(EditorRequiredEventArgs e, string currentValue)
        {
            CSCheckComboBox chkcbx = GetControlFromGridEditor(_chkcbxGridEditor) as CSCheckComboBox;
            if (chkcbx.Items.Count == chkcbx.MaxDropDownItems + 1)
            {
                _chkcbxGridEditor = new MyGridEditor<MyGridEditorElement<EmbeddedCheckCombobox>, EmbeddedCheckCombobox>();
                chkcbx = GetControlFromGridEditor(_chkcbxGridEditor) as CSCheckComboBox;
            }

            if (chkcbx != null)
            {
                FillCheckComboBox(chkcbx, _gridview.CurrentCell.RowInfo);
                chkcbx.Seperator = '|';
                chkcbx.CheckedNames = currentValue;
            }

            e.Editor = _chkcbxGridEditor;
        }

        #endregion
    }

    // Custom Grid Editor Sample from teleric.com
    // http://www.telerik.com/community/forums/winforms/gridview/accept-tab-in-gridview-column.aspx
    public class MyTextBoxEditor : BaseGridEditor
    {
        public override object Value
        {
            get
            {
                MyTextBoxEditorElement editorElement = (MyTextBoxEditorElement)this.EditorElement;
                return editorElement.HostedControl.Text;
            }
            set
            {
                MyTextBoxEditorElement editorElement = (MyTextBoxEditorElement)this.EditorElement;
                if (value == null || value == DBNull.Value)
                {
                    editorElement.HostedControl.Text = string.Empty;
                }
                else
                {
                    editorElement.HostedControl.Text = value.ToString();
                }
            }
        }

        public override void BeginEdit()
        {
            base.BeginEdit();
            MyTextBoxEditorElement editorElement = (MyTextBoxEditorElement)this.EditorElement;
            editorElement.HostedControl.BackColor = Color.White;
        }

        protected override RadElement CreateEditorElement()
        {
            return new MyTextBoxEditorElement();
        }
    }

    public class MyTextBoxEditorElement : RadHostItem
    {
        public MyTextBoxEditorElement()
            : base(new RadTextBox())
        {
            this.StretchHorizontally = true;
            this.StretchVertically = true;
            ((RadTextBox)this.HostedControl).AcceptsReturn = true;
            ((RadTextBox)this.HostedControl).AcceptsTab = true;
            ((RadTextBox)this.HostedControl).Multiline = true;
            ((RadTextBox)this.HostedControl).KeyDown += new KeyEventHandler(MyTextBoxEditorElement_KeyDown);
        }

        void MyTextBoxEditorElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape ||
                (e.KeyCode == Keys.Enter && e.Modifiers == Keys.Shift))
            {
                RadGridView grid = (RadGridView)this.ElementTree.Control;
                grid.GridBehavior.ProcessKeyDown(new KeyEventArgs(e.KeyCode));
            }
        }
    }

    public class MyDateTimeEditor : RadDateTimeEditor
    {
        public override object Value
        {
            get
            {
                if (this.CustomFormat != null && this.CustomFormat != string.Empty)
                {
                    if (this.CustomFormat.ToUpper() == "YYYY-MM-DD")
                    {
                        DateTime dt = System.Convert.ToDateTime(base.Value);
                        if (dt != null)
                        {
                            return dt.ToString("yyyy-MM-dd");
                        }
                    }
                    else if (this.CustomFormat.ToUpper() == "HH:MM")
                    {
                        DateTime dt = System.Convert.ToDateTime(base.Value);
                        if (dt != null)
                        {
                            return dt.ToString("HH:mm");
                        }
                    }
                }

                return base.Value;
            }

            set
            {
                base.Value = value;
            }
        }
    }

    // I implement the generic Custom grid editor, but it not a perfect one.
    // RadSpinEditor does not support the Key press event.
    public class MyGridEditor<T, U> : BaseGridEditor
        where T : MyGridEditorElement<U>, new()
        where U : RadControl, new()
    {
        public override object Value
        {
            get
            {
                T editorElement = (T)this.EditorElement;
                return editorElement.HostedControl.Text;
            }
            set
            {
                T editorElement = (T)this.EditorElement;
                if (value == null || value == DBNull.Value)
                {
                    editorElement.HostedControl.Text = string.Empty;
                }
                else
                {
                    editorElement.HostedControl.Text = value.ToString();
                }
            }
        }

        public override void BeginEdit()
        {
            base.BeginEdit();
            //T editorElement = (T)this.EditorElement;
            //editorElement.HostedControl.BackColor = Color.White;
        }

        protected override RadElement CreateEditorElement()
        {
            return new T();
        }
    }

    public class MyGridEditorElement<T> : RadHostItem where T : RadControl, new()
    {
        public MyGridEditorElement()
            : base(new T())
        {
            this.StretchHorizontally = true;
            this.StretchVertically = true;
            //((T)this.HostedControl).AcceptsReturn = true;
            //((T)this.HostedControl).AcceptsTab = true;
            ((T)this.HostedControl).KeyDown += new KeyEventHandler(T_KeyDown);
        }

        void T_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                RadGridView grid = (RadGridView)this.ElementTree.Control;
                grid.GridBehavior.ProcessKeyDown(new KeyEventArgs(e.KeyCode));
            }
        }
    }

    public class EmbeddedCombobox : RadComboBox
    {
        const int WM_LBUTTONDOWN = 0x201;
        const int WM_KILLFOCUS = 0x8;

        long lastTicks_LButtonDown = 0;

        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadComboBox"; }
        }

        /// <summary>
        /// EK_HI00112777
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDropDownClosing(RadPopupClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("CBX, OnDropDownClosing, ticks=" + System.DateTime.Now.Ticks + ", " + System.DateTime.Now.ToString("fff"));

            if (System.DateTime.Now.Ticks - lastTicks_LButtonDown < 5000000)
            {
                e.Cancel = true;
            }

            base.OnDropDownClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_LBUTTONDOWN:
                    if (!this.IsDroppedDown)
                    {
                        lastTicks_LButtonDown = System.DateTime.Now.Ticks;
                        System.Diagnostics.Debug.WriteLine("CBX, " + m.ToString() + ",ticks=" + lastTicks_LButtonDown + "," + System.DateTime.Now.ToString("fff"));
                    }
                    break;
            }

            base.WndProc(ref m);
        }
        
    }

    public class EmbeddedCheckCombobox : CSCheckComboBox
    {
        const int WM_LBUTTONDOWN = 0x201;

        long lastTicks_LButtonDown = 0;

        protected List<string> listCheckedNames = new List<string>();
        protected List<string> listCheckedTexts = new List<string>();


        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.RadComboBox";
            }
        }

        protected override void OnDropDownClosing(RadPopupClosingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("EmbededCheckCombobox, OnDropDownClosing, ticks=" + System.DateTime.Now.Ticks + ", " + System.DateTime.Now.ToString("fff"));

            if (System.DateTime.Now.Ticks - lastTicks_LButtonDown < 5000000)
            {
                e.Cancel = true;
            }

            base.OnDropDownClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_LBUTTONDOWN:
                    if (!this.IsDroppedDown)
                    {
                        lastTicks_LButtonDown = System.DateTime.Now.Ticks;
                        System.Diagnostics.Debug.WriteLine("EmbededCheckCombobox, " + m.ToString() + ",ticks=" + lastTicks_LButtonDown + "," + System.DateTime.Now.ToString("fff"));
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        protected override void OnItemChecked(object sender)
        {
            _listItemClicked = true;

            RadCheckComboBoxItem item = sender as RadCheckComboBoxItem;
            if (item.IsChecked)
            {
                if (!listCheckedNames.Contains(item.Name))
                {
                    listCheckedNames.Add(item.Name);
                }                

                if (!listCheckedTexts.Contains(item.Text))
                {
                    listCheckedTexts.Add(item.Text);
                }                
            }
            else
            {
                if (listCheckedNames.Contains(item.Name))
                {
                    listCheckedNames.Remove(item.Name);
                }

                if (listCheckedTexts.Contains(item.Text))
                {
                    listCheckedTexts.Remove(item.Text);
                }            
            }

            string names = "";
            string texts = "";
            foreach (string str in listCheckedNames)
            {
                names += str + _seperator;
            }

            foreach (string str in listCheckedTexts)
            {
                texts += str + _seperator;
            }

            _checkedNames = names.Trim(new char[] { _seperator, ' ' });
            this.Text  = texts.Trim(new char[] { _seperator, ' ' });

            _listItemClicked = false;
        }

        public void ClearOldData()
        {
            this.listCheckedNames.Clear();
            this.listCheckedTexts.Clear();
            _listItemClicked = true;
            _checkedNames = string.Empty;
            this.Text = "";
            _listItemClicked = false;
        }

        protected override void SetChecked(string checkedNames)
        {
            _bCheckAll = true;
            ClearOldData();

            string[] keys = checkedNames.Split(_seperator);
            RadCheckComboBoxItem chk;

            foreach (RadItem ri in base.Items)
            {
                chk = ri as RadCheckComboBoxItem;
                if (chk != null)
                {
                    chk.IsChecked = false;
                }
            }

            foreach (string key in keys)
            {
                foreach (RadItem ri in base.Items)
                {
                    chk = ri as RadCheckComboBoxItem;
                    if (chk != null)
                    {
                        // case insensitive
                        if (key == chk.Name)
                        {
                            chk.IsChecked = true;
                            if (!listCheckedNames.Contains(chk.Name))
                            {
                                listCheckedNames.Add(chk.Name);
                            }

                            if (!listCheckedTexts.Contains(chk.Text))
                            {
                                listCheckedTexts.Add(chk.Text);
                            }  
                            break;
                        }                        
                    }
                }
            }            

            string names = "";
            string texts = "";
            foreach (string str in listCheckedNames)
            {
                names += str + _seperator;
            }

            foreach (string str in listCheckedTexts)
            {
                texts += str + _seperator;
            }

            _listItemClicked = true;
            _checkedNames = names.Trim(new char[] { _seperator, ' ' });
            this.Text = texts.Trim(new char[] { _seperator, ' ' });                        
            _listItemClicked = false;

            _bCheckAll = false;
        }

        protected override void CheckAll(bool bCheckAll)
        {
            _bCheckAll = true;
            _bCheckAllorUncheckAll = bCheckAll;

            ClearOldData();

            if (bCheckAll)
            {
                string allNames = string.Empty;
                string allText = string.Empty;

                foreach (RadItem ri in base.Items)
                {
                    RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                    if (chk != null)
                    {
                        allNames += chk.Name + _seperator;
                        allText += chk.Text + _seperator;                        
                        chk.IsChecked = true;
                        if (!listCheckedNames.Contains(chk.Name))
                        {
                            listCheckedNames.Add(chk.Name);
                        }

                        if (!listCheckedTexts.Contains(chk.Text))
                        {
                            listCheckedTexts.Add(chk.Text);
                        }  
                    }
                }
                _listItemClicked = true;
                _checkedNames = allNames.Trim(new char[] { _seperator, ' ' });
                this.Text = allText.Trim(new char[] { _seperator, ' ' });
                _listItemClicked = false;
            }
            else
            {
                foreach (RadItem ri in base.Items)
                {
                    RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                    if (chk != null)
                    {                        
                        chk.IsChecked = false;
                    }
                }
                _listItemClicked = true;
                _checkedNames = string.Empty;
                this.Text = "";
                _listItemClicked = false;
            }

            _bCheckAll = false;
        }


        //protected override void OnItemChecked(object sender)
        //{
        //    _listItemClicked = true;

        //    RadCheckComboBoxItem item = sender as RadCheckComboBoxItem;
        //    string[] checkedItems = _checkedNames.Split(new char[] { _seperator }, StringSplitOptions.RemoveEmptyEntries);
        //    if (item.IsChecked)
        //    {
        //        if (checkedItems != null)
        //        {
        //            List<string> checkedItemList = new List<string>(checkedItems);
        //            if (!checkedItemList.Contains(item.Text))
        //            {
        //                _checkedNames += item.Text + _seperator;
        //            }
        //        }
        //        else
        //        {
        //            _checkedNames += item.Text + _seperator;
        //        }

        //        checkedItems = _checkedNames.Split(new char[] { _seperator }, StringSplitOptions.RemoveEmptyEntries);
        //        if (checkedItems != null && checkedItems.Length > 0)
        //        {
        //            for (int i = checkedItems.Length - 1; i >= 0; i--)
        //            {
        //                if (!IsContainedInItemList(checkedItems[i]))
        //                {
        //                    checkedItems[i] = string.Empty;
        //                    break;
        //                }
        //            }
        //            _checkedNames = string.Empty;
        //            foreach (string checkedItem in checkedItems)
        //            {
        //                if (!string.IsNullOrEmpty(checkedItem))
        //                {
        //                    _checkedNames += checkedItem + _seperator;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (checkedItems != null && checkedItems.Length > 0)
        //        {
        //            for (int i = checkedItems.Length - 1; i >= 0; i--)
        //            {
        //                if (checkedItems[i].Equals(item.Text))
        //                {
        //                    checkedItems[i] = string.Empty;
        //                    break;
        //                }
        //            }
        //            _checkedNames = string.Empty;
        //            foreach (string checkedItem in checkedItems)
        //            {
        //                if (!string.IsNullOrEmpty(checkedItem))
        //                {
        //                    _checkedNames += checkedItem + _seperator;
        //                }
        //            }
        //        }
        //    }
        //    this.Text = _checkedNames.TrimEnd(_seperator);
        //}

        //private bool IsContainedInItemList(string itemText)
        //{
        //    bool isContained = false;
        //    foreach (RadItem item in this.Items)
        //    {
        //        if (item.Text.Equals(itemText))
        //        {
        //            isContained = true;
        //            break;
        //        }
        //    }
        //    return isContained;
        //}
    }

    public class Grid : Telerik.WinControls.UI.RadGridView
    {
        const int WM_SETFOCUS = 0x7;

        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadGridView"; }
        }

        //protected override void WndProc(ref Message m)
        //{
        //    //System.Diagnostics.Debug.WriteLine("Grid, " + m.ToString() + "," + System.DateTime.Now.ToString("fff"));

        //    switch (m.Msg)
        //    {
        //        case WM_SETFOCUS:
        //            {
        //                if (System.DateTime.Now.Ticks - CBX.lastTicks_LButtonDown < 5000000)
        //                {
        //                    System.Diagnostics.Debug.WriteLine("Grid, " + m.ToString() + "," + System.DateTime.Now.ToString("fff"));

        //                    m.Result = (IntPtr)1;
        //                    return;
        //                }
        //            }
        //            break;
        //    }

        //    base.WndProc(ref m);
        //}
    }

    public class ErrorPromptEventArgs : EventArgs
    {
        private string _errorText;

        public string ErrorText
        {
            get { return _errorText; }
            set { _errorText = value; }
        }
        private List<string> _errorTextParameters;

        public List<string> ErrorTextParameters
        {
            get { return _errorTextParameters; }
            set { _errorTextParameters = value; }
        }

    }
}
