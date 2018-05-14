using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Rule
{
    public class QueryCriteriaItem : MappingItem
    {
        private QueryCriteriaType _type = QueryCriteriaType.None;
        public QueryCriteriaType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private QueryCriteriaSignal _signal = QueryCriteriaSignal.None;
        public QueryCriteriaSignal Singal
        {
            get { return _signal; }
            set { _signal = value; }
        }

        private QueryCriteriaOperator _operator = QueryCriteriaOperator.Equal;
        public QueryCriteriaOperator Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        public QueryCriteriaItem()
        {
        }

        public QueryCriteriaItem(string targetfield, string fixValue)
            : base( targetfield, fixValue )
        {
        }

        public QueryCriteriaItem(string targetfield, string fixValue, QueryCriteriaType type)
            : base(targetfield, fixValue)
        {
            _type = type;
        }
        
        public QueryCriteriaItem(string targetfield, GWDataDBField sourcefield)
            : base( targetfield, sourcefield )
        {
        }

        public QueryCriteriaItem(string targetfield, GWDataDBField sourcefield, QueryCriteriaType type)
            : base(targetfield, sourcefield)
        {
            _type = type;
        }

        public QueryCriteriaItem(string targetfield, GWDataDBField sourcefield, string lutName)
            : base(targetfield, sourcefield, lutName)
        {
        }

        public QueryCriteriaItem(string targetfield, GWDataDBField sourcefield, string lutName, QueryCriteriaType type)
            : base(targetfield, sourcefield, lutName)
        {
            _type = type;
        }

        public QueryCriteriaItem(string targetfield, GWDataDBField sourcefield, bool redundancyFlag)
            : base(targetfield, sourcefield, redundancyFlag)
        {
        }

        public QueryCriteriaItem(string targetfield, GWDataDBField sourcefield, bool redundancyFlag, QueryCriteriaType type)
            : base(targetfield, sourcefield, redundancyFlag)
        {
            _type = type;
        }

        public QueryCriteriaItem(string targetfield, GWDataDBField sourcefield, string lutName, bool redundancyFlag)
            : base(targetfield, sourcefield, lutName, redundancyFlag)
        {
        }

        public QueryCriteriaItem(string targetfield, GWDataDBField sourcefield, string lutName, bool redundancyFlag, QueryCriteriaType type)
            : base(targetfield, sourcefield, lutName, redundancyFlag)
        {
            _type = type;
        }

        public QueryCriteriaItem(GWDataDBField targetfield, string sourcefield)
            : base(targetfield, sourcefield)
        {
        }

        public QueryCriteriaItem(GWDataDBField targetfield, string sourcefield, QueryCriteriaType type)
            : base(targetfield, sourcefield)
        {
            _type = type;
        }

        public QueryCriteriaItem(GWDataDBField targetfield, string sourcefield, string lutName)
            : base(targetfield, sourcefield, lutName)
        {
        }

        public QueryCriteriaItem(GWDataDBField targetfield, string sourcefield, string lutName, QueryCriteriaType type)
            : base(targetfield, sourcefield, lutName)
        {
            _type = type;
        }

        public QueryCriteriaItem(GWDataDBField targetfield, string sourcefield, bool redundancyFlag)
            : base(targetfield, sourcefield, redundancyFlag)
        {
        }

        public QueryCriteriaItem(GWDataDBField targetfield, string sourcefield, bool redundancyFlag, QueryCriteriaType type)
            : base(targetfield, sourcefield, redundancyFlag)
        {
            _type = type;
        }

        public QueryCriteriaItem(GWDataDBField targetfield, string sourcefield, string lutName, bool redundancyFlag)
            : base(targetfield, sourcefield, lutName, redundancyFlag)
        {
        }

        public QueryCriteriaItem(GWDataDBField targetfield, string sourcefield, string lutName, bool redundancyFlag, QueryCriteriaType type)
            : base(targetfield, sourcefield, lutName, redundancyFlag)
        {
            _type = type;
        }
    }
}
