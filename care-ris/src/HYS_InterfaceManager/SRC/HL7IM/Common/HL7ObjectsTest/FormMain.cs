using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.Common.HL7Objects.Segments;
using HYS.Common.HL7Objects.Types;
using HYS.Common.HL7Objects;

namespace HYS.Common.HL7ObjectsTest
{
    public partial class FormMain : Form
    {
        private XmlTabControlControler _rcvCtrl;

        public FormMain()
        {
            InitializeComponent();

            _rcvCtrl = new XmlTabControlControler(this.tabControlRcvMsg,
                this.tabPageRcvMsgPlain, this.textBoxRcvMsg,
                this.tabPageRcvMsgTree, this.webBrowserRcvMsg);
        }

        private class HL7 : XObject
        {
            public MSH MSH { get; set; }
        }

        private void buttonGenerateXML_Click(object sender, EventArgs e)
        {
            HL7 msg = new HL7();
            msg.MSH = new MSH()
            {
                MessageControlID = "102108",
                SendingApplication = new EI() { EntityIdentifier = "RISMALL_ADT" },
                SendingFacility = new EI() { EntityIdentifier = "MESA" },
                ReceivingApplication = new EI() { EntityIdentifier = "RIS_CSH_GC" },
                ReceivingFacility = new EI() { EntityIdentifier = "CSH" },
                MessageType = new MSG()
                {
                    MessageCode = "ADT",
                    TriggerEvent = "A01",
                    MessageStructure = "ADT_A01"
                },
                ProcessingID = new PT() { ProcessingID = "P" },
                VersionID = new VID() { VersionID = "2.3.1" },
            };

            string str = msg.ToXMLString(HL7ObjectsHelper.XmlNamespace);
            _rcvCtrl.Open("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + str);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = _rcvCtrl.GetXmlString();
            HL7 msg = XObjectManager.CreateObject<HL7>(str);
            MessageBox.Show(this, msg.ToXMLString());
        }
    }
}
