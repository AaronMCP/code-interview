using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Dicom;
using HYS.DicomAdapter.MWLServer.Dicom;

namespace HYS.DicomAdapter.MWLServer.Forms
{
    public partial class FormService : Form
    {
        public FormService()
        {
            InitializeComponent();

            scpMWL = new WorkListSCPService(null);
        }

        private WorkListSCPService scpMWL;

        public void SetText(string message)
        {
            if (!this.Created) return;
            SetTextHandler dlg = new SetTextHandler(_SetText);
            this.Invoke(dlg, new object[] { message });
        }
        private void _SetText(string message)
        {
            this.textBox1.Text += message + "\r\n";
        }
        private delegate void SetTextHandler(string message);

        private void buttonMWLStart_Click(object sender, EventArgs e)
        {
            scpMWL.Start();
        }

        private void buttonMWLStop_Click(object sender, EventArgs e)
        {
            scpMWL.Stop();
        }
        
    }
}