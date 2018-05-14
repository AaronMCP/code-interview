using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Common.Controlers;

namespace XmlTest
{
    public partial class FormList : Form
    {
        public FormList()
        {
            InitializeComponent();
            _listViewCtrl = new XmlListViewControler<XmlElementItem>(this.listViewMain, true);
        }

        private XmlListViewControler<XmlElementItem> _listViewCtrl;
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            _listViewCtrl.RefreshList(XIMHelper.GetRequestMessage<XmlElementItem>());
        }
    }
}