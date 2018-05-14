using System;
using System.Data.OleDb;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLInboundAdapterObjects
{
    public class ThrPartyDBParamter : XObject
    {
        private int _fieldID = 0;
        public int FieldID
        {
            get { return _fieldID; }
            set { _fieldID = value; }
        }
        
        private OleDbType _fieldType = OleDbType.LongVarChar;
        public OleDbType FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        private string _fieldName = "";
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        //public override string ToString()
        //{
        //    if (_fieldType == ThrPartyDBType.unknown) return "[NULL]";
        //    return FieldName;
        //}

        //public static ThrPartyDBParamter Null = new ThrPartyDBParamter();
    }
}
