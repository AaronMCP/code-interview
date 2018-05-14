using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace CarestreamCommonCtrls
{
    public partial class CSOnlineUserCountSettingForm : RadForm
    {
        private const string ItemSpliter = ";";
        private const string PeriodCountSpliter = "|";
        private const string PeriodSpliter = "~";

        private DataTable _dtDataSource;

        public string OnlineUserCountSettingString
        {
            get 
            {
                string val = string.Empty;
                if (_dtDataSource != null)
                {
                    foreach (DataRow dr in _dtDataSource.Rows)
                    {
                        string beginTime = dr[0].ToString();
                        string endTime = dr[1].ToString();
                        string count = dr[2].ToString();
                        val += beginTime + PeriodSpliter + endTime + PeriodCountSpliter + count + ItemSpliter;
                    }
                }
                return val.TrimEnd(ItemSpliter.ToCharArray());
            }
            set 
            {
                if (_dtDataSource == null)
                {
                    _dtDataSource = new DataTable();
                    _dtDataSource.Columns.Add("BeginTime");
                    _dtDataSource.Columns.Add("EndTime");
                    _dtDataSource.Columns.Add("MaxCount");
                }
                if (!string.IsNullOrWhiteSpace(value))
                {
                    string[] items = value.Split(ItemSpliter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (items != null && items.Length > 0)
                    {
                        foreach (string item in items)
                        {
                            string[] periodCounts = item.Split(PeriodCountSpliter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (periodCounts != null && periodCounts.Length == 2)
                            {
                                string count = periodCounts[1];
                                string[] periods = periodCounts[0].Split(PeriodSpliter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                if (periods != null && periods.Length == 2)
                                {
                                    _dtDataSource.Rows.Add(new object[] { periods[0], periods[1], count });
                                }
                            }
                        }
                    }
                }
            }
        }
        
        public CSOnlineUserCountSettingForm()
        {
            InitializeComponent();
        }

        private void CSOnlineUserCountSettingForm_Load(object sender, EventArgs e)
        {
            this.dgvResult.MasterGridViewTemplate.AllowAddNewRow = false;
            this.dgvResult.MasterGridViewTemplate.AllowDeleteRow = false;
            this.dgvResult.MasterGridViewTemplate.AllowEditRow = false;
            this.dgvResult.MultiSelect = true;

            this.dtpFromTime.Format = DateTimePickerFormat.Custom;
            this.dtpFromTime.CustomFormat = "HH:mm";
            this.dtpFromTime.ShowUpDown = true;
            this.dtpFromTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                 DateTime.Now.Day, 0, 0, 0);

            this.dtpToTime.Format = DateTimePickerFormat.Custom;
            this.dtpToTime.CustomFormat = "HH:mm";
            this.dtpToTime.ShowUpDown = true;
            this.dtpToTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                DateTime.Now.Day, 23, 59, 59);

            this.dtpFromDate.Format = DateTimePickerFormat.Custom;
            this.dtpFromDate.CustomFormat = "yyyy-MM-dd";
            this.dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                 DateTime.Now.Day, 0, 0, 0);

            this.dtpToDate.Format = DateTimePickerFormat.Custom;
            this.dtpToDate.CustomFormat = "yyyy-MM-dd";
            this.dtpToDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                DateTime.Now.Day, 23, 59, 59);

            InitGrid();

            if (_dtDataSource == null)
            {
                _dtDataSource = new DataTable();
                _dtDataSource.Columns.Add("BeginTime");
                _dtDataSource.Columns.Add("EndTime");
                _dtDataSource.Columns.Add("MaxCount");
            }

            this.dgvResult.DataSource = _dtDataSource;
        }

        private void InitGrid()
        {
            GridViewDataColumn col = new GridViewDataColumn();
            col.FieldName = "BeginTime";
            col.HeaderText = "开始时间";
            col.Width = 150;
            this.dgvResult.Columns.Add(col);

            col = new GridViewDataColumn();
            col.FieldName = "EndTime";
            col.HeaderText = "结束时间";
            col.Width = 150;
            this.dgvResult.Columns.Add(col);

            col = new GridViewDataColumn();
            col.FieldName = "MaxCount";
            col.HeaderText = "最大在线用户数";
            col.Width = 150;
            this.dgvResult.Columns.Add(col);
        }

        private bool IsValid()
        {
            DateTime beginTime = new DateTime(this.dtpFromDate.Value.Year, this.dtpFromDate.Value.Month, this.dtpFromDate.Value.Day, this.dtpFromTime.Value.Hour, this.dtpFromTime.Value.Minute, this.dtpFromTime.Value.Second);
            DateTime endTime = new DateTime(this.dtpToDate.Value.Year, this.dtpToDate.Value.Month, this.dtpToDate.Value.Day, this.dtpToTime.Value.Hour, this.dtpToTime.Value.Minute, this.dtpToTime.Value.Second);
            if (beginTime > endTime)
            {
                MessageBox.Show("起始日期不得大于结束日期");
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                string beginTime = string.Format("{0} {1}", this.dtpFromDate.Value.ToString("yyyy-MM-dd"), this.dtpFromTime.Value.ToString("HH:mm"));
                string endTime = string.Format("{0} {1}", this.dtpToDate.Value.ToString("yyyy-MM-dd"), this.dtpToTime.Value.ToString("HH:mm"));
                _dtDataSource.Rows.Add(new object[] { beginTime, endTime, this.nmcCount.Text });
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgvResult.SelectedRows.Count > 0)
            {
                GridViewDataRowInfo[] rows = new GridViewDataRowInfo[this.dgvResult.SelectedRows.Count];
                this.dgvResult.SelectedRows.CopyTo(rows, 0);

                this.dgvResult.GridElement.BeginUpdate();

                for (int i = 0; i < rows.Length; i++)
                {
                    this.dgvResult.Rows.Remove(rows[i]);
                }

                this.dgvResult.GridElement.EndUpdate();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
