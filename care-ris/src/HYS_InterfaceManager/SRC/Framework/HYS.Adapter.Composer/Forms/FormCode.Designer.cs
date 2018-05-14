namespace HYS.Adapter.Composer.Forms
{
    partial class FormCode
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
            this.textBoxWord = new System.Windows.Forms.TextBox();
            this.checkBoxWrap = new System.Windows.Forms.CheckBox();
            this.buttonBIG5_GB = new System.Windows.Forms.Button();
            this.buttonGB_BIG5 = new System.Windows.Forms.Button();
            this.buttonBIG5_GBK = new System.Windows.Forms.Button();
            this.buttonGBK_GB = new System.Windows.Forms.Button();
            this.buttonGB_GBK = new System.Windows.Forms.Button();
            this.buttonGBK_BIG5 = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxWord
            // 
            this.textBoxWord.Location = new System.Drawing.Point(12, 12);
            this.textBoxWord.Multiline = true;
            this.textBoxWord.Name = "textBoxWord";
            this.textBoxWord.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxWord.Size = new System.Drawing.Size(532, 281);
            this.textBoxWord.TabIndex = 0;
            this.textBoxWord.TextChanged += new System.EventHandler(this.textBoxWord_TextChanged);
            // 
            // checkBoxWrap
            // 
            this.checkBoxWrap.AutoSize = true;
            this.checkBoxWrap.Checked = true;
            this.checkBoxWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWrap.Location = new System.Drawing.Point(560, 12);
            this.checkBoxWrap.Name = "checkBoxWrap";
            this.checkBoxWrap.Size = new System.Drawing.Size(81, 17);
            this.checkBoxWrap.TabIndex = 1;
            this.checkBoxWrap.Text = "Word Wrap";
            this.checkBoxWrap.UseVisualStyleBackColor = true;
            this.checkBoxWrap.CheckedChanged += new System.EventHandler(this.checkBoxWrap_CheckedChanged);
            // 
            // buttonBIG5_GB
            // 
            this.buttonBIG5_GB.Location = new System.Drawing.Point(559, 200);
            this.buttonBIG5_GB.Name = "buttonBIG5_GB";
            this.buttonBIG5_GB.Size = new System.Drawing.Size(101, 27);
            this.buttonBIG5_GB.TabIndex = 37;
            this.buttonBIG5_GB.Text = "BIG5 to GB";
            this.buttonBIG5_GB.UseVisualStyleBackColor = true;
            this.buttonBIG5_GB.Click += new System.EventHandler(this.buttonBIG5_GB_Click);
            // 
            // buttonGB_BIG5
            // 
            this.buttonGB_BIG5.Location = new System.Drawing.Point(560, 101);
            this.buttonGB_BIG5.Name = "buttonGB_BIG5";
            this.buttonGB_BIG5.Size = new System.Drawing.Size(100, 27);
            this.buttonGB_BIG5.TabIndex = 36;
            this.buttonGB_BIG5.Text = "GB to BIG5";
            this.buttonGB_BIG5.UseVisualStyleBackColor = true;
            this.buttonGB_BIG5.Click += new System.EventHandler(this.buttonGB_BIG5_Click);
            // 
            // buttonBIG5_GBK
            // 
            this.buttonBIG5_GBK.Location = new System.Drawing.Point(559, 233);
            this.buttonBIG5_GBK.Name = "buttonBIG5_GBK";
            this.buttonBIG5_GBK.Size = new System.Drawing.Size(101, 27);
            this.buttonBIG5_GBK.TabIndex = 35;
            this.buttonBIG5_GBK.Text = "BIG5 to GBK";
            this.buttonBIG5_GBK.UseVisualStyleBackColor = true;
            this.buttonBIG5_GBK.Click += new System.EventHandler(this.buttonBIG5_GBK_Click);
            // 
            // buttonGBK_GB
            // 
            this.buttonGBK_GB.Location = new System.Drawing.Point(560, 134);
            this.buttonGBK_GB.Name = "buttonGBK_GB";
            this.buttonGBK_GB.Size = new System.Drawing.Size(100, 27);
            this.buttonGBK_GB.TabIndex = 34;
            this.buttonGBK_GB.Text = "GBK to GB";
            this.buttonGBK_GB.UseVisualStyleBackColor = true;
            this.buttonGBK_GB.Click += new System.EventHandler(this.buttonGBK_GB_Click);
            // 
            // buttonGB_GBK
            // 
            this.buttonGB_GBK.Location = new System.Drawing.Point(559, 68);
            this.buttonGB_GBK.Name = "buttonGB_GBK";
            this.buttonGB_GBK.Size = new System.Drawing.Size(100, 27);
            this.buttonGB_GBK.TabIndex = 33;
            this.buttonGB_GBK.Text = "GB to GBK";
            this.buttonGB_GBK.UseVisualStyleBackColor = true;
            this.buttonGB_GBK.Click += new System.EventHandler(this.buttonGB_GBK_Click);
            // 
            // buttonGBK_BIG5
            // 
            this.buttonGBK_BIG5.Location = new System.Drawing.Point(559, 167);
            this.buttonGBK_BIG5.Name = "buttonGBK_BIG5";
            this.buttonGBK_BIG5.Size = new System.Drawing.Size(101, 27);
            this.buttonGBK_BIG5.TabIndex = 32;
            this.buttonGBK_BIG5.Text = "GBK to BIG5";
            this.buttonGBK_BIG5.UseVisualStyleBackColor = true;
            this.buttonGBK_BIG5.Click += new System.EventHandler(this.buttonGBK_BIG5_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(560, 35);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(100, 27);
            this.buttonReset.TabIndex = 38;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(560, 266);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 27);
            this.buttonClose.TabIndex = 39;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // FormCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 308);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonBIG5_GB);
            this.Controls.Add(this.buttonGB_BIG5);
            this.Controls.Add(this.buttonBIG5_GBK);
            this.Controls.Add(this.buttonGBK_GB);
            this.Controls.Add(this.buttonGB_GBK);
            this.Controls.Add(this.buttonGBK_BIG5);
            this.Controls.Add(this.checkBoxWrap);
            this.Controls.Add(this.textBoxWord);
            this.Name = "FormCode";
            this.Text = "FormCode";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxWord;
        private System.Windows.Forms.CheckBox checkBoxWrap;
        private System.Windows.Forms.Button buttonBIG5_GB;
        private System.Windows.Forms.Button buttonGB_BIG5;
        private System.Windows.Forms.Button buttonBIG5_GBK;
        private System.Windows.Forms.Button buttonGBK_GB;
        private System.Windows.Forms.Button buttonGB_GBK;
        private System.Windows.Forms.Button buttonGBK_BIG5;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonClose;
    }
}