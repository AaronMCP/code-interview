using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;
using HYS.SQLInboundAdapterObjects;

namespace HYS.SQLInboundAdapterConfiguration.UIControl
{
        public partial class UIDataGrid : DataGrid
        {
            public UIDataGrid()
            {
                InitializeComponent();
            }

            public void MakeTable(DataTable table, XCollection<SQLInQueryCriteriaItem> queryCriteriaList)
            {
                #region DataGrid DataSource configuration
                // Declare variables for DataColumn and DataRow objects.
                DataColumn column;
                DataRow row;

                //Set the DataGrid Caption
                this.CaptionText = table.TableName;

                // Create new DataColumn, set DataType, 
                // ColumnName and add to DataTable.    
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "ID";
                column.Caption = "ID";
                //column.AutoIncrement = false;
                //column.AllowDBNull = false;
                //column.ReadOnly = false;
                //column.Unique = true;
                table.Columns.Add(column);

                // Create second column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Field Name";
                column.Caption = "Field Name";
                //column.AllowDBNull = false;
                //column.AutoIncrement = false;
                //column.ReadOnly = false;
                //column.Unique = false;
                table.Columns.Add(column);

                // Create third column.
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Field Type";
                column.Caption = "Field Type";
                //column.AllowDBNull = false;
                //column.AutoIncrement = false;
                //column.ReadOnly = false;
                //column.Unique = false;
                table.Columns.Add(column);

                //Create forth column
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Field Vaule";
                column.Caption = "Field Value";
                //column.AllowDBNull = false;
                //column.AutoIncrement = false;
                //column.ReadOnly = false;
                //column.Unique = false;
                table.Columns.Add(column);

                //Create fifth column
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Join Type";
                column.Caption = "Join Type";
                //column.AllowDBNull = false;
                //column.AutoIncrement = false;
                //column.ReadOnly = false;
                //column.Unique = false;
                table.Columns.Add(column);

                //DataColumn[] PrimaryKeyColumns = new DataColumn[1];
                //PrimaryKeyColumns[0] = table.Columns["id"];
                //table.PrimaryKey = PrimaryKeyColumns;

                this.DataSource = table;

                // Create four new DataRow objects and add them to the DataTable
                int i = 0;
                foreach (SQLInQueryCriteriaItem item in queryCriteriaList)
                {
                    i++;
                    row = table.NewRow();
                    row[0] = i;
                    row[1] = item.ThirdPartyDBPatamter.FieldName;
                    row[2] = item.ThirdPartyDBPatamter.FieldType;
                    row[3] = "value";
                    row[4] = item.Type.ToString();
                    table.Rows.Add(row);
                }
                table.DefaultView.AllowNew = false;
                //table.DefaultView.AllowEdit = false;

                #endregion

                #region DataGridTableStyle Configuration
                DataGridTableStyle tableStyle = new DataGridTableStyle();
                tableStyle.MappingName = table.TableName;
                this.TableStyles.Add(tableStyle);
                tableStyle.RowHeadersVisible = true;

                tableStyle.ReadOnly = false;
                //tableStyle.RowHeaderWidth = 10;
                tableStyle.AllowSorting = true;
                tableStyle.HeaderBackColor = System.Drawing.SystemColors.Control;
                tableStyle.HeaderForeColor = Color.Black;
                //tableStyle.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 9F,
                //System.Drawing.FontStyle.Bold,
                //System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
                tableStyle.GridLineColor = Color.DarkGray;
                tableStyle.PreferredRowHeight = 25;
                this.BackgroundColor = Color.White;

                GridColumnStylesCollection colStyle = this.TableStyles[0].GridColumnStyles;
                colStyle[0].Width = 30; colStyle[0].NullText = ""; colStyle[0].ReadOnly = true;
                colStyle[1].Width = 70; colStyle[1].NullText = "";
                colStyle[2].Width = 70; colStyle[2].NullText = ""; colStyle[2].ReadOnly = true;
                colStyle[3].Width = 70; colStyle[3].NullText = "";
                colStyle[4].Width = 70; colStyle[4].NullText = "";


                //The color of controls initiallization
                ((DataGridTextBoxColumn)this.TableStyles[0].GridColumnStyles[0]).TextBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                ((DataGridTextBoxColumn)this.TableStyles[0].GridColumnStyles[1]).TextBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                ((DataGridTextBoxColumn)this.TableStyles[0].GridColumnStyles[2]).TextBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                ((DataGridTextBoxColumn)this.TableStyles[0].GridColumnStyles[3]).TextBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                ((DataGridTextBoxColumn)this.TableStyles[0].GridColumnStyles[4]).TextBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                //this.TableStyles[0].HeaderBackColor = System.Drawing.SystemColors.InactiveCaption;

                //this.TableStyles[0].SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
                this.TableStyles[0].SelectionBackColor = Color.FromArgb(169, 178, 202);

                //Add comboBox to Field type column
                DataGridTextBoxColumn tableColumn = (DataGridTextBoxColumn)this.TableStyles[0].GridColumnStyles[2];
                comboBox = MakeComboBox(typeof(OleDbType));
                //comboBox.Visible = false;
                comboBox.SelectionChangeCommitted += new EventHandler(comboBox_SelectionChangeCommitted);
                tableColumn.TextBox.Controls.Add(comboBox);
                tableColumn.TextBox.Enter += new EventHandler(TextBox_Enter);

                //Add comboBox to Join Type column
                tableColumn = (DataGridTextBoxColumn)this.TableStyles[0].GridColumnStyles[4];
                comboBox = MakeComboBox(typeof(QueryCriteriaType));
                comboBox.SelectionChangeCommitted += new EventHandler(comboBox_SelectionChangeCommitted);
                tableColumn.TextBox.Controls.Add(comboBox);
                tableColumn.TextBox.Enter += new EventHandler(TextBox_Enter);
                #endregion
            }

            #region  ComboBox Configuration
            ComboBox comboBox;
            public bool ComboBoxVisible
            {
                set { comboBox.Visible = value; }
            }
            private ComboBox MakeComboBox(Type type)
            {
                EnumComboBox.EnumComboBox comboBox = new EnumComboBox.EnumComboBox();
                comboBox.TheType = type;
                comboBox.Cursor = Cursors.Arrow;
                //comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox.Dock = DockStyle.Fill;

                return comboBox;
            }
            private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
            {
                this[this.CurrentCell] = ((ComboBox)sender).SelectedItem.ToString();
            }
            private void TextBox_Enter(object sender, EventArgs e)
            {
                comboBox.Text = (sender as DataGridTextBox).Text;
                //MessageBox.Show(dtgridPatient[0,3].GetType().ToString());
            }
            #endregion
        }    
}
