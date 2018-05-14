using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.SQLOutboundAdapterConfiguration.Controls;

namespace HYS.SQLOutboundAdapterConfiguration.Forms
{
    public partial class ShowConnection : Form
    {
        ActiveMode configForm;

        public ShowConnection(ActiveMode frm)
        {
            InitializeComponent();
            this.configForm = frm;
        }

        public void TextContent(string content)
        {
            this.txtConnection.Text = content;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            configForm.ConnectionConfig(this.txtConnection.Text);
            this.Close(); ;
        }
    }
}