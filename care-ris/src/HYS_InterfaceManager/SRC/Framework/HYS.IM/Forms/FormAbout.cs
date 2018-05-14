using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HYS.IM.Forms
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            //this.labelVersion.Text = "Version: " + Application.ProductVersion;
            //this.labelVersion.Text = "Version: 2.0.00.B15.P20080402b";
            this.labelVersion.Text = "Version: 1.0.0.0";
        }
    }
}