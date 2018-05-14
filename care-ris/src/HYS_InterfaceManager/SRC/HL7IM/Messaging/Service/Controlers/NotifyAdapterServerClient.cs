﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Globalization;
using HYS.IM.Messaging.Base;

namespace HYS.IM.Messaging.Service.Controlers
{
    public class NotifyAdapterServerClient
    {
        private Uri _serviceUri = null;
        private string _pipeName = "StatusNotifier";
        private EndpointAddress _serviceAddress = null;
        private INotifyAdapterStatus _erviceProxy = null;

        public NotifyAdapterServerClient(string strServerName)
        {
            _pipeName = strServerName;
            _serviceUri = new Uri("net.pipe://localhost/" + _pipeName + "/");
            _serviceAddress = new EndpointAddress(string.Format(CultureInfo.InvariantCulture, "{0}{1}", _serviceUri.OriginalString, _pipeName));
            _erviceProxy = ChannelFactory<INotifyAdapterStatus>.CreateChannel(new NetNamedPipeBinding(), _serviceAddress);
        }
        public void NotifyStatusChanged(string strServiceName, int Status)
        {
            using (_erviceProxy as IDisposable)
            {
                _erviceProxy.NotifyStatusChanged(strServiceName, Status);
            }
        }
    }
}
