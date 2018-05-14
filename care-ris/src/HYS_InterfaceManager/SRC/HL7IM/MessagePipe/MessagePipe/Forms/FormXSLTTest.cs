using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace HYS.MessageDevices.MessagePipe.Processors.XSLT
{
    public partial class FormXSLTTest : Form
    {
        public FormXSLTTest()
        {
            InitializeComponent();
        }

        private void btnTransform_Click(object sender, EventArgs e)
        {
            if (rtBoxOrig.Text.Trim().Equals(""))
            {
                return;
            }
            string XSLTFile = textBox1.Text;

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(XSLTFile);

            try
            {
                string xmlString;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(rtBoxOrig.Text.Trim());
                //XPathDocument doc = new XPathDocument(tr);
                Stream tmpS = new MemoryStream();
                XmlWriter xw = new XmlTextWriter(tmpS, null);
                XPathNavigator nav = doc.CreateNavigator();

                
                XmlReader xr = XmlReader.Create(new StringReader(rtBoxOrig.Text));
                try
                {
                    xslt.Transform(xr, xw);
                    xw.Flush();
                    tmpS.Position = 0;

                    StreamReader sr = new StreamReader(tmpS);

                    xmlString = sr.ReadToEnd();
                    sr.Close();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    xw.Close();
                    tmpS.Close();
                }
                rtBoxOutput.Clear();
                rtBoxOutput.Text = xmlString;
            }
            catch (Exception ee)
            {
                Program.Log.Write(ee);
            }
        }
    }
}
