﻿using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class RequestListDto
    {
        public string UniqueID { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string ErNo { get; set; }

        /// <summary>
        /// 申请类型 
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// 申请类型代码
        /// </summary>
        public int? EventCode { get; set; }

        /// <summary>
        /// 申请部门
        /// </summary>
        public string ApplyDept { get; set; }

        /// <summary>
        /// 申请部门ID
        /// </summary>
        public string ApplyDeptNo { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary>
        public string ApplyDoctor { get; set; }

        /// <summary>
        /// 申请医生ID
        /// </summary>
        public string ApplyDoctorID { get; set; }


        /// <summary>
        /// 病人类型
        /// </summary>
        public string PatientType { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        public string Observation { get; set; }

        /// <summary>
        /// 病史
        /// </summary>
        public string HealthHistory { get; set; }

        public string EAcquisitionURL { get; set; }
        /// <summary>
        /// 收费类型
        /// </summary>
        public string ChargeType { get; set; }

        public string Reason { get; set; }

        public string InhospitalRegion { get; set; }

        public string InhospitalNo { get; set; }

        public string ClinicNo { get; set; }

        public string Comments { get; set; }

        public string BedNo { get; set; }

        public int? IsBedSide { get; set; }

        public int? IsThreedReBuild { get; set; }

        public int? Priority { get; set; }

        public string WebAcquisitionURL { get; set; }

        public string PatientID { get; set; }

        public string LocalName { get; set; }

        public string EnglishName { get; set; }

        public string Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string Telephone { get; set; }

        public string Address { get; set; }

        public string GlobalID { get; set; }

        public string MedicareNo { get; set; }

        public string ReferenceNo { get; set; }

        public string SocialSecurityNo { get; set; }

        public string HisID { get; set; }

        public string CardNo { get; set; }

        public string Domain { get; set; }

        public string Site { get; set; }

        public string IdentityNo { get; set; }
        public DateTime? RequestTime { get; set; }
        public DateTime? PutinTime { get; set; }

        public List<RequestItemDto> RequestItem { get; set; }

        public List<RequestChargeDto> RequestCharge { get; set; }
    }

}