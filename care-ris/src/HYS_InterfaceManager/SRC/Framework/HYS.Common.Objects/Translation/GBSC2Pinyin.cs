using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Objects.Translation
{
    public class GB2Pinyin : PinyinBase
    {
        [DllImport("gatewaylang_SC.dll")]
        internal static extern uint CC2PY(StringBuilder py, StringBuilder cc, uint length);

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
                CC2PY(strPY, strCC, 2);

                return strPY.ToString();
            }
            catch (Exception err)
            {
                if (log != null) log.Write(LogType.Error, err.ToString());
                return "";
            }
        }

        private static GB2Pinyin _instance;
        public static GB2Pinyin Instance
        {
            get
            {
                if (_instance == null) _instance = new GB2Pinyin();
                return _instance;
            }
        }
    }
}
