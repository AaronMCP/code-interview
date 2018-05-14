using Dicom;
using Dicom.Network;
using Dicom.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using HYS.Common.Objects.Logging;
using HYS.Common.Dicom.Net;
using HYS.DicomAdapter.Common;
using HYS.DicomAdapter.MWLServer.Objects;
using HYS.Common.Dicom;

namespace HYS.DicomAdapter.MWLServer.Dicom
{
    public class WorklistSCP : DicomService, IDicomServiceProvider, IDicomCEchoProvider, IDicomCFindProvider
    {
        private Session session = null;

        public System.Net.IPAddress RemoteIP { get; private set; }

        private static DicomTransferSyntax[] AcceptedTransferSyntaxes = new DicomTransferSyntax[] {
				DicomTransferSyntax.ExplicitVRLittleEndian,
				DicomTransferSyntax.ExplicitVRBigEndian,
				DicomTransferSyntax.ImplicitVRLittleEndian,
			};

        public WorklistSCP(Stream stream, Logger log)
            : base(stream, log)
        {
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
                else if (pc.AbstractSyntax == DicomUID.ModalityWorklistInformationModelFIND)
                    pc.AcceptTransferSyntaxes(AcceptedTransferSyntaxes);

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

        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }

        public IEnumerable<DicomCFindResponse> OnCFindRequest(DicomCFindRequest request)
        {
            string prefix = "[WorklistSCP] [" + session + "] ";
            Program.Log.Write(prefix + "Begin processing c-find request.");

            List<DicomCFindResponse> responses = new List<DicomCFindResponse>();
            if (request.Level == DicomQueryRetrieveLevel.Worklist || request.Level == DicomQueryRetrieveLevel.Patient)
            {
                DataSet dsQC = DicomMappingHelper.CreateQCDataSet
                <MWLQueryCriteriaItem>(WorkListSCPService.Service.QCList, request.Dataset);
                WorklistSCPHelper.ModifyQCDataSet(WorkListSCPService.Service.QCList, dsQC);
                if (dsQC == null)
                {
                    Program.Log.Write(prefix + "[WARNING] Process query criteria failed.");
                    responses.Add(new DicomCFindResponse(request, DicomStatus.QueryRetrieveUnableToProcess));
                    return responses;
                }

                DataSet dsQR = WorkListSCPService.Service.RequestData(Program.ConfigMgt.Config.Rule, dsQC);

                if (dsQR == null)
                {
                    Program.Log.Write(prefix + "[WARNING] Query GC Gateway database failed.");
                    responses.Add(new DicomCFindResponse(request, DicomStatus.QueryRetrieveUnableToProcess));
                    return responses;
                }
                WorklistSCPHelper.GenerateRequestedProcedureID(dsQR, session);

                if (WorklistSCPHelper.SplitDataRow(dsQR))
                {
                    Program.Log.Write(prefix + "[INFORMATION] Found multiple values in code value and performed splitting.");
                }

                //fname = path + "\\DataSet_QR_2_SPLIT_" + DateTime.Now.Ticks.ToString() + ".xml";
                //dsQR.WriteXml(fname);

                DElementListWrapper[] resultList = null;

                if (Program.ConfigMgt.Config.MergeElementList)
                {
                    resultList = WorklistSCPHelper.CreateQRElementList
                        <MWLQueryResultItem>(WorkListSCPService.Service.QRList, dsQR);
                }
                else
                {
                    resultList = DicomMappingHelper.CreateQRElementList
                        <MWLQueryResultItem>(WorkListSCPService.Service.QRList, dsQR);
                }

                if (resultList == null)
                {
                    Program.Log.Write(prefix + "[WARNING] Process query result failed.");
                    responses.Add(new DicomCFindResponse(request, DicomStatus.QueryRetrieveUnableToProcess));
                    return responses;
                }

                int count = resultList.Length;
                if (count < 1)
                {
                    Program.Log.Write(prefix + "[WARNING] No query result.");
                }
                else
                {
                    int index = 1;
                    foreach (DElementListWrapper eleList in resultList)
                    {
                        Program.Log.Write(prefix + "Sending query result " + (index++).ToString() + "/" + count.ToString());

                        DicomDataset result = new DicomDataset();
                        if (Program.ConfigMgt.Config.SendCharacterSetTag)
                        {
                            //eleList.List.Add(DHelper.CharacterSet);
                            if (DHelper.iCharacterSet != null)
                                for (int i = 0; i < DHelper.iCharacterSet.Length; i++)
                                {
                                    eleList.List.Add<string>(DicomTag.SpecificCharacterSet, DHelper.iCharacterSet[i]);
                                }
                        }

                        eleList.List.Add<string>(new DicomTag(0x0000, 0x0002), "1.2.840.10008.5.1.4.31");

                        DicomCFindResponse response = new DicomCFindResponse(request, DicomStatus.Pending);
                        response.Dataset = eleList.List;
                        responses.Add(response);
                    }
                }

            }
            responses.Add(new DicomCFindResponse(request, DicomStatus.Success));
            return responses;
        }

    }
}
