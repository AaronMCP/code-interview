namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Forms
{
    partial class FormConfigDispatch
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonCustom = new System.Windows.Forms.RadioButton();
            this.radioButtonRequest = new System.Windows.Forms.RadioButton();
            this.radioButtonPublish = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxValueResponser = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxValueSubscriber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxXPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButtonCustom);
            this.groupBox1.Controls.Add(this.radioButtonRequest);
            this.groupBox1.Controls.Add(this.radioButtonPublish);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxValueResponser);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBoxValueSubscriber);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxPrefix);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxXPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 402);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Message Dispatching Criteria";
            // 
            // radioButtonCustom
            // 
            this.radioButtonCustom.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonCustom.Location = new System.Drawing.Point(25, 78);
            this.radioButtonCustom.Name = "radioButtonCustom";
            this.radioButtonCustom.Size = new System.Drawing.Size(505, 27);
            this.radioButtonCustom.TabIndex = 35;
            this.radioButtonCustom.TabStop = true;
            this.radioButtonCustom.Text = "The value of the following XPath determines where the incoming message would be d" +
                "ispatched.";
            this.radioButtonCustom.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonCustom.UseVisualStyleBackColor = true;
            // 
            // radioButtonRequest
            // 
            this.radioButtonRequest.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonRequest.Location = new System.Drawing.Point(25, 53);
            this.radioButtonRequest.Name = "radioButtonRequest";
            this.radioButtonRequest.Size = new System.Drawing.Size(490, 19);
            this.radioButtonRequest.TabIndex = 34;
            this.radioButtonRequest.TabStop = true;
            this.radioButtonRequest.Text = "Dispatch all incoming message to request channel.";
            this.radioButtonRequest.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonRequest.UseVisualStyleBackColor = true;
            // 
            // radioButtonPublish
            // 
            this.radioButtonPublish.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonPublish.Checked = true;
            this.radioButtonPublish.Location = new System.Drawing.Point(25, 28);
            this.radioButtonPublish.Name = "radioButtonPublish";
            this.radioButtonPublish.Size = new System.Drawing.Size(490, 19);
            this.radioButtonPublish.TabIndex = 33;
            this.radioButtonPublish.TabStop = true;
            this.radioButtonPublish.Text = "Dispatch all incoming message to publish channel.";
            this.radioButtonPublish.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.radioButtonPublish.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(137, 366);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(358, 20);
            this.label8.TabIndex = 32;
            this.label8.Text = "Example: value1|value2|value3|...";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxValueResponser
            // 
            this.textBoxValueResponser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValueResponser.Location = new System.Drawing.Point(137, 343);
            this.textBoxValueResponser.Name = "textBoxValueResponser";
            this.textBoxValueResponser.Size = new System.Drawing.Size(378, 20);
            this.textBoxValueResponser.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Location = new System.Drawing.Point(53, 307);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(462, 33);
            this.label9.TabIndex = 30;
            this.label9.Text = "The value of the previous XPath in incoming message matches the following regular" +
                " expression will be dispatched to request channel:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(137, 277);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(370, 20);
            this.label7.TabIndex = 29;
            this.label7.Text = "Example: value1|value2|value3|...";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxValueSubscriber
            // 
            this.textBoxValueSubscriber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValueSubscriber.Location = new System.Drawing.Point(137, 254);
            this.textBoxValueSubscriber.Name = "textBoxValueSubscriber";
            this.textBoxValueSubscriber.Size = new System.Drawing.Size(378, 20);
            this.textBoxValueSubscriber.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(50, 218);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(465, 33);
            this.label6.TabIndex = 21;
            this.label6.Text = "The value of the previous XPath in incoming message matches the following regular" +
                " expression will be dispatched to publish channel:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(137, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(370, 20);
            this.label5.TabIndex = 26;
            this.label5.Text = "Example: a|www.a.org|b|www.b.org";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(135, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(372, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "Example: /Message/Body/a:Root/b:Element";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPrefix.Location = new System.Drawing.Point(137, 166);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(378, 20);
            this.textBoxPrefix.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(47, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "Prefix definition:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxXPath
            // 
            this.textBoxXPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXPath.Location = new System.Drawing.Point(137, 111);
            this.textBoxXPath.Name = "textBoxXPath";
            this.textBoxXPath.Size = new System.Drawing.Size(378, 20);
            this.textBoxXPath.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(44, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "XPath:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(390, 427);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(76, 25);
            this.buttonOK.TabIndex = 19;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(472, 427);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(76, 25);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormConfigDispatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(560, 464);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfigDispatch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message Dispatching Setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxXPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxValueSubscriber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxValueResponser;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonPublish;
        private System.Windows.Forms.RadioButton radioButtonRequest;
        private System.Windows.Forms.RadioButton radioButtonCustom;
    }
}