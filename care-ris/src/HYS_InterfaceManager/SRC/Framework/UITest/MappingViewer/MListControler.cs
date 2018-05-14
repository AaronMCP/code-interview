using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UITest.MappingViewer
{
    public class MListControler : IControler
    {
        private IPanel _panel;
        public IPanel Panel
        {
            get { return _panel; }
            set { _panel = value; }
        }
        
        private ListBox _listBox;
        private List<IItem> _listItems;

        public MListControler(ListBox lb)
        {
            _listBox = lb;
            _listItems = new List<IItem>();
        }
        public Control GetControl()
        {
            return null;
        }

        public IItem[] Items
        {
            get { return _listItems.ToArray(); }
        }
        public Rectangle GetListRectangle()
        {
            return new Rectangle(_listBox.Location, _listBox.Size);
        }
        public Rectangle GetItemRectangle(IItem item)
        {
            int index = _listItems.IndexOf(item);
            return _listBox.GetItemRectangle(index);
        }

        public IItem AddItem(IItem item)
        {
            item.Controler = this;
            _listItems.Add(item);
            return item;
        }
        public void RemoveItem(IItem item)
        {
            if (_listItems.Contains(item))
            {
                item.Controler = null;
                _listItems.Remove(item);
            }
        }
        public void ClearItem()
        {
            foreach (IItem item in _listItems)
            {
                item.Controler = null;
            }
            _listItems.Clear();
        }

        public void RefreshList()
        {
            _listBox.Items.Clear();
            foreach (IItem item in _listItems)
            {
                _listBox.Items.Add(item);
            }
        }

        public event ItemSelectedHandler ItemSelected;
    }
}
