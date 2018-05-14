using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class InboundRule<TC, TR> : RuleBase<TC, TR>, IInboundRule, IRuleSupplier
        where TC : QueryCriteriaItem
        where TR : QueryResultItem
    {
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
