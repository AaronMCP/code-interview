using System;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections.Generic;

namespace HYS.SQLInboundAdapterConfiguration.Forms
{
    public class ComboxLoader
    {
        public static void LoadOleDBType(ComboBox cb)
        {
            if (cb == null) return;

            string[] list = Enum.GetNames(typeof(OleDbType));
            
            int index = 0;
            cb.Items.Clear();
            for(int i=0; i<list.Length; i++)
            {
                string str = list[i];
                if (str.ToLower() == "varchar") index = i;
                cb.Items.Add(str);
            }

            if (index >= 0 && index < cb.Items.Count)
                cb.SelectedIndex = index;
        }
    }
}
