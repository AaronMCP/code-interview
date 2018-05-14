using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Objects.Logging;
using HYS.IM.Common.WCFHelper.Configurability;
using System.ServiceModel;
using System.ServiceModel.Channels;
using HYS.IM.Common.WCFHelper;
using HYS.IM.Common.WCFHelper.SwA;

namespace HYS.Common.Soap
{
    [ServiceContract]
    public interface IAbstractClientContract
    {
        [OperationContract(Action = "*")]
        Message SendMessage(Message request);
    }

    public class SOAPClient
    {
        private readonly ILogging _log;
        private readonly string _configFile;

        public SOAPClient(string cfgFile, ILogging log)
        {
            _log = log;
            _configFile = cfgFile;
        }

        public bool SendMessage(string uri, string action, string requestSOAPEnvelope, out string responseSOAPEnvelope)
        {
            bool res = false;
            responseSOAPEnvelope = null;
            if (action == null) return res;
            if (uri == null || uri.Length < 1) return res;
            if (requestSOAPEnvelope == null || requestSOAPEnvelope.Length < 1) return res;

            string soapInfo = string.Format("sending SOAP message to uri: {0} with action: {1}", uri, action);
            _log.Write("Begin " + soapInfo);

            try
            {
                //using (ChannelFactory<IAbstractClientContract> factory = new ChannelFactory<IAbstractClientContract>("ABSTRACT_CLIENT_ENDPOINT"))
                using (ConfigurableChannelFactory<IAbstractClientContract> factory = new ConfigurableChannelFactory<IAbstractClientContract>(_configFile))
                {
                    IAbstractClientContract proxy = factory.CreateChannel(new EndpointAddress(uri));
                    using (proxy as IDisposable)
                    {
                        using (OperationContextScope sc = new OperationContextScope(proxy as IContextChannel))
                        {
                            using (Message wcfRequest = SoapMessageHelper.CreateEmptyWCFMessage(
                                                                            SoapEnvelopeVersion.Soap11,
                                                                            WSAddressingVersion.None,
                                                                            action))
                            {
                                OperationContext.Current.OutgoingMessageProperties.Add(SwaEncoderConstants.SoapEnvelopeProperty, requestSOAPEnvelope);
                                using (Message wcfResponse = proxy.SendMessage(wcfRequest))
                                {
                                    responseSOAPEnvelope = SoapMessageHelper.DumpWCFMessage(wcfResponse);
                                    res = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                if (_log != null) _log.Write(err);
                res = false;
            }

            _log.Write(string.Format("End {0}. Result: {1}", soapInfo, res));
            return res;
        }
    }
}
