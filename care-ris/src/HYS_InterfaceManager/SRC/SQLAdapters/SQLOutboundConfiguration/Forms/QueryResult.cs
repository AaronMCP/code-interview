using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Translation;

namespace HYS.SQLOutboundAdapterConfiguration.Forms
{
    public partial class QueryResult : Form
    {
        #region Local members
        Channel channelForm;
        string type = "";
        SQLOutQueryResultItem sqlOutQueryResultItem;
        List<string> parameterSet = new List<string>();
        #endregion

        #region Construct methods
        public QueryResult(Channel channel)
        {
            InitializeComponent();
            GetLUTTables();
            ComboxLoader.LoadOleDBType(this.enumCmbbxFieldType);

            channelForm = channel;
            type = "Add";
            this.Text = "Add Mapping Item";
            sqlOutQueryResultItem = new SQLOutQueryResultItem();

            GetThirdParaSet(channelForm.resultItemList);
            SetThirdPartyNameType();

            this.groupBoxRD.Enabled = (channelForm.QueryMode == ThrPartyDBOperationType.Table.ToString() &&
                channelForm.parentForm.DBconfig.ConnectionParameter.FileConnection == false);
        }

        int resultIndex;
        public QueryResult(Channel channel, int index)
        {
            InitializeComponent();
            GetLUTTables();
            ComboxLoader.LoadOleDBType(this.enumCmbbxFieldType);

            channelForm = channel;
            type = "Edit";
            this.Text = "Edit Mapping Item";
            sqlOutQueryResultItem = channel.resultItemList[index];
            resultIndex = index;

            SetThirdPartyNameType();
            GetThirdParaSet(channelForm.resultItemList);
            ShowInformation();

            this.groupBoxRD.Enabled = (channelForm.QueryMode == ThrPartyDBOperationType.Table.ToString() &&
                channelForm.parentForm.DBconfig.ConnectionParameter.FileConnection == false);
        }
        #endregion

        #region Show and Save
        private void Save()
        {
            if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString())
            {
                enumCmbbxTable.Text = GWDataDBTable.None.ToString();
                cmbbxGWField.Text = "";
            }

            sqlOutQueryResultItem.ThirdPartyDBPatamter.FieldName = txtParameter.Text.Trim();
            sqlOutQueryResultItem.ThirdPartyDBPatamter.FieldType = (OleDbType)Enum.Parse(typeof(OleDbType), enumCmbbxFieldType.Text);
            sqlOutQueryResultItem.TargetField = txtParameter.Text.Trim(); ;
            sqlOutQueryResultItem.SourceField = cmbbxGWField.Text.Trim();
            sqlOutQueryResultItem.GWDataDBField.FieldName = cmbbxGWField.Text.Trim();
            sqlOutQueryResultItem.GWDataDBField.Table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), enumCmbbxTable.Text);
            sqlOutQueryResultItem.RedundancyFlag = Boolean.Parse(enumCmbbxRedundancy.Text);
            if (enumCmbbxTranslation.Text == TranslatingType.LookUpTable.ToString() || enumCmbbxTranslation.Text == TranslatingType.LookUpTableReverse.ToString())
            {
                string str = this.cmbbxResult.SelectedItem as string;
                sqlOutQueryResultItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), enumCmbbxTranslation.Text);
                sqlOutQueryResultItem.Translating.LutName = (str == null) ? "" : str;
                sqlOutQueryResultItem.Translating.ConstValue = "";
            }
            else if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString() || enumCmbbxTranslation.Text == TranslatingType.DefaultValue.ToString())
            {
                sqlOutQueryResultItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), enumCmbbxTranslation.Text);
                sqlOutQueryResultItem.Translating.LutName = "";
                sqlOutQueryResultItem.Translating.ConstValue = txtTranslationValue.Text;
            }
            else
            {
                sqlOutQueryResultItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), enumCmbbxTranslation.Text);
                sqlOutQueryResultItem.Translating.LutName = "";
                sqlOutQueryResultItem.Translating.ConstValue = "";
            }

            if (type == "Add")
            {
                channelForm.resultItemList.Add(sqlOutQueryResultItem);
            }
        }

        private void ShowInformation()
        {
            txtParameter.Text = sqlOutQueryResultItem.ThirdPartyDBPatamter.FieldName;
            enumCmbbxFieldType.Text = sqlOutQueryResultItem.ThirdPartyDBPatamter.FieldType.ToString();
            enumCmbbxTable.Text = sqlOutQueryResultItem.GWDataDBField.Table.ToString();
            cmbbxGWField.Text = sqlOutQueryResultItem.GWDataDBField.FieldName;
            enumCmbbxRedundancy.Text = sqlOutQueryResultItem.RedundancyFlag.ToString();
            enumCmbbxTranslation.Text = sqlOutQueryResultItem.Translating.Type.ToString();
            if (enumCmbbxTranslation.Text == TranslatingType.LookUpTable.ToString() || enumCmbbxTranslation.Text == TranslatingType.LookUpTableReverse.ToString())
            {
                //txtTranslationValue.Text = sqlOutQueryResultItem.Translating.LutName;

                string lutName = sqlOutQueryResultItem.Translating.LutName;
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
                txtTranslationValue.Text = sqlOutQueryResultItem.Translating.ConstValue;
            }
            else
            {
                txtTranslationValue.Text = "";
            }
        }
        #endregion

        #region Controls events
        private void RefreshControls() 
        {
            enumCmbbxTable.Enabled = true;
            cmbbxGWField.Enabled = true;
            lblTableStar.Visible = true;
            lblFieldStar.Visible = true;
            txtTranslationValue.Enabled = true;
            lblLUTStar.Visible = false;
        }

        private void enumCmbbxTranslation_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbbxResult.Visible = false;
            RefreshControls();

            if (enumCmbbxTranslation.Text == "None")
            {
                txtTranslationValue.Enabled = false;
                txtTranslationValue.Text = "";
            }
            else if (enumCmbbxTranslation.Text == "LookUpTable" || enumCmbbxTranslation.Text == "LookUpTableReverse")
            {
                lblLUTStar.Visible = true;
                cmbbxResult.Visible = true;
            }
            else
            {
                if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString())
                {
                    //enumCmbbxTable.Text = GWDataDBTable.None.ToString();
                    enumCmbbxTable.Enabled = false;
                    cmbbxGWField.Enabled = false;
                    lblTableStar.Visible = false;
                    lblFieldStar.Visible = false;
                }
                lblLUTStar.Visible = true;
            }
        }
        
        private void enumCmbbxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbbxGWField.Items.Clear();
            if (enumCmbbxTable.Text == GWDataDBTable.Index.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWDataIndexField);
                cmbbxGWField.Text = "";
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Patient.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWPatientField);
                cmbbxGWField.Text = "";
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Order.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWDataOrderField);
                cmbbxGWField.Text = "";
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Report.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GWDataReportField);
                cmbbxGWField.Text = "";
            }
        }

        private void SetThirdPartyNameType() {
            if (channelForm.QueryMode == ThrPartyDBOperationType.StorageProcedure.ToString())
            {
                this.groupBox1.Text = "Parameter";
                this.lblFieldName.Text = "Parameter Name";
                this.lblFieldType.Text = "Parameter Type";
            }
            else {
                this.groupBox1.Text = "Third Party Field";
                this.lblFieldName.Text = "Field Name";
                this.lblFieldType.Text = "Field Type";
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
                if (channelForm.QueryMode == ThrPartyDBOperationType.StorageProcedure.ToString())
                {
                    MessageBox.Show( "The parameter name should only contain charactor, number or '_', and should begins with charactor, please input another name.", "Third Party Parameter Validate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else {
                    MessageBox.Show( "The field name should only contain charactor, number or '_', and should begins with charactor, please input another name.", "Third Party Field Validate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                    if (CheckItemValid.IsContain(paraName, parameterSet, StringComparison.OrdinalIgnoreCase) && paraName != channelForm.resultItemList[resultIndex].ThirdPartyDBPatamter.FieldName)
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
            if (!IsParaValid(txtParameter.Text.Trim()))
            {
                return;
            }

            switch (NullCheck())
            {
                case NullType.ThirdField:
                    {
                        if (channelForm.QueryMode == ThrPartyDBOperationType.StorageProcedure.ToString())
                        {
                            MessageBox.Show( "Please input a name of parameter!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else {
                            MessageBox.Show( "Please input a name of Field!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                            return;
                    }
                case NullType.Table:
                    {
                        MessageBox.Show("Table name should not be \"None\"!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                case NullType.Field:
                    {
                        MessageBox.Show( "Please choose a field name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                case NullType.Translation:
                    {
                        MessageBox.Show( "Please input a value for translation!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
            }

            if (IsParaExist(txtParameter.Text.Trim()))
            {
                if (channelForm.QueryMode == ThrPartyDBOperationType.StorageProcedure.ToString())
                {
                    MessageBox.Show( "Parameter is existing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show( "Field is existing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //MessageBox.Show(this, "Parameter is existing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            Save();
            channelForm.ShowResultList();
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

    public class ComboxLoader
    {
        public static void LoadOleDBType(ComboBox cb)
        {
            if (cb == null) return;

            string[] list = Enum.GetNames(typeof(OleDbType));

            int index = 0;
            cb.Items.Clear();
            for (int i = 0; i < list.Length; i++)
            {
                string str = list[i];
                if (str.ToLower() == "varchar") index = i;
                cb.Items.Add(str);
            }

            if (index >= 0 && index < cb.Items.Count)
                cb.SelectedIndex = index;
        }
    }
}