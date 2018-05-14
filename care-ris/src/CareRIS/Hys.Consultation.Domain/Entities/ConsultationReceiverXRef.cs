
using Hys.Platform.Domain;
using System;

namespace Hys.Consultation.Domain.Entities
{
  public partial  class ConsultationReceiverXRef:Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }

        public string ConsultationRequestID { get; set; }

        /// <summary>
        /// expert user id
        /// </summary>
        public string ReceiverID { get; set; }
    }
}
