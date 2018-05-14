using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.SQLInboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using System.Data.OleDb;

namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    public partial class QueryCriteria : Form
    {
        #region Local members
        Channel channelForm;
        string type = "";
        SQLInQueryCriteriaItem criteriaItem;
        #endregion

        #region Constructor
        public QueryCriteria(Channel channel)
        {
            InitializeComponent();
            ComboxLoader.LoadOleDBType(this.enumCmbbxThirdPartyFieldType);
            
            channelForm = channel;
            type = "Add";
            this.Text = "Add Parameter";
            criteriaItem = new SQLInQueryCriteriaItem();
        }

        public QueryCriteria(Channel channel, int index)
        {
            InitializeComponent();
            ComboxLoader.LoadOleDBType(this.enumCmbbxThirdPartyFieldType);

            channelForm = channel;
            type = "Edit";
            this.Text = "Edit Parameter";
            criteriaItem = channel.criteriaItemList[index];

            ShowInformation();
        }
        #endregion

        #region Show and Save
        private void Save()
        {           
            criteriaItem.ThirdPartyDBPatamter.FieldName = txtThirdpartyFieldName.Text.Trim();
            criteriaItem.ThirdPartyDBPatamter.FieldType = (OleDbType)Enum.Parse(typeof(OleDbType), enumCmbbxThirdPartyFieldType.Text);
            criteriaItem.Translating.Type = TranslatingType.FixValue;
            criteriaItem.Translating.ConstValue = txtValue.Text;

            criteriaItem.IsNull = this.checkBoxNull.Checked;
            criteriaItem.IsGetFromStorageProcedure = this.checkBoxGetFromSP.Checked;

            if (type == "Add")
            {
                channelForm.criteriaItemList.Add(criteriaItem);
            }
        }
        
        private void ShowInformation() {
            txtThirdpartyFieldName.Text = criteriaItem.ThirdPartyDBPatamter.FieldName;
            enumCmbbxThirdPartyFieldType.Text = criteriaItem.ThirdPartyDBPatamter.FieldType.ToString();
            txtValue.Text = criteriaItem.Translating.ConstValue;
            this.checkBoxNull.Checked = criteriaItem.IsNull;
            this.checkBoxGetFromSP.Checked = criteriaItem.IsGetFromStorageProcedure;
        }
        #endregion

        #region Check the valid of Parameter
        private bool IsParaValid(string paraName)
        {
            if (!CheckItemValid.IsValid(paraName))
            {
                MessageBox.Show( "The parameter name should only contain charactor, number or '_', and should begins with charactor, please input another name.", "Parameter Validate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsParaValid(txtThirdpartyFieldName.Text.Trim()))
            {
                return;
            }

            if (NullCheck() == NullType.Name) {
                MessageBox.Show( "Please input a value of field name!", "Check Field Name",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if (NullCheck() == NullType.Value)
            {
                MessageBox.Show( "Please input a value of critical value!", "Check value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Save();
            channelForm.ShowCriteriaList();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private NullType NullCheck() {
            if (txtThirdpartyFieldName.Text.Trim() == "")
            {
                return NullType.Name;
            }
            //else if (txtValue.Text.Trim() == "")
            //{
            //    return NullType.Value;
            //}
            else 
            {
                return NullType.None;
            }
        }

        private enum NullType
        {
            None,
            Name,
            Value
        }
        #endregion

        private void checkBoxNull_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxNull.Checked)
            {
                this.checkBoxGetFromSP.Checked = false;
                this.txtValue.Text = "";
            }

            this.txtValue.Enabled = !(this.checkBoxNull.Checked || this.checkBoxGetFromSP.Checked);
        }

        private void checkBoxGetFromSP_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxGetFromSP.Checked)
            {
                this.checkBoxNull.Checked = false;
                //this.txtValue.Text = "";          // when cannot get value from storage procedure, use const value as default value
            }

            this.txtValue.Enabled = !(this.checkBoxNull.Checked || this.checkBoxGetFromSP.Checked);
        }
    }
}