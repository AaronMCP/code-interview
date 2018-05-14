using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    public enum PublishResultType
    {
        NoChannel,
        NotMatched,
        SendFailed,
        SendSucceeded,
    }

    // Do not use these classes.
    // Please try to avoid many "new" keywords (initialize a object) on message processing code

    //public class PublishResult
    //{
    //    private List<PublishResultItem> _items = new List<PublishResultItem>();
    //    public List<PublishResultItem> Items
    //    {
    //        get { return _items; }
    //        set { _items = value; }
    //    }
    //}

    //public class PublishResultItem
    //{
    //    private string _pushRouteID = "";
    //    public string PushRouteID
    //    {
    //        get { return _pushRouteID; }
    //        set { _pushRouteID = value; }
    //    }

    //    private Guid _subscriberID;
    //    public Guid SubscriberID
    //    {
    //        get { return _subscriberID; }
    //        set { _subscriberID = value; }
    //    }

    //    private PublishResultType _type;
    //    public PublishResultType Type
    //    {
    //        get { return _type; }
    //        set { _type = value; }
    //    }
    //}
}
