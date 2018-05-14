using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Base.Config
{
    public class SolutionWebSetting : XObject
    {
        private string _virtualPathName = "";
        [XCData(true)]
        public string VirtualPathName
        {
            get { return _virtualPathName; }
            set { _virtualPathName = value; }
        }

        private string _userName = "service";
        [XCData(true)]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private string _password = "service";
        [XCData(true)]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private bool _doNotNeedToLogin;
        public bool DoNotNeedToLogin
        {
            get { return _doNotNeedToLogin; }
            set { _doNotNeedToLogin = value; }
        }

        //private CustomizedWebPage _homePage = new CustomizedWebPage();
        //public CustomizedWebPage HomePage
        //{
        //    get { return _homePage; }
        //    set { _homePage = value; }
        //}

        //private CustomizedWebPageCatalog<CustomizedWebPage> _diagrams = new CustomizedWebPageCatalog<CustomizedWebPage>();
        //public CustomizedWebPageCatalog<CustomizedWebPage> Diagrams
        //{
        //    get { return _diagrams; }
        //    set { _diagrams = value; }
        //}

        //private CustomizedWebPageCatalog<EntityWebPage> _entities = new CustomizedWebPageCatalog<EntityWebPage>();
        //public CustomizedWebPageCatalog<EntityWebPage> Entities
        //{
        //    get { return _entities; }
        //    set { _entities = value; }
        //}

        //private CustomizedWebPageCatalog<CustomizedWebPage> _wizards = new CustomizedWebPageCatalog<CustomizedWebPage>();
        //public CustomizedWebPageCatalog<CustomizedWebPage> Wizards
        //{
        //    get { return _wizards; }
        //    set { _wizards = value; }
        //}

        //public EntityWebPage FindEntityByID(Guid id)
        //{
        //    foreach (EntityWebPage e in Entities.Pages)
        //    {
        //        if (e.EntityID == id) return e;
        //    }

        //    return null;
        //}

        //public EntityWebPage FindEntityByName(string name)
        //{
        //    if (name == null) return null;

        //    foreach (EntityWebPage e in Entities.Pages)
        //    {
        //        if (e.DisplayCaption == name) return e;
        //    }

        //    return null;
        //}
    }
}
