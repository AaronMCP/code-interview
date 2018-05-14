using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonGlobalSettings.BillBoard
{
    [Serializable]
    public class UserInfo
    {
        public string UserId
        {
            get;
            set;
        }

        public string Department
        {
            get;
            set;
        }

        public string UserRole
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }
        public string Site
        {
            get;
            set;
        }
    }

    public class ClientInfo
    {
        public ClientInfo(UserInfo userInfo, BroadcastEventHandler handler)
        {
            this.ClientUserInfo = userInfo;
            this.ClientBrdCastHandler = handler;
        }

        public UserInfo ClientUserInfo
        {
            get;
            set;
        }

        public BroadcastEventHandler ClientBrdCastHandler
        {
            get;
            set;
        }
    }

    [Serializable]
    public class BroadcastEventArgs : EventArgs
    {
        public GCRISNote Note
        {
            get;
            set;
        }

        public BroadcastEventArgs(GCRISNote note)
        {
            this.Note = note;
        }

        public BroadcastEventArgs()
        { }
    }

    [Serializable]
    public class GCRISNote
    {
        private string guid;
        private string title;
        private string body;
        private string groupId;
        private int type;
        private string beginDateTime;
        private string endDateTime;
        private long interval;
        private long showTime;
        private string attachmentURL;
        private string createDate;
        private string creator;
        private string submitter;
        private string submitDate;
        private string submitTo;
        private string approver;
        private string approveDate;
        private string rejector;
        private string rejectDate;
        private string rejectCause;
        private int state;
        private string operationHistory;

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
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public string Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value;
            }
        }
        public string GroupId
        {
            get
            {
                return groupId;
            }
            set
            {
                groupId = value;
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
        public string BeginDateTime
        {
            get
            {
                return beginDateTime;
            }
            set
            {
                beginDateTime = value;
            }
        }
        public string EndDateTime
        {
            get
            {
                return endDateTime;
            }
            set
            {
                endDateTime = value;
            }
        }
        public long Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
            }
        }
        public long ShowTime
        {
            get
            {
                return showTime;
            }
            set
            {
                showTime = value;
            }
        }
        public string AttachmentURL
        {
            get
            {
                return attachmentURL;
            }
            set
            {
                attachmentURL = value;
            }
        }
        public string CreateDate
        {
            get
            {
                return createDate;
            }
            set
            {
                createDate = value;
            }
        }
        public string Creator
        {
            get
            {
                return creator;
            }
            set
            {
                creator = value;
            }
        }
        public string Submitter
        {
            get
            {
                return submitter;
            }
            set
            {
                submitter = value;
            }
        }
        public string SubmitDate
        {
            get
            {
                return submitDate;
            }
            set
            {
                submitDate = value;
            }
        }
        public string SubmitTo
        {
            get
            {
                return submitTo;
            }
            set
            {
                submitTo = value;
            }
        }
        public string Approver
        {
            get
            {
                return approver;
            }
            set
            {
                approver = value;
            }
        }
        public string ApproveDate
        {
            get
            {
                return approveDate;
            }
            set
            {
                approveDate = value;
            }
        }
        public string Rejector
        {
            get
            {
                return rejector;
            }
            set
            {
                rejector = value;
            }
        }
        public string RejectDate
        {
            get
            {
                return rejectDate;
            }
            set
            {
                rejectDate = value;
            }
        }
        public string RejectCause
        {
            get
            {
                return rejectCause;
            }
            set
            {
                rejectCause = value;
            }
        }
        public int State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        public string OperationHistory
        {
            get
            {
                return operationHistory;
            }
            set
            {
                operationHistory = value;
            }
        }

        public int GroupType
        {
            get;
            set;
        }

        public string Site
        {
            get;
            set;
        }

        public GCRISNote(string pGuid, string pTitle, string pBody, string pLevel, int pType, string pBeginDateTime, string pEndDateTime, long pInterval,
            long pShowTime, string pAttachmentURL, string pCreateDate, string pCreator, string pSubmitter, string pSubmitDate, string pSubmitTo, string pApprover,
            string pApproveDate, string pRejector, string pRejectDate, string pRejectCause, int pState, string pOperationHistory, string site)
        {
            guid = pGuid;
            title = pTitle;
            body = pBody;
            groupId = pLevel;
            type = pType;
            beginDateTime = pBeginDateTime;
            endDateTime = pEndDateTime;
            interval = pInterval;
            showTime = pShowTime;
            attachmentURL = pAttachmentURL;
            createDate = pCreateDate;
            creator = pCreator;
            submitter = pSubmitter;
            submitDate = pSubmitDate;
            submitTo = pSubmitTo;
            approver = pApprover;
            approveDate = pApproveDate;
            rejector = pRejector;
            rejectDate = pRejectDate;
            rejectCause = pRejectCause;
            state = pState;
            operationHistory = pOperationHistory;
            Site = site;
        }
        public GCRISNote()
        { }
    }

}
