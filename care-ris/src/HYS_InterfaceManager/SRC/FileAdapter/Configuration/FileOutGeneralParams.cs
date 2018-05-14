using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects;
using HYS.Common.Objects.Rule;

namespace HYS.FileAdapter.Configuration
{
    public class FileOutGeneralParams : XObject
    {        
        #region Timer setup
        private int _timerInterval = 1000*30;  // 30 second
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
        #endregion

        #region File Name Setup 
        private XCollection<GWDataDBField> _PreGWDataDBFields = new XCollection<GWDataDBField>();
        public XCollection<GWDataDBField> PreGWDataDBFields
        {
            get { return _PreGWDataDBFields; }
            set { _PreGWDataDBFields = value; }
        }

        private string _FilePrefix = "";
        public string FilePrefix
        {
            get { return _FilePrefix; }
            set { _FilePrefix = value; }
        }
        private string _FileDtFormat = "yyyyMMddHHmmss";
        public string FileDtFormat
        {
            get { return _FileDtFormat; }
            set { _FileDtFormat = value; }
        }
        private string _FileSuffix = ".ini";
        public string FileSuffix
        {
            get { return _FileSuffix; }
            set { _FileSuffix = value; }
        }
        #endregion

        #region File Path
        private string _FilePath = "";
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
               
        #endregion

        string _CodePageName = "";      //empty means default
        public string CodePageName
        {
            get { return _CodePageName; }
            set { _CodePageName = value; }
        }
    }
}
