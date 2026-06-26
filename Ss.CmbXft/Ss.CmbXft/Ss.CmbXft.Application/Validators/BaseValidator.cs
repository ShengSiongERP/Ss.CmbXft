using FluentValidation;
using Ss.CmbXft.Common.Models.Request;

namespace Ss.CmbXft.Application.Validators;

/// <summary>
/// FluentValidation 标记接口（用于服务注册时的类型过滤）
/// </summary>
public interface IFluentValidation
{
}

/// <summary>
/// 验证器基类：提供通用验证规则与便捷方法
/// </summary>
/// <typeparam name="T">要验证的类型</typeparam>
/// <remarks>
/// <para>设计原则:<br/></para>
/// <list type="bullet">
/// <item>用泛型约束替代运行时类型检查,性能更好且 IDE 智能提示完善</item>
/// <item>所有规则方法均可在构造函数中灵活组合,无强制调用顺序</item>
/// <item>内置分页/筛选/排序的通用验证,业务验证器只需声明字段白名单</item>
/// </list>
/// </remarks>
public abstract class BaseValidator<T> : AbstractValidator<T>, IFluentValidation where T : class
{
    /// <summary>
    /// 构造函数：配置级联模式为遇到第一个错误时停止
    /// </summary>
    protected BaseValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
    }

    #region 基础类型验证

    /// <summary>
    /// 验证字符串不为空且不包含空白字符
    /// </summary>
    protected IRuleBuilderOptions<T, string> NotNullOrWhiteSpace(
        IRuleBuilder<T, string> ruleBuilder,
        string errorMessage = "该字段不能为空或只包含空白字符")
    {
        return ruleBuilder
            .NotEmpty().WithMessage(errorMessage)
            .Must(value => !string.IsNullOrWhiteSpace(value)).WithMessage(errorMessage);
    }

    /// <summary>
    /// 验证字符串长度
    /// </summary>
    protected IRuleBuilderOptions<T, string> LengthBetween(
        IRuleBuilder<T, string> ruleBuilder,
        int min,
        int max,
        string? errorMessage = null)
    {
        string message = errorMessage ?? $"长度必须在{min}到{max}个字符之间";
        return ruleBuilder
            .MinimumLength(min).WithMessage(message)
            .MaximumLength(max).WithMessage(message);
    }

    /// <summary>
    /// 验证数值范围
    /// </summary>
    protected IRuleBuilderOptions<T, TProperty> Range<TProperty>(
        IRuleBuilder<T, TProperty> ruleBuilder,
        TProperty min,
        TProperty max,
        string? errorMessage = null)
        where TProperty : IComparable<TProperty>, IComparable
    {
        string message = errorMessage ?? $"必须在{min}到{max}之间";
        return ruleBuilder.InclusiveBetween(min, max).WithMessage(message);
    }

    /// <summary>
    /// 最小值校验（大于等于 min）
    /// </summary>
    protected IRuleBuilderOptions<T, TProp> Min<TProp>(
        IRuleBuilderInitial<T, TProp> ruleBuilder,
        TProp minValue,
        string? message = null)
        where TProp : IComparable<TProp>, IComparable
    {
        string msg = message ?? $"不能小于 {minValue}";
        return ruleBuilder.GreaterThanOrEqualTo(minValue).WithMessage(msg);
    }

    /// <summary>
    /// 最大值校验（小于等于 max）
    /// </summary>
    protected IRuleBuilderOptions<T, TProp> Max<TProp>(
        IRuleBuilderInitial<T, TProp> ruleBuilder,
        TProp maxValue,
        string? message = null)
        where TProp : IComparable<TProp>, IComparable
    {
        string msg = message ?? $"不能大于 {maxValue}";
        return ruleBuilder.LessThanOrEqualTo(maxValue).WithMessage(msg);
    }

    /// <summary>
    /// 验证枚举值
    /// </summary>
    protected IRuleBuilderOptions<T, TEnum> IsValidEnum<TEnum>(
        IRuleBuilder<T, TEnum> ruleBuilder,
        string? errorMessage = null)
        where TEnum : struct, Enum
    {
        string message = errorMessage ?? $"枚举值无效";
        return ruleBuilder
            .Must(value => Enum.IsDefined(typeof(TEnum), value))
            .WithMessage(message);
    }

    #endregion

    #region 分页验证

    /// <summary>
    /// 分页参数通用校验（仅当 T 实现 <see cref="IPageable"/> 时启用）
    /// </summary>
    /// <remarks>
    /// <para>泛型约束确保编译时类型安全,无需运行时检查。</para>
    /// <para>用法:在构造函数中调用 <c>RuleForPagination();</c>,自动验证 PageIndex &gt; 0 与 PageSize ∈ [1, 1000]。</para>
    /// </remarks>
    protected void RuleForPagination<TRequest>(int maxPageSize = 1000)
        where TRequest : class, IPageable
    {
        // 只有当 T 就是 TRequest 时才添加规则（避免重复）
        if (typeof(T) != typeof(TRequest))
            return;

        RuleFor(x => ((IPageable)x).PageIndex)
            .GreaterThan(0).WithMessage("页码必须大于 0");

        RuleFor(x => ((IPageable)x).PageSize)
            .InclusiveBetween(1, maxPageSize).WithMessage($"每页条数必须在 1~{maxPageSize} 之间");
    }

    #endregion

    #region 筛选与排序验证

    /// <summary>
    /// 筛选条件白名单校验（仅当 T 实现 <see cref="IFilterable"/> 时启用）
    /// </summary>
    /// <param name="allowedFields">允许筛选的字段白名单</param>
    /// <remarks>
    /// <para>FluentValidation 会验证每个 <see cref="FilterItem"/> 的 Field 是否在白名单内。</para>
    /// <para>配合 <see cref="QueryBuilder.BuildPredicate{TEntity}"/> 使用,保证前后端字段安全一致。</para>
    /// </remarks>
    protected void RuleForFilters<TRequest>(params string[] allowedFields)
        where TRequest : class, IFilterable
    {
        if (typeof(T) != typeof(TRequest) || allowedFields.Length == 0)
            return;

        RuleFor(x => ((IFilterable)x).Filters)
            .Must(filters =>
            {
                if (filters is null or { Count: 0 })
                    return true;

                var allowed = new HashSet<string>(allowedFields, StringComparer.OrdinalIgnoreCase);
                return filters.All(f => !string.IsNullOrWhiteSpace(f.Field) && allowed.Contains(f.Field));
            })
            .WithMessage($"筛选字段只允许: {string.Join("、", allowedFields)}");
    }

    /// <summary>
    /// 排序条件白名单校验（仅当 T 实现 <see cref="IFilterable"/> 时启用）
    /// </summary>
    /// <param name="allowedFields">允许排序的字段白名单</param>
    /// <remarks>
    /// <para>FluentValidation 会验证每个 <see cref="SortItem"/> 的 Field 是否在白名单内。</para>
    /// <para>配合 <see cref="QueryBuilder.BuildSorting"/> 使用,保证前后端字段安全一致。</para>
    /// </remarks>
    protected void RuleForSorts<TRequest>(params string[] allowedFields)
        where TRequest : class, IFilterable
    {
        if (typeof(T) != typeof(TRequest) || allowedFields.Length == 0)
            return;

        RuleFor(x => ((IFilterable)x).Sorts)
            .Must(sorts =>
            {
                if (sorts is null or { Count: 0 })
                    return true;

                var allowed = new HashSet<string>(allowedFields, StringComparer.OrdinalIgnoreCase);
                return sorts.All(s => !string.IsNullOrWhiteSpace(s.Field) && allowed.Contains(s.Field));
            })
            .WithMessage($"排序字段只允许: {string.Join("、", allowedFields)}");
    }

    #endregion
}