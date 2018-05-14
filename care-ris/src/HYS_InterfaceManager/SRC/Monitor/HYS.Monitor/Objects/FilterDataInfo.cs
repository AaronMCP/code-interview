using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;

namespace HYS.Adapter.Monitor.Objects
{
    public class FilterDataInfo
    {
        private FilterMode _filterMode = FilterMode.AdvancedListView;
        public FilterMode FilterMode {
            get { return _filterMode; }
            set { _filterMode = value; }
        }

        private SimpleQuery _filterInfo = new SimpleQuery();
        public SimpleQuery FilterInfo {
            get { return _filterInfo; }
            set { _filterInfo = value; }
        }

        private XCollection<QueryCriteriaItem> _filterItemList = new XCollection<QueryCriteriaItem>();
        public XCollection<QueryCriteriaItem> FilterItemList
        {
            get { return _filterItemList; }
            set { _filterItemList = value; }
        }

        private string _filterText = "";
        public string FilterText{
            get { return _filterText; }
            set { _filterText = value; }
        }
    }
}
