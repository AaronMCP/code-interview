using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.DataAccess;

namespace HYS.IM.BusinessControl.DataControl
{
    public class CombinationRec : DObject
    {
        private string _dataIn = "";
        [DField("DataIn", "nvarchar(64)")]
        public string DataIn
        {
            get { return _dataIn; }
            set { _dataIn = value; }
        }

        private string _dataOut = "";
        [DField("DataOut", "nvarchar(64)")]
        public string DataOut
        {
            get { return _dataOut; }
            set { _dataOut = value; }
        }

        private string _dataMappingFile = "";
        [DField("Data_Mapping_File", "nvarchar(255)")]
        public string DataMappingFile
        {
            get { return _dataMappingFile; }
            set { _dataMappingFile = value; }
        }
    }
}
