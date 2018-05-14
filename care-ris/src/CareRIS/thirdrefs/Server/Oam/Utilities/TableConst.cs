using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Utilities.Oam
{
    public class TableConst
    {
        #region Table Name in DataSet
        public const string ModalityType = "ModalityType";
        public const string BodyCategory = "BodyCategory";
        public const string FilmSpecTable = "FilmSpec";
        public const string ProcedureCode = "ProcedureCode";
        public const string BodySystemMap = "tBodySystemMap";
        public const string BodyPartTable = "BodyPart";
        public const string WorkTimeTable = "tWorkTime";
        public const string UserTable = "tUser";
        public const string EmployeePlanTable = "tEmployeePlan";
        public const string ModalityPlanTable = "tModalityPlan";
        public const string ModalityTable = "tModality";
        public const string RadiologistTable = "Radiologist";
        public const string TechnicianTable = "Technician";
        public const string NurseTable = "Nurse";
        public const string Calendar = "WorkingCalendar";
        #endregion

        #region Table Column Name in Table
        public const string ExamSystemColumn = "ExamSystem";
        public const string BodyPartColumn = "BodyPart";
        public const string WorkTimeNameColumn = "WorkTimeName";
        public const string WorkTimeGuidColumn = "WTGuid";
        public const string StartDateColumn = "StartDt";
        public const string EndDateColumn = "EndDt";
        public const string LoginNameColumn = "LoginName";
        public const string LocalNameColumn = "LocalName";
        public const string DepartmentColumn = "Department";
        public const string UserGuidColumn = "UserGuid";
        public const string UserNameColumn = "UserName";
        public const string ModalityColumn = "Modality";
        public const string ModalityGuidColumn = "ModalityGuid";
        public const string ModalityTypeColumn = "ModalityType";
        #endregion

        #region Tag Const in Dictionary

        public const string FilmSpecTag = "4";
        #endregion
    }
}
