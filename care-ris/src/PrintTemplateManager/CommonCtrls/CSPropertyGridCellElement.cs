using System;
using System.Collections.Generic;
using System.Text;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using System.Data;
using System.Drawing;
using Telerik.WinControls.Primitives;

namespace Hys.CommonControls
{
    public class CSPropertyGridCellElement : GridDataCellElement
    {
        #region Properties
        public RadElement ActiveControl
        {
            get
            {
                RadElement activeElement = null;
                foreach (RadElement element in this.Children)
                {
                    if (element.Visibility == ElementVisibility.Visible)
                    {
                        activeElement = element;
                        break;
                    }
                }
                return activeElement;
            }
        }
        #endregion

        RadCheckBoxElement ctlChk = null;
        RadComboBoxElement ctlComboBoxElement = null;
        CSCheckComboBoxElement ctlCheckComboBoxElement = null;
        CSComboBoxExxElement ctlComboBoxExxElement = null;
        RadTextBoxElement ctlTextBox = null;
        RadDateTimePickerElement ctlDataTimePickerElement = null;
        CSColorSelectorElement ctlColorSelectorElement = null;
        RadSpinElement ctlSpin = null;
        CSNumericTextBoxElement ctlNumericTextBox = null;
        CSIntegerTextBoxElement ctlIntTextBox = null;

        public CSPropertyGridCellElement(GridViewColumn column, GridRowElement row)
            : base(column, row)
        {
        }

        #region Override Functions
        protected override Type ThemeEffectiveType
        {
            get { return typeof(GridDataCellElement); }
        }

        protected override void CreateChildElements()
        {
            base.CreateChildElements();

            ctlChk = new RadCheckBoxElement();
            ctlChk.ToggleStateChanged += new StateChangedEventHandler(ctlChk_ToggleStateChanged);
            this.Children.Add(ctlChk);

            ctlComboBoxElement = new RadComboBoxElement();
            ctlComboBoxElement.DropDownStyle = RadDropDownStyle.DropDownList;
            ctlComboBoxElement.ValueChanged += new EventHandler(ctlComboBoxElement_ValueChanged);
            this.Children.Add(ctlComboBoxElement);

            ctlCheckComboBoxElement = new CSCheckComboBoxElement();
            ctlCheckComboBoxElement.PopupClosed += new RadPopupClosedEventHandler(ctlCheckComboBoxElement_PopupClosed);
            this.Children.Add(ctlCheckComboBoxElement);

            ctlComboBoxExxElement = new CSComboBoxExxElement();
            ctlComboBoxExxElement.ValueChanged += new EventHandler(ctlComboBoxExxElement_ValueChanged);
            this.Children.Add(ctlComboBoxExxElement);

            ctlTextBox = new RadTextBoxElement();
            ctlTextBox.TextChanged += new EventHandler(ctlTextBox_TextChanged);
            this.Children.Add(ctlTextBox);

            ctlDataTimePickerElement = new RadDateTimePickerElement();
            ctlDataTimePickerElement.ValueChanged += new EventHandler(ctlDataTimePickerElement_ValueChanged);
            this.Children.Add(ctlDataTimePickerElement);

            ctlColorSelectorElement = new CSColorSelectorElement();
            ctlColorSelectorElement.TextChanged += new EventHandler(ctlColorSelectorElement_TextChanged);
            this.Children.Add(ctlColorSelectorElement);


            ctlSpin = new RadSpinElement();
            ctlSpin.MinValue = 0;
            ctlSpin.MaxValue = 100000;
            ctlSpin.TextChanged += new EventHandler(ctlSpin_TextChanged);
            this.Children.Add(ctlSpin);


            ctlNumericTextBox = new CSNumericTextBoxElement();
            ctlNumericTextBox.TextChanged += new EventHandler(ctlNumericTextBox_TextChanged);
            this.Children.Add(ctlNumericTextBox);


            ctlIntTextBox = new CSIntegerTextBoxElement();
            ctlIntTextBox.TextChanged += new EventHandler(ctlIntTextBox_TextChanged);
            this.Children.Add(ctlIntTextBox);



            foreach (RadElement element in this.Children)
            {
                element.Visibility = ElementVisibility.Collapsed;
            }
        }

        public override void SetContent()
        {
            CSDataRow dr = (RowInfo.DataBoundItem as DataRowView).Row as CSDataRow;
            dr.EmbeddedCtlPara.CtlCell = this;
            if (dr == null)
            {
                base.SetContent();
                return;
            }
            EmbedControlType ctlType = (EmbedControlType)dr.Tag;

            switch (ctlType)
            {
                case  EmbedControlType.CheckBox:
                    UpdateVisibility(ctlChk);
                    object value = this.Value;
                    if (value == null || value == DBNull.Value || bool.FalseString.Equals(value.ToString()))
                    {
                        ctlChk.ToggleState = ToggleState.Off;
                    }
                    else
                    {
                        ctlChk.ToggleState = ToggleState.On;
                    }
                    break;
                case  EmbedControlType.CheckComboBox:
                    AddItem2ComboBox(ctlCheckComboBoxElement, dr);
                    UpdateVisibility(ctlCheckComboBoxElement);
                    ctlCheckComboBoxElement.CheckedNames = this.Value.ToString();
                    break;

                case EmbedControlType.CSComboBoxExx:
                    AddItem2ComboBox(ctlComboBoxExxElement, dr);
                    UpdateVisibility(ctlComboBoxExxElement);
                    bool hasValue1 = false;
                    foreach (RadItem item in ctlComboBoxExxElement.Items)
                    {
                        if (item.Text.Equals(this.Value))
                        {
                            ctlComboBoxExxElement.SelectedItem = item;
                            if (ctlComboBoxExxElement.IsPopupOpen)
                            {
                                ctlComboBoxExxElement.ListBoxElement.Focus();
                            }
                            hasValue1 = true;
                            break;
                        }
                    }
                    if (!hasValue1)
                    {
                        ctlComboBoxExxElement.SelectedIndex = -1;
                    }
                    break;

                case  EmbedControlType.ComboBox:
                    AddItem2ComboBox(ctlComboBoxElement, dr);
                    UpdateVisibility(ctlComboBoxElement);
                    bool hasValue = false;
                    foreach (RadItem item in ctlComboBoxElement.Items)
                    {
                        if (item.Text.Equals(this.Value))
                        {
                            ctlComboBoxElement.SelectedItem = item;
                            hasValue = true;
                            break;
                        }
                    }
                    if (!hasValue)
                    {
                        ctlComboBoxElement.SelectedIndex = -1;
                    }
                    break;
                case EmbedControlType.TextBox:
                    UpdateVisibility(ctlTextBox);
                    ctlTextBox.Text = this.Value.ToString();
                    break;
                case EmbedControlType.DateTimePicker:
                    UpdateVisibility(ctlDataTimePickerElement);
                    DateTime dt = DateTime.Now;
                    DateTime.TryParse(this.Value.ToString(), out dt);
                    ctlDataTimePickerElement.Value = dt;
                    break;
                case EmbedControlType.ColorSelector:
                    UpdateVisibility(ctlTextBox);
                    ctlColorSelectorElement.Value = this.Value;
                    break;
                case EmbedControlType.Spin:
                    UpdateVisibility(ctlSpin);
                    decimal result = 0;
                    if (decimal.TryParse(this.Value.ToString(), out result))
                    {
                        ctlSpin.Value = decimal.Parse(this.Value.ToString());
                    }
                    else
                    {
                        ctlSpin.Value = 0;
                    }
                    break;
                case EmbedControlType.CSNumericTextBox:
                    UpdateVisibility(ctlNumericTextBox);
                    ctlNumericTextBox.Text = this.Value.ToString();
                    break;
                case EmbedControlType.CSIntTextBox:
                    UpdateVisibility(ctlIntTextBox);
                    ctlIntTextBox.Text = this.Value.ToString();
                    break;

            }

        }

        #endregion

        #region Private Functions
        private void UpdateVisibility(RadElement visibleElement)
        {
            visibleElement.Visibility = ElementVisibility.Visible;
            foreach (RadElement element in this.Children)
            {
                if (element != visibleElement)
                {
                    element.Visibility = ElementVisibility.Collapsed;
                }
            }
        }

        private void AddItem2ComboBox(RadComboBoxElement cbb, CSDataRow dr)
        {
            DataTable dt = dr.EmbeddedCtlPara.ComBoCtlDataSource;
            if (cbb.GetType().Equals(typeof(RadComboBoxElement)))
            {
                cbb.ValueChanged -= ctlComboBoxElement_ValueChanged;
                cbb.SelectedIndex = -1;
                cbb.DataSource = dt;
                cbb.DisplayMember = dr.EmbeddedCtlPara.DisplayMember;
                cbb.ValueMember = dr.EmbeddedCtlPara.ValueMember;
                cbb.ValueChanged += new EventHandler(ctlComboBoxElement_ValueChanged);
            }
            else if (cbb.GetType().Equals(typeof(CSCheckComboBoxElement)))
            {
                CSCheckComboBoxElement chkcbb = cbb as CSCheckComboBoxElement;
                chkcbb.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    chkcbb.AddItem(row["LocalName"].ToString());
                }
            }
            else if (cbb.GetType().Equals(typeof(CSComboBoxExxElement)))
            {
                cbb.ValueChanged -= ctlComboBoxExxElement_ValueChanged;
                cbb.SelectedIndex = -1;
                cbb.DataSource = dt;
                cbb.DisplayMember = dr.EmbeddedCtlPara.DisplayMember;
                cbb.ValueMember = dr.EmbeddedCtlPara.ValueMember;
                cbb.ValueChanged += ctlComboBoxExxElement_ValueChanged;
                
            }            
        }
        #endregion

        #region Element Event
        private void ctlChk_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            this.Value = ctlChk.ToggleState == ToggleState.On ? true : false;
        }

        private void ctlComboBoxElement_ValueChanged(object sender, EventArgs e)
        {
            if (ctlComboBoxElement.SelectedItem != null)
            {
                this.Value = (ctlComboBoxElement.SelectedItem as RadComboBoxItem).Text;
            }
            else
            {
                this.Value = DBNull.Value;
            }
        }


        private void ctlComboBoxExxElement_ValueChanged(object sender, EventArgs e)
        {
            if (ctlComboBoxExxElement.SelectedItem != null)
            {
                this.Value = (ctlComboBoxExxElement.SelectedItem as RadComboBoxItem).Text;
            }
            else
            {
                this.Value = DBNull.Value;
            }
        }

        private void ctlCheckComboBoxElement_PopupClosed(object sender, RadPopupClosedEventArgs args)
        {
            this.Value = ctlCheckComboBoxElement.CheckedNames;
        }

        private void ctlTextBox_TextChanged(object sender, EventArgs e)
        {
            this.Value = ctlTextBox.Text;
        }

        private void ctlSpin_TextChanged(object sender, EventArgs e)
        {
            this.Value = ctlSpin.Text;
        }


        private void ctlNumericTextBox_TextChanged(object sender, EventArgs e)
        {
            this.Value = ctlNumericTextBox.Text;
        }

        private void ctlIntTextBox_TextChanged(object sender, EventArgs e)
        {
            this.Value = ctlIntTextBox.Text;
        }

        private void ctlDataTimePickerElement_ValueChanged(object sender, EventArgs e)
        {
            this.Value = ctlDataTimePickerElement.Value.ToShortDateString();
        }

        private void ctlColorSelectorElement_TextChanged(object sender, EventArgs e)
        {
            this.Value = ctlColorSelectorElement.Text;
        }
        #endregion
    }

    public enum EmbedControlType
    {
        ComboBox,
        CheckComboBox,
        CheckBox,
        TextBox,
        DateTimePicker,
        ColorSelector,
        Spin,
        CSNumericTextBox,
        CSIntTextBox,
        CSComboBoxExx

    }

    public class CSColorSelectorElement : RadComboBoxElement
    {
        public CSColorSelectorElement()
            : base()
        {
            ((this.Children[2] as ComboBoxEditorLayoutPanel).Children[1] as RadArrowButtonElement).Click += new EventHandler(MyColorSelectorElement_Click);
        }

        private void MyColorSelectorElement_Click(object sender, EventArgs e)
        {
            using (RadColorDialog dlg = new RadColorDialog())
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    string[] colors = this.Text.Split(",".ToCharArray());
                    if (colors.Length == 3)
                    {
                        Color color = GenerateColorByText();
                        dlg.SelectedColor = color;
                    }
                }
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.Text = dlg.SelectedColor.R.ToString() + "," + dlg.SelectedColor.G.ToString() + "," + dlg.SelectedColor.B.ToString();
                    ((FillPrimitive)this.TextBoxElement.Children[1]).BackColor = dlg.SelectedColor;
                    this.TextBoxElement.TextBoxItem.BackColor = dlg.SelectedColor;
                }
            }
        }

        public override object Value
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value.ToString();
            }
        }

        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar.ToString().Equals(",") || ((System.Windows.Forms.Keys)e.KeyChar).Equals(System.Windows.Forms.Keys.Back));
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.Text))
            {
                string[] colors = this.Text.Split(",".ToCharArray());
                if (colors.Length == 3)
                {
                    Color color = GenerateColorByText();
                    ((FillPrimitive)this.TextBoxElement.Children[1]).BackColor = color;
                    this.TextBoxElement.TextBoxItem.BackColor = color;
                }
            }

            base.OnTextChanged(e);
        }

        private Color GenerateColorByText()
        {
            if (!string.IsNullOrEmpty(this.Text))
            {
                string[] colors = this.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (colors.Length == 3)
                {
                    int red = Convert.ToInt32(colors[0]);
                    int green = Convert.ToInt32(colors[1]);
                    int blue = Convert.ToInt32(colors[2]);
                    if (red >= 0 && red <= 255 && green >= 0 && green <= 255 && blue >= 0 && blue <= 255)
                    {
                        return Color.FromArgb(red, green, blue);
                    }
                }
            }
            return Color.White;
        }
    }

    public class CSGridRowHeaderCellElement : GridRowHeaderCellElement
    {
        object _value = null;
        string _columnName = string.Empty;
        Image _image = null;

        public CSGridRowHeaderCellElement(GridViewColumn column, GridRowElement row, string columnName, object value, Image image)
            : base(column, row)
        {
            _columnName = columnName;
            _value = value;
            _image = image;
        }

        public CSGridRowHeaderCellElement(GridViewColumn column, GridRowElement row)
            : base(column, row)
        {
        }

        public override void UpdateInfo()
        {
            base.UpdateInfo();
            if (this.RowInfo.DataBoundItem != null)
            {
                DataRow dr = (this.RowInfo.DataBoundItem as DataRowView).Row;
                if (dr[_columnName] != DBNull.Value && dr[_columnName].Equals(_value))
                {
                    this.Image = _image;
                }
                this.BackColor = this.RowElement.BackColor;
            }
        }
    }
}
