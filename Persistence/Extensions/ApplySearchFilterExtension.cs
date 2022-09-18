using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Application.Models;

namespace Persistence.Extensions
{
    public static class ApplySearchFiltersExtensions
    {
        public static IQueryable<TEntity> ApplySearchFilter<TEntity>(this IQueryable<TEntity> source, SearchFilter filter)
            where TEntity : class
        {
            if (filter == null)
                return source;

            var lamda = GetLamdaExpression<TEntity>(filter.Field, filter.Operator, filter.FieldValue);
            if (lamda == null)
                return source;

            return source.Where(lamda);
        }

        private static Expression<Func<T, bool>> GetLamdaExpression<T>(string propertyName, string methodName, string propertyValue)
        {
            if (propertyName == null || methodName == null)
                return null;

            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            var property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                return null;

            MemberExpression member = Expression.Property(parameter, property);
            var memberType = member.Type;

            ConstantExpression constant = Expression.Constant(propertyValue);

            if (memberType == typeof(Int32))
                constant = Expression.Constant(Int32.Parse(propertyValue));
            else if (memberType == typeof(DateTime))
                constant = Expression.Constant(DateTime.Parse(propertyValue));
            else if (memberType == typeof(Boolean))
                constant = Expression.Constant(propertyValue.Equals("true", StringComparison.CurrentCultureIgnoreCase));

            Expression expression;
            if (Enum.TryParse(methodName, true, out ExpressionType expressionType))
            {
                expression = Expression.MakeBinary(expressionType, member, constant);
            }
            else
            {
                MethodInfo method = memberType.GetMethods().First(m => m.Name == methodName && m.GetParameters().Length == 1);
                if (method == null)
                    return null;
                expression = Expression.Call(member, method, constant);
            };

            if (expression == null)
                return null;

            var lamda = Expression.Lambda<Func<T, bool>>(expression, parameter);

            return lamda;
        }
    }
}