using System;
using AutoMapper;
using Hys.Consultation.Domain.Entities;

namespace Hys.Consultation.Application.Dtos
{
    public class RoleDto
    {
        public string UniqueID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Permissions { get; set; }
        public DateTime LastEditTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSystem { get; set; }
    }
}