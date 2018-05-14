using Dicom;
using Dicom.Network;
using HYS.DicomAdapter.StorageServer;
using HYS.DicomAdapter.StorageServer.Adapter;
using HYS.DicomAdapter.StorageServer.Dicom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.DicomAdapter.MWLServer.Dicom
{
    public class StorageSCPService
    {
        private DicomServer<StorageSCP> server;
        public static ServiceMain Service;

        public bool Start()
        {
            server = new DicomServer<StorageSCP>(Program.ConfigMgt.Config.SCPConfig.Port);
            return true;
        }

        public bool Stop()
        {
            server.Dispose();
            return true;
        }

        public StorageSCPService(ServiceMain workListService)
        {
            Service = workListService;
            DicomEncoding.Default = Encoding.GetEncoding("GB18030");
        }

		}
}