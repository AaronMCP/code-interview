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
using CommonGlobalSettings;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class ModalityScheduleModel : OamBaseModel
    {
        private string scheduleType = null;
        private int period = 1;
        private DateTime beginTime;
        private DateTime endTime;
        private string modality = null;
        private WorkTimeModel[] workTimeModels = null;
        private List<WorkTimeModel> overlabWorkTimeModels = new List<WorkTimeModel>();
        private string radiologistGuid = null;
        private string technicianGuid = null;
        private string nurseGuid = null;

        public string ScheduleType
        {
            get
            {
                return scheduleType;
            }
            set
            {
                scheduleType = value;
            }
        }

        public int Period
        {
            get
            {
                return period;
            }
            set
            {
                period = value;
            }
        }

        public DateTime BeginTime
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

        public DateTime EndTime
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

        public string Modality
        {
            get
            {
                return modality;
            }
            set
            {
                modality = value;
            }
        }

        public WorkTimeModel[] WorkTimeModels
        {
            get
            {
                return workTimeModels;
            }
            set
            {
                workTimeModels = value;
            }
        }

        public List< WorkTimeModel> OverLabWorkTimeModels
        {
            get
            {
                return overlabWorkTimeModels;
            }
        }
        public string RadiologistGuid 
        {
            get
            {
                return radiologistGuid;
            }
            set
            {
                radiologistGuid = value;
            }
        }

        public string TechnicianGuid 
        {
            get
            {
                return technicianGuid;
            }
            set
            {
                technicianGuid = value;
            }
        }

        public string NurseGuid 
        {
            get
            {
                return nurseGuid;
            }
            set
            {
                nurseGuid = value;
            }
        }

        public override ActionMessage Validator()
        {
            if (scheduleType.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Schedule Type can not be empty.";
                //actionMessage.Message = languageManager.GetString(StringConsts.ScheduleTypeEmpty, (int)ModuleEnum.Oam_Client, StringConsts.ScheduleTypeEmptyDefaultValue);
                return actionMessage;
            }

            if (beginTime == null)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Begin Time can not be empty.";
                //actionMessage.Message = languageManager.GetString(StringConsts.BeginTimeEmpty, (int)ModuleEnum.Oam_Client, StringConsts.BeginTimeEmptyDefaultValue);
                return actionMessage;
            }

            if (period > 1)
            {
                if (endTime == null)
                {
                    ActionMessage actionMessage = new ActionMessage();
                    actionMessage.Message = "The End Time can not be empty.";
                    //actionMessage.Message = languageManager.GetString(StringConsts.EndTimeEmpty, (int)ModuleEnum.Oam_Client, StringConsts.EndTimeEmptyDefaultValue);
                    return actionMessage;
                }

                if (beginTime > endTime)
                {
                    ActionMessage actionMessage = new ActionMessage();
                    actionMessage.Message = "The End Time must be great than the Begin Time.";
                    //actionMessage.Message = languageManager.GetString(StringConsts.EndTimeLessBeginTime, (int)ModuleEnum.Oam_Client, StringConsts.EndTimeLessBeginTimeDefaultValue);
                    return actionMessage;
                }
            }

            if (modality.Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "Please Select modality.";
                //actionMessage.Message = languageManager.GetString(StringConsts.SelectModalityPrompt, (int)ModuleEnum.Oam_Client, StringConsts.SelectModalityPromptDefaultValue);
                return actionMessage;
            }

            if (workTimeModels.Length == 0)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "Please input the schedules.";
                //actionMessage.Message = languageManager.GetString(StringConsts.InputSchedulesPrompt, (int)ModuleEnum.Oam_Client, StringConsts.InputSchedulesPromptDefaultValue);
                return actionMessage;
            }

            if (IsWorkTimeOverlap())
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The work time has overlapped.";
                //actionMessage.Message = languageManager.GetString(StringConsts.WorkTimeOverlap, (int)ModuleEnum.Oam_Client, StringConsts.WorkTimeOverlapDefaultValue);
                return actionMessage;
            }

            return null;
        }

        private bool IsWorkTimeOverlap()
        {
            overlabWorkTimeModels.Clear();
            for (int i = 0; i < workTimeModels.Length; i++)
            {
                WorkTimeModel model = workTimeModels[i];
                DateTime beginTime = Convert.ToDateTime(model.BeginTime);
                DateTime endTime = Convert.ToDateTime(model.EndTime);

                for (int j = i + 1; j < workTimeModels.Length; j++)
                {
                    WorkTimeModel modelCompare = workTimeModels[j];
                    DateTime beginTimeCompare = Convert.ToDateTime(modelCompare.BeginTime);
                    DateTime endTimeCompare = Convert.ToDateTime(modelCompare.EndTime);
                    if (endTime <= beginTimeCompare || beginTime >= endTimeCompare||model.WorkTimeName==modelCompare.WorkTimeName)
                    {
                        continue;
                    }
                    else
                    {
                        if(!overlabWorkTimeModels.Contains(workTimeModels[i]))
                            overlabWorkTimeModels.Add(workTimeModels[i]);
                        if(!overlabWorkTimeModels.Contains(workTimeModels[j]))
                            overlabWorkTimeModels.Add(workTimeModels[j]);
                    }
                }
            }
            if(overlabWorkTimeModels.Count == 0)
                return false;
            else
                return true;
        }
    }
}
