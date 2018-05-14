namespace HYS.IM.Forms
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelDBConnection = new System.Windows.Forms.Label();
            this.textBoxDBConnectionConfig = new System.Windows.Forms.TextBox();
            this.labelView = new System.Windows.Forms.Label();
            this.comboBoxView = new System.Windows.Forms.ComboBox();
            this.buttonDBTestConfig = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonDBTestData = new System.Windows.Forms.Button();
            this.textBoxDBConnectionData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonOSQLTest = new System.Windows.Forms.Button();
            this.textBoxOSQLPath = new System.Windows.Forms.TextBox();
            this.labelOSQLPath = new System.Windows.Forms.Label();
            this.textBoxOSQLArg = new System.Windows.Forms.TextBox();
            this.labelOSQLArg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(346, 381);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(90, 27);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(455, 381);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 27);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelDBConnection
            // 
            this.labelDBConnection.Location = new System.Drawing.Point(11, 114);
            this.labelDBConnection.Name = "labelDBConnection";
            this.labelDBConnection.Size = new System.Drawing.Size(425, 19);
            this.labelDBConnection.TabIndex = 3;
            this.labelDBConnection.Text = "Configuration Database Connection:";
            // 
            // textBoxDBConnectionConfig
            // 
            this.textBoxDBConnectionConfig.Location = new System.Drawing.Point(14, 138);
            this.textBoxDBConnectionConfig.Name = "textBoxDBConnectionConfig";
            this.textBoxDBConnectionConfig.Size = new System.Drawing.Size(422, 20);
            this.textBoxDBConnectionConfig.TabIndex = 4;
            // 
            // labelView
            // 
            this.labelView.Location = new System.Drawing.Point(11, 297);
            this.labelView.Name = "labelView";
            this.labelView.Size = new System.Drawing.Size(425, 19);
            this.labelView.TabIndex = 5;
            this.labelView.Text = "Open Following View as Default After Program Started:";
            // 
            // comboBoxView
            // 
            this.comboBoxView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxView.FormattingEnabled = true;
            this.comboBoxView.Items.AddRange(new object[] {
            "Device view",
            "Interface view"});
            this.comboBoxView.Location = new System.Drawing.Point(15, 324);
            this.comboBoxView.Name = "comboBoxView";
            this.comboBoxView.Size = new System.Drawing.Size(245, 21);
            this.comboBoxView.TabIndex = 6;
            // 
            // buttonDBTestConfig
            // 
            this.buttonDBTestConfig.Location = new System.Drawing.Point(455, 134);
            this.buttonDBTestConfig.Name = "buttonDBTestConfig";
            this.buttonDBTestConfig.Size = new System.Drawing.Size(90, 27);
            this.buttonDBTestConfig.TabIndex = 7;
            this.buttonDBTestConfig.Text = "Test";
            this.buttonDBTestConfig.UseVisualStyleBackColor = true;
            this.buttonDBTestConfig.Click += new System.EventHandler(this.buttonDBTestConfig_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(15, 367);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(533, 2);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            // 
            // buttonDBTestData
            // 
            this.buttonDBTestData.Location = new System.Drawing.Point(455, 73);
            this.buttonDBTestData.Name = "buttonDBTestData";
            this.buttonDBTestData.Size = new System.Drawing.Size(90, 27);
            this.buttonDBTestData.TabIndex = 16;
            this.buttonDBTestData.Text = "Test";
            this.buttonDBTestData.UseVisualStyleBackColor = true;
            this.buttonDBTestData.Click += new System.EventHandler(this.buttonDBTestData_Click);
            // 
            // textBoxDBConnectionData
            // 
            this.textBoxDBConnectionData.Location = new System.Drawing.Point(14, 77);
            this.textBoxDBConnectionData.Name = "textBoxDBConnectionData";
            this.textBoxDBConnectionData.Size = new System.Drawing.Size(422, 20);
            this.textBoxDBConnectionData.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(11, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(425, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "Cache Database Connection:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(350, 28);
            this.label2.TabIndex = 17;
            this.label2.Text = "Interface Manager Configuration";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(15, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(533, 2);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // buttonOSQLTest
            // 
            this.buttonOSQLTest.Location = new System.Drawing.Point(455, 252);
            this.buttonOSQLTest.Name = "buttonOSQLTest";
            this.buttonOSQLTest.Size = new System.Drawing.Size(90, 27);
            this.buttonOSQLTest.TabIndex = 22;
            this.buttonOSQLTest.Text = "Test";
            this.buttonOSQLTest.UseVisualStyleBackColor = true;
            this.buttonOSQLTest.Click += new System.EventHandler(this.buttonOSQLTest_Click);
            // 
            // textBoxOSQLPath
            // 
            this.textBoxOSQLPath.Location = new System.Drawing.Point(15, 197);
            this.textBoxOSQLPath.Name = "textBoxOSQLPath";
            this.textBoxOSQLPath.Size = new System.Drawing.Size(422, 20);
            this.textBoxOSQLPath.TabIndex = 21;
            // 
            // labelOSQLPath
            // 
            this.labelOSQLPath.Location = new System.Drawing.Point(12, 173);
            this.labelOSQLPath.Name = "labelOSQLPath";
            this.labelOSQLPath.Size = new System.Drawing.Size(425, 19);
            this.labelOSQLPath.TabIndex = 20;
            this.labelOSQLPath.Text = "OSQL.EXE File Path:";
            // 
            // textBoxOSQLArg
            // 
            this.textBoxOSQLArg.Location = new System.Drawing.Point(15, 256);
            this.textBoxOSQLArg.Name = "textBoxOSQLArg";
            this.textBoxOSQLArg.Size = new System.Drawing.Size(422, 20);
            this.textBoxOSQLArg.TabIndex = 24;
            // 
            // labelOSQLArg
            // 
            this.labelOSQLArg.Location = new System.Drawing.Point(12, 232);
            this.labelOSQLArg.Name = "labelOSQLArg";
            this.labelOSQLArg.Size = new System.Drawing.Size(425, 19);
            this.labelOSQLArg.TabIndex = 23;
            this.labelOSQLArg.Text = "OSQL.EXE Arguments:";
            // 
            // FormConfig
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(558, 422);
            this.Controls.Add(this.buttonOSQLTest);
            this.Controls.Add(this.textBoxOSQLArg);
            this.Controls.Add(this.labelOSQLArg);
            this.Controls.Add(this.textBoxOSQLPath);
            this.Controls.Add(this.labelOSQLPath);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonDBTestData);
            this.Controls.Add(this.textBoxDBConnectionData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonDBTestConfig);
            this.Controls.Add(this.comboBoxView);
            this.Controls.Add(this.labelView);
            this.Controls.Add(this.textBoxDBConnectionConfig);
            this.Controls.Add(this.labelDBConnection);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "System Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelDBConnection;
        private System.Windows.Forms.TextBox textBoxDBConnectionConfig;
        private System.Windows.Forms.Label labelView;
        private System.Windows.Forms.ComboBox comboBoxView;
        private System.Windows.Forms.Button buttonDBTestConfig;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonDBTestData;
        private System.Windows.Forms.TextBox textBoxDBConnectionData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOSQLTest;
        private System.Windows.Forms.TextBox textBoxOSQLPath;
        private System.Windows.Forms.Label labelOSQLPath;
        private System.Windows.Forms.TextBox textBoxOSQLArg;
        private System.Windows.Forms.Label labelOSQLArg;
    }
}