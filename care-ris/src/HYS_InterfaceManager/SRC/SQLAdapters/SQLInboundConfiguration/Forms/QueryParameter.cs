using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using HYS.SQLInboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Translation;
using HYS.SQLInboundAdapterConfiguration.Controls;

namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    public partial class QueryParameter : Form
    {
        #region Local members
        XCollection<SQLInQueryResultItem> parameterList;
        string type = "";
        SQLInQueryResultItem parameterItem;

        List<GWDataDBField> fieldSet = new List<GWDataDBField>();
        #endregion

        #region Construct methods
        public QueryParameter(XCollection<SQLInQueryResultItem> parameters)
        {
            InitializeComponent();
            GetLUTTables();
            parameterList = parameters;
            parameterItem = new SQLInQueryResultItem();
            type = "Add";
            this.Text = "Add Parameter";

            GetGCFieldSet(parameters);
        }

        //Only for modify
        int parameterIndex;
        public QueryParameter(XCollection<SQLInQueryResultItem> parameters, int index)
        {
            InitializeComponent();
            GetLUTTables();
            parameterList = parameters;
            parameterItem = parameters[index];
            parameterIndex = index;
            type = "Edit";
            this.Text = "Edit Parameter";

            parameterIndex = index;

            GetGCFieldSet(parameters);
            ShowInformation();
        }
        #endregion

        #region Show and Save
        private void ShowInformation()
        {
            txtParameter.Text = parameterItem.TargetField;
            //enumCmbbxThirdPartyFieldType.Text = parameterItem.ThirdPartyDBPatamter.FieldType.ToString();
            enumCmbbxTable.Text = parameterItem.GWDataDBField.Table.ToString();
            cmbbxGWField.Text = parameterItem.GWDataDBField.FieldName;
            enumCmbbxTranslation.Text = parameterItem.Translating.Type.ToString();
            if (enumCmbbxTranslation.Text == TranslatingType.LookUpTable.ToString() || enumCmbbxTranslation.Text == TranslatingType.LookUpTableReverse.ToString())
            {
                //txtTranslationValue.Text = parameterItem.Translating.LutName;

                string lutName = parameterItem.Translating.LutName;
                foreach (string str in this.cmbbxResult.Items)
                {
                    if (str == lutName)
                    {
                        this.cmbbxResult.SelectedItem = str;
                        break;
                    }
                }
            }
            else if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString() || enumCmbbxTranslation.Text == TranslatingType.DefaultValue.ToString())
            {
                txtTranslationValue.Text = parameterItem.Translating.ConstValue;
            }
            else
            {
                txtTranslationValue.Text = "";
            }
            enumCmbbxRedundancy.Text = parameterItem.RedundancyFlag.ToString();
        }

        private void Save()
        {
            if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString())
            {
                txtParameter.Text = "";
            }

            parameterItem.TargetField = txtParameter.Text;
            parameterItem.TargetField = txtParameter.Text;
            parameterItem.SourceField = cmbbxGWField.Text;
            parameterItem.GWDataDBField.FieldName = cmbbxGWField.Text;
            parameterItem.GWDataDBField.Table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), enumCmbbxTable.Text);
            parameterItem.RedundancyFlag = Boolean.Parse(enumCmbbxRedundancy.Text);
            if (enumCmbbxTranslation.Text == TranslatingType.LookUpTable.ToString() || enumCmbbxTranslation.Text == TranslatingType.LookUpTableReverse.ToString())
            {
                string str = this.cmbbxResult.SelectedItem as string;
                parameterItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), enumCmbbxTranslation.Text);
                parameterItem.Translating.LutName = (str == null) ? "" : str;
                parameterItem.Translating.ConstValue = "";
            }
            else if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString() || enumCmbbxTranslation.Text == TranslatingType.DefaultValue.ToString())
            {
                parameterItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), enumCmbbxTranslation.Text);
                parameterItem.Translating.LutName = "";
                parameterItem.Translating.ConstValue = txtTranslationValue.Text;
            }
            else
            {
                parameterItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), enumCmbbxTranslation.Text);
                parameterItem.Translating.LutName = "";
                parameterItem.Translating.ConstValue = "";
            }

            if (type == "Add")
            {
                parameterList.Add(parameterItem);
            }
        }
        #endregion

        #region Controls events
        //bool exist = false;
        //private void txtThirdPartyFieldName_TextChanged(object sender, EventArgs e)
        //{
        //    if (type == "Add")
        //    {
        //        if (IsParaExist(txtThirdPartyFieldName.Text) && txtThirdPartyFieldName.Text != "")
        //        {
        //            lblExist.Visible = true;
        //            exist = true;
        //        }
        //        else
        //        {
        //            lblExist.Visible = false;
        //            exist = false;
        //        }
        //    }
        //    else
        //    {
        //        if (IsParaExist(txtThirdPartyFieldName.Text) && (txtThirdPartyFieldName.Text != parameterList[parameterIndex].SourceField) && txtThirdPartyFieldName.Text != "")
        //        {
        //            lblExist.Visible = true;
        //            exist = true;
        //        }
        //        else
        //        {
        //            lblExist.Visible = false;
        //            exist = false;
        //        }
        //    }
        //}

        private void enumCmbbxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbbxGWField.Items.Clear();
            if (enumCmbbxTable.Text == GWDataDBTable.Index.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWDataIndexField);
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Patient.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWPatientField);
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Order.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWDataOrderField);
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Report.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWDataReportField);
            }
        }

        private void enumCmbbxTranslation_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtParameter.Enabled = true;
            lblParameterStar.Visible = true;
            lblLUTStar.Visible = false;
            cmbbxResult.Visible = false;

            if (enumCmbbxTranslation.Text == TranslatingType.None.ToString())
            {
                txtTranslationValue.Enabled = false;
                txtTranslationValue.Text = "";
            }
            else if (enumCmbbxTranslation.Text == TranslatingType.LookUpTable.ToString() || enumCmbbxTranslation.Text == TranslatingType.LookUpTableReverse.ToString())
            {
                txtTranslationValue.Enabled = true;
                lblLUTStar.Visible = true;
                cmbbxResult.Visible = true;
            }
            else
            {
                if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString())
                {
                    //txtParameter.Text = "";
                    txtParameter.Enabled = false;
                    lblParameterStar.Visible = false;
                }

                txtTranslationValue.Enabled = true;
                lblLUTStar.Visible = true;
            }
        }

        private void GetLUTTables()
        {
            string dbcnn = Program.GWDataDBConnection;
            if (dbcnn != null && dbcnn.Length > 0)
            {
                LutMgt mgt = new LutMgt(new DataBase(dbcnn));
                string[] lutList = mgt.GetLutNames();
                if (lutList != null)
                {
                    foreach (string lut in lutList)
                    {
                        cmbbxResult.Items.Add(lut);
                    }
                }
            }
        }
        #endregion

        #region Check the valid of Parameter and the identity of GC Gateway field
        private bool IsParaValid(string paraName) { 
            if(!CheckItemValid.IsValid(paraName)){
                MessageBox.Show( "The parameter name should only contain charactor, number or '_', and should begins with charactor, please input another name.", "Parameter Validate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }else{
                return true;
            }
        }

        private bool IsGCFieldExist(string paraName)
        {
            foreach( GWDataDBField field in fieldSet ){
                if (type == "Add")
                {
                    if (paraName.Equals(field.FieldName) && field.Table.ToString().Equals(enumCmbbxTable.Text))
                    {
                        return true;
                    }
                }
                else {
                    if (paraName.Equals(field.FieldName) && paraName != parameterList[parameterIndex].GWDataDBField.FieldName && field.Table.ToString().Equals(enumCmbbxTable.Text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void GetGCFieldSet(XCollection<SQLInQueryResultItem> parameterList)
        {
            foreach (SQLInQueryResultItem parameter in parameterList)
            {
                fieldSet.Add(parameter.GWDataDBField);
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (enumCmbbxTranslation.Text != TranslatingType.FixValue.ToString())
            {
                if (!IsParaValid(txtParameter.Text))
                {
                    return;
                }
            }
            if (NullCheck() == NullType.ThirdField && enumCmbbxTranslation.Text != TranslatingType.FixValue.ToString())
            {
                MessageBox.Show( "Please input a value of parameter!", "Check Parameter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (NullCheck() == NullType.Table)
            {
                MessageBox.Show( "Table name should not be \"None\"!", "Check Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (NullCheck() == NullType.Field)
            {
                MessageBox.Show( "Please choose a field name!", "Check Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (IsGCFieldExist(cmbbxGWField.Text)) {
                MessageBox.Show( "GC Gateway field existed!", "Check Gateway Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (NullCheck() == NullType.Translation)
            {
                MessageBox.Show( "Please input a value for translation!", "Check Translation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private NullType NullCheck()
        {
            if (txtParameter.Text.Trim() == "")
            {
                return NullType.ThirdField;
            }
            else if (enumCmbbxTable.Text.Trim() == GWDataDBTable.None.ToString())
            {
                return NullType.Table;
            }
            else if (cmbbxGWField.Text.Trim() == "")
            {
                return NullType.Field;
            }
            else if ((enumCmbbxTranslation.Text.Trim() == TranslatingType.LookUpTable.ToString() ||
enumCmbbxTranslation.Text.Trim() == TranslatingType.LookUpTableReverse.ToString())
&& cmbbxResult.Text.Trim() == "")
            {
                return NullType.Translation;
            }
            else {
                return NullType.None;
            }
        }
 
        private enum NullType
        {
            None,
            ThirdField,
            Table,
            Field,
            Translation
        }
        #endregion
    }
}