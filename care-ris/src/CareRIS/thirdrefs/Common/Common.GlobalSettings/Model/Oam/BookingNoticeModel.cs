using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;

namespace CommonGlobalSettings
{
    public class BookingNoticeModel : OamBaseModel
    {
        private string guid = null;
        public string Guid
        {
            get
            {
                return guid;
            }
            set
             {
                guid = value;
            }
        }

        private string guids = null;
        public string Guids
        {
            get
            {
                return guids;
            }
            set
            {
                guids = value;
            }
        }

        private string templateName = null;
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

        private string bookingNotice = null;
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

        private string modalityType = null;
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
    }
}
