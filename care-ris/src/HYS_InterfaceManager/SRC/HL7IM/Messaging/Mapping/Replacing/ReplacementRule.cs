using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using System.Text.RegularExpressions;

namespace HYS.IM.Messaging.Mapping.Replacing
{
    public class ReplacementRule : XObject
    {
        [XCData(true)]
        public string MatchExpression { get; set; }
        [XCData(true)]
        public string ReplaceExpression { get; set; }
        [XCData(true)]
        public string Description { get; set; }

        /// <summary>
        /// Replace string with regular expression. If error, regular expression exception will be thrown out.
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public string Replace(string originalString)
        {
            return Regex.Replace(originalString, MatchExpression, ReplaceExpression);
        }
    }
}
