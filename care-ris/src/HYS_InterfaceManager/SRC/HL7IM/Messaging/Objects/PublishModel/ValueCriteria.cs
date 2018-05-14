using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    [Obsolete("Do not use this class for message routing between entities, use HYS.IM.Messaging.Objects.RoutingModel.ContentCriteria instead", false)]
    public class ValueCriteria :XObject
    {
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _xPath = "";
        public string XPath
        {
            get { return _xPath; }
            set { _xPath = value; }
        }

        private string _pattern = "";
        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        private ValueCriteriaOperator _operator;
        public ValueCriteriaOperator Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        private ValueCriteriaJoinType _joinType;
        public ValueCriteriaJoinType JoinType
        {
            get { return _joinType; }
            set { _joinType = value; }
        }
    }
}
