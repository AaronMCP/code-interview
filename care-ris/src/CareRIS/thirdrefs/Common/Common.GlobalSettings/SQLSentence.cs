using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    public interface ISQLSentence
    {
        string SQL { get;set;}
        string returnResult(string szParams);
    }
}
