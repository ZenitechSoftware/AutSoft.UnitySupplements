using System;
using System.Collections.Generic;
using System.Linq;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.Extensions
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<TSource> Duplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            var grouped = source.GroupBy(selector);
            var moreThan1 = grouped.Where(i => i.IsMultiple());
            return moreThan1.SelectMany(i => i);
        }

        private static bool IsMultiple<T>(this IEnumerable<T> source)
        {
            // ReSharper disable once GenericEnumeratorNotDisposed
            var enumerator = source.GetEnumerator();
            return enumerator.MoveNext() && enumerator.MoveNext();
        }
    }
}
