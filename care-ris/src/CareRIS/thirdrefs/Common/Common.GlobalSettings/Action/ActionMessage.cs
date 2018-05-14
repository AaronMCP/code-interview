using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Action
{
    [Serializable()]
    public class ActionMessage
    {
        private string messageCode = null;
        private string message = null;

        public ActionMessage()
        {

        }

        public ActionMessage(string messageCode, string message)
        {
            this.messageCode = messageCode;
            this.message = message;
        }

        public string MessageCode 
        {
            get
            {
                return messageCode;
            }
            set
            {
                messageCode = value;
            }
        }

        public string Message 
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
    }
}
