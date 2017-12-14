using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System.Linq
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> lambda, bool condition = false) where T : class
        {
            if (lambda == null || condition)
            {
                return source;
            }
            return source.Where(lambda);
        }
        public static IQueryable<T> Paging<T>(this IQueryable<T> source, int pageIndex, int pageSize, out int total) where T : class
        {
            total = source.Count();
            return source.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize);
        }
        public static IQueryable<T> Paging<T>(this IQueryable<T> source, int pageIndex, int pageSize) where T : class
        {
            return source.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize);
        }
    }
}
