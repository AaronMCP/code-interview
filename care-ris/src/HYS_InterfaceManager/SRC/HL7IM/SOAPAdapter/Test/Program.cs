using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HYS.MessageDevices.SOAPAdapter.Test.Forms;
using System.Web;

namespace HYS.MessageDevices.SOAPAdapter.Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //MessageBox.Show(HttpUtility.HtmlEncode("<a b=\"aasdf\">asdfadf</a>"));
            //MessageBox.Show(HttpUtility.HtmlDecode(HttpUtility.HtmlEncode("<a b=\"aasdf\">asdfadf</a>")));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        static void OtherTesting()
        {
            switch (MessageBox.Show(
                    "Run SOAP Client test, please click \"Yes\". \r\n\r\n" +
                    "Run SOAP Server test, please click \"No\". \r\n\r\n" +
                    "Run XSLT test, please click \"Cancel\".", "SOAP Test",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button3))
            {
                case DialogResult.Yes:
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormSOAPClient());
                    break;
                case DialogResult.No:
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormSOAPServer());
                    break;
                default:
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormXSLT());
                    break;
            }
        }
    }
}
