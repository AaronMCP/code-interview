namespace HYS.DicomAdapter.MWLServer.Forms
{
    partial class FormService
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
            this.buttonMWLStart = new System.Windows.Forms.Button();
            this.buttonMWLStop = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonMWLStart
            // 
            this.buttonMWLStart.Location = new System.Drawing.Point(26, 23);
            this.buttonMWLStart.Name = "buttonMWLStart";
            this.buttonMWLStart.Size = new System.Drawing.Size(139, 32);
            this.buttonMWLStart.TabIndex = 0;
            this.buttonMWLStart.Text = "Start Worklist SCP";
            this.buttonMWLStart.UseVisualStyleBackColor = true;
            this.buttonMWLStart.Click += new System.EventHandler(this.buttonMWLStart_Click);
            // 
            // buttonMWLStop
            // 
            this.buttonMWLStop.Location = new System.Drawing.Point(26, 75);
            this.buttonMWLStop.Name = "buttonMWLStop";
            this.buttonMWLStop.Size = new System.Drawing.Size(139, 32);
            this.buttonMWLStop.TabIndex = 1;
            this.buttonMWLStop.Text = "Stop Worklist SCP";
            this.buttonMWLStop.UseVisualStyleBackColor = true;
            this.buttonMWLStop.Click += new System.EventHandler(this.buttonMWLStop_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(193, 26);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(424, 300);
            this.textBox1.TabIndex = 2;
            this.textBox1.WordWrap = false;
            // 
            // FormService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 360);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonMWLStop);
            this.Controls.Add(this.buttonMWLStart);
            this.Name = "FormService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Worklist SCP Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMWLStart;
        private System.Windows.Forms.Button buttonMWLStop;
        private System.Windows.Forms.TextBox textBox1;
    }
}