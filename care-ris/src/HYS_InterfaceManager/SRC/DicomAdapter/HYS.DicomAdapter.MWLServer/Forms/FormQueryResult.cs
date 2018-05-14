using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Dicom;
using HYS.Common.Dicom.Net;
using HYS.Common.Objects.Rule;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.MWLServer.Dicom;
using HYS.DicomAdapter.MWLServer.Objects;

namespace HYS.DicomAdapter.MWLServer.Forms
{
    public partial class FormQueryResult : Form, IConfigUI
    {
        public FormQueryResult()
        {
            InitializeComponent();

            listControler = new QRListControler(this.listViewElement);
            listControler.DoubleClick += new EventHandler(listControler_DoubleClick);
            listControler.SelectedIndexChanged += new EventHandler(listControler_SelectedIndexChanged);
            listControler.EnsurePrimaryKey = true;
        }

        #region IConfigUI Members

        public Control GetControl()
        {
            this.buttonDefault.Visible = false;
            return this;
        }

        public bool LoadConfig()
        {
            return true;
        }

        public bool SaveConfig()
        {
            return Program.ConfigMgt.Save();
        }

        string IConfigUI.Name
        {
            get
            {
                return "C-FIND-RSP Mapping";
            }
        }

        #endregion

        private QRListControler listControler;

        private void RefreshList()
        {
            List<IDicomMappingItem> list = new List<IDicomMappingItem>();
            foreach (MWLQueryResultItem qr in Program.ConfigMgt.Config.Rule.QueryResult.MappingList)
            {
                list.Add(qr);
            }
            listControler.RefreshList(list.ToArray());
        }
        private void RefreshButton()
        {
            this.buttonClearSelection.Enabled =
                this.buttonSelectAll.Enabled = listControler.GetItemCount() > 0;

            MWLQueryResultItem item = listControler.GetSelectedItem() as MWLQueryResultItem; 
            //if (item == null ||
            //    item.DPath.VR == DVR.SQ ||
            //    item.DPath.VR == DVR.Unknown)
            //{
            //    this.buttonEdit.Enabled = false;
            //}
            //else
            //{
            //    //if (!Program.StandAlone)
            //    //{
            //    //    this.buttonEdit.Enabled =
            //    //         (item.TargetField != WorklistSCPHelper.DataColumns.RequestedProcedureID) &&
            //    //         (item.TargetField != WorklistSCPHelper.DataColumns.ScheduledProcedureStepID);
            //    //}
            //    //else
            //    //{
            //        this.buttonEdit.Enabled = true;
            //    //}
            //}

            this.buttonAdd.Enabled = true;
            bool isValid = (item != null) && (item.DPath.Type == DPathType.Normal);
            this.buttonAddChild.Enabled = isValid && (item.DPath.VR == DVR.SQ);
            this.buttonDelete.Enabled = buttonEdit2.Enabled = isValid;

            if (isValid)
            {
                if (item.TargetField == WorklistSCPHelper.DataColumns.PatientID ||
                    item.TargetField == WorklistSCPHelper.DataColumns.AccessionNumber ||
                    item.TargetField == WorklistSCPHelper.DataColumns.StudyInstanceUID ||
                    item.TargetField == WorklistSCPHelper.DataColumns.RequestedProcedureID ||
                    item.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepID ||
                    item.TargetField == WorklistSCPHelper.DataColumns.CodeValueOfRequestedProcedureCodeSequence ||
                    item.TargetField == WorklistSCPHelper.DataColumns.CodeValueOfScheduledProtocolCodeSequence)
                {
                    this.buttonDelete.Enabled = false;
                }
                else if (item.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepSequence ||
                    item.TargetField == WorklistSCPHelper.DataColumns.ScheduledProtocolCodeSequence ||
                    item.TargetField == WorklistSCPHelper.DataColumns.RequestedProcedureCodeSequence)
                {
                    this.buttonEdit2.Enabled = this.buttonDelete.Enabled = false;
                }
            }
        }

        private void LoadDefault()
        {
            Program.ConfigMgt.Config.Rule.QueryResult.MappingList.Clear();
            MWLQueryResultItem[] qrlist = MWLQueryResultItem.GetDefault();
            foreach (MWLQueryResultItem qr in qrlist)
            {
                Program.ConfigMgt.Config.Rule.QueryResult.MappingList.Add(qr);
            }
            RefreshList();
            RefreshButton();
        }
        private void EditElement()
        {
            MWLQueryResultItem item = listControler.GetSelectedItem() as MWLQueryResultItem;
            if (item == null) return;

            //if (!Program.StandAlone)
            //{
            //    if (item == null ||
            //        item.TargetField == WorklistSCPHelper.DataColumns.RequestedProcedureID ||
            //        item.TargetField == WorklistSCPHelper.DataColumns.ScheduledProcedureStepID) return;
            //}

            FormQRElement frm = new FormQRElement(item);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }
        private void Advance()
        {
            FormQRAdvance frm = new FormQRAdvance();
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            listControler.EnsurePrimaryKey = Program.ConfigMgt.Config.MergeElementList;
            if (listControler.EnsurePrimaryKey)
            {
                foreach (ListViewItem item in this.listViewElement.Items)
                {
                    MWLQueryResultItem qr = item.Tag as MWLQueryResultItem;
                    if (qr != null && qr.TargetField == Program.ConfigMgt.Config.PrimaryKeyColumnName)
                    {
                        qr.DPath.Enable = true;
                        break;
                    }
                }
                RefreshList();
            }
        }

        private void FormQueryResult_Load(object sender, EventArgs e)
        {
            RefreshList();
            RefreshButton();
        }
        private void listControler_DoubleClick(object sender, EventArgs e)
        {
            if (this.buttonEdit.Enabled) EditElement();
        }
        private void listControler_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshButton();
        }

        private void buttonClearSelection_Click(object sender, EventArgs e)
        {
            listControler.ClearAll();
        }
        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            listControler.SelectAll();
        }
        private void buttonDefault_Click(object sender, EventArgs e)
        {
            LoadDefault();
        }
        private void buttonAdvance_Click(object sender, EventArgs e)
        {
            Advance();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditElement();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            MWLQueryResultItem item = listControler.Add<MWLQueryResultItem>(this, Program.ConfigMgt.Config.Rule.QueryResult.MappingList);
            if (item == null) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }

        private void buttonEdit2_Click(object sender, EventArgs e)
        {
            MWLQueryResultItem item = listControler.Edit<MWLQueryResultItem>(this, Program.ConfigMgt.Config.Rule.QueryResult.MappingList);
            if (item == null) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            listControler.Delete<MWLQueryResultItem>(this, Program.ConfigMgt.Config.Rule.QueryResult.MappingList);

            RefreshList();
            RefreshButton();
        }

        private void buttonAddChild_Click(object sender, EventArgs e)
        {
            MWLQueryResultItem item = listControler.AddChild<MWLQueryResultItem>(this, Program.ConfigMgt.Config.Rule.QueryResult.MappingList);
            if (item == null) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }
    }
}