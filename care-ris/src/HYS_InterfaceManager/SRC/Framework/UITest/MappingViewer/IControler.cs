using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace UITest.MappingViewer
{
    public interface IControler
    {
        Control GetControl();
        Rectangle GetListRectangle();
        Rectangle GetItemRectangle(IItem item);
        IPanel Panel { get; set; }

        IItem AddItem(IItem item);
        void RemoveItem(IItem item);
        void ClearItem();

        event ItemSelectedHandler ItemSelected;
    }

    public delegate void ItemSelectedHandler(IControler ctrl, IItem item);
}
