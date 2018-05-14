using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLInboundAdapterObjects
{
    public class ThrPartyAppConfig : XObject
    {
        private int _timerInterval = 1000;
        public int TimerInterval
        {
            get { return _timerInterval; }
            set { _timerInterval = value; }
        }

        private string _filePath = "";
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
    }
}
