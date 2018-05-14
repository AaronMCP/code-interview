using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace UITest.XmlMapper
{
    public class TListViewControler
    {
        private ListView _listView;
        private List<TListViewItem> _items;

        private void DumpItem(StringBuilder sb, TListViewItem item)
        {
            for (int i = 0; i < item._level; i++) sb.Append("  ");
            foreach (ListViewItem.ListViewSubItem sitem in item.SubItems)
            {
                sb.Append(sitem.Text).Append(" ");
            }
            sb.Append("level(").Append(item._level.ToString()).Append(") ");
            sb.Append("visible(").Append(item._visible.ToString()).Append(") ");
            sb.Append("expanded(").Append(item._expanded.ToString()).Append(") ");
            sb.Append("parent(").Append(item._parent).Append(") ");
            sb.Append("children(").Append(item._children.Count).Append(") ");
            sb.Append("controler(").Append(item._controler).Append(") ");
            sb.AppendLine();

            if (item.HasChildren())
            {
                foreach (TListViewItem citem in item.Children)
                {
                    DumpItem(sb, citem);
                }
            }
        }
        internal virtual string Dump()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Items (").Append(_items.Count.ToString()).AppendLine(")");
            sb.AppendLine("-----------------------");
            foreach (TListViewItem item in RootItemList)
            {
                DumpItem(sb, item);
            }
            sb.AppendLine("-----------------------");
            return sb.ToString();
        }

        public TListViewItem SelectedItem
        {
            get
            {
                if (_listView.SelectedItems.Count < 1) return null;
                return _listView.SelectedItems[0] as TListViewItem;
            }
        }
        public TListViewItem[] RootItemList
        {
            get
            {
                List<TListViewItem> tlist = new List<TListViewItem>();
                foreach (TListViewItem t in _items)
                {
                    if (t.IsRoot()) tlist.Add(t);
                }
                return tlist.ToArray();
            }
        }

        public TListViewControler(ListView lv)
        {
            _items = new List<TListViewItem>();

            _listView = lv;
            _listView.View = View.Details;
            _listView.MultiSelect = false;
            _listView.FullRowSelect = true;
            _listView.HideSelection = false;
            _listView.DoubleClick += new EventHandler(_listView_DoubleClick);
        }

        private void _listView_DoubleClick(object sender, EventArgs e)
        {
            if(_listView.SelectedItems.Count < 1 ) return;
            TListViewItem item = _listView.SelectedItems[0] as TListViewItem;
            if (item == null) return;

            if (item.IsExpanded())
            {
                item.Collaps();
            }
            else
            {
                item.Expand();
            }
        }

        public virtual void AddRoot(TListViewItem newItem)
        {
            if (newItem == null || _items.Contains(newItem)) return;

            newItem._parent = null;
            newItem._controler = this;
            newItem._visible = true;

            _listView.Items.Add(newItem);
            _items.Add(newItem);

            newItem.Refresh();
        }
        public virtual void AddChild(TListViewItem parentItem, TListViewItem newItem)
        {
            if (parentItem == newItem) return;
            if (parentItem == null || !_items.Contains(parentItem)) return;
            if (newItem == null || _items.Contains(newItem) || parentItem._children.Contains(newItem)) return;

            newItem._level = parentItem._level + 1;
            newItem._parent = parentItem;
            newItem._controler = this;

            parentItem._children.Add(newItem);
            _items.Add(newItem);

            parentItem.Refresh();
            if (parentItem.IsExpanded())
            {
                parentItem.Collaps();
                parentItem.Expand();
            }
        }
        public virtual void Remove(TListViewItem currentItem)
        {
            if (currentItem == null || !_items.Contains(currentItem)) return;

            bool visible = currentItem._visible;
            currentItem._visible = false;

            if (currentItem.HasChildren())
            {
                currentItem.Collaps();
                foreach (TListViewItem child in currentItem.Children)
                {
                    Remove(child);
                }
            }

            TListViewItem parent = currentItem._parent;
            if (parent != null )
            {
                parent._children.Remove(currentItem);
                if (parent._children.Count < 1) parent.Refresh();
            }

            if (visible)
            {
                _listView.Items.Remove(currentItem);
            }

            _items.Remove(currentItem);
        }
        public virtual void Clear()
        {
            _items.Clear();
            _listView.Items.Clear();
        }
    }
}
