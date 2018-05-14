using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace XmlTest
{
    public class IEControler
    {
        private bool _xml;
        public bool XML
        {
            get { return _xml; }
            set { _xml = value; }
        }

        private string _tmpFile;
        private Control _target;
        private Control _container;
        private CheckBox _checkBox;
        private WebBrowser _webBrowser;

        public IEControler(Control container, Control target, CheckBox checkBox, bool xml)
        {
            _xml = xml;
            _target = target;
            _checkBox = checkBox;
            _container = container;

            Refresh();

            if (_target != null) _target.TextChanged += new EventHandler(_target_TextChanged);
            if (_checkBox != null) _checkBox.CheckedChanged += new EventHandler(_checkBox_CheckedChanged);
        }
        public IEControler(Control container, Control target, CheckBox checkBox)
            : this(container, target, checkBox, false)
        {
        }

        private void _webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (_tmpFile != null) File.Delete(_tmpFile);
        }
        private void _checkBox_CheckedChanged(object sender, EventArgs e)
        {
            Refresh();
        }
        private void _target_TextChanged(object sender, EventArgs e)
        {
            RefreshText();
        }

        public void RefreshText()
        {
            if (_target == null ||
                _webBrowser == null) return;

            if (!(_target.Created && _target.IsHandleCreated &&
                _webBrowser.Created && _webBrowser.IsHandleCreated)) return;

            if (_webBrowser.Visible)
            {
                if (_xml)
                {
                    string tmpFile = Path.GetTempFileName();
                    File.Delete(tmpFile);
                    _tmpFile = tmpFile + ".xml";
                    using (StreamWriter sw = File.CreateText(_tmpFile))
                    {
                        sw.Write(_target.Text);
                    }
                    _webBrowser.Navigate(_tmpFile);                    
                }
                else
                {
                    _webBrowser.DocumentText = _target.Text;
                }
            }
        }
        public void Refresh()
        {
            if (_target == null ||
                _container == null ||
                _checkBox == null) return;

            if (!(_target.Created && _target.IsHandleCreated &&
                _checkBox.Created && _checkBox.IsHandleCreated &&
                _container.Created && _container.IsHandleCreated)) return;

            if (_checkBox.Checked)
            {
                if (_webBrowser == null)
                {
                    _webBrowser = new WebBrowser();
                    _webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(_webBrowser_DocumentCompleted);
                }
                _webBrowser.Location = _target.Location;
                _webBrowser.Size = _target.Size;
                _webBrowser.Anchor = _target.Anchor;
                _container.Controls.Add(_webBrowser);
                _webBrowser.Visible = true;
                _target.Visible = false;
                RefreshText();
            }
            else
            {
                if (_container.Controls.Contains(_webBrowser))
                {
                    _container.Controls.Remove(_webBrowser);
                    _webBrowser.Visible = false;
                    _target.Visible = true;
                }
            }
        }
    }
}
