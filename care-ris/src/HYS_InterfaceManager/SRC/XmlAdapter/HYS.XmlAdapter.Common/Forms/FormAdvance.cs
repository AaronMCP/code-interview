using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.Rule;
using HYS.XmlAdapter.Common.Objects;

namespace HYS.XmlAdapter.Common.Forms
{
    public partial class FormAdvance : Form
    {
        private XIMOutboundMessage _message;
        public XIMOutboundMessage Message
        {
            get { return _message; }
        }

        public FormAdvance(XIMOutboundMessage message)
        {
            InitializeComponent();
            _message = message;
        }

        private void Add()
        {
            FormQCItem frm = new FormQCItem(null, _message.QueryCriterias);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            QueryCriteriaItem item = frm.QCItem;
            if (item == null) return;

            _message.QueryCriterias.Add(item);
            
            RefreshList();
            SelectItem(item);
        }
        private void Edit()
        {
            QueryCriteriaItem item = GetSelectedItem();
            if (item == null) return;

            FormQCItem frm = new FormQCItem(item, _message.QueryCriterias);
            if (frm.ShowDialog(this) != DialogResult.OK) return;

            RefreshList();
            SelectItem(item);
        }
        private void Delete()
        {
            QueryCriteriaItem item = GetSelectedItem();
            if (item == null) return;

            _message.QueryCriterias.Remove(item);

            RefreshList();
            RefreshButtons();
        }
        
        private void RefreshList()
        {
            this.listViewQCList.Items.Clear();
            foreach (QueryCriteriaItem item in _message.QueryCriterias)
            {
                ListViewItem i = new ListViewItem(item.GWDataDBField.GetFullFieldName());
                i.SubItems.Add(item.Operator.ToString());
                i.SubItems.Add(item.Translating.ConstValue);
                i.SubItems.Add(item.Type.ToString());
                i.Tag = item;
                this.listViewQCList.Items.Add(i);
            }
        }
        private void RefreshButtons()
        {
            QueryCriteriaItem item = GetSelectedItem();
            this.buttonEdit.Enabled = this.buttonDelete.Enabled = item != null;
        }
        
        private QueryCriteriaItem GetSelectedItem()
        {
            if (this.listViewQCList.SelectedItems.Count < 1) return null;
            return this.listViewQCList.SelectedItems[0].Tag as QueryCriteriaItem;
        }
        private void SelectItem(QueryCriteriaItem item)
        {
            foreach (ListViewItem i in this.listViewQCList.Items)
            {
                if (item == i.Tag as QueryCriteriaItem)
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
        private void listViewQCList_DoubleClick(object sender, EventArgs e)
        {
            Edit();
        }
        private void listViewQCList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshButtons();
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FormAdvance_Load(object sender, EventArgs e)
        {
            RefreshList();
            RefreshButtons();
        }
    }
}