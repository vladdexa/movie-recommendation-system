using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class Utils
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }

        public static bool ListContains<T>(this IEnumerable<T> firstList, IEnumerable<T> secondList)
        {
            return secondList.Any(firstList.Contains);
        }
    }
}