namespace HYS.Adapter.Composer.Forms
{
    partial class FormScript
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
            this.textBoxLocation = new System.Windows.Forms.TextBox();
            this.labelDevice = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxScript = new System.Windows.Forms.TextBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.buttonIMParam = new System.Windows.Forms.Button();
            this.labelError = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxServiceName = new System.Windows.Forms.TextBox();
            this.buttonGetStatus = new System.Windows.Forms.Button();
            this.buttonServices = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxLocation
            // 
            this.textBoxLocation.Location = new System.Drawing.Point(117, 45);
            this.textBoxLocation.Name = "textBoxLocation";
            this.textBoxLocation.ReadOnly = true;
            this.textBoxLocation.Size = new System.Drawing.Size(470, 20);
            this.textBoxLocation.TabIndex = 25;
            // 
            // labelDevice
            // 
            this.labelDevice.Location = new System.Drawing.Point(16, 46);
            this.labelDevice.Name = "labelDevice";
            this.labelDevice.Size = new System.Drawing.Size(120, 21);
            this.labelDevice.TabIndex = 24;
            this.labelDevice.Text = "Script location:";
            this.labelDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 21);
            this.label1.TabIndex = 26;
            this.label1.Text = "Script type:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "InstallScript",
            "UninstallScript",
            "StartScript",
            "StopScript"});
            this.comboBoxType.Location = new System.Drawing.Point(117, 22);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(183, 21);
            this.comboBoxType.TabIndex = 27;
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBoxType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 21);
            this.label2.TabIndex = 28;
            this.label2.Text = "Script content:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxScript
            // 
            this.textBoxScript.Location = new System.Drawing.Point(117, 76);
            this.textBoxScript.Multiline = true;
            this.textBoxScript.Name = "textBoxScript";
            this.textBoxScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxScript.Size = new System.Drawing.Size(470, 323);
            this.textBoxScript.TabIndex = 29;
            this.textBoxScript.WordWrap = false;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(606, 76);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(83, 38);
            this.buttonLoad.TabIndex = 30;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(606, 120);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(83, 38);
            this.buttonSave.TabIndex = 31;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(606, 164);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(83, 38);
            this.buttonExecute.TabIndex = 32;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // buttonIMParam
            // 
            this.buttonIMParam.Location = new System.Drawing.Point(606, 361);
            this.buttonIMParam.Name = "buttonIMParam";
            this.buttonIMParam.Size = new System.Drawing.Size(83, 38);
            this.buttonIMParam.TabIndex = 33;
            this.buttonIMParam.Text = "View IM Parameters";
            this.buttonIMParam.UseVisualStyleBackColor = true;
            this.buttonIMParam.Click += new System.EventHandler(this.buttonIMParam_Click);
            // 
            // labelError
            // 
            this.labelError.ForeColor = System.Drawing.Color.Red;
            this.labelError.Location = new System.Drawing.Point(337, 21);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(250, 21);
            this.labelError.TabIndex = 34;
            this.labelError.Text = "Cannot load DeviceDir file";
            this.labelError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelError.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 412);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 21);
            this.label3.TabIndex = 35;
            this.label3.Text = "Service Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxServiceName
            // 
            this.textBoxServiceName.Location = new System.Drawing.Point(117, 413);
            this.textBoxServiceName.Name = "textBoxServiceName";
            this.textBoxServiceName.Size = new System.Drawing.Size(325, 20);
            this.textBoxServiceName.TabIndex = 36;
            // 
            // buttonGetStatus
            // 
            this.buttonGetStatus.Location = new System.Drawing.Point(448, 408);
            this.buttonGetStatus.Name = "buttonGetStatus";
            this.buttonGetStatus.Size = new System.Drawing.Size(139, 28);
            this.buttonGetStatus.TabIndex = 37;
            this.buttonGetStatus.Text = "Get Service Status";
            this.buttonGetStatus.UseVisualStyleBackColor = true;
            this.buttonGetStatus.Click += new System.EventHandler(this.buttonGetStatus_Click);
            // 
            // buttonServices
            // 
            this.buttonServices.Location = new System.Drawing.Point(606, 408);
            this.buttonServices.Name = "buttonServices";
            this.buttonServices.Size = new System.Drawing.Size(83, 28);
            this.buttonServices.TabIndex = 38;
            this.buttonServices.Text = "List Services";
            this.buttonServices.UseVisualStyleBackColor = true;
            this.buttonServices.Click += new System.EventHandler(this.buttonServices_Click);
            // 
            // FormScript
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 451);
            this.Controls.Add(this.buttonServices);
            this.Controls.Add(this.buttonGetStatus);
            this.Controls.Add(this.textBoxServiceName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.buttonIMParam);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.textBoxScript);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLocation);
            this.Controls.Add(this.labelDevice);
            this.Name = "FormScript";
            this.Text = "FormScript";
            this.Load += new System.EventHandler(this.FormScript_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLocation;
        private System.Windows.Forms.Label labelDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxScript;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Button buttonIMParam;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxServiceName;
        private System.Windows.Forms.Button buttonGetStatus;
        private System.Windows.Forms.Button buttonServices;
    }
}