namespace HYS.Adapter.Monitor.Forms
{
    partial class AddRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pMain = new System.Windows.Forms.Panel();
            this.pTables = new System.Windows.Forms.Panel();
            this.pReport = new System.Windows.Forms.Panel();
            this.groupBoxReport = new System.Windows.Forms.GroupBox();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.pOrder = new System.Windows.Forms.Panel();
            this.groupBoxOrder = new System.Windows.Forms.GroupBox();
            this.dataGridViewOrder = new System.Windows.Forms.DataGridView();
            this.pPatient = new System.Windows.Forms.Panel();
            this.groupBoxPatient = new System.Windows.Forms.GroupBox();
            this.dataGridViewPatient = new System.Windows.Forms.DataGridView();
            this.pBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pTop = new System.Windows.Forms.Panel();
            this.groupBoxIndex = new System.Windows.Forms.GroupBox();
            this.dataGridViewIndex = new HYS.Adapter.Monitor.Controls.UIDataGridView();
            this.pMain.SuspendLayout();
            this.pTables.SuspendLayout();
            this.pReport.SuspendLayout();
            this.groupBoxReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            this.pOrder.SuspendLayout();
            this.groupBoxOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrder)).BeginInit();
            this.pPatient.SuspendLayout();
            this.groupBoxPatient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatient)).BeginInit();
            this.pBottom.SuspendLayout();
            this.pTop.SuspendLayout();
            this.groupBoxIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // pMain
            // 
            this.pMain.Controls.Add(this.pTables);
            this.pMain.Controls.Add(this.pBottom);
            this.pMain.Controls.Add(this.pTop);
            this.pMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pMain.Location = new System.Drawing.Point(0, 0);
            this.pMain.Name = "pMain";
            this.pMain.Size = new System.Drawing.Size(792, 666);
            this.pMain.TabIndex = 3;
            // 
            // pTables
            // 
            this.pTables.Controls.Add(this.pReport);
            this.pTables.Controls.Add(this.pOrder);
            this.pTables.Controls.Add(this.pPatient);
            this.pTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTables.Location = new System.Drawing.Point(0, 174);
            this.pTables.Name = "pTables";
            this.pTables.Size = new System.Drawing.Size(792, 450);
            this.pTables.TabIndex = 16;
            this.pTables.SizeChanged += new System.EventHandler(this.pTables_SizeChanged);
            // 
            // pReport
            // 
            this.pReport.Controls.Add(this.groupBoxReport);
            this.pReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pReport.Location = new System.Drawing.Point(0, 300);
            this.pReport.Name = "pReport";
            this.pReport.Size = new System.Drawing.Size(792, 150);
            this.pReport.TabIndex = 3;
            // 
            // groupBoxReport
            // 
            this.groupBoxReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxReport.Controls.Add(this.dataGridViewReport);
            this.groupBoxReport.Location = new System.Drawing.Point(9, 0);
            this.groupBoxReport.Name = "groupBoxReport";
            this.groupBoxReport.Size = new System.Drawing.Size(774, 150);
            this.groupBoxReport.TabIndex = 1;
            this.groupBoxReport.TabStop = false;
            this.groupBoxReport.Text = "Report Table";
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReport.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewReport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewReport.Location = new System.Drawing.Point(6, 16);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewReport.Size = new System.Drawing.Size(762, 125);
            this.dataGridViewReport.TabIndex = 0;
            this.dataGridViewReport.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReport_CellEnter);
            // 
            // pOrder
            // 
            this.pOrder.Controls.Add(this.groupBoxOrder);
            this.pOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pOrder.Location = new System.Drawing.Point(0, 150);
            this.pOrder.Name = "pOrder";
            this.pOrder.Size = new System.Drawing.Size(792, 150);
            this.pOrder.TabIndex = 1;
            // 
            // groupBoxOrder
            // 
            this.groupBoxOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOrder.Controls.Add(this.dataGridViewOrder);
            this.groupBoxOrder.Location = new System.Drawing.Point(9, 2);
            this.groupBoxOrder.Name = "groupBoxOrder";
            this.groupBoxOrder.Size = new System.Drawing.Size(774, 148);
            this.groupBoxOrder.TabIndex = 0;
            this.groupBoxOrder.TabStop = false;
            this.groupBoxOrder.Text = "Order Table";
            // 
            // dataGridViewOrder
            // 
            this.dataGridViewOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOrder.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewOrder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrder.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewOrder.Location = new System.Drawing.Point(6, 16);
            this.dataGridViewOrder.Name = "dataGridViewOrder";
            this.dataGridViewOrder.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewOrder.Size = new System.Drawing.Size(762, 126);
            this.dataGridViewOrder.TabIndex = 0;
            this.dataGridViewOrder.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewOrder_CellEnter);
            // 
            // pPatient
            // 
            this.pPatient.Controls.Add(this.groupBoxPatient);
            this.pPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.pPatient.Location = new System.Drawing.Point(0, 0);
            this.pPatient.Name = "pPatient";
            this.pPatient.Size = new System.Drawing.Size(792, 150);
            this.pPatient.TabIndex = 0;
            // 
            // groupBoxPatient
            // 
            this.groupBoxPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPatient.Controls.Add(this.dataGridViewPatient);
            this.groupBoxPatient.Location = new System.Drawing.Point(9, 0);
            this.groupBoxPatient.Name = "groupBoxPatient";
            this.groupBoxPatient.Size = new System.Drawing.Size(774, 150);
            this.groupBoxPatient.TabIndex = 0;
            this.groupBoxPatient.TabStop = false;
            this.groupBoxPatient.Text = "Patient Table";
            // 
            // dataGridViewPatient
            // 
            this.dataGridViewPatient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPatient.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewPatient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPatient.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewPatient.Location = new System.Drawing.Point(6, 16);
            this.dataGridViewPatient.Name = "dataGridViewPatient";
            this.dataGridViewPatient.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPatient.Size = new System.Drawing.Size(762, 128);
            this.dataGridViewPatient.TabIndex = 0;
            this.dataGridViewPatient.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPatient_CellEnter);
            // 
            // pBottom
            // 
            this.pBottom.Controls.Add(this.btnCancel);
            this.pBottom.Controls.Add(this.btnOK);
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(0, 624);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(792, 42);
            this.pBottom.TabIndex = 15;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(700, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 22);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(617, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(77, 22);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pTop
            // 
            this.pTop.Controls.Add(this.groupBoxIndex);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(792, 174);
            this.pTop.TabIndex = 17;
            // 
            // groupBoxIndex
            // 
            this.groupBoxIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxIndex.Controls.Add(this.dataGridViewIndex);
            this.groupBoxIndex.Location = new System.Drawing.Point(9, 3);
            this.groupBoxIndex.Name = "groupBoxIndex";
            this.groupBoxIndex.Size = new System.Drawing.Size(774, 168);
            this.groupBoxIndex.TabIndex = 13;
            this.groupBoxIndex.TabStop = false;
            this.groupBoxIndex.Text = "Index Table";
            // 
            // dataGridViewIndex
            // 
            this.dataGridViewIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewIndex.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewIndex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewIndex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewIndex.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewIndex.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewIndex.Frozen = false;
            this.dataGridViewIndex.FrozenColumn = -1;
            this.dataGridViewIndex.FrozenRow = -1;
            this.dataGridViewIndex.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewIndex.Location = new System.Drawing.Point(8, 19);
            this.dataGridViewIndex.MultiSelect = false;
            this.dataGridViewIndex.Name = "dataGridViewIndex";
            this.dataGridViewIndex.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewIndex.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewIndex.Size = new System.Drawing.Size(758, 140);
            this.dataGridViewIndex.TabIndex = 1;
            this.dataGridViewIndex.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewIndex_CellEnter);
            // 
            // AddRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 666);
            this.Controls.Add(this.pMain);
            this.Name = "AddRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.pMain.ResumeLayout(false);
            this.pTables.ResumeLayout(false);
            this.pReport.ResumeLayout(false);
            this.groupBoxReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            this.pOrder.ResumeLayout(false);
            this.groupBoxOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrder)).EndInit();
            this.pPatient.ResumeLayout(false);
            this.groupBoxPatient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatient)).EndInit();
            this.pBottom.ResumeLayout(false);
            this.pTop.ResumeLayout(false);
            this.groupBoxIndex.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIndex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pMain;
        private System.Windows.Forms.Panel pBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pTables;
        private System.Windows.Forms.Panel pReport;
        private System.Windows.Forms.GroupBox groupBoxReport;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.Panel pOrder;
        private System.Windows.Forms.GroupBox groupBoxOrder;
        private System.Windows.Forms.DataGridView dataGridViewOrder;
        private System.Windows.Forms.Panel pPatient;
        private System.Windows.Forms.GroupBox groupBoxPatient;
        private System.Windows.Forms.DataGridView dataGridViewPatient;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.GroupBox groupBoxIndex;
        private HYS.Adapter.Monitor.Controls.UIDataGridView dataGridViewIndex;



    }
}