using System;
using System.Windows.Forms;
using CSH.eHeath.HL7Gateway.Manager;
using HYS.Common.Objects.License2;

namespace HYS.HL7IM.Manager.Forms
{
    public partial class FormLicense : Form
    {
        public FormLicense()
        {
            InitializeComponent();
            listControler = new DeviceLicenseListControler(this.listViewMain, this.labelTime);
        }

        private DeviceLicenseListControler listControler;

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormLicense_Load(object sender, EventArgs e)
        {
            listControler.RefreshList(Program.License);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Program.License = listControler.Reset(Program.LogWapper);
        }
    }
}