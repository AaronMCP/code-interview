using AutoUpdaterDotNET;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Hys.CareAgent.Main
{
    public class CustomHeaderMessageInspector : IDispatchMessageInspector
    {
        Dictionary<string, string> requiredHeaders;
        public CustomHeaderMessageInspector(Dictionary<string, string> headers)
        {
            requiredHeaders = headers ?? new Dictionary<string, string>();
        }

        private bool versionOld(string requestVersion)
        {
            if (string.IsNullOrEmpty(requestVersion))
            {
                return false;
            }

            System.Version currentVersion;

            try
            {
                currentVersion = new System.Version(requestVersion);
            }
            catch
            {
                return false;
            }

            var installedVersion = Assembly.GetExecutingAssembly().GetName().Version;

            return installedVersion < currentVersion;
        }

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            HttpRequestMessageProperty httpRequestHeader = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
            var version = httpRequestHeader.Headers["Version"];
            
            if (versionOld(version))
            {
                var updateUrl = httpRequestHeader.Headers["Update"];
                AutoUpdater.Start(updateUrl);
                instanceContext.Abort();
            }

            if (httpRequestHeader.Method.ToUpper() == "OPTIONS")
            {
                instanceContext.Abort();
            }

            return httpRequestHeader;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            HttpRequestMessageProperty httpRequestHeader = correlationState as HttpRequestMessageProperty;
            HttpResponseMessageProperty httpResponseHeader = reply.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;

            foreach (KeyValuePair<string, string> item in this.requiredHeaders)
            {
                httpResponseHeader.Headers.Add(item.Key, item.Value);
            }

            string method = httpRequestHeader.Method;
            if (method.ToUpper() == "OPTIONS")
            {
                httpResponseHeader.StatusCode = HttpStatusCode.OK;
            }
        }
    }
}
