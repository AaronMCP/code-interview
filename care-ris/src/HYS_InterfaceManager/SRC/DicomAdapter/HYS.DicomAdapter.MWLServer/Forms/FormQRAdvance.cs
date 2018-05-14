using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.MWLServer.Dicom;

namespace HYS.DicomAdapter.MWLServer.Forms
{
    public partial class FormQRAdvance : Form
    {
        public FormQRAdvance()
        {
            InitializeComponent();
        }

        private void LoadSetting()
        {
            string pkColumn = Program.ConfigMgt.Config.PrimaryKeyColumnName;
            if (pkColumn == WorklistSCPHelper.DataColumns.StudyInstanceUID)
            {
                SingleCheckItem(this.checkedListBoxMerge, 0);
            }
            else if (pkColumn == WorklistSCPHelper.DataColumns.AccessionNumber)
            {
                SingleCheckItem(this.checkedListBoxMerge, 1);
            }
            else if (pkColumn == WorklistSCPHelper.DataColumns.PatientID)
            {
                SingleCheckItem(this.checkedListBoxMerge, 2);
            }

            string cvColumn = Program.ConfigMgt.Config.CodeValueColumnName;
            if (cvColumn == WorklistSCPHelper.DataColumns.CodeValueOfScheduledProtocolCodeSequence)
            {
                SingleCheckItem(this.checkedListBoxSplit, 0);
            }
            else if (cvColumn == WorklistSCPHelper.DataColumns.CodeValueOfRequestedProcedureCodeSequence)
            {
                SingleCheckItem(this.checkedListBoxSplit, 1);
            }

            this.textBoxSeperator.Text = Program.ConfigMgt.Config.CodeValueSeperator.ToString();

            radioButtonSpliterEnable.Checked = Program.ConfigMgt.Config.SplitCodeValue;
            radioButtonMergerEnable.Checked = Program.ConfigMgt.Config.MergeElementList;

            this.comboBoxCharacterSet.Text = Program.ConfigMgt.Config.CharacterSetName;
        }
        private bool SaveSetting()
        {
            if (this.radioButtonSpliterEnable.Checked && this.checkedListBoxSplit.CheckedItems.Count < 1)
            {
                SingleCheckItem(this.checkedListBoxSplit, 0);
            }

            if (this.radioButtonMergerEnable.Checked && this.checkedListBoxMerge.CheckedItems.Count < 1)
            {
                SingleCheckItem(this.checkedListBoxMerge, 0);
            }

            if (this.radioButtonSpliterEnable.Checked &&
                this.textBoxSeperator.Text.Length < 1 &&
                this.checkedListBoxSplit.CheckedItems.Count > 0)
            {
                MessageBox.Show(this, "Please input a multiple value seperating charator.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.textBoxSeperator.Focus();
                return false;
            }

            string pkColumn = "";
            if (this.checkedListBoxMerge.CheckedItems.Count > 0)
            {
                int index = this.checkedListBoxMerge.Items.IndexOf(this.checkedListBoxMerge.CheckedItems[0]);
                switch (index)
                {
                    case 0: pkColumn = WorklistSCPHelper.DataColumns.StudyInstanceUID; break;
                    case 1: pkColumn = WorklistSCPHelper.DataColumns.AccessionNumber; break;
                    case 2: pkColumn = WorklistSCPHelper.DataColumns.PatientID; break;
                }
            }
            Program.ConfigMgt.Config.PrimaryKeyColumnName = pkColumn;

            string cvColumn = "";
            if (this.checkedListBoxSplit.CheckedItems.Count > 0)
            {
                int index = this.checkedListBoxSplit.Items.IndexOf(this.checkedListBoxSplit.CheckedItems[0]);
                switch (index)
                {
                    case 0: cvColumn = WorklistSCPHelper.DataColumns.CodeValueOfScheduledProtocolCodeSequence; break;
                    case 1: cvColumn = WorklistSCPHelper.DataColumns.CodeValueOfRequestedProcedureCodeSequence; break;
                }
            }
            Program.ConfigMgt.Config.CodeValueColumnName = cvColumn;

            string str = this.textBoxSeperator.Text;
            Program.ConfigMgt.Config.CodeValueSeperator = str.Length > 0 ? str[0] : '\0';

            Program.ConfigMgt.Config.SplitCodeValue = radioButtonSpliterEnable.Checked;
            Program.ConfigMgt.Config.MergeElementList = radioButtonMergerEnable.Checked;

            Program.ConfigMgt.Config.CharacterSetName = this.comboBoxCharacterSet.Text;

            return true;
        }
        private void FixMergeList()
        {
            if (checkedListBoxSplit.CheckedIndices.Count > 0)
            {
                FixMergeList(checkedListBoxSplit.CheckedIndices[0] == 1);
            }
            else
            {
                FixMergeList(false);
            }
        }
        private void FixMergeList(bool value)
        {
            if (value)
            {
                this.radioButtonMergerDisable.Checked = true;
                this.panelMerge.Enabled = false;
            }
            else
            {
                this.panelMerge.Enabled = true;
            }
        }
        
        private bool _checkLocked = false;
        private void SingleCheckItem(CheckedListBox clbox, int index)
        {
            _checkLocked = true;
            for (int i = 0; i < clbox.Items.Count; i++) clbox.SetItemChecked(i, false);
            clbox.SetItemChecked(index, true);
            _checkLocked = false;
        }
        private void checkedListBoxSplit_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_checkLocked) return;
            if (e.NewValue == CheckState.Checked) SingleCheckItem(this.checkedListBoxSplit, e.Index);
            FixMergeList(e.Index == 1);

            // DICOM PS 3.4 - 2006 Page 185
            // The Requested Procedure COde Sequence shall contain only a single item.
        }
        private void checkedListBoxMerge_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_checkLocked) return;
            if (e.NewValue == CheckState.Checked) SingleCheckItem(this.checkedListBoxMerge, e.Index);
        }
        private void radioButtonMergerEnable_CheckedChanged(object sender, EventArgs e)
        {
            this.checkedListBoxMerge.Enabled = this.radioButtonMergerEnable.Checked;
        }
        private void radioButtonSpliterEnable_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxSeperator.Enabled =
                this.checkedListBoxSplit.Enabled = this.radioButtonSpliterEnable.Checked;
        }
        private void FormQRAdvance_Load(object sender, EventArgs e)
        {
            LoadSetting();
            FixMergeList();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveSetting())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}