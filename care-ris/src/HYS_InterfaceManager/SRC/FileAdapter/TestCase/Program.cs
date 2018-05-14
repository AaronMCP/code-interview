using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.FileAdapter.Common;

namespace TestCase
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            IniFile2 inifile = new IniFile2("MacOrder4.ini","BIG5");
            System.Diagnostics.Debug.WriteLine(inifile.ReadValue("Patient", "PatientID"));
            System.Diagnostics.Debug.WriteLine(inifile.ReadValue("Patient", "PatientName"));
            System.Diagnostics.Debug.WriteLine(inifile.ReadValue("Order", "OBR_ID"));
            System.Diagnostics.Debug.WriteLine(inifile.ReadValue("Order", "AccNo"));
            inifile.Path = "MacOrder4_GB.ini";
            //inifile.Encoder = Encoding.UTF8;
            inifile.Encoder = Encoding.GetEncoding("GB18030");
            inifile.WriteValue("Patient", "PatientID", "111111");
            //inifile.WriteValue("Patient", "PatientName", "Bill^Gates");
            inifile.WriteValue("Order", "OBR_ID", "2323");
            inifile.WriteValue("Order", "AccesionNumber", "asdfasdfa,sf");
            return;

            //string[] slist = "aaa[bbb]ccc\r\n[ddd]\r\n".Split(new char[] { '[', ']' });

            Dictionary<string, string> list = new Dictionary<string, string>();
            list["a"] = "b";
            list["a"] = "C";
            MessageBox.Show(list["a"]);
            //MessageBox.Show(list["b"]);

            string s = "";
            using (System.IO.FileStream fs = new FileStream("MacOrder5_utf8.txt", FileMode.Open))
            {
                StreamReader sr = new StreamReader(fs);
                s = sr.ReadToEnd();
                sr.Close();
            }

            System.Diagnostics.Debug.Write(s);

            s = "";
            using (FileStream fs = File.OpenRead("MacOrder4.txt"))
            {
                byte[] b = new byte[1024];
                //UTF8Encoding t = new UTF8Encoding(true);
                Encoding t = Encoding.GetEncoding(950);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    s += t.GetString(b);
                }
            }

            System.Diagnostics.Debug.Write(s);

            IniFile f = new IniFile(Application.StartupPath + "\\MacOrder4.txt");
            s = f.ReadValue("Patient", "PatientID", "");
            System.Diagnostics.Debug.WriteLine(s);
            s = f.ReadValue("Patient", "PatientName", "");
            System.Diagnostics.Debug.WriteLine(s);

            return;


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}