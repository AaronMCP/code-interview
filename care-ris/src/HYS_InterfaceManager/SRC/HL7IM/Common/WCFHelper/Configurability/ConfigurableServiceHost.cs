using System;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace HYS.IM.Common.WCFHelper.Configurability
{
    /// <summary>
    /// A custom service host which takes in a path to a 
    /// custom configuration file
    /// </summary>
    public class ConfigurableServiceHost : ServiceHost
    {
        private string _configurationPath;

        public ConfigurableServiceHost(string configurationPath, Type serviceType, params Uri[] baseAddresses)
        {
            _configurationPath = configurationPath;
            base.InitializeDescription(serviceType, new UriSchemeKeyedCollection(baseAddresses));
        }
        public ConfigurableServiceHost(string configurationPath, object singeltonInstance, params Uri[] baseAddresses)
        {
            _configurationPath = configurationPath;
            base.InitializeDescription(singeltonInstance, new UriSchemeKeyedCollection(baseAddresses));
        }

        protected override void ApplyConfiguration()
        {
            ExeConfigurationFileMap filemap = new ExeConfigurationFileMap();
            filemap.ExeConfigFilename = _configurationPath;

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(filemap, ConfigurationUserLevel.None);
            ServiceModelSectionGroup serviceModel = ServiceModelSectionGroup.GetSectionGroup(config);

            bool loaded = false;
            foreach (ServiceElement se in serviceModel.Services.Services)
            {
                if (!loaded &&
                    se.Name == this.Description.ConfigurationName)
                {
                    base.LoadConfigurationSection(se);
                    loaded = true;
                }
            }

            if (!loaded)
            {
                throw new ArgumentException(string.Format("ServiceElement doesn't exist in the configuration file: {0}", _configurationPath));
            }
        }

        public object Tag { get; set; }
    }
}
