namespace Management.Test
{
    partial class FormMain
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
            this.buttonScriptRemoteCall = new System.Windows.Forms.Button();
            this.buttonScriptLocalCall = new System.Windows.Forms.Button();
            this.textBoxScriptWorkingPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxScriptArgument = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonScriptBrowse = new System.Windows.Forms.Button();
            this.textBoxScriptFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSvcStopRemote = new System.Windows.Forms.Button();
            this.buttonSvcStartRemote = new System.Windows.Forms.Button();
            this.buttonSvcStopLocal = new System.Windows.Forms.Button();
            this.buttonSvcStartLocal = new System.Windows.Forms.Button();
            this.buttonSvcGetStatus = new System.Windows.Forms.Button();
            this.textBoxNTServiceName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonConfigBrowse = new System.Windows.Forms.Button();
            this.textBoxMgtSvcConfigFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonScriptRemoteCall);
            this.groupBox1.Controls.Add(this.buttonScriptLocalCall);
            this.groupBox1.Controls.Add(this.textBoxScriptWorkingPath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxScriptArgument);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonScriptBrowse);
            this.groupBox1.Controls.Add(this.textBoxScriptFileName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 156);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Script Test";
            // 
            // buttonScriptRemoteCall
            // 
            this.buttonScriptRemoteCall.Location = new System.Drawing.Point(292, 112);
            this.buttonScriptRemoteCall.Name = "buttonScriptRemoteCall";
            this.buttonScriptRemoteCall.Size = new System.Drawing.Size(80, 27);
            this.buttonScriptRemoteCall.TabIndex = 8;
            this.buttonScriptRemoteCall.Text = "Call (remote)";
            this.buttonScriptRemoteCall.UseVisualStyleBackColor = true;
            this.buttonScriptRemoteCall.Click += new System.EventHandler(this.buttonScriptRemoteCall_Click);
            // 
            // buttonScriptLocalCall
            // 
            this.buttonScriptLocalCall.Location = new System.Drawing.Point(206, 112);
            this.buttonScriptLocalCall.Name = "buttonScriptLocalCall";
            this.buttonScriptLocalCall.Size = new System.Drawing.Size(80, 27);
            this.buttonScriptLocalCall.TabIndex = 7;
            this.buttonScriptLocalCall.Text = "Call (local)";
            this.buttonScriptLocalCall.UseVisualStyleBackColor = true;
            this.buttonScriptLocalCall.Click += new System.EventHandler(this.buttonScriptLocalCall_Click);
            // 
            // textBoxScriptWorkingPath
            // 
            this.textBoxScriptWorkingPath.Location = new System.Drawing.Point(120, 86);
            this.textBoxScriptWorkingPath.Name = "textBoxScriptWorkingPath";
            this.textBoxScriptWorkingPath.Size = new System.Drawing.Size(424, 20);
            this.textBoxScriptWorkingPath.TabIndex = 6;
            this.textBoxScriptWorkingPath.Text = "c:\\";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(22, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Working Path:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxScriptArgument
            // 
            this.textBoxScriptArgument.Location = new System.Drawing.Point(120, 60);
            this.textBoxScriptArgument.Name = "textBoxScriptArgument";
            this.textBoxScriptArgument.Size = new System.Drawing.Size(424, 20);
            this.textBoxScriptArgument.TabIndex = 4;
            this.textBoxScriptArgument.Text = "http://www.baidu.com";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(22, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Arguments:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonScriptBrowse
            // 
            this.buttonScriptBrowse.Location = new System.Drawing.Point(120, 112);
            this.buttonScriptBrowse.Name = "buttonScriptBrowse";
            this.buttonScriptBrowse.Size = new System.Drawing.Size(80, 27);
            this.buttonScriptBrowse.TabIndex = 2;
            this.buttonScriptBrowse.Text = "Browse";
            this.buttonScriptBrowse.UseVisualStyleBackColor = true;
            this.buttonScriptBrowse.Click += new System.EventHandler(this.buttonScriptBrowse_Click);
            // 
            // textBoxScriptFileName
            // 
            this.textBoxScriptFileName.Location = new System.Drawing.Point(120, 34);
            this.textBoxScriptFileName.Name = "textBoxScriptFileName";
            this.textBoxScriptFileName.Size = new System.Drawing.Size(424, 20);
            this.textBoxScriptFileName.TabIndex = 1;
            this.textBoxScriptFileName.Tag = "";
            this.textBoxScriptFileName.Text = "explorer";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Script File Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSvcStopRemote);
            this.groupBox2.Controls.Add(this.buttonSvcStartRemote);
            this.groupBox2.Controls.Add(this.buttonSvcStopLocal);
            this.groupBox2.Controls.Add(this.buttonSvcStartLocal);
            this.groupBox2.Controls.Add(this.buttonSvcGetStatus);
            this.groupBox2.Controls.Add(this.textBoxNTServiceName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(14, 247);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(569, 102);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "NT Service Test";
            // 
            // buttonSvcStopRemote
            // 
            this.buttonSvcStopRemote.Location = new System.Drawing.Point(464, 57);
            this.buttonSvcStopRemote.Name = "buttonSvcStopRemote";
            this.buttonSvcStopRemote.Size = new System.Drawing.Size(80, 27);
            this.buttonSvcStopRemote.TabIndex = 7;
            this.buttonSvcStopRemote.Text = "Stop (remote)";
            this.buttonSvcStopRemote.UseVisualStyleBackColor = true;
            this.buttonSvcStopRemote.Click += new System.EventHandler(this.buttonSvcStopRemote_Click);
            // 
            // buttonSvcStartRemote
            // 
            this.buttonSvcStartRemote.Location = new System.Drawing.Point(378, 57);
            this.buttonSvcStartRemote.Name = "buttonSvcStartRemote";
            this.buttonSvcStartRemote.Size = new System.Drawing.Size(80, 27);
            this.buttonSvcStartRemote.TabIndex = 6;
            this.buttonSvcStartRemote.Text = "Start (remote)";
            this.buttonSvcStartRemote.UseVisualStyleBackColor = true;
            this.buttonSvcStartRemote.Click += new System.EventHandler(this.buttonSvcStartRemote_Click);
            // 
            // buttonSvcStopLocal
            // 
            this.buttonSvcStopLocal.Location = new System.Drawing.Point(292, 57);
            this.buttonSvcStopLocal.Name = "buttonSvcStopLocal";
            this.buttonSvcStopLocal.Size = new System.Drawing.Size(80, 27);
            this.buttonSvcStopLocal.TabIndex = 5;
            this.buttonSvcStopLocal.Text = "Stop (local)";
            this.buttonSvcStopLocal.UseVisualStyleBackColor = true;
            this.buttonSvcStopLocal.Click += new System.EventHandler(this.buttonSvcStopLocal_Click);
            // 
            // buttonSvcStartLocal
            // 
            this.buttonSvcStartLocal.Location = new System.Drawing.Point(206, 57);
            this.buttonSvcStartLocal.Name = "buttonSvcStartLocal";
            this.buttonSvcStartLocal.Size = new System.Drawing.Size(80, 27);
            this.buttonSvcStartLocal.TabIndex = 4;
            this.buttonSvcStartLocal.Text = "Start (local)";
            this.buttonSvcStartLocal.UseVisualStyleBackColor = true;
            this.buttonSvcStartLocal.Click += new System.EventHandler(this.buttonSvcStartLocal_Click);
            // 
            // buttonSvcGetStatus
            // 
            this.buttonSvcGetStatus.Location = new System.Drawing.Point(120, 57);
            this.buttonSvcGetStatus.Name = "buttonSvcGetStatus";
            this.buttonSvcGetStatus.Size = new System.Drawing.Size(80, 27);
            this.buttonSvcGetStatus.TabIndex = 3;
            this.buttonSvcGetStatus.Text = "Get Status";
            this.buttonSvcGetStatus.UseVisualStyleBackColor = true;
            this.buttonSvcGetStatus.Click += new System.EventHandler(this.buttonSvcGetStatus_Click);
            // 
            // textBoxNTServiceName
            // 
            this.textBoxNTServiceName.Location = new System.Drawing.Point(120, 31);
            this.textBoxNTServiceName.Name = "textBoxNTServiceName";
            this.textBoxNTServiceName.Size = new System.Drawing.Size(424, 20);
            this.textBoxNTServiceName.TabIndex = 3;
            this.textBoxNTServiceName.Text = "RHISPIX_HL7RCV_HL7IN_MSGPIPE_SOAPOUT";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(22, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "NT Service Name:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonConfigBrowse);
            this.groupBox3.Controls.Add(this.textBoxMgtSvcConfigFile);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(14, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(568, 69);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Remote Service Setting";
            // 
            // buttonConfigBrowse
            // 
            this.buttonConfigBrowse.Location = new System.Drawing.Point(464, 21);
            this.buttonConfigBrowse.Name = "buttonConfigBrowse";
            this.buttonConfigBrowse.Size = new System.Drawing.Size(80, 27);
            this.buttonConfigBrowse.TabIndex = 9;
            this.buttonConfigBrowse.Text = "Browse";
            this.buttonConfigBrowse.UseVisualStyleBackColor = true;
            this.buttonConfigBrowse.Click += new System.EventHandler(this.buttonConfigBrowse_Click);
            // 
            // textBoxMgtSvcConfigFile
            // 
            this.textBoxMgtSvcConfigFile.Location = new System.Drawing.Point(120, 28);
            this.textBoxMgtSvcConfigFile.Name = "textBoxMgtSvcConfigFile";
            this.textBoxMgtSvcConfigFile.Size = new System.Drawing.Size(338, 20);
            this.textBoxMgtSvcConfigFile.TabIndex = 4;
            this.textBoxMgtSvcConfigFile.Text = "../../../Management.Service\\bin\\Debug\\ManagementService.xml";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(22, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "Configuration File:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 369);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Text = "Management Service Test";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxScriptFileName;
        private System.Windows.Forms.Button buttonScriptBrowse;
        private System.Windows.Forms.TextBox textBoxScriptArgument;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxScriptWorkingPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonScriptLocalCall;
        private System.Windows.Forms.Button buttonScriptRemoteCall;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxNTServiceName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSvcGetStatus;
        private System.Windows.Forms.Button buttonSvcStartLocal;
        private System.Windows.Forms.Button buttonSvcStopLocal;
        private System.Windows.Forms.Button buttonSvcStopRemote;
        private System.Windows.Forms.Button buttonSvcStartRemote;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxMgtSvcConfigFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonConfigBrowse;
    }
}

