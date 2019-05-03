using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZigmaWeb.Common.Data;

namespace ZigmaWeb.Common.ExtensionMethod
{
    public static class IQueryableExtension
    {
        public static DataSourceResult ToDataSourceResult<TSource, TResult>(this IQueryable<TSource> queryable, DataSourceRequest request, Func<TSource, TResult> selector)
        {
            var recordsTotal = queryable.Count();
            var data = queryable
                .ToList()
                .Select(selector);

            return new DataSourceResult() {
                recordsTotal = recordsTotal,
                data = data
            };
        }


        public static DataSourceResult ToDataSourceResult<TSource>(this IQueryable<TSource> queryable, DataSourceRequest request)
        {
            var recordsTotal = queryable.Count();
            var data = queryable.ToList();

            return new DataSourceResult() {
                recordsTotal = recordsTotal,
                data = data
            };
        }
    }
}
