namespace HYS.DicomAdapter.MWLServer.Forms
{
    partial class FormAutoGenIDs
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownThread = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCount = new System.Windows.Forms.NumericUpDown();
            this.buttonStepID = new System.Windows.Forms.Button();
            this.listViewID = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.buttonGUID = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMaxLenGUID = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownMaxLenStepID = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxMACAddress = new System.Windows.Forms.TextBox();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.buttonClearList = new System.Windows.Forms.Button();
            this.buttonValidate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThread)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLenGUID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLenStepID)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(26, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Generated ID Count in Each Thread:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(26, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "Working Thread Count:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownThread
            // 
            this.numericUpDownThread.Location = new System.Drawing.Point(215, 21);
            this.numericUpDownThread.Name = "numericUpDownThread";
            this.numericUpDownThread.Size = new System.Drawing.Size(85, 20);
            this.numericUpDownThread.TabIndex = 19;
            this.numericUpDownThread.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numericUpDownCount
            // 
            this.numericUpDownCount.Location = new System.Drawing.Point(215, 47);
            this.numericUpDownCount.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.numericUpDownCount.Name = "numericUpDownCount";
            this.numericUpDownCount.Size = new System.Drawing.Size(85, 20);
            this.numericUpDownCount.TabIndex = 18;
            this.numericUpDownCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // buttonStepID
            // 
            this.buttonStepID.Location = new System.Drawing.Point(435, 54);
            this.buttonStepID.Name = "buttonStepID";
            this.buttonStepID.Size = new System.Drawing.Size(100, 33);
            this.buttonStepID.TabIndex = 17;
            this.buttonStepID.Text = "Generate Step ID";
            this.buttonStepID.UseVisualStyleBackColor = true;
            this.buttonStepID.Click += new System.EventHandler(this.buttonStepID_Click);
            // 
            // listViewID
            // 
            this.listViewID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewID.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewID.Location = new System.Drawing.Point(27, 139);
            this.listViewID.Name = "listViewID";
            this.listViewID.Size = new System.Drawing.Size(592, 233);
            this.listViewID.TabIndex = 16;
            this.listViewID.UseCompatibleStateImageBehavior = false;
            this.listViewID.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Generated ID ";
            this.columnHeader1.Width = 317;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Length";
            this.columnHeader2.Width = 47;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Thread ID";
            // 
            // buttonGUID
            // 
            this.buttonGUID.Location = new System.Drawing.Point(338, 54);
            this.buttonGUID.Name = "buttonGUID";
            this.buttonGUID.Size = new System.Drawing.Size(91, 33);
            this.buttonGUID.TabIndex = 15;
            this.buttonGUID.Text = "Generate GUID";
            this.buttonGUID.UseVisualStyleBackColor = true;
            this.buttonGUID.Click += new System.EventHandler(this.buttonGUID_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(26, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 20);
            this.label3.TabIndex = 23;
            this.label3.Text = "Max Length Limitation of GUID:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownMaxLenGUID
            // 
            this.numericUpDownMaxLenGUID.Location = new System.Drawing.Point(215, 73);
            this.numericUpDownMaxLenGUID.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDownMaxLenGUID.Name = "numericUpDownMaxLenGUID";
            this.numericUpDownMaxLenGUID.Size = new System.Drawing.Size(85, 20);
            this.numericUpDownMaxLenGUID.TabIndex = 22;
            this.numericUpDownMaxLenGUID.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(26, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 20);
            this.label4.TabIndex = 25;
            this.label4.Text = "Max Length Limitation of Step ID:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownMaxLenStepID
            // 
            this.numericUpDownMaxLenStepID.Location = new System.Drawing.Point(215, 99);
            this.numericUpDownMaxLenStepID.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownMaxLenStepID.Name = "numericUpDownMaxLenStepID";
            this.numericUpDownMaxLenStepID.Size = new System.Drawing.Size(85, 20);
            this.numericUpDownMaxLenStepID.TabIndex = 24;
            this.numericUpDownMaxLenStepID.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(335, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 20);
            this.label5.TabIndex = 26;
            this.label5.Text = "Encoded Local MAC Address:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMACAddress
            // 
            this.textBoxMACAddress.Location = new System.Drawing.Point(497, 22);
            this.textBoxMACAddress.Name = "textBoxMACAddress";
            this.textBoxMACAddress.ReadOnly = true;
            this.textBoxMACAddress.Size = new System.Drawing.Size(122, 20);
            this.textBoxMACAddress.TabIndex = 27;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Average Time per ID (ms)";
            this.columnHeader4.Width = 134;
            // 
            // buttonClearList
            // 
            this.buttonClearList.Location = new System.Drawing.Point(541, 54);
            this.buttonClearList.Name = "buttonClearList";
            this.buttonClearList.Size = new System.Drawing.Size(78, 33);
            this.buttonClearList.TabIndex = 28;
            this.buttonClearList.Text = "Clear List";
            this.buttonClearList.UseVisualStyleBackColor = true;
            this.buttonClearList.Click += new System.EventHandler(this.buttonClearList_Click);
            // 
            // buttonValidate
            // 
            this.buttonValidate.Location = new System.Drawing.Point(338, 93);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.Size = new System.Drawing.Size(281, 26);
            this.buttonValidate.TabIndex = 29;
            this.buttonValidate.Text = "Is there duplicated ID in the list?";
            this.buttonValidate.UseVisualStyleBackColor = true;
            this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
            // 
            // FormAutoGenIDs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 396);
            this.Controls.Add(this.buttonValidate);
            this.Controls.Add(this.buttonClearList);
            this.Controls.Add(this.textBoxMACAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownMaxLenStepID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownMaxLenGUID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownThread);
            this.Controls.Add(this.numericUpDownCount);
            this.Controls.Add(this.buttonStepID);
            this.Controls.Add(this.listViewID);
            this.Controls.Add(this.buttonGUID);
            this.Name = "FormAutoGenIDs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ID Generation Testing";
            this.Load += new System.EventHandler(this.FormAutoGenIDs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThread)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLenGUID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxLenStepID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownThread;
        private System.Windows.Forms.NumericUpDown numericUpDownCount;
        private System.Windows.Forms.Button buttonStepID;
        private System.Windows.Forms.ListView listViewID;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button buttonGUID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxLenGUID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxLenStepID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxMACAddress;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonClearList;
        private System.Windows.Forms.Button buttonValidate;
    }
}