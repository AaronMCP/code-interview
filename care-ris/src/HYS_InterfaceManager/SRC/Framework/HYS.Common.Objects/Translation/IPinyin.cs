using System;
using System.Collections.Generic;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Objects.Translation
{
    public interface IPinyin
    {
        string ConvertName(string chineseName);
        string ConvertName(string chineseName, ILogging log);
    }
}
