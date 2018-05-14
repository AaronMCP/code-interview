namespace HYS.IM.MessageDevices.CSBAdpater.Outbound.Forms
{
    partial class FormConfig
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
            this.textBoxDBCnn = new System.Windows.Forms.TextBox();
            this.buttonDBTest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSQLIn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxXSLTPath = new System.Windows.Forms.TextBox();
            this.checkBoxTransform = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxDBCnn
            // 
            this.textBoxDBCnn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDBCnn.Location = new System.Drawing.Point(15, 38);
            this.textBoxDBCnn.Name = "textBoxDBCnn";
            this.textBoxDBCnn.Size = new System.Drawing.Size(425, 20);
            this.textBoxDBCnn.TabIndex = 15;
            // 
            // buttonDBTest
            // 
            this.buttonDBTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDBTest.Location = new System.Drawing.Point(458, 33);
            this.buttonDBTest.Name = "buttonDBTest";
            this.buttonDBTest.Size = new System.Drawing.Size(67, 25);
            this.buttonDBTest.TabIndex = 17;
            this.buttonDBTest.Text = "Test";
            this.buttonDBTest.UseVisualStyleBackColor = true;
            this.buttonDBTest.Click += new System.EventHandler(this.buttonDBTest_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(350, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "CS Broker SQL Server Database Connection String:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSQLIn
            // 
            this.textBoxSQLIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSQLIn.Location = new System.Drawing.Point(15, 101);
            this.textBoxSQLIn.Name = "textBoxSQLIn";
            this.textBoxSQLIn.Size = new System.Drawing.Size(425, 20);
            this.textBoxSQLIn.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "CS Broker Passive SQL Inbound Interface Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(458, 212);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(67, 25);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(373, 212);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(67, 25);
            this.buttonOK.TabIndex = 21;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxXSLTPath
            // 
            this.textBoxXSLTPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXSLTPath.Location = new System.Drawing.Point(15, 161);
            this.textBoxXSLTPath.Name = "textBoxXSLTPath";
            this.textBoxXSLTPath.Size = new System.Drawing.Size(425, 20);
            this.textBoxXSLTPath.TabIndex = 22;
            this.textBoxXSLTPath.Visible = false;
            // 
            // checkBoxTransform
            // 
            this.checkBoxTransform.AutoSize = true;
            this.checkBoxTransform.Location = new System.Drawing.Point(15, 138);
            this.checkBoxTransform.Name = "checkBoxTransform";
            this.checkBoxTransform.Size = new System.Drawing.Size(424, 17);
            this.checkBoxTransform.TabIndex = 23;
            this.checkBoxTransform.Text = "Transform message into CS Broker DataSet schema by using the following XSLT file:" +
                "";
            this.checkBoxTransform.UseVisualStyleBackColor = true;
            this.checkBoxTransform.Visible = false;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 258);
            this.Controls.Add(this.checkBoxTransform);
            this.Controls.Add(this.textBoxXSLTPath);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxSQLIn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDBCnn);
            this.Controls.Add(this.buttonDBTest);
            this.Controls.Add(this.label2);
            this.Name = "FormConfig";
            this.Text = "CS Broker Outbound Adapter Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDBCnn;
        private System.Windows.Forms.Button buttonDBTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSQLIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxXSLTPath;
        private System.Windows.Forms.CheckBox checkBoxTransform;
    }
}