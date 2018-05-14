using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UITest.MappingViewer
{
    public partial class MListForm : Form, IControler
    {
        public MListForm()
        {
            InitializeComponent();
        }

        #region IControler Members

        public Control GetControl()
        {
            this.TopLevel = false;
            return this;
        }

        public Rectangle GetListRectangle()
        {
            return new Rectangle(this.Location, this.Size);
        }

        public Rectangle GetItemRectangle(IItem item)
        {
            int index = listBoxMain.Items.IndexOf(item);
            Rectangle rec = listBoxMain.GetItemRectangle(index);
            if (rec.IsEmpty) return rec;

            int titleHeight = this.Height - this.ClientSize.Height;
            rec.Y += titleHeight;

            return rec;
        }

        public IItem AddItem(IItem item)
        {
            item.Controler = this;
            listBoxMain.Items.Add(item);
            return item;
        }

        public void RemoveItem(IItem item)
        {
            if (listBoxMain.Items.Contains(item))
            {
                item.Controler = null;
                listBoxMain.Items.Remove(item);
            }
        }

        public void ClearItem()
        {
            foreach (IItem item in listBoxMain.Items)
            {
                item.Controler = null;
            }
            listBoxMain.Items.Clear();
        }

        private IPanel _panel;
        public IPanel Panel
        {
            get
            {
                return _panel;
            }
            set
            {
                _panel = value;
            }
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            _panel.Redraw();
        }

        public event ItemSelectedHandler ItemSelected;

        private void listBoxMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ItemSelected == null) return;
            if (this.listBoxMain.SelectedItems.Count < 1) return;
            IItem item = this.listBoxMain.SelectedItems[0] as IItem;
            ItemSelected(this, item);
        }

        #endregion
    }
}