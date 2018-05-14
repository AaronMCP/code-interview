namespace HYS.IM.Wizard
{
    partial class InterfaceConfigurationPage
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonConfiguration = new System.Windows.Forms.Button();
            this.panelProcess = new System.Windows.Forms.Panel();
            this.progressBarProcess = new System.Windows.Forms.ProgressBar();
            this.labelProcess = new System.Windows.Forms.Label();
            this.groupBoxDevice = new System.Windows.Forms.GroupBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelDirection = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelDevice = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.checkBoxAutoStart = new System.Windows.Forms.CheckBox();
            this.panelProcess.SuspendLayout();
            this.groupBoxDevice.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.DarkGray;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(650, 28);
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "STEP 3 : INTERFACE CONFIGURATION";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(613, 52);
            this.label2.TabIndex = 8;
            this.label2.Text = "Now you have a change to modify configuration before installing the interface, pl" +
                "ease click the button below to do so, or click \"Complete\" button and install the" +
                " interface with default configuration.";
            // 
            // buttonConfiguration
            // 
            this.buttonConfiguration.Location = new System.Drawing.Point(26, 129);
            this.buttonConfiguration.Name = "buttonConfiguration";
            this.buttonConfiguration.Size = new System.Drawing.Size(227, 25);
            this.buttonConfiguration.TabIndex = 9;
            this.buttonConfiguration.Text = "Modify Configuration";
            this.buttonConfiguration.UseVisualStyleBackColor = true;
            this.buttonConfiguration.Click += new System.EventHandler(this.buttonConfiguration_Click);
            // 
            // panelProcess
            // 
            this.panelProcess.Controls.Add(this.progressBarProcess);
            this.panelProcess.Controls.Add(this.labelProcess);
            this.panelProcess.Location = new System.Drawing.Point(17, 248);
            this.panelProcess.Name = "panelProcess";
            this.panelProcess.Size = new System.Drawing.Size(253, 71);
            this.panelProcess.TabIndex = 17;
            // 
            // progressBarProcess
            // 
            this.progressBarProcess.Location = new System.Drawing.Point(10, 35);
            this.progressBarProcess.Name = "progressBarProcess";
            this.progressBarProcess.Size = new System.Drawing.Size(225, 24);
            this.progressBarProcess.TabIndex = 11;
            // 
            // labelProcess
            // 
            this.labelProcess.Location = new System.Drawing.Point(7, 5);
            this.labelProcess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(228, 27);
            this.labelProcess.TabIndex = 10;
            this.labelProcess.Text = "Registering services... ";
            this.labelProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxDevice
            // 
            this.groupBoxDevice.Controls.Add(this.labelDescription);
            this.groupBoxDevice.Controls.Add(this.label10);
            this.groupBoxDevice.Controls.Add(this.labelDirection);
            this.groupBoxDevice.Controls.Add(this.label8);
            this.groupBoxDevice.Controls.Add(this.labelType);
            this.groupBoxDevice.Controls.Add(this.label6);
            this.groupBoxDevice.Controls.Add(this.labelDevice);
            this.groupBoxDevice.Controls.Add(this.label4);
            this.groupBoxDevice.Controls.Add(this.labelName);
            this.groupBoxDevice.Controls.Add(this.label5);
            this.groupBoxDevice.Location = new System.Drawing.Point(310, 110);
            this.groupBoxDevice.Name = "groupBoxDevice";
            this.groupBoxDevice.Size = new System.Drawing.Size(314, 198);
            this.groupBoxDevice.TabIndex = 18;
            this.groupBoxDevice.TabStop = false;
            this.groupBoxDevice.Text = "Interface Information";
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDescription.BackColor = System.Drawing.SystemColors.Info;
            this.labelDescription.Location = new System.Drawing.Point(103, 140);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(195, 45);
            this.labelDescription.TabIndex = 9;
            this.labelDescription.Text = "  ";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(16, 140);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 19);
            this.label10.TabIndex = 8;
            this.label10.Text = "Description:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDirection
            // 
            this.labelDirection.BackColor = System.Drawing.SystemColors.Info;
            this.labelDirection.Location = new System.Drawing.Point(103, 112);
            this.labelDirection.Name = "labelDirection";
            this.labelDirection.Size = new System.Drawing.Size(195, 19);
            this.labelDirection.TabIndex = 7;
            this.labelDirection.Text = "  ";
            this.labelDirection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 19);
            this.label8.TabIndex = 6;
            this.label8.Text = "Direction:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelType
            // 
            this.labelType.BackColor = System.Drawing.SystemColors.Info;
            this.labelType.Location = new System.Drawing.Point(103, 83);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(195, 19);
            this.labelType.TabIndex = 5;
            this.labelType.Text = "  ";
            this.labelType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 19);
            this.label6.TabIndex = 4;
            this.label6.Text = "Type:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelDevice
            // 
            this.labelDevice.BackColor = System.Drawing.SystemColors.Info;
            this.labelDevice.Location = new System.Drawing.Point(103, 54);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(195, 19);
            this.labelDevice.TabIndex = 3;
            this.labelDevice.Text = "  ";
            this.labelDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "Device:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelName
            // 
            this.labelName.BackColor = System.Drawing.SystemColors.Info;
            this.labelName.Location = new System.Drawing.Point(103, 25);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(195, 19);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "  ";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "Name:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonPrev
            // 
            this.buttonPrev.Location = new System.Drawing.Point(310, 327);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(91, 25);
            this.buttonPrev.TabIndex = 21;
            this.buttonPrev.Text = "<< Back";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(533, 327);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(91, 25);
            this.buttonCancel.TabIndex = 20;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(423, 327);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(91, 25);
            this.buttonNext.TabIndex = 19;
            this.buttonNext.Text = "Complete";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // checkBoxAutoStart
            // 
            this.checkBoxAutoStart.Checked = true;
            this.checkBoxAutoStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoStart.Location = new System.Drawing.Point(29, 174);
            this.checkBoxAutoStart.Name = "checkBoxAutoStart";
            this.checkBoxAutoStart.Size = new System.Drawing.Size(223, 19);
            this.checkBoxAutoStart.TabIndex = 22;
            this.checkBoxAutoStart.Text = "Start interface after installation";
            this.checkBoxAutoStart.UseVisualStyleBackColor = true;
            // 
            // InterfaceConfigurationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxAutoStart);
            this.Controls.Add(this.buttonPrev);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.groupBoxDevice);
            this.Controls.Add(this.panelProcess);
            this.Controls.Add(this.buttonConfiguration);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InterfaceConfigurationPage";
            this.Size = new System.Drawing.Size(650, 371);
            this.panelProcess.ResumeLayout(false);
            this.groupBoxDevice.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonConfiguration;
        private System.Windows.Forms.Panel panelProcess;
        private System.Windows.Forms.ProgressBar progressBarProcess;
        private System.Windows.Forms.Label labelProcess;
        private System.Windows.Forms.GroupBox groupBoxDevice;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelDirection;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelDevice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.CheckBox checkBoxAutoStart;
    }
}
