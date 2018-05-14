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
using HYS.SocketAdapter.Configuration;
using HYS.Common.Xml;

namespace HYS.SocketAdapter.SocketInboundAdapterConfiguration.Forms
{
    public partial class FQueryCriteria : Form
    {
        #region Local members
        FChannel parentlForm;
        XCollection<SocketInQueryCriteriaItem> criteriaList;
        SocketInQueryCriteriaItem criteriaItem;
        string type;

        #endregion

        #region Constructor
        public FQueryCriteria(FChannel frm,XCollection<SocketInQueryCriteriaItem> criteriaItems)
        {
            InitializeComponent();
            parentlForm = frm;
            criteriaList = criteriaItems;
            criteriaItem = new SocketInQueryCriteriaItem();
            type = "Add";
        }

        public FQueryCriteria(FChannel frm,XCollection<SocketInQueryCriteriaItem> criteriaItems,int index)
        {
            InitializeComponent();
            parentlForm = frm;
            criteriaList = criteriaItems;
            criteriaItem = criteriaItems[index];
            type = "Edit";

            ShowCriteriaItem();
        }
        #endregion

        #region Show and Save
        private void ShowCriteriaItem()
        {
            this.txtThirdpartyFieldName.Text = criteriaItem.ThirdPartyDBPatamter.FieldName;
            this.enumCmbbxThirdPartyFieldType.Text = criteriaItem.ThirdPartyDBPatamter.FieldType.ToString();
            this.enumCmbbxOperator.Text = criteriaItem.Operator.ToString();
            this.txtValue.Text = criteriaItem.Translating.ConstValue;
            this.enumCmbbxJoin.Text = criteriaItem.Type.ToString();
        }

        private void Save()
        {
            criteriaItem.ThirdPartyDBPatamter.FieldName = this.txtThirdpartyFieldName.Text.Trim();
            criteriaItem.ThirdPartyDBPatamter.FieldType = (OleDbType)Enum.Parse(typeof(OleDbType),enumCmbbxThirdPartyFieldType.Text);
            criteriaItem.Operator = (QueryCriteriaOperator)Enum.Parse(typeof(QueryCriteriaOperator), enumCmbbxOperator.Text);
            criteriaItem.Translating.ConstValue = this.txtValue.Text;
            criteriaItem.Type = (QueryCriteriaType)Enum.Parse(typeof(QueryCriteriaType),enumCmbbxJoin.Text);

            if(type == "Add"){
                criteriaList.Add(criteriaItem);
            }
        }
        #endregion

        #region Check the valid of Parameter
        private bool IsParaValid(string paraName)
        {
            if (!CheckItemValid.IsValid(paraName))
            {
                MessageBox.Show( "The field name should only contain character, number or '_', and should begin with character, please input another name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool NullExist()
        {
            if (txtThirdpartyFieldName.Text.Trim() == "")
            {
                MessageBox.Show( "Please input a value of field name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else if (txtValue.Text.Trim() == "")
            {
                MessageBox.Show( "Please input a value of criterial value!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsParaValid(txtThirdpartyFieldName.Text.Trim())) {
                return;
            }

            if (NullExist()) {
                return;
            }

            Save();
            parentlForm.ShowCriteriaList();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}