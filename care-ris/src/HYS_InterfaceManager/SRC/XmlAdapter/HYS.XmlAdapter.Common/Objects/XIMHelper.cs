using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace HYS.XmlAdapter.Common.Objects
{
    public class XIMHelper
    {
        public static bool IsComplex(XIMType type)
        {
            return (type != XIMType.STR &&
                type != XIMType.INT &&
                type != XIMType.DEC &&
                type != XIMType.DAT &&
                type != XIMType.TIM &&
                type != XIMType.DTM &&
                type != XIMType.PNM &&
                type != XIMType.MSR &&
                type != XIMType.ADR &&
                type != XIMType.PHN &&
                type != XIMType.INS &&
                type != XIMType.PHY &&
                type != XIMType.ID);
        }

        private static void AppendRoot<T>(string root, T item)
            where T : IXmlElementItem
        {
            if (item == null) return;
            item.Element.XPath = root + XmlElement.XPathSeperator + item.Element.XPath;
        }
        private static void AppendRoot<T>(string root, List<T> list)
            where T : IXmlElementItem
        {
            if (list == null) return;
            foreach (T item in list)
            {
                AppendRoot(root, item);
            }
        }
        private static void AppendRoot<T>(IXmlElementItem root, T item)
            where T : IXmlElementItem
        {
            if (root == null) return;
            AppendRoot(root.Element.XPath, item);
        }
        private static void AppendRoot<T>(IXmlElementItem root, List<T> list)
            where T : IXmlElementItem
        {
            if (root == null) return;
            AppendRoot(root.Element.XPath, list);
        }

        private static List<T> GetItems<T>(Type t)
            where T : IXmlElementItem, new()
        {
            List<T> list = new List<T>();
            if (t != null)
            {
                FieldInfo[] fList = t.GetFields(BindingFlags.Public | BindingFlags.Static);
                if (fList != null)
                {
                    foreach (FieldInfo f in fList)
                    {
                        XmlElement ele = t.InvokeMember(f.Name, BindingFlags.Public | BindingFlags.Static | BindingFlags.GetField,
                            null, null, new object[] { }) as XmlElement;
                        if (ele != null)
                        {
                            //list.Add(new XmlElementItem(ele));
                            
                            T newItem = new T();
                            newItem.Element = ele.Clone();
                            list.Add(newItem);
                        }
                    }
                }
            }
            return list;
        }
        private static List<T> GetItems<T>(T root, Type t)
            where T : IXmlElementItem, new()
        {
            List<T> list = GetItems<T>(t);
            AppendRoot(root, list);
            return list;
        }

        public static List<T> GetRequestMessage<T>()
            where T : IXmlElementItem, new()
        {
            return GetItems<T>(typeof(XmlMessage.Request));
        }
        public static List<T> GetResponseMessage<T>()
            where T : IXmlElementItem, new()
        {
            return GetItems<T>(typeof(XmlMessage.Response));
        }
        public static List<T> GetSubItem<T>(T rootItem)
            where T : IXmlElementItem, new()
        {
            if (rootItem == null ) return null;

            switch (rootItem.Element.Type)
            {
                default:
                    return GetItems<T>(rootItem, rootItem.Element.ClassType);
                case XIMType.COD:
                    return GetItems<T>(rootItem, typeof(XIMType_COD));
                case XIMType.UID:
                    return GetItems<T>(rootItem, typeof(XIMType_UID));
                case XIMType.LOC :
                    return GetItems<T>(rootItem, typeof(XIMType_LOC));
                case XIMType.HD:
                    return GetItems<T>(rootItem, typeof(XIMType_HD));
                case XIMType.DR:
                    return GetItems<T>(rootItem, typeof(XIMType_DR));
            }
        }
    }
}
