using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace HYS.IM.Controler
{
    public class ListViewControler
    {
        private ListView _listView;
        private ListViewItemComparerHelper _sortHelper;

        private int currentColumn = -1;
        private bool dontknowwhy = true;
        public ListViewControler( ListView lstView )
        {
            _listView = lstView;
            if (_listView == null) throw new ArgumentNullException();
            _sortHelper = new ListViewItemComparerHelper(_listView);
        }
        public void GroupBy(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= _listView.Columns.Count) return;
            currentColumn = columnIndex;

            Hashtable gList = new Hashtable();            
            foreach (ListViewItem item in _listView.Items)
            {
                string text = item.SubItems[columnIndex].Text;
                if (gList.ContainsKey(text))
                {
                    ArrayList list = gList[text] as ArrayList;
                    if (list != null) list.Add(item);
                }
                else
                {
                    ArrayList list = new ArrayList();
                    gList[text] = list;
                    list.Add(item);
                }
            }

            _listView.Groups.Clear();
            foreach (DictionaryEntry de in gList)
            {
                ListViewGroup g = new ListViewGroup(de.Key as string, HorizontalAlignment.Left);
                _listView.Groups.Add(g);

                ArrayList ilist = de.Value as ArrayList;
                if (ilist == null) return;

                foreach (ListViewItem i in ilist)
                {
                    i.Group = g;
                }
            }

            _listView.ShowGroups = true;

            if (dontknowwhy)
            {
                dontknowwhy = false;
                GroupBy(columnIndex);
            }
        }
        public void HideGroup()
        {
            _listView.ShowGroups = false;
            currentColumn = -1;
            dontknowwhy = true;
        }
        public void Refresh()
        {
            GroupBy(currentColumn);
        }
    }
}
