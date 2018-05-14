using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Mapping.Replacing
{
    /// <summary>
    /// This registry contains frequently used regular expressions for string replacement.
    /// </summary>
    public static class ReplacementRuleRegistry
    {
        private static XCollection<ReplacementRule> _rules;
        public static XCollection<ReplacementRule> Rules
        {
            get
            {
                lock (typeof(ReplacementRuleRegistry))
                {
                    if (_rules != null) return _rules;
                    return _rules = CreateDefaultReplacementRuleList();
                }
            }
        }

        /// <summary>
        /// You can create a replacement rule list in your configuration file for user to select.
        /// And use the this function to initialize it as default configuration.
        /// </summary>
        /// <returns></returns>
        public static XCollection<ReplacementRule> CreateDefaultReplacementRuleList()
        {
            XCollection<ReplacementRule> l = new XCollection<ReplacementRule>();

            l.Add(new ReplacementRule()
            {
                MatchExpression = @"(^\s*)|(\s*$)",
                ReplaceExpression = "",
                Description = "Trim blank on the both sides of a string."
            });

            l.Add(new ReplacementRule()
            {
                MatchExpression = @"[\^]",
                ReplaceExpression = " ",
                Description = "Replace ^ with blank in person name."
            });
            l.Add(new ReplacementRule()
            {
                MatchExpression = @"\b(?<family>\w+)\^(?<given>\w+)\b",
                ReplaceExpression = "${given}^${family}",
                Description = "Transform person name from Family Name^Given Name to Given Name^Family Name."
            });

            l.Add(new ReplacementRule()
            {
                MatchExpression = "[/]",
                ReplaceExpression = "-",
                Description = "Replace / with - in date time string."
            });
            l.Add(new ReplacementRule()
            {
                MatchExpression = @"\b(?<month>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})\b",
                ReplaceExpression = "${year}-${month}-${day}",
                Description = "Transform date time from MM/DD/YYYY to YYYY-MM-DD."
            });
            l.Add(new ReplacementRule()
            {
                MatchExpression = @"\b(?<year>\d{2,4})(?<month>\d{1,2})(?<day>\d{1,2})\b",
                ReplaceExpression = "${year}-${month}-${day}",
                Description = "Transform date time from YYYYMMDD to YYYY-MM-DD."
            });
            l.Add(new ReplacementRule()
            {
                MatchExpression = @"\b(?<year>\d{2,4})(?<month>\d{1,2})(?<day>\d{1,2})(?<hour>\d{1,2})(?<minute>\d{1,2})(?<second>\d{1,2})\b",
                ReplaceExpression = "${year}-${month}-${day} ${hour}:${minute}:${second}",
                Description = "Transform date time from YYYYMMDDHHMMSS to YYYY-MM-DD HH:MM:SS."
            });
            l.Add(new ReplacementRule()
            {
                MatchExpression = @"\b(?<year>\d{2,4})(?<month>\d{1,2})(?<day>\d{1,2})(?<hour>\d{1,2})(?<minute>\d{1,2})(?<second>\d{1,2}).(?<fractal>\d{1,3})\b",
                ReplaceExpression = "${year}-${month}-${day} ${hour}:${minute}:${second}",
                Description = "Transform date time from YYYYMMDDHHMMSS.FFF to YYYY-MM-DD HH:MM:SS."
            });

            return l;
        }
    }
}
