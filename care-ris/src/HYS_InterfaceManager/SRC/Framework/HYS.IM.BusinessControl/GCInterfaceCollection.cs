using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.BusinessControl
{
    public class GCInterfaceCollection : CollectionBase
    {
        public GCInterface Add(GCInterface value)
        {
            List.Add(value as object);
            return value;
        }

        public void Remove(GCInterface value)
        {
            List.Remove(value as object);
        }

        public void Insert(int index, GCInterface value)
        {
            List.Insert(index, value as object);
        }

        public bool Contains(GCInterface value)
        {
            return List.Contains(value as object);
        }

        public GCInterface this[int index]
        {
            get { return (List[index] as GCInterface); }
        }

        public int IndexOf(GCInterface value)
        {
            return List.IndexOf(value);
        }

        public GCInterfaceCollection Copy()
        {
            GCInterfaceCollection clone = new GCInterfaceCollection();

            foreach (GCInterface c in List)
                clone.Add(c);

            return clone;
        }
    }
}
