using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using HYS.FileAdapter.Configuration;
using HYS.FileAdapter.Common;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Adapter.Base;
using Windows.Lib;

namespace TestCase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            IniFile f = new IniFile(Application.StartupPath+"\\test.ini");
            f.WriteValue("sect", "key", "vlue123");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cbFTTAR.Items.Clear();

            Type t = typeof(FileInGeneralParams.InFileTreatTypeAfterRead);
            FieldInfo[] fis = t.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo fi in fis)
            {
                this.cbFTTAR.Items.Add(fi.Name.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FindData fd = new FindData();
            fd.fileAttributes = Convert.ToInt32(FileAttributes.Archive);
            IntPtr handle = Kernel32.FindFirstFile(this.textBox1.Text, fd);
            while (handle != null)
            {
                StringBuilder sbAttr = new StringBuilder();
                //Type t = typeof(FileAttributes);
                //FieldInfo[] fis = t.GetFields(BindingFlags.Public | BindingFlags.Static);
                //foreach( FieldInfo fi in fis)
                //    if( fi.

                if ((fd.fileAttributes & (int)FileAttributes.Archive) == (int)FileAttributes.Archive)
                    sbAttr.Append("Archive/");
                if ((fd.fileAttributes & (int)FileAttributes.Compressed) == (int)FileAttributes.Compressed)
                    sbAttr.Append("Compressed/");
                if ((fd.fileAttributes & (int)FileAttributes.Device) == (int)FileAttributes.Device)
                    sbAttr.Append("Device/");
                if ((fd.fileAttributes & (int)FileAttributes.Directory) == (int)FileAttributes.Directory)
                    sbAttr.Append("Directory/");
                if ((fd.fileAttributes & (int)FileAttributes.Encrypted) == (int)FileAttributes.Encrypted)
                    sbAttr.Append("Encrypted/");
                if ((fd.fileAttributes & (int)FileAttributes.Encrypted) == (int)FileAttributes.Encrypted)
                    sbAttr.Append("Encrypted/");
                


                this.listBox1.Items.Add(fd.fileName+":"+sbAttr.ToString());
                if (Kernel32.FindNextFile(handle, fd) == 0)
                    break;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            string[] files = StarFile.FindFiles(this.textBox1.Text, "");
            foreach (string f in files)
                this.listBox1.Items.Add(f);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            string[] files = StarFile.FindFolders(this.textBox1.Text, "");
            foreach (string f in files)
                this.listBox1.Items.Add(f);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.textBox2.Text))
                Directory.CreateDirectory(this.textBox2.Text);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string[] files = new string[]{
                "f1.txt",
                "f2.txt",
                "f5.txt",
                "f4.txt",
                "f3.txt"
            };
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < files.Length; i++)
                sb.Append(files[i]+",");
            SortFile(files);
            sb.Append("sorted:");
            for (int i = 0; i < files.Length; i++)
                sb.Append(files[i] + ",");
            MessageBox.Show(sb.ToString());
        }

        private void SortFile(string[] files)
        {
            SortedList<string, string> slFiles = new SortedList<string, string>();
            foreach (string f in files)
                slFiles.Add(f, f);
            for (int i = 0; i < files.Length; i++)
                files[i] = slFiles.Values[i];
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
            StringBuilder sb = new StringBuilder();
            #region Index            
            sb.AppendLine("[F_INDEX]");
            sb.AppendLine("EVENT_TYPE=00");            
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Index))
            {
                if (f.GetFullFieldName() == GWDataDBField.i_IndexGuid.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_DataDateTime.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_PROCESS_FLAG.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_EventType.GetFullFieldName()) continue;
                sb.AppendLine( f.FieldName.ToUpper() + "=" + f.FieldName.ToUpper());               
            }
            #endregion           

            #region Patient
            sb.AppendLine("");      
            sb.AppendLine("[F_PATIENT]");            
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Patient))
            {

                if (f.GetFullFieldName() == GWDataDBField.p_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_DT.GetFullFieldName()) continue;
                
                sb.AppendLine( f.FieldName.ToUpper() + "=" + f.FieldName.ToUpper());
            }
            #endregion           

            #region Order
            sb.AppendLine("");      
            sb.AppendLine("[F_ORDER]");
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Order))
            {
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_DT.GetFullFieldName()) continue;

                sb.AppendLine( f.FieldName.ToUpper() + "=" + f.FieldName.ToUpper());
            }
            #endregion       
 
            #region Report
            sb.AppendLine("");      
            sb.AppendLine("[F_REPORT]");
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Report))
            {

                if (f.GetFullFieldName() == GWDataDBField.r_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.r_DATA_DT.GetFullFieldName()) continue;

                sb.AppendLine(f.FieldName.ToUpper() + "=" + f.FieldName.ToUpper());
            }
            #endregion      
            
            this.tbIniFile.Text = sb.ToString();
            
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("c:\\filein\\test1.ini", FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(tbIniFile.Text);
            sw.Close();
            fs.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.Now.ToString(""));
        }

        
    }
}