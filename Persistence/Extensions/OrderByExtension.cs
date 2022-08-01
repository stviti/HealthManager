using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Persistence.Extensions
{
    public static class OrderByExtension
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string propertyName, bool isDescending = false)
        {
            if (String.IsNullOrEmpty(propertyName))
                return source;

            var type = typeof(TEntity);

            var property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                return source;

            ParameterExpression parameter = Expression.Parameter(type, "p");

            MemberExpression member = Expression.Property(parameter, property);

            LambdaExpression lambda = Expression.Lambda(member, parameter);

            string methodName = isDescending ? "OrderByDescending" : "OrderBy";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                      new Type[] { type, member.Type },
                      source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<TEntity>(methodCallExpression);
        }


    }
}
