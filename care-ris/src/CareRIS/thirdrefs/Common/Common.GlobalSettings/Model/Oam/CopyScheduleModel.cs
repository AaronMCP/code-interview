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
using System.Data;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class CopyScheduleModel : OamBaseModel
    {
        private string type = null;//Employee or Modality or Template
        private DateTime beginTime;
        private DateTime endTime;
        private string[] employees = null;
        private string modality = null;
        private DataTable schedules = null;

        public string ScheduleType 
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

        public DataTable Schedules 
        {
            get
            {
                return schedules;
            }
            set
            {
                schedules = value;
            }
        }
    }
}
