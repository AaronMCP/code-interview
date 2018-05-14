using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class MappingRule<T> : XObject
        where T : MappingItem
    {
        private XCollection<T> _mappingList = new XCollection<T>();
        public XCollection<T> MappingList
        {
            get { return _mappingList; }
            set { _mappingList = value; }
        }

        public T Find_IndexTable_DataID_Field()
        {
            foreach (T item in MappingList)
            {
                if (item.GWDataDBField.Table == GWDataDBTable.Index &&
                    item.GWDataDBField.FieldName == "Data_ID")
                {
                    return item;
                }
            }
            return null;
        }
    }
}
