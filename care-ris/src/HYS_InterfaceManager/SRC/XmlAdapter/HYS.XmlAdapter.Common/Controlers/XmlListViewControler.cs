using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.XmlAdapter.Common.Objects;

namespace HYS.XmlAdapter.Common.Controlers
{
    public class XmlListViewControler<T>
        where T : IXmlElementItem, new()
    {
        private ListView _listView;

        private static Color ActiveColor = Color.Blue;
        private static Color DefaultColor = Color.Black;
        
        private void _listView_DoubleClick(object sender, EventArgs e)
        {
            if (_listView.SelectedItems.Count < 1) return;
            XmlListViewItem<T> item = _listView.SelectedItems[0] as XmlListViewItem<T>;
            if (item.Valid) return;

            if (DoubleClick != null) DoubleClick(this, e);
        }
        private void _listView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            XmlListViewItem<T> node = _listView.Items[e.Index] as XmlListViewItem<T>;
            if (node == null) return;

            switch (e.NewValue)
            {
                case CheckState.Checked:
                    {
                        node.Expand();
                        if (node.Valid)
                        {
                            node.Item.Enable = true;
                            node.ForeColor = ActiveColor;
                        }
                        else
                        {
                            e.NewValue = CheckState.Unchecked;
                        }
                        break;
                    }
                case CheckState.Unchecked:
                    {
                        node.Collaps();
                        node.Item.Enable = false;
                        node.ForeColor = DefaultColor;
                        break;
                    }
            }
        }
        private void _listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null) SelectedIndexChanged(this, e);
        }

        public XmlListViewControler(ListView listView, bool isInbound)
        {
            _listView = listView;
            _isInbound = isInbound;
            _listView.DoubleClick += new EventHandler(_listView_DoubleClick);
            _listView.ItemCheck += new ItemCheckEventHandler(_listView_ItemCheck);
            _listView.SelectedIndexChanged += new EventHandler(_listView_SelectedIndexChanged);
        }

        internal List<T> _list;
        internal bool _isLoading;
        internal int _indexCounter;
        internal bool _isInbound;

        public T GetSelectedItem()
        {
            if (_listView.SelectedItems.Count < 1) return default(T);
            XmlListViewItem<T> item = _listView.SelectedItems[0] as XmlListViewItem<T>;
            return item.Item;
        }
        public bool HasSelectedItem()
        {
            T t = GetSelectedItem();
            return (t != null && !XIMHelper.IsComplex(t.Element.Type));
        }
        public void SelectItem(T item)
        {
            foreach (XmlListViewItem<T> i in _listView.Items)
            {
                if (i.Item.Equals(item))
                {
                    i.Selected = true;
                    i.EnsureVisible();
                    Color clrB = i.BackColor;
                    //Color clrF = i.ForeColor;
                    i.Refresh();
                    i.BackColor = clrB;
                    //i.ForeColor = clrF;
                    i.ForeColor = item.Enable ? ActiveColor : DefaultColor;
                    break;
                }
            }
        }
        public void RefreshList(List<T> list)
        {
            _list = list;

            _listView.Items.Clear();
            if (list == null) return;

            List<T> rootList = new List<T>();

            foreach (T i in list)
            {
                if (i.Element.IsRoot()) rootList.Add(i);
            }

            _isLoading = true;
            _indexCounter = 0;
            foreach (T i in rootList)
            {
                XmlListViewItem<T> item = _listView.Items.Add(new XmlListViewItem<T>(i, this)) as XmlListViewItem<T>;
                _indexCounter++;
            }
            _isLoading = false;
        }
        public List<T> GetCurrentList()
        {
            return _list;
        }

        public event EventHandler DoubleClick;
        public event EventHandler SelectedIndexChanged;
    }
}
