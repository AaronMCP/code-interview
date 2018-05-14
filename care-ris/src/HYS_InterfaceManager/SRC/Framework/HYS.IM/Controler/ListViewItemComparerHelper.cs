using System;
using System.Collections;
using System.Windows.Forms;

namespace HYS.IM.Controler
{
	/// <summary>
	/// ListViewItemComparerHelper 的摘要说明。
	/// </summary>
	public class ListViewItemComparerHelper
	{
		private ListView listview;
		private int currentColumnIndex = -1;
		private Hashtable table = new Hashtable();
		private SortOrder GetSortOrder( int columnIndex )
		{
			SortOrder s = SortOrder.None;
			object o = table[columnIndex];

			if( o == null || columnIndex != currentColumnIndex )
			{
				table[columnIndex] = SortOrder.Ascending;
				s = SortOrder.Ascending;
			}
			else
			{
				if( (SortOrder) o == SortOrder.Ascending )
				{
					table[columnIndex] = SortOrder.Descending;
					s = SortOrder.Descending;
				}
				else
				{
					table[columnIndex] = SortOrder.Ascending;
					s = SortOrder.Ascending;
				}
			}

			currentColumnIndex = columnIndex;
			return s;
		}
		public ListViewItemComparerHelper( ListView list )
		{
			listview = list;
			if( listview != null ) listview.ColumnClick += new ColumnClickEventHandler(listview_ColumnClick);
		}

		private void listview_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			listview.ListViewItemSorter = new ListViewItemComparer( e.Column, GetSortOrder( e.Column ) );
		}
	}
}
