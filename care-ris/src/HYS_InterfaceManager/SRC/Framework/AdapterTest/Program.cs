using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using HYS.Adapter.Base;

namespace AdapterTest
{
    static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int MultiByteToWideChar(int CodePage, int dwFlags, StringBuilder lpMultiByteStr, int cchMultiByte, byte[] lpWideCharStr, int cchWideChar);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int WideCharToMultiByte(int CodePage, int dwFlags, byte[] lpWideCharStr, int cchWideChar, StringBuilder lpMultiByteStr, int cchMultiByte, string lpDefaultChar, StringBuilder lpUsedDefaultChar);

        //[DllImport("kernel32.dll", SetLastError = true)]
        //public static extern int MultiByteToWideChar(int CodePage, int dwFlags, byte[] lpMultiByteStr, int cchMultiByte, byte[] lpWideCharStr, int cchWideChar);

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern int WideCharToMultiByte(int CodePage, int dwFlags, byte[] lpWideCharStr, int cchWideChar, byte[] lpMultiByteStr, int cchMultiByte, string lpDefaultChar, StringBuilder lpUsedDefaultChar);

        [DllImport("kernel32.dll", EntryPoint = "LCMapStringA")]
        public static extern int LCMapString(int Locale, int dwMapFlags, byte[] lpSrcStr, int cchSrc, byte[] lpDestStr, int cchDest);

        const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
        const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

        public enum ConvertType
        {
            Simplified,
            Traditional
        }

        //private Encoding gb2312 = Encoding.GetEncoding(936);

        public static string SCTCConvert(ConvertType c, string strSource) 
        {
            byte[] source = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strSource);
            byte[] dest = new byte[source.Length];
            switch (c)
            {
                case ConvertType.Simplified:
                    {
                        LCMapString(0x0804, LCMAP_SIMPLIFIED_CHINESE, source, -1, dest, source.Length);
                        break;
                    }
                case ConvertType.Traditional:
                    {
                        LCMapString(0x0804, LCMAP_TRADITIONAL_CHINESE, source, -1, dest, source.Length);
                        break;
                    }
            }
            return System.Text.Encoding.GetEncoding("GB2312").GetString(dest);
        }

        public static string BIG5toGB(string strSource)
        {
            StringBuilder sbSource = new StringBuilder(strSource);
            //byte[] sbTarget = new byte[strSource.Length * 2 + 1];
            byte[] sbTarget = new byte[strSource.Length * 2];
            int nReturn = MultiByteToWideChar(950, 0, sbSource, strSource.Length * 2, sbTarget, strSource.Length * 2);
            //int nReturn = MultiByteToWideChar(950, 0, sbSource, strSource.Length * 2, sbTarget, strSource.Length * 2 + 1);
            string str = sbTarget.ToString();
            StringBuilder sbOut = new StringBuilder();
            nReturn = WideCharToMultiByte(936, 0, sbTarget, nReturn, sbOut, strSource.Length * 2, "?", null);
            //nReturn = WideCharToMultiByte(936, 0, sbTarget, nReturn, sbOut, nReturn, "?", new StringBuilder("?"));
            str = sbOut.ToString();
            return str;

            //byte[] source = Encoding.GetEncoding("BIG5").GetBytes(strSource);
            //byte[] target = new byte[source.Length];
            //byte[] output = new byte[source.Length];
            //int nReturn = MultiByteToWideChar(950, 0, source, source.Length, target, target.Length);
            //nReturn = WideCharToMultiByte(936, 0, target, nReturn, output, output.Length, "?", null);
            //string str = Encoding.GetEncoding("GB2312").GetString(output);
            //return str;
        }

        //[Wrong]
        //File:  9kXlOHilzQ8PTkLD/LeJgLst5HOAXlfoxshbirRlH2unJyhzAIsDR8px7Rl9Qom1
        //Object: fvrc5Zz6AaL6kfwGtzvQsdjPM2PFZo=
        //[Right]
        //File: 3A802Yp6SkzmaLGwMK0b6w==
        //Object: service

        public static Logging Log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
            return;

            byte[] blist = new byte[] { 0xd9, 0x87 };
            string strGBK = Encoding.GetEncoding("GB18030").GetString(blist);
            blist = new byte[] {  0xc0, 0xb5 };
            string strGB = Encoding.GetEncoding("GB18030").GetString(blist);
            blist = new byte[] { 0xbf, 0xe0 };
            string strGBK2 = Encoding.GetEncoding("GB18030").GetString(blist);
            string strBIG5 = Encoding.GetEncoding("BIG5").GetString(blist);
            blist = new byte[] { 0xf4, 0x8c };
            string strUnicode = Encoding.Unicode.GetString(blist);
            MessageBox.Show("\u8cf4" + strGBK + strGB + strGBK2 + strBIG5 + strUnicode);

            // GB -> BIG5
            blist = Encoding.GetEncoding("BIG5").GetBytes("Ù‡");// ("\u8cf4");
            strBIG5 = Encoding.GetEncoding("BIG5").GetString(blist);
            strGB = Encoding.GetEncoding("GB18030").GetString(blist);
            MessageBox.Show(strBIG5 + strGB);

            // BIG5 -> GB
            blist = Encoding.GetEncoding("GB18030").GetBytes("\u82e6");//GetBytes("¿à");
            strBIG5 = Encoding.GetEncoding("BIG5").GetString(blist);
            strGB = Encoding.GetEncoding("GB18030").GetString(blist);
            MessageBox.Show(strBIG5 + strGB);

            blist = Encoding.Unicode.GetBytes("Àµ");
            
            //blist = Encoding.GetEncoding("BIG5").GetBytes("¿à");
            //strBIG5 = Encoding.GetEncoding("BIG5").GetString(blist);
            //strGB = Encoding.GetEncoding("GB18030").GetString(blist);
            //MessageBox.Show(strBIG5 + strGB);

            MessageBox.Show(string.Format("_{0}-{{{{0}}}}-\r\n-{1}~~{2}``", 
                new object[] { "a", "bb", "ccc", "dddd" }));

            Log = new Logging("\\AdapterTest.log");
            LoggingHelper.EnableApplicationLogging(Log);
            LoggingHelper.EnableXmlLogging(Log);

            MessageBox.Show(int.MaxValue.ToString());
            MessageBox.Show(((DayOfWeek)5).ToString());

            //DataCrypto dc = new DataCrypto();
            //MessageBox.Show(dc.Encrypto("service"));
            //MessageBox.Show(dc.Encrypto(dc.Encrypto("3A802Yp6SkzmaLGwMK0b6w==")));
            //MessageBox.Show(dc.Encrypto(dc.Decrypto("3A802Yp6SkzmaLGwMK0b6w==")));
            //return;

            //string caption = "DemoAdapter of GC Gateway";
            //AdapterMessage am = new AdapterMessage(4, AdapterStatus.Stopped);
            //am.PostMessage(caption);
            //return;

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }

    
}