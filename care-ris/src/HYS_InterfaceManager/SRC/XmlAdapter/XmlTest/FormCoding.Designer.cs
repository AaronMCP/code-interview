namespace XmlTest
{
    partial class FormCoding
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSource = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonEncode = new System.Windows.Forms.Button();
            this.textBoxByte = new System.Windows.Forms.TextBox();
            this.buttonDecode = new System.Windows.Forms.Button();
            this.textBoxTarget = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source String";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSource
            // 
            this.textBoxSource.Location = new System.Drawing.Point(21, 40);
            this.textBoxSource.Multiline = true;
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSource.Size = new System.Drawing.Size(242, 199);
            this.textBoxSource.TabIndex = 1;
            this.textBoxSource.Text = "a°¡b";
            this.textBoxSource.WordWrap = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(348, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Byte Array";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonEncode
            // 
            this.buttonEncode.Location = new System.Drawing.Point(278, 100);
            this.buttonEncode.Name = "buttonEncode";
            this.buttonEncode.Size = new System.Drawing.Size(59, 57);
            this.buttonEncode.TabIndex = 3;
            this.buttonEncode.Text = "UTF-8 \r\nEncode \r\n-->\r\n";
            this.buttonEncode.UseVisualStyleBackColor = true;
            this.buttonEncode.Click += new System.EventHandler(this.buttonEncode_Click);
            // 
            // textBoxByte
            // 
            this.textBoxByte.Location = new System.Drawing.Point(351, 40);
            this.textBoxByte.Multiline = true;
            this.textBoxByte.Name = "textBoxByte";
            this.textBoxByte.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxByte.Size = new System.Drawing.Size(242, 199);
            this.textBoxByte.TabIndex = 4;
            this.textBoxByte.WordWrap = false;
            // 
            // buttonDecode
            // 
            this.buttonDecode.Location = new System.Drawing.Point(610, 100);
            this.buttonDecode.Name = "buttonDecode";
            this.buttonDecode.Size = new System.Drawing.Size(59, 57);
            this.buttonDecode.TabIndex = 5;
            this.buttonDecode.Text = "UTF-8 \r\nDecode \r\n-->\r\n";
            this.buttonDecode.UseVisualStyleBackColor = true;
            this.buttonDecode.Click += new System.EventHandler(this.buttonDecode_Click);
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.Location = new System.Drawing.Point(684, 40);
            this.textBoxTarget.Multiline = true;
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTarget.Size = new System.Drawing.Size(242, 199);
            this.textBoxTarget.TabIndex = 6;
            this.textBoxTarget.WordWrap = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(681, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "Target String";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonCut
            // 
            this.buttonCut.Location = new System.Drawing.Point(534, 16);
            this.buttonCut.Name = "buttonCut";
            this.buttonCut.Size = new System.Drawing.Size(59, 21);
            this.buttonCut.TabIndex = 8;
            this.buttonCut.Text = "Cut";
            this.buttonCut.UseVisualStyleBackColor = true;
            this.buttonCut.Click += new System.EventHandler(this.buttonCut_Click);
            // 
            // FormCoding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 263);
            this.Controls.Add(this.buttonCut);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.buttonDecode);
            this.Controls.Add(this.textBoxByte);
            this.Controls.Add(this.buttonEncode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSource);
            this.Controls.Add(this.label1);
            this.Name = "FormCoding";
            this.Text = "FormCoding";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonEncode;
        private System.Windows.Forms.TextBox textBoxByte;
        private System.Windows.Forms.Button buttonDecode;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCut;
    }
}