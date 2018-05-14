namespace HYS.Common.HL7v2Test
{
    partial class Form1
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
            this.rtBoxHL7 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.rtBoxXML = new System.Windows.Forms.RichTextBox();
            this.cmBoxHL7File = new System.Windows.Forms.ComboBox();
            this.btnConvertHL7 = new System.Windows.Forms.Button();
            this.rtBoxHL72 = new System.Windows.Forms.RichTextBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtBoxHL7
            // 
            this.rtBoxHL7.Location = new System.Drawing.Point(12, 55);
            this.rtBoxHL7.Name = "rtBoxHL7";
            this.rtBoxHL7.Size = new System.Drawing.Size(235, 300);
            this.rtBoxHL7.TabIndex = 0;
            this.rtBoxHL7.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "HL7 Message";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(253, 55);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(94, 23);
            this.btnConvert.TabIndex = 2;
            this.btnConvert.Text = "Convert to XML";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // rtBoxXML
            // 
            this.rtBoxXML.Location = new System.Drawing.Point(353, 57);
            this.rtBoxXML.Name = "rtBoxXML";
            this.rtBoxXML.Size = new System.Drawing.Size(218, 298);
            this.rtBoxXML.TabIndex = 3;
            this.rtBoxXML.Text = "";
            // 
            // cmBoxHL7File
            // 
            this.cmBoxHL7File.FormattingEnabled = true;
            this.cmBoxHL7File.Location = new System.Drawing.Point(12, 12);
            this.cmBoxHL7File.Name = "cmBoxHL7File";
            this.cmBoxHL7File.Size = new System.Drawing.Size(295, 21);
            this.cmBoxHL7File.TabIndex = 4;
            this.cmBoxHL7File.SelectedIndexChanged += new System.EventHandler(this.cmBoxHL7File_SelectedIndexChanged);
            // 
            // btnConvertHL7
            // 
            this.btnConvertHL7.Location = new System.Drawing.Point(590, 57);
            this.btnConvertHL7.Name = "btnConvertHL7";
            this.btnConvertHL7.Size = new System.Drawing.Size(94, 23);
            this.btnConvertHL7.TabIndex = 5;
            this.btnConvertHL7.Text = "Convert to HL7";
            this.btnConvertHL7.UseVisualStyleBackColor = true;
            this.btnConvertHL7.Click += new System.EventHandler(this.btnConvertHL7_Click);
            // 
            // rtBoxHL72
            // 
            this.rtBoxHL72.Location = new System.Drawing.Point(690, 55);
            this.rtBoxHL72.Name = "rtBoxHL72";
            this.rtBoxHL72.Size = new System.Drawing.Size(218, 298);
            this.rtBoxHL72.TabIndex = 6;
            this.rtBoxHL72.Text = "";
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(12, 387);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(94, 23);
            this.btnCompare.TabIndex = 7;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(598, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 482);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.rtBoxHL72);
            this.Controls.Add(this.btnConvertHL7);
            this.Controls.Add(this.cmBoxHL7File);
            this.Controls.Add(this.rtBoxXML);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtBoxHL7);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtBoxHL7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.RichTextBox rtBoxXML;
        private System.Windows.Forms.ComboBox cmBoxHL7File;
        private System.Windows.Forms.Button btnConvertHL7;
        private System.Windows.Forms.RichTextBox rtBoxHL72;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

