using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.UIControl
{
    public class DPanelButton : Button 
    {
        public DPanelButton()
        {
            this.TextAlign = ContentAlignment.BottomCenter;
            this.FlatAppearance.BorderSize = 0;
            this.FlatStyle = FlatStyle.Flat;
        }
    }
}
