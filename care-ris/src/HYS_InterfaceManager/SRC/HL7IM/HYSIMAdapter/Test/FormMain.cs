using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MSG = HYS.Messaging.Objects;
using HYS.Common.Xml;
using System.Xml;

namespace HYS.MessageDevices.CSBAdapter.Test
{
    public partial class FormMain : Form
    {
        private XmlTabControlControler _ctrlHL7Msg;
        private XmlTabControlControler _ctrlCSBMsg;
        private FileComboBoxControler _ctrlSampleHL7Msg;

        public FormMain()
        {
            InitializeComponent();

            _ctrlHL7Msg = new XmlTabControlControler(this.tabControlHL7Msg,
                this.tabPageHL7MsgPlain, this.textBoxHL7Msg,
                this.tabPageHL7MsgTree, this.webBrowserHL7Msg);
            _ctrlCSBMsg = new XmlTabControlControler(this.tabControlCSBMsg,
                this.tabPageCSBMsgPlain, this.textBoxCSBMsg,
                this.tabPageCSBMsgTree, this.webBrowserCSBMsg);

            _ctrlSampleHL7Msg = new FileComboBoxControler(this.comboBoxSampleHL7Message,
                Path.Combine(Application.StartupPath, "SampleHL7Messages"), true);
            _ctrlSampleHL7Msg.ItemSelected += delegate(FileComboBoxItem item)
            {
                _ctrlHL7Msg.Open(item.FileContent);
            };
            _ctrlSampleHL7Msg.SelectTheFirstItem();
        }

        private void buttonTransform_Click(object sender, EventArgs e)
        {
            string file = Path.Combine(Application.StartupPath, this.textBoxXSLT.Text.Trim());
            XMLTransformer t = XMLTransformer.CreateFromFile(file, Program.Log);
            if (t == null)
            {
                MessageBox.Show(this, "Create XSLT transformer failed. See the log file for details.", this.Text);
                return;
            }

            string csbxml = "";
            string hl7xml = _ctrlHL7Msg.GetXmlString();
            if (t.TransformString(hl7xml, ref csbxml, XSLTExtensionType.None))
            {
                _ctrlCSBMsg.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + csbxml);
            }
            else
            {
                MessageBox.Show(this, "XSLT transform failed. See the log file for details.", this.Text);
                return;
            }
        }
        private void buttonReadPatientMsg_Click(object sender, EventArgs e)
        {
            ReadSampleMessage("SampleCSBMessages\\CSB_DATASET_PATIENT.xml");
        }
        private void buttonReadOrderMessage_Click(object sender, EventArgs e)
        {
            ReadSampleMessage("SampleCSBMessages\\CSB_DATASET_ORDER.xml");
        }
        private void buttonReadReportMessage_Click(object sender, EventArgs e)
        {
            ReadSampleMessage("SampleCSBMessages\\CSB_DATASET_REPORT.xml");
        }
        private void buttonGenDS_Click(object sender, EventArgs e)
        {
            string xml = _ctrlCSBMsg.GetXmlString();
            if (string.IsNullOrEmpty(xml)) return;

            MSG.Message msg = XObjectManager.CreateObject<MSG.Message>(xml);
            if (msg == null)
            {
                MessageBox.Show(this, "Deserialize CSB DataSet Message failed. See the log file for details.", this.Text);
                return;
            }

            try
            {
                DataSet ds = new DataSet();
                XmlReadMode m = ds.ReadXml(XmlReader.Create(new StringReader(msg.Body)));
                //MessageBox.Show(this, string.Format(
                //        "Data Set Name: {0}\r\n" +
                //        "Table Count: {1}\r\n" +
                //        "Row Count: {2}\r\n",
                //        ds.DataSetName,
                //        ds.Tables.Count,
                //        (ds.Tables.Count > 0) ? ds.Tables[0].Rows.Count : 0),
                //   m.ToString());

                if (ds.Tables.Count > 0)
                {
                    this.dataGridViewDS.DataSource = ds.Tables[0];
                    this.labelDSName.Text = ds.DataSetName + " (" + ds.Tables[0].Rows.Count + ")";
                }
                else
                {
                    this.dataGridViewDS.DataSource = null;
                    this.labelDSName.Text = ds.DataSetName + " (0)";
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
                MessageBox.Show(this, "Deserialize DataSet XML failed. See the log file for details.\r\n\r\nMessage: " + err.Message, this.Text);
            }
        }

        private void ReadSampleMessage(string filename)
        {
            try
            {
                string file = Path.Combine(Application.StartupPath, filename);
                _ctrlCSBMsg.Open(File.ReadAllText(file));
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
                MessageBox.Show(this, "Read sample CSB DataSet Message failed. See the log file for details.\r\n\r\nMessage: " + err.Message, this.Text);
            }
        }
    }
}
