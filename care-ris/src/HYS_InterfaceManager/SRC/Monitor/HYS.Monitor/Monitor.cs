using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data.OleDb;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Rule;
using HYS.Adapter.Monitor.Utility;
using HYS.Adapter.Monitor.Objects;
using HYS.Adapter.Monitor.Forms;
using HYS.Common.Objects.Device;

namespace HYS.Adapter.Monitor
{
    public partial class Monitor : Form
    {
        #region Constructor
        public Monitor()
        {
            InitializeComponent();

            LogModuleInitialization();

            if (Program.DeviceMgt.DeviceDirInfor.Header.Direction == DirectionType.BIDIRECTIONAL &&
                Program.DeviceMgt.DeviceDirInfor.Header.Type != DeviceType.DAP)
            {
                this.tabControl.TabPages.Remove(this.tabPageDataMgt);
            }
            else
            {
                DataManagerInitialization();
            }

            if (Program.InIM)
            {
                this.MinimizeBox = false;
                //this.TopMost = true;
            }
        }
        #endregion

        #region ------Log Module------
        #region Local members
        private const string FULL_HEAD_PATTERN = @"\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d\.\d\d\d\d\d\d  \[\w*\]  {\w*}";
        private const string PARTIAL_HEAD_PATTERN = @"\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d\.\d\d\d\d\d\d  \[\w*\]";
        private const string DATEtIME_PATTERN = @"\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d\.\d\d\d\d\d\d";
        private const string LEVEL_PATTERN = @"\[\S*\]";
        private const string MODULE_PATTERN = @"{\S*}";

        private int restLenth;
        private string adapterName;
        //private string assemblyName = "";
        string directoryPath;
        private List<string> fileNameList = new List<string>();

        private DateTime lastDateStart;
        private DateTime lastDateEnd;
        private string lastType;
        #endregion

        #region Initialization
        private void LogModuleInitialization()
        {
            #region Data initialization
            this.Text = Program.DeviceMgt.DeviceDirInfor.Header.Name + " Monitor";

            #region Use for assembly name
            adapterName = Program.DeviceMgt.DeviceDirInfor.Header.Name;
            restLenth = GWLogData.DateFomat.Length + GWLogData.FilePostfix.Length + adapterName.Length + 2;
            #endregion

            directoryPath = Application.StartupPath + "\\" + GWLogData.FileDirectory;

            #region Set DateTime and LogLevel
            lastDateStart = lastDateEnd = DateTime.Now;
            lastType = LogType.Debug.ToString();

            dateTimePickerStart.Value = dateTimePickerEnd.Value = lastDateStart;
            cmbbxLevel.Text = lastType;
            #endregion

            #region Set Listview style
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 18);
            lstvLog.SmallImageList = imgList;
            #endregion
            #endregion

            #region Load default log information
            if (GetFileNameList(lastDateStart, lastDateEnd))
            {
                try
                {
                    foreach (string item in fileNameList)
                    {
                        checkedListBoxFileNames.Items.Add(item, true);
                    }

                    LogDataMgt.Log.LogItemList.Clear();
                    foreach (string fileName in fileNameList)
                    {
                        string filePath = directoryPath + "\\" + fileName;
                        //LoadFile(filePath);           //20070427  too slow when log files are too many
                    }

                    ShowLogInfo((LogType)Enum.Parse(typeof(LogType), lastType));
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            #endregion
        }
        #endregion

        #region Utility methods
        public void ShowLogInfo(LogType type)
        {
            int i = 0;
            lstvLog.Items.Clear();
            foreach (LogItem logItem in LogDataMgt.Log.LogItemList)
            {
                if (logItem.LogType >= type)
                {
                    i++;
                    ListViewItem viewItem = new ListViewItem(i.ToString());
                    SetListViewItemBackColor(viewItem, logItem.LogType);
                    viewItem.SubItems.Add(logItem.DateTime);
                    viewItem.SubItems.Add(logItem.LogType.ToString());
                    viewItem.SubItems.Add(logItem.AssemblyName);
                    viewItem.SubItems.Add(logItem.Module);
                    viewItem.SubItems.Add(logItem.Description);

                    try
                    {
                        lstvLog.Items.Add(viewItem);
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                }
            }

            if (lstvLog.Items.Count > 0) lstvLog.Items[lstvLog.Items.Count - 1].EnsureVisible();
        }

        private void SetListViewItemBackColor(ListViewItem viewItem, LogType type)
        {
            switch (type)
            {
                case LogType.Debug:
                    {
                        viewItem.BackColor = System.Drawing.Color.AliceBlue;
                        break;
                    }
                case LogType.Info:
                    {
                        viewItem.BackColor = System.Drawing.SystemColors.Info;
                        break;
                    }
                case LogType.Warning:
                    {
                        viewItem.BackColor = System.Drawing.Color.LightSteelBlue;
                        break;
                    }
                case LogType.Error:
                    {
                        viewItem.BackColor = System.Drawing.Color.Pink;
                        break;
                    }
            }
        }

        private bool LoadFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Can't load file!");
                    return false;
                }

                //In Jimmy's code, the last line in log file is always missed. So add an empty line to log file before calling Jimmy's code.

                try
                {
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine("");
                    }
                }
                catch
                {
                }

                using (StreamReader sr = File.OpenText(filePath))
                {
                    string logHead = "";
                    string start = "";
                    bool isFull = false;

                    while (!sr.EndOfStream)
                    {
                        #region Process the end at the beginning
                        if (!Regex.Match(start, PARTIAL_HEAD_PATTERN).Success)
                        {
                            while (!Regex.Match(start, PARTIAL_HEAD_PATTERN).Success)
                            {
                                if ((start = sr.ReadLine()) == null)
                                {
                                    return false;
                                }
                            }
                            continue;
                        }
                        #endregion

                        #region Get text of log item
                        StringBuilder logDescription = new StringBuilder();
                        if (Regex.Match(start, FULL_HEAD_PATTERN).Success)
                        {
                            logHead = Regex.Match(start, FULL_HEAD_PATTERN).Value;
                            isFull = true;

                            string temp = start.Substring(logHead.Length).TrimStart(' ');
                            logDescription.Append(temp);
                        }
                        else
                        {
                            logHead = Regex.Match(start, PARTIAL_HEAD_PATTERN).Value;
                            isFull = false;

                            string temp = start.Substring(logHead.Length).TrimStart(' ');
                            logDescription.Append(temp);
                        }
                        start = "";

                        #region Process following code when log info is multiline
                        while (!Regex.Match(start, PARTIAL_HEAD_PATTERN).Success)
                        {
                            if ((start = sr.ReadLine()) != null)
                            {
                                if (!Regex.Match(start, PARTIAL_HEAD_PATTERN).Success && start.Replace("=", "").Trim() != "")
                                {
                                    logDescription.Append(start + "\r\n");
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        #endregion
                        #endregion

                        #region Transform string-format to xml-format
                        try   //When occuring exception, ignore this log item
                        {
                            LogItem item = new LogItem();
                            item.DateTime = Regex.Match(logHead, DATEtIME_PATTERN).Value;
                            item.LogType = (LogType)Enum.Parse(typeof(LogType), Regex.Match(logHead, LEVEL_PATTERN).Value.Replace("[", "").Replace("]", ""));
                            item.AssemblyName = Path.GetFileName(filePath).Substring(adapterName.Length + 1, Path.GetFileName(filePath).Length - restLenth);
                            item.Module = Regex.Match(logHead, MODULE_PATTERN).Value.Replace("{", "").Replace("}", "");
                            item.Description = logDescription.ToString().Trim();

                            LogDataMgt.Log.LogItemList.Add(item);
                        }
                        catch (Exception err)
                        {
                            Program.Log.Write(err);
                        }
                        #endregion
                    }
                    return true;
                }
            }
            catch (FileNotFoundException err)
            {
                MessageBox.Show(err.Message + "\r\nMaybe it has been deleted or has not been created!", "Load File Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            catch (Exception err)
            {
                Program.Log.Write(err.Message);
                return false;
            }
        }

        private bool GetFileNameList(DateTime dateStart, DateTime dateEnd)
        {
            if (dateStart > dateEnd)
            {
                MessageBox.Show("Date parameters are valid. Please reset them.", "Invalid Parameter Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerStart.Value = lastDateStart;
                dateTimePickerEnd.Value = lastDateEnd;
                return false;
            }

            try
            {
                string directoryPath = Application.StartupPath + "\\" + GWLogData.FileDirectory;
                string[] fileList = Directory.GetFiles(directoryPath, "*" + GWLogData.FilePostfix, SearchOption.TopDirectoryOnly);

                fileNameList.Clear();
                while (int.Parse(dateStart.ToString(GWLogData.DateFomat)) <= int.Parse(dateEnd.ToString(GWLogData.DateFomat)))
                {
                    foreach (string fileName in fileList)
                    {
                        if (Regex.Match(fileName, dateStart.ToString(GWLogData.DateFomat)).Success)
                        {
                            fileNameList.Add(Path.GetFileName(fileName));
                        }
                    }
                    try
                    {
                        dateStart = dateStart.AddDays(1);
                    }
                    catch (Exception err)
                    {
                        Program.Log.Write(err.Message);
                    }
                }
                return true;
            }
            catch
            {
                MessageBox.Show("Get file list failed!");
                return false;
            }
        }
        #endregion

        #region Buttons events
        private void btnShowFileList_Click(object sender, EventArgs e)
        {
            if (dateTimePickerStart.Value > dateTimePickerEnd.Value)
            {
                MessageBox.Show("Date parameters are valid. Please reset them.", "Invalid Parameter Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                dateTimePickerStart.Value = lastDateStart;
                dateTimePickerEnd.Value = lastDateEnd;

                return;
            }

            if (GetFileNameList(dateTimePickerStart.Value, dateTimePickerEnd.Value))
            {
                checkedListBoxFileNames.Items.Clear();
                checkedListBoxFileNames.Items.AddRange(fileNameList.ToArray());
                lstvLog.Items.Clear();
                lastDateStart = dateTimePickerStart.Value;
                lastDateEnd = dateTimePickerEnd.Value;
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string directoryPath = Application.StartupPath + "\\" + GWLogData.FileDirectory;
            if (checkedListBoxFileNames.SelectedIndex >= 0)
            {
                string fileName = directoryPath + "\\" + checkedListBoxFileNames.SelectedItem;
                Process.Start("notepad.exe", fileName);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxFileNames.Items.Count; i++)
            {
                checkedListBoxFileNames.SetItemChecked(i, true);
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (int i in checkedListBoxFileNames.CheckedIndices)
            {
                checkedListBoxFileNames.SetItemChecked(i, false);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            CheckedListBox.CheckedItemCollection selectFiles = checkedListBoxFileNames.CheckedItems;

            LogDataMgt.Log.LogItemList.Clear();
            foreach (string fileName in selectFiles)
            {
                string filePath = directoryPath + "\\" + fileName;
                LoadFile(filePath);
            }

            if (lastType != cmbbxLevel.Text)
            {
                lastType = cmbbxLevel.Text;
            }

            ShowLogInfo((LogType)Enum.Parse(typeof(LogType), lastType));
        }

        private void btnShowLog_Click(object sender, EventArgs e)
        {
            if (lstvLog.SelectedItems.Count > 0)
            {
                LogItemInfo frm = new LogItemInfo();
                frm.ShowInfo(lstvLog.SelectedItems[0]);
                frm.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Please choose a log item!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Controls events
        private void checkedListBoxFileNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBoxFileNames.SelectedIndex < 0)
            {
                btnOpenFile.Enabled = false;
            }
            else
            {
                btnOpenFile.Enabled = true;
            }
        }

        private void lstvLog_DoubleClick(object sender, EventArgs e)
        {
            if (lstvLog.SelectedItems.Count < 1) return;
            LogItemInfo frm = new LogItemInfo();
            frm.ShowInfo(lstvLog.SelectedItems[0]);
            frm.ShowDialog(this);
        }

        private void lstvLog_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.lstvLog.Columns[e.Column].Tag == null)
                this.lstvLog.Columns[e.Column].Tag = true;
            bool tabK = (bool)this.lstvLog.Columns[e.Column].Tag;
            if (tabK)
                this.lstvLog.Columns[e.Column].Tag = false;
            else
                this.lstvLog.Columns[e.Column].Tag = true;
            this.lstvLog.ListViewItemSorter = new ListViewSort(e.Column, this.lstvLog.Columns[e.Column].Tag);
        }

        private void lstvLog_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                btnShowLog.Enabled = true;
            }
            else
            {
                btnShowLog.Enabled = false;
            }
        }
        #endregion
        #endregion

        #region ------Data Manager Module------
        #region Local members
        private FilterDataInfo _filterDataInfo = new FilterDataInfo();
        public FilterDataInfo FilterDataInfo {
            get { return _filterDataInfo; }
            set { _filterDataInfo = value; }
        }


        private int GUIDColumnIndex = 0;
        private void GetGUIDColumnIndex()
        {
            string queryStr = QueryRuleControl.GetHeadersQueryString(GWDataDBTable.Index);
            if (Program.DataAccess != null)
            {
                DataSet ds = Program.DataAccess.ExecuteQuery(queryStr);

                if (ds != null)
                {
                    GUIDColumnIndex = ds.Tables[0].Columns.IndexOf(GWDataDBField.i_IndexGuid.FieldName);
                }
            }
        }
        #endregion

        #region Initialization
        private void DataManagerInitialization()
        {
            #region Data initialization
            int totalHeight = pTables.Height;
            pPatient.Height = totalHeight / 3;
            pOrder.Height = totalHeight / 3;

            pLeft.Width = (pControl.Width - pMiddle.Width) / 2;
            this.dateTimePickerStart.Value = DateTime.Now;
            this.dateTimePickerEnd.Value = DateTime.Now;
            #endregion

            #region Load default database data infomation
            if (!Program.DataAccess.TestDBConnection())
            {
                btnAdd.Enabled = false;
                MessageBox.Show("Connect To Database Failed");
            }
            else
            {
                //GetGUIDColumnIndex(); 
                #region Get today query scrpt
                SimpleQuery query = new SimpleQuery();
                DateTime today = DateTime.Now;
                query.StartTime = new SimpleQueryItem(GWDataDBField.i_DataDateTime, today.ToString("yyyy-MM-dd 00:00:00.000"));
                query.EndTime = new SimpleQueryItem(GWDataDBField.i_DataDateTime, today.ToString("yyyy-MM-dd 23:59:59.999"));
                string queryStr = QueryRuleControl.GetFilterString(query);
                #endregion

                if (ShowDataIndexTable(queryStr))
                {
                    InitializationDataTables();
                }
                else {       
                    btnFilter.Enabled = false;
                    btnAdvQuery.Enabled = false;
                    btnToday.Enabled = false;
                    btnAdd.Enabled = false;
                    Program.Log.Write(LogType.Warning,"Can't find the matched tables in database!");
                    MessageBox.Show(this, "Data Management is disable!", "Warning",  MessageBoxButtons.OK ,MessageBoxIcon.Warning);
                }
            }
            #endregion
        }
        #endregion

        #region Data Filter
        DataTable indexTable;
        public bool ShowDataIndexTable(string querySql)
        {
            dataGridViewIndex.DataSource = null;
            DataSet dataSet = Program.DataAccess.ExecuteQuery(querySql);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                indexTable = dataSet.Tables[0];
                indexTable.DefaultView.AllowNew = false;
                dataGridViewIndex.DataSource = indexTable;
                dataGridViewIndex.Columns["Data_DT"].DefaultCellStyle.Format = "G";
                return true;
            }
            else {
                return false;
            }
        }

        SimpleQuery _filterInfo;
        private string GetQueryStatement()
        {
            _filterInfo = new SimpleQuery();
            GenerateQueryInfo();
            return QueryRuleControl.GetFilterString(_filterInfo);
        }

        private void GenerateQueryInfo()
        {
            if (dtpStart.Checked)
            {
                _filterInfo.StartTime = new SimpleQueryItem(GWDataDBField.i_DataDateTime, dtpStart.Value.ToString("yyyy-MM-dd 00:00:00.000"));
            }
            if (dtpEnd.Checked)
            {
                _filterInfo.EndTime = new SimpleQueryItem(GWDataDBField.i_DataDateTime, dtpEnd.Value.ToString("yyyy-MM-dd 23:59:59.999"));
            }
            if (txtEventType.Text.Trim() != "")
            {
                _filterInfo.EventType = new SimpleQueryItem(GWDataDBField.i_EventType, txtEventType.Text);
            }            
            if (txtPID.Text.Trim() != "")
            {
                _filterInfo.PatientID = new SimpleQueryItem(GWDataDBField.p_PatientID, txtPID.Text);
            }
            if (txtPatientName.Text.Trim() != "")
            {
                _filterInfo.PatientName = new SimpleQueryItem(GWDataDBField.p_PatientName, txtPatientName.Text);
            }
            if (txtOrderNo.Text.Trim() != "")
            {
                _filterInfo.OrderNo = new SimpleQueryItem(GWDataDBField.o_OrderNo, txtOrderNo.Text);
            }
            if (txtAccNo.Text.Trim() != "")
            {
                _filterInfo.AccessionNo = new SimpleQueryItem(GWDataDBField.o_FILLER_NO, txtAccNo.Text);
            }
            if (txtStudyUID.Text.Trim() != "")
            {
                _filterInfo.StudyInstanceUID = new SimpleQueryItem(GWDataDBField.o_STUDY_INSTANCE_UID, txtStudyUID.Text);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string queryStr = GetQueryStatement();
            ShowDataIndexTable(queryStr);
            RefreshTables();
        }

        private void btnDataFilter_Click(object sender, EventArgs e)
        {
            DataFilter frm = new DataFilter(this);
            frm.ShowDialog(this);
            RefreshTables();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpStart.Checked = true;
            dtpEnd.Checked = true;
            dtpStart.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;

            SimpleQuery query = new SimpleQuery();
            DateTime today = DateTime.Now;
            query.StartTime = new SimpleQueryItem(GWDataDBField.i_DataDateTime, today.ToString("yyyy-MM-dd 00:00:00.000"));
            query.EndTime = new SimpleQueryItem(GWDataDBField.i_DataDateTime, today.ToString("yyyy-MM-dd 23:59:59.999"));

            string queryStr = QueryRuleControl.GetFilterString(query);
            ShowDataIndexTable(queryStr);
            RefreshTables();
        }
        #endregion

        #region Data Management
        #region Show and Refresh Data Tables Infomation
        DataTable patientTable;
        DataTable orderTable;
        DataTable reportTable;
        private void ShowTables(string queryString) {
            DataSet ds = Program.DataAccess.ExecuteQuery(queryString);

            if (ds != null)
            {
                if (ds.Tables.Count == 3)
                {
                    patientTable = ds.Tables[0];
                    patientTable.DefaultView.AllowNew = false;
                    dataGridViewPatient.DataSource = patientTable;

                    orderTable = ds.Tables[1];
                    orderTable.DefaultView.AllowNew = false;
                    dataGridViewOrder.DataSource = orderTable;

                    reportTable = ds.Tables[2];
                    reportTable.DefaultView.AllowNew = false;
                    dataGridViewReport.DataSource = reportTable;
                }
            }
        }

        private void dataGridViewIndex_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string guid = dataGridViewIndex.Rows[e.RowIndex].Cells[GUIDColumnIndex].Value.ToString();

                string queryStr = QueryRuleControl.GetTablesString(guid);
                ShowTables(queryStr);

                dataGridViewPatient.ClearSelection();
                dataGridViewOrder.ClearSelection();
                dataGridViewReport.ClearSelection();

                dataGridViewIndex.Rows[e.RowIndex].Selected = true;
            }
        }

        private void RefreshTables() {
            if (patientTable != null && patientTable.Rows.Count > 0) {
                patientTable.Rows.Remove(patientTable.Rows[0]);
            }

            if (orderTable != null && orderTable.Rows.Count > 0)
            {
                orderTable.Rows.Remove(orderTable.Rows[0]);
            }

            if (reportTable != null && reportTable.Rows.Count > 0)
            {
                reportTable.Rows.Remove(reportTable.Rows[0]);
            }
        }

        private void InitializationDataTables() {
            string queryString = QueryRuleControl.GetHeadersQueryString(GWDataDBTable.Patient)
                             + QueryRuleControl.GetHeadersQueryString(GWDataDBTable.Order)
                             + QueryRuleControl.GetHeadersQueryString(GWDataDBTable.Report);

            ShowTables(queryString);
        }
        #endregion

        #region Controls events      
        private void Monitor_SizeChanged(object sender, EventArgs e)
        {
            int totalHeight = pTables.Height;
            pPatient.Height = totalHeight / 3;
            pOrder.Height = totalHeight / 3;

            int totalWidth = pControl.Width;
            pLeft.Width = (totalWidth - pMiddle.Width)/ 2;
        }

        #endregion

        #region Add record
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddRecord frm = new AddRecord(this);
            frm.ShowDialog(this);

            if (dataGridViewIndex.Rows.Count > 0)
            {
                dataGridViewIndex.Rows[dataGridViewIndex.Rows.Count - 1].Cells[0].Selected = true;
                dataGridViewIndex.Rows[dataGridViewIndex.Rows.Count - 1].Selected = true;
            }
            dataGridViewIndex.Focus();
        }

        //After add a new record to database, add a new record to the relevant dataTables and show them on the dataGridviews
        public void AfterAddRecord(DataTable table) {
            if (table != null && table.Rows.Count > 0) {
                DataRow row = indexTable.NewRow();

                int i = 0;
                foreach (object o in table.Rows[0].ItemArray)
                {
                    row[i] = o.ToString();
                    i++;
                }

                indexTable.Rows.Add(row);

                string guid = dataGridViewIndex.Rows[indexTable.Rows.Count - 1].Cells[GUIDColumnIndex].Value.ToString();
                string queryStr = QueryRuleControl.GetTablesString(guid);
                ShowTables(queryStr);
            }
        }
        #endregion

        #region Modify record
        string _guid;
        private void dataGridViewIndex_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewIndex.SelectedRows.Count > 0)
            {
                btnModify.Enabled = true;
                btnDelete.Enabled = true;
                _guid = dataGridViewIndex.SelectedRows[0].Cells[0].Value.ToString();
            }
            else {
                btnModify.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            AddRecord frm = new AddRecord(_guid, this);
            frm.ShowDialog(this);

            btnFilter_Click(sender, e);
            RefreshTables();
            //dataGridViewPatient.DataSource = null;
            //dataGridViewOrder.DataSource = null;
            //dataGridViewReport.DataSource = null;
        }

        #region Get Actual DataTable Row
        private int GetActualRow(string guid, GWDataDBTable tableType)
        {
            DataTable dataTable = null;
            switch (tableType)
            {
                case GWDataDBTable.Index:
                    {
                        dataTable = indexTable;
                        break;
                    }
                case GWDataDBTable.Patient:
                    {
                        dataTable = patientTable;
                        break;
                    }
                case GWDataDBTable.Order:
                    {
                        dataTable = orderTable;
                        break;
                    }
                case GWDataDBTable.Report:
                    {
                        dataTable = reportTable;
                        break;
                    }
            }

            int actualIndex = -1;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][0].ToString() == guid)
                {
                    actualIndex = i;
                    break;
                }
            }

            return actualIndex;
        }

        public bool UpdateDataTable(string guid, GWDataDBTable tableType, object[] objects)
        {
            DataTable dataTable = null;
            switch (tableType)
            {
                case GWDataDBTable.Index:
                    {
                        dataTable = indexTable;
                        break;
                    }
                case GWDataDBTable.Patient:
                    {
                        dataTable = patientTable;
                        break;
                    }
                case GWDataDBTable.Order:
                    {
                        dataTable = orderTable;
                        break;
                    }
                case GWDataDBTable.Report:
                    {
                        dataTable = reportTable;
                        break;
                    }
            }

            if (dataTable == null)
            {
                return false;
            }
            else if (dataTable.Columns.Count == objects.Length)
            {
                int actualIndex = -1;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i][0].ToString() == guid)
                    {
                        actualIndex = i;
                        break;
                    }
                }

                if (actualIndex == -1)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < objects.Length; i++)
                    {
                        dataTable.Rows[actualIndex][i] = objects[i];
                    }
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        #endregion

        #region Delete record
        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dataGridViewIndex.SelectedCells.Count > 0)
            {
                if (MessageBox.Show(this, "Are you sure to delete this record?", "Delete Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    int selectRow = dataGridViewIndex.SelectedCells[0].RowIndex;
                    int selectColumn = dataGridViewIndex.SelectedCells[0].ColumnIndex;
                    string guid = dataGridViewIndex.Rows[selectRow].Cells[GUIDColumnIndex].Value.ToString();
                    string queryString = QueryRuleControl.GetDeleteString(guid);
                    if (Program.DataAccess.ExecuteNoneQuery(queryString))
                    {
                        indexTable.Rows.Remove(indexTable.Rows[selectRow]);

                        RefreshTables();
                    }
                    if (dataGridViewIndex.SelectedCells.Count == 0 && dataGridViewIndex.Rows.Count > 0)
                    {
                        dataGridViewIndex.Rows[selectRow - 1].Selected = true;
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Please Choose a record!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Obsoleted functions
        [Obsolete("This method is expired.", false)]
        private GWDataDBTable GetSelectTable()
        {
            int i = 0;
            GWDataDBTable table = GWDataDBTable.None;
            if (dataGridViewIndex.SelectedCells.Count > 0)
            {
                i++;
                table = GWDataDBTable.Index;
            }
            if (dataGridViewPatient.SelectedCells.Count > 0)
            {
                i++;
                table = GWDataDBTable.Patient;
            }
            if (dataGridViewOrder.SelectedCells.Count > 0)
            {
                i++;
                table = GWDataDBTable.Order;
            }
            if (dataGridViewReport.SelectedCells.Count > 0)
            {
                i++;
                table = GWDataDBTable.Report;
            }

            if (table == GWDataDBTable.None)
            {
                MessageBox.Show("You don't choose a record!");
                return GWDataDBTable.None;
            }

            if (i > 1)
            {
                MessageBox.Show("Please just choose one table!");
                return GWDataDBTable.None;
            }

            return table;
        }

        [Obsolete("This method is expired.", false)]
        private string GetSelectGUID(GWDataDBTable table)
        {
            string guid = null;
            if (table == GWDataDBTable.Index)
            {
                int index = dataGridViewIndex.SelectedCells[0].RowIndex;
                guid = dataGridViewIndex.Rows[index].Cells[0].Value.ToString();
            }
            if (table == GWDataDBTable.Patient)
            {
                int index = dataGridViewPatient.SelectedCells[0].RowIndex;
                guid = dataGridViewPatient.Rows[index].Cells[0].Value.ToString();
            }
            if (table == GWDataDBTable.Order)
            {
                int index = dataGridViewOrder.SelectedCells[0].RowIndex;
                guid = dataGridViewOrder.Rows[index].Cells[0].Value.ToString();
            }
            if (table == GWDataDBTable.Report)
            {
                int index = dataGridViewReport.SelectedCells[0].RowIndex;
                guid = dataGridViewReport.Rows[index].Cells[0].Value.ToString();
            }

            return guid;
        }
        #endregion
        #endregion

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}