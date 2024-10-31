using DogAPI.DAL.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace DogAPI.DAL.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> OrderByAttribute<T>(this IQueryable<T> source, string attribute, string order)
        {
            PropertyInfo propInfo = typeof(T).GetProperty(attribute)
                ?? throw new Exception($"Type {attribute} does not exist on {typeof(T).Name} attribute");

            if (!typeof(IComparable).IsAssignableFrom(propInfo.PropertyType))
                throw new InvalidOperationException($"Property {attribute} on type {typeof(T).Name} incomparable");

            var param = Expression.Parameter(typeof(T), "d");

            var propertyAccess = Expression.Property(param, propInfo.Name);

            var castedProperty = Expression.Convert(propertyAccess, typeof(object));

            var orderByExpression = Expression.Lambda<Func<T, object>>(castedProperty, param);

            var result = order.ToLower() switch
            {
                "asc" => source.OrderBy(orderByExpression),
                "desc" => source.OrderByDescending(orderByExpression),
                _ => throw new Exception($"Unable to sort by current order: {order}")
            };

            return result;
        }
    }
}
