using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Management.Config
{
    public class SolutionInfo : XObject
    {
        private Guid _solutionID;
        public Guid SolutionID
        {
            get { return _solutionID; }
            set { _solutionID = value; }
        }

        private string _name = "";
        [XCData(true)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description = "";
        [XCData(true)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _solutionVersion = "";
        [XCData(true)]
        public string SolutionVersion
        {
            get { return _solutionVersion; }
            set { _solutionVersion = value; }
        }

        private string _solutionUpdateDateTime = "";
        [XCData(true)]
        public string SolutionUpdateDateTime
        {
            get { return _solutionUpdateDateTime; }
            set { _solutionUpdateDateTime = value; }
        }

        private string _solutionPath = "";
        [XCData(true)]
        public string SolutionPath
        {
            get { return _solutionPath; }
            set { _solutionPath = value; }
        }

        private SolutionWebSetting _webSetting = new SolutionWebSetting();
        public SolutionWebSetting WebSetting
        {
            get { return _webSetting; }
            set { _webSetting = value; }
        }
    }
}
