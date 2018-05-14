using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.UIControl
{
    public interface IPage
    {
        Control GetControl();
        event PageEventHandler MoveNext;
        event PageEventHandler MovePrev;
        event PageEventHandler CloseAll;
    }

    public delegate void PageEventHandler(IPage me) ;
}
