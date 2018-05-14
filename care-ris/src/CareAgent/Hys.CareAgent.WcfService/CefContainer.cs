using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hys.CareAgent.WcfService
{
    public partial class CefContainer : Form
    {
        private readonly ChromiumWebBrowser browser;

        public CefContainer(string url)
        {
            InitializeComponent();

            browser = new ChromiumWebBrowser(url)
            {
                Dock = DockStyle.Fill
            };
            browser.TitleChanged += browser_TitleChanged;
            browserPanel.Controls.Add(browser);
        }

        void browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.Text = e.Title;
            }));
        }

        public new void Dispose()
        {
            if (!browser.IsDisposed)
            {
                browser.Dispose();
            }

            base.Dispose();
        }

        private void CefContainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!browser.IsDisposed)
            {
                browser.Dispose();
            }
        }
    }
}
