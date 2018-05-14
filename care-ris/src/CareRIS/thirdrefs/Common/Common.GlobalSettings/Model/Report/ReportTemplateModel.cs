using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using System.Data;


namespace CommonGlobalSettings
{
    [Serializable()]
    public class ReportTemplateModel : ReportTemplateBaseModel
    {
        private string templateName;
        private string modalityType;
        private string bodyPart;
        private string wys;
        private string wyg;
        private string appendInfo;
        private string techInfo;
        private string checkItemName;
        private string doctorAdvice;
        private string shortcutCode;
        private string acrCode;
        private string acrAnatomicDesc;
        private string acrPathologicDesc;
        private string bodyCategory;
        private string gender;
        private int positive;
        private DataSet directoryDataSet = null;
        

        #region Property
        public DataSet DirectoryDataSet
        {
            get
            {
                return directoryDataSet;
            }
            set
            {
                directoryDataSet = value;
            }
        }
        public string TemplateName
        {
            get 
            {
                return templateName;
            }
            set
            {
                
                    templateName = value;
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
        public string WYS
        {
            get
            {
                return wys;
            }
            set 
            {
                wys = value;
            }
        }
        public string WYG
        {
            get
            {
                return wyg;
            }
            set
            {
                wyg = value;
            }
        }
        public string AppendInfo
        {
            get
            {
                return appendInfo;
            }
            set
            {
                appendInfo = value;
            }
        }
        public string TechInfo
        {
            get
            {
                return techInfo;
            }
            set
            {
                techInfo = value;
            }
        }
        public string CheckItemName
        {
            get
            {
                return checkItemName;
            }
            set
            {
                checkItemName = value;
            }
        }
        public string DoctorAdvice
        {
            get
            {
                return doctorAdvice;
            }
            set
            {
                doctorAdvice = value;
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
        public string ACRCode
        {
            get
            {
                return acrCode;
            }
            set
            {
                acrCode = value;
            }
        }
        public string ACRAnatomicDesc
        {
            get
            {
                return acrAnatomicDesc;
            }
            set
            {
                acrAnatomicDesc = value;
            }
        }
        public string ACRPathologicDesc
        {
            get
            {
                return acrPathologicDesc;
            }
            set
            {
                acrPathologicDesc = value;
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
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public int Positive
        {
            get { return positive; }
            set { positive = value; }
        }
        #endregion
        public override void Reset()
        {
            templateName = "";
            modalityType = "";
            bodyPart = "";
            wys = "";
            wyg = "";
            appendInfo = "";
            techInfo = "";
            checkItemName = "";
            doctorAdvice = "";
            shortcutCode = "";
            acrCode = "";
            acrAnatomicDesc = "";
            acrPathologicDesc = "";
            bodyCategory = "";
            positive = 0;
            

        }
        public override ActionMessage Validator()
        {
            if (templateName.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "'Template Name must not be empty!'";
                return actionMessage;
            }
            if (modalityType.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "'Modality Type must not be empty!'";
                return actionMessage;
            }
            if (bodyPart.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "'Body Part must not be empty!'";
                return actionMessage;
            }
          
            return null;
        }
    }
}
