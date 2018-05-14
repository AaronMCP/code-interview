using System;
using System.Collections.Generic;
using System.Text;
using Telerik.WinControls.UI;
using Telerik.WinControls.Primitives;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
//using Kodak.GCRIS.Client.FrameWork.Profile;

namespace Hys.CommonControls
{
    public class CSComboBoxExx : CSComboBox
    {

        System.DateTime _dtLastClicked = System.DateTime.Now;
        System.DateTime _tmKeyPress = System.DateTime.Now;
        private string _checkedNames = string.Empty;
        int inputTime = 2;
        Timer _timer = null;
        string _strInputCode = string.Empty;
        string _strPrevInput = string.Empty;
        bool _bFindByPY = true;
        int _nCurrentIndex = 0;

        public CSComboBoxExx()
        {


            this.ComboBoxElement.TextBoxElement.TextBoxItem.ReadOnly = true;
            this.ComboBoxElement.ArrowButton.MouseDown += new MouseEventHandler(ArrowButton_MouseDown);
            AutoCompleteMode = AutoCompleteMode.Suggest;

            this.ComboBoxElement.KeyPress += new KeyPressEventHandler(CSComboBoxExx_KeyPress);


         
            //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"Software\Carestream\RIS GC\");
            //string time = Convert.ToString(key.GetValue("InputInterval"));
            //key.Close();

            //if (!Int32.TryParse(time, out inputTime))
            //    inputTime = 2;
            //inputTime = iTime > 0 ? iTime : inputTime;


            _timer = new Timer();
            _timer.Interval = 200;
            _timer.Start();
            _timer.Tick += new EventHandler(_timer_Tick);
        }

        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadComboBox"; }
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

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            // RadComboBox dose not implement the OnSelectedValueChanged Event,
            // so we do it mannually.
            OnSelectedValueChanged(e);

            base.OnSelectedIndexChanged(e);

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
                this.DropDownWidth = maxWidth;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.Font = new System.Drawing.Font("MS Reference Sans Serif", this.Font.Size, this.Font.Style);
            if (this.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                this.DropDownStyle = ComboBoxStyle.DropDown;
                this.ComboBoxElement.TextBoxElement.TextBoxItem.ReadOnly = true;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (IsDroppedDown)
            {
                base.OnMouseWheel(e);
            }
        }


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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                this.ComboBoxElement.ArrowButton.MouseDown -= new MouseEventHandler(ArrowButton_MouseDown);
                if (_timer != null)
                {
                    _timer.Stop();
                    _timer.Tick -= new EventHandler(_timer_Tick);
                    _timer.Dispose();
                    _timer = null;
                }
                //if (this.DataSource == null)
                //    base.Items.Clear();
            }

            base.Dispose(disposing);
        }

        protected override void OnDropDownClosing(RadPopupClosingEventArgs e)
        {
            if (_dtLastClicked.AddMilliseconds(500) > System.DateTime.Now)
            {
                e.Cancel = true;
            }

            if (Parent is CSGridView && base.ComboBoxElement.ArrowButton.IsMouseDown)
            {
                e.Cancel = true;
            }

            base.OnDropDownClosing(e);
        }
        private void CSComboBoxExx_KeyPress(object sender, KeyPressEventArgs e)
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

            e.Handled = true;
        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now > _tmKeyPress.AddMilliseconds(500) && _strInputCode.Length > _strPrevInput.Length)
            {
                if (_nCurrentIndex < 0 || _nCurrentIndex >= this.Items.Count) _nCurrentIndex = 0;

                string firstChar = "";

                for (int i = 0; i < this.Items.Count; i++)
                {
                    RadItem ri = this.Items[(_nCurrentIndex + i + 1) % this.Items.Count];

                    if (ri == null)
                        continue;

                    firstChar = FirstPYManager.Instance.GetPYFirstCharacter(ri.Text).ToUpper();

                    if (firstChar.StartsWith(_strInputCode))
                    {
                        _nCurrentIndex = (_nCurrentIndex + i + 1) % this.Items.Count;

                        //RadComboBoxElement element = (RadComboBoxElement)base.RootElement.Children[0];
                        this.ComboBoxElement.ListBoxElement.ClearSelected();
                        this.ComboBoxElement.ListBoxElement.ScrollElementIntoView(ri);
                        //if (!this.IsDroppedDown)
                        {
                            this.ComboBoxElement.ListBoxElement.SelectedItem = ri;
                        }

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
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(threadPingYinParser), this);
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

    }

    public class CSComboBoxExxElement : RadComboBoxElement
    {

        System.DateTime _dtLastClicked1 = System.DateTime.Now;
        System.DateTime _tmKeyPress1 = System.DateTime.Now;
        int inputTime = 2;
        Timer _timer = null;
        string _strInputCode = string.Empty;
        string _strPrevInput = string.Empty;
        bool _bFindByPY = true;
        int _nCurrentIndex = 0;

        private bool _allowEscapeKey = true;
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowEscapeKey
        {
            get { return _allowEscapeKey; }
            set { _allowEscapeKey = value; }
        }

        protected override Type ThemeEffectiveType
        {
            get { return typeof(RadComboBoxElement); }
        }


        public CSComboBoxExxElement()
        {


            base.TextBoxElement.TextBoxItem.ReadOnly = true;
            this.ArrowButton.MouseDown += new MouseEventHandler(ArrowButton_MouseDown);

            AutoCompleteMode = AutoCompleteMode.Suggest;

            this.KeyPress += new KeyPressEventHandler(CSComboBoxExx_KeyPress);


            //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"Software\Carestream\RIS GC\");
            //string time = key.GetValue("InputInterval").ToString();
            //key.Close();

            //int iTime = System.Convert.ToInt32(time);
            //inputTime = iTime > 0 ? iTime : inputTime;


            _timer = new Timer();
            _timer.Interval = 200;
            _timer.Start();
            _timer.Tick += new EventHandler(_timer_Tick);
        }

        public override object Value
        {
            get
            {
                return this.SelectedValue;
            }
            set
            {
                this.SelectedValue = value;
            }
        }

        private void ArrowButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.TextBoxElement.TextBoxItem.HostedControl != null && this.TextBoxElement.TextBoxItem.HostedControl is TextBox)
            {
                (this.TextBoxElement.TextBoxItem.HostedControl as TextBox).Select();
            }
            else
            {
                this.TextBoxElement.TextBoxItem.Select();
            }

            #region backup
            /*
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
                this.DropDownWidth = maxWidth;
            }
            */
            #endregion
        }

        /*
protected override void OnCreateControl()
{
base.OnCreateControl();

this.Font = new System.Drawing.Font("MS Reference Sans Serif", this.Font.Size, this.Font.Style);
if (this.DropDownStyle == ComboBoxStyle.DropDownList)
{
this.DropDownStyle = ComboBoxStyle.DropDown;
this.ComboBoxElement.TextBoxElement.TextBoxItem.ReadOnly = true;
}

}
 */

        /*
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (IsDroppedDown)
            {
                base.OnMouseWheel(e);
            }
        }
        */

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


        protected override void DisposeUnmanagedResources()
        {


            base.ArrowButton.MouseDown -= new MouseEventHandler(ArrowButton_MouseDown);
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Tick -= new EventHandler(_timer_Tick);
                _timer.Dispose();
                _timer = null;
            }
            //if (this.DataSource == null)
            //    base.Items.Clear();
            base.DisposeUnmanagedResources();
        }

        /*
    protected override void OnDropDownClosing(RadPopupClosingEventArgs e)
    {
        if (_dtLastClicked1.AddMilliseconds(500) > System.DateTime.Now)
        {
            e.Cancel = true;
        }

        base.OnDropDownClosing(e);
    }
         */
        private void CSComboBoxExx_KeyPress(object sender, KeyPressEventArgs e)
        {
            Debug.WriteLine("CSComboBoxExx Focused:" + this.IsFocused.ToString());
            Debug.WriteLine("CSComboBoxExx_KeyPress:char:" + e.KeyChar.ToString());
            if (_allowEscapeKey && this.TextBoxElement.TextBoxItem.ReadOnly)
            {
                Keys pressedKey = (Keys)e.KeyChar;
                if (pressedKey == Keys.Escape)
                {
                    this.SelectedIndex = -1;
                    this.Text = string.Empty;
                }
            }

            if (!_bFindByPY)
            {
                return;
            }

            if (DateTime.Now > _tmKeyPress1.AddSeconds(inputTime))
            {
                _strInputCode = _strPrevInput = string.Empty;
            }

            _strInputCode += e.KeyChar.ToString().ToUpper();

            _tmKeyPress1 = DateTime.Now;

            e.Handled = true;
        }


        private void _timer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now > _tmKeyPress1.AddMilliseconds(500) && _strInputCode.Length > _strPrevInput.Length)
            {
                Debug.WriteLine("_timer_Tick come(DateTime.Now > _tmKeyPress1.AddMilliseconds(500)):" + DateTime.Now.ToLongTimeString());
                if (_nCurrentIndex < 0 || _nCurrentIndex >= this.Items.Count) _nCurrentIndex = 0;

                string firstChar = "";

                for (int i = 0; i < this.Items.Count; i++)
                {
                    RadItem ri = this.Items[(_nCurrentIndex + i + 1) % this.Items.Count];

                    if (ri == null)
                        continue;

                    firstChar = FirstPYManager.Instance.GetPYFirstCharacter(ri.Text).ToUpper();

                    if (firstChar.StartsWith(_strInputCode))
                    {
                        _nCurrentIndex = (_nCurrentIndex + i + 1) % this.Items.Count;

                        //RadComboBoxElement element = (RadComboBoxElement)base.RootElement.Children[0];
                        this.ListBoxElement.ClearSelected();
                        this.ListBoxElement.ScrollElementIntoView(ri);
                        //if (!this.IsDroppedDown)
                        {
                            this.ListBoxElement.SelectedItem = ri;

                        }
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
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(threadPingYinParser));
                thread.Start(radItemArray);
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

    }

}
