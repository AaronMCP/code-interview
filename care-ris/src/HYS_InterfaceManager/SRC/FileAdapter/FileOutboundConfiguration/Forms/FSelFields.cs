using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;
using HYS.Adapter.Base;
using HYS.Common.Xml;

namespace HYS.FileAdapter.FileOutboundAdapterConfiguration.Forms
{
    public partial class FSelFields : Form
    {
       
               
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

        XCollection<GWDataDBField> _FileFields;
        public DialogResult ShowDialog(IWin32Window owner, XCollection<GWDataDBField> FileFields)
        {

            _FileFields = FileFields;
            CheckFields(_FileFields);
            return base.ShowDialog(owner);
        }



        #endregion


        #region Load Field
        private void LoadFields()
        {
            this.listView1.Items.Clear();

            GWDataDBField[] Index   = GWDataDBField.GetFields(GWDataDBTable.Index);
            GWDataDBField[] Patient = GWDataDBField.GetFields(GWDataDBTable.Patient);
            GWDataDBField[] Order   = GWDataDBField.GetFields(GWDataDBTable.Order);
            GWDataDBField[] Report  = GWDataDBField.GetFields(GWDataDBTable.Report);

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

        private void CheckFields(XCollection<GWDataDBField> fields)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                this.listView1.Items[i].Checked = false;
                GWDataDBField dbf = (GWDataDBField)listView1.Items[i].Tag;
                foreach (GWDataDBField f in fields)
                {

                    if (f.GetFullFieldName() == dbf.GetFullFieldName())
                        this.listView1.Items[i].Checked = true;                   
                        
                }
            }
        }

        

        #endregion 

        #region Save Field
        private void SaveChecked()
        {
            _FileFields.Clear();

            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                ListViewItem lvi = this.listView1.Items[i];
                GWDataDBField dbf = (GWDataDBField)lvi.Tag;

                if (lvi.Checked)
                    _FileFields.Add(dbf);
            }
                       
        }
        #endregion
        private void btOK_Click(object sender, EventArgs e)
        {
            //if (this.listView1.CheckedItems.Count < 1)
            //{
            //    MessageBox.Show("Please select some items!");
            //    return;
            //}
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

        
    }
}