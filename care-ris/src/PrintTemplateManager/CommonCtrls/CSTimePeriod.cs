using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Hys.CommonControls
{
    public partial class CSTimePeriod : UserControl
    {
        public CSTimePeriod()
        {
            InitializeComponent();
            this.dtpBegin.KeyDown += ListenKeyEvent;
            this.dtpEnd.KeyDown += ListenKeyEvent;
            this.dtpBegin.MouseDown += ListenMouseEvent;
            this.dtpEnd.MouseDown += ListenMouseEvent;
        }

        #region Associate

        CSDateTimePickerNullable GetTarget(object sender)
        {
            return ((Control)sender).Name == this.dtpBegin.Name ? this.dtpEnd : this.dtpBegin;
        }

        void ListenKeyEvent(object sender, KeyEventArgs e)
        {
            var target = GetTarget(sender);
            if (Nullable && (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete))
                target.Value = null;
            else
                target.SuspendNullable();
        }

        void ListenMouseEvent(object sender, MouseEventArgs e)
        {
            var target = GetTarget(sender);
            target.SuspendNullable();
        }

        #endregion

        void AdjustSize(int width)
        {
            SuspendLayout();

            var lblWidth = this.lblSeparator.Size.Width;
            var dtpWidth = (width - lblWidth)/2;

            this.dtpBegin.Size = new Size(dtpWidth, 20);

            this.lblSeparator.Location = new Point(dtpWidth, 3);

            this.dtpEnd.Location = new Point(dtpWidth + lblWidth, 0);
            this.dtpEnd.Size = new Size(dtpWidth, 20);

            ResumeLayout(false);
            PerformLayout();
        }

        #region Properties

        public DateTimePickerFormat Format
        {
            get
            {
                if (this.dtpBegin.Format != this.dtpEnd.Format)
                    this.dtpEnd.Format = this.dtpBegin.Format;
                return this.dtpBegin.Format;
            }
            set { this.dtpBegin.Format = this.dtpEnd.Format = value; }
        }

        public string CustomFormat
        {
            get
            {
                if (this.dtpBegin.CustomFormat != this.dtpEnd.CustomFormat)
                    this.dtpEnd.CustomFormat = this.dtpBegin.CustomFormat;
                return this.dtpBegin.CustomFormat;
            }
            set { this.dtpBegin.CustomFormat = this.dtpEnd.CustomFormat = value; }
        }

        public bool Nullable
        {
            get
            {
                if (this.dtpBegin.Nullable != this.dtpEnd.Nullable)
                    this.dtpEnd.Nullable = this.dtpBegin.Nullable;
                return this.dtpBegin.Nullable;
            }
            set { this.dtpBegin.Nullable = this.dtpEnd.Nullable = value; }
        }

        public bool ShowUpDown
        {
            get
            {
                if (this.dtpBegin.ShowUpDown != this.dtpEnd.ShowUpDown)
                    this.dtpEnd.ShowUpDown = this.dtpBegin.ShowUpDown;
                return this.dtpBegin.ShowUpDown;
            }
            set { this.dtpBegin.ShowUpDown = this.dtpEnd.ShowUpDown = value; }
        }

        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = this.dtpBegin.Font = this.lblSeparator.Font = this.dtpEnd.Font = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CSDateTimePickerNullable Begin
        {
            get { return this.dtpBegin; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CSDateTimePickerNullable End
        {
            get { return this.dtpEnd; }
        }

        #endregion

        #region Events

        protected override void OnSizeChanged(EventArgs e)
        {
            AdjustSize(Width);
            base.OnSizeChanged(e);
        }

        #endregion

        #region Extensions

        #region Validation

        public string ErrorMessage { get; set; }

        public bool IsValid(ErrorProvider errorProvider = null)
        {
            var isValid = this.dtpBegin.Value.HasValue && this.dtpEnd.Value.HasValue && this.dtpBegin.Value.Value <= this.dtpEnd.Value.Value
                || Nullable && !this.dtpBegin.Value.HasValue && !this.dtpEnd.Value.HasValue;

            if (!isValid && errorProvider != null)
                errorProvider.SetError(this, ErrorMessage);

            return isValid;
        }

        #endregion

        #region Read / Write

        public string BeginKey { get; set; }

        public string EndKey { get; set; }

        public void Read(DataRow dr)
        {
            Begin.Value = dr.GetValue<DateTime?>(BeginKey);
            End.Value = dr.GetValue<DateTime?>(EndKey);
        }

        public void Write(DataRow dr)
        {
            dr.SetValue(BeginKey, Begin.Value);
            dr.SetValue(EndKey, End.Value);
        }

        #endregion

        #endregion
    }
}
