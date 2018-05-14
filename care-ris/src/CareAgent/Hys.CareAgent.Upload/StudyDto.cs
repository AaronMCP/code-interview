using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hys.CareAgent.Upload
{
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
