using System;
using System.Collections.Generic;
using System.Text;
using Telerik.WinControls.UI;
using System.Windows.Forms;

namespace Hys.CommonControls
{
    public class CSDateTimePicker : RadDateTimePicker
    {
        string HHmmss = "00:00:00"; //the time before select calendar date
        public CSDateTimePicker()
            : base()
        {
            this.DateTimePickerElement.CalendarSize = new System.Drawing.Size(250, 191);
        }

        public override System.Drawing.Color BackColor
        {
            get
            {
                return (this.DateTimePickerElement.TextBoxElement).BackColor;
            }
            set
            {
                this.DateTimePickerElement.TextBoxElement.BackColor = value;
            }
        }

        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadDateTimePicker"; }
        }

        public override string Text
        {
            get
            {
                return base.Value.ToString();
            }
            set
            {
                DateTime dt = DateTime.MinValue;
                if (DateTime.TryParse(value, out dt))
                {
                    base.Value = dt;
                }
            }
        }

        

        protected override void OnOpened(EventArgs args)
        {
            base.OnOpened(args);
            HHmmss = this.Value.ToString("HH:mm:ss");
            RadDateTimePickerCalendar calendar = this.DateTimePickerElement.GetCurrentBehavior() as RadDateTimePickerCalendar;
            #region adjust the calendar dropdown's location
            RadDateTimePickerDropDown dropDownCalendar = calendar.PopupControl as RadDateTimePickerDropDown;
            if (dropDownCalendar.Bounds.Right > Screen.PrimaryScreen.Bounds.Width)
            {
                dropDownCalendar.Location = new System.Drawing.Point(dropDownCalendar.Location.X - (dropDownCalendar.Bounds.Right - Screen.PrimaryScreen.Bounds.Width), dropDownCalendar.Location.Y);
            }
            #endregion
            if (calendar != null)
            {
                calendar.Calendar.ShowFooter = true;
                calendar.Calendar.CalendarElement.CalendarStatusElement.Text = "";
                calendar.Calendar.TodayButton.Click -= TodayButton_Click;
                calendar.Calendar.TodayButton.Click += new EventHandler(TodayButton_Click);
                calendar.Calendar.TodayButton.Text =  Culture.LCID == 1033 ? "Today" : "今天";
                calendar.Calendar.ClearButton.Visibility = Telerik.WinControls.ElementVisibility.Hidden;


                if (this.Value.Equals(this.MinDate))
                {
                    calendar.Calendar.FocusedDate = System.DateTime.Now;
                }
            }
        }


        private void TodayButton_Click(object sender, EventArgs e)
        {
            this.Value = DateTime.Now;
            RadDateTimePickerCalendar calendar = this.DateTimePickerElement.GetCurrentBehavior() as RadDateTimePickerCalendar;
            if (calendar != null)
            {
                calendar.PopupControl.ClosePopup(RadPopupCloseReason.CloseCalled);
            }
        }

        protected override void OnClosed(RadPopupClosedEventArgs args)
        {
            base.OnClosed(args);
            this.Value = DateTime.Parse(this.Value.ToString("yyyy-MM-dd ") + HHmmss);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.Font = new System.Drawing.Font("MS Reference Sans Serif", this.Font.Size, this.Font.Style);
        }

        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
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
    }
}
