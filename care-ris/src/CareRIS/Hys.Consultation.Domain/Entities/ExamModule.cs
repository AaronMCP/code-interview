using Hys.Platform.Domain;
using System;

namespace Hys.Consultation.Domain.Entities
{
    public partial class ExamModule : Entity
    {
        public override object UniqueId { get { return ID; } }

        public int ID { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Position { get; set; }
        public bool Visible { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
