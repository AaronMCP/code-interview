using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace HYS.IM.Common.WCFHelper.Configurability
{
    public class ConfigurationHelper
    {
        public static string[] GetEndpointNameFromClientConfig(string wcfConfigFile)
        {
            if (!File.Exists(wcfConfigFile)) return null;

            ExeConfigurationFileMap executionFileMap = new ExeConfigurationFileMap();
            executionFileMap.ExeConfigFilename = wcfConfigFile;

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(executionFileMap, ConfigurationUserLevel.None);
            ServiceModelSectionGroup serviceModeGroup = ServiceModelSectionGroup.GetSectionGroup(config);

            List<string> elist = new List<string>();
            foreach (ChannelEndpointElement endpoint in serviceModeGroup.Client.Endpoints)
            {
                elist.Add(endpoint.Name);
            }

            return elist.ToArray();
        }
    }
}
