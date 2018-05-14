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
using System.Text.RegularExpressions;
using Common.Action;
using CommonGlobalSettings;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class WorkTimeModel : BaseModel
    {
        private string guid = null;
        private string workTimeName = null;
        private string beginTime = null;
        private string endTime = null;
        private bool[] periodMark = null;

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

        public string WorkTimeName 
        {
            get
            {
                return workTimeName;
            }
            set
            {
                workTimeName = value;
            }
        }

        public string BeginTime 
        {
            get
            {
                return beginTime;
            }
            set
            {
                beginTime = value;
            }
        }

        public string EndTime 
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }

        public bool[] PeriodMark 
        {
            get
            {
                return periodMark;
            }
            set
            {
                periodMark = value;
            }
        }

        public override ActionMessage Validator()
        {
            if(workTimeName.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Work Time Name can not be empty.";
                //actionMessage.Message = languageManager.GetString(StringConsts.WorkTimeNameEmpty, (int)ModuleEnum.Oam_Client, StringConsts.WorkTimeNameEmptyDefaultValue);
                return actionMessage;
            }

            if (!IsValidString(workTimeName))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Work Time Name is not valid.";
                return actionMessage;
            }

            if(workTimeName.Length > 32)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Work Time Name is too long.";
                //actionMessage.Message = languageManager.GetString(StringConsts.WorkTimeNameTooLong, (int)ModuleEnum.Oam_Client, StringConsts.WorkTimeNameTooLongDefaultValue);
                return actionMessage;
            }

            if (beginTime.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The begin time can not be empty.";
                //actionMessage.Message = languageManager.GetString(StringConsts.BeginTimeEmpty, (int)ModuleEnum.Oam_Client, StringConsts.BeginTimeEmptyDefaultValue);
                return actionMessage;
            }

            if (endTime.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The end time can not be empty.";
                //actionMessage.Message = languageManager.GetString(StringConsts.EndTimeEmpty, (int)ModuleEnum.Oam_Client, StringConsts.EndTimeEmptyDefaultValue);
                return actionMessage;
            }

            DateTime beginDateTime = Convert.ToDateTime(beginTime);
            DateTime endDateTime = Convert.ToDateTime(endTime);
            if (beginDateTime.CompareTo(endDateTime) >= 0)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The End Time must be great than the Begin Time.";
                //actionMessage.Message = languageManager.GetString(StringConsts.EndTimeLessBeginTime, (int)ModuleEnum.Oam_Client, StringConsts.EndTimeLessBeginTimeDefaultValue);
                return actionMessage;
            }

            return null;
        }

        private bool IsValidString(string strName)
        {
            if (Regex.IsMatch(strName, "[,|'\"]"))
            {
                return false;
            }

            return true;
        }
    }
}
