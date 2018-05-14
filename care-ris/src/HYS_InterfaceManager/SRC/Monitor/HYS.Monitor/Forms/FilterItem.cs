using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Adapter.Monitor.Utility;

namespace HYS.Adapter.Monitor
{
    public partial class FilterItem : Form
    {
        #region Local members
        XCollection<QueryCriteriaItem> filterItemList;
        QueryCriteriaItem filterItem;
        string type;
        #endregion

        #region Constructors
        public FilterItem(XCollection<QueryCriteriaItem> filterList)
        {
            InitializeComponent();

            filterItemList = filterList;
            this.Text = "Add Query Item";
            filterItem = new QueryCriteriaItem();
            type = "Add";
        }

        public FilterItem(XCollection<QueryCriteriaItem> filterList, int index)
        {
            InitializeComponent();

            filterItemList = filterList;
            this.Text = "Modify Query Item";
            filterItem = filterList[index];
            type = "Modify";
            ShowInfo();
        }
        #endregion

        #region Show and Save
        private void ShowInfo() {
            enumCmbbxTable.Text = filterItem.GWDataDBField.Table.ToString();
            cmbbxGWField.Text = filterItem.GWDataDBField.FieldName;
            enumCmbbxOperator.Text = filterItem.Operator.ToString();
            txtValue.Text = filterItem.Translating.ConstValue;
            enumCmbbxJoin.Text = filterItem.Type.ToString();
        }

        private void Save() {
            filterItem.GWDataDBField.Table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), enumCmbbxTable.Text);
            filterItem.GWDataDBField.FieldName = cmbbxGWField.Text;
            filterItem.Operator = (QueryCriteriaOperator)Enum.Parse(typeof(QueryCriteriaOperator), enumCmbbxOperator.Text);
            filterItem.Translating.ConstValue = txtValue.Text;
            filterItem.Type = (QueryCriteriaType)Enum.Parse(typeof(QueryCriteriaType), enumCmbbxJoin.Text);

            if (type == "Add") {
                filterItemList.Add(filterItem);
            }
        }
        #endregion

        #region Controls events
        private void enumCmbbxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbbxGWField.Items.Clear();
            if (enumCmbbxTable.Text == GWDataDBTable.Index.ToString())
            {
                cmbbxGWField.Items.AddRange(GWDBControl.GWDataIndexField);
                cmbbxGWField.SelectedIndex = 0;
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Patient.ToString())
            {
                cmbbxGWField.Items.AddRange(GWDBControl.GWPatientField);
                cmbbxGWField.SelectedIndex = 0;
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Order.ToString())
            {
                cmbbxGWField.Items.AddRange(GWDBControl.GWDataOrderField);
                cmbbxGWField.SelectedIndex = 0;
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Report.ToString())
            {
                cmbbxGWField.Items.AddRange(GWDBControl.GWDataReportField);
                cmbbxGWField.SelectedIndex = 0;
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
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