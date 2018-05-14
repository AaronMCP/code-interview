using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class QueryCriteriaRule<T> : MappingRule<T>
        where T : QueryCriteriaItem
    {
        private QueryCriteriaRuleType _type = QueryCriteriaRuleType.None;
        public QueryCriteriaRuleType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _sqlStatement = "";
        [XCData(true)]
        public string SQLStatement
        {
            get { return _sqlStatement; }
            set { _sqlStatement = value; }
        }
    }
}
