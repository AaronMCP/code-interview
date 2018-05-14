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

namespace CommonGlobalSettings
{
    [Serializable()]
    public class ResourceModel : OamBaseModel
    {
        private string modalityGuid = null;
        private string modalityName = null;
        private string modalityType = null;
        private string room = null;
        private string ipAddress = null;
        private string maxLoad = null;
        private string description = null;
        private int bookingShowMode = 0;
        private bool applyHaltPeriod = false;
        private bool addModalityType = false;
        private bool deleteModalityType = false;
        private DateTime beginDt ;
        private DateTime endDt ;
        private string strDomain; 
        private string site;
        private string workstationIP = null;


        public string ModalityGuid 
        {
            get
            {
                return modalityGuid;
            }
            set
            {
                modalityGuid = value;
            }
        }

        public string ModalityName 
        {
            get
            {
                return modalityName;
            }
            set
            {
                modalityName = value;
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

        public string Room 
        {
            get
            {
                return room;
            }
            set
            {
                room = value;
            }
        }

        public string IPAddress 
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
            }
        }

        public string MaxLoad 
        {
            get
            {
                return maxLoad;
            }
            set
            {
                maxLoad = value;
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

        public string Domain
        {
            get
            {
                return strDomain;
            }
            set
            {
                strDomain = value;

            }
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

        public int BookingShowMode
        {
            get
            {
                return bookingShowMode;
            }
            set
            {
                bookingShowMode = value;

            }
        }

        public bool ApplyHaltPeriod
        {
            get
            {
                return applyHaltPeriod;
            }
            set
            {
                applyHaltPeriod = value;

            }
        }

        public bool AddModalityType
        {
            get
            {
                return addModalityType;
            }
            set
            {
                addModalityType = value;
            }
        }

        public bool DeleteModalityType
        {
            get
            {
                return deleteModalityType;
            }
            set
            {
                deleteModalityType = value;
            }
        }

        public DateTime BeginDt
        {
            get
            {
                return beginDt;
            }
            set
            {
                beginDt = value;
            }
        }

        public DateTime EndDt
        {
            get
            {
                return endDt;
            }
            set
            {
                endDt = value;
            }
        }

        public string WorkStationIP
        {
            get
            {
                return workstationIP;
            }
            set
            {
                workstationIP = value;
            }
        }
        public override ActionMessage Validator()
        {
            //Modality
            if(modalityName.Length > 32)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Modality is too long.";
                return actionMessage;
            }

            if(!IsValidString(modalityName))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Modality is not valid.";
                return actionMessage;
            }

            //Room
            if(room.Length > 256)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Room is too long.";
                return actionMessage;
            }

            if(!IsValidString(room))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Room is not valid.";
                return actionMessage;
            }

            //IPAddress
            //string express = "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$";
            string express = "^(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])$";
            Regex regex = new Regex(express);
            if(!regex.IsMatch(IPAddress))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The IP Address is not valid.";
                return actionMessage;
            }

            //WorkStation IPAddress
            if (!regex.IsMatch(WorkStationIP))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The WorkStation IP Address is not valid.";
                return actionMessage;
            }

            //MaxLoad
            try
            {
                int maxLoadValue = Convert.ToInt32(MaxLoad);
                if(maxLoadValue <= 0)
                {
                    ActionMessage actionMessage = new ActionMessage();
                    actionMessage.Message = "The MaxLoad must be positive integer.";
                    return actionMessage;
                }
            }
            catch(Exception e)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The MaxLoad must be positive integer.";
                return actionMessage;
            }

            //Description
            if(description.Length > 256)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Description is too long.";
                return actionMessage;
            }

            //if (!IsValidString(description))
            //{
            //    ActionMessage actionMessage = new ActionMessage();
            //    actionMessage.Message = "The Description is not valid.";
            //    return actionMessage;
            //}
            //begin datetime and end datetime check
            if (applyHaltPeriod)
            {
                if (beginDt >= endDt)
                {
                    ActionMessage actionMessage = new ActionMessage();
                    actionMessage.Message = "TheEndTimeMustBeGreaterThanBeginTime";
                    return actionMessage;
                }
            }

            return null;
        }

        private bool IsValidString(string strName)
        {
            if (strName.Contains(@"'")||strName.Contains(@"&") || strName.Contains(@"="))
            {
                return false;
            }

            return true;
        }
    }
}
