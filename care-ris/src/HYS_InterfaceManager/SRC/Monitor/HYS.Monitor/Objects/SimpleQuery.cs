using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.Adapter.Monitor.Objects
{
    public class SimpleQuery : XObject
    {
        private SimpleQueryItem _eventType = new SimpleQueryItem();
        public SimpleQueryItem EventType
        {
            get { return _eventType; }
            set { _eventType = value; }
        }

        private SimpleQueryItem _startTime = new SimpleQueryItem();
        public SimpleQueryItem StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        private SimpleQueryItem _endTime = new SimpleQueryItem();
        public SimpleQueryItem EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        private SimpleQueryItem _patientID = new SimpleQueryItem();
        public SimpleQueryItem PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }

        private SimpleQueryItem _patientName = new SimpleQueryItem();
        public SimpleQueryItem PatientName
        {
            get { return _patientName; }
            set { _patientName = value; }
        }

        private SimpleQueryItem _orderNo = new SimpleQueryItem();
        public SimpleQueryItem OrderNo
        {
            get { return _orderNo; }
            set { _orderNo = value; }
        }

        private SimpleQueryItem _accNo = new SimpleQueryItem();
        public SimpleQueryItem AccessionNo
        {
            get { return _accNo; }
            set { _accNo = value; }
        }

        private SimpleQueryItem _studyUID = new SimpleQueryItem();
        public SimpleQueryItem StudyInstanceUID
        {
            get { return _studyUID; }
            set { _studyUID = value; }
        }

        public SimpleQuery()
        {
        }

        public void ClearData() { 
            
        }
    }
}
