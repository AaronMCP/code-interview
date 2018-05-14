using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Dicom;
using HYS.Common.Dicom.Net;
using HYS.Common.Objects.Rule;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.MWLServer.Dicom;
using HYS.DicomAdapter.MWLServer.Objects;

namespace HYS.DicomAdapter.MWLServer.Forms
{
    public partial class FormQRElement : Form
    {
        private MWLQueryResultItem _qrItem;
        public MWLQueryResultItem QRItem
        {
            get { return _qrItem; }
        }

        private TagControler _tagControler;
        private FieldControler _fieldControler;

        private bool _isAuto;
        public FormQRElement(MWLQueryResultItem item)
        {
            InitializeComponent();
            
            _qrItem = item;
            
            _isAuto = (item.TargetField == WorklistSCPHelper.DataColumns.RequestedProcedureID ||
                item.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepID ||
                item.TargetField == WorklistSCPHelper.DataColumns.StudyInstanceUID);

            _tagControler = new TagControler(true, this.groupBoxDicom,
                this.comboBoxVR, this.comboBoxTag, this.textBoxGroupNum, this.textBoxElementNum);
            _tagControler.OnValueChanged += new EventHandler(_tagControler_OnValueChanged);

            _fieldControler = new FieldControler(true, this.groupBoxGateway,
                this.comboBoxTable, this.comboBoxField, this.checkBoxFixValue,
                this.textBoxFixValue, this.checkBoxLUT, this.comboBoxLUT, false);
            _fieldControler.OnValueChanged += new EventHandler(_fieldControler_OnValueChanged);

            _tagControler.Enabled = false;
        }

        private void InitializeData()
        {
            _tagControler.Initialize();
            _fieldControler.Initialize(Program.ConfigMgt.Config.GWDataDBConnection, Program.Log);
        }
        private void RefreshButton()
        {
            this.buttonOK.Enabled = _fieldControler.ValidateValue() && _tagControler.ValidateValue();
        }
        private void LoadSetting()
        {
            _fieldControler.LoadSetting(_qrItem);
            _tagControler.LoadSetting(_qrItem.DPath);

            if (_isAuto)
            {
                //this.checkBoxFixValue.Enabled = false;
                //this.checkBoxFixValue.Checked = false;
                //this.checkBoxLUT.Enabled = false;
                //this.checkBoxLUT.Checked = false;
                //this.textBoxFixValue.Enabled = false;
                //this.comboBoxLUT.Enabled = false;
                this.checkBoxAuto.Visible = true;

                if (_qrItem.TargetField == WorklistSCPHelper.DataColumns.RequestedProcedureID)
                {
                    this.checkBoxAuto.Checked = Program.ConfigMgt.Config.AutoGenerateRPID;
                }
                if (_qrItem.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepID)
                {
                    this.checkBoxAuto.Checked = Program.ConfigMgt.Config.AutoGenerateSPSPID;
                }
                if (_qrItem.TargetField == WorklistSCPHelper.DataColumns.StudyInstanceUID)
                {
                    this.checkBoxAuto.Checked = Program.ConfigMgt.Config.AutoGenerateSTDUID;
                }
            }
        }
        private bool SaveSetting()
        {
            _fieldControler.SaveSetting(_qrItem);
            _tagControler.SaveSetting(_qrItem.DPath);

            //foreach (MWLQueryResultItem item in Program.ConfigMgt.Config.Rule.QueryResult.MappingList)
            //{
            //    if (item == _qrItem ||
            //        item.GWDataDBField.Table == GWDataDBTable.None) continue;
            //    if (item.GWDataDBField.Table == _qrItem.GWDataDBField.Table &&
            //        item.GWDataDBField.FieldName == _qrItem.GWDataDBField.FieldName)
            //    {
            //        MessageBox.Show(this, "Element (" + item.DPath.GetTagName() +
            //            ") has been mapped to this GC Gateway field ("
            //            + _qrItem.GWDataDBField.GetFullFieldName() + "). \r\n"
            //            + "Pease change another GC Gateway field.", "Warning",
            //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        this.comboBoxField.Focus();
            //        return false;
            //    }
            //}

            //if (_isAuto)
            //{
            //    if (_qrItem.GWDataDBField.Table == GWDataDBTable.None ||
            //        _qrItem.GWDataDBField.FieldName.Length < 1)
            //    {
            //        MessageBox.Show(this, "Please select a field for this item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //}

            if (_isAuto)
            {
                if(_qrItem.TargetField == WorklistSCPHelper.DataColumns.RequestedProcedureID)
                {
                    Program.ConfigMgt.Config.AutoGenerateRPID = this.checkBoxAuto.Checked;
                }
                if(_qrItem.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepID )
                {
                    Program.ConfigMgt.Config.AutoGenerateSPSPID = this.checkBoxAuto.Checked ;
                }
                if (_qrItem.TargetField == WorklistSCPHelper.DataColumns.StudyInstanceUID)
                {
                    Program.ConfigMgt.Config.AutoGenerateSTDUID = this.checkBoxAuto.Checked;
                }
            }

            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!SaveSetting()) return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void FormQRElement_Load(object sender, EventArgs e)
        {
            InitializeData();
            LoadSetting();
        }
        private void _tagControler_OnValueChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }
        private void _fieldControler_OnValueChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }

        private void checkBoxAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAuto.Checked)
            {
                this.checkBoxFixValue.Checked = false;
                this.checkBoxLUT.Checked = false;
            }
        }

        private void checkBoxFixValue_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxFixValue.Checked)
            {
                this.checkBoxAuto.Checked = false;
            }
        }

        private void checkBoxLUT_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLUT.Checked)
            {
                this.checkBoxAuto.Checked = false;
            }
        }
    }
}