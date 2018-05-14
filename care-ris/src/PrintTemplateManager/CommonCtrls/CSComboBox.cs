using System;
using System.Collections.Generic;
using System.Text;
using Telerik.WinControls.UI;
using Telerik.WinControls.Primitives;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Drawing;
using System.ComponentModel;

namespace Hys.CommonControls
{
    public class CSComboBox : RadComboBox
    {
        public CSComboBox()
        {
            this.ComboBoxElement.ArrowButton.MouseDown += new MouseEventHandler(ArrowButton_MouseDown);
            this.ComboBoxElement.TextBoxElement.KeyPress += new KeyPressEventHandler(TextBoxElement_KeyPress);
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

        private System.Windows.Forms.ComboBoxStyle dropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
        [DefaultValue(System.Windows.Forms.ComboBoxStyle.DropDown)]
        new public System.Windows.Forms.ComboBoxStyle DropDownStyle
        {
            get
            {
                return dropDownStyle;
            }
            set
            {
                if (value == ComboBoxStyle.Simple)
                {
                    dropDownStyle = ComboBoxStyle.DropDown;
                    base.DropDownStyle = RadDropDownStyle.DropDown;
                }
                else if(value == ComboBoxStyle.DropDownList )
                {
                    if (!IsDesignMode)
                    {
                        dropDownStyle = ComboBoxStyle.DropDown;
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

        private bool _allowEscapeKey = true;
        private bool _isInEdit = false;
        [DefaultValue(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowEscapeKey
        {
            get { return _allowEscapeKey; }
            set { _allowEscapeKey = value; }
        }

        public int Add(string text)
        {
            RadComboBoxItem cbxItem = new RadComboBoxItem(text);

            return this.Items.Add(cbxItem);
        }

        public int Add(string text, string szName)
        {
            RadComboBoxItem cbxItem = new RadComboBoxItem(text);
            cbxItem.Name = szName;

            return this.Items.Add(cbxItem);
            
        }

        public int Add(string text, string szName,string tooltip)
        {
            RadComboBoxItem cbxItem = new RadComboBoxItem(text);
            cbxItem.Name = szName;
            cbxItem.ToolTipText = tooltip;

            return this.Items.Add(cbxItem);

        }


        public bool Contains(string text)
        {
            RadItemCollection.RadItemEnumerator iterator = this.Items.GetEnumerator();
            while (iterator.MoveNext())
            {
                if ((iterator.Current as RadComboBoxItem).Text == text)
                {
                    return true;
                }
            }

            return false;
        }

        public int FindString(string text)
        {
            int i = 0;
            RadItemCollection.RadItemEnumerator iterator = this.Items.GetEnumerator();
            while (iterator.MoveNext())
            {
                if ((iterator.Current as RadComboBoxItem).Text.ToUpper() == text.ToUpper())
                {
                    return i;
                }

                ++i;
            }

            return -1;
        }

        public void Insert(int idx, string text)
        {
            base.Items.Insert(idx, new RadComboBoxItem(text));
        }       

        /// <summary>
        /// If we do not use data-binding, SelectedValue will be NULL always.
        /// So we implement this property.
        /// </summary>
        public string SelectedName
        {
            get
            {
                if(SelectedIndex >=0 && SelectedIndex < Items.Count)
                {
                    return Items[SelectedIndex].Name;
                }

                return string.Empty;
            }
            set
            {
                RadItemCollection.RadItemEnumerator iterator = this.Items.GetEnumerator();
                while (iterator.MoveNext())
                {
                    if ((iterator.Current as RadComboBoxItem).Name.ToUpper() == value.ToUpper())
                    {
                        SelectedItem = iterator.Current;
                        return;
                    }
                }
            }
        }

        public new string SelectedText
        {
            get
            {
                if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
                {
                    return Items[SelectedIndex].Text;
                }

                return string.Empty;
            }
            set
            {
                SelectedItem = null;

                RadItemCollection.RadItemEnumerator iterator = this.Items.GetEnumerator();
                while (iterator.MoveNext())
                {
                    if ((iterator.Current as RadComboBoxItem).Text.ToUpper() == value.ToUpper())
                    {
                        SelectedItem = iterator.Current;
                        return;
                    }
                }
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

        private void TextBoxElement_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_allowEscapeKey && this.ComboBoxElement.TextBoxElement.TextBoxItem.ReadOnly)
            {
                Keys pressedKey = (Keys)e.KeyChar;
                if (pressedKey == Keys.Escape)
                {
                    this.SelectedIndex = -1;
                    this.Text = string.Empty;
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            _isInEdit = true;
            base.OnTextChanged(e);
            _isInEdit = false;
        }

        protected override void OnDropDownOpening(CancelEventArgs e)
        {
            if (_isInEdit && !IsDroppedDown)
            {
                _isInEdit = false;
                e.Cancel = true;
            }
            base.OnDropDownOpening(e);
        }

        protected override void OnDropDownClosed(RadPopupClosedEventArgs e)
        {
            _isInEdit = false;
            base.OnDropDownClosed(e);
        }
    }
}
