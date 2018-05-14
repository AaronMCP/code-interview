using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Queuing.LPC;
using HYS.IM.Common.Logging;
using System.Windows.Forms;

namespace HYS.IM.Messaging.Base.Controler
{
    public class EntityContainer
    {
        private ILog _log;
        public EntityContainer(EntityHostConfig config, ILog log)
        {
            _config = config;
            _log = log;
        }

        private EntityHostConfig _config;
        public EntityHostConfig Config
        {
            get { return _config; }
            set { _config = value; }
        }

        public readonly List<EntityAgent> EntityList = new List<EntityAgent>();
        public EntityAgent FindEntityAgent(Type t)
        {
            if (t == null) return null;
            foreach (EntityAgent a in EntityList)
            {
                //if (a.EntityType.Equals(t)) return a;
                Type et = a.EntityType;
                if (et == null) return null;
                if (et.FullName == t.FullName) return a;
            }
            return null;
        }

        //#region load entity

        //public readonly Dictionary<Guid, EntityAgent> EntityList = new Dictionary<Guid, EntityAgent>();
        
        //private bool LoadEntityList()
        //{
        //    int count = 0;
        //    EntityList.Clear();
        //    foreach (EntityAssemblyConfig cfg in _config.Entities)
        //    {
        //        Guid id = cfg.EntityID;
        //        if (EntityList.ContainsKey(id))
        //        {
        //            _log.Write(LogType.Error, "Message entity has already existed in the list. ID:" + cfg.EntityID);
        //        }
        //        else
        //        {
        //            EntityAgent a = new EntityAgent(cfg, _log);
        //            EntityList.Add(id, a);
        //            count++;
        //        }
        //    }
        //    return count == _config.Entities.Count;
        //}
        //private bool InitializeEntity()
        //{
        //    EntityList.Clear();
        //    EntityDictionary.Entities.Clear();

        //    foreach (EntityAssemblyConfig cfg in _config.Entities)
        //    {
        //        EntityAgent a = new EntityAgent(cfg, _log);

        //        //IMessageEntity2 e = a.EntityInstance;
        //        //if (e == null) return false;

        //        //if (!e.Initialize(cfg.InitializeArgument))
        //        //{
        //        //    _log.Write("Initialize message entity failed. " + a.ToString());
        //        //    return false;
        //        //}

        //        //EntityConfigImpl c = a.EntityConfig;
        //        //if (c == null) return false;

        //        EntityDictionary.Entities.Add(c.EntityID, e);
        //        EntityList.Add(a);
        //    }

        //    return EntityList.Count == _config.Entities.Count;

        //    //if (!LoadEntityList()) return false;

        //    //EntityDictionary.Entities.Clear();
        //    //foreach (KeyValuePair<Guid, EntityAgent> p in EntityList)
        //    //{
        //    //    EntityAgent a = p.Value;
        //    //    IMessageEntity2 e = a.EntityInstance;
        //    //    if (e == null) return false;

        //    //    if (!e.Initialize())
        //    //    {
        //    //        _log.Write("Initialize message entity failed. " + a.ToString());
        //    //        return false;
        //    //    }

        //    //    EntityDictionary.Entities.Add(p.Key, e);
        //    //}

        //    //return true;
        //}
        //private bool InitializeInteraction()
        //{
        //    foreach (EntityAgent a in EntityList)
        //    {
        //        a.InitializePublisherAgent();
        //        a.InitializeSubscriberAgent();
        //    }

        //    return true;

        //    //foreach (KeyValuePair<Guid, EntityAgent> p in EntityList)
        //    //{
        //    //    EntityAgent a = p.Value;
        //    //    a.InitializePublisherAgent();
        //    //    a.InitializeSubscriberAgent();
        //    //}

        //    //return true;
        //}
        //private bool UninitializeInteraction()
        //{
        //    foreach (EntityAgent a in EntityList)
        //    {
        //        a.UnintializePublisherAgent();
        //        a.UninitializeSubscriberAgent();
        //    }

        //    return true;
        //}

        //#endregion

        //#region compose subscribe/publish relation

        //public class PushPair
        //{
        //    public Guid PublisherID;
        //    public Guid SubscriberID;
        //}

        //public readonly List<PushPair> PushList = new List<PushPair>();

        //private void FindSubscribePublish()
        //{
        //    PushList.Clear();
        //    foreach (KeyValuePair<Guid, EntityAgent> p in EntityList)
        //    {
        //        InteractionTypes t = p.Value.EntityAttribute.Interaction;
        //        if ((t & InteractionTypes.Publisher) == InteractionTypes.Publisher)
        //        {
        //            EntityConfigImpl cfg = p.Value.EntityConfig;
        //            PublishConfig pcfg = cfg.PublishConfig;

        //            if (pcfg == null)
        //            {
        //                _log.Write(LogType.Error, "Cannot find publication configuration for message entity. ID:" + p.Value);
        //                continue;
        //            }

        //            foreach (PublishSenderChannel chn in pcfg.Channels)
        //            {
        //                if (chn.ProtocolType != ProtocolType.LPC) continue;

        //                LPCSenderParameter param = chn.LPCParameter;
        //                if (param == null)
        //                {
        //                    _log.Write(LogType.Error, "Cannot find LPC publication sender parameter for message entity. ID:" + p.Value);
        //                    continue;
        //                }

        //                Guid sID = param.EntityID;
        //                if (!EntityList.ContainsKey(sID))
        //                {
        //                    _log.Write(LogType.Error, "Cannot find subscriber ID: " + sID.ToString() + " indicated by  LPC publication sender parameter for message entity. ID:" + p.Value);
        //                    continue;
        //                }

        //                PushPair pair = new PushPair();
        //                pair.PublisherID = p.Key;
        //                pair.SubscriberID = sID;
        //                PushList.Add(pair);
        //            }
        //        }
        //    }
        //}
        //internal void ComposeSubscribePublish()
        //{
        //    FindSubscribePublish();

        //    foreach (PushPair p in PushList)
        //    {
        //        EntityAgent pAgent = EntityList[p.PublisherID];
        //        EntityAgent sAgent = EntityList[p.SubscriberID];

        //        IPublisher pEntity = pAgent.EntityInstance as IPublisher;

        //        if (pEntity == null)
        //        {
        //            _log.Write(LogType.Error, "Cannot load publisher instance from message entity. ID:" + p.PublisherID);
        //            continue;
        //        }

        //        ISubscriber sEntity = sAgent.EntityInstance as ISubscriber;

        //        if (sEntity == null)
        //        {
        //            _log.Write(LogType.Error, "Cannot load subscriber instance from message entity. ID:" + p.SubscriberID);
        //            continue;
        //        }

        //        pEntity.RegisterSubscriber(sEntity);

        //        _log.Write(LogType.Information, "Combining publisher (" + pAgent.ToString() + ") and subscriber (" + sAgent.ToString() + ") succeeded.");
        //    }
        //}

        //#endregion

        //#region compose request/response relation

        //public class PullPair
        //{
        //    public Guid RequesterID;
        //    public Guid ResponserID;

        //    internal IRequester Requester;
        //    internal IResponser Responser;
            
        //    internal void Combine()
        //    {
        //        Requester.OnMessageRequest += new MessageRequestHandler(Requester_OnMessageRequest);
        //    }
        //    internal void Uncombine()
        //    {
        //        Requester.OnMessageRequest -= new MessageRequestHandler(Requester_OnMessageRequest);
        //    }

        //    private bool Requester_OnMessageRequest(Message request, ref Message response)
        //    {
        //        return Responser.ProcessMessage(request, ref response);
        //    }
        //}

        //public readonly List<PullPair> PullList = new List<PullPair>();

        //private void FindRequestResponse()
        //{
        //    PullList.Clear();
        //    foreach (KeyValuePair<Guid, EntityAgent> p in EntityList)
        //    {
        //        InteractionTypes t = p.Value.EntityAttribute.Interaction;
        //        if ((t & InteractionTypes.Requester) == InteractionTypes.Requester)
        //        {
        //            EntityConfigImpl cfg = p.Value.EntityConfig;
        //            RequestConfig rcfg = cfg.RequestConfig;

        //            if (rcfg == null)
        //            {
        //                _log.Write(LogType.Error, "Cannot find request configuration for message entity. ID:" + p.Value);
        //                continue;
        //            }

        //            foreach (RequestChannel chn in rcfg.Channels)
        //            {
        //                if (chn.ProtocolType != ProtocolType.LPC) continue;

        //                LPCParameter param = chn.LPCParameter;
        //                if (param == null)
        //                {
        //                    _log.Write(LogType.Error, "Cannot find LPC request parameter for message entity. ID:" + p.Value);
        //                    continue;
        //                }

        //                Guid sID = param.EntityID;
        //                if (!EntityList.ContainsKey(sID))
        //                {
        //                    _log.Write(LogType.Error, "Cannot find responser ID: " + sID.ToString() + " indicated by  LPC request parameter for message entity. ID:" + p.Value);
        //                    continue;
        //                }

        //                PullPair pair = new PullPair();
        //                pair.RequesterID = p.Key;
        //                pair.ResponserID = sID;
        //                PullList.Add(pair);
        //            }
        //        }
        //    }
        //}
        //internal void ComposeRequestResponse()
        //{
        //    FindRequestResponse();

        //    foreach (PullPair p in PullList)
        //    {
        //        EntityAgent rqAgent = EntityList[p.RequesterID];
        //        EntityAgent rspAgent = EntityList[p.ResponserID];

        //        IRequester rqEntity = rqAgent.EntityInstance as IRequester;

        //        if (rqEntity == null)
        //        {
        //            _log.Write(LogType.Error, "Cannot load requester instance from message entity. ID:" + p.RequesterID);
        //            continue;
        //        }

        //        IResponser rspEntity = rspAgent.EntityInstance as IResponser;

        //        if (rspEntity == null)
        //        {
        //            _log.Write(LogType.Error, "Cannot load responser instance from message entity. ID:" + p.ResponserID);
        //            continue;
        //        }

        //        p.Requester = rqEntity;
        //        p.Responser = rspEntity;
        //        p.Combine();

        //        _log.Write(LogType.Information, "Combining requester (" + rqEntity.ToString() + ") and responser (" + rspEntity.ToString() + ") succeeded.");
        //    }
        //}

        //#endregion

        [Obsolete("Recommendation: Runtime Parameter Needed.", false)]
        public bool Initialize()
        {
            return Initialize(null, null);
        }

        public bool Initialize(LogConfig log)
        {
            return Initialize(log, null);
        }

        public bool Initialize(string description)
        {
            return Initialize(null, description);
        }

        internal static string BaseDirectory = Application.StartupPath;
        
        public bool Initialize(LogConfig log, string description)
        {
            EntityList.Clear();
            EntityDictionary.Entities.Clear();

            foreach (EntityAssemblyConfig cfg in _config.Entities)
            {
                if (!cfg.Enable)
                {
                    _log.Write(LogType.Information, "Skip initializing assembly: " + cfg.EntityInfo.EntityID.ToString());
                    continue;
                }

                EntityAgent a = new EntityAgent(cfg, _log);

                EntityInitializeArgument arg = cfg.InitializeArgument;
                arg.ConfigFilePath = ConfigHelper.GetFullPath(BaseDirectory, arg.ConfigFilePath);
                arg.ConfigFilePath = ConfigHelper.DismissDotDotInThePath(arg.ConfigFilePath);

                _log.Write(LogType.Information, "Try to intialize entity in path: " + arg.ConfigFilePath);

                if (description != null) arg.Description = description;
                if (log != null) arg.LogConfig = log;

                if (a.Initialize(arg))
                {
                    EntityDictionary.Entities.Add(a.EntityConfig.EntityID, a.EntityInstance);
                    EntityList.Add(a);

                    _log.Write(LogType.Information, "Initialize entity succeeded.");
                }
                else
                {
                    _log.Write(LogType.Information, "Initialize entity failed.");
                }
            }

            foreach (EntityAgent a in EntityList) a.InitializeInteraction(InteractionTypes.Subscriber);
            foreach (EntityAgent a in EntityList) a.InitializeInteraction(InteractionTypes.Publisher);
            foreach (EntityAgent a in EntityList) a.InitializeInteraction(InteractionTypes.Responser);
            foreach (EntityAgent a in EntityList) a.InitializeInteraction(InteractionTypes.Requester);

            return EntityList.Count == _config.Entities.Count;
        }

        public bool Start()
        {
            foreach (EntityAgent a in EntityList)
            {
                if (a.Start())
                {
                    _log.Write(LogType.Information, "Start entity succeeded.");
                }
                else
                {
                    _log.Write(LogType.Information, "Start entity failed.");
                }
            }

            return true;
        }

        public bool Stop()
        {
            foreach (EntityAgent a in EntityList)
            {
                if (a.Stop())
                {
                    _log.Write(LogType.Information, "Stop entity succeeded.");
                }
                else
                {
                    _log.Write(LogType.Information, "Stop entity failed.");
                }
            }

            return true;
        }

        public bool Uninitalize()
        {
            foreach (EntityAgent a in EntityList)
            {
                if (a.Uninitialize())
                {
                    _log.Write(LogType.Information, "Uninitialize entity succeeded.");
                }
                else
                {
                    _log.Write(LogType.Information, "Uninitialize entity failed.");
                }
            }
            
            return true;
        }
    }
}
