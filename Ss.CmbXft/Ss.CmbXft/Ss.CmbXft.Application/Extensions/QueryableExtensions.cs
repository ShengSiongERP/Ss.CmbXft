using System.Linq.Expressions;

namespace Ss.CmbXft.Application.Extensions;

/// <summary>
/// IQueryable 扩展方法，简化条件查询构建
/// </summary>
public static class QueryableExtensions
{
    #region WhereIf

    /// <summary>
    /// 当条件为 true 时，追加 Where 过滤
    /// </summary>
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    /// <summary>
    /// 当条件为 true 时，追加 Where 过滤（IEnumerable 版本）
    /// </summary>
    public static IEnumerable<T> WhereIf<T>(
        this IEnumerable<T> source,
        bool condition,
        Func<T, bool> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    #endregion

    #region WhereIfNotEmpty / WhereIfNotWhiteSpace

    /// <summary>
    /// 当字符串非 null 且非空时，追加 Where 过滤
    /// 适用于精确匹配或自定义匹配逻辑
    /// </summary>
    public static IQueryable<T> WhereIfNotEmpty<T>(
        this IQueryable<T> source,
        string? value,
        Expression<Func<T, bool>> predicate)
    {
        return !string.IsNullOrEmpty(value) ? source.Where(predicate) : source;
    }

    /// <summary>
    /// 当字符串非 null 且非空白时，追加 Where 过滤
    /// 适用于精确匹配或自定义匹配逻辑
    /// </summary>
    public static IQueryable<T> WhereIfNotWhiteSpace<T>(
        this IQueryable<T> source,
        string? value,
        Expression<Func<T, bool>> predicate)
    {
        return !string.IsNullOrWhiteSpace(value) ? source.Where(predicate) : source;
    }

    #endregion

    #region WhereIfNotNull

    /// <summary>
    /// 当值非 null 时，追加 Where 过滤
    /// 适用于可空值类型（int?、bool?、DateTime? 等）
    /// </summary>
    public static IQueryable<T> WhereIfNotNull<T, TValue>(
        this IQueryable<T> source,
        TValue? value,
        Expression<Func<T, bool>> predicate)
        where TValue : struct
    {
        return value.HasValue ? source.Where(predicate) : source;
    }

    /// <summary>
    /// 当引用类型非 null 时，追加 Where 过滤
    /// </summary>
    public static IQueryable<T> WhereIfNotNull<T>(
        this IQueryable<T> source,
        object? value,
        Expression<Func<T, bool>> predicate)
    {
        return value is not null ? source.Where(predicate) : source;
    }

    #endregion

    #region WhereContains

    /// <summary>
    /// 当关键字非空白时，追加 Contains 过滤（模糊匹配）
    /// </summary>
    public static IQueryable<T> WhereContains<T>(
        this IQueryable<T> source,
        string? keyword,
        Expression<Func<T, string>> propertySelector)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return source;

        // 构造 Contains 表达式: property.Contains(keyword)
        var parameter = propertySelector.Parameters[0];
        var containsMethod = typeof(string).GetMethod(nameof(string.Contains), [typeof(string)])!;
        var body = Expression.Call(propertySelector.Body, containsMethod, Expression.Constant(keyword));
        var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

        return source.Where(lambda);
    }

    /// <summary>
    /// 当关键字非空白时，追加 Contains 过滤（模糊匹配，忽略大小写）
    /// </summary>
    public static IQueryable<T> WhereContainsIgnoreCase<T>(
        this IQueryable<T> source,
        string? keyword,
        Expression<Func<T, string>> propertySelector)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return source;

        var parameter = propertySelector.Parameters[0];
        var toLowerMethod = typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!;
        var containsMethod = typeof(string).GetMethod(nameof(string.Contains), [typeof(string)])!;

        var toLowerBody = Expression.Call(propertySelector.Body, toLowerMethod);
        var body = Expression.Call(toLowerBody, containsMethod, Expression.Constant(keyword.ToLower()));
        var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

        return source.Where(lambda);
    }

    #endregion

    #region WhereIn

    /// <summary>
    /// 当集合非 null 且有元素时，追加 Contains 过滤（IN 查询）
    /// 示例：query.WhereIn(codes, p => p.ProductCode)
    /// </summary>
    public static IQueryable<T> WhereIn<T, TValue>(
        this IQueryable<T> source,
        IEnumerable<TValue>? values,
        Expression<Func<T, TValue>> propertySelector)
    {
        if (values is null)
            return source;

        var list = values as IList<TValue> ?? values.ToList();
        if (list.Count == 0)
            return source;

        // 构造 Contains 表达式: values.Contains(property)
        var parameter = propertySelector.Parameters[0];
        var containsMethod = typeof(Enumerable)
            .GetMethods()
            .First(m => m.Name == nameof(Enumerable.Contains) && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(TValue));

        var body = Expression.Call(
            containsMethod,
            Expression.Constant(list, typeof(IEnumerable<TValue>)),
            propertySelector.Body);
        var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);

        return source.Where(lambda);
    }

    /// <summary>
    /// 当逗号分隔字符串非空白时，按分隔符拆分后追加 Contains 过滤
    /// 示例：query.WhereInSplit("A,B,C", p => p.Code)
    /// </summary>
    public static IQueryable<T> WhereInSplit<T>(
        this IQueryable<T> source,
        string? commaSeparatedValues,
        Expression<Func<T, string>> propertySelector)
    {
        if (string.IsNullOrWhiteSpace(commaSeparatedValues))
            return source;

        var values = commaSeparatedValues.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return source.WhereIn(values, propertySelector);
    }

    #endregion

    #region OrderByIf

    /// <summary>
    /// 当条件为 true 时，追加升序排序
    /// </summary>
    public static IOrderedQueryable<T> OrderByIf<T, TKey>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, TKey>> keySelector)
    {
        return condition ? source.OrderBy(keySelector) : source.OrderBy(x => 0);
    }

    /// <summary>
    /// 当条件为 true 时，追加降序排序
    /// </summary>
    public static IOrderedQueryable<T> OrderByDescendingIf<T, TKey>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, TKey>> keySelector)
    {
        return condition ? source.OrderByDescending(keySelector) : source.OrderBy(x => 0);
    }

    /// <summary>
    /// 根据排序方向条件，自动选择升序或降序
    /// </summary>
    public static IOrderedQueryable<T> OrderBy<T, TKey>(
        this IQueryable<T> source,
        Expression<Func<T, TKey>> keySelector,
        bool descending)
    {
        return descending ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
    }

    #endregion

    #region Page

    /// <summary>
    /// 分页查询（Skip + Take）
    /// pageIndex 从 1 开始
    /// </summary>
    public static IQueryable<T> Page<T>(
        this IQueryable<T> source,
        int pageIndex,
        int pageSize)
    {
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;

        return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
    }

    #endregion

    #region WhereIfHasValue (集合版)

    /// <summary>
    /// 当集合非 null 且有元素时，追加 Where 过滤
    /// </summary>
    public static IQueryable<T> WhereIfAny<T, TValue>(
        this IQueryable<T> source,
        IEnumerable<TValue>? values,
        Expression<Func<T, bool>> predicate)
    {
        if (values is null) return source;
        var list = values as IList<TValue> ?? values.ToList();
        return list.Count > 0 ? source.Where(predicate) : source;
    }

    #endregion

    #region Conditional (链式条件构建器)

    /// <summary>
    /// 链式条件构建器，支持多条件组合
    /// 用法：query.Conditional(q => q.Where(...), condition1).Conditional(q => q.Where(...), condition2)
    /// </summary>
    public static IQueryable<T> Conditional<T>(
        this IQueryable<T> source,
        Func<IQueryable<T>, IQueryable<T>> apply,
        bool condition)
    {
        return condition ? apply(source) : source;
    }

    #endregion
}
//扩展方法	用途	典型场景
//WhereIf(condition, predicate) 	        条件为 true 时追加 Where	query.WhereIf(status.HasValue, p => p.Status == status.Value)
//WhereIfNotEmpty(value, predicate)	        字符串非空时追加 Where	    query.WhereIfNotEmpty(name, p => p.Name.Contains(name))
//WhereIfNotWhiteSpace(value, predicate)	字符串非空白时追加 Where	query.WhereIfNotWhiteSpace(keyword, p => p.Title.Contains(keyword))
//WhereIfNotNull(value, predicate)	        可空值非 null 时追加 Where	query.WhereIfNotNull(status, p => p.Status == status.Value)
//WhereContains(keyword, selector)	        关键字 Contains 模糊匹配	query.WhereContains(keyword, p => p.Name)
//WhereContainsIgnoreCase(...)	            忽略大小写的 Contains	    query.WhereContainsIgnoreCase(keyword, p => p.Name)
//WhereIn(values, selector)	                IN 查询（集合包含）	        query.WhereIn(codes, p => p.ProductCode)
//WhereInSplit(csv, selector)	            逗号分隔字符串 IN 查询	    query.WhereInSplit("A,B,C", p => p.Code)
//OrderByIf / OrderByDescendingIf	        条件排序	                query.OrderByIf(needsSort, p => p.CreateTime)
//OrderBy(selector, descending)	            按方向自动升降序	        query.OrderBy(p => p.CreateTime, desc)
//Page(pageIndex, pageSize)	                分页（pageIndex 从 1 开始）	query.Page(1, 20)
//WhereIfAny(values, predicate)	            集合有元素时才过滤	        query.WhereIfAny(ids, p => ids.Contains(p.Id))
//Conditional(apply, condition)	            链式条件构建器	            query.Conditional(q => q.Where(...), cond)