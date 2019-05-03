using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZigmaWeb.Common.Data;

namespace ZigmaWeb.Common.ExtensionMethod
{
    public static class IQueryableExtension
    {
        public static IQueryable<TDestination> MapTo<TDestination>(this IQueryable queryable, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return queryable.ProjectTo<TDestination>(membersToExpand);
        }
    }
}
