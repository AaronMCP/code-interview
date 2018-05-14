using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace HYS.FileAdapter.FileInboundAdapterConfiguration
{
    //Implentment the Interfance of ListViewSort for listview sort
    public class ListViewSort : IComparer
    {
        private int col;
        private bool descK;

        public ListViewSort()
        {
            col = 0;
        }

        public ListViewSort(int column, object Desc)
        {
            descK = (bool)Desc;
            col = column; //Current column
        }

        public int Compare(object x, object y)
        {
            int tempInt = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            if (descK) return tempInt;
            else return -tempInt;
        }
    }
}
