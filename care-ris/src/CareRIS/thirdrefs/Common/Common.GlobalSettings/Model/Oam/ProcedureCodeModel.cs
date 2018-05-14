#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using System.Text.RegularExpressions;
using System.Data;
namespace CommonGlobalSettings
{
    [Serializable()]
    public class ProcedureCodeModel : OamBaseModel
    {
        private string procedureCode = null;
        private string modalityType = null;
        private string checkingItem = null;
        private string defaultModality = null;
        private string clinicalModality = null;
        private string frequency = "0";
        private string bodyPartFrequency = "0";
        private string checkingItemFrequency = "0";
        private string chargeRate = "0";
        private string contrastName = null;
        private string contrastDose = null;
        private string description = null;
        private string englishDescription = null;
        private string bodyCategory = null;
        private string bodyPart = null;
        private string filmSpec = null;
        private string filmCount = "0";
        private string duration = null;
        private string preparation = null;
        private string imageCount = "0";
        private string exposalCount = "0";
        private string bookingNotice = null;
        private string shortcutCode = null;
        private string examSystem = null;
        private int enhance = 0;
        private int effective = 0;
        private int radiography = 0;
        private int puncture = 0;
        private int useOldCharge = 1;//default use old procedurecode's charge
        private DataSet ds = null;
        private string domain = null;
        private int technicianWeight = 1;
        private int radiologistWeight = 1;
        private int approvedRadiologistWeight = 1;
        private string site = null;

        public string ProcedureCode
        {
            get
            {
                return procedureCode;
            }
            set
            {
                procedureCode = value;
            }
        }

        public string ModalityType
        {
            get
            {
                return modalityType;
            }
            set
            {
                modalityType = value;
            }
        }

        public string CheckingItem
        {
            get
            {
                return checkingItem;
            }
            set
            {
                checkingItem = value;
            }
        }

        public string DefaultModality
        {
            get
            {
                return defaultModality;
            }
            set
            {
                defaultModality = value;
            }
        }

        public string ClinicalModality
        {
            get
            {
                return clinicalModality;
            }
            set
            {
                clinicalModality = value;
            }
        }
        
        public string Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                if (value.Trim().Equals(""))
                {
                    frequency = "0";
                }
                else
                {
                    frequency = value;
                }
            }
        }

        public string BodyPartFrequency
        {
            get
            {
                return bodyPartFrequency;
            }
            set
            {
                if (value.Trim().Equals(""))
                {
                    bodyPartFrequency = "0";
                }
                else
                {
                    bodyPartFrequency = value;
                }
            }
        }

        public string CheckingItemFrequency
        {
            get
            {
                return checkingItemFrequency;
            }
            set
            {
                if (value.Trim().Equals(""))
                {
                    checkingItemFrequency = "0";
                }
                else
                {
                    checkingItemFrequency = value;
                }
            }
        }

        public string ChargeRate
        {
            get
            {
                return chargeRate;
            }
            set
            {
                if (value.Trim().Equals(""))
                {
                    chargeRate = "0";
                }
                else
                {
                    chargeRate = value;
                }
            }
        }

        public string ContrastName
        {
            get
            {
                return contrastName;
            }
            set
            {
                contrastName = value;
            }
        }

        public string ContrastDose
        {
            get
            {
                return contrastDose;
            }
            set
            {
                contrastDose = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public string EnglishDescription
        {
            get
            {
                return englishDescription;
            }
            set
            {
                englishDescription = value;
            }
        }

        public string BodyCategory
        {
            get
            {
                return bodyCategory;
            }
            set
            {
                bodyCategory = value;
            }
        }

        public string BodyPart
        {
            get
            {
                return bodyPart;
            }
            set
            {
                bodyPart = value;
            }
        }

        public string FilmSpec
        {
            get
            {
                return filmSpec;
            }
            set
            {
                filmSpec = value;
            }
        }

        public string FilmCount
        {
            get
            {
                return filmCount;
            }
            set
            {
                if (value.Trim().Equals(""))
                {
                    filmCount = "0";
                }
                else
                {
                    filmCount = value;
                }
            }
        }

        public string Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }

        public string Preparation
        {
            get
            {
                return preparation;
            }
            set
            {
                preparation = value;
            }
        }

        public string ImageCount
        {
            get
            {
                return imageCount;
            }
            set
            {
                if (value.Trim().Equals(""))
                {
                    imageCount = "0";
                }
                else
                {
                    imageCount = value;
                }
            }
        }

        public string ExposalCount
        {
            get
            {
                return exposalCount;
            }
            set
            {
                if (value.Trim().Equals(""))
                {
                    exposalCount = "0";
                }
                else
                {
                    exposalCount = value;
                }
            }
        }

        public string BookingNotice
        {
            get
            {
                return bookingNotice;
            }
            set
            {
                bookingNotice = value;
            }
        }

        public string ShortcutCode
        {
            get
            {
                return shortcutCode;
            }
            set
            {
                shortcutCode = value;
            }
        }

        public string ExamSystem
        {
            get
            {
                return examSystem;
            }
            set
            {
                examSystem = value;
            }
        }

        public int Enhance
        {
            get
            {
                return enhance;
            }
            set
            {
                enhance = value;
            }
        }

        public int Effective
        {
            get
            {
                return effective;
            }
            set
            {
                effective = value;
            }
        }

        public int Puncture
        {
            get
            {
                return puncture;
            }
            set
            {
                puncture = value;
            }
        }

        public int Radiography
        {
            get
            {
                return radiography;
            }
            set
            {
                radiography = value;
            }
        }

        public DataSet Ds
        {
            get
            {
                return ds;
            }
            set
            {
                ds = value;
            }
        }

        public int UseOldCharge
        {
            get
            {
                return useOldCharge;
            }
            set
            {
                useOldCharge = value;
            }
        }

        public string Domain
        {
            get
            {
                return domain;
            }
            set
            {
                domain = value;
            }
        }

        public int TechnicianWeight
        {
            get
            {
                return technicianWeight;
            }
            set
            {
                technicianWeight = value;
            }
        }

        public int RadiologistWeight
        {
            get
            {
                return radiologistWeight;
            }
            set
            {
                radiologistWeight = value;
            }
        }

        public int ApprovedRadiologistWeight
        {
            get { return this.approvedRadiologistWeight; }
            set { this.approvedRadiologistWeight = value; }
        }

        public string Site
        {
            get
            {
                return site;
            }
            set
            {
                site = value;
            }
        }

        public override void Reset()
        {
            procedureCode = "";
            modalityType = "";
            checkingItem = "";
            frequency = "";
            chargeRate = "";
            contrastName = "";
            contrastDose = "";
            description = "";
            englishDescription = "";
            bodyCategory = "";
            bodyPart = "";
            filmSpec = "";
            filmCount = "";
            duration = "";
            preparation = "";
            imageCount = "";
            exposalCount = "";
            bookingNotice = "";
            shortcutCode = "";
            enhance = 0;
            effective = 0;
            ds = null;
            domain = "";
            technicianWeight = 1;
            approvedRadiologistWeight = 1;
            site = "";

        }

        public override ActionMessage Validator()
        {
            if (procedureCode.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "'Procedure Code' must not be empty!";
                return actionMessage;
            }

            //判断用户输入内容是否有效
            if (!Regex.IsMatch(procedureCode.Trim(), @"^[A-Za-z_0-9]+$"))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "ProcedurecodeInputValid";
                return actionMessage;
            }

            if (modalityType.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "'Modality' must not be empty!";
                return actionMessage;
            }

            if (bodyPart.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "'Body Part' must not be empty!";
                return actionMessage;
            }

            if (checkingItem.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "'Checking Item' must not be empty!";
                return actionMessage;
            }

            if (bodyCategory.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "'Body Category' must not be empty!";
                return actionMessage;
            }

            if (duration.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "'Duration' should not be less than 10!";
                return actionMessage;
            }
            if (!IsValid(modalityType))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "Modality Type contain invalid charactor";
                return actionMessage;

            }

            if (!IsValid(checkingItem))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "CheckingItem contain invalid charactor";
                return actionMessage;
            }

            if (!IsValid(contrastName))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "contrastName contain invalid charactor";
                return actionMessage;
            }
            if (!IsValid(description))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "description contain invalid charactor";
                return actionMessage;
            }
            if (!IsValid(englishDescription))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "englishDescription contain invalid charactor";
                return actionMessage;
            }

            if (!IsValid(bodyCategory))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "bodyCategory contain invalid charactor";
                return actionMessage;
            }
            if (!IsValid(bodyPart))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "bodyPart contain invalid charactor";
                return actionMessage;
            }
            if (!IsValid(preparation))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "preparation contain invalid charactor";
                return actionMessage;
            }
            if (!IsValidExcludeComma(bookingNotice))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "bookingNotice contain invalid charactor";
                return actionMessage;
            }
            if (!IsValid(shortcutCode))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "shortcutCode contain invalid charactor";
                return actionMessage;
            }
            if (!IsValid(contrastDose))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "ContrastDose contain invalid charactor";
                return actionMessage;
            }
            if (englishDescription != "" && Regex.IsMatch(englishDescription, "[^a-zA-Z0-9 ]"))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "English Description must input alphabeta or numberic charactor";
                return actionMessage;
            }

            return null;
        }
        private bool IsValid(string strItem)
        {
            if (Regex.IsMatch(strItem, "['\"|,]"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidExcludeComma(string strItem)
        {
            if (Regex.IsMatch(strItem, "['\"|]"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ActionMessage IsFrequencyValid()
        {
            ActionMessage actionMessage = new ActionMessage();
            if (string.IsNullOrEmpty(frequency.Trim()) || string.IsNullOrEmpty(bodyPartFrequency.Trim()) || string.IsNullOrEmpty(checkingItemFrequency.Trim()))
            {
                actionMessage.Message = "FrequencyCanNotBeEmpty";
            }
            else if (!Regex.IsMatch(frequency.Trim(), @"^[0-9]+$") || !Regex.IsMatch(bodyPartFrequency.Trim(), @"^[0-9]+$") || !Regex.IsMatch(checkingItemFrequency.Trim(), @"^[0-9]+$"))
            {
                actionMessage.Message = "FrequencyShouldOnlyBeNumber";
            }
            return actionMessage;
        }
    }
}
