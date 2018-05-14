using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using HYS.IM.Common.Logging;
using HYS.IM.Common.WCFHelper;
using HYS.IM.Common.WCFHelper.SwA;
using HYS.IM.Common.WCFHelper.Configurability;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPClient.Controler
{
    [ServiceContract]
    public interface IAbstractContract
    {
        [OperationContract(Action = "*")]    
        Message SendMessage(Message request);
    }

    public class SOAPSender
    {
        private readonly ILog _log;
        private readonly string _configFile;

        public SOAPSender(string cfgFile, ILog log)
        {
            _log = log;
            _configFile = cfgFile;
        }

        public bool SendMessage(string uri, string action, string request, ref string response)
        {
            bool res = false;
            if (action == null) return res;
            if (uri == null || uri.Length < 1) return res;
            if (request == null || request.Length < 1) return res;

            string soapInfo = string.Format("sending SOAP message to uri: {0} with action: {1}", uri, action);
            _log.Write("Begin " + soapInfo);

            try
            {
                //using (ChannelFactory<IAbstractContract> factory = new ChannelFactory<IAbstractContract>("ABSTRACT_CLIENT_ENDPOINT"))
                using (ConfigurableChannelFactory<IAbstractContract> factory = new ConfigurableChannelFactory<IAbstractContract>(_configFile))
                {
                    IAbstractContract proxy = factory.CreateChannel(new EndpointAddress(uri));
                    using (proxy as IDisposable)
                    {
                        using (OperationContextScope sc = new OperationContextScope(proxy as IContextChannel))
                        {
                            using (Message wcfRequest = SoapMessageHelper.CreateEmptyWCFMessage(
                                                                            SoapEnvelopeVersion.Soap11,
                                                                            WSAddressingVersion.None,
                                                                            action))
                            {
                                OperationContext.Current.OutgoingMessageProperties.Add(SwaEncoderConstants.SoapEnvelopeProperty, request);
                                using (Message wcfResponse = proxy.SendMessage(wcfRequest))
                                {
                                    response = SoapMessageHelper.DumpWCFMessage(wcfResponse);
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
