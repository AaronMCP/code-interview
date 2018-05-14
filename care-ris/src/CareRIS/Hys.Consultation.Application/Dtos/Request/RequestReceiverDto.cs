using System;

namespace Hys.Consultation.Application.Dtos
{
    public class RequestReceiverDto
    {
        public string RequestID { get; set; }

        /// <summary>
        /// Receive User
        /// </summary>
        public string ReceiveUserID { get; set; }

        /// <summary>
        /// Request belong to which hospital
        /// </summary>
        public string ReceiveHospitalID { get; set; }

        /// <summary>
        /// 0: center 1: expert
        /// </summary>
        public int ConsultantType { get; set; }

        /// <summary>
        /// the Id of center or expert
        /// </summary>
        public string[] ReceiverIDs { get; set; }

        /// <summary>
        /// Appointment Date
        /// </summary>
        public string ExpectedTimeRange { get; set; }
        public DateTime? ExpectedDate { get; set; }

        /// <summary>
        /// Consolution Date 
        /// </summary>
        public DateTime? ConsolutionDate { get; set; }
        public string ConsolutionTimeRange { get; set; }

        /// <summary>
        /// Is expected date
        /// </summary>
        public bool IsExpected { get; set; }

        /// <summary>
        /// Service Type
        /// </summary>
        public string ServiceTypeID { get; set; }
    }
}
