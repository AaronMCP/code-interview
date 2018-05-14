using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.CareAgent.DAP.Entity;

namespace Hys.CareAgent.DAP
{
    public class DICOMInfo
    {      //Patient Group
        public String PatientID{ get; set; }
        public String PatientName{ get; set; }
        public String PatientDOB { get; set; }
        public String PatientAge { get; set; }
        public String PatientSex { get; set; }
        //Study Group
        public String StudyInstanceUID { get; set; }
        public String AccessionNo { get; set; }
        public String BodyPart { get; set; }
        public String Modality { get; set; }
        public String StudyDate { get; set; }
        public String StudyTime { get; set; }
        public String StudyDescription { get; set; }
        public String ReferPhysician { get; set; }
        public String Manufacture { get; set; }
        public String InstitutionName { get; set; }
        public String StationName { get; set; }
        //Series Group
        public String SeriesInstanceUID { get; set; }
        public Int32 SeriesNo { get; set; }
        public String PatientPosition { get; set; }
        public String ViewPosition { get; set; }
        public String SeriesDate { get; set; }
        public String SeriesTime { get; set; }
        public String SeriesDescription { get; set; }

        //Image Group
        public String SOPInstanceUID { get; set; }
        public String ImageNo { get; set; }
        public String ImageType { get; set; }
        public Int32 NumberOfFrames { get; set; }
        public String ImageDate { get; set; }
        public String ImageTime { get; set; }
        public Int32 SamplesPerPixel { get; set; }
        public Int32 ImageRows { get; set; }
        public Int32 ImageColumns { get; set; }
        public Int32 BitsAllocated { get; set; }
        public Int32 BitsStored { get; set; }
        public String PixelSpacing { get; set; }
        public String PhotometricIntr { get; set; }
        public Int32 KVP { get; set; }
        public Int32 Exposure { get; set; }

    }
}
