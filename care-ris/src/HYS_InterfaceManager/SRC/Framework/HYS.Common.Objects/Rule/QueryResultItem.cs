using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Rule
{
    public class QueryResultItem : MappingItem
    {
        public QueryResultItem()
        {
        }

        public QueryResultItem(string targetfield, string fixValue)
            : base( targetfield, fixValue )
        {
        }
        
        public QueryResultItem(string targetfield, GWDataDBField sourcefield)
            : base( targetfield, sourcefield )
        {
        }

        public QueryResultItem(string targetfield, GWDataDBField sourcefield, string lutName)
            : base(targetfield, sourcefield, lutName)
        {
        }

        public QueryResultItem(string targetfield, GWDataDBField sourcefield, bool redundancyFlag)
            : base(targetfield, sourcefield, redundancyFlag)
        {
        }

        public QueryResultItem(string targetfield, GWDataDBField sourcefield, string lutName, bool redundancyFlag)
            : base(targetfield, sourcefield, lutName, redundancyFlag)
        {
        }

        public QueryResultItem(GWDataDBField targetfield, string sourcefield)
            : base(targetfield, sourcefield)
        {
        }

        public QueryResultItem(GWDataDBField targetfield, string sourcefield, string lutName)
            : base(targetfield, sourcefield, lutName)
        {
        }

        public QueryResultItem(GWDataDBField targetfield, string sourcefield, bool redundancyFlag)
            : base(targetfield, sourcefield, redundancyFlag)
        {
        }

        public QueryResultItem(GWDataDBField targetfield, string sourcefield, string lutName, bool redundancyFlag)
            : base(targetfield, sourcefield, lutName, redundancyFlag)
        {
        }
    }
}
