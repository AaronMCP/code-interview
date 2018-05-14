namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Forms
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
            System.Windows.Forms.Label label4;
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxSQLOut = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDBCnn = new System.Windows.Forms.TextBox();
            this.buttonDBTest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTimeInterval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonDispatch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTimeInterval)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(20, 145);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(346, 13);
            label4.TabIndex = 34;
            label4.Text = "The time interval for reading data from CS Broker SQL Server Database:";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(400, 370);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(67, 25);
            this.buttonOK.TabIndex = 29;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(485, 370);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(67, 25);
            this.buttonCancel.TabIndex = 28;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxSQLOut
            // 
            this.textBoxSQLOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSQLOut.Location = new System.Drawing.Point(23, 108);
            this.textBoxSQLOut.Name = "textBoxSQLOut";
            this.textBoxSQLOut.Size = new System.Drawing.Size(415, 20);
            this.textBoxSQLOut.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 20);
            this.label1.TabIndex = 40;
            this.label1.Text = "CS Broker Passive SQL Outbound Interface Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDBCnn
            // 
            this.textBoxDBCnn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDBCnn.Location = new System.Drawing.Point(23, 45);
            this.textBoxDBCnn.Name = "textBoxDBCnn";
            this.textBoxDBCnn.Size = new System.Drawing.Size(415, 20);
            this.textBoxDBCnn.TabIndex = 36;
            // 
            // buttonDBTest
            // 
            this.buttonDBTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDBTest.Location = new System.Drawing.Point(444, 40);
            this.buttonDBTest.Name = "buttonDBTest";
            this.buttonDBTest.Size = new System.Drawing.Size(67, 25);
            this.buttonDBTest.TabIndex = 38;
            this.buttonDBTest.Text = "Test";
            this.buttonDBTest.UseVisualStyleBackColor = true;
            this.buttonDBTest.Click += new System.EventHandler(this.buttonDBTest_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(350, 20);
            this.label2.TabIndex = 37;
            this.label2.Text = "CS Broker SQL Server Database Connection String:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxTimeInterval
            // 
            this.textBoxTimeInterval.Location = new System.Drawing.Point(23, 169);
            this.textBoxTimeInterval.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.textBoxTimeInterval.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.textBoxTimeInterval.Name = "textBoxTimeInterval";
            this.textBoxTimeInterval.Size = new System.Drawing.Size(102, 20);
            this.textBoxTimeInterval.TabIndex = 41;
            this.textBoxTimeInterval.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "millisecond";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonDBTest);
            this.groupBox1.Controls.Add(this.textBoxTimeInterval);
            this.groupBox1.Controls.Add(this.textBoxDBCnn);
            this.groupBox1.Controls.Add(this.textBoxSQLOut);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(label4);
            this.groupBox1.Location = new System.Drawing.Point(22, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 210);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database And Timer Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonDispatch);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(22, 250);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(530, 103);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Broker Message Processing";
            // 
            // buttonDispatch
            // 
            this.buttonDispatch.Location = new System.Drawing.Point(23, 58);
            this.buttonDispatch.Name = "buttonDispatch";
            this.buttonDispatch.Size = new System.Drawing.Size(187, 24);
            this.buttonDispatch.TabIndex = 27;
            this.buttonDispatch.Text = "Message Dispatching Setting";
            this.buttonDispatch.UseVisualStyleBackColor = true;
            this.buttonDispatch.Click += new System.EventHandler(this.buttonDispatch_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.Location = new System.Drawing.Point(20, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(490, 20);
            this.label7.TabIndex = 26;
            this.label7.Text = "The following rule determine how to dispatch incoming message to request or publi" +
                "sh channel";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 414);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.MinimumSize = new System.Drawing.Size(441, 220);
            this.Name = "FormConfig";
            this.Text = "CS Broker Inbound Adapter Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.textBoxTimeInterval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxSQLOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDBCnn;
        private System.Windows.Forms.Button buttonDBTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown textBoxTimeInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonDispatch;
        private System.Windows.Forms.Label label7;
    }
}