using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public class CodePageHelper
    {
        public static void LoadEncoding(ComboBox cb)
        {
            if (cb == null) return;

            int index = 0;
            cb.Items.Clear();
            int codePage = System.Text.Encoding.Default.CodePage;
            foreach (EncodingInfo ei in System.Text.Encoding.GetEncodings())
            {
                int i = cb.Items.Add("(" + ei.CodePage.ToString() + ") " + ei.DisplayName);
                if (codePage == ei.CodePage) index = i;
            }

            if (index >= 0 && index < cb.Items.Count) cb.SelectedIndex = index;
        }
        public static void SetEncoding(ComboBox cb, string codePageName)
        {
            if (cb == null || codePageName == null || codePageName.Length < 1) return;
            EncodingInfo[] eilist = System.Text.Encoding.GetEncodings();
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
            EncodingInfo[] eilist = System.Text.Encoding.GetEncodings();
            if (index >= 0 && index < eilist.Length) return eilist[index].Name;
            return "";
        }
    }
}
