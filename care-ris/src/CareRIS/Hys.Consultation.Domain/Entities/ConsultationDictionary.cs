using Hys.Consultation.Domain.Enums;
using Hys.Platform.Domain;
using System;

namespace Hys.Consultation.Domain.Entities
{
    public partial class ConsultationDictionary : Entity
    {
        public override object UniqueId { get { return DictionaryID; } }

        public string DictionaryID { get; set; }
        public DictionaryType Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
