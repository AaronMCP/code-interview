using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace XmlTest
{
    public partial class FormCoding : Form
    {
        public FormCoding()
        {
            InitializeComponent();
        }

        private char Seperator = ' ';

        private void buttonEncode_Click(object sender, EventArgs e)
        {
            string sourceString = this.textBoxSource.Text;
            byte[] bArray = Encoding.UTF8.GetBytes(sourceString);
            //this.textBoxByte.Tag = bArray;
            
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bArray)
            {
                sb.Append(b.ToString("X"));
                sb.Append(Seperator);
            }
            this.textBoxByte.Text = sb.ToString();
        }

        private void buttonDecode_Click(object sender, EventArgs e)
        {
            //byte[] bArray = this.textBoxByte.Tag as byte[];
            
            List<byte> bList = new List<byte>();
            string byteString = this.textBoxByte.Text.Trim();
            string[] byteStringArray = byteString.Split(Seperator);
            foreach (string str in byteStringArray)
            {
                byte b = byte.Parse(str, System.Globalization.NumberStyles.AllowHexSpecifier);
                bList.Add(b);
            }

            byte[] bArray = bList.ToArray();
            string targetString = (bArray == null) ? "" : Encoding.UTF8.GetString(bArray);
            this.textBoxTarget.Text = targetString;
        }

        private void buttonCut_Click(object sender, EventArgs e)
        {
            this.textBoxByte.Text = this.textBoxByte.Text.Substring(7);
        }
    }
}