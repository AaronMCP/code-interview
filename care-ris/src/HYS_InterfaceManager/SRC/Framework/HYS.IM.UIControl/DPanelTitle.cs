using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace HYS.IM.UIControl
{
    public class DPanelTitle : Label
    {
        public DPanelTitle()
        {
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.TextAlign = ContentAlignment.MiddleCenter;
            RefreshTitle();
        }

        private bool _expand;
        public bool Expand
        {
            get
            {
                return _expand;
            }
            set
            {
                _expand = value;
                RefreshTitle();
            }
        }

        private void RefreshTitle()
        {
            if (Expand)
            {
                this.Image = Properties.Resources.Expend;
            }
            else
            {
                this.Image = Properties.Resources.Collapse;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            Expand = !Expand;
            base.OnClick(e);
        }
    }
}
