using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using System.Data.OleDb;

namespace HYS.SQLOutboundAdapterConfiguration.Forms
{
    public partial class QueryCriteria : Form
    {
        #region Local members
        Channel channelForm;
        string type = "";

        //Only use for modify
        SQLOutQueryCriteriaItem sqlOutQueryCriteriaItem;
        int selectIndex;
        #endregion

        #region Constructor
        public QueryCriteria(Channel channel)
        {
            InitializeComponent();
            channelForm = channel;
            type = "Add";
            sqlOutQueryCriteriaItem = new SQLOutQueryCriteriaItem();
            this.Text = "Add Filter Item";
        }
      
        public QueryCriteria(Channel channel, int index)
        {
            InitializeComponent();
            channelForm = channel;
            type = "Edit";
            this.Text = "Edit Filter Item";

            selectIndex = index;
            sqlOutQueryCriteriaItem = channel.criteriaItemList[index];
            ShowInformation(selectIndex);
        }
        #endregion

        #region Controls events
        private void enumCmbbxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbbxGWField.Items.Clear();
            if (enumCmbbxTable.Text == GWDataDBTable.Index.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWDataIndexField);
                //cmbbxGWField.Text = "";
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Patient.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWPatientField);
                //cmbbxGWField.Text = "";
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Order.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWDataOrderField);
                //cmbbxGWField.Text = "";
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Report.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWDataReportField);
                //cmbbxGWField.Text = "";
            }
        }
        #endregion

        #region Show and Save
        private void ShowInformation(int index) {
            enumCmbbxTable.Text = sqlOutQueryCriteriaItem.GWDataDBField.Table.ToString();
            cmbbxGWField.Text = sqlOutQueryCriteriaItem.GWDataDBField.FieldName;
            enumCmbbxOperator.Text = sqlOutQueryCriteriaItem.Operator.ToString();
            txtValue.Text = sqlOutQueryCriteriaItem.Translating.ConstValue;
            enumCmbbxJoin.Text = sqlOutQueryCriteriaItem.Type.ToString();
        }

        private void Save()
        {
            sqlOutQueryCriteriaItem.GWDataDBField.FieldName = cmbbxGWField.Text.Trim();
            sqlOutQueryCriteriaItem.GWDataDBField.Table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), enumCmbbxTable.Text);
            sqlOutQueryCriteriaItem.Operator = (QueryCriteriaOperator)Enum.Parse(typeof(QueryCriteriaOperator), enumCmbbxOperator.Text);
            sqlOutQueryCriteriaItem.Translating.Type = TranslatingType.FixValue;
            sqlOutQueryCriteriaItem.Translating.ConstValue = txtValue.Text;
            sqlOutQueryCriteriaItem.Type = (QueryCriteriaType)Enum.Parse(typeof(QueryCriteriaType), enumCmbbxJoin.Text);

            if (type == "Add")
            {
                channelForm.criteriaItemList.Add(sqlOutQueryCriteriaItem);
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (NullCheck())
            {
                case NullType.Table:
                    {
                        MessageBox.Show( "Table name should not be \"None\"!", "Check Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                case NullType.Field:
                    {
                        MessageBox.Show( "Please choose a field name!", "Check Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                case NullType.Value:
                    {
                        MessageBox.Show( "Please input a criteria value!", "Check Translation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                case NullType.Join:
                    {
                        MessageBox.Show( "Join type should not be \"None\"!", "Check Join", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
            }
            
            Save();
            channelForm.ShowCriteriaList();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private NullType NullCheck()
        {
            if (enumCmbbxTable.Text.Trim() == GWDataDBTable.None.ToString())
            {
                return NullType.Table;
            }
            else if (cmbbxGWField.Text.Trim() == "")
            {
                return NullType.Field;
            }
            //else if (txtValue.Text.Trim() == "")
            //{
            //    return NullType.Value;
            //}
            else if(enumCmbbxJoin.Text == QueryCriteriaType.None.ToString())
            {
                return NullType.Join;
            }
            else
            {
                return NullType.None;
            }
        }

        private enum NullType
        {
            None,
            Table,
            Field,
            Value,
            Join
        }
        #endregion
    }
}