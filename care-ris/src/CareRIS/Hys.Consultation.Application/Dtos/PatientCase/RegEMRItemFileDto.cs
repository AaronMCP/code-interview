using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos.PatientCase
{
    public class RegEMRItemFileDto
    {
        public string UniqueID { get; set; }
        public int ItemType { get; set; }
        public string DicomPrefix { get; set; }
        public string SrcFilePath { get; set; }
        public string SrcInfo { get; set; }
        public string FileName { get; set; }
        public string CreatorID { get; set; }
        public string Description { get; set; }
        public bool? IsFromRis { get; set; }
        public StudyDto Study { get; set; }
    }

    public class StudyDto
    {
        public string UniqueID { get; set; }
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientDOB { get; set; }
        public string PatientAge { get; set; }
        public string PatientSex { get; set; }
        public string AccessionNo { get; set; }
        public string BodyPart { get; set; }
        public string Modality { get; set; }
        public string ExamCode { get; set; }
        public string StudyDate { get; set; }
        public string StudyTime { get; set; }
        public string StudyDescription { get; set; }
        public string ReferPhysician { get; set; }
    }
}
