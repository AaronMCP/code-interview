using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Rule;
using HYS.Adapter.Monitor.Utility;
using HYS.Adapter.Monitor.Objects;

namespace HYS.Adapter.Monitor.Forms
{
    public partial class AddRecord : Form
    {
        Monitor parentForm;
        string type = "";

        public AddRecord(Monitor frm)
        {
            InitializeComponent();
            parentForm = frm;
            this.Text = "Add Record";
            type = "Add";
            InitializationAdd();
        }

        string _guid;
        public AddRecord(string guid, Monitor frm)
        {
            InitializeComponent();
            _guid = guid;
            parentForm = frm;
            this.Text = "Modify Record";
            type = "Modify";
            ShowRecordInfo();
        }

        DataTable indexTable = null;
        DataTable patientTable = null;
        DataTable orderTable = null;
        DataTable reportTable = null;

        private void InitializationAdd()
        {
            string queryString = QueryRuleControl.GetHeadersQueryString(GWDataDBTable.Index)
                             +  QueryRuleControl.GetHeadersQueryString(GWDataDBTable.Patient)
                             + QueryRuleControl.GetHeadersQueryString(GWDataDBTable.Order)
                             + QueryRuleControl.GetHeadersQueryString(GWDataDBTable.Report);

            DataSet ds = Program.DataAccess.ExecuteQuery(queryString);
            if (ds != null && ds.Tables.Count == 4)
            {
                #region Get Table Headers
                indexTable = ds.Tables[0];
                indexTable.DefaultView.AllowNew = false;
                dataGridViewIndex.DataSource = indexTable;

                patientTable = ds.Tables[1];
                patientTable.DefaultView.AllowNew = false;
                dataGridViewPatient.DataSource = patientTable;

                orderTable = ds.Tables[2];
                orderTable.DefaultView.AllowNew = false;
                dataGridViewOrder.DataSource = orderTable;

                reportTable = ds.Tables[3];
                reportTable.DefaultView.AllowNew = false;
                dataGridViewReport.DataSource = reportTable;
                #endregion

                SetUnNullColumns();

                #region Add a record to tables
                Guid guid = Guid.NewGuid();
                DateTime now = DateTime.Now;

                DataRow row = indexTable.NewRow();
                row[GWDataDBField.i_IndexGuid.FieldName] = guid;
                row[GWDataDBField.i_DataDateTime.FieldName] = now;
                row[GWDataDBField.i_PROCESS_FLAG.FieldName] = 0;
                indexTable.Rows.Add(row);

                row = patientTable.NewRow();
                row[GWDataDBField.p_DATA_ID.FieldName] = guid;
                row[GWDataDBField.p_DATA_DT.FieldName] = now;

                patientTable.Rows.Add(row);

                row = orderTable.NewRow();
                row[GWDataDBField.o_DATA_ID.FieldName] = guid;
                row[GWDataDBField.o_DATA_DT.FieldName] = now;
                orderTable.Rows.Add(row);

                row = reportTable.NewRow();
                row[GWDataDBField.r_DATA_ID.FieldName] = guid;
                row[GWDataDBField.r_DATA_DT.FieldName] = now;
                reportTable.Rows.Add(row);
                #endregion
            }
        }

        private void ShowRecordInfo() {
            SetDataTable(dataGridViewIndex, ref indexTable, GWDataDBTable.Index);
            SetDataTable(dataGridViewPatient, ref patientTable, GWDataDBTable.Patient);
            SetDataTable(dataGridViewOrder, ref orderTable, GWDataDBTable.Order);
            SetDataTable(dataGridViewReport, ref reportTable, GWDataDBTable.Report);
            SetUnNullColumns();
        }

        private void SetDataTable(DataGridView dataGridView, ref DataTable dataTable, GWDataDBTable table)
        {
            string queryString = QueryRuleControl.GetSelectRecordString(_guid, table);
            DataSet ds = Program.DataAccess.ExecuteQuery(queryString);

            if (ds != null && ds.Tables.Count > 0)
            {
                dataTable = ds.Tables[0];
                dataTable.DefaultView.AllowNew = false;
                dataGridView.DataSource = dataTable;
            }
        }

        private bool SubmitRecord()
        {
            GWDataDBTable recordType = CheckAddValid();
            if (recordType == GWDataDBTable.None)
            {
                return false;
            }

            if (type == "Add")
            {
                if (!AddNewRecord(recordType))
                {
                    MessageBox.Show(this, "Exception occurs when add a new record to database.Do you want to continue to add this new record? \r\nIf click YES, you will continue; if click No, this operation will be given up!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else if (type == "Modify")
            {
                if (!ModifyRecord(recordType))
                {
                    MessageBox.Show(this, "Exception occurs when add a new record to database.Do you want to continue to add this new record? \r\nIf click YES, you will continue; if click No, this operation will be given up!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }
        
        #region Utility Functions
        #region Add
        private bool AddNewRecord(GWDataDBTable recordType)
        {
            string queryString = "";

            if (recordType == GWDataDBTable.None)
            {
                return false; ;
            }
            if (recordType >= GWDataDBTable.Report)
            {
                queryString += QueryRuleControl.GetAddString(dataGridViewReport.Columns, dataGridViewReport.Rows[0], GWDataDBTable.Report);
            }
            if (recordType >= GWDataDBTable.Order)
            {
                queryString += QueryRuleControl.GetAddString(dataGridViewOrder.Columns, dataGridViewOrder.Rows[0], GWDataDBTable.Order);
            }
            if (recordType >= GWDataDBTable.Patient)
            {
                queryString += QueryRuleControl.GetAddString(dataGridViewPatient.Columns, dataGridViewPatient.Rows[0], GWDataDBTable.Patient);
                int index = indexTable.Rows.Count - 1;
                queryString += QueryRuleControl.GetAddString(dataGridViewIndex.Columns, dataGridViewIndex.Rows[index], GWDataDBTable.Index);
            }

            bool result = Program.DataAccess.ExecuteNoneQuery(queryString);
            return result;
        }

        private GWDataDBTable CheckAddValid()
        {
            if (CheckRowValid(indexTable, indexTable.Rows.Count - 1, GWDBControl.DataIndexUnNullField) == false)
            {
                MessageBox.Show(this, "The record of index table is not valid!Do you want to correct this new record.\r\nIf click YES, you will correct and then continue; if click No, this operation will be given up!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return GWDataDBTable.None;
            }

            if (reportTable.Rows.Count > 0 && IsNullRow(reportTable.Rows[0]) == false)
            {
                if (!CheckRowValid(patientTable, 0, GWDBControl.PatientUnNullField))
                {
                    MessageBox.Show(this, "The record of patient table is not valid!Do you want to correct this new record.\r\nIf click YES, you will correct and then continue; if click No, this operation will be given up!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    return GWDataDBTable.None;
                }
                if (!CheckRowValid(orderTable, 0, GWDBControl.OrderUnNullField))
                {
                    MessageBox.Show(this, "The record of order table is not valid!Do you want to correct this new record.\r\nIf click YES, you will correct and then continue; if click No, this operation will be given up!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    return GWDataDBTable.None;
                }
                if (!CheckRowValid(reportTable, 0, GWDBControl.ReportUnNullField))
                {
                    MessageBox.Show(this, "The record of report table is not valid!Do you want to correct this new record.\r\nIf click YES, you will correct and then continue; if click No, this operation will be given up!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    return GWDataDBTable.None;
                }

                return GWDataDBTable.Report;
            }
            else if (orderTable.Rows.Count > 0 && !IsNullRow(orderTable.Rows[0]))
            {
                if (!CheckRowValid(patientTable, 0, GWDBControl.PatientUnNullField))
                {
                    MessageBox.Show(this, "The record of patient table is not valid!Do you want to correct this new record.\r\nIf click YES, you will correct and then continue; if click No, this operation will be given up!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    return GWDataDBTable.None;
                }
                if (!CheckRowValid(orderTable, 0, GWDBControl.OrderUnNullField))
                {
                    MessageBox.Show(this, "The record of order table is not valid!Do you want to correct this new record.\r\nIf click YES, you will correct and then continue; if click No, this operation will be given up!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    return GWDataDBTable.None;
                }

                return GWDataDBTable.Order;
            }
            else if (patientTable.Rows.Count > 0 && !IsNullRow(patientTable.Rows[0]))
            {
                if (!CheckRowValid(patientTable, 0, GWDBControl.PatientUnNullField))
                {
                    MessageBox.Show(this, "The record of patient table is not valid!Do you want to correct this new record.\r\nIf click YES, you will correct and then continue; if click No, this operation will be given up!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    return GWDataDBTable.None;
                }
                else
                {
                    return GWDataDBTable.Patient;
                }
            }
            else
            {
                MessageBox.Show(this, "It is not valid that none of the patient or order or report table has a record!Do you want to correct this new record.\r\nIf click YES, you will correct and then continue; if click No, this operation will be given up!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return GWDataDBTable.None;
            }
        }
        #endregion

        #region Mpdify
        private bool ModifyRecord(GWDataDBTable recordType)
        {
            string queryString = "";

            if (recordType == GWDataDBTable.None)
            {
                return false; ;
            }
            if (recordType >= GWDataDBTable.Report)
            {
                queryString += QueryRuleControl.GetUpdateString(dataGridViewReport.Columns, dataGridViewReport.Rows[0], GWDataDBTable.Report);
            }
            if (recordType >= GWDataDBTable.Order)
            {
                queryString += QueryRuleControl.GetUpdateString(dataGridViewOrder.Columns, dataGridViewOrder.Rows[0], GWDataDBTable.Order);
            }
            if (recordType >= GWDataDBTable.Patient)
            {
                queryString += QueryRuleControl.GetUpdateString(dataGridViewPatient.Columns, dataGridViewPatient.Rows[0], GWDataDBTable.Patient);
                int index = indexTable.Rows.Count - 1;
                queryString += QueryRuleControl.GetUpdateString(dataGridViewIndex.Columns, dataGridViewIndex.Rows[index], GWDataDBTable.Index);
            }

            bool result = Program.DataAccess.ExecuteNoneQuery(queryString);
            return result;
        }
        #endregion

        private bool CheckRowValid(DataTable table, int index, object[] list)
        {
            DataRow row = table.Rows[index];

            if (!IsNullRow(row))
            {
                foreach (object o in list)
                {
                    string colName = o.ToString();
                    if (row[colName].ToString().Trim() == "")
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsNullRow(DataRow row)
        {
            int i = 0;
            foreach (object o in row.ItemArray)
            {
                if (o.ToString().Trim() != "")
                {
                    i++;
                }
            }

            if (i > 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region DataGrid Style
        private void dataGridViewIndex_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                dataGridViewIndex.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            else
            {
                dataGridViewIndex.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }
        }

        private void dataGridViewPatient_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                dataGridViewPatient.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            else
            {
                dataGridViewPatient.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }
        }

        private void dataGridViewOrder_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                dataGridViewOrder.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            else
            {
                dataGridViewOrder.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }
        }

        private void dataGridViewReport_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                dataGridViewReport.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            else
            {
                dataGridViewReport.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }
        }

        private void SetUnNullColumns() {            
            for (int i = 0; i<indexTable.Columns.Count;i++)
            {
                foreach (object o in GWDBControl.DataIndexUnNullField)
                {
                    if (indexTable.Columns[i].ColumnName == o.ToString())
                    {
                        dataGridViewIndex.Columns[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;// System.Drawing.SystemColors.Info;
                    }
                }
            }

            for (int i = 0; i < patientTable.Columns.Count; i++)
            {
                foreach (object o in GWDBControl.PatientUnNullField)
                {
                    if (patientTable.Columns[i].ColumnName == o.ToString())
                    {
                        dataGridViewPatient.Columns[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;// System.Drawing.SystemColors.Info;
                    }
                }
            }

            for (int i = 0; i < orderTable.Columns.Count; i++)
            {
                foreach (object o in GWDBControl.OrderUnNullField)
                {
                    if (orderTable.Columns[i].ColumnName == o.ToString())
                    {
                        dataGridViewOrder.Columns[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;// System.Drawing.SystemColors.Info;
                    }
                }
            }

            for (int i = 0; i < reportTable.Columns.Count; i++)
            {
                foreach (object o in GWDBControl.ReportUnNullField)
                {
                    if (reportTable.Columns[i].ColumnName == o.ToString())
                    {
                        dataGridViewReport.Columns[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;// System.Drawing.SystemColors.Info;
                    }
                }
            }
        }
        #endregion

        private void pTables_SizeChanged(object sender, EventArgs e)
        {
            int totalHeight = pTables.Height;
            pPatient.Height = totalHeight / 3;
            pOrder.Height = totalHeight / 3;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SubmitRecord())
            {
                parentForm.AfterAddRecord(indexTable);

                this.Close();
            }
            //parentForm.AfterAddRecord(indexTable);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}