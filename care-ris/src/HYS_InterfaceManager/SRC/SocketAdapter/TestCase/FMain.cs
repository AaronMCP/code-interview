using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace TestCase
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TcpListener listen = new TcpListener(IPAddress.Parse("127.0.0.1"), 6000);
            //listen.Server.Bind(new IPEndPoint(IPAddress.Any, 6000));
            //listen.Server.Listen(10);
            //listen.Start();


            //Socket listen = new Socket(new IPEndPoint(IPAddress.Any, 6000).AddressFamily, SocketType.Raw, ProtocolType.Tcp);
            //listen.Bind(new IPEndPoint(IPAddress.Any, 6000));
            //listen.Listen(10);
            //listen.Start();
           
            FTestConnection ftc = new FTestConnection();
            ftc.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = Convert.ToInt32(textBox1.Text).ToString("D08");
        }

        struct MyStruct
        {
            public int id;
            public string name;

        }
        private void button3_Click(object sender, EventArgs e)
        {
            MyStruct st;
            st.id = 1;
            st.name = "2";

            st = new MyStruct();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            byte[] buf1 = System.Text.Encoding.Default.GetBytes("12345");
            byte[] buf2 = new byte[10];
            Array.Copy(buf1, buf2, 5);
            Array.Copy(buf1,0, buf2,5, 5);

            string s = System.Text.Encoding.Default.GetString(buf2);

            MessageBox.Show(s);
        }

        class Class1
        {
            public Class1()
            {
                MessageBox.Show("Contruct Class1");
            }
        }

        class Class2
        {

            public Class2()
            {
                MessageBox.Show("Contruct Class2");
            }

            Class1 c1 = new Class1();

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Class2 c2 = new Class2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //NO, codepage, name, dispname, windowscodepate
            int i = 0;
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                i++;
                ListViewItem lvi = this.listView1.Items.Add(i.ToString());
                
                lvi.SubItems.Add(ei.CodePage.ToString());
                lvi.SubItems.Add(ei.Name.ToString());
                lvi.SubItems.Add(ei.DisplayName.ToString());
                Encoding en = ei.GetEncoding();
                lvi.SubItems.Add(en.WindowsCodePage.ToString());

                byte[] buf = en.GetBytes("A");
                lvi.SubItems.Add(buf.Length.ToString());

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FPressureTest f = new FPressureTest();
            try
            {
                f.ShowDialog();
            }
            finally
            {

            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10; i++)
                sb.AppendLine(Guid.NewGuid().ToString());
            MessageBox.Show(sb.ToString());
        }

        private void btDtTest_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(tbDt.Text);
                MessageBox.Show(dt.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}