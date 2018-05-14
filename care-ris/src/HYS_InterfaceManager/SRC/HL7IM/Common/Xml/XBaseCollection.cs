using System;
using System.Collections;

namespace HYS.HL7IM.Common.Xml
{
	/// <summary>
	/// 可映射到XML的对象基类集合
	/// </summary>
	public class XBaseCollection  : CollectionBase
	{
		public XBase Add(XBase value)
		{
			List.Add(value as object);
			return value;
		}

		public void Remove(XBase value)
		{
			List.Remove(value as object);
		}

		public void Insert(int index, XBase value)
		{
			List.Insert(index, value as object);
		}

		public bool Contains(XBase value)
		{
			return List.Contains(value as object);
		}

		public XBase this[int index]
		{
			get { return (List[index] as XBase); }
		}

		public int IndexOf(XBase value)
		{
			return List.IndexOf(value);
		}

		public XBaseCollection Copy()
		{
			XBaseCollection clone = new XBaseCollection();

			foreach(XBase c in List)
				clone.Add(c);

			return clone;
		}

        public IList GetList()
        {
            return List;
        }
	}
}
