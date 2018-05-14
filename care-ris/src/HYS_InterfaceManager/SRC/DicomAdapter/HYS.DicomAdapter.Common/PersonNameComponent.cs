using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.DicomAdapter.Common
{
    public class PersonNameComponent : XObject
    {
        public PersonNameComponent()
        {
        }
        public PersonNameComponent(PersonNameComponentType type, string displayName)
        {
            _type = type;
            _displayName = displayName;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        private PersonNameComponentType _type = PersonNameComponentType.FamilyName;
        public PersonNameComponentType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _displayName = "";
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }
    }

    public enum PersonNameComponentType
    {
        FamilyName,
        GivenName,
        MiddleName,
        Prefix,
        Suffix,
    }
}
