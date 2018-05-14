using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HYS.IM.Messaging.Base
{
    [ServiceContract(Namespace = "http://www.carestream.com/StatusNotifier")]
    public interface INotifyAdapterStatus
    {
        [OperationContract]
        void NotifyStatusChanged(string strServiceName, int status);
    }
}
