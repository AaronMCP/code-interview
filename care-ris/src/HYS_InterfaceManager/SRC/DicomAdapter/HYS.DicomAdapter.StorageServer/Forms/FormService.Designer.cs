namespace HYS.DicomAdapter.StorageServer.Forms
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonMWLStop = new System.Windows.Forms.Button();
            this.buttonMWLStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(197, 30);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(424, 300);
            this.textBox1.TabIndex = 8;
            this.textBox1.WordWrap = false;
            // 
            // buttonMWLStop
            // 
            this.buttonMWLStop.Location = new System.Drawing.Point(28, 83);
            this.buttonMWLStop.Name = "buttonMWLStop";
            this.buttonMWLStop.Size = new System.Drawing.Size(139, 32);
            this.buttonMWLStop.TabIndex = 7;
            this.buttonMWLStop.Text = "Stop Storage SCP";
            this.buttonMWLStop.UseVisualStyleBackColor = true;
            this.buttonMWLStop.Click += new System.EventHandler(this.buttonMWLStop_Click);
            // 
            // buttonMWLStart
            // 
            this.buttonMWLStart.Location = new System.Drawing.Point(28, 30);
            this.buttonMWLStart.Name = "buttonMWLStart";
            this.buttonMWLStart.Size = new System.Drawing.Size(139, 32);
            this.buttonMWLStart.TabIndex = 6;
            this.buttonMWLStart.Text = "Start Storage SCP";
            this.buttonMWLStart.UseVisualStyleBackColor = true;
            this.buttonMWLStart.Click += new System.EventHandler(this.buttonMWLStart_Click);
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
            this.Text = "Storage SCP Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonMWLStop;
        private System.Windows.Forms.Button buttonMWLStart;
    }
}