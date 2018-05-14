using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
namespace CommonGlobalSettings
{
    [Serializable()]
    public class ExaminTemplateModel : ReportTemplateBaseModel
    {
        private string templateName;
        private string modalityType;

        private string templateInfo;
        private string shortcutCode;

        private int type;
        private string templateGuid;
        private string userGuid;


        #region Property
        public string TemplateGuid
        {
            get
            {
                return templateGuid;
            }
            set
            {
                templateGuid = value;
            }
        }
        public int Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
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
        public string TemplateInfo
        {
            get
            {
                return templateInfo;
            }
            set
            {
                templateInfo = value;
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
        public string UserGuid
        {
            get
            {
                return userGuid;
            }
            set
            {
                userGuid = value;
            }
        }
        public string Site { get; set; }

        #endregion
        public override void Reset()
        {
            templateName = "";
            modalityType = "";

            templateInfo = "";
            shortcutCode = "";



        }
        public override ActionMessage Validator()
        {
            if (templateName.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "'Template Name must not be empty!'";
                return actionMessage;
            }
            if (templateGuid.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "'Template Guid must not be empty!'";
                return actionMessage;
            }

            if (!type.Equals(0) && !type.Equals(1))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "type must be '0' or '1'!'";
                return actionMessage;
            }

            return null;
        }
    }
}
