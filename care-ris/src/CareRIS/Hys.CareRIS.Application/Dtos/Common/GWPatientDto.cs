using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class GWPatientDto
    {
        public string UniqueID { get; set; }
        public DateTime? DataTime { get; set; }
        public string PatientID { get; set; }
        public string PriorPatientID { get; set; }
        public string OtherPID { get; set; }
        public string PatientName { get; set; }
        public string PatientLocalName { get; set; }
        public string MotherMaidenName { get; set; }
        public string BirthDate { get; set; }
        public string Sex { get; set; }
        public string PatientAlias { get; set; }
        public string Race { get; set; }
        public string Address { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumberHome { get; set; }
        public string PhoneNumberBusiness { get; set; }
        public string PrimaryLanguage { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string AccountNumber { get; set; }
        public string SSNBumber { get; set; }
        public string DriverLicNumber { get; set; }
        public string EthnicGroup { get; set; }
        public string BirthPlace { get; set; }
        public string CitizenShip { get; set; }
        public string VeteransMilStatus { get; set; }
        public string Nationality { get; set; }
        public string PatientType { get; set; }
        public string PatientLocation { get; set; }
        public string PatientStatus { get; set; }
        public string VisitNumber { get; set; }
        public string BedNumber { get; set; }
        public string Customer1 { get; set; }
        public string Customer2 { get; set; }
        public string Customer3 { get; set; }
        public string Customer4 { get; set; }
        public string PriorPatientName { get; set; }
        public string PriorVisitNumber { get; set; }
    }
}
