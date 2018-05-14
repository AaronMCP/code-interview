using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FileInterfaceTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //using (StreamWriter sw = File.CreateText("C:\\FILE_IN_TEST\\a.ini"))
            //{
            //    sw.WriteLine("[SectionA]");
            //    sw.WriteLine("KeyA=1");
            //    sw.Flush();

            //    this.labelStatus.Text = "Writing KeyA";
            //    Application.DoEvents();
            //    Thread.Sleep(3000);

            //    sw.WriteLine("KeyB=2");
            //    sw.Flush();

            //    this.labelStatus.Text = "Writing KeyB";
            //    Application.DoEvents();
            //    Thread.Sleep(3000);

            //    sw.WriteLine("KeyC=3");
            //    sw.Flush();

            //    this.labelStatus.Text = "Writing KeyC";
            //}

            using (FileStream fs = File.Open("C:\\FILE_IN_TEST\\a.ini", FileMode.Create))
            {
                WriteString(fs, "[SectionA]\r\n");
                WriteString(fs, "KeyA=1\r\n");
                fs.Flush();

                this.labelStatus.Text = "Writing KeyA";
                Application.DoEvents();
                Thread.Sleep(3000);

                WriteString(fs, "KeyB=2\r\n");
                fs.Flush();

                this.labelStatus.Text = "Writing KeyB";
                Application.DoEvents();
                Thread.Sleep(3000);

                WriteString(fs, "KeyC=3\r\n");
                fs.Flush();

                this.labelStatus.Text = "Writing KeyC";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FileStream fs = File.Open("C:\\FILE_IN_TEST\\a.ini", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
            {
                WriteString(fs,"[SectionA]\r\n");
                WriteString(fs, "KeyA=1\r\n");
                fs.Flush();

                this.labelStatus.Text = "Writing KeyA";
                Application.DoEvents();
                Thread.Sleep(3000);

                WriteString(fs, "KeyB=2\r\n");
                fs.Flush();

                this.labelStatus.Text = "Writing KeyB";
                Application.DoEvents();
                Thread.Sleep(3000);

                WriteString(fs, "KeyC=3\r\n");
                fs.Flush();

                this.labelStatus.Text = "Writing KeyC";
            }
        }

        private void WriteString(FileStream fs, string str)
        {
            byte[] buf = Encoding.ASCII.GetBytes(str);
            fs.Write(buf, 0, buf.Length);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thr = new Thread(new ThreadStart(ReadStringShare));
            thr.Start();
        }

        private void ReadStringShare()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    using (FileStream fs = File.Open("C:\\FILE_IN_TEST\\a.ini", FileMode.Open, FileAccess.Read, FileShare.Write))
                    {
                        bool isEOF = false;
                        while (!isEOF)
                        {
                            byte[] buf = new byte[1024];
                            int len = fs.Read(buf, 0, 1024);
                            if (len <= 0)
                            {
                                isEOF = true;
                            }
                            else
                            {
                                string str = Encoding.ASCII.GetString(buf, 0, len);
                                sb.Append(str);
                            }
                        }
                    }
                    this.Invoke(new DisplayStringHanlder(DisplayString), new object[] { sb.ToString() });

                }
                catch (Exception e)
                {
                    this.Invoke(new DisplayStringHanlder(DisplayString), new object[] { e.Message });
                }

                Thread.Sleep(1000);
                if (fromClosing) break;
            }
        }

        private void ReadStringSperate()
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    using (FileStream fs = File.OpenRead("C:\\FILE_IN_TEST\\a.ini"))
                    {
                        bool isEOF = false;
                        while (!isEOF)
                        {
                            byte[] buf = new byte[1024];
                            int len = fs.Read(buf, 0, 1024);
                            if (len <= 0)
                            {
                                isEOF = true;
                            }
                            else
                            {
                                string str = Encoding.ASCII.GetString(buf, 0, len);
                                sb.Append(str);
                            }
                        }
                    }
                    this.Invoke(new DisplayStringHanlder(DisplayString), new object[] { sb.ToString() });

                }
                catch (Exception e)
                {
                    this.Invoke(new DisplayStringHanlder(DisplayString), new object[] { e.Message });
                }

                Thread.Sleep(1000);
                if (fromClosing) break;
            }
        }

        private delegate void DisplayStringHanlder(string str);

        private void DisplayString(string str)
        {
            this.textBox1.Text += str + "\r\n";
        }

        private bool fromClosing = false;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            fromClosing = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread thr = new Thread(new ThreadStart(ReadStringSperate));
            thr.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            File.Delete("C:\\FILE_IN_TEST\\a.ini");
        }
    }
}