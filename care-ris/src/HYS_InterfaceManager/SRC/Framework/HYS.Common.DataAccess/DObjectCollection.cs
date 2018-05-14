using System;
using System.Collections;

namespace HYS.Common.DataAccess
{
	/// <summary>
	/// DObjectCollection 的摘要说明。
	/// </summary>
	public class DObjectCollection : CollectionBase
	{
		public DObjectCollection()
		{
		}

		public DObject Add(DObject value)
		{
			base.List.Add(value as object);
			return value;
		}

		public void AddRange(DObject[] values)
		{
			foreach(DObject page in values) Add(page);
		}

		public void Remove(DObject value)
		{
			base.List.Remove(value as object);
		}

		public void Insert(int index, DObject value)
		{
			base.List.Insert(index, value as object);
		}

		public bool Contains(DObject value)
		{
			return base.List.Contains(value as object);
		}

		public DObject this[int index]
		{
			get { return (base.List[index] as DObject); }
		}

		public int IndexOf(DObject value)
		{
			return base.List.IndexOf(value);
		}

		public DObjectCollection Copy()
		{
			DObjectCollection clone = new DObjectCollection();
			foreach(DObject c in base.List) clone.Add(c);
			return clone;
		}
	}
}
