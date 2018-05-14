using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Common.HL7v2.Xml;
using HYS.Common.Logging;
using HYS.Common.Xml;

namespace HYS.MessageDevices.HL7Adapter.HL7Test
{
    public partial class FormHL7V2XML : Form
    {
        public FormHL7V2XML()
        {
            _log = new VirtualLogger(this);
            InitializeComponent();
            LoadTypes();
        }
        private void LoadTypes()
        {
            this.comboBoxType.Items.Clear();
            foreach (string s in XmlTransformerFactory.TransformerRegistry)
            {
                this.comboBoxType.Items.Add(s);
            }
            if (this.comboBoxType.Items.Count > 0) this.comboBoxType.SelectedIndex = 0;
        }

        private VirtualLogger _log;
        private class VirtualLogger : ILog
        {
            private FormHL7V2XML _form;
            public VirtualLogger(FormHL7V2XML frm)
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
                if(type == LogType.Error ) ShowMessageBox(msg);
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
            if (this.comboBoxType.SelectedItem == null) return;
            string type = this.comboBoxType.SelectedItem.ToString();

            int times = (int)this.numericUpDownTimes.Value;
            string hl7Msg = this.textBoxHL7Msg.Text.Trim();
            string xmlMsg = "";

            XmlTransformerBase t = XmlTransformerFactory.CreateTransformer(type, _log);

            bool res = false;
            List<TimeSpan> tsList = new List<TimeSpan>();
            for (int i = 0; i < times; i++)
            {
                DateTime dtBegin = DateTime.Now;
                res = t.TransformHL7v2ToXml(hl7Msg, out xmlMsg);
                DateTime dtEnd = DateTime.Now;
                if (!res) break;
                tsList.Add(dtEnd.Subtract(dtBegin));
            }

            if (res)
            {
                double totalTime = 0;
                foreach (TimeSpan ts in tsList) totalTime += ts.TotalMilliseconds;
                this.labelPerformHL7toXML.Text = ((double)(totalTime / (double)tsList.Count)).ToString() + "ms";
            }
            else
            {
                this.labelPerformHL7toXML.Text = "error";
            }

            this.textBoxXmlMsg.Text = xmlMsg;
        }
        private void buttonXMLtoHL7_Click(object sender, EventArgs e)
        {
            if (this.comboBoxType.SelectedItem == null) return;
            string type = this.comboBoxType.SelectedItem.ToString();

            int times = (int)this.numericUpDownTimes.Value;
            string xmlMsg = this.textBoxXmlMsg.Text.Trim();
            string hl7Msg = "";

            XmlTransformerBase t = XmlTransformerFactory.CreateTransformer(type, _log);

            bool res = false;
            List<TimeSpan> tsList = new List<TimeSpan>();
            for (int i = 0; i < times; i++)
            {
                DateTime dtBegin = DateTime.Now;
                res = t.TransformXmlToHL7v2(xmlMsg, out hl7Msg);
                DateTime dtEnd = DateTime.Now;
                if (!res) break;
                tsList.Add(dtEnd.Subtract(dtBegin));
            }

            if (res)
            {
                double totalTime = 0;
                foreach (TimeSpan ts in tsList) totalTime += ts.TotalMilliseconds;
                this.labelPerformXMLtoHL7.Text = ((double)(totalTime / (double)tsList.Count)).ToString() + "ms";
            }
            else
            {
                this.labelPerformXMLtoHL7.Text = "error";
            }

            this.textBoxTransHL7Msg.Text = hl7Msg;
        }

        private void checkBoxHL7Msg_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxHL7Msg.WordWrap = this.checkBoxHL7Msg.Checked;
        }
        private void checkBoxXmlMsg_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxXmlMsg.WordWrap = this.checkBoxXmlMsg.Checked;
        }
        private void checkBoxTransHL7Msg_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxTransHL7Msg.WordWrap = this.checkBoxTransHL7Msg.Checked;
        }
    }
}
