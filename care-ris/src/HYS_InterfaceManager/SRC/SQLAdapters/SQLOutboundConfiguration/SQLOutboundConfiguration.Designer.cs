namespace HYS.SQLOutboundAdapterConfiguration
{
    partial class SQLOutboundConfiguration
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
            this.panelBottom = new System.Windows.Forms.Panel();
            this.groupBoxLineDown = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panelGenaeal = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioBtnPassive = new System.Windows.Forms.RadioButton();
            this.radioBtnActive = new System.Windows.Forms.RadioButton();
            this.pContainer = new System.Windows.Forms.Panel();
            this.panelBottom.SuspendLayout();
            this.panelGenaeal.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.groupBoxLineDown);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnOK);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 519);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(792, 40);
            this.panelBottom.TabIndex = 2;
            // 
            // groupBoxLineDown
            // 
            this.groupBoxLineDown.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxLineDown.Location = new System.Drawing.Point(0, 0);
            this.groupBoxLineDown.Name = "groupBoxLineDown";
            this.groupBoxLineDown.Size = new System.Drawing.Size(792, 3);
            this.groupBoxLineDown.TabIndex = 0;
            this.groupBoxLineDown.TabStop = false;
            this.groupBoxLineDown.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(699, 9);
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
            this.btnOK.Location = new System.Drawing.Point(613, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelGenaeal
            // 
            this.panelGenaeal.Controls.Add(this.groupBox1);
            this.panelGenaeal.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGenaeal.Location = new System.Drawing.Point(0, 0);
            this.panelGenaeal.Name = "panelGenaeal";
            this.panelGenaeal.Size = new System.Drawing.Size(792, 88);
            this.panelGenaeal.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioBtnPassive);
            this.groupBox1.Controls.Add(this.radioBtnActive);
            this.groupBox1.Location = new System.Drawing.Point(15, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Outbound Mode";
            // 
            // radioBtnPassive
            // 
            this.radioBtnPassive.AutoSize = true;
            this.radioBtnPassive.Location = new System.Drawing.Point(23, 48);
            this.radioBtnPassive.Name = "radioBtnPassive";
            this.radioBtnPassive.Size = new System.Drawing.Size(438, 17);
            this.radioBtnPassive.TabIndex = 1;
            this.radioBtnPassive.TabStop = true;
            this.radioBtnPassive.Text = "Passive Mode   (3rd Party Application Query/Retrive Data From GC Gateway Database" +
                ")";
            this.radioBtnPassive.UseVisualStyleBackColor = true;
            this.radioBtnPassive.CheckedChanged += new System.EventHandler(this.radioBtnPassive_CheckedChanged);
            // 
            // radioBtnActive
            // 
            this.radioBtnActive.AutoSize = true;
            this.radioBtnActive.Location = new System.Drawing.Point(23, 22);
            this.radioBtnActive.Name = "radioBtnActive";
            this.radioBtnActive.Size = new System.Drawing.Size(340, 17);
            this.radioBtnActive.TabIndex = 0;
            this.radioBtnActive.TabStop = true;
            this.radioBtnActive.Text = "Active Mode     (GC Gateway Write Data to 3rd Party Data Source)";
            this.radioBtnActive.UseVisualStyleBackColor = true;
            // 
            // pContainer
            // 
            this.pContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pContainer.Location = new System.Drawing.Point(0, 88);
            this.pContainer.Name = "pContainer";
            this.pContainer.Size = new System.Drawing.Size(792, 431);
            this.pContainer.TabIndex = 1;
            // 
            // SQLOutboundConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 559);
            this.Controls.Add(this.pContainer);
            this.Controls.Add(this.panelGenaeal);
            this.Controls.Add(this.panelBottom);
            this.Name = "SQLOutboundConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Destination";
            this.panelBottom.ResumeLayout(false);
            this.panelGenaeal.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.GroupBox groupBoxLineDown;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panelGenaeal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pContainer;
        private System.Windows.Forms.RadioButton radioBtnPassive;
        private System.Windows.Forms.RadioButton radioBtnActive;
    }
}