using System;
using System.Collections.Generic;

namespace ZigmaWeb.Common.ExtensionMethod
{
    public static class EnumerableExtention
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }
    }
}
