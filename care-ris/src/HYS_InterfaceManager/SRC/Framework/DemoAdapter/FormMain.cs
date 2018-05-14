using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DemoAdapter.Controlers;
using DemoAdapter.Configuration;
using HYS.Adapter.Base;

namespace DemoAdapter
{
    public partial class FormMain : Form, IMessageFilter 
    {
        public FormMain()
        {
            InitializeComponent();
            controler = new DemoControler();
        }

        private DemoControler controler;

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            FormConfig frm = new FormConfig();
            frm.ShowDialog(this);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            controler.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            controler.Stop();
        }

        private void buttonCallme_Click(object sender, EventArgs e)
        {
            string caption = "DemoAdapter of GC Gateway";
            //string caption = "HYS Interface Engine Manager";
            ////string className = "WindowsForms10.Window.8.app.0.3b95145";
            //int hwnd = Win32Api.FindWindow(null, caption);
            ////int hwnd = Win32Api.FindWindow(className, null);
            
            //// use caption after all...

            //IntPtr h = this.Handle;
            //IntPtr i = (IntPtr)hwnd;
            //MessageBox.Show(i.ToString()+" " + h.ToString());

            //Win32Api.PostMessage(i, MsgID, 1, 2);
            ////Win32Api.SendMessage(i, MsgID, (IntPtr)1, "2");

            AdapterMessage am = new AdapterMessage(123, AdapterStatus.Stopped);
            am.PostMessage(caption);
        }

        //private int MsgID = 0xFFFF;

        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
            //if (m.Msg != MsgID) return false;

            //this.Text = m.LParam.ToString();
            
            //MessageBox.Show(m.HWnd.ToString() + "\r\n" +
            //    m.Msg.ToString() + "\r\n" +
            //    m.WParam.ToString() + "\r\n" +
            //    m.LParam.ToString() + "\r\n");

            AdapterMessage am = AdapterMessage.FromMessage(m);
            if (am == null) return false;

            MessageBox.Show(am.ToString());
            return true;
        }

        #endregion

        private void FormMain_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }
    }
}