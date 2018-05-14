using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Objects.Device;

namespace HYS.IM.BusinessEntity
{
    public interface IDevice
    {
        string FolderPath {get;}
        DeviceDir Directory { get;}
    }
}
