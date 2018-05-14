using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Common.Dicom;
using HYS.Common.Objects.Rule;

namespace UITest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MessageBox.Show(RuleControl.ParseConstValue(""));
            MessageBox.Show(RuleControl.ParseConstValue("asdfaf"));
            MessageBox.Show(RuleControl.ParseConstValue("a''sdf'af"));
            MessageBox.Show(RuleControl.ParseConstValue("{SQ"));
            MessageBox.Show(RuleControl.ParseConstValue("{SQLasdfasdfaf"));
            MessageBox.Show(RuleControl.ParseConstValue("SQL}asdfasdfaf"));
            MessageBox.Show(RuleControl.ParseConstValue("{SQL}asdfasdfaf"));
            MessageBox.Show(RuleControl.ParseConstValue("{SQL}asdfa'af"));
            MessageBox.Show(RuleControl.ParseConstValue(@"\{SQL}asdfasdfaf"));
            MessageBox.Show(RuleControl.ParseConstValue(@"\\{SQL}asdfasdfaf"));
            MessageBox.Show(RuleControl.ParseConstValue(@"\{SQL}"));
            MessageBox.Show(RuleControl.ParseConstValue(@"{SQL}"));


            //Form frm = new Form();
            //TextBox b = new TextBox();
            //b.Multiline = true;
            //b.Dock = DockStyle.Fill;
            //frm.Controls.Add(b);
            //frm.ShowDialog();

            //MessageBox.Show(string.Format(b.Text, 1, 2));
            //return;

            //MessageBox.Show("wait a minute.");

            //DLogMgt.OnError += new EventHandler(DLogMgt_OnError);
            //DLogMgt.OnLog += new DLogHandler(DLogMgt_OnLog);

            //DElement e1 = new DElement(0x0010, 0x0010, DVR.PN, "Bill^Gates");
            //MessageBox.Show(e1.Value);

            //DElement e2 = new DElement(0x0007, 0x0007, DVR.LO, "Hello world ÷ÌÕ∑");
            //MessageBox.Show(e2.Value);

            ////DHelper.CharacterSetName = "ISO 2022 IR 13";
            ////DHelper.CharacterSetName = "ISO 2022 IR 87";
            //DElementList eleList = DElementList.OpenFile(@"D:\Standard\DICOM\DicomFile_Japan\SCSH31.dcm");
            ////DElementList eleList = DElementList.OpenFile(@"D:\Standard\DICOM\DicomFile_Japan\SCSH32.dcm");
            ////DElementList eleList = DElementList.OpenFile(@"D:\Standard\DICOM\DicomFile\001.dcm");
            //DElement eleCS = eleList.GetElement(0x00080005);
            //if (eleCS != null)
            //{
            //    MessageBox.Show(eleCS.Value);
            //    //eleCS.Value = DHelper.CharacterSetName;
            //    //MessageBox.Show(eleCS.Value);
            //}
            //DElement elePN = eleList.GetElement(0x00100010);
            //MessageBox.Show(elePN.Value);

            //using (StreamWriter sw = File.CreateText("File.txt"))
            //{
            //    sw.Write(eleList.ToXMLString());
            //}

            //return;

            //MessageBox.Show(((int)' ').ToString());
            //return;

            XmlDocument doc = new XmlDocument();
            doc.Load(@"../../XMLBlank\test.xml");
            XmlNode nodeR = doc.GetElementsByTagName("r")[0];
            XmlNode nodeA = nodeR.ChildNodes[0];
            XmlNode nodeB = nodeR.ChildNodes[1];
            XmlNode nodeC = nodeR.ChildNodes[2];
            XmlNode nodeD = nodeR.ChildNodes[3];

            MessageBox.Show("a:" + nodeA.InnerText + ";\r\n"
            + "b:" + nodeB.InnerText + ";\r\n"
            + "c:" + nodeC.InnerText + ";\r\n"
            + "d:" + nodeD.InnerText + ";\r\n");

            XPathDocument myXPathDoc = new XPathDocument(@"../../XMLBlank\test.xml");
            using (XmlTextWriter myWriter = new XmlTextWriter(@"../../XMLBlank\result" + DateTime.Now.Ticks + ".xml", null))
            {
                XslCompiledTransform myXslTrans = new XslCompiledTransform();
                myXslTrans.Load(@"../../XMLBlank\transfrom.xsl");
                myXslTrans.Transform(myXPathDoc, null, myWriter);
            }

            //return;
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        static void DLogMgt_OnLog(DLogType type, object sender, string message)
        {
            StringBuilder sb = new StringBuilder();
            switch (type)
            {
                default:
                case DLogType.Debug:
                    {
                        sb.Append("[KDT] ");
                        break;
                    }
                case DLogType.Error:
                    {
                        sb.Append("[KDT_ERROR] ");
                        if (sender == null)
                        {
                            sb.Append("[NULL] ");
                        }
                        else
                        {
                            sb.Append(sender.ToString() + " ");
                        }
                        break;
                    }
                case DLogType.Warning:
                    {
                        sb.Append("[KDT_WARNING] ");
                        break;
                    }
            }
            if (message == null)
            {
                sb.Append("[NULL]");
            }
            else
            {
                sb.Append(message);
            }
            MessageBox.Show(sb.ToString());
        }

        static void DLogMgt_OnError(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[KDT_EXCEPTION] ");
            if (sender == null)
            {
                sb.Append("[NULL]");
            }
            else
            {
                sb.Append(sender.ToString());
            }
            Exception err = DLogMgt.LastError;
            if (err == null)
            {
                sb.Append(" [NULL]");
            }
            else
            {
                sb.Append("\r\n");
                sb.Append(err.ToString());
            }
            MessageBox.Show(sb.ToString());
        }
    }
}