using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.License2;

namespace HYS.IM.Forms
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
            Program.License = listControler.Reset(Program.Log);
        }
    }
}