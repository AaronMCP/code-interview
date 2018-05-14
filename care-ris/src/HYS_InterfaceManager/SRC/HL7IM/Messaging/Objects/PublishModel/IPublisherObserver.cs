using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    /// <summary>
    /// Considering of avoiding "new" keywords (initialize a object) on message processing code (for performance),
    /// we do not want to return a publication result object by IPublisher.OnMessagePublish().
    /// Instead, we use IPublisherObserver to return publication result.
    /// It is also good for doing some AOP things.
    /// 
    /// For simplification, assume that IPublisherObserver should be implemented by a class that has already implemented IPublisher.
    /// </summary>
    public interface IPublisherObserver
    {
        void PublishingMessage(PublishResultType type, Message message, IPushRoute route);
    }
}
