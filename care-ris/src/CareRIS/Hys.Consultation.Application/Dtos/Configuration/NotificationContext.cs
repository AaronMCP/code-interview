using System;
using System.Collections.Generic;
using Hys.CrossCutting.Common.Extensions;

namespace Hys.Consultation.Application.Dtos.Configuration
{
    public class NotificationContext
    {
        public string Translate(string template)
        {
            var mapping = new Dictionary<string, string>{
                {"医生所在医院",DoctorHospitalName},
                {"医生姓名",DoctorName},
                {"病人姓名",PatientName},
                {"会诊类型",ConsolutionType},
                {"会诊日期",ConsolutionDate},
                {"会诊管理员姓名",ConsolutionAdminName},
                {"会诊管理员所在医院",ConsolutionHospitalName},
            };
            var text = template;
            if (!String.IsNullOrEmpty(template))
            {
                mapping.ForEach(m => { text = text.Replace("{" + m.Key + "}", m.Value); });
            }
            return text;
        }

        public string ConsolutionHospitalName { get; set; }
        public string ConsolutionAdminName { get; set; }
        public string ConsolutionDate { get; set; }
        public string ConsolutionType { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string DoctorHospitalName { get; set; }
    }
}
