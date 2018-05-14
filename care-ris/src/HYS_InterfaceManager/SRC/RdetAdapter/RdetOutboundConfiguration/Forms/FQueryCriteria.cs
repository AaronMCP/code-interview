using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using HYS.Adapter.Base;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Rule;
using HYS.RdetAdapter.Configuration;
using HYS.Common.Xml;

namespace HYS.RdetAdapter.RdetOutboundAdapterConfiguration.Forms
{
    public partial class FQueryCriteria : Form
    {
        #region Local members
        FChannel parentlForm;
        XCollection<RdetOutQueryCriterialItem> criteriaList;
        RdetOutQueryCriterialItem criteriaItem;
        string type;

        #endregion

        #region Constructor
        public FQueryCriteria(FChannel frm, XCollection<RdetOutQueryCriterialItem> criteriaItems)
        {
            InitializeComponent();
            parentlForm = frm;
            criteriaList = criteriaItems;
            criteriaItem = new RdetOutQueryCriterialItem();
            type = "Add";
            this.Text = "Add Criteria Item";
        }

        public FQueryCriteria(FChannel frm, XCollection<RdetOutQueryCriterialItem> criteriaItems, int index)
        {
            InitializeComponent();
            parentlForm = frm;
            criteriaList = criteriaItems;
            criteriaItem = criteriaItems[index];
            type = "Modify";
            this.Text = "Modify criteria Item";

            ShowCriteriaItem();
        }
        #endregion

        #region Show and Save
        private void ShowCriteriaItem()
        {
            this.enumCmbbxTable.Text = criteriaItem.GWDataDBField.Table.ToString();
            this.cmbbxGatewayField.Text = criteriaItem.GWDataDBField.FieldName;
            this.enumCmbbxOperator.Text = criteriaItem.Operator.ToString();
            this.txtValue.Text = criteriaItem.Translating.ConstValue;
            this.enumCmbbxJoin.Text = criteriaItem.Type.ToString();
        }

        private void Save()
        {
            criteriaItem.GWDataDBField.Table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), enumCmbbxTable.Text);
            criteriaItem.GWDataDBField.FieldName = this.cmbbxGatewayField.Text.Trim();
            criteriaItem.Operator = (QueryCriteriaOperator)Enum.Parse(typeof(QueryCriteriaOperator), this.enumCmbbxOperator.Text);
            criteriaItem.Translating.ConstValue = this.txtValue.Text;
            criteriaItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), cbValueType.Text);// TranslatingType.FixValue;
            criteriaItem.Type = (QueryCriteriaType)Enum.Parse(typeof(QueryCriteriaType), enumCmbbxJoin.Text);

            if (type == "Add")
            {
                criteriaList.Add(criteriaItem);
            }
        }
        #endregion
        


        #region Controls events
        private void enumCmbbxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbbxGatewayField.Items.Clear();
            cmbbxGatewayField.Text = "";
            if (enumCmbbxTable.Text == GWDataDBTable.Index.ToString())
            {
                cmbbxGatewayField.Items.AddRange(GWFields.GWDataIndexField);
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Patient.ToString())
            {
                cmbbxGatewayField.Items.AddRange(GWFields.GWPatientField);
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Order.ToString())
            {
                cmbbxGatewayField.Items.AddRange(GWFields.GWDataOrderField);
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Report.ToString())
            {
                cmbbxGatewayField.Items.AddRange(GWFields.GWDataReportField);
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            Save();
            parentlForm.ShowCriteriaList();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}