using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Translation
{
    public class PinyinFactory
    {
        public static IPinyin GetInstance(PinyinType type)
        {
            switch (type)
            {
                default: return EmptyPinyin.Instance;
                case PinyinType.GB2Pinyin: return GB2Pinyin.Instance;
                case PinyinType.GBK2RomaPinyin: return GBK2RomaPinyin.Instance;
                case PinyinType.BIG52RomaPinyin: return BIG52RomaPinyin.Instance;
            }
        }
    }

    class EmptyPinyin : IPinyin
    {
        public static EmptyPinyin Instance = new EmptyPinyin();

        #region IPinyin Members

        public string ConvertName(string chineseName)
        {
            return chineseName;
        }

        public string ConvertName(string chineseName, HYS.Common.Objects.Logging.ILogging log)
        {
            return chineseName;
        }

        #endregion
    }

    public enum PinyinType
    {
        None,
        GB2Pinyin,
        GBK2RomaPinyin,
        BIG52RomaPinyin,
    }
}
