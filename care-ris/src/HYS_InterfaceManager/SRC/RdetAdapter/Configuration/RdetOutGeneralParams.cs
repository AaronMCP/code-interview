using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.RdetAdapter.Configuration
{
    public class RdetOutGeneralParams : XObject
    {
       
        private int _timerInterval = 1000 * 60;  // 30 second
        public int TimerInterval
        {
            get { return _timerInterval; }
            set { _timerInterval = value; }
        }

        private bool _timerEnable = false;
        public bool TimerEnable
        {
            get { return _timerEnable; }
            set { _timerEnable = value; }
        }

        private bool _UseCache = true;
        public bool UseCache
        {
            get { return _UseCache; }
            set { _UseCache = value; }
        }

        //Connect access database
        private string _CacheDBConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=rdetcache.mdb";
        public string CacheDBConnStr
        {
            get { return _CacheDBConnStr; }
            set { _CacheDBConnStr = value; }
        }
    }
}
