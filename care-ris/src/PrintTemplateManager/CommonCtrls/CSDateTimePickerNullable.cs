using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Hys.CommonControls
{
    public class CSDateTimePickerNullable : DateTimePicker
    {
        const string NullableFormat = " ";
        bool isSelfSetting;
        string originalCustomFormat;
        bool originalCustomFormatInitialized;
        DateTimePickerFormat? originalFormat;

        bool IsNullableState
        {
            get { return Format == DateTimePickerFormat.Custom && CustomFormat == NullableFormat; }
        }

        void SetNullable(bool nullable)
        {
            if (!this.originalFormat.HasValue)
            {
                this.originalFormat = Format;
            }
            if (!this.originalCustomFormatInitialized)
            {
                this.originalCustomFormat = CustomFormat;
                this.originalCustomFormatInitialized = true;
            }

            this.isSelfSetting = true;
            Format = Nullable && nullable ? DateTimePickerFormat.Custom : this.originalFormat.Value;
            CustomFormat = Nullable && nullable ? NullableFormat : this.originalCustomFormat;
            this.isSelfSetting = false;
        }

        #region Properties

        public bool Nullable { get; set; }

        #region DefaultValue

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? DefaultValue { get; private set; }

        Func<DateTime?> defaultValueSetter;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<DateTime?> DefaultValueSetter
        {
            get { return this.defaultValueSetter; }
            set
            {
                this.defaultValueSetter = value;
                GetDefaultValue();
            }
        }

        DateTime? GetDefaultValue()
        {
            DefaultValue = null;
            if (this.defaultValueSetter != null)
                DefaultValue = this.defaultValueSetter();
            if (Nullable
                && IsNullableState
                && DefaultValue.HasValue)
                base.Value = DefaultValue.Value;
            return DefaultValue;
        }

        #endregion

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DateTime? Value
        {
            get { return IsNullableState ? (DateTime?)null : base.Value; }
            set
            {
                if (!Nullable && !value.HasValue)
                    throw new ArgumentNullException("Value");

                if (value.HasValue)
                    base.Value = value.Value;
                else
                    GetDefaultValue();
                SetNullable(!value.HasValue);
            }
        }

        public new DateTimePickerFormat Format
        {
            get { return base.Format; }
            set
            {
                if (!this.isSelfSetting)
                    this.originalFormat = value;
                base.Format = value;
            }
        }

        public new string CustomFormat
        {
            get { return base.CustomFormat; }
            set
            {
                if (!this.isSelfSetting)
                {
                    this.originalCustomFormat = value;
                    this.originalCustomFormatInitialized = true;
                }
                base.CustomFormat = value;
            }
        }

        #endregion

        #region Events

        protected override void OnMouseDown(MouseEventArgs e)
        {
            SuspendNullable();
            base.OnMouseDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Nullable)
            {
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                    Value = null;
                else
                    SuspendNullable();
            }
            base.OnKeyDown(e);
        }

        public void SuspendNullable()
        {
            if (Nullable && IsNullableState)
            {
                GetDefaultValue();
                SetNullable(false);
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            #region 将全角数字转换成半角 for EK_HI00118796

            if (e.KeyChar == 12288)
            {
                e.KeyChar = (char)32;
            }


            if (e.KeyChar > 65280 && e.KeyChar < 65375)
            {
                e.KeyChar = (char)(e.KeyChar - 65248);
            }

            #endregion
        }

        #endregion
    }
}
