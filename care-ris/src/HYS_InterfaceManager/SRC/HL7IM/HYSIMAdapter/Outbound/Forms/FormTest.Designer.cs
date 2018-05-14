namespace HYS.IM.MessageDevices.CSBAdpater.Outbound.Forms
{
    partial class FormTest
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
            this.buttonReadOrderMessage = new System.Windows.Forms.Button();
            this.buttonProcess = new System.Windows.Forms.Button();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.tabControlCSBMsg = new System.Windows.Forms.TabControl();
            this.tabPageCSBMsgPlain = new System.Windows.Forms.TabPage();
            this.textBoxCSBMsg = new System.Windows.Forms.TextBox();
            this.tabPageCSBMsgTree = new System.Windows.Forms.TabPage();
            this.webBrowserCSBMsg = new System.Windows.Forms.WebBrowser();
            this.dataGridViewDS = new System.Windows.Forms.DataGridView();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.tabControlCSBMsg.SuspendLayout();
            this.tabPageCSBMsgPlain.SuspendLayout();
            this.tabPageCSBMsgTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDS)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonReadOrderMessage
            // 
            this.buttonReadOrderMessage.Location = new System.Drawing.Point(12, 12);
            this.buttonReadOrderMessage.Name = "buttonReadOrderMessage";
            this.buttonReadOrderMessage.Size = new System.Drawing.Size(157, 28);
            this.buttonReadOrderMessage.TabIndex = 4;
            this.buttonReadOrderMessage.Text = "Read Sample Order Message";
            this.buttonReadOrderMessage.UseVisualStyleBackColor = true;
            this.buttonReadOrderMessage.Click += new System.EventHandler(this.buttonReadOrderMessage_Click);
            // 
            // buttonProcess
            // 
            this.buttonProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonProcess.Location = new System.Drawing.Point(284, 12);
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Size = new System.Drawing.Size(85, 28);
            this.buttonProcess.TabIndex = 40;
            this.buttonProcess.Text = "Insert DataSet";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new System.EventHandler(this.buttonProcess_Click);
            // 
            // buttonQuery
            // 
            this.buttonQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuery.Location = new System.Drawing.Point(375, 12);
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.Size = new System.Drawing.Size(221, 28);
            this.buttonQuery.TabIndex = 41;
            this.buttonQuery.Text = "Read Lastest 100 Records From CS Broker";
            this.buttonQuery.UseVisualStyleBackColor = true;
            this.buttonQuery.Click += new System.EventHandler(this.buttonQuery_Click);
            // 
            // tabControlCSBMsg
            // 
            this.tabControlCSBMsg.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControlCSBMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlCSBMsg.Controls.Add(this.tabPageCSBMsgPlain);
            this.tabControlCSBMsg.Controls.Add(this.tabPageCSBMsgTree);
            this.tabControlCSBMsg.Location = new System.Drawing.Point(12, 52);
            this.tabControlCSBMsg.Multiline = true;
            this.tabControlCSBMsg.Name = "tabControlCSBMsg";
            this.tabControlCSBMsg.SelectedIndex = 0;
            this.tabControlCSBMsg.Size = new System.Drawing.Size(584, 209);
            this.tabControlCSBMsg.TabIndex = 42;
            // 
            // tabPageCSBMsgPlain
            // 
            this.tabPageCSBMsgPlain.Controls.Add(this.textBoxCSBMsg);
            this.tabPageCSBMsgPlain.Location = new System.Drawing.Point(4, 4);
            this.tabPageCSBMsgPlain.Name = "tabPageCSBMsgPlain";
            this.tabPageCSBMsgPlain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCSBMsgPlain.Size = new System.Drawing.Size(557, 201);
            this.tabPageCSBMsgPlain.TabIndex = 0;
            this.tabPageCSBMsgPlain.Text = "Plain Text";
            this.tabPageCSBMsgPlain.UseVisualStyleBackColor = true;
            // 
            // textBoxCSBMsg
            // 
            this.textBoxCSBMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxCSBMsg.Location = new System.Drawing.Point(3, 3);
            this.textBoxCSBMsg.Multiline = true;
            this.textBoxCSBMsg.Name = "textBoxCSBMsg";
            this.textBoxCSBMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCSBMsg.Size = new System.Drawing.Size(551, 195);
            this.textBoxCSBMsg.TabIndex = 18;
            // 
            // tabPageCSBMsgTree
            // 
            this.tabPageCSBMsgTree.Controls.Add(this.webBrowserCSBMsg);
            this.tabPageCSBMsgTree.Location = new System.Drawing.Point(4, 4);
            this.tabPageCSBMsgTree.Name = "tabPageCSBMsgTree";
            this.tabPageCSBMsgTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCSBMsgTree.Size = new System.Drawing.Size(557, 201);
            this.tabPageCSBMsgTree.TabIndex = 1;
            this.tabPageCSBMsgTree.Text = "Tree View";
            this.tabPageCSBMsgTree.UseVisualStyleBackColor = true;
            // 
            // webBrowserCSBMsg
            // 
            this.webBrowserCSBMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserCSBMsg.Location = new System.Drawing.Point(3, 3);
            this.webBrowserCSBMsg.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserCSBMsg.Name = "webBrowserCSBMsg";
            this.webBrowserCSBMsg.Size = new System.Drawing.Size(551, 195);
            this.webBrowserCSBMsg.TabIndex = 0;
            // 
            // dataGridViewDS
            // 
            this.dataGridViewDS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDS.Location = new System.Drawing.Point(12, 275);
            this.dataGridViewDS.Name = "dataGridViewDS";
            this.dataGridViewDS.Size = new System.Drawing.Size(578, 117);
            this.dataGridViewDS.TabIndex = 43;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(175, 12);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(103, 28);
            this.buttonGenerate.TabIndex = 44;
            this.buttonGenerate.Text = "Generate DataSet";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 404);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.dataGridViewDS);
            this.Controls.Add(this.tabControlCSBMsg);
            this.Controls.Add(this.buttonQuery);
            this.Controls.Add(this.buttonProcess);
            this.Controls.Add(this.buttonReadOrderMessage);
            this.MinimumSize = new System.Drawing.Size(616, 438);
            this.Name = "FormTest";
            this.Text = "CS Broker Outbound Adapter Test";
            this.tabControlCSBMsg.ResumeLayout(false);
            this.tabPageCSBMsgPlain.ResumeLayout(false);
            this.tabPageCSBMsgPlain.PerformLayout();
            this.tabPageCSBMsgTree.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonReadOrderMessage;
        private System.Windows.Forms.Button buttonProcess;
        private System.Windows.Forms.Button buttonQuery;
        private System.Windows.Forms.TabControl tabControlCSBMsg;
        private System.Windows.Forms.TabPage tabPageCSBMsgPlain;
        private System.Windows.Forms.TextBox textBoxCSBMsg;
        private System.Windows.Forms.TabPage tabPageCSBMsgTree;
        private System.Windows.Forms.WebBrowser webBrowserCSBMsg;
        private System.Windows.Forms.DataGridView dataGridViewDS;
        private System.Windows.Forms.Button buttonGenerate;
    }
}