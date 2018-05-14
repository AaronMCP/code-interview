using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Configuration;
using System.ServiceModel.Dispatcher;
using System.Configuration;

namespace HYS.IM.Common.WCFHelper.Filter
{
    public class ActionMappingEndpointBehaviorExtension : BehaviorExtensionElement
    {
        private class ActionMappingContractFilter : MessageFilter
        {
            private string _fromAction = string.Empty;
            private string _toAction = string.Empty;

            public ActionMappingContractFilter(string fromAction, string toAction)
            {
                _fromAction = fromAction;
                _toAction = toAction;
            }

            public override bool Match(System.ServiceModel.Channels.Message message)
            {
                if (message != null &&
                    message.Headers != null &&
                    (message.Headers.Action == _fromAction || _fromAction == "*"))
                {
                    message.Headers.Action = _toAction; // "urn:SubmitObjectsRequest";
                }

                return true;
            }

            public override bool Match(System.ServiceModel.Channels.MessageBuffer buffer)
            {
                return true;
            }
        }

        public override Type BehaviorType
        {
            get { return typeof(CustomEndpointBehavior); }
        }

        protected override object CreateBehavior()
        {
            MessageFilter addressFilter = new MatchAllMessageFilter();
            MessageFilter contractFilter = new ActionMappingContractFilter(fromAction, toAction);
            CustomEndpointBehavior behavior = new CustomEndpointBehavior(addressFilter, contractFilter);
            return behavior;
        }

        [ConfigurationProperty("fromAction", DefaultValue = "OLD_ACTION", IsRequired = true)]
        public String fromAction
        {
            get
            {
                return base["fromAction"].ToString();
            }
            set
            {
                base["fromAction"] = value;
            }
        }

        [ConfigurationProperty("toAction", DefaultValue = "NEW_ACTION", IsRequired = true)]
        public String toAction
        {
            get
            {
                return base["toAction"].ToString();
            }
            set
            {
                base["toAction"] = value;
            }
        }
    }
}
