using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Dicom;
using HYS.DicomAdapter.Common;

namespace HYS.DicomAdapter.StorageServer.Dicom
{
    public class StorageIOD : DElementList
    {
        public StorageIOD()
        {
        }

        public static StorageIOD Storage()
        {
            StorageIOD iod = new StorageIOD();

            //Patient
            iod.Add(getPatientsName());
            iod.Add(getPatientID());
            iod.Add(getIssuerOfPatientID());            //Patient Module
            iod.Add(getPatientsBirthDate());
            iod.Add(getPatientsBirthTime());            //DV CR
            iod.Add(getOtherPatientIds());            //DV CR
            iod.Add(getOtherPatientNames());            //DV CR
            iod.Add(getPatientsSex());
            iod.Add(getPatientsWeight());
            iod.Add(getPatientComments());              //Patient Module
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

            //Study
            iod.Add(getStudyInstanceUID());
            iod.Add(getStudyDate());
            iod.Add(getStudyTime());
            iod.Add(getReferringPhysiciansName());
            iod.Add(getReferringPhysiciansTelephoneNumber());
            iod.Add(getStudyID());
            iod.Add(getAccessionNumber());
            iod.Add(getStudyDesciption());
            iod.Add(getNameOfPhysiciansReadingStudy());
            iod.Add(getAdmittingDiagnosisDescription());
            iod.Add(getStudyPriorityID());
            iod.Add(getOrderCallbackPhoneNumber());

            DicomMappingHelper.SetCatagory(iod, "Study");

            //Series
            iod.Add(getModality());
            iod.Add(getSeriesInstanceUID());
            iod.Add(getSeriesNumber());
            iod.Add(getLaterality());
            iod.Add(getSeriesDate());
            iod.Add(getSeriesTime());
            iod.Add(getPerformingPhysiciansName());
            iod.Add(getProtocolName());
            iod.Add(getSeriesDesciption());
            iod.Add(getOperatorsName());
            iod.Add(getBodyPartExamined());
            iod.Add(getPatientPosition());

            DicomMappingHelper.SetCatagory(iod, "Series");

            //Equipment
            iod.Add(getManufacturer());
            iod.Add(getInstitutionName());
            iod.Add(getInstitutionAddress());
            iod.Add(getStationName());
            iod.Add(getInstitutionalDepartmentName());
            iod.Add(getManufacturesModelName());
            iod.Add(getDeviceSerialNumber());
            iod.Add(getSoftwareVersions());
            iod.Add(getSpatialResolution());
            iod.Add(getDateOfLastCalibration());
            iod.Add(getTimeOfLastCalibration());
            iod.Add(getAcqusitionDeviceProcessingDescription());
            iod.Add(getViewPosition());
            iod.Add(getPixelPaddingValue());

            DicomMappingHelper.SetCatagory(iod, "Equipment");

            //Image
            iod.Add(getInstanceNumber());
            iod.Add(getPatientOrientation());
            iod.Add(getContentDate());
            iod.Add(getContentTime());
            iod.Add(getImageType());
            iod.Add(getAcquisitionNumber());
            iod.Add(getAcquisitionDate());
            iod.Add(getAcquisitionTime());
            iod.Add(getAcquisitionDateTime());
            iod.Add(getImagesInAcquisition());
            iod.Add(getImageComments());
            iod.Add(getSamplesPerPixel());
            iod.Add(getPhotometricInterpertation());
            iod.Add(getRows());
            iod.Add(getColumns());
            iod.Add(getPixelSpacing());
            iod.Add(getBitsAllocated());
            iod.Add(getBitsStored());
            iod.Add(getHighBit());
            iod.Add(getPixcelRepresentation());
            iod.Add(getQualityControlImage());
            iod.Add(getWindowCenter());
            iod.Add(getWindowWidth());
            iod.Add(getBurnedInAnnotation());
            iod.Add(getLossyImageCompression());
            iod.Add(getLossyImageCompressionRatio());
            iod.Add(getLossyImageCompressionMethod());
            iod.Add(getPresentationLUTShape());

            DicomMappingHelper.SetCatagory(iod, "Image");

            //SOP Common
            iod.Add(getSOPClassUID());
            iod.Add(getSOPInstanceUID());
            iod.Add(getSpecificCharacterSet());
            iod.Add(getInstanceCreationDate());
            iod.Add(getInstanceCreationTime());
            iod.Add(getInstanceCreatorUID());
            iod.Add(getSOPInstanceStatus());
            iod.Add(getSOPAuthorizationDateAndTime());
            iod.Add(getSOPAuthorizationComment());
            iod.Add(getAuthorizationEquipmentCertificationNumber());

            DicomMappingHelper.SetCatagory(iod, "SOP Common");

            return iod;
        }
        public static DPath[] StorageDPath()
        {
            return DicomMappingHelper.CreateDPath(Storage());
        }

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

        public static DElement getPatientComments()
        {
            return new DElement(0x0010, 0x4000, DVR.LT, DValueType.Type3);
        }
        public static DElement getIssuerOfPatientID()
        {
            return new DElement(0x0010, 0x0021, DVR.LO, DValueType.Type3);
        }

        #endregion

        #region Study

        public static DElement getStudyInstanceUID()
        {
            return new DElement(0x0020, 0x000D, DVR.UI, DValueType.Type1);
        }
        public static DElement getStudyDate()
        {
            return new DElement(0x0008, 0x0020, DVR.DA, DValueType.Type2);
        }
        public static DElement getStudyTime()
        {
            return new DElement(0x0008, 0x0030, DVR.TM, DValueType.Type2);
        }
        public static DElement getReferringPhysiciansName()
        {
            return new DElement(0x0008, 0x0090, DVR.PN, DValueType.Type2);
        }
        public static DElement getReferringPhysiciansTelephoneNumber()
        {
            return new DElement(0x0008, 0x0094, DVR.SH, DValueType.Type2);
        }
        public static DElement getStudyID()
        {
            return new DElement(0x0020, 0x0010, DVR.SH, DValueType.Type2);
        }
        public static DElement getAccessionNumber()
        {
            return new DElement(0x0008, 0x0050, DVR.SH, DValueType.Type2);
        }
        public static DElement getStudyDesciption()
        {
            return new DElement(0x0008, 0x1030, DVR.LO, DValueType.Type3);
        }
        public static DElement getNameOfPhysiciansReadingStudy()
        {
            return new DElement(0x0008, 0x1060, DVR.PN, DValueType.Type3);
        }
        public static DElement getAdmittingDiagnosisDescription()
        {
            return new DElement(0x0008, 0x1080, DVR.LO, DValueType.Type3);
        }
        public static DElement getStudyPriorityID()
        {
            return new DElement(0x0032, 0x000C, DVR.CS, DValueType.Type3);
        }
        public static DElement getOrderCallbackPhoneNumber()
        {
            return new DElement(0x0040, 0x2010, DVR.SH, DValueType.Type3);
        }

        #endregion

        #region Series

        public static DElement getModality()
        {
            return new DElement(0x0008, 0x0060, DVR.CS, DValueType.Type1);
        }
        public static DElement getSeriesInstanceUID()
        {
            return new DElement(0x0020, 0x000E, DVR.UI, DValueType.Type1);
        }
        public static DElement getSeriesNumber()
        {
            return new DElement(0x0020, 0x0011, DVR.IS, DValueType.Type2);
        }
        public static DElement getLaterality()
        {
            return new DElement(0x0020, 0x0060, DVR.CS, DValueType.Type2);
        }
        public static DElement getSeriesDate()
        {
            return new DElement(0x0008, 0x0021, DVR.DA, DValueType.Type3);
        }
        public static DElement getSeriesTime()
        {
            return new DElement(0x0008, 0x0031, DVR.TM, DValueType.Type3);
        }
        public static DElement getPerformingPhysiciansName()
        {
            return new DElement(0x0008, 0x1050, DVR.PN, DValueType.Type3);
        }
        public static DElement getProtocolName()
        {
            return new DElement(0x0018, 0x1030, DVR.LO, DValueType.Type3);
        }
        public static DElement getSeriesDesciption()
        {
            return new DElement(0x0008, 0x103E, DVR.LO, DValueType.Type3);
        }
        public static DElement getOperatorsName()
        {
            return new DElement(0x0008, 0x1070, DVR.PN, DValueType.Type3);
        }
        public static DElement getBodyPartExamined()
        {
            return new DElement(0x0018, 0x0015, DVR.CS, DValueType.Type3);
        }
        public static DElement getPatientPosition()
        {
            return new DElement(0x0018, 0x5100, DVR.CS, DValueType.Type2);
        }

        #endregion

        #region Equipment

        public static DElement getManufacturer()
        {
            return new DElement(0x0008, 0x0070, DVR.LO, DValueType.Type2);
        }
        public static DElement getInstitutionName()
        {
            return new DElement(0x0008, 0x0080, DVR.LO, DValueType.Type3);
        }
        public static DElement getInstitutionAddress()
        {
            return new DElement(0x0008, 0x0081, DVR.LO, DValueType.Type3);
        }
        public static DElement getStationName()
        {
            return new DElement(0x0008, 0x1010, DVR.SH, DValueType.Type3);
        }
        public static DElement getInstitutionalDepartmentName()
        {
            return new DElement(0x0008, 0x1040, DVR.LO, DValueType.Type3);
        }
        public static DElement getManufacturesModelName()
        {
            return new DElement(0x0008, 0x1090, DVR.LO, DValueType.Type3);
        }
        public static DElement getDeviceSerialNumber()
        {
            return new DElement(0x0018, 0x1000, DVR.LO, DValueType.Type3);
        }
        public static DElement getSoftwareVersions()
        {
            return new DElement(0x0018, 0x1020, DVR.LO, DValueType.Type3);
        }
        public static DElement getImagePixelSpacing()
        {
            return new DElement(0x0018, 0x1164, DVR.DS, DValueType.Type3);
        }
        public static DElement getSpatialResolution()
        {
            return new DElement(0x0018, 0x1050, DVR.DS, DValueType.Type3);
        }
        public static DElement getDateOfLastCalibration()
        {
            return new DElement(0x0018, 0x1200, DVR.DA, DValueType.Type3);
        }
        public static DElement getTimeOfLastCalibration()
        {
            return new DElement(0x0018, 0x1201, DVR.TM, DValueType.Type3);
        }
        public static DElement getAcqusitionDeviceProcessingDescription()
        {
            return new DElement(0x0018, 0x1400, DVR.LO, DValueType.Type3);
        }
        public static DElement getViewPosition()
        {
            return new DElement(0x0018, 0x5101, DVR.CS, DValueType.Type3);
        }
        public static DElement getPixelPaddingValue()
        {
            return new DElement(0x0028, 0x0120, DVR.SS, DValueType.Type3);
        }

        #endregion

        #region Image

        public static DElement getInstanceNumber()
        {
            return new DElement(0x0020, 0x0013, DVR.IS, DValueType.Type2);
        }
        public static DElement getPatientOrientation()
        {
            return new DElement(0x0020, 0x0020, DVR.CS, DValueType.Type2);
        }
        public static DElement getContentDate()
        {
            return new DElement(0x0008, 0x0023, DVR.DA, DValueType.Type2);
        }
        public static DElement getContentTime()
        {
            return new DElement(0x0008, 0x0033, DVR.TM, DValueType.Type2);
        }
        public static DElement getImageType()
        {
            return new DElement(0x0008, 0x0008, DVR.CS, DValueType.Type3);
        }
        public static DElement getAcquisitionNumber()
        {
            return new DElement(0x0020, 0x0012, DVR.IS, DValueType.Type3);
        }
        public static DElement getAcquisitionDate()
        {
            return new DElement(0x0008, 0x0022, DVR.DA, DValueType.Type3);
        }
        public static DElement getAcquisitionTime()
        {
            return new DElement(0x0008, 0x0032, DVR.TM, DValueType.Type3);
        }
        public static DElement getAcquisitionDateTime()
        {
            return new DElement(0x0008, 0x002A, DVR.DT, DValueType.Type3);
        }
        public static DElement getImagesInAcquisition()
        {
            return new DElement(0x0020, 0x1002, DVR.IS, DValueType.Type3);
        }
        public static DElement getImageComments()
        {
            return new DElement(0x0020, 0x4000, DVR.LT, DValueType.Type3);
        }

        public static DElement getSamplesPerPixel()
        {
            return new DElement(0x0028, 0x0002, DVR.US, DValueType.Type3);
        }
        public static DElement getPhotometricInterpertation()
        {
            return new DElement(0x0028, 0x0004, DVR.CS, DValueType.Type3);
        }
        public static DElement getRows()
        {
            return new DElement(0x0028, 0x0010, DVR.US, DValueType.Type3);
        }
        public static DElement getColumns()
        {
            return new DElement(0x0028, 0x0011, DVR.US, DValueType.Type3);
        }
        public static DElement getPixelSpacing()
        {
            return new DElement(0x0028, 0x0030, DVR.DS, DValueType.Type3);
        }
        public static DElement getBitsAllocated()
        {
            return new DElement(0x0028, 0x0100, DVR.US, DValueType.Type3);
        }
        public static DElement getBitsStored()
        {
            return new DElement(0x0028, 0x0101, DVR.US, DValueType.Type3);
        }
        public static DElement getHighBit()
        {
            return new DElement(0x0028, 0x0102, DVR.US, DValueType.Type3);
        }
        public static DElement getPixcelRepresentation()
        {
            return new DElement(0x0028, 0x0103, DVR.US, DValueType.Type3);
        }

        public static DElement getQualityControlImage()
        {
            return new DElement(0x0028, 0x0300, DVR.CS, DValueType.Type3);
        }

        public static DElement getWindowCenter()
        {
            return new DElement(0x0028, 0x1050, DVR.DS, DValueType.Type3);
        }
        public static DElement getWindowWidth()
        {
            return new DElement(0x0028, 0x1051, DVR.DS, DValueType.Type3);
        }

        public static DElement getBurnedInAnnotation()
        {
            return new DElement(0x0028, 0x0301, DVR.CS, DValueType.Type3);
        }
        public static DElement getLossyImageCompression()
        {
            return new DElement(0x0028, 0x2110, DVR.CS, DValueType.Type3);
        }
        public static DElement getLossyImageCompressionRatio()
        {
            return new DElement(0x0028, 0x2112, DVR.DS, DValueType.Type3);
        }
        public static DElement getLossyImageCompressionMethod()
        {
            return new DElement(0x0028, 0x2114, DVR.CS, DValueType.Type3);
        }
        public static DElement getPresentationLUTShape()
        {
            return new DElement(0x2050, 0x0020, DVR.CS, DValueType.Type3);
        } 
        
        #endregion

        #region SOP Common

        public static DElement getSOPClassUID()
        {
            return new DElement(0x0008, 0x0016, DVR.UI, DValueType.Type1);
        }
        public static DElement getSOPInstanceUID()
        {
            return new DElement(0x0008, 0x0018, DVR.UI, DValueType.Type1);
        }
        public static DElement getSpecificCharacterSet()
        {
            return new DElement(0x0008, 0x0005, DVR.CS, DValueType.Type1);
        }
        public static DElement getInstanceCreationDate()
        {
            return new DElement(0x0008, 0x0012, DVR.DA, DValueType.Type3);
        }
        public static DElement getInstanceCreationTime()
        {
            return new DElement(0x0008, 0x0013, DVR.TM, DValueType.Type3);
        }
        public static DElement getInstanceCreatorUID()
        {
            return new DElement(0x0008, 0x0014, DVR.UI, DValueType.Type3);
        }
        public static DElement getSOPInstanceStatus()
        {
            return new DElement(0x0100, 0x0410, DVR.CS, DValueType.Type3);
        }
        public static DElement getSOPAuthorizationDateAndTime()
        {
            return new DElement(0x0100, 0x0420, DVR.DT, DValueType.Type3);
        }
        public static DElement getSOPAuthorizationComment()
        {
            return new DElement(0x0100, 0x0424, DVR.LT, DValueType.Type3);
        }
        public static DElement getAuthorizationEquipmentCertificationNumber()
        {
            return new DElement(0x0100, 0x0426, DVR.LO, DValueType.Type3);
        }

        #endregion
    }
}
