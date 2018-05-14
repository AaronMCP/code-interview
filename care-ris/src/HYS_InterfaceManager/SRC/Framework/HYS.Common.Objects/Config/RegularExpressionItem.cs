using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HYS.Common.Objects.Logging;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Config
{
    public class RegularExpressionItem : XObject
    {
        private string _regularExpression;
        [XCData(true)]
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Regular expression be used to perform the replacement.")]
        public string Expression
        {
            get { return _regularExpression; }
            set { _regularExpression = value; }
        }

        private string _replacementString;
        [XCData(true)]
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Replacement string be used to perform the replacement.")]
        public string Replacement
        {
            get { return _replacementString; }
            set { _replacementString = value; }
        }

        private string _description;
        [XCData(true)]
        [Category("Will be modified by Adapter.Config when installing an interface on IM. Don't need to be modified when composing a device.")]
        [Description("Description of the replacement.")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public RegularExpressionItem Clone()
        {
            RegularExpressionItem item = new RegularExpressionItem();
            item.Replacement = Replacement;
            item.Description = Description;
            item.Expression = Expression;
            return item;
        }

        public string Replace(string sourceString)
        {
            return Replace(sourceString, null);
        }
        public string Replace(string sourceString, ILogging log)
        {
            try
            {
                if (sourceString == null) return "";

                //log.Write(sourceString);

                //log.Write("Pattern:" + Expression + ";Replacement:" + Replacement);

                string str =  Regex.Replace(sourceString, Expression, Replacement);

                //log.Write(str);

                return str;
            }
            catch (Exception err)
            {
                if (log != null) log.Write(LogType.Error, err.ToString());
                return sourceString;
            }
        }
    }
}
