using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLOutboundAdapterObjects
{
    public class ThirdDBConnection : XObject
    {
        private bool _isChanged = false;
        public bool IsChanged
        {
            get { return _isChanged; }
            set { _isChanged = value; }
        }

        private string _provider = "SQLNCLI";   //"SQLOLEDB";
        public string Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }
        
        private string _server = "";
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        private string _database = "";
        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }

        private string _user = "";
        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _password = "";
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _connectionStr = "";
        public string ConnectionStr
        {
            get { return _connectionStr; }
            set { _connectionStr = value; }
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

        private string _fileNameExtension = "";
        [XCData(true)]
        public string FileNameExtension
        {
            get { return _fileNameExtension; }
            set { _fileNameExtension = value; }
        }

        private bool _writeIndexFile = false;
        public bool WriteIndexFile
        {
            get { return _writeIndexFile; }
            set { _writeIndexFile = value; }
        }

        private string _indexFileFolder = "";
        [XCData(true)]
        public string IndexFileFolder
        {
            get { return _indexFileFolder; }
            set { _indexFileFolder = value; }
        }

        private bool _keepSchemaFile = false;
        /// <summary>
        /// Note: the schema ini file auto generated by OLEDB driver when creating csv file will be deleted automatically by default.
        /// </summary>
        public bool KeepSchemaFile
        {
            get { return _keepSchemaFile; }
            set { _keepSchemaFile = value; }
        }

        private string _schemaFileName = "schema.ini";
        [XCData(true)]
        public string SchemaFileName
        {
            get { return _schemaFileName; }
            set { _schemaFileName = value; }
        }

        private string _tempFileNameExtension = ".tmp";
        public string TempFileNameExtensin
        {
            get { return _tempFileNameExtension; }
            set { _tempFileNameExtension = value; }
        }
    }
}
