using System;
using System.Collections.Generic;
using System.Text;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;


namespace HYS.SocketAdapter.Common
{
    public class KKMath
    {
        static public bool OperationIsTrue( object Param1, QueryCriteriaOperator Operator, object Param2 )
        {
            bool result = false;

            switch (Operator)
	            {
		            case QueryCriteriaOperator.Equal:
                     result = Param1.ToString().Equals( Param2.ToString(), StringComparison.OrdinalIgnoreCase );
                     break;
                    case QueryCriteriaOperator.EqualLargerThan:
                        result = Param1.ToString().CompareTo(Param2.ToString()) >= 0;
                     break;
                    case QueryCriteriaOperator.EqualSmallerThan:
                        result = Param1.ToString().CompareTo(Param2.ToString()) <= 0;
                     break;
                    case QueryCriteriaOperator.LargerThan:
                        result = Param1.ToString().CompareTo(Param2.ToString()) > 0;
                     break;
                    case QueryCriteriaOperator.NotEqual:
                        result = Param1.ToString().CompareTo(Param2.ToString()) != 0;
                     break;
                    case QueryCriteriaOperator.SmallerThan:
                        result = Param1.ToString().CompareTo(Param2.ToString()) < 0;
                     break;
                    default:
                     break;
	            }
                return result;
        }

        public class LogicItem
        {
            public bool Value;
            public QueryCriteriaType Type;
            public LogicItem(bool v, QueryCriteriaType t)
            {
                Value = v;
                Type = t;
            }
        }

        static public bool JoinLogicItem(List<LogicItem> iList)
        {
            if (iList == null || iList.Count < 1) return true;

            bool value = iList[0].Value;
            if (iList.Count == 1) return value;
            for (int i = 1; i < iList.Count; i++)
            {
                LogicItem item = iList[i];
                if (item.Type == QueryCriteriaType.And)
                {
                    value = item.Value && value;
                }
                else if (item.Type == QueryCriteriaType.Or)
                {
                    value = item.Value || value;
                }
            }
            return value;
        }
    }
}
