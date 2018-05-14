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

namespace CommonGlobalSettings
{
    [Serializable()]
    public class EmployeeScheduleModel : OamBaseModel
    {
        private string scheduleType = null;
        private int period = 1;
        private DateTime beginTime;
        private DateTime endTime;
        private string[] employees = null;
        private WorkTimeModel[] workTimeModels = null;
        private bool isTemplate = false;
        private string templateName = "";

        private bool isScheduled = false;//indicates that at least one employee has been scheduled

        public bool IsScheduled 
        {
            get
            {
                return isScheduled;
            }
            set
            {
                isScheduled = value;
            }
        }

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

        public string[] Employees 
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
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

        public bool IsTemplate 
        {
            get
            {
                return isTemplate;
            }
            set
            {
                isTemplate = value;
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

        public override ActionMessage Validator()
        {
            if (scheduleType.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Schedule Type can not be empty.";
                return actionMessage;
            }

            if(beginTime == null)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The Begin Time can not be empty.";
                return actionMessage;
            }

            if(period > 1)
            {
                if(endTime == null)
                {
                    ActionMessage actionMessage = new ActionMessage();
                    actionMessage.Message = "The End Time can not be empty.";
                    return actionMessage;
                }

                if(beginTime > endTime)
                {
                    ActionMessage actionMessage = new ActionMessage();
                    actionMessage.Message = "The End Time must be great than the Begin Time.";
                    return actionMessage;
                }
            }

            if(employees.Length == 0)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "Please Select at least more than one employee.";
                return actionMessage;
            }

            if(workTimeModels.Length == 0)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "Please input the schedules.";
                return actionMessage;
            }

            if(IsWorkTimeOverlap())
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "The work time has overlapped.";
                return actionMessage;
            }

            if(isTemplate && templateName.Trim().Equals(""))
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "Please input the template name.";
                return actionMessage;
            }

            return null;
        }

        private bool IsWorkTimeOverlap()
        {
            for(int i = 0; i < workTimeModels.Length; i++)
            {
                WorkTimeModel model = workTimeModels[i];
                DateTime beginTime = Convert.ToDateTime(model.BeginTime);
                DateTime endTime = Convert.ToDateTime(model.EndTime);

                for(int j = i + 1; j < workTimeModels.Length; j++)
                {
                    WorkTimeModel modelCompare = workTimeModels[j];
                    DateTime beginTimeCompare = Convert.ToDateTime(modelCompare.BeginTime);
                    DateTime endTimeCompare = Convert.ToDateTime(modelCompare.EndTime);
                    if (endTime <= beginTimeCompare || beginTime >= endTimeCompare)
                    {
                        continue;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
