using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Dicom;
using HYS.DicomAdapter.Common;

namespace HYS.DicomAdapter.MWLServer.Dicom
{
    public class WorklistIOD : DElementList
    {
        public WorklistIOD()
        {
        }

        public static WorklistIOD Create(bool isResult)
        {
            WorklistIOD iod = new WorklistIOD();

            //Scheduled Procedure Step
            if (isResult)
            {
                iod.Add(getScheduledProcedureStepSequence(
                            getScheduledProcedureStepSequenceItem(
                                getScheduledProtocolCodeSequenceItem(
                                    getProtocolContextSequenceItem())
                                )
                            )
                        );
            }
            else
            {
                iod.Add(getScheduledProcedureStepSequence(
                            getScheduledProcedureStepSequenceItem(
                                getScheduledProtocolCodeSequenceItem()
                                )
                            )
                        );
            }

            DicomMappingHelper.SetCatagory(iod, "Scheduled Procedure Step");

            //Request Procedure
            iod.Add(getRequestProcedureID());
            iod.Add(getRequestProcedureDescription());
            iod.Add(getRequestProcedureCodeSequence(getRequestProcedureCodeSequenceItem()));
            iod.Add(getStudyInstanceUID());
            iod.Add(getReferencedStudySequence(getReferencedStudySequenceItem()));

            iod.Add(getReasonForTheRequestedProcedure());
            iod.Add(getRequestedProcedurePriority());
            iod.Add(getPatientTransportArrangements());
            iod.Add(getRequestedProcedureLocation());   //DV CR

            DicomMappingHelper.SetCatagory(iod, "Request Procedure");
        
            //Image Service Request
            iod.Add(getAccessionNumber());
            iod.Add(getRequestingPhysician());
            iod.Add(getReferringPhysiciansName());

            iod.Add(getRequestingService());            //DV CR

            DicomMappingHelper.SetCatagory(iod, "Image Service Request");
        
            //Visit
            iod.Add(getAdmissionID());
            iod.Add(getCurrentPatientLocation());
            iod.Add(getReferencedPatientSequence(getReferencedPatientSequenceItem()));

            iod.Add(getVisitStatusID());                //DV CR
            iod.Add(getPatientsInstitutionResidence()); //DV CR

            DicomMappingHelper.SetCatagory(iod, "Visit");

            //Patient
            iod.Add(getPatientsName());
            iod.Add(getPatientID());
            iod.Add(getPatientsBirthDate());
            iod.Add(getPatientsBirthTime());            //DV CR
            iod.Add(getOtherPatientIds());            //DV CR
            iod.Add(getOtherPatientNames());            //DV CR
            iod.Add(getPatientsSex());
            iod.Add(getPatientsWeight());
            iod.Add(getConfidentialityConstraintOnPatientData());
            iod.Add(getPatientState());
            iod.Add(getPregnancyStatus());
            iod.Add(getMedicalAlerts());
            iod.Add(getContrastAllergies());
            iod.Add(getSpecialNeeds());

            iod.Add(getPatientsAge());                  //DV CR
            iod.Add(getPatientsSize());                 //DV CR
            iod.Add(getEthnicGroup());                  //DV CR     
            iod.Add(getOccupation());                   //DV CR
            iod.Add(getAdditionalPatientsHistory());    //DV CR

            DicomMappingHelper.SetCatagory(iod, "Patient");

            iod.Add(getExposureDoseSequence(getExposureDoseSequenceItem()));

            return iod;
        }
        public static DPath[] CreateDPath(bool isResult)
        {
            return DicomMappingHelper.CreateDPath(Create(isResult));
        }

        #region Scheduled Procedure Step

        public static DElement getScheduledProcedureStepSequence(params DElementList[] ScheduledProcedureStepSequenceItems)
        {
            DElement ele = new DElement(0x0040, 0x0100,DVR.SQ,DValueType.Type1);
            foreach (DElementList sqList in ScheduledProcedureStepSequenceItems)
            {
                ele.Sequence.Add(sqList);
            }
            return ele;
        }
        
        public static DElementList getScheduledProcedureStepSequenceItem(params DElementList[] ScheduledProtocolCodeSequenceItems)
        {
            DElementList list = new DElementList();

            DElement ScheduledStationAETitle = list.Add(new DElement(0x0040, 0x0001, DVR.AE, DValueType.Type1));
            DElement ScheduledProcedureStepDate = list.Add(new DElement(0x0040, 0x0002, DVR.DA, DValueType.Type1));
            DElement ScheduledProcedureStepTime = list.Add(new DElement(0x0040, 0x0003, DVR.TM, DValueType.Type1));
            DElement Modality = list.Add(new DElement(0x0008, 0x0060, DVR.CS, DValueType.Type1));
            DElement ScheduledPerformingPhysiciansName = list.Add(new DElement(0x0040, 0x0006, DVR.PN, DValueType.Type2));
            DElement ScheduledProcedureStepDescription = list.Add(new DElement(0x0040, 0x0007, DVR.LO, DValueType.Type1));
            DElement ScheduledStationName = list.Add(new DElement(0x0040, 0x0010, DVR.SH, DValueType.Type2));
            DElement ScheduledProcedureStepLocation = list.Add(new DElement(0x0040, 0x0011, DVR.SH, DValueType.Type2));
            if (ScheduledProtocolCodeSequenceItems.Length > 0)
            {
                DElement ScheduledProtocolCodeSequence = new DElement(0x0040, 0x0008, DVR.SQ, DValueType.Type1);
                foreach (DElementList sqList in ScheduledProtocolCodeSequenceItems)
                {
                    ScheduledProtocolCodeSequence.Sequence.Add(sqList);
                }
                list.Add(ScheduledProtocolCodeSequence);
            }
            DElement PreMedication = list.Add(new DElement(0x0040, 0x0012, DVR.LO, DValueType.Type2C));
            DElement ScheduledProcedureStepID = list.Add(new DElement(0x0040,0x0009,DVR.SH,DValueType.Type1 ));
            DElement RequestContrastAgent = list.Add(new DElement(0x0032, 0x1070, DVR.LO, DValueType.Type2C));
            DElement ScheduledProcedureStepStatus = list.Add(new DElement(0x0040, 0x0020, DVR.CS, DValueType.Type3));

            return list;
        }
        public static DElementList getScheduledProtocolCodeSequenceItem(params DElementList[] ProtocolContextSequenceItems)
        {
            DElementList list = new DElementList();

            DElement CodeValue = list.Add(new DElement(0x0008, 0x0100, DVR.SH, DValueType.Type1));
            DElement CodeSchemeDesignator = list.Add(new DElement(0x0008, 0x0102, DVR.SH, DValueType.Type1));
            DElement CodeSchemeVersion = list.Add(new DElement(0x0008, 0x0103, DVR.SH, DValueType.Type3));
            DElement CodeMeaning = list.Add(new DElement(0x0008, 0x0104, DVR.LO, DValueType.Type3));
            if (ProtocolContextSequenceItems.Length > 0)
            {
                DElement ProtocolContextSequence = new DElement(0x0040, 0x0440, DVR.SQ, DValueType.Type3);
                foreach (DElementList sqList in ProtocolContextSequenceItems)
                {
                    ProtocolContextSequence.Sequence.Add(sqList);
                }
                list.Add(ProtocolContextSequence);
            }

            return list;
        }
        public static DElementList getProtocolContextSequenceItem()
        {
            DElementList list = new DElementList();

            DElement ValueType = list.Add(new DElement(0x0040, 0xA040, DVR.CS, DValueType.Type1));

            DElement ConceptNameCodeSequence = new DElement(0x0040, 0xA043, DVR.SQ, DValueType.Type1);
            ConceptNameCodeSequence.Sequence.Add(getCodeElementList());
            list.Add(ConceptNameCodeSequence);

            DElement DateTime = list.Add(new DElement(0x0040, 0xA120, DVR.DT, DValueType.Type1));
            DElement PersonName = list.Add(new DElement(0x0040, 0xA123, DVR.PN, DValueType.Type1));
            DElement TextValue = list.Add(new DElement(0x0040, 0xA160, DVR.UT, DValueType.Type1));

            DElement ConceptCodeSequence = new DElement(0x0040, 0xA168, DVR.SQ, DValueType.Type1);
            ConceptCodeSequence.Sequence.Add(getCodeElementList());
            list.Add(ConceptCodeSequence);

            DElement MeasurementUnitsCodeSequence = new DElement(0x0040, 0x08EA, DVR.SQ, DValueType.Type1);
            MeasurementUnitsCodeSequence.Sequence.Add(getCodeElementList());
            list.Add(MeasurementUnitsCodeSequence);

            return list;
        }
        public static DElementList getCodeElementList()
        {
            DElementList list = new DElementList();
            DElement CodeValue = list.Add(new DElement(0x0008, 0x0100, DVR.SH, DValueType.Type1));
            DElement CodeSchemeDesignator = list.Add(new DElement(0x0008, 0x0102, DVR.SH, DValueType.Type1));
            DElement CodeSchemeVersion = list.Add(new DElement(0x0008, 0x0103, DVR.SH, DValueType.Type3));
            DElement CodeMeaning = list.Add(new DElement(0x0008, 0x0104, DVR.LO, DValueType.Type3));
            return list;
        }

        #endregion

        #region Request Procedure

        public static DElement getRequestProcedureID()
        {
            return new DElement(0x0040, 0x1001, DVR.SH, DValueType.Type1);
        }
        public static DElement getRequestProcedureDescription()
        {
            return new DElement(0x0032, 0x1060, DVR.LO, DValueType.Type1);
        }
        public static DElement getRequestProcedureCodeSequence(params DElementList[] RequestProcedureCodeSequenceItems)
        {
            DElement ele = new DElement(0x0032, 0x1064, DVR.SQ, DValueType.Type1);
            foreach (DElementList sqList in RequestProcedureCodeSequenceItems)
            {
                ele.Sequence.Add(sqList);
            }
            return ele;
        }
        public static DElement getStudyInstanceUID()
        {
            return new DElement(0x0020, 0x000D, DVR.UI, DValueType.Type1);
        }
        public static DElement getReferencedStudySequence(params DElementList[] ReferencedStudySequenceItems)
        {
            DElement ele = new DElement(0x0008, 0x1110, DVR.SQ, DValueType.Type2);
            foreach (DElementList sqList in ReferencedStudySequenceItems)
            {
                ele.Sequence.Add(sqList);
            }
            return ele;
        }

        public static DElementList getRequestProcedureCodeSequenceItem()
        {
            DElementList list = new DElementList();

            DElement CodeValue = list.Add(new DElement(0x0008, 0x0100, DVR.SH, DValueType.Type1));
            DElement CodeSchemeDesignator = list.Add(new DElement(0x0008, 0x0102, DVR.SH, DValueType.Type1));
            DElement CodeSchemeVersion = list.Add(new DElement(0x0008, 0x0103, DVR.SH, DValueType.Type3));
            DElement CodeMeaning = list.Add(new DElement(0x0008, 0x0104, DVR.LO, DValueType.Type3));

            return list;
        }
        public static DElementList getReferencedStudySequenceItem()
        {
            DElementList list = new DElementList();

            DElement ReferencedSOPClassUID = list.Add(new DElement(0x0008, 0x1150, DVR.UI, DValueType.Type1));
            DElement ReferencedSOPInstanceUID = list.Add(new DElement(0x0008, 0x1155, DVR.UI, DValueType.Type1));

            return list;
        }

        public static DElement getReasonForTheRequestedProcedure()
        {
            return new DElement(0x0040, 0x1002, DVR.LO, DValueType.Type2);
        }
        public static DElement getRequestedProcedurePriority()
        {
            return new DElement(0x0040, 0x1003, DVR.SH, DValueType.Type2);
        }
        public static DElement getPatientTransportArrangements()
        {
            return new DElement(0x0040, 0x1004, DVR.LO, DValueType.Type2);
        }
        public static DElement getRequestedProcedureLocation()
        {
            return new DElement(0x0040, 0x1005, DVR.LO, DValueType.Type3);
        }

        #endregion

        #region Image Service Request

        public static DElement getAccessionNumber()
        {
            return new DElement(0x0008, 0x0050, DVR.SH, DValueType.Type2);
        }
        public static DElement getRequestingPhysician()
        {
            return new DElement(0x0032, 0x1032, DVR.PN, DValueType.Type2);
        }
        public static DElement getReferringPhysiciansName()
        {
            return new DElement(0x0008, 0x0090, DVR.PN, DValueType.Type2);
        }

        public static DElement getRequestingService()
        {
            return new DElement(0x0032, 0x1033, DVR.LO, DValueType.Type3);
        }

        #endregion

        #region Visit

        public static DElement getAdmissionID()
        {
            return new DElement(0x0038, 0x0010, DVR.LO, DValueType.Type2);
        }
        public static DElement getCurrentPatientLocation()
        {
            return new DElement(0x0038, 0x0300, DVR.LO, DValueType.Type2);
        }
        public static DElement getReferencedPatientSequence(params DElementList[] ReferencedPatientSequenceItems)
        {
            DElement ele = new DElement(0x0008, 0x1120, DVR.SQ, DValueType.Type2);
            foreach (DElementList sqList in ReferencedPatientSequenceItems)
            {
                ele.Sequence.Add(sqList);
            }
            return ele;
        }

        public static DElementList getReferencedPatientSequenceItem()
        {
            DElementList list = new DElementList();

            DElement ReferencedSOPClassUID = list.Add(new DElement(0x0008, 0x1150, DVR.UI, DValueType.Type1));
            DElement ReferencedSOPInstanceUID = list.Add(new DElement(0x0008, 0x1155, DVR.UI, DValueType.Type1));

            return list;
        }

        public static DElement getVisitStatusID()
        {
            return new DElement(0x0038, 0x0008, DVR.CS, DValueType.Type3);
        }
        public static DElement getPatientsInstitutionResidence()
        {
            return new DElement(0x0038, 0x0400, DVR.LO, DValueType.Type3);
        }

        #endregion

        #region Patient

        public static DElement getPatientsName()
        {
            return new DElement(0x0010, 0x0010, DVR.PN, DValueType.Type1);
        }
        public static DElement getPatientID()
        {
            return new DElement(0x0010, 0x0020, DVR.LO, DValueType.Type1);
        }
        public static DElement getPatientsBirthDate()
        {
            return new DElement(0x0010, 0x0030, DVR.DA, DValueType.Type2);
        }
        public static DElement getPatientsSex()
        {
            return new DElement(0x0010, 0x0040, DVR.CS, DValueType.Type2);
        }
        public static DElement getPatientsWeight()
        {
            return new DElement(0x0010, 0x1030, DVR.DS, DValueType.Type2);
        }
        public static DElement getConfidentialityConstraintOnPatientData()
        {
            return new DElement(0x0040, 0x3001, DVR.LO, DValueType.Type2);
        }
        public static DElement getPatientState()
        {
            return new DElement(0x0038, 0x0500, DVR.LO, DValueType.Type2);
        }
        public static DElement getPregnancyStatus()
        {
            return new DElement(0x0010, 0x21C0, DVR.US, DValueType.Type2);
        }
        public static DElement getMedicalAlerts()
        {
            return new DElement(0x0010, 0x2000, DVR.LO, DValueType.Type2);
        }
        public static DElement getContrastAllergies()
        {
            return new DElement(0x0010, 0x2110, DVR.LO, DValueType.Type2);
        }
        public static DElement getSpecialNeeds()
        {
            return new DElement(0x0038, 0x0050, DVR.LO, DValueType.Type2);
        }

        public static DElement getPatientsAge()
        {
            return new DElement(0x0010, 0x1010, DVR.AS, DValueType.Type3);
        }
        public static DElement getPatientsSize()
        {
            return new DElement(0x0010, 0x1020, DVR.DS, DValueType.Type3);
        }
        public static DElement getEthnicGroup()
        {
            return new DElement(0x0010, 0x2160, DVR.SH, DValueType.Type3);
        }
        public static DElement getOccupation()
        {
            return new DElement(0x0010, 0x2180, DVR.SH, DValueType.Type3);
        }
        public static DElement getAdditionalPatientsHistory()
        {
            return new DElement(0x0010, 0x21B0, DVR.LT, DValueType.Type3);
        }
        public static DElement getPatientsBirthTime()
        {
            return new DElement(0x0010, 0x0032, DVR.TM, DValueType.Type3);
        }
        public static DElement getOtherPatientIds()
        {
            return new DElement(0x0010, 0x1000, DVR.LO, DValueType.Type3);
        }
        public static DElement getOtherPatientNames()
        {
            return new DElement(0x0010, 0x1001, DVR.PN, DValueType.Type3);
        }

        #endregion

        public static DElement getExposureDoseSequence(params DElementList[] ExposureDoseSequenceItems)
        {
            DElement ele = new DElement(0x0040, 0x030E, DVR.SQ, DValueType.Type3);
            foreach (DElementList sqList in ExposureDoseSequenceItems)
            {
                ele.Sequence.Add(sqList);
            }
            return ele;
        }
        public static DElementList getExposureDoseSequenceItem()
        {
            DElementList list = new DElementList();

            DElement KVP = list.Add(new DElement(0x0018, 0x0060, DVR.DS, DValueType.Type3));
            DElement ExposureTime = list.Add(new DElement(0x0018, 0x1150, DVR.IS, DValueType.Type3));
            DElement XRayTubeCurrentInuA = list.Add(new DElement(0x0018, 0x8151, DVR.DS, DValueType.Type3));

            return list;
        }
    }
}
