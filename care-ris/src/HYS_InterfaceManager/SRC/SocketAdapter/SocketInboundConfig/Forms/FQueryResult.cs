using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using HYS.Adapter.Base;
using HYS.Common.Objects.Translation;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Rule;
using HYS.SocketAdapter.Configuration;
using HYS.Common.Xml;
using HYS.Common.DataAccess;

namespace HYS.SocketAdapter.SocketInboundAdapterConfiguration.Forms
{
    public partial class FQueryResult : Form
    {
        #region Local members
        FChannel parentlForm;
        XCollection<SocketInQueryResultItem> resultList;
        SocketInQueryResultItem resultItem;
        List<GWDataDBField> fieldSet = new List<GWDataDBField>();
        string type;

        #endregion

        #region Constructor
        public FQueryResult(FChannel frm,XCollection<SocketInQueryResultItem> resultItems)
        {
            InitializeComponent();
            parentlForm = frm;
            type = "Add";
            resultList = resultItems;
            resultItem = new SocketInQueryResultItem();
            GetLUTTables(SocketInboundAdapterConfigMgt.SocketInAdapterConfig.LookupTables);

            GetGCFieldSet(resultList);
        }

        int resultIndex;
        public FQueryResult(FChannel frm,XCollection<SocketInQueryResultItem> resultItems,int index)
        {
            InitializeComponent();
            parentlForm = frm;
            type = "Edit";
            resultList = resultItems;
            resultIndex = index;
            resultItem = resultItems[index];
            GetLUTTables(SocketInboundAdapterConfigMgt.SocketInAdapterConfig.LookupTables);

            GetGCFieldSet(resultList);
            ShowResultItem();
        }
        #endregion

        #region Show and Save
        private void ShowResultItem()
        {
            this.txtThirdPartyFieldName.Text = resultItem.ThirdPartyDBPatamter.FieldName;
            this.enumCmbbxThirdPartyFieldType.Text = resultItem.ThirdPartyDBPatamter.FieldType.ToString();
            this.enumCmbbxTable.Text = resultItem.GWDataDBField.Table.ToString();
            this.cmbbxGWField.Text = resultItem.GWDataDBField.FieldName;
            this.enumCmbbxTranslation.Text = resultItem.Translating.Type.ToString();
            if (enumCmbbxTranslation.Text == TranslatingType.LookUpTable.ToString() || enumCmbbxTranslation.Text == TranslatingType.LookUpTableReverse.ToString())
            {
                //this.cmbbxResult.Text = resultItem.Translating.LutName;

                string lutName = resultItem.Translating.LutName;
                foreach (LUTWrapper w in this.cmbbxResult.Items)
                {
                    if (w.TableName == lutName)
                    {
                        this.cmbbxResult.SelectedItem = w;
                        break;
                    }
                }
            }
            else if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString() || enumCmbbxTranslation.Text == TranslatingType.DefaultValue.ToString())
            {
                this.txtTranslationValue.Text = resultItem.Translating.ConstValue;
            }
            else {
                this.txtTranslationValue.Text = "";
            }
            enumCmbbxRedundancy.Text = resultItem.RedundancyFlag.ToString();           
        }

        private void Save()
        {
            if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString())
            {
                txtThirdPartyFieldName.Text = "";
            }

            resultItem.ThirdPartyDBPatamter.FieldName = this.txtThirdPartyFieldName.Text.Trim();
            resultItem.ThirdPartyDBPatamter.FieldType = (OleDbType)Enum.Parse(typeof(OleDbType), this.enumCmbbxThirdPartyFieldType.Text);
            resultItem.GWDataDBField.Table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), this.enumCmbbxTable.Text);
            resultItem.GWDataDBField.FieldName = this.cmbbxGWField.Text.Trim();
            resultItem.SourceField = this.txtThirdPartyFieldName.Text.Trim();
            resultItem.TargetField = this.cmbbxGWField.Text.Trim();
            resultItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType),this.enumCmbbxTranslation.Text);
            if (enumCmbbxTranslation.Text == TranslatingType.LookUpTable.ToString() || enumCmbbxTranslation.Text == TranslatingType.LookUpTableReverse.ToString())
            {
                LUTWrapper w = this.cmbbxResult.SelectedItem as LUTWrapper;
                if (w != null)
                {
                    resultItem.Translating.LutName = w.TableName;
                    resultItem.Translating.ConstValue = "";
                }
            }
            else if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString() || enumCmbbxTranslation.Text == TranslatingType.DefaultValue.ToString())
            {
                resultItem.Translating.LutName = "";
                resultItem.Translating.ConstValue = this.txtTranslationValue.Text;
            }
            else
            {
                resultItem.Translating.LutName = "";
                resultItem.Translating.ConstValue = "";
            }
            resultItem.RedundancyFlag = bool.Parse(enumCmbbxRedundancy.Text);           
            
            if (type == "Add")
            {
                resultList.Add(resultItem);
            }
        }
        #endregion

        #region Controls events
        private void enumCmbbxTable_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbbxGWField.Items.Clear();
            cmbbxGWField.Text = "";
            List<GWDataDBField> fields = new List<GWDataDBField>();
            foreach (SocketInQueryResultItem item in resultList)
            {
                // ------------- 20070418 -------------
                if (resultItem.GWDataDBField != null &&
                    resultItem.GWDataDBField.Table == item.GWDataDBField.Table &&
                    resultItem.GWDataDBField.FieldName == item.GWDataDBField.FieldName)
                {
                    continue;
                }
                // ------------------------------------

                fields.Add(item.GWDataDBField);
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
            // ------------- 20070418 -------------

            //cmbbxGWField.Items.Clear();
            //cmbbxGWField.Text = "";
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
            txtTranslationValue.Visible = true;
            cmbbxResult.Visible = false;
            txtThirdPartyFieldName.Enabled = true;
            enumCmbbxThirdPartyFieldType.Enabled = true;
            lblFieldStar.Visible = true;
            lblTypeStar.Visible = true;
            lblLUTStar.Visible = false;

            if (enumCmbbxTranslation.Text == TranslatingType.None.ToString())
            {
                txtTranslationValue.Enabled = false;
                txtTranslationValue.Text = "";
            }
            else if (enumCmbbxTranslation.Text == TranslatingType.LookUpTable.ToString() || enumCmbbxTranslation.Text == TranslatingType.LookUpTableReverse.ToString())
            {
                txtTranslationValue.Enabled = true;
                lblLUTStar.Visible = true;
                txtTranslationValue.Visible = false;
                cmbbxResult.Visible = true;
            }
            else
            {
                if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString())
                {
                    txtThirdPartyFieldName.Enabled = false;
                    enumCmbbxThirdPartyFieldType.Enabled = false;
                    lblFieldStar.Visible = false;
                    lblTypeStar.Visible = false;
                }

                txtTranslationValue.Visible = true;
                cmbbxResult.Visible = false;
                txtTranslationValue.Enabled = true;
                lblLUTStar.Visible = true;
            }
        }

        private class LUTWrapper : LookupTable
        {
            public LUTWrapper(string tableName)
            {
                this.DisplayName = this.TableName = tableName;
            }

            public LUTWrapper(LookupTable lut)
            {
                this.DisplayName = lut.DisplayName;
                this.TableName = lut.TableName;
            }

            public override string ToString()
            {
                return DisplayName;
            }
        }


        private void GetLUTTables(XCollection<LookupTable> lookupTables)
        {
            foreach (LookupTable table in lookupTables)
            {
                cmbbxResult.Items.Add(new LUTWrapper(table));
            }

            string dbcnn = Program.GWDataDBConnection;
            if (dbcnn != null && dbcnn.Length > 0)
            {
                LutMgt mgt = new LutMgt(new DataBase(dbcnn));
                string[] lutList = mgt.GetLutNames();
                if (lutList != null)
                {
                    foreach (string lut in lutList)
                    {
                        bool hasAdded = false;
                        foreach (LookupTable table in lookupTables)
                        {
                            if (table.TableName == lut)
                            {
                                hasAdded = true;
                                break;
                            }
                        }
                        if (!hasAdded) cmbbxResult.Items.Add(new LUTWrapper(lut));
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
                MessageBox.Show( "The field name should only contain character, number or '_', and should begin with character, please input another name.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsGCFieldExist(string fieldName)
        {
            foreach (GWDataDBField field in fieldSet)
            {
                if (type == "Add")
                {
                    if (fieldName.Equals(field.FieldName) && field.Table.ToString().Equals(enumCmbbxTable.Text))
                    {
                        return true;
                    }
                }
                else
                {
                    if (fieldName.Equals(field.FieldName) && (fieldName != resultList[resultIndex].GWDataDBField.FieldName) && field.Table.ToString().Equals(enumCmbbxTable.Text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void GetGCFieldSet(XCollection<SocketInQueryResultItem> fieldList)
        {
            foreach (SocketInQueryResultItem field in fieldList)
            {
                fieldSet.Add(field.GWDataDBField);
            }
        }

        private bool NullExist()
        {
            if (txtThirdPartyFieldName.Text.Trim() == "" && enumCmbbxTranslation.Text != TranslatingType.FixValue.ToString())
            {
                MessageBox.Show( "Please input a value of source field name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else if (enumCmbbxTable.Text.Trim() == GWDataDBTable.None.ToString())
            {
                MessageBox.Show( "Please choose a table name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else if (cmbbxGWField.Text.Trim() == "")
            {
                MessageBox.Show( "Please choose a field name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else if ((enumCmbbxTranslation.Text.Trim() == TranslatingType.LookUpTable.ToString() ||
      enumCmbbxTranslation.Text.Trim() == TranslatingType.LookUpTableReverse.ToString())
           && cmbbxResult.Text.Trim() == "")
            {
                MessageBox.Show("Please input a value for translation!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (enumCmbbxTranslation.Text != TranslatingType.FixValue.ToString()) {
                if (!IsParaValid(txtThirdPartyFieldName.Text.Trim()))
                {
                    return;
                }
            }

            if (NullExist()) {
                return;
            }

            if (IsGCFieldExist(cmbbxGWField.Text)) {
                MessageBox.Show( "GC Gateway field is existing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Save();
            parentlForm.ShowResultList();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}