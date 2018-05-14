namespace SQLOutboundAdapter.Forms
{
    partial class FormConfig
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.labelMS = new System.Windows.Forms.Label();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.labelInterval = new System.Windows.Forms.Label();
            this.textBoxSQL = new System.Windows.Forms.TextBox();
            this.labelSQLStatement = new System.Windows.Forms.Label();
            this.textBoxDBCnn = new System.Windows.Forms.TextBox();
            this.labelDBCnn = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(341, 178);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(100, 26);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(456, 178);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 26);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.Controls.Add(this.labelMS);
            this.panelMain.Controls.Add(this.numericUpDownInterval);
            this.panelMain.Controls.Add(this.labelInterval);
            this.panelMain.Controls.Add(this.textBoxSQL);
            this.panelMain.Controls.Add(this.labelSQLStatement);
            this.panelMain.Controls.Add(this.textBoxDBCnn);
            this.panelMain.Controls.Add(this.labelDBCnn);
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(570, 172);
            this.panelMain.TabIndex = 9;
            // 
            // labelMS
            // 
            this.labelMS.Location = new System.Drawing.Point(265, 131);
            this.labelMS.Name = "labelMS";
            this.labelMS.Size = new System.Drawing.Size(203, 17);
            this.labelMS.TabIndex = 15;
            this.labelMS.Text = "ms";
            this.labelMS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Location = new System.Drawing.Point(130, 131);
            this.numericUpDownInterval.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(129, 20);
            this.numericUpDownInterval.TabIndex = 14;
            // 
            // labelInterval
            // 
            this.labelInterval.Location = new System.Drawing.Point(9, 133);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(203, 17);
            this.labelInterval.TabIndex = 13;
            this.labelInterval.Text = "Query interval:";
            this.labelInterval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSQL
            // 
            this.textBoxSQL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSQL.Location = new System.Drawing.Point(130, 57);
            this.textBoxSQL.Multiline = true;
            this.textBoxSQL.Name = "textBoxSQL";
            this.textBoxSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSQL.Size = new System.Drawing.Size(426, 63);
            this.textBoxSQL.TabIndex = 12;
            this.textBoxSQL.Text = "SELECT * FROM DeviceTable";
            // 
            // labelSQLStatement
            // 
            this.labelSQLStatement.Location = new System.Drawing.Point(9, 56);
            this.labelSQLStatement.Name = "labelSQLStatement";
            this.labelSQLStatement.Size = new System.Drawing.Size(203, 17);
            this.labelSQLStatement.TabIndex = 11;
            this.labelSQLStatement.Text = "SQL statement:";
            this.labelSQLStatement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDBCnn
            // 
            this.textBoxDBCnn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDBCnn.Location = new System.Drawing.Point(130, 21);
            this.textBoxDBCnn.Name = "textBoxDBCnn";
            this.textBoxDBCnn.Size = new System.Drawing.Size(426, 20);
            this.textBoxDBCnn.TabIndex = 10;
            this.textBoxDBCnn.Text = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=database.mdb";
            // 
            // labelDBCnn
            // 
            this.labelDBCnn.Location = new System.Drawing.Point(9, 22);
            this.labelDBCnn.Name = "labelDBCnn";
            this.labelDBCnn.Size = new System.Drawing.Size(203, 17);
            this.labelDBCnn.TabIndex = 9;
            this.labelDBCnn.Text = "DB connection string:";
            this.labelDBCnn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormConfig
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(568, 216);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SQL Outbound Adapter Configuration";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelMS;
        private System.Windows.Forms.NumericUpDown numericUpDownInterval;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.TextBox textBoxSQL;
        private System.Windows.Forms.Label labelSQLStatement;
        private System.Windows.Forms.TextBox textBoxDBCnn;
        private System.Windows.Forms.Label labelDBCnn;
    }
}