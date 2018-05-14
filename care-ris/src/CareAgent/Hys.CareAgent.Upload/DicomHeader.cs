using System;
using System.Text;
using System.Diagnostics;
using kdt_managed;

namespace Hys.CareAgent.Upload
{
    class DicomHeader : IDisposable
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");
        //Patient Group
        public String PatientID;
        public String PatientName;
        public String PatientDOB;
        public String PatientAge;
        public String PatientSex;
        //Study Group
        public String StudyInstanceUID;
        public String AccessionNo;
        public String BodyPart;
        public String Modality;
        public String StudyDate;
        public String StudyTime;
        public String StudyDescription;
        public String ReferPhysician;
        public String Manufacture;
        public String InstitutionName;
        public String StationName;
        //Series Group
        public String SeriesInstanceUID;
        public Int32 SeriesNo;
        public String PatientPosition;
        public String ViewPosition;
        public String SeriesDate;
        public String SeriesTime;
        public String SeriesDescription;

        //Image Group
        public String SOPInstanceUID;
        public String ImageNo;
        public String ImageType;
        public Int32 NumberOfFrames;
        public String ImageDate;
        public String ImageTime;
        public Int32 SamplesPerPixel;
        public Int32 ImageRows;
        public Int32 ImageColumns;
        public Int32 BitsAllocated;
        public Int32 BitsStored;
        public String PixelSpacing;
        public String PhotometricIntr;
        public Int32 KVP;
        public Int32 Exposure;

        public StudyDto StudyDto = new StudyDto();
        public SeriesDto SeriesDto = new SeriesDto();
        public ImageDto ImageDto = new ImageDto();

        public DicomHeader()
        {
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private string ParseDateTime(string strDT)
        {
            string strRet = "";
            try
            {
                strRet = System.DateTime.ParseExact(strDT, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");
            }
            catch (Exception exTime)
            {
                _logger.Error(strDT + " is invalid time format. ExchangeService will try to reparse it.");
                strRet = System.DateTime.ParseExact(strDT, "yyyy.MM.dd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");
            }
            return strRet;
        }

        public bool BuildDicomInfo(MElementList elmlist)
        {
            bool bRet = false;
            try
            {
                //Patient Group
                //PK Verify
                GetStringTagValue(ref PatientID, elmlist, tag_t.kPatientID);
                if (PatientID == "")
                {
                    _logger.Error("DicomHeader.BuildDcmInfo(): " + "PatientID is empty.");
                    return false;
                }
                GetLocalStringTagValue(ref PatientName, elmlist, tag_t.kPatientsName);
                GetDateTimeTagValue(ref PatientDOB, elmlist, tag_t.kPatientsBirthDate);
                //yyyy-MM-dd
                if (PatientDOB != " ")
                {
                    PatientDOB = ParseDateTime(PatientDOB);
                }
                GetStringTagValue(ref PatientAge, elmlist, tag_t.kPatientsAge);
                GetStringTagValue(ref PatientSex, elmlist, tag_t.kPatientsSex);
                //Study Group
                GetUIDTagValue(ref StudyInstanceUID, elmlist, tag_t.kStudyInstanceUID);
                if (StudyInstanceUID == "")
                {
                    _logger.Error("DicomHeader.BuildDcmInfo(): " + "StudyInstanceUID is empty.");
                    return false;
                }
                GetStringTagValue(ref AccessionNo, elmlist, tag_t.kAccessionNumber);
                GetStringTagValue(ref BodyPart, elmlist, tag_t.kBodyPartExamined);
                GetStringTagValue(ref Modality, elmlist, tag_t.kModality);
                GetDateTimeTagValue(ref StudyDate, elmlist, tag_t.kStudyDate);
                if (StudyDate != " ")
                {
                    StudyDate = ParseDateTime(StudyDate);
                }
                GetDateTimeTagValue(ref StudyTime, elmlist, tag_t.kStudyTime);
                GetStringTagValue(ref StudyDescription, elmlist, tag_t.kStudyDescription);
                GetStringTagValue(ref ReferPhysician, elmlist, tag_t.kReferringPhysiciansName);
                GetStringTagValue(ref Manufacture, elmlist, tag_t.kManufacturer);
                GetStringTagValue(ref InstitutionName, elmlist, tag_t.kInstitutionName);
                GetStringTagValue(ref StationName, elmlist, tag_t.kStationName);
                //Series Group
                GetUIDTagValue(ref SeriesInstanceUID, elmlist, tag_t.kSeriesInstanceUID);
                if (SeriesInstanceUID == "")
                {
                    _logger.Error("DicomHeader.BuildDcmInfo(): " + "SeriesInstanceUID is empty.");
                    return false;
                }
                GetIntTagValue(ref SeriesNo, elmlist, tag_t.kSeriesNumber);
                GetStringTagValue(ref PatientPosition, elmlist, tag_t.kPatientPosition);
                GetStringTagValue(ref ViewPosition, elmlist, tag_t.kViewPosition);
                GetDateTimeTagValue(ref SeriesDate, elmlist, tag_t.kSeriesDate);
                if (SeriesDate != " ")
                {
                    SeriesDate = ParseDateTime(SeriesDate);
                }
                GetDateTimeTagValue(ref SeriesTime, elmlist, tag_t.kSeriesTime);
                GetStringTagValue(ref SeriesDescription, elmlist, tag_t.kSeriesDescription);
                //Image Group
                GetUIDTagValue(ref SOPInstanceUID, elmlist, tag_t.kSOPInstanceUID);
                if (SOPInstanceUID == "")
                {
                    _logger.Error("DicomHeader.BuildDcmInfo(): " + "SOPInstanceUID is empty.");
                    return false;
                }
                GetStringTagValue(ref ImageNo, elmlist, tag_t.kInstanceNumber);
                //InstanceNumber of some dicom is ""
                if (string.IsNullOrEmpty(ImageNo))
                {
                    ImageNo = "0";
                }

                GetStringTagValue(ref ImageType, elmlist, tag_t.kImageType);
                GetIntTagValue(ref NumberOfFrames, elmlist, tag_t.kNumberOfFrames);
                GetDateTimeTagValue(ref ImageDate, elmlist, tag_t.kAcquisitionDate);
                if (ImageDate != " ")
                {
                    ImageDate = ParseDateTime(ImageDate);
                }
                GetDateTimeTagValue(ref ImageTime, elmlist, tag_t.kAcquisitionTime);
                GetIntTagValue(ref SamplesPerPixel, elmlist, tag_t.kSamplesPerPixel);
                GetIntTagValue(ref ImageRows, elmlist, tag_t.kRows);
                GetIntTagValue(ref ImageColumns, elmlist, tag_t.kColumns);
                GetIntTagValue(ref BitsAllocated, elmlist, tag_t.kBitsAllocated);
                GetIntTagValue(ref BitsStored, elmlist, tag_t.kBitsStored);
                GetStringTagValue(ref PixelSpacing, elmlist, tag_t.kPixelSpacing);
                GetStringTagValue(ref PhotometricIntr, elmlist, tag_t.kPhotometricInterpretation);
                GetIntTagValue(ref KVP, elmlist, tag_t.kKVP);
                GetIntTagValue(ref Exposure, elmlist, tag_t.kExposureTime);

                _logger.Error("DicomHeader.BuildDcmInfo(): " + "build dicom info successfully.");
            }
            catch (Exception e)
            {
                _logger.Error("DicomHeader.BuildDcmInfo(): " + "pop up an exception--" + e.Message + ".");
            }

            return true;
        }

        private void GetUIDTagValue(ref string strUID, MElementList elmlist, tag_t tag)
        {
            using (MElementRef eref = elmlist.get_Element(tag))
            {
                if (eref != null && eref.value_count != 0 && eref.value_count != -1)
                    strUID = eref.get_uid(0);
                else
                    strUID = "";
            }
        }

        private void GetStringTagValue(ref string strValue, MElementList elmlist, tag_t tag)
        {
            try
            {
                using (MElementRef eref = elmlist.get_Element(tag))
                {
                    if (eref != null && eref.value_count != 0 && eref.value_count != -1)
                        strValue = eref.get_string(0);
                    else
                        strValue = "";
                }
            }
            catch (Exception e)
            {
                strValue = "";
                _logger.Error("DicomHeader.GetStringTagValue(): " + "get string for tag " + tag.ToString() + " pop up an exception--" + e.Message + ".");
            }
        }
        /// <summary>
        /// For some tag with local language, e.g PatientName
        /// ISO_IR 192: use UTF8 encoding local string
        /// ISO_IR 100: use GB2312 encoding local string
        /// </summary>
        /// <param name="strValue"></param>
        /// <param name="elmlist"></param>
        /// <param name="tag"></param>
        private void GetLocalStringTagValue(ref string strValue, MElementList elmlist, tag_t tag)
        {
            try
            {
                String str_speccharacter = "";
                using (MElementRef intereref = elmlist.get_Element(tag_t.kSpecificCharacterSet))
                {
                    if (intereref != null && intereref.value_count != 0 && intereref.value_count != -1)
                        str_speccharacter = intereref.get_string(0);
                    else
                        str_speccharacter = "";
                }
                using (MElementRef eref = elmlist.get_Element(tag))
                {
                    Byte[] byte_patName = null;
                    if (str_speccharacter.Trim() == "ISO_IR 192")
                    {
                        if (eref != null && eref.value_count != 0 && eref.value_count != -1)
                            byte_patName = eref.get_blob(0);
                        strValue = Encoding.UTF8.GetString(byte_patName);
                    }
                    else if (str_speccharacter.Trim() == "ISO_IR 100")
                    {
                        if (eref != null && eref.value_count != 0 && eref.value_count != -1)
                            byte_patName = eref.get_blob(0);
                        strValue = Encoding.GetEncoding("GB2312").GetString(byte_patName);
                    }
                    else
                    {
                        if (eref != null && eref.value_count != 0 && eref.value_count != -1)
                            strValue = eref.get_string(0);
                        else
                            strValue = " ";
                    }
                }



            }
            catch (Exception e)
            {
                strValue = " ";
                _logger.Error("DCMHeader.GetStringTagValue(): " + "get string for tag " + tag.ToString() + " pop up an exception--" + e.Message + ".");
            }
        }
        private void GetIntTagValue(ref int nValue, MElementList elmlist, tag_t tag)
        {
            try
            {

                using (MElementRef eref = elmlist.get_Element(tag))
                {
                    if (eref != null && eref.value_count != 0 && eref.value_count != -1)
                        nValue = eref.get_sint(0);
                    else
                        nValue = 0;
                }
            }
            catch (Exception e)
            {
                _logger.Error("DCMHeader.GetIntTagValue(): " + "get int for tag " + tag.ToString() + " pop up an exception--" + e.Message + ".");
                nValue = 0;
            }

        }

        private void GetDateTimeTagValue(ref string strValue, MElementList elmlist, tag_t tag)
        {
            try
            {
                using (MElementRef eref = elmlist.get_Element(tag))
                {
                    if (eref != null && eref.value_count != 0 && eref.value_count != -1)
                    {
                        strValue = eref.get_string(0);
                    }
                    else
                        strValue = " ";
                }
            }
            catch (Exception e)
            {
                strValue = " ";
                _logger.Error("DCMHeader.GetDateTimeTagValue(): " + "get datetime for tag " + tag.ToString() + " pop up an exception--" + e.Message + ".");
            }

        }

        public DCMInfo GetDCMInfo()
        {


            StudyDto studyDto = new StudyDto();
            studyDto.UniqueID = StudyInstanceUID;
            studyDto.PatientID = PatientID;
            studyDto.PatientName = PatientName;
            studyDto.PatientDOB = PatientDOB;
            studyDto.PatientAge = PatientAge;
            studyDto.PatientSex = PatientSex;
            studyDto.AccessionNo = AccessionNo;
            studyDto.BodyPart = BodyPart;
            studyDto.Modality = Modality;
            studyDto.ExamCode = "";
            studyDto.StudyDate = StudyDate;
            studyDto.StudyTime = StudyTime;
            studyDto.StudyDescription = StudyDescription;
            studyDto.ReferPhysician = ReferPhysician;

            SeriesDto seriesDto = new SeriesDto();
            seriesDto.UniqueID = SeriesInstanceUID;
            seriesDto.StudyInstanceUID = StudyInstanceUID;
            seriesDto.BodyPart = BodyPart;
            seriesDto.Modality = Modality;

            ImageDto imageDto = new ImageDto();
            imageDto.UniqueID = SOPInstanceUID;
            imageDto.SeriesInstanceUID = SeriesInstanceUID;


            DCMInfo dcmInfo = new DCMInfo();
            dcmInfo.StudyDto = studyDto;
            dcmInfo.SeriesDto = seriesDto;
            dcmInfo.ImageDto = imageDto;

            return dcmInfo;
        }

        /// <summary>
        /// -1:failed;0:succuss;1:already exist/not update
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public int SaveToDB()
        {
            //DAPPatient tsPatient = new DAPPatient();
            //tsPatient.FillPatient(PatientID,PatientName ,PatientDOB,
            //                      PatientAge, PatientSex);

            //if (tsPatient.SaveToDB() == DAPDALResults.DAL_INSERTFAILED)
            //    return 1;

            //DAPStudy tsStudy = new DAPStudy();
            //tsStudy.FillStudy(StudyInstanceUID, PatientID, AccessionNo,BodyPart,Modality,
            //                      StudyDate, StudyTime, StudyDescription, ReferPhysician,0,0,
            //                      Manufacture, InstitutionName, StationName, "", CommonStatus.ONLINE);

            //if (tsStudy.SaveToDB() == DAPDALResults.DAL_INSERTFAILED)
            //    return 1;

            //DAPSeries tsSereis = new DAPSeries();
            //tsSereis.FillSeries(SeriesInstanceUID,StudyInstanceUID,BodyPart,Modality, SeriesNo,
            //                        0,PatientPosition,ViewPosition,SeriesDate,SeriesTime,SeriesDescription,CommonStatus.ONLINE);

            //if (tsSereis.SaveToDB() == DAPDALResults.DAL_INSERTFAILED)
            //    return 1;

            //DAPImage tsImage = new DAPImage();

            //tsImage.FillImage(SOPInstanceUID, SeriesInstanceUID, StorageFilePath, ImageNo, ImageType, NumberOfFrames,
            //                0,ImageDate,ImageTime,SamplesPerPixel,ImageRows,ImageColumns,BitsAllocated,
            //                BitsStored,PixelSpacing,PhotometricIntr,KVP,Exposure,"",false,CommonStatus.ONLINE);
            ////image already exists, not update
            //DAPDALResults ret = tsImage.SaveToDB();
            //if (ret == DAPDALResults.DAL_EMREXISTS)
            //{
            //    Trace.WriteLineIf(m_Tracing.TraceWarning, "DicomHeader.SaveToDB(): " + "this image has already exist in database.");
            //    MessageObject msgObject = new MessageObject();
            //    msgObject.strMessageText = "DICOM_SAVEDB_EMREXISTS";
            //    Controller.Controller.statusNotifier.SendS2CMessage(msgObject);
            //    return 1;
            //}
            //else if (ret == DAPDALResults.DAL_INSERTFAILED)
            //    return 1;
            return 0;
        }


    }
}
