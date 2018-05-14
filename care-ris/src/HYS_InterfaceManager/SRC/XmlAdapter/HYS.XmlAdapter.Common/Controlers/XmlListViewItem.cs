using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Objects.Rule;
using HYS.XmlAdapter.Common.Objects;

namespace HYS.XmlAdapter.Common.Controlers
{
    public class XmlListViewItem<T> : ListViewItem
        where T : IXmlElementItem, new()
    {
        private XmlListViewControler<T> _control;
        public XmlListViewItem(T item, XmlListViewControler<T> ctl)
        {
            _item = item;
            _control = ctl;
            Initialize();
            RefreshExpand(false);
        }

        private void RefreshExpand(bool expand)
        {
            if (!CanHasChild) return;
            if (expand)
            {
                this.Text = "--";
            }
            else
            {
                this.Text = "+";
            }
        }
        private void Initialize()
        {
            bool isInbound = _control._isInbound;

            if (isInbound)
            {
                _valid = _item.GWDataDBField.Table != GWDataDBTable.None || CanHasChild;
            }
            else
            {
                _valid = CanHasChild ||
                        (!(_item.Translating.Type != TranslatingType.FixValue &&
                        _item.GWDataDBField.Table == GWDataDBTable.None));
            }
            
            this.Checked = _item.Enable && Valid;

            if (_item.Translating.Type == TranslatingType.FixValue && isInbound)
            {
                this.SubItems.Add("");
            }
            else
            {
                this.SubItems.Add(_item.Element.GetLastName());
            }

            bool hasChild = CanHasChild;
            if (hasChild)
            {
                this.SubItems.Add("");
            }
            else
            {
                this.SubItems.Add(_item.Element.Type.ToString());
            }
            if (_item.GWDataDBField.Table == GWDataDBTable.None ||
                ((!isInbound)&&_item.Translating.Type == TranslatingType.FixValue))
            {
                this.SubItems.Add("");
            }
            else
            {
                this.SubItems.Add(_item.GWDataDBField.ToString());
            }
            if (hasChild)
            {
                this.SubItems.Add("");
            }
            else
            {
                this.SubItems.Add(_item.Translating.ToString());
            }
            if (isInbound && _item.RedundancyFlag)
            {
                this.SubItems.Add("True");
            }

            if (!Valid)
            {
                this.ForeColor = Color.Gray;
            }
        }

        private const int _luminanceStep = 10;
        private static int GetNewLuminance(int value)
        {
            int newValue = value - _luminanceStep;
            if (newValue < _luminanceStep) newValue = _luminanceStep;
            return newValue;
        }
        private void DropChild(XmlListViewItem<T> item)
        {
            if (item == null) return;
            foreach (XmlListViewItem<T> c in item.Children)
            {
                DropChild(c);
                c.ListView.Items.Remove(c);
                c.Item.Enable = false;
            }
            item.Children.Clear();
        }
        private void DropChild()
        {
            DropChild(this);
        }

        private void LoadChild(List<T> list, bool newChild)
        {
            if (list == null) return;

            int dataindex = _control._list.IndexOf(_item);
            int viewindex = this.ListView.Items.IndexOf(this);
            Color clr = Color.FromArgb(GetNewLuminance(this.BackColor.R),
                GetNewLuminance(this.BackColor.G), GetNewLuminance(this.BackColor.B));
            
            foreach (T i in list)
            {
                if (newChild) _control._list.Insert(++dataindex, i);
                XmlListViewItem<T> node = new XmlListViewItem<T>(i, _control);
                node.BackColor = clr;
                Children.Add(node);

                if (_control._isLoading)
                {
                    _control._indexCounter++;
                    this.ListView.Items.Insert(_control._indexCounter, node);
                }
                else
                {
                    this.ListView.Items.Insert(++viewindex, node);
                }
            }
        }
        private void LoadChild()
        {
            List<T> list = new List<T>();

            if (CanHasChild)
            {
                int index = _control._list.IndexOf(_item) + 1;
                for (; index < _control._list.Count; index++)
                {
                    T item = _control._list[index];
                    if (item.Element.IsChildOf(_item.Element)) list.Add(item);
                }
            }

            if (list.Count > 0)
            {
                LoadChild(list, false);
            }
            else
            {
                list = XIMHelper.GetSubItem<T>(Item);
                LoadChild(list, true);
            }
        }

        public bool CanHasChild
        {
            get
            {
                return XIMHelper.IsComplex(_item.Element.Type);
            }
        }
        public readonly List<XmlListViewItem<T>> Children = new List<XmlListViewItem<T>>();

        private T _item;
        public T Item
        {
            get { return _item; }
            set { _item = value; }
        }

        private bool _valid;
        public bool Valid
        {
            get { return _valid; }
        }

        public void Expand()
        {
            if (!CanHasChild) return;
            RefreshExpand(true);
            LoadChild();
        }
        public void Collaps()
        {
            if (!CanHasChild) return;
            RefreshExpand(false);
            DropChild();
        }
        public void Refresh()
        {
            this.SubItems.Clear();
            Initialize();
        }
    }
}
