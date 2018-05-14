namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    partial class Channel
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
            this.panelGeneral = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxStatus = new System.Windows.Forms.CheckBox();
            this.lblExist = new System.Windows.Forms.Label();
            this.txtChannelName = new System.Windows.Forms.TextBox();
            this.lblChannelName = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelResult = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.lstvResult = new System.Windows.Forms.ListView();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.btnResultAdd = new System.Windows.Forms.Button();
            this.btnResultDelete = new System.Windows.Forms.Button();
            this.btnResultModify = new System.Windows.Forms.Button();
            this.panelCritreia = new System.Windows.Forms.Panel();
            this.buttonTest = new System.Windows.Forms.Button();
            this.btnCriteriaModify = new System.Windows.Forms.Button();
            this.btnCriteriaDelete = new System.Windows.Forms.Button();
            this.btnCriteriaAdd = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.labelFileSQLNote = new System.Windows.Forms.Label();
            this.lblCriteria = new System.Windows.Forms.Label();
            this.lstvCriteria = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.txtStatement = new System.Windows.Forms.TextBox();
            this.panelModeName = new System.Windows.Forms.Panel();
            this.checkBoxSQLText = new System.Windows.Forms.CheckBox();
            this.txtModeName = new System.Windows.Forms.TextBox();
            this.lblModeName = new System.Windows.Forms.Label();
            this.panelQueryMode = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.enumCmbbxOperationType = new EnumComboBox.EnumComboBox();
            this.lblModeType = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelResult.SuspendLayout();
            this.panelCritreia.SuspendLayout();
            this.panelModeName.SuspendLayout();
            this.panelQueryMode.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGeneral
            // 
            this.panelGeneral.Controls.Add(this.groupBox1);
            this.panelGeneral.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGeneral.Location = new System.Drawing.Point(0, 0);
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Size = new System.Drawing.Size(812, 72);
            this.panelGeneral.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxStatus);
            this.groupBox1.Controls.Add(this.lblExist);
            this.groupBox1.Controls.Add(this.txtChannelName);
            this.groupBox1.Controls.Add(this.lblChannelName);
            this.groupBox1.Location = new System.Drawing.Point(15, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(785, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // checkBoxStatus
            // 
            this.checkBoxStatus.AutoSize = true;
            this.checkBoxStatus.Checked = true;
            this.checkBoxStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStatus.Location = new System.Drawing.Point(450, 25);
            this.checkBoxStatus.Name = "checkBoxStatus";
            this.checkBoxStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxStatus.Size = new System.Drawing.Size(59, 17);
            this.checkBoxStatus.TabIndex = 1;
            this.checkBoxStatus.Text = "Enable";
            this.checkBoxStatus.UseVisualStyleBackColor = true;
            this.checkBoxStatus.CheckedChanged += new System.EventHandler(this.checkBoxStatus_CheckedChanged);
            // 
            // lblExist
            // 
            this.lblExist.AutoSize = true;
            this.lblExist.ForeColor = System.Drawing.Color.Red;
            this.lblExist.Location = new System.Drawing.Point(153, 45);
            this.lblExist.Name = "lblExist";
            this.lblExist.Size = new System.Drawing.Size(86, 13);
            this.lblExist.TabIndex = 1;
            this.lblExist.Text = "Name Is Existed!";
            this.lblExist.Visible = false;
            // 
            // txtChannelName
            // 
            this.txtChannelName.Location = new System.Drawing.Point(156, 23);
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.Size = new System.Drawing.Size(250, 20);
            this.txtChannelName.TabIndex = 0;
            this.txtChannelName.TextChanged += new System.EventHandler(this.txtChannelName_TextChanged);
            // 
            // lblChannelName
            // 
            this.lblChannelName.AutoSize = true;
            this.lblChannelName.Location = new System.Drawing.Point(23, 27);
            this.lblChannelName.Name = "lblChannelName";
            this.lblChannelName.Size = new System.Drawing.Size(77, 13);
            this.lblChannelName.TabIndex = 54;
            this.lblChannelName.Text = "Channel Name";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnOK);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 583);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(812, 40);
            this.panelBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(720, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(634, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panelResult);
            this.groupBox2.Controls.Add(this.panelCritreia);
            this.groupBox2.Controls.Add(this.panelModeName);
            this.groupBox2.Controls.Add(this.panelQueryMode);
            this.groupBox2.Location = new System.Drawing.Point(15, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(785, 503);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Query Third Party Data Source";
            // 
            // panelResult
            // 
            this.panelResult.Controls.Add(this.lblResult);
            this.panelResult.Controls.Add(this.lstvResult);
            this.panelResult.Controls.Add(this.btnResultAdd);
            this.panelResult.Controls.Add(this.btnResultDelete);
            this.panelResult.Controls.Add(this.btnResultModify);
            this.panelResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelResult.Location = new System.Drawing.Point(3, 201);
            this.panelResult.Name = "panelResult";
            this.panelResult.Size = new System.Drawing.Size(779, 299);
            this.panelResult.TabIndex = 3;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(20, 9);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(100, 13);
            this.lblResult.TabIndex = 73;
            this.lblResult.Text = "Result Set Mapping";
            // 
            // lstvResult
            // 
            this.lstvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.lstvResult.FullRowSelect = true;
            this.lstvResult.HideSelection = false;
            this.lstvResult.Location = new System.Drawing.Point(153, 9);
            this.lstvResult.Name = "lstvResult";
            this.lstvResult.Size = new System.Drawing.Size(539, 284);
            this.lstvResult.TabIndex = 0;
            this.lstvResult.UseCompatibleStateImageBehavior = false;
            this.lstvResult.View = System.Windows.Forms.View.Details;
            this.lstvResult.DoubleClick += new System.EventHandler(this.lstvResult_DoubleClick);
            this.lstvResult.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvResult_ColumnClick);
            this.lstvResult.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvResult_ItemSelectionChanged);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "#";
            this.columnHeader7.Width = 25;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Field Name";
            this.columnHeader8.Width = 93;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Field Type";
            this.columnHeader9.Width = 89;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "GC Gateway Field";
            this.columnHeader10.Width = 125;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Translation";
            this.columnHeader11.Width = 92;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Check Redundancy";
            this.columnHeader12.Width = 111;
            // 
            // btnResultAdd
            // 
            this.btnResultAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultAdd.Location = new System.Drawing.Point(698, 9);
            this.btnResultAdd.Name = "btnResultAdd";
            this.btnResultAdd.Size = new System.Drawing.Size(70, 22);
            this.btnResultAdd.TabIndex = 1;
            this.btnResultAdd.Text = "Add";
            this.btnResultAdd.UseVisualStyleBackColor = true;
            this.btnResultAdd.Click += new System.EventHandler(this.btnResultAdd_Click);
            // 
            // btnResultDelete
            // 
            this.btnResultDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultDelete.Enabled = false;
            this.btnResultDelete.Location = new System.Drawing.Point(698, 65);
            this.btnResultDelete.Name = "btnResultDelete";
            this.btnResultDelete.Size = new System.Drawing.Size(70, 22);
            this.btnResultDelete.TabIndex = 3;
            this.btnResultDelete.Text = "Delete";
            this.btnResultDelete.UseVisualStyleBackColor = true;
            this.btnResultDelete.Click += new System.EventHandler(this.btnResultDelete_Click);
            // 
            // btnResultModify
            // 
            this.btnResultModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultModify.Enabled = false;
            this.btnResultModify.Location = new System.Drawing.Point(698, 37);
            this.btnResultModify.Name = "btnResultModify";
            this.btnResultModify.Size = new System.Drawing.Size(70, 22);
            this.btnResultModify.TabIndex = 2;
            this.btnResultModify.Text = "Edit";
            this.btnResultModify.UseVisualStyleBackColor = true;
            this.btnResultModify.Click += new System.EventHandler(this.btnResultModify_Click);
            // 
            // panelCritreia
            // 
            this.panelCritreia.Controls.Add(this.buttonTest);
            this.panelCritreia.Controls.Add(this.btnCriteriaModify);
            this.panelCritreia.Controls.Add(this.btnCriteriaDelete);
            this.panelCritreia.Controls.Add(this.btnCriteriaAdd);
            this.panelCritreia.Controls.Add(this.btnApply);
            this.panelCritreia.Controls.Add(this.labelFileSQLNote);
            this.panelCritreia.Controls.Add(this.lblCriteria);
            this.panelCritreia.Controls.Add(this.lstvCriteria);
            this.panelCritreia.Controls.Add(this.txtStatement);
            this.panelCritreia.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCritreia.Location = new System.Drawing.Point(3, 82);
            this.panelCritreia.Name = "panelCritreia";
            this.panelCritreia.Size = new System.Drawing.Size(779, 119);
            this.panelCritreia.TabIndex = 2;
            // 
            // buttonTest
            // 
            this.buttonTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTest.Location = new System.Drawing.Point(632, 90);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(136, 22);
            this.buttonTest.TabIndex = 76;
            this.buttonTest.Text = "Select Sample File";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Visible = false;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // btnCriteriaModify
            // 
            this.btnCriteriaModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaModify.Enabled = false;
            this.btnCriteriaModify.Location = new System.Drawing.Point(554, 34);
            this.btnCriteriaModify.Name = "btnCriteriaModify";
            this.btnCriteriaModify.Size = new System.Drawing.Size(70, 22);
            this.btnCriteriaModify.TabIndex = 2;
            this.btnCriteriaModify.Text = "Edit";
            this.btnCriteriaModify.UseVisualStyleBackColor = true;
            this.btnCriteriaModify.Click += new System.EventHandler(this.btnCriteriaModify_Click);
            // 
            // btnCriteriaDelete
            // 
            this.btnCriteriaDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaDelete.Enabled = false;
            this.btnCriteriaDelete.Location = new System.Drawing.Point(554, 62);
            this.btnCriteriaDelete.Name = "btnCriteriaDelete";
            this.btnCriteriaDelete.Size = new System.Drawing.Size(70, 22);
            this.btnCriteriaDelete.TabIndex = 3;
            this.btnCriteriaDelete.Text = "Delete";
            this.btnCriteriaDelete.UseVisualStyleBackColor = true;
            this.btnCriteriaDelete.Click += new System.EventHandler(this.btnCriteriaDelete_Click);
            // 
            // btnCriteriaAdd
            // 
            this.btnCriteriaAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaAdd.Location = new System.Drawing.Point(554, 6);
            this.btnCriteriaAdd.Name = "btnCriteriaAdd";
            this.btnCriteriaAdd.Size = new System.Drawing.Size(70, 22);
            this.btnCriteriaAdd.TabIndex = 1;
            this.btnCriteriaAdd.Text = "Add";
            this.btnCriteriaAdd.UseVisualStyleBackColor = true;
            this.btnCriteriaAdd.Click += new System.EventHandler(this.btnCriteriaAdd_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(554, 90);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(70, 22);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Advance";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // labelFileSQLNote
            // 
            this.labelFileSQLNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFileSQLNote.Location = new System.Drawing.Point(554, 6);
            this.labelFileSQLNote.Name = "labelFileSQLNote";
            this.labelFileSQLNote.Size = new System.Drawing.Size(212, 106);
            this.labelFileSQLNote.TabIndex = 75;
            this.labelFileSQLNote.Text = "Note: Please use {FileName} to represent the data files in the folder, e.g. SELEC" +
                "T * FROM {FileName}. \r\n\r\nAnd then please click the following button to view insi" +
                "de a sample data file.";
            this.labelFileSQLNote.Visible = false;
            // 
            // lblCriteria
            // 
            this.lblCriteria.AutoSize = true;
            this.lblCriteria.Location = new System.Drawing.Point(20, 6);
            this.lblCriteria.Name = "lblCriteria";
            this.lblCriteria.Size = new System.Drawing.Size(82, 13);
            this.lblCriteria.TabIndex = 74;
            this.lblCriteria.Text = "Input Parameter";
            // 
            // lstvCriteria
            // 
            this.lstvCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstvCriteria.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader17});
            this.lstvCriteria.FullRowSelect = true;
            this.lstvCriteria.HideSelection = false;
            this.lstvCriteria.Location = new System.Drawing.Point(154, 6);
            this.lstvCriteria.Name = "lstvCriteria";
            this.lstvCriteria.Size = new System.Drawing.Size(394, 107);
            this.lstvCriteria.TabIndex = 0;
            this.lstvCriteria.UseCompatibleStateImageBehavior = false;
            this.lstvCriteria.View = System.Windows.Forms.View.Details;
            this.lstvCriteria.DoubleClick += new System.EventHandler(this.lstvCriteria_DoubleClick);
            this.lstvCriteria.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstvCriteria_ColumnClick);
            this.lstvCriteria.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstvCriteria_ItemSelectionChanged);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "#";
            this.columnHeader6.Width = 24;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Parameter Name";
            this.columnHeader13.Width = 125;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Parameter Type";
            this.columnHeader14.Width = 130;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Value";
            this.columnHeader17.Width = 96;
            // 
            // txtStatement
            // 
            this.txtStatement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatement.Location = new System.Drawing.Point(153, 6);
            this.txtStatement.Multiline = true;
            this.txtStatement.Name = "txtStatement";
            this.txtStatement.Size = new System.Drawing.Size(395, 107);
            this.txtStatement.TabIndex = 0;
            this.txtStatement.Visible = false;
            // 
            // panelModeName
            // 
            this.panelModeName.Controls.Add(this.checkBoxSQLText);
            this.panelModeName.Controls.Add(this.txtModeName);
            this.panelModeName.Controls.Add(this.lblModeName);
            this.panelModeName.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelModeName.Location = new System.Drawing.Point(3, 54);
            this.panelModeName.Name = "panelModeName";
            this.panelModeName.Size = new System.Drawing.Size(779, 28);
            this.panelModeName.TabIndex = 1;
            // 
            // checkBoxSQLText
            // 
            this.checkBoxSQLText.AutoSize = true;
            this.checkBoxSQLText.Location = new System.Drawing.Point(417, 7);
            this.checkBoxSQLText.Name = "checkBoxSQLText";
            this.checkBoxSQLText.Size = new System.Drawing.Size(200, 17);
            this.checkBoxSQLText.TabIndex = 90;
            this.checkBoxSQLText.Text = "Call Storage Procedure as SQL Text.";
            this.checkBoxSQLText.UseVisualStyleBackColor = true;
            this.checkBoxSQLText.CheckedChanged += new System.EventHandler(this.checkBoxSQLText_CheckedChanged);
            // 
            // txtModeName
            // 
            this.txtModeName.Location = new System.Drawing.Point(153, 4);
            this.txtModeName.Name = "txtModeName";
            this.txtModeName.Size = new System.Drawing.Size(249, 20);
            this.txtModeName.TabIndex = 0;
            // 
            // lblModeName
            // 
            this.lblModeName.AutoSize = true;
            this.lblModeName.Location = new System.Drawing.Point(20, 7);
            this.lblModeName.Name = "lblModeName";
            this.lblModeName.Size = new System.Drawing.Size(127, 13);
            this.lblModeName.TabIndex = 89;
            this.lblModeName.Text = "Storage Procedure Name";
            // 
            // panelQueryMode
            // 
            this.panelQueryMode.Controls.Add(this.groupBox3);
            this.panelQueryMode.Controls.Add(this.enumCmbbxOperationType);
            this.panelQueryMode.Controls.Add(this.lblModeType);
            this.panelQueryMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelQueryMode.Location = new System.Drawing.Point(3, 16);
            this.panelQueryMode.Name = "panelQueryMode";
            this.panelQueryMode.Size = new System.Drawing.Size(779, 38);
            this.panelQueryMode.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 35);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(779, 3);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // enumCmbbxOperationType
            // 
            this.enumCmbbxOperationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enumCmbbxOperationType.FormattingEnabled = true;
            this.enumCmbbxOperationType.Items.AddRange(new object[] {
            "StorageProcedure",
            "Table",
            "View"});
            this.enumCmbbxOperationType.Location = new System.Drawing.Point(153, 7);
            this.enumCmbbxOperationType.Name = "enumCmbbxOperationType";
            this.enumCmbbxOperationType.Size = new System.Drawing.Size(250, 21);
            this.enumCmbbxOperationType.StartIndex = 0;
            this.enumCmbbxOperationType.TabIndex = 0;
            this.enumCmbbxOperationType.TheType = typeof(HYS.SQLInboundAdapterObjects.ThrPartyDBOperationType);
            this.enumCmbbxOperationType.SelectedValueChanged += new System.EventHandler(this.enumCmbbxOperationType_SelectedIndexChanged);
            // 
            // lblModeType
            // 
            this.lblModeType.AutoSize = true;
            this.lblModeType.Location = new System.Drawing.Point(20, 7);
            this.lblModeType.Name = "lblModeType";
            this.lblModeType.Size = new System.Drawing.Size(72, 13);
            this.lblModeType.TabIndex = 60;
            this.lblModeType.Text = "Access Mode";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.groupBox2);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 72);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(812, 551);
            this.panelMain.TabIndex = 1;
            // 
            // Channel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 623);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelGeneral);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(820, 650);
            this.Name = "Channel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.Channel_Load);
            this.panelGeneral.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panelResult.ResumeLayout(false);
            this.panelResult.PerformLayout();
            this.panelCritreia.ResumeLayout(false);
            this.panelCritreia.PerformLayout();
            this.panelModeName.ResumeLayout(false);
            this.panelModeName.PerformLayout();
            this.panelQueryMode.ResumeLayout(false);
            this.panelQueryMode.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGeneral;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblExist;
        private System.Windows.Forms.TextBox txtChannelName;
        private System.Windows.Forms.Label lblChannelName;
        private System.Windows.Forms.CheckBox checkBoxStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ListView lstvResult;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Button btnResultAdd;
        private System.Windows.Forms.Button btnResultDelete;
        private System.Windows.Forms.Button btnResultModify;
        private System.Windows.Forms.Panel panelCritreia;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCriteriaModify;
        private System.Windows.Forms.Button btnCriteriaDelete;
        private System.Windows.Forms.Button btnCriteriaAdd;
        private System.Windows.Forms.Label lblCriteria;
        private System.Windows.Forms.ListView lstvCriteria;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.TextBox txtStatement;
        private System.Windows.Forms.Panel panelModeName;
        private System.Windows.Forms.TextBox txtModeName;
        private System.Windows.Forms.Label lblModeName;
        private System.Windows.Forms.Panel panelQueryMode;
        private System.Windows.Forms.GroupBox groupBox3;
        private EnumComboBox.EnumComboBox enumCmbbxOperationType;
        private System.Windows.Forms.Label lblModeType;
        private System.Windows.Forms.CheckBox checkBoxSQLText;
        private System.Windows.Forms.Label labelFileSQLNote;
        private System.Windows.Forms.Button buttonTest;

        public EnumComboBox.EnumComboBox EnumCmbbxOperationType {
            get { return this.enumCmbbxOperationType; }
        }
    }
}