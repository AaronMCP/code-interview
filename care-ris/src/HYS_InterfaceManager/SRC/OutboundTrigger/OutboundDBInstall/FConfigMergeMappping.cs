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
    public partial class FConfigMergeMappping : Form
    {
        private MergeFieldMapping _map = null;

        public FConfigMergeMappping()
        {
            InitializeComponent();

            InitForm();
        }

        private void InitForm()
        {
            this.comboBoxTable.DataSource = Enum.GetNames(typeof(GWDataDBTable));
        }

        public MergeFieldMapping Mapping { get; set; }

        internal DialogResult ShowDialog(IWin32Window owner, MergeFieldMapping map)
        {
            _map = map;

            LoadMapping();

            return this.ShowDialog(owner);
        }

        private void LoadMapping()
        {
            comboBoxTable.Text = _map.InboundTable;
            comboBoxTable_SelectedIndexChanged(comboBoxTable, EventArgs.Empty);

            comboBoxField.Text = _map.InboundField;
        }

        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxField.Items.Clear();

            GWDataDBTable table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), comboBoxTable.Text);
            if (table == GWDataDBTable.None)
            {
                return;
            }

            GWDataDBField[] fields = GWDataDBField.GetFields(table);
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

                this.comboBoxField.Items.Add(field.FieldName);
            }
            this.comboBoxField.SelectedIndex = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (SaveFieldMapping())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool SaveFieldMapping()
        {
            GWDataDBTable table = (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), comboBoxTable.Text);
            if (table == GWDataDBTable.None)
            {
                MessageBox.Show("Please select inbound table and field.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBoxTable.Focus();
                return false;
            }

            Mapping = new MergeFieldMapping();
            Mapping.OutboundTable = _map.OutboundTable;
            Mapping.OutboundField = _map.OutboundField;
            Mapping.InboundTable = comboBoxTable.Text.Trim();
            Mapping.InboundField = comboBoxField.Text.Trim();

            return true;
        }
    }
}
