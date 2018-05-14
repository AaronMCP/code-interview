using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CrossCutting.Common.Utils
{
    public static class SearchingUtil
    {
        public static SearchingType ProcessSearchValue(string searchValue, out string actualSearchValue)
        {
            SearchingType searchingType = SearchingType.Exact;
            bool isStartWithStar = searchValue.StartsWith("*");
            bool isEndWithStar = searchValue.EndsWith("*");
            bool containsVaildValue = searchValue.Any(c => !c.Equals('*') && !c.Equals(' ')); // make sure the value contains character other than '*' only
            if ((isStartWithStar || isEndWithStar) && containsVaildValue)
            {
                if (isStartWithStar && isEndWithStar)
                {
                    actualSearchValue = searchValue.Substring(1, searchValue.Length - 2);
                    return SearchingType.BothSideFuzzyMatching;
                }
                else if (isStartWithStar)
                {
                    actualSearchValue = searchValue.Substring(1);
                    return SearchingType.LeftFuzzyMatching;
                }
                else if (isEndWithStar)
                {
                    actualSearchValue = searchValue.Substring(0, searchValue.Length - 1);
                    return SearchingType.RightFuzzyMatching;
                }
            }
            actualSearchValue = searchValue;
            return searchingType;
        }
    }

    public enum SearchingType
    {
        Exact = 0,
        LeftFuzzyMatching,
        RightFuzzyMatching,
        BothSideFuzzyMatching
    }
}
