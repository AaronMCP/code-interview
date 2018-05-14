using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.CareAgent.DAP.Entity;

namespace Hys.CareAgent.DAP
{
   public class DICOMInfoDto
    {
       public string  StudyInstanceUID { get; set; }
        public string SOPInstanceUID { get; set; }
        public string SeriesInstanceUID { get; set; }
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
        public DateTime? ReveiveTime { get; set; }
        public int SeriesNo { get; set; }
        public int ImageNo { get; set; }
        public string FilePath { get; set; }
        public DateTime CreateTime { get; set; }

        //public Study Study { get; set; }
        //public Series Series { get; set; }
        //public Image Image { get; set; }
    }
}
