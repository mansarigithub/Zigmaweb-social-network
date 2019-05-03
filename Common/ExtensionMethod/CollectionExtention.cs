using System;
using System.Collections.Generic;

namespace ZigmaWeb.Common.ExtensionMethod
{
    public static class CollectionExtention
    {
        public static void AddRange<T>(this ICollection<T> collectionToExtend, IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                collectionToExtend.Add(item);
            }
        }
    }
}
