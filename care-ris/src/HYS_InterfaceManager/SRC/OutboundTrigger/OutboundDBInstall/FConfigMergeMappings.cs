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
    public partial class FConfigMergeMappings : Form
    {
        public FConfigMergeMappings()
        {
            InitializeComponent();

            LoadFields();
        }

        public DialogResult ShowDialog(IWin32Window owner, MergeFieldMappings mappings, bool bWizard)
        {
            if (!bWizard)
            {
                labWizard.Text = " Please configure merging field mappings";
            }

            LoadMappings(mappings);

            return this.ShowDialog(owner);
        }

        private void LoadFields()
        {
            this.listViewMappings.Items.Clear();

            GWDataDBField[] Index = GWDataDBField.GetFields(GWDataDBTable.Index);
            GWDataDBField[] Patient = GWDataDBField.GetFields(GWDataDBTable.Patient);
            GWDataDBField[] Order = GWDataDBField.GetFields(GWDataDBTable.Order);
            GWDataDBField[] Report = GWDataDBField.GetFields(GWDataDBTable.Report);

            foreach (GWDataDBField item in Index)
            {
                if (item.FieldName.ToUpper() == GWDataDBField.i_IndexGuid.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.i_DataDateTime.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.i_PROCESS_FLAG.FieldName.ToUpper())
                    continue;

                ListViewItem lvi = this.listViewMappings.Items.Add("");
                lvi.SubItems.Add(item.FieldName);
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.Group = listViewMappings.Groups["grpIndex"];
                lvi.Tag = item;

            }

            foreach (GWDataDBField item in Patient)
            {
                if (item.FieldName.ToUpper() == GWDataDBField.p_DATA_ID.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.p_DATA_DT.FieldName.ToUpper())
                    continue;
                ListViewItem lvi = this.listViewMappings.Items.Add("");
                lvi.SubItems.Add(item.FieldName);
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.Group = listViewMappings.Groups["grpPatient"];
                lvi.Tag = item;

            }

            foreach (GWDataDBField item in Order)
            {
                if (item.FieldName.ToUpper() == GWDataDBField.o_DATA_ID.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.o_DATA_DT.FieldName.ToUpper())
                    continue;

                ListViewItem lvi = this.listViewMappings.Items.Add("");
                lvi.SubItems.Add(item.FieldName);
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.Group = listViewMappings.Groups["grpOrder"];
                lvi.Tag = item;
            }

            foreach (GWDataDBField item in Report)
            {
                if (item.FieldName.ToUpper() == GWDataDBField.r_DATA_ID.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.r_DATA_DT.FieldName.ToUpper())
                    continue;
                ListViewItem lvi = this.listViewMappings.Items.Add("");
                lvi.SubItems.Add(item.FieldName);
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
                lvi.Group = listViewMappings.Groups["grpReport"];
                lvi.Tag = item;
            }
        }

        private void LoadMappings(MergeFieldMappings mappings)
        {
            this.listViewMappings.ItemCheck -= new ItemCheckEventHandler(listViewMappings_ItemCheck);
            for (int i = 0; i < listViewMappings.Items.Count; i++)
            {
                GWDataDBField dbf = (GWDataDBField)listViewMappings.Items[i].Tag;

                MergeFieldMapping map = mappings.FindMapping(dbf.Table.ToString(), dbf.FieldName);
                if ( map != null)
                {
                    this.listViewMappings.Items[i].Checked = true;
                    this.listViewMappings.Items[i].SubItems[2].Text = "<==";
                    this.listViewMappings.Items[i].SubItems[3].Text = map.InboundTable + "." + map.InboundField;
                    this.listViewMappings.Items[i].SubItems[1].Tag = map;
                }
                else
                {
                    this.listViewMappings.Items[i].Checked = false;
                    this.listViewMappings.Items[i].SubItems[2].Text = string.Empty;
                    this.listViewMappings.Items[i].SubItems[3].Text = string.Empty;
                    this.listViewMappings.Items[i].SubItems[1].Tag = null;
                }
            }

            this.listViewMappings.ItemCheck += new ItemCheckEventHandler(listViewMappings_ItemCheck);
        }

        private void listViewMappings_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                this.listViewMappings.Items[e.Index].SubItems[2].Text = string.Empty;
                this.listViewMappings.Items[e.Index].SubItems[3].Text = string.Empty;
                this.listViewMappings.Items[e.Index].SubItems[1].Tag = null;
            }
            else
            {
                GWDataDBField dbf = (GWDataDBField)listViewMappings.Items[e.Index].Tag;

                MergeFieldMapping map = listViewMappings.Items[e.Index].SubItems[1].Tag as MergeFieldMapping;
                if (map == null)
                {
                    map = new MergeFieldMapping();
                    map.OutboundTable = dbf.Table.ToString();
                    map.OutboundField = dbf.FieldName;
                    map.InboundTable = dbf.Table.ToString();
                    map.InboundField = dbf.FieldName;
                }

                 this.listViewMappings.Items[e.Index].SubItems[2].Text = "<==";
                 this.listViewMappings.Items[e.Index].SubItems[3].Text = map.InboundTable + "." + map.InboundField;
                 this.listViewMappings.Items[e.Index].SubItems[1].Tag = map;
            }
        }

        private void btAllCheck_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewMappings.Items.Count; i++)
            {
                this.listViewMappings.Items[i].Checked = true;
            }
        }

        private void btAllNoCheck_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewMappings.Items.Count; i++)
            {
                this.listViewMappings.Items[i].Checked = false;
            }
        }

        private void FConfigMergeMappings_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            SaveMergeFieldMapping();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SaveMergeFieldMapping()
        {
            MergeFieldMappings = new MergeFieldMappings();
            foreach (ListViewItem item in this.listViewMappings.Items)
            {
                if (item.Checked)
                {
                    MergeFieldMappings.Add(item.SubItems[1].Tag as MergeFieldMapping);
                }
            }
        }

        public MergeFieldMappings MergeFieldMappings { get; set; }

        private void buttonEditMapping_Click(object sender, EventArgs e)
        {
            if (this.listViewMappings.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select the item which you want to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            GWDataDBField dbf = (GWDataDBField)listViewMappings.SelectedItems[0].Tag;

            MergeFieldMapping map = listViewMappings.SelectedItems[0].SubItems[1].Tag as MergeFieldMapping;
            if (map == null)
            {
                map = new MergeFieldMapping();
                map.OutboundTable = dbf.Table.ToString();
                map.OutboundField = dbf.FieldName;
                map.InboundTable = dbf.Table.ToString();
                map.InboundField = dbf.FieldName;
            }

            FConfigMergeMappping frm = new FConfigMergeMappping();
            if (frm.ShowDialog(this, map) == DialogResult.OK)
            {
                map = frm.Mapping.Clone();
                listViewMappings.SelectedItems[0].SubItems[2].Text = "<==";
                listViewMappings.SelectedItems[0].SubItems[3].Text = map.InboundTable + "." + map.InboundField;
                listViewMappings.SelectedItems[0].SubItems[1].Tag = map;

                this.listViewMappings.ItemCheck -= new ItemCheckEventHandler(listViewMappings_ItemCheck);
                listViewMappings.SelectedItems[0].Checked = true;
                this.listViewMappings.ItemCheck += new ItemCheckEventHandler(listViewMappings_ItemCheck);
            }
        }
    }
}
