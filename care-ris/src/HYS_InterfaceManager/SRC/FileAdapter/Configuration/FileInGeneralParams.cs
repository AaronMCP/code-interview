using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Adapter.Base;

namespace HYS.FileAdapter.Configuration
{
    public class FileInGeneralParams : XObject
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
        private string _FilePrefix = "";
        public string FilePrefix
        {
            get { return _FilePrefix; }
            set { _FilePrefix = value; }
        }
        //private string _FileDtFormat = "yyyyMMddHHmmss";
        //public string FileDtFormat
        //{
        //    get { return _FileDtFormat; }
        //    set { _FileDtFormat = value; }
        //}
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

        //used to move files whose have been treated to
        private string _InFileMovePath = "";
        public string InFileMovePath
        {
            get { return _InFileMovePath; }
            set { _InFileMovePath = value; }
        }

        private InFileTreatTypeAfterRead _InFileTreatTypeAfterRead = InFileTreatTypeAfterRead.None;
        public InFileTreatTypeAfterRead FileTreatTypeAfterRead
        {
            get { return _InFileTreatTypeAfterRead; }
            set { _InFileTreatTypeAfterRead = value; }

        }

        public enum InFileTreatTypeAfterRead
        {
            None,
            Delete,
            Move
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
