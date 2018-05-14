using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Mapping
{
    public class DuplexProcessConfig : XObject
    {
        private OneWayProcessConfig _preProcessConfig = new OneWayProcessConfig();
        public OneWayProcessConfig PreProcessConfig
        {
            get { return _preProcessConfig; }
            set { _preProcessConfig = value; }
        }

        private OneWayProcessConfig _postProcessConfig = new OneWayProcessConfig();
        public OneWayProcessConfig PostProcessConfig
        {
            get { return _postProcessConfig; }
            set { _postProcessConfig = value; }
        }
    }
}
