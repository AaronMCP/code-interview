using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HYS.SocketAdapter.SocketInboundAdapterConfiguration.Forms
{
    public partial class FQuerySQL : Form
    {
        FChannel parentForm;

        public FQuerySQL(FChannel frm)
        {
            InitializeComponent();
            parentForm = frm;
            this.txtSQL.Text = frm.SQLString;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            parentForm.SQLString = this.txtSQL.Text;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}