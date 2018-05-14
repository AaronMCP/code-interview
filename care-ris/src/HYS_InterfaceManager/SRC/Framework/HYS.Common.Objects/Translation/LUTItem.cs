using System;
using System.Collections.Generic;
using HYS.Common.DataAccess;

namespace HYS.Common.Objects.Translation
{
    public class LUTItem : DObject
    {
        private int _id;
        [DMainKey, DAutoIncrementing]
        [DField("ID", "int IDENTITY(1,1) PRIMARY KEY")]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _sourceValue = "";
        [DField("SourceValue", "nvarchar(255)")]
        public string SourceValue
        {
            get { return _sourceValue; }
            set { _sourceValue = value; }
        }

        private string _targetValue = "";
        [DField("TargetValue", "nvarchar(255)")]
        public string TargetValue
        {
            get { return _targetValue; }
            set { _targetValue = value; }
        }
    }
}
