using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;

namespace SQLOutboundAdapter.Objects
{
    public class Configuration : XObject
    {
        private int _queryInterval = 5000;  //default value
        public int QueryInterval
        {
            get { return _queryInterval; }
            set { _queryInterval = value; }
        }

        private string _dbConnection;
        public string DBConnection
        {
            get { return _dbConnection; }
            set { _dbConnection = value; }
        }

        private string _sqlStatement;
        [XCData(true)]
        public string SQLStatement
        {
            get { return _sqlStatement; }
            set { _sqlStatement = value; }
        }
    }
}
