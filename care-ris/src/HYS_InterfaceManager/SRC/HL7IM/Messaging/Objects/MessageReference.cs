using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Objects
{
    public class MessageReference : XObject
    {
        //private XCollection<ReferenceFile> _fileList = new XCollection<ReferenceFile>("File");
        private ReferenceFileCollection _fileList = new ReferenceFileCollection();
        public ReferenceFileCollection FileList
        {
            get { return _fileList; }
            set { _fileList = value; }
        }
    }
}
