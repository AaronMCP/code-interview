using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hys.CareAgent.DAP.Entity;
using kdt_managed;

namespace Hys.CareAgent.DAP.Mappers
{

    public class DICOMInfoMapper : Profile
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");

        public new string ProfileName
        {
            get { return this.GetType().Name; }
        }

        protected override void Configure()
        {

            AutoMapper.Mapper.CreateMap<MElementList, DICOMInfo>()
                .ForMember(el => el.PatientID, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kPatientID)))
                 .ForMember(el => el.PatientName, opt => opt.MapFrom(dc => GetLocalStringTagValue(dc, tag_t.kPatientsName)))
                  .ForMember(el => el.PatientDOB, opt => opt.MapFrom(dc => GetDateTimeTagValue(dc, tag_t.kPatientsBirthDate)))
                   .ForMember(el => el.PatientAge, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kPatientsAge)))
                    .ForMember(el => el.PatientSex, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kPatientsSex)))

                    .ForMember(el => el.StudyInstanceUID, opt => opt.MapFrom(dc => GetUIDTagValue(dc, tag_t.kStudyInstanceUID)))
                 .ForMember(el => el.AccessionNo, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kAccessionNumber)))
                  .ForMember(el => el.BodyPart, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kBodyPartExamined)))
                   .ForMember(el => el.Modality, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kModality)))
                    .ForMember(el => el.StudyDate, opt => opt.MapFrom(dc => GetDateTimeTagValue(dc, tag_t.kStudyDate)))
                    .ForMember(el => el.StudyTime, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kStudyTime)))
                 .ForMember(el => el.StudyDescription, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kStudyDescription)))
                  .ForMember(el => el.ReferPhysician, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kReferringPhysiciansName)))
                   .ForMember(el => el.Manufacture, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kManufacturer)))
                    .ForMember(el => el.InstitutionName, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kInstitutionName)))
                    .ForMember(el => el.StationName, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kStationName)))
                 .ForMember(el => el.SeriesInstanceUID, opt => opt.MapFrom(dc => GetUIDTagValue(dc, tag_t.kSeriesInstanceUID)))
                  .ForMember(el => el.SeriesNo, opt => opt.MapFrom(dc => GetIntTagValue(dc, tag_t.kSeriesNumber)))
                   .ForMember(el => el.PatientPosition, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kPatientPosition)))
                    .ForMember(el => el.ViewPosition, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kViewPosition)))

                           .ForMember(el => el.SeriesDate, opt => opt.MapFrom(dc => GetDateTimeTagValue(dc, tag_t.kSeriesDate)))
                 .ForMember(el => el.SeriesTime, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kSeriesTime)))
                  .ForMember(el => el.SeriesDescription, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kSeriesDescription)))
                   .ForMember(el => el.SOPInstanceUID, opt => opt.MapFrom(dc => GetUIDTagValue(dc, tag_t.kSOPInstanceUID)))
                    .ForMember(el => el.ImageNo, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kInstanceNumber)))
                    .ForMember(el => el.ImageType, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kImageType)))
                 .ForMember(el => el.NumberOfFrames, opt => opt.MapFrom(dc => GetIntTagValue(dc, tag_t.kNumberOfFrames)))
                  .ForMember(el => el.ImageDate, opt => opt.MapFrom(dc => GetDateTimeTagValue(dc, tag_t.kAcquisitionDate)))
                   .ForMember(el => el.ImageTime, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kAcquisitionTime)))
                    .ForMember(el => el.SamplesPerPixel, opt => opt.MapFrom(dc => GetIntTagValue(dc, tag_t.kSamplesPerPixel)))
                      .ForMember(el => el.ImageRows, opt => opt.MapFrom(dc => GetIntTagValue(dc, tag_t.kRows)))
                        .ForMember(el => el.ImageColumns, opt => opt.MapFrom(dc => GetIntTagValue(dc, tag_t.kColumns)))
                          .ForMember(el => el.BitsAllocated, opt => opt.MapFrom(dc => GetIntTagValue(dc, tag_t.kBitsAllocated)))
                            .ForMember(el => el.BitsStored, opt => opt.MapFrom(dc => GetIntTagValue(dc, tag_t.kBitsStored)))
                    .ForMember(el => el.PixelSpacing, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kPixelSpacing)))
                 .ForMember(el => el.PhotometricIntr, opt => opt.MapFrom(dc => GetStringTagValue(dc, tag_t.kPhotometricInterpretation)))
                  .ForMember(el => el.KVP, opt => opt.MapFrom(dc => GetIntTagValue(dc, tag_t.kKVP)))
                   .ForMember(el => el.Exposure, opt => opt.MapFrom(dc => GetIntTagValue(dc, tag_t.kExposureTime)));
        }


        private string GetUIDTagValue(MElementList elmlist, tag_t tag)
        {
            var strUID = string.Empty;
            using (MElementRef eref = elmlist.get_Element(tag))
            {
                if (eref != null && eref.value_count != 0 && eref.value_count != -1)
                    strUID = eref.get_uid(0);
                return strUID;
            }
        }

        private string GetStringTagValue(MElementList elmlist, tag_t tag)
        {
            var strValue = string.Empty;
            try
            {
                using (MElementRef eref = elmlist.get_Element(tag))
                {
                    if (eref != null && eref.value_count != 0 && eref.value_count != -1)
                        strValue = eref.get_string(0);
                    else
                        strValue = "";
                    return strValue;
                }
            }
            catch (Exception e)
            {
                _logger.Error("DicomHeader.GetStringTagValue(): " + "get string for tag " + tag.ToString() + " pop up an exception--" + e.Message + ".");
                return strValue;
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
        private string GetLocalStringTagValue(MElementList elmlist, tag_t tag)
        {
            var strValue = string.Empty;
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
                    return strValue;
                }
            }
            catch (Exception e)
            {
                _logger.Error("DCMHeader.GetStringTagValue(): " + "get string for tag " + tag.ToString() + " pop up an exception--" + e.Message + ".");
                return strValue;
            }
        }
        private int GetIntTagValue(MElementList elmlist, tag_t tag)
        {
            var nValue = 0;
            try
            {
                using (MElementRef eref = elmlist.get_Element(tag))
                {
                    if (eref != null && eref.value_count != 0 && eref.value_count != -1)
                    {
                        nValue = eref.get_sint(0);
                    }
                    return nValue;
                }
            }
            catch (Exception e)
            {
                _logger.Error("DCMHeader.GetIntTagValue(): " + "get int for tag " + tag.ToString() + " pop up an exception--" + e.Message + ".");
                return nValue;
            }
        }

        private string GetDateTimeTagValue(MElementList elmlist, tag_t tag)
        {
            var strValue = string.Empty;
            var strRet = string.Empty;
            try
            {
                using (MElementRef eref = elmlist.get_Element(tag))
                {
                    if (eref != null && eref.value_count != 0 && eref.value_count != -1)
                    {
                        strValue = eref.get_string(0);
                    }
                    if (!string.IsNullOrEmpty((strValue)))
                    {
                        strRet = System.DateTime.ParseExact(strValue, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");
                    }
                    return strRet;
                }
            }
            catch (Exception e)
            {
                _logger.Error("DCMHeader.GetDateTimeTagValue(): " + "get datetime for tag " + tag.ToString() + " pop up an exception--" + e.Message + ".");
                strRet = System.DateTime.ParseExact(strValue, "yyyy.MM.dd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");
                return strRet;
            }
        }
    }
}
