using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Logging;
using HYS.Common.Xml;
using HYS.Messaging.Mapping.Transforming;

namespace HYS.MessageDevices.HL7Adapter.HL7Test
{
    public partial class FormXMLTOXML : Form
    {
        public FormXMLTOXML()
        {
            _log = new VirtualLogger(this);
            InitializeComponent();
        }

        private VirtualLogger _log;
        private class VirtualLogger : ILog
        {
            private FormXMLTOXML _form;
            public VirtualLogger(FormXMLTOXML frm)
            {
                _form = frm;
            }

            private void ShowMessageBox(string text)
            {
                MessageBox.Show(_form, text);
            }

            #region ILog Members

            public void Write(string msg)
            {
            }

            public void Write(LogType type, string msg)
            {
                if (type == LogType.Error) ShowMessageBox(msg);
            }

            public void Write(Exception err)
            {
                ShowMessageBox(err.ToString());
            }

            public void DumpToFile(string folder, string filename, XObject message)
            {
            }

            public bool DumpData
            {
                get { return false; }
            }

            #endregion
        }

        private void buttonHL7toXML_Click(object sender, EventArgs e)
        {
            string hl7Msg = this.textBoxSourceXml.Text.Trim();
            string xmlMsg = "";
            XMLTransformer t = XMLTransformer.CreateFromFile(this.textBoxXSLTPath.Text.Trim(), _log);

            if (t.TransformString(hl7Msg, ref xmlMsg, XSLTExtensionTypes.None))
            {
                this.textBoxXmlMsg.Text = xmlMsg;
            }
            else
            {
                _log.Write("Transforming fialed.");
            }
        }

        private void checkBoxHL7Msg_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxSourceXml.WordWrap = this.checkBoxHL7Msg.Checked;
        }
        private void checkBoxXmlMsg_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxXmlMsg.WordWrap = this.checkBoxXmlMsg.Checked;
        }
        private void checkBoxTransHL7Msg_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxSourceXml.WordWrap = this.checkBoxHL7Msg.Checked;
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text files (*.xsl)|*.xsl";
            dlg.FileOk += new CancelEventHandler(dlg_FileOk);
            dlg.ShowDialog(this);
        }

        void dlg_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog dlg = sender as OpenFileDialog;
            this.textBoxXSLTPath.Text = dlg.FileName;
        }
    }
}
