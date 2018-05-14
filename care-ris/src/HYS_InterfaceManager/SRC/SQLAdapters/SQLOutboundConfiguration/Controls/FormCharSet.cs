using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYS.SQLOutboundAdapterConfiguration.Controls
{
    public partial class FormCharSet : Form
    {
        public FormCharSet()
        {
            InitializeComponent();
            LoadEncoding(this.listBoxCharSet);
        }

        private static void LoadEncoding(ListBox cb)
        {
            if (cb == null) return;

            int index = 0;
            cb.Items.Clear();
            int codePage = Encoding.Default.CodePage;
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                int i = cb.Items.Add(ei.DisplayName + " (" + ei.CodePage.ToString() + ")");
                if (codePage == ei.CodePage) index = i;
            }

            if (index >= 0 && index < cb.Items.Count) cb.SelectedIndex = index;
        }
        private static void SetEncoding(ListBox cb, string codePageNumber)
        {
            if (cb == null || codePageNumber == null || codePageNumber.Length < 1) return;
            EncodingInfo[] eilist = Encoding.GetEncodings();
            for (int i = 0; i < eilist.Length; i++)
            {
                //if (eilist[i].Name == codePageName)
                if (eilist[i].CodePage.ToString() == codePageNumber)
                {
                    if (i < cb.Items.Count) cb.SelectedIndex = i;
                    break;
                }
            }
        }
        private static string GetEncoding(ListBox cb)
        {
            if (cb == null) return "";
            int index = cb.SelectedIndex;
            EncodingInfo[] eilist = Encoding.GetEncodings();
            if (index >= 0 && index < eilist.Length) return eilist[index].CodePage.ToString();
            return "";
        }

        private void CopyToClipBoardAndClose()
        {
            string codePageNumber = GetEncoding(this.listBoxCharSet);
            Clipboard.SetText(codePageNumber);
            this.Close();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            CopyToClipBoardAndClose();
        }
        private void listBoxCharSet_DoubleClick(object sender, EventArgs e)
        {
            CopyToClipBoardAndClose();
        }
    }
}
