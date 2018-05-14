using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Common.HL7v2.Xml.Transforming;
using System.IO;

namespace HYS.Common.HL7v2Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmBoxHL7File.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(".\\HL7Message");
            FileInfo[] HL7list = dir.GetFiles();
            foreach (FileInfo file in HL7list)
            {
                cmBoxHL7File.Items.Add(file.Name);
            }

            cmBoxHL7File.Text = cmBoxHL7File.Items[0].ToString();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            
            rtBoxHL7.Clear();
            string msgstr = File.ReadAllText(".\\HL7Message\\"+cmBoxHL7File.Text);
            rtBoxHL7.Text = msgstr;
            if (rtBoxHL7.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please input HL7 message");
                //eturn;
            }

            DateTime start = DateTime.Now;
            string str ="";
            try
            {
                HL7Message msg = new HL7Message(msgstr, 0);
                str  = msg.getXML();
            }catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            DateTime end = DateTime.Now;

            label2.Text = (end - start).ToString();
            /*
             * Stream stream = new MemoryStream();
            msg.outputXML(stream);

            StreamReader sr = new StreamReader(stream);
            stream.Position = 0;
            StringBuilder sb = new StringBuilder();
            sb.Append(sr.ReadToEnd());

            sr.Close();
             */

            rtBoxXML.Clear();
            rtBoxXML.Text = str;
        }

        private void cmBoxHL7File_SelectedIndexChanged(object sender, EventArgs e)
        {
            string msgstr = File.ReadAllText(".\\HL7Message\\" + cmBoxHL7File.Text);
            rtBoxHL7.Text = msgstr;
        }

        private void btnConvertHL7_Click(object sender, EventArgs e)
        {
            if (rtBoxXML.Text.Trim().Equals(""))
            {
                return;
            }
            //MemoryStream ms = new MemoryStream();
            string str ="";
            DateTime start = DateTime.Now;
            try
            {
                HL7Message msg = new HL7Message(rtBoxXML.Text, 1);
                 str= msg.get();
            }catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

            DateTime end = DateTime.Now;

            label3.Text = (end - start).ToString();

            

            rtBoxHL72.Clear();
            rtBoxHL72.Text = str;
           
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (rtBoxHL7.Text.Trim().Equals(rtBoxHL72.Text.Trim()))
            {
                MessageBox.Show("Original Message is same as Output Message!");
            }
        }
    }
}
