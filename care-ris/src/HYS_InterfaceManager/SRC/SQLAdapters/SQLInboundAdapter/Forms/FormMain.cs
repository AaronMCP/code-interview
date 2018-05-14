using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SQLInboundAdapter.Adapters;

namespace SQLInboundAdapter.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private AdapterMain Adapter = new AdapterMain();
        private void FormMain_Load(object sender, EventArgs e)
        {
            Adapter.Initialize(null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Start")
            {
                Adapter.Start(null);
                button1.Text = "Stop";
            }
            else
            {
                Adapter.Stop(null);
                button1.Text = "Start";
            }
        }

    }
}