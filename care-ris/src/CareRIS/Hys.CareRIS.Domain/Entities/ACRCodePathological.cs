﻿using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class ACRCodePathological : Entity
    {
        public override object UniqueId { get { return UniqueID; } }

        public string UniqueID { get; set; }
        public string AID { get; set; }
        public string PID { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public string Domain { get; set; }
    }
}
