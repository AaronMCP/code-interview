using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using HYS.XmlAdapter.Common.Files;

namespace XmlTest
{
    public partial class FormDirectory : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GetFileTime(int hFile,
            ref System.Runtime.InteropServices.ComTypes.FILETIME lpCreationTime,
            ref System.Runtime.InteropServices.ComTypes.FILETIME lpLastAccessTime,
            ref System.Runtime.InteropServices.ComTypes.FILETIME lpLastWriteTime);

        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public int lpSecurityDescriptor;
            public int bInheritHandle;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int CreateFileA(string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            ref SECURITY_ATTRIBUTES lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            uint hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int CloseHandle(int hObject);

        private string GetFileTimeString(string fileName)
        {
            int hFile = 0;
            try
            {
                SECURITY_ATTRIBUTES a = new SECURITY_ATTRIBUTES();
                hFile = CreateFileA(fileName, (uint)0x80000000, 0x1 | 0x2, ref a, 0x3, 0, 0);
                if (hFile != 0)
                {
                    System.Runtime.InteropServices.ComTypes.FILETIME ftCreate = new System.Runtime.InteropServices.ComTypes.FILETIME();
                    System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccess = new System.Runtime.InteropServices.ComTypes.FILETIME();
                    System.Runtime.InteropServices.ComTypes.FILETIME ftLastWrite = new System.Runtime.InteropServices.ComTypes.FILETIME();
                    int ret = GetFileTime(hFile, ref ftCreate, ref ftLastAccess, ref ftLastWrite);
                    return ftCreate.dwHighDateTime.ToString() + "_" + ftCreate.dwLowDateTime.ToString();
                }
                else
                {
                    return fileName;
                }
            }
            catch (Exception err)
            {
                return fileName + "_" + err.ToString();
            }
            finally
            {
                if (hFile != 0) CloseHandle(hFile);
            }
        }

        public FormDirectory()
        {
            InitializeComponent();

            DirectoryMonitorConfig cfg = new DirectoryMonitorConfig();
            _monitor = new DirectoryMonitor(cfg, Program.Log);
            _monitor.OnRequest += new HYS.XmlAdapter.Common.RequestEventHandler(_monitor_OnRequest);
        }

        private bool ProcessFile(string fileName)
        {
            this.listBoxFiles.Items.Add(fileName);
            return this.checkBoxProcess.Checked;
        }

        private bool _process = false;
        private DirectoryMonitor _monitor;
        private bool _monitor_OnRequest(string receiveData, ref string sendData)
        {
            return _process;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            this.listBoxFiles.Items.Clear();
            if (!Directory.Exists(this.textBoxPath.Text)) return;
            string[] filelist = Directory.GetFiles(this.textBoxPath.Text, "*.xml");

            SortedList<string, string> sortedList = new SortedList<string, string>();
            foreach (string file in filelist)
            {
                // Win32API GetFileTime() is still not acurate enough
                //string key = GetFileTimeString(file) + file.ToString();
                string key = file;
                sortedList.Add(key, file);
            }

            foreach (KeyValuePair<string, string> pair in sortedList)
            {
                this.listBoxFiles.Items.Add(pair.Value);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.textBoxPath.Text)) Directory.CreateDirectory(this.textBoxPath.Text);
            for (int i = 0; i < 100; i++)
            {
                using (StreamWriter sw = File.CreateText(this.textBoxPath.Text + "\\" + i.ToString("00") + "_" + (DateTime.Now.Ticks + i).ToString() + ".xml")) { }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.listBoxFiles.Items.Clear();
            if (!Directory.Exists(this.textBoxPath.Text)) return;
            string[] filelist = Directory.GetFiles(this.textBoxPath.Text, "*.xml");
            foreach (string file in filelist) File.Delete(file);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            _process = this.checkBoxProcess.Checked;
            _monitor.Config.SourcePath = this.textBoxPath.Text;
            _monitor.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _monitor.Stop();
        }
    }
}