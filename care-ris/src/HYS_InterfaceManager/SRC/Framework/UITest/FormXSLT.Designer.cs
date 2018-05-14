namespace UITest
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.buttonAddRoot = new System.Windows.Forms.Button();
            this.buttonAddChild = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.checkBoxAutoExpand = new System.Windows.Forms.CheckBox();
            this.buttonDump = new System.Windows.Forms.Button();
            this.buttonLoadSchema = new System.Windows.Forms.Button();
            this.buttonLoadSchemaFile = new System.Windows.Forms.Button();
            this.buttonXPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(27, 88);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(352, 209);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 39;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 211;
            // 
            // buttonAddRoot
            // 
            this.buttonAddRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddRoot.Location = new System.Drawing.Point(429, 88);
            this.buttonAddRoot.Name = "buttonAddRoot";
            this.buttonAddRoot.Size = new System.Drawing.Size(80, 32);
            this.buttonAddRoot.TabIndex = 1;
            this.buttonAddRoot.Text = "Add Root";
            this.buttonAddRoot.UseVisualStyleBackColor = true;
            this.buttonAddRoot.Click += new System.EventHandler(this.buttonAddRoot_Click);
            // 
            // buttonAddChild
            // 
            this.buttonAddChild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddChild.Location = new System.Drawing.Point(429, 126);
            this.buttonAddChild.Name = "buttonAddChild";
            this.buttonAddChild.Size = new System.Drawing.Size(80, 32);
            this.buttonAddChild.TabIndex = 2;
            this.buttonAddChild.Text = "Add Child";
            this.buttonAddChild.UseVisualStyleBackColor = true;
            this.buttonAddChild.Click += new System.EventHandler(this.buttonAddChild_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(429, 164);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(80, 32);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(429, 202);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(80, 32);
            this.buttonClear.TabIndex = 4;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(24, 326);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Input Box:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValue.Location = new System.Drawing.Point(93, 323);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(286, 20);
            this.textBoxValue.TabIndex = 6;
            // 
            // checkBoxAutoExpand
            // 
            this.checkBoxAutoExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAutoExpand.AutoSize = true;
            this.checkBoxAutoExpand.Location = new System.Drawing.Point(429, 252);
            this.checkBoxAutoExpand.Name = "checkBoxAutoExpand";
            this.checkBoxAutoExpand.Size = new System.Drawing.Size(87, 17);
            this.checkBoxAutoExpand.TabIndex = 7;
            this.checkBoxAutoExpand.Text = "Auto Expand";
            this.checkBoxAutoExpand.UseVisualStyleBackColor = true;
            // 
            // buttonDump
            // 
            this.buttonDump.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDump.Location = new System.Drawing.Point(429, 311);
            this.buttonDump.Name = "buttonDump";
            this.buttonDump.Size = new System.Drawing.Size(80, 32);
            this.buttonDump.TabIndex = 8;
            this.buttonDump.Text = "Dump";
            this.buttonDump.UseVisualStyleBackColor = true;
            this.buttonDump.Click += new System.EventHandler(this.buttonDump_Click);
            // 
            // buttonLoadSchema
            // 
            this.buttonLoadSchema.Location = new System.Drawing.Point(27, 29);
            this.buttonLoadSchema.Name = "buttonLoadSchema";
            this.buttonLoadSchema.Size = new System.Drawing.Size(105, 32);
            this.buttonLoadSchema.TabIndex = 9;
            this.buttonLoadSchema.Text = "Load Schema";
            this.buttonLoadSchema.UseVisualStyleBackColor = true;
            this.buttonLoadSchema.Click += new System.EventHandler(this.buttonLoadSchema_Click);
            // 
            // buttonLoadSchemaFile
            // 
            this.buttonLoadSchemaFile.Location = new System.Drawing.Point(138, 29);
            this.buttonLoadSchemaFile.Name = "buttonLoadSchemaFile";
            this.buttonLoadSchemaFile.Size = new System.Drawing.Size(145, 32);
            this.buttonLoadSchemaFile.TabIndex = 10;
            this.buttonLoadSchemaFile.Text = "Load Schema From File";
            this.buttonLoadSchemaFile.UseVisualStyleBackColor = true;
            this.buttonLoadSchemaFile.Click += new System.EventHandler(this.buttonLoadSchemaFile_Click);
            // 
            // buttonXPath
            // 
            this.buttonXPath.Location = new System.Drawing.Point(289, 29);
            this.buttonXPath.Name = "buttonXPath";
            this.buttonXPath.Size = new System.Drawing.Size(90, 32);
            this.buttonXPath.TabIndex = 11;
            this.buttonXPath.Text = "Get XPath";
            this.buttonXPath.UseVisualStyleBackColor = true;
            this.buttonXPath.Click += new System.EventHandler(this.buttonXPath_Click);
            // 
            // FormXSLTIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 378);
            this.Controls.Add(this.buttonXPath);
            this.Controls.Add(this.buttonLoadSchemaFile);
            this.Controls.Add(this.buttonLoadSchema);
            this.Controls.Add(this.buttonDump);
            this.Controls.Add(this.checkBoxAutoExpand);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAddChild);
            this.Controls.Add(this.buttonAddRoot);
            this.Controls.Add(this.listView1);
            this.Name = "FormXSLTIn";
            this.Text = "FormXSLTIn";
            this.Load += new System.EventHandler(this.FormXSLTIn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonAddRoot;
        private System.Windows.Forms.Button buttonAddChild;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.CheckBox checkBoxAutoExpand;
        private System.Windows.Forms.Button buttonDump;
        private System.Windows.Forms.Button buttonLoadSchema;
        private System.Windows.Forms.Button buttonLoadSchemaFile;
        private System.Windows.Forms.Button buttonXPath;
    }
}