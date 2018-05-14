using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class OutboundRule<TC, TR> : RuleBase<TC, TR>, IOutboundRule, IRuleSupplier
        where TC : QueryCriteriaItem
        where TR : QueryResultItem
    {
        private bool _autoUpdateProcessFlag;
        [XNode(false)] // is not supported by Rule Engine right now.
        public override bool AutoUpdateProcessFlag
        {
            get { return _autoUpdateProcessFlag; }
            set { _autoUpdateProcessFlag = value; }
        }

        private bool _checkProcessFlag;
        public override bool CheckProcessFlag
        {
            get { return _checkProcessFlag; }
            set { _checkProcessFlag = value; }
        }

        private int _maxRecordCount = 1000;
        public int MaxRecordCount
        {
            get { return _maxRecordCount; }
            set { _maxRecordCount = value; }
        }

        #region IRuleSupplier Members

        public virtual string GetInstallDBScript()
        {
            return "";
        }

        public virtual string GetUninstallDBScript()
        {
            return "";
        }

        #endregion
    }
}
