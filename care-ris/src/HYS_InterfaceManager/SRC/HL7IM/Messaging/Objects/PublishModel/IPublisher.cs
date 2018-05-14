using System;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    public interface IPublisher
    {
        //PublicationDescription GetDescription();
        event MessagePublishHandler OnMessagePublish;
    }

    public delegate bool MessagePublishHandler(Message message);

    // Return value means whether message has been successfully pushed into publication route/channel.
    // It means subscriber has successfully received the message (for LPC) or will successfully received the message sooner or later (for MSMQ).
    // It does not means subscriber has successfully processed the message.
    // 20081017
}
