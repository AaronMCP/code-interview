namespace HYS.Adapter.Monitor.Controls
{
    partial class UCAdvancedMode
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
            this.pTop = new System.Windows.Forms.Panel();
            this.lblChanged = new System.Windows.Forms.Label();
            this.radioBtnStatement = new System.Windows.Forms.RadioButton();
            this.radioButtonListview = new System.Windows.Forms.RadioButton();
            this.pMain = new System.Windows.Forms.Panel();
            this.pTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pTop
            // 
            this.pTop.Controls.Add(this.lblChanged);
            this.pTop.Controls.Add(this.radioBtnStatement);
            this.pTop.Controls.Add(this.radioButtonListview);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(560, 33);
            this.pTop.TabIndex = 1;
            // 
            // lblChanged
            // 
            this.lblChanged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChanged.AutoSize = true;
            this.lblChanged.BackColor = System.Drawing.SystemColors.Control;
            this.lblChanged.ForeColor = System.Drawing.Color.Red;
            this.lblChanged.Location = new System.Drawing.Point(376, 13);
            this.lblChanged.Name = "lblChanged";
            this.lblChanged.Size = new System.Drawing.Size(170, 13);
            this.lblChanged.TabIndex = 2;
            this.lblChanged.Text = "Script has been changed manually";
            this.lblChanged.Visible = false;
            // 
            // radioBtnStatement
            // 
            this.radioBtnStatement.AutoSize = true;
            this.radioBtnStatement.Location = new System.Drawing.Point(122, 9);
            this.radioBtnStatement.Name = "radioBtnStatement";
            this.radioBtnStatement.Size = new System.Drawing.Size(99, 17);
            this.radioBtnStatement.TabIndex = 1;
            this.radioBtnStatement.TabStop = true;
            this.radioBtnStatement.Text = "Statement View";
            this.radioBtnStatement.UseVisualStyleBackColor = true;
            this.radioBtnStatement.CheckedChanged += new System.EventHandler(this.radioButtonText_CheckedChanged);
            // 
            // radioButtonListview
            // 
            this.radioButtonListview.AutoSize = true;
            this.radioButtonListview.Checked = true;
            this.radioButtonListview.Location = new System.Drawing.Point(13, 9);
            this.radioButtonListview.Name = "radioButtonListview";
            this.radioButtonListview.Size = new System.Drawing.Size(67, 17);
            this.radioButtonListview.TabIndex = 0;
            this.radioButtonListview.TabStop = true;
            this.radioButtonListview.Text = "List View";
            this.radioButtonListview.UseVisualStyleBackColor = true;
            // 
            // pMain
            // 
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 33);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(560, 292);
            this.pMain.TabIndex = 2;
            // 
            // UCAdvancedMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pMain);
            this.Controls.Add(this.pTop);
            this.Name = "UCAdvancedMode";
            this.Size = new System.Drawing.Size(560, 325);
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.RadioButton radioBtnStatement;
        private System.Windows.Forms.RadioButton radioButtonListview;
        private System.Windows.Forms.Label lblChanged;

    }
}
