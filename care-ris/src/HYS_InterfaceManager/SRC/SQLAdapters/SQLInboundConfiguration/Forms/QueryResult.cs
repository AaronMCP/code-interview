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


namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    public partial class QueryResult : Form
    {
        #region Local members
        Channel channelForm;
        string type = "";
        SQLInQueryResultItem resultItem;
        XCollection<GWDataDBField> resultList = new XCollection<GWDataDBField>();
        #endregion

        #region Construct methods
        public QueryResult(Channel channel)
        {
            InitializeComponent();
            GetLUTTables();
            ComboxLoader.LoadOleDBType(this.enumCmbbxThirdPartyFieldType);

            channelForm = channel;
            type = "Add";
            resultItem = new SQLInQueryResultItem();
            this.Text = "Add Mapping Item";

            GetGCFieldSet(channel.resultItemList);
        }

        int resultIndex;
        public QueryResult(Channel channel, int index)
        {
            InitializeComponent();
            GetLUTTables();
            ComboxLoader.LoadOleDBType(this.enumCmbbxThirdPartyFieldType);

            channelForm = channel;
            type = "Edit";
            this.Text = "Edit Mapping Item";
            resultItem = channel.resultItemList[index];

            resultIndex = index;

            GetGCFieldSet(channel.resultItemList);
            ShowInformation();
        }
        #endregion

        #region Show and Save
        private void Save()
        {
            if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString())
            {
                txtThirdPartyFieldName.Text = "";
            }

            resultItem.ThirdPartyDBPatamter.FieldName = txtThirdPartyFieldName.Text.Trim();
            resultItem.ThirdPartyDBPatamter.FieldType = (OleDbType)Enum.Parse(typeof(OleDbType), enumCmbbxThirdPartyFieldType.Text);
            resultItem.SourceField = txtThirdPartyFieldName.Text.Trim(); ;
            resultItem.TargetField = cmbbxGWField.Text.Trim();
            resultItem.GWDataDBField.FieldName = cmbbxGWField.Text.Trim();
            resultItem.GWDataDBField.Table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), enumCmbbxTable.Text);
            resultItem.RedundancyFlag = Boolean.Parse(enumCmbbxRedundancy.Text);
            if (enumCmbbxTranslation.Text == TranslatingType.LookUpTable.ToString() || enumCmbbxTranslation.Text == TranslatingType.LookUpTableReverse.ToString())
            {
                string str = this.cmbbxResult.SelectedItem as string;
                resultItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), enumCmbbxTranslation.Text);
                resultItem.Translating.LutName = (str == null) ? "" : str;
                resultItem.Translating.ConstValue = "";
            }
            else if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString() || enumCmbbxTranslation.Text == TranslatingType.DefaultValue.ToString())
            {
                resultItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), enumCmbbxTranslation.Text);
                resultItem.Translating.LutName = "";
                resultItem.Translating.ConstValue = txtTranslationValue.Text;
            }
            else
            {
                resultItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), enumCmbbxTranslation.Text);
                resultItem.Translating.LutName = "";
                resultItem.Translating.ConstValue = "";
            }

            if (type == "Add")
            {
                channelForm.resultItemList.Add(resultItem);
            }
        }
        
        private void ShowInformation()
        {
            txtThirdPartyFieldName.Text = resultItem.ThirdPartyDBPatamter.FieldName;
            enumCmbbxThirdPartyFieldType.Text = resultItem.ThirdPartyDBPatamter.FieldType.ToString();
            enumCmbbxTable.Text = resultItem.GWDataDBField.Table.ToString();
            cmbbxGWField.Text = resultItem.GWDataDBField.FieldName;
            enumCmbbxTranslation.Text = resultItem.Translating.Type.ToString();
            if (enumCmbbxTranslation.Text == TranslatingType.LookUpTable.ToString() || enumCmbbxTranslation.Text == TranslatingType.LookUpTableReverse.ToString())
            {
                //txtTranslationValue.Text = resultItem.Translating.LutName;

                string lutName = resultItem.Translating.LutName;
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
                txtTranslationValue.Text = resultItem.Translating.ConstValue;
            }
            else
            {
                txtTranslationValue.Text = "";
            }
            enumCmbbxRedundancy.Text = resultItem.RedundancyFlag.ToString();
        }
        #endregion

        #region Controls events
        private void enumCmbbxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbbxGWField.Items.Clear();
            cmbbxGWField.Text = "";
            List<GWDataDBField> fields = new List<GWDataDBField>();
            foreach (GWDataDBField item in resultList )
            {
                // ------------- 20070418 -------------
                if (resultItem.GWDataDBField != null &&
                    resultItem.GWDataDBField.Table == item.Table &&
                    resultItem.GWDataDBField.FieldName == item.FieldName)
                {
                    continue;
                }
                // ------------------------------------

                fields.Add(item);
            }

            if (enumCmbbxTable.Text == GWDataDBTable.Index.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GetGWDataField_Left(GWDataDBTable.Index, fields.ToArray()));
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Patient.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GetGWDataField_Left(GWDataDBTable.Patient, fields.ToArray()));
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Order.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GetGWDataField_Left(GWDataDBTable.Order, fields.ToArray()));
            }
            if (enumCmbbxTable.Text == GWDataDBTable.Report.ToString())
            {
                cmbbxGWField.Items.AddRange(GWFields.GetGWDataField_Left(GWDataDBTable.Report, fields.ToArray()));
            }

            // ------------- 20070418 -------------
            //if (!(resultItem.GWDataDBField == GWDataDBField.Null || resultItem.GWDataDBField.FieldName == null))
            //    cmbbxGWField.Items.Add(resultItem.GWDataDBField.FieldName);
            // ------------------------------------


            //cmbbxGWField.Items.Clear();
            //if (enumCmbbxTable.Text == GWDataDBTable.Index.ToString())
            //{
            //    cmbbxGWField.Items.AddRange(GWFields.GWDataIndexField);
            //}
            //if (enumCmbbxTable.Text == GWDataDBTable.Patient.ToString())
            //{
            //    cmbbxGWField.Items.AddRange(GWFields.GWPatientField);
            //}
            //if (enumCmbbxTable.Text == GWDataDBTable.Order.ToString())
            //{
            //    cmbbxGWField.Items.AddRange(GWFields.GWDataOrderField);
            //}
            //if (enumCmbbxTable.Text == GWDataDBTable.Report.ToString())
            //{
            //    cmbbxGWField.Items.AddRange(GWFields.GWDataReportField);
            //}
        }

        private void enumCmbbxTranslation_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtThirdPartyFieldName.Enabled = true;
            enumCmbbxThirdPartyFieldType.Enabled = true;
            lblFieldStar.Visible = true;
            lblTypeStar.Visible = true;
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
                if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString()) {
                    //txtThirdPartyFieldName.Text = "";
                    txtThirdPartyFieldName.Enabled = false;
                    enumCmbbxThirdPartyFieldType.Enabled = false;
                    lblFieldStar.Visible = false;
                    lblTypeStar.Visible = false;
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
        private bool IsParaValid(string paraName)
        {
            if (!CheckItemValid.IsValid(paraName))
            {
                MessageBox.Show( "The field name should only contain charactor, number or '_', and should begins with charactor, please input another name.", "Field Validate!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsGCFieldExist(string paraName)
        {
            foreach (GWDataDBField field in resultList)
            {
                if (type == "Add")
                {
                    if (paraName.Equals(field.FieldName) && field.Table.ToString().Equals(enumCmbbxTable.Text))
                    {
                        return true;
                    }
                }
                else {
                    if (paraName.Equals(field.FieldName) && (paraName != channelForm.resultItemList[resultIndex].GWDataDBField.FieldName) && field.Table.ToString().Equals(enumCmbbxTable.Text))
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
                resultList.Add(parameter.GWDataDBField);
            }
        }
        #endregion

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (enumCmbbxTranslation.Text != TranslatingType.FixValue.ToString())
            {
                if (!IsParaValid(txtThirdPartyFieldName.Text.Trim()))
                {
                    return;
                }
            }
            if (NullCheck() == NullType.ThirdField && enumCmbbxTranslation.Text != TranslatingType.FixValue.ToString())
            {
                MessageBox.Show( "Please input a value of source field name!", "Check Source Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (NullCheck() == NullType.Table)
            {
                MessageBox.Show( "Table name should not be \"None\"!", "Check Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (NullCheck() == NullType.Field)
            {
                MessageBox.Show( "Please choose a field name!", "Check Target Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            channelForm.ShowResultList();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private NullType NullCheck()
        {
            if (txtThirdPartyFieldName.Text.Trim() == "")
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