namespace CarestreamCommonCtrls
{
    partial class CSOnlineUserCountSettingForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new Hys.CommonControls.CSButton();
            this.btnOK = new Hys.CommonControls.CSButton();
            this.btnDelete = new Hys.CommonControls.CSButton();
            this.btnAdd = new Hys.CommonControls.CSButton();
            this.dgvResult = new Hys.CommonControls.CSGridView();
            this.csGroupBox1 = new Hys.CommonControls.CSGroupBox();
            this.nmcCount = new Hys.CommonControls.CSNumericTextBox();
            this.csLabel5 = new Hys.CommonControls.CSLabel();
            this.csLabel3 = new Hys.CommonControls.CSLabel();
            this.csLabel2 = new Hys.CommonControls.CSLabel();
            this.dtpToTime = new Hys.CommonControls.CSDateTimePicker();
            this.dtpFromTime = new Hys.CommonControls.CSDateTimePicker();
            this.dtpToDate = new Hys.CommonControls.CSDateTimePicker();
            this.dtpFromDate = new Hys.CommonControls.CSDateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult.MasterGridViewTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.csGroupBox1)).BeginInit();
            this.csGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmcCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.csLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.csLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.csLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(235, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(73, 28);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(156, 338);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(73, 28);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(235, 145);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(73, 28);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Remove";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(156, 145);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(73, 28);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvResult
            // 
            this.dgvResult.AllowColumnSelection = false;
            this.dgvResult.AllowImageColumnResize = false;
            this.dgvResult.Location = new System.Drawing.Point(11, 179);
            // 
            // 
            // 
            this.dgvResult.MasterGridViewTemplate.AllowAddNewRow = false;
            this.dgvResult.MasterGridViewTemplate.AllowCellContextMenu = false;
            this.dgvResult.MasterGridViewTemplate.AllowColumnHeaderContextMenu = false;
            this.dgvResult.MasterGridViewTemplate.AllowDeleteRow = false;
            this.dgvResult.MasterGridViewTemplate.AllowDragToGroup = false;
            this.dgvResult.MasterGridViewTemplate.ShowRowHeaderColumn = false;
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowSel = -1;
            this.dgvResult.ShowGroupPanel = false;
            this.dgvResult.Size = new System.Drawing.Size(403, 153);
            this.dgvResult.TabIndex = 9;
            // 
            // csGroupBox1
            // 
            this.csGroupBox1.Controls.Add(this.nmcCount);
            this.csGroupBox1.Controls.Add(this.csLabel5);
            this.csGroupBox1.Controls.Add(this.csLabel3);
            this.csGroupBox1.Controls.Add(this.csLabel2);
            this.csGroupBox1.Controls.Add(this.dtpToTime);
            this.csGroupBox1.Controls.Add(this.dtpFromTime);
            this.csGroupBox1.Controls.Add(this.dtpToDate);
            this.csGroupBox1.Controls.Add(this.dtpFromDate);
            this.csGroupBox1.FooterImageIndex = -1;
            this.csGroupBox1.FooterImageKey = "";
            this.csGroupBox1.HeaderImageIndex = -1;
            this.csGroupBox1.HeaderImageKey = "";
            this.csGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.csGroupBox1.HeaderText = "";
            this.csGroupBox1.Location = new System.Drawing.Point(13, 13);
            this.csGroupBox1.Name = "csGroupBox1";
            this.csGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.csGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.csGroupBox1.Size = new System.Drawing.Size(401, 126);
            this.csGroupBox1.TabIndex = 0;
            // 
            // nmcCount
            // 
            this.nmcCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nmcCount.DecimalPlaces = 0;
            this.nmcCount.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.nmcCount.Location = new System.Drawing.Point(128, 88);
            this.nmcCount.Name = "nmcCount";
            // 
            // 
            // 
            this.nmcCount.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.nmcCount.ShowBorder = true;
            this.nmcCount.Size = new System.Drawing.Size(100, 20);
            this.nmcCount.TabIndex = 25;
            this.nmcCount.TabStop = false;
            // 
            // csLabel5
            // 
            this.csLabel5.Location = new System.Drawing.Point(13, 90);
            this.csLabel5.Name = "csLabel5";
            this.csLabel5.Size = new System.Drawing.Size(107, 18);
            this.csLabel5.TabIndex = 24;
            this.csLabel5.Text = "MaxOnlineCount";
            this.csLabel5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // csLabel3
            // 
            this.csLabel3.Location = new System.Drawing.Point(13, 27);
            this.csLabel3.Name = "csLabel3";
            this.csLabel3.Size = new System.Drawing.Size(38, 18);
            this.csLabel3.TabIndex = 23;
            this.csLabel3.Text = "From";
            this.csLabel3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // csLabel2
            // 
            this.csLabel2.Location = new System.Drawing.Point(13, 58);
            this.csLabel2.Name = "csLabel2";
            this.csLabel2.Size = new System.Drawing.Size(22, 18);
            this.csLabel2.TabIndex = 22;
            this.csLabel2.Text = "To";
            this.csLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // dtpToTime
            // 
            this.dtpToTime.AutoSize = true;
            this.dtpToTime.CalendarSize = new System.Drawing.Size(250, 191);
            this.dtpToTime.Culture = new System.Globalization.CultureInfo("zh-CN");
            this.dtpToTime.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.dtpToTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpToTime.Location = new System.Drawing.Point(234, 54);
            this.dtpToTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpToTime.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpToTime.Name = "dtpToTime";
            this.dtpToTime.NullDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpToTime.Size = new System.Drawing.Size(150, 25);
            this.dtpToTime.TabIndex = 21;
            this.dtpToTime.TabStop = false;
            this.dtpToTime.Text = "11/25/2011 14:23:24";
            this.dtpToTime.Value = new System.DateTime(2011, 11, 25, 14, 23, 24, 785);
            // 
            // dtpFromTime
            // 
            this.dtpFromTime.AutoSize = true;
            this.dtpFromTime.CalendarSize = new System.Drawing.Size(250, 191);
            this.dtpFromTime.Culture = new System.Globalization.CultureInfo("zh-CN");
            this.dtpFromTime.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.dtpFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromTime.Location = new System.Drawing.Point(234, 23);
            this.dtpFromTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromTime.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpFromTime.Name = "dtpFromTime";
            this.dtpFromTime.NullDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpFromTime.Size = new System.Drawing.Size(150, 25);
            this.dtpFromTime.TabIndex = 19;
            this.dtpFromTime.TabStop = false;
            this.dtpFromTime.Text = "11/25/2011 14:23:24";
            this.dtpFromTime.Value = new System.DateTime(2011, 11, 25, 14, 23, 24, 785);
            // 
            // dtpToDate
            // 
            this.dtpToDate.AutoSize = true;
            this.dtpToDate.CalendarSize = new System.Drawing.Size(250, 191);
            this.dtpToDate.Culture = new System.Globalization.CultureInfo("zh-CN");
            this.dtpToDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpToDate.Location = new System.Drawing.Point(78, 54);
            this.dtpToDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpToDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.NullDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpToDate.Size = new System.Drawing.Size(150, 25);
            this.dtpToDate.TabIndex = 18;
            this.dtpToDate.TabStop = false;
            this.dtpToDate.Text = "05/18/2012 10:34:35";
            this.dtpToDate.Value = new System.DateTime(2012, 5, 18, 10, 34, 35, 16);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.AutoSize = true;
            this.dtpFromDate.CalendarSize = new System.Drawing.Size(250, 191);
            this.dtpFromDate.Culture = new System.Globalization.CultureInfo("zh-CN");
            this.dtpFromDate.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromDate.Location = new System.Drawing.Point(78, 23);
            this.dtpFromDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.NullDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Size = new System.Drawing.Size(150, 25);
            this.dtpFromDate.TabIndex = 16;
            this.dtpFromDate.TabStop = false;
            this.dtpFromDate.Text = "05/18/2012 10:34:35";
            this.dtpFromDate.Value = new System.DateTime(2012, 5, 18, 10, 34, 35, 16);
            // 
            // CSOnlineUserCountSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 378);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.csGroupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CSOnlineUserCountSettingForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置时间段及最大在线用户数";
            this.Load += new System.EventHandler(this.CSOnlineUserCountSettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult.MasterGridViewTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.csGroupBox1)).EndInit();
            this.csGroupBox1.ResumeLayout(false);
            this.csGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmcCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.csLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.csLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.csLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Hys.CommonControls.CSGroupBox csGroupBox1;
        private Hys.CommonControls.CSDateTimePicker dtpToDate;
        private Hys.CommonControls.CSDateTimePicker dtpFromDate;
        private Hys.CommonControls.CSNumericTextBox nmcCount;
        private Hys.CommonControls.CSLabel csLabel5;
        private Hys.CommonControls.CSLabel csLabel3;
        private Hys.CommonControls.CSLabel csLabel2;
        private Hys.CommonControls.CSDateTimePicker dtpToTime;
        private Hys.CommonControls.CSDateTimePicker dtpFromTime;
        private Hys.CommonControls.CSButton btnCancel;
        private Hys.CommonControls.CSButton btnOK;
        private Hys.CommonControls.CSButton btnDelete;
        private Hys.CommonControls.CSButton btnAdd;
        private Hys.CommonControls.CSGridView dgvResult;
    }
}