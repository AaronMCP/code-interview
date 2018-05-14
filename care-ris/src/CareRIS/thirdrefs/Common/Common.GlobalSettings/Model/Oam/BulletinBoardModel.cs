using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace CommonGlobalSettings
{
    [Serializable()]
    public class BulletinBoardModel : OamBaseModel
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

        private string title = null;
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

        private string groupId = "";
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

        private int groupType;
        public int GroupType
        {
            get { return groupType; }
            set { groupType = value; }
        }

        private int type = -1;
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

        private DateTime beginDate;
        public DateTime BeginDate
        {
            get
            {
                return beginDate;
            }
            set
            {
                beginDate = value;
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }

        private long interval = -1;
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

        private long showTime = -1;
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

        private string body = null;
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

        private string attachmentURL = null;
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

        private string creator = null;
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

        private DateTime createDate;
        public DateTime CreateDate
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

        private string submitter = null;
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


        private DateTime submitDate;
        public DateTime SubmitDate
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

        private string submitTo = null;
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


        private string approver = null;
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

        private DateTime approveDate;
        public DateTime ApproveDate
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

        private string rejector = null;
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

        private DateTime rejectDate;
        public DateTime RejectDate
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

        private string rejectCause = null;
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

        private int state = -1;
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

        private string operationHistory = null;
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

        private int actionType = 0;
        public int ActionType
        {
            get
            {
                return actionType;
            }
            set
            {
                actionType = value;
            }
        }

        private string levels = null;//for query use only
        public string Levels
        {
            get
            {
                return levels;
            }
            set
            {
                levels = value;
            }
        }

        private string types = null;//for query use only
        public string Types
        {
            get
            {
                return types;
            }
            set
            {
                types = value;
            }
        }

        private string submitters = null;//for query use only
        public string Submitters
        {
            get
            {
                return submitters;
            }
            set
            {
                submitters = value;
            }
        }

        private string approvers = null;//for query use only
        public string Approvers
        {
            get
            {
                return approvers;
            }
            set
            {
                approvers = value;
            }
        }

        private string publisher = null;
        public string Publisher
        {
            get
            {
                return publisher;
            }
            set
            {
                publisher = value;
            }
        }

        private DateTime publishDate;
        public DateTime PublishDate
        {
            get
            {
                return publishDate;
            }
            set
            {
                publishDate = value;
            }
        }

        private DateTime createDateFrom;//using for query
        public DateTime CreateDateFrom
        {
            get
            {
                return createDateFrom;
            }
            set
            {
                createDateFrom = value;
            }
        }
        private DateTime createDateTo;//using for query
        public DateTime CreateDateTo
        {
            get
            {
                return createDateTo;
            }
            set
            {
                createDateTo = value;
            }
        }

        private string states = "";//using for query, default is -1 = all bulletins
        public string States
        {
            get
            {
                return states;
            }
            set
            {
                states = value;
            }
        }

        private Byte searchByDate = 0X00;//default not by date(0001:by create date,0010:by publish date, 0011: by two)
        public Byte SearchByDate
        {
            get
            {
                return searchByDate;
            }
            set
            {
                searchByDate = value;
            }
        }

        private string publishers;
        public string Publishers
        {
            get
            {
                return publishers;
            }
            set
            {
                publishers = value;
            }
        }

        private DateTime publishDateFrom;//using for query
        public DateTime PublishDateFrom
        {
            get
            {
                return publishDateFrom;
            }
            set
            {
                publishDateFrom = value;
            }
        }
        private DateTime publishDateTo;//using for query
        public DateTime PublishDateTo
        {
            get
            {
                return publishDateTo;
            }
            set
            {
                publishDateTo = value;
            }
        }

        public string Site { get; set; }
    }
}
