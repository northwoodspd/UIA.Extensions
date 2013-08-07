using System;
using System.Collections.Generic;
using System.Linq;

namespace UIA.Fluent.Extensions
{
    public static class EnumerableExtensions
    {
        public static T Before<T>(this IList<T> items, T what)
        {
            var indexOfWhat = items.IndexOf(what);
            return items.ElementAtOrDefault(indexOfWhat - 1);
        }

        public static T After<T>(this IList<T> items, T what)
        {
            var indexOfWhat = items.IndexOf(what);
            return items.ElementAtOrDefault(indexOfWhat + 1);
        }

        public static int IndexOf<T>(this IEnumerable<T> items, Func<T, bool> trueCondition)
        {
            var foundIndex = 0;
            foreach (var item in items)
            {
                if (trueCondition(item)) return foundIndex;
                ++foundIndex;
            }

            return -1;
        }
    }
}