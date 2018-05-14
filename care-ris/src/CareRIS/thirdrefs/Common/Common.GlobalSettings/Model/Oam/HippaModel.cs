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
using System.Collections;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class HippaModel : OamBaseModel, ICloneable
    {
        private Guid aLGuid;
        private ArrayList eventID = null;
        private string eventActionCode = null;
        private DateTime eventDt;
        private string eventOutComeIndicator = null;
        private ArrayList eventTypeCode = null;
        private Guid userGuid;
        private ArrayList userName = null;
        private string userIsRequestor = null;
        private string roleName = null;
        private string partObjectTypeCode = null;
        private string partObjectTypeCodeRole = null;
        private string partObjectIDTypeCode = null;
        private string partObjectID = null;
        private string partObjectName = null;
        private string partObjectDetail = null;
        private string comments = null;
        private string patientName = "";
        private string patientID = "";

        private int pageRowCnt = 0; //设置每页显示几行记录
        private int pageSizeIndex = 0; //设置当前显示第几页的数据(分页功能)
        private int allRowCnt = 0;    //设置总共有多少条记录
       
        //作为查询条件,根据相距天数还是时间跨度来查询
        private int duringDays = -1;
        private DateTime eventBeginDt;
        private DateTime eventEndDt;

        public Guid ALGuid
        {
            get { return this.aLGuid; }
            set { this.aLGuid = value; }
        }

        public ArrayList EventID
        {
            get { return this.eventID; }
            set { this.eventID = value; }
        }
        public string EventActionCode
        {
            get { return this.eventActionCode; }
            set { this.eventActionCode = value; }
        }

        public DateTime EventDT
        {
            get { return this.eventDt; }
            set { this.eventDt = value; }
        }

        public string EventOutComeIndicator
        {
            get { return this.eventOutComeIndicator; }
            set { this.eventOutComeIndicator = value; }
        }

        public ArrayList EventTypeCode
        {
            get { return this.eventTypeCode; }
            set { this.eventTypeCode = value; }
        }

        public Guid UserGuid
        {
            get { return this.userGuid; }
            set { this.userGuid = value; }
        }

        public ArrayList UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }

        public string UserIsRequestor
        {
            get { return this.userIsRequestor; }
            set { this.userIsRequestor = value; }
        }

        public string RoleName
        {
            get { return this.roleName; }
            set { this.roleName = value; }
        }

        public string PartObjectTypeCode
        {
            get { return this.partObjectTypeCode; }
            set { this.partObjectTypeCode = value; }
        }

        public string PartObjectTypeCodeRole
        {
            get { return this.partObjectTypeCodeRole; }
            set { this.partObjectTypeCodeRole = value; }
        }

        public string PartObjectIDTypeCode
        {
            get { return this.partObjectIDTypeCode; }
            set { this.partObjectIDTypeCode = value; }
        }

        public string PartObjectID
        {
            get { return this.partObjectID; }
            set { this.partObjectID = value; }
        }

        public string PartObjectName
        {
            get { return this.partObjectName; }
            set { this.partObjectName = value; }
        }

        public string PartObjectDetail
        {
            get { return this.partObjectDetail; }
            set { this.partObjectDetail = value; }
        }

        public string Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public int DuringDays
        {
            get { return this.duringDays; }
            set { this.duringDays = value; }
        }

        public DateTime EventBeginDt
        {
            get { return this.eventBeginDt; }
            set { this.eventBeginDt = value; }
        }

        public DateTime EventEndDt
        {
            get { return this.eventEndDt; }
            set { this.eventEndDt = value; }
        }

        public int PageRowCnt
        {
            get { return this.pageRowCnt; }
            set { this.pageRowCnt = value; }
        }

        public int PageSizeIndex
        {
            get { return this.pageSizeIndex;}
            set { this.pageSizeIndex = value; }
        }

        public int AllRowCnt
        {
            get { return this.allRowCnt; }
            set { this.allRowCnt = value; }
        }

        public string PatientID
        {
            get { return this.patientID; }
            set { this.patientID = value; }
        }

        public string PatientName
        {
            get { return this.patientName; }
            set { this.patientName = value; }
        }

        #region Added by Blue for [RC507] - US16220, 07/17/2014
        private ArrayList _eventCategory;

        public ArrayList EventCategory
        {
            get { return _eventCategory; }
            set { _eventCategory = value; }
        }
        private string _operationResult;

        public string OperationResult
        {
            get { return _operationResult; }
            set { _operationResult = value; }
        }
        private bool _isVIP;

        public bool IsVIP
        {
            get { return _isVIP; }
            set { _isVIP = value; }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

    }
}
