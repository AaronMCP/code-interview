using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Config;

namespace HYS.SQLInboundAdapterObjects
{
    public class ThirdDBConnection : XObject
    {
        private bool _isChanged = false;
        public bool IsChanged {
            get { return _isChanged; }
            set { _isChanged = value;}
        }

        private string _provider = "SQLNCLI"; //"SQLOLEDB";
        [XCData(true)]
        public string Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        private string _server = "";
        [XCData(true)]
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        private string _database = "";
        [XCData(true)]
        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }

        private string _user = "";
        [XCData(true)]
        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _password = "";
        [XCData(true)]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _connectionStr = "";
        [XCData(true)]
        public string ConnectionStr
        {
            get { return _connectionStr; }
            set { _connectionStr = value; }
        }

        private bool _suicideWhenOleDbException = false;
        /// <summary>
        /// sybase OLEDB provider cannot recover itself when network recover after a short break, unless restart the process (by Windows NT Service Recovering Machenism)
        /// </summary>
        public bool SuicideWhenOleDbException
        {
            get { return _suicideWhenOleDbException; }
            set { _suicideWhenOleDbException = value; }
        }

        private string _suicideWhenOleDbExceptionErrorCodeExclude = "40001";    // deadlock
        public string SuicideWhenOleDbExceptionErrorCodeExclude
        {
            get { return _suicideWhenOleDbExceptionErrorCodeExclude; }
            set { _suicideWhenOleDbExceptionErrorCodeExclude = value; }
        }

        private bool _fileConnection = false;
        public bool FileConnection
        {
            get { return _fileConnection; }
            set { _fileConnection = value; }
        }

        private string _fileFolder = "";
        [XCData(true)]
        public string FileFolder
        {
            get { return _fileFolder; }
            set { _fileFolder = value; }
        }

        private string _fileConnectionString = "";
        [XCData(true)]
        public string FileConnectionString
        {
            get { return _fileConnectionString; }
            set { _fileConnectionString = value; }
        }

        private string _fileNamePattern = "";
        /// <summary>
        /// Note: be used for finding data file when IndexFileDriven==false,
        /// be used for finding index file when IndexFileDriven==true.
        /// </summary>
        [XCData(true)]
        public string FileNamePattern
        {
            get { return _fileNamePattern; }
            set { _fileNamePattern = value; }
        }

        private bool _indexFileDriven = false;
        public bool IndexFileDriven
        {
            get { return _indexFileDriven; }
            set { _indexFileDriven = value; }
        }

        private string _indexfileFolder = "";
        [XCData(true)]
        public string IndexFileFolder
        {
            get { return _indexfileFolder; }
            set { _indexfileFolder = value; }
        }

        private bool _moveFileWhenError = true;
        /// <summary>
        /// Note: the file will be deleted automatically when success.
        /// </summary>
        public bool MoveFileWhenError
        {
            get { return _moveFileWhenError; }
            set { _moveFileWhenError = value; }
        }

        private string _movefileFolder = "";
        [XCData(true)]
        public string MoveFileFolder
        {
            get { return _movefileFolder; }
            set { _movefileFolder = value; }
        }

        private bool _fileTransformBeforeRead = false;
        public bool FileTransformBeforeRead
        {
            get { return _fileTransformBeforeRead; }
            set { _fileTransformBeforeRead = value; }
        }

        private RegularExpressionItem _fileTransformRule = new RegularExpressionItem()
        {
            Expression = @",(?<minute_and_second>\d{2}:\d{2}.\d{1}),",
            Replacement = ",\"${minute_and_second}\",",
            Description = "Replace oledb-invalid datetime format into a free text value by adding \"\".",
        };
        public RegularExpressionItem FileTransformRule
        {
            get { return _fileTransformRule; }
            set { _fileTransformRule = value; }
        }
    }
}
