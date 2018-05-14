using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.Primitives;
using System.ComponentModel;

namespace Hys.CommonControls
{
    public class CSTextBox : RadTextBox
    {
        private Timer _timer = null;
        private Int32 _timerInterval = 0;
        public delegate void MyTick(CSTextBox target, object sender, EventArgs e);
        public MyTick Tick;
        private bool _enterKeyActive = false;
        [DefaultValue(false)]
        [Description("If it is true and user push enter key, it will be processed!")]
        public bool EnterKeyActive
        {
            get
            {
                return _enterKeyActive;
            }
            set
            {
                _enterKeyActive = value;
            }
        }

        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadTextBox"; }
        }

        public System.Windows.Forms.BorderStyle BorderStyle
        {
            get { return BorderStyle.FixedSingle; }
            set { }
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                return (this.TextBoxElement.Children[0] as RadTextBoxItem).BackColor;
            }
            set
            {
                (this.TextBoxElement.Children[0] as RadTextBoxItem).BackColor = value;
                (this.TextBoxElement.Children[1] as FillPrimitive).BackColor = value;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && !_enterKeyActive)
            {
                return false;
                //this.OnKeyDown(new KeyEventArgs(keyData));

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.Font = new System.Drawing.Font("MS Reference Sans Serif", this.Font.Size, this.Font.Style);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (_timerInterval > 0)
            {
                if (_timer == null)
                {
                    _timer = new Timer();
                    _timer.Interval = _timerInterval;
                    _timer.Tick += new EventHandler(_timer_Tick);
                }
                _timer.Start();
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (_timer != null && _timer.Enabled)
            {
                _timer.Stop();
            }
        }

        public Int32 TimerInterval
        {
            get
            {
                return _timerInterval;
            }
            set
            {
                if (value >= 0)
                {
                    _timerInterval = value;
                    if (_timer != null)
                    {
                        _timer.Interval = _timerInterval;
                    }
                }
            }
        }

        public void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            this.Tick(this, sender, e);
            _timer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_timer != null && _timer.Enabled)
                {
                    _timer.Stop();
                    _timer.Dispose();
                }
                GC.SuppressFinalize(this);
            }
        }

        ~CSTextBox()
        {
            if (_timer != null && _timer.Enabled)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }
    }

    public class CSSpinEditor : RadSpinEditor
    {
        public CSSpinEditor()
        {
            this.Maximum = decimal.MaxValue;
        }

        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadSpinEditor"; }
        }

        public System.Windows.Forms.BorderStyle BorderStyle
        {
            get { return BorderStyle.FixedSingle; }
            set
            {
                ;
            }
        }

        public int MaxLength
        {
            get
            {
                return this.Maximum.ToString().Length;
            }

            set
            {
                this.Maximum = (int)Math.Pow(10, value + 1) - 1;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.Font = new System.Drawing.Font("MS Reference Sans Serif", this.Font.Size, this.Font.Style);
        }
    }

    /// <summary>
    /// TextBox that can receive the integer.
    /// </summary>
    public class CSIntegerTextBox : TextBox, ISupportInitialize
    {
        string _prevTxt = string.Empty;

        public void BeginInit() { }

        public void EndInit() { }

        public override int MaxLength
        {
            get
            {
                return base.MaxLength - 1;
            }
            set
            {
                base.MaxLength = value + 1;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            Int64 itmp;

            if (this.Text == string.Empty)
            {

            }
            else if (Int64.TryParse(this.Text, out itmp))
            {
                _prevTxt = this.Text;
            }
            else
            {
                this.Text = _prevTxt;

                return;
            }

            base.OnTextChanged(e);
        }

        // Not implement for RadTextBox, So we use the standard TextBox.
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            Int64 itmp;

            if (e.KeyChar == (char)Keys.Back)
            {

            }
            else if (this.Text.Trim() != string.Empty && !Int64.TryParse(this.Text, out itmp))
            {
                e.Handled = true;
            }
            else if (this.Text.Length >= MaxLength - 1)
            {
                e.Handled = true;
            }
            else if (e.KeyChar > '9' || e.KeyChar < '0')
            {
                e.Handled = true;
            }

            if (!e.Handled)
                _prevTxt = this.Text;

            base.OnKeyPress(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.Font = new System.Drawing.Font("MS Reference Sans Serif", this.Font.Size, this.Font.Style);
        }
    };

    public class CSNumericTextBox : RadSpinEditor
    {
        public CSNumericTextBox()
        {
            base.DecimalPlaces = 2;
            SpinElement.TextBoxItem.KeyPress += new KeyPressEventHandler(TextBoxItem_KeyPress);
        }

        private void TextBoxItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {

            }
            else if (e.KeyChar > '9' || e.KeyChar < '0')
            {
                e.Handled = true;
            }
        }

        public System.Windows.Forms.BorderStyle BorderStyle
        {
            get { return BorderStyle.FixedSingle; }
            set
            {
                ;
            }
        }

        //public int MaxLength
        //{
        //    get
        //    {
        //        return maxlength;
        //    }

        //    set
        //    {
        //        maxlength = value;
        //        base.Maximum = 10 ^ value - 1;
        //    }
        //}

        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadSpinEditor"; }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.Font = new System.Drawing.Font("MS Reference Sans Serif", this.Font.Size, this.Font.Style);
        }

        private int decimalPlaces = 2;
        [DefaultValue(2)]
        new public int DecimalPlaces
        {
            get
            {
                return decimalPlaces;
            }
            set
            {
                decimalPlaces = value;
                base.DecimalPlaces = value;
            }
        }
    }

    /// <summary>
    /// TextBox that can receive integer or float, 
    ///     derived from System.Windows.Forms.TextBox.
    /// </summary>
    //public class CSNumericTextBox : TextBox, ISupportInitialize
    //{
    //    string _prevTxt = string.Empty;

    //    public void BeginInit() { }

    //    public void EndInit() { }

    //    protected override void OnTextChanged(EventArgs e)
    //    {
    //        int itmp;
    //        float ftmp;

    //        if (this.Text == string.Empty)
    //        {

    //        }
    //        else if (int.TryParse(this.Text, out itmp))
    //        {

    //        }
    //        else if (float.TryParse(this.Text, out ftmp))
    //        {

    //        }
    //        else if (this.Text != ".")
    //        {
    //            this.Text = _prevTxt;

    //            return;
    //        }

    //        _prevTxt = this.Text;

    //        base.OnTextChanged(e);
    //    }

    //    protected override void OnKeyPress(KeyPressEventArgs e)
    //    {
    //        if (e.KeyChar == (char)Keys.Back)
    //        {

    //        }
    //        else if (e.KeyChar == '.')
    //        {
    //            if (this.Text == string.Empty || this.Text.IndexOf('.') >= 0)
    //            {
    //                e.Handled = true;
    //            }
    //        }
    //        else if (e.KeyChar > '9' || e.KeyChar < '0')
    //        {
    //            e.Handled = true;
    //        }

    //        _prevTxt = this.Text;

    //        base.OnKeyPress(e);
    //    }
    //};

    public class CSIntegerTextBoxElement : RadTextBoxElement
    {


        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadTextBoxElement);
            }
        }

        public CSIntegerTextBoxElement()
        {
            this.KeyPress += new KeyPressEventHandler(CSIntegerTextBoxElement_KeyPress);
        }

        protected override void OnTextChanging(Telerik.WinControls.TextChangingEventArgs e)
        {
            int itmp;

            this.Text = this.Text.Trim();

            if (this.Text != string.Empty && !int.TryParse(this.Text, out itmp))
            {
                e.Cancel = true;
            }

            base.OnTextChanging(e);
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.Handled = true;
            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {

            }
            else if (e.KeyChar > '9' || e.KeyChar < '0')
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        void CSIntegerTextBoxElement_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {

            }
            else if (e.KeyChar > '9' || e.KeyChar < '0')
            {
                e.Handled = true;
            }
        }
    }

    public class CSNumericTextBoxElement : RadTextBoxElement
    {
        public CSNumericTextBoxElement()
        {
            this.KeyPress += new KeyPressEventHandler(CSNumericTextBoxElement_KeyPress);
        }

        protected override Type ThemeEffectiveType
        {
            get
            {
                return typeof(RadTextBoxElement);
            }
        }

        protected override void OnTextChanging(Telerik.WinControls.TextChangingEventArgs e)
        {
            float ftmp;

            this.Text = this.Text.Trim();

            if (this.Text != string.Empty && !float.TryParse(this.Text, out ftmp))
            {
                e.Cancel = true;
            }

            base.OnTextChanging(e);
        }

        // Strange bug, OnKeyPress will not be raised, 
        // but its KeyPress event can be raised.
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {

            }
            else if (e.KeyChar == '.')
            {
                if (this.Text == string.Empty || this.Text.IndexOf('.') >= 0)
                {
                    e.Handled = true;
                }
            }
            else if (e.KeyChar > '9' || e.KeyChar < '0')
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        void CSNumericTextBoxElement_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {

            }
            else if (e.KeyChar == '.')
            {
                if (this.Text == string.Empty || this.Text.IndexOf('.') >= 0)
                {
                    e.Handled = true;
                }
            }
            else if (e.KeyChar > '9' || e.KeyChar < '0')
            {
                e.Handled = true;
            }
        }
    }
}
