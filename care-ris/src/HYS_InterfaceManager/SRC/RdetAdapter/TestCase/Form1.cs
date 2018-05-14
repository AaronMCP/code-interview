using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace TestCase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] buf = System.Text.Encoding.Unicode.GetBytes(textBox1.Text);

            string sUnicode = System.Text.Encoding.Unicode.GetString(buf);
            string sdefault = System.Text.Encoding.Default.GetString(buf);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            ht.Add("name1", "value1");
            ht.Add("name2", "value2");
            string s = "";
            foreach (string name in ht.Keys)
            {
                s = s + name + "=" + ht[name] + "\r\n";
            }
            MessageBox.Show(s);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FTestConnection frm = new FTestConnection();
            frm.ShowDialog();
        }

        private void btToString_Click(object sender, EventArgs e)
        {
            this.tbResult.Text = dateTimePicker1.Value.ToString(this.tbFormat.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string s = "ABCD";
            if(this.tbTestEqule.Text.Trim().ToLower()==s.ToLower())
               MessageBox.Show(tbTestEqule.Text+"=="+s);
            else
               MessageBox.Show(tbTestEqule.Text + "!=" + s);

        }
    }
}