using Ss.CmbXft.Common.Exceptions;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace Ss.CmbXft.Common.Models.Request;

/// <summary>
/// 查询构建器
/// </summary>
public static class QueryBuilder
{
    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="filters">筛选条件</param>
    /// <param name="allowedFields">允许筛选的字段白名单</param>
    /// <returns>查询表达式</returns>
    public static Expression<Func<TEntity, bool>>? BuildPredicate<TEntity>(List<FilterItem>? filters, params string[] allowedFields)
    {
        if (filters == null || filters.Count == 0)
            return null;
        //白名单
        HashSet<string>? allowedFieldSet = allowedFields.Length == 0 ? null : allowedFields.ToHashSet(StringComparer.OrdinalIgnoreCase);

        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");

        Expression? body = null;

        foreach (FilterItem filter in filters)
        {
            if (filter == null || string.IsNullOrWhiteSpace(filter.Field))
                throw new BusinessException(ApiResultEnum.ValidateError, "筛选条件项不能为空");

            if (allowedFieldSet != null && !allowedFieldSet.Contains(filter.Field))
                throw new BusinessException(ApiResultEnum.ValidateError, $"不允许筛选字段：{filter.Field}，允许筛选字段：{string.Join(",", allowedFields)}");

            PropertyInfo? property = GetProperty(typeof(TEntity), filter.Field);
            if (property == null)
                throw new BusinessException(ApiResultEnum.ValidateError, $"实体 {typeof(TEntity).Name} 不存在筛选字段：{filter.Field}");

            Expression? expression = BuildFilterExpression(parameter, property, filter);
            if (expression == null)
                throw new BusinessException(ApiResultEnum.ValidateError, $"字段 {filter.Field} 不支持筛选操作符 {filter.Operator} 或传入值非法");

            body = body == null ? expression : filter.IsOr
                    ? Expression.OrElse(body, expression)
                    : Expression.AndAlso(body, expression);
        }

        if (body == null)
            return null;

        return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
    }


    ///// <summary>
    ///// 构建排序字符串
    ///// </summary>
    //public static string BuildSorting(List<SortItem>? sorts, string defaultSorting, params string[] allowedFields)
    //{
    //    if (sorts == null || sorts.Count == 0)
    //        return defaultSorting;
    //    //白名单
    //    HashSet<string>? allowedFieldSet = allowedFields.Length == 0 ? null : allowedFields.ToHashSet(StringComparer.OrdinalIgnoreCase);

    //    List<string> validSorts = sorts
    //        .Where(s => !string.IsNullOrWhiteSpace(s.Field))
    //        .Where(s => allowedFieldSet == null || allowedFieldSet.Contains(s.Field))
    //        .Select(s => $"{s.Field} {(s.Desc ? "DESC" : "ASC")}")
    //        .ToList();

    //    if (validSorts.Count == 0)
    //        return defaultSorting;

    //    return string.Join(", ", validSorts);
    //}
    /// <summary>
    /// 构建安全排序字符串，非法字段直接抛出业务校验异常，杜绝静默失效与SQL注入
    /// </summary>
    /// <param name="sorts">前端排序集合</param>
    /// <param name="defaultSorting">无合法排序时默认排序</param>
    /// <param name="allowedFields">允许排序字段白名单</param>
    /// <returns>数据库可执行排序字符串</returns>
    public static string BuildSorting(List<SortItem>? sorts, string defaultSorting, params string[] allowedFields)
    {
        if (sorts is null or { Count: 0 }) return defaultSorting;

        HashSet<string>? allowedFieldSet = allowedFields.Length == 0 ? null : allowedFields.ToHashSet(StringComparer.OrdinalIgnoreCase);

        List<string> validSorts = new(sorts.Count);

        foreach (SortItem sort in sorts)
        {
            if (string.IsNullOrWhiteSpace(sort.Field)) throw new BusinessException(ApiResultEnum.ValidateError, "排序字段不能为空");

            string? canonical = null;
            if (allowedFieldSet is not null)
            {
                foreach (var field in allowedFieldSet)
                {
                    if (field.Equals(sort.Field, StringComparison.OrdinalIgnoreCase))
                    {
                        canonical = field;
                        break;
                    }
                }
                // 不在白名单
                if (canonical is null) throw new BusinessException(ApiResultEnum.ValidateError, $"不允许排序字段「{sort.Field}」，允许排序字段：{string.Join("、", allowedFields)}");
            }
            else
            {
                // 无白名单，强制校验字段合法字符，防SQL注入
                if (!IsValidFieldName(sort.Field)) throw new BusinessException(ApiResultEnum.ValidateError, $"排序字段「{sort.Field}」包含非法字符，仅允许字母、数字、下划线、小数点");
                canonical = sort.Field;
            }

            validSorts.Add($"{canonical} {(sort.Desc ? "DESC" : "ASC")}");
        }

        return validSorts.Count == 0 ? defaultSorting : string.Join(", ", validSorts);
    }

    /// <summary>
    /// 校验排序字段仅包含安全字符，防止SQL注入
    /// </summary>
    private static bool IsValidFieldName(string field)
    {
        // 仅允许 大小写字母、数字、下划线、点（支持导航属性如 User.Name）
        return System.Text.RegularExpressions.Regex.IsMatch(field, "^[a-zA-Z0-9_.]+$");
    }

    private static Expression? BuildFilterExpression(
        ParameterExpression parameter,
        PropertyInfo property,
        FilterItem filter)
    {
        MemberExpression propertyExpression = Expression.Property(parameter, property);

        return filter.Operator switch
        {
            FilterOperator.Equal =>
                BuildSingleValueExpression(propertyExpression, property, filter.Value, Expression.Equal),

            FilterOperator.NotEqual =>
                BuildSingleValueExpression(propertyExpression, property, filter.Value, Expression.NotEqual),

            FilterOperator.GreaterThan =>
                BuildSingleValueExpression(propertyExpression, property, filter.Value, Expression.GreaterThan),

            FilterOperator.GreaterThanOrEqual =>
                BuildSingleValueExpression(propertyExpression, property, filter.Value, Expression.GreaterThanOrEqual),

            FilterOperator.LessThan =>
                BuildSingleValueExpression(propertyExpression, property, filter.Value, Expression.LessThan),

            FilterOperator.LessThanOrEqual =>
                BuildSingleValueExpression(propertyExpression, property, filter.Value, Expression.LessThanOrEqual),

            FilterOperator.Contains =>
                BuildStringExpression(propertyExpression, property, filter.Value, nameof(string.Contains), false),

            FilterOperator.NotContains =>
                BuildStringExpression(propertyExpression, property, filter.Value, nameof(string.Contains), true),

            FilterOperator.StartsWith =>
                BuildStringExpression(propertyExpression, property, filter.Value, nameof(string.StartsWith), false),

            FilterOperator.EndsWith =>
                BuildStringExpression(propertyExpression, property, filter.Value, nameof(string.EndsWith), false),

            FilterOperator.In =>
                BuildInExpression(propertyExpression, property, filter.Values, false),

            FilterOperator.NotIn =>
                BuildInExpression(propertyExpression, property, filter.Values, true),

            FilterOperator.IsNull =>
                BuildNullExpression(propertyExpression, true),

            FilterOperator.IsNotNull =>
                BuildNullExpression(propertyExpression, false),

            FilterOperator.Between =>
                BuildBetweenExpression(propertyExpression, property, filter.MinValue, filter.MaxValue, false),

            FilterOperator.NotBetween =>
                BuildBetweenExpression(propertyExpression, property, filter.MinValue, filter.MaxValue, true),

            _ => null
        };
    }

    private static Expression? BuildSingleValueExpression(
        Expression propertyExpression,
        PropertyInfo property,
        string? value,
        Func<Expression, Expression, BinaryExpression> expressionBuilder)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        Type targetType = GetNonNullableType(property.PropertyType);

        object? convertedValue = ConvertValue(value, targetType);
        if (convertedValue == null)
            return null;

        Expression left = BuildNullableSafeLeftExpression(propertyExpression, property.PropertyType, out Expression? hasValueExpression);

        ConstantExpression constant = Expression.Constant(convertedValue, targetType);

        BinaryExpression comparison = expressionBuilder(left, constant);

        if (hasValueExpression == null)
            return comparison;

        return Expression.AndAlso(hasValueExpression, comparison);
    }

    private static Expression? BuildStringExpression(
        Expression propertyExpression,
        PropertyInfo property,
        string? value,
        string methodName,
        bool negate)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        if (property.PropertyType != typeof(string))
            return null;

        BinaryExpression notNull = Expression.NotEqual(
            propertyExpression,
            Expression.Constant(null, typeof(string)));

        MethodInfo? method = typeof(string).GetMethod(methodName, new[] { typeof(string) });
        if (method == null)
            return null;

        MethodCallExpression call = Expression.Call(
            propertyExpression,
            method,
            Expression.Constant(value));

        BinaryExpression body = Expression.AndAlso(notNull, call);

        return negate
            ? Expression.Not(body)
            : body;
    }

    private static Expression? BuildInExpression(
        Expression propertyExpression,
        PropertyInfo property,
        List<string>? values,
        bool negate)
    {
        if (values == null || values.Count == 0)
            return null;

        List<string> realValues = values
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Select(v => v.Trim())
            .ToList();

        if (realValues.Count == 0)
            return null;

        Type targetType = GetNonNullableType(property.PropertyType);

        List<object> convertedValues = new();

        foreach (string value in realValues)
        {
            object? convertedValue = ConvertValue(value, targetType);
            if (convertedValue != null)
                convertedValues.Add(convertedValue);
        }

        if (convertedValues.Count == 0)
            return null;

        Type listType = typeof(List<>).MakeGenericType(targetType);
        object? typedList = Activator.CreateInstance(listType);

        MethodInfo? addMethod = listType.GetMethod("Add");
        if (typedList == null || addMethod == null)
            return null;

        foreach (object item in convertedValues)
        {
            _ = addMethod.Invoke(typedList, new[] { item });
        }

        Expression left = BuildNullableSafeLeftExpression(propertyExpression, property.PropertyType, out Expression? hasValueExpression);

        MethodInfo? containsMethod = listType.GetMethod("Contains", new[] { targetType });
        if (containsMethod == null)
            return null;

        MethodCallExpression contains = Expression.Call(
            Expression.Constant(typedList),
            containsMethod,
            left);

        Expression body = negate
            ? Expression.Not(contains)
            : contains;

        if (hasValueExpression != null)
        {
            body = Expression.AndAlso(hasValueExpression, body);
        }

        return body;
    }

    private static Expression BuildNullExpression(
        Expression propertyExpression,
        bool isNull)
    {
        ConstantExpression nullConstant = Expression.Constant(null, propertyExpression.Type);

        return isNull
            ? Expression.Equal(propertyExpression, nullConstant)
            : Expression.NotEqual(propertyExpression, nullConstant);
    }

    private static Expression? BuildBetweenExpression(
        Expression propertyExpression,
        PropertyInfo property,
        string? minValue,
        string? maxValue,
        bool negate)
    {
        if (string.IsNullOrWhiteSpace(minValue) || string.IsNullOrWhiteSpace(maxValue))
            return null;

        Type targetType = GetNonNullableType(property.PropertyType);

        object? convertedMinValue = ConvertValue(minValue, targetType);
        object? convertedMaxValue = ConvertValue(maxValue, targetType);

        if (convertedMinValue == null || convertedMaxValue == null)
            return null;

        Expression left = BuildNullableSafeLeftExpression(propertyExpression, property.PropertyType, out Expression? hasValueExpression);

        ConstantExpression minConstant = Expression.Constant(convertedMinValue, targetType);
        ConstantExpression maxConstant = Expression.Constant(convertedMaxValue, targetType);

        BinaryExpression greaterThanOrEqual = Expression.GreaterThanOrEqual(left, minConstant);
        BinaryExpression lessThanOrEqual = Expression.LessThanOrEqual(left, maxConstant);

        Expression between = Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);

        if (negate)
        {
            between = Expression.Not(between);
        }

        if (hasValueExpression != null)
        {
            between = Expression.AndAlso(hasValueExpression, between);
        }

        return between;
    }

    private static Expression BuildNullableSafeLeftExpression(
        Expression propertyExpression,
        Type propertyType,
        out Expression? hasValueExpression)
    {
        Type? underlyingType = Nullable.GetUnderlyingType(propertyType);

        if (underlyingType == null)
        {
            hasValueExpression = null;
            return propertyExpression;
        }

        hasValueExpression = Expression.Property(propertyExpression, nameof(Nullable<int>.HasValue));
        return Expression.Property(propertyExpression, nameof(Nullable<int>.Value));
    }

    private static PropertyInfo? GetProperty(Type type, string field)
    {
        return type.GetProperty(
            field,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
    }

    private static Type GetNonNullableType(Type type)
    {
        return Nullable.GetUnderlyingType(type) ?? type;
    }

    private static object? ConvertValue(string value, Type targetType)
    {
        try
        {
            if (targetType == typeof(string))
                return value;

            if (targetType == typeof(int))
                return int.Parse(value, CultureInfo.InvariantCulture);

            if (targetType == typeof(long))
                return long.Parse(value, CultureInfo.InvariantCulture);

            if (targetType == typeof(decimal))
                return decimal.Parse(value, CultureInfo.InvariantCulture);

            if (targetType == typeof(double))
                return double.Parse(value, CultureInfo.InvariantCulture);

            if (targetType == typeof(float))
                return float.Parse(value, CultureInfo.InvariantCulture);

            if (targetType == typeof(bool))
                return bool.Parse(value);

            if (targetType == typeof(DateTime))
                return DateTime.Parse(value, CultureInfo.InvariantCulture);

            if (targetType.IsEnum)
            {
                if (int.TryParse(value, out int enumNumber))
                    return Enum.ToObject(targetType, enumNumber);

                return Enum.Parse(targetType, value, ignoreCase: true);
            }

            if (targetType == typeof(Guid))
                return Guid.Parse(value);

            return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
        }
        catch
        {
            return null;
        }
    }
}