using Hys.Platform.Domain;
using System;

namespace Hys.Consultation.Domain.Entities
{
    public partial class ConsultationReport : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string RequestID { get; set; }

        /// <summary>
        /// wirter
        /// </summary>
        public string EditorID { get; set; }
        public string Advice { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Description { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
