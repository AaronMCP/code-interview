
namespace Hys.Consultation.Application.Dtos
{
    public class PatientBaseDto
    {
        public string PatientCaseID { get; set; }
        public string PatientName { get; set; }
        public string CurrentAge { get; set; }
        public string Gender { get; set; }
        public string PatientNo { get; set; }
        public int IsDeleted { get; set; }
        public string IdentityCard { get; set; }

        public string PatientBaseInfo
        {
            get
            {
                return PatientName + "  " + Gender + "  " + CurrentAge;
            }
        }
    }
}
