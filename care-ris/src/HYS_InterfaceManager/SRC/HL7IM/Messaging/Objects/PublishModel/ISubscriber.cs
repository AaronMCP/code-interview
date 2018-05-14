using System;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    public interface ISubscriber
    {
        //SubscriptionDescription GetDescription();
        void ReceiveMessage(IPushRoute route, Message message);

        // here use SubscriptionRule instead of PushChannel, in order to seperate logical and physical implementations
        // for example, in the future multiple rules/contracts can apply on one channel.   20080815

        // Once this function is called, it means subscriber has successfully received the message.
        // There is no return value for this function. 
        // Because publisher does not want to know whether the message has been successfully processed when there is no need for retry.
        // If retry is needed, publisher usually cannot know whether the message has been successfully processed through this fucntion's return value either, such as in MSMQ channel. 
        // We do not want to use MSMQ acknownledgement queue here, which will bring complexity and reduce flexibility.
        // It is better for subscriber to use other Push or Pull channel to tell publisher whether the message has been successfully processed.
        // 20081017
    }
}
