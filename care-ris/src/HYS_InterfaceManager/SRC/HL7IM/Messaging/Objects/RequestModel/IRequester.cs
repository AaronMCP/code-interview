using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Objects.RequestModel
{
    public interface IRequester
    {
        //RequestDescription GetDescription();
        event MessageRequestHandler OnMessageRequest;
    }

    public delegate bool MessageRequestHandler(IPullRoute route, Message request, out Message response);

    // here use RequestRule instead of PullChannel, in order to seperate logical and physical implementations
    // for example, in the future multiple rules/contracts can apply on one channel.   20080815
}
