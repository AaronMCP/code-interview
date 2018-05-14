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
using HYS.DicomAdapter.MWLServer.Objects;

namespace HYS.DicomAdapter.MWLServer.Forms
{
    public partial class FormQueryCriteria : Form, IConfigUI
    {
        public FormQueryCriteria()
        {
            InitializeComponent();

            listControler = new QCListControler(this.listViewElement);
            listControler.DoubleClick += new EventHandler(listControler_DoubleClick);
            listControler.SelectedIndexChanged += new EventHandler(listControler_SelectedIndexChanged);
        }

        #region IConfigUI Members

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
                return "C-FIND-RQ Mapping";
            }
        }

        #endregion

        #endregion

        private QCListControler listControler;

        private void RefreshList()
        {
            List<IDicomMappingItem> list = new List<IDicomMappingItem>();
            foreach (MWLQueryCriteriaItem qr in Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList)
            {
                list.Add(qr);
            }
            listControler.RefreshList(list.ToArray());
        }
        private void RefreshButton()
        {
            this.buttonClearSelection.Enabled =
                this.buttonSelectAll.Enabled = listControler.GetItemCount() > 0;

            MWLQueryCriteriaItem item = listControler.GetSelectedItem() as MWLQueryCriteriaItem;
            //if (item == null ||
            //    item.DPath.VR == DVR.SQ ||
            //    item.DPath.VR == DVR.Unknown)
            //{
            //    this.buttonEdit.Enabled = false;
            //}
            //else
            //{
            //    if (item.DPath.GetTag() == DicomMappingHelper.Tags.ScheduledProcedureStepStartTime)
            //    {
            //        this.buttonEdit.Enabled = false;
            //    }
            //    else
            //    {
            //        this.buttonEdit.Enabled = true;
            //    }
            //}

            this.buttonAdd.Enabled = true;
            bool isValid = (item != null) && (item.DPath.Type == DPathType.Normal);
            this.buttonAddChild.Enabled = isValid && (item.DPath.VR == DVR.SQ);
            this.buttonDelete.Enabled = buttonEdit2.Enabled = isValid;

            if (isValid)
            {
                if (item.DPath.GetTag() == DicomMappingHelper.Tags.ScheduledProcedureStepStartTime)
                {
                    this.buttonAdd.Enabled = this.buttonDelete.Enabled = this.buttonEdit2.Enabled = false;
                }
                else if (item.DPath.GetTag() == DicomMappingHelper.Tags.ScheduledProcedureStepStartDate)
                {
                    this.buttonEdit2.Enabled = true;
                    this.buttonDelete.Enabled = false;
                }
                else if (item.DPath.GetTag() == DicomMappingHelper.Tags.ScheduledProcedureStepSequence)
                {
                    this.buttonEdit2.Enabled = this.buttonDelete.Enabled = false;
                }
            }
        }

        private void LoadDefault()
        {
            Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList.Clear();
            MWLQueryCriteriaItem[] qrlist = MWLQueryCriteriaItem.GetDefault();
            foreach (MWLQueryCriteriaItem qr in qrlist)
            {
                Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList.Add(qr);
            }
            RefreshList();
            RefreshButton();
        }
        private void EditElement()
        {
            MWLQueryCriteriaItem item = listControler.GetSelectedItem() as MWLQueryCriteriaItem;
            if (item == null ||
                item.DPath.GetTag() == DicomMappingHelper.Tags.ScheduledProcedureStepStartTime) return;

            FormQCElement frm = new FormQCElement(item);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }
        private void Advance()
        {
            FormQCAdvance frm = new FormQCAdvance();
            frm.ShowDialog(this);
        }

        private void FormQueryCriteria_Load(object sender, EventArgs e)
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
            MWLQueryCriteriaItem item = listControler.Add<MWLQueryCriteriaItem>(this, Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList);
            if (item == null) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }

        private void buttonEdit2_Click(object sender, EventArgs e)
        {
            MWLQueryCriteriaItem item = listControler.Edit<MWLQueryCriteriaItem>(this, Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList);
            if (item == null) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            listControler.Delete<MWLQueryCriteriaItem>(this, Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList);

            RefreshList();
            RefreshButton();
        }

        private void buttonAddChild_Click(object sender, EventArgs e)
        {
            MWLQueryCriteriaItem item = listControler.AddChild<MWLQueryCriteriaItem>(this, Program.ConfigMgt.Config.Rule.QueryCriteria.MappingList);
            if (item == null) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }
    }
}