using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HYS.Adapter.Base;
using System.Runtime.InteropServices;
using HYS.Common.Objects.Translation;

namespace AdapterTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            Array ary = Enum.GetValues(typeof(PinyinType));
            this.comboBoxPYType.Items.Clear();
            foreach (object o in ary)
            {
                string name = GetPinyinTypeName((PinyinType)o);
                this.comboBoxPYType.Items.Add(name);
            }
            if (this.comboBoxPYType.Items.Count > 0) this.comboBoxPYType.SelectedIndex = 0;
        }

        internal static string GetPinyinTypeName(PinyinType t)
        {
            switch (t)
            {
                default: return "";
                case PinyinType.BIG52RomaPinyin: return "BIG5 to Roma Pinyin";
                case PinyinType.GBK2RomaPinyin: return "GB(TC) to Roma Pinyin";
                case PinyinType.GB2Pinyin: return "GB(SC) to Pinyin";
            }
        }

        private IConfigUI _config;
        private void LoadAdapterInfor(Type adapterType)
        {
            AdapterEntryAttributeBase a = AssemblyHelper.GetApdaterEntryAttribute<AdapterEntryAttributeBase>(adapterType);

            if (a == null)
            {
                MessageBox.Show("Cannot find information of type " + adapterType.ToString() + "\r\n\r\n" + AssemblyHelper.LastErrorInfor);
                return;
            }

            this.textBoxName.Text = a.Name;
            this.textBoxDesc.Text = a.Description;
        }
        private void LoadAdapterConfig(Type adapterType)
        {
            IAdapterConfig adapter = AssemblyHelper.CreateAdapter <IAdapterConfig>(adapterType);

            if (adapter == null)
            {
                MessageBox.Show("Cannot create instance of type " + adapterType.ToString() + "\r\n\r\n" + AssemblyHelper.LastErrorInfor);
                return;
            }

            IConfigUI[] uilist = adapter.GetConfigUI();
            if (uilist != null && uilist.Length > 0) _config = uilist[0];

            if (_config == null)
            {
                MessageBox.Show("Cannot get config implementation from adapter " + adapterType.ToString() );
                return;
            }

            Control ctrl = _config.GetControl();

            if (ctrl == null)
            {
                MessageBox.Show("Cannot get config GUI from adapter " + adapterType.ToString() );
                return;
            }

            AssemblyHelper.PrepareControl(ctrl, tabPage2);
            tabPage2.Controls.Add(ctrl);

            //this.Text = _config.FileName;
            _config.LoadConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fname = this.textBoxFile.Text;
            Type adapterType = AssemblyHelper.FindAdapter<AdapterEntryAttributeBase>(fname);

            if (adapterType == null)
            {
                MessageBox.Show("Cannot find adapter implementation in " + fname + "\r\n\r\n" + AssemblyHelper.LastErrorInfor);
                return;
            }

            LoadAdapterInfor(adapterType);
            LoadAdapterConfig(adapterType);
        }

        private void buttonReloadConfig_Click(object sender, EventArgs e)
        {
            if(_config!=null) _config.LoadConfig();
        }

        private void buttonSaveConfig_Click(object sender, EventArgs e)
        {
            if (_config != null) _config.SaveConfig();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string caption = "DemoAdapter of GC Gateway";
            AdapterMessage am = new AdapterMessage(4, AdapterStatus.Stopped);
            am.PostMessage(caption);
        }

        // -----------------------------------

        private void InsertList(object o)
        {
            int index = this.listBox1.Items.Add(o);
            this.listBox1.SelectedIndex = index;
        }
        private delegate void InsertHanlder(object o);
        private void InsertListSafe(object o)
        {
            this.Invoke(new InsertHanlder(InsertList), new object[] { o });
        }

        private void working()
        {
            this.listBox1.Items.Add(m.WaitOne() + " working");
            for (int i = 0; i < 10; i++)
            {
                InsertList(i);

                Application.DoEvents();
                Thread.Sleep(500);
                Application.DoEvents();
            }
            m.ReleaseMutex();
        }
        private void interrupt()
        {
            this.listBox1.Items.Add(m.WaitOne() + " interrupt");
            InsertList("hello");
            m.ReleaseMutex();
        }

        private static Mutex m = new Mutex();

        private void buttonStart_Click(object sender, EventArgs e)
        {
            working();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            interrupt();
        }

        private void RunThreads()
        {
            // Create the threads that will use the protected resource.
            for (int i = 0; i < 3; i++)
            {
                Thread myThread = new Thread(new ThreadStart(MyThreadProc));
                myThread.Name = String.Format("Thread{0}", i + 1);
                myThread.Start();
            }

            // The main thread exits, but the application continues to
            // run until all foreground threads have exited.
        }

        private void MyThreadProc()
        {
            for (int i = 0; i < 3; i++)
            {
                UseResource();
            }
        }

        // This method represents a resource that must be synchronized
        // so that only one thread at a time can enter.
        private void UseResource()
        {
            // Wait until it is safe to enter.
            bool res = m.WaitOne(100000,true);

            if (!res)
            {
                InsertListSafe(Thread.CurrentThread.Name + " wait time out !!!");
                return;
            }

            InsertListSafe(Thread.CurrentThread.Name + " has entered the protected area");

            // Place code to access non-reentrant resources here.

            // Simulate some work.
            Thread.Sleep(500);

            InsertListSafe(Thread.CurrentThread.Name + " is leaving the protected area\r\n");

            // Release the Mutex.
            m.ReleaseMutex();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            RunThreads();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection cn = new OleDbConnection();
            cn.ConnectionString = "Provider=SQLNCLI;Server=CN-SH-D0406210;Database=GWDataDB;UID=10095177;Trusted_Connection=Yes;";

            string spName = "sp_out_test_GarbageCollection";

            OleDbCommand cmd = new OleDbCommand(spName, cn);
            cmd.CommandType = CommandType.StoredProcedure;

            OleDbParameter paramProcessFlag = new OleDbParameter();
            paramProcessFlag.Direction = ParameterDirection.Input;
            paramProcessFlag.ParameterName = "@ProcessFlag";
            paramProcessFlag.OleDbType = OleDbType.VarWChar;
            cmd.Parameters.Add(paramProcessFlag);
            paramProcessFlag.Value = "1";

            OleDbParameter paramFromTime = new OleDbParameter();
            paramFromTime.Direction = ParameterDirection.Input;
            paramFromTime.ParameterName = "@FromDateTime";
            paramFromTime.OleDbType = OleDbType.DBTimeStamp;
            paramFromTime.Value = DBNull.Value;
            cmd.Parameters.Add(paramFromTime);

            OleDbParameter paramToTime = new OleDbParameter();
            paramToTime.Direction = ParameterDirection.Input;
            paramToTime.ParameterName = "@ToDateTime";
            paramToTime.OleDbType = OleDbType.DBTimeStamp;
            cmd.Parameters.Add(paramToTime);
            paramToTime.Value = DateTime.Now;

            OleDbParameter paramResult = new OleDbParameter();
            paramResult.Direction = ParameterDirection.Output;
            paramResult.ParameterName = "@result";
            paramResult.OleDbType = OleDbType.Integer;
            cmd.Parameters.Add(paramResult);

            cn.Open();
            int result = cmd.ExecuteNonQuery();
            cn.Close();

            MessageBox.Show(result.ToString() + " " + paramResult.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process p = Process.Start("HYS.IM.Adapter.Config.exe");
            p.Exited += new EventHandler(p_Exited);
            p.EnableRaisingEvents = true;
        }

        void p_Exited(object sender, EventArgs e)
        {
            Process p = sender as Process;
            MessageBox.Show(p.ExitCode.ToString());
        }

        private void buttonFindSQLServer_Click(object sender, EventArgs e)
        {
            string fileName = "C:\\Program Files\\Microsoft SQL Server\\90\\Tools\\Binn\\osql.exe";
            string argument = "-S (local) -E-i \"D:\\ClearCase\\GCGateway_LiangLiang_view\\gcgateway\\V2.0\\SRC\\HYS\\AdapterTest\\bin\\Debug\\InstallTable.sql\"";
            //string argument = "-S (local) -E -i \"D:\\ClearCase\\GCGateway_LiangLiang_view\\gcgateway\\V2.0\\SRC\\HYS\\AdapterTest\\bin\\Debug\\UninstallTable.sql\"";

            if (System.IO.File.Exists(fileName))
            {
                Form dlg = new Form();
                dlg.Text = "Processing";
                dlg.Size = new Size(500, 500);
                TextBox tb = new TextBox();
                tb.Dock = DockStyle.Fill;
                tb.BackColor = Color.Black;
                tb.ForeColor = Color.White;
                tb.ScrollBars = ScrollBars.Vertical;
                tb.WordWrap = true;
                tb.Multiline = true;
                dlg.Controls.Add(tb);
                //dlg.Owner = this;
                dlg.Show();

                tb.Text += "---------------- Invoking Begin ----------------\r\n";
                Application.DoEvents();

                //ProcessStartInfo processStartInfo = new ProcessStartInfo(fileName, "-L");
                ProcessStartInfo processStartInfo = new ProcessStartInfo(fileName, argument);
                processStartInfo.UseShellExecute = false;
                processStartInfo.CreateNoWindow = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;

                tb.Text += "Command: " + fileName + "\r\n";
                tb.Text += "Argument: " + argument + "\r\n";

                Process process = Process.Start(processStartInfo);
                process.WaitForExit();
                listBoxSQLServer.Items.Clear();
                //int line = 1;
                //int index = 0;
                //int count = 16;
                string server = null;
                while (process.StandardOutput.Peek() > -1)
                {
                    server = process.StandardOutput.ReadLine().Trim();

                    //char[] chrList = new char[count];
                    //process.StandardOutput.Read(chrList, index, count);
                    //index += count;

                    //StringBuilder sb = new StringBuilder();
                    //sb.Append(chrList);
                    //server = sb.ToString();
                    
                    tb.Text += server + "\r\n";
                    tb.SelectionStart = tb.Text.Length - 1;
                    tb.ScrollToCaret();
                    Application.DoEvents();
                    Thread.Sleep(50);

                    //line += 1;
                    //if (line > 6)
                    //{
                    //    listBoxSQLServer.Items.Add(server);
                    //}
                    //server = null;
                }

                tb.Text += "---------------- Invoking End ----------------\r\n\r\n";
                Application.DoEvents();
                Thread.Sleep(500);
                dlg.Dispose();
            }
            listBoxSQLServer.Items.Remove(Environment.MachineName);
            listBoxSQLServer.Items.Add("localhost");
        }

        private void buttonCC2PY_Click(object sender, EventArgs e)
        {
            //StringBuilder strCC = new StringBuilder();
            //strCC.Append(this.textBoxWord.Text);
            //StringBuilder strPY = new StringBuilder();

            //uint i = CC2PY(strPY, strCC, 2);

            //MessageBox.Show(strPY.ToString());

            MessageBox.Show(PinyinFactory.GetInstance((PinyinType)this.comboBoxPYType.SelectedIndex).ConvertName(this.textBoxWord.Text,Program.Log));
        }

        [DllImport("gatewaylang_SC.dll")]
        public static extern uint CC2PY(StringBuilder py, StringBuilder cc, uint length);

        private void buttonReplace_Click(object sender, EventArgs e)
        {
            string str = this.textBoxSource.Text;
            string exp = this.textBoxRegExp.Text;
            string rep = this.textBoxReplace.Text;

            string res = Regex.Replace(str, exp, rep);
            
            this.textBoxResult.Text = res;
            this.textBoxResult.SelectAll();

            // (^\s*)|(\s*$)
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string str = this.textBoxWord.Text;
            this.textBoxWord.Text = ChineseCode.GBK2BIG5(str);
            
            //string str = this.textBoxWord.Text;
            //byte[] blist2 = Encoding.GetEncoding("BIG5").GetBytes(str);
            //byte[] blist = Encoding.GetEncoding("GB2312").GetBytes(str);
            //str = Encoding.GetEncoding("GB18030").GetString(blist);
            //MessageBox.Show(str);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string str = this.textBoxWord.Text;
            str = Program.SCTCConvert(Program.ConvertType.Traditional, str);
            this.textBoxWord.Text = str;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string str = this.textBoxWord.Text;
            str = Program.SCTCConvert(Program.ConvertType.Simplified, str);
            this.textBoxWord.Text = str;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string str = this.textBoxWord.Text;
            //MessageBox.Show(Program.BIG5toGB(str));
            this.textBoxWord.Text = ChineseCode.BIG52GBK(str);

            //StringBuilder sb = new StringBuilder();
            //foreach (char c in str)
            //{
            //    string cstr = c.ToString() + "¸É¸É";
            //    string ccstr = Program.BIG5toGB(cstr);
            //    string cccstr = ccstr.Substring(0, 1);
            //    sb.Append(cccstr);
            //}
            //MessageBox.Show(sb.ToString());
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string str = this.textBoxWord.Text;
            this.textBoxWord.Text = ChineseCode.GB2BIG5(str);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string str = this.textBoxWord.Text;
            this.textBoxWord.Text = ChineseCode.BIG52GB(str);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //string str = this.textBoxWord.Text;
            //this.textBoxWord.Text = Convert(950, 936, str);

            string unicodeString = this.textBoxWord.Text; // "This string contains the unicode character Pi(\u03a0)";

            // Create two different encodings.
            Encoding ascii = Encoding.GetEncoding(936);// Encoding.ASCII;
            Encoding unicode = Encoding.GetEncoding(950);// Encoding.Unicode;

            // Convert the string into a byte[].
            byte[] unicodeBytes = unicode.GetBytes(unicodeString);

            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string.
            // This is a slightly different approach to converting to illustrate
            // the use of GetCharCount/GetChars.
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);

            // Display the strings created before and after the conversion.
            MessageBox.Show("Original string: " + unicodeString);
            MessageBox.Show("Ascii converted string: " + asciiString);

        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int MultiByteToWideChar(int CodePage, int dwFlags, StringBuilder lpMultiByteStr, int cchMultiByte, byte[] lpWideCharStr, int cchWideChar);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int WideCharToMultiByte(int CodePage, int dwFlags, byte[] lpWideCharStr, int cchWideChar, StringBuilder lpMultiByteStr, int cchMultiByte, string lpDefaultChar, StringBuilder lpUsedDefaultChar);


        public static string Convert(int fromCP, int toCP, string strSource)
        {
            StringBuilder sbSource = new StringBuilder(strSource);
            byte[] sbTarget = new byte[strSource.Length * 2];
            int nReturn = MultiByteToWideChar(fromCP, 0, sbSource, strSource.Length * 2, sbTarget, strSource.Length * 2);
            StringBuilder sbOut = new StringBuilder();
            nReturn = WideCharToMultiByte(toCP, 0, sbTarget, nReturn, sbOut, strSource.Length * 2, "?", null);
            string str = sbOut.ToString();
            return str;
        }
    }
}