namespace OutboundDBInstall
{
    partial class formFilter
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
            this.components = new System.ComponentModel.Container();
            this.gBoxFilterEdior = new System.Windows.Forms.GroupBox();
            this.BtnDel = new System.Windows.Forms.Button();
            this.tViewField = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gBoxEditFilter = new System.Windows.Forms.GroupBox();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.tBoxLogicValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbBoxLogic = new System.Windows.Forms.ComboBox();
            this.tViewFilter = new System.Windows.Forms.TreeView();
            this.cMenuStripFilter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOr = new System.Windows.Forms.Button();
            this.btnAnd = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gBoxFilterEdior.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gBoxEditFilter.SuspendLayout();
            this.cMenuStripFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBoxFilterEdior
            // 
            this.gBoxFilterEdior.Controls.Add(this.BtnDel);
            this.gBoxFilterEdior.Controls.Add(this.tViewField);
            this.gBoxFilterEdior.Controls.Add(this.groupBox1);
            this.gBoxFilterEdior.Controls.Add(this.btnOr);
            this.gBoxFilterEdior.Controls.Add(this.btnAnd);
            this.gBoxFilterEdior.Dock = System.Windows.Forms.DockStyle.Top;
            this.gBoxFilterEdior.Location = new System.Drawing.Point(0, 0);
            this.gBoxFilterEdior.Margin = new System.Windows.Forms.Padding(4);
            this.gBoxFilterEdior.Name = "gBoxFilterEdior";
            this.gBoxFilterEdior.Padding = new System.Windows.Forms.Padding(4);
            this.gBoxFilterEdior.Size = new System.Drawing.Size(648, 446);
            this.gBoxFilterEdior.TabIndex = 1;
            this.gBoxFilterEdior.TabStop = false;
            this.gBoxFilterEdior.Text = "Filter Editor";
            // 
            // BtnDel
            // 
            this.BtnDel.Location = new System.Drawing.Point(279, 294);
            this.BtnDel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.Size = new System.Drawing.Size(68, 26);
            this.BtnDel.TabIndex = 5;
            this.BtnDel.Text = "Del <<";
            this.BtnDel.UseVisualStyleBackColor = true;
            this.BtnDel.Click += new System.EventHandler(this.DeleteFilter_Click);
            // 
            // tViewField
            // 
            this.tViewField.Dock = System.Windows.Forms.DockStyle.Left;
            this.tViewField.FullRowSelect = true;
            this.tViewField.Location = new System.Drawing.Point(4, 22);
            this.tViewField.Margin = new System.Windows.Forms.Padding(4);
            this.tViewField.Name = "tViewField";
            this.tViewField.Size = new System.Drawing.Size(267, 420);
            this.tViewField.TabIndex = 4;
            this.tViewField.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tViewField_AfterSelect);
            this.tViewField.Click += new System.EventHandler(this.tViewField_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gBoxEditFilter);
            this.groupBox1.Controls.Add(this.tViewFilter);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(356, 22);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(288, 420);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // gBoxEditFilter
            // 
            this.gBoxEditFilter.Controls.Add(this.btnApplyFilter);
            this.gBoxEditFilter.Controls.Add(this.tBoxLogicValue);
            this.gBoxEditFilter.Controls.Add(this.label2);
            this.gBoxEditFilter.Controls.Add(this.label1);
            this.gBoxEditFilter.Controls.Add(this.cbBoxLogic);
            this.gBoxEditFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBoxEditFilter.Location = new System.Drawing.Point(4, 232);
            this.gBoxEditFilter.Margin = new System.Windows.Forms.Padding(4);
            this.gBoxEditFilter.Name = "gBoxEditFilter";
            this.gBoxEditFilter.Padding = new System.Windows.Forms.Padding(4);
            this.gBoxEditFilter.Size = new System.Drawing.Size(280, 184);
            this.gBoxEditFilter.TabIndex = 1;
            this.gBoxEditFilter.TabStop = false;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Location = new System.Drawing.Point(43, 151);
            this.btnApplyFilter.Margin = new System.Windows.Forms.Padding(4);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(100, 26);
            this.btnApplyFilter.TabIndex = 4;
            this.btnApplyFilter.Text = "Apply";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            // 
            // tBoxLogicValue
            // 
            this.tBoxLogicValue.Location = new System.Drawing.Point(43, 102);
            this.tBoxLogicValue.Margin = new System.Windows.Forms.Padding(4);
            this.tBoxLogicValue.Name = "tBoxLogicValue";
            this.tBoxLogicValue.Size = new System.Drawing.Size(216, 25);
            this.tBoxLogicValue.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Value";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Logic Operator";
            // 
            // cbBoxLogic
            // 
            this.cbBoxLogic.FormattingEnabled = true;
            this.cbBoxLogic.Items.AddRange(new object[] {
            "=",
            "<>",
            ">",
            "<",
            ">=",
            "<=",
            "LIKE"});
            this.cbBoxLogic.Location = new System.Drawing.Point(43, 40);
            this.cbBoxLogic.Margin = new System.Windows.Forms.Padding(4);
            this.cbBoxLogic.Name = "cbBoxLogic";
            this.cbBoxLogic.Size = new System.Drawing.Size(216, 23);
            this.cbBoxLogic.TabIndex = 0;
            // 
            // tViewFilter
            // 
            this.tViewFilter.ContextMenuStrip = this.cMenuStripFilter;
            this.tViewFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.tViewFilter.Location = new System.Drawing.Point(4, 22);
            this.tViewFilter.Margin = new System.Windows.Forms.Padding(4);
            this.tViewFilter.Name = "tViewFilter";
            this.tViewFilter.Size = new System.Drawing.Size(280, 210);
            this.tViewFilter.TabIndex = 0;
            this.tViewFilter.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tViewFilter_AfterSelect);
            this.tViewFilter.Click += new System.EventHandler(this.tViewFilter_Click);
            // 
            // cMenuStripFilter
            // 
            this.cMenuStripFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteFilter});
            this.cMenuStripFilter.Name = "cMenuStripFilter";
            this.cMenuStripFilter.Size = new System.Drawing.Size(168, 28);
            this.cMenuStripFilter.Opening += new System.ComponentModel.CancelEventHandler(this.cMenuStripFilter_Opening);
            // 
            // DeleteFilter
            // 
            this.DeleteFilter.Name = "DeleteFilter";
            this.DeleteFilter.Size = new System.Drawing.Size(167, 24);
            this.DeleteFilter.Text = "Delete Filter";
            this.DeleteFilter.Click += new System.EventHandler(this.DeleteFilter_Click);
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(280, 199);
            this.btnOr.Margin = new System.Windows.Forms.Padding(4);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(68, 26);
            this.btnOr.TabIndex = 2;
            this.btnOr.Text = "Or >>";
            this.btnOr.UseVisualStyleBackColor = true;
            this.btnOr.Click += new System.EventHandler(this.btnOr_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.Location = new System.Drawing.Point(280, 149);
            this.btnAnd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(68, 26);
            this.btnAnd.TabIndex = 1;
            this.btnAnd.Text = "And >>";
            this.btnAnd.UseVisualStyleBackColor = true;
            this.btnAnd.Click += new System.EventHandler(this.btnAnd_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(109, 454);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 26);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(312, 454);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 26);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // formFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 495);
            this.Controls.Add(this.gBoxFilterEdior);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "formFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter";
            this.Load += new System.EventHandler(this.formFilter_Load);
            this.gBoxFilterEdior.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gBoxEditFilter.ResumeLayout(false);
            this.gBoxEditFilter.PerformLayout();
            this.cMenuStripFilter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gBoxFilterEdior;
        private System.Windows.Forms.Button btnAnd;
        private System.Windows.Forms.Button btnOr;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView tViewFilter;
        private System.Windows.Forms.GroupBox gBoxEditFilter;
        private System.Windows.Forms.TextBox tBoxLogicValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBoxLogic;
        private System.Windows.Forms.TreeView tViewField;
        private System.Windows.Forms.ContextMenuStrip cMenuStripFilter;
        private System.Windows.Forms.ToolStripMenuItem DeleteFilter;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button BtnDel;

    }
}