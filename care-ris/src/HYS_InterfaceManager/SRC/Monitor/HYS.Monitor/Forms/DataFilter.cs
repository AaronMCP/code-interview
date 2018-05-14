using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Monitor.Controls;
using HYS.Adapter.Monitor.Utility;
using HYS.Adapter.Monitor.Objects;

namespace HYS.Adapter.Monitor
{
    public partial class DataFilter : Form
    {
        #region Local members
        private UCAdvancedMode pageAdvanced;
        Monitor parentForm;
        FilterDataInfo filterDataInfo;
        #endregion

        public DataFilter(Monitor frm)
        {
            InitializeComponent();

            filterDataInfo = frm.FilterDataInfo;
            pageAdvanced = new UCAdvancedMode(filterDataInfo);
            parentForm = frm;

            Initialization();
        }

        private void Initialization()
        {
            this.groupBoxInfo.Controls.Add(pageAdvanced);
            pageAdvanced.Dock = DockStyle.Fill;
            pageAdvanced.ShowInfo();
        }

        private string GetQueryStatement()
        {
            return pageAdvanced.GetQueryStatement();
        }

        private void Save()
        {
            pageAdvanced.Save();

            //MessageBox.Show(filterDataInfo.FilterMode.ToString());

            #region Clear up unused data
            if (filterDataInfo.FilterMode != FilterMode.AdvancedListView)
            {
                filterDataInfo.FilterItemList.Clear();
            }
            if (filterDataInfo.FilterMode != FilterMode.AdvancedText)
            {
                filterDataInfo.FilterText = "";
            }
            #endregion
        }

        #region Controls events
        private void btnExcute_Click(object sender, EventArgs e)
        {
            string queryStr = GetQueryStatement();
            parentForm.ShowDataIndexTable(queryStr);
            Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}