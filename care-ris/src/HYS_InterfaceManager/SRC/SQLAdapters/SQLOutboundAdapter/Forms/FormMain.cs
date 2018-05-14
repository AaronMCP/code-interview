using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SQLOutboundAdapter.Adapters;

namespace SQLOutboundAdapter.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            adpter.Initialize(null);
        }

        AdapterMain adpter = new AdapterMain();
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                adpter.Start(null);
                button1.Text = "Stop";
            }
            else
            {
                adpter.Stop(null);
                button1.Text = "Start";
            }
        }
    }
}