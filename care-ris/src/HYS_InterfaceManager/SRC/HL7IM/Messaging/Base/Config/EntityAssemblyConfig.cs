using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class EntityAssemblyConfig : XObject
    {
        //private Guid _entityID;
        //public Guid EntityID
        //{
        //    get { return _entityID; }
        //    set { _entityID = value; }
        //}

        //private string _entityName = "";
        //public string EntityName
        //{
        //    get { return _entityName; }
        //    set { _entityName = value; }
        //}

        private bool _enable = true;
        /// <summary>
        /// To be used by the EntityContainer class to determine whether the actually load the assembly.
        /// </summary>
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        private string _className = "";
        [XCData(true)]
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        
        private string _assemblyLocation = "";
        [XCData(true)]
        public string AssemblyLocation
        {
            get { return _assemblyLocation; }
            set { _assemblyLocation = value; }
        }

        private EntityInitializeArgument _initializeArgument = new EntityInitializeArgument();
        public EntityInitializeArgument InitializeArgument
        {
            get { return _initializeArgument; }
            set { _initializeArgument = value; }
        }

        private EntityInformation _entityInfo = new EntityInformation();
        public EntityInformation EntityInfo
        {
            get { return _entityInfo; }
            set { _entityInfo = value; }
        }

        //private string _configFileLocation = "";
        //public string ConfigFileLocation
        //{
        //    get { return _configFileLocation; }
        //    set { _configFileLocation = value; }
        //}
    }
}
