using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace HYS.HL7IM.Manager.Controler
{
    public class ControlerBase
    {
        protected Form frmMain;
        protected ControlerBase(Form main)
        {
            frmMain = main;
            if (frmMain == null) throw new ArgumentNullException();
        }
        public virtual void Refresh()
        {
        }

        protected StatusStrip statusBar;
        public void AttachStatusStrip(StatusStrip ss)
        {
            statusBar = ss;
        }
        protected void SetStatus(string str)
        {
            if (statusBar == null||statusBar.Items.Count < 1) return;
            statusBar.Items[0].Text = str;
        }
        protected void ClearStatus()
        {
            if (statusBar == null || statusBar.Items.Count < 1) return;
            statusBar.Items[0].Text = "Ready";
        }
        protected void SetInfor(string str)
        {
            if (statusBar == null || statusBar.Items.Count < 2) return;
            statusBar.Items[1].Text = str;
        }
        protected void ClearInfor()
        {
            if (statusBar == null || statusBar.Items.Count < 2) return;
            statusBar.Items[1].Text = "";
        }
    }
}
