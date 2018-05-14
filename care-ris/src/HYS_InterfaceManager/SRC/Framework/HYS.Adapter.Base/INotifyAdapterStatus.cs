using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HYS.Adapter.Base
{
    [ServiceContract(Namespace = "http://www.HaoYiSheng.com/StatusNotifier")]
    public interface INotifyAdapterStatus
    {
        [OperationContract]
        void NotifyStatusChanged(int interfaceID, int status);
    }
}
