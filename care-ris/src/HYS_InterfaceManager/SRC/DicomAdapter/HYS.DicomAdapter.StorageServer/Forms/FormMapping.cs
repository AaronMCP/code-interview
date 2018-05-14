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
using HYS.DicomAdapter.StorageServer.Objects;

namespace HYS.DicomAdapter.StorageServer.Forms
{
    public partial class FormMapping : Form, IConfigUI
    {
        public FormMapping()
        {
            InitializeComponent();

            listControler = new ListControler2(true, this.listViewElement, Program.ConfigMgt.Config.GWDataDBConnection, Program.Log, true);
            listControler.DoubleClick += new EventHandler(listControler_DoubleClick);
            listControler.SelectedIndexChanged += new EventHandler(listControler_SelectedIndexChanged);
        }

        private ListControler2 listControler;

        private void RefreshList()
        {
            List<IDicomMappingItem> list = new List<IDicomMappingItem>();
            foreach (StorageItem qr in Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList)
            {
                list.Add(qr);
            }
            listControler.RefreshList(list.ToArray());
        }
        private void RefreshButton()
        {
            this.buttonClearSelection.Enabled =
                this.buttonSelectAll.Enabled = listControler.GetItemCount() > 0;

            StorageItem item = listControler.GetSelectedItem() as StorageItem;
            //if (item == null ||
            //    item.DPath.VR == DVR.SQ ||
            //    item.DPath.VR == DVR.Unknown)
            //    this.buttonEdit.Enabled = false;
            //else
            //    this.buttonEdit.Enabled = true;

            bool isValid = (item != null) && (item.DPath.Type == DPathType.Normal);
            this.buttonAddChild.Enabled = isValid && (item.DPath.VR == DVR.SQ);
            this.buttonDelete.Enabled = buttonEdit2.Enabled = isValid;
        }

        private void LoadDefault()
        {
            Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList.Clear();
            StorageItem[] qrlist = StorageItem.GetDefault();
            foreach (StorageItem qr in qrlist)
            {
                Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList.Add(qr);
            }
            RefreshList();
            RefreshButton();
        }
        private void EditElement()
        {
            StorageItem item = listControler.GetSelectedItem() as StorageItem;
            if (item == null) return;

            FormElement frm = new FormElement(item, Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }

        private void FormMapping_Load(object sender, EventArgs e)
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
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditElement();
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
                return this.Text;
            }
        }

        #endregion

        #endregion

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            StorageItem item = listControler.Add<StorageItem>(this, Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList);
            if (item == null) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }

        private void buttonEdit2_Click(object sender, EventArgs e)
        {
            StorageItem item = listControler.Edit<StorageItem>(this, Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList);
            if (item == null) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            listControler.Delete<StorageItem>(this, Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList);

            RefreshList();
            RefreshButton();
        }

        private void buttonAddChild_Click(object sender, EventArgs e)
        {
            StorageItem item = listControler.AddChild<StorageItem>(this, Program.ConfigMgt.Config.StorageRule.QueryResult.MappingList);
            if (item == null) return;

            RefreshList();
            listControler.SelectItem(item);
            RefreshButton();
        }
    }
}