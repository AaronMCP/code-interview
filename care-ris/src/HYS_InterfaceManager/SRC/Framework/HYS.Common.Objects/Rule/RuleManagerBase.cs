using System;
using System.IO;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class RuleManagerBase<TC, TR>
        where TC : QueryCriteriaItem
        where TR : QueryResultItem
    {
        protected Type _ruleType = typeof(RuleBase<TC, TR>);
        protected RuleBase<TC, TR> _Rule = new RuleBase<TC, TR>();
        protected string _FileName = "Rule.xml";

        public void Load()
        {
            using (StreamReader sr = File.OpenText(_FileName))
            {
                string str = sr.ReadToEnd();
                _Rule = XObjectManager.CreateObject(str, _ruleType) as RuleBase<TC, TR>;
            }
        }

        public void Save()
        {
            using (StreamWriter sw = File.CreateText(_FileName))
            {
                string str = _Rule.ToXMLString();
                str = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + str;
                sw.Write(str);
            }
        }
    }
}
