using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Translation;
using HYS.SQLOutboundAdapterConfiguration.Controls;

namespace HYS.SQLOutboundAdapterConfiguration.Forms
{
    public partial class QueryOutParameter : Form
    {
        #region Local members
        XCollection<SQLOutQueryResultItem> parameterList;
        string type = "";
        SQLOutQueryResultItem parameterItem;
        List<string> parameterSet = new List<string>();
        #endregion

        #region Construct methods
        public QueryOutParameter(XCollection<SQLOutQueryResultItem> parameters)
        {
            InitializeComponent();
            GetLUTTables();
            parameterList = parameters;
            parameterItem = new SQLOutQueryResultItem();
            type = "Add";
            this.Text = "Add Mapping Item";

            GetThirdParaSet(parameters);
        }

        int parameterIndex;
        public QueryOutParameter(XCollection<SQLOutQueryResultItem> parameters, int index)
        {
            InitializeComponent();
            GetLUTTables();
            parameterList = parameters;
            parameterItem = parameters[index];
            type = "Edit";
            this.Text = "Edit Mapping Item";
            parameterIndex = index;

            GetThirdParaSet(parameters);
            ShowInformation();
        }
        #endregion

        #region Show and Save
        private void ShowInformation()
        {
            txtParameter.Text = parameterItem.ThirdPartyDBPatamter.FieldName;
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
        }

        private void Save()
        {
            if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString())
            {
                enumCmbbxTable.Text = GWDataDBTable.None.ToString();
                cmbbxGWField.Text = "";
            }

            parameterItem.ThirdPartyDBPatamter.FieldName = txtParameter.Text;
            parameterItem.GWDataDBField.FieldName = cmbbxGWField.Text;
            parameterItem.GWDataDBField.Table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), enumCmbbxTable.Text);
            parameterItem.SourceField = cmbbxGWField.Text;
            parameterItem.TargetField = txtParameter.Text;
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
            enumCmbbxTable.Enabled = true;
            cmbbxGWField.Enabled = true;
            lblTableStar.Visible = true;
            lblFieldStar.Visible = true;
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
                    //enumCmbbxTable.Text =GWDataDBTable.None.ToString();
                    //cmbbxGWField.Text = "";
                    enumCmbbxTable.Enabled = false;
                    cmbbxGWField.Enabled = false;
                    lblTableStar.Visible = false;
                    lblFieldStar.Visible = false;
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

        #region Check the identity and valid of Parameter
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

        private bool IsParaExist(string paraName)
        {
            if (type == "Add")
            {
                if (CheckItemValid.IsContain(paraName, parameterSet, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            else
            {
                if (CheckItemValid.IsContain(paraName, parameterSet, StringComparison.OrdinalIgnoreCase) && paraName != parameterList[parameterIndex].GWDataDBField.FieldName)
                {
                    return true;
                }
            }
            return false;
        }

        private void GetThirdParaSet(XCollection<SQLOutQueryResultItem> parameterList)
        {
            foreach (SQLOutQueryResultItem parameter in parameterList)
            {
                parameterSet.Add(parameter.ThirdPartyDBPatamter.FieldName);
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsParaValid(txtParameter.Text))
            {
                return;
            }

            if (IsParaExist(txtParameter.Text))
            {
                MessageBox.Show( "Parameter Existed!", "Check Parameter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                return;
            }

            switch (NullCheck())
            {
                case NullType.ThirdField:
                    {
                        MessageBox.Show( "Please input a name of parameter!", "Check Parameter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
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
                case NullType.Translation:
                    {
                        MessageBox.Show( "Please input a value for translation!", "Check Translation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
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
            else if (enumCmbbxTable.Text.Trim() == GWDataDBTable.None.ToString() && enumCmbbxTranslation.Text != TranslatingType.FixValue.ToString())
            {
                return NullType.Table;
            }
            else if (cmbbxGWField.Text.Trim() == "" && enumCmbbxTranslation.Text != TranslatingType.FixValue.ToString())
            {
                return NullType.Field;
            }
            else if ((enumCmbbxTranslation.Text.Trim() == TranslatingType.LookUpTable.ToString() ||
enumCmbbxTranslation.Text.Trim() == TranslatingType.LookUpTableReverse.ToString())
&& cmbbxResult.Text.Trim() == "")
            {
                return NullType.Translation;
            }
            else
            {
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