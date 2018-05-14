using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace HYS.FileAdapter.Common
{
    public class ComboxLoader
    {
        public static void LoadEncoding(ComboBox cb)
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
        public static void SetEncoding(ComboBox cb, string codePageName)
        {
            if (cb == null || codePageName == null || codePageName.Length < 1) return;
            EncodingInfo[] eilist = Encoding.GetEncodings();
            for (int i = 0; i < eilist.Length; i++)
            {
                if (eilist[i].Name == codePageName)
                {
                    if (i < cb.Items.Count) cb.SelectedIndex = i;
                    break;
                }
            }
        }
        public static string GetEncoding(ComboBox cb)
        {
            if (cb == null) return "";
            int index = cb.SelectedIndex;
            EncodingInfo[] eilist = Encoding.GetEncodings();
            if (index >= 0 && index < eilist.Length) return eilist[index].Name;
            return "";
        }
    }
}
