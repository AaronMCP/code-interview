using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.IM.UIControl;

namespace UITest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DPanelTitle pt = new DPanelTitle();
            pt.BackColor = Color.DarkGray;
            pt.Text = "Devices";

            DPanelButton btn = new DPanelButton();
            btn.Image = Properties.Resources.blueball;
            btn.Text = "Add Device";

            DPanel pnl = new DPanel(pt, new DPanelButton[] { btn });

            //---------------------

            DPanelTitle pt2 = new DPanelTitle();
            pt2.BackColor = Color.DarkGray;
            pt2.Text = "Interfaces";

            DPanelButton btn21 = new DPanelButton();
            btn21.Image = Properties.Resources.blueball;
            btn21.Text = "Install Interface";
            DPanelButton btn22 = new DPanelButton();
            btn22.Image = Properties.Resources.blueball;
            btn22.Text = "Uninstall Interface";

            DPanel pnl2 = new DPanel(pt2, new DPanelButton[] { btn21, btn22 });
            pnl2.Collapse();

            //---------------------

            DPanelContainer pc = new DPanelContainer(new DPanel[] { pnl, pnl2 });
            pc.Dock = DockStyle.Fill;

            this.panel1.Controls.Add(pc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listView1.Items[0].Group = this.listView1.Groups[0];
            this.listView1.Items[1].Group = this.listView1.Groups[1];
            this.listView1.ShowGroups = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.listView1.Items[0].Group = this.listView1.Groups[2];
            this.listView1.Items[1].Group = this.listView1.Groups[3];
            this.listView1.ShowGroups = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.listView1.ShowGroups = false;
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            
        }
    }
}