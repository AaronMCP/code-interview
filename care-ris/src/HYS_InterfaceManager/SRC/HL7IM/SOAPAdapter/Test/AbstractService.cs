using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using HYS.Common.WCFHelper;
using System.ServiceModel;
using HYS.Common.WCFHelper.Configurability;

namespace HYS.MessageDevices.SOAPAdapter.Test
{
    public class AbstractService : IAbstractContract
    {
        private FormSOAPServer _server;
        public AbstractService()
        {
            ConfigurableServiceHost host = OperationContext.Current.Host as ConfigurableServiceHost;
            if (host == null)
                throw new ArithmeticException("Cannot find ConfigurableServiceHost in current operation context.");

            _server = host.Tag as FormSOAPServer;
            if (_server == null)
                throw new ArithmeticException("Cannot find FormSOAPServer in current operation context.");
        }

        public Message SendMessage(Message request)
        {
            return null;
        }

        public Message ProcessMessage(Message request)
        {
            string req = SoapMessageHelper.DumpWCFMessage(request);
            _server.DisplayRequestMessage(req);

            //Message msg = SoapMessageHelper.CreateEmptyWCFMessage(
            //    SoapEnvelopeVersion.Soap11,
            //    WSAddressingVersion.None,
            //    "SomeReplyAction");

            string response = _server._response;
            Message msg = SoapMessageHelper.CreateWCFMessageWithEnvelope(
                SoapEnvelopeVersion.Soap11,
                WSAddressingVersion.None,
                "*", response);

            return msg;
        }
    }
}
