using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.BusinessEntity
{
    public class GCDeviceCollection : CollectionBase
    {
        public GCDevice Add(GCDevice value)
        {
            List.Add(value as object);
            return value;
        }

        public void Remove(GCDevice value)
        {
            List.Remove(value as object);
        }

        public void Insert(int index, GCDevice value)
        {
            List.Insert(index, value as object);
        }

        public bool Contains(GCDevice value)
        {
            return List.Contains(value as object);
        }

        public GCDevice this[int index]
        {
            get { return (List[index] as GCDevice); }
        }

        public int IndexOf(GCDevice value)
        {
            return List.IndexOf(value);
        }

        public GCDeviceCollection Copy()
        {
            GCDeviceCollection clone = new GCDeviceCollection();

            foreach (GCDevice c in List)
                clone.Add(c);

            return clone;
        }

    }
}
