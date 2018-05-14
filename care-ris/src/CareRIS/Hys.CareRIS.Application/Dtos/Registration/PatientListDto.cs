using System;

namespace Hys.CareRIS.Application.Dtos
{
    public class PatientListDto
    {
        public string UniqueID { get; set; }
        public string PatientID { get; set; }
        public string PatientNo { get; set; }
        public string LocalName { get; set; }
        public string EnglishName { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public bool? IsVip { get; set; }
        public string Marriage { get; set; }
        public string MedicareNo { get; set; }
        public string RemotePID { get; set; }
        public string Comments { get; set; }
        public string Alias { get; set; }
        public string Domain { get; set; }
        public string GlobalID { get; set; }
        public string Site { get; set; }
        public string SocialSecurityNo { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? IsUploaded { get; set; }
        public string MatchKey { get; set; }
        public string Optional1 { get; set; }
        public string Optional2 { get; set; }
        public string Optional3 { get; set; }
        public string ParentName { get; set; }
        public string HisID { get; set; }
    }
}
