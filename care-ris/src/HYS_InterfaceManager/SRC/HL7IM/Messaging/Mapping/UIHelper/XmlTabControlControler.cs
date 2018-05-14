using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HYS.IM.Messaging.Mapping.Transforming.UIHelper
{
    /// <summary>
    /// Put a tab control with two tab pages on your WinForm GUI, one tab page contains a text box, the other tab page contains a web browser control.
    /// This class will help you manage these UI controls to display XML text.
    /// </summary>
    public class XmlTabControlControler
    {
        private TabControl _tabCtrl;
        private TabPage _tabPageTree;
        private TabPage _tabPagePlain;
        private WebBrowser _webBrwsTree;
        private TextBox _txtBoxPlain;

        private void _tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_tabCtrl.SelectedTab == _tabPageTree)
            {
                string str = _txtBoxPlain.Text.Trim();
                if (str == null || str.Length < 1) return;

                //_webBrwsTree.DocumentText = str;
                string tmpFile = Path.GetTempFileName();
                using (StreamWriter sw = File.CreateText(tmpFile))
                {
                    sw.Write(str);
                }
                _webBrwsTree.Tag = tmpFile;
                _webBrwsTree.Navigate(tmpFile);
            }
        }
        private void _webBrwsTree_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            WebBrowser wb = sender as WebBrowser;
            if (wb == null) return;
            string tmpFile = wb.Tag as string;
            if (tmpFile == null) return;
            File.Delete(tmpFile);
        }

        public XmlTabControlControler(TabControl tabCtrl, TabPage tabPagePlain, TextBox txtBoxPlain, TabPage tabPageTree, WebBrowser webBrwsTree)
        {
            _tabCtrl = tabCtrl;
            _tabPageTree = tabPageTree;
            _tabPagePlain = tabPagePlain;
            _webBrwsTree = webBrwsTree;
            _txtBoxPlain = txtBoxPlain;

            if (_tabCtrl == null) throw new ArgumentNullException();
            if (_tabPageTree == null) throw new ArgumentNullException();
            if (_tabPagePlain == null) throw new ArgumentNullException();
            if (_webBrwsTree == null) throw new ArgumentNullException();
            if (_txtBoxPlain == null) throw new ArgumentNullException();

            _tabCtrl.SelectedIndexChanged += new EventHandler(_tabCtrl_SelectedIndexChanged);
            _webBrwsTree.Navigated += new WebBrowserNavigatedEventHandler(_webBrwsTree_Navigated);
        }

        public void Open(string xmlString)
        {
            string str = (xmlString == null) ? "" : xmlString;
            if (_tabCtrl.SelectedTab == _tabPagePlain)
            {
                _txtBoxPlain.Text = str;
            }
            else if (_tabCtrl.SelectedTab == _tabPageTree)
            {
                _txtBoxPlain.Text = str;
                _tabCtrl_SelectedIndexChanged(_tabCtrl, EventArgs.Empty);
            }
        }

        public string GetXmlString()
        {
            return _txtBoxPlain.Text;
        }
    }
}
