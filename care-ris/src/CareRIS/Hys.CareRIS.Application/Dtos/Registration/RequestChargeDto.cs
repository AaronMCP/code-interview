using System;

namespace Hys.CareRIS.Application.Dtos
{
    public class RequestChargeDto
    {
        public string UniqueID { get; set; }

        public string RequestID { get; set; }

        public string RequestItemID { get; set; }

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public double? Price { get; set; }

        public int? Amount { get; set; }

        public bool? IsItemCharged { get; set; }
    }
}
