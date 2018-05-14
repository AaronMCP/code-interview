using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UITest.XmlMapper;

namespace UITest
{
    public partial class FormXSLT : Form
    {
        public FormXSLT()
        {
            InitializeComponent();

            //_listViewControler = new TListViewControler(this.listView1);
            _listViewControler = new XListViewControler(this.listView1);

            TListViewItem root1 = new TListViewItem();
            root1.SubItems.Add("patient");

            _listViewControler.AddRoot(root1);

            TListViewItem child11 = new TListViewItem();
            child11.SubItems.Add("ID");

            _listViewControler.AddChild(root1, child11);

            TListViewItem child12 = new TListViewItem();
            child12.SubItems.Add("Name");

            _listViewControler.AddChild(root1, child12);

            TListViewItem root2 = new TListViewItem();
            root2.SubItems.Add("study");
            
            _listViewControler.AddRoot(root2);

            TListViewItem child21 = new TListViewItem();
            child21.SubItems.Add("DateTime");

            _listViewControler.AddChild(root2, child21);
        }

        private XListViewControler _listViewControler;

        private void FormXSLTIn_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonAddRoot_Click(object sender, EventArgs e)
        {
            TListViewItem root = new TListViewItem(this.textBoxValue.Text);
            _listViewControler.AddRoot(root);
            root.EnsureVisible();
        }

        private void buttonAddChild_Click(object sender, EventArgs e)
        {
            TListViewItem item = _listViewControler.SelectedItem;
            if (item != null)
            {
                TListViewItem child = new TListViewItem(this.textBoxValue.Text);
                _listViewControler.AddChild(item, child);
                if (this.checkBoxAutoExpand.Checked)
                {
                    item.Expand();
                    child.EnsureVisible();
                }
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            TListViewItem item = _listViewControler.SelectedItem;
            if (item != null) _listViewControler.Remove(item);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            _listViewControler.Clear();
        }

        private void buttonDump_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            TextBox tb = new TextBox();
            tb.Text = _listViewControler.Dump();
            tb.ScrollBars = ScrollBars.Both;
            tb.Dock = DockStyle.Fill;
            tb.WordWrap = false;
            tb.Multiline = true;
            frm.Controls.Add(tb);
            frm.ShowDialog(this);
        }

        private void buttonLoadSchema_Click(object sender, EventArgs e)
        {
            _listViewControler.Clear();
            _listViewControler.LoadXmlSchema("xIS_XML_request_message.xsd");
        }

        private void buttonLoadSchemaFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            string fileName = dlg.FileName;
            _listViewControler.Clear();
            _listViewControler.LoadXmlSchema(fileName);
        }

        private void buttonXPath_Click(object sender, EventArgs e)
        {
            XListViewItem item = _listViewControler.SelectedItem as XListViewItem;
            if (item != null) MessageBox.Show(item.GetXPath());
        }
    }
}