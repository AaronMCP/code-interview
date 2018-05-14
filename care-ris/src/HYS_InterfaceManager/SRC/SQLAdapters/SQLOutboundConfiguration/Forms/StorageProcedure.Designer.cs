namespace HYS.SQLOutboundAdapterConfiguration.Forms
{
    partial class StorageProcedure
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelName = new System.Windows.Forms.Panel();
            this.txtSPName = new System.Windows.Forms.TextBox();
            this.radioBtnStatement = new System.Windows.Forms.RadioButton();
            this.lblExist = new System.Windows.Forms.Label();
            this.radioBtnParameter = new System.Windows.Forms.RadioButton();
            this.lblSPName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panelName.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panelMain);
            this.groupBox1.Controls.Add(this.panelName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(763, 516);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Storage Procedure";
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(3, 83);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(757, 430);
            this.panelMain.TabIndex = 1;
            // 
            // panelName
            // 
            this.panelName.Controls.Add(this.txtSPName);
            this.panelName.Controls.Add(this.radioBtnStatement);
            this.panelName.Controls.Add(this.lblExist);
            this.panelName.Controls.Add(this.radioBtnParameter);
            this.panelName.Controls.Add(this.lblSPName);
            this.panelName.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelName.Location = new System.Drawing.Point(3, 16);
            this.panelName.Name = "panelName";
            this.panelName.Size = new System.Drawing.Size(757, 67);
            this.panelName.TabIndex = 0;
            // 
            // txtSPName
            // 
            this.txtSPName.Location = new System.Drawing.Point(163, 16);
            this.txtSPName.Name = "txtSPName";
            this.txtSPName.Size = new System.Drawing.Size(582, 20);
            this.txtSPName.TabIndex = 0;
            this.txtSPName.MouseLeave += new System.EventHandler(this.txtSPName_MouseLeave);
            this.txtSPName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtSPName_MouseClick);
            this.txtSPName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtSPName_MouseDoubleClick);
            this.txtSPName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSPName_KeyUp);
            this.txtSPName.TextChanged += new System.EventHandler(this.txtSPName_TextChanged);
            this.txtSPName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSPName_KeyDown);
            // 
            // radioBtnStatement
            // 
            this.radioBtnStatement.AutoSize = true;
            this.radioBtnStatement.Location = new System.Drawing.Point(163, 51);
            this.radioBtnStatement.Name = "radioBtnStatement";
            this.radioBtnStatement.Size = new System.Drawing.Size(99, 17);
            this.radioBtnStatement.TabIndex = 76;
            this.radioBtnStatement.Text = "Statement View";
            this.radioBtnStatement.UseVisualStyleBackColor = true;
            this.radioBtnStatement.CheckedChanged += new System.EventHandler(this.radioBtnStatement_CheckedChanged);
            // 
            // lblExist
            // 
            this.lblExist.AutoSize = true;
            this.lblExist.ForeColor = System.Drawing.Color.Red;
            this.lblExist.Location = new System.Drawing.Point(160, 38);
            this.lblExist.Name = "lblExist";
            this.lblExist.Size = new System.Drawing.Size(75, 13);
            this.lblExist.TabIndex = 62;
            this.lblExist.Text = "Name Existed!";
            this.lblExist.Visible = false;
            // 
            // radioBtnParameter
            // 
            this.radioBtnParameter.AutoSize = true;
            this.radioBtnParameter.Checked = true;
            this.radioBtnParameter.Location = new System.Drawing.Point(18, 51);
            this.radioBtnParameter.Name = "radioBtnParameter";
            this.radioBtnParameter.Size = new System.Drawing.Size(99, 17);
            this.radioBtnParameter.TabIndex = 75;
            this.radioBtnParameter.TabStop = true;
            this.radioBtnParameter.Text = "Parameter View";
            this.radioBtnParameter.UseVisualStyleBackColor = true;
            // 
            // lblSPName
            // 
            this.lblSPName.AutoSize = true;
            this.lblSPName.Location = new System.Drawing.Point(15, 19);
            this.lblSPName.Margin = new System.Windows.Forms.Padding(0);
            this.lblSPName.Name = "lblSPName";
            this.lblSPName.Size = new System.Drawing.Size(127, 13);
            this.lblSPName.TabIndex = 60;
            this.lblSPName.Text = "Storage Procedure Name";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(692, 535);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(606, 535);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // StorageProcedure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "StorageProcedure";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox1.ResumeLayout(false);
            this.panelName.ResumeLayout(false);
            this.panelName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton radioBtnStatement;
        private System.Windows.Forms.RadioButton radioBtnParameter;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelName;
        private System.Windows.Forms.Label lblExist;
        private System.Windows.Forms.Label lblSPName;
        private System.Windows.Forms.TextBox txtSPName;
    }
}