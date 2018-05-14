namespace HYS.MessageDevices.SOAPAdapter.Test
{
    partial class FormXSLT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormXSLT));
            this.checkBoxSource = new System.Windows.Forms.CheckBox();
            this.textBoxSource = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxXSLT = new System.Windows.Forms.CheckBox();
            this.textBoxXSLT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxTarget = new System.Windows.Forms.CheckBox();
            this.textBoxTarget = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonTransform = new System.Windows.Forms.Button();
            this.labelPerform = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBoxSource
            // 
            this.checkBoxSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSource.AutoSize = true;
            this.checkBoxSource.Checked = true;
            this.checkBoxSource.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSource.Location = new System.Drawing.Point(533, 13);
            this.checkBoxSource.Name = "checkBoxSource";
            this.checkBoxSource.Size = new System.Drawing.Size(81, 17);
            this.checkBoxSource.TabIndex = 18;
            this.checkBoxSource.Text = "Word Wrap";
            this.checkBoxSource.UseVisualStyleBackColor = true;
            this.checkBoxSource.CheckedChanged += new System.EventHandler(this.checkBoxSource_CheckedChanged);
            // 
            // textBoxSource
            // 
            this.textBoxSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSource.Location = new System.Drawing.Point(12, 33);
            this.textBoxSource.Multiline = true;
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSource.Size = new System.Drawing.Size(602, 132);
            this.textBoxSource.TabIndex = 17;
            this.textBoxSource.Text = resources.GetString("textBoxSource.Text");
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 19);
            this.label3.TabIndex = 16;
            this.label3.Text = "Source XML:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxXSLT
            // 
            this.checkBoxXSLT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXSLT.AutoSize = true;
            this.checkBoxXSLT.Checked = true;
            this.checkBoxXSLT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxXSLT.Location = new System.Drawing.Point(533, 170);
            this.checkBoxXSLT.Name = "checkBoxXSLT";
            this.checkBoxXSLT.Size = new System.Drawing.Size(81, 17);
            this.checkBoxXSLT.TabIndex = 21;
            this.checkBoxXSLT.Text = "Word Wrap";
            this.checkBoxXSLT.UseVisualStyleBackColor = true;
            this.checkBoxXSLT.CheckedChanged += new System.EventHandler(this.checkBoxXSLT_CheckedChanged);
            // 
            // textBoxXSLT
            // 
            this.textBoxXSLT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXSLT.Location = new System.Drawing.Point(12, 190);
            this.textBoxXSLT.Multiline = true;
            this.textBoxXSLT.Name = "textBoxXSLT";
            this.textBoxXSLT.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxXSLT.Size = new System.Drawing.Size(602, 139);
            this.textBoxXSLT.TabIndex = 20;
            this.textBoxXSLT.Text = resources.GetString("textBoxXSLT.Text");
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 19);
            this.label1.TabIndex = 19;
            this.label1.Text = "XSLT:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxTarget
            // 
            this.checkBoxTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxTarget.AutoSize = true;
            this.checkBoxTarget.Checked = true;
            this.checkBoxTarget.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTarget.Location = new System.Drawing.Point(533, 335);
            this.checkBoxTarget.Name = "checkBoxTarget";
            this.checkBoxTarget.Size = new System.Drawing.Size(81, 17);
            this.checkBoxTarget.TabIndex = 24;
            this.checkBoxTarget.Text = "Word Wrap";
            this.checkBoxTarget.UseVisualStyleBackColor = true;
            this.checkBoxTarget.CheckedChanged += new System.EventHandler(this.checkBoxTarget_CheckedChanged);
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTarget.Location = new System.Drawing.Point(12, 355);
            this.textBoxTarget.Multiline = true;
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTarget.Size = new System.Drawing.Size(602, 125);
            this.textBoxTarget.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(9, 333);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 19);
            this.label2.TabIndex = 22;
            this.label2.Text = "Target XML:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonTransform
            // 
            this.buttonTransform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTransform.Location = new System.Drawing.Point(630, 33);
            this.buttonTransform.Name = "buttonTransform";
            this.buttonTransform.Size = new System.Drawing.Size(74, 46);
            this.buttonTransform.TabIndex = 0;
            this.buttonTransform.Text = "Transform";
            this.buttonTransform.UseVisualStyleBackColor = true;
            this.buttonTransform.Click += new System.EventHandler(this.buttonTransform_Click);
            // 
            // labelPerform
            // 
            this.labelPerform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPerform.Location = new System.Drawing.Point(630, 91);
            this.labelPerform.Name = "labelPerform";
            this.labelPerform.Size = new System.Drawing.Size(74, 29);
            this.labelPerform.TabIndex = 25;
            this.labelPerform.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormXSLT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 492);
            this.Controls.Add(this.labelPerform);
            this.Controls.Add(this.buttonTransform);
            this.Controls.Add(this.checkBoxTarget);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxXSLT);
            this.Controls.Add(this.textBoxXSLT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxSource);
            this.Controls.Add(this.textBoxSource);
            this.Controls.Add(this.label3);
            this.Name = "FormXSLT";
            this.Text = "XSLT Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxSource;
        private System.Windows.Forms.TextBox textBoxSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxXSLT;
        private System.Windows.Forms.TextBox textBoxXSLT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxTarget;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonTransform;
        private System.Windows.Forms.Label labelPerform;
    }
}