using System;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.XmlAdapter.Common.Objects;

namespace HYS.XmlAdapter.Common.Controlers
{
    public class MessageListViewControler<T>
        where T : XIMMessage
    {
        private ListView _listView;
        private XCollection<T> _list;

        public MessageListViewControler(ListView listView, XCollection<T> list)
        {
            _list = list;
            _listView = listView;
            _listView.DoubleClick += new EventHandler(_listView_DoubleClick);
            _listView.SelectedIndexChanged += new EventHandler(_listView_SelectedIndexChanged);
        }

        public void RefreshList()
        {
            _listView.Items.Clear();
            int index = 1;
            foreach (T m in _list)
            {
                ListViewItem item = new ListViewItem((index++).ToString());
                item.SubItems.Add(m.HL7EventType.GetKey());
                item.SubItems.Add(m.HL7EventType.Description);
                item.SubItems.Add((new GWEventTypeControler.GWEventTypeWrapper(m.GWEventType)).ToString());
                item.Tag = m;
                _listView.Items.Add(item);
            }
        }
        public T GetSelectedItem()
        {
            if (_listView.SelectedItems.Count < 1) return null;
            return _listView.SelectedItems[0].Tag as T;
        }
        public bool HasSelectedItem()
        {
            return _listView.SelectedItems.Count > 0;
        }
        public void SelectItem( T item)
        {
            foreach (ListViewItem i in _listView.Items)
            {
                if (item == i.Tag as T)
                {
                    i.Selected = true;
                    break;
                }
            }
        }
        
        public event EventHandler DoubleClick;
        public event EventHandler SelectedIndexChanged;

        private void _listView_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClick != null) DoubleClick(this, e);
        }
        private void _listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null) SelectedIndexChanged(this, e);
        }
    }
}
