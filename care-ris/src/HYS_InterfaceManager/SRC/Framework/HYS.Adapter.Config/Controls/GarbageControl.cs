using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Config.Controls
{
    public partial class GarbageControl : UserControl, IConfigControl
    {
        public GarbageControl()
        {
            InitializeComponent();
            InitializeParticularTime();

            if (Program.InIM || Program.InIMWizard)
            {
                this.groupBoxWS.Visible = false;
            }

            this.groupBoxWS.Visible = false;
        }

        public void DisableGarbageSetting()
        {
            this.groupBoxGC.Visible = this.groupBoxGCR.Visible = false;
            this.groupBoxLog.Location = this.groupBoxGC.Location;
        }

        private void InitializeParticularTime()
        {
            string[] strlist;

            this.comboBoxMonth.Items.Clear();
            strlist = ParticularTime.GetMonths();
            if (strlist != null) foreach (string str in strlist) this.comboBoxMonth.Items.Add(str);
            if (this.comboBoxMonth.Items.Count > 0) this.comboBoxMonth.SelectedIndex = 0;

            this.comboBoxMonthDay.Items.Clear();
            strlist = ParticularTime.GetMonthDays();
            if (strlist != null) foreach (string str in strlist) this.comboBoxMonthDay.Items.Add(str);
            if (this.comboBoxMonthDay.Items.Count > 0) this.comboBoxMonthDay.SelectedIndex = 0;

            this.comboBoxWeekDay.Items.Clear();
            strlist = ParticularTime.GetWeekDays();
            if (strlist != null) foreach (string str in strlist) this.comboBoxWeekDay.Items.Add(str);
            if (this.comboBoxWeekDay.Items.Count > 0) this.comboBoxWeekDay.SelectedIndex = 0;
        }
        private void LoadParticularTime()
        {
            int value = 0;
            this.dateTimePickerTime.Enabled = Program.ServiceMgt.Config.GarbageCollection.StartAtParticularTime;

            value = (int)Program.ServiceMgt.Config.GarbageCollection.ParticularTime.Month;
            if (value > 0) this.comboBoxMonth.SelectedIndex = value - 1;
            this.checkBoxMonth.Checked = value > 0;

            value = (int)Program.ServiceMgt.Config.GarbageCollection.ParticularTime.MonthDay;
            if (value > 0) this.comboBoxMonthDay.SelectedIndex = value - 1;
            this.checkBoxMonthDay.Checked = value > 0;

            value = (int)Program.ServiceMgt.Config.GarbageCollection.ParticularTime.WeekDay;
            if (value > 0) this.comboBoxWeekDay.SelectedIndex = value - 1;
            this.checkBoxWeekDay.Checked = value > 0;
        }
        private void SaveParticularTime()
        {
            int value = 0;

            if (this.checkBoxMonth.Enabled && this.checkBoxMonth.Checked) value = this.comboBoxMonth.SelectedIndex + 1;
            Program.ServiceMgt.Config.GarbageCollection.ParticularTime.Month = (Month)value;

            value = 0;

            if (this.checkBoxMonthDay.Enabled && this.checkBoxMonthDay.Checked) value = this.comboBoxMonthDay.SelectedIndex + 1;
            Program.ServiceMgt.Config.GarbageCollection.ParticularTime.MonthDay = (MonthDay)value;

            value = 0;

            if (this.checkBoxWeekDay.Enabled && this.checkBoxWeekDay.Checked) value = this.comboBoxWeekDay.SelectedIndex + 1;
            Program.ServiceMgt.Config.GarbageCollection.ParticularTime.WeekDay = (WeekDay)value;
        }

        #region IConfigControl Members

        public bool LoadConfig()
        {
            #region Garbage Load
            if (Program.ServiceMgt != null)
            {
                this.checkBoxEnable.Checked = Program.ServiceMgt.Config.GarbageCollection.Enable;
                this.numericUpDownInterval.Value = Program.ServiceMgt.Config.GarbageCollection.Interval;
                this.checkBoxCheckProcessFlag.Checked = Program.ServiceMgt.Config.GarbageCollection.CheckProcessFlag;
                this.checkBoxCheckExpireTime.Checked = Program.ServiceMgt.Config.GarbageCollection.CheckExpireTime;
                this.numericUpDownDay.Value = Program.ServiceMgt.Config.GarbageCollection.ExpireTime.Days;
                this.numericUpDownHour.Value = Program.ServiceMgt.Config.GarbageCollection.ExpireTime.Hours;
                this.numericUpDownMinute.Value = Program.ServiceMgt.Config.GarbageCollection.ExpireTime.Minutes;
                this.numericUpDownSecond.Value = Program.ServiceMgt.Config.GarbageCollection.ExpireTime.Seconds;

                this.radioButtonInterval.Checked = !Program.ServiceMgt.Config.GarbageCollection.StartAtParticularTime;
                this.radioButtonParticular.Checked = Program.ServiceMgt.Config.GarbageCollection.StartAtParticularTime;
                this.dateTimePickerTime.Value = Program.ServiceMgt.Config.GarbageCollection.ParticularTime.Time;

                LoadParticularTime();
            }
            else
            {
                this.groupBoxGC.Enabled = this.groupBoxGCR.Enabled = false;
            }
            #endregion

            #region Log Config Load
            DeviceDir dir = Program.DeviceMgt.DeviceDirInfor;

            this.enumComboBoxLevel.Text = dir.LogInfo.LogType.ToString();
            this.numericUpDownFileDuration.Value = dir.LogInfo.FileDuration;
            #endregion

            return true;
        }

        public bool SaveConfig()
        {
            #region Garbage Save
            if (Program.ServiceMgt != null)
            {
                Program.ServiceMgt.Config.GarbageCollection.Enable = this.checkBoxEnable.Checked;
                Program.ServiceMgt.Config.GarbageCollection.Interval = (int)this.numericUpDownInterval.Value;
                Program.ServiceMgt.Config.GarbageCollection.CheckProcessFlag = this.checkBoxCheckProcessFlag.Checked;
                Program.ServiceMgt.Config.GarbageCollection.CheckExpireTime = this.checkBoxCheckExpireTime.Checked;
                Program.ServiceMgt.Config.GarbageCollection.ExpireTime = new TimeSpan
                    ((int)this.numericUpDownDay.Value,
                    (int)this.numericUpDownHour.Value,
                    (int)this.numericUpDownMinute.Value,
                    (int)this.numericUpDownSecond.Value);

                Program.ServiceMgt.Config.GarbageCollection.StartAtParticularTime = this.radioButtonParticular.Checked;
                Program.ServiceMgt.Config.GarbageCollection.ParticularTime.Time = this.dateTimePickerTime.Value;
                
                SaveParticularTime();

                if (!Program.ServiceMgt.Save())
                {
                    MessageBox.Show("Save Garbage Collection configuration failed.");
                }
            }
            #endregion

            #region Log Config Save
            DeviceDir dir = Program.DeviceMgt.DeviceDirInfor;

            dir.LogInfo.LogType = (LogType)Enum.Parse(typeof(LogType),enumComboBoxLevel.Text);
            dir.LogInfo.FileDuration = (int)this.numericUpDownFileDuration.Value;

            if (!Program.DeviceMgt.SaveDeviceDir())
            {
                MessageBox.Show("Save logging configuration failed.");
            }
            #endregion

            return true;
        }

        #endregion

        private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
        {
            this.panelGCTime.Enabled = this.groupBoxGCR.Enabled = this.checkBoxEnable.Checked;
        }
        private void checkBoxCheckExpireTime_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDownDay.Enabled =
                this.numericUpDownHour.Enabled =
                    this.numericUpDownMinute.Enabled =
                        this.numericUpDownSecond.Enabled = this.checkBoxCheckExpireTime.Checked;
        }
        private void radioButtonInterval_CheckedChanged(object sender, EventArgs e)
        {
            this.numericUpDownInterval.Enabled = this.radioButtonInterval.Checked;
            
            this.dateTimePickerTime.Enabled =
                this.checkBoxMonth.Enabled =
                this.checkBoxMonthDay.Enabled =
                this.checkBoxWeekDay.Enabled = !this.radioButtonInterval.Checked;

            if (this.checkBoxMonth.Enabled) this.checkBoxMonth.Enabled = this.checkBoxMonth.Checked;

            this.comboBoxMonth.Enabled = this.checkBoxMonth.Enabled && this.checkBoxMonth.Checked;
            this.comboBoxMonthDay.Enabled = this.checkBoxMonthDay.Enabled && this.checkBoxMonthDay.Checked;
            this.comboBoxWeekDay.Enabled = this.checkBoxWeekDay.Enabled && this.checkBoxWeekDay.Checked;
        }
        private void checkBoxMonthDay_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxMonthDay.Enabled = this.checkBoxMonthDay.Checked;
            this.checkBoxMonth.Enabled = this.checkBoxMonthDay.Checked;
            if (this.checkBoxMonthDay.Checked)
            {
                this.checkBoxWeekDay.Checked = false;
            }
            else
            {
                this.checkBoxMonth.Checked = false;
            }
        }
        private void checkBoxWeekDay_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxWeekDay.Enabled = this.checkBoxWeekDay.Checked;
            if (this.checkBoxWeekDay.Checked)
            {
                this.checkBoxMonthDay.Checked = this.checkBoxMonth.Checked = false;
            }
        }
        private void checkBoxMonth_CheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxMonth.Enabled = this.checkBoxMonth.Checked;
        }

        private void buttonWSBrowse_Click(object sender, EventArgs e)
        {
            string url = this.textBoxWSLocation.Text.Trim();
            if (url.Length < 1) return;
            ProcessStartInfo info = new ProcessStartInfo(url);
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;
            Process.Start(info);

        }
        private void buttonWSInvoke_Click(object sender, EventArgs e)
        {
            TabControl tabCtrl = this.Parent.Parent as TabControl;
            if (tabCtrl == null) return;

            foreach (TabPage tabPage in tabCtrl.TabPages)
            {
                foreach (Control ctl in tabPage.Controls)
                {
                    IConfigControl cCtrl = ctl as IConfigControl;
                    if (cCtrl != null)
                    {
                        cCtrl.SaveConfig();
                        continue;
                    }
                    IConfigUI aCtrl = ctl as IConfigUI;
                    if (aCtrl != null)
                    {
                        aCtrl.SaveConfig();
                        continue;
                    }
                }
            }
        }

        private void buttonAdditionalQC_Click(object sender, EventArgs e)
        {
            FormQCSetting frm = new FormQCSetting(Program.ServiceMgt.Config.GarbageCollection.AdditionalCriteria);
            frm.ShowDialog(this);
        }
    }
}
