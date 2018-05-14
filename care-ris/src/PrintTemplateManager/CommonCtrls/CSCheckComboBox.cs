using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Layouts;
using System.ComponentModel;
namespace Hys.CommonControls
{
    /// <summary>
    /// Note:
    /// 1> DO NOT add items by "RadCheckComboBox.Items.Add()", instead using AddItem() function.
    /// 2> DO NOT contain comma in the name and text string of items.
    /// 3> DO NOT use the data binding.
    /// 
    /// Example:
    /// // add some check box items
    /// _radChkCbx.AddItem("0", "A");
    /// _radChkCbx.AddItem("1", "B");
    /// _radChkCbx.AddItem("2", "C");
    /// _radChkCbx.AddItem("3", "D");
    /// 
    /// // set the checked items
    /// _radChkCbx.CheckedNames = "1,3";
    /// 
    /// // get the checked items
    /// string checkedValues = _radChkCbx.CheckedNames;
    /// 
    /// // get the checked text
    /// string checkedText = _radChkCbx.Text;
    /// </summary>
    public class CSCheckComboBox : RadComboBox
    {
        System.DateTime _dtLastClicked = System.DateTime.Now;
        System.DateTime _tmKeyPress = System.DateTime.Now;
        bool isChkRightClicked = false;
        protected string _checkedNames = string.Empty;
        protected char _seperator = ',';
        const int inputTime = 2;
        Timer _timer = null;
        string _strInputCode = string.Empty;
        string _strPrevInput = string.Empty;
        bool _bFindByPY = false;
        int _nCurrentIndex = 0;
        protected bool _listItemClicked = false;
        protected bool _bCheckAll = false;//for do not every item refresh the text
        protected bool _bCheckAllorUncheckAll = false;//for do not every item refresh the text
        private bool _bKeyBoardSelected = false;//for spacekey select the combobox item

        #region DelayLoad
        private bool _canDelayLoadItems;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanDelayLoadItems
        {
            get { return _canDelayLoadItems; }
            set { _canDelayLoadItems = value; }
        }

        private DataTable _datasource;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTable DataSource
        {
            get { return _datasource; }
            set { _datasource = value; }
        }
        private string _nameField;

        public string DataSourceNameField
        {
            get { return _nameField; }
            set { _nameField = value; }
        }
        private string _textField;

        public string DataSourceTextField
        {
            get { return _textField; }
            set { _textField = value; }
        }

        private int _firstLoadCount = 10;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FirstLoadCount
        {
            get { return _firstLoadCount; }
            set { _firstLoadCount = value; }
        }
        private bool _isLoadMoreClicked;
        #endregion

        public CSCheckComboBox()
        {
            //DropDownStyle = RadDropDownStyle.DropDown;
            //this.ComboBoxElement.TextBoxElement.TextBoxItem.ReadOnly = true;
            this.ComboBoxElement.ArrowButton.MouseDown += new MouseEventHandler(ArrowButton_MouseDown);

            //RadElement rad = FindFirstElementByType(this.RootElement, typeof(Telerik.WinControls.UI.RadTextBoxElement));
            //BorderPrimitive border = FindFirstObjectByType(rad, typeof(Telerik.WinControls.Primitives.BorderPrimitive)) as BorderPrimitive;
            //if (border != null)
            //{
            //    border.Width = 0;
            //    border.TopColor = border.LeftColor = border.RightColor = border.BottomColor = Color.Transparent;
            //}

            AutoCompleteMode = AutoCompleteMode.Suggest;
            this.IntegralHeight = true;

            this.KeyPress += new KeyPressEventHandler(CSCheckComboBox_KeyPress);
            this.ComboBoxElement.RadPropertyChanging += new RadPropertyChangingEventHandler(ComboBoxElement_RadPropertyChanging);
            this.ComboBoxElement.TextBoxElement.TextBoxItem.RadPropertyChanging += new RadPropertyChangingEventHandler(ComboBoxElement_RadPropertyChanging);

            this.ComboBoxElement.ListBoxElement.SelectionMode = SelectionMode.One;

            _timer = new Timer();
            _timer.Interval = 200;
            _timer.Start();
            _timer.Tick += new EventHandler(_timer_Tick);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int WM_KEYDOWN = 0x100;
            Keys keyCode = (Keys)(int)msg.WParam & Keys.KeyCode;
            if (msg.Msg == WM_KEYDOWN && keyCode == Keys.Delete)
            {
                return true;
            }
            else if (msg.Msg == WM_KEYDOWN && keyCode == Keys.Escape && !IsDroppedDown)
            {
                CheckAll(false);
                if (OnCheckedChanged != null) OnCheckedChanged(this);
                isChkRightClicked = false;
                return true;
            }
            else if (msg.Msg == WM_KEYDOWN && (keyCode == Keys.Up || keyCode == Keys.Down) && !IsDroppedDown)
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                return (this.ComboBoxElement.TextBoxElement.Children[0] as RadTextBoxItem).BackColor;
            }
            set
            {
                (this.ComboBoxElement.TextBoxElement.Children[0] as RadTextBoxItem).BackColor = value;
                (this.ComboBoxElement.Children[0] as FillPrimitive).BackColor = value;
            }
        }

        private RadDropDownStyle dropDownStyle = RadDropDownStyle.DropDown;
        [DefaultValue(RadDropDownStyle.DropDown)]
        new public RadDropDownStyle DropDownStyle
        {
            get
            {
                return dropDownStyle;
            }
            set
            {
                if (value == RadDropDownStyle.DropDown)
                {
                    dropDownStyle = RadDropDownStyle.DropDown;
                    base.DropDownStyle = RadDropDownStyle.DropDown;
                }
                else if (((RadDropDownStyle)value) == RadDropDownStyle.DropDownList)
                {
                    if (!IsDesignMode)
                    {
                        dropDownStyle = RadDropDownStyle.DropDown;
                        ComboBoxElement.TextBoxElement.TextBoxItem.ReadOnly = true;
                        base.DropDownStyle = RadDropDownStyle.DropDown;
                    }
                    else//designmode
                    {
                        dropDownStyle = value;
                        base.DropDownStyle = (RadDropDownStyle)value;
                    }
                }
            }
        }

        /// <summary>
        /// The comma is regarded as the separator keyword, 
        /// so name and txt should not contain the comma.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="txt"></param>
        public void AddItem(string name, string txt)
        {
            System.Diagnostics.Debug.Assert(
                name != null && txt != null &&
                name.IndexOf(_seperator) < 0 && txt.IndexOf(_seperator) < 0,
                "Please do not use " + _seperator);

            if (_canDelayLoadItems && base.Items.Count == _firstLoadCount && !_isLoadMoreClicked)
            {
                RadComboBoxItem loadMoreElement = new RadComboBoxItem();
                loadMoreElement.Text = "¸ü¶à...";
                loadMoreElement.Click += new EventHandler(loadMoreElement_Click);
                this.Items.Add(loadMoreElement);
                return;
            }
            else if (_canDelayLoadItems && base.Items.Count >= _firstLoadCount + 1 && !_isLoadMoreClicked)
            {
                return;
            }

            RadCheckComboBoxItem chk = new RadCheckComboBoxItem(txt);
            chk.Name = name;
            //chk.Text = txt;
            //chk.ShowBorder = false;
            //chk.ForeColor = Color.Transparent;
            chk.MouseUp += new MouseEventHandler(chk_MouseUp);
            chk.MouseDown += new MouseEventHandler(chk_MouseDown);
            (chk.Children[3] as RadCheckBoxElement).MouseUp += new MouseEventHandler(chk_MouseUp);
            (chk.Children[3] as RadCheckBoxElement).MouseDown += new MouseEventHandler(chk_MouseDown);
            chk.ToggleStateChanged += new StateChangedEventHandler(chk_ToggleStateChanged);
            chk.ToggleStateChanging += new StateChangingEventHandler(chk_ToggleStateChanging);
            chk.MouseEnter += new EventHandler(chk_MouseEnter);
            //chk.RadPropertyChanging += new RadPropertyChangingEventHandler(chk_RadPropertyChanging);

            base.Items.Add(chk);
        }

        private void loadMoreElement_Click(object sender, EventArgs e)
        {
            _isLoadMoreClicked = true;
            this.Items.Clear();
            if (_datasource != null)
            {
                foreach (DataRow dr in _datasource.Rows)
                {
                    AddItem(dr[_nameField].ToString(), dr[_textField].ToString());
                }
            }
            SetChecked(_checkedNames);
            this.ShowDropDown();
            _isLoadMoreClicked = false;
        }

        public void AddItem(string text)
        {
            AddItem(text, text);
        }

        public void InsertItem(int index, string name, string txt)
        {
            System.Diagnostics.Debug.Assert(
                           name != null && txt != null &&
                           name.IndexOf(_seperator) < 0 && txt.IndexOf(_seperator) < 0,
                           "Please do not use " + _seperator);

            RadCheckComboBoxItem chk = new RadCheckComboBoxItem(txt);
            chk.Name = name;
            //chk.Text = txt;
            //chk.ShowBorder = false;
            //chk.ForeColor = Color.Transparent;
            chk.MouseUp += new MouseEventHandler(chk_MouseUp);
            chk.MouseDown += new MouseEventHandler(chk_MouseDown);
            chk.ToggleStateChanged += new StateChangedEventHandler(chk_ToggleStateChanged);
            chk.ToggleStateChanging += new StateChangingEventHandler(chk_ToggleStateChanging);
            base.Items.Insert(index, chk);
        }

        public void SetAllItemsState(bool isChecked)
        {
            CheckAll(isChecked);
        }

        public string CheckedNames
        {
            get { return _checkedNames; }
            set
            {
                _checkedNames = value;
                SetChecked(value);
                //if (string.IsNullOrEmpty(value))
                //{
                //    _checkedNames = value;
                //}
            }
        }

        public char Seperator
        {
            get { return _seperator; }
            set { _seperator = value; }
        }

        public List<RadCheckComboBoxItem> CheckedItems
        {
            get
            {
                List<RadCheckComboBoxItem> list = new List<RadCheckComboBoxItem>();
                foreach (RadItem item in this.Items)
                {
                    RadCheckComboBoxItem chkitem = item as RadCheckComboBoxItem;
                    if (chkitem != null && chkitem.IsChecked == true)
                    {
                        list.Add(chkitem);
                    }
                }
                return list;
            }
        }

        /// <summary>
        /// http://www.telerik.com/support/kb/winforms/themes/inherit-themes-from-radcontrols.aspx
        /// 
        /// Inherit themes from RadControls
        /// 
        /// When you inherit from a RadControl (or any RadControl descendant), 
        /// the original control themes are not automatically inherited. 
        /// 
        /// if you want to get a derived rad control, you can ...
        /// public class RadCustomButton : RadButton  
        /// {
        ///    public override string ThemeClassName  
        ///    {  
        ///        get 
        ///        {  
        ///            return typeof(RadButton).FullName;  
        ///        }  
        ///    }  
        /// } 
        /// 
        /// if you want to get a derived rad element, you can ...
        /// public class MyRadButtonElement : RadButtonElement     
        /// {     
        ///     protected override Type ThemeEffectiveType     
        ///     {     
        ///         get    
        ///         {     
        ///             return typeof(RadButtonElement);     
        ///         }     
        ///     }     
        /// } 
        /// </summary>
        public override string ThemeClassName
        {
            get
            {
                return "Telerik.WinControls.UI.RadComboBox";
            }
        }

        public delegate void CheckedChangedHandler(object sender);

        public event CheckedChangedHandler OnCheckedChanged;

        public bool ContainName(string name)
        {
            RadItemCollection.RadItemEnumerator it = this.Items.GetEnumerator();
            while (it.MoveNext())
            {
                if (it.Current.Name != null && it.Current.Name.ToUpper() == name.ToUpper())
                    return true;
            }

            return false;
        }

        public bool ContainText(string text)
        {
            RadItemCollection.RadItemEnumerator it = this.Items.GetEnumerator();
            while (it.MoveNext())
            {
                if (it.Current.Text != null && it.Current.Text.ToUpper() == text.ToUpper())
                    return true;
            }

            return false;
        }

        public void Clear()
        {
            this.Text = string.Empty;
            this.Items.Clear();
            this.CheckedNames = "";
        }

        #region private functions

        void ComboBoxElement_RadPropertyChanging(object sender, RadPropertyChangingEventArgs args)
        {
            if (args.Property.Name.Equals("Text"))
            {
                if (_listItemClicked)
                {
                    return;
                }
                else
                {
                    if (IsDroppedDown)
                    {
                        args.Cancel = true;
                    }
                }

            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.Font = new System.Drawing.Font("MS Reference Sans Serif", this.Font.Size, this.Font.Style);
            if (this.DropDownStyle == RadDropDownStyle.DropDownList)
            {
                this.DropDownStyle = RadDropDownStyle.DropDown;
                this.ComboBoxElement.TextBoxElement.TextBoxItem.ReadOnly = true;
            }
        }

        /// <summary>
        /// do no action while no drop down
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (IsDroppedDown)
            {
                base.OnMouseWheel(e);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (RadItem ri in base.Items)
                {
                    RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                    if (chk != null)
                    {
                        chk.MouseUp -= new MouseEventHandler(chk_MouseUp);
                        chk.MouseDown -= new MouseEventHandler(chk_MouseDown);
                        chk.ToggleStateChanged -= new StateChangedEventHandler(chk_ToggleStateChanged);
                        chk.ToggleStateChanging -= new StateChangingEventHandler(chk_ToggleStateChanging);
                        (chk.Children[3] as RadCheckBoxElement).MouseUp -= new MouseEventHandler(chk_MouseUp);
                        (chk.Children[3] as RadCheckBoxElement).MouseDown -= new MouseEventHandler(chk_MouseDown);
                        chk.MouseEnter -= new EventHandler(chk_MouseEnter);
                    }
                }
                this.ComboBoxElement.ArrowButton.MouseDown -= new MouseEventHandler(ArrowButton_MouseDown);
                this.ComboBoxElement.RadPropertyChanging -= new RadPropertyChangingEventHandler(ComboBoxElement_RadPropertyChanging);
                this.ComboBoxElement.TextBoxElement.TextBoxItem.RadPropertyChanging -= new RadPropertyChangingEventHandler(ComboBoxElement_RadPropertyChanging);
                if (_timer != null)
                {
                    _timer.Stop();
                    _timer.Tick -= new EventHandler(_timer_Tick);
                    _timer.Dispose();
                    _timer = null;
                }
                //if (this.DataSource == null)
                //    base.Items.Clear();

                //System.Diagnostics.Debug.WriteLine("CSCheckComboBox_Dispose");
            }

            base.Dispose(disposing);
        }

        protected override void OnDropDownClosing(RadPopupClosingEventArgs e)
        {
            if ((e.CloseReason == RadPopupCloseReason.Mouse ||
                e.CloseReason == RadPopupCloseReason.CloseCalled) &&
                this.ComboBoxElement.ListBoxElement.IsMouseOverElement || _bKeyBoardSelected)
            {
                e.Cancel = true;
            }
            isChkRightClicked = false;
            _bKeyBoardSelected = false;
            base.OnDropDownClosing(e);
        }


        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);

            /* the following will cause text lost
            this.Text = string.Empty;
            foreach (RadCheckComboBoxItem item in CheckedItems)
            {
                this.Text += item.Text + _seperator;
            }
            */
            this.Text = this.Text.TrimEnd(_seperator);
            if (IsDroppedDown)
            {
                CloseDropDown();
            }
        }

        private void CSCheckComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!_bFindByPY)
            {
                return;
            }

            if (DateTime.Now > _tmKeyPress.AddSeconds(inputTime))
            {
                _strInputCode = _strPrevInput = string.Empty;
            }

            _strInputCode += e.KeyChar.ToString().ToUpper();

            _tmKeyPress = DateTime.Now;

            if (e.KeyChar == (char)Keys.Space && this.SelectedIndex > -1 && this.IsDroppedDown)
            {
                _bKeyBoardSelected = true;
                RadCheckComboBoxItem currentItem = this.Items[this.SelectedIndex] as RadCheckComboBoxItem;
                currentItem.IsChecked = !currentItem.IsChecked;
                this.ComboBoxElement.ListBoxElement.SelectedItem = currentItem;//need select it again
            }
            e.Handled = true;
        }

        private void chk_MouseDown(object sender, MouseEventArgs e)
        {
            isChkRightClicked = false;

            _dtLastClicked = System.DateTime.Now;
        }

        private void chk_MouseUp(object sender, MouseEventArgs e)
        {
            _listItemClicked = true;
            if (e.Button == MouseButtons.Right)
            {
                bool bNotChecked = true;
                foreach (RadItem ri in base.Items)
                {
                    RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                    if (chk != null)
                    {
                        if (chk.IsChecked == true)
                        {
                            bNotChecked = false;
                            break;
                        }
                    }
                }

                CheckAll(bNotChecked);
                if (OnCheckedChanged != null) OnCheckedChanged(this);
                isChkRightClicked = true;
            }
            else if (e.Button == MouseButtons.Left && sender is RadCheckComboBoxItem)
            {
                (sender as RadCheckComboBoxItem).IsChecked = !(sender as RadCheckComboBoxItem).IsChecked;
            }
            _listItemClicked = false;
            _dtLastClicked = System.DateTime.Now;
        }

        private void chk_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            if (isChkRightClicked && IsDroppedDown)
                args.Canceled = true;
        }

        private void chk_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (_bCheckAll)//check all no need to set text one by one
            {
                return;
            }
            OnItemChecked(sender);
            if (OnCheckedChanged != null) OnCheckedChanged(this);

            Form owner = this.FindForm();
            if (owner != null)
            {
                owner.Focus();
            }
        }

        //void chk_RadPropertyChanging(object sender, RadPropertyChangingEventArgs args)
        //{
        //    RadCheckComboBoxItem chkcbx = sender as RadCheckComboBoxItem;
        //    if (chkcbx == null)
        //        return;

        //    System.Diagnostics.Debug.WriteLine(
        //        "chk_RadPropertyChanging,"
        //        + chkcbx.Text + ","
        //        + args.Property.Name.ToString() + ","
        //        + args.OldValue.ToString() + ","
        //        + args.NewValue.ToString() + ","
        //        + System.DateTime.Now.ToString("mm:ss fff"));
        //}

        void chk_MouseEnter(object sender, EventArgs e)
        {
            setSelectedColor(sender as RadItem);
            //System.Diagnostics.Debug.WriteLine("chk_MouseEnter");
        }

        protected virtual void OnItemChecked(object sender)
        {

            _listItemClicked = true;
            _strInputCode = _strPrevInput = string.Empty;

            _checkedNames = string.Empty;
            string txt = string.Empty;

            foreach (RadItem ri in base.Items)
            {
                RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                if (chk != null &&
                    chk.IsChecked &&
                    chk.Text != null && chk.Text != string.Empty)
                {
                    _checkedNames += ri.Name + _seperator;
                    txt += ri.Text + _seperator;
                }
            }

            _checkedNames = _checkedNames.Trim(_seperator);
            this.Text = txt.Trim(_seperator);
            _listItemClicked = false;
        }

        protected virtual void CheckAll(bool bCheckAll)
        {
            _bCheckAll = true;
            _bCheckAllorUncheckAll = bCheckAll;
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
                    }
                }

                SetChecked(allNames.Trim(new char[] { _seperator, ' ' }));
                _checkedNames = allNames.Trim(new char[] { _seperator, ' ' });
                this.Text = allText.Trim(new char[] { _seperator, ' ' });
            }
            else
            {
                SetChecked(string.Empty);
                _checkedNames = string.Empty;
                this.Text = "";
            }
            _bCheckAll = false;
        }

        protected virtual void SetChecked(string checkedNames)
        {

            string[] keys = checkedNames.Split(_seperator);

            System.Collections.Specialized.StringDictionary maps = new System.Collections.Specialized.StringDictionary();

            foreach (string key in keys)
            {
                if (!maps.ContainsKey(key))
                {
                    maps.Add(key, string.Empty);
                }
            }

            _checkedNames = string.Empty;

            foreach (RadItem ri in base.Items)
            {
                RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                if (chk != null)
                {
                    // case insensitive
                    chk.IsChecked = maps.ContainsKey(chk.Name) ? true : false;
                }
            }

            _checkedNames = string.Empty;

            foreach (RadItem ri in base.Items)
            {
                RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                if (chk != null && chk.IsChecked)
                {
                    _checkedNames += chk.Name + _seperator;
                }
            }

            _checkedNames = _checkedNames.Trim(new char[] { _seperator, ' ' });
        }

        private RadElement FindFirstElementByType(RadElement rItem, System.Type type)
        {
            //System.Diagnostics.Debug.WriteLine(rItem.ToString());
            if (rItem == null)
                return null;

            if (type == rItem.GetType())
                return rItem;

            RadElement ret = null;
            foreach (RadElement subElement in rItem.Children)
            {
                ret = FindFirstElementByType(subElement, type);
                if (ret != null)
                    return ret;
            }

            return null;
        }

        private Object FindFirstObjectByType(RadElement rItem, System.Type type)
        {
            //System.Diagnostics.Debug.WriteLine(rItem.ToString());
            if (rItem == null)
                return null;

            if (type == rItem.GetType())
                return rItem;

            Object ret = null;
            foreach (Object subElement in rItem.Children)
            {
                ret = FindFirstObjectByType(subElement as RadElement, type);
                if (ret != null)
                    return ret;
            }

            return null;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (!IsDroppedDown)
            {
                return;
            }

            if (DateTime.Now > _tmKeyPress.AddMilliseconds(500) && _strInputCode.Length > _strPrevInput.Length)
            {
                if (_nCurrentIndex < 0 || _nCurrentIndex >= this.Items.Count) _nCurrentIndex = 0;

                string firstChar = "";

                for (int i = 0; i < this.Items.Count; i++)
                {
                    RadItem ri = this.Items[(_nCurrentIndex + i + 1) % this.Items.Count];

                    RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                    if (chk == null)
                        continue;

                    firstChar = FirstPYManager.Instance.GetPYFirstCharacter(ri.Text).ToUpper();

                    if (firstChar.StartsWith(_strInputCode))
                    {
                        _nCurrentIndex = (_nCurrentIndex + i + 1) % this.Items.Count;

                        //RadComboBoxElement element = (RadComboBoxElement)base.RootElement.Children[0];
                        this.ComboBoxElement.ListBoxElement.ClearSelected();
                        this.ComboBoxElement.ListBoxElement.ScrollElementIntoView(chk);
                        this.ComboBoxElement.ListBoxElement.SelectedItem = ri;

                        setSelectedColor(ri);

                        System.Diagnostics.Debug.WriteLine("Selected, IDX=" + i.ToString()
                            + ",SelectedIndex=" + this.SelectedIndex.ToString()
                            + ",Input=" + _strInputCode
                            + ",Text=" + ri.Text
                            + ",firstPY=" + firstChar);

                        break;
                    }
                }

                _strPrevInput = _strInputCode;
            }
        }

        private void setSelectedColor(RadItem selectedItem)
        {
            foreach (RadItem ri in this.ComboBoxElement.ListBoxElement.Items)
            {
                if (ri == selectedItem)
                {
                    FillPrimitive fp = ri.Children[0] as FillPrimitive;
                    fp.Visibility = ElementVisibility.Visible;
                    fp.GradientStyle = GradientStyles.Linear;
                    //fp.BackColor = Color.Orange;
                    fp.BackColor = Color.FromArgb(245, 201, 154);
                    fp.BackColor2 = Color.FromArgb(250, 174, 96);
                    fp.BackColor3 = Color.FromArgb(248, 142, 42);
                    fp.BackColor4 = Color.FromArgb(250, 193, 101);

                    BorderPrimitive bp = ri.Children[1] as BorderPrimitive;
                    bp.Visibility = ElementVisibility.Hidden;

                    TextPrimitive tp = ri.Children[2].Children[1].Children[0] as TextPrimitive;
                    tp.ForeColor = Color.Black;
                }
                else
                {
                    FillPrimitive fp = ri.Children[0] as FillPrimitive;
                    //fp.BackColor = Color.White;
                    fp.Visibility = ElementVisibility.Hidden;

                    BorderPrimitive bp = ri.Children[1] as BorderPrimitive;
                    bp.Visibility = ElementVisibility.Hidden;

                    TextPrimitive tp = ri.Children[2].Children[1].Children[0] as TextPrimitive;
                    tp.ForeColor = Color.Black;
                }
            }
        }

        private void afterSelected()
        {
            string ret = "";

            foreach (RadItem ri in this.Items)
            {
                RadCheckComboBoxItem chkcbx = ri as RadCheckComboBoxItem;
                if (chkcbx == null)
                    return;

                FillPrimitive fp = ri.Children[0] as FillPrimitive;
                TextPrimitive tp = ri.Children[2].Children[1].Children[0] as TextPrimitive;

                ret += chkcbx.Text
                    //+ ",Active=" + chkcbx.Active 
                    //+ ",IsMouseOver=" + chkcbx.IsMouseOver 
                    //+ ",BackColor=" + chkcbx.BackColor.ToString()
                    + ",FillPrimitive.BackColor=" + fp.BackColor.ToString()
                    + ",FillPrimitive.Visibility=" + fp.Visibility.ToString()
                    + ",FillPrimitive.Bounds=" + fp.Bounds.ToString()
                    //+ ",FillPrimitive.ForeColor=" + fp.ForeColor.ToString()
                    //+ ",TextPrimitive.BackColor=" + tp.BackColor.ToString()
                    + ",TextPrimitive.ForeColor=" + tp.ForeColor.ToString()
                    + ";\r\n";
            }

            System.Diagnostics.Debug.WriteLine(ret);
        }

        /// <summary>
        /// Prepare the pinyin cache
        /// EK_HI00114201
        /// </summary>
        public virtual void CompleteFillData()
        {
            _bFindByPY = true;
            //System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(threadPingYinParser), this);
            if (Items.Count > 0)
            {
                RadItem[] radItemArray;
                radItemArray = Items.ToArray();
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(threadPingYinParser), radItemArray);
                //System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(threadPingYinParser));
                //thread.Start(radItemArray);
            }
        }

        private static void threadPingYinParser(object obj)
        {
            RadItem[] radItemArray = obj as RadItem[];
            if (radItemArray == null)
            {
                System.Diagnostics.Debug.WriteLine("Failed to threadPingYinParser because of null parameter!");
                return;
            }

            foreach (RadItem ri in radItemArray)
            {
                if (ri == null || string.IsNullOrEmpty(ri.Text))
                    continue;

                string key = ri.Text.ToUpper().Trim();

                string py = FirstPYManager.Instance.GetPYFirstCharacter(key);
            }
        }

        private void ArrowButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Items.Count > 0)
            {
                int maxWidth = this.Width;
                foreach (RadItem item in this.Items)
                {
                    Graphics g = this.CreateGraphics();
                    SizeF sf = g.MeasureString(item.Text, item.Font);
                    if ((int)sf.Width > maxWidth)
                    {
                        maxWidth = (int)sf.Width;
                    }
                }
                this.DropDownWidth = maxWidth + 40;
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            setSelectedColor(this.SelectedItem as RadItem);

            base.OnSelectedIndexChanged(e);
        }

        #endregion
    }

    /// <summary>
    /// Note:
    /// 1> DO NOT add items by "RadCheckComboBox.Items.Add()", instead using AddItem() function.
    /// 2> DO NOT contain comma in the name and text string of items.
    /// 3> DO NOT use the data binding.
    /// 
    /// Example:
    /// // add some check box items
    /// _radChkCbx.AddItem("0", "A");
    /// _radChkCbx.AddItem("1", "B");
    /// _radChkCbx.AddItem("2", "C");
    /// _radChkCbx.AddItem("3", "D");
    /// 
    /// // set the checked items
    /// _radChkCbx.CheckedNames = "1,3";
    /// 
    /// // get the checked items
    /// string checkedValues = _radChkCbx.CheckedNames;
    /// 
    /// // get the checked text
    /// string checkedText = _radChkCbx.Text;
    /// </summary>
    public class CSCheckComboBoxElement : RadComboBoxElement
    {
        System.DateTime _dtLastClicked = System.DateTime.Now;
        bool isChkRightClicked = false;
        private string _checkedNames = string.Empty;

        public CSCheckComboBoxElement()
        {
            //RadElement rad = FindFirstElementByType(this.RootElement, typeof(Telerik.WinControls.UI.RadTextBoxElement));
            //BorderPrimitive border = FindFirstObjectByType(rad, typeof(Telerik.WinControls.Primitives.BorderPrimitive)) as BorderPrimitive;
            //if (border != null)
            //{
            //    border.Width = 0;
            //    border.TopColor = border.LeftColor = border.RightColor = border.BottomColor = Color.Transparent;
            //}
        }

        /// <summary>
        /// The comma is regarded as the separator keyword, 
        /// so name and txt should not contain the comma.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="txt"></param>
        public void AddItem(string name, string txt)
        {
            System.Diagnostics.Debug.Assert(
                name != null && txt != null &&
                name.IndexOf(',') < 0 && txt.IndexOf(',') < 0,
                "Please do not use comma!");

            RadCheckComboBoxItem chk = new RadCheckComboBoxItem(txt);
            chk.Name = name;
            chk.Text = txt;
            chk.MouseUp += new MouseEventHandler(chk_MouseUp);
            chk.MouseDown += new MouseEventHandler(chk_MouseDown);
            chk.ToggleStateChanged += new StateChangedEventHandler(chk_ToggleStateChanged);
            chk.ToggleStateChanging += new StateChangingEventHandler(chk_ToggleStateChanging);
            base.Items.Add(chk);
        }

        public void AddItem(string text)
        {
            AddItem(text, text);
        }

        public void SetAllItemsState(bool isChecked)
        {
            CheckAll(isChecked);
        }

        public string CheckedNames
        {
            get { return _checkedNames; }
            set { SetChecked(value); }
        }

        public List<RadCheckComboBoxItem> CheckedItems
        {
            get
            {
                List<RadCheckComboBoxItem> list = new List<RadCheckComboBoxItem>();
                foreach (RadCheckComboBoxItem item in this.Items)
                {
                    if (item.IsChecked)
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
        }

        public delegate void CheckedChangedHandler(object sender);

        public event CheckedChangedHandler OnCheckedChanged;

        #region private functions
        private void chk_MouseDown(object sender, MouseEventArgs e)
        {
            isChkRightClicked = false;

            _dtLastClicked = System.DateTime.Now;
        }

        private void chk_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                bool bNotChecked = true;
                foreach (RadItem ri in base.Items)
                {
                    RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                    if (chk != null)
                    {
                        if (chk.IsChecked)
                        {
                            bNotChecked = false;
                            break;
                        }
                    }
                }

                CheckAll(bNotChecked);

                isChkRightClicked = true;
            }

            _dtLastClicked = System.DateTime.Now;
        }

        private void chk_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            if (isChkRightClicked && IsDroppedDown)
                args.Canceled = true;
        }

        private void chk_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            OnItemChecked();

            if (OnCheckedChanged != null) OnCheckedChanged(this);
        }

        private void OnItemChecked()
        {

            _checkedNames = string.Empty;
            string txt = string.Empty;

            foreach (RadItem ri in base.Items)
            {
                RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                if (chk != null &&
                    chk.IsChecked &&
                    chk.Text != null && chk.Text != string.Empty)
                {
                    _checkedNames += ri.Text + ",";
                    txt += ri.Text + ",";
                }
            }

            _checkedNames = _checkedNames.Trim(",".ToCharArray());
            this.Text = txt.Trim(",".ToCharArray());
        }

        private void CheckAll(bool bCheckAll)
        {
            if (bCheckAll)
            {
                string allNames = string.Empty;

                foreach (RadItem ri in base.Items)
                {
                    RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                    if (chk != null)
                    {
                        allNames += chk.Name + ",";
                    }
                }

                SetChecked(allNames.Trim(", ".ToCharArray()));
            }
            else
            {
                SetChecked(string.Empty);
            }
        }

        private void SetChecked(string checkedNames)
        {
            string[] keys = checkedNames.Split(new char[] { ',' });

            System.Collections.Specialized.StringDictionary maps = new System.Collections.Specialized.StringDictionary();

            foreach (string key in keys)
            {
                if (!maps.ContainsKey(key))
                {
                    maps.Add(key, string.Empty);
                }
            }

            foreach (RadItem ri in base.Items)
            {
                RadCheckComboBoxItem chk = ri as RadCheckComboBoxItem;
                if (chk != null)
                {
                    // case insensitive
                    chk.IsChecked = maps.ContainsKey(chk.Name);
                }
            }
        }

        private RadElement FindFirstElementByType(RadElement rItem, System.Type type)
        {
            //System.Diagnostics.Debug.WriteLine(rItem.ToString());
            if (rItem == null)
                return null;

            if (type == rItem.GetType())
                return rItem;

            RadElement ret = null;
            foreach (RadElement subElement in rItem.Children)
            {
                ret = FindFirstElementByType(subElement, type);
                if (ret != null)
                    return ret;
            }

            return null;
        }

        private Object FindFirstObjectByType(RadElement rItem, System.Type type)
        {
            //System.Diagnostics.Debug.WriteLine(rItem.ToString());
            if (rItem == null)
                return null;

            if (type == rItem.GetType())
                return rItem;

            Object ret = null;
            foreach (Object subElement in rItem.Children)
            {
                ret = FindFirstObjectByType(subElement as RadElement, type);
                if (ret != null)
                    return ret;
            }

            return null;
        }

        #endregion
    }

    public class RadCheckComboBoxItem : RadComboBoxItem
    {
        public static RadProperty propertyActive = RadProperty.Register(
            "Active",
            typeof(bool),
            typeof(RadCheckComboBoxItem),
            new RadElementPropertyMetadata(false, ElementPropertyOptions.AffectsDisplay));

        private RadCheckBoxElement checkBoxElement = new RadCheckBoxElement();

        public bool Active
        {
            get
            {
                return (bool)this.GetValue(propertyActive);
            }
            set
            {
                this.SetValue(propertyActive, value);
            }
        }

        public bool IsChecked
        {
            get
            {
                return this.checkBoxElement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
            }
            set
            {
                ToggleState toggleState = value ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
                if (this.checkBoxElement.ToggleState != toggleState)
                {
                    this.checkBoxElement.ToggleState = toggleState;
                    //StateChangedEventArgs args = new StateChangedEventArgs(this.checkBoxElement.ToggleState);
                    //checkBoxElement_ToggleStateChanged(this, args);
                }
            }
        }

        public ToggleState ToggleState
        {
            get { return IsChecked ? ToggleState.On : ToggleState.Off; }
            set
            {
                IsChecked = value == ToggleState.On;
            }
        }

        public RadCheckComboBoxItem(string text)
            : base(text)
        {

        }

        public RadCheckComboBoxItem(string text, MouseEventHandler OnClick)
            : base(text, OnClick)
        {

        }

        public event StateChangedEventHandler ToggleStateChanged;
        public event StateChangingEventHandler ToggleStateChanging;


        protected override void CreateChildElements()
        {
            base.CreateChildElements();

            this.Children[2].Children[1].Margin = new Padding(15, 0, 0, 0);
            checkBoxElement.StretchHorizontally = false;
            checkBoxElement.StretchVertically = false;
            checkBoxElement.ToggleStateChanged += new StateChangedEventHandler(checkBoxElement_ToggleStateChanged);
            checkBoxElement.ToggleStateChanging += new StateChangingEventHandler(checkBoxElement_ToggleStateChanging);
            this.Children.Add(checkBoxElement);
        }

        void checkBoxElement_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            if (ToggleStateChanging != null)
            {
                ToggleStateChanging(this, args);
            }
        }

        void checkBoxElement_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (ToggleStateChanged != null)
            {

                ToggleStateChanged(this, args);
            }
        }


    }
}