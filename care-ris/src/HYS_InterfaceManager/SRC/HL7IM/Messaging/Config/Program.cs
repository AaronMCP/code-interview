using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Forms;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base.Controler;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Mapping;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Messaging.Objects.RoutingModel;
using System.Reflection;

namespace HYS.IM.Messaging.Config
{
    static class Program
    {
        public const string AppName = "EntityConfigHostConfig";
        //public const string SolutionDirFileName = @"../" + SolutionConfig.SolutionDirFileName;
        public const string SolutionDirFileName = "../../" + SolutionConfig.SolutionDirFileName; //20100419

        public static LogControler Log;
        public static ConfigManager<EntityConfigHostConfig> ConfigMgt;
        public static ConfigManager<SolutionConfig> SolutionMgt;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //if (args != null && args.Length > 0 && args[0] == "-set") createDefaultConfigSilently = true;

            if (PreLoading(args))
            {
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

                if (!HandleArguments(args))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain());
                }
            }

            BeforeExit();
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                Log.Write(LogType.Warning, "Failed to resolve: " + args.Name);
                string fname = args.Name.Substring(0, args.Name.IndexOf(',')) + ".dll";
                string fpath = ConfigHelper.DismissDotDotInThePath(EntityLoader.LoadingAssemblyFileName);
                fpath = Path.GetDirectoryName(fpath);
                fname = Path.Combine(fpath, fname);
                Log.Write(LogType.Warning, "Try to resolve: " + fname);

                return Assembly.LoadFile(fname);
            }
            catch (Exception err)
            {
                Log.Write(err);
                return null;
            }
        }

        static bool HandleArguments(string[] args)
        {
            if (args == null || args.Length < 1) return false;

            foreach (string arg in args)
            {
                switch (arg)
                {
                    case "-c":  // select entity
                        {
                            Program.Log.Write("Begin selecting message entity into configuration GUI host.");
                            ShowEntityAssemblyConfig();
                            Program.Log.Write("End selecting message entity into configuration GUI host.");
                            return true;
                        }
                    case "-r":  // register entity into solution
                        {
                            Program.Log.Write("Begin registering message entity into integration solution.");
                            RegisterEntityIntoSolution();
                            Program.Log.Write("End registering message entity into integration solution.");
                            return true;
                        }
                    case "-u":  // unregister entity from solution
                        {
                            Program.Log.Write("Begin unregistering message entity into integration solution.");
                            UnregisterEntityFromSolution();
                            Program.Log.Write("End unregistering message entity into integration solution.");
                            return true;
                        }
                    case "-a":  // apply channel configuration to other entities
                        {
                            Program.Log.Write("Begin applying channel configuration to other message entities.");
                            ApplyChannelConfigurations(true);
                            Program.Log.Write("End applying channel configuration to other message entities.");
                            return true;
                        }
                    case "-add":    // add publisher or responser and apply configuration to it
                        {
                            Program.Log.Write("Begin adding channel and apply to other message entities.");
                            AddAndApplyChannel(args);
                            Program.Log.Write("End adding channel and apply to other message entities.");
                            return true;
                        }
                    case "-del":    // delete publisher or responser 
                        // (without apply configuration to publisher/responser, which should manually delete on its config GUI,
                        // because the publisher or responser is usually be unregistered from solution and its folder be deleted)
                        {
                            Program.Log.Write("Begin deleting channel without apply to other message entities.");
                            DeleteChannel(args);
                            Program.Log.Write("End deleting channel without apply to other message entities.");
                            return true;
                        }
                    //case "-set":   // set entity assembly config into the config GUI host configuration
                    //    {
                    //        Program.Log.Write("Begin setting entity assembly config.");
                    //        SetEntityAssemblyConfig(args);
                    //        Program.Log.Write("End setting entity assembly config.");
                    //        return true;
                    //    }
                }
            }

            return false;
        }

        static void ShowEntityAssemblyConfig()
        {
            try
            {
                FormEntity<MessageEntityConfigEntryAttribute, IMessageEntityConfig> frm = new FormEntity<MessageEntityConfigEntryAttribute, IMessageEntityConfig>(Log, true);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    EntityAssemblyConfig e = frm.Entity;
                    if (e != null)
                    {
                        ConfigMgt.Config.EntityAssembly = e;
                        if (ConfigMgt.Save())
                        {
                            Log.Write("Save config file succeeded. " + ConfigMgt.FileName);
                        }
                        else
                        {
                            Log.Write(LogType.Error, "Save config file failed. " + ConfigMgt.FileName);
                            Log.Write(ConfigMgt.LastError);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Log.Write(err);
            }
        }

        static void RegisterEntityIntoSolution()
        {
            try
            {
                EntityAssemblyConfig entity = Program.ConfigMgt.Config.EntityAssembly;
                foreach (EntityContractBase e in SolutionMgt.Config.Entities)
                {
                    if (e.EntityID == entity.EntityInfo.EntityID)
                    {
                        MessageBox.Show("Following message entity has already existed in the integration solution.\r\n\r\n"
                                    + entity.EntityInfo.Name + " (" + entity.EntityInfo.EntityID + ")",
                                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                EntityConfigAgent agent = new EntityConfigAgent(Program.ConfigMgt.Config.EntityAssembly, Program.Log);
                if (agent.Initialize(Program.ConfigMgt.Config.EntityAssembly.InitializeArgument))
                {
                    EntityConfigBase cfg = agent.EntityConfig;
                    EntityContractBase c = new EntityContractBase();

                    c.Name = cfg.Name;
                    c.DeviceName = cfg.DeviceName;
                    c.Direction = cfg.Direction;
                    c.Interaction = cfg.Interaction;
                    c.Description = cfg.Description;
                    c.EntityID = cfg.EntityID;
                    if (cfg.PublishConfig != null) c.Publication = cfg.PublishConfig.Publication;
                    if (cfg.ResponseConfig != null) c.ResponseDescription = cfg.ResponseConfig.ResponseContract;
                    c.AssemblyConfig = GetEntityAssemblyConfigForSolution(entity);

                    //string webConfigFilePath = Path.Combine(Application.StartupPath, EntityWebConfig.EntityWebConfigFileName);
                    //ConfigManager<EntityWebConfig> webmgt = new ConfigManager<EntityWebConfig>(webConfigFilePath);
                    //if (webmgt.Load())
                    //{
                    //    Log.Write("Register entity with default web config at: " + webConfigFilePath);
                    //    SolutionMgt.Config.RegisterEntity(c, webmgt.Config);
                    //}
                    //else
                    //{
                    //    Log.Write("Register entity without default web config.");
                    //    SolutionMgt.Config.RegisterEntity(c);
                    //}

                    Log.Write("Registering entity.");
                    SolutionMgt.Config.RegisterEntity(c);

                    if (SolutionMgt.Save())
                    {
                        Log.Write("Save solution dir file succeeded. " + SolutionMgt.FileName);

                        MessageBox.Show("Register following message entity into the integration solution succeeded.\r\n\r\n"
                                    + entity.EntityInfo.Name + " (" + entity.EntityInfo.EntityID + ")",
                                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Log.Write(LogType.Error, "Save solution dir file failed.");
                        Log.Write(SolutionMgt.LastError);

                        MessageBox.Show("Register following message entity into the integration solution failed.\r\n\r\n"
                                    + entity.EntityInfo.Name + " (" + entity.EntityInfo.EntityID + ")",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    agent.Uninitialize();
                }
            }
            catch (Exception err)
            {
                Log.Write(err);
            }
        }

        static void UnregisterEntityFromSolution()
        {
            try
            {
                EntityAssemblyConfig entity = Program.ConfigMgt.Config.EntityAssembly;

                EntityContractBase contract = null;
                foreach (EntityContractBase e in SolutionMgt.Config.Entities)
                {
                    if (e.EntityID == entity.EntityInfo.EntityID)
                    {
                        contract = e;
                        break;
                    }
                }

                if (contract == null)
                {
                    MessageBox.Show("Following message entity does not exist in the integration solution.\r\n\r\n"
                                    + entity.EntityInfo.Name + " (" + entity.EntityInfo.EntityID + ")",
                                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    //SolutionMgt.Config.Entities.Remove(contract);
                    SolutionMgt.Config.UnregisterEnity(contract);

                    if (SolutionMgt.Save())
                    {
                        Log.Write("Save solution dir file succeeded. " + SolutionMgt.FileName);

                        MessageBox.Show("Unregister following message entity from the integration solution succeeded.\r\n\r\n"
                                    + entity.EntityInfo.Name + " (" + entity.EntityInfo.EntityID + ")",
                                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Log.Write(LogType.Error, "Save solution dir file failed.");
                        Log.Write(SolutionMgt.LastError);

                        MessageBox.Show("Unregister following message entity from the integration solution failed.\r\n\r\n"
                                    + entity.EntityInfo.Name + " (" + entity.EntityInfo.EntityID + ")",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception err)
            {
                Log.Write(err);
            }
        }

        static void AddAndApplyChannel(string[] args)
        {
            int count = 6;
            if (args.Length < count)
            {
                Program.Log.Write("Arguement is not enough.");
                return;
            }

            try
            {
                string type = args[1];
                string entityID = args[2];
                string entityName = args[3];
                string messageType = args[4];
                string protocolType = args[5];

                switch (type.ToLowerInvariant())
                {
                    case "publisher":
                        {
                            MessageType mt = MessageType.Parse(messageType);
                            if (mt == null)
                            {
                                Program.Log.Write("Message Type can not be parse from: " + messageType);
                                return;
                            }

                            PushChannelConfig c = new PushChannelConfig();
                            c.ReceiverEntityID = Program.ConfigMgt.Config.EntityAssembly.EntityInfo.EntityID;
                            c.ReceiverEntityName = Program.ConfigMgt.Config.EntityAssembly.EntityInfo.Name;
                            c.ProtocolType = (ProtocolType)Enum.Parse(typeof(ProtocolType), protocolType);
                            c.Subscription.Type = RoutingRuleType.MessageType;
                            c.Subscription.MessageTypeList.Add(mt);
                            c.SenderEntityID = new Guid(entityID);
                            c.SenderEntityName = entityName;

                            switch (c.ProtocolType)
                            {
                                case ProtocolType.LPC: ChannelHelper.GenerateLPCChannel(c); break;
                                case ProtocolType.MSMQ: ChannelHelper.GenerateMSMQChannel(c); break;
                                default: Program.Log.Write("Following protocol type is not supported by now: " + c.ProtocolType.ToString()); return;
                            }

                            EntityConfigAgent agent = new EntityConfigAgent(Program.ConfigMgt.Config.EntityAssembly, Program.Log);
                            if (agent.Initialize(Program.ConfigMgt.Config.EntityAssembly.InitializeArgument))
                            {
                                EntityConfigBase cfg = agent.EntityConfig;
                                if (cfg == null)
                                {
                                    Program.Log.Write("Cannot read configuration file.");
                                }
                                else
                                {
                                    if (cfg.SubscribeConfig == null) cfg.SubscribeConfig = new SubscribeConfig();
                                    cfg.SubscribeConfig.Channels.Add(c);

                                    if (agent.EntityConfigInstance.SaveConfiguration())
                                    {
                                        Program.Log.Write("Write configuration file successfully.");
                                    }
                                    else
                                    {
                                        Program.Log.Write("Cannot write configuration file.");
                                    }
                                }
                                agent.Uninitialize();
                            }
                            break;
                        }
                    case "responser":
                        {
                            PullChannelConfig c = new PullChannelConfig();
                            c.ReceiverEntityID = Program.ConfigMgt.Config.EntityAssembly.EntityInfo.EntityID;
                            c.ReceiverEntityName = Program.ConfigMgt.Config.EntityAssembly.EntityInfo.Name;
                            c.ProtocolType = (ProtocolType)Enum.Parse(typeof(ProtocolType), protocolType);
                            c.SenderEntityID = new Guid(entityID);
                            c.SenderEntityName = entityName;

                            switch (c.ProtocolType)
                            {
                                case ProtocolType.LPC: ChannelHelper.GenerateLPCChannel(c); break;
                                default: Program.Log.Write("Following protocol type is not supported by now: " + c.ProtocolType.ToString()); return;
                            }

                            EntityConfigAgent agent = new EntityConfigAgent(Program.ConfigMgt.Config.EntityAssembly, Program.Log);
                            if (agent.Initialize(Program.ConfigMgt.Config.EntityAssembly.InitializeArgument))
                            {
                                EntityConfigBase cfg = agent.EntityConfig;
                                if (cfg == null)
                                {
                                    Program.Log.Write("Cannot read configuration file.");
                                }
                                else
                                {
                                    if (cfg.RequestConfig == null) cfg.RequestConfig = new RequestConfig();
                                    cfg.RequestConfig.Channels.Add(c);

                                    if (agent.EntityConfigInstance.SaveConfiguration())
                                    {
                                        Program.Log.Write("Write configuration file successfully.");
                                    }
                                    else
                                    {
                                        Program.Log.Write("Cannot write configuration file.");
                                    }
                                }
                                agent.Uninitialize();
                            }
                            break;
                        }
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
            }

            ApplyChannelConfigurations(false);
        }

        static void ApplyChannelConfigurations(bool showMsg)
        {
            try
            {
                EntityConfigAgent agent = new EntityConfigAgent(Program.ConfigMgt.Config.EntityAssembly, Program.Log);
                if (agent.Initialize(Program.ConfigMgt.Config.EntityAssembly.InitializeArgument))
                {
                    EntityConfigBase cfg = agent.EntityConfig;

                    if ((cfg.Interaction & InteractionTypes.Subscriber) == InteractionTypes.Subscriber)
                    {
                        bool res = true;
                        foreach (PushChannelConfig chn in cfg.SubscribeConfig.Channels)
                        {
                            if (!UpdateChannelInPublisher(chn))
                            {
                                res = false;
                                break;
                            }
                        }

                        if (res)
                        {
                            string str = "Apply channels to publisher succeeded.";
                            Log.Write(str);

                            if (showMsg)
                                MessageBox.Show(str, "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string str = "Apply channels to publisher failed.";
                            Log.Write(LogType.Error, str);

                            if (showMsg)
                                MessageBox.Show(str, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if ((cfg.Interaction & InteractionTypes.Requester) == InteractionTypes.Requester)
                    {
                        bool res = true;
                        foreach (PullChannelConfig chn in cfg.RequestConfig.Channels)
                        {
                            if (!UpdateChannelInResponser(chn))
                            {
                                res = false;
                                break;
                            }
                        }

                        if (res)
                        {
                            string str = "Apply channels to responser succeeded.";
                            Log.Write(str);

                            if (showMsg)
                                MessageBox.Show(str, "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            string str = "Apply channels to responser failed.";
                            Log.Write(LogType.Error, str);

                            if (showMsg)
                                MessageBox.Show(str, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    //else
                    //{
                    //    MessageBox.Show("No channel need to be applied.", "Information",
                    //        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}

                    agent.Uninitialize();
                }
            }
            catch (Exception err)
            {
                Log.Write(err);
            }
        }

        static bool UpdateChannelInPublisher(PushChannelConfig cfg)
        {
            EntityAssemblyConfig acfg = null;
            foreach (EntityContractBase c in Program.SolutionMgt.Config.Entities)
            {
                if (c.EntityID == cfg.SenderEntityID)
                {
                    acfg = c.AssemblyConfig;
                    break;
                }
            }

            if (acfg == null)
            {
                Program.Log.Write(LogType.Error, "Cannot find publisher in the integration solution: "
                    + cfg.SenderEntityName + " (" + cfg.SenderEntityID + ")");
                return false;
            }

            bool ret = false;
            acfg = GetEntityAssemblyConfigForEntity(acfg);
            EntityConfigAgent agent = new EntityConfigAgent(acfg, Program.Log);
            if (agent.Initialize(acfg.InitializeArgument))
            {
                EntityConfigBase ecfg = agent.EntityConfig;
                if (ecfg == null)
                {
                    Program.Log.Write(LogType.Error, "Cannot get publisher configuration: "
                    + cfg.SenderEntityName + " (" + cfg.SenderEntityID + ")");
                }
                else
                {
                    List<PushChannelConfig> deleteList = new List<PushChannelConfig>();
                    foreach (PushChannelConfig chn in ecfg.PublishConfig.Channels)
                    {
                        if (chn.ReceiverEntityID == cfg.ReceiverEntityID)
                        {
                            deleteList.Add(chn);
                            break;
                        }
                    }

                    foreach (PushChannelConfig chn in deleteList)
                        ecfg.PublishConfig.Channels.Remove(chn);

                    ecfg.PublishConfig.Channels.Add(cfg);

                    if (agent.EntityConfigInstance.SaveConfiguration())
                    {
                        ret = true;
                        Program.Log.Write("Save publisher configuration succeeded: "
                                              + cfg.SenderEntityName + " (" + cfg.SenderEntityID + ")");
                    }
                    else
                    {
                        Program.Log.Write(LogType.Error, "Save publisher configuration failed: "
                                          + cfg.SenderEntityName + " (" + cfg.SenderEntityID + ")");
                    }
                }

                agent.Uninitialize();
            }

            return ret;
        }

        static bool UpdateChannelInResponser(PullChannelConfig cfg)
        {
            EntityAssemblyConfig acfg = null;
            foreach (EntityContractBase c in Program.SolutionMgt.Config.Entities)
            {
                if (c.EntityID == cfg.ReceiverEntityID)
                {
                    acfg = c.AssemblyConfig;
                    break;
                }
            }

            if (acfg == null)
            {
                Program.Log.Write(LogType.Error, "Cannot find responser in the integration solution: "
                    + cfg.ReceiverEntityName + " (" + cfg.ReceiverEntityID + ")");
                return false;
            }

            bool ret = false;
            acfg = GetEntityAssemblyConfigForEntity(acfg);
            EntityConfigAgent agent = new EntityConfigAgent(acfg, Program.Log);
            if (agent.Initialize(acfg.InitializeArgument))
            {
                EntityConfigBase ecfg = agent.EntityConfig;
                if (ecfg == null)
                {
                    Program.Log.Write(LogType.Error, "Cannot get responser configuration: "
                    + cfg.ReceiverEntityName + " (" + cfg.ReceiverEntityID + ")");
                }
                else
                {
                    List<PullChannelConfig> deleteList = new List<PullChannelConfig>();
                    foreach (PullChannelConfig chn in ecfg.ResponseConfig.Channels)
                    {
                        if (chn.SenderEntityID == cfg.SenderEntityID)
                        {
                            deleteList.Add(chn);
                            break;
                        }
                    }

                    foreach (PullChannelConfig chn in deleteList)
                        ecfg.ResponseConfig.Channels.Remove(chn);

                    ecfg.ResponseConfig.Channels.Add(cfg);

                    if (agent.EntityConfigInstance.SaveConfiguration())
                    {
                        ret = true;
                        Program.Log.Write("Save responser configuration succeeded: "
                                              + cfg.ReceiverEntityName + " (" + cfg.ReceiverEntityID + ")");
                    }
                    else
                    {
                        Program.Log.Write(LogType.Error, "Save responser configuration failed: "
                                          + cfg.ReceiverEntityName + " (" + cfg.ReceiverEntityID + ")");
                    }
                }

                agent.Uninitialize();
            }

            return ret;
        }

        static void DeleteChannel(string[] args)
        {
            int count = 3;
            if (args.Length < count)
            {
                Program.Log.Write("Arguement is not enough.");
                return;
            }

            try
            {
                string type = args[1];
                string entityID = args[2];
                Guid eID = new Guid(entityID);

                switch (type.ToLowerInvariant())
                {
                    case "publisher":
                        {
                            EntityConfigAgent agent = new EntityConfigAgent(Program.ConfigMgt.Config.EntityAssembly, Program.Log);
                            if (agent.Initialize(Program.ConfigMgt.Config.EntityAssembly.InitializeArgument))
                            {
                                EntityConfigBase cfg = agent.EntityConfig;
                                if (cfg == null || cfg.SubscribeConfig == null)
                                {
                                    Program.Log.Write("Cannot read SubscribeConfig from configuration file.");
                                }
                                else
                                {
                                    List<PushChannelConfig> dlist = new List<PushChannelConfig>();
                                    foreach (PushChannelConfig c in cfg.SubscribeConfig.Channels) if (c.SenderEntityID == eID) dlist.Add(c);
                                    foreach (PushChannelConfig c in dlist) cfg.SubscribeConfig.Channels.Remove(c);
                                    agent.EntityConfigInstance.SaveConfiguration();
                                }
                                agent.Uninitialize();
                            }
                            break;
                        }
                    case "responser":
                        {
                            EntityConfigAgent agent = new EntityConfigAgent(Program.ConfigMgt.Config.EntityAssembly, Program.Log);
                            if (agent.Initialize(Program.ConfigMgt.Config.EntityAssembly.InitializeArgument))
                            {
                                EntityConfigBase cfg = agent.EntityConfig;
                                if (cfg == null || cfg.RequestConfig == null)
                                {
                                    Program.Log.Write("Cannot read SubscribeConfig from configuration file.");
                                }
                                else
                                {
                                    List<PushChannelConfig> dlist = new List<PushChannelConfig>();
                                    foreach (PushChannelConfig c in cfg.RequestConfig.Channels) if (c.ReceiverEntityID == eID) dlist.Add(c);
                                    foreach (PushChannelConfig c in dlist) cfg.RequestConfig.Channels.Remove(c);
                                    agent.EntityConfigInstance.SaveConfiguration();
                                }
                                agent.Uninitialize();
                            }
                            break;
                        }
                }
            }
            catch (Exception err)
            {
                Log.Write(err);
            }
        }

        //static void SetEntityAssemblyConfig(string[] args)
        //{
        //    int count = 8;
        //    if (args.Length < count)
        //    {
        //        Program.Log.Write("Arguement is not enough.");
        //        return;
        //    }

        //    try
        //    {
        //        string entityID = args[1];
        //        string entityName = args[2];
        //        string deviceName = args[3];
        //        string description = args[4];
        //        string className = args[5];
        //        string assemblyLocation = args[6];
        //        string configFilePath = args[7];

        //        Guid eID = new Guid(entityID);
        //        EntityAssemblyConfig entityCfg = new EntityAssemblyConfig();
        //        entityCfg.ClassName = className;
        //        entityCfg.AssemblyLocation = assemblyLocation;
        //        entityCfg.InitializeArgument.ConfigFilePath = configFilePath;
        //        entityCfg.EntityInfo.EntityID = eID;
        //        entityCfg.EntityInfo.Name = entityName;
        //        entityCfg.EntityInfo.DeviceName = deviceName;
        //        entityCfg.EntityInfo.Description = description;

        //        ConfigMgt.Config.EntityAssembly = entityCfg;

        //        if (ConfigMgt.Save())
        //        {
        //            Log.Write("Set entity assembly config success.");
        //        }
        //        else
        //        {
        //            Log.Write(ConfigMgt.LastError);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        Log.Write(err);
        //    }
        //}

        static EntityAssemblyConfig GetEntityAssemblyConfigForSolution(EntityAssemblyConfig cfg)
        {
            EntityAssemblyConfig c = new EntityAssemblyConfig();

            //string appPath = Application.StartupPath;
            //string folderName = Path.GetFileName(appPath.TrimEnd('\\'));

            string slnDirFileName = ConfigHelper.DismissDotDotInThePath(ConfigHelper.GetFullPath(SolutionMgt.FileName));
            string slnDirPath = Path.GetDirectoryName(slnDirFileName);

            if (Path.IsPathRooted(cfg.AssemblyLocation))
            {
                c.AssemblyLocation = cfg.AssemblyLocation;
            }
            else
            {
                //c.AssemblyLocation = folderName + "\\" + cfg.AssemblyLocation;

                string fullPath = ConfigHelper.DismissDotDotInThePath(ConfigHelper.GetFullPath(cfg.AssemblyLocation));
                string relativePath = ConfigHelper.GetRelativePath(slnDirPath, fullPath);
                c.AssemblyLocation = relativePath;
            }

            if (Path.IsPathRooted(cfg.InitializeArgument.ConfigFilePath))
            {
                c.InitializeArgument.ConfigFilePath = cfg.InitializeArgument.ConfigFilePath;
            }
            else
            {
                //c.InitializeArgument.ConfigFilePath = folderName + "\\" + cfg.InitializeArgument.ConfigFilePath;

                string fullPath = ConfigHelper.DismissDotDotInThePath(ConfigHelper.GetFullPath(cfg.InitializeArgument.ConfigFilePath));
                string relativePath = ConfigHelper.GetRelativePath(slnDirPath, fullPath);
                c.InitializeArgument.ConfigFilePath = relativePath + "\\";
            }

            c.ClassName = cfg.ClassName;
            c.EntityInfo.Name = cfg.EntityInfo.Name;
            c.EntityInfo.EntityID = cfg.EntityInfo.EntityID;
            c.EntityInfo.Description = cfg.EntityInfo.Description;

            return c;
        }

        static EntityAssemblyConfig GetEntityAssemblyConfigForEntity(EntityAssemblyConfig cfg)
        {
            EntityAssemblyConfig c = new EntityAssemblyConfig();

            string appPath = Application.StartupPath;
            string folderName = Path.GetFileName(appPath.TrimEnd('\\'));

            if (Path.IsPathRooted(cfg.AssemblyLocation))
            {
                c.AssemblyLocation = cfg.AssemblyLocation;
            }
            else
            {
                c.AssemblyLocation = "..\\..\\" + cfg.AssemblyLocation;
            }

            if (Path.IsPathRooted(cfg.InitializeArgument.ConfigFilePath))
            {
                c.InitializeArgument.ConfigFilePath = cfg.InitializeArgument.ConfigFilePath;
            }
            else
            {
                c.InitializeArgument.ConfigFilePath = "..\\..\\" + cfg.InitializeArgument.ConfigFilePath;
            }

            c.ClassName = cfg.ClassName;
            c.EntityInfo.Name = cfg.EntityInfo.Name;
            c.EntityInfo.EntityID = cfg.EntityInfo.EntityID;
            c.EntityInfo.Description = cfg.EntityInfo.Description;

            return c;
        }

        static void LoadSolutionDir()
        {
            SolutionMgt = new ConfigManager<SolutionConfig>(ConfigHelper.GetFullPath(SolutionDirFileName));
            if (SolutionMgt.Load())
            {
                Log.Write("Load solution dir file succeeded. " + SolutionMgt.FileName);
            }
            else
            {
                Log.Write(LogType.Error, "Load solution dir file failed.");
                Log.Write(SolutionMgt.LastError);

                if (MessageBox.Show("Cannot load solution dir file from:\r\n" + SolutionMgt.FileName +
                    "\r\n\r\nDo you want to create a solution dir file with default setting and continue?",
                    AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SolutionMgt.Config = new SolutionConfig();
                    if (SolutionMgt.Save())
                    {
                        Log.Write("Create solution dir file succeeded. " + SolutionMgt.FileName);
                    }
                    else
                    {
                        Log.Write(LogType.Error, "Create solution dir file failed. " + SolutionMgt.FileName);
                        Log.Write(SolutionMgt.LastError);
                    }
                }
            }
        }

        //private static bool createDefaultConfigSilently;

        internal static bool PreLoading(string[] args)
        {
            //Log = new LogControler(AppName);
            //LogHelper.EnableApplicationLogging(Log);
            //LogHelper.EnableXmlLogging(Log);
            //Log.WriteAppStart(AppName, args);

            ConfigMgt = new ConfigManager<EntityConfigHostConfig>(EntityConfigHostConfig.ConfigHostConfigFileName);
            if (ConfigMgt.Load())
            {
                Log = new LogControler(AppName, ConfigMgt.Config.LogConfig);
                LogHelper.EnableApplicationLogging(Log);
                LogHelper.EnableXmlLogging(Log);
                Log.WriteAppStart(AppName, args);

                Log.Write("Load config succeeded. " + ConfigMgt.FileName);

                LoadSolutionDir();
                return true;
            }
            else
            {
                Log = new LogControler(AppName);
                LogHelper.EnableApplicationLogging(Log);
                LogHelper.EnableXmlLogging(Log);
                Log.WriteAppStart(AppName, args);

                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                Log.Write(ConfigMgt.LastError);

                if (//createDefaultConfigSilently ||
                    MessageBox.Show("Cannot load " + AppName + " configuration file. \r\n" +
                    ConfigMgt.FileName + "\r\n\r\nDo you want to create a configuration file with default setting and continue?",
                    AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ConfigMgt.Config = new EntityConfigHostConfig();
                    ConfigMgt.Config.LogConfig.HostName = "ConfigGUI";
                    if (ConfigMgt.Save())
                    {
                        Log.Write("Create config file succeeded. " + ConfigMgt.FileName);
                        return true;
                    }
                    else
                    {
                        Log.Write(LogType.Error, "Create config file failed. " + ConfigMgt.FileName);
                        Log.Write(ConfigMgt.LastError);
                        return false;
                    }
                }

                return false;
            }
        }

        internal static void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }
    }
}