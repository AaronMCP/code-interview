using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Objects.Translation
{
    public class BIG52RomaPinyin : PinyinBase
    {
        private const int JKTCPYCNVT_NOBLANK = 0x0001;
        private const int JKTCPYCNVT_NOUPPERCAPITAL = 0x0002;
        private const int JKTCPYCNVT_ALLUPPER = 0x0004;
        private const int JKTCPYCNVT_DASHLINK = 0x0008;

        [DllImport("JKTCPYCnvt_BIG5.dll")]
        internal static extern uint GetTCPinyin(StringBuilder py, StringBuilder cc, uint dwDestSize, uint dwFlags);

        internal override string Convert(string chinese)
        {
            return Convert(chinese, null);
        }
        internal override string Convert(string chinese, ILogging log)
        {
            if (chinese == null || chinese.Length < 1) return "";

            try
            {
                StringBuilder strCC = new StringBuilder();
                strCC.Append(chinese);

                StringBuilder strPY = new StringBuilder();
                GetTCPinyin(strCC, strPY, 256, JKTCPYCNVT_NOUPPERCAPITAL);

                return strPY.ToString();
            }
            catch (Exception err)
            {
                if (log != null) log.Write(LogType.Error, err.ToString());
                return "";
            }
        }

        private static BIG52RomaPinyin _instance;
        public static BIG52RomaPinyin Instance
        {
            get
            {
                if (_instance == null) _instance = new BIG52RomaPinyin();
                return _instance;
            }
        }
    }
}
