using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.Platform.Domain;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class Procedurecode: Entity
    {

        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string ProcedureCode { get; set; }
        public string Description { get; set; }
        public string EnglishDescription { get; set; }
        public string ModalityType { get; set; }
        public string BodyPart { get; set; }
        public string CheckingItem { get; set; }
        public decimal? Charge { get; set; }
        public string Preparation { get; set; }
        public int? Frequency { get; set; }
        public string BodyCategory { get; set; }
        public int? Duration { get; set; }
        public string FilmSpec { get; set; }
        public int? FilmCount { get; set; }
        public string ContrastName { get; set; }
        public string ContrastDose { get; set; }
        public int? ImageCount { get; set; }
        public int? ExposalCount { get; set; }
        public string BookingNotice { get; set; }
        public string ShortcutCode { get; set; }
        public int? Enhance { get; set; }
        public int? ApproveWarningTime { get; set; }
        public int? Effective { get; set; }
        public string Domain { get; set; }
        public int? Externals { get; set; }
        public int? CheckingItemFrequency { get; set; }
        public string UniqueID { get; set; }
        public int TechnicianWeight { get; set; }
        public int RadiologistWeight { get; set; }
        public int ApprovedRadiologistWeight { get; set; }
        public string DefaultModality { get; set; }
        public string Site { get; set; }


        //
        public int Puncture { get; set; }
        public int Radiography { get; set; }
        public int? BodypartFrequency { get; set; }
        



    }
}
