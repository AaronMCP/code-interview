using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;

namespace OutboundDBInstall
{
    public partial class FSelFields : Form
    {
        public enum FormType
        { ftPKF, ftMgF }

        PKFields _PKFields;
        MergeFields _MergeFields;
        FormType _FormType;


        public FSelFields()
        {
            InitializeComponent();
            this.LoadFields();
        }

        #region Show
        private new DialogResult ShowDialog()
        {
            return base.ShowDialog();
        }

        public DialogResult ShowDialog(IWin32Window owner, PKFields pkfs, bool bWizard)
        {

            if (bWizard)
            {
                this.Text = "Merging Record Wizard";
                this.labWizard.Text = "Step 1: Please Select Match Fields";
            }
            else
            {
                this.Text = "Merging Record Setting";
                this.labWizard.Text = "Please Select Match Fields";
            }
            _FormType = FormType.ftPKF;
            _PKFields = pkfs;
            CheckFields(pkfs);
            return base.ShowDialog(owner);
        }



        public DialogResult ShowDialog(IWin32Window owner, MergeFields mgfs, bool bWizard)
        {
            if (bWizard)
            {
                this.Text = "Merging Record Wizard";
                this.labWizard.Text = "Step 2: Please Select Merge Fields";
            }
            else
            {
                this.Text = "Merging Record Setting";
                this.labWizard.Text = "Please Select Merge Fields";
            }
            _FormType = FormType.ftMgF;
            _MergeFields = mgfs;
            CheckFields(mgfs);
            return base.ShowDialog(owner);
        }

        #endregion


        #region Load Field
        private void LoadFields()
        {
            this.listView1.Items.Clear();

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

                ListViewItem lvi = this.listView1.Items.Add("");
                lvi.SubItems.Add(item.FieldName);
                lvi.SubItems.Add("");
                lvi.Group = listView1.Groups["grpIndex"];
                lvi.Tag = item;

            }

            foreach (GWDataDBField item in Patient)
            {
                if (item.FieldName.ToUpper() == GWDataDBField.p_DATA_ID.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.p_DATA_DT.FieldName.ToUpper())
                    continue;
                ListViewItem lvi = this.listView1.Items.Add("");
                lvi.SubItems.Add(item.FieldName);
                lvi.Group = listView1.Groups["grpPatient"];
                lvi.Tag = item;

            }

            foreach (GWDataDBField item in Order)
            {
                if (item.FieldName.ToUpper() == GWDataDBField.o_DATA_ID.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.o_DATA_DT.FieldName.ToUpper())
                    continue;

                ListViewItem lvi = this.listView1.Items.Add("");
                lvi.SubItems.Add(item.FieldName);
                lvi.Group = listView1.Groups["grpOrder"];
                lvi.Tag = item;
            }

            foreach (GWDataDBField item in Report)
            {
                if (item.FieldName.ToUpper() == GWDataDBField.r_DATA_ID.FieldName.ToUpper())
                    continue;
                if (item.FieldName.ToUpper() == GWDataDBField.r_DATA_DT.FieldName.ToUpper())
                    continue;
                ListViewItem lvi = this.listView1.Items.Add("");
                lvi.SubItems.Add(item.FieldName);
                lvi.Group = listView1.Groups["grpReport"];
                lvi.Tag = item;
            }
        }

        private void CheckFields(PKFields pkfs)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                GWDataDBField dbf = (GWDataDBField)listView1.Items[i].Tag;
                //if (pkfs.FindField(dbf.Table.ToString(), dbf.FieldName) != null)
                if (pkfs.FindField(GWDataDB.GetTableName(dbf.Table), dbf.FieldName) != null)
                    this.listView1.Items[i].Checked = true;
                else
                    this.listView1.Items[i].Checked = false;
            }
        }

        private void CheckFields(MergeFields mgfs)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                GWDataDBField dbf = (GWDataDBField)listView1.Items[i].Tag;
                //if (mgfs.FindField(dbf.Table.ToString(), dbf.FieldName) != null)
                if (mgfs.FindField(GWDataDB.GetTableName(dbf.Table), dbf.FieldName) != null)
                    this.listView1.Items[i].Checked = true;
                else
                    this.listView1.Items[i].Checked = false;
            }
        }

        #endregion

        #region Save Field
        private void SaveChecked()
        {
            if (_FormType == FormType.ftPKF)
            {
                _PKFields.Clear();
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    ListViewItem lvi = this.listView1.Items[i];
                    GWDataDBField dbf = (GWDataDBField)lvi.Tag;

                    if (lvi.Checked)
                        _PKFields.Add(new PKField(GWDataDB.GetTableName(dbf.Table), dbf.FieldName, "AND"));
                    //_PKFields.Add(new PKField(dbf.Table.ToString(),dbf.FieldName,"AND"));
                }
            }

            if (_FormType == FormType.ftMgF)
            {
                _MergeFields.Clear();
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    ListViewItem lvi = this.listView1.Items[i];
                    GWDataDBField dbf = (GWDataDBField)lvi.Tag;

                    if (lvi.Checked)
                        _MergeFields.Add(new MergeField(GWDataDB.GetTableName(dbf.Table), dbf.FieldName));
                    //_MergeFields.Add(new MergeField(dbf.Table.ToString(), dbf.FieldName));
                }
            }
        }
        #endregion
        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.listView1.CheckedItems.Count < 1)
            {
                MessageBox.Show("Please select some items!");
                return;
            }
            SaveChecked();
            this.DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btAllCheck_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.Items)
                item.Checked = true;
        }

        private void btAllNoCheck_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.Items)
                item.Checked = false;
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            EditOutboundField(e.Item);
        }

        private static void EditOutboundField(ListViewItem item)
        {
            //MatchFieldTree tree = item.Tag as MatchFieldTree;
            //FConfigMergingCriteria frm = new FConfigMergingCriteria(tree);

            //frm.ShowDialog();

            //item.Tag = frm.CriteriaTree;
            //item.SubItems[2].Text = frm.CriteriaTree.GetSQLStatement() ;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            EditOutboundField(listView1.SelectedItems[0]);
        }


    }
}