namespace HYS.XmlAdapter.Common.Forms
{
    partial class FormMessage<T>
        where T : HYS.XmlAdapter.Common.Objects.XIMMessage, new()
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
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonAddElement = new System.Windows.Forms.Button();
            this.buttonAddChild = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.listViewMain = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRedundancy = new System.Windows.Forms.ColumnHeader();
            this.groupBoxXIS = new System.Windows.Forms.GroupBox();
            this.buttonAdvance = new System.Windows.Forms.Button();
            this.comboBoxGCGateway = new System.Windows.Forms.ComboBox();
            this.labelGCEventType = new System.Windows.Forms.Label();
            this.comboBoxHL7 = new System.Windows.Forms.ComboBox();
            this.labelHL7EventType = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBoxXIS.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Location = new System.Drawing.Point(723, 100);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(87, 27);
            this.buttonEdit.TabIndex = 6;
            this.buttonEdit.Text = "Edit Mapping";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Location = new System.Drawing.Point(723, 166);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(87, 27);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Visible = false;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonAddElement);
            this.groupBox1.Controls.Add(this.buttonAddChild);
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Controls.Add(this.listViewMain);
            this.groupBox1.Controls.Add(this.buttonEdit);
            this.groupBox1.Controls.Add(this.buttonReset);
            this.groupBox1.Location = new System.Drawing.Point(12, 123);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(830, 504);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Mapping";
            // 
            // buttonAddElement
            // 
            this.buttonAddElement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddElement.Location = new System.Drawing.Point(723, 67);
            this.buttonAddElement.Name = "buttonAddElement";
            this.buttonAddElement.Size = new System.Drawing.Size(87, 27);
            this.buttonAddElement.TabIndex = 11;
            this.buttonAddElement.Text = "Insert Element";
            this.buttonAddElement.UseVisualStyleBackColor = true;
            this.buttonAddElement.Click += new System.EventHandler(this.buttonAddElement_Click);
            // 
            // buttonAddChild
            // 
            this.buttonAddChild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddChild.Location = new System.Drawing.Point(723, 34);
            this.buttonAddChild.Name = "buttonAddChild";
            this.buttonAddChild.Size = new System.Drawing.Size(87, 27);
            this.buttonAddChild.TabIndex = 10;
            this.buttonAddChild.Text = "Add Child";
            this.buttonAddChild.UseVisualStyleBackColor = true;
            this.buttonAddChild.Click += new System.EventHandler(this.buttonAddChild_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(723, 133);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(87, 27);
            this.buttonDelete.TabIndex = 9;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // listViewMain
            // 
            this.listViewMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMain.CheckBoxes = true;
            this.listViewMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeaderRedundancy});
            this.listViewMain.FullRowSelect = true;
            this.listViewMain.HideSelection = false;
            this.listViewMain.Location = new System.Drawing.Point(21, 34);
            this.listViewMain.MultiSelect = false;
            this.listViewMain.Name = "listViewMain";
            this.listViewMain.Size = new System.Drawing.Size(685, 448);
            this.listViewMain.TabIndex = 8;
            this.listViewMain.UseCompatibleStateImageBehavior = false;
            this.listViewMain.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 34;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "XIM Name";
            this.columnHeader2.Width = 219;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "XIM Type";
            this.columnHeader3.Width = 63;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "GC Gateway Field";
            this.columnHeader4.Width = 191;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Translation";
            this.columnHeader5.Width = 134;
            // 
            // columnHeaderRedundancy
            // 
            this.columnHeaderRedundancy.Text = "Check Redundancy";
            this.columnHeaderRedundancy.Width = 120;
            // 
            // groupBoxXIS
            // 
            this.groupBoxXIS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxXIS.Controls.Add(this.buttonAdvance);
            this.groupBoxXIS.Controls.Add(this.comboBoxGCGateway);
            this.groupBoxXIS.Controls.Add(this.labelGCEventType);
            this.groupBoxXIS.Controls.Add(this.comboBoxHL7);
            this.groupBoxXIS.Controls.Add(this.labelHL7EventType);
            this.groupBoxXIS.Location = new System.Drawing.Point(13, 12);
            this.groupBoxXIS.Name = "groupBoxXIS";
            this.groupBoxXIS.Size = new System.Drawing.Size(829, 105);
            this.groupBoxXIS.TabIndex = 2;
            this.groupBoxXIS.TabStop = false;
            this.groupBoxXIS.Text = "Event Type Mapping";
            // 
            // buttonAdvance
            // 
            this.buttonAdvance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdvance.Location = new System.Drawing.Point(722, 29);
            this.buttonAdvance.Name = "buttonAdvance";
            this.buttonAdvance.Size = new System.Drawing.Size(87, 27);
            this.buttonAdvance.TabIndex = 9;
            this.buttonAdvance.Text = "Advance";
            this.buttonAdvance.UseVisualStyleBackColor = true;
            this.buttonAdvance.Visible = false;
            this.buttonAdvance.Click += new System.EventHandler(this.buttonAdvance_Click);
            // 
            // comboBoxGCGateway
            // 
            this.comboBoxGCGateway.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxGCGateway.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGCGateway.FormattingEnabled = true;
            this.comboBoxGCGateway.Location = new System.Drawing.Point(176, 66);
            this.comboBoxGCGateway.MaxDropDownItems = 30;
            this.comboBoxGCGateway.Name = "comboBoxGCGateway";
            this.comboBoxGCGateway.Size = new System.Drawing.Size(529, 21);
            this.comboBoxGCGateway.TabIndex = 7;
            // 
            // labelGCEventType
            // 
            this.labelGCEventType.Location = new System.Drawing.Point(17, 65);
            this.labelGCEventType.Name = "labelGCEventType";
            this.labelGCEventType.Size = new System.Drawing.Size(171, 21);
            this.labelGCEventType.TabIndex = 6;
            this.labelGCEventType.Text = "To GC Gateway Event Type:";
            this.labelGCEventType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxHL7
            // 
            this.comboBoxHL7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxHL7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHL7.FormattingEnabled = true;
            this.comboBoxHL7.Location = new System.Drawing.Point(176, 29);
            this.comboBoxHL7.MaxDropDownItems = 30;
            this.comboBoxHL7.Name = "comboBoxHL7";
            this.comboBoxHL7.Size = new System.Drawing.Size(529, 21);
            this.comboBoxHL7.TabIndex = 5;
            // 
            // labelHL7EventType
            // 
            this.labelHL7EventType.Location = new System.Drawing.Point(17, 29);
            this.labelHL7EventType.Name = "labelHL7EventType";
            this.labelHL7EventType.Size = new System.Drawing.Size(169, 21);
            this.labelHL7EventType.TabIndex = 4;
            this.labelHL7EventType.Text = "From XIS Event Type:";
            this.labelHL7EventType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(755, 636);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(662, 636);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 27);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 672);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxXIS);
            this.MinimizeBox = false;
            this.Name = "FormMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message Mapping";
            this.Load += new System.EventHandler(this.FormMessage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBoxXIS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxXIS;
        private System.Windows.Forms.Label labelHL7EventType;
        private System.Windows.Forms.ListView listViewMain;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ComboBox comboBoxHL7;
        private System.Windows.Forms.ComboBox comboBoxGCGateway;
        private System.Windows.Forms.Label labelGCEventType;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ColumnHeader columnHeaderRedundancy;
        private System.Windows.Forms.Button buttonAdvance;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAddChild;
        private System.Windows.Forms.Button buttonAddElement;
    }
}