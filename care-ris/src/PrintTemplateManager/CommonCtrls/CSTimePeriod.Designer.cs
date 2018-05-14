namespace Hys.CommonControls
{
    partial class CSTimePeriod
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
            if (disposing)
            {
                this.dtpBegin.KeyDown -= ListenKeyEvent;
                this.dtpEnd.KeyDown -= ListenKeyEvent;
                this.dtpBegin.MouseDown -= ListenMouseEvent;
                this.dtpEnd.MouseDown -= ListenMouseEvent;
            }

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
            this.dtpBegin = new Hys.CommonControls.CSDateTimePickerNullable();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.dtpEnd = new Hys.CommonControls.CSDateTimePickerNullable();
            this.SuspendLayout();
            // 
            // dtpBegin
            // 
            this.dtpBegin.CustomFormat = "HH:mm";
            this.dtpBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBegin.Location = new System.Drawing.Point(0, 0);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Nullable = true;
            this.dtpBegin.ShowUpDown = true;
            this.dtpBegin.Size = new System.Drawing.Size(65, 20);
            this.dtpBegin.TabIndex = 0;
            // 
            // lblSeparator
            // 
            this.lblSeparator.AutoSize = true;
            this.lblSeparator.Location = new System.Drawing.Point(65, 3);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(14, 13);
            this.lblSeparator.TabIndex = 1;
            this.lblSeparator.Text = "~";
            this.lblSeparator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "HH:mm";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(79, 0);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Nullable = true;
            this.dtpEnd.ShowUpDown = true;
            this.dtpEnd.Size = new System.Drawing.Size(65, 20);
            this.dtpEnd.TabIndex = 2;
            // 
            // FormTimePeriod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.lblSeparator);
            this.Controls.Add(this.dtpBegin);
            this.Name = "CSTimePeriod";
            this.Size = new System.Drawing.Size(144, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Hys.CommonControls.CSDateTimePickerNullable dtpBegin;
        private System.Windows.Forms.Label lblSeparator;
        private Hys.CommonControls.CSDateTimePickerNullable dtpEnd;
    }
}
