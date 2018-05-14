using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace UITest.XmlMapper
{
    public class TListViewItem : ListViewItem
    {
        public TListViewItem()
        {
            _children = new List<TListViewItem>();
            Refresh();
        }
        public TListViewItem(params string[] valueList) : this()
        {
            foreach (string val in valueList)
            {
                this.SubItems.Add(val);
            }
            Refresh();
        }

        internal int _level;
        internal bool _visible;
        internal bool _expanded;
        internal TListViewItem _parent;
        internal TListViewControler _controler;
        internal List<TListViewItem> _children;

        public bool IsRoot()
        {
            return _parent == null;
        }
        public bool IsVisible()
        {
            return _visible;
        }
        public bool IsExpanded()
        {
            return _expanded;
        }
        public bool HasChildren()
        {
            return _children.Count > 0;
        }
        public TListViewItem[] Children
        {
            get { return _children.ToArray(); }
        }
        
        public virtual void Expand()
        {
            if (IsExpanded()) return;
            if (!HasChildren()) return;

            int index = ListView.Items.IndexOf(this);
            if (index < 0) return;

            foreach (TListViewItem child in Children)
            {
                ListView.Items.Insert(++index, child);
                child._visible = true;
                child.Refresh();
            }

            _expanded = true;
            Refresh();
        }
        public virtual void Collaps()
        {
            if (!IsExpanded()) return;
            if (!HasChildren()) return;

            foreach (TListViewItem child in Children)
            {
                child.Collaps();
                child._visible = false;
                if (ListView.Items.Contains(child)) ListView.Items.Remove(child);
            }

            _expanded = false;
            Refresh();
        }
        public virtual void Refresh()
        {
            string signal = "";
            if (HasChildren())
            {
                if (IsExpanded())
                {
                    signal = "--";
                }
                else
                {
                    signal = "+ ";
                }
            }
            else
            {
                signal = "";
            }
            this.Text = signal;

            if (this.SubItems.Count > 1)
            {
                string str = this.SubItems[1].Text.Trim();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < this._level; i++) sb.Append("  ");
                this.SubItems[1].Text = sb.ToString() + str;

                //string str = this.SubItems[1].Text.Trim().TrimStart('+', '-', ' ');
                //StringBuilder sb = new StringBuilder();
                //for (int i = 0; i < this._level; i++) sb.Append("    ");
                //this.SubItems[1].Text = sb.ToString() + signal + " " + str;
            }

            int num = 255 - this._level * 10;
            if (num < 50) num = 50;
            this.BackColor = Color.FromArgb(num, num, num);
        }
    }
}
