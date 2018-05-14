using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;

namespace OutboundDBInstall
{
    public partial class FConfigCriteria : Form
    {
        private bool _isNew = true;

        public FConfigCriteria()
        {
            InitializeComponent();

            InitForm();
        }

        private void InitForm()
        {
            this.comboBoxOutTable.DataSource = Enum.GetNames(typeof(GWDataDBTable));
            this.comboBoxLogicOperator.DataSource = Enum.GetNames(typeof(LogicOperator));
            this.comboBoxInTable.DataSource = Enum.GetNames(typeof(GWDataDBTable));
        }

        public event SaveCondidtionEventHandler SaveCriteria;

        protected void OnSaveCriteria()
        {
            if (null != SaveCriteria)
            {
                SaveCondidtionEventArgs e = new SaveCondidtionEventArgs();
                e.IsNew = _isNew;
                e.Criteria = GetCriteria();

                SaveCriteria(this, e);
            }
        }

        private MatchField GetCriteria()
        {
           MatchField mf = new MatchField();
           mf.LogicOperator = (LogicOperator)Enum.Parse(typeof(LogicOperator), comboBoxLogicOperator.Text);
           mf.TableName = comboBoxOutTable.Text;
           mf.FieldName = comboBoxOutField.Text;
           mf.FixValue = GetCriteriaValue();
           return mf;
        }

        private MatchFieldValue GetCriteriaValue()
        {
            MatchFieldValue v = new MatchFieldValue();
            if (radioButtonFixValue.Checked)
            {
                v.ValueType = FieldValueType.FixValue;
                v.Value = textBoxFixValue.Text;
            }
            else
            {
                v.ValueType = FieldValueType.InboundTableField;
                v.InboundTableName = comboBoxInTable.Text;
                v.InboundFieldName = comboBoxInField.Text;
            }

            return v;
        }

        public void ShowDialog(IWin32Window owner, MatchField criteria)
        {
            _isNew = false;
            LoadCriteria(criteria);
            this.ShowDialog(owner);
        }

        private void LoadCriteria(MatchField criteria)
        {
            GWDataDBField[] fields = GWDataDBField.GetFields((GWDataDBTable)Enum.Parse(typeof(GWDataDBTable),criteria.TableName));

            this.comboBoxOutField.Items.Clear();
            foreach (GWDataDBField field in fields)
            {
                this.comboBoxOutField.Items.Add(field.FieldName);
            }

            this.comboBoxOutTable.Text = criteria.TableName;
            this.comboBoxOutField.Text = criteria.FieldName;
            this.comboBoxLogicOperator.Text = criteria.LogicOperator.ToString();

            if (criteria.FixValue.ValueType == FieldValueType.FixValue)
            {
                this.textBoxFixValue.Text = criteria.FixValue.Value;
                this.radioButtonFixValue.Checked = true;
                this.radioButtonInboundField.Checked = false;
                textBoxFixValue.Enabled = true;
                comboBoxInTable.Enabled = false;
                comboBoxInField.Enabled = false;
            }
            else
            {
                fields = GWDataDBField.GetFields((GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), criteria.FixValue.InboundTableName));

                this.comboBoxInField.Items.Clear();
                foreach (GWDataDBField field in fields)
                {
                    this.comboBoxInField.Items.Add(field.FieldName);
                }
                this.comboBoxInTable.Text = criteria.FixValue.InboundTableName;
                this.comboBoxInField.Text = criteria.FixValue.InboundFieldName;
                this.radioButtonFixValue.Checked = false;
                this.radioButtonInboundField.Checked = true;
                textBoxFixValue.Enabled = false;
                comboBoxInTable.Enabled = true;
                comboBoxInField.Enabled = true; ;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                return;
            }

            OnSaveCriteria();

            this.Close();
        }

        private bool CheckData()
        {
            if (comboBoxOutTable.Text == GWDataDBTable.None.ToString())
            {
                MessageBox.Show("Please select outbound table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBoxOutTable.Focus();
                return false;
            }

            if (radioButtonFixValue.Checked && string.IsNullOrEmpty(textBoxFixValue.Text.Trim()))
            {
                MessageBox.Show("Please intput the fix value.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxFixValue.Focus();
                return false;
            }

            if (radioButtonInboundField.Checked && comboBoxInTable.Text == GWDataDBTable.None.ToString())
            {
                MessageBox.Show("Please select inbound table.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBoxInTable.Focus();
                return false;
            }

            return true;
        }

        private void comboBoxOutTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            GWDataDBTable table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), comboBoxOutTable.Text);
            if (table == GWDataDBTable.None)
            {
                this.comboBoxOutField.Items.Clear();
                return;
            }

            GWDataDBField[] fields = GWDataDBField.GetFields(table);

            this.comboBoxOutField.Items.Clear();
            foreach (GWDataDBField field in fields)
            {
                if (field.FieldName.ToUpper() == GWDataDBField.i_IndexGuid.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.i_DataDateTime.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.i_PROCESS_FLAG.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.p_DATA_ID.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.p_DATA_DT.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.o_DATA_ID.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.o_DATA_DT.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.r_DATA_ID.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.r_DATA_DT.FieldName.ToUpper())
                    continue;

                this.comboBoxOutField.Items.Add(field.FieldName);
            }
            this.comboBoxOutField.SelectedIndex = 0;
        }

        private void radioButtonFixValue_CheckedChanged(object sender, EventArgs e)
        {
            EnableValueControls();
        }

        private void EnableValueControls()
        {
            if (radioButtonFixValue.Checked)
            {
                textBoxFixValue.Enabled = true;
                comboBoxInTable.Enabled = false;
                comboBoxInField.Enabled = false;
                radioButtonInboundField.Checked = false;
            }
            else
            {
                textBoxFixValue.Enabled = false;
                comboBoxInTable.Enabled = true;
                comboBoxInField.Enabled = true;
            }
        }

        private void radioButtonInboundField_CheckedChanged(object sender, EventArgs e)
        {
            EnableValueControls();
            if (radioButtonInboundField.Checked)
            {
                radioButtonFixValue.Checked = false;
            }
        }

        private void comboBoxInTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            GWDataDBTable table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), comboBoxInTable.Text);
            if (table == GWDataDBTable.None)
            {
                this.comboBoxInField.Items.Clear();
                return;
            }

            GWDataDBField[] fields = GWDataDBField.GetFields(table);

            this.comboBoxInField.Items.Clear();
            foreach (GWDataDBField field in fields)
            {
                if (field.FieldName.ToUpper() == GWDataDBField.i_IndexGuid.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.i_DataDateTime.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.i_PROCESS_FLAG.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.p_DATA_ID.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.p_DATA_DT.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.o_DATA_ID.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.o_DATA_DT.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.r_DATA_ID.FieldName.ToUpper())
                    continue;
                if (field.FieldName.ToUpper() == GWDataDBField.r_DATA_DT.FieldName.ToUpper())
                    continue;

                this.comboBoxInField.Items.Add(field.FieldName);
            }
            this.comboBoxInField.SelectedIndex = 0;
        }
    }

    public class SaveCondidtionEventArgs : EventArgs
    {
        public MatchField Criteria { get; set; }

        public bool IsNew { get; set; }
    }

    public delegate void SaveCondidtionEventHandler(object sender,SaveCondidtionEventArgs e);
}
