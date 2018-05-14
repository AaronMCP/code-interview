namespace HYS.IM.Forms
{
    partial class FormLUTMgt
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
            this.listViewLUTList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.listViewLUTItem = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSourceValue = new System.Windows.Forms.TextBox();
            this.textBoxTargetValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonAddItem = new System.Windows.Forms.Button();
            this.buttonModifyItem = new System.Windows.Forms.Button();
            this.buttonDeleteItem = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxTableName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonAddTable = new System.Windows.Forms.Button();
            this.buttonDeleteTable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(10, 487);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(654, 2);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // listViewLUTList
            // 
            this.listViewLUTList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewLUTList.FullRowSelect = true;
            this.listViewLUTList.HideSelection = false;
            this.listViewLUTList.Location = new System.Drawing.Point(12, 48);
            this.listViewLUTList.MultiSelect = false;
            this.listViewLUTList.Name = "listViewLUTList";
            this.listViewLUTList.Size = new System.Drawing.Size(261, 330);
            this.listViewLUTList.TabIndex = 0;
            this.listViewLUTList.UseCompatibleStateImageBehavior = false;
            this.listViewLUTList.View = System.Windows.Forms.View.Details;
            this.listViewLUTList.SelectedIndexChanged += new System.EventHandler(this.listViewLUTList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 24;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Table Name";
            this.columnHeader2.Width = 193;
            // 
            // listViewLUTItem
            // 
            this.listViewLUTItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewLUTItem.FullRowSelect = true;
            this.listViewLUTItem.HideSelection = false;
            this.listViewLUTItem.Location = new System.Drawing.Point(298, 48);
            this.listViewLUTItem.MultiSelect = false;
            this.listViewLUTItem.Name = "listViewLUTItem";
            this.listViewLUTItem.Size = new System.Drawing.Size(365, 330);
            this.listViewLUTItem.TabIndex = 1;
            this.listViewLUTItem.UseCompatibleStateImageBehavior = false;
            this.listViewLUTItem.View = System.Windows.Forms.View.Details;
            this.listViewLUTItem.SelectedIndexChanged += new System.EventHandler(this.listViewLUTItem_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "#";
            this.columnHeader3.Width = 24;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Source Value";
            this.columnHeader4.Width = 159;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Target Value";
            this.columnHeader5.Width = 154;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(295, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Look Up Table Content";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(295, 400);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Source Value:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSourceValue
            // 
            this.textBoxSourceValue.Location = new System.Drawing.Point(374, 400);
            this.textBoxSourceValue.Name = "textBoxSourceValue";
            this.textBoxSourceValue.Size = new System.Drawing.Size(289, 20);
            this.textBoxSourceValue.TabIndex = 4;
            this.textBoxSourceValue.TextChanged += new System.EventHandler(this.textBoxSourceValue_TextChanged);
            // 
            // textBoxTargetValue
            // 
            this.textBoxTargetValue.Location = new System.Drawing.Point(374, 426);
            this.textBoxTargetValue.Name = "textBoxTargetValue";
            this.textBoxTargetValue.Size = new System.Drawing.Size(289, 20);
            this.textBoxTargetValue.TabIndex = 6;
            this.textBoxTargetValue.TextChanged += new System.EventHandler(this.textBoxTargetValue_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(295, 426);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Target Value:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonAddItem
            // 
            this.buttonAddItem.Location = new System.Drawing.Point(414, 454);
            this.buttonAddItem.Name = "buttonAddItem";
            this.buttonAddItem.Size = new System.Drawing.Size(79, 27);
            this.buttonAddItem.TabIndex = 7;
            this.buttonAddItem.Text = "Add";
            this.buttonAddItem.UseVisualStyleBackColor = true;
            this.buttonAddItem.Click += new System.EventHandler(this.buttonAddItem_Click);
            // 
            // buttonModifyItem
            // 
            this.buttonModifyItem.Location = new System.Drawing.Point(499, 454);
            this.buttonModifyItem.Name = "buttonModifyItem";
            this.buttonModifyItem.Size = new System.Drawing.Size(79, 27);
            this.buttonModifyItem.TabIndex = 8;
            this.buttonModifyItem.Text = "Edit";
            this.buttonModifyItem.UseVisualStyleBackColor = true;
            this.buttonModifyItem.Click += new System.EventHandler(this.buttonModifyItem_Click);
            // 
            // buttonDeleteItem
            // 
            this.buttonDeleteItem.Location = new System.Drawing.Point(584, 454);
            this.buttonDeleteItem.Name = "buttonDeleteItem";
            this.buttonDeleteItem.Size = new System.Drawing.Size(79, 27);
            this.buttonDeleteItem.TabIndex = 9;
            this.buttonDeleteItem.Text = "Delete";
            this.buttonDeleteItem.UseVisualStyleBackColor = true;
            this.buttonDeleteItem.Click += new System.EventHandler(this.buttonDeleteItem_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(585, 497);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(79, 27);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(224, 28);
            this.label4.TabIndex = 11;
            this.label4.Text = "Look Up Table List";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(12, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 2);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(298, 40);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(365, 2);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(298, 384);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(365, 2);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(12, 384);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(262, 2);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            // 
            // textBoxTableName
            // 
            this.textBoxTableName.Location = new System.Drawing.Point(88, 400);
            this.textBoxTableName.Name = "textBoxTableName";
            this.textBoxTableName.Size = new System.Drawing.Size(185, 20);
            this.textBoxTableName.TabIndex = 17;
            this.textBoxTableName.TextChanged += new System.EventHandler(this.textBoxTableName_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(13, 400);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Table Name:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonAddTable
            // 
            this.buttonAddTable.Location = new System.Drawing.Point(110, 454);
            this.buttonAddTable.Name = "buttonAddTable";
            this.buttonAddTable.Size = new System.Drawing.Size(79, 27);
            this.buttonAddTable.TabIndex = 18;
            this.buttonAddTable.Text = "Add";
            this.buttonAddTable.UseVisualStyleBackColor = true;
            this.buttonAddTable.Click += new System.EventHandler(this.buttonAddTable_Click);
            // 
            // buttonDeleteTable
            // 
            this.buttonDeleteTable.Location = new System.Drawing.Point(195, 454);
            this.buttonDeleteTable.Name = "buttonDeleteTable";
            this.buttonDeleteTable.Size = new System.Drawing.Size(79, 27);
            this.buttonDeleteTable.TabIndex = 19;
            this.buttonDeleteTable.Text = "Delete";
            this.buttonDeleteTable.UseVisualStyleBackColor = true;
            this.buttonDeleteTable.Click += new System.EventHandler(this.buttonDeleteTable_Click);
            // 
            // FormLUTMgt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(676, 534);
            this.Controls.Add(this.buttonDeleteTable);
            this.Controls.Add(this.buttonAddTable);
            this.Controls.Add(this.textBoxTableName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listViewLUTList);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonDeleteItem);
            this.Controls.Add(this.buttonModifyItem);
            this.Controls.Add(this.buttonAddItem);
            this.Controls.Add(this.textBoxTargetValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSourceValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewLUTItem);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLUTMgt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Look Up Table Management";
            this.Load += new System.EventHandler(this.FormLUTMgt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewLUTList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listViewLUTItem;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSourceValue;
        private System.Windows.Forms.TextBox textBoxTargetValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonAddItem;
        private System.Windows.Forms.Button buttonModifyItem;
        private System.Windows.Forms.Button buttonDeleteItem;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBoxTableName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAddTable;
        private System.Windows.Forms.Button buttonDeleteTable;


    }
}