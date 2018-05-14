using Hys.Platform.Domain;
using System;


namespace Hys.Consultation.Domain.Entities
{
    public partial class PatientCase : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        /// <summary>
        /// PatientCaseID
        /// </summary>
        public string UniqueID { get; set; }

        /// <summary>
        /// PatientID
        /// </summary>
        public string PatientNo { get; set; }

        public string HospitalId { get; set; }

        public string PatientName { get; set; }

        public string PatientNamePy { get; set; }

        public string IdentityCard { get; set; }

        public string InsuranceNumber { get; set; }

        public string Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public string Telephone { get; set; }

        public string Age { get; set; }

        public string ClinicalDiagnosis { get; set; }

        public string History { get; set; }

        public int? Progress { get; set; }

        public string Creator { get; set; }
        public string CreatorName { get; set; }

        public DateTime CreateTime { get; set; }

        public int Status { get; set; }

        public string LastEditUser { get; set; }

        public DateTime LastEditTime { get; set; }

        /// <summary>
        /// Delete info
        /// </summary>
        public int IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string DeleteReason { get; set; }
        public string DeleteUser { get; set; }
        public string DeletePublicAccountName { get; set; }

        /// <summary>
        /// orderId from RIS
        /// </summary>
        public string OrderID { get; set; }
    }
}
