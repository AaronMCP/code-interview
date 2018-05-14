using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using HYS.Common.Dicom;
using HYS.Common.Dicom.Net;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Logging;
using HYS.Common.Soap;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.StorageServer.Adapter;
using HYS.DicomAdapter.StorageServer.Objects;
using System.IO;
using Dicom.Network;
using Dicom;
using Dicom.Log;
using HYS.DicomAdapter.MWLServer.Dicom;

namespace HYS.DicomAdapter.StorageServer.Dicom
{
    public class StorageSCP : DicomService, IDicomServiceProvider, IDicomCStoreProvider, IDicomCEchoProvider
    {
        private static DicomTransferSyntax[] AcceptedTransferSyntaxes = new DicomTransferSyntax[] {
				DicomTransferSyntax.ExplicitVRLittleEndian,
				DicomTransferSyntax.ExplicitVRBigEndian,
				DicomTransferSyntax.ImplicitVRLittleEndian
			};

        private static DicomTransferSyntax[] AcceptedImageTransferSyntaxes = new DicomTransferSyntax[] {
				// Lossless
				DicomTransferSyntax.JPEGLSLossless,
				DicomTransferSyntax.JPEG2000Lossless,
				DicomTransferSyntax.JPEGProcess14SV1,
				DicomTransferSyntax.JPEGProcess14,
				DicomTransferSyntax.RLELossless,
			
				// Lossy
				DicomTransferSyntax.JPEGLSNearLossless,
				DicomTransferSyntax.JPEG2000Lossy,
				DicomTransferSyntax.JPEGProcess1,
				DicomTransferSyntax.JPEGProcess2_4,

				// Uncompressed
				DicomTransferSyntax.ExplicitVRLittleEndian,
				DicomTransferSyntax.ExplicitVRBigEndian,
				DicomTransferSyntax.ImplicitVRLittleEndian
			};
        private Session session = null;
        private List<string> _StorageServiceUID = new List<string>();
        public System.Net.IPAddress RemoteIP { get; private set; }
        private HYS.Common.Soap.XMLTransformer _reqTran;
        private SOAPClientEx _SOAPClient;

        public StorageSCP(Stream stream, Logger log)
            : base(stream, log)
        {
            foreach (StorageServiceUID uid in Program.ConfigMgt.Config.StorageServiceUIDs)
            {
                _StorageServiceUID.Add(uid.UID);
            }

            var pi = stream.GetType().GetProperty("Socket", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (pi != null)
            {
                var endPoint = ((System.Net.Sockets.Socket)pi.GetValue(stream, null)).RemoteEndPoint as System.Net.IPEndPoint;
                RemoteIP = endPoint.Address;
            }
            else
            {
                RemoteIP = new System.Net.IPAddress(new byte[] { 127, 0, 0, 1 });
            }
            _SOAPClient = new SOAPClientEx(Program.ConfigMgt.Config.SOAPClientSetting, Application.StartupPath, Program.Log);
            _reqTran = HYS.Common.Soap.XMLTransformer.CreateFromFile(ConfigHelper.GetFullPath(Application.StartupPath, Program.ConfigMgt.Config.XSLTFileToTransformDICOMtoSOAP), Program.Log);
        }

        public void OnReceiveAssociationRequest(DicomAssociation association)
        {
            if (association.CalledAE != Program.ConfigMgt.Config.SCPConfig.AETitle)
            {
                SendAssociationReject(DicomRejectResult.Permanent, DicomRejectSource.ServiceUser, DicomRejectReason.CalledAENotRecognized);
                return;
            }

            session = new Session(association.CalledAE, RemoteIP.ToString(), association.CallingAE, 1);
            foreach (var pc in association.PresentationContexts)
            {
                if (pc.AbstractSyntax == DicomUID.Verification)
                    pc.AcceptTransferSyntaxes(AcceptedTransferSyntaxes);
                else if (pc.AbstractSyntax.StorageCategory != DicomStorageCategory.None)
                    pc.AcceptTransferSyntaxes(AcceptedImageTransferSyntaxes);

                if (_StorageServiceUID.Contains(pc.AbstractSyntax.UID))
                {
                    pc.SetResult(DicomPresentationContextResult.Accept);
                }
            }

            SendAssociationAccept(association);
        }

        public void OnReceiveAssociationReleaseRequest()
        {
            SendAssociationReleaseResponse();
        }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
        }

        public void OnConnectionClosed(Exception exception)
        {
        }

        public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
        {
            string prefix = "[STORAGESCP] [" + session + "] ";
            var studyUid = request.Dataset.Get<string>(DicomTag.StudyInstanceUID);
            var instUid = request.SOPInstanceUID.UID;

            Program.Log.Write(prefix + "Begin processing c-store request. " + request.SOPClassUID.UID);

            bool res = true;
             DElementList iod = new DElementList(request.Dataset);

            if (Program.ConfigMgt.Config.SOAPEnable) //SOAP communication, 20121017
            {
                string requestDataSet = iod.ToDicomXMLString();
                string requestSoap = string.Empty;
                string responseSoap = string.Empty;

                string tick = DateTime.Now.Ticks.ToString();

                res = _reqTran.TransformString(requestDataSet, ref requestSoap);

                if (!res)
                {
                    Program.Log.Write(LogType.Error, "Transforming DICOM message to SOAP failed. ");
                    return new DicomCStoreResponse(request, DicomStatus.ProcessingFailure);
                }

                res = _SOAPClient.SendMessage(requestSoap, out responseSoap);

                if (!res)
                {
                    Program.Log.Write(LogType.Error, "Sending SOAP message failed. ");
                    return new DicomCStoreResponse(request, DicomStatus.ProcessingFailure);
                }
            }
            else
            {
                if (StorageSCPService.Service == null)
                {
                    Program.Log.Write(prefix + "[WARNING] SCP is not binded to GC Gateway, dump data to local folder only.");
                    iod.SaveXmlFile("StorageIOD_" + DateTime.Now.Ticks.ToString() + ".xml");
                }
                else
                {
                    DataSet dsIOD = DicomMappingHelper.CreateQCDataSet
                        <StorageItem>(StorageSCPService.Service.StorageList, request.Dataset);

                    if (dsIOD == null)
                    {
                        Program.Log.Write(prefix + "[WARNING] Process storage IOD failed.");
                        return new DicomCStoreResponse(request, DicomStatus.ProcessingFailure);
                    }

                    res = StorageSCPService.Service.SaveData(Program.ConfigMgt.Config.StorageRule, dsIOD);

                    if (!res)
                    {
                        Program.Log.Write(prefix + "[WARNING] Insert GC Gateway database failed.");
                        return new DicomCStoreResponse(request, DicomStatus.ProcessingFailure);
                    }
                }
            }
            Program.Log.Write("[STORAGESCP] [" + session + "] Finish processing c-store request. Result:" + res.ToString());

            return new DicomCStoreResponse(request, DicomStatus.Success);
        }

        public void OnCStoreRequestException(string tempFileName, Exception e)
        {
            // let library handle logging and error response
        }

        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }
    }
}
