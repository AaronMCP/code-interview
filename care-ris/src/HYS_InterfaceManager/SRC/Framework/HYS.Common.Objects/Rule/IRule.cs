using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Rule
{
    public interface IRule
    {
        /// <summary>
        /// Identification of this data processing rule. 
        /// This identification should be different between different devices.
        /// This identification will be used to name storage procedure in GC Gateway database.
        /// </summary>
        string RuleID { get;}

        /// <summary>
        /// Name of this data processing rule.
        /// </summary>
        string RuleName { get;}
    }
}
