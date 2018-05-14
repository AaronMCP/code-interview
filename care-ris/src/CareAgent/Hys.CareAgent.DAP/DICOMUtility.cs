using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Hys.CareAgent.DAP.Entity;
using kdt_managed;
using System.IO;

namespace Hys.CareAgent.DAP
{
    public class DICOMUtility
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");
        public DICOMUtility()
        {
            
        }

        public static bool BuildDICOMInfo(MElementList elmlist,ref DICOMInfoDto dicomDto)
        {
            try
            {
                 var dicom = Mapper.Map<DICOMInfo>(elmlist);
                 dicomDto.StudyInstanceUID = dicom.StudyInstanceUID;
                 dicomDto.SeriesInstanceUID = dicom.SeriesInstanceUID;
                 dicomDto.SOPInstanceUID = dicom.SOPInstanceUID;
                 dicomDto.PatientID = dicom.PatientID;
                 dicomDto.PatientName = dicom.PatientName;
                 dicomDto.PatientDOB = dicom.PatientDOB;
                 dicomDto.PatientAge = dicom.PatientAge;
                 dicomDto.PatientSex = dicom.PatientSex;
                 dicomDto.AccessionNo = dicom.AccessionNo;
                 dicomDto.BodyPart = dicom.BodyPart;
                 dicomDto.Modality = dicom.Modality;
                 dicomDto.ExamCode = string.Empty;
                 dicomDto.StudyDate = dicom.StudyDate;
                 dicomDto.StudyTime = dicom.StudyTime;
                 dicomDto.StudyDescription = dicom.StudyDescription;
                 dicomDto.ReferPhysician = dicom.ReferPhysician;
                dicomDto.SeriesNo = dicom.SeriesNo;
                int parseNo;
                int.TryParse(dicom.ImageNo, out parseNo);
                dicomDto.ImageNo = parseNo;
        
                if (dicom.PatientID == "")
                {
                    _logger.Error("DicomHeader.BuildDcmInfo(): " + "PatientID is empty.");
                    return false;
                }
                if (dicom.StudyInstanceUID == "")
                {
                    _logger.Error("DicomHeader.BuildDcmInfo(): " + "StudyInstanceUID is empty.");
                    return false;
                }
                //Series Group
                if (dicom.SeriesInstanceUID == "")
                {
                    _logger.Error("DicomHeader.BuildDcmInfo(): " + "SeriesInstanceUID is empty.");
                    return false;
                }
                if (dicom.SOPInstanceUID == "")
                {
                    _logger.Error("DicomHeader.BuildDcmInfo(): " + "SOPInstanceUID is empty.");
                    return false;
                }
                _logger.Error("DicomHeader.BuildDcmInfo(): " + "build dicom info successfully.");
                return true;
            }
            catch (Exception e)
            {
                _logger.Error("DicomHeader.BuildDcmInfo(): " + "pop up an exception--" + e.Message + ".");
                return false;
            }
        }

  

    }
}
