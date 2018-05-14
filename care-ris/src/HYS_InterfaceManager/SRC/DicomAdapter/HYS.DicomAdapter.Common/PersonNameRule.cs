using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Dicom;

namespace HYS.DicomAdapter.Common
{
    public class PersonNameRule : XObject
    {
        private PersonNameGroupType _selectedGroup = PersonNameGroupType.None;
        public PersonNameGroupType SelectedGroup
        {
            get { return _selectedGroup; }
            set { _selectedGroup = value; }
        }

        private XCollection<PersonNameComponent> _components = new XCollection<PersonNameComponent>();
        public XCollection<PersonNameComponent> Components
        {
            get { return _components; }
            set { _components = value; }
        }

        public static PersonNameRule GetDefault()
        {
            PersonNameRule rule = new PersonNameRule();
            rule.SelectedGroup = PersonNameGroupType.SingleByte;
            rule.Components.Clear();
            rule.Components.Add(new PersonNameComponent(PersonNameComponentType.FamilyName, "Family Name"));
            rule.Components.Add(new PersonNameComponent(PersonNameComponentType.GivenName, "Given Name"));
            rule.Components.Add(new PersonNameComponent(PersonNameComponentType.MiddleName, "Middle Name"));
            rule.Components.Add(new PersonNameComponent(PersonNameComponentType.Prefix, "Prefix"));
            rule.Components.Add(new PersonNameComponent(PersonNameComponentType.Suffix, "Suffix"));
            return rule;
        }

        private class PersonNameGroup
        {
            public string OrginalName = "";
            public string FamilyName = "";
            public string GivenName = "";
            public string MiddleName = "";
            public string Prefix = "";
            public string Suffix = "";
        }
        private string GetName(PersonNameGroup group)
        {
            StringBuilder sb = new StringBuilder();
            foreach (PersonNameComponent c in Components)
            {
                switch (c.Type)
                {
                    case PersonNameComponentType.FamilyName :
                        sb.Append(group.FamilyName);
                        sb.Append(DPersonName.ComponentDelimiter);
                        break;
                    case PersonNameComponentType.GivenName :
                        sb.Append(group.GivenName);
                        sb.Append(DPersonName.ComponentDelimiter);
                        break;
                    case PersonNameComponentType.MiddleName:
                        sb.Append(group.MiddleName);
                        sb.Append(DPersonName.ComponentDelimiter);
                        break;
                    case PersonNameComponentType.Prefix:
                        sb.Append(group.Prefix);
                        sb.Append(DPersonName.ComponentDelimiter);
                        break;
                    case PersonNameComponentType.Suffix:
                        sb.Append(group.Suffix);
                        sb.Append(DPersonName.ComponentDelimiter);
                        break;
                }
            }
            string str = sb.ToString().Trim(DPersonName.ComponentDelimiter);
            return str = str.Replace(DPersonName.ComponentDelimiter, '*');
        }
        private PersonNameGroup GetGroup(string group)
        {
            PersonNameGroup sGroup = new PersonNameGroup();
            sGroup.OrginalName = group;

            string[] sList = sGroup.OrginalName.Split(DPersonName.ComponentDelimiter);
            if (sList.Length > 0) sGroup.FamilyName = sList[0];
            if (sList.Length > 1) sGroup.GivenName = sList[1];
            if (sList.Length > 2) sGroup.MiddleName = sList[2];
            if (sList.Length > 3) sGroup.Prefix = sList[3];
            if (sList.Length > 4) sGroup.Suffix = sList[4];

            return sGroup;
        }
        public string Parse(string personName)
        {
            if (SelectedGroup == PersonNameGroupType.None) return personName;
            if (personName == null) return "";

            string[] groupList = personName.Split(DPersonName.GroupDelimiter);
            switch (SelectedGroup)
            {
                case PersonNameGroupType.SingleByte :
                    if (groupList.Length > 0) return GetName(GetGroup(groupList[0]));
                    break;
                case PersonNameGroupType.Ideographic:
                    if (groupList.Length > 1) return GetName(GetGroup(groupList[1]));
                    break;
                case PersonNameGroupType.Phonetic:
                    if (groupList.Length > 2) return GetName(GetGroup(groupList[2]));
                    break;
            }

            return "";
        }
    }

    public enum PersonNameGroupType
    {
        None,
        SingleByte,
        Ideographic,
        Phonetic,
    }
}
