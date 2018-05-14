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
    public partial class QueryInParameter : Form
    {
        #region Local members
        XCollection<SQLOutQueryCriteriaItem> parameterList;
        string type = "";
        SQLOutQueryCriteriaItem parameterItem;
        #endregion

        #region Construct methods
        public QueryInParameter(XCollection<SQLOutQueryCriteriaItem> parameters)
        {
            InitializeComponent();
            GetLUTTables();
            parameterList = parameters;
            parameterItem = new SQLOutQueryCriteriaItem();
            type = "Add";
            this.Text = "Add Parameter";
        }

        public QueryInParameter(XCollection<SQLOutQueryCriteriaItem> parameters, int index)
        {
            InitializeComponent();
            GetLUTTables();
            parameterList = parameters;
            parameterItem = parameters[index];
            type = "Edit";
            this.Text = "Edit Parameter";

            ShowInformation();
        }
        #endregion

        #region Show and Save
        private void ShowInformation()
        {
            txtParameter.Text = parameterItem.SourceField;
            enumCmbbxTable.Text = parameterItem.GWDataDBField.Table.ToString();
            cmbbxGWField.Text = parameterItem.GWDataDBField.FieldName;
            enumCmbbxOperator.Text = parameterItem.Operator.ToString();

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
            enumCmbbxJoin.Text = parameterItem.Type.ToString();
        }

        private void Save()
        {
            if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString())
            {
                txtParameter.Text = "";
            }

            parameterItem.SourceField = txtParameter.Text;
            parameterItem.GWDataDBField.FieldName = cmbbxGWField.Text;
            parameterItem.GWDataDBField.Table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), enumCmbbxTable.Text);
            parameterItem.Operator = (QueryCriteriaOperator)Enum.Parse(typeof(QueryCriteriaOperator), enumCmbbxOperator.Text);

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

            parameterItem.Type = (QueryCriteriaType)Enum.Parse(typeof(QueryCriteriaType), enumCmbbxJoin.Text);

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
            txtParameter.Enabled = true;
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
                    //txtParameter.Text = "";
                    txtParameter.Enabled = false;
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

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (NullCheck())
            {
                case NullType.Parameter:
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
                case NullType.Join:
                    {
                        MessageBox.Show( "Join type should not be \"None\"!", "Check Join", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (txtParameter.Text.Trim() == "" && enumCmbbxTranslation.Text != TranslatingType.FixValue.ToString())
            {
                return NullType.Parameter;
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
            else if(enumCmbbxJoin.Text == QueryCriteriaType.None.ToString())
            {
                return NullType.Join;
            }
            {
                return NullType.None;
            }
        }

        private enum NullType
        {
            None,
            Parameter,
            Table,
            Field,
            Translation,
            Join
        }
        #endregion
    }
}