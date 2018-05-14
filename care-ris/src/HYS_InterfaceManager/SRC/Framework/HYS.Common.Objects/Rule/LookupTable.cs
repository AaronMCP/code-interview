using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class LookupTable : XObject
    {
        public LookupTable()
        {
            _tableName = GWDataDB.GetPrivateLutName();
        }

        private string _tableName = "";
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        private string _displayName = "";
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        private XCollection<LookupItem> _table = new XCollection<LookupItem>();
        public XCollection<LookupItem> Table
        {
            get { return _table; }
            set { _table = value; }
        }

        public LookupItem Find(string source)
        {
            foreach (LookupItem item in Table)
            {
                if (item.SourceValue == source) return item;
            }
            return null;
        }
    }
}
