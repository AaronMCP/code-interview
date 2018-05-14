using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.XDS
{
    public class XdsRegistryErrorList
    {
        public string HighestSeverity { get; set; }

        private List<XdsRegistryError> _registryErrors = null;
        public List<XdsRegistryError> RegistryErrors
        {
            get
            {
                if (_registryErrors == null)
                {
                    _registryErrors = new List<XdsRegistryError>();
                }

                return _registryErrors;
            }
            set
            {
                _registryErrors = value;
            }
        }
    }
}
