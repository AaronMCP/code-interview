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
using HYS.DicomAdapter.StorageServer.Objects;

namespace HYS.DicomAdapter.StorageServer.Forms
{
    public partial class FormStorage : Form, IConfigUI
    {
        public FormStorage()
        {
            InitializeComponent();
        }

        private void LoadDefault()
        {
            Program.ConfigMgt.Config.StorageServiceUIDs.Clear();
            StorageServiceUID[] uidList = StorageServiceUID.GetStorageServiceUIDs();
            foreach (StorageServiceUID uid in uidList) Program.ConfigMgt.Config.StorageServiceUIDs.Add(uid);
            RefreshList();
        }
        private void RefreshList()
        {
            this.listViewService.Items.Clear();
            foreach (StorageServiceUID uid in Program.ConfigMgt.Config.StorageServiceUIDs)
            {
                ListViewItem item = new ListViewItem(uid.Name);
                item.SubItems.Add(uid.UID);
                item.Checked = uid.Enable;
                item.Tag = uid;
                this.listViewService.Items.Add(item);
            }
        }
        private void SelectAll()
        {
            foreach (ListViewItem item in this.listViewService.Items)
            {
                item.Checked = true;
            }
        }
        private void ClearAll()
        {
            foreach (ListViewItem item in this.listViewService.Items)
            {
                item.Checked = false;
            }
        }

        private void FormStorage_Load(object sender, EventArgs e)
        {
            RefreshList();
        }
        private void buttonDefault_Click(object sender, EventArgs e)
        {
            LoadDefault();
        }
        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            SelectAll();
        }
        private void buttonClearSelection_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void listViewService_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            e.Item.ForeColor = e.Item.Checked ? Color.Blue : Color.Black;
            StorageServiceUID uid = e.Item.Tag as StorageServiceUID;
            if (uid != null) uid.Enable = e.Item.Checked;
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
                return this.Text;
            }
        }

        #endregion

        #region Edit SOP Class List

        private void Add()
        {
            FormUID frm = new FormUID(null);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            StorageServiceUID uid = frm.UID;
            if (uid == null) return;

            Program.ConfigMgt.Config.StorageServiceUIDs.Add(uid);
            
            RefreshList();
            SelectItem(uid);
        }
        private void Edit()
        {
            StorageServiceUID uid = GetSelectedItem();
            if (uid == null) return;

            FormUID frm = new FormUID(uid);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            RefreshList();
            SelectItem(uid);
        }
        private void Delete()
        {
            StorageServiceUID uid = GetSelectedItem();
            if (uid == null) return;

            Program.ConfigMgt.Config.StorageServiceUIDs.Remove(uid);

            RefreshList();
            RefreshButtons();
        }

        private void RefreshButtons()
        {
            this.buttonEdit.Enabled =
                this.buttonDelete.Enabled =
                GetSelectedItem() != null;
        }
        private StorageServiceUID GetSelectedItem()
        {
            if (this.listViewService.SelectedItems.Count < 1) return null;
            return this.listViewService.SelectedItems[0].Tag as StorageServiceUID;
        }
        private void SelectItem(StorageServiceUID uid)
        {
            foreach (ListViewItem i in this.listViewService.Items)
            {
                if (i.Tag == uid)
                {
                    i.Selected = true;
                    i.EnsureVisible();
                    break;
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void listViewService_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        #endregion
    }
}