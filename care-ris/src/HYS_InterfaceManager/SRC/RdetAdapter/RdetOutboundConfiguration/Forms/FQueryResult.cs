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
using HYS.Common.Objects.Translation;
using HYS.RdetAdapter.Configuration;
using HYS.Common.Xml;
using HYS.Common.DataAccess;

namespace HYS.RdetAdapter.RdetOutboundAdapterConfiguration.Forms
{
    public partial class FQueryResult : Form
    {
        #region Local members
        FChannel parentlForm;
        XCollection<RdetOutQueryResultItem> resultList;
        RdetOutQueryResultItem resultItem;
        string type;
        #endregion

        #region Constructor
        public FQueryResult(FChannel frm, XCollection<RdetOutQueryResultItem> resultItems)
        {
            InitializeComponent();            
            parentlForm = frm;
            GetLUTTables(RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.LookupTables);
            type = "Add";
            resultList = resultItems;
            resultItem = new RdetOutQueryResultItem();
            this.Text = "Add Result Item";
            
        }

        public FQueryResult(FChannel frm, XCollection<RdetOutQueryResultItem> resultItems, int index)
        {
            InitializeComponent();            
            parentlForm = frm;
            GetLUTTables(RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.LookupTables);
            type = "Modify";
            resultList = resultItems;
            resultItem = resultItems[index];

            this.Text = "Modify Result Item";
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
            else
            {
                this.txtTranslationValue.Text = "";
            }
            enumCmbbxRedundancy.Text = resultItem.RedundancyFlag.ToString();
        }

        private void Save()
        {
            resultItem.ThirdPartyDBPatamter.FieldName = this.txtThirdPartyFieldName.Text.Trim();
            resultItem.ThirdPartyDBPatamter.FieldType = (OleDbType)Enum.Parse(typeof(OleDbType), this.enumCmbbxThirdPartyFieldType.Text);
            resultItem.GWDataDBField.Table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), this.enumCmbbxTable.Text);
            resultItem.GWDataDBField.FieldName = this.cmbbxGWField.Text.Trim();
            resultItem.SourceField = this.cmbbxGWField.Text.Trim();
            resultItem.TargetField = this.txtThirdPartyFieldName.Text.Trim(); 
            resultItem.Translating.Type = (TranslatingType)Enum.Parse(typeof(TranslatingType), this.enumCmbbxTranslation.Text);
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

        private void enumCmbbxTranslation_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTranslationValue.Visible = true;
            cmbbxResult.Visible = false;

            if (enumCmbbxTranslation.Text == "None")
            {
                txtTranslationValue.Enabled = false;
                txtTranslationValue.Text = "";
                lblResult.Text = "";
            }
            else if (enumCmbbxTranslation.Text == "LookUpTable" || enumCmbbxTranslation.Text == "LookUpTableReverse")
            {
                txtTranslationValue.Enabled = true;
                lblResult.Text = "TableName";
                txtTranslationValue.Visible = false;
                cmbbxResult.Visible = true;
            }
            else
            {
                txtTranslationValue.Enabled = true;
                lblResult.Text = "ConstValue";
            }
            /*
            if (enumCmbbxTranslation.Text == "None")
            {
                txtTranslationValue.Enabled = false;
                txtTranslationValue.Text = "";
                lblResult.Text = "";
            }
            else if (enumCmbbxTranslation.Text == "LookUpTable" || enumCmbbxTranslation.Text == "LookUpTableReverse")
            {
                txtTranslationValue.Enabled = true;
                lblResult.Text = "TableName";
            }
            else
            {
                txtTranslationValue.Enabled = true;
                lblResult.Text = "ConstValue";
            }*/
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

        #region OK and Cancel
        private void btnOK_Click(object sender, EventArgs e)
        {
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