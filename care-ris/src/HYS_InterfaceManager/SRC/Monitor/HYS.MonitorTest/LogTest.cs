using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using HYS.Adapter.Base;

namespace HYS.MonitorTest
{
    public partial class LogTest : Form
    {
        public LogTest()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            //Monitor frm = new Monitor();
            //frm.Show(this);
        }

        private void btbConfig_Click(object sender, EventArgs e)
        {
            LogConfig frm = new LogConfig();
            frm.Show(this);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string head = "";
            string text = "[2007-01-12 11:26:32.117208]  [Info]  {hello}     Write Info log!";
            string pattern = @"\[\d\d\d\d-\d\d-\d\d \d\d:\d\d:\d\d\.\d\d\d\d\d\d\]  \[\w*\]  {\w*}";

            head = Regex.Match(text, pattern).Value;
            string temp = text.Substring(head.Length);
            MessageBox.Show("     Hello\r\n" + temp);


            //string filePath = GWLogData.GetDirectoryPath() + "\\" + "aaa" + "_" + DateTime.Now.ToString(GWLogData.DateFomat) + GWLogData.FilePostfix;
            //int i = 0;

            //using (StreamReader sr = File.OpenText(filePath))
            //{
            //    while (!sr.EndOfStream)
            //    {
            //        sr.ReadLine();
            //        i++;
            //    }

            //    MessageBox.Show(i.ToString());
            //}

            //string directoryPath = Application.StartupPath + "\\" + GWLogData.FileDirectory;
            //string pattern = "*" + GWLogData.FilePostfix;
            //string[] fileNameList = Directory.GetFiles(directoryPath, pattern, SearchOption.TopDirectoryOnly);

            //pattern = "(20070108|20070110).log";
            //List<string> listStr = new List<string>();

            //foreach (string str in fileNameList) {
            //    if (Regex.Match(str, pattern).Success) {
            //        listStr.Add(Path.GetFileName(str));
            //    }
            //}

            //pattern = "";
            //foreach (string fileName in listStr)
            //{
            //    pattern += fileName + "\r\n";
            //    //sb.Append(fileName + "\r\n");
            //}
            //MessageBox.Show(pattern);
        }
    }
}