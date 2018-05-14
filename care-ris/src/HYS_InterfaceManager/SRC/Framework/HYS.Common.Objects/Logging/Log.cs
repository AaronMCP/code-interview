using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Logging
{
    public class Log:XObject
    {
        private XCollection<LogItem> _logItemList = new XCollection<LogItem>();
        public XCollection<LogItem> LogItemList {
            get { return _logItemList; }
            set { _logItemList = value; }
        }
    }
}
