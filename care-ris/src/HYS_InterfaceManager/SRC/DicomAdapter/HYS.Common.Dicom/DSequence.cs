using HYS.Common.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.Common.Dicom
{
    public class DSequence : XObjectCollection
    {
        private DElement _rootElement;

        [Obsolete("Please do not use this constructor.", false)]
        public DSequence()
            : base(typeof(DElementList))
        {
        }

        public DSequence(DElement rootElement)
            : base(typeof(DElementList))
        {
            _rootElement = rootElement;
        }

        [XNode(false)]
        public new int Count
        {
            get
            {
                return base.Count;
            }
        }

        [XNode(false)]
        public new DElementList this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) return null;
                return base[index] as DElementList;
            }
        }

        public override XBase Add(XBase value)
        {
            return Add(value as DElementList) as XBase;
        }

        internal DElementList _add(DElementList value)
        {
            return base.Add(value) as DElementList;
        }

        public bool Contains(DElementList value)
        {
            return base.Contains(value);
        }
        [Obsolete("This method is not supported.", true)]
        public new DSequence Copy()
        {
            return null;
        }
        public new IEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }
        public int IndexOf(DElementList value)
        {
            return base.IndexOf(value);
        }
        [Obsolete("This method is not supported.", true)]
        public void Insert(int index, DElementList value)
        {
        }
        public void Remove(DElementList value)
        {
            //int index = IndexOf(value);
            //if (_rootElement.IsRef)
            //{
            //    if (index < 0 || index >= _rootElement._element.value_count) return;
            //    _rootElement._element.remove_value(index);
            //    base.Remove(value);
            //}
            //else
            //{
            //    if (index < 0 || index >= _rootElement._element.VM) return;
            //    _rootElement._element.remove_value(index);
            //    base.Remove(value);
            //}
        }

        public DElementList[] ToArray()
        {
            List<DElementList> list = new List<DElementList>();
            for (int i = 0; i < Count; i++) list.Add(this[i]);
            return list.ToArray();
        }
    }
}
