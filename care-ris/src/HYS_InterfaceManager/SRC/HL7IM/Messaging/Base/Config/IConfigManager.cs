using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public interface IConfigManager
    {
        bool Load();
        bool Save();
        XObject Config { get; }
        Exception LastError { get; }
        string LastErrorInfor { get; }
    }
}
