using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Objects.License2;
using HYS.Common.Objects.Logging;

namespace AdapterTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonReadDog_Click(object sender, EventArgs e)
        {
            ReadDog();
            if (lic == null)
            {
                MessageBox.Show("failed");
            }
            else
            {
                this.propertyGridDog.SelectedObject = lic.Header;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            buttonReadDog_Click(null, null);
        }

        private class LogHanlder : ILogging
        {
            private string ID;
            public LogHanlder()
            {
                ID = DateTime.Now.Ticks.ToString();
            }

            #region ILogging Members

            public void Write(Exception err)
            {
                Write(LogType.Error, err.ToString());
            }

            public void Write(string msg)
            {
                Write(LogType.Debug, msg);
            }

            public void Write(LogType type, string msg)
            {
                using (StreamWriter sw = File.AppendText(Application.StartupPath + "\\doglog.txt"))
                {
                    sw.WriteLine( "(" + ID + ") [" + type.ToString() + "] " + msg);
                }
            }

            #endregion
        }

        private GWLicense lic;
        private void ReadDog()
        {
            LogHanlder log = new LogHanlder();
            GWLicenseAgent a = new GWLicenseAgent();
            lic = a.LoginGetLicenseLogout(this, log);
            if (lic == null)
            {
                log.Write("license: NULL");
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (DeviceLicense l in lic.Devices)
                {
                    byte[] blist = l.GetValue();
                    sb.AppendLine(l.Name + "(" + blist[0].ToString() + "," + blist[1].ToString() + ") ");
                }
                log.Write("license: " + sb.ToString());
            }
        }
    }
}