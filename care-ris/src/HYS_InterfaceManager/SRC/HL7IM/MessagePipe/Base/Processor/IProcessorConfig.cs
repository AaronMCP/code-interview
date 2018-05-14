using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HYS.Common.Logging;

namespace HYS.MessageDevices.MessagePipe.Base.Processor
{
    public interface IProcessorConfig
    {
        bool Initialize(ConfigurationInitializationParameter parameter);
        bool CreateConfig(Form parentForm, out string configXmlString);
        bool EditConfig(Form parentForm, ref string configXmlString);
        bool Uninitilize();
    }
}
