namespace AdapterTest
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
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonReloadConfig = new System.Windows.Forms.Button();
            this.buttonSaveConfig = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonFindSQLServer = new System.Windows.Forms.Button();
            this.listBoxSQLServer = new System.Windows.Forms.ListBox();
            this.textBoxWord = new System.Windows.Forms.TextBox();
            this.buttonCC2PY = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSource = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxRegExp = new System.Windows.Forms.TextBox();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.textBoxReplace = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxPYType = new System.Windows.Forms.ComboBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(27, 19);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(519, 20);
            this.textBoxFile.TabIndex = 0;
            this.textBoxFile.Text = "DemoAdapter.exe";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(552, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 27);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(26, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(606, 333);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxDesc);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBoxName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(598, 307);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Adapter information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Location = new System.Drawing.Point(112, 64);
            this.textBoxDesc.Multiline = true;
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.ReadOnly = true;
            this.textBoxDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDesc.Size = new System.Drawing.Size(189, 87);
            this.textBoxDesc.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(28, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(112, 29);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            this.textBoxName.Size = new System.Drawing.Size(189, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(28, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(598, 307);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Adapter config";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonReloadConfig
            // 
            this.buttonReloadConfig.Location = new System.Drawing.Point(485, 409);
            this.buttonReloadConfig.Name = "buttonReloadConfig";
            this.buttonReloadConfig.Size = new System.Drawing.Size(143, 30);
            this.buttonReloadConfig.TabIndex = 3;
            this.buttonReloadConfig.Text = "Reload Config";
            this.buttonReloadConfig.UseVisualStyleBackColor = true;
            this.buttonReloadConfig.Click += new System.EventHandler(this.buttonReloadConfig_Click);
            // 
            // buttonSaveConfig
            // 
            this.buttonSaveConfig.Location = new System.Drawing.Point(336, 409);
            this.buttonSaveConfig.Name = "buttonSaveConfig";
            this.buttonSaveConfig.Size = new System.Drawing.Size(143, 30);
            this.buttonSaveConfig.TabIndex = 4;
            this.buttonSaveConfig.Text = "Save Config";
            this.buttonSaveConfig.UseVisualStyleBackColor = true;
            this.buttonSaveConfig.Click += new System.EventHandler(this.buttonSaveConfig_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(26, 409);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 30);
            this.button2.TabIndex = 5;
            this.button2.Text = "Post Message";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(662, 82);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(237, 355);
            this.listBox1.TabIndex = 6;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(662, 15);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(62, 27);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(730, 15);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(62, 27);
            this.buttonStop.TabIndex = 8;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(798, 15);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(62, 27);
            this.buttonInsert.TabIndex = 9;
            this.buttonInsert.Text = "Insert";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(662, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 27);
            this.button3.TabIndex = 10;
            this.button3.Text = "Call SP";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(175, 409);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(123, 30);
            this.button4.TabIndex = 11;
            this.button4.Text = "Adapter.Config.exe";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonFindSQLServer
            // 
            this.buttonFindSQLServer.Location = new System.Drawing.Point(907, 48);
            this.buttonFindSQLServer.Name = "buttonFindSQLServer";
            this.buttonFindSQLServer.Size = new System.Drawing.Size(126, 27);
            this.buttonFindSQLServer.TabIndex = 12;
            this.buttonFindSQLServer.Text = "Find SQL Server";
            this.buttonFindSQLServer.UseVisualStyleBackColor = true;
            this.buttonFindSQLServer.Click += new System.EventHandler(this.buttonFindSQLServer_Click);
            // 
            // listBoxSQLServer
            // 
            this.listBoxSQLServer.FormattingEnabled = true;
            this.listBoxSQLServer.Location = new System.Drawing.Point(907, 82);
            this.listBoxSQLServer.Name = "listBoxSQLServer";
            this.listBoxSQLServer.Size = new System.Drawing.Size(235, 355);
            this.listBoxSQLServer.TabIndex = 13;
            // 
            // textBoxWord
            // 
            this.textBoxWord.Location = new System.Drawing.Point(907, 478);
            this.textBoxWord.Name = "textBoxWord";
            this.textBoxWord.Size = new System.Drawing.Size(235, 20);
            this.textBoxWord.TabIndex = 14;
            this.textBoxWord.Text = "÷ÌÕ∑";
            // 
            // buttonCC2PY
            // 
            this.buttonCC2PY.Location = new System.Drawing.Point(1039, 531);
            this.buttonCC2PY.Name = "buttonCC2PY";
            this.buttonCC2PY.Size = new System.Drawing.Size(103, 27);
            this.buttonCC2PY.TabIndex = 15;
            this.buttonCC2PY.Text = "CC 2 PY";
            this.buttonCC2PY.UseVisualStyleBackColor = true;
            this.buttonCC2PY.Click += new System.EventHandler(this.buttonCC2PY_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(27, 461);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Source Text:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSource
            // 
            this.textBoxSource.Location = new System.Drawing.Point(142, 461);
            this.textBoxSource.Name = "textBoxSource";
            this.textBoxSource.Size = new System.Drawing.Size(337, 20);
            this.textBoxSource.TabIndex = 17;
            this.textBoxSource.Text = "aaa^aaaaa";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(27, 488);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 19);
            this.label4.TabIndex = 18;
            this.label4.Text = "Regular Expression:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxRegExp
            // 
            this.textBoxRegExp.Location = new System.Drawing.Point(142, 487);
            this.textBoxRegExp.Name = "textBoxRegExp";
            this.textBoxRegExp.Size = new System.Drawing.Size(337, 20);
            this.textBoxRegExp.TabIndex = 19;
            this.textBoxRegExp.Text = "[\\^]";
            // 
            // buttonReplace
            // 
            this.buttonReplace.Location = new System.Drawing.Point(512, 532);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(116, 27);
            this.buttonReplace.TabIndex = 20;
            this.buttonReplace.Text = "Replace";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // textBoxReplace
            // 
            this.textBoxReplace.Location = new System.Drawing.Point(142, 513);
            this.textBoxReplace.Name = "textBoxReplace";
            this.textBoxReplace.Size = new System.Drawing.Size(337, 20);
            this.textBoxReplace.TabIndex = 22;
            this.textBoxReplace.Text = " ";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(27, 514);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 19);
            this.label5.TabIndex = 21;
            this.label5.Text = "Replacement:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(142, 539);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.Size = new System.Drawing.Size(337, 20);
            this.textBoxResult.TabIndex = 24;
            this.textBoxResult.Text = " ";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(27, 540);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 19);
            this.label6.TabIndex = 23;
            this.label6.Text = "Result Text:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxPYType
            // 
            this.comboBoxPYType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPYType.FormattingEnabled = true;
            this.comboBoxPYType.Location = new System.Drawing.Point(907, 504);
            this.comboBoxPYType.Name = "comboBoxPYType";
            this.comboBoxPYType.Size = new System.Drawing.Size(235, 21);
            this.comboBoxPYType.TabIndex = 25;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(656, 498);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 27);
            this.button5.TabIndex = 26;
            this.button5.Text = "GBK to BIG5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(774, 532);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(112, 27);
            this.button6.TabIndex = 27;
            this.button6.Text = "GB to GBK";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(774, 498);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(112, 27);
            this.button7.TabIndex = 28;
            this.button7.Text = "GBK to GB";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(656, 531);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(112, 27);
            this.button8.TabIndex = 29;
            this.button8.Text = "BIG5 to GBK";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(656, 465);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(112, 27);
            this.button9.TabIndex = 30;
            this.button9.Text = "GB to BIG5";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(774, 465);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(112, 27);
            this.button10.TabIndex = 31;
            this.button10.Text = "BIG5 to GB";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(907, 532);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(103, 27);
            this.button11.TabIndex = 32;
            this.button11.Text = "Test";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 583);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.comboBoxPYType);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxReplace);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonReplace);
            this.Controls.Add(this.textBoxRegExp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxSource);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCC2PY);
            this.Controls.Add(this.textBoxWord);
            this.Controls.Add(this.listBoxSQLServer);
            this.Controls.Add(this.buttonFindSQLServer);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonInsert);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonSaveConfig);
            this.Controls.Add(this.buttonReloadConfig);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDesc;
        private System.Windows.Forms.Button buttonReloadConfig;
        private System.Windows.Forms.Button buttonSaveConfig;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonFindSQLServer;
        private System.Windows.Forms.ListBox listBoxSQLServer;
        private System.Windows.Forms.TextBox textBoxWord;
        private System.Windows.Forms.Button buttonCC2PY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSource;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRegExp;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.TextBox textBoxReplace;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxPYType;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
    }
}

