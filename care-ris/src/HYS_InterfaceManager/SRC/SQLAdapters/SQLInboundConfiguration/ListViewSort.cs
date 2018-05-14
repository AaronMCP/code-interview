using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace HYS.SQLInboundAdapterConfiguration
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
            int result = 0;
            if (col != 0)
            {
                result = String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                if (descK) return result;
                else return -result;
            }
            else {
                int lvX = 0;
                int lvY = 0;

                try
                {
                    lvX = Int32.Parse(((ListViewItem)x).SubItems[col].Text);
                    lvY = Int32.Parse(((ListViewItem)y).SubItems[col].Text);
                }
                catch (Exception e){
                    string exStr = e.Message;
                    return 0;
                }

                if (lvX > lvY)
                {
                    result = 1;
                }
                else if (lvX < lvY)
                {
                    result = -1;
                }
                else {
                    result =  0;
                }

                if (descK)
                {
                    return result;
                }
                else {
                    return -result;
                }
            }
        }
    }
}
