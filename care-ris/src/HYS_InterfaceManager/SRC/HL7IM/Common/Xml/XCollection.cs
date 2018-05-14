using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace HYS.HL7IM.Common.Xml
{
    public class XCollection<T> : XObjectCollection
        where T : XObject
    {
        public XCollection()
            : base(typeof(T))
        {
        }

        // 20080708 : to support customized node name in collection

        /// <summary>
        /// Use this constructor only when XCollection is a member of another XObject, 
        /// and this member should be initialized in this XObject's default constructor,
        /// because customized node name can not be correctly parsed,
        /// when XObjectManager create the XCollection instance with its default constructor.
        /// </summary>
        /// <param name="nodeName"></param>
        public XCollection(string nodeName)
            : base(typeof(T))
        {
            _nodeName = nodeName;
        }

        public XCollection(T[] list)
            : base(typeof(T))
        {
            if (list != null) foreach (T t in list) Add(t);
        }

        private string _nodeName;
        internal override string XMLChildNodeName
        {
            get
            {
                if (_nodeName != null && _nodeName.Length > 0) return _nodeName;
                return base.XMLChildNodeName;
            }
        }

        //private List<T> list = new List<T>();

        public T Add(T value)
        {
            XObject x = value as XObject;
            return base.Add(x) as T;
        }

        public void Remove(T value)
        {
            XObject x = value as XObject;
            base.Remove(x);
        }

        public int IndexOf(T value)
        {
            XObject x = value as XObject;
            return base.IndexOf(x);
        }

        public void Insert(int index, T value)
        {
            XObject x = value as XObject;
            base.Insert(index, x);
        }

        public bool Contains(T value)
        {
            XObject x = value as XObject;
            return base.Contains(x);
        }

        public new XCollection<T> Copy()
        {
            XCollection<T> list = new XCollection<T>();
            foreach (T o in this)
            {
                list.Add(o);
            }
            return list;
        }

        [XNode(false)]
        public new T this[int index]
        {
            // Use base class to process actual collection operation
            get { return (base[index] as T); }
        }

        public new IEnumerable<T> GetList()
        {
            return base.GetList().Cast<T>();
        }

        public T[] ToArray()
        {
            return GetList().ToArray<T>();
        }
    }
}
