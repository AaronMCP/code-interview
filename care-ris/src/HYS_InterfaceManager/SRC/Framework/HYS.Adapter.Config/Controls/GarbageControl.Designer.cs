namespace HYS.Adapter.Config.Controls
{
    partial class GarbageControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxGC = new System.Windows.Forms.GroupBox();
            this.panelGCTime = new System.Windows.Forms.Panel();
            this.dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxMonth = new System.Windows.Forms.ComboBox();
            this.checkBoxMonth = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxMonthDay = new System.Windows.Forms.ComboBox();
            this.checkBoxMonthDay = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxWeekDay = new System.Windows.Forms.ComboBox();
            this.checkBoxWeekDay = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonParticular = new System.Windows.Forms.RadioButton();
            this.radioButtonInterval = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.checkBoxEnable = new System.Windows.Forms.CheckBox();
            this.groupBoxGCR = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownSecond = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownMinute = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownHour = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownDay = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxCheckExpireTime = new System.Windows.Forms.CheckBox();
            this.checkBoxCheckProcessFlag = new System.Windows.Forms.CheckBox();
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.enumComboBoxLevel = new HYS.Adapter.Config.UIControls.EnumComboBox();
            this.numericUpDownFileDuration = new System.Windows.Forms.NumericUpDown();
            this.lblFileDuration = new System.Windows.Forms.Label();
            this.lblLogLevel = new System.Windows.Forms.Label();
            this.groupBoxWS = new System.Windows.Forms.GroupBox();
            this.buttonWSInvoke = new System.Windows.Forms.Button();
            this.buttonWSBrowse = new System.Windows.Forms.Button();
            this.textBoxWSLocation = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonAdditionalQC = new System.Windows.Forms.Button();
            this.groupBoxGC.SuspendLayout();
            this.panelGCTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            this.groupBoxGCR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDay)).BeginInit();
            this.groupBoxLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFileDuration)).BeginInit();
            this.groupBoxWS.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxGC
            // 
            this.groupBoxGC.Controls.Add(this.panelGCTime);
            this.groupBoxGC.Controls.Add(this.checkBoxEnable);
            this.groupBoxGC.Location = new System.Drawing.Point(13, 12);
            this.groupBoxGC.Name = "groupBoxGC";
            this.groupBoxGC.Size = new System.Drawing.Size(384, 268);
            this.groupBoxGC.TabIndex = 1;
            this.groupBoxGC.TabStop = false;
            this.groupBoxGC.Text = "Garbage Collection Control";
            // 
            // panelGCTime
            // 
            this.panelGCTime.Controls.Add(this.dateTimePickerTime);
            this.panelGCTime.Controls.Add(this.label12);
            this.panelGCTime.Controls.Add(this.label11);
            this.panelGCTime.Controls.Add(this.comboBoxMonth);
            this.panelGCTime.Controls.Add(this.checkBoxMonth);
            this.panelGCTime.Controls.Add(this.label10);
            this.panelGCTime.Controls.Add(this.comboBoxMonthDay);
            this.panelGCTime.Controls.Add(this.checkBoxMonthDay);
            this.panelGCTime.Controls.Add(this.label9);
            this.panelGCTime.Controls.Add(this.comboBoxWeekDay);
            this.panelGCTime.Controls.Add(this.checkBoxWeekDay);
            this.panelGCTime.Controls.Add(this.label1);
            this.panelGCTime.Controls.Add(this.radioButtonParticular);
            this.panelGCTime.Controls.Add(this.radioButtonInterval);
            this.panelGCTime.Controls.Add(this.label2);
            this.panelGCTime.Controls.Add(this.numericUpDownInterval);
            this.panelGCTime.Enabled = false;
            this.panelGCTime.Location = new System.Drawing.Point(46, 61);
            this.panelGCTime.Name = "panelGCTime";
            this.panelGCTime.Size = new System.Drawing.Size(327, 176);
            this.panelGCTime.TabIndex = 19;
            // 
            // dateTimePickerTime
            // 
            this.dateTimePickerTime.Enabled = false;
            this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTime.Location = new System.Drawing.Point(64, 66);
            this.dateTimePickerTime.Name = "dateTimePickerTime";
            this.dateTimePickerTime.ShowUpDown = true;
            this.dateTimePickerTime.Size = new System.Drawing.Size(141, 20);
            this.dateTimePickerTime.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(39, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 20);
            this.label12.TabIndex = 13;
            this.label12.Text = "At ";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(211, 146);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 21);
            this.label11.TabIndex = 18;
            this.label11.Text = "Every Year.";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxMonth
            // 
            this.comboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMonth.Enabled = false;
            this.comboBoxMonth.FormattingEnabled = true;
            this.comboBoxMonth.Location = new System.Drawing.Point(64, 146);
            this.comboBoxMonth.MaxDropDownItems = 12;
            this.comboBoxMonth.Name = "comboBoxMonth";
            this.comboBoxMonth.Size = new System.Drawing.Size(141, 21);
            this.comboBoxMonth.TabIndex = 17;
            // 
            // checkBoxMonth
            // 
            this.checkBoxMonth.Enabled = false;
            this.checkBoxMonth.Location = new System.Drawing.Point(22, 146);
            this.checkBoxMonth.Name = "checkBoxMonth";
            this.checkBoxMonth.Size = new System.Drawing.Size(55, 21);
            this.checkBoxMonth.TabIndex = 16;
            this.checkBoxMonth.Text = "At";
            this.checkBoxMonth.UseVisualStyleBackColor = true;
            this.checkBoxMonth.CheckedChanged += new System.EventHandler(this.checkBoxMonth_CheckedChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(211, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 21);
            this.label10.TabIndex = 15;
            this.label10.Text = "Every Month.";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxMonthDay
            // 
            this.comboBoxMonthDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMonthDay.Enabled = false;
            this.comboBoxMonthDay.FormattingEnabled = true;
            this.comboBoxMonthDay.Location = new System.Drawing.Point(64, 119);
            this.comboBoxMonthDay.MaxDropDownItems = 12;
            this.comboBoxMonthDay.Name = "comboBoxMonthDay";
            this.comboBoxMonthDay.Size = new System.Drawing.Size(141, 21);
            this.comboBoxMonthDay.TabIndex = 14;
            // 
            // checkBoxMonthDay
            // 
            this.checkBoxMonthDay.Location = new System.Drawing.Point(22, 119);
            this.checkBoxMonthDay.Name = "checkBoxMonthDay";
            this.checkBoxMonthDay.Size = new System.Drawing.Size(55, 21);
            this.checkBoxMonthDay.TabIndex = 13;
            this.checkBoxMonthDay.Text = "At";
            this.checkBoxMonthDay.UseVisualStyleBackColor = true;
            this.checkBoxMonthDay.CheckedChanged += new System.EventHandler(this.checkBoxMonthDay_CheckedChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(211, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 21);
            this.label9.TabIndex = 12;
            this.label9.Text = "Every Week.";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxWeekDay
            // 
            this.comboBoxWeekDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWeekDay.Enabled = false;
            this.comboBoxWeekDay.FormattingEnabled = true;
            this.comboBoxWeekDay.Location = new System.Drawing.Point(64, 92);
            this.comboBoxWeekDay.Name = "comboBoxWeekDay";
            this.comboBoxWeekDay.Size = new System.Drawing.Size(141, 21);
            this.comboBoxWeekDay.TabIndex = 11;
            // 
            // checkBoxWeekDay
            // 
            this.checkBoxWeekDay.Location = new System.Drawing.Point(22, 92);
            this.checkBoxWeekDay.Name = "checkBoxWeekDay";
            this.checkBoxWeekDay.Size = new System.Drawing.Size(55, 21);
            this.checkBoxWeekDay.TabIndex = 10;
            this.checkBoxWeekDay.Text = "At";
            this.checkBoxWeekDay.UseVisualStyleBackColor = true;
            this.checkBoxWeekDay.CheckedChanged += new System.EventHandler(this.checkBoxWeekDay_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(211, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Everyday.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radioButtonParticular
            // 
            this.radioButtonParticular.AutoSize = true;
            this.radioButtonParticular.Location = new System.Drawing.Point(0, 37);
            this.radioButtonParticular.Name = "radioButtonParticular";
            this.radioButtonParticular.Size = new System.Drawing.Size(236, 17);
            this.radioButtonParticular.TabIndex = 6;
            this.radioButtonParticular.TabStop = true;
            this.radioButtonParticular.Text = "Start Garbage Collection in a Particular Time.";
            this.radioButtonParticular.UseVisualStyleBackColor = true;
            // 
            // radioButtonInterval
            // 
            this.radioButtonInterval.AutoSize = true;
            this.radioButtonInterval.Location = new System.Drawing.Point(0, 6);
            this.radioButtonInterval.Name = "radioButtonInterval";
            this.radioButtonInterval.Size = new System.Drawing.Size(181, 17);
            this.radioButtonInterval.TabIndex = 5;
            this.radioButtonInterval.TabStop = true;
            this.radioButtonInterval.Text = "Start Garbage Collection in Every";
            this.radioButtonInterval.UseVisualStyleBackColor = true;
            this.radioButtonInterval.CheckedChanged += new System.EventHandler(this.radioButtonInterval_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(286, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "ms";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Enabled = false;
            this.numericUpDownInterval.Location = new System.Drawing.Point(203, 6);
            this.numericUpDownInterval.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownInterval.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(81, 20);
            this.numericUpDownInterval.TabIndex = 3;
            this.numericUpDownInterval.Value = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.Location = new System.Drawing.Point(22, 30);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(344, 18);
            this.checkBoxEnable.TabIndex = 2;
            this.checkBoxEnable.Text = "Enable Garbage Collection.";
            this.checkBoxEnable.UseVisualStyleBackColor = true;
            this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.checkBoxEnable_CheckedChanged);
            // 
            // groupBoxGCR
            // 
            this.groupBoxGCR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGCR.Controls.Add(this.buttonAdditionalQC);
            this.groupBoxGCR.Controls.Add(this.label7);
            this.groupBoxGCR.Controls.Add(this.numericUpDownSecond);
            this.groupBoxGCR.Controls.Add(this.label6);
            this.groupBoxGCR.Controls.Add(this.numericUpDownMinute);
            this.groupBoxGCR.Controls.Add(this.label5);
            this.groupBoxGCR.Controls.Add(this.numericUpDownHour);
            this.groupBoxGCR.Controls.Add(this.label4);
            this.groupBoxGCR.Controls.Add(this.numericUpDownDay);
            this.groupBoxGCR.Controls.Add(this.label3);
            this.groupBoxGCR.Controls.Add(this.checkBoxCheckExpireTime);
            this.groupBoxGCR.Controls.Add(this.checkBoxCheckProcessFlag);
            this.groupBoxGCR.Enabled = false;
            this.groupBoxGCR.Location = new System.Drawing.Point(403, 12);
            this.groupBoxGCR.Name = "groupBoxGCR";
            this.groupBoxGCR.Size = new System.Drawing.Size(372, 268);
            this.groupBoxGCR.TabIndex = 2;
            this.groupBoxGCR.TabStop = false;
            this.groupBoxGCR.Text = "Garbage Collection Rule";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(284, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 21);
            this.label7.TabIndex = 12;
            this.label7.Text = "Second(s)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownSecond
            // 
            this.numericUpDownSecond.Enabled = false;
            this.numericUpDownSecond.Location = new System.Drawing.Point(146, 175);
            this.numericUpDownSecond.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownSecond.Name = "numericUpDownSecond";
            this.numericUpDownSecond.Size = new System.Drawing.Size(132, 20);
            this.numericUpDownSecond.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(284, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 21);
            this.label6.TabIndex = 10;
            this.label6.Text = "Minute(s)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownMinute
            // 
            this.numericUpDownMinute.Enabled = false;
            this.numericUpDownMinute.Location = new System.Drawing.Point(146, 149);
            this.numericUpDownMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownMinute.Name = "numericUpDownMinute";
            this.numericUpDownMinute.Size = new System.Drawing.Size(132, 20);
            this.numericUpDownMinute.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(284, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "Hour(s)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownHour
            // 
            this.numericUpDownHour.Enabled = false;
            this.numericUpDownHour.Location = new System.Drawing.Point(146, 123);
            this.numericUpDownHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownHour.Name = "numericUpDownHour";
            this.numericUpDownHour.Size = new System.Drawing.Size(132, 20);
            this.numericUpDownHour.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(284, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Day(s)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownDay
            // 
            this.numericUpDownDay.Enabled = false;
            this.numericUpDownDay.Location = new System.Drawing.Point(146, 97);
            this.numericUpDownDay.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownDay.Name = "numericUpDownDay";
            this.numericUpDownDay.Size = new System.Drawing.Size(132, 20);
            this.numericUpDownDay.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(39, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Keep Data In:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxCheckExpireTime
            // 
            this.checkBoxCheckExpireTime.Location = new System.Drawing.Point(22, 67);
            this.checkBoxCheckExpireTime.Name = "checkBoxCheckExpireTime";
            this.checkBoxCheckExpireTime.Size = new System.Drawing.Size(334, 18);
            this.checkBoxCheckExpireTime.TabIndex = 3;
            this.checkBoxCheckExpireTime.Text = "Delete Data According to Expire Time.";
            this.checkBoxCheckExpireTime.UseVisualStyleBackColor = true;
            this.checkBoxCheckExpireTime.CheckedChanged += new System.EventHandler(this.checkBoxCheckExpireTime_CheckedChanged);
            // 
            // checkBoxCheckProcessFlag
            // 
            this.checkBoxCheckProcessFlag.Location = new System.Drawing.Point(22, 30);
            this.checkBoxCheckProcessFlag.Name = "checkBoxCheckProcessFlag";
            this.checkBoxCheckProcessFlag.Size = new System.Drawing.Size(334, 18);
            this.checkBoxCheckProcessFlag.TabIndex = 2;
            this.checkBoxCheckProcessFlag.Text = "Delete Data Processed";
            this.checkBoxCheckProcessFlag.UseVisualStyleBackColor = true;
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLog.Controls.Add(this.label8);
            this.groupBoxLog.Controls.Add(this.enumComboBoxLevel);
            this.groupBoxLog.Controls.Add(this.numericUpDownFileDuration);
            this.groupBoxLog.Controls.Add(this.lblFileDuration);
            this.groupBoxLog.Controls.Add(this.lblLogLevel);
            this.groupBoxLog.Location = new System.Drawing.Point(13, 286);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(762, 105);
            this.groupBoxLog.TabIndex = 15;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "Log Console";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(257, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 21);
            this.label8.TabIndex = 52;
            this.label8.Text = "Day(s)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // enumComboBoxLevel
            // 
            this.enumComboBoxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumComboBoxLevel.FormattingEnabled = true;
            this.enumComboBoxLevel.Items.AddRange(new object[] {
            "Debug",
            "Info",
            "Warning",
            "Error"});
            this.enumComboBoxLevel.Location = new System.Drawing.Point(110, 31);
            this.enumComboBoxLevel.Name = "enumComboBoxLevel";
            this.enumComboBoxLevel.Size = new System.Drawing.Size(141, 21);
            this.enumComboBoxLevel.TabIndex = 51;
            this.enumComboBoxLevel.TheType = typeof(HYS.Common.Objects.Logging.LogType);
            // 
            // numericUpDownFileDuration
            // 
            this.numericUpDownFileDuration.Location = new System.Drawing.Point(110, 63);
            this.numericUpDownFileDuration.Name = "numericUpDownFileDuration";
            this.numericUpDownFileDuration.Size = new System.Drawing.Size(141, 20);
            this.numericUpDownFileDuration.TabIndex = 50;
            // 
            // lblFileDuration
            // 
            this.lblFileDuration.Location = new System.Drawing.Point(19, 63);
            this.lblFileDuration.Name = "lblFileDuration";
            this.lblFileDuration.Size = new System.Drawing.Size(85, 20);
            this.lblFileDuration.TabIndex = 49;
            this.lblFileDuration.Text = "File Duration:";
            this.lblFileDuration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLogLevel
            // 
            this.lblLogLevel.Location = new System.Drawing.Point(19, 31);
            this.lblLogLevel.Name = "lblLogLevel";
            this.lblLogLevel.Size = new System.Drawing.Size(85, 21);
            this.lblLogLevel.TabIndex = 28;
            this.lblLogLevel.Text = "Log Level:";
            this.lblLogLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxWS
            // 
            this.groupBoxWS.Controls.Add(this.buttonWSInvoke);
            this.groupBoxWS.Controls.Add(this.buttonWSBrowse);
            this.groupBoxWS.Controls.Add(this.textBoxWSLocation);
            this.groupBoxWS.Controls.Add(this.label13);
            this.groupBoxWS.Location = new System.Drawing.Point(13, 398);
            this.groupBoxWS.Name = "groupBoxWS";
            this.groupBoxWS.Size = new System.Drawing.Size(761, 63);
            this.groupBoxWS.TabIndex = 16;
            this.groupBoxWS.TabStop = false;
            this.groupBoxWS.Text = "Create Remote Interface";
            // 
            // buttonWSInvoke
            // 
            this.buttonWSInvoke.Location = new System.Drawing.Point(604, 28);
            this.buttonWSInvoke.Name = "buttonWSInvoke";
            this.buttonWSInvoke.Size = new System.Drawing.Size(64, 20);
            this.buttonWSInvoke.TabIndex = 32;
            this.buttonWSInvoke.Text = "Invoke";
            this.buttonWSInvoke.UseVisualStyleBackColor = true;
            this.buttonWSInvoke.Click += new System.EventHandler(this.buttonWSInvoke_Click);
            // 
            // buttonWSBrowse
            // 
            this.buttonWSBrowse.Location = new System.Drawing.Point(534, 28);
            this.buttonWSBrowse.Name = "buttonWSBrowse";
            this.buttonWSBrowse.Size = new System.Drawing.Size(64, 20);
            this.buttonWSBrowse.TabIndex = 31;
            this.buttonWSBrowse.Text = "Browse";
            this.buttonWSBrowse.UseVisualStyleBackColor = true;
            this.buttonWSBrowse.Click += new System.EventHandler(this.buttonWSBrowse_Click);
            // 
            // textBoxWSLocation
            // 
            this.textBoxWSLocation.Location = new System.Drawing.Point(111, 28);
            this.textBoxWSLocation.Name = "textBoxWSLocation";
            this.textBoxWSLocation.Size = new System.Drawing.Size(417, 20);
            this.textBoxWSLocation.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(19, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 18);
            this.label13.TabIndex = 29;
            this.label13.Text = "IM Location:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonAdditionalQC
            // 
            this.buttonAdditionalQC.Location = new System.Drawing.Point(22, 209);
            this.buttonAdditionalQC.Name = "buttonAdditionalQC";
            this.buttonAdditionalQC.Size = new System.Drawing.Size(255, 27);
            this.buttonAdditionalQC.TabIndex = 13;
            this.buttonAdditionalQC.Text = "Additional Garbarge Collection Criteria";
            this.buttonAdditionalQC.UseVisualStyleBackColor = true;
            this.buttonAdditionalQC.Click += new System.EventHandler(this.buttonAdditionalQC_Click);
            // 
            // GarbageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxWS);
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.groupBoxGCR);
            this.Controls.Add(this.groupBoxGC);
            this.Name = "GarbageControl";
            this.Size = new System.Drawing.Size(793, 473);
            this.groupBoxGC.ResumeLayout(false);
            this.panelGCTime.ResumeLayout(false);
            this.panelGCTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            this.groupBoxGCR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDay)).EndInit();
            this.groupBoxLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFileDuration)).EndInit();
            this.groupBoxWS.ResumeLayout(false);
            this.groupBoxWS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxGC;
        private System.Windows.Forms.CheckBox checkBoxEnable;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxGCR;
        private System.Windows.Forms.CheckBox checkBoxCheckProcessFlag;
        private System.Windows.Forms.CheckBox checkBoxCheckExpireTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownSecond;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownMinute;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownHour;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownDay;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.NumericUpDown numericUpDownFileDuration;
        private System.Windows.Forms.Label lblFileDuration;
        private System.Windows.Forms.Label lblLogLevel;
        private HYS.Adapter.Config.UIControls.EnumComboBox enumComboBoxLevel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioButtonInterval;
        private System.Windows.Forms.RadioButton radioButtonParticular;
        private System.Windows.Forms.DateTimePicker dateTimePickerTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxWeekDay;
        private System.Windows.Forms.CheckBox checkBoxWeekDay;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxMonthDay;
        private System.Windows.Forms.CheckBox checkBoxMonthDay;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxMonth;
        private System.Windows.Forms.CheckBox checkBoxMonth;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panelGCTime;
        private System.Windows.Forms.GroupBox groupBoxWS;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonWSInvoke;
        private System.Windows.Forms.Button buttonWSBrowse;
        private System.Windows.Forms.TextBox textBoxWSLocation;
        private System.Windows.Forms.Button buttonAdditionalQC;
    }
}
