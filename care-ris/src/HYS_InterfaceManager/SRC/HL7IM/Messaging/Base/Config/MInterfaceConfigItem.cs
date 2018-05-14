using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class MInterfaceConfigItem : XObject
    {
        private string _caption = "";
        [XCData(true)]
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        private string _prompt = "";
        [XCData(true)]
        public string Prompt
        {
            get { return _prompt; }
            set { _prompt = value; }
        }

        private string _xpath = "";
        [XCData(true)]
        public string XPath
        {
            get { return _xpath; }
            set { _xpath = value; }
        }

        private bool _useCDataTag;
        public bool UseCDataTag
        {
            get { return _useCDataTag; }
            set { _useCDataTag = value; }
        }

        private bool _enableValidation;
        public bool EnableValidation
        {
            get { return _enableValidation; }
            set { _enableValidation = value; }
        }

        private string _validationRegularExpression = "";
        [XCData(true)]
        public string ValidationRegularExpression
        {
            get { return _validationRegularExpression; }
            set { _validationRegularExpression = value; }
        }
    }
}
