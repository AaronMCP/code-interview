using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    /// <summary>
    /// Used to create a storage procedure
    /// 
    /// - mapping / translating (share)
    /// - query criteria (for outbound only)
    /// - redundancy filter (for inbound only)
    /// </summary>
    public class RuleBase<TC,TR> : XObject, IRule
        where TC : QueryCriteriaItem
        where TR : QueryResultItem
    {
        private QueryCriteriaRule<TC> _queryCriteriaRule = new QueryCriteriaRule<TC>();
        public QueryCriteriaRule<TC> QueryCriteria
        {
            get { return _queryCriteriaRule; }
            set { _queryCriteriaRule = value; }
        }

        private QueryResultRule<TR> _queryResultRule = new QueryResultRule<TR>();
        public QueryResultRule<TR> QueryResult
        {
            get { return _queryResultRule; }
            set { _queryResultRule = value; }
        }

        public RuleBase()
        {
            _ruleID = RuleControl.GetRandomNumber();
        }

        #region IRule members

        private string _ruleID;
        public string RuleID
        {
            get { return _ruleID; }
            set { _ruleID = value; }
        }

        private string _ruleName;
        public string RuleName
        {
            get { return _ruleName; }
            set { _ruleName = value; }
        }

        public QueryResultItem[] GetQueryResultItems()
        {
            List<QueryResultItem> mList = new List<QueryResultItem>();
            foreach (TR mpi in QueryResult.MappingList)
            {
                mList.Add(mpi);
            }
            return mList.ToArray();
        }

        public QueryCriteriaItem[] GetQueryCriteriaItems()
        {
            List<QueryCriteriaItem> mList = new List<QueryCriteriaItem>();
            foreach (TC mpi in QueryCriteria.MappingList)
            {
                mList.Add(mpi);
            }
            return mList.ToArray();
        }

        [XNode(false)]
        public QueryCriteriaRuleType QueryCriteriaRuleType
        {
            get
            {
                return QueryCriteria.Type;
            }
        }

        [XNode(false)]
        public virtual bool AutoUpdateProcessFlag
        {
            get { return false; }
            set { }
        }

        [XNode(false)]
        public virtual bool CheckProcessFlag
        {
            get { return false; }
            set { }
        }

        #endregion
    }
}
