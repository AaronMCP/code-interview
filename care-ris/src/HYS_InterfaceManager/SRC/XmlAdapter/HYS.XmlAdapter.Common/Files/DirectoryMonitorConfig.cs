using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Common.Files
{
    public class DirectoryMonitorConfig : XObject
    {
        private string _sourcePath = "InputFiles";
        public string SourcePath
        {
            get { return _sourcePath; }
            set { _sourcePath = value; }
        }

        private int _timerInterval = 10000; //10s
        public int TimerInterval
        {
            get { return _timerInterval; }
            set { _timerInterval = value; }
        }

        private bool _deleteProcessedFile;
        public bool DeleteProcessedFile
        {
            get { return _deleteProcessedFile; }
            set { _deleteProcessedFile = value; }
        }

        private string _successFolder = "ok_files";
        public string SuccessFolder
        {
            get { return _successFolder; }
            set { _successFolder = value; }
        }

        private string _failureFolder = "err_files";
        public string FailureFolder
        {
            get { return _failureFolder; }
            set { _failureFolder = value; }
        }
    }
}
