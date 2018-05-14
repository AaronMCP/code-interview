namespace HYS.SQLInboundAdapterConfiguration
{
    partial class SQLInboundConfiguration
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panelInteract = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioBtnPassive = new System.Windows.Forms.RadioButton();
            this.radioBtnActive = new System.Windows.Forms.RadioButton();
            this.pContainer = new System.Windows.Forms.Panel();
            this.radioBtnAccess = new System.Windows.Forms.RadioButton();
            this.panelBottom.SuspendLayout();
            this.panelInteract.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnOK);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 545);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(792, 40);
            this.panelBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(699, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(613, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelInteract
            // 
            this.panelInteract.Controls.Add(this.groupBox1);
            this.panelInteract.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInteract.Location = new System.Drawing.Point(0, 0);
            this.panelInteract.Name = "panelInteract";
            this.panelInteract.Size = new System.Drawing.Size(792, 119);
            this.panelInteract.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioBtnAccess);
            this.groupBox1.Controls.Add(this.radioBtnPassive);
            this.groupBox1.Controls.Add(this.radioBtnActive);
            this.groupBox1.Location = new System.Drawing.Point(15, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(765, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Inbound Mode";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // radioBtnPassive
            // 
            this.radioBtnPassive.AutoSize = true;
            this.radioBtnPassive.Location = new System.Drawing.Point(23, 48);
            this.radioBtnPassive.Name = "radioBtnPassive";
            this.radioBtnPassive.Size = new System.Drawing.Size(400, 17);
            this.radioBtnPassive.TabIndex = 1;
            this.radioBtnPassive.Text = "Passive Mode   (Third Party Application Save Data Into GC Gateway Database)";
            this.radioBtnPassive.UseVisualStyleBackColor = true;
            this.radioBtnPassive.CheckedChanged += new System.EventHandler(this.radioBtnPassive_CheckedChanged);
            // 
            // radioBtnActive
            // 
            this.radioBtnActive.AutoSize = true;
            this.radioBtnActive.Checked = true;
            this.radioBtnActive.Location = new System.Drawing.Point(23, 22);
            this.radioBtnActive.Name = "radioBtnActive";
            this.radioBtnActive.Size = new System.Drawing.Size(405, 17);
            this.radioBtnActive.TabIndex = 0;
            this.radioBtnActive.TabStop = true;
            this.radioBtnActive.Text = "Active Mode   (GC Gateway Query/Retrieve Data From Third Party Data Source)";
            this.radioBtnActive.UseVisualStyleBackColor = true;
            this.radioBtnActive.CheckedChanged += new System.EventHandler(this.radioBtnPassive_CheckedChanged);
            // 
            // pContainer
            // 
            this.pContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pContainer.Location = new System.Drawing.Point(0, 119);
            this.pContainer.Name = "pContainer";
            this.pContainer.Size = new System.Drawing.Size(792, 426);
            this.pContainer.TabIndex = 1;
            // 
            // radioBtnAccess
            // 
            this.radioBtnAccess.AutoSize = true;
            this.radioBtnAccess.Location = new System.Drawing.Point(23, 74);
            this.radioBtnAccess.Name = "radioBtnAccess";
            this.radioBtnAccess.Size = new System.Drawing.Size(238, 17);
            this.radioBtnAccess.TabIndex = 2;
            this.radioBtnAccess.Text = "Access Mode   (Load Third Party Application)";
            this.radioBtnAccess.UseVisualStyleBackColor = true;
            this.radioBtnAccess.CheckedChanged += new System.EventHandler(this.radioBtnPassive_CheckedChanged);
            // 
            // SQLInboundConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 585);
            this.Controls.Add(this.pContainer);
            this.Controls.Add(this.panelInteract);
            this.Controls.Add(this.panelBottom);
            this.Name = "SQLInboundConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Source";
            this.panelBottom.ResumeLayout(false);
            this.panelInteract.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panelInteract;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pContainer;
        private System.Windows.Forms.RadioButton radioBtnPassive;
        private System.Windows.Forms.RadioButton radioBtnActive;
        private System.Windows.Forms.RadioButton radioBtnAccess;
    }
}