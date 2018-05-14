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
using HYS.FileAdapter.Configuration;
using HYS.Common.Xml;

namespace HYS.FileAdapter.FileInboundAdapterConfiguration.Forms
{
    public partial class FQueryCriteria : Form
    {
        #region Local members
        FChannel parentlForm;
        XCollection<FileInQueryCriteriaItem> criteriaList;
        FileInQueryCriteriaItem criteriaItem;
        string type;

        #endregion

        #region Constructor
        public FQueryCriteria(FChannel frm,XCollection<FileInQueryCriteriaItem> criteriaItems)
        {
            InitializeComponent();
            parentlForm = frm;
            criteriaList = criteriaItems;
            criteriaItem = new FileInQueryCriteriaItem();
            criteriaItem.ThirdPartyDBPatamter.FieldType = OleDbType.VarChar; // ini file, all field is varchar
            type = "Add";

            ShowCriteriaItem();
        }

        public FQueryCriteria(FChannel frm,XCollection<FileInQueryCriteriaItem> criteriaItems,int index)
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
            this.tbSectionName.Text = criteriaItem.ThirdPartyDBPatamter.SectionName;
            this.txtThirdpartyFieldName.Text = criteriaItem.ThirdPartyDBPatamter.FieldName;
            this.enumCmbbxThirdPartyFieldType.Text = criteriaItem.ThirdPartyDBPatamter.FieldType.ToString();
            this.enumCmbbxOperator.Text = criteriaItem.Operator.ToString();
            this.txtValue.Text = criteriaItem.Translating.ConstValue;
            this.enumCmbbxJoin.Text = criteriaItem.Type.ToString();
        }

        private void Save()
        {
            criteriaItem.ThirdPartyDBPatamter.SectionName = this.tbSectionName.Text.Trim();
            criteriaItem.ThirdPartyDBPatamter.FieldName = this.txtThirdpartyFieldName.Text.Trim();
            criteriaItem.ThirdPartyDBPatamter.FieldType = (OleDbType)Enum.Parse(typeof(OleDbType), enumCmbbxThirdPartyFieldType.Text.Trim());
            criteriaItem.Operator = (QueryCriteriaOperator)Enum.Parse(typeof(QueryCriteriaOperator), enumCmbbxOperator.Text.Trim());
            criteriaItem.Translating.ConstValue = this.txtValue.Text;
            criteriaItem.Type = (QueryCriteriaType)Enum.Parse(typeof(QueryCriteriaType), enumCmbbxJoin.Text.Trim());

            if(type == "Add"){
                criteriaList.Add(criteriaItem);
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
    }
}