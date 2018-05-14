using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace HYS.IM.Common.WCFHelper.Filter
{
    public class CustomEndpointBehavior : IEndpointBehavior
    {
        private MessageFilter _addressFilter;
        private MessageFilter _contractFilter;

        public CustomEndpointBehavior(MessageFilter addressFilter, MessageFilter contractFilter)
        {
            _addressFilter = addressFilter;
            _contractFilter = contractFilter;
        }

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.AddressFilter = _addressFilter;
            endpointDispatcher.ContractFilter = _contractFilter;
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion
    }
}
