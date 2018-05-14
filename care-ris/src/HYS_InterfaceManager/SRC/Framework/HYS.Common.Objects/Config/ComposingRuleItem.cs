using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Objects.Config
{
    public class ComposingRuleItem : GWDataDBField
    {
        private XCollection<GWDataDBField> _fromFields = new XCollection<GWDataDBField>();
        public XCollection<GWDataDBField> FromFields
        {
            get { return _fromFields; }
            set { _fromFields = value; }
        }

        private string _composePattern = "";
        [XCData(true)]
        public string ComposePattern
        {
            get { return _composePattern; }
            set { _composePattern = value; }
        }

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public ComposingRuleItem()
        {
        }
        public ComposingRuleItem(GWDataDBField field)
            : base(field.FieldName, field.Table, field.IsAuto)
        {
        }
        public ComposingRuleItem(string field, GWDataDBTable table, bool isAuto)
            : base(field, table, isAuto)
        {
        }

        public new ComposingRuleItem Clone()
        {
            ComposingRuleItem item = new ComposingRuleItem(FieldName, Table, IsAuto);
            item.FromFields = FromFields.Copy();
            item.ComposePattern = ComposePattern;
            item.Description = Description;
            return item;
        }

        public string Compose(string[] sourceString)
        {
            return Compose(sourceString, null);
        }
        public string Compose(string[] sourceString, ILogging log)
        {
            try
            {
                if (sourceString == null) return "";
                return string.Format(ComposePattern, sourceString);
            }
            catch (Exception err)
            {
                if (log != null) log.Write(LogType.Error, err.ToString());
                return "";
            }
        }
    }
}
