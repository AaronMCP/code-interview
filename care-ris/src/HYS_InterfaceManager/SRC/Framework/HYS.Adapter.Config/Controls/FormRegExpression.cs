using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Config.Controls
{
    public partial class FormRegExpression : Form
    {
        public FormRegExpression()
        {
            InitializeComponent();
        }

        private void RefreshList()
        {
            this.listViewList.Items.Clear();
            XCollection<RegularExpressionItem> list = GetRegExpressions();
            if (list == null) return;

            foreach (RegularExpressionItem i in list)
            {
                ListViewItem item = new ListViewItem(i.Expression);
                item.SubItems.Add(i.Replacement);
                item.SubItems.Add(i.Description);
                item.Tag = i;
                this.listViewList.Items.Add(item);
            }
        }
        private void SaveSetting()
        {
            if (this.listViewList.SelectedItems.Count > 0)
            {
                _item = this.listViewList.SelectedItems[0].Tag as RegularExpressionItem;
            }
        }

        private RegularExpressionItem _item;
        public RegularExpressionItem RegExpression
        {
            get { return _item; }
        }

        private static XCollection<RegularExpressionItem> _list;
        public static XCollection<RegularExpressionItem> GetRegExpressions()
        {
            if (_list == null)
            {
                IMCfgMgt mgt = new IMCfgMgt();
                mgt.FileName = Application.StartupPath + "\\..\\..\\" + mgt.FileName;
                if (mgt.Load())
                {
                    _list = mgt.Config.RegularExpressions;
                    Program.Log.Write("Load IM configuration (for regular expressions) succeeded.");
                }
                else
                {
                    Program.Log.Write(LogType.Warning, "Load IM configuration (for regular expressions) failed.");
                }
            }
            return _list;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveSetting();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void FormRegExpression_Load(object sender, EventArgs e)
        {
            RefreshList();
        }
        private void listViewList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = this.listViewList.SelectedItems.Count > 0;
        }
    }
}