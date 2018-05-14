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

namespace HYS.SocketAdapter.SocketOutboundAdapterConfiguration.Forms
{
    public partial class FQueryResult : Form
    {
        #region Local members
        FChannel parentlForm;
        XCollection<SocketOutQueryResultItem> resultList;
        SocketOutQueryResultItem resultItem;
        List<string> fieldSet = new List<string>();
        string type;
        #endregion

        #region Constructor
        public FQueryResult(FChannel frm, XCollection<SocketOutQueryResultItem> resultItems)
        {
            InitializeComponent();
            parentlForm = frm;
            type = "Add";
            resultList = resultItems;
            resultItem = new SocketOutQueryResultItem();
            this.Text = "Add Result Item";
            GetLUTTables(SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.LookupTables);
            GetThirdParaSet(resultItems);
        }

        int resultIndex;
        public FQueryResult(FChannel frm, XCollection<SocketOutQueryResultItem> resultItems, int index)
        {
            InitializeComponent();
            parentlForm = frm;
            type = "Edit";
            resultList = resultItems;
            resultIndex = index;
            resultItem = resultItems[index];
            GetLUTTables(SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.LookupTables);            
            this.Text = "Edit Result Item";
            GetThirdParaSet(resultItems);
            ShowResultItem();
        }
        #endregion

        #region Show and Save
        private void ShowResultItem()
        {
            this.enumCmbbxTable.Text = resultItem.GWDataDBField.Table.ToString();
            this.cmbbxGWField.Text = resultItem.GWDataDBField.FieldName;
            this.txtThirdPartyFieldName.Text = resultItem.ThirdPartyDBPatamter.FieldName;
            this.enumCmbbxThirdPartyFieldType.Text = resultItem.ThirdPartyDBPatamter.FieldType.ToString();
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
            else
            {
                this.txtTranslationValue.Text = "";
            }
            enumCmbbxRedundancy.Text = resultItem.RedundancyFlag.ToString();
        }

        private void Save()
        {
            if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString())
            {
                enumCmbbxTable.Text = GWDataDBTable.None.ToString();
            }

            resultItem.GWDataDBField.Table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), this.enumCmbbxTable.Text.Trim());
            resultItem.GWDataDBField.FieldName = this.cmbbxGWField.Text.Trim();
            resultItem.ThirdPartyDBPatamter.FieldName = this.txtThirdPartyFieldName.Text.Trim();
            resultItem.ThirdPartyDBPatamter.FieldType = (OleDbType)Enum.Parse(typeof(OleDbType), this.enumCmbbxThirdPartyFieldType.Text.Trim());
            resultItem.SourceField = this.cmbbxGWField.Text.Trim();
            resultItem.TargetField = this.txtThirdPartyFieldName.Text.Trim();
            resultItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), this.enumCmbbxTranslation.Text.Trim());
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

        private void RefreshControls()
        {
            enumCmbbxTable.Enabled = true;
            cmbbxGWField.Enabled = true;
            lblTableStar.Visible = true;
            lblFieldStar.Visible = true;
            txtTranslationValue.Enabled = true;
            lblLUTStar.Visible = false;
            txtTranslationValue.Visible = true;
            cmbbxResult.Visible = false;
        }

        private void enumCmbbxTranslation_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshControls();

            if (enumCmbbxTranslation.Text == "None")
            {
                txtTranslationValue.Enabled = false;
                txtTranslationValue.Text = "";
            }
            else if (enumCmbbxTranslation.Text == "LookUpTable" || enumCmbbxTranslation.Text == "LookUpTableReverse")
            {
                lblLUTStar.Visible = true;
                txtTranslationValue.Enabled = true;

                txtTranslationValue.Visible = false;
                cmbbxResult.Visible = true;
            }
            else
            {
                if (enumCmbbxTranslation.Text == TranslatingType.FixValue.ToString())
                {
                    enumCmbbxTable.Enabled = false;
                    cmbbxGWField.Enabled = false;
                    lblTableStar.Visible = false;
                    lblFieldStar.Visible = false;
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

        #region Check the identity and valid of Parameter
        private bool IsFieldValid(string paraName)
        {
            if (!CheckItemValid.IsValid(paraName))
            {
                MessageBox.Show( "The field name should only contain character, number or '_', and should begin with character, please input another name.", "Third Party Field Validate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsFieldExist(string paraName)
        {
            if (type == "Add")
            {
                if (CheckItemValid.IsContain(paraName, fieldSet, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            else
            {
                if (CheckItemValid.IsContain(paraName, fieldSet, StringComparison.OrdinalIgnoreCase) && paraName != resultList[resultIndex].ThirdPartyDBPatamter.FieldName)
                {
                    return true;
                }
            }
            return false;
        }

        private void GetThirdParaSet(XCollection<SocketOutQueryResultItem> fieldList)
        {
            foreach (SocketOutQueryResultItem field in fieldList)
            {
                fieldSet.Add(field.ThirdPartyDBPatamter.FieldName);
            }
        }

        private bool NullExist()
        {
            if (enumCmbbxTable.Text.Trim() == GWDataDBTable.None.ToString() && enumCmbbxTranslation.Text != TranslatingType.FixValue.ToString())
            {
                MessageBox.Show( "Please choose a table name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else if (cmbbxGWField.Text.Trim() == "" && enumCmbbxTranslation.Text != TranslatingType.FixValue.ToString())
            {
                MessageBox.Show( "Please choose a field name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else if (txtThirdPartyFieldName.Text.Trim() == "")
            {
                MessageBox.Show( "Please input a value of target field name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else if ((enumCmbbxTranslation.Text.Trim() == TranslatingType.LookUpTable.ToString() ||
 enumCmbbxTranslation.Text.Trim() == TranslatingType.LookUpTableReverse.ToString())
      && cmbbxResult.Text.Trim() == "")
            {
                MessageBox.Show( "Please input a value for translation!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (!IsFieldValid(txtThirdPartyFieldName.Text.Trim()))
            {
                return;
            }

            if (NullExist()) {
                return;
            }

            if (IsFieldExist(txtThirdPartyFieldName.Text.Trim()))
            {
                MessageBox.Show( "Field is existing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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