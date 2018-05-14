using Dicom.Network;
using HYS.Adapter.Base;
using HYS.Common.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.Common.Dicom.Net
{
    public class Modality : XObject
    {
        
        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private string _aeTitle = "";
        public string AETitle
        {
            get { return _aeTitle; }
            set { _aeTitle = value; }
        }

        private string _ipAddress = "";
        public string IPAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public bool Echo(string callingAE)
        {
            return Echo(AETitle, IPAddress, Port, callingAE);
        }
        public static bool Echo(string calledAE, string calledIP, int calledPort, string callingAE)
        {
            return Echo(calledAE, calledIP, calledPort, callingAE, 10000);
        }
        public static bool Echo(string calledAE, string calledIP, int calledPort, string callingAE, int timeOut)
        {
            if (string.IsNullOrEmpty(calledIP) || calledPort < 0) return false;

            int associationTimeOut = timeOut;

            bool result = false;
            try
            {
                var client = new DicomClient();
                client.NegotiateAsyncOps();
                DicomCEchoRequest dicomCEchoRequest = new DicomCEchoRequest();
                dicomCEchoRequest.OnResponseReceived = (DicomCEchoRequest request, DicomCEchoResponse response) =>
                {
                    result = response.Status == DicomStatus.Success;
                };

                client.AddRequest(dicomCEchoRequest);
                client.WaitForAssociation(associationTimeOut);
                client.Send(calledIP, calledPort, false, callingAE, calledAE);             // Alt 1
                client.Release();
            }
            catch (Exception ex)
            {
                LogMgt.Logger.Write("echo calledIP:" + calledIP + " calledPort:" + calledPort + " calledAE:" + calledAE + "error:" + ex.ToString());
            }
            return result;
        }

        public bool Echo2(string callingAE)
        {
            return Echo2(AETitle, IPAddress, Port, callingAE);
        }
        public bool Echo2(IConfig localCfg)
        {
            if (localCfg == null) return false;
            return Echo2(AETitle, IPAddress, Port, localCfg.AETitle, localCfg.AssociationTimeOut, localCfg.ImplementationClassUID, localCfg.ImplementationVersion, localCfg.MaxPduLength);
        }
        public static bool Echo2(string calledAE, string calledIP, int calledPort, string callingAE)
        {
            return Echo2(calledAE, calledIP, calledPort, callingAE, 10000);
        }

        public static bool Echo2(string calledAE, string calledIP, int calledPort, string callingAE, int timeOut)
        {
            return Echo2(calledAE, calledIP, calledPort, callingAE, 10000, "1.2.840.113564.3.1.64", "2.1", 128);
        }

        public static bool Echo2(string calledAE, string calledIP, int calledPort, string callingAE, int timeOut, string implUID, string implVersion, int pduKB)
        {
            if (string.IsNullOrEmpty(calledIP) || calledPort < 0) return false;

            int associationTimeOut = timeOut;

            bool result = false;
            try
            {
                var client = new DicomClient();
                client.NegotiateAsyncOps();
                DicomCEchoRequest dicomCEchoRequest = new DicomCEchoRequest();
                dicomCEchoRequest.OnResponseReceived = (DicomCEchoRequest request, DicomCEchoResponse response) =>
                {
                    result = response.Status == DicomStatus.Success;
                };

                client.AddRequest(dicomCEchoRequest);
                client.WaitForAssociation(associationTimeOut);
                client.Send(calledIP, calledPort, false, callingAE, calledAE);             // Alt 1
                client.Release();
            }
            catch (Exception ex)
            {
                LogMgt.Logger.Write("echo2 calledIP:" + calledIP + " calledPort:" + calledPort + " calledAE:" + calledAE + "error:" + ex.ToString());
            }
            return result;
        }

    }
}
