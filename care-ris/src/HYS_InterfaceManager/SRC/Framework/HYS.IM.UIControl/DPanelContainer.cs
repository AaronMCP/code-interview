using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.UIControl
{
    public class DPanelContainer : Panel
    {
        public DPanelContainer(DPanel[] panels)
        {
            _panels = panels;
            if (_panels == null) throw (new ArgumentNullException());

            InitializeControls();
        }

        private DPanel[] _panels;
        public DPanel[] Panels
        {
            get { return _panels; }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; }
        }

        private void RefreshControls()
        {
            this.SuspendLayout();

            int height = 0;
            foreach (DPanel pnl in _panels)
            {
                pnl.Top = height;
                height += pnl.Height;
                pnl.Width = this.ClientSize.Width;
                pnl.Left = 0;
            }

            bool showScrollBar = height > this.ClientSize.Height;
            this.AdjustFormScrollbars(showScrollBar);

            foreach (DPanel pnl in _panels)
            {
                pnl.Width = this.ClientSize.Width;
            }

            this.SuspendLayout();
        }
        private void InitializeControls()
        {
            //this.AutoScroll = true;
            this.Controls.AddRange(_panels);
            foreach (DPanel pnl in _panels)
            {
                pnl.Expanded += new PanelExpandHandler(Panel_Expanded);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            RefreshControls();
        }
        private void Panel_Expanded(DPanel sender, bool expand)
        {
            RefreshControls();
        }
    }
}
