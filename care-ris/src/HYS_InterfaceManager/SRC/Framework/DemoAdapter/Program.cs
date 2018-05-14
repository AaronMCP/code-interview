using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DemoAdapter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Regex regA = new Regex(@"[^\w\.@-]");
            Regex regN = new Regex(@"[^\d]");
            //[^\x00-\xff]*  

            MessageBox.Show(('b' > 'A').ToString());
            //MessageBox.Show(regA.Replace("aaaaa000-2323", "").ToString());
            MessageBox.Show(
                Regex.Replace(Regex.Replace(@"00    aa aaa÷ÌÕ∑0.00-23:_<>?2/\;3{}[]""''fff!~`3@#$%^&%^&*()-+=|", @"[^\w]", ""),@"[^\x00-\xff]",""));
            //MessageBox.Show(Regex.Replace(@"00aaaaa÷ÌÕ∑0.00-23:_<>?2/\;3{}[]""''fff!~`3@#$%^&%^&*()-+=|", @"[^\x00-\xff]", ""));
            return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}