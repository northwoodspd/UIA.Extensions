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
            return indexOfWhat == -1 ? default(T) : items.ElementAtOrDefault(indexOfWhat + 1);
        }

        public static void Times(this int howManytimes, Action<int> doWhat)
        {
            Enumerable.Range(0, howManytimes).ForEach(doWhat);
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> doIt)
        {
            foreach (var item in items)
            {
                doIt(item);
            }
        }
    }
}