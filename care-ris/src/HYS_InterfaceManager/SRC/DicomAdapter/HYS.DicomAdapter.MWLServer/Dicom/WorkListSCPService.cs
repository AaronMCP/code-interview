using Dicom;
using Dicom.Network;
using HYS.DicomAdapter.MWLServer.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.DicomAdapter.MWLServer.Dicom
{
    public class WorkListSCPService
    {
        private DicomServer<WorklistSCP> server;
        public static ServiceMain Service;

        public bool Start()
        {
            server = new DicomServer<WorklistSCP>(Program.ConfigMgt.Config.SCPConfig.Port);
            return true;
        }

        public bool Stop()
        {
            server.Dispose();
            return true;
        }

        public WorkListSCPService(ServiceMain workListService)
        {
            Service = workListService;
            DicomEncoding.Default = Encoding.GetEncoding("GB18030");
        }

		}
}