using System;
using System.Collections;
using System.Windows.Forms;

namespace HYS.HL7IM.Manager.Controler
{
	/// <summary>
	/// ListViewItemComparer 的摘要说明。
	/// </summary>
	public class ListViewItemComparer : IComparer 
	{
		private int col = 0;
		private SortOrder _order = SortOrder.Ascending;

		public ListViewItemComparer( int column, SortOrder order ) 
		{
			col = column;
			if( order != SortOrder.None ) _order = order;
		}
 
		public int Compare(object x, object y) 
		{
			//string strX = ((ListViewItem)x).SubItems[col].Text;
			//string strY = ((ListViewItem)y).SubItems[col].Text;

			string strX = "";
			string strY = "";

			ListViewItem iX = (ListViewItem)x;
			ListViewItem iY = (ListViewItem)y;

			if( col >=0 && col < iX.SubItems.Count ) strX = iX.SubItems[col].Text;
			if( col >=0 && col < iY.SubItems.Count ) strY = iY.SubItems[col].Text;

			if( _order == SortOrder.Ascending )
				return string.Compare(strX, strY);
			else
				return string.Compare(strY, strX);
		}
	}
}
