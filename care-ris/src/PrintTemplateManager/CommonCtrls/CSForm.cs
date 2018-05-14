using System;
using System.Collections.Generic;
using System.Text;
using Telerik.WinControls.UI;
using System.Windows.Forms;

namespace Hys.CommonControls
{
    public class CSForm : RadForm
    {
        private bool _isExclusive = false;

        public bool IsExclusive
        {
            get { return _isExclusive; }
            set { _isExclusive = value; }
        }

        public override string ThemeClassName
        {
            get { return "Telerik.WinControls.UI.RadForm"; }
        }

        const int WM_NCLBUTTONDOWN = 0x00A1;
        const int HTCAPTION = 2;
        protected override void WndProc(ref Message m)
        {
            if (_isExclusive && m.Msg == WM_NCLBUTTONDOWN && m.WParam.ToInt32() == HTCAPTION)
                return; 

            base.WndProc(ref m);
        }
    }
}
