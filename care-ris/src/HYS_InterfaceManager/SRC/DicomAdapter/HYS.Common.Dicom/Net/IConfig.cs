using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Dicom.Net
{
    public interface IConfig
    {
        string ImplementationClassUID { get;set;}
        string ImplementationVersion { get;set;}
        int MaxPduLength { get;set;}
        int AssociationTimeOut { get;set;}
        int SessionTimeOut { get;set;}
        string AETitle { get;set;}
    }
}
