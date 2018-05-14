using System;
using System.Text;
using System.Collections.Generic;
//using HYS.IM.Common.DataAccess;
using HYS.IM.Common.Logging;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class SolutionConfig : XObject
    {
        public const string SolutionDirFileName = "SolutionDir.xml";

        public static SolutionConfig GetDefault()
        {
            SolutionConfig c = new SolutionConfig();

            c.SolutionVersion = "1.0";

            c.DBParameter.OSQLFileName = @"osql\osql.exe";

            //c.WebSetting.HomePage.PageUrl = "Main.aspx";
            //c.WebSetting.HomePage.DisplayCaption = "Home";
            //c.WebSetting.Diagrams.DisplayCaption = "Diagrams";
            //c.WebSetting.Entities.DisplayCaption = "Entities";
            //c.WebSetting.Wizards.DisplayCaption = "Wizards";

            return c;
        }

        private Guid _solutionID;
        public Guid SolutionID
        {
            get { return _solutionID; }
            set { _solutionID = value; }
        }

        private string _name = "";
        [XCData(true)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description = "";
        [XCData(true)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _solutionVersion = "";
        [XCData(true)]
        public string SolutionVersion
        {
            get { return _solutionVersion; }
            set { _solutionVersion = value; }
        }

        private string _solutionUpdateDateTime = "";
        [XCData(true)]
        public string SolutionUpdateDateTime
        {
            get { return _solutionUpdateDateTime; }
            set { _solutionUpdateDateTime = value; }
        }

        public EntityContractBase FindEntityByID(Guid id)
        {
            foreach (EntityContractBase e in Entities)
            {
                if (e.EntityID == id) return e;
            }

            return null;
        }
        
        public EntityContractBase FindEntityByName(string name)
        {
            if (name == null) return null;

            foreach (EntityContractBase e in Entities)
            {
                if (e.Name == name) return e;
            }

            return null;
        }

        public void RegisterEntity(EntityContractBase e)
        {
            if (e == null) return;

            foreach (EntityContractBase c in _entities)
            {
                if (c.EntityID == e.EntityID) return;
            }

            _entities.Add(e);
        }

        //public void RegisterEntity(EntityContractBase e, EntityWebConfig cfg)
        //{
        //    if (e == null) return;

        //    foreach (EntityContractBase c in _entities)
        //    {
        //        if (c.EntityID == e.EntityID) return;
        //    }

        //    _entities.Add(e);

        //    //foreach (EntityWebPage w in _webSetting.Entities.Pages)
        //    //{
        //    //    if (w.EntityID == e.EntityID) return;
        //    //}

        //    //EntityWebPage p = new EntityWebPage();
        //    //p.DisplayCaption = e.Name;
        //    //p.PageUrl = "Entity.aspx?name=" + e.Name;
        //    //p.EntityName = e.Name;
        //    //p.EntityID = e.EntityID;

        //    //CustomizedWebPage pConfig = new CustomizedWebPage();
        //    //pConfig.DisplayCaption = "Configuration";
        //    //pConfig.PageUrl = "Config.aspx?name=" + e.Name;
        //    //p.SubPages.Add(pConfig);

        //    //CustomizedWebPage pMonitor = new CustomizedWebPage();
        //    //pMonitor.DisplayCaption = "Monitor";
        //    //pMonitor.PageUrl = "Monitor.aspx?name=" + e.Name;
        //    //p.SubPages.Add(pMonitor);

        //    //if (cfg != null) p.DefaultConfigPageSetting = cfg;

        //    //_webSetting.Entities.Pages.Add(p);
        //}

        public void UnregisterEnity(EntityContractBase e)
        {
            if (e == null) return;

            EntityContractBase ec = null;
            foreach (EntityContractBase c in _entities)
            {
                if (c.EntityID == e.EntityID)
                {
                    ec = c;
                    break;
                }
            }

            if (ec != null) _entities.Remove(ec);

            //EntityWebPage ew = null;
            //foreach (EntityWebPage w in _webSetting.Entities.Pages)
            //{
            //    if (w.EntityID == e.EntityID)
            //    {
            //        ew = w;
            //        break;
            //    }
            //}

            //if (ew != null) _webSetting.Entities.Pages.Remove(ew);
        }

        private XCollection<EntityContractBase> _entities = new XCollection<EntityContractBase>();
        public XCollection<EntityContractBase> Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }

        private XCollection<NTServiceHostInfo> _hosts = new XCollection<NTServiceHostInfo>();
        public XCollection<NTServiceHostInfo> Hosts
        {
            get { return _hosts; }
            set { _hosts = value; }
        }

        //private XCollection<NTServiceHostInformation> _hosts = new XCollection<NTServiceHostInformation>();
        //public XCollection<NTServiceHostInformation> Hosts
        //{
        //    get { return _hosts; }
        //    set { _hosts = value; }
        //}

        private DataBaseScriptParameter _dbParameter = new DataBaseScriptParameter();
        public DataBaseScriptParameter DBParameter
        {
            get { return _dbParameter; }
            set { _dbParameter = value; }
        }

        private XCollection<MInterface> _interfaces = new XCollection<MInterface>();
        public XCollection<MInterface> Interfaces
        {
            get { return _interfaces; }
            set { _interfaces = value; }
        }

        private SolutionWebSetting _webSetting = new SolutionWebSetting();
        public SolutionWebSetting WebSetting
        {
            get { return _webSetting; }
            set { _webSetting = value; }
        }

        private LogConfig _logConfig = new LogConfig();
        public LogConfig LogConfig
        {
            get { return _logConfig; }
            set { _logConfig = value; }
        }

        #region For v1.0 Gate 3 Release

        // To simply ISMP for G3 release by exposing/maintaining duplicated NT Service Host of File Outbound

        //private XCollection<NTServiceHostGroup> _hostGroups = new XCollection<NTServiceHostGroup>();
        //public XCollection<NTServiceHostGroup> HostGroups
        //{
        //    get { return _hostGroups; }
        //    set { _hostGroups = value; }
        //}

        //public NTServiceHostGroup FindHostGroupByName(string name)
        //{
        //    if (name == null) return null;

        //    foreach (NTServiceHostGroup g in HostGroups)
        //    {
        //        if (g.GroupName == name) return g;
        //    }

        //    return null;
        //}

        //public bool IsNTServiceHostExist(string interfaceName, string instanceName)
        //{
        //    if (interfaceName == null || instanceName == null) return false;

        //    NTServiceHostGroup g = FindHostGroupByName(interfaceName);
        //    if (g == null)
        //    {
        //        return false;
        //    }

        //    if (g.DefaultNTServiceHostName == instanceName) return true;

        //    NTServiceHostInformation i = g.FindNTServiceHostByName(instanceName);
        //    if (i == null)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //public void RegisterNTServiceHost(string groupName, string hostName)
        //{
        //    if (groupName == null || hostName == null) return;

        //    NTServiceHostGroup g = FindHostGroupByName(groupName);
        //    if (g == null)
        //    {
        //        g = new NTServiceHostGroup();
        //        g.DefaultNTServiceHostName = g.GroupName = groupName;
        //    }

        //    NTServiceHostInformation i = g.FindNTServiceHostByName(hostName);
        //    if (i == null)
        //    {
        //        i = new NTServiceHostInformation();
        //        i.ServiceName = hostName;
        //        g.NTServiceHostList.Add(i);
        //    }
        //}

        //public void UnregisterNTServiceHost(string groupName, string hostName)
        //{
        //    if (groupName == null || hostName == null) return;

        //    NTServiceHostGroup g = FindHostGroupByName(groupName);
        //    if (g == null) return;

        //    NTServiceHostInformation i = g.FindNTServiceHostByName(hostName);
        //    if (i != null) g.NTServiceHostList.Remove(i);
        //}

        #endregion
    }

    public class DataBaseScriptParameter : XObject
    {
        private string _osqlFileName = "C:\\Program Files\\Microsoft SQL Server\\90\\Tools\\Binn\\osql.exe";
        public string OSQLFileName
        {
            get { return _osqlFileName; }
            set { _osqlFileName = value; }
        }

        //private string _osqlArgument = "-S (local) -E";
        private string _osqlArgument = "-S (local)\\SQLExpress -U sa -P 123456";
        public string OSQLArgument
        {
            get { return _osqlArgument; }
            set { _osqlArgument = value; }
        }

        private string _osqlDatabase = "master";
        public string OSQLDatabase
        {
            get { return _osqlDatabase; }
            set { _osqlDatabase = value; }
        }
    }
}
