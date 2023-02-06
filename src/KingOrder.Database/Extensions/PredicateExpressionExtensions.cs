using Castle.Components.DictionaryAdapter.Xml;
using System.Linq.Expressions;
using System.Reflection;

namespace KingOrder.Database.Extensions
{
    public static class PredicateExpressionExtensions
    {
        #region constants

        private const string ExpressionContainsOfMethod = "Contains";
        private const string ExpressionParameterType = "type";
        private const string ExpressionParameterX = "x";

        #endregion

        #region public methods implementations

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {

            ParameterExpression p = a.Parameters[0];

            SubstExpressionVisitor visitor = new SubstExpressionVisitor();
            visitor.Subst[b.Parameters[0]] = p;

            Expression body = Expression.AndAlso(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {

            ParameterExpression p = a.Parameters[0];

            SubstExpressionVisitor visitor = new SubstExpressionVisitor();
            visitor.Subst[b.Parameters[0]] = p;

            Expression body = Expression.OrElse(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        public static Expression<Func<TEntity, bool>> BuildPredicateExpressionByEntityAndFilter<TEntity, TFilter>(TFilter filters) where TEntity : class
        {
            var entityProperties = typeof(TEntity).GetProperties();
            var filtersProperties = typeof(TFilter).GetProperties();
            var propertiesToGetValues = new List<PropertyInfo>();

            var intersectedProperties = entityProperties.Select(c => c.Name).Intersect(filtersProperties.Select(c => c.Name));
            propertiesToGetValues = filtersProperties.Where(c => intersectedProperties.Contains(c.Name)).ToList();

            Expression<Func<TEntity, bool>> predicateResult = null;

            foreach (var propToGetValue in propertiesToGetValues)
            {
                Expression<Func<TEntity, bool>> predicate = null;

                var fieldName = propToGetValue.Name;
                var fieldValue = propToGetValue.GetValue(filters);

                if (propToGetValue.PropertyType == typeof(string) && string.IsNullOrEmpty((string)fieldValue))
                    continue;

                var prop = entityProperties.FirstOrDefault(c => c.Name.Equals(propToGetValue.Name));

                if (prop == null)
                    continue;

                //var isNullable = fieldValue == null ? true : false;
                var isNullable = IsNullable(prop.PropertyType);
                var parameter = Expression.Parameter(typeof(TEntity), ExpressionParameterX);
                var member = Expression.Property(parameter, fieldName);

                if (isNullable && fieldValue != null)
                {
                    var filter1 = Expression.Constant(Convert.ChangeType(fieldValue, member.Type.GetGenericArguments()[0]));
                    Expression typeFilter = Expression.Convert(filter1, member.Type);
                    var body = Expression.Equal(member, typeFilter);
                    predicate = Expression.Lambda<Func<TEntity, bool>>(body, parameter);
                }
                else
                {
                    if (fieldValue == null)
                        continue;

                    if (prop.PropertyType == typeof(Guid))
                    {
                        var guidValue = (Guid)fieldValue;

                        if (guidValue == Guid.Empty || guidValue == default(Guid))
                            continue;
                    }

                    if (prop.PropertyType == typeof(string))
                    {
                        var parameterExp = Expression.Parameter(typeof(TEntity), ExpressionParameterType);
                        var propertyExp = Expression.Property(parameterExp, prop.Name);
                        MethodInfo method = typeof(string).GetMethod(ExpressionContainsOfMethod, new[] { typeof(string) });
                        var someValue = Expression.Constant((string)fieldValue, typeof(string));
                        var containsMethodExp = Expression.Call(propertyExp, method, someValue);

                        predicate = Expression.Lambda<Func<TEntity, bool>>(containsMethodExp, parameterExp);
                    }
                    else
                    {
                        var constant = Expression.Constant(Convert.ChangeType(fieldValue, prop.PropertyType));
                        var body = Expression.Equal(member, constant);
                        predicate = Expression.Lambda<Func<TEntity, bool>>(body, parameter);
                    }
                }

                if (predicateResult == null)
                    predicateResult = predicate;
                else
                    predicateResult = predicateResult.And(predicate);
            }

            return predicateResult;
        }

        static bool IsNullable<T>(T obj)
        {
            if (obj == null) return true; // obvious
            Type type = typeof(T);
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }

        static bool IsNullable<T>()
        {
            Type type = typeof(T);
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }

        #endregion
    }

    internal class SubstExpressionVisitor : ExpressionVisitor
    {
        #region properties

        public Dictionary<Expression, Expression> Subst = new Dictionary<Expression, Expression>();

        #endregion

        #region protected methods implementations

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression newValue;

            if (Subst.TryGetValue(node, out newValue))
                return newValue;

            return node;
        }

        #endregion
    }
}
