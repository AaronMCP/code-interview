namespace XmlTest
{
    partial class FormTransform
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
            this.textBoxInXML = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxInXML = new System.Windows.Forms.CheckBox();
            this.buttonSplitItem = new System.Windows.Forms.Button();
            this.listBoxItem = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxXMLItem = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxXMLItem = new System.Windows.Forms.CheckBox();
            this.checkBoxDataSetItem = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDataSetItem = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBoxDataSet = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxXSLPath = new System.Windows.Forms.TextBox();
            this.textBoxXSL = new System.Windows.Forms.TextBox();
            this.checkBoxXSL = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridGCDataSet = new System.Windows.Forms.DataGrid();
            this.buttonTransform = new System.Windows.Forms.Button();
            this.buttonLoadDataSet = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonEventType = new System.Windows.Forms.Button();
            this.checkBoxDataSetItemOut = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxDataSetItemOut = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.listBoxDataSetOut = new System.Windows.Forms.ListBox();
            this.buttonSplitDataSet = new System.Windows.Forms.Button();
            this.checkBoxXMLItemOut = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxXMLItemOut = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.listBoxItemOut = new System.Windows.Forms.ListBox();
            this.buttonTransformOut = new System.Windows.Forms.Button();
            this.buttonSaveOut = new System.Windows.Forms.Button();
            this.buttonOpenOut = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxXSLOut = new System.Windows.Forms.TextBox();
            this.checkBoxXSLOut = new System.Windows.Forms.CheckBox();
            this.textBoxXSLPathOut = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxOutXML = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.checkBoxOutXML = new System.Windows.Forms.CheckBox();
            this.buttonMerge = new System.Windows.Forms.Button();
            this.buttonDisplayDataSet = new System.Windows.Forms.Button();
            this.listBoxMerged = new System.Windows.Forms.ListBox();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGCDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxInXML
            // 
            this.textBoxInXML.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxInXML.Location = new System.Drawing.Point(12, 34);
            this.textBoxInXML.Multiline = true;
            this.textBoxInXML.Name = "textBoxInXML";
            this.textBoxInXML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxInXML.Size = new System.Drawing.Size(293, 416);
            this.textBoxInXML.TabIndex = 12;
            this.textBoxInXML.Text = "<company>\r\n\t<name>XYZ Inc.</name>\r\n\t<address1>One Abc Way</address1>\r\n\t<address2>" +
                "Some avenue</address2>\r\n\t<city>Tech city</city>\r\n\t<country>Neverland</country>\r\n" +
                "</company>";
            this.textBoxInXML.WordWrap = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 22);
            this.label1.TabIndex = 13;
            this.label1.Text = "Input XML:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxInXML
            // 
            this.checkBoxInXML.AutoSize = true;
            this.checkBoxInXML.Location = new System.Drawing.Point(273, 16);
            this.checkBoxInXML.Name = "checkBoxInXML";
            this.checkBoxInXML.Size = new System.Drawing.Size(36, 17);
            this.checkBoxInXML.TabIndex = 14;
            this.checkBoxInXML.Text = "IE";
            this.checkBoxInXML.UseVisualStyleBackColor = true;
            // 
            // buttonSplitItem
            // 
            this.buttonSplitItem.Location = new System.Drawing.Point(321, 61);
            this.buttonSplitItem.Name = "buttonSplitItem";
            this.buttonSplitItem.Size = new System.Drawing.Size(32, 68);
            this.buttonSplitItem.TabIndex = 15;
            this.buttonSplitItem.Text = ">>";
            this.buttonSplitItem.UseVisualStyleBackColor = true;
            this.buttonSplitItem.Click += new System.EventHandler(this.buttonSplitItem_Click);
            // 
            // listBoxItem
            // 
            this.listBoxItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxItem.FormattingEnabled = true;
            this.listBoxItem.Location = new System.Drawing.Point(372, 36);
            this.listBoxItem.Name = "listBoxItem";
            this.listBoxItem.Size = new System.Drawing.Size(185, 147);
            this.listBoxItem.TabIndex = 16;
            this.listBoxItem.SelectedIndexChanged += new System.EventHandler(this.listBoxItem_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(369, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(176, 22);
            this.label2.TabIndex = 17;
            this.label2.Text = "Splitted XML List:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxXMLItem
            // 
            this.textBoxXMLItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXMLItem.Location = new System.Drawing.Point(563, 36);
            this.textBoxXMLItem.Multiline = true;
            this.textBoxXMLItem.Name = "textBoxXMLItem";
            this.textBoxXMLItem.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxXMLItem.Size = new System.Drawing.Size(294, 147);
            this.textBoxXMLItem.TabIndex = 18;
            this.textBoxXMLItem.Text = "<company>\r\n\t<name>XYZ Inc.</name>\r\n\t<address1>One Abc Way</address1>\r\n\t<address2>" +
                "Some avenue</address2>\r\n\t<city>Tech city</city>\r\n\t<country>Neverland</country>\r\n" +
                "</company>";
            this.textBoxXMLItem.WordWrap = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(560, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 22);
            this.label3.TabIndex = 19;
            this.label3.Text = "Splitted XML Item:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxXMLItem
            // 
            this.checkBoxXMLItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXMLItem.AutoSize = true;
            this.checkBoxXMLItem.Location = new System.Drawing.Point(821, 16);
            this.checkBoxXMLItem.Name = "checkBoxXMLItem";
            this.checkBoxXMLItem.Size = new System.Drawing.Size(36, 17);
            this.checkBoxXMLItem.TabIndex = 20;
            this.checkBoxXMLItem.Text = "IE";
            this.checkBoxXMLItem.UseVisualStyleBackColor = true;
            // 
            // checkBoxDataSetItem
            // 
            this.checkBoxDataSetItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDataSetItem.AutoSize = true;
            this.checkBoxDataSetItem.Location = new System.Drawing.Point(821, 192);
            this.checkBoxDataSetItem.Name = "checkBoxDataSetItem";
            this.checkBoxDataSetItem.Size = new System.Drawing.Size(36, 17);
            this.checkBoxDataSetItem.TabIndex = 25;
            this.checkBoxDataSetItem.Text = "IE";
            this.checkBoxDataSetItem.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(560, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 22);
            this.label4.TabIndex = 24;
            this.label4.Text = "Splitted DataSet Item:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDataSetItem
            // 
            this.textBoxDataSetItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDataSetItem.Location = new System.Drawing.Point(563, 211);
            this.textBoxDataSetItem.Multiline = true;
            this.textBoxDataSetItem.Name = "textBoxDataSetItem";
            this.textBoxDataSetItem.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDataSetItem.Size = new System.Drawing.Size(294, 134);
            this.textBoxDataSetItem.TabIndex = 23;
            this.textBoxDataSetItem.Text = "<company>\r\n\t<name>XYZ Inc.</name>\r\n\t<address1>One Abc Way</address1>\r\n\t<address2>" +
                "Some avenue</address2>\r\n\t<city>Tech city</city>\r\n\t<country>Neverland</country>\r\n" +
                "</company>";
            this.textBoxDataSetItem.WordWrap = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(369, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 22);
            this.label5.TabIndex = 22;
            this.label5.Text = "Splitted DataSet List:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxDataSet
            // 
            this.listBoxDataSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxDataSet.FormattingEnabled = true;
            this.listBoxDataSet.Location = new System.Drawing.Point(372, 211);
            this.listBoxDataSet.Name = "listBoxDataSet";
            this.listBoxDataSet.Size = new System.Drawing.Size(185, 134);
            this.listBoxDataSet.TabIndex = 21;
            this.listBoxDataSet.SelectedIndexChanged += new System.EventHandler(this.listBoxDataSet_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(12, 453);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(176, 22);
            this.label6.TabIndex = 26;
            this.label6.Text = "XSL File Path:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxXSLPath
            // 
            this.textBoxXSLPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxXSLPath.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxXSLPath.Location = new System.Drawing.Point(12, 477);
            this.textBoxXSLPath.Multiline = true;
            this.textBoxXSLPath.Name = "textBoxXSLPath";
            this.textBoxXSLPath.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxXSLPath.Size = new System.Drawing.Size(255, 105);
            this.textBoxXSLPath.TabIndex = 27;
            this.textBoxXSLPath.Text = "D:\\ClearCase\\GCGateway_LiangLiang_view\\gcgateway\\V2.0\\SRC\\HYS.XmlAdap" +
                "ter\\HYS.XmlAdapter.Inbound\\bin\\Debug\\XSL\\NOTIFICATION(PATIENT_ADMIT_" +
                "VISIT)_to_10.xsl";
            // 
            // textBoxXSL
            // 
            this.textBoxXSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxXSL.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxXSL.Location = new System.Drawing.Point(12, 607);
            this.textBoxXSL.Multiline = true;
            this.textBoxXSL.Name = "textBoxXSL";
            this.textBoxXSL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxXSL.Size = new System.Drawing.Size(293, 265);
            this.textBoxXSL.TabIndex = 29;
            this.textBoxXSL.Text = "<company>\r\n\t<name>XYZ Inc.</name>\r\n\t<address1>One Abc Way</address1>\r\n\t<address2>" +
                "Some avenue</address2>\r\n\t<city>Tech city</city>\r\n\t<country>Neverland</country>\r\n" +
                "</company>";
            this.textBoxXSL.WordWrap = false;
            // 
            // checkBoxXSL
            // 
            this.checkBoxXSL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxXSL.AutoSize = true;
            this.checkBoxXSL.BackColor = System.Drawing.SystemColors.Control;
            this.checkBoxXSL.Location = new System.Drawing.Point(273, 586);
            this.checkBoxXSL.Name = "checkBoxXSL";
            this.checkBoxXSL.Size = new System.Drawing.Size(36, 17);
            this.checkBoxXSL.TabIndex = 30;
            this.checkBoxXSL.Text = "IE";
            this.checkBoxXSL.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.Location = new System.Drawing.Point(12, 582);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(176, 22);
            this.label7.TabIndex = 31;
            this.label7.Text = "XSL File Content:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpen.Location = new System.Drawing.Point(273, 477);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(32, 50);
            this.buttonOpen.TabIndex = 32;
            this.buttonOpen.Text = "O";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.Location = new System.Drawing.Point(369, 350);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(176, 22);
            this.label8.TabIndex = 34;
            this.label8.Text = "GC Gateway DataSet:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataGridGCDataSet
            // 
            this.dataGridGCDataSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridGCDataSet.DataMember = "";
            this.dataGridGCDataSet.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridGCDataSet.Location = new System.Drawing.Point(372, 375);
            this.dataGridGCDataSet.Name = "dataGridGCDataSet";
            this.dataGridGCDataSet.Size = new System.Drawing.Size(485, 135);
            this.dataGridGCDataSet.TabIndex = 33;
            // 
            // buttonTransform
            // 
            this.buttonTransform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTransform.Location = new System.Drawing.Point(322, 211);
            this.buttonTransform.Name = "buttonTransform";
            this.buttonTransform.Size = new System.Drawing.Size(32, 104);
            this.buttonTransform.TabIndex = 28;
            this.buttonTransform.Text = "V";
            this.buttonTransform.UseVisualStyleBackColor = true;
            this.buttonTransform.Click += new System.EventHandler(this.buttonTransform_Click);
            // 
            // buttonLoadDataSet
            // 
            this.buttonLoadDataSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLoadDataSet.Location = new System.Drawing.Point(322, 369);
            this.buttonLoadDataSet.Name = "buttonLoadDataSet";
            this.buttonLoadDataSet.Size = new System.Drawing.Size(32, 104);
            this.buttonLoadDataSet.TabIndex = 35;
            this.buttonLoadDataSet.Text = "V";
            this.buttonLoadDataSet.UseVisualStyleBackColor = true;
            this.buttonLoadDataSet.Click += new System.EventHandler(this.buttonLoadDataSet_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(273, 531);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(32, 50);
            this.buttonSave.TabIndex = 36;
            this.buttonSave.Text = "S";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonEventType
            // 
            this.buttonEventType.Location = new System.Drawing.Point(322, 34);
            this.buttonEventType.Name = "buttonEventType";
            this.buttonEventType.Size = new System.Drawing.Size(31, 21);
            this.buttonEventType.TabIndex = 37;
            this.buttonEventType.Text = "?";
            this.buttonEventType.UseVisualStyleBackColor = true;
            this.buttonEventType.Click += new System.EventHandler(this.buttonEventType_Click);
            // 
            // checkBoxDataSetItemOut
            // 
            this.checkBoxDataSetItemOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDataSetItemOut.AutoSize = true;
            this.checkBoxDataSetItemOut.Location = new System.Drawing.Point(821, 521);
            this.checkBoxDataSetItemOut.Name = "checkBoxDataSetItemOut";
            this.checkBoxDataSetItemOut.Size = new System.Drawing.Size(36, 17);
            this.checkBoxDataSetItemOut.TabIndex = 42;
            this.checkBoxDataSetItemOut.Text = "IE";
            this.checkBoxDataSetItemOut.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.Location = new System.Drawing.Point(560, 518);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(176, 22);
            this.label9.TabIndex = 41;
            this.label9.Text = "Splitted DataSet Item:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDataSetItemOut
            // 
            this.textBoxDataSetItemOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDataSetItemOut.Location = new System.Drawing.Point(563, 540);
            this.textBoxDataSetItemOut.Multiline = true;
            this.textBoxDataSetItemOut.Name = "textBoxDataSetItemOut";
            this.textBoxDataSetItemOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDataSetItemOut.Size = new System.Drawing.Size(294, 132);
            this.textBoxDataSetItemOut.TabIndex = 40;
            this.textBoxDataSetItemOut.Text = "<company>\r\n\t<name>XYZ Inc.</name>\r\n\t<address1>One Abc Way</address1>\r\n\t<address2>" +
                "Some avenue</address2>\r\n\t<city>Tech city</city>\r\n\t<country>Neverland</country>\r\n" +
                "</company>";
            this.textBoxDataSetItemOut.WordWrap = false;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label10.Location = new System.Drawing.Point(369, 517);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(176, 22);
            this.label10.TabIndex = 39;
            this.label10.Text = "Splitted DataSet List:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxDataSetOut
            // 
            this.listBoxDataSetOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxDataSetOut.FormattingEnabled = true;
            this.listBoxDataSetOut.Location = new System.Drawing.Point(372, 540);
            this.listBoxDataSetOut.Name = "listBoxDataSetOut";
            this.listBoxDataSetOut.Size = new System.Drawing.Size(185, 134);
            this.listBoxDataSetOut.TabIndex = 38;
            this.listBoxDataSetOut.SelectedIndexChanged += new System.EventHandler(this.listBoxDataSetOut_SelectedIndexChanged);
            // 
            // buttonSplitDataSet
            // 
            this.buttonSplitDataSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSplitDataSet.Location = new System.Drawing.Point(321, 540);
            this.buttonSplitDataSet.Name = "buttonSplitDataSet";
            this.buttonSplitDataSet.Size = new System.Drawing.Size(32, 104);
            this.buttonSplitDataSet.TabIndex = 43;
            this.buttonSplitDataSet.Text = "V";
            this.buttonSplitDataSet.UseVisualStyleBackColor = true;
            this.buttonSplitDataSet.Click += new System.EventHandler(this.buttonSplitDataSet_Click);
            // 
            // checkBoxXMLItemOut
            // 
            this.checkBoxXMLItemOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXMLItemOut.AutoSize = true;
            this.checkBoxXMLItemOut.Location = new System.Drawing.Point(821, 690);
            this.checkBoxXMLItemOut.Name = "checkBoxXMLItemOut";
            this.checkBoxXMLItemOut.Size = new System.Drawing.Size(36, 17);
            this.checkBoxXMLItemOut.TabIndex = 49;
            this.checkBoxXMLItemOut.Text = "IE";
            this.checkBoxXMLItemOut.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.Location = new System.Drawing.Point(560, 687);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(176, 22);
            this.label11.TabIndex = 48;
            this.label11.Text = "Splitted XML Item:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxXMLItemOut
            // 
            this.textBoxXMLItemOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXMLItemOut.Location = new System.Drawing.Point(563, 709);
            this.textBoxXMLItemOut.Multiline = true;
            this.textBoxXMLItemOut.Name = "textBoxXMLItemOut";
            this.textBoxXMLItemOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxXMLItemOut.Size = new System.Drawing.Size(294, 160);
            this.textBoxXMLItemOut.TabIndex = 47;
            this.textBoxXMLItemOut.Text = "<company>\r\n\t<name>XYZ Inc.</name>\r\n\t<address1>One Abc Way</address1>\r\n\t<address2>" +
                "Some avenue</address2>\r\n\t<city>Tech city</city>\r\n\t<country>Neverland</country>\r\n" +
                "</company>";
            this.textBoxXMLItemOut.WordWrap = false;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.Location = new System.Drawing.Point(369, 686);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(176, 22);
            this.label12.TabIndex = 46;
            this.label12.Text = "Splitted XML List:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBoxItemOut
            // 
            this.listBoxItemOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxItemOut.FormattingEnabled = true;
            this.listBoxItemOut.Location = new System.Drawing.Point(372, 709);
            this.listBoxItemOut.Name = "listBoxItemOut";
            this.listBoxItemOut.Size = new System.Drawing.Size(185, 160);
            this.listBoxItemOut.TabIndex = 45;
            this.listBoxItemOut.SelectedIndexChanged += new System.EventHandler(this.listBoxItemOut_SelectedIndexChanged);
            // 
            // buttonTransformOut
            // 
            this.buttonTransformOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTransformOut.Location = new System.Drawing.Point(321, 709);
            this.buttonTransformOut.Name = "buttonTransformOut";
            this.buttonTransformOut.Size = new System.Drawing.Size(32, 68);
            this.buttonTransformOut.TabIndex = 44;
            this.buttonTransformOut.Text = "V";
            this.buttonTransformOut.UseVisualStyleBackColor = true;
            this.buttonTransformOut.Click += new System.EventHandler(this.buttonTransformOut_Click);
            // 
            // buttonSaveOut
            // 
            this.buttonSaveOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveOut.Location = new System.Drawing.Point(909, 89);
            this.buttonSaveOut.Name = "buttonSaveOut";
            this.buttonSaveOut.Size = new System.Drawing.Size(32, 50);
            this.buttonSaveOut.TabIndex = 63;
            this.buttonSaveOut.Text = "S";
            this.buttonSaveOut.UseVisualStyleBackColor = true;
            this.buttonSaveOut.Click += new System.EventHandler(this.buttonSaveOut_Click);
            // 
            // buttonOpenOut
            // 
            this.buttonOpenOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenOut.Location = new System.Drawing.Point(909, 34);
            this.buttonOpenOut.Name = "buttonOpenOut";
            this.buttonOpenOut.Size = new System.Drawing.Size(32, 50);
            this.buttonOpenOut.TabIndex = 62;
            this.buttonOpenOut.Text = "O";
            this.buttonOpenOut.UseVisualStyleBackColor = true;
            this.buttonOpenOut.Click += new System.EventHandler(this.buttonOpenOut_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.Location = new System.Drawing.Point(909, 139);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(176, 22);
            this.label13.TabIndex = 61;
            this.label13.Text = "XSL File Content:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxXSLOut
            // 
            this.textBoxXSLOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXSLOut.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxXSLOut.Location = new System.Drawing.Point(909, 164);
            this.textBoxXSLOut.Multiline = true;
            this.textBoxXSLOut.Name = "textBoxXSLOut";
            this.textBoxXSLOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxXSLOut.Size = new System.Drawing.Size(293, 265);
            this.textBoxXSLOut.TabIndex = 59;
            this.textBoxXSLOut.Text = "<company>\r\n\t<name>XYZ Inc.</name>\r\n\t<address1>One Abc Way</address1>\r\n\t<address2>" +
                "Some avenue</address2>\r\n\t<city>Tech city</city>\r\n\t<country>Neverland</country>\r\n" +
                "</company>";
            this.textBoxXSLOut.WordWrap = false;
            // 
            // checkBoxXSLOut
            // 
            this.checkBoxXSLOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxXSLOut.AutoSize = true;
            this.checkBoxXSLOut.BackColor = System.Drawing.SystemColors.Control;
            this.checkBoxXSLOut.Location = new System.Drawing.Point(1170, 143);
            this.checkBoxXSLOut.Name = "checkBoxXSLOut";
            this.checkBoxXSLOut.Size = new System.Drawing.Size(36, 17);
            this.checkBoxXSLOut.TabIndex = 60;
            this.checkBoxXSLOut.Text = "IE";
            this.checkBoxXSLOut.UseVisualStyleBackColor = false;
            // 
            // textBoxXSLPathOut
            // 
            this.textBoxXSLPathOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXSLPathOut.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxXSLPathOut.Location = new System.Drawing.Point(947, 34);
            this.textBoxXSLPathOut.Multiline = true;
            this.textBoxXSLPathOut.Name = "textBoxXSLPathOut";
            this.textBoxXSLPathOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxXSLPathOut.Size = new System.Drawing.Size(255, 105);
            this.textBoxXSLPathOut.TabIndex = 58;
            this.textBoxXSLPathOut.Text = "D:\\ClearCase\\GCGateway_LiangLiang_view\\gcgateway\\V2.0\\SRC\\HYS.XmlAdap" +
                "ter\\HYS.XmlAdapter.Outbound\\bin\\Debug\\XSL\\12_to_PATIENT_DISCHARGE.xs" +
                "l";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.Location = new System.Drawing.Point(944, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(176, 22);
            this.label14.TabIndex = 57;
            this.label14.Text = "XSL File Path:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxOutXML
            // 
            this.textBoxOutXML.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutXML.Location = new System.Drawing.Point(907, 456);
            this.textBoxOutXML.Multiline = true;
            this.textBoxOutXML.Name = "textBoxOutXML";
            this.textBoxOutXML.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOutXML.Size = new System.Drawing.Size(293, 216);
            this.textBoxOutXML.TabIndex = 64;
            this.textBoxOutXML.Text = "<company>\r\n\t<name>XYZ Inc.</name>\r\n\t<address1>One Abc Way</address1>\r\n\t<address2>" +
                "Some avenue</address2>\r\n\t<city>Tech city</city>\r\n\t<country>Neverland</country>\r\n" +
                "</company>";
            this.textBoxOutXML.WordWrap = false;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.Location = new System.Drawing.Point(904, 434);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(176, 22);
            this.label15.TabIndex = 65;
            this.label15.Text = "Output XML:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxOutXML
            // 
            this.checkBoxOutXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxOutXML.AutoSize = true;
            this.checkBoxOutXML.Location = new System.Drawing.Point(1168, 438);
            this.checkBoxOutXML.Name = "checkBoxOutXML";
            this.checkBoxOutXML.Size = new System.Drawing.Size(36, 17);
            this.checkBoxOutXML.TabIndex = 66;
            this.checkBoxOutXML.Text = "IE";
            this.checkBoxOutXML.UseVisualStyleBackColor = true;
            // 
            // buttonMerge
            // 
            this.buttonMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMerge.Location = new System.Drawing.Point(869, 709);
            this.buttonMerge.Name = "buttonMerge";
            this.buttonMerge.Size = new System.Drawing.Size(32, 68);
            this.buttonMerge.TabIndex = 67;
            this.buttonMerge.Text = "M     >>";
            this.buttonMerge.UseVisualStyleBackColor = true;
            this.buttonMerge.Click += new System.EventHandler(this.buttonMerge_Click);
            // 
            // buttonDisplayDataSet
            // 
            this.buttonDisplayDataSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDisplayDataSet.Location = new System.Drawing.Point(869, 456);
            this.buttonDisplayDataSet.Name = "buttonDisplayDataSet";
            this.buttonDisplayDataSet.Size = new System.Drawing.Size(32, 54);
            this.buttonDisplayDataSet.TabIndex = 68;
            this.buttonDisplayDataSet.Text = ">>";
            this.buttonDisplayDataSet.UseVisualStyleBackColor = true;
            this.buttonDisplayDataSet.Click += new System.EventHandler(this.buttonDisplayDataSet_Click);
            // 
            // listBoxMerged
            // 
            this.listBoxMerged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMerged.FormattingEnabled = true;
            this.listBoxMerged.Location = new System.Drawing.Point(907, 709);
            this.listBoxMerged.Name = "listBoxMerged";
            this.listBoxMerged.Size = new System.Drawing.Size(293, 160);
            this.listBoxMerged.TabIndex = 69;
            this.listBoxMerged.SelectedIndexChanged += new System.EventHandler(this.listBoxMerged_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.Location = new System.Drawing.Point(907, 687);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(176, 22);
            this.label16.TabIndex = 70;
            this.label16.Text = "Merged XML Item:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormTransform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 894);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.listBoxMerged);
            this.Controls.Add(this.buttonDisplayDataSet);
            this.Controls.Add(this.buttonMerge);
            this.Controls.Add(this.textBoxOutXML);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.checkBoxOutXML);
            this.Controls.Add(this.buttonSaveOut);
            this.Controls.Add(this.buttonOpenOut);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBoxXSLOut);
            this.Controls.Add(this.checkBoxXSLOut);
            this.Controls.Add(this.textBoxXSLPathOut);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.checkBoxXMLItemOut);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxXMLItemOut);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.listBoxItemOut);
            this.Controls.Add(this.buttonTransformOut);
            this.Controls.Add(this.buttonSplitDataSet);
            this.Controls.Add(this.checkBoxDataSetItemOut);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxDataSetItemOut);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.listBoxDataSetOut);
            this.Controls.Add(this.buttonEventType);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoadDataSet);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridGCDataSet);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxXSL);
            this.Controls.Add(this.checkBoxXSL);
            this.Controls.Add(this.buttonTransform);
            this.Controls.Add(this.textBoxXSLPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBoxDataSetItem);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxDataSetItem);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBoxDataSet);
            this.Controls.Add(this.checkBoxXMLItem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxXMLItem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxItem);
            this.Controls.Add(this.buttonSplitItem);
            this.Controls.Add(this.textBoxInXML);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxInXML);
            this.MinimumSize = new System.Drawing.Size(1220, 928);
            this.Name = "FormTransform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormTransform";
            this.Load += new System.EventHandler(this.FormInbound_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGCDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInXML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxInXML;
        private System.Windows.Forms.Button buttonSplitItem;
        private System.Windows.Forms.ListBox listBoxItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxXMLItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxXMLItem;
        private System.Windows.Forms.CheckBox checkBoxDataSetItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDataSetItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBoxDataSet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxXSLPath;
        private System.Windows.Forms.TextBox textBoxXSL;
        private System.Windows.Forms.CheckBox checkBoxXSL;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGrid dataGridGCDataSet;
        private System.Windows.Forms.Button buttonTransform;
        private System.Windows.Forms.Button buttonLoadDataSet;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonEventType;
        private System.Windows.Forms.CheckBox checkBoxDataSetItemOut;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxDataSetItemOut;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox listBoxDataSetOut;
        private System.Windows.Forms.Button buttonSplitDataSet;
        private System.Windows.Forms.CheckBox checkBoxXMLItemOut;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxXMLItemOut;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox listBoxItemOut;
        private System.Windows.Forms.Button buttonTransformOut;
        private System.Windows.Forms.Button buttonSaveOut;
        private System.Windows.Forms.Button buttonOpenOut;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxXSLOut;
        private System.Windows.Forms.CheckBox checkBoxXSLOut;
        private System.Windows.Forms.TextBox textBoxXSLPathOut;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxOutXML;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox checkBoxOutXML;
        private System.Windows.Forms.Button buttonMerge;
        private System.Windows.Forms.Button buttonDisplayDataSet;
        private System.Windows.Forms.ListBox listBoxMerged;
        private System.Windows.Forms.Label label16;
    }
}